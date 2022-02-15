using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;
using Crash;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class Level_NSF : Level<NSF_Pair>
    {
        public override Dictionary<int, string> CategoryNames
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    [0] = "Zones",
                    [1] = "Entities",
                };
            }
            set { }
        }

        public override void Load(NSF_Pair file)
        {

            int EntID = 0;
            foreach (OldZoneEntry zone in file.nsf.GetEntries<OldZoneEntry>())
            {
                Object_OldZone OldZone = new Object_OldZone();
                OldZone.Load(zone);
                OldZone.ObjectCategory = 0;
                OldZone.ID = zone.EID;
                ObjectData.Add(OldZone);

                foreach (OldEntity ent in zone.Entities)
                {
                    Object_OldEntity BPoint = new Object_OldEntity();
                    BPoint.Zone = zone;
                    BPoint.Load(ent);
                    BPoint.ObjectCategory = 1;
                    BPoint.ID = EntID;
                    ObjectData.Add(BPoint);
                    EntID++;
                }
            }
            foreach (ZoneEntry zone in file.nsf.GetEntries<ZoneEntry>())
            {
                Object_Zone Zone = new Object_Zone();
                Zone.Load(zone);
                Zone.ObjectCategory = 0;
                Zone.ID = zone.EID;
                ObjectData.Add(Zone);

                foreach (Entity ent in zone.Entities)
                {
                    Object_Entity BPoint = new Object_Entity();
                    BPoint.Zone = zone;
                    BPoint.Load(ent);
                    BPoint.ObjectCategory = 1;
                    BPoint.ID = EntID;
                    ObjectData.Add(BPoint);
                    EntID++;
                }
            }
            foreach (NewZoneEntry zone in file.nsf.GetEntries<NewZoneEntry>())
            {
                Object_NewZone Zone = new Object_NewZone();
                Zone.Load(zone);
                Zone.ObjectCategory = 0;
                Zone.ID = zone.EID;
                ObjectData.Add(Zone);

                foreach (Entity ent in zone.Entities)
                {
                    Object_NewEntity BPoint = new Object_NewEntity();
                    BPoint.Zone = zone;
                    BPoint.Load(ent);
                    BPoint.ObjectCategory = 1;
                    BPoint.ID = EntID;
                    ObjectData.Add(BPoint);
                    EntID++;
                }
            }
            /*
            foreach (OldSceneryEntry zone in file.nsf.GetEntries<OldSceneryEntry>())
            {
                SceneryData_NSF_Old scenery = new SceneryData_NSF_Old();
                scenery.Load(zone);
                if (scenery.Vertices.Count > 0)
                {
                    CollisionData.Add(scenery);
                }
            }
            */

        }

        public override void Save(NSF_Pair file)
        {

        }
    }
}
