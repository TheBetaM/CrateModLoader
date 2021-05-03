using System;
using System.Collections.Generic;
using CTRFramework;
using CTRFramework.Shared;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Rand_PantsColor : ModStruct<CtrModel>
    {
        private Random rand;
        private Color targetColor;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
            targetColor = Color.FromArgb(0, (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
            targetColor = Color.FromArgb(0, 0, 255, 0);
        }

        public override void ModPass(CtrModel model)
        {
            foreach(CtrMesh mesh in model.Entries)
            {
                mesh.scale = new Vector4s(0, 0, 0, 0);
                //if (mesh.name.Contains("crash"))
                //{
                    for (int i = 0; i < mesh.cols.Count; i++)
                    {
                        if (mesh.cols[i].Z > 0 && mesh.cols[i].X < 110 && mesh.cols[i].Y < 110)
                        {
                            float intensity = mesh.cols[i].Z / 255f;
                            mesh.cols[i] = new Vector4b((byte)(targetColor.R * intensity), (byte)(targetColor.G * intensity), (byte)(targetColor.B * intensity), 0);
                        }
                    }
                //}
            }
        }
    }
}
