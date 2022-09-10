using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrkMdlxBuilder
{
    class Mesh
    {
        //------------SAME--------------

        List<SlimDX.Vector3> OutputVertices;
        List<int> OutputVerticesID;
        List<List<int>> Matrices;
        List<List<float>> Influences;
        //------------SAME--------------
        List<SlimDX.Vector2> UV;
        List<int> Indices;
        List<byte[]> Colors;
        int dmaAddress = 0;

        List<byte[]> VIFs = new List<byte[]>(0);
        List<byte[]> DMAs = new List<byte[]>(0);
        byte[] MATIS = new byte[4];

        public void Clear()
        {
            this.VIFs.Clear();
            this.DMAs.Clear();
            this.MATIS = new byte[4];

            this.OutputVertices.Clear();
            this.OutputVerticesID.Clear();
            this.Matrices.Clear();
            this.Influences.Clear();
            this.UV.Clear();
            this.Indices.Clear();
            this.Colors.Clear();
        }

        public int Seuil
        {
            get;set;
        }

        public Mesh()
        {
            this.OutputVertices = new List<SlimDX.Vector3>(0);
            this.OutputVerticesID = new List<int>(0);
            this.Colors = new List<byte[]>(0);
            this.Matrices = new List<List<int>>(0);
            this.Influences = new List<List<float>>(0);
            this.UV = new List<SlimDX.Vector2>(0);
            this.Indices = new List<int>(0);
            this.Shadow = Form1.checkBox4Checked;
            this.Seuil = Form1.maxVU;
        }
        
        public void AddVertex(SlimDX.Vector3 vert, int indexID)
        {
            int index = this.OutputVerticesID.IndexOf(indexID);

            if (index>-1)
            {
                this.Indices.Add(index);
            }
            else
            {
                this.Indices.Add(this.OutputVerticesID.Count);
                this.OutputVertices.Add(vert);
                this.OutputVerticesID.Add(indexID);

                this.Matrices.Add(new List<int>(0));
                this.Influences.Add(new List<float>(0));
            }
        }

        public bool Shadow { get; set;}
        
        public Bone[] ComputedBones;
        public Bone[] Bones;
        public static int maxInfGlobal = 0;

        public void Update(bool finir)
        {
            List<int> indBuffer = new List<int>(0);
            List<SlimDX.Vector2> UVsBuffer = new List<SlimDX.Vector2>(0);
            List<byte> flags = new List<byte>(0);
            List<int> indices = new List<int>(0);
            List<SlimDX.Vector2> UVs = new List<SlimDX.Vector2>(0);
            List<byte[]> colors = new List<byte[]>(0);
            List<byte[]> colorsBuffer = new List<byte[]>(0);

            List<List<float>> influences = new List<List<float>>(0);
            List<List<int>> matrices = new List<List<int>>(0);

            List<SlimDX.Vector3> vertices = new List<SlimDX.Vector3>(0);

            int lastRegistredMat = 0;

            for (int i = 0; i < this.OutputVertices.Count; i++)
            {
                influences.Add(new List<float>(0));
                matrices.Add(new List<int>(0));
                vertices.Add(this.OutputVertices[i]);
                for (int j = 0; j < this.Influences[i].Count; j++)
                {
                    lastRegistredMat = this.Matrices[i][j];
                    matrices.Last().Add(this.Matrices[i][j]);
                    influences.Last().Add(this.Influences[i][j]);
                }
                if (influences.Last().Count==0)
                {
                    matrices.Last().Add(lastRegistredMat);
                    influences.Last().Add(1);
                    /*matrices.RemoveAt(matrices.Count-1);
                    influences.RemoveAt(influences.Count-1);
                    vertices.RemoveAt(vertices.Count-1);*/
                }
                if (this.Shadow)
                {
                    int max = 0;
                    for (int j = 0; j < influences[i].Count; j++)
                    {
                        if (influences[i][j] > influences[i][max])
                            max = j;
                    }
                    int mat = matrices[i][max];
                    influences[i].Clear();
                    matrices[i].Clear();

                    influences[i].Add(1f);
                    matrices[i].Add(mat);
                }
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                float somme = 0;
                for (int j = 0; j < influences[i].Count; j++)
                {
                    somme += influences[i][j];
                }

                if (somme < 1)
                {
                    float partage = (1 - somme) / (float)influences[i].Count;
                    for (int j = 0; j < influences[i].Count; j++)
                    {
                        influences[i][j] += partage;
                    }
                }
            }
            

            for (int i=0;i<this.Indices.Count;i++)
            {
                int ind = this.Indices[i];
                SlimDX.Vector2 uv =this.UV.Count>0? this.UV[i] : SlimDX.Vector2.Zero;
                byte[] col = i< this.Colors.Count ? this.Colors[i]: new byte[] {128,128,128,128};

                indBuffer.Insert(0, ind);
                UVsBuffer.Insert(0, uv);
                colorsBuffer.Insert(0, col);

                if (indBuffer.Count==3)
                {
                    if (!Form1.checkBox1Checked|| indices.Count == 0)
                    {
                        flags.Add(0x10);
                        flags.Add(0x10);
                        flags.Add(0x20);
                        indices.Add(indBuffer[2]);
                        indices.Add(indBuffer[1]);
                        indices.Add(indBuffer[0]);
                        UVs.Add(UVsBuffer[2]);
                        UVs.Add(UVsBuffer[1]);
                        UVs.Add(UVsBuffer[0]);
                        colors.Add(colorsBuffer[2]);
                        colors.Add(colorsBuffer[1]);
                        colors.Add(colorsBuffer[0]);
                    }
                    else
                    {
                        if (indices[indices.Count - 2] == indBuffer[0] &&
                            indices[indices.Count - 1] == indBuffer[1])
                        {
                            indices.Add(indBuffer[2]);
                            flags.Add(0x30);
                            UVs.Add(UVsBuffer[2]);
                            colors.Add(colorsBuffer[2]);
                        }
                        else
                        if (indices[indices.Count - 2] == indBuffer[2] &&
                            indices[indices.Count - 1] == indBuffer[1])
                        {
                            indices.Add(indBuffer[0]);
                            flags.Add(0x20);
                            UVs.Add(UVsBuffer[0]);
                            colors.Add(colorsBuffer[0]);
                        }
                        else
                        {
                            indices.Add(indBuffer[2]);
                            indices.Add(indBuffer[1]);
                            indices.Add(indBuffer[0]);
                            flags.Add(0x10);
                            flags.Add(0x10);
                            flags.Add(0x20);
                            UVs.Add(UVsBuffer[2]);
                            UVs.Add(UVsBuffer[1]);
                            UVs.Add(UVsBuffer[0]);
                            colors.Add(colorsBuffer[2]);
                            colors.Add(colorsBuffer[1]);
                            colors.Add(colorsBuffer[0]);
                        }
                    }
                    indBuffer.Clear();
                }
            }
            /*str += "\r\n";
            str += "\r\n";
            for (int i = 0; i < indices.Count; i++)
            {
                str += indices[i].ToString("d2") + "|";
            }
            str += "\r\n";
            for (int i = 0; i < flags.Count; i++)
            {
                str += flags[i].ToString("X2") + "|";
            }
            str += "\r\n";
            str += "UV count:"+this.UV.Count+"\r\n";
            str += "flags count:"+flags.Count+ "\r\n";
            str += "indices count:" + indices.Count+ "\r\n";
            System.IO.File.WriteAllText(@"D:\Documents\Visual Studio 2017\Projects\SrkMdlxBuilder\SrkMdlxBuilder\bin\Debug\Dae sample\data.txt", str);*/

            SrkBinary UVBinary = new SrkBinary(new byte[this.Shadow ? 0 : (8 + UVs.Count*4)]);
            if (!this.Shadow)
            {
                UVBinary.WriteInt(0, 0x01000101, false);
                UVBinary.WriteInt(4, 0x65008000 + 0x00010000 * UVs.Count, false);

                for (int i=0;i< UVs.Count;i++)
                {
                    UVBinary.WriteInt(8 + i * 4, (short)(UVs[i].X * 4095), false);
                    UVBinary.WriteInt(8 + i * 4 +2, (short)(UVs[i].Y * 4095), false);
                }
            }


            SrkBinary avantIndicesMask = new SrkBinary(new byte[8]);
            avantIndicesMask.WriteInt(0,0x20000000,false);
            avantIndicesMask.WriteInt(4, 0xCFCFCFCF, false);

            /* INDICIES AND FLAGS  4  ALIGNED  */

            int multiple4 = indices.Count;
            while (multiple4 % 4 > 0)
                multiple4++;

            SrkBinary IndicesBinary = new SrkBinary(new byte[8+ multiple4]);
            IndicesBinary.WriteInt(0, 0x01000101, false);
            IndicesBinary.WriteInt(4, 0x7200C000 + 0x00010000 * indices.Count, false);

            for (int i = 0; i < indices.Count; i++)
            {
                IndicesBinary.Buffer[8 + i] = (byte)indices[i];
            }
            

            SrkBinary avantFlagsMask = new SrkBinary(new byte[8]);
            avantFlagsMask.WriteInt(0, 0x20000000, false);
            avantFlagsMask.WriteInt(4, 0x3F3F3F3F, false);

            SrkBinary FlagsBinary = new SrkBinary(new byte[8 + multiple4]);
            FlagsBinary.WriteInt(0, 0x01000101, false);
            FlagsBinary.WriteInt(4, 0x7200C000 + 0x00010000 * indices.Count, false);

            for (int i = 0; i < flags.Count; i++)
            {
                FlagsBinary.Buffer[8 + i] = (byte)flags[i];
            }


            bool color = false;

            for (int i=0;i<colors.Count;i++)
            {
                if (colors[i][0] != 80 ||
                    colors[i][1] != 80 ||
                    colors[i][2] != 80 ||
                    colors[i][3] != 80)
                {
                    color = Form1.checkBox3Checked;
                    break;
                }
            }

            SrkBinary ColorsBinary = new SrkBinary(new byte[color ? (8 + colors.Count * 4) : 0]);
            
            if (color)
            {
                ColorsBinary.WriteInt(0, 0x01000101, false);
                ColorsBinary.WriteInt(4, 0x6E00C000 + 0x00010000 * colors.Count, false);
                for (int i=0;i< colors.Count;i++)
                {
                    Array.Copy(colors[i], 0, ColorsBinary.Buffer, 8 + i * 4, 4);
                }
            }

            bool quaternion = false;
            int verticesCountWithInfs = 0;
            

            while (false)
            {
                int maxInfluence = 0;
                for (int i = 0; i < influences.Count; i++)
                {
                    if (influences[i].Count> maxInfluence)
                        maxInfluence = influences[i].Count;
                }
                int isthereCount = 0;
                for (int i=0;i< maxInfluence;i++)
                {
                    bool isThereI = false;
                    for (int j = 0; j < influences.Count; j++)
                    {
                        if (influences[j].Count == (i+1))
                        {
                            isthereCount++;
                            isThereI = true;
                            break;
                        }
                    }
                    if (!isThereI)
                    {
                        for (int j = 0; j < influences.Count; j++)
                        {
                            if (influences[j].Count == (i + 2))
                            {
                                int smallestInfInd = 0;
                                float somme = 0;
                                for (int k = 0; k < influences[j].Count; k++)
                                {
                                    if (influences[j][k] < influences[j][smallestInfInd])
                                    {
                                        smallestInfInd = k;
                                    }
                                    somme += influences[j][k];
                                }
                                somme = influences[j][smallestInfInd] / (float)influences[j].Count;

                                influences[j].RemoveAt(smallestInfInd);
                                matrices[j].RemoveAt(smallestInfInd);
                                

                                for (int k = 0; k < influences[j].Count; k++)
                                {
                                    influences[j][k]+= somme;
                                }

                                break;
                            }
                        }
                    }
                }
                if (isthereCount==maxInfluence)
                {
                    break;
                }
                else
                {
                    if (maxInfluence >3)
                    continue;
                }
            }


            for (int i = 0; i < influences.Count; i++)
            {
                verticesCountWithInfs += influences[i].Count;
            }

            quaternion = verticesCountWithInfs > vertices.Count;
            

            SrkBinary beforeVerticesMask = new SrkBinary(new byte[quaternion ? 0: (0x1C)]);
            if (!quaternion)
            {
                beforeVerticesMask.WriteInt(0,0x31000000,false);
                beforeVerticesMask.WriteFloat(4,1,false);
                beforeVerticesMask.WriteFloat(8,1,false);
                beforeVerticesMask.WriteFloat(0x0C, 1, false);
                beforeVerticesMask.WriteFloat(0x10, 1, false);
                beforeVerticesMask.WriteInt(0x14, 0x20000000, false);
                beforeVerticesMask.WriteInt(0x18, 0x80808080, false);
            }

            SrkBinary verticesBinary = new SrkBinary(new byte[8 + (quaternion ? verticesCountWithInfs*16 : vertices.Count*12)]);
            verticesBinary.WriteInt(0, 0x01000101, false);
            if (quaternion)
            {
                verticesBinary.WriteInt(4, 0x6C008000 + 0x00010000 * verticesCountWithInfs, false);
            }
            else
            {
                verticesBinary.WriteInt(4, 0x78008000 + 0x00010000 * vertices.Count, false);
            }

            List<int> skins = new List<int>(0);
            
            List<int> substitusInfCount = new List<int>(0);

            List<int> orderedIndexes = new List<int>(0);

            List<List<int>> influencesPath = new List<List<int>>(0);
            
            for (int i = 0; i < influences.Count; i++)
            {
                while (influencesPath.Count< influences[i].Count)
                {
                    influencesPath.Add(new List<int>(0));
                }
                substitusInfCount.Add(influences[i].Count);
            }
            /* boucle infinie possible*/
            
            while (substitusInfCount.Count>0)
            {
                int min = 5000;
                int smallestIndex = -1;
                for (int i=0;i< substitusInfCount.Count;i++)
                {
                    if (substitusInfCount[i]< min)
                    {
                        smallestIndex = i;
                        min = substitusInfCount[i];
                    }
                }
                if (smallestIndex<0)
                    break;
                orderedIndexes.Add(smallestIndex);
                substitusInfCount[smallestIndex] = 5000;
                influencesPath[min-1].Add(smallestIndex);
                //Console.WriteLine(matrices[smallestIndex].Count);
            }
            //Console.WriteLine("");


            if (quaternion)
            {
                for (int i = 0; i < indices.Count; i++)
                {
                    int toSeek = IndicesBinary.Buffer[8 + i];
                    for (int j = 0; j < orderedIndexes.Count; j++)
                    {
                        if (orderedIndexes[j] == toSeek)
                        {
                            IndicesBinary.Buffer[8 + i] = (byte)j;
                        }
                    }
                }
                int pos = 0;
                
                for (int i = 0; i < vertices.Count; i++)
                {
                    List<int> smallerSorted = new List<int>(0);
                    bool[] alreadyDone = new bool[influences[i].Count];

                    int smallestIndex = 0;

                    for (int k = 0; k < influences[i].Count; k++)
                    {
                        for (int j = 0; j < influences[i].Count; j++)
                        {
                            if (alreadyDone[j] == false && influences[i][j] > influences[i][smallestIndex])
                            {
                                smallestIndex = j;
                            }
                        }
                        alreadyDone[smallestIndex] = true;
                        smallerSorted.Add(smallestIndex);
                    }


                    for (int j = 0; j < influences[i].Count; j++)
                    {
                        int ind = j;// smallerSorted[j];


                        int mat = matrices[i][ind];
                        skins.Add(mat);

                        SlimDX.Vector3 v3 = new SlimDX.Vector3(
                            vertices[i].X,
                            vertices[i].Y,
                            vertices[i].Z);

                        SlimDX.Vector4 v4 = new SlimDX.Vector4(v3.X, v3.Y, v3.Z, 1) * influences[i][ind];


                        v4 = SlimDX.Vector4.Transform(v4, SlimDX.Matrix.Invert(this.ComputedBones[mat].Matrice));
                        
                        

                        verticesBinary.WriteFloat(8 + pos, v4.X * Form1.scale/*this.ComputedBones[mat].ScaleX*/, false);
                        verticesBinary.WriteFloat(8 + 4 + pos, v4.Y * Form1.scale/*this.ComputedBones[mat].ScaleY*/, false);
                        verticesBinary.WriteFloat(8 + 8 + pos, v4.Z * Form1.scale/*this.ComputedBones[mat].ScaleZ*/, false);

                        verticesBinary.WriteFloat(8 + 12 + pos, influences[i][ind], false);
                        pos += 16;
                    }
                }
            }
            else
            {
                for (int i = 0; i < vertices.Count; i++)
                {
                    int mat = matrices[i][0];
                    skins.Add(mat);

                    SlimDX.Vector4 v4 = new SlimDX.Vector4(
                        vertices[i].X,
                        vertices[i].Y,
                        vertices[i].Z, 1);

                    v4 = SlimDX.Vector4.Transform(v4, SlimDX.Matrix.Invert(this.ComputedBones[mat].Matrice));

                    verticesBinary.WriteFloat(8 + i * 12, v4.X * Form1.scale/* this.ComputedBones[mat].ScaleX*/, false);
                    verticesBinary.WriteFloat(8 + 4 + i * 12, v4.Y * Form1.scale /* this.ComputedBones[mat].ScaleY*/, false);
                    verticesBinary.WriteFloat(8 + 8 + i * 12, v4.Z * Form1.scale  /*this.ComputedBones[mat].ScaleZ*/, false);
                }
            }
            //skins


            List<int> skinsMergedCount = new List<int>(0);
            List<int> skinsMerged = new List<int>(0);
            int lastSkin = -1;
            for (int i = 0; i < skins.Count; i++)
            {
                int skin = skins[i];
                if (lastSkin!=skin)
                {
                    skinsMergedCount.Add(1);
                    skinsMerged.Add(skin);
                }
                else
                {
                    skinsMergedCount[skinsMergedCount.Count - 1]++;
                }
                lastSkin = skin;
            }


            SrkBinary skinsBinary = new SrkBinary(new byte[0x1000]);
            skinsBinary.WriteInt(0, 0x01000101, false);

            int position = 0;
            for (int i = 0; i < skinsMergedCount.Count; i++)
            {
                skinsBinary.WriteInt(8 + position, skinsMergedCount[i], false);
                position += 4;
            }

            while (position % 16 > 0)
                position++;

            int maxInf = 0;

            if (quaternion)
            {
                maxInf = influencesPath.Count;
                for (int i=0;i< influencesPath.Count; i++)
                {
                    skinsBinary.Buffer[8 + position] = (byte)influencesPath[i].Count;
                    position+=4;
                }
                while (position % 16 > 0)
                    position++;

                for (int i = 0; i < influencesPath.Count; i++)
                {
                    for (int j = 0; j < influencesPath[i].Count; j++)
                    {
                        int outputPos = 0;
                        for (int k=0;k< influencesPath[i][j];k++)
                        {
                            outputPos += influences[k].Count;
                        }
                        for (int k = 0; k < (i + 1); k++)
                        {
                            skinsBinary.WriteInt(8 + position, outputPos, false);
                            position += 4;
                            outputPos++;
                        }
                    }

                    while (position % 16 > 0)
                        position++;
                }
            }

            byte[] buff = new byte[position+8];

            Array.Copy(skinsBinary.Buffer, 0, buff, 0, buff.Length);
            skinsBinary = new SrkBinary(buff);

            skinsBinary.WriteInt(4, 0x6C008000 + 0x00010000 * (position/16), false);

            SrkBinary MPHeader = new SrkBinary(new byte[this.Shadow ? 0x38 : 0x48]);

            MPHeader.WriteInt(0, 0x01000101, false);
            MPHeader.WriteInt(4, 0x6C008000 + 0x00010000 * (MPHeader.Buffer.Length / 0x10), false);

            int indicesCount = indices.Count; /*  +uv  */

            int verticesCount = 0;
            if (quaternion)
            {
                verticesCount = verticesCountWithInfs;
            }
            else
            {
                verticesCount = vertices.Count;
            }
            int cntBox1 = skinsMerged.Count;


            int colorsCount = 0;

            if (color)
            {
                colorsCount = this.Colors.Count;
            }






            int indicesOffset = 0; /*  +uv  */
            int verticesOffset = 0;

            int colorsOffset = 0;
            int box1Offset = 0;
            int dmaOffset = 0;

            if (this.Shadow)
                MPHeader.WriteInt(8, 2, false);
            else
                MPHeader.WriteInt(8, 1, false);


            if (this.Shadow)
                indicesOffset = 0x30;
            else
                indicesOffset = 0x40;

            if (color)
            {
                colorsOffset = indicesOffset + indicesCount * 16;
                verticesOffset = colorsOffset + colorsCount * 16;
            }
            else
            {
                verticesOffset = indicesOffset + indicesCount * 16;
            }

            box1Offset = verticesOffset + verticesCount * 16;

            dmaOffset = box1Offset + position;


            MPHeader.WriteInt(8 + 0x10, indicesCount, false);
            MPHeader.WriteInt(8 + 0x14, indicesOffset / 0x10, false);
            MPHeader.WriteInt(8 + 0x18, box1Offset / 0x10, false);
            MPHeader.WriteInt(8 + 0x1C, dmaOffset / 0x10, false);

            int offSpecHeader = quaternion ?(box1Offset + cntBox1 * 4) : 0;

            while (offSpecHeader % 16 > 0)
                offSpecHeader++;

            int offSpec = quaternion ? (offSpecHeader + maxInf*4) : 0;

            while (offSpec % 16 > 0)
                offSpec++;

            if (this.Shadow)
            {
                MPHeader.WriteInt(8, 2, false);

                MPHeader.WriteInt(8 + 0x20, verticesCount, false);
                MPHeader.WriteInt(8 + 0x24, verticesOffset / 0x10, false);
                MPHeader.WriteInt(8 + 0x28, 0 / 0x10, false);
                MPHeader.WriteInt(8 + 0x2C, cntBox1, false);
            }
            else
            {
                MPHeader.WriteInt(8, 1, false);
                MPHeader.WriteInt(8 + 0x20, colorsCount, false);
                MPHeader.WriteInt(8 + 0x24, colorsOffset / 0x10, false);
                MPHeader.WriteInt(8 + 0x28, maxInf, false);
                MPHeader.WriteInt(8 + 0x2C, offSpecHeader / 0x10 , false);

                MPHeader.WriteInt(8, 1, false);
                MPHeader.WriteInt(8 + 0x30, verticesCount, false);
                MPHeader.WriteInt(8 + 0x34, verticesOffset / 0x10, false);
                MPHeader.WriteInt(8 + 0x38, offSpec / 0x10, false);
                MPHeader.WriteInt(8 + 0x3C, cntBox1, false);
            }

            IndicesBinary.Buffer[4] = (byte)(indicesOffset / 0x10);
            FlagsBinary.Buffer[4] = (byte)(indicesOffset / 0x10);

            if (!this.Shadow)
                UVBinary.Buffer[4] = (byte)(indicesOffset / 0x10);

            if (color)
                ColorsBinary.Buffer[4] = (byte)(colorsOffset / 0x10);
            verticesBinary.Buffer[4] = (byte)(verticesOffset / 0x10);
            skinsBinary.Buffer[4] = (byte)(box1Offset / 0x10);

            byte[] allTogether = MPHeader.Buffer;
            if (!this.Shadow)
            allTogether = Form1.Combine(allTogether, UVBinary.Buffer);

            
            allTogether = Form1.Combine(allTogether, avantIndicesMask.Buffer);
            allTogether = Form1.Combine(allTogether, IndicesBinary.Buffer);


            allTogether = Form1.Combine(allTogether, avantFlagsMask.Buffer);
            allTogether = Form1.Combine(allTogether, FlagsBinary.Buffer);

            if (color)
                allTogether = Form1.Combine(allTogether, ColorsBinary.Buffer);

            allTogether = Form1.Combine(allTogether, beforeVerticesMask.Buffer);

            allTogether = Form1.Combine(allTogether, verticesBinary.Buffer);
            allTogether = Form1.Combine(allTogether, skinsBinary.Buffer);
            while (allTogether.Length % 16 > 0)
                allTogether = Form1.Combine(allTogether, new byte[4]);

            if (
                (indices.Count > 0 && 
                cntBox1>0 && 
                vertices.Count > 0 &&

                (dmaOffset + cntBox1 * 0x40 >= this.Seuil||

                indices.Count > 230 ||
                cntBox1 > 230 ||
                vertices.Count > 230 ||
                colors.Count > 230)
                
                ) || finir)
            {
                VIFs.Add(allTogether);
                SrkBinary dma = new SrkBinary(new byte[32+ cntBox1*16]);
                dma.WriteInt(0, (allTogether.Length / 16) + 0x30000000, false);

                dma.WriteInt(4, dmaAddress, false);
                dmaAddress += allTogether.Length;
                int count = BitConverter.ToInt32(MATIS, 0);

                for (int i=0;i< cntBox1;i++)
                {
                    count++;
                    dma.WriteInt(16 + i * 16, 0x30000004,false);
                    dma.WriteInt(16 + 4 + i * 16, skinsMerged[i],false);
                    dma.WriteInt(16 + 8 + i * 16, 0x01000101, false);
                    dma.WriteInt(16 + 12 + i * 16, 0x6C048000 + (dmaOffset/0x10)+4*i, false);

                    MATIS = Form1.Combine(MATIS, BitConverter.GetBytes(skinsMerged[i]));
                }
                MATIS = Form1.Combine(MATIS, new byte[] { 255, 255, 255, 255 });
                count++;
                byte[] countB = BitConverter.GetBytes(count);

                MATIS[0] = countB[0];
                MATIS[1] = countB[1];
                MATIS[2] = countB[2];
                MATIS[3] = countB[3];

                dma.WriteInt(dma.Buffer.Length-16, 0x10000000, false);
                dma.WriteInt(dma.Buffer.Length-16 + 8, 0x17000000, false);
                DMAs.Add(dma.Buffer);
                
                this.OutputVertices.Clear();
                this.OutputVerticesID.Clear();
                this.Matrices.Clear();
                this.Influences.Clear();
                this.UV.Clear();
                this.Indices.Clear();
                this.Colors.Clear();
            }

            /*if (quaternion)
            {
                System.IO.File.WriteAllBytes(@"D:\Documents\Visual Studio 2017\Projects\SrkMdlxBuilder\SrkMdlxBuilder\bin\Debug\Dae sample\test.bin", allTogether);
                Console.WriteLine("");
            }*/

            //Form1.SubArray


            /*indicesOffset
            box1Offset
            matricesOffset
                verticesOffset
                colorsOffset*/
            //skinsBinary.WriteInt(4, 0x6C008000 + 0x00010000 * indices.Count, false);

        }
        

        public byte[] GetBytes()
        {
            byte[] output = new byte[0x20];
             
            for (int i = 0; i < this.VIFs.Count; i++)
            {
                output = Form1.Combine(output, this.VIFs[i]);
            }
            int dmasSize = 0;
            for (int i = 0; i < this.DMAs.Count; i++)
            {
                output = Form1.Combine(output, this.DMAs[i]);
                dmasSize += this.DMAs[i].Length;
            }
            output = Form1.Combine(output, MATIS);
            output[output.Length - 4] = 0;
            output[output.Length - 3] = 0;
            output[output.Length - 2] = 0;
            output[output.Length - 1] = 0;
            while (output.Length % 16 > 0)
            {
                output = Form1.Combine(output, new byte[4]);
            }

            SrkBinary bnr = new SrkBinary(output);
            bnr.WriteInt(0, 3, false);
            bnr.WriteInt(0x10, dmaAddress, false);
            bnr.WriteInt(0x14, dmaAddress+ dmasSize, false);
            bnr.WriteInt(0x18, dmasSize/0x10, false);

            return bnr.Buffer;
        }

        

        public void AddInfluence(int mat, float inf)
        {
            for (int i = 0; i < this.Matrices[this.Indices.Last()].Count; i++)
            {
                if (this.Matrices[this.Indices.Last()][i] == mat)
                    return;
            }

            float somme = inf;
            for (int i = 0; i < this.Influences[this.Indices.Last()].Count; i++)
            {
                somme += this.Influences[this.Indices.Last()][i];
            }
            if (somme>1)
                return;

            this.Matrices[this.Indices.Last()].Add(mat);
            this.Influences[this.Indices.Last()].Add(inf);
        }

        public void AddColor(byte[] color)
        {
            this.Colors.Add(color);
        }
        public bool stopUv = false;

        public void AddUV(SlimDX.Vector2 uv)
        {
            if (this.Shadow)
                return;
            this.UV.Add(uv);
        }
    }
}
