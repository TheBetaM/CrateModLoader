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
        //Animations: Animation: ID - anim0-joint1
        // Source ID - anim0-joint1-time
	    //  Float array ID - anim0-joint1-time-array, count - anim frames: float array of time from 0 to end time of anim clip
	    //  Technique Common
	    //   Accessor source - float array ID, count - anim frames
	    //    Param name - TIME, type - float
        // Source ID - anim0-joint1-matrix
	    //  Float array ID - anim0-joint1-matrix-array, count - anim frames times 16: float array of matrices
	    //  Technique Common
	    //   Accessor source - float array ID, count - anim frames, stride - 16
	    //    Param name - TRANSFORM, type - float4x4
	    // Source ID - anim0-joint1-interpolation
	    //  Name array ID - anim0-joint1-interpolation-array, count - anim frames: name array of interpolation LINEAR
	    //  Technique Common
	    //   Accessor source - name array ID, count - anim frames
	    //    Param name - INTERPOLATION, type - name
        // Sampler ID - anim0-joint1-sampler
	    //  Input semantic - INPUT, source - time source ID
	    //  Input semantic - OUTPUT, source - matrix source ID
	    //  Input semantic - INTERPOLATION, source - interpolation source ID
        // Channel source - ID of sampler, target - joint1/transform
	    //One animation object for every joint of every animation

        private static library_animation_clips cachedAnim_clips = new library_animation_clips();
        //AnimationClips: Animation Clip ID - any, name - any, end - float (length of animation)
        // Instance Animation URL - same as Animation ID (One of these for all joints of the animation)

        private static library_controllers cachedControllers = new library_controllers();
        //Controllers: Controller: ID - controller0
        // Skin: source - ID of geometry
        //  Source ID - controller0-joints
	    //   Name array ID - controller0-joints-array, count - number of joints: string array of names of joints
	    //   Technique Common
	    //    Accessor source - Name array ID, count - number of joints
	    //     Param Name - JOINT, type - Name
        //  Source ID - controller0-bind_poses
	    //   Float array ID - controller0-bind_poses-array, count - number of joints times 16: float array
	    //   Technique Common
	    //    Accessor source - Float array ID, count - number of joints, stride - 16
	    //     Param Name - TRANSFORM, type - float4x4
        //  Source ID - controller0-weights
	    //   Float array ID - controller0-weights-array, count - array size: float array
	    //   Technique Common
	    //    Accessor source - Float array ID, count - array size
	    //     Param Name - WEIGHT, type - float
        //  Joints
	    //   Input semantic - JOINT, source - ID of joints source
	    //   Input semantic - INV_BIND_MATRIX, source - ID of bind poses
        //  Vertex Weights count - same as geometry positions array size
	    //   Input semantic - JOINT, source - ID of joints source, offset 0
	    //   Input semantic - WEIGHT, source - ID of weights source, offset 1
	    //   Vcount: list of ints, number of bones affected (1) ?
	    //   V: List of indices that describe which bonues are associated with each vertex ? index list?

        private static library_effects cachedEffects = new library_effects();
        //Effects: Effect ID - same id as in material, name - any
        // Profile_Common
        //  Newparam SID - Image-surface
	    //   Surface type - 2D
	    //    Init From: ID of image
	    //    Format: A8R8G8B8
        //  Newparam SID - Image-sampler
	    //   Sampler2D
	    //    Source Image-surface
	    //    Wrap_s CLAMP
	    //    Wrap_t CLAMP
	    //    Minfilter NEAREST
	    //    Magfilter NEAREST
	    //    Mipfilter NEAREST
        //  Technique SID - common
	    //   Phong
	    //    Diffuse
	    //     Texture texture - Image-sampler, texcoord - tc
	    //    Transparent
	    //     Texture texture - Image-sampler, texcoord - tc

        private static library_geometries cachedGeoms = new library_geometries();
        //Geometries: Geometry ID - same as skin source, name - same as visual scene node name?
        // Mesh
        //  Source ID - geometry0-positions
	    //   float array ID - geometry0-positions-array, count - array size: Positions float array
	    //   technique common
	    //    accessor source - float array ID, count - array size divided by 3, stride - 3
	    //     param name - X, type - float
	    //     param name - Y, type - float
	    //     param name - Z, type - float
        //  Source ID - geometry0-texcoords
	    //   float array ID - geometry0-texcoords-array, count - array size: Positions float array
	    //   technique common
	    //    accessor source - float array ID, count - array size divided by 2, stride - 2
	    //     param name - S, type - float
	    //     param name - T, type - float
	    //  Source ID - geometry0-colors
	    //   float array ID - geometry0-colors-array, count - array size: Positions float array
	    //   technique common
	    //    accessor source - float array ID, count - array size divided by 3, stride - 3
	    //     param name - R, type - float
	    //     param name - G, type - float
	    //     param name - B, type - float
        //  Vertices ID - geometry0-vertices
	    //   Input semantic - POSITION, source - Positions source ID
	    //   Input semantic - TEXCOORD, source - Texcoords source ID
	    //   Input semantic - COLOR, source - Colors source ID
        //  Triangles material - ID of material, count - vertices count
	    //   Input semantic - VERTEX, source - ID of Vertices section above, offset 0
	    //   P - Array of primitives (int) ? packed normal list?

        private static library_materials cachedMats = new library_materials();
        //Materials: Material ID - same name as in node and geometry, name any? or same name as effect name
        // Instance Effect URL - effect name

        private static library_visual_scenes cachedVscenes = new library_visual_scenes();
        //Visual Scenes: Visual Scene: ID - same as url in Scene, name any
        // Node Joints ID - joint1, SID - joint1, name any? type JOINT
	    //  Matrix SID - transform: Transform matrix
	    //  Node Joints ID - joint2 SID - joint2 name any? Type JOINT
	    //   ETC (from SkeletonParent of the joint)
        // Node Model ID - node0 name polygon0 type NODE
	    //  Instance Controller URL - same as controller ID
	    //   Skeleton: ID of skeleton root node
	    //   Bind Material
	    //    Technique Common
	    //     Instance Material symbol - same ID as Material, target - same ID as material
	    //      Bind Vertex Input semantic - TC, input_semantic TEXCOORD

        private static library_images cachedImages = new library_images();
        //Images: Image ID - same as in effects
	    // init_from - file name of texture

        public static void LoadAnim(ref Animation refAnim)
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

        }

        public static void ExportModel(string ExportPath)
        {
            COLLADA Model = new COLLADA();

            LoadModel(ref Model);

            Model.Save(ExportPath);
        }

        public static void AddSkinnedModelWithAnimations(ref Skin SkinChunk, ref SkeletonCTTR SkeletonChunk, ref Shader[] ShaderChunks)//, ref Animation[] AnimChunks)
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
                geom_positions.count = (ulong)posList.Positions.Length * 3;
                geom_positions.Values = new double[geom_positions.count];
                for (ulong i = 0; i < geom_positions.count / 3; i++)
                {
                    if (i % 3 == 0)
                    {
                        geom_positions.Values[i] = posList.Positions[i / 3].X;
                    }
                    else if (i % 3 == 1)
                    {
                        geom_positions.Values[i] = posList.Positions[i / 3].Y;
                    }
                    else
                    {
                        geom_positions.Values[i] = posList.Positions[i / 3].Z;
                    }
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
                geom_normals.count = (ulong)normals.Normals.Length * 3;
                geom_normals.Values = new double[geom_normals.count];
                for (ulong i = 0; i < geom_normals.count; i++)
                {
                    if (i % 3 == 0)
                    {
                        geom_normals.Values[i] = normals.Normals[i / 3].X;
                    }
                    else if (i % 3 == 1)
                    {
                        geom_normals.Values[i] = normals.Normals[i / 3].Y;
                    }
                    else
                    {
                        geom_normals.Values[i] = normals.Normals[i / 3].Z;
                    }
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
                geom_texcoords.count = (ulong)UV.UVs.Length * 2;
                geom_texcoords.Values = new double[geom_texcoords.count];
                for (ulong i = 0; i < geom_texcoords.count; i++)
                {
                    ulong pos = i / 2;
                    geom_texcoords.Values[i] = UV.UVs[pos].X;
                    i++;
                    geom_texcoords.Values[i] = UV.UVs[pos].Y;
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

                polylist meshTriangles = new polylist();
                meshTriangles.material = "";
                meshTriangles.count = (ulong)SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices.Length / 4;
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
                string packed_vcount = "";
                for (int i = 0; i < SkinChunk.GetChildren<PrimitiveGroupCTTR>()[primgroups].GetChildren<IndexList>()[0].Indices.Length / 3; i++)
                {
                    packed_vcount += "3 ";
                }
                meshTriangles.vcount = packed_vcount;
                
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
                Joints_Names.Values = new string[Joints_Names.count];
                for (ulong i = 0; i < Joints_Names.count; i++)
                {
                    Joints_Names.Values[i] = SkeletonChunk.GetChildren<SkeletonJointCTTR>()[i].Name;
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
                Bind_Poses.Values = new double[Bind_Poses.count];
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
                Weights_Array.Values = new double[Joints_Names.count];
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
            cachedVscenes.visual_scene[0].node = new node[SkinChunk.NumPrimGroups + 1];
            cachedVscenes.visual_scene[0].node[0] = new node();
            //cachedVscenes.visual_scene[0].node[0].Items = new object[1];
            //cachedVscenes.visual_scene[0].node[0].Items[0] = new matrix();

            for (int i = 0; i < SkeletonChunk.Children.Count; i++)
            {
                SkeletonJointCTTR joint = (SkeletonJointCTTR)SkeletonChunk.Children[i];
                if (joint.SkeletonParent == 0)
                {
                    //cachedVscenes.visual_scene[0].node[0].node1 = new node[10];
                }
            }

            cachedVscenes.visual_scene[0].node[0].id = SkeletonChunk.Name;
            cachedVscenes.visual_scene[0].node[0].type = NodeType.JOINT;

            //todo: joints node tree
            // SkeletonParent - 0 means root, the rest is chunk child index
            // SkeletonJointCTTR.BindPose matrix as transform matrix?

            for (int i = 1; i < SkinChunk.NumPrimGroups + 1; i++)
            {
                cachedVscenes.visual_scene[0].node[i] = new node();

                cachedVscenes.visual_scene[0].node[i].id = "node" + i;
                cachedVscenes.visual_scene[0].node[i].name = "polygon" + i;
                cachedVscenes.visual_scene[0].node[i].type = NodeType.NODE;
                cachedVscenes.visual_scene[0].node[i].instance_controller = new instance_controller[1] { new instance_controller() };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].url = "#" + cachedControllers.controller[i - 1].id;
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].skeleton = new string[1] { "#" + cachedVscenes.visual_scene[0].node[0].id };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material = new bind_material();
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common = new instance_material[1] { new instance_material() };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].symbol = "#" + cachedMats.material[0].id;
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].target = "#" + cachedMats.material[0].id;
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].bind_vertex_input = new instance_materialBind_vertex_input[1] { new instance_materialBind_vertex_input() };
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].bind_vertex_input[0].semantic = "tc";
                cachedVscenes.visual_scene[0].node[i].instance_controller[0].bind_material.technique_common[0].bind_vertex_input[0].input_semantic = "TEXCOORD";
            }
            


            //not working:
            // - vertex weights error out
            // - mesh comes out wrong

            //animclips
            //anims

        }


    }
}
