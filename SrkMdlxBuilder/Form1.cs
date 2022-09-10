using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace SrkMdlxBuilder
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            /*string[] models = Directory.GetFiles(@"D:\Jeux\Kingdom Hearts\app_KH2Tools\export\obj\","*.mdlx");

            int maxMeshCount = 0;
            int maxIndicesCount = 0;
            int maxVerticesCount = 0;
            int maxColorsCount = 0;
            int maxInfluencesCount = 0;
            int maxMatricesCount = 0;

            int maxOffMatrices = 0;

            int moyMeshCount = 0;
            int moyIndicesCount = 0;
            int moyVerticesCount = 0;
            int moyColorsCount = 0;
            int moyInfluencesCount = 0;
            int moyMatricesCount = 0;

            uint modelsVerified = 0;

            for (int i=0;i<models.Length;i++)
            {
                Console.WriteLine(i+"/"+ models.Length);
                if (SrkBinary.GetBytes(models[i]))
                {
                    SrkBinary binary = new SrkBinary();
                    int max = binary.ReadInt(4, false);
                    bool isTheretype4 = false;
                    for (int j=0;j< max;j++)
                    {
                        if (binary.ReadInt(0x10+j*0x10, false)==4)
                        {
                            int address = binary.ReadInt(0x18 + j * 0x10, false);
                            int length = binary.ReadInt(0x1C + j * 0x10, false);
                            binary = new SrkBinary(SubArray(binary.Buffer,address,length));
                            isTheretype4 = true;
                            break;
                        }
                    }
                    if (isTheretype4)
                    {
                        int sweep = 0;
                        while (sweep+16< binary.Buffer.Length)
                        {
                            sweep += 16;
                            if (binary.Buffer[sweep]>0)
                            {
                                break;
                            }
                        }
                        binary = new SrkBinary(SubArray(binary.Buffer, sweep, binary.Buffer.Length-sweep));
                        int meshCount = binary.ReadInt(0x1C, false);
                        if (meshCount<1||meshCount > 1000)
                        {
                            continue;
                        }
                        if (meshCount > maxMeshCount)
                            maxMeshCount = meshCount;

                        moyMeshCount += meshCount;

                        modelsVerified++;
                        
                        int moyIndicesCount_ = 0;
                        int moyVerticesCount_ = 0;
                        int moyColorsCount_ = 0;
                        int moyInfluencesCount_ = 0;
                        int moyMatricesCount_ = 0;
                        int vifCount = 0;

                        for (int k=0;k< meshCount;k++)
                        {
                            int dmaAddress = binary.ReadInt(0x30+k*0x20,false);
                            int firstVifAddress = binary.ReadInt(dmaAddress + 4, false)+8;
                            int type = binary.ReadInt(firstVifAddress, false);
                            if (type==1)
                            {
                                vifCount++;
                                int indicesCount = binary.ReadInt(firstVifAddress + 0x10, false);
                                int verticesCount = binary.ReadInt(firstVifAddress + 0x30, false);
                                int colorsCount = binary.ReadInt(firstVifAddress + 0x20, false);
                                int influencesCount = binary.ReadInt(firstVifAddress + 0x28, false);
                                int matricesCount = binary.ReadInt(firstVifAddress + 0x3C, false);

                                int offMatrices = binary.ReadInt(firstVifAddress + 0x1C, false);
                                if (offMatrices + matricesCount * 4 > maxOffMatrices)
                                    maxOffMatrices = offMatrices + matricesCount * 4;

                                if (indicesCount > maxIndicesCount)
                                    maxIndicesCount = indicesCount;

                                if (verticesCount > maxVerticesCount)
                                    maxVerticesCount = verticesCount;

                                if (colorsCount > maxColorsCount)
                                    maxColorsCount = colorsCount;

                                if (influencesCount > maxInfluencesCount)
                                    maxInfluencesCount = influencesCount;

                                if (matricesCount > maxMatricesCount)
                                    maxMatricesCount = matricesCount;


                                moyIndicesCount_ += indicesCount;
                                moyVerticesCount_ += verticesCount;
                                moyColorsCount_ += colorsCount;
                                moyInfluencesCount_ += influencesCount;
                                moyMatricesCount_ += matricesCount;

                            }
                        }

                        moyIndicesCount += (moyIndicesCount_/ vifCount);
                        moyVerticesCount += (moyVerticesCount_/ vifCount);
                        moyColorsCount += (moyColorsCount_/ vifCount);
                        moyInfluencesCount += (moyInfluencesCount_/ vifCount);
                        moyMatricesCount += (moyMatricesCount_ / vifCount);
                    }
                }
                SrkBinary.Flush();
            }
            string output = "Data collected over "+ modelsVerified+ " models.\r\n\r\n" +
                "maxMeshCount: " + maxMeshCount +"\r\n" +
                "maxIndicesCount: "+ maxIndicesCount + "\r\n" +
                "maxVerticesCount: "+ maxVerticesCount + "\r\n" +
                "maxColorsCount: "+ maxColorsCount + "\r\n" +
                "maxInfluencesCount: "+ maxInfluencesCount + "\r\n" +
                "maxMatricesCount: "+ maxMatricesCount + "\r\n\r\n\r\n" +

                "moyMeshCount: " + (moyMeshCount/(float)modelsVerified) + "\r\n" +
                "moyIndicesCount: " + (moyIndicesCount / (float)modelsVerified) + "\r\n" +
                "moyVerticesCount: " + (moyVerticesCount / (float)modelsVerified) + "\r\n" +
                "moyColorsCount: " + (moyColorsCount / (float)modelsVerified) + "\r\n" +
                "moyInfluencesCount: " + (moyInfluencesCount / (float)modelsVerified) + "\r\n" +
                "moyMatricesCount: " + (moyMatricesCount / (float)modelsVerified)+"\r\n\r\n\r\n"+
                maxOffMatrices.ToString("X2");
            File.WriteAllText("output.txt", output);
            */



            string[] param = new string[] { "False","True","False","False","False", "False", "False", "False", "2560", "False", "False", "1.05" };

            
            if (File.Exists("params.ini"))
            {
                string[] spli = File.ReadAllText("params.ini").Split(',');
                int taille = param.Length;
                if (spli.Length < taille)
                    taille = spli.Length;
                Array.Copy(spli,0,param,0, taille);
            }

            checkBox7.Checked = Boolean.Parse(param[0]);
            checkBox1.Checked = Boolean.Parse(param[1]);
            checkBox3.Checked = Boolean.Parse(param[2]);
            checkBox5.Checked = Boolean.Parse(param[3]);
            checkBox6.Checked = Boolean.Parse(param[4]);
            checkBox2.Checked = Boolean.Parse(param[5]);
            checkBox8.Checked = Boolean.Parse(param[6]);
            checkBox9.Checked = Boolean.Parse(param[7]);
            numericUpDown2.Value = int.Parse(param[8]);
            checkBox4.Checked = Boolean.Parse(param[9]);
            checkBox10.Checked = Boolean.Parse(param[10]);
            numericUpDown3.Value = Decimal.Parse(param[11].Replace(".", System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator));

            swapUV = checkBox7.Checked;
            checkBox1Checked = checkBox1.Checked;
            checkBox3Checked = checkBox3.Checked;
            checkBox5Checked = checkBox5.Checked;
            checkBox6Checked = checkBox6.Checked;
            reuseSkeleton = checkBox2.Checked;
            checkBox8Checked = checkBox8.Checked;
            checkBox9Checked = checkBox9.Checked;
            maxVU = (int)numericUpDown2.Value;
            reuseTim = checkBox4.Checked;
            reuseEverythingElse = checkBox10.Checked;


            Stream originalFileStream = (Stream)(new MemoryStream(SrkMdlxBuilder.Properties.Resources.tims_bin));


            /*
            FileStream fileStream = new FileStream("tims_bin.gz", FileMode.Create);
            using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(fileStream, System.IO.Compression.CompressionMode.Compress, true))
            {
                zip.Write(SrkMdlxBuilder.Properties.Resources.tims_bin, 0, SrkMdlxBuilder.Properties.Resources.tims_bin.Length);
            }
            */
            using (MemoryStream decompressedFileStream = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream decompressionStream = new System.IO.Compression.GZipStream(originalFileStream, System.IO.Compression.CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(decompressedFileStream);
                    Tims.Binary = new byte[decompressedFileStream.Length];
                    decompressedFileStream.Position = 0;
                    decompressedFileStream.Read(Tims.Binary,0, Tims.Binary.Length);
                }
            }

            checkBox1_CheckedChanged(null, null);
            checkBox3_CheckedChanged(null, null);
            numericUpDown1_ValueChanged(null, null);

            Tims.Sizes = new Size[Tims.SizesString.Length][];
            for (int i = 0; i < Tims.SizesString.Length; i++)
            {
                List<Size> sizes = new List<Size>(0);
                for (int j = 0; j < Tims.SizesString[i].Length; j += 2)
                {
                    int width = (int)Math.Pow(2, (int)((byte)(Tims.SizesString[i][j]) - 0x30));
                    int height = (int)Math.Pow(2, (int)((byte)(Tims.SizesString[i][j + 1]) - 0x30));
                    sizes.Add(new Size(width, height));
                }
                Tims.Sizes[i] = sizes.ToArray();
            }
            /*int[] wrongTimes = new int[] { 265, 273,276,277 };
            for (int i=0;i< wrongTimes.Length;i++)
            {
                Tims.Sizes[wrongTimes[i]] = Tims.Sizes[0];
                Tims.addresses[wrongTimes[i]] = Tims.addresses[0];
                Tims.Lengthes[wrongTimes[i]] = Tims.Lengthes[0];
                Tims.outputLengthes[wrongTimes[i]] = Tims.outputLengthes[0];
                Tims.SizesString[wrongTimes[i]] = Tims.SizesString[0];
            }*/

            meshesModel = new List<SrkBinary>(0);
            meshesShadow = new List<SrkBinary>(0);
            //args = new string[] { @"D:\Documents\Visual Studio 2017\Projects\SrkMdlxBuilder\SrkMdlxBuilder\bin\Debug\Sora Kairi Riku\Sora\Sora.dae" };
            //args = new string[] { @"D:\Desktop\neku\Neku Sakuraba\last\test.dae" };
            if (args.Length > 0)
            {
                openFile(args[0]);
                saveButton.Enabled = true;
            }

        }

        public static SrkBinary binary;
        public static SrkBinary binarySkeleton;
        public static SrkBinary binarySkeletonDefinitions;

        public static List<SrkBinary> meshesModel;
        public static List<SrkBinary> meshesShadow;

        private void newButton_Click(object sender, EventArgs e)
        {
            openFile("petiteChaineToutePourrie");
            saveButton.Enabled = true;
        }
        public static Bone[] BonesList = new Bone[0];

        //Trim a byte array
        public static byte[] SubArray(byte[] data, long index, long length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        //Combine two bytes array
        public static byte[] Combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        public void UpdateSkel()
        {
            BonesList = new Bone[binarySkeleton.Buffer.Length/0x40];
            for (int j = 0; j < BonesList.Length; j++)
            {
                BonesList[j] = new Bone(binarySkeleton.ReadShort(j * 0x40, false));
                short parentIndex = binarySkeleton.ReadShort(0x04 + j * 0x40 ,false);
                if (parentIndex > -1)
                    BonesList[j].Parent = BonesList[parentIndex];
            }
            Bone.ComputeMatrices(BonesList, BonesList[0]);
            //Bone.DecomputeMatrices(BonesList[140]);
        }

        private void GetBinaries()
        {
            listBox.SelectedIndex = -1;
            binary.Pointer = 0x90;
            int shadowPointer = binary.ReadInt(0x0C, true);
            int skeletonPointer = binary.ReadInt(0x14, true);
            int skeletonDefinitionsPointer = binary.ReadInt(0x18, true);
            short boneCount = binary.ReadShort(0x10, true);
            binarySkeleton = new SrkBinary(new byte[boneCount * 0x40]);
            Array.Copy(binary.Buffer, binary.Pointer + skeletonPointer, binarySkeleton.Buffer, 0, binarySkeleton.Buffer.Length);

            binarySkeletonDefinitions = new SrkBinary(new byte[skeletonPointer - skeletonDefinitionsPointer]);
            Array.Copy(binary.Buffer, binary.Pointer + skeletonDefinitionsPointer, binarySkeletonDefinitions.Buffer, 0, binarySkeletonDefinitions.Buffer.Length);
            UpdateSkel();

            int modelLength;
            int shadowLength;

            int length = (shadowPointer > 0) ? shadowPointer : binary.Buffer.Length-0x90;
            while (length % 16 > 0) length++;
            modelLength = length;
            
            length = (shadowPointer > 0) ? binary.Buffer.Length - 0x90 - shadowPointer : 0;
            while (length % 16 > 0) length++;
            shadowLength = length;
            meshesModel.Clear();
            meshesShadow.Clear();
            
            int meshCount = binary.ReadInt(0x1C, true);
            for (int m = 0; m < meshCount; m++)
            {
                int dmaStart = binary.ReadInt(0x30 + m * 0x20, true);
                int meshStart = binary.ReadInt(dmaStart + 4, true);

                int meshLength = modelLength - meshStart;
                if (shadowPointer > 0)
                    meshLength = shadowPointer - meshStart;

                if (m < meshCount - 1)
                {
                    int nextDmaStart = binary.ReadInt(0x30 + (m + 1) * 0x20, true);
                    int nextMeshStart = binary.ReadInt(nextDmaStart + 4, true);

                    meshLength = nextMeshStart - meshStart;
                }
                int plus = 0;
                while ((meshLength + plus) % 16 > 0)
                    plus++;

                SrkBinary currMesh = new SrkBinary(new byte[meshLength + 0x20+ plus]);

                Array.Copy(binary.Buffer, binary.Pointer + 0x20 + m * 0x20, currMesh.Buffer, 0, 0x20);
                Array.Copy(binary.Buffer, binary.Pointer + meshStart, currMesh.Buffer, 0x20, meshLength);

                int insideDmaOff = currMesh.ReadInt(0x10, false) - meshStart;
                int insideMatiOff = currMesh.ReadInt(0x14, false) - meshStart;
                currMesh.WriteInt(0x10, insideDmaOff, false);
                currMesh.WriteInt(0x14, insideMatiOff, false);
                insideDmaOff += 0x20;
                insideMatiOff += 0x20;

                int firstOff = currMesh.ReadInt(insideDmaOff+4,false);
                currMesh.WriteInt(insideDmaOff + 4, 0, false);
                int matiCount = currMesh.ReadInt(insideMatiOff, false);
                for (int c=0;c< matiCount;c++)
                {
                    insideMatiOff += 4;
                    insideDmaOff += 0x10;
                    if (currMesh.ReadInt(insideMatiOff, false)<0)
                    {
                        insideDmaOff += 0x10;
                        currMesh.WriteInt(insideDmaOff + 4, currMesh.ReadInt(insideDmaOff + 4, false) - firstOff, false);
                    }
                }

                meshesModel.Insert(meshesModel.Count, currMesh);
            }
            
            if (shadowPointer>0)
            {
                binary.Pointer += shadowPointer;
                meshCount = binary.ReadInt(0x1C, true);
                for (int m = 0; m < meshCount; m++)
                {
                    int dmaStart = binary.ReadInt(0x30 + m * 0x20, true);
                    int meshStart = binary.ReadInt(dmaStart + 4, true);

                    int meshLength = binary.Buffer.Length;

                    if (m < meshCount - 1)
                    {
                        int nextDmaStart = binary.ReadInt(0x30 + (m + 1) * 0x20, true);
                        int nextMeshStart = binary.ReadInt(nextDmaStart + 4, true);
                        meshLength = nextMeshStart - meshStart;
                    }
                    else
                        meshLength -= (meshStart + binary.Pointer);
                    
                    SrkBinary currMesh = new SrkBinary(new byte[meshLength + 0x20]);
                    Array.Copy(binary.Buffer, binary.Pointer + 0x20 + m * 0x20, currMesh.Buffer, 0, 0x20);
                    Array.Copy(binary.Buffer, binary.Pointer + meshStart, currMesh.Buffer, 0x20, meshLength);

                    int insideDmaOff = currMesh.ReadInt(0x10, false) - meshStart;
                    int insideMatiOff = currMesh.ReadInt(0x14, false) - meshStart;
                    currMesh.WriteInt(0x10, insideDmaOff, false);
                    currMesh.WriteInt(0x14, insideMatiOff, false);
                    insideDmaOff += 0x20;
                    insideMatiOff += 0x20;

                    int firstOff = currMesh.ReadInt(insideDmaOff + 4, false);
                    currMesh.WriteInt(insideDmaOff + 4, 0, false);
                    int matiCount = currMesh.ReadInt(insideMatiOff, false);
                    for (int c = 0; c < matiCount; c++)
                    {
                        insideMatiOff += 4;
                        insideDmaOff += 0x10;
                        if (currMesh.ReadInt(insideMatiOff, false) < 0)
                        {
                            insideDmaOff += 0x10;
                            currMesh.WriteInt(insideDmaOff + 4, currMesh.ReadInt(insideDmaOff + 4, false) - firstOff, false);
                        }
                    }
                    meshesShadow.Insert(meshesShadow.Count, currMesh);
                }
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                openFile(openFileDialog.FileName);
                if (openFileDialog.FileName.Length > 0)
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                openFileDialog.FileName = Path.GetFileName(openFileDialog.FileName);
            }
        }

        public bool newFile()
        {
            if (binary != null)
            {
                if (binary.DataEdited)
                {
                    if (MessageBox.Show("Changes have not been saved, Are you sure ?", "Changes not saved", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        return true;
                }
                binary.DataEdited = false;
            }
            return false;
        }
        public static bool barMode = false;
        public static List<int> types = new List<int>(0);
        public static List<string> names = new List<string>(0);
        public static List<int> lengthes = new List<int>(0);
        public static List<SrkBinary> binaries = new List<SrkBinary>(0);
        public static int first0x04 = -1;
        public static string[] SkiString = new string[0];

        public void openFile(string fname)
        {
            if (Path.GetExtension(fname).ToLower()==".dae")
            {
                if (Form1.reuseEverythingElse && File.Exists(Path.GetDirectoryName(fname) + @"\" + Path.GetFileNameWithoutExtension(fname) + ".mdlx"))
                {
                    openFile(Path.GetDirectoryName(fname) + @"\" + Path.GetFileNameWithoutExtension(fname) + ".mdlx");
                    if (!Form1.reuseSkeleton)
                    {
                        binarySkeletonDefinitions = new SrkBinary(new byte[0x120]);
                        binarySkeletonDefinitions.WriteFloat(0x00, -1000f, false);
                        binarySkeletonDefinitions.WriteFloat(0x04, -1000f, false);
                        binarySkeletonDefinitions.WriteFloat(0x08, -1000f, false);
                        binarySkeletonDefinitions.WriteFloat(0x0C, 1f, false);
                        binarySkeletonDefinitions.WriteFloat(0x10, 1000f, false);
                        binarySkeletonDefinitions.WriteFloat(0x14, 1000f, false);
                        binarySkeletonDefinitions.WriteFloat(0x18, 1000f, false);
                        binarySkeletonDefinitions.WriteFloat(0x1C, 1f, false);
                        binarySkeletonDefinitions.WriteFloat(0x110, 10f, false);
                        for (int i=0;i<0xE0;i++)
                        {
                            binarySkeletonDefinitions.Buffer[0x30+i] = 255;
                        }
                    }
                }
                else if (binarySkeleton == null || binarySkeleton.Buffer.Length < 0x40)
                {
                    openFile("petiteChaineToutePourrie");
                    meshesShadow.Clear();
                }

                //meshesShadow.Clear();

                if (File.Exists(Path.GetDirectoryName(fname) + @"\" + Path.GetFileNameWithoutExtension(fname) + "-shadow.dae"))
                {
                    var t2 = new DAE(Path.GetDirectoryName(fname) + @"\" + Path.GetFileNameWithoutExtension(fname) + "-shadow.dae");
                    t2.Shadow = true;
                    t2.Parse();
                    t2.MakeMDLX();
                    openFileDialog.FileName = fname;
                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(fname);
                }
                string fnE = Path.GetFileNameWithoutExtension(fname);
                if (File.Exists(Path.GetDirectoryName(fname) + @"\" + fnE+"-skin.dae"))
                {
                    DAE toSkin = new DAE(fname);
                    toSkin.Parse();
                    DAE toReferTo = new DAE(Path.GetDirectoryName(fname) + @"\" + fnE + "-skin.dae");
                    toReferTo.Parse();
                        for (int i = 0; i < toSkin.Triangle.Count; i++)
                        for (int j = 0; j < toSkin.Triangle[i].Count; j ++)
                        {
                            int vIndex = toSkin.Triangle[i][j][toSkin.vertexTriInd[i]];
                            SlimDX.Vector3 pos = toSkin.Vertex[i][vIndex];
                            float closestD = Single.MaxValue;
                            int[] cloestInd = new int[] {0,0};

                            for (int i2 = 0; i2 < toReferTo.Triangle.Count; i2++)
                                for (int j2 = 0; j2 < toReferTo.Triangle[i2].Count; j2++)
                                {
                                    int vIndex2 = toReferTo.Triangle[i2][j2][toReferTo.vertexTriInd[i2]];
                                    SlimDX.Vector3 pos2 = toReferTo.Vertex[i2][vIndex2];
                                    float dist = SlimDX.Vector3.Distance(pos, pos2);
                                    if (dist < closestD)
                                    {
                                        cloestInd[0] = i2;
                                        cloestInd[1] = vIndex2;
                                        closestD = dist;
                                    }
                                }
                            toSkin.InfluencesIndices[i][vIndex].Clear();
                            toSkin.Influences[i][vIndex].Clear();

                            for (int k = 0; k < toReferTo.InfluencesIndices[cloestInd[0]][cloestInd[1]].Count; k++)
                            {
                                toSkin.InfluencesIndices[i][vIndex].Add(toReferTo.InfluencesIndices[cloestInd[0]][cloestInd[1]][k]);
                                toSkin.Influences[i][vIndex].Add(toReferTo.Influences[cloestInd[0]][cloestInd[1]][k]);
                            }
                            toSkin.Bones = toReferTo.Bones;
                            toSkin.ComputedBones = toReferTo.ComputedBones;
                        }
                    toSkin.MakeMDLX();
                    openFileDialog.FileName = fname;
                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(fname);
                }
                else
                {
                    var t = new DAE(fname);
                    t.Parse();

                    t.MakeMDLX();
                    openFileDialog.FileName = fname;
                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(fname);
                }

                saveButton_Click(Path.GetDirectoryName(fname)+@"\"+Path.GetFileNameWithoutExtension(fname) + "-fromDAE.mdlx", null);
                //System.Diagnostics.Process.Start(Path.GetDirectoryName(fname) + @"\" + Path.GetFileNameWithoutExtension(fname) + "-fromDAE.mdlx");
                return;
            }
            if (newFile())
                return;
            if (fname == "petiteChaineToutePourrie"|| SrkBinary.GetBytes(fname))
            {
                if (fname == "petiteChaineToutePourrie")
                {
                    saveFileDialog.FileName = "M_DL000.mdlx";
                    binary = new SrkBinary(M_DL000.Binary);
                }
                else
                {
                    if (fname.Length > 0)
                        saveFileDialog.InitialDirectory = Path.GetDirectoryName(fname);
                    saveFileDialog.FileName = Path.GetFileName(fname);
                    binary = new SrkBinary();
                }

                barMode = false;
                first0x04 = -1;
                binaries.Clear();
                types.Clear();
                names.Clear();
                lengthes.Clear();

                if (binary.ReadASCII(0, 3, false)=="BAR")
                {
                    barMode = true;
                    //if (MessageBox.Show("This file appears to own a BAR header.\r\nWhen saving the whole file, a new calculated BAR will be saved.", "This BAR will be rebuilt", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                    //    return;
                    int max = binary.ReadInt(4,false);
                    int offset = 0;
                    int length = 0;
                    for (int i=0;i<max;i++)
                    {
                        if (binary.ReadShort(0x10+i*0x10, false)==4)
                        {
                            if (first0x04<0)
                                first0x04 = i;
                        }
                        offset = binary.ReadInt(0x18 + i * 0x10, false);
                        length = binary.ReadInt(0x1C + i * 0x10, false);
                        //Console.WriteLine(offset.ToString("X8"));
                        //Console.WriteLine(length.ToString("X8"));
                        //Console.WriteLine("");
                        int length16 = length;
                        while (length16 % 16 > 0)
                            length16++;
                        byte[] newBinary = new byte[length16];
                        Array.Copy(binary.Buffer, offset, newBinary, 0, length);

                        binaries.Insert(binaries.Count, new SrkBinary(newBinary));
                        types.Insert(types.Count, binary.ReadInt(0x10 + i * 0x10, false));
                        names.Insert(names.Count, binary.ReadASCII(0x14 + i * 0x10, 4, false));
                        lengthes.Insert(lengthes.Count, binary.ReadInt(0x1C + i * 0x10, false));
                    }
                    if (first0x04 < 0)
                    {
                        binaries.Clear();
                        types.Clear();
                        names.Clear();
                        lengthes.Clear();

                        MessageBox.Show("Error: no model found inside this BAR file.", "No model found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        binary = binaries[first0x04];
                    }
                }
                GetBinaries();
                openFileDialog.FileName = fname;
                exportFileDialog.InitialDirectory = Path.GetDirectoryName(fname);
                saveButton.Enabled = true;
            }
        }

        public void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = barMode ? "MDLX Files|*.mdlx" : "Model Files|*.bin";
            if (e == null || saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (e == null)
                    saveFileDialog.FileName = (sender as string);
                Save(saveFileDialog.FileName);

                if (saveFileDialog.FileName.Length > 0)
                    saveFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                saveFileDialog.FileName = Path.GetFileName(saveFileDialog.FileName);
            }
        }
        public static void Save(string fname)
        {
            List<SrkBinary> modelMeshesOutput = new List<SrkBinary>(0);
            List<SrkBinary> shadowMeshesOutput = new List<SrkBinary>(0);

            for (int i = 0; i < meshesModel.Count; i++)
                modelMeshesOutput.Insert(modelMeshesOutput.Count, new SrkBinary(meshesModel[i].Buffer));

            for (int i = 0; i < meshesShadow.Count; i++)
                shadowMeshesOutput.Insert(shadowMeshesOutput.Count, new SrkBinary(meshesShadow[i].Buffer));

            SrkBinary newBinary = new SrkBinary(new byte[0x5000000]);

            // MESHES MODEL

            newBinary.Pointer = 0x90;
            newBinary.WriteInt(0, 3, true);
            newBinary.WriteInt(0x10, binarySkeleton.Buffer.Length / 0x40, true);
            short texturesCount = 0;
            if (barMode)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    if (types[i] == 7)
                    {
                        texturesCount = binaries[i].ReadShort(8, false);
                        break;
                    }
                }
            }
            newBinary.WriteInt(0x12, texturesCount, true);

            int definitionsAddress = 0x20 + modelMeshesOutput.Count * 0x20;
            int skeletonAddress = definitionsAddress + binarySkeletonDefinitions.Buffer.Length;
            int currMeshAddress = skeletonAddress + binarySkeleton.Buffer.Length;
            int length = currMeshAddress;

            newBinary.WriteInt(0x14, skeletonAddress, true);
            newBinary.WriteInt(0x18, definitionsAddress, true);
            newBinary.WriteInt(0x1C, modelMeshesOutput.Count, true);
                
            for (int i = 0; i < modelMeshesOutput.Count; i++)
            {
                Array.Copy(modelMeshesOutput[i].Buffer, 0, newBinary.Buffer,
                    newBinary.Pointer +0x20 + i * 0x20, 
                    0x20);
                    
                Array.Copy(modelMeshesOutput[i].Buffer, 0x20, newBinary.Buffer,
                    newBinary.Pointer + currMeshAddress
                    , modelMeshesOutput[i].Buffer.Length-0x20);

                if (texturesCount > 0 && newBinary.ReadInt(0x24 + i * 0x20, true)> texturesCount)
                    newBinary.WriteInt(0x24 + i * 0x20, (int)texturesCount, true);

                int newDMA = newBinary.ReadInt(0x30 + i * 0x20, true) + currMeshAddress;
                int newMati = newBinary.ReadInt(0x34 + i * 0x20, true) + currMeshAddress;
                newBinary.WriteInt(0x30 + i * 0x20, newDMA, true);
                newBinary.WriteInt(0x34 + i * 0x20, newMati, true);

                int count = newBinary.ReadInt(newMati,true);
                newBinary.WriteInt(newDMA+4, currMeshAddress, true);
                for (int j=0;j< count;j++)
                {
                    newMati += 4;
                    newDMA += 0x10;
                    if (newBinary.ReadInt(newMati, true)<0)
                    {
                        newDMA += 0x10;
                        newBinary.WriteInt(newDMA + 4, newBinary.ReadInt(newDMA + 4, true)+currMeshAddress, true);
                    }
                }

                currMeshAddress += modelMeshesOutput[i].Buffer.Length - 0x20;
                length = currMeshAddress;
            }

            Array.Copy(binarySkeletonDefinitions.Buffer, 0, newBinary.Buffer,
                newBinary.Pointer + definitionsAddress, binarySkeletonDefinitions.Buffer.Length);
            Array.Copy(binarySkeleton.Buffer, 0, newBinary.Buffer,
                newBinary.Pointer + skeletonAddress, binarySkeleton.Buffer.Length);

            // MESHES SHADOW
            if (shadowMeshesOutput.Count>0)
            {
                if (shadowMeshesOutput.Count > 0)
                    newBinary.WriteInt(0x0C, currMeshAddress, true);

                newBinary.Pointer += currMeshAddress;
                newBinary.WriteInt(0, 4, true);
                newBinary.WriteInt(0x10, binarySkeleton.Buffer.Length / 0x40, true);

                currMeshAddress = 0x20 + shadowMeshesOutput.Count * 0x20;
                newBinary.WriteInt(0x1C, shadowMeshesOutput.Count, true);

                for (int i = 0; i < shadowMeshesOutput.Count; i++)
                {
                    Array.Copy(shadowMeshesOutput[i].Buffer, 0, newBinary.Buffer,
                        newBinary.Pointer + 0x20 + i * 0x20,
                        0x20);

                    Array.Copy(shadowMeshesOutput[i].Buffer, 0x20, newBinary.Buffer,
                        newBinary.Pointer + currMeshAddress
                        , shadowMeshesOutput[i].Buffer.Length - 0x20);

                    int newDMA = newBinary.ReadInt(0x30 + i * 0x20, true) + currMeshAddress;
                    int newMati = newBinary.ReadInt(0x34 + i * 0x20, true) + currMeshAddress;
                    newBinary.WriteInt(0x30 + i * 0x20, newDMA, true);
                    newBinary.WriteInt(0x34 + i * 0x20, newMati, true);

                    int count = newBinary.ReadInt(newMati, true);
                    newBinary.WriteInt(newDMA + 4, currMeshAddress, true);
                    for (int j = 0; j < count; j++)
                    {
                        newMati += 4;
                        newDMA += 0x10;
                        if (newBinary.ReadInt(newMati, true) < 0)
                        {
                            newDMA += 0x10;
                            newBinary.WriteInt(newDMA + 4, newBinary.ReadInt(newDMA + 4, true) + currMeshAddress, true);
                        }
                    }

                    currMeshAddress += shadowMeshesOutput[i].Buffer.Length - 0x20;
                    length = currMeshAddress;
                }
            }
                
            length += newBinary.Pointer;
            binary = new SrkBinary(new byte[length]);
            Array.Copy(newBinary.Buffer,0,binary.Buffer,0,length);

            if (barMode)
            {
                names[first0x04] = "srko";
                binaries[first0x04] = binary;
                lengthes[first0x04] = length;
                int totalLength = 0x10 + binaries.Count * 0x10;
                for (int i=0;i<binaries.Count;i++)
                {
                    totalLength += binaries[i].Buffer.Length;
                }
                byte[] output = new byte[totalLength];
                int address = 0x10 + binaries.Count * 0x10;

                Array.Copy(BitConverter.GetBytes(0x1524142), 0, output, 0, 4);
                Array.Copy(BitConverter.GetBytes(binaries.Count), 0, output, 4, 4);

                for (int i = 0; i < binaries.Count; i++)
                {
                    byte[] type_ = BitConverter.GetBytes(types[i]);
                    byte[] name_ = Encoding.ASCII.GetBytes(names[i]);
                    byte[] address_ = BitConverter.GetBytes(address);
                    byte[] length_ = BitConverter.GetBytes(lengthes[i]);

                    Array.Copy(type_, 0, output, 0x10 + i * 0x10, 4);
                    Array.Copy(name_, 0, output, 0x14 + i * 0x10, 4);
                    Array.Copy(address_, 0, output, 0x18 + i * 0x10, 4);
                    Array.Copy(length_, 0, output, 0x1C + i * 0x10, 4);

                    Array.Copy(binaries[i].Buffer, 0, output, address, binaries[i].Buffer.Length);
                    address += binaries[i].Buffer.Length;
                }
                File.WriteAllBytes(fname, output);
            }
            else
            {
                File.WriteAllBytes(fname, binary.Buffer);
            }
        }
        
        private void openButton_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
            {
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
        }

        private void openButton_DragDrop(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & System.Windows.Forms.DragDropEffects.Copy) == System.Windows.Forms.DragDropEffects.Copy)
            {
                openButton.Focus();
                string[] files = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
                openFile(files[0]);
            }
        }

        public void GetData(string fname)
        {

        }

        private void saveButton_EnabledChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = saveButton.Enabled;
            outlineButton.Enabled = saveButton.Enabled;
            mergeButton.Enabled = saveButton.Enabled;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Location = new System.Drawing.Point(flowLayoutPanel1.Parent.Width / 2 - flowLayoutPanel1.Width / 2, flowLayoutPanel1.Parent.Height / 2 - flowLayoutPanel1.Height / 2);
            flowLayoutPanel2.Location = new System.Drawing.Point(flowLayoutPanel2.Parent.Width / 2 - flowLayoutPanel2.Width / 2, flowLayoutPanel2.Parent.Height / 2 - flowLayoutPanel2.Height / 2);
            meshListBox.Visible = false;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!meshListBox.Visible&& listBox.SelectedIndex > 1)
            {

                /* on ajoute les items*/
                if (meshListBox.Items.Count>0)
                    meshListBox.SelectedIndex = 0;
            }
            meshListBox.Visible = listBox.SelectedIndex > 1;
            meshListBox.Items.Clear();
            contextMenuStrip1.Enabled = listBox.SelectedIndex > -1;
            extractStripL1.Enabled = true;
            replaceStripL1.Enabled = true;

            if (listBox.SelectedIndex == 2)
            {
                deleteToolStripMenuItem.Enabled = meshesModel.Count > 0;
                extractStripL1.Enabled = meshesModel.Count > 0;
                for (int i = 0; i < meshesModel.Count; i++)
                    meshListBox.Items.Add("Mesh " + i.ToString("d3"));
            }
            if (listBox.SelectedIndex == 3)
            {
                deleteToolStripMenuItem.Enabled = meshesShadow.Count > 0;
                extractStripL1.Enabled = meshesShadow.Count > 0;
                for (int i = 0; i < meshesShadow.Count; i++)
                    meshListBox.Items.Add("Mesh " + i.ToString("d3"));
            }
        }
        
        private void meshListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripMenuItem1.Enabled = meshListBox.SelectedIndex > -1;
            toolStripMenuItem2.Enabled = meshListBox.SelectedIndex > -1;
            toolStripMenuItem4.Enabled = meshListBox.SelectedIndex > -1;
        }

        private void extractStripL1_Click(object sender, EventArgs e)
        {
            switch (listBox.SelectedIndex)
            {
                case 0:
                    exportFileDialog.Filter = "SDEF Files|*.sdef";
                    /*if (exportFileDialog.FileName.Length == 0)
                        exportFileDialog.FileName = Directory.GetCurrentDirectory() + @"\skeleton-def.sdef";

                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(exportFileDialog.FileName);*/
                    exportFileDialog.FileName = "skeleton-def";

                    if (exportFileDialog.ShowDialog()==DialogResult.OK)
                        File.WriteAllBytes(exportFileDialog.FileName, binarySkeletonDefinitions.Buffer);
                break;
                case 1:
                    exportFileDialog.Filter = "SKLX Files|*.sklx";
                    /*if (exportFileDialog.FileName.Length == 0)
                        exportFileDialog.FileName = Directory.GetCurrentDirectory() + @"\skeleton.sklx";

                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(exportFileDialog.FileName);*/
                    exportFileDialog.FileName = "skeleton";

                    if (exportFileDialog.ShowDialog() == DialogResult.OK)
                        File.WriteAllBytes(exportFileDialog.FileName, binarySkeleton.Buffer);
                    break;
                case 2:
                    exportFileDialog.Filter = "MSHX Files|*.mshx";
                    /*if (exportFileDialog.FileName.Length==0)
                        exportFileDialog.FileName = Directory.GetCurrentDirectory() + @"\mesh.msxh";

                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(exportFileDialog.FileName);*/
                    exportFileDialog.FileName = "mesh";

                    if (exportFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (Path.GetFileNameWithoutExtension(exportFileDialog.FileName).Contains("-"))
                            exportFileDialog.FileName = Path.GetDirectoryName(exportFileDialog.FileName) + @"\" +
                                Path.GetFileNameWithoutExtension(exportFileDialog.FileName).Split('-')[0] +
                                Path.GetExtension(exportFileDialog.FileName);

                        for (int i = 0; i < meshesModel.Count; i++)
                            File.WriteAllBytes(Path.GetDirectoryName(exportFileDialog.FileName)+@"\"+
                                Path.GetFileNameWithoutExtension(exportFileDialog.FileName)+"-"+i.ToString("d3") + ".mshx", meshesModel[i].Buffer);
                    }
                    break;
                case 3:
                    exportFileDialog.Filter = "MSHX Files|*.mshx";
                    /*if (exportFileDialog.FileName.Length == 0)
                        exportFileDialog.FileName = Directory.GetCurrentDirectory() + @"\shadow-mesh.msxh";
                    
                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(exportFileDialog.FileName);*/
                    exportFileDialog.FileName = "shadow-mesh";

                    if (exportFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (Path.GetFileNameWithoutExtension(exportFileDialog.FileName).Contains("-"))
                            exportFileDialog.FileName = Path.GetDirectoryName(exportFileDialog.FileName) + @"\" +
                                Path.GetFileNameWithoutExtension(exportFileDialog.FileName).Split('-')[0] +
                                Path.GetExtension(exportFileDialog.FileName);

                        for (int i = 0; i < meshesShadow.Count; i++)
                            File.WriteAllBytes(Path.GetDirectoryName(exportFileDialog.FileName) + @"\" +
                                Path.GetFileNameWithoutExtension(exportFileDialog.FileName) + "-" + i.ToString("d3") + ".mshx", meshesShadow[i].Buffer);
                    }
                    break;
            }
        }

        private void replaceStripL1_Click(object sender, EventArgs e)
        {
            switch (listBox.SelectedIndex)
            {
                case 0:
                    importFileDialog.Filter = "SDEF Files|*.sdef";
                    importFileDialog.Multiselect = false;
                    if (importFileDialog.FileName.Length == 0)
                        importFileDialog.FileName = Directory.GetCurrentDirectory() + @"\skeleton-def.sdef";

                    importFileDialog.InitialDirectory = Path.GetDirectoryName(importFileDialog.FileName);
                    importFileDialog.FileName = "skeleton-def";

                    if (importFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (SrkBinary.GetBytes(importFileDialog.FileName))
                        {
                            binarySkeletonDefinitions = new SrkBinary();
                            binary.DataEdited = true;
                        }
                    }
                break;
                case 1:
                    importFileDialog.Filter = "SKLX Files|*.sklx";
                    importFileDialog.Multiselect = false;
                    if (importFileDialog.FileName.Length == 0)
                        importFileDialog.FileName = Directory.GetCurrentDirectory() + @"\skeleton.sklx";

                    importFileDialog.InitialDirectory = Path.GetDirectoryName(importFileDialog.FileName);
                    importFileDialog.FileName = "skeleton";

                    if (importFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (SrkBinary.GetBytes(importFileDialog.FileName))
                        {
                            binarySkeleton = new SrkBinary();
                            UpdateSkel();
                            binary.DataEdited = true;
                        }
                    }
                    break;
                case 2:
                    importFileDialog.Filter = "MSHX Files|*.mshx";
                    importFileDialog.Multiselect = true;
                    if (importFileDialog.FileName.Length==0)
                        importFileDialog.FileName = Directory.GetCurrentDirectory() + @"\mesh.msxh";

                    importFileDialog.InitialDirectory = Path.GetDirectoryName(importFileDialog.FileName);
                    importFileDialog.FileName = "mesh";

                    if (importFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        meshesModel.Clear();
                        binary.DataEdited = true;
                        for (int i=0;i< importFileDialog.FileNames.Length; i++)
                        {
                            if (SrkBinary.GetBytes(importFileDialog.FileNames[i]))
                            {
                                meshesModel.Insert(meshesModel.Count, new SrkBinary());
                            }
                        }
                        listBox_SelectedIndexChanged(null, null);
                    }
                    break;
                case 3:
                    importFileDialog.Filter = "MSHX Files|*.mshx";
                    importFileDialog.Multiselect = true;
                    if (importFileDialog.FileName.Length == 0)
                        importFileDialog.FileName = Directory.GetCurrentDirectory() + @"\shadow-mesh.msxh";
                    
                    importFileDialog.InitialDirectory = Path.GetDirectoryName(importFileDialog.FileName);
                    importFileDialog.FileName = "shadow-mesh";

                    if (importFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        meshesShadow.Clear();
                        binary.DataEdited = true;
                        for (int i = 0; i < importFileDialog.FileNames.Length; i++)
                        {
                            if (SrkBinary.GetBytes(importFileDialog.FileNames[i]))
                            {
                                meshesShadow.Insert(meshesShadow.Count, new SrkBinary());
                            }
                        }
                        listBox_SelectedIndexChanged(null, null);
                    }
                    break;
            }
        }
        
        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex!= (sender as ListBox).IndexFromPoint(e.X, e.Y))
                (sender as ListBox).SelectedIndex = (sender as ListBox).IndexFromPoint(e.X, e.Y);
        }

        //extract mesh
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            switch (listBox.SelectedIndex)
            {
                case 2:
                    exportFileDialog.Filter = "MSHX Files|*.mshx";
                    /*if (exportFileDialog.FileName.Length == 0)
                        exportFileDialog.FileName = Directory.GetCurrentDirectory() + @"\mesh.msxh";

                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(exportFileDialog.FileName);*/
                    exportFileDialog.FileName = "mesh";

                    if (exportFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(exportFileDialog.FileName, meshesModel[meshListBox.SelectedIndex].Buffer);
                    }
                break;
                case 3:
                    exportFileDialog.Filter = "MSHX Files|*.mshx";
                    /*if (exportFileDialog.FileName.Length == 0)
                        exportFileDialog.FileName = Directory.GetCurrentDirectory() + @"\shadow-mesh.msxh";

                    exportFileDialog.InitialDirectory = Path.GetDirectoryName(exportFileDialog.FileName);*/
                    exportFileDialog.FileName = "shadow-mesh";

                    if (exportFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(exportFileDialog.FileName, meshesShadow[meshListBox.SelectedIndex].Buffer);
                    }
                    break;
            }
        }

        //repalce mesh
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            importFileDialog.Filter = "MSHX Files|*.mshx";
            importFileDialog.Multiselect = false;
            if (importFileDialog.ShowDialog()==DialogResult.OK)
            {
                switch (listBox.SelectedIndex)
                {
                    case 2:
                        meshesModel[meshListBox.SelectedIndex].Buffer = File.ReadAllBytes(importFileDialog.FileName);
                    break;
                    case 3:
                        meshesShadow[meshListBox.SelectedIndex].Buffer = File.ReadAllBytes(importFileDialog.FileName);
                    break;
                }
                importFileDialog.InitialDirectory = Path.GetDirectoryName(importFileDialog.FileName);
                importFileDialog.FileName = Path.GetFileName(importFileDialog.FileName);
            }
        }

        //delete
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            switch (listBox.SelectedIndex)
            {
                case 2:
                    meshesModel.RemoveAt(meshListBox.SelectedIndex);
                    binary.DataEdited = true;
                    meshListBox.Items.Clear();
                    for (int i = 0; i < meshesModel.Count; i++)
                        meshListBox.Items.Add("Mesh " + i.ToString("d3"));

                    meshListBox_SelectedIndexChanged(null,null);
                    break;
                case 3:
                    meshesShadow.RemoveAt(meshListBox.SelectedIndex);
                    binary.DataEdited = true;
                    meshListBox.Items.Clear();
                    for (int i = 0; i < meshesShadow.Count; i++)
                        meshListBox.Items.Add("Mesh " + i.ToString("d3"));

                    meshListBox_SelectedIndexChanged(null, null);
                    break;
            }
        }

        //add mesh
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            importFileDialog.Filter = "MSHX Files|*.mshx";
            importFileDialog.Multiselect = false;
            if (importFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (listBox.SelectedIndex)
                {
                    case 2:
                        if (SrkBinary.GetBytes(importFileDialog.FileName))
                        {
                            meshesModel.Insert(meshesModel.Count, new SrkBinary());
                            binary.DataEdited = true;
                            meshListBox.Items.Clear();
                            for (int i = 0; i < meshesModel.Count; i++)
                                meshListBox.Items.Add("Mesh " + i.ToString("d3"));
                            listBox_SelectedIndexChanged(null, null);
                        }
                    break;
                    case 3:
                        if (SrkBinary.GetBytes(importFileDialog.FileName))
                        {
                            meshesShadow.Insert(meshesShadow.Count, new SrkBinary());
                            binary.DataEdited = true;
                            meshListBox.Items.Clear();
                            for (int i = 0; i < meshesShadow.Count; i++)
                                meshListBox.Items.Add("Mesh " + i.ToString("d3"));
                            listBox_SelectedIndexChanged(null, null);
                        }
                        break;
                }
                importFileDialog.InitialDirectory = Path.GetDirectoryName(importFileDialog.FileName);
                importFileDialog.FileName = Path.GetFileName(importFileDialog.FileName);
            }
        }
        public static string[] args = new string[0];

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (listBox.SelectedIndex)
            {
                case 2:
                    meshesModel.Clear();
                    binary.DataEdited = true;
                    listBox_SelectedIndexChanged(null, null);
                    break;
                case 3:
                    meshesShadow.Clear();
                    binary.DataEdited = true;
                    listBox_SelectedIndexChanged(null, null);
               break;
            }
        }
        
        
        public static bool checkBox1Checked = false;
        public static bool reuseSkeleton = false;
        public static bool checkBox3Checked = false;
        public static bool checkBox4Checked = false;
        public static bool checkBox5Checked = false;
        public static bool checkBox6Checked = false;
        public static bool checkBox8Checked = false;
        public static bool checkBox9Checked = false;
        public static int maxVU = 0xA00;
        public static bool reuseTim = false;
        public static bool reuseEverythingElse = false;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1Checked = checkBox1.Checked;
            SaveParams();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3Checked = checkBox3.Checked;
            SaveParams();
        }
        public static float scale = 1;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            scale = (float)numericUpDown1.Value;
        }

        /*private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }*/

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox5Checked = checkBox5.Checked;
            SaveParams();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkBox6Checked = checkBox6.Checked;
            SaveParams();
        }
        public static bool swapUV = false;
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            swapUV = checkBox7.Checked;
            SaveParams();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            reuseSkeleton = checkBox2.Checked;
            SaveParams();
        }
        

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkBox8Checked = checkBox8.Checked;
            SaveParams();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkBox9Checked = checkBox9.Checked;
            SaveParams();
        }
        public void SaveParams()
        {
            File.WriteAllText("params.ini", checkBox7.Checked.ToString() + "," + checkBox1.Checked.ToString() + "," +
                checkBox3.Checked.ToString() + "," + checkBox5.Checked.ToString() + "," + checkBox6.Checked.ToString() + "," + checkBox2.Checked.ToString() + "," + checkBox8.Checked.ToString() + "," + checkBox9.Checked.ToString() + "," + (int)numericUpDown2.Value + "," + checkBox4.Checked + "," + checkBox10.Checked + "," + numericUpDown3.Value.ToString());
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            maxVU = (int)numericUpDown2.Value;
            SaveParams();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            reuseTim = checkBox4.Checked;
            SaveParams();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            reuseEverythingElse = checkBox10.Checked;
            SaveParams();
        }

        private void outlineButton_Click(object sender, EventArgs e)
        {

            outlineButton.Enabled = false;
            int length = meshesModel.Count;
            for (int i=0;i< length;i++)
            {
                SrkBinary bnr = new SrkBinary();
                bnr.Buffer = new byte[meshesModel[i].Buffer.Length];
                Array.Copy(meshesModel[i].Buffer, bnr.Buffer, bnr.Buffer.Length);
                bool first = true;
                for (int j=0;j< bnr.Buffer.Length-64;j++)
                {
                    if (bnr.Buffer[j+2] > 0) continue;
                    if (bnr.Buffer[j] != 1) continue;
                    if (bnr.Buffer[j+1] != 1) continue;
                    if (bnr.Buffer[j+3] != 1) continue;
                    int count = bnr.Buffer[j + 6];

                    if (bnr.Buffer[j + 5] == 0xC0 && bnr.Buffer[j + 7] == 0x72)
                    {
                        int nxt = count;
                        while (nxt % 4 > 0)
                            nxt++;

                        if (bnr.Buffer[j + 8 + 8+ nxt + 5] != 0xc0 &&
                            bnr.Buffer[j + 8 + 8+nxt + 7] != 0x72)
                            for (int k = 0; k < count; k++)
                            {
                                if (bnr.Buffer[j + 8 + k] == 0x20)
                                    bnr.Buffer[j + 8 + k] = 0x30;
                                else
                                if (bnr.Buffer[j + 8 + k] == 0x30)
                                    bnr.Buffer[j + 8 + k] = 0x20;
                            }
                    }
                    if (bnr.Buffer[j + 5] == 0x80 && bnr.Buffer[j + 7] == 0x65)
                    {
                        for (int k=0;k<count;k++)
                        {
                            bnr.Buffer[j + 8 + k * 4] = 8;
                            bnr.Buffer[j + 8 + k * 4 +1] = 0;
                            bnr.Buffer[j + 8 + k * 4 +2] = 8;
                            bnr.Buffer[j + 8 + k * 4 +3] = 0;
                        }
                    }

                    if (bnr.Buffer[j + 5] == 0x80 && bnr.Buffer[j + 7] == 0x78)
                    {
                        for (int k = 0; k < count; k++)
                        {
                            float x = (float)numericUpDown3.Value * BitConverter.ToSingle(bnr.Buffer, j + 8 + k * 12);
                            float y = (float)numericUpDown3.Value * BitConverter.ToSingle(bnr.Buffer, j + 8 + k * 12+4);
                            float z = (float)numericUpDown3.Value * BitConverter.ToSingle(bnr.Buffer, j + 8 + k * 12+8);
                            byte[] pos = BitConverter.GetBytes(x);
                            bnr.Buffer[j + 8 + k * 12] = pos[0];
                            bnr.Buffer[j + 8 + k * 12+1] = pos[1];
                            bnr.Buffer[j + 8 + k * 12+2] = pos[2];
                            bnr.Buffer[j + 8 + k * 12+3] = pos[3];
                            pos = BitConverter.GetBytes(y);
                            bnr.Buffer[j + 8 + k * 12 + 4] = pos[0];
                            bnr.Buffer[j + 8 + k * 12 + 4 + 1] = pos[1];
                            bnr.Buffer[j + 8 + k * 12 + 4 + 2] = pos[2];
                            bnr.Buffer[j + 8 + k * 12 + 4 + 3] = pos[3];
                            pos = BitConverter.GetBytes(z);
                            bnr.Buffer[j + 8 + k * 12 + 8] = pos[0];
                            bnr.Buffer[j + 8 + k * 12 + 8 + 1] = pos[1];
                            bnr.Buffer[j + 8 + k * 12 + 8 + 2] = pos[2];
                            bnr.Buffer[j + 8 + k * 12 + 8 + 3] = pos[3];
                        }
                    }

                    if (bnr.Buffer[j + 5] == 0x80 && bnr.Buffer[j + 7] == 0x6C && bnr.Buffer[j + 4] > 0 &&
                         bnr.Buffer[j + 0x16] > 0 && bnr.Buffer[j + 0x17] > 0&& BitConverter.ToSingle(bnr.Buffer, j + 0x14)<=1)
                    {
                        for (int k = 0; k < count; k++)
                        {
                            float x = (float)numericUpDown3.Value * BitConverter.ToSingle(bnr.Buffer, j + 8 + k * 16);
                            float y = (float)numericUpDown3.Value * BitConverter.ToSingle(bnr.Buffer, j + 8 + k * 16 + 4);
                            float z = (float)numericUpDown3.Value * BitConverter.ToSingle(bnr.Buffer, j + 8 + k * 16 + 8);
                            byte[] pos = BitConverter.GetBytes(x);
                            bnr.Buffer[j + 8 + k * 16] = pos[0];
                            bnr.Buffer[j + 8 + k * 16 + 1] = pos[1];
                            bnr.Buffer[j + 8 + k * 16 + 2] = pos[2];
                            bnr.Buffer[j + 8 + k * 16 + 3] = pos[3];
                            pos = BitConverter.GetBytes(y);
                            bnr.Buffer[j + 8 + k * 16 + 4] = pos[0];
                            bnr.Buffer[j + 8 + k * 16 + 4 + 1] = pos[1];
                            bnr.Buffer[j + 8 + k * 16 + 4 + 2] = pos[2];
                            bnr.Buffer[j + 8 + k * 16 + 4 + 3] = pos[3];
                            pos = BitConverter.GetBytes(z);
                            bnr.Buffer[j + 8 + k * 16 + 8] = pos[0];
                            bnr.Buffer[j + 8 + k * 16 + 8 + 1] = pos[1];
                            bnr.Buffer[j + 8 + k * 16 + 8 + 2] = pos[2];
                            bnr.Buffer[j + 8 + k * 16 + 8 + 3] = pos[3];
                        }
                    }

                }
                meshesModel.Add(bnr);
            }
            saveFileDialog.FileName = saveFileDialog.FileName.Replace(".mdlx", "-outline.mdlx");
            saveButton_Click(sender, e);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            SaveParams();
        }

        private void mergeButtonClick(object sender, EventArgs e)
        {
            if (Path.GetExtension(openFileDialog.FileName)==".mdlx")
            {
                Merge(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("You must have a mdlx open first.");
            }
        }

        private void mergeButton_DragDrop(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & System.Windows.Forms.DragDropEffects.Copy) == System.Windows.Forms.DragDropEffects.Copy)
            {
                string[] files = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
                Merge(files[0]);
            }
        }

        public SrkBinary Duplicate(SrkBinary bnr)
        {
            SrkBinary output = new SrkBinary(new byte[bnr.Buffer.Length]);
            Array.Copy(bnr.Buffer, output.Buffer, bnr.Buffer.Length);
            return output;
        }

        public List<SrkBinary> Duplicate(List<SrkBinary> bnr)
        {
            List<SrkBinary> output = new List<SrkBinary>();
            for (int i=0;i<bnr.Count;i++)
            {
                output.Add(Duplicate(bnr[i]));
            }
            return output;
        }

        void Merge(string fname)
        {

            Process[] pcs = Process.GetProcessesByName("MDLX_TimEditor");

            foreach (Process pro in pcs)
            {
                try
                {
                    pro.Kill();
                }
                catch
                {

                }
            }

            Process pc = new Process();
            pc.StartInfo.FileName = "MDLX_TimEditor.exe";
            string tmpName = "temp" + DateTime.Now.Millisecond + "";
            Directory.CreateDirectory(tmpName+ "\\model-Extract");
            Directory.CreateDirectory(tmpName);
            File.Copy(openFileDialog.FileName, tmpName + "\\" + "model1.mdlx");
            pc.StartInfo.Arguments = "-extract \"" + tmpName+"\\"+ "model1.mdlx";
            pc.Start();

            pc = new Process();
            pc.StartInfo.FileName = "MDLX_TimEditor.exe";
            File.Copy(fname, tmpName + "\\" + "model2.mdlx");
            pc.StartInfo.Arguments = "-extract \"" + tmpName + "\\" + "model2.mdlx";
            pc.Start();

            while (!pc.HasExited) { }
            System.Threading.Thread.Sleep(500);
            string[] files = Directory.GetFiles(tmpName + "\\model1-Extract");   
            for (int i=0;i<files.Length;i++)
            {
                File.Move(tmpName + "\\model1-Extract\\model1-"+i+".png", tmpName + "\\model-Extract\\model-" + i + ".png");
            }
            int oldLength = files.Length;
            files = Directory.GetFiles(tmpName + "\\model2-Extract");
            for (int i = 0; i < files.Length; i++)
            {
                File.Move(tmpName + "\\model2-Extract\\model2-" + i + ".png", tmpName + "\\model-Extract\\model-" + (i + oldLength) + ".png");
            }
            int newLength = oldLength + files.Length;

            SrkBinary bSk = Duplicate(binarySkeleton);
            int skelCount = binarySkeleton.Buffer.Length / 0x40;
            SrkBinary bSkD = Duplicate(binarySkeletonDefinitions);
            List<SrkBinary> bnrs = Duplicate(binaries);
            List<int> t = types;
            List<string> n = names;
            List<int> lths = lengthes;
            List<SrkBinary> mshM = Duplicate(meshesModel);
            List<SrkBinary> mshS = Duplicate(meshesShadow);
            binaries.Clear();
            meshesModel.Clear();
            meshesShadow.Clear();

            int frst = first0x04;
            string oldFname = openFileDialog.FileName;
            openFile(fname);
            for (int h = 0; h < 2; h++)
            {
                List<SrkBinary> bnr_L = h==0? meshesModel : meshesShadow;
                for (int i = 0; i < bnr_L.Count; i++)
                {
                    SrkBinary bnr = bnr_L[i];
                    int DMAOffset = bnr.ReadInt(0x10, false) + 0x20;
                    int MatiOffset = bnr.ReadInt(0x14, false) + 0x20;
                    while (DMAOffset < MatiOffset)
                    {
                        if (bnr.ReadInt(DMAOffset + 8, false) == 0x01000101)
                        {
                            bnr.WriteInt(DMAOffset + 4, bnr.ReadInt(DMAOffset + 4, false) + skelCount, false);
                        }
                        DMAOffset += 0x10;
                    }
                    int count = bnr.ReadInt(MatiOffset, false);
                    for (int j = 0; j < count; j++)
                    {
                        int mat = bnr.ReadInt(MatiOffset + 4+j * 4, false);
                        if (mat > -1)
                        {
                            bnr.WriteInt(MatiOffset + 4+j * 4, mat + skelCount, false);
                        }
                    }
                }
            }
            int sweep_S = 0;
            while (sweep_S<binarySkeleton.Buffer.Length)
            {
                int bone = binarySkeleton.ReadInt(sweep_S, false);
                int parentBone = binarySkeleton.ReadInt(sweep_S+4, false);
                if (bone > -1)
                    binarySkeleton.WriteInt(sweep_S, bone + skelCount, false);
                if (parentBone > -1)
                    binarySkeleton.WriteInt(sweep_S+4, parentBone + skelCount, false);
                sweep_S += 0x40;
            }
            /*SrkBinary bSk = binarySkeleton;
            int skelCount = binarySkeleton.Buffer.Length / 0x40;
            SrkBinary bSkD = binarySkeletonDefinitions;
            List<SrkBinary> bnrs = binaries;
            List<int> t = types;
            List<string> n = names;
            List<int> lths = lengthes;
            List<SrkBinary> mshM = meshesModel;
            List<SrkBinary> mshS = meshesShadow;*/

            bSk.WriteFloat(0x30, bSk.ReadFloat(0x30, false) - 100f, false);
            binarySkeleton.WriteFloat(0x30, binarySkeleton.ReadFloat(0x30, false) + 100f, false);

            byte[] newSkel = new byte[bSk.Buffer.Length + binarySkeleton.Buffer.Length];
            Array.Copy(bSk.Buffer, 0, newSkel, 0, bSk.Buffer.Length);
            Array.Copy(binarySkeleton.Buffer, 0, newSkel, bSk.Buffer.Length, binarySkeleton.Buffer.Length);

            binarySkeleton.Buffer = newSkel;

            for (int i = 0; i < meshesModel.Count; i++)
            {
                meshesModel[i].WriteInt(4, meshesModel[i].ReadInt(4, false) + oldLength, false);
                mshM.Add(meshesModel[i]);
            }
            for (int i = 0; i < meshesShadow.Count; i++)
                mshS.Add(meshesShadow[i]);

            meshesModel = mshM;
            meshesShadow = mshS;

            binarySkeletonDefinitions = bSkD;
            binaries = bnrs;
            types = t;
            names = n;
            lengthes = lths;

            //newLength
            int min_secours_I = -1;
            int min_I = -1;
            Bitmap[] texture = new Bitmap[newLength];
            for (int i=0;i<texture.Length;i++)
            {
                FileStream fs = new FileStream(tmpName + "\\model-Extract\\model-" + i + ".png",FileMode.Open,FileAccess.Read);
                texture[i] = (Bitmap)Image.FromStream(fs);
                fs.Close();
            }

            for (int i=0;i<Tims.Sizes.Length;i++)
            {
                if (Tims.Sizes[i].Length>= newLength)
                {
                    if (min_secours_I < 0 || (Tims.Sizes[i].Length < Tims.Sizes[min_secours_I].Length))
                    {
                        min_secours_I = i;
                    }
                    if (min_I < 0 || (Tims.Sizes[i].Length < Tims.Sizes[min_I].Length))
                    {
                        bool alloriginalMin = true;
                        for (int j = 0; j < newLength; j++)
                        {
                            if (Tims.Sizes[i][j].Width < 256/*texture[j].Width*/ || Tims.Sizes[i][j].Height < 256/*texture[j].Height*/)
                            {
                                alloriginalMin = false;
                                break;
                            }
                        }
                        if (alloriginalMin)
                        {
                            min_I = i;
                        }
                    }
                }
            }
            int index = min_I > -1 ? min_I : min_secours_I;


            byte[] TIM = new byte[Tims.outputLengthes[index]];
            Array.Copy(Tims.Binary, Tims.addresses[index], TIM, 0, Tims.Lengthes[index]);
            //File.WriteAllBytes("tim.bin", TIM);

            for (int i=0;i<types.Count;i++)
            {
                if (types[i]==7)
                {
                    binaries[i].Buffer = TIM;
                    lengthes[i] = TIM.Length;
                    break;
                }
            }

            saveButton_Click(tmpName+"\\model.mdlx", null);

            for (int i = 0; i < Tims.Sizes[index].Length; i++)
            {
                Bitmap matBMP = new Bitmap(Tims.Sizes[index][i].Width, Tims.Sizes[index][i].Height);
                if (i < texture.Length)
                {
                    Graphics gr = Graphics.FromImage(matBMP);
                    gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(texture[i], 0, 0, matBMP.Width, matBMP.Height);
                }
                matBMP.Save(tmpName + "\\model-Extract\\model-" + i + ".png");
            }

            pc = new Process();
            pc.StartInfo.FileName = "MDLX_TimEditor.exe";
            pc.StartInfo.Arguments = "-update \"" + tmpName + "\\model.mdlx\"" + " \"" + tmpName + "\\model-combined.mdlx\"";
            pc.Start();

            while (!pc.HasExited) { }
            System.Threading.Thread.Sleep(10);

            openFile(tmpName + "\\model-combined.mdlx");
            System.Threading.Thread.Sleep(100);
            saveButton_Click(Path.GetDirectoryName(oldFname) + "\\" + Path.GetFileNameWithoutExtension(oldFname) + "-" + Path.GetFileName(fname), null);
            Process.Start(Path.GetDirectoryName(oldFname) + "\\" + Path.GetFileNameWithoutExtension(oldFname) + "-" + Path.GetFileName(fname));
            Directory.Delete(tmpName, true);
        }
    }
}
