using System;
using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor
{
    public class ParticleDataController : ItemController
    {
        public new ParticleData Data { get; set; }

        public ParticleDataController(MainForm topform, ParticleData item) : base(topform, item)
        {
            Data = item;
            //AddMenu("Open editor", Menu_OpenEditor);
        }

        protected override string GetName()
        {
            if (Data.IsStandalone)
            {
                return $"PTL File";
            }
            else
            {
                return $"Particle Data [ID {Data.ID}]";
            }
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();

            if (!Data.IsStandalone)
            {
                text.Add($"ID: {Data.ID}");
                text.Add($"Size: {Data.Size}");
            }
            else
            {
                text.Add($"Size: {Data.Size}");
            }

            if (Data.IsDefault)
            {
                text.Add(string.Format("Particle Bank 1: Material ID: {0:X8}; Texture ID: {1:X8}", Data.ParticleMaterialID_1, Data.ParticleTextureID_1));
                text.Add(string.Format("Particle Bank 2: Material ID: {0:X8}; Texture ID: {1:X8}", Data.ParticleMaterialID_2, Data.ParticleTextureID_2));
                text.Add(string.Format("Particle Bank 3: Material ID: {0:X8}; Texture ID: {1:X8}", Data.ParticleMaterialID_3, Data.ParticleTextureID_3));
                text.Add(string.Format("Decal Bank: Material ID: {0:X8}; Texture ID: {1:X8}", Data.DecalMaterialID, Data.DecalTextureID));
            }

            text.Add($"Particle Types: {Data.ParticleTypeCount}");
            if (Data.ParticleTypeCount > 0)
            {
                for (int i = 0; i < Data.ParticleTypeCount; i++)
                {
                    text.Add($"Name: {Data.ParticleTypes[i].Name} ");
                }
            }
            text.Add($"Particle Instances: {Data.ParticleInstanceCount}");
            if (Data.ParticleInstanceCount > 0)
            {
                for (int i = 0; i < Data.ParticleInstances.Count; i++)
                {
                    text.Add($"#{i} Name: {Data.ParticleInstances[i].Name} ");
                    //text.Add(string.Format("#{0} Ints: {1:X8}; {2:X8}; {3:X8}; {4:X8};", i, Data.ParticleInstances[i].UnkInt1, Data.ParticleInstances[i].UnkInt2, Data.ParticleInstances[i].UnkInt3, Data.ParticleInstances[i].UnkInt4));
                    text.Add($"#{i} Pos: {Data.ParticleInstances[i].Position.X}; {Data.ParticleInstances[i].Position.Y}; {Data.ParticleInstances[i].Position.Z}; {Data.ParticleInstances[i].Position.W} ");
                    text.Add($"#{i} Rot: {Data.ParticleInstances[i].Rot_X}; {Data.ParticleInstances[i].Rot_Y}; {Data.ParticleInstances[i].Rot_Z} ");
                    text.Add($"#{i} Vars: {Data.ParticleInstances[i].UnkZero}; {Data.ParticleInstances[i].UnkShorts[0]}; {Data.ParticleInstances[i].UnkShorts[1]}; {Data.ParticleInstances[i].UnkShorts[2]}; {Data.ParticleInstances[i].UnkShorts[3]}; {Data.ParticleInstances[i].UnkShorts[4]}; {Data.ParticleInstances[i].UnkShorts[5]}; " +
                        $"{Data.ParticleInstances[i].UnkShorts[6]}; {Data.ParticleInstances[i].UnkShorts[7]}; {Data.ParticleInstances[i].UnkShorts[8]}; {Data.ParticleInstances[i].UnkShorts[9]}; {Data.ParticleInstances[i].UnkShorts[10]}; {Data.ParticleInstances[i].UnkShorts[11]}; ");
                }
            }

            TextPrev = text.ToArray();
        }

        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor((ItemController)Node.Tag);
        }
    }
}