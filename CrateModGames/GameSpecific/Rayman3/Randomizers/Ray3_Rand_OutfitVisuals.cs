using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_OutfitVisuals : ModStruct<TPL_File>
    {
        public override void ModPass(TPL_File file)
        {

            if (file.Name.ToLower().Contains("fix.tpl"))
            {
                Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
                string basePath = file.FolderName;

                //Shuffle outfit textures
                string[] outfitFiles = new string[] { "fix.tpl.mm7.png", "fix.tpl.mm8.png", "fix.tpl.mm9.png", "fix.tpl.mm10.png", "fix.tpl.mm11.png" };
                File.Move(basePath + outfitFiles[0], basePath + "outfit0.png");
                File.Move(basePath + outfitFiles[1], basePath + "outfit1.png");
                File.Move(basePath + outfitFiles[2], basePath + "outfit2.png");
                File.Move(basePath + outfitFiles[3], basePath + "outfit3.png");
                File.Move(basePath + outfitFiles[4], basePath + "outfit4.png");
                string[] copterFiles = new string[] { "fix.tpl.mm15.png", "fix.tpl.mm18.png", "fix.tpl.mm16.png", "fix.tpl.mm19.png", "fix.tpl.mm17.png" };
                File.Move(basePath + copterFiles[0], basePath + "copter0.png");
                File.Move(basePath + copterFiles[1], basePath + "copter1.png");
                File.Move(basePath + copterFiles[2], basePath + "copter2.png");
                File.Move(basePath + copterFiles[3], basePath + "copter3.png");
                File.Move(basePath + copterFiles[4], basePath + "copter4.png");

                List<int> RandList = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    RandList.Add(i);
                }
                int targetpos = 0;

                for (int i = 0; i < 5; i++)
                {
                    targetpos = randState.Next(0, RandList.Count);
                    File.Move(basePath + "outfit" + RandList[targetpos] + ".png", basePath + outfitFiles[i]);
                    File.Move(basePath + "copter" + RandList[targetpos] + ".png", basePath + copterFiles[i]);
                    RandList.RemoveAt(targetpos);
                }
            }
        }
    }
}
