/* Paths.cs
 * namespace ColladaLoader
 * 
 * Class description:
 * This class is used for the configuration of the colladaloaders resource paths.
 * Just change the paths in the class or addapt the class so it returns you engine's paths
 *
 * SlimDX Colladaloader library
 * By Tom Boeckx
 * http://www.tomsdevshack.be
 * Copyright (c) 2010 
 * 
 * Released under GNU Gpl 
 * See 'http://www.gnu.org/licenses/gpl.html' for license details
 * 
 * Based upon Benjamin 'abi' Nitschke's article 'Skeletal Bone Animation and Skinning with Collada Models in XNA'
 * See 'http://abi.exdream.com/Blog/post/2007/02/25/Skeletal-Bone-Animation-and-Skinning-with-Collada-Models-in-XNA.aspx' for original article
 *
 * 
 * */
using System;
using System.Collections.Generic;
using System.Text;

namespace ColladaLoader
{
    static class Paths 
    {
        static public String Effects
        {
            get
            {
                return System.IO.Directory.GetCurrentDirectory()+"\\..\\..\\Effects\\";
            }
        }
        static public String Models
        {
            get
            {
                return System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\Models\\";
            }
        }
    }
}
