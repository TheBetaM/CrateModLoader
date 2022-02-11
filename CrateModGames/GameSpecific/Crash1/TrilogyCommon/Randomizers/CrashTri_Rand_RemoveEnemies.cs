using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_RemoveEnemies : ModStruct<NSF_Pair>
    {
        private Random rand;
        private bool isRandom;

        private List<EntityTypePair> EnemiesToRemove_C2 = new List<EntityTypePair>()
        {
            // Turtle Woods
            new EntityTypePair(20, 1), //armadillo
            new EntityTypePair(2, 0), //spiky turtle
            new EntityTypePair(6, 0), //dive bird
            // Snow Go
            new EntityTypePair(25, 1), //penguin pulse
            new EntityTypePair(25, 0), //penguin
            new EntityTypePair(24, 0), //seal
            new EntityTypePair(24, 1), //seal path
            new EntityTypePair(32, 0), //smasher
            new EntityTypePair(32, 1), //smasher constant
            new EntityTypePair(27, 0), //porcupine
            // Hang Eight
            new EntityTypePair(47, 5), //fish
            new EntityTypePair(47, 4), //mine float
            new EntityTypePair(38, 4), //evil plant
            //new EntityTypePair(47, 8), //whirlpool
            new EntityTypePair(47, 7), //mine path
            // The Pits
            new EntityTypePair(6, 2), //bird 3D
            new EntityTypePair(2, 5), //saw turtle
            // Crash Dash
            new EntityTypePair(39, 0), //mine
            new EntityTypePair(39, 1), //fence
            // Snow Biz
            new EntityTypePair(32, 3), //icicle
            new EntityTypePair(27, 1), //porcupine smart
            new EntityTypePair(32, 2), //roller
            new EntityTypePair(25, 2), //penguin path
            // Bear It
            new EntityTypePair(48, 2), //orca
            // The Eel Deal
            new EntityTypePair(28, 0), //rat
            new EntityTypePair(28, 1), //rat tunnel
            new EntityTypePair(13, 0), //scrubber
            new EntityTypePair(13, 6), //scrubber circle
            new EntityTypePair(12, 1), //fan
            new EntityTypePair(13, 8), //scrubber tunnel ring
            //new EntityTypePair(12, 8), //eel
            new EntityTypePair(12, 9), //barrel tunnel
            new EntityTypePair(21, 0), //mech floater
            new EntityTypePair(21, 3), //mech bob
            // Sewer Or Later
            new EntityTypePair(16, 0), //welder
            new EntityTypePair(28, 4), //rat circle
            new EntityTypePair(13, 5), //scrubber path
            new EntityTypePair(12, 2), //fan break
            // Bear Down
            new EntityTypePair(48, 5), //lab lift
            // Road To Ruin
            new EntityTypePair(7, 1), //fireface watcher
            new EntityTypePair(28, 2), //possum path
            new EntityTypePair(7, 0), //fireface
            new EntityTypePair(10, 0), //monkey hop
            new EntityTypePair(28, 3), //lizard path
            new EntityTypePair(11, 0), //gorilla boulder
            // UnBearable
            new EntityTypePair(39, 8), //critter
            new EntityTypePair(20, 0), //armadillo
            new EntityTypePair(33, 0), //hunter
            // Hangin Out
            new EntityTypePair(13, 10), //scrubber tunnel
            new EntityTypePair(21, 4), //mech path
            new EntityTypePair(28, 6), //rat tunnel ring
            new EntityTypePair(21, 1), //mech hanger
            new EntityTypePair(28, 4), //rat circle
            new EntityTypePair(21, 5), //mech path floater
            // Diggin It
            new EntityTypePair(38, 0), //spore plant
            new EntityTypePair(39, 7), //tiki
            new EntityTypePair(39, 4), //timed spark
            new EntityTypePair(39, 6), //timed spark
            //new EntityTypePair(18, 2), //hive
            new EntityTypePair(18, 0), //bee hive
            new EntityTypePair(45, 1), //labjack
            // Piston It Away
            new EntityTypePair(55, 1), //piston
            new EntityTypePair(55, 5), //robot walker
            new EntityTypePair(53, 0), //fred
            new EntityTypePair(55, 2), //pad
            //new EntityTypePair(55, 3), //gun
            //new EntityTypePair(55, 4), //gun down
            //new EntityTypePair(55, 0), //piston up
            new EntityTypePair(56, 0), //pusher
            // Rock It
            new EntityTypePair(35, 11), //space cable
            new EntityTypePair(35, 13), //space gun
            //new EntityTypePair(42, 0), //space lab
            new EntityTypePair(35, 15), //space ring
            // Night Fight
            new EntityTypePair(46, 0), //dragonfly
        };

        private List<EntityTypePair> EnemiesToRemove_C3 = new List<EntityTypePair>()
        {
            // Toad Village
            new EntityTypePair(11, 1), //goat
            new EntityTypePair(11, 2), //knight
            new EntityTypePair(11, 6), //log
            new EntityTypePair(11, 7), //fence
            new EntityTypePair(11, 0), //frog
            // Under Pressure
            new EntityTypePair(2, 1), //shark
            new EntityTypePair(23, 0), //mine
            new EntityTypePair(2, 2), //eel
            new EntityTypePair(2, 0), //pufferfish
            new EntityTypePair(2, 3), //paddle
            // Orient Express
            new EntityTypePair(8, 4), //stone assistant
            //new EntityTypePair(7, 0), //assistant
            new EntityTypePair(6, 0), //dragon
            new EntityTypePair(8, 0), //barrel
            // Bone Yard
            new EntityTypePair(13, 0), //grass
            new EntityTypePair(16, 2), //tery
            new EntityTypePair(19, 0), //swamp assistant
            // Makin Waves
            new EntityTypePair(42, 0), //jet mine
            new EntityTypePair(46, 0), //boat guy
            new EntityTypePair(42, 1), //jet mine circle
            new EntityTypePair(15, 16), //ship
            new EntityTypePair(15, 18), //ship
            // Gee Wiz
            new EntityTypePair(25, 0), //wizard
            // Hang Em High
            new EntityTypePair(61, 0), //guard
            new EntityTypePair(59, 0), //carpet guy
            new EntityTypePair(58, 0), //scorpion
            new EntityTypePair(65, 0), //vase guy
            // Tomb Time
            new EntityTypePair(37, 2), //cobra
            new EntityTypePair(37, 0), //croc
            new EntityTypePair(41, 0), //flamer
            //new EntityTypePair(39, 0), //vase
            new EntityTypePair(38, 0), //switch guy
            new EntityTypePair(37, 4), //spear
            // Midnight Run
            new EntityTypePair(31, 0), //runner
            // Dino Might
            new EntityTypePair(21, 0), //crash fish
            // Deep Trouble
            new EntityTypePair(70, 0), //laser
            // High Time
            new EntityTypePair(74, 0), //thrower
            new EntityTypePair(60, 0), //knife guy
            // Double Header
            new EntityTypePair(81, 0), //giant
            // Sphynxinator
            new EntityTypePair(57, 0), //sarcophagus
            // Tell No Tales
            new EntityTypePair(49, 0), //crows nest
            new EntityTypePair(47, 0), //shark
            // Future Frenzy
            new EntityTypePair(77, 6), //future fence
            new EntityTypePair(79, 0), //jumper
            new EntityTypePair(82, 0), //ufo
            new EntityTypePair(83, 0), //spinner
            // Tomb Wader
            new EntityTypePair(71, 0), //scarab
            new EntityTypePair(76, 0), //jump gator
            new EntityTypePair(69, 0), //pusher wave
            new EntityTypePair(75, 0), //shield guy
            // Gone Tomorrow
            new EntityTypePair(82, 0), //bazooka robot
            new EntityTypePair(77, 10), //future fence 2D
            // Eggipus Rex
            new EntityTypePair(16, 9), //ptery
        };

        public CrashTri_Rand_RemoveEnemies()
        {
            isRandom = Crash2.Crash2_Props_Main.Option_RandEnemiesMissing.Enabled || Crash3.Crash3_Props_Main.Option_RandEnemiesMissing.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(NSF_Pair npair)
        {
            /*
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldEntity ent in zone.Entities)
                {
                    if (ent.Type == 34)
                    {
                        //todo Crash 1 version
                    }
                }
            }
            */


            foreach (ZoneEntry zone in npair.nsf.GetEntries<ZoneEntry>())
            {
                if (zone.EName != "H2_gZ") // piston it death route
                {
                    foreach (Entity ent in zone.Entities)
                    {
                        if (ent.Type != null && ent.Subtype != null)
                        {
                            foreach (EntityTypePair pair in EnemiesToRemove_C2)
                            {
                                if (pair.Type == ent.Type && pair.Subtype == ent.Subtype)
                                {
                                    if (!isRandom || (isRandom && rand.Next(2) == 0))
                                    {
                                        ent.Type = 3;
                                        ent.Subtype = 16;
                                        ent.AlternateID = null;
                                        ent.TimeTrialReward = null;
                                        ent.Victims.Clear();
                                        ent.BonusBoxCount = null;
                                        ent.BoxCount = null;
                                        ent.DDASection = null;
                                        ent.DDASettings = null;
                                        ent.ZMod = null;
                                        ent.OtherSettings = null;
                                        ent.Settings.Clear();
                                        ent.ExtraProperties.Clear();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (NewZoneEntry zone in npair.nsf.GetEntries<NewZoneEntry>())
            {
                if (zone.EName != "77_gZ") // eggipus entrance
                {
                    foreach (Entity ent in zone.Entities)
                    {
                        ent.Name = null; // may need the extra space
                        if (ent.Type != null && ent.Subtype != null)
                        {
                            foreach (EntityTypePair pair in EnemiesToRemove_C3)
                            {
                                if (pair.Type == ent.Type && pair.Subtype == ent.Subtype)
                                {
                                    if (!isRandom || (isRandom && rand.Next(2) == 0))
                                    {
                                        ent.Type = 3;
                                        ent.Subtype = 16;
                                        ent.AlternateID = null;
                                        ent.TimeTrialReward = null;
                                        ent.Victims.Clear();
                                        ent.BonusBoxCount = null;
                                        ent.BoxCount = null;
                                        ent.DDASection = null;
                                        ent.DDASettings = null;
                                        ent.ZMod = null;
                                        ent.OtherSettings = null;
                                        ent.Scaling = 0;
                                        ent.Settings.Clear();
                                        ent.Settings.Add(new EntitySetting(0, 0));
                                        ent.ExtraProperties.Clear();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
