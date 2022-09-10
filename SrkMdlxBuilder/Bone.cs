using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SrkMdlxBuilder
{
    public class Bone
    {
        public float ScaleX
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x10, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x10, value, false); }
        }
        public float ScaleY
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x14, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x14, value, false); }
        }
        public float ScaleZ
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x18, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x18, value, false); }
        }

        public float RotateX
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x20, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x20, value, false); }
        }
        public float RotateY
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x24, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x24, value, false); }
        }
        public float RotateZ
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x28, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x28, value, false); }
        }

        public float TranslateX
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x30, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x30, value, false); }
        }
        public float TranslateY
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x34, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x34, value, false); }
        }
        public float TranslateZ
        {
            get { return Form1.binarySkeleton.ReadFloat(this.ID * 0x40 + 0x38, false); }
            set { Form1.binarySkeleton.WriteFloat(this.ID * 0x40 + 0x38, value, false); }
        }

        public SlimDX.Quaternion Quaternion { get; set; }
        public SlimDX.Vector3 Vecteur { get; set; }
        public SlimDX.Matrix Matrice { get; set; }
        public bool IsSkinned { get; set; }

        public Bone Parent { get; set; }
        public short ID { get; set; }

        public static void ComputeMatrices(Bone[] bones, Bone bone)
        {
            SlimDX.Quaternion Qo;
            SlimDX.Vector3 Vo;

            if (bone.Parent == null)
            {
                Qo = bone.Quaternion;
                Vo = bone.Vecteur;
            }
            else
            {
                Qo = bone.Parent.Quaternion;
                Vo = bone.Parent.Vecteur;
            }

            SlimDX.Vector3 Vt = SlimDX.Vector3.TransformCoordinate(new SlimDX.Vector3(bone.TranslateX, bone.TranslateY, bone.TranslateZ), SlimDX.Matrix.RotationQuaternion(Qo));
            bone.Vecteur = Vo + Vt;

            SlimDX.Quaternion Qt = SlimDX.Quaternion.Identity;

            if (Math.Abs(bone.RotateX) > 0) Qt = SlimDX.Quaternion.Multiply(Qt, SlimDX.Quaternion.RotationAxis(new SlimDX.Vector3(1, 0, 0), bone.RotateX));
            if (Math.Abs(bone.RotateY) > 0) Qt = SlimDX.Quaternion.Multiply(Qt, SlimDX.Quaternion.RotationAxis(new SlimDX.Vector3(0, 1, 0), bone.RotateY));
            if (Math.Abs(bone.RotateZ) > 0) Qt = SlimDX.Quaternion.Multiply(Qt, SlimDX.Quaternion.RotationAxis(new SlimDX.Vector3(0, 0, 1), bone.RotateZ));
            
            bone.Quaternion = Qt * Qo;
            bone.Matrice = SlimDX.Matrix.RotationQuaternion(bone.Quaternion) * SlimDX.Matrix.Translation(bone.Vecteur);

            for (int i = 1; i < bones.Length; i++)
            {
                if (bones[i] != null & bones[i].Parent != null && bones[i].Parent.ID == bone.ID)
                    ComputeMatrices(bones, bones[i]);
            }
        }
        
        public Bone(short id_)
        {
            this.ID = id_;
            this.Parent = null;
            this.Quaternion = SlimDX.Quaternion.Identity;
            this.Vecteur = SlimDX.Vector3.Zero;
            this.Matrice = SlimDX.Matrix.Identity;
        }

    }
}
