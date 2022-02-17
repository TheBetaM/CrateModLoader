using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Level_XOM : Level<XOM_File>
    {
        public override Dictionary<int, string> CategoryNames
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    [0] = "Collision Data",
                    [1] = "Worms Forts Exported Data",
                    [2] = "Reference Points",
                    [3] = "Build Points",
                    [4] = "Trigger Spheres",
                    [5] = "Trigger Boxes",
                };
            }
            set { }
        }

        public override void Load(XOM_File file)
        {
            List<XCollisionGeometry> Cols = file.GetContainers<XCollisionGeometry>();
            if (Cols != null && Cols.Count > 0)
            {
                int ColID = 0;
                foreach (XCollisionGeometry Col in Cols)
                {
                    CollisionData_XOM XOM_Col = new CollisionData_XOM();
                    XOM_Col.Load(Col);
                    XOM_Col.ObjectCategory = 0;
                    XOM_Col.ID = ColID;
                    ObjectData.Add(XOM_Col);
                    ColID++;
                }
            }

            XFortsExportedData Export = file.GetContainer<XFortsExportedData>();
            if (Export != null)
            {
                Object_FortsExportedData ExportData = new Object_FortsExportedData();
                ExportData.Load(Export);
                ExportData.ObjectCategory = 1;
                ExportData.ID = 0;
                ObjectData.Add(ExportData);

                for (int i = 0; i < Export.BuildPoints.Count; i++)
                {
                    Object_BuildPoint BPoint = new Object_BuildPoint();
                    BPoint.Load(Export.BuildPoints[i]);
                    BPoint.ObjectCategory = 3;
                    BPoint.ID = i;
                    ObjectData.Add(BPoint);
                }

                for (int i = 0; i < Export.RefPoints.Count; i++)
                {
                    Object_RefPoint BPoint = new Object_RefPoint();
                    BPoint.Load(Export.RefPoints[i]);
                    BPoint.ObjectCategory = 2;
                    BPoint.ID = i;
                    ObjectData.Add(BPoint);
                }

                for (int i = 0; i < Export.PhantomBoxes.Count; i++)
                {
                    Object_TriggerBox BPoint = new Object_TriggerBox();
                    BPoint.Load(Export.PhantomBoxes[i]);
                    BPoint.ObjectCategory = 5;
                    BPoint.ID = i;
                    ObjectData.Add(BPoint);
                }

                for (int i = 0; i < Export.PhantomSpheres.Count; i++)
                {
                    Object_TriggerSphere BPoint = new Object_TriggerSphere();
                    BPoint.Load(Export.PhantomSpheres[i]);
                    BPoint.ObjectCategory = 4;
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
