using System;
using System.Collections.Generic;
using System.Text;
using Pure3D;
using Pure3D.Chunks;
using Collada141;
//BetaM - Side project

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public static class ModelExporter
    {

        private static library_animations cachedAnims = new library_animations();
        private static library_animation_clips cachedAnim_clips = new library_animation_clips();
        private static library_controllers cachedControllers = new library_controllers();
        private static library_effects cachedEffects = new library_effects();
        private static library_geometries cachedGeoms = new library_geometries();
        private static library_materials cachedMats = new library_materials();
        private static library_visual_scenes cachedVscenes = new library_visual_scenes();
        private static library_images cachedImages = new library_images();

        public static void LoadAnim(ref Pure3D.Chunks.Animation refAnim)
        {
            cachedAnims.animation = new animation[1];
            cachedAnims.animation[cachedAnims.animation.Length - 1].name = refAnim.Name;
            cachedAnims.animation[cachedAnims.animation.Length - 1].Items[0] = new channel();

        }
        public static void LoadAnimClip(ref AnimationGroup refAnim)
        {
            cachedAnim_clips.animation_clip = new animation_clip[1];
            cachedAnim_clips.animation_clip[cachedAnim_clips.animation_clip.Length - 1].name = refAnim.Name;
            cachedAnim_clips.animation_clip[cachedAnim_clips.animation_clip.Length - 1].start = 0;
        }

        public static void TestExport()
        {
            Pure3D.File CrashOnfootAnim1 = new Pure3D.File();
            CrashOnfootAnim1.Load(ModLoaderGlobals.ToolsPath + "file.p3d");
            PrintHierarchy(CrashOnfootAnim1.RootChunk, 0);

            Shader[] shaders = CrashOnfootAnim1.RootChunk.GetChildren<Shader>();
            //AddSkinnedModelWithAnimations(ref CrashOnfootAnim1.RootChunk.GetChildren<Skin>()[0], ref CrashOnfootAnim1.RootChunk.GetChildren<SkeletonCTTR>()[0], ref shaders);
            AddSkinnedModel(ref CrashOnfootAnim1.RootChunk.GetChildren<Skin>()[0], ref CrashOnfootAnim1.RootChunk.GetChildren<SkeletonCTTR>()[0], ref shaders);
            ExportModel(ModLoaderGlobals.ToolsPath + "out.dae");

        }

        static void PrintHierarchy(Chunk chunk, int indent)
        {
            Console.WriteLine("{1}{0}", chunk.ToString(), new string('\t', indent));

            foreach (var child in chunk.Children)
                PrintHierarchy(child, indent + 1);
        }


        public static void LoadModel(ref COLLADA OutModel)
        {

            OutModel.version = VersionType.Item141;

            OutModel.Items = new object[8];
            OutModel.Items[0] = cachedImages;
            OutModel.Items[1] = cachedMats;
            OutModel.Items[2] = cachedEffects;
            OutModel.Items[3] = cachedGeoms;
            OutModel.Items[4] = cachedControllers;
            OutModel.Items[5] = cachedAnims;
            OutModel.Items[6] = cachedAnim_clips;
            OutModel.Items[7] = cachedVscenes;
            //COLLADAScene endScene = new COLLADAScene();
            //endScene.instance_visual_scene = new InstanceWithExtra();
            //endScene.instance_visual_scene.url = "Scene0";
            //OutModel.Items[8] = endScene;

            OutModel.asset = new asset();
            //Model.asset.up_axis = UpAxisType.Z_UP;
            //Model.asset.unit = new assetUnit();
            //Model.asset.unit.name = "meter";
            //Model.asset.unit.meter = 1;

            OutModel.scene = new COLLADAScene();
            OutModel.scene.instance_visual_scene = new InstanceWithExtra();
            OutModel.scene.instance_visual_scene.url = "#Scene0";

        }

        public static void ExportModel(string ExportPath)
        {
            COLLADA Model = new COLLADA();

            LoadModel(ref Model);

            Model.Save(ExportPath);
        }

        public static void AddSkinnedModel(ref Skin SkinChunk, ref SkeletonCTTR SkeletonChunk, ref Shader[] ShaderChunks, bool SplitMesh = false)
        {

            cachedGeoms.geometry = new geometry[SkinChunk.NumPrimGroups];
            cachedVscenes.visual_scene = new visual_scene[1] { new visual_scene() };
            cachedVscenes.visual_scene[0].id = "Scene0";
            cachedVscenes.visual_scene[0].name = "Scene0";
            //CacheMaterialsTextures(Materials, TotalShaders, tex_sec);

            for (int submodel = 0; submodel < SkinChunk.NumPrimGroups; submodel++)
            {
                PrimitiveGroupCTTR Group = SkinChunk.GetChildren<PrimitiveGroupCTTR>()[submodel];
                PositionList PosList = Group.GetChildren<PositionList>()[0];
                NormalList NormList = Group.GetChildren<NormalList>()[0];
                UVList UVList = Group.GetChildren<UVList>()[0];
                IndexList IndList = Group.GetChildren<IndexList>()[0];

                cachedGeoms.geometry[submodel] = new geometry();
                cachedGeoms.geometry[submodel].id = "geometry" + submodel;
                cachedGeoms.geometry[submodel].name = "geometry" + submodel;
                mesh newMesh = new Collada141.mesh();

                newMesh.source = new source[3];

                newMesh.source[0] = new source();
                newMesh.source[0].id = "geometry" + submodel + "-positions";
                float_array geom_positions = new float_array();
                geom_positions.id = "geometry" + submodel + "-positions-array";

                geom_positions.count = (ulong)PosList.Positions.Length * 3;
                geom_positions.Values = new float[geom_positions.count];
                int posit = 0;
                for (int i = 0; i < (int)PosList.Positions.Length; i++)
                {
                    geom_positions.Values[posit] = PosList.Positions[i].X;
                    posit++;
                    geom_positions.Values[posit] = PosList.Positions[i].Y;
                    posit++;
                    geom_positions.Values[posit] = PosList.Positions[i].Z;
                    posit++;
                }

                newMesh.source[0].Item = geom_positions;
                newMesh.source[0].technique_common = new sourceTechnique_common();
                newMesh.source[0].technique_common.accessor = new accessor();
                newMesh.source[0].technique_common.accessor.source = "#" + geom_positions.id;
                newMesh.source[0].technique_common.accessor.count = geom_positions.count / 3;
                newMesh.source[0].technique_common.accessor.stride = 3;
                newMesh.source[0].technique_common.accessor.param = new param[3] { new param(), new param(), new param(), };
                newMesh.source[0].technique_common.accessor.param[0].name = "X";
                newMesh.source[0].technique_common.accessor.param[0].type = "float";
                newMesh.source[0].technique_common.accessor.param[1].name = "Y";
                newMesh.source[0].technique_common.accessor.param[1].type = "float";
                newMesh.source[0].technique_common.accessor.param[2].name = "Z";
                newMesh.source[0].technique_common.accessor.param[2].type = "float";

                newMesh.source[1] = new source();
                newMesh.source[1].id = "geometry" + submodel + "-mesh-map-0";
                float_array geom_texcoords = new float_array();
                geom_texcoords.id = "geometry" + submodel + "-mesh-map-array";

                geom_texcoords.count = (ulong)UVList.UVs.Length * 2;
                geom_texcoords.Values = new float[geom_texcoords.count];
                int coord = 0;
                for (int i = 0; i < (int)UVList.UVs.Length; i++)
                {
                    geom_texcoords.Values[coord] = UVList.UVs[i].X;
                    coord++;
                    geom_texcoords.Values[coord] = UVList.UVs[i].Y;
                    coord++;
                }

                newMesh.source[1].Item = geom_texcoords;
                newMesh.source[1].technique_common = new sourceTechnique_common();
                newMesh.source[1].technique_common.accessor = new accessor();
                newMesh.source[1].technique_common.accessor.source = "#" + geom_texcoords.id;
                newMesh.source[1].technique_common.accessor.count = geom_texcoords.count / 2;
                newMesh.source[1].technique_common.accessor.stride = 2;
                newMesh.source[1].technique_common.accessor.param = new param[2] { new param(), new param(), };
                newMesh.source[1].technique_common.accessor.param[0].name = "S";
                newMesh.source[1].technique_common.accessor.param[0].type = "float";
                newMesh.source[1].technique_common.accessor.param[1].name = "T";
                newMesh.source[1].technique_common.accessor.param[1].type = "float";

                newMesh.source[2] = new source();
                newMesh.source[2].id = "geometry" + submodel + "-normals";
                float_array geom_normals = new float_array();
                geom_normals.id = "geometry" + submodel + "-normals-array";

                geom_normals.count = (ulong)NormList.Normals.Length * 3;
                geom_normals.Values = new float[geom_normals.count];
                posit = 0;
                for (int i = 0; i < (int)NormList.Normals.Length; i++)
                {
                    geom_normals.Values[posit] = NormList.Normals[i].X;
                    posit++;
                    geom_normals.Values[posit] = NormList.Normals[i].Y;
                    posit++;
                    geom_normals.Values[posit] = NormList.Normals[i].Z;
                    posit++;
                }

                newMesh.source[2].Item = geom_normals;
                newMesh.source[2].technique_common = new sourceTechnique_common();
                newMesh.source[2].technique_common.accessor = new accessor();
                newMesh.source[2].technique_common.accessor.source = "#" + geom_normals.id;
                newMesh.source[2].technique_common.accessor.count = geom_normals.count / 3;
                newMesh.source[2].technique_common.accessor.stride = 3;
                newMesh.source[2].technique_common.accessor.param = new param[3] { new param(), new param(), new param(), };
                newMesh.source[2].technique_common.accessor.param[0].name = "X";
                newMesh.source[2].technique_common.accessor.param[0].type = "float";
                newMesh.source[2].technique_common.accessor.param[1].name = "Y";
                newMesh.source[2].technique_common.accessor.param[1].type = "float";
                newMesh.source[2].technique_common.accessor.param[2].name = "Z";
                newMesh.source[2].technique_common.accessor.param[2].type = "float";

                /*
                newMesh.source[2] = new source();
                newMesh.source[2].id = "geometry" + submodel + "-colors-Col";
                float_array geom_colors = new float_array();
                geom_colors.id = "geometry" + submodel + "-colors-Col-array";

                geom_colors.count = (ulong)Sub.VertexCount * 4;
                geom_colors.Values = new float[geom_colors.count];
                coord = 0;
                for (int i = 0; i < (int)Sub.VData.Count; i++)
                {
                    geom_colors.Values[coord] = Sub.VData[i].R / 255f;
                    coord++;
                    geom_colors.Values[coord] = Sub.VData[i].G / 255f;
                    coord++;
                    geom_colors.Values[coord] = Sub.VData[i].B / 255f;
                    coord++;
                    geom_colors.Values[coord] = Sub.VData[i].A / 255f;
                    coord++;
                }

                newMesh.source[2].Item = geom_colors;
                newMesh.source[2].technique_common = new sourceTechnique_common();
                newMesh.source[2].technique_common.accessor = new accessor();
                newMesh.source[2].technique_common.accessor.source = "#" + geom_colors.id;
                newMesh.source[2].technique_common.accessor.count = geom_colors.count / 4;
                newMesh.source[2].technique_common.accessor.stride = 4;
                newMesh.source[2].technique_common.accessor.param = new param[4] { new param(), new param(), new param(), new param() };
                newMesh.source[2].technique_common.accessor.param[0].name = "R";
                newMesh.source[2].technique_common.accessor.param[0].type = "float";
                newMesh.source[2].technique_common.accessor.param[1].name = "G";
                newMesh.source[2].technique_common.accessor.param[1].type = "float";
                newMesh.source[2].technique_common.accessor.param[2].name = "B";
                newMesh.source[2].technique_common.accessor.param[2].type = "float";
                newMesh.source[2].technique_common.accessor.param[3].name = "A";
                newMesh.source[2].technique_common.accessor.param[3].type = "float";
                */

                newMesh.vertices = new vertices();
                newMesh.vertices.id = "geometry" + submodel + "-vertices";
                newMesh.vertices.name = "geometry" + submodel + "-vertices";
                newMesh.vertices.input = new InputLocal[3];
                newMesh.vertices.input[0] = new InputLocal();
                newMesh.vertices.input[0].semantic = "POSITION";
                newMesh.vertices.input[0].source = "#geometry" + submodel + "-positions";
                newMesh.vertices.input[1] = new InputLocal();
                newMesh.vertices.input[1].semantic = "TEXCOORD";
                newMesh.vertices.input[1].source = "#geometry" + submodel + "-mesh-map-0";
                newMesh.vertices.input[2] = new InputLocal();
                newMesh.vertices.input[2].semantic = "NORMAL";
                newMesh.vertices.input[2].source = "#geometry" + submodel + "-normals";
                //newMesh.vertices.input[2] = new InputLocal();
                //newMesh.vertices.input[2].semantic = "COLOR";
                //newMesh.vertices.input[2].source = "#geometry" + submodel + "-colors-Col";

                // triangle strip to list
                List<uint> TriList = new List<uint>();
                for (int i = 0; i< IndList.Indices.Length - 2; i++)
                {
                    if (i % 1 == 0)
                    {
                        TriList.Add(IndList.Indices[i + 0]);
                        TriList.Add(IndList.Indices[i + 2]);
                        TriList.Add(IndList.Indices[i + 1]);
                    }
                    else
                    {
                        TriList.Add(IndList.Indices[i + 0]);
                        TriList.Add(IndList.Indices[i + 1]);
                        TriList.Add(IndList.Indices[i + 2]);
                    }
                }

                triangles meshTriangles = new triangles();
                meshTriangles.material = "";
                meshTriangles.count = (ulong)TriList.Count;
                meshTriangles.input = new InputLocalOffset[1];
                meshTriangles.input[0] = new InputLocalOffset();
                meshTriangles.input[0].semantic = "VERTEX";
                meshTriangles.input[0].source = "#" + newMesh.vertices.id;
                meshTriangles.input[0].offset = 0;
                string packed_primitives = "";
                for (int i = 0; i < TriList.Count; i++)
                {
                    packed_primitives += $"{TriList[i]} ";
                }

                meshTriangles.p = packed_primitives;

                newMesh.Items = new object[1];
                newMesh.Items[0] = meshTriangles;

                cachedGeoms.geometry[submodel].Item = newMesh;

            }

            uint RootJointsCount = SkinChunk.NumPrimGroups;
            List<SkeletonJointCTTR> Joints = new List<SkeletonJointCTTR>();
            for (int i = 0; i < SkeletonChunk.Children.Count; i++)
            {
                if (SkeletonChunk.Children[i] is SkeletonJointCTTR joint)
                {
                    Joints.Add(joint);
                    if (joint.SkeletonParent == 0)
                    {
                        RootJointsCount++;
                    }
                }
            }

            cachedVscenes.visual_scene[0].node = new node[1];
            cachedVscenes.visual_scene[0].node[0] = new node();
            node RootNode = cachedVscenes.visual_scene[0].node[0];
            RootNode.sid = "Armature";
            RootNode.id = "Armature";
            RootNode.name = "Armature";
            RootNode.type = NodeType.NODE;
            RootNode.Items = new object[1];
            RootNode.ItemsElementName = new ItemsChoiceType2[1] { ItemsChoiceType2.matrix };
            RootNode.Items[0] = BasePos;
            RootNode.node1 = new node[RootJointsCount];
            
            for (int i = 0; i < RootJointsCount; i++)
            {
                RootNode.node1[i] = new node();
            }

            int MainJoint = 0;
            for (int i = 0; i < Joints.Count; i++)
            {
                if (Joints[i].SkeletonParent == 0)
                {
                    AddJointTree(RootNode.node1[MainJoint], Joints[i], i, Joints);
                    MainJoint++;
                }
            }

            for (int i = 0; i < SkinChunk.NumPrimGroups; i++)
            {
                RootNode.node1[i + 1] = new node();
                node SkinNode = RootNode.node1[i + 1];

                SkinNode.id = "node" + i;
                SkinNode.name = "polygon" + i;
                SkinNode.type = NodeType.NODE;

                SkinNode.instance_geometry = new instance_geometry[1] { new instance_geometry() };
                SkinNode.instance_geometry[0].url = "#" + cachedGeoms.geometry[i].id;
                SkinNode.instance_geometry[0].bind_material = new bind_material();
                SkinNode.instance_geometry[0].bind_material.technique_common = new instance_material[1];

                instance_material MatInstance = new instance_material();
                MatInstance.symbol = $"Material{i}";
                MatInstance.target = $"#Material{i}-shader";
                MatInstance.bind_vertex_input = new instance_materialBind_vertex_input[1] { new instance_materialBind_vertex_input() };
                MatInstance.bind_vertex_input[0].input_semantic = "TEXCOORD";
                MatInstance.bind_vertex_input[0].semantic = "UVChannel_1";
                MatInstance.bind_vertex_input[0].input_set = (ulong)0;

                SkinNode.instance_geometry[0].bind_material.technique_common[0] = MatInstance;

            }

            cachedMats.material = new material[SkinChunk.NumPrimGroups];
            cachedEffects.effect = new effect[SkinChunk.NumPrimGroups];
            List<uint> TexDupeCheck = new List<uint>();

            for (int mat = 0; mat < SkinChunk.NumPrimGroups; mat++)
            {
                //HGO_Model.Material ThisMaterial = HGO.Materials[mat];
                cachedEffects.effect[mat] = new effect();
                cachedEffects.effect[mat].id = $"Material{mat}-effect";
                cachedEffects.effect[mat].name = $"Material{mat}-effect";

                cachedEffects.effect[mat].Items = new effectFx_profile_abstractProfile_COMMON[1];
                effectFx_profile_abstractProfile_COMMON CommonMat = new effectFx_profile_abstractProfile_COMMON();
                fx_newparam_common SurfParam = new fx_newparam_common();
                fx_newparam_common SampParam = new fx_newparam_common();
                effectFx_profile_abstractProfile_COMMONTechnique TechParam = new effectFx_profile_abstractProfile_COMMONTechnique();


                SurfParam.sid = $"Material{mat}-effect-Image-surface";
                SurfParam.surface = new fx_surface_common();
                SurfParam.surface.type = fx_surface_type_enum.Item2D;
                SurfParam.surface.init_from = new fx_surface_init_from_common[1];
                SurfParam.surface.init_from[0] = new fx_surface_init_from_common();
                SurfParam.surface.init_from[0].Value = "";
                SurfParam.surface.init_from[0].Value = $"image{mat}";

                SampParam.sid = $"Material{mat}-effect-Image-sampler";
                SampParam.sampler2D = new fx_sampler2D_common();
                SampParam.sampler2D.source = $"Material{mat}-effect-Image-surface";

                TechParam.sid = "common";
                effectFx_profile_abstractProfile_COMMONTechniqueLambert common_lambert = new effectFx_profile_abstractProfile_COMMONTechniqueLambert();
                common_color_or_texture_type textureType = new common_color_or_texture_type();
                common_color_or_texture_typeTexture textureContent = new common_color_or_texture_typeTexture();
                textureContent.texture = $"Material{mat}-effect-Image-sampler";
                textureContent.texcoord = "UVChannel_1";
                textureType.Item = textureContent;
                common_lambert.diffuse = textureType;

                TechParam.Item = common_lambert;

                CommonMat.Items = new object[2] { SurfParam, SampParam };
                CommonMat.technique = TechParam;
                cachedEffects.effect[mat].Items[0] = CommonMat;

                cachedMats.material[mat] = new material();
                cachedMats.material[mat].id = $"Material{mat}-shader";

                string sname = SkinChunk.GetChildren<PrimitiveGroupCTTR>()[mat].ShaderName;
                cachedMats.material[mat].name = $"{sname}";

                cachedMats.material[mat].instance_effect = new instance_effect();
                cachedMats.material[mat].instance_effect.url = $"#Material{mat}-effect";
            }


            cachedImages.image = new image[SkinChunk.NumPrimGroups];
            for (int i = 0; i < SkinChunk.NumPrimGroups; i++)
            {
                cachedImages.image[i] = new image();
                cachedImages.image[i].id = $"image{i}";
                string sname = SkinChunk.GetChildren<PrimitiveGroupCTTR>()[i].ShaderName;
                cachedImages.image[i].name = $"{sname}_tex";
                cachedImages.image[i].Item = $"Texture{i}.png";
                for (int s = 0; s < ShaderChunks.Length; s++)
                {
                    if (ShaderChunks[s].Name == sname)
                    {
                        if (ShaderChunks[s].GetChildren<ShaderTextureParam>() != null)
                        {
                            foreach (ShaderTextureParam tparam in ShaderChunks[s].GetChildren<ShaderTextureParam>())
                            {
                                if (tparam.Param == "TEX")
                                {
                                    cachedImages.image[i].Item = $"{tparam.Value}";
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }

        }

        public static float[] ConvertMatrix(float[] ModelMatrix)
        {
            float[] Matrix = new float[16];
            Matrix[0] = ModelMatrix[0];
            Matrix[1] = ModelMatrix[4];
            Matrix[2] = ModelMatrix[8];
            Matrix[3] = ModelMatrix[12];
            Matrix[4] = ModelMatrix[1];
            Matrix[5] = ModelMatrix[5];
            Matrix[6] = ModelMatrix[9];
            Matrix[7] = ModelMatrix[13];
            Matrix[8] = ModelMatrix[2];
            Matrix[9] = ModelMatrix[6];
            Matrix[10] = ModelMatrix[10];
            Matrix[11] = ModelMatrix[14];
            Matrix[12] = ModelMatrix[3];
            Matrix[13] = ModelMatrix[7];
            Matrix[14] = ModelMatrix[11];
            Matrix[15] = ModelMatrix[15];
            return Matrix;
        }

        public static matrix BasePos = new matrix() { sid = "transform", Values = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 } };

        public static void AddSkinnedModelWithAnimations(ref Pure3D.Chunks.Skin SkinChunk, ref SkeletonCTTR SkeletonChunk, ref Shader[] ShaderChunks)//, ref Animation[] AnimChunks)
        {

            List<uint> IndexList = new List<uint>();

            cachedGeoms.geometry = new geometry[SkinChunk.NumPrimGroups];
            for (int primgroups = 0; primgroups < SkinChunk.NumPrimGroups; primgroups++)
            {
                cachedGeoms.geometry[primgroups] = new geometry();
                cachedGeoms.geometry[primgroups].id = "geometry" + primgroups;
                cachedGeoms.geometry[primgroups].name = "geometry" + primgroups;
                mesh newMesh = new Collada141.mesh();
                
                newMesh.vertices = new vertices();
                newMesh.vertices.id = "geometry" + primgroups + "-vertices";
                newMesh.vertices.name = "geometry" + primgroups + "-vertices";
                newMesh.vertices.input = new InputLocal[1];
                newMesh.vertices.input[0] = new InputLocal();
                newMesh.vertices.input[0].semantic = "POSITION";
                newMesh.vertices.input[0].source = "#geometry" + primgroups + "-positions";
                //newMesh.vertices.input[1] = new InputLocal();
                //newMesh.vertices.input[1].semantic = "TEXCOORD";
                //newMesh.vertices.input[1].source = "#geometry" + primgroups + "-texcoords";
                //newMesh.vertices.input[2].semantic = "COLOR";
                //newMesh.vertices.input[2].source = "#geometry0-colors";

                newMesh.source = new source[3];

                newMesh.source[0] = new source();
                newMesh.source[0].id = "geometry" + primgroups + "-positions";
                float_array geom_positions = new float_array();
                geom_positions.id = "geometry" + primgroups + "-positions-array";
                PositionList posList = SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<PositionList>()[0];
                geom_positions.count = (ulong)posList.Positions.Length;
                //geom_positions.Values = new double[geom_positions.count * 3];
                int posit = 0;
                for (ulong i = 0; i < geom_positions.count; i++)
                {
                    geom_positions.Values[posit] = posList.Positions[i].X;
                    posit++;
                    geom_positions.Values[posit] = posList.Positions[i].Y;
                    posit++;
                    geom_positions.Values[posit] = posList.Positions[i].Z;
                    posit++;
                }

                newMesh.source[0].Item = geom_positions;
                newMesh.source[0].technique_common = new sourceTechnique_common();
                newMesh.source[0].technique_common.accessor = new accessor();
                newMesh.source[0].technique_common.accessor.source = "#" + geom_positions.id;
                newMesh.source[0].technique_common.accessor.count = geom_positions.count / 3;
                newMesh.source[0].technique_common.accessor.stride = 3;
                newMesh.source[0].technique_common.accessor.param = new param[3] { new param(), new param(), new param(), };
                newMesh.source[0].technique_common.accessor.param[0].name = "X";
                newMesh.source[0].technique_common.accessor.param[0].type = "float";
                newMesh.source[0].technique_common.accessor.param[1].name = "Y";
                newMesh.source[0].technique_common.accessor.param[1].type = "float";
                newMesh.source[0].technique_common.accessor.param[2].name = "Z";
                newMesh.source[0].technique_common.accessor.param[2].type = "float";

                newMesh.source[1] = new source();
                newMesh.source[1].id = "geometry" + primgroups + "-mesh-normals";
                float_array geom_normals = new float_array();
                geom_normals.id = "geometry" + primgroups + "-mesh-map-array";
                NormalList normals = SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<NormalList>()[0];
                geom_normals.count = (ulong)normals.Normals.Length;
                //geom_normals.Values = new double[geom_normals.count * 3];
                int norm = 0;
                for (ulong i = 0; i < geom_normals.count; i++)
                {
                    geom_normals.Values[norm] = normals.Normals[i].X;
                    norm++;
                    geom_normals.Values[norm] = normals.Normals[i].Y;
                    norm++;
                    geom_normals.Values[norm] = normals.Normals[i].Z;
                    norm++;
                }

                newMesh.source[1].Item = geom_normals;
                newMesh.source[1].technique_common = new sourceTechnique_common();
                newMesh.source[1].technique_common.accessor = new accessor();
                newMesh.source[1].technique_common.accessor.source = "#" + geom_normals.id;
                newMesh.source[1].technique_common.accessor.count = geom_positions.count / 3;
                newMesh.source[1].technique_common.accessor.stride = 3;
                newMesh.source[1].technique_common.accessor.param = new param[3] { new param(), new param(), new param(), };
                newMesh.source[1].technique_common.accessor.param[0].name = "X";
                newMesh.source[1].technique_common.accessor.param[0].type = "float";
                newMesh.source[1].technique_common.accessor.param[1].name = "Y";
                newMesh.source[1].technique_common.accessor.param[1].type = "float";
                newMesh.source[1].technique_common.accessor.param[2].name = "Z";
                newMesh.source[1].technique_common.accessor.param[2].type = "float";

                newMesh.source[2] = new source();
                newMesh.source[2].id = "geometry" + primgroups + "-mesh-map-0";
                float_array geom_texcoords = new float_array();
                geom_texcoords.id = "geometry" + primgroups + "-mesh-map-array";
                UVList UV = SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<UVList>()[0];
                geom_texcoords.count = (ulong)UV.UVs.Length;
                //geom_texcoords.Values = new double[geom_texcoords.count * 2];
                int coord = 0;
                for (ulong i = 0; i < geom_texcoords.count; i++)
                {
                    geom_texcoords.Values[coord] = UV.UVs[i].X;
                    coord++;
                    geom_texcoords.Values[coord] = UV.UVs[i].Y;
                    coord++;
                }

                newMesh.source[2].Item = geom_texcoords;
                newMesh.source[2].technique_common = new sourceTechnique_common();
                newMesh.source[2].technique_common.accessor = new accessor();
                newMesh.source[2].technique_common.accessor.source = "#" + geom_texcoords.id;
                newMesh.source[2].technique_common.accessor.count = geom_texcoords.count / 2;
                newMesh.source[2].technique_common.accessor.stride = 2;
                newMesh.source[2].technique_common.accessor.param = new param[2] { new param(), new param(), };
                newMesh.source[2].technique_common.accessor.param[0].name = "S";
                newMesh.source[2].technique_common.accessor.param[0].type = "float";
                newMesh.source[2].technique_common.accessor.param[1].name = "T";
                newMesh.source[2].technique_common.accessor.param[1].type = "float";

                /*
                newMesh.source[3].id = "geometry" + primgroups + "-colors-Col";
                float_array geom_colors = new float_array();
                geom_colors.id = "geometry" + primgroups + "-colors-Col-array";
                ColourList colors = SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<ColourList>()[0];
                geom_colors.count = (ulong)colors.Colours.Length * 3;
                geom_colors.Values = new double[geom_colors.count];
                for (ulong i = 0; i < geom_colors.count / 3; i++)
                {
                    if (i % 3 == 0)
                    {
                        geom_colors.Values[i] = colors.Colours[i];
                    }
                    else if (i % 3 == 1)
                    {
                        geom_colors.Values[i] = colors.Colours[i / 3];
                    }
                    else
                    {
                        geom_colors.Values[i] = colors.Colours[i / 3];
                    }
                }

                newMesh.source[3].Item = geom_colors;
                newMesh.source[3].technique_common = new sourceTechnique_common();
                newMesh.source[3].technique_common.accessor = new accessor();
                newMesh.source[3].technique_common.accessor.source = geom_colors.id;
                newMesh.source[3].technique_common.accessor.count = geom_colors.count / 3;
                newMesh.source[3].technique_common.accessor.stride = 3;
                newMesh.source[3].technique_common.accessor.param = new param[3];
                newMesh.source[3].technique_common.accessor.param[0].name = "R";
                newMesh.source[3].technique_common.accessor.param[0].type = "float";
                newMesh.source[3].technique_common.accessor.param[1].name = "G";
                newMesh.source[3].technique_common.accessor.param[1].type = "float";
                newMesh.source[3].technique_common.accessor.param[2].name = "B";
                newMesh.source[3].technique_common.accessor.param[2].type = "float";
                */

                //polygons meshTriangles = new polygons();
                //polylist meshTriangles = new polylist();
                triangles meshTriangles = new triangles();
                meshTriangles.material = "";
                meshTriangles.count = (ulong)SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices.Length / 3;
                meshTriangles.input = new InputLocalOffset[3];
                meshTriangles.input[0] = new InputLocalOffset();
                meshTriangles.input[0].semantic = "VERTEX";
                meshTriangles.input[0].source = "#" + newMesh.vertices.id;
                meshTriangles.input[0].offset = 0;
                meshTriangles.input[1] = new InputLocalOffset();
                meshTriangles.input[1].semantic = "NORMAL";
                meshTriangles.input[1].source = "#" + "geometry" + primgroups + "-mesh-normals";
                meshTriangles.input[1].offset = 1;
                meshTriangles.input[2] = new InputLocalOffset();
                meshTriangles.input[2].semantic = "TEXCOORD";
                meshTriangles.input[2].source = "#" + "geometry" + primgroups + "-mesh-map-0";
                meshTriangles.input[2].offset = 2;
                meshTriangles.input[2].set = 0;
                string packed_primitives = "";
                for (int i = 0; i < SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices.Length; i++)
                {
                    packed_primitives += SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices[i];
                    packed_primitives += " ";
                }
                //string packed_vcount = "";
                //for (int i = 0; i < SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices.Length / 4; i++)
                //{
                //    packed_vcount += "4 ";
                //}
                //meshTriangles.vcount = packed_vcount;
                
                meshTriangles.p = packed_primitives;

                newMesh.Items = new object[1];
                newMesh.Items[0] = meshTriangles;

                cachedGeoms.geometry[primgroups].Item = newMesh;

                for (int i = 0; i < SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices.Length; i++)
                {
                    IndexList.Add(SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices[i]);
                }
            }

            cachedMats.material = new material[ShaderChunks.Length];
            List<string> shader_tex = new List<string>();
            for (int mat = 0; mat < ShaderChunks.Length; mat++)
            {
                cachedMats.material[mat] = new material();
                cachedMats.material[mat].id = "mat" + ShaderChunks[mat].Name;
                cachedMats.material[mat].name = ShaderChunks[mat].Name;
                cachedMats.material[mat].instance_effect = new instance_effect();
                cachedMats.material[mat].instance_effect.url = "#" + ShaderChunks[mat].Name; //effect name
                if (ShaderChunks[mat].GetChildren<ShaderTextureParam>().Length > 0)
                {
                    if (!shader_tex.Contains(ShaderChunks[mat].GetChildren<ShaderTextureParam>()[0].Value))
                    {
                        shader_tex.Add(ShaderChunks[mat].GetChildren<ShaderTextureParam>()[0].Value);
                    }
                }
            }

            cachedImages.image = new image[shader_tex.Count];
            for (int i = 0; i < cachedImages.image.Length; i++)
            {
                cachedImages.image[i] = new image();
                cachedImages.image[i].id = "image" + shader_tex[i];
                //fx_surface_init_from_common init_from = new fx_surface_init_from_common();
                //init_from.Value = shader_tex[i];
                //cachedImages.image[i].Item = init_from;
            }

            cachedEffects.effect = new effect[ShaderChunks.Length];
            for (int mat = 0; mat < ShaderChunks.Length; mat++)
            {
                cachedEffects.effect[mat] = new effect();
                cachedEffects.effect[mat].id = ShaderChunks[mat].Name;
                cachedEffects.effect[mat].name = ShaderChunks[mat].Name;
                cachedEffects.effect[mat].newparam = new fx_newparam_common[2];
                cachedEffects.effect[mat].newparam[0] = new fx_newparam_common();
                cachedEffects.effect[mat].newparam[0].sid = "Image-surface";
                cachedEffects.effect[mat].newparam[0].surface = new fx_surface_common();
                cachedEffects.effect[mat].newparam[0].surface.type = fx_surface_type_enum.Item2D;
                cachedEffects.effect[mat].newparam[0].surface.init_from = new fx_surface_init_from_common[1];
                cachedEffects.effect[mat].newparam[0].surface.init_from[0] = new fx_surface_init_from_common();
                cachedEffects.effect[mat].newparam[0].surface.init_from[0].Value = ShaderChunks[mat].Name;
                cachedEffects.effect[mat].newparam[0].surface.format = "A8R8G8B8";

                cachedEffects.effect[mat].newparam[1] = new fx_newparam_common();
                cachedEffects.effect[mat].newparam[1].sid = "Image-sampler";
                cachedEffects.effect[mat].newparam[1].sampler2D = new fx_sampler2D_common();
                cachedEffects.effect[mat].newparam[1].sampler2D.source = "Image-surface";
                cachedEffects.effect[mat].newparam[1].sampler2D.wrap_s = fx_sampler_wrap_common.CLAMP;
                cachedEffects.effect[mat].newparam[1].sampler2D.wrap_t = fx_sampler_wrap_common.CLAMP;
                cachedEffects.effect[mat].newparam[1].sampler2D.minfilter = fx_sampler_filter_common.NEAREST;
                cachedEffects.effect[mat].newparam[1].sampler2D.magfilter = fx_sampler_filter_common.NEAREST;
                cachedEffects.effect[mat].newparam[1].sampler2D.mipfilter = fx_sampler_filter_common.NEAREST;

                cachedEffects.effect[mat].Items = new effectFx_profile_abstractProfile_COMMON[1];
                cachedEffects.effect[mat].Items[0] = new effectFx_profile_abstractProfile_COMMON();
                cachedEffects.effect[mat].Items[0].technique = new effectFx_profile_abstractProfile_COMMONTechnique();
                cachedEffects.effect[mat].Items[0].technique.sid = "common";
                effectFx_profile_abstractProfile_COMMONTechniquePhong common_phong = new effectFx_profile_abstractProfile_COMMONTechniquePhong();
                common_color_or_texture_typeTexture textureType = new common_color_or_texture_typeTexture();
                textureType.texture = "Image-sampler";
                textureType.texcoord = "tc";
                common_phong.diffuse = new common_color_or_texture_type();
                common_phong.diffuse.Item = textureType;
                common_phong.transparent = new common_transparent_type();
                common_phong.transparent.Item = textureType;

                cachedEffects.effect[mat].Items[0].technique.Item = common_phong;
            }

            
            cachedControllers.controller = new controller[SkinChunk.NumPrimGroups];

            for (int a = 0; a < SkinChunk.NumPrimGroups; a++)
            {
                cachedControllers.controller[a] = new controller();
                cachedControllers.controller[a].id = SkinChunk.Name + a;
                cachedControllers.controller[a].name = SkinChunk.Name + a;

                skin ControllerSkin = new skin();

                ControllerSkin.source1 = "#geometry" + a;
                ControllerSkin.source = new source[3];

                ControllerSkin.source[0] = new source();
                ControllerSkin.source[0].id = "controller" + a + "-joints";
                Name_array Joints_Names = new Name_array();
                Joints_Names.id = "controller" + a + "-joints-array";
                Joints_Names.count = (ulong)SkeletonChunk.GetChildren<SkeletonJointCTTR>().Length;
                //Joints_Names.Values = new string[Joints_Names.count];
                for (ulong i = 0; i < Joints_Names.count; i++)
                {
                    //Joints_Names.Values[i] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].Name;
                }

                ControllerSkin.source[0].Item = Joints_Names;
                ControllerSkin.source[0].technique_common = new sourceTechnique_common();
                ControllerSkin.source[0].technique_common.accessor = new accessor();
                ControllerSkin.source[0].technique_common.accessor.source = "#" + Joints_Names.id;
                ControllerSkin.source[0].technique_common.accessor.count = Joints_Names.count;
                ControllerSkin.source[0].technique_common.accessor.param = new param[1] { new param() };
                ControllerSkin.source[0].technique_common.accessor.param[0].name = "JOINT";
                ControllerSkin.source[0].technique_common.accessor.param[0].type = "Name";

                ControllerSkin.source[1] = new source();
                ControllerSkin.source[1].id = "controller" + a + "-bind_poses";
                float_array Bind_Poses = new float_array();
                Bind_Poses.id = "controller" + a + "-bind_poses-array";
                Bind_Poses.count = (ulong)SkeletonChunk.GetChildren<SkeletonJointCTTR>().Length * 16;
                //Bind_Poses.Values = new double[Bind_Poses.count];
                int bind_pose_pos = 0;
                for (ulong i = 0; i < Joints_Names.count; i++)
                {
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M11;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M12;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M13;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M14;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M21;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M22;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M23;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M24;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M31;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M32;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M33;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M34;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M41;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M42;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M43;
                    bind_pose_pos++;
                    Bind_Poses.Values[bind_pose_pos] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].RestPose.M44;
                    bind_pose_pos++;
                }

                ControllerSkin.source[1].Item = Bind_Poses;
                ControllerSkin.source[1].technique_common = new sourceTechnique_common();
                ControllerSkin.source[1].technique_common.accessor = new accessor();
                ControllerSkin.source[1].technique_common.accessor.source = "#" + Bind_Poses.id;
                ControllerSkin.source[1].technique_common.accessor.count = Bind_Poses.count;
                ControllerSkin.source[1].technique_common.accessor.param = new param[1] { new param() };
                ControllerSkin.source[1].technique_common.accessor.param[0].name = "TRANSFORM";
                ControllerSkin.source[1].technique_common.accessor.param[0].type = "float4x4";

                ControllerSkin.source[2] = new source();
                ControllerSkin.source[2].id = "controller" + a + "-weights";
                float_array Weights_Array = new float_array();
                Weights_Array.id = "controller" + a + "-weights-array";
                Weights_Array.count = Joints_Names.count;
                //Weights_Array.Values = new double[Joints_Names.count];
                for (int i = 0; i < Weights_Array.Values.Length; i++)
                {
                    Weights_Array.Values[i] = 1;
                }

                ControllerSkin.source[2].Item = Weights_Array;
                ControllerSkin.source[2].technique_common = new sourceTechnique_common();
                ControllerSkin.source[2].technique_common.accessor = new accessor();
                ControllerSkin.source[2].technique_common.accessor.source = "#" + Weights_Array.id;
                ControllerSkin.source[2].technique_common.accessor.count = Weights_Array.count;
                ControllerSkin.source[2].technique_common.accessor.param = new param[1] { new param() };
                ControllerSkin.source[2].technique_common.accessor.param[0].name = "WEIGHT";
                ControllerSkin.source[2].technique_common.accessor.param[0].type = "float";

                ControllerSkin.joints = new skinJoints();
                ControllerSkin.joints.input = new InputLocal[2] { new InputLocal(), new InputLocal() };
                ControllerSkin.joints.input[0].semantic = "JOINT";
                ControllerSkin.joints.input[0].source = "#" + ControllerSkin.source[0].id;
                ControllerSkin.joints.input[1].semantic = "INV_BIND_MATRIX";
                ControllerSkin.joints.input[1].source = "#" + ControllerSkin.source[1].id;

                ControllerSkin.vertex_weights = new skinVertex_weights();
                ControllerSkin.vertex_weights.count = (ulong)IndexList.Count;
                ControllerSkin.vertex_weights.input = new InputLocalOffset[2] { new InputLocalOffset(), new InputLocalOffset() };
                ControllerSkin.vertex_weights.input[0].semantic = "JOINT";
                ControllerSkin.vertex_weights.input[0].source = "#" + ControllerSkin.source[0].id;
                ControllerSkin.vertex_weights.input[0].offset = 0;
                ControllerSkin.vertex_weights.input[1].semantic = "WEIGHT";
                ControllerSkin.vertex_weights.input[1].source = "#" + ControllerSkin.source[2].id;
                ControllerSkin.vertex_weights.input[1].offset = 1;
                string vertex_vcount = "";
                for (int i = 0; i < IndexList.Count; i++)
                {
                    vertex_vcount += "1 ";
                }
                ControllerSkin.vertex_weights.vcount = vertex_vcount;

                string index_list_str = "";
                for (int i = 0; i < IndexList.Count; i++)
                {
                    index_list_str += IndexList[i] + " ";
                }

                ControllerSkin.vertex_weights.v = index_list_str;

                cachedControllers.controller[a].Item = ControllerSkin;
            }
            

            cachedVscenes.visual_scene = new visual_scene[1] { new visual_scene() };
            cachedVscenes.visual_scene[0].id = "Scene0";
            cachedVscenes.visual_scene[0].name = "Scene0";

            uint RootJointsCount = 0;
            List<SkeletonJointCTTR> Joints = new List<SkeletonJointCTTR>();
            for (int i = 0; i < SkeletonChunk.Children.Count; i++)
            {
                if (SkeletonChunk.Children[i] is SkeletonJointCTTR joint)
                {
                    Joints.Add(joint);
                    if (joint.SkeletonParent == 0)
                    {
                        RootJointsCount++;
                    }
                }
            }

            cachedVscenes.visual_scene[0].node = new node[SkinChunk.NumPrimGroups + 1];
            cachedVscenes.visual_scene[0].node[0] = new node();
            cachedVscenes.visual_scene[0].node[0].sid = "Armature";
            cachedVscenes.visual_scene[0].node[0].id = "Armature";
            cachedVscenes.visual_scene[0].node[0].name = "Armature";
            cachedVscenes.visual_scene[0].node[0].type = NodeType.NODE;
            cachedVscenes.visual_scene[0].node[0].node1 = new node[RootJointsCount];
            for (int i = 0; i < RootJointsCount; i++)
            {
                cachedVscenes.visual_scene[0].node[0].node1[i] = new node();
            }
            cachedVscenes.visual_scene[0].node[0].Items = new object[5];
            cachedVscenes.visual_scene[0].node[0].ItemsElementName = new ItemsChoiceType2[5] { ItemsChoiceType2.translate, ItemsChoiceType2.rotate, ItemsChoiceType2.rotate, ItemsChoiceType2.rotate, ItemsChoiceType2.scale };
            cachedVscenes.visual_scene[0].node[0].Items[0] = new TargetableFloat3() { sid = "location", Values = new double[3] { 0, 0, 0 } };
            cachedVscenes.visual_scene[0].node[0].Items[1] = new rotate() { sid = "rotationZ", Values = new double[4] { 0, 0, 1, 0 } };
            cachedVscenes.visual_scene[0].node[0].Items[2] = new rotate() { sid = "rotationY", Values = new double[4] { 0, 1, 0, 0 } };
            cachedVscenes.visual_scene[0].node[0].Items[3] = new rotate() { sid = "rotationX", Values = new double[4] { 1, 0, 0, 90 } };
            cachedVscenes.visual_scene[0].node[0].Items[4] = new TargetableFloat3() { sid = "scale", Values = new double[3] { 1, 1, 1 } };

            int MainJoint = 0;
            for (int i = 0; i < Joints.Count; i++)
            {
                if (Joints[i].SkeletonParent == 0)
                {
                    AddJointTree(cachedVscenes.visual_scene[0].node[0].node1[MainJoint], Joints[i], i, Joints);
                    MainJoint++;
                }
            }

            for (int i = 1; i < SkinChunk.NumPrimGroups + 1; i++)
            {
                cachedVscenes.visual_scene[0].node[i] = new node();

                cachedVscenes.visual_scene[0].node[i].id = "node" + i;
                cachedVscenes.visual_scene[0].node[i].name = "polygon" + i;
                cachedVscenes.visual_scene[0].node[i].type = NodeType.NODE;
                cachedVscenes.visual_scene[0].node[i].instance_controller = new instance_controller[1] { new instance_controller() };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].url = "#" + cachedControllers.controller[i - 1].id;
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].skeleton = new string[cachedVscenes.visual_scene[0].node[0].node1.Length];
                for (int n = 0; n < cachedVscenes.visual_scene[0].node[0].node1.Length; n++)
                {
                    cachedVscenes.visual_scene[0].node[i].instance_controller[0].skeleton[n] = "#" + cachedVscenes.visual_scene[0].node[0].node1[n].id;
                }
                //cachedVscenes.visual_scene[0].node[i].instance_controller[0].skeleton = new string[1] { "#" + cachedVscenes.visual_scene[0].node[0].node1[0].id };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material = new bind_material();
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common = new instance_material[1] { new instance_material() };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].symbol = "#" + cachedMats.material[0].id;
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].target = "#" + cachedMats.material[0].id;
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].bind_vertex_input = new instance_materialBind_vertex_input[1] { new instance_materialBind_vertex_input() };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].bind_vertex_input[0].semantic = "tc";
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].bind_vertex_input[0].input_semantic = "TEXCOORD";

                cachedVscenes.visual_scene[0].node[i].Items = new object[5];
                cachedVscenes.visual_scene[0].node[i].ItemsElementName = new ItemsChoiceType2[5] { ItemsChoiceType2.translate, ItemsChoiceType2.rotate, ItemsChoiceType2.rotate, ItemsChoiceType2.rotate, ItemsChoiceType2.scale };
                cachedVscenes.visual_scene[0].node[i].Items[0] = new TargetableFloat3() { sid = "location", Values = new double[3] { 0, 0, 0 } };
                cachedVscenes.visual_scene[0].node[i].Items[1] = new rotate() { sid = "rotationZ", Values = new double[4] { 0, 0, 1, 0 } };
                cachedVscenes.visual_scene[0].node[i].Items[2] = new rotate() { sid = "rotationY", Values = new double[4] { 0, 1, 0, 0 } };
                cachedVscenes.visual_scene[0].node[i].Items[3] = new rotate() { sid = "rotationX", Values = new double[4] { 1, 0, 0, 0 } };
                cachedVscenes.visual_scene[0].node[i].Items[4] = new TargetableFloat3() { sid = "scale", Values = new double[3] { 1, 1, 1 } };
            }

            //animclips
            //anims

        }

        public static void AddJointTree(node ParentNode, SkeletonJointCTTR Joint, int JointID, List<SkeletonJointCTTR> Joints)
        {

            ParentNode.id = Joint.Name;
            ParentNode.name = Joint.Name;
            ParentNode.type = NodeType.JOINT;
            matrix NodeMatrix = new matrix();
            float[] NodeMatrixValues = new float[16];
            NodeMatrixValues[0] = Joint.RestPose.M11;
            NodeMatrixValues[1] = Joint.RestPose.M12;
            NodeMatrixValues[2] = Joint.RestPose.M13;
            NodeMatrixValues[3] = Joint.RestPose.M14;
            NodeMatrixValues[4] = Joint.RestPose.M21;
            NodeMatrixValues[5] = Joint.RestPose.M22;
            NodeMatrixValues[6] = Joint.RestPose.M23;
            NodeMatrixValues[7] = Joint.RestPose.M24;
            NodeMatrixValues[8] = Joint.RestPose.M31;
            NodeMatrixValues[9] = Joint.RestPose.M32;
            NodeMatrixValues[10] = Joint.RestPose.M33;
            NodeMatrixValues[11] = Joint.RestPose.M34;
            NodeMatrixValues[12] = Joint.RestPose.M41;
            NodeMatrixValues[13] = Joint.RestPose.M42;
            NodeMatrixValues[14] = Joint.RestPose.M43;
            NodeMatrixValues[15] = Joint.RestPose.M44;
            NodeMatrix.sid = "transform";
            NodeMatrix.Values = ConvertMatrix(NodeMatrixValues); //NodeMatrixValues;
            ParentNode.Items = new object[1];
            ParentNode.ItemsElementName = new ItemsChoiceType2[1] { ItemsChoiceType2.matrix };
            ParentNode.Items[0] = NodeMatrix;
            //ParentNode.extra = new extra[1];
            //extra ExtraData = new extra();
            //ExtraData.technique = new technique[1];
            //technique ExtraTech = new technique();
            //ExtraTech.profile = "blender";
            //ExtraData.technique[0] = ExtraTech;
            //ParentNode.extra[0] = ExtraData;

            uint ChildCount = 0;
            for (int i = 0; i < Joints.Count; i++)
            {
                if (Joints[i].SkeletonParent == JointID && JointID != i && Joints[i].SkeletonParent != 0)
                {
                    ChildCount++;
                }
            }

            ParentNode.node1 = new node[ChildCount];
            int c = 0;
            for (int i = 0; i < Joints.Count; i++)
            {
                if (Joints[i].SkeletonParent == JointID && JointID != i && Joints[i].SkeletonParent != 0)
                {
                    ParentNode.node1[c] = new node();
                    AddJointTree(ParentNode.node1[c], Joints[i], i, Joints);
                    c++;
                }
            }

        }
    }
}
