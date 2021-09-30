using Twinsanity;
using System.Collections.Generic;
using System;

namespace TwinsaityEditor
{
    public class CollisionSurfaceController : ItemController
    {
        public new CollisionSurface Data { get; set; }

        public CollisionSurfaceController(MainForm topform, CollisionSurface item) : base(topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            uint surf = Data.SurfaceID;
            if (MainFile.Data.Type == TwinsFile.FileType.MonkeyBallRM && Enum.IsDefined(typeof(DefaultEnums.SurfaceTypes_MonkeyBall), surf))
            {
                return (DefaultEnums.SurfaceTypes_MonkeyBall)surf + " [ID " + Data.ID.ToString() + "]";
            }
            else if (Enum.IsDefined(typeof(DefaultEnums.SurfaceTypes), surf))
            {
                return (DefaultEnums.SurfaceTypes)surf + " [ID " + Data.ID.ToString() + "]";
            }
            else
            {
                return string.Format("Collision Surface [ID {0:X8}]", Data.ID);
            }


        }

        protected override void GenText()
        {
            List<string> text = new List<string>();

            text.Add(string.Format("ID: {0:X8}", Data.ID));
            text.Add($"Size: {Data.Size}");

            uint surf = Data.SurfaceID;
            if (Enum.IsDefined(typeof(DefaultEnums.SurfaceTypes), surf))
            {
                text.Add($"Type: { (DefaultEnums.SurfaceTypes)surf }");
            }
            else
            {
                text.Add($"Type: { Data.SurfaceID }");
            }

            text.Add("Flags:");
            for (int i = 0; i < Data.Flags.Length; i++)
            {
                string flag = Convert.ToString(Data.Flags[i], 2);
                if (flag.Length < 8)
                {
                    while (flag.Length < 8)
                    {
                        flag = "0" + flag;
                    }
                }
                text.Add(flag);
            }
            for (int i = 0; i < Data.SoundIDs.Length; i++)
            {
                if (i == 0 || i == 1 || i == 4 || i == 7 || i == 8)
                {
                    text.Add($"Sound ID {i}: { Data.SoundIDs[i] }");
                }
                else
                {
                    text.Add($"Unk ID {i}: { Data.SoundIDs[i] }");
                }
            }
            for (int i = 0; i < Data.Floats.Length; i++)
            {
                text.Add($"Float Set {i}: { Data.Floats[i].X }; { Data.Floats[i].Y }; { Data.Floats[i].Z }; { Data.Floats[i].W }; ");
            }
            for (int i = 0; i < Data.UnkInts.Length; i++)
            {
                text.Add($"UnkInt {i}: { Data.UnkInts[i] }");
            }

            TextPrev = text.ToArray();

        }
    }
}