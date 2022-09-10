using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrkMdlxBuilder
{
    class MDLXVIF
    {
        VIF vif;
        DataPack MPHead;
        DataPack UV;
        DataPack Indices;
        DataPack Flags;
        DataPack VerticesColor;
        DataPack Vertices;
        DataPack SkinTable;

        public void PackElements()
        {
            if (!this.Shadow)
            {
                this.UV.Pack();
                this.UV.Offset = this.Shadow ? 0x30 : 0x40;
                this.UV.Unpack(this.vif, 4, 0);
            }

            this.Indices.Pack();
            this.Indices.Offset = this.Shadow ? 0x30 : 0x40;
            if (this.Shadow)
                this.Indices.Unpack(this.vif, 2, 0);
            else
                this.Indices.Unpack(this.vif, 4, 2);


            this.Flags.Pack();
            this.Flags.Offset = this.Shadow ? 0x30 : 0x40;
            if (this.Shadow)
                this.Flags.Unpack(this.vif, 2, 1);
            else
                this.Flags.Unpack(this.vif, 4, 3);

            if (VertexColor)
            {
                this.VerticesColor.Pack();
                this.VerticesColor.Offset = this.vif.VUCursor;
                this.VerticesColor.Unpack(this.vif, 4, 0);
            }

            this.Vertices.Pack();
            this.Vertices.Offset = this.vif.VUCursor;
            this.Vertices.Unpack(this.vif, 4, 0);

            while (this.skinning.Count % 4 > 0)
                this.skinning.Insert(this.skinning.Count, 0);

            for (int i = 0; i < this.skinning.Count; i += 4)
                this.SkinTable.InsertElements(BitConverter.GetBytes(this.skinning[i]), BitConverter.GetBytes(this.skinning[i + 1]), BitConverter.GetBytes(this.skinning[i + 2]), BitConverter.GetBytes(this.skinning[i + 3]));

            this.SkinTable.Pack();
            this.SkinTable.Offset = this.vif.VUCursor;
            this.SkinTable.Unpack(this.vif, 4, 0);
            //here
            this.DMAOffset = this.vif.VUCursor / 0x10;

            this.MPHead.InsertElements(BitConverter.GetBytes(this.Shadow ? 2 : 1), BitConverter.GetBytes(0), BitConverter.GetBytes(0), BitConverter.GetBytes(0));
            this.MPHead.InsertElements(BitConverter.GetBytes(this.Indices.Count), BitConverter.GetBytes(this.Indices.Offset / 0x10), BitConverter.GetBytes(this.SkinTable.Offset / 0x10), BitConverter.GetBytes(this.DMAOffset));
            if (!this.Shadow)
                this.MPHead.InsertElements(BitConverter.GetBytes(this.VerticesColor.Count), BitConverter.GetBytes(this.VerticesColor.Offset / 0x10), BitConverter.GetBytes(0), BitConverter.GetBytes(0));
            this.MPHead.InsertElements(BitConverter.GetBytes(this.Vertices.Count), BitConverter.GetBytes(this.Vertices.Offset / 0x10), BitConverter.GetBytes(0), BitConverter.GetBytes(this.skinningCount));


            this.MPHead.Pack();
            this.MPHead.Offset = 0;
            this.vif.VUCursor = 0;
            this.MPHead.Unpack(this.vif, 4, 0);

            this.vif.AppendPack(this.MPHead);
            this.vif.AppendPack(this.UV);
            this.vif.AppendPack(this.Indices);
            this.vif.AppendPack(this.Flags);
            if (this.VertexColor)
                this.vif.AppendPack(this.VerticesColor);
            this.vif.AppendPack(this.Vertices);
            this.vif.AppendPack(this.SkinTable);
            this.vif.Pack();
            //System.IO.File.WriteAllBytes(@"VIF_Packet.vif", this.vif.Packet);
        }

        public int DMAOffset_estimation
        {
            get
            {
                int size = 0;

                if (!this.Shadow)
                {
                    size = this.Shadow ? 0x30 : 0x40;
                    size += this.UV.Count * 4 * 4;
                }

                size = this.Shadow ? 0x30 : 0x40;
                if (this.Shadow)
                    size += this.Indices.Count * 4 * 2;
                else
                    size += this.Indices.Count * 4 * 4;

                size = VIF.Helper.Align(size, 16);

                size = this.Shadow ? 0x30 : 0x40;
                if (this.Shadow)
                    size += this.Flags.Count * 4 * 2;
                else
                    size += this.Flags.Count * 4 * 4;

                size = VIF.Helper.Align(size, 16);


                if (VertexColor)
                {
                    size += this.VerticesColor.Count * 4 * 4;
                }

                size += this.Vertices.Count * 4 * 4;

                int skinCount = this.skinning.Count;

                while (skinCount % 4 > 0)
                    skinCount++;

                size += skinCount * 4;

                return size/16;
            }
        }

        public byte[] Packet
        { get { return this.vif.Packet; } }

        public bool Shadow
        { get; }

        public bool VertexColor
        { get; }

        public bool Quaternion
        { get; }

        public int DMAOffset
        { get; set; }

        public MDLXVIF(bool shadowMode, bool enableVertexColor, bool quaternion)
        {
            this.vif = new VIF();
            this.Shadow = shadowMode;
            this.VertexColor = enableVertexColor;
            this.Quaternion = quaternion;

            this.MPHead = new DataPack(0x01000101);
            this.MPHead.Offset = 0;
            this.MPHead.VN = 3; /* 4 elements per array */
            this.MPHead.V1 = 0; /* 32 bit elements */

            this.UV = new DataPack(0x01000101);
            this.UV.VN = 1; /* 2 elements per array */
            this.UV.V1 = 1; /* 16 bit elements */
            this.UV.Masked = false;

            this.Indices = new DataPack(0x01000101);
            this.Indices.VN = 0; /* 1 element per array */
            this.Indices.V1 = 2; /* 8 bit elements */
            this.Indices.Masked = true;
            this.Indices.Signed = false;
            this.Indices.AppendMask(new Mask(0x20000000, new byte[] { 0xCF }));

            this.Flags = new DataPack(0x01000101);
            this.Flags.VN = 0; /* 1 element per array */
            this.Flags.V1 = 2; /* 8 bit elements */
            this.Flags.Masked = true;
            this.Flags.Signed = false;
            this.Flags.AppendMask(new Mask(0x20000000, new byte[] { 0x3F }));

            this.VerticesColor = new DataPack(0x01000101);
            this.VerticesColor.VN = 3; /* 4 elements per array */
            this.VerticesColor.V1 = 2; /* 8 bit elements */
            this.VerticesColor.Signed = false;

            this.Vertices = new DataPack(0x01000101);
            if (quaternion)
                this.Vertices.VN = 3; /* 4 elements per array */
            else
            {
                this.Vertices.VN = 2; /* 3 elements per array */
                this.Vertices.AppendMask(new Mask(0x31000000, BitConverter.GetBytes(1f)));
                this.Vertices.AppendMask(new Mask(0x20000000, new byte[] { 0x80 }));
            }
            this.Vertices.V1 = 0; /* 32 bit elements */
            this.Vertices.Masked = true;
            this.Vertices.Signed = true;

            this.skinning = new List<int>(0);
            this.SkinTable = new DataPack(0x01000101);
            this.SkinTable.VN = 3; /* 4 elements per array */
            this.SkinTable.V1 = 0; /* 32 bit elements */
        }

        public void InsertUV(float U, float V)
        {
            if (this.UV.Signed)
                this.UV.InsertElements(BitConverter.GetBytes((short)(4095 * U)), BitConverter.GetBytes((short)(4095 * V)));
            else
                this.UV.InsertElements(BitConverter.GetBytes((ushort)(4095 * U)), BitConverter.GetBytes((ushort)(4095 * V)));
        }

        public void InsertIndex(int index)
        {
            this.Indices.InsertElements(new byte[] { (byte)index });
        }

        public void InsertFlag(int index)
        {
            this.Flags.InsertElements(new byte[] { (byte)index });
        }

        public void InsertColor(byte r, byte g, byte b, byte a)
        {
            this.VerticesColor.InsertElements(new byte[] { r }, new byte[] { g }, new byte[] { b }, new byte[] { a });
        }

        public void InsertVertex(float x, float y, float z)
        {
            this.Vertices.InsertElements(BitConverter.GetBytes(x), BitConverter.GetBytes(y), BitConverter.GetBytes(z));
        }

        public void InsertVertex(float x, float y, float z, float w)
        {
            this.Vertices.InsertElements(BitConverter.GetBytes(x), BitConverter.GetBytes(y), BitConverter.GetBytes(z), BitConverter.GetBytes(w));
        }

        List<int> skinning;
        int skinningCount;
        public void InsertSkin(int matrix)
        {
            this.skinning.Insert(this.skinning.Count, matrix);
            skinningCount++;
        }
    }

    class VIF
    {
        public static class Helper
        {
            public static int Align(int val, int byteAlign)
            {
                while (val % byteAlign > 0)
                    val++;
                return val;
            }
        }

        public byte[] VUMemory;
        public byte[] Packet;
        List<DataPack> dataPacks; 

        public VIF()
        {
            this.VUMemory = new byte[16 * 1024]; /* 16KB */
            this.Packet = new byte[0];
            this.dataPacks = new List<DataPack>(0);
            this.VUCursor = 0;
        }

        public void AppendPack(DataPack pack)
        {
            this.dataPacks.Insert(this.dataPacks.Count, pack);
        }

        public int Count
        {
            get {return this.dataPacks.Count;}
        }

        public int VUCursor
        {
            get;set;
        }

        public void Pack()
        {
            int length = 0;
            for (int i=0;i<this.dataPacks.Count;i++)
                length += this.dataPacks[i].Bytes.Length;
            length = VIF.Helper.Align(length, 16);
            this.Packet = new byte[length];
            length = 0;
            for (int i = 0; i < this.dataPacks.Count; i++)
            {
                Array.Copy(this.dataPacks[i].Bytes, 0, this.Packet, length, this.dataPacks[i].Bytes.Length);
                length += this.dataPacks[i].Bytes.Length;
            }
            System.IO.File.WriteAllBytes("VUMem.bin",this.VUMemory);
        }
    }

    class Mask
    {
        public byte[] Bytes;
        
        public Mask(uint command, byte[] pattern)
        {
            this.Bytes = new byte[4 + pattern.Length * 4];
            byte[] commandBytes = BitConverter.GetBytes(command);
            for (int i = 0; i < 4; i++)
                this.Bytes[i] = commandBytes[i];

            for (int i = 0; i < pattern.Length; i++)
            {
                this.Bytes[4 + pattern.Length * 0 + i] = pattern[i];
                this.Bytes[4 + pattern.Length * 1 + i] = pattern[i];
                this.Bytes[4 + pattern.Length * 2 + i] = pattern[i];
                this.Bytes[4 + pattern.Length * 3 + i] = pattern[i];
            }
        }
    }

    class DataPack
    {
        uint cycleCommand;
        byte VUOffset;
        byte usn; /* "Signed or not" data containing byte. */
        public byte unpackCommand;

        public DataPack(uint cycle_command, uint command)
        {
            this.cycleCommand = cycle_command;
            this.VUOffset = (byte)(command % 0x100);
            this.usn = (byte)((command % 0x10000) / 0x100);
            //this.Count = (byte)((command % 0x1000000) / 0x10000);
            this.unpackCommand = (byte)(command / 0x01000000);
            this.Arrays = new List<byte[][]>(0);
            this.Bytes = new byte[0];
            this.Masks = new List<Mask>(0);
            this.MasksOffset = 0;
        }

        public DataPack(uint cycle_command)
        {
            this.cycleCommand = cycle_command;
            this.VUOffset = 0;
            this.usn = 0x80;
            this.unpackCommand = 0x6C;
            this.Arrays = new List<byte[][]>(0);
            this.Bytes = new byte[0];
            this.Masks = new List<Mask>(0);
            this.MasksOffset = 0;
        }

        List<byte[][]> Arrays = new List<byte[][]>(0);

        public int ElementSize
        {
            get { return new int[] { 32, 16, 8, 16 }[this.V1]; }
        }

        public int ArraySize
        {
            get { return this.ElementSize * this.ElementDimension; }
        }


        public int ElementDimension
        {
            get { return this.VN + 1; }
        }

        public void InsertElements(byte[] val1, byte[] val2, byte[] val3, byte[] val4)
        {
            if (this.Packed)
                throw new Exception("The pack has been packed already and is now locked.");

            if (val1.Length > 4)
                throw new Exception("These elements are bigger than the V1 maximum data size.");
            int inputLength = new int[] { -1, 2, 1, -1, 0 }[val1.Length];

            if (inputLength != this.V1)
                throw new Exception("This array contains " + (val1.Length * 8) + "bits elements while the V1 asks for " + this.ElementSize + "bits elements.");

            if (this.VN != 3)
                throw new Exception("You are trying to enter 4 elements while the VN asks for " + this.ElementDimension + " elements arrays.");

            if (Arrays.Count == 255)
                throw new Exception("You can not enter any new array in this Pack. Maximum reached.");
            Arrays.Insert(Arrays.Count, new byte[][] { val1, val2, val3, val4 });
        }

        public void InsertElements(byte[] val1, byte[] val2, byte[] val3)
        {
            if (this.Packed)
                throw new Exception("The pack has been packed already and is now locked.");

            if (val1.Length > 4)
                throw new Exception("These elements are bigger than the V1 maximum data size.");
            int inputLength = new int[] { -1, 2, 1, -1, 0 }[val1.Length];

            if (inputLength != this.V1)
                throw new Exception("This array contains " + (val1.Length * 8) + "bits elements while the V1 asks for " + this.ElementSize + "bits elements.");

            if (this.VN != 2)
                throw new Exception("You are trying to enter 3 elements while the VN asks for " + this.ElementDimension + " elements arrays.");

            if (Arrays.Count == 255)
                throw new Exception("You can not enter any new array in this Pack. Maximum reached.");
            Arrays.Insert(Arrays.Count, new byte[][] { val1, val2, val3});
        }

        public void InsertElements(byte[] val1, byte[] val2)
        {
            if (this.Packed)
                throw new Exception("The pack has been packed already and is now locked.");

            if (val1.Length > 4)
                throw new Exception("These elements are bigger than the V1 maximum data size.");
            int inputLength = new int[] { -1, 2, 1, -1, 0 }[val1.Length];

            if (inputLength != this.V1)
                throw new Exception("This array contains " + (val1.Length * 8) + "bits elements while the V1 asks for " + this.ElementSize + "bits elements.");

            if (this.VN != 1)
                throw new Exception("You are trying to enter 2 elements while the VN asks for " + this.ElementDimension + " elements arrays.");

            if (Arrays.Count == 255)
                throw new Exception("You can not enter any new array in this Pack. Maximum reached.");
            Arrays.Insert(Arrays.Count, new byte[][] { val1, val2 });
        }

        public void InsertElements(byte[] val1)
        {
            if (this.Packed)
                throw new Exception("The pack has been packed already and is now locked.");

            if (val1.Length > 4)
                throw new Exception("These elements are bigger than the V1 maximum data size.");

            int inputLength = new int[] { -1, 2, 1, -1, 0 }[val1.Length];

            if (inputLength != this.V1)
                throw new Exception("This array contains " + (val1.Length * 8) + "bits elements while the V1 asks for " + this.ElementSize + "bits elements.");
            if (this.VN != 0)
                throw new Exception("You are trying to enter 1 element while the VN asks for " + this.ElementDimension + " elements arrays.");

            if (Arrays.Count==255)
                throw new Exception("You can not enter any new array in this Pack. Maximum reached.");
            Arrays.Insert(Arrays.Count, new byte[][] { val1 });
        }

        public byte[] Bytes { get; set; }

        public void Pack()
        {
            if (this.Packed)
                throw new Exception("The pack has been packed already and is now locked.");

            if (this.Arrays.Count != this.Count)
                throw new Exception("You have entered less elements than the pack is supposed to contain. Unable to Pack.");

            int v1Length = new int[] { 4, 2, 1, 2 }[this.V1];
            int packLength = 8 + this.Count * v1Length * (this.VN + 1);
            packLength = VIF.Helper.Align(packLength, 4);
            
            byte[] output = new byte[this.MasksOffset + packLength];

            if (this.Masks.Count > 0)
            {
                int off = 0;
                for (int i = 0; i < this.Masks.Count; i++)
                {
                    Array.Copy(this.Masks[i].Bytes, 0, output, off, this.Masks[i].Bytes.Length);
                    off += this.Masks[i].Bytes.Length;
                }
            }

            byte[] cycle = BitConverter.GetBytes(this.cycleCommand);
            for (int i = 0; i < 4; i++) output[this.MasksOffset + i] = cycle[i];
            output[this.MasksOffset + 4] = this.VUOffset;
            output[this.MasksOffset + 5] = this.usn;
            output[this.MasksOffset + 6] = (byte)this.Count;
            output[this.MasksOffset + 7] = this.unpackCommand;
            int cursor = 8;
            for (int i = 0; i < this.Count; i++)
            {
                for (int v = 0; v < this.Arrays[i].Length; v++)
                {
                    for (int j = 0; j < this.Arrays[i][v].Length; j++)
                    {
                        output[this.MasksOffset + cursor + j] = this.Arrays[i][v][j];
                    }
                    cursor += this.Arrays[i][v].Length;
                }
            }
            this.packed = true;
            this.Bytes = output;
        }
        public List<Mask> Masks;
        public int MasksOffset;

        public void AppendMask(Mask mask)
        {
            this.Masks.Insert(this.Masks.Count, mask);
            this.MasksOffset += mask.Bytes.Length;
        }

        public bool Packed { get { return this.packed; } }
        bool packed;


        /* Unpack method is convenient to get the VU cursor at correct position and then get the next DataPack offset in a VIF. */
        public void Unpack(VIF vif, int ReservedBlocks, int InBlockIndex)
        {
            if (!this.Packed)
                throw new Exception("You must pack the data before to unpack it.");
            
            vif.VUCursor = this.Offset;
            for (int i = 0; i < this.Count; i++)
            {
                for (int v = 0; v < this.Arrays[i].Length; v++)
                {
                    for (int j = 0; j < this.Arrays[i][v].Length; j++)
                    {
                        vif.VUMemory[vif.VUCursor + j + (v+ InBlockIndex) * 4] = this.Arrays[i][v][j];
                    }
                }
                vif.VUCursor += ReservedBlocks * 4;
            }
            vif.VUCursor = VIF.Helper.Align(vif.VUCursor,16);
        }
        
        public int Count
        {
            get { return this.Arrays.Count; }
        }

        public int Offset
        {
            get
            {
                return this.VUOffset * 0x10;
            }
            set
            {
                this.VUOffset = (byte)(value / 0x10);
                if (this.Packed)
                    this.Bytes[this.MasksOffset + 4] = this.VUOffset;
            }
        }

        public bool Signed
        {
            get
            {
                return (usn % 0x80) / 0x40 == 0;
            }
            set
            {

                if (value ^ this.Signed)
                {
                    if (this.Signed)
                        usn += 0x40;
                    else
                        usn -= 0x40;
                }
                if (this.Packed)
                    this.Bytes[this.MasksOffset + 5] = this.usn;
            }
        }


        public bool Interrupted
        {
            get
            {
                return this.unpackCommand / 0x80 > 0;
            }
            set
            {
                if (value ^ this.Interrupted)
                {
                    if (this.Interrupted)
                        this.unpackCommand -= 0x80;
                    else
                        this.unpackCommand += 0x80;

                    if (this.Packed)
                        this.Bytes[this.MasksOffset + 7] = this.unpackCommand;
                }
            }
        }

        public bool Masked
        {
            get
            {
                return (this.unpackCommand % 0x20) / 0x10 > 0;
            }
            set
            {
                if (value ^ this.Masked)
                {
                    if (this.Masked)
                        this.unpackCommand -= 0x10;
                    else
                        this.unpackCommand += 0x10;
                    if (this.Packed)
                        this.Bytes[this.MasksOffset + 7] = this.unpackCommand;
                }

            }
        }

        public int VN
        {
            get
            {
                return (this.unpackCommand % 0x10) / 4;
            }
            set
            {
                if (this.Arrays.Count > 0)
                    throw new Exception("You can not edit this command when elements has been entered already.");

                int leftOnly = (this.unpackCommand / 0x10) * 0x10;
                this.unpackCommand = (byte)(leftOnly + this.V1 + (value % 4) * 4);

                if (this.Packed)
                    this.Bytes[this.MasksOffset + 7] = this.unpackCommand;
            }
        }

        public int V1
        {
            get
            {
                return this.unpackCommand % 4;
            }
            set
            {
                if (this.Arrays.Count > 0)
                    throw new Exception("You can not edit this command when elements has been entered already.");

                this.unpackCommand = (byte)((this.unpackCommand / 0x04) * 0x04 + (value % 4));

                if (this.Packed)
                    this.Bytes[this.MasksOffset + 7] = this.unpackCommand;
            }
        }

    }
}
