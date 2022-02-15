using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Level_LEV : Level<CtrScene>
    {
        public override Dictionary<int, string> CategoryNames
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    [0] = "Scene",
                    [1] = "Starting Points",
                    [2] = "Pickups",
                    [3] = "Spawns",
                    [4] = "Restart Points",
                };
            }
            set { }
        }

        public override void Load(CtrScene file)
        {
            //Object_SceneHeader scene = new Object_SceneHeader();
            //scene.ObjectCategory = 0;
            //scene.Load(file.header);
            //ObjectData.Add(scene);

            for (int i = 0; i < file.header.startGrid.Length; i++)
            {
                Object_StartGrid spawn = new Object_StartGrid();
                spawn.ID = i;
                spawn.ObjectCategory = 1;
                spawn.Load(file.header.startGrid[i]);
                ObjectData.Add(spawn);
            }

            for (int i = 0; i < file.pickups.Count; i++)
            {
                Object_Pickup pickup = new Object_Pickup();
                pickup.Load(file.pickups[i]);
                pickup.ObjectCategory = 2;
                pickup.ID = i;
                ObjectData.Add(pickup);
            }

            if (file.mesh != null)
            {
                CollisionData_MeshInfo mesh = new CollisionData_MeshInfo();
                mesh.Load(file.mesh);
                CollisionData.Add(mesh);
            }

            if (file.spawnGroups != null)
            {
                for (int i = 0; i < file.spawnGroups.Entries.Count; i++)
                {
                    Object_SpawnGroup spawn = new Object_SpawnGroup();
                    spawn.ID = i;
                    spawn.SpawnID = i;
                    spawn.ObjectCategory = 3;
                    spawn.Load(file.spawnGroups);
                    ObjectData.Add(spawn);
                }
            }

            if (file.restartPts != null)
            {
                for (int i = 0; i < file.restartPts.Count; i++)
                {
                    Object_RestartPoint spawn = new Object_RestartPoint();
                    spawn.ID = i;
                    spawn.ObjectCategory = 4;
                    spawn.Load(file.restartPts[i]);
                    ObjectData.Add(spawn);
                }
            }
            
        }

        public override void Save(CtrScene file)
        {

        }
    }
}
