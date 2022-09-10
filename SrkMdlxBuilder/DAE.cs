using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using SlimDX;

namespace SrkMdlxBuilder
{
    class DAE
    {
        XmlDocument Document;
        public bool Shadow;
        public DAE(string filename)
        {
            this.Document = new XmlDocument();
            string fileData = System.IO.File.ReadAllText(filename, Encoding.UTF8);

            fileData = fileData.Replace("xmlns", "whocares");
            this.Document.LoadXml(fileData);

            this.vertexTriInd = new List<int>(0);
            this.normalTriInd = new List<int>(0);
            this.uvTriInd = new List<int>(0);
            this.colorTriInd = new List<int>(0);

            this.GeometryIDs = new List<string>(0);
            this.Vertex = new List<List<SlimDX.Vector3>>(0);
            this.Normal = new List<List<SlimDX.Vector3>>(0);
            this.UV = new List<List<SlimDX.Vector2>>(0);
            this.UV_repeat = new List<bool>(0);
            this.meshRepeats = new List<bool>(0);
            this.meshRepeatsIndices = new List<int>(0);

            this.VertexColor = new List<List<byte[]>>(0);

            this.Triangle = new List<List<int[]>>(0);
            this.Influences = new List<List<List<float>>>(0);
            this.InfluencesIndices = new List<List<List<int>>>(0);
            
            this.materialNames = new List<string>(0);
            this.meshRepeats = new List<bool>(0);
            this.materialFileNames = new List<string>(0);
            this.Name = Path.GetFileNameWithoutExtension(filename);
            this.DirectoryName = Path.GetDirectoryName(filename);
        }
        public string Name = "";
        public string DirectoryName = "";

        public List<string> GeometryIDs;

        public List<List<SlimDX.Vector3>> Vertex;
        public List<List<SlimDX.Vector3>> Normal;
        public List<List<SlimDX.Vector2>> UV;
        public List<bool> UV_repeat;
        public List<List<byte[]>> VertexColor;

        public List<List<int[]>> Triangle;

        public List<List<List<int>>> InfluencesIndices;
        public List<List<List<float>>> Influences;
        

        public static string ToString(SlimDX.Matrix m)
        {
            string s = "";
            s += m.M11.ToString("0.000000") + " " + m.M21.ToString("0.000000") + " " + m.M31.ToString("0.000000") + " " + m.M41.ToString("0.000000") + "\r\n";
            s += m.M12.ToString("0.000000") + " " + m.M22.ToString("0.000000") + " " + m.M32.ToString("0.000000") + " " + m.M42.ToString("0.000000") + "\r\n";
            s += m.M13.ToString("0.000000") + " " + m.M23.ToString("0.000000") + " " + m.M33.ToString("0.000000") + " " + m.M43.ToString("0.000000") + "\r\n";
            s += m.M14.ToString("0.000000") + " " + m.M24.ToString("0.000000") + " " + m.M34.ToString("0.000000") + " " + m.M44.ToString("0.000000") + "\r\n";
            return s;
        }
        public static string ToStringAccurate(SlimDX.Matrix m)
        {
            string s = "";
            s += ((Decimal)m.M11) + " " + ((Decimal)m.M21) + " " + ((Decimal)m.M31) + " " + ((Decimal)m.M41) + "\r\n";
            s += ((Decimal)m.M12) + " " + ((Decimal)m.M22) + " " + ((Decimal)m.M32) + " " + ((Decimal)m.M42) + "\r\n";
            s += ((Decimal)m.M13) + " " + ((Decimal)m.M23) + " " + ((Decimal)m.M33) + " " + ((Decimal)m.M43) + "\r\n";
            s += ((Decimal)m.M14) + " " + ((Decimal)m.M24) + " " + ((Decimal)m.M34) + " " + ((Decimal)m.M44) + "\r\n";
            return s;
        }

        public static string Format(string toFormat)
        {
            toFormat = toFormat.Replace("\r\n", "\n");
            while (toFormat.Contains("\n\n"))
                toFormat = toFormat.Replace("\n\n", "\n");

            toFormat = toFormat.Replace("\n", " ");

            while (toFormat.Contains("  "))
                toFormat = toFormat.Replace("  ", " ");
            toFormat = toFormat.Replace(".", System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            if (toFormat[0] == ' ')
                toFormat = toFormat.Remove(0, 1);
            if (toFormat[toFormat.Length - 1] == ' ')
                toFormat = toFormat.Remove(toFormat.Length - 1, 1);
            return toFormat;
        }
        public List<int> vertexTriInd;
        public List<int> normalTriInd;
        public List<int> uvTriInd;
        public List<int> colorTriInd;

        List<string> materialNames;
        List<string> materialFileNames;
        List<bool> meshRepeats;
        List<int> meshRepeatsIndices;
        XmlNodeList skinnings_;
        XmlNodeList materials_;
        XmlNodeList geometries_;

        public void Parse()
        {
            materials_ = this.Document.SelectNodes("//library_materials/material");

            if (!this.Shadow&&materials_ != null)
                for (int i = 0; i < materials_.Count; i++)
                {
                    string materialID = materials_[i].SelectNodes("@id")[0].InnerText;
                    string effectID = materials_[i].SelectNodes("instance_effect/@url")[0].InnerText.Remove(0, 1);

                    XmlNode effectNode = this.Document.SelectNodes("//library_effects/effect[@id='" + effectID + "']")[0];
                    string imageID = "";
                    XmlNodeList surfInitNode = effectNode.SelectNodes("profile_COMMON//surface/init_from");
                    if (surfInitNode != null && surfInitNode.Count > 0)
                    {
                        imageID = surfInitNode[0].InnerText;
                    }

                    XmlNodeList textureNode = effectNode.SelectNodes("profile_COMMON//texture/@texture");
                    if (imageID.Length == 0 && textureNode != null && textureNode.Count > 0)
                    {
                        imageID = textureNode[0].InnerText;
                    }
                    string fileName = this.Document.SelectNodes("//library_images/image[@id='" + imageID + "']/init_from")[0].InnerText;
                    fileName = fileName.Replace("file://", "");
                    fileName = fileName.Replace("../", "");
                    fileName = fileName.Replace("./", "");

                    fileName = fileName.Replace("/", "\\");
                    

                    if (Path.GetExtension(fileName).Length == 0)
                        fileName += ".png";

                    string[] spli = fileName.Split('\\');
                    bool exists = false;

                    string fname = "";
                    if (spli.Length > 0)
                    {
                        fileName = spli[spli.Length - 1];
                        if (File.Exists(this.DirectoryName + @"\" + fileName))
                        {
                            fname = this.DirectoryName + @"\" + fileName;
                            exists = true;
                        }
                    }

                    if (!exists && File.Exists(fileName))
                    {
                        fname = fileName;
                        exists = true;
                    }

                    this.materialNames.Add(materialID);
                    this.materialFileNames.Add(fname);
                }

            var wrapnode = this.Document.SelectNodes("//technique[@profile='MAYA']");
            bool wrapUV = wrapnode != null && wrapnode.Count > 0;
            geometries_ = this.Document.SelectNodes("//library_geometries/geometry");
            for (int i = 0; i < geometries_.Count; i++)
            {
                XmlNode mesh = geometries_[i].SelectNodes("mesh")[0];
                this.GeometryIDs.Add(geometries_[i].SelectNodes("@id")[0].InnerText);

                XmlNodeList inputs = mesh.SelectNodes("*/input[@semantic]");


                string vertexID = "";
                string normalID = "";
                string texcoordID = "";
                string colorID = "";

                vertexTriInd.Add(-1);
                normalTriInd.Add(-1);
                uvTriInd.Add(-1);
                colorTriInd.Add(-1);
                int stride = 0;
                for (int j = 0; j < inputs.Count; j++)
                {
                    string semanticAtt = inputs[j].SelectNodes("@semantic")[0].InnerText.ToUpper();
                    XmlNodeList offsetAtt = inputs[j].SelectNodes("@offset");

                    switch (semanticAtt)
                    {
                        case "VERTEX":
                            vertexTriInd[vertexTriInd.Count - 1] = 0;
                            if (offsetAtt != null && offsetAtt.Count > 0)
                            {
                                vertexTriInd[vertexTriInd.Count - 1] = int.Parse(offsetAtt[0].InnerText);
                                stride++;
                            }
                            break;
                        case "POSITION":
                            vertexID = inputs[j].SelectNodes("@source")[0].InnerText.Remove(0, 1);
                            break;
                        case "NORMAL":
                            normalTriInd[normalTriInd.Count - 1] = 0;
                            if (offsetAtt != null && offsetAtt.Count > 0)
                            {
                                normalTriInd[normalTriInd.Count - 1] = int.Parse(offsetAtt[0].InnerText);
                                stride++;
                            }
                            normalID = inputs[j].SelectNodes("@source")[0].InnerText.Remove(0, 1);
                            break;
                        case "TEXCOORD":
                            uvTriInd[uvTriInd.Count - 1] = 0;
                            if (offsetAtt != null && offsetAtt.Count > 0)
                            {
                                uvTriInd[uvTriInd.Count - 1] = int.Parse(offsetAtt[0].InnerText);
                                stride++;
                            }
                            texcoordID = inputs[j].SelectNodes("@source")[0].InnerText.Remove(0, 1);
                            break;
                        case "COLOR":
                            colorTriInd[colorTriInd.Count - 1] = 0;
                            if (offsetAtt != null && offsetAtt.Count > 0)
                            {
                                colorTriInd[colorTriInd.Count - 1] = int.Parse(offsetAtt[0].InnerText);
                                stride++;
                            }
                            colorID = inputs[j].SelectNodes("@source")[0].InnerText.Remove(0, 1);
                            break;
                    }
                }

                if (vertexID.Length == 0)
                    throw new Exception("Error: Mesh " + i + " does not have vertices. Remove this mesh before to use this tool.");

                string[] vertexArray = new string[0];
                string[] normalArray = new string[0];
                string[] texcoordArray = new string[0];
                string[] colorArray = new string[0];

                if (vertexID.Length > 0)
                    vertexArray = Format(mesh.SelectNodes(@"source[@id='" + vertexID + "']/float_array")[0].InnerText).Split(' ');
                if (normalID.Length > 0)
                    normalArray = Format(mesh.SelectNodes(@"source[@id='" + normalID + "']/float_array")[0].InnerText).Split(' ');
                if (texcoordID.Length > 0)
                    texcoordArray = Format(mesh.SelectNodes(@"source[@id='" + texcoordID + "']/float_array")[0].InnerText).Split(' ');
                if (colorID.Length > 0)
                    colorArray = Format(mesh.SelectNodes(@"source[@id='" + colorID + "']/float_array")[0].InnerText).Split(' ');

                this.Vertex.Add(new List<SlimDX.Vector3>(0));

                this.Normal.Add(new List<SlimDX.Vector3>(0));
                this.UV.Add(new List<SlimDX.Vector2>(0));
                this.UV_repeat.Add(false);
                this.VertexColor.Add(new List<byte[]>(0));

                this.Triangle.Add(new List<int[]>(0));

                this.Influences.Add(new List<List<float>>(0));
                this.InfluencesIndices.Add(new List<List<int>>(0));

                for (int j = 0; j < vertexArray.Length; j += 3)
                {
                    float x = Single.Parse(vertexArray[j ]);
                    float y = Single.Parse(vertexArray[j + 1]);
                    float z = Single.Parse(vertexArray[j + 2]);

                    this.Vertex.Last().Add(new SlimDX.Vector3(x,y,z));
                }

                for (int j = 0; j < normalArray.Length; j += 3)
                    this.Normal.Last().Add(new SlimDX.Vector3(Single.Parse(normalArray[j]), Single.Parse(normalArray[j + 1]), Single.Parse(normalArray[j + 2])));

                for (int j = 0; j < texcoordArray.Length; j += 2)
                {
                    SlimDX.Vector2 uv = (Form1.swapUV) ? new SlimDX.Vector2(Single.Parse(texcoordArray[j]), 1 - Single.Parse(texcoordArray[j + 1])):
                            new SlimDX.Vector2(Single.Parse(texcoordArray[j]), Single.Parse(texcoordArray[j + 1]));

                    if (uv.X<0||uv.X>1||uv.Y<0||uv.X>1)
                    {
                        this.UV_repeat[this.UV_repeat.Count-1] = true;
                    }
                    this.UV.Last().Add(uv);
                }

                for (int j = 0; j < colorArray.Length; j += 4)
                    this.VertexColor.Last().Add(new byte[] {
                        (byte)(Single.Parse(colorArray[j]) * 128) ,
                        (byte)(Single.Parse(colorArray[j + 1]) * 128),
                        (byte)(Single.Parse(colorArray[j + 2]) * 128),
                        (byte)(Single.Parse(colorArray[j + 3]) * 128)});

                string[] triangleArray = Format(mesh.SelectNodes(@"triangles/p")[0].InnerText).Split(' ');

                for (int j = 0; j < triangleArray.Length; j += stride)
                {
                    int[] indices = new int[stride];
                    for (int k = 0; k < stride; k++)
                        indices[k] = int.Parse(triangleArray[j + k]);

                    this.Triangle.Last().Add(indices);
                }

            }

            this.Bones = new Bone[0];
            this.ComputedBones = new Bone[0];
            var bonesNode = this.Document.SelectNodes(@"//library_visual_scenes/visual_scene//node[@type='JOINT']"); // and not(contains(@name,'mesh'))

            if (bonesNode != null && bonesNode.Count > 0)
            {
                this.Bones = new Bone[bonesNode.Count];
                this.ComputedBones = new Bone[bonesNode.Count];

                bonesNode = this.Document.SelectNodes(@"//library_visual_scenes/visual_scene/node[@type='JOINT']");
                for (int i=0;i< bonesNode.Count;i++)
                {
                    XmlNode bone000 = bonesNode[i];
                    GetBoneNames(bone000);
                    GetBones(bone000);
                    UnwrapSkeleton(bone000, -1);
                }
                ComputeMatrices();

                bool skeletonDefined = false;
                if (Form1.reuseSkeleton && SrkBinary.GetBytes(this.DirectoryName+@"\"+Path.GetFileNameWithoutExtension(this.Name)+".mdlx"))
                {
                    SrkBinary originalBin = new SrkBinary();
                    int mx = originalBin.ReadInt(4,false);
                    for (int i=0;i< mx;i++)
                    {
                        if (originalBin.ReadShort(0x10+i*0x10,false)==4)
                        {
                            originalBin.Pointer = originalBin.ReadInt(0x18 + i * 0x10, false)+0x90;
                            skeletonDefined = true;
                            break;
                        }
                    }
                    int bonescount = originalBin.ReadShort(0x10,true);
                    int bonesAddress = originalBin.Pointer + originalBin.ReadInt(0x14,true);
                    Form1.binarySkeleton = new SrkBinary(new byte[0x40 * bonescount]);
                    Array.Copy(originalBin.Buffer, bonesAddress, Form1.binarySkeleton.Buffer, 0, Form1.binarySkeleton.Buffer.Length);
                    

                    for (int j = 0; j < this.Bones.Length; j++)
                    {
                        this.Bones[j] = new Bone(Form1.binarySkeleton.ReadShort(j * 0x40, false));
                        this.ComputedBones[j] = new Bone(Form1.binarySkeleton.ReadShort(j * 0x40, false));

                        short parentIndex = Form1.binarySkeleton.ReadShort(4 + j * 0x40, false);
                        if (parentIndex > -1)
                        {
                            this.ComputedBones[j].Parent = this.ComputedBones[parentIndex];
                        }
                    }
                    Bone.ComputeMatrices(this.ComputedBones, this.ComputedBones[0]);
                }
                if (!skeletonDefined)
                {
                    Form1.binarySkeleton = new SrkBinary(new byte[0x40 * this.Bones.Length]);
                    for (int i = 0; i < this.Bones.Length; i++)
                    {
                        SlimDX.Matrix mat = this.Bones[i].Matrice;

                        Form1.binarySkeleton.WriteInt(i * 0x40, i, false);
                        if (this.Bones[i].Parent != null)
                            Form1.binarySkeleton.WriteInt(4 + i * 0x40, this.Bones[i].Parent.ID, false);
                        else
                            Form1.binarySkeleton.WriteInt(4 + i * 0x40, -1, false);

                        SlimDX.Vector3 scale = SlimDX.Vector3.Zero;
                        SlimDX.Vector3 translate = SlimDX.Vector3.Zero;
                        SlimDX.Quaternion q = SlimDX.Quaternion.Identity;


                        mat.Decompose(out scale, out q, out translate);

                        Form1.binarySkeleton.WriteFloat(0x10 + i * 0x40, 1, false);
                        Form1.binarySkeleton.WriteFloat(0x14 + i * 0x40, 1, false);
                        Form1.binarySkeleton.WriteFloat(0x18 + i * 0x40, 1, false);

                        OpenTK.Matrix4 mq = OpenTK.Matrix4.CreateFromQuaternion(new OpenTK.Matrix4(mat.M11, mat.M12, mat.M13, mat.M14, mat.M21, mat.M22, mat.M23, mat.M24, mat.M31, mat.M32, mat.M33, mat.M34, mat.M41, mat.M42, mat.M43, mat.M44).ExtractRotation());

                        double sy = Math.Sqrt(mq.M11 * mq.M11 + mq.M12 * mq.M12);
                        bool singular = sy < 1e-6;
                        float rotX, rotY, rotZ;

                        if (!singular)
                        {
                            rotX = (float)(Math.Atan2(mq.M23, mq.M33));
                            rotY = (float)(Math.Atan2(-mq.M13, sy));
                            rotZ = (float)(Math.Atan2(mq.M12, mq.M11));
                        }
                        else
                        {
                            rotX = (float)(Math.Atan2(-mq.M32, mq.M22));
                            rotY = (float)(Math.Atan2(-mq.M13, sy));
                            rotZ = 0;
                        }

                        Form1.binarySkeleton.WriteFloat(0x20 + i * 0x40, rotX, false);
                        Form1.binarySkeleton.WriteFloat(0x24 + i * 0x40, rotY, false);
                        Form1.binarySkeleton.WriteFloat(0x28 + i * 0x40, rotZ, false);

                        Form1.binarySkeleton.WriteFloat(0x30 + i * 0x40, translate.X * Form1.scale, false);
                        Form1.binarySkeleton.WriteFloat(0x34 + i * 0x40, translate.Y * Form1.scale, false);
                        Form1.binarySkeleton.WriteFloat(0x38 + i * 0x40, translate.Z * Form1.scale, false);
                    }

                }
            }
            Console.WriteLine(Bones.Length);
            skinnings_ = this.Document.SelectNodes(@"//library_controllers/controller/skin");
            for (int i = 0; i < skinnings_.Count; i++)
            {
                var sourceNodes = skinnings_[i].SelectNodes("@source");
                if (sourceNodes == null || sourceNodes.Count == 0) continue;
                int geometryIndex = this.GeometryIDs.IndexOf(sourceNodes[0].InnerText.Remove(0, 1));
                if (geometryIndex < 0)
                    continue;

                string jointID = skinnings_[i].SelectNodes(@"*/input[@semantic='JOINT']/@source")[0].InnerText.Remove(0, 1);
                string[] jointsArray = Format(skinnings_[i].SelectNodes(@"source[@id='" + jointID + "']/Name_array")[0].InnerText).Split(' ');
                string[] skinCounts = Format(skinnings_[i].SelectNodes(@"vertex_weights/vcount")[0].InnerText).Split(' ');
                string[] skins = Format(skinnings_[i].SelectNodes(@"vertex_weights/v")[0].InnerText).Split(' ');
                string[] skinsWeights = Format(skinnings_[i].SelectNodes(@"source[contains(@id, 'eights')]/float_array")[0].InnerText).Split(' ');

                int tabIndex = 0;

                for (int j = 0; j < skinCounts.Length; j++)
                {
                    int influenceCount = int.Parse(skinCounts[j]);

                    this.Influences[geometryIndex].Add(new List<float>(0));
                    this.InfluencesIndices[geometryIndex].Add(new List<int>(0));

                    for (int k = 0; k < influenceCount; k++)
                    {
                        int jointIndex = int.Parse(skins[tabIndex]);
                        int weightIndex = int.Parse(skins[tabIndex + 1]);
                        float weight = Single.Parse(skinsWeights[weightIndex]);

                        string jointName = jointsArray[jointIndex];

                        this.Influences[geometryIndex].Last().Add(weight);
                        this.InfluencesIndices[geometryIndex].Last().Add(BoneNames.IndexOf(jointName));
                        
                        tabIndex += 2;
                    }

                }
            }
        }
        public void MakeMDLX()
        {

            SlimDX.Vector3 min = new SlimDX.Vector3(Single.MaxValue, Single.MaxValue, Single.MaxValue);
            SlimDX.Vector3 max = new SlimDX.Vector3(Single.MinValue, Single.MinValue, Single.MinValue);

            for (int i = 0; i < this.Vertex.Count; i++)
            {
                for (int j = 0; j < this.Vertex[i].Count; j++)
                {
                    if (this.Vertex[i][j].X < min.X) min.X = this.Vertex[i][j].X;
                    if (this.Vertex[i][j].X > max.X) max.X = this.Vertex[i][j].X;

                    if (this.Vertex[i][j].Y < min.Y) min.Y = this.Vertex[i][j].Y;
                    if (this.Vertex[i][j].Y > max.Y) max.Y = this.Vertex[i][j].Y;

                    if (this.Vertex[i][j].Z < min.Z) min.Z = this.Vertex[i][j].Z;
                    if (this.Vertex[i][j].Z > max.Z) max.Z = this.Vertex[i][j].Z;
                }

            }

            Form1.binarySkeletonDefinitions.WriteFloat(0, min.X, false);
            Form1.binarySkeletonDefinitions.WriteFloat(4, min.Y, false);
            Form1.binarySkeletonDefinitions.WriteFloat(8, min.Z, false);

            Form1.binarySkeletonDefinitions.WriteFloat(0x10, max.X, false);
            Form1.binarySkeletonDefinitions.WriteFloat(0x14, max.Y, false);
            Form1.binarySkeletonDefinitions.WriteFloat(0x18, max.Z, false);


            if (this.Shadow)
                Form1.meshesShadow.Clear();
            else
                Form1.meshesModel.Clear();
            Console.WriteLine("Skinning done.");

            for (CurrentProcessingMesh = 0; CurrentProcessingMesh < this.Triangle.Count; CurrentProcessingMesh++)
            {
                if (Form1.checkBox8Checked)
                    for (int i = 0; i < this.Bones.Length; i++)
                    {
                        for (int j = 0; j < this.Triangle[CurrentProcessingMesh].Count; j += 3)
                        {
                            int[] first = Cloner(this.Triangle[CurrentProcessingMesh][j + 0]);
                            int[] second = Cloner(this.Triangle[CurrentProcessingMesh][j + 1]);
                            int[] third = Cloner(this.Triangle[CurrentProcessingMesh][j + 2]);

                            int vIndex1 = first[vertexTriInd[CurrentProcessingMesh]];
                            int vIndex2 = second[vertexTriInd[CurrentProcessingMesh]];
                            int vIndex3 = third[vertexTriInd[CurrentProcessingMesh]];
                            int max1 = 0;
                            int max2 = 0;
                            int max3 = 0;
                            for (int k = 0; k < this.InfluencesIndices[CurrentProcessingMesh][vIndex1].Count; k++)
                            {
                                if (this.Influences[CurrentProcessingMesh][vIndex1][k] >
                                    this.Influences[CurrentProcessingMesh][vIndex1][max1])
                                {
                                    max1 = k;
                                }
                            }
                            for (int k = 0; k < this.InfluencesIndices[CurrentProcessingMesh][vIndex2].Count; k++)
                            {
                                if (this.Influences[CurrentProcessingMesh][vIndex2][k] >
                                    this.Influences[CurrentProcessingMesh][vIndex2][max2])
                                {
                                    max2 = k;
                                }
                            }
                            for (int k = 0; k < this.InfluencesIndices[CurrentProcessingMesh][vIndex3].Count; k++)
                            {
                                if (this.Influences[CurrentProcessingMesh][vIndex3][k] >
                                    this.Influences[CurrentProcessingMesh][vIndex3][max3])
                                {
                                    max3 = k;
                                }
                            }
                            if (this.Influences[CurrentProcessingMesh][vIndex1][max1] == i ||
                                this.Influences[CurrentProcessingMesh][vIndex2][max2] == i ||
                                this.Influences[CurrentProcessingMesh][vIndex3][max3] == i)
                            {
                                this.Triangle[CurrentProcessingMesh].RemoveAt(j);
                                this.Triangle[CurrentProcessingMesh].RemoveAt(j);
                                this.Triangle[CurrentProcessingMesh].RemoveAt(j);
                                this.Triangle[CurrentProcessingMesh].Insert(0, third);
                                this.Triangle[CurrentProcessingMesh].Insert(0, second);
                                this.Triangle[CurrentProcessingMesh].Insert(0, first);
                            }
                        }
                    }

                mesh = new Mesh();
                mesh.ComputedBones = this.ComputedBones;
                mesh.Bones = this.Bones;
                mesh.Shadow = this.Shadow;

                float ini = (float)this.Triangle[CurrentProcessingMesh].Count;
                while (this.Triangle[CurrentProcessingMesh].Count > 0)
                {
                    Console.WriteLine("Mesh " + (CurrentProcessingMesh + 1) + "/" + this.Triangle.Count + "  " + Math.Round((1 - (this.Triangle[CurrentProcessingMesh].Count / ini)) * 100, 0) + " %");

                    int[] first = Cloner(this.Triangle[CurrentProcessingMesh][0]);
                    int[] second = Cloner(this.Triangle[CurrentProcessingMesh][1]);
                    int[] third = Cloner(this.Triangle[CurrentProcessingMesh][2]);

                    int vIndex1 = first[vertexTriInd[CurrentProcessingMesh]];
                    int vIndex2 = second[vertexTriInd[CurrentProcessingMesh]];
                    int vIndex3 = third[vertexTriInd[CurrentProcessingMesh]];

                    int uvIndex1 = -1;
                    int uvIndex2 = -1;
                    int uvIndex3 = -1;

                    if (!this.Shadow)
                    {
                        uvIndex1 = first[uvTriInd[CurrentProcessingMesh]];
                        uvIndex2 = second[uvTriInd[CurrentProcessingMesh]];
                        uvIndex3 = third[uvTriInd[CurrentProcessingMesh]];
                    }

                    int colorIndex1 = -1;
                    int colorIndex2 = -1;
                    int colorIndex3 = -1;

                    if (colorTriInd[CurrentProcessingMesh] > -1)
                    {
                        colorIndex1 = first[colorTriInd[CurrentProcessingMesh]];
                        colorIndex2 = second[colorTriInd[CurrentProcessingMesh]];
                        colorIndex3 = third[colorTriInd[CurrentProcessingMesh]];
                    }

                    for (int m = 0; m < 3; m++)
                    {
                        DoVIF(this.Triangle[CurrentProcessingMesh][0], CurrentProcessingMesh);
                        this.Triangle[CurrentProcessingMesh].RemoveAt(0);
                    }
                    mesh.Update(this.Triangle[CurrentProcessingMesh].Count == 0);

                    for (int j = 0; j < this.Triangle[CurrentProcessingMesh].Count; j += 3)
                    {
                        for (int h = 0; h < 3; h++)
                        {
                            int[] first_ = Cloner(this.Triangle[CurrentProcessingMesh][j + ((0 + h) % 3)]);
                            int[] second_ = Cloner(this.Triangle[CurrentProcessingMesh][j + ((1 + h) % 3)]);
                            int[] third_ = Cloner(this.Triangle[CurrentProcessingMesh][j + ((2 + h) % 3)]);

                            int vIndex1_ = first_[vertexTriInd[CurrentProcessingMesh]];
                            int vIndex2_ = second_[vertexTriInd[CurrentProcessingMesh]];
                            int vIndex3_ = third_[vertexTriInd[CurrentProcessingMesh]];

                            int uvIndex1_ = -1;
                            int uvIndex2_ = -1;
                            int uvIndex3_ = -1;
                            if (!this.Shadow)
                            {
                                uvIndex1_ = first_[uvTriInd[CurrentProcessingMesh]];
                                uvIndex2_ = second_[uvTriInd[CurrentProcessingMesh]];
                                uvIndex3_ = third_[uvTriInd[CurrentProcessingMesh]];
                            }

                            int colorIndex1_ = -1;
                            int colorIndex2_ = -1;
                            int colorIndex3_ = -1;

                            if (colorTriInd[CurrentProcessingMesh] > -1)
                            {
                                colorIndex1_ = first_[colorTriInd[CurrentProcessingMesh]];
                                colorIndex2_ = second_[colorTriInd[CurrentProcessingMesh]];
                                colorIndex3_ = third_[colorTriInd[CurrentProcessingMesh]];
                            }



                            if ((vIndex2_ == vIndex3 && vIndex3_ == vIndex2 &&
                                uvIndex2_ == uvIndex3 && uvIndex3_ == uvIndex2 &&
                                colorIndex2_ == colorIndex3 && colorIndex3_ == colorIndex2) ||
                                ((vIndex1_ == vIndex2 && vIndex2_ == vIndex1 &&
                                uvIndex1_ == uvIndex2 && uvIndex2_ == uvIndex1 &&
                                colorIndex1_ == colorIndex2 && colorIndex2_ == colorIndex1)))
                            {
                                DoVIF(first_, CurrentProcessingMesh);
                                DoVIF(second_, CurrentProcessingMesh);
                                DoVIF(third_, CurrentProcessingMesh);

                                for (int m = 0; m < 3; m++)
                                {
                                    //DoVIF(this.Triangle[CurrentProcessingMesh][0], CurrentProcessingMesh);
                                    this.Triangle[CurrentProcessingMesh].RemoveAt(j);
                                }
                                mesh.Update(this.Triangle[CurrentProcessingMesh].Count == 0);

                                j = 0;
                                vIndex1 = vIndex1_;
                                vIndex2 = vIndex2_;
                                vIndex3 = vIndex3_;

                                uvIndex1 = uvIndex1_;
                                uvIndex2 = uvIndex2_;
                                uvIndex3 = uvIndex3_;

                                colorIndex1 = colorIndex1_;
                                colorIndex2 = colorIndex2_;
                                colorIndex3 = colorIndex3_;

                                break;
                            }

                        }
                    }
                }

                meshRepeats.Add(this.UV_repeat[CurrentProcessingMesh]);
                if (this.Shadow)
                    Form1.meshesShadow.Insert(Form1.meshesShadow.Count, new SrkBinary(mesh.GetBytes()));
                else
                    Form1.meshesModel.Insert(Form1.meshesModel.Count, new SrkBinary(mesh.GetBytes()));
            }
            Console.WriteLine("Triangle strips done.");

            List<string> materialsFnames = new List<string>(0);

            for (int i = 0; i < this.Triangle.Count; i++)
            {
                string currController = "";
                for (int l = 0; l < skinnings_.Count; l++)
                {
                    var node = skinnings_[l].SelectNodes("@source");
                    if (node != null && node.Count > 0 && node[0].InnerText == "#" + this.GeometryIDs[i])
                    {
                        node = skinnings_[l].ParentNode.SelectNodes("@id");
                        if (node != null && node.Count > 0)
                        {
                            currController = node[0].InnerText;
                            break;
                        }
                    }
                }
                string matID = "";
                if (matID.Length == 0)
                {
                    XmlNodeList instanceGeometry = this.Document.SelectNodes("//library_visual_scenes/visual_scene/node/instance_geometry[@url='#" + GeometryIDs[i] + "']//instance_material/@target");
                    if (instanceGeometry != null && instanceGeometry.Count > 0)
                    {
                        matID = instanceGeometry[0].InnerText.Remove(0, 1);
                        if (!materialNames.Contains(matID))
                            matID = "";
                    }
                }
                if (matID.Length == 0)
                {
                    XmlNodeList instanceController = this.Document.SelectNodes("//library_visual_scenes/visual_scene/node/instance_controller[@url='#" + currController + "']//instance_material/@target");
                    if (instanceController != null && instanceController.Count > 0)
                    {
                        matID = instanceController[0].InnerText.Remove(0, 1);
                        if (!materialNames.Contains(matID))
                            matID = "";
                    }
                }

                if (matID.Length > 0)
                {
                    string fname = materialFileNames[materialNames.IndexOf(matID)];
                    int mmaterialIndex = materialsFnames.Count;

                    if (!materialsFnames.Contains(fname))
                        materialsFnames.Add(fname);
                    else
                        mmaterialIndex = materialsFnames.IndexOf(fname);

                    if (meshRepeats[i])
                    {
                        meshRepeatsIndices.Add(mmaterialIndex);
                    }
                    if (!this.Shadow)
                    {
                        Form1.meshesModel[i].WriteInt(0, 3, false);
                        Form1.meshesModel[i].WriteInt(4, mmaterialIndex, false);
                    }
                }

            }

            bool alreadyTexture = this.Shadow;
            if (Form1.reuseTim && SrkBinary.GetBytes(this.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(this.Name) + ".mdlx"))
            {
                SrkBinary bnr = new SrkBinary();
                int max_ = bnr.ReadInt(4, false);

                bool no7 = true;
                for (int i = 0; i < max_; i++)
                {
                    int type = bnr.ReadShort(0x10 + i * 0x10, false);
                    if (type == 7)
                    {
                        int address = bnr.ReadInt(0x18 + i * 0x10, false);
                        int length = bnr.ReadInt(0x1C + i * 0x10, false);
                        int length16 = length;
                        while (length16 % 16 > 0)
                            length16++;
                        for (int j = 0; j < Form1.types.Count; j++)
                        {
                            if (Form1.types[j] == 7)
                            {
                                no7 = false;
                                Form1.binaries[j] = new SrkBinary(Form1.SubArray(bnr.Buffer, address, length16));
                                Form1.lengthes[j] = length;
                            }
                        }
                        break;
                    }
                }
                if (no7)
                {
                    for (int i = 0; i < max_; i++)
                    {
                        int type = bnr.ReadShort(0x10 + i * 0x10, false);
                        if (type == 7)
                        {
                            int address = bnr.ReadInt(0x18 + i * 0x10, false);
                            int length = bnr.ReadInt(0x1C + i * 0x10, false);
                            int length16 = length;
                            while (length16 % 16 > 0)
                                length16++;
                            for (int j = 0; j < Form1.types.Count; j++)
                            {
                                if (Form1.types[j] == 4)
                                {
                                    no7 = false;
                                    Form1.binaries.Insert(j + 1, new SrkBinary(Form1.SubArray(bnr.Buffer, address, length16)));
                                    Form1.lengthes.Insert(j + 1, length);
                                    Form1.names.Insert(j + 1, "tim_");
                                    Form1.types.Insert(j + 1, 7);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                alreadyTexture = true;
            }

            if (!alreadyTexture)
            {
                if (materialsFnames.Count > 0 && File.Exists("MDLX_TimEditor.exe"))
                {
                    int goodIndex = -1;

                    List<int> possibleIndicesTemp = new List<int>(0);
                    List<int> possibleIndices = new List<int>(0);

                    int smallestCount = 1000;
                    int biggestAire = 0;
                    int recommendedIndex = 0;

                    for (int i = 0; i < Tims.Sizes.Length; i++)
                    {
                        if (Tims.Sizes[i].Length >= materialsFnames.Count)
                        {
                            possibleIndices.Add(i);
                        }

                        if (Tims.Sizes[i].Length >= materialsFnames.Count)
                        {
                            int aire = 0;
                            int sameratioAll = 0;
                            for (int j = 0; j < Tims.Sizes[i].Length; j++)
                            {
                                aire += Tims.Sizes[i][j].Width * Tims.Sizes[i][j].Height;
                                if (Tims.Sizes[i][j].Height == 256)
                                {
                                    sameratioAll++;
                                }
                            }
                            if (sameratioAll == Tims.Sizes[i].Length && Tims.Sizes[i].Length <= smallestCount)
                            {
                                smallestCount = Tims.Sizes[i].Length;
                                if (aire > biggestAire)
                                {
                                    recommendedIndex = i;
                                    biggestAire = aire;
                                }
                            }
                        }
                    }


                    for (int i = 0; i < possibleIndices.Count; i++)
                    {
                        for (int j = 0; j < Tims.Sizes[possibleIndices[i]].Length; j++)
                        {
                            System.Drawing.Size size = Tims.Sizes[possibleIndices[i]][j];
                            if (j < materialFileNames.Count)
                            {
                                /*FileStream fs = new FileStream(materialFileNames[j],FileMode.Open,FileAccess.Read,FileShare.Read);

                                Bitmap bmp = (Bitmap)Image.FromStream(fs);*/
                                if (size.Height < 256 && !Form1.checkBox6Checked)
                                {
                                    possibleIndices.RemoveAt(i);
                                    i--;
                                    break;
                                }
                                //fs.Close();
                            }
                        }
                    }
                    if (Form1.checkBox5Checked)
                    {
                        int selectedIndex = -1;

                        if (possibleIndices.Count == 1)
                        {
                            goodIndex = possibleIndices.Last();
                        }
                        else if (askAgainFileNames.Contains(this.Name))
                        {
                            goodIndex = askAgainChoices[askAgainFileNames.IndexOf(this.Name)];
                        }
                        else
                        {
                            Form form = new Form();
                            form.FormBorderStyle = FormBorderStyle.FixedDialog;
                            form.Size = new Size(1000, 440);
                            form.Text = "Select the texture sizes you wish to have.";
                            form.StartPosition = FormStartPosition.CenterParent;
                            form.Padding = new Padding(20, 20, 20, 50);
                            Panel pn = new Panel();
                            pn.MinimumSize = new Size(0, 300);

                            pn.Dock = DockStyle.Top;

                            Button btn = new Button();
                            btn.Dock = DockStyle.Top;
                            btn.MinimumSize = new Size(0, 40);
                            btn.Text = "Accept";

                            btn.Click += new EventHandler((object o, EventArgs e) => { form.Close(); });

                            CheckBox askAgain = new CheckBox();
                            askAgain.Dock = DockStyle.Top;
                            askAgain.Text = "Remember my choice for this file and session.";

                            pn.Location = new System.Drawing.Point(0, 0);
                            pn.BorderStyle = BorderStyle.FixedSingle;
                            form.Controls.Add(askAgain);
                            form.Controls.Add(btn);
                            form.Controls.Add(pn);


                            FlowLayoutPanel flp = new FlowLayoutPanel();
                            flp.Dock = DockStyle.Right;
                            flp.MinimumSize = new Size(800, 0);
                            flp.AutoScroll = true;
                            pn.Controls.Add(flp);

                            ListBox lb = new ListBox();
                            lb.Dock = DockStyle.Left;

                            for (int i = 0; i < possibleIndices.Count; i++)
                            {
                                lb.Items.Add("Combination " + i);
                            }
                            pn.Controls.Add(lb);

                            lb.SelectedIndexChanged += new EventHandler((object o, EventArgs e) => {
                                btn.Enabled = lb.SelectedIndex > -1;
                                if (btn.Enabled)
                                {
                                    flp.Controls.Clear();
                                    selectedIndex = lb.SelectedIndex;
                                    for (int i = 0; i < Tims.Sizes[possibleIndices[lb.SelectedIndex]].Length; i++)
                                    {
                                        PictureBox ptb = new PictureBox();
                                        ptb.BorderStyle = BorderStyle.FixedSingle;

                                        ptb.SizeMode = PictureBoxSizeMode.AutoSize;
                                        Bitmap img = new Bitmap(Tims.Sizes[possibleIndices[lb.SelectedIndex]][i].Width,
                                        Tims.Sizes[possibleIndices[lb.SelectedIndex]][i].Height);

                                        if (i < materialFileNames.Count)
                                        {
                                            FileStream fs = new FileStream(materialFileNames[i], FileMode.Open, FileAccess.Read, FileShare.Read);
                                            Bitmap bmp = (Bitmap)Image.FromStream(fs);
                                            Graphics gr = Graphics.FromImage(img);
                                            gr.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, img.Width, img.Height));
                                        }
                                        ptb.Image = img;
                                        flp.Controls.Add(ptb);
                                    }
                                }

                            });

                            lb.SelectedIndex = 0;
                            form.ShowDialog();

                            if (askAgain.Checked)
                            {
                                askAgainFileNames.Add(this.Name);
                                askAgainChoices.Add(possibleIndices[selectedIndex]);
                            }
                        }

                        if (selectedIndex > -1)
                            goodIndex = possibleIndices[selectedIndex];
                    }
                    else
                        goodIndex = recommendedIndex;

                    byte[]  TIM = new byte[Tims.outputLengthes[goodIndex]];

                    if (Tims.addresses[goodIndex] + Tims.Lengthes[goodIndex] > Tims.Binary.Length)
                        Tims.Lengthes[goodIndex] = Tims.Binary.Length - Tims.addresses[goodIndex];
                    Array.Copy(Tims.Binary, Tims.addresses[goodIndex], TIM, 0, Tims.Lengthes[goodIndex]);



                    //meshRepeatsIndices
                    SrkBinary bn = new SrkBinary(TIM);
                    int count = bn.ReadInt(0x8, false);
                    int off = bn.ReadInt(0x18, false);
                    for (int hh = 0; hh < count; hh++)
                    {
                        if (meshRepeatsIndices.Contains(hh) && Form1.checkBox9Checked)
                        {
                            bn.WriteInt(off + hh * 0xA0 + 0x80, (ushort)0x0FFF, false);
                            bn.WriteInt(off + hh * 0xA0 + 0x82, (ushort)0xFF00, false);

                            //bn.WriteInt(off + hh * 0xA0 + 0x84, 0, false);
                        }
                    }

                    bool replaced = false;
                    for (int i = 0; i < Form1.types.Count; i++)
                    {
                        if (Form1.types[i] == 7)
                        {
                            Form1.names[i] = "tim_";
                            Form1.binaries[i] = bn;


                            Form1.lengthes[i] = TIM.Length;
                            replaced = true;
                            break;
                        }
                    }
                    if (!replaced)
                    {
                        Form1.binaries.Add(bn);
                        Form1.types.Add(7);
                        Form1.names.Add("tim_");
                        Form1.lengthes.Add(TIM.Length);
                    }

                    Form1.Save(this.DirectoryName + @"\" + this.Name + "-fromDAE.Temp.mdlx");
                    Directory.CreateDirectory(this.DirectoryName + @"\" + this.Name + "-fromDAE.Temp-Extract");

                    for (int i = 0; i < Tims.Sizes[goodIndex].Length; i++)
                    {
                        Bitmap currBMP = new Bitmap(Tims.Sizes[goodIndex][i].Width, Tims.Sizes[goodIndex][i].Height);
                        if (i < materialsFnames.Count)
                        {
                            //Console.WriteLine(materialsFnames[i]);
                            Bitmap matBMP = (Bitmap)Image.FromFile(materialsFnames[i]);

                            Graphics gr = Graphics.FromImage(currBMP);
                            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                            gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                            gr.DrawImage(matBMP, 0, 0, currBMP.Width, currBMP.Height);
                        }
                        currBMP.Save(this.DirectoryName + @"\" + this.Name + @"-fromDAE.Temp-Extract\" + this.Name + "-fromDAE.Temp-" + i + ".png");
                    }
                    //Process.Start(this.Name + @".Temp-Extract\");
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
                    pc.StartInfo.Arguments = "-update \"" + this.DirectoryName + @"\" + this.Name + "-fromDAE.Temp.mdlx\"" + " \"" + this.DirectoryName + @"\" + this.Name + "-fromDAE.mdlx\"";
                    pc.Start();

                    new System.Threading.Thread(() =>
                    {
                        while (Process.GetProcessesByName("MDLX_TimEditor").Length > 0) { }

                        while (File.Exists(this.DirectoryName + @"\" + this.Name + "-fromDAE.Temp.mdlx"))
                        {
                            try
                            {
                                File.Delete(this.DirectoryName + @"\" + this.Name + "-fromDAE.Temp.mdlx");
                            }
                            catch
                            {

                            }
                        }
                        while (Directory.Exists(this.DirectoryName + @"\" + this.Name + "-fromDAE.Temp-Extract"))
                        {
                            try
                            {
                                Directory.Delete(this.DirectoryName + @"\" + this.Name + "-fromDAE.Temp-Extract", true);
                            }
                            catch
                            {

                            }
                        }
                        //SrkBinary.GetBytes(this.DirectoryName + @"\" + this.Name + "-fromDAE.mdlx");
                        //SrkBinary bin = new SrkBinary();


                        System.Diagnostics.Process.Start(this.DirectoryName + @"\" + this.Name + "-fromDAE.mdlx");
                    }
                    ).Start();
                }
            }
            else
            {
                new System.Threading.Thread(() =>
                {
                    Stopwatch stp = new Stopwatch();
                    stp.Start();
                    while (stp.Elapsed.Seconds < 2 && !File.Exists(this.DirectoryName + @"\" + this.Name + "-fromDAE.mdlx")) { }
                    if (File.Exists(this.DirectoryName + @"\" + this.Name + "-fromDAE.mdlx"))
                        System.Diagnostics.Process.Start(this.DirectoryName + @"\" + this.Name + "-fromDAE.mdlx");
                }).Start();
            }
        }

        public static List<string> askAgainFileNames = new List<string>(0);
        public static List<int> askAgainChoices = new List<int>(0);


        Mesh mesh;

        public void DoVIF(int[] index, int CurrentProcessingMesh)
        {
            int vertexIndex = -1;
            if (vertexTriInd[CurrentProcessingMesh] > -1)
                vertexIndex = index[vertexTriInd[CurrentProcessingMesh]];

            int normalIndex = -1;
            if (normalTriInd[CurrentProcessingMesh] > -1)
                normalIndex = index[normalTriInd[CurrentProcessingMesh]];

            int uvIndex = -1;
            if (uvTriInd[CurrentProcessingMesh] > -1)
                uvIndex = index[uvTriInd[CurrentProcessingMesh]];

            int colorIndex = -1;
            if (uvTriInd[CurrentProcessingMesh] > -1 && colorTriInd[CurrentProcessingMesh] > -1)
                colorIndex = index[colorTriInd[CurrentProcessingMesh]];

            SlimDX.Vector3 vertex = SlimDX.Vector3.Zero;
            SlimDX.Vector3 normal = SlimDX.Vector3.Zero;
            SlimDX.Vector2 uv = SlimDX.Vector2.Zero;

            byte[] color = new byte[0];

            if (vertexIndex > -1)
            {
                vertex = this.Vertex[CurrentProcessingMesh][vertexIndex];

                if (normalIndex > -1)
                    normal = this.Normal[CurrentProcessingMesh][normalIndex];

                if (uvIndex > -1)
                    uv = this.UV[CurrentProcessingMesh][uvIndex];

                if (colorIndex > -1)
                {
                    color = this.VertexColor[CurrentProcessingMesh][colorIndex];
                    mesh.AddColor(color);
                }


                mesh.AddVertex(vertex, vertexIndex);
                mesh.AddUV(uv);

                List<int> mats = new List<int>(0);
                List<float> matInfs = new List<float>(0);

                //mesh.AddInfluence(0, 1f);
                for (int f = 0; f < this.InfluencesIndices[CurrentProcessingMesh][vertexIndex].Count; f++)
                {
                    int mat = this.InfluencesIndices[CurrentProcessingMesh][vertexIndex][f];
                    float inf = this.Influences[CurrentProcessingMesh][vertexIndex][f];
                    if (inf>0)
                    mesh.AddInfluence(mat, inf);
                }
            }
        }



        public int CurrentProcessingMesh = 0;

        List<int> alreadyIndicies = new List<int>(0);

        public int[] Cloner(int[] input)
        {
            int[] output = new int[input.Length];

            Array.Copy(input, output, input.Length);
            return output;
        }
        //public float scaleVert = 1;
        
        public Bone[] Bones;
        public Bone[] ComputedBones;

        public void ComputeMatrices()
        {
            for (int i = 0; i < this.ComputedBones.Length; i++)
            {
                SlimDX.Matrix mat = this.ComputedBones[i].Matrice;
                for (int j = 0; j < this.ComputedBones.Length; j++)
                {
                    if (j == i) continue;
                    if (this.ComputedBones[j]!=null&&this.ComputedBones[j].Parent != null && this.ComputedBones[j].Parent.ID == this.ComputedBones[i].ID)
                    {
                        this.ComputedBones[j].Matrice *= mat;
                    }
                }
            }
        }
        List<string> BoneNames = new List<string>(0);

        public void GetBoneNames(XmlNode node)
        {
            string name = GetBoneName(node);
            if (!BoneNames.Contains(name))
            {
                BoneNames.Add(name);
            }
            XmlNodeList bones = node.SelectNodes("node");
            for (int i = 0; i < bones.Count; i++)
            {
                GetBoneNames(bones[i]);
            }
        }

        public void GetBones(XmlNode node)
        {
            short id = GetBoneID(node);
            this.Bones[id] = new Bone(id);
            this.Bones[id].Matrice = GetBoneMatrix(node);
            this.ComputedBones[id] = new Bone(id);
            this.ComputedBones[id].Matrice = GetBoneMatrix(node);

            XmlNodeList bones = node.SelectNodes("node");
            for (int i = 0; i < bones.Count; i++)
            {
                GetBones(bones[i]);
            }
        }

        public void UnwrapSkeleton(XmlNode node, int parentID)
        {
            short id = GetBoneID(node);
            if (parentID > -1)
            {
                this.Bones[id].Parent = this.Bones[parentID];
                this.ComputedBones[id].Parent = this.ComputedBones[parentID];
            }

            XmlNodeList bones = node.SelectNodes("node");
            for (int i = 0; i < bones.Count; i++)
            {
                UnwrapSkeleton(bones[i], id);
            }
        }

        public short GetBoneID(XmlNode node)
        {
            short ind = (short)this.BoneNames.IndexOf(GetBoneName(node));
            return ind;
        }

        public string GetBoneName(XmlNode node)
        {
            for (int i = 0; i < node.Attributes.Count; i++)
                if (node.Attributes[i].Name.ToLower() == "name")
                {
                    return node.Attributes[i].InnerText;
                }
            return "";
        }


        public SlimDX.Matrix GetBoneMatrix(XmlNode node)
        {
            SlimDX.Matrix mat = SlimDX.Matrix.Identity;
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].Name.ToLower() == "matrix")
                {
                    string[] inner = Format(node.ChildNodes[i].InnerText).Split(' ');
                    mat.M11 = Single.Parse(inner[0]);
                    mat.M21 = Single.Parse(inner[1]);
                    mat.M31 = Single.Parse(inner[2]);
                    mat.M41 = Single.Parse(inner[3]);
                    mat.M12 = Single.Parse(inner[4]);
                    mat.M22 = Single.Parse(inner[5]);
                    mat.M32 = Single.Parse(inner[6]);
                    mat.M42 = Single.Parse(inner[7]);
                    mat.M13 = Single.Parse(inner[8]);
                    mat.M23 = Single.Parse(inner[9]);
                    mat.M33 = Single.Parse(inner[10]);
                    mat.M43 = Single.Parse(inner[11]);
                    mat.M14 = Single.Parse(inner[12]);
                    mat.M24 = Single.Parse(inner[13]);
                    mat.M34 = Single.Parse(inner[14]);
                    mat.M44 = Single.Parse(inner[15]);
                    /*mat.M11 = Single.Parse(inner[0]);
                    mat.M12 = Single.Parse(inner[1]);
                    mat.M13 = Single.Parse(inner[2]);
                    mat.M14 = Single.Parse(inner[3]);
                    mat.M21 = Single.Parse(inner[4]);
                    mat.M22 = Single.Parse(inner[5]);
                    mat.M23 = Single.Parse(inner[6]);
                    mat.M24 = Single.Parse(inner[7]);
                    mat.M31 = Single.Parse(inner[8]);
                    mat.M32 = Single.Parse(inner[9]);
                    mat.M33 = Single.Parse(inner[10]);
                    mat.M34 = Single.Parse(inner[11]);
                    mat.M41 = Single.Parse(inner[12]);
                    mat.M42 = Single.Parse(inner[13]);
                    mat.M43 = Single.Parse(inner[14]);
                    mat.M44 = Single.Parse(inner[15]);*/
                    break;
                }
            }
            return mat;
        }
    }
}
