using System;
using System.Collections.Generic;
using System.Xml;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv.Mods
{
    public class SMBA_Rand_ChallengeMode : ModStruct<XmlDocument>
    {
        public override void ModPass(XmlDocument xml)
        {
            if (xml.HasChildNodes && xml.FirstChild.Name == "settings")
            {
                Random rand = new Random(ModLoaderGlobals.RandomizerSeed);
                int Challenge_Time = 60 + rand.Next(0, 20); // max 87
                int Challenge_StartLives = 3 + rand.Next(-1, 7);
                int Challenge_MaxLives = 99;
                int Challenge_Continues = 5 + rand.Next(-2, 5);
                int Challenge_ExtraLife = 50 + rand.Next(-40, 10);
                int Challenge_BananaScore = 100 + rand.Next(-50, 150);

                xml["settings"]["challenge"]["time"].InnerText = Challenge_Time.ToString();
                xml["settings"]["challenge"]["start_lives"].InnerText = Challenge_StartLives.ToString();
                xml["settings"]["challenge"]["max_lives"].InnerText = Challenge_MaxLives.ToString();
                xml["settings"]["challenge"]["continues"].InnerText = Challenge_Continues.ToString();
                xml["settings"]["challenge"]["extra_life"].InnerText = Challenge_ExtraLife.ToString();
                xml["settings"]["challenge"]["banana_score"].InnerText = Challenge_BananaScore.ToString();
            }
            if (xml.HasChildNodes && xml.FirstChild.Name == "challengemode")
            {
                Random rand = new Random(ModLoaderGlobals.RandomizerSeed);
                int Count_Beginner = xml["challengemode"]["beginner"].ChildNodes.Count;
                int Count_Advanced = xml["challengemode"]["advanced"].ChildNodes.Count;
                int Count_Expert = xml["challengemode"]["expert"].ChildNodes.Count;
                int Count_PSP = xml["challengemode"]["psp"].ChildNodes.Count;

                List<XmlNode> LevelNodes = new List<XmlNode>();
                foreach (XmlNode node in xml["challengemode"]["beginner"].ChildNodes)
                {
                    LevelNodes.Add(node);
                }
                foreach (XmlNode node in xml["challengemode"]["advanced"].ChildNodes)
                {
                    LevelNodes.Add(node);
                }
                foreach (XmlNode node in xml["challengemode"]["expert"].ChildNodes)
                {
                    LevelNodes.Add(node);
                }
                foreach (XmlNode node in xml["challengemode"]["psp"].ChildNodes)
                {
                    LevelNodes.Add(node);
                }

                xml["challengemode"]["beginner"].RemoveAll();
                xml["challengemode"]["advanced"].RemoveAll();
                xml["challengemode"]["expert"].RemoveAll();
                xml["challengemode"]["psp"].RemoveAll();

                int iter = 0;
                int r = 0;
                while (iter < Count_Beginner)
                {
                    r = rand.Next(LevelNodes.Count);
                    xml["challengemode"]["beginner"].AppendChild(LevelNodes[r]);
                    LevelNodes.RemoveAt(r);
                    iter++;
                }
                iter = 0;
                while (iter < Count_Advanced)
                {
                    r = rand.Next(LevelNodes.Count);
                    xml["challengemode"]["advanced"].AppendChild(LevelNodes[r]);
                    LevelNodes.RemoveAt(r);
                    iter++;
                }
                iter = 0;
                while (iter < Count_Expert)
                {
                    r = rand.Next(LevelNodes.Count);
                    xml["challengemode"]["expert"].AppendChild(LevelNodes[r]);
                    LevelNodes.RemoveAt(r);
                    iter++;
                }
                iter = 0;
                while (iter < Count_PSP)
                {
                    r = rand.Next(LevelNodes.Count);
                    xml["challengemode"]["psp"].AppendChild(LevelNodes[r]);
                    LevelNodes.RemoveAt(r);
                    iter++;
                }

            }
        }
    }
}
