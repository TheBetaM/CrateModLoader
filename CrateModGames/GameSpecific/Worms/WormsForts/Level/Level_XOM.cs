using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Level_XOM : Level<XOM_File>
    {
        public override Dictionary<int, string> CategoryNames => new Dictionary<int, string>()
        {
            [1] = "Build Points",
        };

        public override void Load(XOM_File file)
        {
            List<XCollisionGeometry> Cols = file.GetContainers<XCollisionGeometry>();
            if (Cols != null && Cols.Count > 0)
            {
                foreach (XCollisionGeometry Col in Cols)
                {
                    CollisionData_XOM XOM_Col = new CollisionData_XOM();
                    XOM_Col.Load(Col);
                    CollisionData.Add(XOM_Col);
                }
            }

            XFortsExportedData Export = file.GetContainer<XFortsExportedData>();
            if (Export != null)
            {
                for (int i = 0; i < Export.BuildPoints.Count; i++)
                {
                    Object_BuildPoint BPoint = new Object_BuildPoint();
                    BPoint.Load(Export.BuildPoints[i]);
                    BPoint.ObjectCategory = 1;
                    BPoint.ID = i;
                    ObjectData.Add(BPoint);
                }
            }
        }

        public override void Save(XOM_File file)
        {
            // todo: save collision changes

            XFortsExportedData Export = file.GetContainer<XFortsExportedData>();
            //Export.BuildPoints.Clear();
            foreach (LevelObjectDataBase P in ObjectData)
            {
                if (P is Object_BuildPoint Point)
                {
                    Point.Save(Export.BuildPoints[Point.ID]);
                }
            }
        }
    }
}
