using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public class Titans_Rand_CutsceneCostumes : ModStruct<GenericModStruct>
    {
        private Random rand;

        private List<string> Costume_Battler = new List<string>()
        {
            "17ed8fa",
            "17ed8fb",
            "17ed8fc",
            "17ed8fd",
            "17ed8fe",
            "17ed900",
            "17ed901",
            "17ed902",
            "17ed918",
            "17ed91a",
            "17ed91b",
            "2e5c4ac6",
            "17ed91e",
            "17ed91f",
            "17ed920",
            "17ed938",
            "17ed93b",
            "17ed93e",
            "17ed95b",
        };
        private List<string> Costume_Bratgirl = new List<string>()
        {
            "98025b7b",
            "98025b7c",
            "98025b7d",
            "98025b7e",
            "98025b7f",
            "98025b81",
            "98025b82",
            "98025b83",
            "98025b99",
            "98025b9b",
            "98025b9c",
            "68491865",
            "98025b9f",
            "98025ba0",
            "98025ba1",
            "98025bb9",
            "98025bbc",
            "98025bbf",
            "98025bdc",
        };
        private List<string> Costume_Chiratta = new List<string>()
        {
            "37906b0c",
            "37906b0d",
            "37906b0e",
            "37906b0f",
            "37906b10",
            "37906b12",
            "37906b13",
            "37906b14",
            "37906b2a",
            "37906b2c",
            "37906b2d",
            "ba7cfaf4",
            "37906b30",
            "37906b31",
            "37906b32",
            "37906b4a",
            "37906b4d",
            "37906b50",
            "37906b6d",
        };
        private List<string> Costume_Eelectric = new List<string>()
        {
           "5000aef2",
            "5000aef3",
            "5000aef4",
            "5000aef5",
            "5000aef6",
            "5000aef8",
            "5000aef9",
            "5000aefa",
            "5000af10",
            "5000af12",
            "5000af13",
            "b01533ce",
            "5000af16",
            "5000af17",
            "5000af18",
            "5000af30",
            "5000af33",
            "5000af36",
            "5000af53",
        };
        private List<string> Costume_Grizzly = new List<string>()
        {
            "ab4cbfe9",
            "ab4cbfea",
            "ab4cbfeb",
            "ab4cbfec",
            "ab4cbfed",
            "ab4cbfef",
            "ab4cbff0",
            "ab4cbff1",
            "ab4cc007",
            "ab4cc009",
            "ab4cc00a",
            "be4b41b7",
            "ab4cc00d",
            "ab4cc00e",
            "ab4cc00f",
            "ab4cc027",
            "ab4cc02a",
            "ab4cc02d",
            "ab4cc04a",
        };
        private List<string> Costume_Koala = new List<string>()
        {
            "3eac4642",
            "3eac4643",
            "3eac4644",
            "3eac4645",
            "3eac4646",
            "3eac4648",
            "3eac4649",
            "3eac464a",
            "3eac4660",
            "3eac4662",
            "3eac4663",
            "96dc867e",
            "3eac4666",
            "3eac4667",
            "3eac4668",
            "3eac4680",
            "3eac4683",
            "3eac4686",
            "3eac46a3",
        };
        private List<string> Costume_Kooala = new List<string>()
        {
            "59712a2f",
            "59712a30",
            "59712a31",
            "59712a32",
            "59712a33",
            "59712a35",
            "59712a36",
            "59712a37",
            "59712a4d",
            "59712a4f",
            "59712a50",
            "d4b42031",
            "59712a53",
            "59712a54",
            "59712a55",
            "59712a6d",
            "59712a70",
            "59712a73",
            "59712a90",
        };
        private List<string> Costume_Monkey = new List<string>()
        {
            "dbae1b87",
            "dbae1b88",
            "dbae1b89",
            "dbae1b8a",
            "dbae1b8b",
            "dbae1b8d",
            "dbae1b8e",
            "dbae1b8f",
            "dbae1ba5",
            "dbae1ba7",
            "dbae1ba8",
            "9a1559d9",
            "dbae1bab",
            "dbae1bac",
            "dbae1bad",
            "dbae1bc5",
            "dbae1bc8",
            "dbae1bcb",
            "dbae1be8",
        };
        private List<string> Costume_Normal = new List<string>()
        {
            "c6b664d5",
            "c6b664d6",
            "c6b664d7",
            "c6b664d8",
            "c6b664d9",
            "c6b664db",
            "c6b664dc",
            "c6b664dd",
            "c6b664f3",
            "c6b664f5",
            "c6b664f6",
            "10163a4b",
            "c6b664f9",
            "c6b664fa",
            "c6b664fb",
            "c6b66513",
            "c6b66516",
            "c6b66519",
            "c6b66536",
        };
        private List<string> Costume_Parafox = new List<string>()
        {
           "bf7e0d25",
            "bf7e0d26",
            "bf7e0d27",
            "bf7e0d28",
            "bf7e0d29",
            "bf7e0d2b",
            "bf7e0d2c",
            "bf7e0d2d",
            "bf7e0d43",
            "bf7e0d45",
            "bf7e0d46",
            "30439bfb",
            "bf7e0d49",
            "bf7e0d4a",
            "bf7e0d4b",
            "bf7e0d63",
            "bf7e0d66",
            "bf7e0d69",
            "bf7e0d86",
        };
        private List<string> Costume_Rabbit = new List<string>()
        {
            "d0e4b160",
            "d0e4b161",
            "d0e4b162",
            "d0e4b163",
            "d0e4b164",
            "d0e4b166",
            "d0e4b167",
            "d0e4b168",
            "d0e4b17e",
            "d0e4b180",
            "d0e4b181",
            "4bb17f20",
            "d0e4b184",
            "d0e4b185",
            "d0e4b186",
            "d0e4b19e",
            "d0e4b1a1",
            "d0e4b1a4",
            "d0e4b1c1",
        };
        private List<string> Costume_Ratcicle = new List<string>()
        {
            "863a06e9",
            "863a06ea",
            "863a06eb",
            "863a06ec",
            "863a06ed",
            "863a06ef",
            "863a06f0",
            "863a06f1",
            "863a0707",
            "863a0709",
            "863a070a",
            "4106dab7",
            "863a070d",
            "863a070e",
            "863a070f",
            "863a0727",
            "863a072a",
            "863a072d",
            "863a074a",
        };
        private List<string> Costume_Roller = new List<string>()
        {
            "77950d90",
            "77950d91",
            "77950d92",
            "77950d93",
            "77950d94",
            "77950d96",
            "77950d97",
            "77950d98",
            "77950dae",
            "77950db0",
            "77950db1",
            "7b0ca8f0",
            "77950db4",
            "77950db5",
            "77950db6",
            "77950dce",
            "77950dd1",
            "77950dd4",
            "77950df1",
        };
        private List<string> Costume_Scorporilla = new List<string>()
        {
            "348a545c",
            "348a545d",
            "348a545e",
            "348a545f",
            "348a5460",
            "348a5462",
            "348a5463",
            "348a5464",
            "348a547a",
            "348a547c",
            "348a547d",
            "5cc03ba4",
            "348a5480",
            "348a5481",
            "348a5482",
            "348a549a",
            "348a549d",
            "348a54a0",
            "348a54bd",
        };
        private List<string> Costume_Shellephant = new List<string>()
        {
            "bf3e735a",
            "bf3e735b",
            "bf3e735c",
            "bf3e735d",
            "bf3e735e",
            "bf3e7360",
            "bf3e7361",
            "bf3e7362",
            "bf3e7378",
            "bf3e737a",
            "bf3e737b",
            "288ffc66",
            "bf3e737e",
            "bf3e737f",
            "bf3e7380",
            "bf3e7398",
            "bf3e739b",
            "bf3e739e",
            "bf3e73bb",
        };
        private List<string> Costume_Shurtle = new List<string>()
        {
            "ce71f499",
            "ce71f49a",
            "ce71f49b",
            "ce71f49c",
            "ce71f49d",
            "ce71f49f",
            "ce71f4a0",
            "ce71f4a1",
            "ce71f4b7",
            "ce71f4b9",
            "ce71f4ba",
            "ffcca307",
            "ce71f4bd",
            "ce71f4be",
            "ce71f4bf",
            "ce71f4d7",
            "ce71f4da",
            "ce71f4dd",
            "ce71f4fa",
        };
        private List<string> Costume_Skeleton = new List<string>()
        {
            "4756590d",
            "4756590e",
            "4756590f",
            "47565910",
            "47565911",
            "47565913",
            "47565914",
            "47565915",
            "4756592b",
            "4756592d",
            "4756592e",
            "a374cd13",
            "47565931",
            "47565932",
            "47565933",
            "4756594b",
            "4756594e",
            "47565951",
            "4756596e",
        };
        private List<string> Costume_Sludge = new List<string>()
        {
            "cadb7a74",
            "cadb7a75",
            "cadb7a76",
            "cadb7a77",
            "cadb7a78",
            "cadb7a7a",
            "cadb7a7b",
            "cadb7a7c",
            "cadb7a92",
            "cadb7a94",
            "cadb7a95",
            "9093d88c",
            "cadb7a98",
            "cadb7a99",
            "cadb7a9a",
            "cadb7ab2",
            "cadb7ab5",
            "cadb7ab8",
            "cadb7ad5",
        };
        private List<string> Costume_Spike = new List<string>()
        {
            "bea9f16e",
            "bea9f16f",
            "bea9f170",
            "bea9f171",
            "bea9f172",
            "bea9f174",
            "bea9f175",
            "bea9f176",
            "bea9f18c",
            "bea9f18e",
            "bea9f18f",
            "169440d2",
            "bea9f192",
            "bea9f193",
            "bea9f194",
            "bea9f1ac",
            "bea9f1af",
            "bea9f1b2",
            "bea9f1cf",
        };
        private List<string> Costume_Stinky = new List<string>()
        {
            "737ed06",
            "737ed07",
            "737ed08",
            "737ed09",
            "737ed0a",
            "737ed0c",
            "737ed0d",
            "737ed0e",
            "737ed24",
            "737ed26",
            "737ed27",
            "dfc5b83a",
            "737ed2a",
            "737ed2b",
            "737ed2c",
            "737ed44",
            "737ed47",
            "737ed4a",
            "737ed67",
        };
        private List<string> Costume_Valentine = new List<string>()
        {
            "5668677e",
            "5668677f",
            "56686780",
            "56686781",
            "56686782",
            "56686784",
            "56686785",
            "56686786",
            "5668679c",
            "5668679e",
            "5668679f",
            "76a48cc2",
            "566867a2",
            "566867a3",
            "566867a4",
            "566867bc",
            "566867bf",
            "566867c2",
            "566867df",
        };

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        private enum Costume
        {
            Normal = 0,
            Battler,
            Bratgirl,
            Chiratta,
            Eelectric,
            Grizzly,
            Koala,
            Kooala,
            Monkey,
            Parafox,
            Rabbit,
            Raticcle,
            Roller,
            Scorporilla,
            Shellephant,
            Shurtle,
            Skeleton,
            Sludge,
            Spike,
            Stinky,
            Valentine,
        }

        private string GetName(Costume skin, int id)
        {
            switch (skin)
            {
                default:
                case Costume.Normal:
                    return Costume_Normal[id];
                case Costume.Battler:
                    return Costume_Battler[id];
                case Costume.Bratgirl:
                    return Costume_Bratgirl[id];
                case Costume.Chiratta:
                    return Costume_Chiratta[id];
                case Costume.Eelectric:
                    return Costume_Eelectric[id];
                case Costume.Grizzly:
                    return Costume_Grizzly[id];
                case Costume.Koala:
                    return Costume_Koala[id];
                case Costume.Kooala:
                    return Costume_Kooala[id];
                case Costume.Monkey:
                    return Costume_Monkey[id];
                case Costume.Parafox:
                    return Costume_Parafox[id];
                case Costume.Rabbit:
                    return Costume_Rabbit[id];
                case Costume.Raticcle:
                    return Costume_Ratcicle[id];
                case Costume.Roller:
                    return Costume_Roller[id];
                case Costume.Scorporilla:
                    return Costume_Scorporilla[id];
                case Costume.Shellephant:
                    return Costume_Shellephant[id];
                case Costume.Shurtle:
                    return Costume_Shurtle[id];
                case Costume.Skeleton:
                    return Costume_Skeleton[id];
                case Costume.Sludge:
                    return Costume_Sludge[id];
                case Costume.Spike:
                    return Costume_Spike[id];
                case Costume.Stinky:
                    return Costume_Stinky[id];
                case Costume.Valentine:
                    return Costume_Valentine[id];
            }
        }

        public override void ModPass(GenericModStruct mod)
        {
            string path_extr = mod.ExtractedPath + @"default\";

            for (int i = 0; i < Costume_Normal.Count; i++)
            {
                for (int d = 0; d < 21; d++)
                {
                    string file = path_extr + "cinematics/" + GetName((Costume)d, i) + ".p3d";
                    File.Move(file, file + "1");
                }
            }

            List<int> Copy = new List<int>();
            for(int i = 0; i < 21; i++)
            {
                Copy.Add(i);
            }
            int r = 0;

            for (int i = 0; i < Costume_Normal.Count; i++)
            {
                List<int> Swapper = new List<int>();
                List<int> Valid = new List<int>(Copy);

                while (Valid.Count > 0)
                {
                    r = rand.Next(Valid.Count);
                    Swapper.Add(Valid[r]);
                    Valid.RemoveAt(r);
                }

                for (int d = 0; d < 21; d++)
                {
                    string newfile = path_extr + "cinematics/" + GetName((Costume)Swapper[d], i) + ".p3d";
                    string oldfile = path_extr + "cinematics/" + GetName((Costume)d, i) + ".p3d1";
                    File.Move(oldfile, newfile);
                }
            }

        }
    }
}
