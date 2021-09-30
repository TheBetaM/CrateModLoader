using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using OpenTK.Audio.OpenAL;
using Twinsanity;
using TwinsaityEditor.Viewers;
using System.Collections.Generic;

namespace TwinsaityEditor
{
    /// <summary>
    /// Contains windows of every worker, utility, viewer and editor for public use
    /// </summary>
    public struct Context
    {
        public Context(ref BDWorker bD, ref ELFPatcher eLF, ref MHWorker mH, ref PSMWorker pSM, ref TextureImport textureImport,
                ref IDAsker iDAsker, ref InstanceScripting instanceScripting, ref LibManager libManager, ref Randomizer randomizer,
                ref Search search, ref TriggerTreeForm triggerTree, ref TwoIDAsker twoIDAsker, ref NewSM2_Dialog newSM2,
                ref GeoDataVis geoDataVis, ref HexView hexView, ref ID4ModelViewer iD4ModelViewer, ref ModelViewer modelViewer,
                ref TextureViewer textureViewer, ref BehaviorEditor behaviorEditor, ref FuckingShitEditor fuckingShitEditor,
                ref GameObjectEditor gameObjectEditor, ref GCEditor gCEditor, ref ID4Editor iD4Editor, ref InstanceEditor instanceEditor,
                ref MaterialEditor materialEditor, ref OGIEditor oGIEditor, ref PathEditor pathEditor, ref PositionEditor positionEditor,
                ref SceneryEditor sceneryEditor, ref SurfaceBehaviorEditor surfaceBehaviorEditor, ref TerrainEditor terrainEditor,
                ref TriggerEditor triggerEditor)
        {
            workerBD = bD;
            workerPatcher = eLF;
            workerMH = mH;
            workerPSM = pSM;
            workerImport = textureImport;

            utilIDAsker = iDAsker;
            utilInstScript = instanceScripting;
            utilLibManager = libManager;
            utilRandomizer = randomizer;
            utilSearch = search;
            utilTriggerTree = triggerTree;
            utilTwoIDAsker = twoIDAsker;
            utilNewSM2 = newSM2;

            viewerGeoData = geoDataVis;
            viewerHex = hexView;
            viewerID4Model = iD4ModelViewer;
            viewerModel = modelViewer;
            viewerTexture = textureViewer;

            editBehavior = behaviorEditor;
            editFuckingShit = fuckingShitEditor;
            editGameObject = gameObjectEditor;
            editGC = gCEditor;
            editID4 = iD4Editor;
            editInstance = instanceEditor;
            editMaterial = materialEditor;
            editOGI = oGIEditor;
            editPath = pathEditor;
            editPosition = positionEditor;
            editScenery = sceneryEditor;
            editSurfBehavior = surfaceBehaviorEditor;
            editTerrain = terrainEditor;
            editTrigger = triggerEditor;
        }
        #region WORKERS
        public readonly BDWorker       workerBD;
        public readonly ELFPatcher     workerPatcher;
        public readonly MHWorker       workerMH;
        public readonly PSMWorker      workerPSM;
        public readonly TextureImport  workerImport;
        #endregion
        #region UTILITIES
        public readonly IDAsker            utilIDAsker;
        public readonly InstanceScripting  utilInstScript;
        public readonly LibManager         utilLibManager;
        public readonly Randomizer         utilRandomizer;
        public readonly Search             utilSearch;
        public readonly TriggerTreeForm    utilTriggerTree;
        public readonly TwoIDAsker         utilTwoIDAsker;
        public readonly NewSM2_Dialog      utilNewSM2;
        #endregion
        #region VIEWERS
        public readonly GeoDataVis     viewerGeoData;
        public readonly HexView        viewerHex;
        public readonly ID4ModelViewer viewerID4Model;
        public readonly ModelViewer    viewerModel;
        public readonly TextureViewer  viewerTexture;
        #endregion
        #region EDITORS
        public readonly BehaviorEditor editBehavior;
        public readonly FuckingShitEditor editFuckingShit;
        public readonly GameObjectEditor editGameObject;
        public readonly GCEditor editGC;
        public readonly ID4Editor editID4;
        public readonly InstanceEditor editInstance;
        public readonly MaterialEditor editMaterial;
        public readonly OGIEditor editOGI;
        public readonly PathEditor editPath;
        public readonly PositionEditor editPosition;
        public readonly SceneryEditor editScenery;
        public readonly SurfaceBehaviorEditor editSurfBehavior;
        public readonly TerrainEditor editTerrain;
        public readonly TriggerEditor editTrigger;
        #endregion
    }

    /// <summary>
    /// The main form controlling everthing
    /// </summary>
    public partial class TwinsanityEditorForm
    {
        /// <summary>
        /// Main initializer
        /// </summary>
        public TwinsanityEditorForm()
        {
            InitializeComponent();
            InitializeWorkers();
            InitializeUtilities();
            InitializeViewers();
            InitializeEditors();
            InitializeContext();
            Control[] controls = new Control[] { _workBD,  _workPatcher,  _workMH,  _workPSM,  _workTextureImport,  _utilIDAsker,  _utilInstanceScripting,
                                  _utilLibManager,  _utilRandomizer,  _utilSearch,  _utilTriggerTree,  _utilTwoIDAsker,  _utilNewSM2,
                                  _viewGeoData,  _viewHex,  _viewID4Model,  _viewModel,  _viewTexture,  _editBehavior,  _editFuckingShit,
                                  _editGameObject,  _editGC,  _editID4,  _editInstance,  _editMaterial,  _editOGI,  _editPath,
                                  _editPosition,  _editScenery,  _editSurfBehavior,  _editTerrain,  _editTrigger};
            Handlers.DisposeHandler.AddObjects(controls);
            Handlers.DisposeHandler.OnControlDisposal += DisposeHandler_ControlDisposal;
        }
        #region INITIALIZERS
        ///<summary>
        ///Initializers workers(BDWorker, ELFPatcher, TextureImporter, etc.)
        ///</summary>
        internal void InitializeWorkers()
        {
            _workBD = new BDWorker { Visible = false };
            _workPatcher = new ELFPatcher { Visible = false };
            _workMH = new MHWorker { Visible = false };
            _workPSM = new PSMWorker { Visible = false };
            _workTextureImport = new TextureImport() {Visible = false };
        }

        ///<summary>
        ///Initializers utilities(LibManager, InstanceScripter, Search, etc.)
        ///</summary>
        internal void InitializeUtilities()
        {
            _utilIDAsker = new IDAsker { Visible = false };
            _utilInstanceScripting = new InstanceScripting { Visible = false };
            _utilLibManager = new LibManager { Visible = false };
            _utilRandomizer = new Randomizer { Visible = false };
            _utilSearch = new Search { Visible = false };
            _utilTriggerTree = new TriggerTreeForm { Visible = false };
            _utilTwoIDAsker = new TwoIDAsker { Visible = false };
            _utilNewSM2 = new NewSM2_Dialog { Visible = false };
        }

        ///<summary>
        ///Initializers viewers(GeoDataViewer, HexViewer, ModelViewer, etc.)
        ///</summary>
        internal void InitializeViewers()
        {
            _viewGeoData = new GeoDataVis { Visible = false };
            _viewHex = new HexView { Visible = false };
            _viewID4Model = new ID4ModelViewer { Visible = false };
            _viewModel = new ModelViewer { Visible = false };
            _viewTexture = new TextureViewer { Visible = false };
        }

        ///<summary>
        ///Initializers editors(InstanceEditor, GameObjectEditor, GCEditor, etc.)
        ///</summary>
        internal void InitializeEditors()
        {
            _editBehavior = new BehaviorEditor(this) { Visible = false };
            _editFuckingShit = new FuckingShitEditor(this) { Visible = false };
            _editGameObject = new GameObjectEditor(this) { Visible = false };
            _editGC = new GCEditor(this) { Visible = false };
            _editID4 = new ID4Editor(this) { Visible = false };
            _editInstance = new InstanceEditor(this) { Visible = false };
            _editMaterial = new MaterialEditor(this) { Visible = false };
            _editOGI = new OGIEditor(this) { Visible = false };
            _editPath = new PathEditor(this) { Visible = false };
            _editPosition = new PositionEditor(this) { Visible = false };
            _editScenery = new SceneryEditor(this) { Visible = false };
            _editSurfBehavior = new SurfaceBehaviorEditor(this) { Visible = false };
            _editTerrain = new TerrainEditor(this) { Visible = false };
            _editTrigger = new TriggerEditor(this) { Visible = false };
        }

        internal void InitializeContext()
        {
            context = new Context(ref _workBD, ref _workPatcher, ref _workMH, ref _workPSM, ref _workTextureImport, ref _utilIDAsker, ref _utilInstanceScripting,
                                 ref _utilLibManager, ref _utilRandomizer, ref _utilSearch, ref _utilTriggerTree, ref _utilTwoIDAsker, ref _utilNewSM2,
                                 ref _viewGeoData, ref _viewHex, ref _viewID4Model, ref _viewModel, ref _viewTexture, ref _editBehavior, ref _editFuckingShit,
                                 ref _editGameObject, ref _editGC, ref _editID4, ref _editInstance, ref _editMaterial, ref _editOGI, ref _editPath,
                                 ref _editPosition, ref _editScenery, ref _editSurfBehavior, ref _editTerrain, ref _editTrigger);
        }
        #endregion

        #region PUBLIC_DATA
        public TwinsFile fileData = new TwinsFile();
        public Context context;
        #endregion
        #region PRIVATE_MEMBERS
        /****WORKERS****/
        private BDWorker _workBD;
        private ELFPatcher _workPatcher;
        private MHWorker _workMH;
        private PSMWorker _workPSM;
        private TextureImport _workTextureImport;
        /**************/
        /****VIEWERS****/
        private GeoDataVis _viewGeoData;
        private HexView _viewHex;
        private ID4ModelViewer _viewID4Model;
        private ModelViewer _viewModel;
        private TextureViewer _viewTexture;
        /***************/
        /****UTILITIES****/
        private IDAsker _utilIDAsker;
        private InstanceScripting _utilInstanceScripting;
        private LibManager _utilLibManager;
        private Randomizer _utilRandomizer;
        private Search _utilSearch;
        private TriggerTreeForm _utilTriggerTree;
        private TwoIDAsker _utilTwoIDAsker;
        private NewSM2_Dialog _utilNewSM2;
        /****************/
        /****EDITORS****/
        private BehaviorEditor _editBehavior;
        private FuckingShitEditor _editFuckingShit;
        private GameObjectEditor _editGameObject;
        private GCEditor _editGC;
        private ID4Editor _editID4;
        private InstanceEditor _editInstance;
        private MaterialEditor _editMaterial;
        private OGIEditor _editOGI;
        private PathEditor _editPath;
        private PositionEditor _editPosition;
        private SceneryEditor _editScenery;
        private SurfaceBehaviorEditor _editSurfBehavior;
        private TerrainEditor _editTerrain;
        private TriggerEditor _editTrigger;
        /***************/

        /*
        'Sets'
        Static RotX As uint = 0

        */
        private uint RotX = 0;
        /*
        Static RotY As uint = 0

        */
        private uint RotY = 0;
        /*
        Static RotZ As uint = 0

        */
        private uint RotZ = 0;
        /*
        Static Size1 As int = 0

        */
        private int Size1 = 0;
        /*
        Static Size2 As int = 0

        */
        private int Size2 = 0;
        /*
        Static Size3 As int = 0

        */
        private int Size3 = 0;
        /*
        Static Something1() As UInt16 = {}

        */
        private ushort[] Something1 = new ushort[] { };
        /*
        Static Something2() As UInt16 = {}

        */
        private ushort[] Something2 = new ushort[] { };
        /*
        Static Something3() As UInt16 = {}

        */
        private ushort[] Something3 = new ushort[] { };
        /*
        Static ParametersHeader As uint = 131328

        */
        private uint ParametersHeader = 131328;
        /*
        Static UnkI32 As uint = 270

        */
        private uint UnkI32 = 270;
        /*
        Static UnkI321Number As int = 0

        */
        private int UnkI321Number = 0;
        /*
        Static UnkI322Number As int = 1

        */
        private int UnkI322Number = 1;
        /*
        Static UnkI323Number As int = 2

        */
        private int UnkI323Number = 2;
        /*
        Static UnkI321() As uint = {}

        */
        private uint[] UnkI321 = new uint[] { };
        /*
        Static UnkI322() As Single = {1.0}

        */
        private float[] UnkI322 = new[] { 1.0f };
        /*
        Static UnkI323() As uint = {0, 0}

        */
        private uint[] UnkI323 = new uint[] { 0, 0 };
        #endregion
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenLevel.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (OpenLevel.FilterIndex == 1)
                    fileData.LoadRM2File(OpenLevel.FileName);
                else if (OpenLevel.FilterIndex == 2)
                    fileData.LoadSM2(OpenLevel.FileName);
                else if (OpenLevel.FilterIndex == 3)
                    fileData.LoadDemoRM2File(OpenLevel.FileName);
                LoadTree();
                //_viewGeoData.VBuffer = null;
                Text = "Twinsaity Editor by Neo_Kesha [" + OpenLevel.FileName + "] ";
            }
        }

        private void DisposeHandler_ControlDisposal(object sender, EventArgs e)
        {
            InitializeWorkers();
            InitializeUtilities();
            InitializeViewers();
            InitializeEditors();
            InitializeContext();

            Control[] controls = new Control[] { _workBD,  _workPatcher,  _workMH,  _workPSM,  _workTextureImport,  _utilIDAsker,  _utilInstanceScripting,
                                  _utilLibManager,  _utilRandomizer,  _utilSearch,  _utilTriggerTree,  _utilTwoIDAsker,  _utilNewSM2,
                                  _viewGeoData,  _viewHex,  _viewID4Model,  _viewModel,  _viewTexture,  _editBehavior,  _editFuckingShit,
                                  _editGameObject,  _editGC,  _editID4,  _editInstance,  _editMaterial,  _editOGI,  _editPath,
                                  _editPosition,  _editScenery,  _editSurfBehavior,  _editTerrain,  _editTrigger};
            Handlers.DisposeHandler.Clear();
            Handlers.DisposeHandler.AddObjects(controls);
        }

        private void LoadTree()
        {
            TreeView1.Nodes.Clear();
            TreeView1.Nodes.Add("Level Root");
            TreeView1.TopNode.Tag = "Root";
            TreeView1.BeginUpdate();
            for (int i = 0; i < fileData.Records; i++)
            {
                TreeNode node = TreeView1.TopNode;
                LoadNode(fileData.Item[i], ref node);
                TreeView1.TopNode = node;
            }
            TreeView1.EndUpdate();
            TreeView1.SelectedNode = TreeView1.TopNode;
        }
        private void LoadNode(BaseObject Item, ref TreeNode Node)
        {
            Node.Nodes.Add("");
            int[] indexes = new int[Node.LastNode.Level - 1 + 1];
            int lvl = Node.LastNode.Level;
            TreeNode TempNode = Node.LastNode;
            if (Item is GraphicsSection)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Graphics Section (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Master";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is CodeSection)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Code Section (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Master";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is DemoCodeSection)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Demo Code Section (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Master";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is InstanceInfoSection)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Instance Info Section (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Master";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is DemoInstanceInfoSection)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Demo Instance Info Section (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Master";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Textures)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Textures (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Materials)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Materials (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Models)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Models (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is ID4Models)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "ID4Models (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is GCs)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "GCs (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is GameObjects)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "GameObjects (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is DemoGameObjects)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "DemoGameObjects (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Scripts)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Scripts (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Animations)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Animations (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is OGIs)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "OGIs (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is SoundDescriptions)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Sound Effects Desc. (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is SoundbankDescriptions)
            {
                switch (Item.ID)
                {
                    case 7:
                        {
                            Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "English Voice (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                            break;
                        }

                    case 8:
                        {
                            Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "French Voice (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                            break;
                        }

                    case 9:
                        {
                            Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "German Voice (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                            break;
                        }

                    case 10:
                        {
                            Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Spanish Voice (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                            break;
                        }

                    case 11:
                        {
                            Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Italian Voice (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                            break;
                        }

                    case 12:
                        {
                            Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Japan(?) Voice (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                            break;
                        }
                }
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Instances)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Instances (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is DemoInstances)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "DemoInstances (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Triggers)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Triggers (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Positions)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Positions (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Paths)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Paths (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is FuckingShits)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "FuckingShits (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Behaviors)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Behaviors (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is SurfaceBehaviours)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Surface's Behaviors (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is Terrains)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Terrains (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is GeoData)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "GeoData ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Texture)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Texture  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Material)
            {
                Material M = (Material)Item;
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + M.Name + "  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Model)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Model  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is ID4Model)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "ID4Model  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Twinsanity.GC)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "GC  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is GameObject)
            {
                GameObject GO = (GameObject)Item;
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + GO.Name + "  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is DemoGameObject)
            {
                DemoGameObject GO = (DemoGameObject)Item;
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + GO.Name + "  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Script)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Script  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
                Script S = (Script)Item;
                if (S.Name.Length > 0)
                    Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + S.Name + "  ID:" + Item.ID.ToString();
            }
            else if (Item is Animation)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Animation  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is OGI)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "OGI  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is SoundDescription)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Sound Description  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Sound)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Sound Effect Bank  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Instance)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Instance  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is DemoInstance)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "DemoInstance  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Trigger)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Trigger  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Position)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Position  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Path)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Path  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is FuckingShit)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "FuckingShit  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Behavior)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Behavior ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is SurfaceBehaviour)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Surface's Behavior  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is SubChunk)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "SubChunks  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Scenery)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Scenery  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is Terrain)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Terrain  ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
            else if (Item is BaseSection)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Unknown Section (" + Item.Records.ToString() + " Records) ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Slave";
                for (int i = 0; i <= Item.Records - 1; i++)
                {
                    TreeNode node = Node.LastNode;
                    LoadNode(Item._Item[i], ref node);
                }
            }
            else if (Item is BaseItem)
            {
                Node.LastNode.Text = Node.LastNode.Index.ToString() + ") " + "Unknown Item ID:" + Item.ID.ToString();
                Node.LastNode.Tag = "Item";
            }
        }

        static public int[] CalculateIndexes(TreeNode Node)
        {
            int[] indexes = new int[Node.Level + 1];
            int lvl = Node.Level;
            TreeNode TempNode = Node;
            while (lvl > 0)
            {
                indexes[Node.Level - lvl + 1] = TempNode.Index;
                TempNode = TempNode.Parent;
                lvl -= 1;
            }
            return indexes;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeView1.SelectedNode.Tag.ToString() == "Root")
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
                Button3.Enabled = false;
                Button4.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button8.Enabled = false;
                Button9.Enabled = false;
                Button10.Enabled = false;
                Button11.Enabled = false;
                Button13.Enabled = false;
                Button14.Enabled = false;
            }
            else if (TreeView1.SelectedNode.Tag.ToString() == "Master")
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
                Button3.Enabled = false;
                Button4.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button8.Enabled = false;
                Button9.Enabled = false;
                Button10.Enabled = false;
                Button11.Enabled = false;
                Button13.Enabled = false;
                Button14.Enabled = false;
            }
            else if (TreeView1.SelectedNode.Tag.ToString() == "Slave")
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
                Button3.Enabled = true;
                Button4.Enabled = false;
                Button5.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
                Button8.Enabled = true;
                Button9.Enabled = false;
                Button10.Enabled = false;
                Button11.Enabled = true;
                Button13.Enabled = false;
                Button14.Enabled = true;
            }
            else if (TreeView1.SelectedNode.Tag.ToString() == "Item")
            {
                Button1.Enabled = true;
                Button2.Enabled = true;
                Button3.Enabled = false;
                Button4.Enabled = true;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button8.Enabled = false;
                Button9.Enabled = true;
                Button10.Enabled = true;
                Button11.Enabled = true;
                Button13.Enabled = true;
                Button14.Enabled = false;
            }
            RefreshSummary();
        }
        private void RefreshSummary()
        {
            System.IO.MemoryStream TextBuffer = new System.IO.MemoryStream();
            System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(TextBuffer);
            if (TreeView1.SelectedNode.Tag.ToString() == "Item")
            {
                BaseItem Obj = (BaseItem)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
                Writer.Write("ID: " + Obj.ID.ToString() + " " + Obj.NodeName);
                Writer.Write("Offset: " + Obj.Offset.ToString() + " Base: " + Obj.Base.ToString() + " Size: " + Obj.Size.ToString());
                if (Obj is GameObject)
                {
                    GameObject GO = (GameObject)Obj;
                    Writer.Write("Name: " + GO.Name);
                }
                else if (Obj is Material)
                {
                    Material MTL = (Material)Obj;
                    Writer.Write("Name: " + MTL.Name);
                }
                else if (Obj is Twinsanity.GC)
                {
                    Twinsanity.GC GC = (Twinsanity.GC)Obj;
                    Writer.Write("Model: " + GC.Model.ToString());
                    Materials MTLs = (Materials)fileData.Item[0]._Item[1];
                    for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                    {
                        for (int j = 0; j <= MTLs._Item.Length - 1; j++)
                        {
                            if (MTLs._Item[j].ID == GC.Material[i])
                            {
                                Material MTL = (Material)MTLs._Item[j];
                                Writer.Write("Name: " + MTL.Name + " (ID: " + MTL.ID.ToString() + ")");
                            }
                        }
                    }
                }
                else if (Obj is Script)
                {
                    Script SCR = (Script)Obj;
                    Writer.Write("Name: " + SCR.Name);
                }
                else if (Obj is Texture)
                {
                    Texture TEX = (Texture)Obj;
                    Writer.Write(TEX.Width.ToString() + "x" + TEX.Height.ToString() + (TEX.Mip > 1 ? " Mip " : "" + (TEX.PaletteFlag == 19 ? " Palette" : "")));
                }
                else if (Obj is Instance)
                {
                    Instance INST = (Instance)Obj;
                    GameObjects Objects = (GameObjects)fileData.Item[1]._Item[0];
                    GameObject GO = null;
                    for (int i = 0; i <= Objects._Item.Length - 1; i++)
                    {
                        if (Objects._Item[i].ID == INST.ObjectID)
                        {
                            GO = (GameObject)Objects._Item[i];
                            break;
                        }
                    }
                    if (!(GO == null))
                        Writer.Write("Object " + GO.Name + " (ID: " + GO.ID.ToString() + ")");
                    else
                        Writer.Write("Object is not defined (ID: " + GO.ID.ToString() + ")");
                    Writer.Write("Position (" + INST.X.ToString() + ";" + INST.Y.ToString() + ";" + INST.Z.ToString() + ")");
                }
                else if (Obj is Trigger)
                {
                    Trigger TRIG = (Trigger)Obj;
                    InstanceInfoSection IIF = (InstanceInfoSection)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent.Parent));
                    for (int k = 0; k <= TRIG.SectionSize - 1; k++)
                    {
                        Instance INST = (Instance)IIF._Item[6]._Item[TRIG.SomeUInt16[k]];
                        GameObjects Objects = (GameObjects)fileData.Item[1]._Item[0];
                        GameObject GO = null;
                        for (int i = 0; i <= Objects._Item.Length - 1; i++)
                        {
                            if (Objects._Item[i].ID == INST.ObjectID)
                            {
                                GO = (GameObject)Objects._Item[i];
                                break;
                            }
                        }
                        Writer.Write("Instance " + TRIG.SomeUInt16[k].ToString());
                        if (!(GO == null))
                            Writer.Write("Object " + GO.Name + " (ID: " + GO.ID.ToString() + ")");
                        else
                            Writer.Write("Object is not defined (ID: " + GO.ID.ToString() + ")");
                    }
                    Writer.Write("Position (" + TRIG.Coordinate[1].X.ToString() + ";" + TRIG.Coordinate[1].Y.ToString() + ";" + TRIG.Coordinate[1].Z.ToString() + ")");
                    Writer.Write("Size (" + TRIG.Coordinate[2].X.ToString() + ";" + TRIG.Coordinate[2].Y.ToString() + ";" + TRIG.Coordinate[2].Z.ToString() + ")");
                }
                else if (Obj is Scenery)
                {
                    Scenery SCEN = (Scenery)Obj;
                    Writer.Write("Header: " + SCEN.Header.ToString());
                    Writer.Write("Header: " + SCEN.LevelName);
                    Writer.Write("Header: " + SCEN.Flags.ToString());
                    Writer.Write("Header: " + SCEN.EntryHeader.ToString());
                    Writer.Write("SkyBox: " + SCEN.SBID.ToString());
                    foreach (Scenery.Entry3 e3 in SCEN.E3)
                    {
                        Writer.Write("Header: " + e3.EntryHeader.ToString());
                        Writer.Write("GCCount: " + e3.GCCount.ToString() + " SBCount: " + e3.SBCount.ToString());
                        for (int i = 0; i <= e3.GCCount + e3.SBCount - 1; i++)
                            Writer.Write(string.Format("{0},({1},{2},{3},{4})-({5},{6},{7},{8})", i, e3.Vector1[i].X, e3.Vector1[i].Y, e3.Vector1[i].Z, e3.Vector1[i].W, e3.Vector2[i].X, e3.Vector2[i].Y, e3.Vector2[i].Z, e3.Vector2[i].W));
                        for (int i = 0; i <= e3.GCCount - 1; i++)
                            Writer.Write("GCID " + i.ToString() + " : " + e3.GCID[i].ToString());
                        for (int i = 0; i <= e3.SBCount - 1; i++)
                            Writer.Write("SBID " + i.ToString() + " : " + e3.SBID[i].ToString());
                        for (int i = 0; i <= e3.GCCount + e3.SBCount - 1; i++)
                        {
                            Writer.Write("Matrix " + i.ToString());
                            Writer.Write(string.Format("{0},{1},{2},{3}", e3.ChunkMatrix[i].x1, e3.ChunkMatrix[i].y1, e3.ChunkMatrix[i].z1, e3.ChunkMatrix[i].w1));
                            Writer.Write(string.Format("{0},{1},{2},{3}", e3.ChunkMatrix[i].x2, e3.ChunkMatrix[i].y2, e3.ChunkMatrix[i].z2, e3.ChunkMatrix[i].w2));
                            Writer.Write(string.Format("{0},{1},{2},{3}", e3.ChunkMatrix[i].x3, e3.ChunkMatrix[i].y3, e3.ChunkMatrix[i].z3, e3.ChunkMatrix[i].w3));
                            Writer.Write(string.Format("{0},{1},{2},{3}", e3.ChunkMatrix[i].x4, e3.ChunkMatrix[i].y4, e3.ChunkMatrix[i].z4, e3.ChunkMatrix[i].w4));
                        }
                    }
                }
                else if (Obj is Terrain)
                {
                    Terrain T = (Terrain)Obj;
                    Writer.Write("Header: " + T.Header.ToString());
                    Writer.Write("Num: " + T.Num.ToString());
                    for (int i = 0; i <= T.Num - 1; i++)
                        Writer.Write("K(" + i.ToString() + "): " + T.K[i].ToString());
                    for (int i = 0; i <= 3; i++)
                        Writer.Write("ID(" + i.ToString() + "): " + T.IDS[i].ToString());
                }
            }
            else
            {
                BaseSection Obj = (BaseSection)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
                Writer.Write("ID: " + Obj.ID.ToString() + " " + Obj.NodeName);
                Writer.Write("Offset: " + Obj.Offset.ToString() + " Base: " + Obj.Base.ToString() + " Size: " + Obj.Size.ToString());
                int n;
                n = Obj?._Item.Length ?? 0;
                Writer.Write("Elements: " + n.ToString() + " ContentSize: " + Obj.ContentSize.ToString());
            }
            TextBuffer.Position = 0;
            System.IO.BinaryReader Reader = new System.IO.BinaryReader(TextBuffer);
            string Text = "";
            while (TextBuffer.Position < TextBuffer.Length)
                Text += Reader.ReadString() + Strings.Chr(13) + Strings.Chr(10);
            Summary.Text = Text;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
            System.IO.MemoryStream Obj = fileData.Get_Stream(indexes);
            _viewHex.LoadHEX(Obj.ToArray(), (int)Obj.Length, 0);
            _viewHex.Show();
        }
        private void Button11_Click(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode.Tag.ToString() == "Item")
            {
                string Name = TreeView1.SelectedNode.Text.Replace("|", " ");
                Name = Name.Replace(":", " ");
                ExtractItem.FileName = Name;
                if (ExtractItem.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Extract(TreeView1.SelectedNode, ExtractItem.FileName);
            }
            else if (TreeView1.SelectedNode.Tag.ToString() == "Slave")
            {
                if (ExtractBunch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i <= TreeView1.SelectedNode.Nodes.Count - 1; i++)
                    {
                        string Folder = TreeView1.SelectedNode.Text.Replace("|", " ");
                        Folder = Folder.Replace(":", " ");
                        string Name = TreeView1.SelectedNode.Nodes[i].Text.Replace("|", " ");
                        Name = Name.Replace(":", " ");
                        System.IO.Directory.CreateDirectory(ExtractBunch.SelectedPath + @"\" + Folder);
                        Extract(TreeView1.SelectedNode.Nodes[i], ExtractBunch.SelectedPath + @"\" + Folder + @"\" + Name);
                    }
                }
            }
        }
        private void Extract(TreeNode Node, string Path)
        {
            int[] indexes = CalculateIndexes(Node);
            System.IO.MemoryStream ObjStream = fileData.Get_Stream(indexes);
            System.IO.FileStream File = new System.IO.FileStream(Path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
            Writer.Write(ObjStream.ToArray());
            File.Close();
        }

        private void TreeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.P)
                    ManagerToolStripMenuItem_Click(null, null);
                else if (e.KeyCode == Keys.V)
                    TexturesToolStripMenuItem_Click(null, null);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = false;
            Button4.Enabled = false;
            Button5.Enabled = false;
            Button6.Enabled = false;
            Button7.Enabled = false;
            Button8.Enabled = false;
            Button9.Enabled = false;
            Button10.Enabled = false;
            Button11.Enabled = false;
            Button13.Enabled = false;
            Button14.Enabled = false;
            _workBD.GetFolderBrowser().SelectedPath = Application.StartupPath;
            LoadLibrary(ref Library);
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (OpenItem.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream File = new System.IO.FileStream(OpenItem.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader Reader = new System.IO.BinaryReader(File);
                System.IO.MemoryStream Stream = new System.IO.MemoryStream();
                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(Stream);
                Writer.Write(Reader.ReadBytes((int)File.Length));
                int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
                fileData.Put_Stream(Stream, indexes);
                Stream.Dispose();
                Stream.Close();
                File.Dispose();
                File.Close();
                BaseObject Obj = fileData.Get_Item(indexes);
                if ((Obj is Material))
                {
                    Material Mat = (Material)Obj;
                    TreeView1.SelectedNode.Text = Mat.Name + " ID:" + Mat.ID.ToString();
                }
                else if ((Obj is GameObject))
                {
                    GameObject GO = (GameObject)Obj;
                    TreeView1.SelectedNode.Text = GO.Name + " ID:" + GO.ID.ToString();
                }
                else if ((Obj is Script))
                {
                    Script S = (Script)Obj;
                    TreeView1.SelectedNode.Text = "Script ID:" + S.ID.ToString();
                    if (S.Name.Length > 0)
                        TreeView1.SelectedNode.Text = S.Name + " ID:" + S.ID.ToString();
                }
                RefreshSummary();
            }
        }
        private void Button3_Click(object sender, EventArgs e) // Here
        {
            if (AddItem.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int j = 0; j <= AddItem.FileNames.Length - 1; j++)
                {
                    System.IO.FileStream File = new System.IO.FileStream(AddItem.FileNames[j], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    System.IO.BinaryReader Reader = new System.IO.BinaryReader(File);
                    System.IO.MemoryStream Stream = new System.IO.MemoryStream();
                    System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(Stream);
                    Writer.Write(Reader.ReadBytes((int)File.Length));
                    int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
                    fileData.Add_Item(indexes);
                    TreeView1.SelectedNode.Nodes.Add("");
                    indexes = CalculateIndexes(TreeView1.SelectedNode.LastNode);
                    fileData.Put_Stream(Stream, indexes);
                    BaseObject NewObj = fileData.Get_Item(indexes);
                    if (AddItem.FileNames[j].Contains("ID"))
                    {
                        string[] str = AddItem.FileNames[j].Split('\\');
                        string Name = str[str.Length - 1];
                        Name = Name.Substring(Name.IndexOf("ID") + 2);
                        Name = Name.Trim(' ');
                        try
                        {
                            NewObj.ID = uint.Parse(Name);
                        }
                        catch (Exception ex)
                        {
                            _utilIDAsker.Get_IDBox().Text = str[str.Length - 1];
                            _utilIDAsker.ShowDialog();
                            NewObj.ID = _utilIDAsker.ID;
                        }
                    }
                    else
                    {
                        _utilIDAsker.ShowDialog();
                        NewObj.ID = _utilIDAsker.ID;
                    }
                    fileData.Put_Item(NewObj, indexes);
                    fileData.Recalculate();
                    Stream.Dispose();
                    Stream.Close();
                    File.Dispose();
                    File.Close();
                    BaseObject Obj = fileData.Get_Item(indexes);
                    if ((Obj is Material))
                    {
                        Material Mat = (Material)Obj;
                        TreeView1.SelectedNode.LastNode.Text = Mat.Name + " ID:" + Mat.ID.ToString();
                    }
                    else if ((Obj is GameObject))
                    {
                        GameObject GO = (GameObject)Obj;
                        TreeView1.SelectedNode.LastNode.Text = GO.Name + " ID:" + GO.ID.ToString();
                    }
                    else if ((Obj is Script))
                    {
                        Script S = (Script)Obj;
                        TreeView1.SelectedNode.LastNode.Text = "Script ID:" + S.ID.ToString();
                        if (S.Name.Length > 0)
                            TreeView1.SelectedNode.LastNode.Text = S.Name + " ID:" + S.ID.ToString();
                    }
                    else if ((Obj is Texture))
                        TreeView1.SelectedNode.LastNode.Text = "Texture ID:" + Obj.ID.ToString();
                    else if ((Obj is Model))
                        TreeView1.SelectedNode.LastNode.Text = "Model ID:" + Obj.ID.ToString();
                    else if ((Obj is ID4Model))
                        TreeView1.SelectedNode.LastNode.Text = "ID4Model ID:" + Obj.ID.ToString();
                    else if ((Obj is Twinsanity.GC))
                        TreeView1.SelectedNode.LastNode.Text = "GC ID:" + Obj.ID.ToString();
                    else if ((Obj is Animation))
                        TreeView1.SelectedNode.LastNode.Text = "Animation ID:" + Obj.ID.ToString();
                    else if ((Obj is OGI))
                        TreeView1.SelectedNode.LastNode.Text = "OGI ID:" + Obj.ID.ToString();
                    else if ((Obj is SoundDescription))
                        TreeView1.SelectedNode.LastNode.Text = "Sound Description ID:" + Obj.ID.ToString();
                    else if ((Obj is Sound))
                        TreeView1.SelectedNode.LastNode.Text = "Sound ID:" + Obj.ID.ToString();
                    else if ((Obj is Instance))
                        TreeView1.SelectedNode.LastNode.Text = "Instance ID:" + Obj.ID.ToString();
                    else if ((Obj is Trigger))
                        TreeView1.SelectedNode.LastNode.Text = "Trigger ID:" + Obj.ID.ToString();
                    else if ((Obj is Position))
                        TreeView1.SelectedNode.LastNode.Text = "Position ID:" + Obj.ID.ToString();
                    else if ((Obj is Path))
                        TreeView1.SelectedNode.LastNode.Text = "Path ID:" + Obj.ID.ToString();
                    else if ((Obj is Behavior))
                        TreeView1.SelectedNode.LastNode.Text = "Behavior ID:" + Obj.ID.ToString();
                    else if ((Obj is FuckingShit))
                        TreeView1.SelectedNode.LastNode.Text = "FuckingShit ID:" + Obj.ID.ToString();
                    else if ((Obj is SurfaceBehaviour))
                        TreeView1.SelectedNode.LastNode.Text = "Surface Behaviour ID:" + Obj.ID.ToString();
                    else if ((Obj is GeoData))
                        TreeView1.SelectedNode.LastNode.Text = "GeoData ID:" + Obj.ID.ToString();
                    else if ((Obj is SubChunk))
                        TreeView1.SelectedNode.LastNode.Text = "SubChunk ID:" + Obj.ID.ToString();
                    else if ((Obj is BaseItem))
                        TreeView1.SelectedNode.LastNode.Text = "Unknown Item ID:" + Obj.ID.ToString();
                    TreeView1.SelectedNode.LastNode.Tag = "Item";
                }
            }
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
            fileData.Add_Item(indexes);
            TreeView1.SelectedNode.Nodes.Add("");
            indexes = CalculateIndexes(TreeView1.SelectedNode.LastNode);
            BaseObject Obj = fileData.Get_Item(indexes);
            if ((Obj is Material))
            {
                Material Mat = (Material)Obj;
                TreeView1.SelectedNode.LastNode.Text = "Material ID:" + Mat.ID.ToString();
            }
            else if ((Obj is GameObject))
            {
                GameObject GO = (GameObject)Obj;
                TreeView1.SelectedNode.LastNode.Text = "Game Object ID:" + GO.ID.ToString();
            }
            else if ((Obj is DemoGameObject))
            {
                DemoGameObject GO = (DemoGameObject)Obj;
                TreeView1.SelectedNode.LastNode.Text = "Demo Game Object ID:" + GO.ID.ToString();
            }
            else if ((Obj is Script))
            {
                Script S = (Script)Obj;
                TreeView1.SelectedNode.LastNode.Text = "Script ID:" + S.ID.ToString();
            }
            else if ((Obj is Texture))
                TreeView1.SelectedNode.LastNode.Text = "Texture ID:" + Obj.ID.ToString();
            else if ((Obj is Model))
                TreeView1.SelectedNode.LastNode.Text = "Model ID:" + Obj.ID.ToString();
            else if ((Obj is ID4Model))
                TreeView1.SelectedNode.LastNode.Text = "ID4Model ID:" + Obj.ID.ToString();
            else if ((Obj is Twinsanity.GC))
                TreeView1.SelectedNode.LastNode.Text = "GC ID:" + Obj.ID.ToString();
            else if ((Obj is Animation))
                TreeView1.SelectedNode.LastNode.Text = "Animation ID:" + Obj.ID.ToString();
            else if ((Obj is OGI))
                TreeView1.SelectedNode.LastNode.Text = "OGI ID:" + Obj.ID.ToString();
            else if ((Obj is SoundDescription))
                TreeView1.SelectedNode.LastNode.Text = "Sound Description ID:" + Obj.ID.ToString();
            else if ((Obj is Sound))
                TreeView1.SelectedNode.LastNode.Text = "Sound ID:" + Obj.ID.ToString();
            else if ((Obj is Instance))
                TreeView1.SelectedNode.LastNode.Text = "Instance ID:" + Obj.ID.ToString();
            else if ((Obj is DemoInstance))
                TreeView1.SelectedNode.LastNode.Text = "DemoInstance ID:" + Obj.ID.ToString();
            else if ((Obj is Trigger))
                TreeView1.SelectedNode.LastNode.Text = "Trigger ID:" + Obj.ID.ToString();
            else if ((Obj is Position))
                TreeView1.SelectedNode.LastNode.Text = "Position ID:" + Obj.ID.ToString();
            else if ((Obj is Path))
                TreeView1.SelectedNode.LastNode.Text = "Path ID:" + Obj.ID.ToString();
            else if ((Obj is Behavior))
                TreeView1.SelectedNode.LastNode.Text = "Behavior ID:" + Obj.ID.ToString();
            else if ((Obj is FuckingShit))
                TreeView1.SelectedNode.LastNode.Text = "FuckingShit ID:" + Obj.ID.ToString();
            else if ((Obj is SurfaceBehaviour))
                TreeView1.SelectedNode.LastNode.Text = "Surface Behaviour ID:" + Obj.ID.ToString();
            else if ((Obj is GeoData))
                TreeView1.SelectedNode.LastNode.Text = "GeoData ID:" + Obj.ID.ToString();
            else if ((Obj is SubChunk))
                TreeView1.SelectedNode.LastNode.Text = "SubChunk ID:" + Obj.ID.ToString();
            else if ((Obj is BaseItem))
                TreeView1.SelectedNode.LastNode.Text = "Unknown Item ID:" + Obj.ID.ToString();
            TreeView1.SelectedNode.LastNode.Tag = "Item";
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            BaseItem Obj = (BaseItem)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
            if (Obj is SoundDescription)
            {
                SoundDescriptions Sect = (SoundDescriptions)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                Sound SoundBank = (Sound)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent.Parent.Nodes[TreeView1.SelectedNode.Parent.Index + 1]));
                SoundDescription RSB = (SoundDescription)Obj;
                int D = (int)RSB.SoundSize;
                int O = (int)RSB.SoundOffset;
                int I = TreeView1.SelectedNode.Index;
                for (int j = I; j <= Sect.Records - 2; j++)
                {
                    SoundDescription SD = (SoundDescription)Sect._Item[j + 1];
                    SD.SoundOffset -= (uint)D;
                    Sect._Item[j] = Sect._Item[j + 1];
                }
                Sect.Records -= 1;
                Array.Resize(ref Sect._Item, Sect.Records);
                System.IO.MemoryStream SB = new System.IO.MemoryStream();
                System.IO.BinaryWriter SBW = new System.IO.BinaryWriter(SB);
                System.IO.BinaryReader SBR = new System.IO.BinaryReader(SoundBank.ByteStream);
                SoundBank.ByteStream.Position = 0;
                SBW.Write(SBR.ReadBytes(O));
                SoundBank.ByteStream.Position += D;
                SBW.Write(SBR.ReadBytes((int)(SoundBank.ByteStream.Length - SoundBank.ByteStream.Position)));
                SB.Position = 0;
                fileData.Put_Stream(SB, CalculateIndexes(TreeView1.SelectedNode.Parent.Parent.Nodes[TreeView1.SelectedNode.Parent.Index + 1]));
                fileData.Put_Item(Sect, CalculateIndexes(TreeView1.SelectedNode.Parent));
            }
            else
            {
                int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
                fileData.Delete_Item(indexes);
            }
            TreeView1.SelectedNode.Remove();
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenLevel.FilterIndex == 1)
                SaveLevel.FilterIndex = 1;
            else if (OpenLevel.FilterIndex == 2)
                SaveLevel.FilterIndex = 3;
            if (System.IO.File.Exists(OpenLevel.FileName))
            {
                if (Interaction.MsgBox("Rewrite old file?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
                    fileData.Save(OpenLevel.FileName);
                else if (SaveLevel.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    fileData.Save(SaveLevel.FileName);
            }
            else if (SaveLevel.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                fileData.Save(SaveLevel.FileName);
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveLevel.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                fileData.Save(SaveLevel.FileName);
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            object Obj = fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
            if (Obj is GameObjects)
            {
                if (ImportGO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string Path = ImportGO.SelectedPath;
                    if (System.IO.File.Exists(Path + @"\main.txt"))
                    {
                        TreeView1.BeginUpdate();
                        System.IO.FileStream Desc = new System.IO.FileStream(Path + @"\main.txt", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        System.IO.StreamReader Reader = new System.IO.StreamReader(Desc);
                        TreeNode Textures = TreeView1.Nodes[0].Nodes[0].Nodes[0];
                        TreeNode Materials = TreeView1.Nodes[0].Nodes[0].Nodes[1];
                        TreeNode Models = TreeView1.Nodes[0].Nodes[0].Nodes[2];
                        TreeNode GCs = TreeView1.Nodes[0].Nodes[0].Nodes[3];
                        TreeNode ID4 = TreeView1.Nodes[0].Nodes[0].Nodes[4];
                        TreeNode ID5 = TreeView1.Nodes[0].Nodes[0].Nodes[5];
                        TreeNode GO = TreeView1.Nodes[0].Nodes[1].Nodes[0];
                        TreeNode Scripts = TreeView1.Nodes[0].Nodes[1].Nodes[1];
                        TreeNode Animations = TreeView1.Nodes[0].Nodes[1].Nodes[2];
                        TreeNode OGIs = TreeView1.Nodes[0].Nodes[1].Nodes[3];
                        TreeNode ID4Scripts = TreeView1.Nodes[0].Nodes[1].Nodes[4];
                        TreeNode SFXD = TreeView1.Nodes[0].Nodes[1].Nodes[5];
                        TreeNode SFX = TreeView1.Nodes[0].Nodes[1].Nodes[6];
                        TreeNode SVE = TreeView1.Nodes[0].Nodes[1].Nodes[7];
                        TreeNode SVF = TreeView1.Nodes[0].Nodes[1].Nodes[8];
                        TreeNode SVG = TreeView1.Nodes[0].Nodes[1].Nodes[9];
                        TreeNode SVS = TreeView1.Nodes[0].Nodes[1].Nodes[10];
                        TreeNode SVI = TreeView1.Nodes[0].Nodes[1].Nodes[11];
                        TreeNode SVJ = TreeView1.Nodes[0].Nodes[1].Nodes[12];
                        while (!Reader.EndOfStream)
                        {
                            string op, ID, ItemPath;
                            string str = Reader.ReadLine();
                            if (str == "END")
                                break;
                            op = str.Split(' ')[0];
                            ID = str.Split(' ')[1];
                            ItemPath = str.Remove(0, op.Length + ID.Length + 1);
                            switch (op)
                            {
                                case "tex":
                                    {
                                        TreeNode Node = Textures;
                                        string NodeName = "Texture ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "mtl":
                                    {
                                        TreeNode Node = Materials;
                                        string NodeName = "Material ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "mdl":
                                    {
                                        TreeNode Node = Models;
                                        string NodeName = "Model ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "gc":
                                    {
                                        TreeNode Node = GCs;
                                        string NodeName = "GC ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "id4":
                                    {
                                        TreeNode Node = ID4;
                                        string NodeName = "ID4Graphics ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "id5":
                                    {
                                        TreeNode Node = ID5;
                                        string NodeName = "ID5Graphics ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "obj":
                                    {
                                        TreeNode Node = GO;
                                        string NodeName = "GameObject ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            GameObject G = (GameObject)ITM;
                                            Node.LastNode.Text = G.Name;
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "scr":
                                    {
                                        TreeNode Node = Scripts;
                                        string NodeName = "Script ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            Script S = (Script)ITM;
                                            Node.LastNode.Text = S.Name;
                                            if (S.Name == "")
                                                Node.LastNode.Text = "Script";
                                            Node.LastNode.Text += " ID: " + ID;
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "ani":
                                    {
                                        TreeNode Node = Animations;
                                        string NodeName = "Animation ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "ogi":
                                    {
                                        TreeNode Node = OGIs;
                                        string NodeName = "OGI ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "id4p":
                                    {
                                        TreeNode Node = ID4Scripts;
                                        string NodeName = "Projectile ";
                                        bool flag = true;
                                        BaseSection BS = (BaseSection)fileData.Get_Item(CalculateIndexes(Node));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            fileData.Add_Item(CalculateIndexes(Node));
                                            Node.Nodes.Add(NodeName + "ID: " + ID);
                                            Node.LastNode.Tag = "Item";
                                            System.IO.FileStream Stream = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader StreamR = new System.IO.BinaryReader(Stream);
                                            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                                            System.IO.BinaryWriter MStreamW = new System.IO.BinaryWriter(MStream);
                                            MStreamW.Write(StreamR.ReadBytes((int)Stream.Length));
                                            fileData.Put_Stream(MStream, CalculateIndexes(Node.LastNode));
                                            BaseItem ITM = (BaseItem)fileData.Get_Item(CalculateIndexes(Node.LastNode));
                                            ITM.ID = uint.Parse(ID);
                                            fileData.Put_Item(ITM, CalculateIndexes(Node.LastNode));
                                        }

                                        break;
                                    }

                                case "sfx":
                                    {
                                        bool flag = true;
                                        SoundDescriptions BS = (SoundDescriptions)fileData.Get_Item(CalculateIndexes(SFXD));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            SoundDescriptions SbD = (SoundDescriptions)fileData.Get_Item(CalculateIndexes(SFXD));
                                            System.IO.FileStream SND = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader _Reader = new System.IO.BinaryReader(SND);
                                            uint _ID = _Reader.ReadUInt32();
                                            UInt16 Frequency = _Reader.ReadUInt16();
                                            uint SoundSize = _Reader.ReadUInt32();
                                            uint SoundOffset;
                                            if (SbD.Records == 0)
                                                SoundOffset = 0;
                                            else
                                            {
                                                SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                                                SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                                            }
                                            byte[] SoundBank = _Reader.ReadBytes((int)SoundSize);
                                            _Reader.Close();
                                            SND.Close();
                                            fileData.Add_Item(CalculateIndexes(SFXD));
                                            SFXD.Nodes.Add("SoundDescription ID: " + ID.ToString());
                                            SFXD.LastNode.Tag = "Item";
                                            SbD = (SoundDescriptions)fileData.Get_Item(CalculateIndexes(SFXD));
                                            System.IO.MemoryStream Stream = fileData.Get_Stream(CalculateIndexes(SFX));
                                            System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                                            StreamWriter.Write(SoundBank);
                                            fileData.Put_Stream(Stream, CalculateIndexes(SFX));
                                            SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(SFXD.LastNode));
                                            SD.ID = _ID;
                                            SD.Head = 3;
                                            SD.Frequency = Frequency;
                                            SD.Param1 = 32;
                                            SD.Param2 = 16;
                                            SD.Param3 = 8192;
                                            SD.Param4 = 8192;
                                            SD.SoundSize = SoundSize;
                                            SD.SoundOffset = SoundOffset;
                                            fileData.Put_Item(SD, CalculateIndexes(SFXD.LastNode));
                                        }

                                        break;
                                    }

                                case "sve":
                                    {
                                        bool flag = true;
                                        SoundbankDescriptions BS = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVE));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            SoundbankDescriptions SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVE));
                                            System.IO.FileStream SND = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader _Reader = new System.IO.BinaryReader(SND);
                                            uint _ID = _Reader.ReadUInt32();
                                            UInt16 Frequency = _Reader.ReadUInt16();
                                            uint SoundSize = _Reader.ReadUInt32();
                                            uint SoundOffset;
                                            if (SbD.Records == 0)
                                                SoundOffset = 0;
                                            else
                                            {
                                                SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                                                SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                                            }
                                            byte[] SoundBank = _Reader.ReadBytes((int)SoundSize);
                                            _Reader.Close();
                                            SND.Close();
                                            fileData.Add_Item(CalculateIndexes(SVE));
                                            SVE.Nodes.Add("SoundDescription ID: " + ID.ToString());
                                            SVE.LastNode.Tag = "Item";
                                            SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVE));
                                            System.IO.MemoryStream Stream = SbD.SoundBank;
                                            System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                                            StreamWriter.Write(SoundBank);
                                            SbD.SoundBank = Stream;
                                            fileData.Put_Item(SbD, CalculateIndexes(SVE));
                                            SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(SVE.LastNode));
                                            SD.ID = _ID;
                                            SD.Head = 3;
                                            SD.Frequency = Frequency;
                                            SD.Param1 = 32;
                                            SD.Param2 = 16;
                                            SD.Param3 = 8192;
                                            SD.Param4 = 8192;
                                            SD.SoundSize = SoundSize;
                                            SD.SoundOffset = SoundOffset;
                                            fileData.Put_Item(SD, CalculateIndexes(SVE.LastNode));
                                        }

                                        break;
                                    }

                                case "svf":
                                    {
                                        bool flag = true;
                                        SoundbankDescriptions BS = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVF));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            SoundbankDescriptions SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVF));
                                            System.IO.FileStream SND = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader _Reader = new System.IO.BinaryReader(SND);
                                            uint _ID = _Reader.ReadUInt32();
                                            UInt16 Frequency = _Reader.ReadUInt16();
                                            uint SoundSize = _Reader.ReadUInt32();
                                            uint SoundOffset;
                                            if (SbD.Records == 0)
                                                SoundOffset = 0;
                                            else
                                            {
                                                SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                                                SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                                            }
                                            byte[] SoundBank = _Reader.ReadBytes((int)SoundSize);
                                            _Reader.Close();
                                            SND.Close();
                                            fileData.Add_Item(CalculateIndexes(SVF));
                                            SVF.Nodes.Add("SoundDescription ID: " + ID.ToString());
                                            SVF.LastNode.Tag = "Item";
                                            SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVF));
                                            System.IO.MemoryStream Stream = SbD.SoundBank;
                                            System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                                            StreamWriter.Write(SoundBank);
                                            SbD.SoundBank = Stream;
                                            fileData.Put_Item(SbD, CalculateIndexes(SVF));
                                            SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(SVF.LastNode));
                                            SD.ID = _ID;
                                            SD.Head = 3;
                                            SD.Frequency = Frequency;
                                            SD.Param1 = 32;
                                            SD.Param2 = 16;
                                            SD.Param3 = 8192;
                                            SD.Param4 = 8192;
                                            SD.SoundSize = SoundSize;
                                            SD.SoundOffset = SoundOffset;
                                            fileData.Put_Item(SD, CalculateIndexes(SVF.LastNode));
                                        }

                                        break;
                                    }

                                case "svg":
                                    {
                                        bool flag = true;
                                        SoundbankDescriptions BS = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVG));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            SoundbankDescriptions SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVG));
                                            System.IO.FileStream SND = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader _Reader = new System.IO.BinaryReader(SND);
                                            uint _ID = _Reader.ReadUInt32();
                                            UInt16 Frequency = _Reader.ReadUInt16();
                                            uint SoundSize = _Reader.ReadUInt32();
                                            uint SoundOffset;
                                            if (SbD.Records == 0)
                                                SoundOffset = 0;
                                            else
                                            {
                                                SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                                                SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                                            }
                                            byte[] SoundBank = _Reader.ReadBytes((int)SoundSize);
                                            _Reader.Close();
                                            SND.Close();
                                            fileData.Add_Item(CalculateIndexes(SVG));
                                            SVG.Nodes.Add("SoundDescription ID: " + ID.ToString());
                                            SVG.LastNode.Tag = "Item";
                                            SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVG));
                                            System.IO.MemoryStream Stream = SbD.SoundBank;
                                            System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                                            StreamWriter.Write(SoundBank);
                                            SbD.SoundBank = Stream;
                                            fileData.Put_Item(SbD, CalculateIndexes(SVG));
                                            SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(SVG.LastNode));
                                            SD.ID = _ID;
                                            SD.Head = 3;
                                            SD.Frequency = Frequency;
                                            SD.Param1 = 32;
                                            SD.Param2 = 16;
                                            SD.Param3 = 8192;
                                            SD.Param4 = 8192;
                                            SD.SoundSize = SoundSize;
                                            SD.SoundOffset = SoundOffset;
                                            fileData.Put_Item(SD, CalculateIndexes(SVG.LastNode));
                                        }

                                        break;
                                    }

                                case "svs":
                                    {
                                        bool flag = true;
                                        SoundbankDescriptions BS = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVS));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            SoundbankDescriptions SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVS));
                                            System.IO.FileStream SND = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader _Reader = new System.IO.BinaryReader(SND);
                                            uint _ID = _Reader.ReadUInt32();
                                            UInt16 Frequency = _Reader.ReadUInt16();
                                            uint SoundSize = _Reader.ReadUInt32();
                                            uint SoundOffset;
                                            if (SbD.Records == 0)
                                                SoundOffset = 0;
                                            else
                                            {
                                                SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                                                SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                                            }
                                            byte[] SoundBank = _Reader.ReadBytes((int)SoundSize);
                                            _Reader.Close();
                                            SND.Close();
                                            fileData.Add_Item(CalculateIndexes(SVS));
                                            SVS.Nodes.Add("SoundDescription ID: " + ID.ToString());
                                            SVS.LastNode.Tag = "Item";
                                            SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVS));
                                            System.IO.MemoryStream Stream = SbD.SoundBank;
                                            System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                                            StreamWriter.Write(SoundBank);
                                            SbD.SoundBank = Stream;
                                            fileData.Put_Item(SbD, CalculateIndexes(SVS));
                                            SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(SVS.LastNode));
                                            SD.ID = _ID;
                                            SD.Head = 3;
                                            SD.Frequency = Frequency;
                                            SD.Param1 = 32;
                                            SD.Param2 = 16;
                                            SD.Param3 = 8192;
                                            SD.Param4 = 8192;
                                            SD.SoundSize = SoundSize;
                                            SD.SoundOffset = SoundOffset;
                                            fileData.Put_Item(SD, CalculateIndexes(SVS.LastNode));
                                        }

                                        break;
                                    }

                                case "svi":
                                    {
                                        bool flag = true;
                                        SoundbankDescriptions BS = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVI));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            SoundbankDescriptions SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVI));
                                            System.IO.FileStream SND = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader _Reader = new System.IO.BinaryReader(SND);
                                            uint _ID = _Reader.ReadUInt32();
                                            UInt16 Frequency = _Reader.ReadUInt16();
                                            uint SoundSize = _Reader.ReadUInt32();
                                            uint SoundOffset;
                                            if (SbD.Records == 0)
                                                SoundOffset = 0;
                                            else
                                            {
                                                SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                                                SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                                            }
                                            byte[] SoundBank = _Reader.ReadBytes((int)SoundSize);
                                            _Reader.Close();
                                            SND.Close();
                                            fileData.Add_Item(CalculateIndexes(SVI));
                                            SVI.Nodes.Add("SoundDescription ID: " + ID.ToString());
                                            SVI.LastNode.Tag = "Item";
                                            SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVI));
                                            System.IO.MemoryStream Stream = SbD.SoundBank;
                                            System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                                            StreamWriter.Write(SoundBank);
                                            SbD.SoundBank = Stream;
                                            fileData.Put_Item(SbD, CalculateIndexes(SVI));
                                            SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(SVI.LastNode));
                                            SD.ID = _ID;
                                            SD.Head = 3;
                                            SD.Frequency = Frequency;
                                            SD.Param1 = 32;
                                            SD.Param2 = 16;
                                            SD.Param3 = 8192;
                                            SD.Param4 = 8192;
                                            SD.SoundSize = SoundSize;
                                            SD.SoundOffset = SoundOffset;
                                            fileData.Put_Item(SD, CalculateIndexes(SVI.LastNode));
                                        }

                                        break;
                                    }

                                case "svj":
                                    {
                                        bool flag = true;
                                        SoundbankDescriptions BS = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVJ));
                                        for (int i = 0; i <= BS.Records - 1; i++)
                                        {
                                            if (uint.Parse(ID) == BS._Item[i].ID)
                                            {
                                                i = BS.Records;
                                                flag = false;
                                            }
                                        }
                                        if (flag)
                                        {
                                            SoundbankDescriptions SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVJ));
                                            System.IO.FileStream SND = new System.IO.FileStream(Path + ItemPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                            System.IO.BinaryReader _Reader = new System.IO.BinaryReader(SND);
                                            uint _ID = _Reader.ReadUInt32();
                                            UInt16 Frequency = _Reader.ReadUInt16();
                                            uint SoundSize = _Reader.ReadUInt32();
                                            uint SoundOffset;
                                            if (SbD.Records == 0)
                                                SoundOffset = 0;
                                            else
                                            {
                                                SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                                                SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                                            }
                                            byte[] SoundBank = _Reader.ReadBytes((int)SoundSize);
                                            _Reader.Close();
                                            SND.Close();
                                            fileData.Add_Item(CalculateIndexes(SVJ));
                                            SVJ.Nodes.Add("SoundDescription ID: " + ID.ToString());
                                            SVJ.LastNode.Tag = "Item";
                                            SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(SVJ));
                                            System.IO.MemoryStream Stream = SbD.SoundBank;
                                            System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                                            StreamWriter.Write(SoundBank);
                                            SbD.SoundBank = Stream;
                                            fileData.Put_Item(SbD, CalculateIndexes(SVJ));
                                            SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(SVJ.LastNode));
                                            SD.ID = _ID;
                                            SD.Head = 3;
                                            SD.Frequency = Frequency;
                                            SD.Param1 = 32;
                                            SD.Param2 = 16;
                                            SD.Param3 = 8192;
                                            SD.Param4 = 8192;
                                            SD.SoundSize = SoundSize;
                                            SD.SoundOffset = SoundOffset;
                                            fileData.Put_Item(SD, CalculateIndexes(SVJ.LastNode));
                                        }

                                        break;
                                    }
                            }
                            Application.DoEvents();
                        }
                        TreeView1.EndUpdate();
                        Interaction.MsgBox("Done.");
                    }
                    else
                        Interaction.MsgBox("Main.txt not found");
                }
            }
            else if (Obj is SoundDescriptions)
            {
                if (AddItem.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SoundDescriptions SbD = (SoundDescriptions)Obj;
                    for (int i = 0; i <= AddItem.FileNames.Length - 1; i++)
                    {
                        System.IO.FileStream SND = new System.IO.FileStream(AddItem.FileNames[i], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        System.IO.BinaryReader Reader = new System.IO.BinaryReader(SND);
                        uint ID = Reader.ReadUInt32();
                        UInt16 Frequency = Reader.ReadUInt16();
                        uint SoundSize = Reader.ReadUInt32();
                        uint SoundOffset;
                        if (SbD.Records == 0)
                            SoundOffset = 0;
                        else
                        {
                            SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                            SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                        }
                        byte[] SoundBank = Reader.ReadBytes((int)SoundSize);
                        Reader.Close();
                        SND.Close();
                        fileData.Add_Item(CalculateIndexes(TreeView1.SelectedNode));
                        TreeView1.SelectedNode.Nodes.Add("SoundDescription ID: " + ID.ToString());
                        TreeView1.SelectedNode.LastNode.Tag = "Item";
                        SbD = (SoundDescriptions)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
                        System.IO.MemoryStream Stream = fileData.Get_Stream(CalculateIndexes(TreeView1.SelectedNode.Parent.Nodes[6]));
                        System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                        StreamWriter.Write(SoundBank);
                        fileData.Put_Stream(Stream, CalculateIndexes(TreeView1.SelectedNode.Parent.Nodes[6]));
                        SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.LastNode));
                        SD.ID = ID;
                        SD.Head = 3;
                        SD.Frequency = Frequency;
                        SD.Param1 = 32;
                        SD.Param2 = 16;
                        SD.Param3 = 8192;
                        SD.Param4 = 8192;
                        SD.SoundSize = SoundSize;
                        SD.SoundOffset = SoundOffset;
                        fileData.Put_Item(SD, CalculateIndexes(TreeView1.SelectedNode.LastNode));
                    }
                }
            }
            else if (Obj is SoundbankDescriptions)
            {
                if (AddItem.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SoundbankDescriptions SbD = (SoundbankDescriptions)Obj;
                    for (int i = 0; i <= AddItem.FileNames.Length - 1; i++)
                    {
                        System.IO.FileStream SND = new System.IO.FileStream(AddItem.FileNames[i], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        System.IO.BinaryReader Reader = new System.IO.BinaryReader(SND);
                        uint ID = Reader.ReadUInt32();
                        UInt16 Frequency = Reader.ReadUInt16();
                        uint SoundSize = Reader.ReadUInt32();
                        uint SoundOffset;
                        if (SbD.Records == 0)
                            SoundOffset = 0;
                        else
                        {
                            SoundDescription SDesc = (SoundDescription)SbD._Item[SbD._Item.Length - 1];
                            SoundOffset = SDesc.SoundSize + SDesc.SoundOffset;
                        }
                        byte[] SoundBank = Reader.ReadBytes((int)SoundSize);
                        Reader.Close();
                        SND.Close();
                        fileData.Add_Item(CalculateIndexes(TreeView1.SelectedNode));
                        TreeView1.SelectedNode.Nodes.Add("SoundDescription ID: " + ID.ToString());
                        TreeView1.SelectedNode.LastNode.Tag = "Item";
                        SbD = (SoundbankDescriptions)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
                        System.IO.MemoryStream Stream = SbD.SoundBank;
                        System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(Stream);
                        StreamWriter.Write(SoundBank);
                        SbD.SoundBank = Stream;
                        fileData.Put_Item(SbD, CalculateIndexes(TreeView1.SelectedNode));
                        SoundDescription SD = (SoundDescription)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.LastNode));
                        SD.ID = ID;
                        SD.Head = 3;
                        SD.Frequency = Frequency;
                        SD.Param1 = 32;
                        SD.Param2 = 16;
                        SD.Param3 = 8192;
                        SD.Param4 = 8192;
                        SD.SoundSize = SoundSize;
                        SD.SoundOffset = SoundOffset;
                        fileData.Put_Item(SD, CalculateIndexes(TreeView1.SelectedNode.LastNode));
                    }
                }
            }
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            if (_utilTwoIDAsker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
                BaseSection Section = (BaseSection)fileData.Get_Item(indexes);
                TreeNode Node1 = new TreeNode(), Node2 = new TreeNode();
                int ind1, ind2;
                ind1 = -1;
                ind2 = -1;
                for (int i = 0; i <= Section.Records - 1; i++)
                {
                    if (Section._Item[i].ID == _utilTwoIDAsker.ID1)
                    {
                        Node1 = TreeView1.SelectedNode.Nodes[i];
                        ind1 = i;
                    }
                    if (Section._Item[i].ID == _utilTwoIDAsker.ID2)
                    {
                        Node2 = TreeView1.SelectedNode.Nodes[i];
                        ind2 = i;
                    }
                }
                if ((!(ind1 == -1)) & (!(ind2 == -1)))
                {
                    int[] indexes1 = CalculateIndexes(Node1);
                    int[] indexes2 = CalculateIndexes(Node2);
                    fileData.Put_Stream(fileData.Get_Stream(indexes2), indexes1);
                    BaseObject Obj = fileData.Get_Item(indexes1);
                    if ((Obj is Material))
                    {
                        Material Mat = (Material)Obj;
                        TreeView1.SelectedNode.Nodes[ind1].Text = Mat.Name + " ID:" + Mat.ID.ToString();
                    }
                    else if ((Obj is GameObject))
                    {
                        GameObject GO = (GameObject)Obj;
                        TreeView1.SelectedNode.Nodes[ind1].Text = GO.Name + " ID:" + GO.ID.ToString();
                    }
                    else if ((Obj is Script))
                    {
                        Script S = (Script)Obj;
                        TreeView1.SelectedNode.Nodes[ind1].Text = "Script ID:" + S.ID.ToString();
                        if (S.Name.Length > 0)
                            TreeView1.SelectedNode.Nodes[ind1].Text = S.Name + " ID:" + S.ID.ToString();
                    }
                }
                else
                    Interaction.MsgBox("Cannot find one of those IDs");
            }
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Button14_Click(object sender, EventArgs e)
        {
            int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
            int[] inst_indexes;
            BaseObject Obj = fileData.Get_Item(indexes);
        
        // ----'
            if (Obj is Instances)
            {
                if (_utilInstanceScripting.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] script = _utilInstanceScripting.Get_ScriptBox().Lines;
                    for (int i = 0; i <= script.Length - 1; i++)
                    {
                        string[] cmd = script[i].Split('|');
                        try
                        {
                            switch (cmd[0])
                            {
                                case "LIBPOINT":
                                    {
                                        float X = float.Parse(cmd[1]);
                                        float Y = float.Parse(cmd[2]);
                                        float Z = float.Parse(cmd[3]);
                                        Libr rec = new Libr { ID = 0 };
                                        try
                                        {
                                            uint ID = uint.Parse(cmd[4]);
                                            for (int n = 0; n <= Library.Length - 1; n++)
                                            {
                                                if (Library[n].ID == ID)
                                                {
                                                    rec = Library[n];
                                                    n = Library.Length;
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            string Name = cmd[4];
                                            for (int n = 0; n <= Library.Length - 1; n++)
                                            {
                                                if (Library[n].Name == Name)
                                                {
                                                    rec = Library[n];
                                                    n = Library.Length;
                                                }
                                            }
                                        }
                                        fileData.Add_Item(indexes);
                                        TreeView1.SelectedNode.Nodes.Add("");
                                        inst_indexes = CalculateIndexes(TreeView1.SelectedNode.LastNode);
                                        Instance INST = (Instance)fileData.Get_Item(inst_indexes);
                                        INST.X = X;
                                        INST.Y = Y;
                                        INST.Z = Z;
                                        INST.ObjectID = (ushort)rec.ID;
                                        // PARAMS
                                        INST.RX = (ushort)RotX;
                                        INST.RY = (ushort)RotY;
                                        INST.RZ = (ushort)RotZ;
                                        INST.Size1 = rec.InstReferSize;
                                        INST.Size2 = rec.PosReferSize;
                                        INST.Size3 = rec.PathReferSize;
                                        INST.Something1 = rec.InstRefer;
                                        INST.Something2 = rec.PosRefer;
                                        INST.Something3 = rec.PathRefer;
                                        INST.UnkI32 = rec.ParamNumber;
                                        INST.UnkI321Number = rec.SomeParamSize;
                                        INST.UnkI322Number = rec.FloatSize;
                                        INST.UnkI323Number = rec.IntSize;
                                        INST.UnkI321 = rec.SomeParams;
                                        INST.UnkI322 = rec.Floats;
                                        INST.UnkI323 = rec.Ints;
                                        // ------
                                        fileData.Put_Item(INST, inst_indexes);
                                        TreeView1.SelectedNode.LastNode.Text = "Instance ID:" + INST.ID.ToString();
                                        TreeView1.SelectedNode.LastNode.Tag = "Item";
                                        break;
                                    }

                                case "LIBFILL":
                                    {
                                        float X = float.Parse(cmd[1]);
                                        float Y = float.Parse(cmd[2]);
                                        float Z = float.Parse(cmd[3]);
                                        float W = float.Parse(cmd[4]);
                                        float H = float.Parse(cmd[5]);
                                        float L = float.Parse(cmd[6]);
                                        float StX = float.Parse(cmd[7]);
                                        float StY = float.Parse(cmd[8]);
                                        float StZ = float.Parse(cmd[9]);
                                        Libr rec = new Libr { ID = 0 };
                                        try
                                        {
                                            uint ID = uint.Parse(cmd[10]);
                                            for (int n = 0; n <= Library.Length - 1; n++)
                                            {
                                                if (Library[n].ID == ID)
                                                {
                                                    rec = Library[n];
                                                    n = Library.Length;
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            string Name = cmd[10];
                                            for (int n = 0; n <= Library.Length - 1; n++)
                                            {
                                                if (Library[n].Name == Name)
                                                {
                                                    rec = Library[n];
                                                    n = Library.Length;
                                                }
                                            }
                                        }
                                        int shX, shY, shZ;
                                        shX = 0;
                                        shY = 0;
                                        shZ = 0;
                                        while (X + shX * StX < X + W)
                                        {
                                            while (Y + shY * StY < Y + H)
                                            {
                                                while (Z + shZ * StZ < Z + L)
                                                {
                                                    fileData.Add_Item(indexes);
                                                    TreeView1.SelectedNode.Nodes.Add("");
                                                    inst_indexes = CalculateIndexes(TreeView1.SelectedNode.LastNode);
                                                    Instance INST = (Instance)fileData.Get_Item(inst_indexes);
                                                    INST.X = X + shX * StX;
                                                    INST.Y = Y + shY * StY;
                                                    INST.Z = Z + shZ * StZ;
                                                    INST.ObjectID = (ushort)rec.ID;
                                                    INST.RX = (ushort)RotX;
                                                    INST.RY = (ushort)RotY;
                                                    INST.RZ = (ushort)RotZ;
                                                    INST.Size1 = rec.InstReferSize;
                                                    INST.Size2 = rec.PosReferSize;
                                                    INST.Size3 = rec.PathReferSize;
                                                    INST.Something1 = rec.InstRefer;
                                                    INST.Something2 = rec.PosRefer;
                                                    INST.Something3 = rec.PathRefer;
                                                    INST.UnkI32 = rec.ParamNumber;
                                                    INST.UnkI321Number = rec.SomeParamSize;
                                                    INST.UnkI322Number = rec.FloatSize;
                                                    INST.UnkI323Number = rec.IntSize;
                                                    INST.UnkI321 = rec.SomeParams;
                                                    INST.UnkI322 = rec.Floats;
                                                    INST.UnkI323 = rec.Ints;
                                                    fileData.Put_Item(INST, inst_indexes);
                                                    TreeView1.SelectedNode.LastNode.Text = "Instance ID:" + INST.ID.ToString();
                                                    TreeView1.SelectedNode.LastNode.Tag = "Item";
                                                    shZ += 1;
                                                }
                                                shZ = 0;
                                                shY += 1;
                                            }
                                            shY = 0;
                                            shX += 1;
                                        }

                                        break;
                                    }

                                case "SET":
                                    {
                                        switch (cmd[1])
                                        {
                                            case "LIB":
                                                {
                                                    Libr rec = new Libr { };
                                                    try
                                                    {
                                                        uint ID = uint.Parse(cmd[2]);
                                                        for (int n = 0; n <= Library.Length - 1; n++)
                                                        {
                                                            if (Library[n].ID == ID)
                                                                rec = Library[n];
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        string Name = cmd[2];
                                                        for (int n = 0; n <= Library.Length - 1; n++)
                                                        {
                                                            if (Library[n].Name == Name)
                                                                rec = Library[n];
                                                        }
                                                    }
                                                    Size1 = rec.InstReferSize;
                                                    Size2 = rec.PosReferSize;
                                                    Size3 = rec.PathReferSize;
                                                    Something1 = rec.InstRefer;
                                                    Something2 = rec.PosRefer;
                                                    Something3 = rec.PathRefer;
                                                    UnkI32 = rec.ParamNumber;
                                                    UnkI321Number = rec.SomeParamSize;
                                                    UnkI322Number = rec.FloatSize;
                                                    UnkI323Number = rec.IntSize;
                                                    UnkI321 = rec.SomeParams;
                                                    UnkI322 = rec.Floats;
                                                    UnkI323 = rec.Ints;
                                                    break;
                                                }

                                            case "CLEAR":
                                                {
                                                    RotX = 0;
                                                    RotY = 0;
                                                    RotZ = 0;
                                                    Size1 = 0;
                                                    Size2 = 0;
                                                    Size3 = 0;
                                                    Something1 = new ushort[] { };
                                                    Something2 = new ushort[] { };
                                                    Something3 = new ushort[] { };
                                                    ParametersHeader = 131328;
                                                    UnkI32 = 270;
                                                    UnkI321Number = 0;
                                                    UnkI322Number = 1;
                                                    UnkI323Number = 2;
                                                    UnkI321 = new uint[] { };
                                                    UnkI322 = new float[] { 1065353216 };
                                                    UnkI323 = new uint[] { 0, 0 };
                                                    break;
                                                }

                                            case "COPY":
                                                {
                                                    Instances Section = (Instances)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
                                                    uint ID = uint.Parse(cmd[2]);
                                                    for (int j = 0; j <= Section.Records - 1; j++)
                                                    {
                                                        if (Section._Item[j].ID == ID)
                                                        {
                                                            Instance INST = (Instance)Section._Item[j];
                                                            RotX = INST.RX;
                                                            RotY = INST.RY;
                                                            RotZ = INST.RZ;
                                                            Size1 = INST.Size1;
                                                            Size2 = INST.Size2;
                                                            Size3 = INST.Size3;
                                                            Something1 = INST.Something1;
                                                            Something2 = INST.Something2;
                                                            Something3 = INST.Something3;
                                                            ParametersHeader = INST.ParametersHeader;
                                                            UnkI32 = INST.UnkI32;
                                                            UnkI321Number = INST.UnkI321Number;
                                                            UnkI322Number = INST.UnkI322Number;
                                                            UnkI323Number = INST.UnkI323Number;
                                                            UnkI321 = INST.UnkI321;
                                                            UnkI322 = INST.UnkI322;
                                                            UnkI323 = INST.UnkI323;
                                                        }
                                                    }

                                                    break;
                                                }

                                            case "ROTX":
                                                {
                                                    RotX = UInt16.Parse(cmd[2]);
                                                    break;
                                                }

                                            case "ROTY":
                                                {
                                                    RotY = UInt16.Parse(cmd[2]);
                                                    break;
                                                }

                                            case "ROTZ":
                                                {
                                                    RotX = UInt16.Parse(cmd[2]);
                                                    break;
                                                }

                                            case "ROT":
                                                {
                                                    RotX = UInt16.Parse(cmd[2]);
                                                    RotY = UInt16.Parse(cmd[3]);
                                                    RotZ = UInt16.Parse(cmd[4]);
                                                    break;
                                                }

                                            case "SOME1":
                                                {
                                                    Size1 = int.Parse(cmd[2]);
                                                    Array.Resize(ref Something1, Size1);
                                                    for (int n = 0; n <= Size1 - 1; n++)
                                                        Something1[n] = UInt16.Parse(cmd[3 + n]);
                                                    break;
                                                }

                                            case "SOME2":
                                                {
                                                    Size2 = int.Parse(cmd[2]);
                                                    Array.Resize(ref Something2, Size2);
                                                    for (int n = 0; n <= Size2 - 1; n++)
                                                        Something2[n] = UInt16.Parse(cmd[3 + n]);
                                                    break;
                                                }

                                            case "SOME3":
                                                {
                                                    Size3 = int.Parse(cmd[2]);
                                                    Array.Resize(ref Something3, Size3);
                                                    for (int n = 0; n <= Size3 - 1; n++)
                                                        Something3[n] = UInt16.Parse(cmd[3 + n]);
                                                    break;
                                                }

                                            case "PARAMHEAD":
                                                {
                                                    ParametersHeader = uint.Parse(cmd[2]);
                                                    break;
                                                }

                                            case "PARAMNUMBER":
                                                {
                                                    UnkI32 = uint.Parse(cmd[2]);
                                                    break;
                                                }

                                            case "PARAM1":
                                                {
                                                    UnkI321Number = int.Parse(cmd[2]);
                                                    Array.Resize(ref UnkI321, UnkI321Number);
                                                    for (int n = 0; n <= UnkI321Number - 1; n++)
                                                        UnkI321[n] = uint.Parse(cmd[3 + n]);
                                                    break;
                                                }

                                            case "PARAM2":
                                                {
                                                    UnkI322Number = int.Parse(cmd[2]);
                                                    Array.Resize(ref UnkI322, UnkI322Number);
                                                    for (int n = 0; n <= UnkI322Number - 1; n++)
                                                        UnkI322[n] = float.Parse(cmd[3 + n]);
                                                    break;
                                                }

                                            case "PARAM3":
                                                {
                                                    UnkI323Number = int.Parse(cmd[2]);
                                                    Array.Resize(ref UnkI323, UnkI323Number);
                                                    for (int n = 0; n <= UnkI323Number - 1; n++)
                                                        UnkI323[n] = uint.Parse(cmd[3 + n]);
                                                    break;
                                                }
                                        }

                                        break;
                                    }

                                case "POINT":
                                    {
                                        float X = float.Parse(cmd[1]);
                                        float Y = float.Parse(cmd[2]);
                                        float Z = float.Parse(cmd[3]);
                                        float ID = float.Parse(cmd[4]);
                                        fileData.Add_Item(indexes);
                                        TreeView1.SelectedNode.Nodes.Add("");
                                        inst_indexes = CalculateIndexes(TreeView1.SelectedNode.LastNode);
                                        Instance INST = (Instance)fileData.Get_Item(inst_indexes);
                                        INST.X = X;
                                        INST.Y = Y;
                                        INST.Z = Z;
                                        INST.ObjectID = (ushort)ID;
                                        // PARAMS
                                        INST.RX = (ushort)RotX;
                                        INST.RY = (ushort)RotY;
                                        INST.RZ = (ushort)RotZ;
                                        INST.Size1 = Size1;
                                        INST.Size2 = Size2;
                                        INST.Size3 = Size3;
                                        INST.Something1 = Something1;
                                        INST.Something2 = Something2;
                                        INST.Something3 = Something3;
                                        INST.ParametersHeader = ParametersHeader;
                                        INST.UnkI32 = UnkI32;
                                        INST.UnkI321 = UnkI321;
                                        INST.UnkI321Number = UnkI321Number;
                                        INST.UnkI322 = UnkI322;
                                        INST.UnkI322Number = UnkI322Number;
                                        INST.UnkI323 = UnkI323;
                                        INST.UnkI323Number = UnkI323Number;
                                        // ------
                                        fileData.Put_Item(INST, inst_indexes);
                                        TreeView1.SelectedNode.LastNode.Text = "Instance ID:" + INST.ID.ToString();
                                        TreeView1.SelectedNode.LastNode.Tag = "Item";
                                        break;
                                    }

                                case "FILL":
                                    {
                                        float X = float.Parse(cmd[1]);
                                        float Y = float.Parse(cmd[2]);
                                        float Z = float.Parse(cmd[3]);
                                        float W = float.Parse(cmd[4]);
                                        float H = float.Parse(cmd[5]);
                                        float L = float.Parse(cmd[6]);
                                        float StX = float.Parse(cmd[7]);
                                        float StY = float.Parse(cmd[8]);
                                        float StZ = float.Parse(cmd[9]);
                                        float ID = float.Parse(cmd[10]);
                                        int shX, shY, shZ;
                                        shX = 0;
                                        shY = 0;
                                        shZ = 0;
                                        while (X + shX * StX < X + W)
                                        {
                                            while (Y + shY * StY < Y + H)
                                            {
                                                while (Z + shZ * StZ < Z + L)
                                                {
                                                    fileData.Add_Item(indexes);
                                                    TreeView1.SelectedNode.Nodes.Add("");
                                                    inst_indexes = CalculateIndexes(TreeView1.SelectedNode.LastNode);
                                                    Instance INST = (Instance)fileData.Get_Item(inst_indexes);
                                                    INST.X = X + shX * StX;
                                                    INST.Y = Y + shY * StY;
                                                    INST.Z = Z + shZ * StZ;
                                                    INST.ObjectID = (ushort)ID;
                                                    INST.RX = (ushort)RotX;
                                                    INST.RY = (ushort)RotY;
                                                    INST.RZ = (ushort)RotZ;
                                                    INST.Size1 = Size1;
                                                    INST.Size2 = Size2;
                                                    INST.Size3 = Size3;
                                                    INST.Something1 = Something1;
                                                    INST.Something2 = Something2;
                                                    INST.Something3 = Something3;
                                                    INST.ParametersHeader = ParametersHeader;
                                                    INST.UnkI32 = UnkI32;
                                                    INST.UnkI321 = UnkI321;
                                                    INST.UnkI321Number = UnkI321Number;
                                                    INST.UnkI322 = UnkI322;
                                                    INST.UnkI322Number = UnkI322Number;
                                                    INST.UnkI323 = UnkI323;
                                                    INST.UnkI323Number = UnkI323Number;
                                                    fileData.Put_Item(INST, inst_indexes);
                                                    TreeView1.SelectedNode.LastNode.Text = "Instance ID:" + INST.ID.ToString();
                                                    TreeView1.SelectedNode.LastNode.Tag = "Item";
                                                    shZ += 1;
                                                }
                                                shZ = 0;
                                                shY += 1;
                                            }
                                            shY = 0;
                                            shX += 1;
                                        }

                                        break;
                                    }
                            }
                        }
                        catch (Exception ex)
                        {
                            Interaction.MsgBox("Cannnot Execute line " + (i + 1).ToString());
                        }
                    }
                }
            }
        }
        private void Button10_Click(object sender, EventArgs e)
        {
            TreeNode Node = TreeView1.SelectedNode.Parent;
            int[] indexes = CalculateIndexes(Node);
            BaseSection Section = (BaseSection)fileData.Get_Item(indexes);
            int Category;
            if (Section is Materials)
                Category = 1;
            else if (Section is GCs)
                Category = 2;
            else if (Section is GameObjects)
                Category = 3;
            else if (Section is Instances)
                Category = 4;
            else if (Section is Scripts)
                Category = 0;
            else
                Category = 0;
            int Result = -2;
            switch (Category)
            {
                case 0:
                    {
                        _utilSearch.SearchID.Items.Clear();
                        _utilSearch.SearchID.Items.Add("ID");
                        int SelectedIndex = _utilSearch.SIndex;
                        int ItemsCount = _utilSearch.SearchID.Items.Count;
                        if (SelectedIndex > ItemsCount - 1)
                            _utilSearch.SearchID.SelectedIndex = ItemsCount - 1;
                        if (_utilSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Result = -1;
                            if (_utilSearch.SearchID.SelectedIndex == 0)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    if (Section._Item[i].ID == uint.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = Section.Records;
                                    }
                                }
                            }
                        }

                        break;
                    }

                case 1:
                    {
                        _utilSearch.SearchID.Items.Clear();
                        _utilSearch.SearchID.Items.Add("ID");
                        _utilSearch.SearchID.Items.Add("Name");
                        _utilSearch.SearchID.Items.Add("TextureID");
                        int SelectedIndex = _utilSearch.SIndex;
                        int ItemsCount = _utilSearch.SearchID.Items.Count;
                        if (SelectedIndex > ItemsCount - 1)
                            _utilSearch.SearchID.SelectedIndex = ItemsCount - 1;
                        Materials Mats = (Materials)Section;
                        if (_utilSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Result = -1;
                            if (_utilSearch.SearchID.SelectedIndex == 0)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Mats.Records - 1; i++)
                                {
                                    if (Mats._Item[i].ID == uint.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = Mats.Records;
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 1)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Mats.Records - 1; i++)
                                {
                                    Material mat = (Material)Mats._Item[i];
                                    if (mat.Name.ToUpper().Contains(_utilSearch.SearchFor.Text.ToUpper()))
                                    {
                                        Result = i;
                                        i = Mats.Records;
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 2)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Mats.Records - 1; i++)
                                {
                                    Material mat = (Material)Mats._Item[i];
                                    if (mat.Texture == uint.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = Mats.Records;
                                    }
                                }
                            }
                        }

                        break;
                    }

                case 2:
                    {
                        _utilSearch.SearchID.Items.Clear();
                        _utilSearch.SearchID.Items.Add("ID");
                        _utilSearch.SearchID.Items.Add("MaterialID");
                        _utilSearch.SearchID.Items.Add("ModelID");
                        int SelectedIndex = _utilSearch.SIndex;
                        int ItemsCount = _utilSearch.SearchID.Items.Count;
                        if (SelectedIndex > ItemsCount - 1)
                            _utilSearch.SearchID.SelectedIndex = ItemsCount - 1;
                        GCs GComps = (GCs)Section;
                        if (_utilSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Result = -1;
                            if (_utilSearch.SearchID.SelectedIndex == 0)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= GComps.Records - 1; i++)
                                {
                                    if (GComps._Item[i].ID == uint.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = GComps.Records;
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 1)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= GComps.Records - 1; i++)
                                {
                                    Twinsanity.GC GComp = (Twinsanity.GC)GComps._Item[i];
                                    for (int j = 0; j <= GComp.MaterialNumber - 1; j++)
                                    {
                                        if (GComp.Material[j] == uint.Parse(_utilSearch.SearchFor.Text))
                                        {
                                            Result = i;
                                            i = GComps.Records;
                                        }
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 2)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= GComps.Records - 1; i++)
                                {
                                    Twinsanity.GC GComp = (Twinsanity.GC)GComps._Item[i];
                                    if (GComp.Model == uint.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = GComps.Records;
                                    }
                                }
                            }
                        }

                        break;
                    }

                case 3:
                    {
                        _utilSearch.SearchID.Items.Clear();
                        _utilSearch.SearchID.Items.Add("ID");
                        _utilSearch.SearchID.Items.Add("OGI");
                        _utilSearch.SearchID.Items.Add("Animation");
                        _utilSearch.SearchID.Items.Add("Script");
                        _utilSearch.SearchID.Items.Add("Object");
                        _utilSearch.SearchID.Items.Add("Sound");
                        int SelectedIndex = _utilSearch.SIndex;
                        int ItemsCount = _utilSearch.SearchID.Items.Count;
                        if (SelectedIndex > ItemsCount - 1)
                            _utilSearch.SearchID.SelectedIndex = ItemsCount - 1;
                        if (_utilSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Result = -1;
                            if (_utilSearch.SearchID.SelectedIndex == 0)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    if (Section._Item[i].ID == uint.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = Section.Records;
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 1)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    GameObject Obj = (GameObject)Section._Item[i];
                                    for (int j = 0; j <= Obj.OGINumber - 1; j++)
                                    {
                                        if (Obj.OGI[j] == UInt16.Parse(_utilSearch.SearchFor.Text))
                                        {
                                            Result = i;
                                            i = Section.Records;
                                        }
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 2)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    GameObject Obj = (GameObject)Section._Item[i];
                                    for (int j = 0; j <= Obj.AnimationNumber - 1; j++)
                                    {
                                        if (Obj.Animation[j] == UInt16.Parse(_utilSearch.SearchFor.Text))
                                        {
                                            Result = i;
                                            i = Section.Records;
                                        }
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 3)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    GameObject Obj = (GameObject)Section._Item[i];
                                    for (int j = 0; j <= Obj.ScriptNumber - 1; j++)
                                    {
                                        if (Obj.Script[j] == UInt16.Parse(_utilSearch.SearchFor.Text))
                                        {
                                            Result = i;
                                            i = Section.Records;
                                        }
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 4)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    GameObject Obj = (GameObject)Section._Item[i];
                                    for (int j = 0; j <= Obj.GameObjectNumber - 1; j++)
                                    {
                                        if (Obj._GameObject[j] == UInt16.Parse(_utilSearch.SearchFor.Text))
                                        {
                                            Result = i;
                                            i = Section.Records;
                                        }
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 5)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    GameObject Obj = (GameObject)Section._Item[i];
                                    for (int j = 0; j <= Obj.SoundNumber - 1; j++)
                                    {
                                        if (Obj.Sound[j] == UInt16.Parse(_utilSearch.SearchFor.Text))
                                        {
                                            Result = i;
                                            i = Section.Records;
                                        }
                                    }
                                }
                            }
                        }

                        break;
                    }

                case 4:
                    {
                        _utilSearch.SearchID.Items.Clear();
                        _utilSearch.SearchID.Items.Add("ID");
                        _utilSearch.SearchID.Items.Add("ObjectID");
                        int SelectedIndex = _utilSearch.SIndex;
                        int ItemsCount = _utilSearch.SearchID.Items.Count;
                        if (SelectedIndex > ItemsCount - 1)
                            _utilSearch.SearchID.SelectedIndex = ItemsCount - 1;
                        if (_utilSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Result = -1;
                            if (_utilSearch.SearchID.SelectedIndex == 0)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    if (Section._Item[i].ID == uint.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = Section.Records;
                                    }
                                }
                            }
                            else if (_utilSearch.SearchID.SelectedIndex == 1)
                            {
                                for (int i = TreeView1.SelectedNode.Index; i <= Section.Records - 1; i++)
                                {
                                    Instance INST = (Instance)Section._Item[i];
                                    if (INST.ObjectID == UInt16.Parse(_utilSearch.SearchFor.Text))
                                    {
                                        Result = i;
                                        i = Section.Records;
                                    }
                                }
                            }
                        }

                        break;
                    }
            }
            if (Result == -1)
                Interaction.MsgBox("Nothing found");
            else if (!(Result == -2))
            {
                TreeView1.SelectedNode = TreeView1.SelectedNode.Parent.Nodes[Result];
                TreeView1.Focus();
            }
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            if (_utilRandomizer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int SIndex, EIndex, Cycles;
                SIndex = _utilRandomizer.StartIndex;
                EIndex = _utilRandomizer.EndIndex;
                Cycles = _utilRandomizer.Cycles;
                int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
                BaseSection Section = (BaseSection)fileData.Get_Item(indexes);
                if (Section is Instances)
                {
                    Instances INSTS = (Instances)Section;
                    for (int i = 0; i <= Cycles - 1; i++)
                    {
                        int IND1 = (int)Math.Round(VBMath.Rnd() * (EIndex - SIndex)) + SIndex;
                        int IND2 = (int)Math.Round(VBMath.Rnd() * (EIndex - SIndex)) + SIndex;
                        Instance INST1, INST2;
                        INST1 = (Instance)INSTS._Item[IND1];
                        INST2 = (Instance)INSTS._Item[IND2];
                        uint TMP = INST1.ObjectID;
                        INST1.ObjectID = INST2.ObjectID;
                        INST2.ObjectID = (ushort)TMP;
                        INSTS._Item[IND1] = INST1;
                        INSTS._Item[IND2] = INST2;
                    }
                    fileData.Put_Item(INSTS, indexes);
                }
                else
                {
                    for (int i = 0; i <= Cycles - 1; i++)
                    {
                        int IND1 = (int)Math.Round(VBMath.Rnd() * (EIndex - SIndex)) + SIndex;
                        int IND2 = (int)Math.Round(VBMath.Rnd() * (EIndex - SIndex)) + SIndex;
                        uint TMP = Section._Item[IND1].ID;
                        Section._Item[IND1].ID = Section._Item[IND2].ID;
                        Section._Item[IND2].ID = TMP;
                        TreeView1.SelectedNode.Nodes[IND1].Text = TreeView1.SelectedNode.Nodes[IND1].Text.Replace("ID:" + Section._Item[IND2].ID.ToString(), "ID:" + Section._Item[IND1].ID.ToString());
                        TreeView1.SelectedNode.Nodes[IND2].Text = TreeView1.SelectedNode.Nodes[IND2].Text.Replace("ID:" + Section._Item[IND1].ID.ToString(), "ID:" + Section._Item[IND2].ID.ToString());
                    }
                    fileData.Put_Item(Section, indexes);
                }
            }
        }
        private void BDWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _workBD.Show();
        }
        private void ElfPatcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _workPatcher.Show();
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Text = "Twinsainty Editor v0.43.0" + Strings.Chr(13) + Strings.Chr(10);
            Text += "Programming and Reverse Engineering: Neo_Kesha" + Strings.Chr(13) + Strings.Chr(10);
            Text += "Refactored and redesigned by: Smartkin\n";
            Text += "Testing and support: Marko1000Marko, GPro, ManDude, supermoe1985, BetaM, smartkin, Ch1ppyone" + Strings.Chr(13) + Strings.Chr(10);
            Text += "http://twinsanity-hacking.tigersoftware.ru/";
            Interaction.MsgBox(Text);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
            BaseObject Obj = fileData.Get_Item(indexes);
            if (Obj is GameObject)
            {
                if (ExtractBunch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // ---INITIALIZATION---'
                    GameObject GO = (GameObject)Obj;
                    UInt16[] OGI = new ushort[] { };
                    UInt16[] Animation = new ushort[] { };
                    UInt16[] Script = new ushort[] { };
                    UInt16[] ExtraScript = new UInt16[GO.ScriptNumber - 1 + 1];
                    UInt16[] Sound = new ushort[] { };
                    uint[] GC = new uint[] { };
                    uint[] Model = new uint[] { };
                    uint[] ID4 = new uint[] { };
                    uint[] ID5 = new uint[] { };
                    uint[] ID4P = new uint[] { };
                    uint[] Material = new uint[] { };
                    uint[] Texture = new uint[] { };
                    string Name = TreeView1.SelectedNode.Text.Replace(":", " ");
                    Name = Name.Replace("|", " ");
                    Name = ExtractBunch.SelectedPath + @"\" + Name;
                    System.IO.Directory.CreateDirectory(Name);
                    BaseSection OGIs = new BaseSection();
                    BaseSection Animations = new BaseSection();
                    BaseSection Scripts = new BaseSection();
                    BaseSection ID4Ps = new BaseSection();
                    BaseSection Sounds = new BaseSection();
                    BaseSection SoundsEnglish = new BaseSection();
                    BaseSection SoundsFrench = new BaseSection();
                    BaseSection SoundsGerman = new BaseSection();
                    BaseSection SoundsSpanish = new BaseSection();
                    BaseSection SoundsItalian = new BaseSection();
                    BaseSection SoundsJapan = new BaseSection();
                    BaseSection Textures = new BaseSection();
                    BaseSection Materials = new BaseSection();
                    BaseSection Models = new BaseSection();
                    BaseSection ID4s = new BaseSection();
                    BaseSection ID5s = new BaseSection();
                    BaseSection GCs = new BaseSection();
                    int CodeIndex = 0, GraphicsIndex = 0;
                    // ---SECTIONS GETTING---'
                    for (int i = 0; i <= fileData.Records - 1; i++)
                    {
                        if (fileData.Item[i].ID == 11)
                        {
                            GraphicsIndex = i;
                            for (int j = 0; j <= fileData.Item[i].Records - 1; j++)
                            {
                                switch (fileData.Item[i]._Item[j].ID)
                                {
                                    case 0:
                                        {
                                            Textures = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 1:
                                        {
                                            Materials = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 2:
                                        {
                                            Models = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 3:
                                        {
                                            GCs = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 4:
                                        {
                                            ID4s = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 5:
                                        {
                                            ID5s = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }
                                }
                            }
                        }
                        else if (fileData.Item[i].ID == 10)
                        {
                            CodeIndex = i;
                            for (int j = 0; j <= fileData.Item[i].Records - 1; j++)
                            {
                                switch (fileData.Item[i]._Item[j].ID)
                                {
                                    case 1:
                                        {
                                            Scripts = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 2:
                                        {
                                            Animations = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 3:
                                        {
                                            OGIs = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 4:
                                        {
                                            ID4Ps = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 5:
                                        {
                                            Sounds = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 7:
                                        {
                                            SoundsEnglish = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 8:
                                        {
                                            SoundsFrench = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 9:
                                        {
                                            SoundsGerman = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 10:
                                        {
                                            SoundsSpanish = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 11:
                                        {
                                            SoundsItalian = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }

                                    case 12:
                                        {
                                            SoundsJapan = (BaseSection)fileData.Item[i]._Item[j];
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    // ---GETTING IDs---'
                    // --OGI--'
                    for (int i = 0; i <= GO.OGINumber - 1; i++)
                    {
                        var flag = true;
                        for (int j = 0; j <= OGI.Length - 1; j++)
                        {
                            if (OGI[j] == GO.OGI[i])
                            {
                                flag = false;
                                j = OGI.Length;
                            }
                        }
                        if (flag & !(GO.OGI[i] == 65535))
                        {
                            Array.Resize(ref OGI, OGI.Length + 1);
                            OGI[OGI.Length - 1] = GO.OGI[i];
                        }
                    }
                    // --Animation--'
                    for (int i = 0; i <= GO.AnimationNumber - 1; i++)
                    {
                        var flag = true;
                        for (int j = 0; j <= Animation.Length - 1; j++)
                        {
                            if (Animation[j] == GO.Animation[i])
                            {
                                flag = false;
                                j = Animation.Length;
                            }
                        }
                        if (flag & !(GO.Animation[i] == 65535))
                        {
                            Array.Resize(ref Animation, Animation.Length + 1);
                            Animation[Animation.Length - 1] = GO.Animation[i];
                        }
                    }
                    // --Script--'
                    for (int i = 0; i <= GO.ChildScriptNumber - 1; i++)
                    {
                        var flag = true;
                        for (int j = 0; j <= Script.Length - 1; j++)
                        {
                            if (Script[j] == GO.ChildScript[i])
                            {
                                flag = false;
                                j = Script.Length;
                            }
                        }
                        if (flag & !(GO.ChildScript[i] == 65535))
                        {
                            Array.Resize(ref Script, Script.Length + 1);
                            Script[Script.Length - 1] = GO.ChildScript[i];
                        }
                    }
                    // --ID4Projectiles--'
                    for (int i = 0; i <= GO.ChildID4Number - 1; i++)
                    {
                        var flag = true;
                        for (int j = 0; j <= ID4P.Length - 1; j++)
                        {
                            if (ID4P[j] == GO.ChildID4[i])
                            {
                                flag = false;
                                j = ID4P.Length;
                            }
                        }
                        if (flag & !(GO.ChildID4[i] == 65535))
                        {
                            Array.Resize(ref ID4P, ID4P.Length + 1);
                            ID4P[ID4P.Length - 1] = GO.ChildID4[i];
                        }
                    }
                    // --ExtraScript--'
                    // For i As int = 0 To Script.Length - 1
                    // For j As int = 0 To fileData.Item(1).Item(1).Records - 1
                    // If fileData.Item(1).Item(1).Item(j).ID = Script(i) Then
                    // Dim ind() As int = CalculateIndexes(TreeView1.Nodes(0).Nodes(1).Nodes(1).Nodes(j))
                    // Dim S As IO.MemoryStream = fileData.Get_Stream(ind)
                    // Dim rdr As New IO.BinaryReader(S)
                    // S.Position = 8
                    // Array.Resize(Script, Script.Length + 1)
                    // Script(Script.Length - 1) = rdr.ReadUInt16 - 1
                    // End If
                    // Next j
                    // Next i
                    // --Sound--'
                    for (int i = 0; i <= GO.SoundNumber - 1; i++)
                    {
                        var flag = true;
                        for (int j = 0; j <= Sound.Length - 1; j++)
                        {
                            if (Sound[j] == GO.Sound[i])
                            {
                                flag = false;
                                j = Sound.Length;
                            }
                        }
                        if (flag & !(GO.Sound[i] == 65535))
                        {
                            Array.Resize(ref Sound, Sound.Length + 1);
                            Sound[Sound.Length - 1] = GO.Sound[i];
                        }
                    }
                    // --ID4-5--'
                    for (int i = 0; i <= OGI.Length - 1; i++)
                    {
                        for (int j = 0; j <= fileData.Item[1]._Item[3].Records - 1; j++)
                        {
                            if (fileData.Item[1]._Item[3]._Item[j].ID == OGI[i])
                            {
                                int[] ind = CalculateIndexes(TreeView1.Nodes[0].Nodes[1].Nodes[3].Nodes[j]);
                                OGI OG = (OGI)fileData.Get_Item(ind);
                                if (!(OG.SomeInt321 == 0))
                                {
                                    Array.Resize(ref ID4, ID4.Length + 1);
                                    ID4[ID4.Length - 1] = OG.SomeInt321;
                                }
                                if (!(OG.SomeInt322 == 0))
                                {
                                    Array.Resize(ref ID5, ID5.Length + 1);
                                    ID5[ID5.Length - 1] = OG.SomeInt322;
                                }
                            }
                        }
                    }
                    // --GC--'
                    for (int i = 0; i <= OGI.Length - 1; i++)
                    {
                        for (int j = 0; j <= fileData.Item[1]._Item[3].Records - 1; j++)
                        {
                            if (fileData.Item[1]._Item[3]._Item[j].ID == OGI[i])
                            {
                                int[] ind = CalculateIndexes(TreeView1.Nodes[0].Nodes[1].Nodes[3].Nodes[j]);
                                OGI OG = (OGI)fileData.Get_Item(ind);
                                for (int a = 0; a <= OG.GCNumber - 1; a++)
                                {
                                    Array.Resize(ref GC, GC.Length + 1);
                                    GC[GC.Length - 1] = OG.GCI[a].GCID;
                                }
                            }
                        }
                    }
                    // --Materials And Models--'
                    for (int i = 0; i <= GC.Length - 1; i++)
                    {
                        for (int j = 0; j <= fileData.Item[0]._Item[3].Records - 1; j++)
                        {
                            if (fileData.Item[0]._Item[3]._Item[j].ID == GC[i])
                            {
                                int[] ind = CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[3].Nodes[j]);
                                Twinsanity.GC G = (Twinsanity.GC)fileData.Get_Item(ind);
                                for (int a = 0; a <= G.MaterialNumber - 1; a++)
                                {
                                    Array.Resize(ref Material, Material.Length + 1);
                                    Material[Material.Length - 1] = G.Material[a];
                                }
                                Array.Resize(ref Model, Model.Length + 1);
                                Model[Model.Length - 1] = G.Model;
                            }
                        }
                    }
                    // --ExtraMaterials--'
                    for (int i = 0; i <= ID4.Length - 1; i++)
                    {
                        for (int j = 0; j <= fileData.Item[0]._Item[4].Records - 1; j++)
                        {
                            if (fileData.Item[0]._Item[4]._Item[j].ID == ID4[i])
                            {
                                ID4Model ID4Model = (ID4Model)fileData.Item[0]._Item[4]._Item[j];
                                for (int k = 0; k <= ID4Model.SubModels - 1; k++)
                                {
                                    Array.Resize(ref Material, Material.Length + 1);
                                    Material[Material.Length - 1] = ID4Model.SubModel[k].MaterialID;
                                }
                            }
                        }
                    }
                    for (int i = 0; i <= ID5.Length - 1; i++)
                    {
                        for (int j = 0; j <= fileData.Item[0]._Item[5].Records - 1; j++)
                        {
                            if (fileData.Item[0]._Item[5]._Item[j].ID == ID5[i])
                            {
                                int[] ind = CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[5].Nodes[j]);
                                System.IO.MemoryStream ID = fileData.Get_Stream(ind);
                                System.IO.BinaryReader IDReader = new System.IO.BinaryReader(ID);
                                ID.Position = 0;
                                while (ID.Position + 4 <= ID.Length)
                                {
                                    if (IDReader.ReadUInt32() == 16777476)
                                    {
                                        ID.Position -= 104;
                                        uint MID = IDReader.ReadUInt32();
                                        if (!(MID == 0))
                                        {
                                            Array.Resize(ref Material, Material.Length + 1);
                                            Material[Material.Length - 1] = MID;
                                        }
                                        ID.Position += 100;
                                    }
                                }
                            }
                        }
                    }
                    // --Textures--'
                    for (int i = 0; i <= Material.Length - 1; i++)
                    {
                        for (int j = 0; j <= fileData.Item[0]._Item[1].Records - 1; j++)
                        {
                            if (fileData.Item[0]._Item[1]._Item[j].ID == Material[i])
                            {
                                int[] ind = CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[j]);
                                Material M = (Material)fileData.Get_Item(ind);
                                Array.Resize(ref Texture, Texture.Length + 1);
                                Texture[Texture.Length - 1] = M.Texture;
                            }
                        }
                    }
                    // ---EXTRACTOIN---'
                    string ObjName = GO.Name.Replace("|", " ");
                    ObjName = ObjName.Replace(":", " ");
                    System.IO.FileStream Log = new System.IO.FileStream(Name + @"\main.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    System.IO.StreamWriter LogWriter = new System.IO.StreamWriter(Log);
                    Extract(TreeView1.SelectedNode, Name + @"\" + ObjName + "ID " + GO.ID.ToString());
                    LogWriter.WriteLine("obj " + GO.ID.ToString() + @" \" + ObjName + "ID " + GO.ID.ToString());
                    System.IO.Directory.CreateDirectory(Name + @"\Textures");
                    for (int i = 0; i <= Texture.Length - 1; i++)
                    {
                        for (int j = 0; j <= Textures.Records - 1; j++)
                        {
                            if (Textures._Item[j].ID == Texture[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[j], Name + @"\Textures\" + ObjName);
                                LogWriter.WriteLine("tex " + Texture[i].ToString() + @" \Textures\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\Materials");
                    for (int i = 0; i <= Material.Length - 1; i++)
                    {
                        for (int j = 0; j <= Materials.Records - 1; j++)
                        {
                            if (Materials._Item[j].ID == Material[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[j], Name + @"\Materials\" + ObjName);
                                LogWriter.WriteLine("mtl " + Material[i].ToString() + @" \Materials\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\Models");
                    for (int i = 0; i <= Model.Length - 1; i++)
                    {
                        for (int j = 0; j <= Models.Records - 1; j++)
                        {
                            if (Models._Item[j].ID == Model[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[0].Nodes[2].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[0].Nodes[2].Nodes[j], Name + @"\Models\" + ObjName);
                                LogWriter.WriteLine("mdl " + Model[i].ToString() + @" \Models\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\GCs");
                    for (int i = 0; i <= GC.Length - 1; i++)
                    {
                        for (int j = 0; j <= GCs.Records - 1; j++)
                        {
                            if (GCs._Item[j].ID == GC[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[0].Nodes[3].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[0].Nodes[3].Nodes[j], Name + @"\GCs\" + ObjName);
                                LogWriter.WriteLine("gc " + GC[i].ToString() + @" \GCs\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\ID4Graphics");
                    for (int i = 0; i <= ID4.Length - 1; i++)
                    {
                        for (int j = 0; j <= ID4s.Records - 1; j++)
                        {
                            if (ID4s._Item[j].ID == ID4[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[0].Nodes[4].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[0].Nodes[4].Nodes[j], Name + @"\ID4Graphics\" + ObjName);
                                LogWriter.WriteLine("id4 " + ID4[i].ToString() + @" \ID4Graphics\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\ID5Graphics");
                    for (int i = 0; i <= ID5.Length - 1; i++)
                    {
                        for (int j = 0; j <= ID5s.Records - 1; j++)
                        {
                            if (ID5s._Item[j].ID == ID5[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[0].Nodes[5].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[0].Nodes[5].Nodes[j], Name + @"\ID5Graphics\" + ObjName);
                                LogWriter.WriteLine("id5 " + ID5[i].ToString() + @" \ID5Graphics\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\Scripts");
                    for (int i = 0; i <= Script.Length - 1; i++)
                    {
                        for (int j = 0; j <= Scripts.Records - 1; j++)
                        {
                            if (Scripts._Item[j].ID == Script[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[1].Nodes[1].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[1].Nodes[1].Nodes[j], Name + @"\Scripts\" + ObjName);
                                LogWriter.WriteLine("scr " + Script[i].ToString() + @" \Scripts\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\Animations");
                    for (int i = 0; i <= Animation.Length - 1; i++)
                    {
                        for (int j = 0; j <= Animations.Records - 1; j++)
                        {
                            if (Animations._Item[j].ID == Animation[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[1].Nodes[2].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[1].Nodes[2].Nodes[j], Name + @"\Animations\" + ObjName);
                                LogWriter.WriteLine("ani " + Animation[i].ToString() + @" \Animations\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\OGIs");
                    for (int i = 0; i <= OGI.Length - 1; i++)
                    {
                        for (int j = 0; j <= OGIs.Records - 1; j++)
                        {
                            if (OGIs._Item[j].ID == OGI[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[1].Nodes[3].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[1].Nodes[3].Nodes[j], Name + @"\OGIs\" + ObjName);
                                LogWriter.WriteLine("ogi " + OGI[i].ToString() + @" \OGIs\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\ID4Ps");
                    for (int i = 0; i <= ID4P.Length - 1; i++)
                    {
                        for (int j = 0; j <= ID4Ps.Records - 1; j++)
                        {
                            if (ID4Ps._Item[j].ID == ID4P[i])
                            {
                                ObjName = TreeView1.Nodes[0].Nodes[1].Nodes[4].Nodes[j].Text.Replace(":", " ");
                                ObjName = ObjName.Replace("|", " ");
                                Extract(TreeView1.Nodes[0].Nodes[1].Nodes[4].Nodes[j], Name + @"\ID4Ps\" + ObjName);
                                LogWriter.WriteLine("id4p " + ID4P[i].ToString() + @" \ID4Ps\" + ObjName);
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\Sounds");
                    for (int i = 0; i <= Sound.Length - 1; i++)
                    {
                        for (int j = 0; j <= Sounds.Records - 1; j++)
                        {
                            if (Sounds._Item[j].ID == Sound[i])
                            {
                                System.IO.FileStream File = new System.IO.FileStream(Name + @"\Sounds\Sound " + Sounds._Item[j].ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                                SoundDescription SD = (SoundDescription)Sounds._Item[j];
                                int[] ind = CalculateIndexes(TreeView1.Nodes[0].Nodes[1].Nodes[6]);
                                System.IO.MemoryStream _Sound = fileData.Get_Stream(ind);
                                Writer.Write(SD.ID);
                                Writer.Write(SD.Frequency);
                                Writer.Write(SD.SoundSize);
                                System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(_Sound);
                                _Sound.Position = SD.SoundOffset;
                                Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                                Writer.Close();
                                File.Close();
                                LogWriter.WriteLine("sfx " + SD.ID.ToString() + @" \Sounds\Sound " + Sounds._Item[j].ID.ToString());
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\SoundsEnglish");
                    for (int i = 0; i <= Sound.Length - 1; i++)
                    {
                        for (int j = 0; j <= SoundsEnglish.Records - 1; j++)
                        {
                            if (SoundsEnglish._Item[j].ID == Sound[i])
                            {
                                System.IO.FileStream File = new System.IO.FileStream(Name + @"\SoundsEnglish\Sound  " + SoundsEnglish._Item[j].ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                                SoundDescription SD = (SoundDescription)SoundsEnglish._Item[j];
                                SoundbankDescriptions Bank = (SoundbankDescriptions)SoundsEnglish;
                                System.IO.MemoryStream _Sound = Bank.SoundBank;
                                Writer.Write(SD.ID);
                                Writer.Write(SD.Frequency);
                                Writer.Write(SD.SoundSize);
                                System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(_Sound);
                                _Sound.Position = SD.SoundOffset;
                                Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                                Writer.Close();
                                File.Close();
                                LogWriter.WriteLine("sve " + SD.ID.ToString() + @" \SoundsEnglish\Sound  " + SoundsEnglish._Item[j].ID.ToString());
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\SoundsFrench");
                    for (int i = 0; i <= Sound.Length - 1; i++)
                    {
                        for (int j = 0; j <= SoundsFrench.Records - 1; j++)
                        {
                            if (SoundsFrench._Item[j].ID == Sound[i])
                            {
                                System.IO.FileStream File = new System.IO.FileStream(Name + @"\SoundsFrench\Sound  " + SoundsFrench._Item[j].ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                                SoundDescription SD = (SoundDescription)SoundsFrench._Item[j];
                                SoundbankDescriptions Bank = (SoundbankDescriptions)SoundsFrench;
                                System.IO.MemoryStream _Sound = Bank.SoundBank;
                                Writer.Write(SD.ID);
                                Writer.Write(SD.Frequency);
                                Writer.Write(SD.SoundSize);
                                System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(_Sound);
                                _Sound.Position = SD.SoundOffset;
                                Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                                Writer.Close();
                                File.Close();
                                LogWriter.WriteLine("svf " + SD.ID.ToString() + @" \SoundsFrench\Sound  " + SoundsFrench._Item[j].ID.ToString());
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\SoundsGerman");
                    for (int i = 0; i <= Sound.Length - 1; i++)
                    {
                        for (int j = 0; j <= SoundsGerman.Records - 1; j++)
                        {
                            if (SoundsGerman._Item[j].ID == Sound[i])
                            {
                                System.IO.FileStream File = new System.IO.FileStream(Name + @"\SoundsGerman\Sound  " + SoundsGerman._Item[j].ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                                SoundDescription SD = (SoundDescription)SoundsGerman._Item[j];
                                SoundbankDescriptions Bank = (SoundbankDescriptions)SoundsGerman;
                                System.IO.MemoryStream _Sound = Bank.SoundBank;
                                Writer.Write(SD.ID);
                                Writer.Write(SD.Frequency);
                                Writer.Write(SD.SoundSize);
                                System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(_Sound);
                                _Sound.Position = SD.SoundOffset;
                                Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                                Writer.Close();
                                File.Close();
                                LogWriter.WriteLine("svg " + SD.ID.ToString() + @" \SoundsGerman\Sound  " + SoundsGerman._Item[j].ID.ToString());
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\SoundsSpanish");
                    for (int i = 0; i <= Sound.Length - 1; i++)
                    {
                        for (int j = 0; j <= SoundsSpanish.Records - 1; j++)
                        {
                            if (SoundsSpanish._Item[j].ID == Sound[i])
                            {
                                System.IO.FileStream File = new System.IO.FileStream(Name + @"\SoundsSpanish\Sound  " + SoundsSpanish._Item[j].ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                                SoundDescription SD = (SoundDescription)SoundsSpanish._Item[j];
                                SoundbankDescriptions Bank = (SoundbankDescriptions)SoundsSpanish;
                                System.IO.MemoryStream _Sound = Bank.SoundBank;
                                Writer.Write(SD.ID);
                                Writer.Write(SD.Frequency);
                                Writer.Write(SD.SoundSize);
                                System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(_Sound);
                                _Sound.Position = SD.SoundOffset;
                                Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                                Writer.Close();
                                File.Close();
                                LogWriter.WriteLine("svs " + SD.ID.ToString() + @" \SoundsSpanish\Sound  " + SoundsSpanish._Item[j].ID.ToString());
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\SoundsItalian");
                    for (int i = 0; i <= Sound.Length - 1; i++)
                    {
                        for (int j = 0; j <= SoundsItalian.Records - 1; j++)
                        {
                            if (SoundsItalian._Item[j].ID == Sound[i])
                            {
                                System.IO.FileStream File = new System.IO.FileStream(Name + @"\SoundsItalian\Sound  " + SoundsItalian._Item[j].ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                                SoundDescription SD = (SoundDescription)SoundsItalian._Item[j];
                                SoundbankDescriptions Bank = (SoundbankDescriptions)SoundsItalian;
                                System.IO.MemoryStream _Sound = Bank.SoundBank;
                                Writer.Write(SD.ID);
                                Writer.Write(SD.Frequency);
                                Writer.Write(SD.SoundSize);
                                System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(_Sound);
                                _Sound.Position = SD.SoundOffset;
                                Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                                Writer.Close();
                                File.Close();
                                LogWriter.WriteLine("svi " + SD.ID.ToString() + @" \SoundsItalian\Sound  " + SoundsItalian._Item[j].ID.ToString());
                            }
                        }
                    }
                    System.IO.Directory.CreateDirectory(Name + @"\SoundsJapan");
                    for (int i = 0; i <= Sound.Length - 1; i++)
                    {
                        for (int j = 0; j <= SoundsJapan.Records - 1; j++)
                        {
                            if (SoundsJapan._Item[j].ID == Sound[i])
                            {
                                System.IO.FileStream File = new System.IO.FileStream(Name + @"\SoundsJapan\Sound " + SoundsJapan._Item[j].ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                                SoundDescription SD = (SoundDescription)SoundsJapan._Item[j];
                                SoundbankDescriptions Bank = (SoundbankDescriptions)SoundsJapan;
                                System.IO.MemoryStream _Sound = Bank.SoundBank;
                                Writer.Write(SD.ID);
                                Writer.Write(SD.Frequency);
                                Writer.Write(SD.SoundSize);
                                System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(_Sound);
                                _Sound.Position = SD.SoundOffset;
                                Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                                Writer.Close();
                                File.Close();
                                LogWriter.WriteLine("svj " + SD.ID.ToString() + @" \SoundsJapan\Sound " + SoundsJapan._Item[j].ID.ToString());
                            }
                        }
                    }
                    LogWriter.Write("END");
                    LogWriter.Close();
                    Log.Close();
                }
            }
            else if (Obj is SoundDescription)
            {
                string Name = TreeView1.SelectedNode.Text.Replace("|", " ");
                Name = Name.Replace(":", " ");
                ExtractItem.FileName = Name;
                if (ExtractItem.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.FileStream File = new System.IO.FileStream(ExtractItem.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(File);
                    SoundDescription SD = (SoundDescription)Obj;
                    object Par = fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                    System.IO.MemoryStream Sound;
                    if (Par is SoundbankDescriptions)
                    {
                        SoundbankDescriptions SbD = (SoundbankDescriptions)Par;
                        Sound = SbD.SoundBank;
                    }
                    else
                        Sound = fileData.Get_Stream(CalculateIndexes(TreeView1.SelectedNode.Parent.Parent.Nodes[TreeView1.SelectedNode.Parent.Index + 1]));
                    Writer.Write(SD.ID);
                    Writer.Write(SD.Frequency);
                    Writer.Write(SD.SoundSize);
                    System.IO.BinaryReader StreamReader = new System.IO.BinaryReader(Sound);
                    Sound.Position = SD.SoundOffset;
                    Writer.Write(StreamReader.ReadBytes((int)SD.SoundSize));
                    StreamReader.Close();
                    Sound.Close();
                    Writer.Close();
                    File.Close();
                }
            }
        }

        private void NewRM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileData.NewRM2();
            LoadTree();
        }

        private void NewSM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_utilNewSM2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileData.NewSM2(_utilNewSM2.TextBox1.Text);
                LoadTree();
            }
        }

        private void E3ConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = E3Converter.ShowDialog();
            if (res == DialogResult.Cancel)
                return;

            string Path = E3Converter.FileNames[0];
            int pos = Path.Length - 1;
            while (!(Path[pos] == '\\'))
                pos -= 1;
            Path = Path.Remove(pos + 1, Path.Length - pos - 1);
            for (int i = 0; i <= E3Converter.FileNames.Length - 1; i++)
            {
                switch (E3Converter.FilterIndex)
                {
                    case 1 // E3 to Release GameObject
                   :
                        {
                            DemoGameObject E3GO = new DemoGameObject();
                            System.IO.FileStream File = new System.IO.FileStream(E3Converter.FileNames[i], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            System.IO.BinaryReader FileReader = new System.IO.BinaryReader(File);
                            E3GO.Base = 0;
                            E3GO.Offset = 0;
                            E3GO.Size = (uint)File.Length;
                            E3GO.Load(ref File, ref FileReader);
                            GameObject GO = new GameObject();
                            /// 
                            ConvertE3GO(ref GO, ref E3GO);
                            /// 
                            System.IO.MemoryStream BS = GO.Get_Stream(0, null);
                            File.Close();
                            if (!System.IO.Directory.Exists(Path + @"Release\"))
                                System.IO.Directory.CreateDirectory(Path + @"Release\");
                            string Name = GO.Name.Replace("|", " ");
                            Name = Name.Replace(":", " ");
                            File = new System.IO.FileStream(Path + @"Release\" + Name + " ID " + GO.ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            System.IO.BinaryWriter FileWriter = new System.IO.BinaryWriter(File);
                            FileWriter.Write(BS.ToArray());
                            File.Close();
                            break;
                        }

                    case 2 // E3 to Release Instance
             :
                        {
                            DemoInstance E3INST = new DemoInstance();
                            System.IO.FileStream File = new System.IO.FileStream(E3Converter.FileNames[i], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            System.IO.BinaryReader FileReader = new System.IO.BinaryReader(File);
                            E3INST.Base = 0;
                            E3INST.Offset = 0;
                            E3INST.Size = (uint)File.Length;
                            E3INST.Load(ref File, ref FileReader);
                            Instance INST = new Instance();
                            /// 
                            ConvertE3INST(ref INST, ref E3INST);
                            /// 
                            System.IO.MemoryStream BS = INST.Get_Stream(0, null);
                            File.Close();
                            if (!System.IO.Directory.Exists(Path + @"Release\"))
                                System.IO.Directory.CreateDirectory(Path + @"Release\");
                            File = new System.IO.FileStream(Path + @"Release\" + "Instance ID " + INST.ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            System.IO.BinaryWriter FileWriter = new System.IO.BinaryWriter(File);
                            FileWriter.Write(BS.ToArray());
                            File.Close();
                            break;
                        }

                    case 3 // E3 to Release Level
             :
                        {
                            RM2 E3RM2 = new RM2();
                            E3RM2.LoadDemoRM2(E3Converter.FileNames[i]);
                            RM2 RM2 = new RM2();
                            RM2.Header = E3RM2.Header;
                            RM2.Size = E3RM2.Size;
                            RM2.Records = E3RM2.Records;
                            Array.Resize(ref RM2.Item, RM2.Records);
                            for (int j = 0; j <= RM2.Records - 1; j++)
                            {
                                switch (E3RM2.Item[j].ID)
                                {
                                    case 8:
                                    case 9:
                                    case 11:
                                        {
                                            RM2.Item[j] = E3RM2.Item[j];
                                            break;
                                        }

                                    case 10:
                                        {
                                            DemoCodeSection E3Cs = (DemoCodeSection)E3RM2.Item[j];
                                            CodeSection Cs = new CodeSection();
                                            Cs.Records = E3Cs.Records;
                                            Cs.Header = E3Cs.Header;
                                            Cs.ID = 10;
                                            Array.Resize(ref Cs._Item, Cs.Records);
                                            for (int a = 0; a <= Cs.Records - 1; a++)
                                            {
                                                if (!(E3Cs._Item[a].ID == 0))
                                                    Cs._Item[a] = E3Cs._Item[a];
                                                else
                                                {
                                                    DemoGameObjects E3GOs = (DemoGameObjects)E3Cs._Item[a];
                                                    GameObjects GOs = new GameObjects();
                                                    GOs.Records = E3GOs.Records;
                                                    GOs.Header = E3GOs.Header;
                                                    GOs.ID = 0;
                                                    Array.Resize(ref GOs._Item, GOs.Records);
                                                    for (int b = 0; b <= GOs.Records - 1; b++)
                                                    {
                                                        DemoGameObject E3GO = (DemoGameObject)E3GOs._Item[b];
                                                        GameObject GO = new GameObject();
                                                        ConvertE3GO(ref GO, ref E3GO);
                                                        GOs._Item[b] = GO;
                                                    }
                                                    Cs._Item[a] = GOs;
                                                }
                                            }
                                            RM2.Item[j] = Cs;
                                            break;
                                        }

                                    default:
                                        {
                                            DemoInstanceInfoSection E3IIs = (DemoInstanceInfoSection)E3RM2.Item[j];
                                            InstanceInfoSection IIs = new InstanceInfoSection();
                                            IIs.Records = E3IIs.Records;
                                            IIs.Header = E3IIs.Header;
                                            IIs.ID = E3IIs.ID;
                                            Array.Resize(ref IIs._Item, IIs.Records);
                                            for (int a = 0; a <= IIs.Records - 1; a++)
                                            {
                                                if (!(E3IIs._Item[a].ID == 6))
                                                    IIs._Item[a] = E3IIs._Item[a];
                                                else
                                                {
                                                    DemoInstances E3Is = (DemoInstances)E3IIs._Item[a];
                                                    Instances INSTs = new Instances();
                                                    INSTs.Records = E3Is.Records;
                                                    INSTs.Header = E3Is.Header;
                                                    INSTs.ID = 6;
                                                    Array.Resize(ref INSTs._Item, INSTs.Records);
                                                    for (int b = 0; b <= INSTs.Records - 1; b++)
                                                    {
                                                        DemoInstance E3INST = (DemoInstance)E3Is._Item[b];
                                                        Instance INST = new Instance();
                                                        ConvertE3INST(ref INST, ref E3INST);
                                                        INSTs._Item[b] = INST;
                                                    }
                                                    IIs._Item[a] = INSTs;
                                                }
                                            }
                                            RM2.Item[j] = IIs;
                                            break;
                                        }
                                }
                            }
                            if (!System.IO.Directory.Exists(Path + @"Release\"))
                                System.IO.Directory.CreateDirectory(Path + @"Release\");
                            string Name = E3Converter.FileNames[i].Remove(0, pos + 1);
                            RM2.Save(Path + @"Release\" + Name);
                            break;
                        }

                    case 4 // Release to E3 GameObject
             :
                        {
                            GameObject GO = new GameObject();
                            System.IO.FileStream File = new System.IO.FileStream(E3Converter.FileNames[i], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            System.IO.BinaryReader FileReader = new System.IO.BinaryReader(File);
                            GO.Base = 0;
                            GO.Offset = 0;
                            GO.Size = (uint)File.Length;
                            GO.Load(ref File, ref FileReader);
                            DemoGameObject E3GO = new DemoGameObject();
                            /// 
                            ConvertGO(ref E3GO, ref GO);
                            /// 
                            System.IO.MemoryStream BS = E3GO.Get_Stream(0, null);
                            File.Close();
                            if (!System.IO.Directory.Exists(Path + @"E3\"))
                                System.IO.Directory.CreateDirectory(Path + @"E3\");
                            string Name = E3GO.Name.Replace("|", " ");
                            Name = Name.Replace(":", " ");
                            File = new System.IO.FileStream(Path + @"E3\" + Name + " ID " + E3GO.ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            System.IO.BinaryWriter FileWriter = new System.IO.BinaryWriter(File);
                            FileWriter.Write(BS.ToArray());
                            File.Close();
                            break;
                        }

                    case 5: // Release to E3 Instance
             
                        {
                            Instance INST = new Instance();
                            System.IO.FileStream File = new System.IO.FileStream(E3Converter.FileNames[i], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            System.IO.BinaryReader FileReader = new System.IO.BinaryReader(File);
                            INST.Base = 0;
                            INST.Offset = 0;
                            INST.Size = (uint)File.Length;
                            INST.Load(ref File, ref FileReader);
                            DemoInstance E3INST = new DemoInstance();
                            /// 
                            ConvertINST(ref E3INST, ref INST);
                            /// 
                            System.IO.MemoryStream BS = E3INST.Get_Stream(0, null);
                            File.Close();
                            if (!System.IO.Directory.Exists(Path + @"E3\"))
                                System.IO.Directory.CreateDirectory(Path + @"E3\");
                            File = new System.IO.FileStream(Path + @"E3\" + "Instance ID " + E3INST.ID.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            System.IO.BinaryWriter FileWriter = new System.IO.BinaryWriter(File);
                            FileWriter.Write(BS.ToArray());
                            File.Close();
                            break;
                        }

                    case 6: // Release to E3 Level
             
                        {
                            RM2 RM2 = new RM2();
                            RM2.LoadRM2(E3Converter.FileNames[i]);
                            RM2 E3RM2 = new RM2();
                            E3RM2.Header = RM2.Header;
                            E3RM2.Size = RM2.Size;
                            E3RM2.Records = RM2.Records;
                            Array.Resize(ref E3RM2.Item, E3RM2.Records);
                            for (int j = 0; j <= RM2.Records - 1; j++)
                            {
                                switch (RM2.Item[j].ID)
                                {
                                    case 8:
                                    case 9:
                                    case 11:
                                        {
                                            E3RM2.Item[j] = RM2.Item[j];
                                            break;
                                        }

                                    case 10:
                                        {
                                            CodeSection Cs = (CodeSection)RM2.Item[j];
                                            DemoCodeSection E3Cs = new DemoCodeSection();
                                            E3Cs.Records = Cs.Records;
                                            E3Cs.Header = Cs.Header;
                                            E3Cs.ID = 10;
                                            Array.Resize(ref E3Cs._Item, E3Cs.Records);
                                            for (int a = 0; a <= E3Cs.Records - 1; a++)
                                            {
                                                if (!(Cs._Item[a].ID == 0))
                                                    E3Cs._Item[a] = Cs._Item[a];
                                                else
                                                {
                                                    GameObjects GOs = (GameObjects)Cs._Item[a];
                                                    DemoGameObjects E3GOs = new DemoGameObjects();
                                                    E3GOs.Records = GOs.Records;
                                                    E3GOs.Header = GOs.Header;
                                                    E3GOs.ID = 0;
                                                    Array.Resize(ref E3GOs._Item, E3GOs.Records);
                                                    for (int b = 0; b <= E3GOs.Records - 1; b++)
                                                    {
                                                        GameObject GO = (GameObject)GOs._Item[b];
                                                        DemoGameObject E3GO = new DemoGameObject();
                                                        ConvertGO(ref E3GO, ref GO);
                                                        E3GOs._Item[b] = E3GO;
                                                    }
                                                    E3Cs._Item[a] = E3GOs;
                                                }
                                            }
                                            E3RM2.Item[j] = E3Cs;
                                            break;
                                        }

                                    default:
                                        {
                                            InstanceInfoSection IIs = (InstanceInfoSection)RM2.Item[j];
                                            DemoInstanceInfoSection E3IIs = new DemoInstanceInfoSection();
                                            E3IIs.Records = IIs.Records;
                                            E3IIs.Header = IIs.Header;
                                            E3IIs.ID = IIs.ID;
                                            Array.Resize(ref E3IIs._Item, E3IIs.Records);
                                            for (int a = 0; a <= E3IIs.Records - 1; a++)
                                            {
                                                if (!(IIs._Item[a].ID == 6))
                                                    E3IIs._Item[a] = IIs._Item[a];
                                                else
                                                {
                                                    Instances INSTs = (Instances)IIs._Item[a];
                                                    DemoInstances E3INSTs = new DemoInstances();
                                                    E3INSTs.Records = INSTs.Records;
                                                    E3INSTs.Header = INSTs.Header;
                                                    E3INSTs.ID = 6;
                                                    Array.Resize(ref E3INSTs._Item, E3INSTs.Records);
                                                    for (int b = 0; b <= E3INSTs.Records - 1; b++)
                                                    {
                                                        Instance INST = (Instance)INSTs._Item[b];
                                                        DemoInstance E3INST = new DemoInstance();
                                                        ConvertINST(ref E3INST, ref INST);
                                                        E3INSTs._Item[b] = E3INST;
                                                    }
                                                    E3IIs._Item[a] = E3INSTs;
                                                }
                                            }
                                            E3RM2.Item[j] = E3IIs;
                                            break;
                                        }
                                }
                            }
                            if (!System.IO.Directory.Exists(Path + @"E3\"))
                                System.IO.Directory.CreateDirectory(Path + @"E3\");
                            string Name = E3Converter.FileNames[i].Remove(0, pos + 1);
                            E3RM2.Save(Path + @"E3\" + Name);
                            break;
                        }
                }
            }
        }
        private void ConvertE3GO(ref GameObject GO, ref DemoGameObject E3GO)
        {
            GO.ID = E3GO.ID;
            GO.Class1 = E3GO.Class1;
            GO.Class2 = E3GO.Class2;
            GO.Class3 = E3GO.Class3;
            GO.Name = E3GO.Name;
            GO.UnkI32Number = E3GO.UnkI32Number;
            GO.OGINumber = E3GO.OGINumber;
            GO.AnimationNumber = E3GO.AnimationNumber;
            GO.ScriptNumber = E3GO.ScriptNumber;
            GO.GameObjectNumber = E3GO.GameObjectNumber;
            GO.SoundNumber = E3GO.SoundNumber;
            GO.UnkI32 = E3GO.UnkI32;
            GO.OGI = E3GO.OGI;
            GO.Animation = E3GO.Animation;
            GO.Script = E3GO.Script;
            GO._GameObject = E3GO._GameObject;
            GO.Sound = E3GO.Sound;
            GO.ParametersHeader = E3GO.ParametersHeader;
            GO.ParametersUnkI32 = E3GO.ParametersUnkI32;
            GO.ParametersUnkI321Number = E3GO.ParametersUnkI321Number;
            GO.ParametersUnkI322Number = E3GO.ParametersUnkI322Number;
            GO.ParametersUnkI323Number = E3GO.ParametersUnkI323Number;
            GO.ParametersUnkI321 = E3GO.ParametersUnkI321;
            GO.ParametersUnkI322 = E3GO.ParametersUnkI322;
            GO.ParametersUnkI323 = E3GO.ParametersUnkI323;
            GO.ChildFlag = E3GO.ChildFlag;
            GO.ChildObjectNumber = E3GO.ChildObjectNumber;
            GO.ChildOGINumber = E3GO.ChildOGINumber;
            GO.ChildAnimationNumber = E3GO.ChildAnimationNumber;
            GO.ChildScriptNumber = E3GO.ChildScriptNumber;
            GO.ChildID4Number = E3GO.ChildID4Number;
            GO.ChildSoundNumber = E3GO.ChildSoundNumber;
            GO.ChildUnknownNumber = E3GO.ChildUnknownNumber;
            GO.ChildObject = E3GO.ChildObject;
            GO.ChildOGI = E3GO.ChildOGI;
            GO.ChildAnimation = E3GO.ChildAnimation;
            GO.ChildScript = E3GO.ChildScript;
            GO.ChildID4 = E3GO.ChildID4;
            GO.ChildSound = E3GO.ChildSound;
            GO.ChildUnknown = E3GO.ChildUnknown;
            GO.ScriptLength = E3GO.ScriptLength;
            GO.ScriptParameters = E3GO.ScriptParameters;
            GO.ScriptArray = E3GO.ScriptArray;
        }
        private void ConvertE3INST(ref Instance INST, ref DemoInstance E3INST)
        {
            INST.ID = E3INST.ID;
            INST.X = E3INST.X;
            INST.Y = E3INST.Y;
            INST.Z = E3INST.Z;
            INST.W = E3INST.W;
            INST.RX = E3INST.RX;
            INST.RY = E3INST.RY;
            INST.RZ = E3INST.RZ;
            INST.COMRX = E3INST.COMRX;
            INST.COMRY = E3INST.COMRY;
            INST.COMRZ = E3INST.COMRZ;
            INST.Size1 = E3INST.Size1;
            INST.Size2 = E3INST.Size2;
            INST.Size3 = E3INST.Size3;
            INST.SomeNum1 = E3INST.SomeNum1;
            INST.SomeNum2 = E3INST.SomeNum2;
            INST.SomeNum3 = E3INST.SomeNum3;
            INST.Something1 = E3INST.Something1;
            INST.Something2 = E3INST.Something2;
            INST.Something3 = E3INST.Something3;
            INST.ObjectID = E3INST.ObjectID;
            INST.AfterOID = E3INST.AfterOID;
            INST.ParametersHeader = E3INST.ParametersHeader;
            INST.UnkI32 = E3INST.UnkI32;
            INST.UnkI321Number = E3INST.UnkI321Number;
            INST.UnkI322Number = E3INST.UnkI322Number;
            INST.UnkI323Number = E3INST.UnkI323Number;
            INST.UnkI321 = E3INST.UnkI321;
            INST.UnkI322 = E3INST.UnkI322;
            INST.UnkI323 = E3INST.UnkI323;
        }
        private void ConvertGO(ref DemoGameObject E3GO, ref GameObject GO)
        {
            E3GO.ID = GO.ID;
            E3GO.Class1 = GO.Class1;
            E3GO.Class2 = GO.Class2;
            E3GO.Class3 = GO.Class3;
            E3GO.Name = GO.Name;
            E3GO.UnkI32Number = GO.UnkI32Number;
            E3GO.OGINumber = GO.OGINumber;
            E3GO.AnimationNumber = GO.AnimationNumber;
            E3GO.ScriptNumber = GO.ScriptNumber;
            E3GO.GameObjectNumber = GO.GameObjectNumber;
            E3GO.SoundNumber = GO.SoundNumber;
            E3GO.UnkI32 = GO.UnkI32;
            E3GO.OGI = GO.OGI;
            E3GO.Animation = GO.Animation;
            E3GO.Script = GO.Script;
            E3GO._GameObject = GO._GameObject;
            E3GO.Sound = GO.Sound;
            E3GO.ParametersHeader = GO.ParametersHeader;
            E3GO.ParametersUnkI32 = GO.ParametersUnkI32;
            E3GO.ParametersUnkI321Number = GO.ParametersUnkI321Number;
            E3GO.ParametersUnkI322Number = GO.ParametersUnkI322Number;
            E3GO.ParametersUnkI323Number = GO.ParametersUnkI323Number;
            E3GO.ParametersUnkI321 = GO.ParametersUnkI321;
            E3GO.ParametersUnkI322 = GO.ParametersUnkI322;
            E3GO.ParametersUnkI323 = GO.ParametersUnkI323;
            E3GO.ChildFlag = GO.ChildFlag;
            E3GO.ChildObjectNumber = GO.ChildObjectNumber;
            E3GO.ChildOGINumber = GO.ChildOGINumber;
            E3GO.ChildAnimationNumber = GO.ChildAnimationNumber;
            E3GO.ChildScriptNumber = GO.ChildScriptNumber;
            E3GO.ChildID4Number = GO.ChildID4Number;
            E3GO.ChildSoundNumber = GO.ChildSoundNumber;
            E3GO.ChildUnknownNumber = GO.ChildUnknownNumber;
            E3GO.ChildObject = GO.ChildObject;
            E3GO.ChildOGI = GO.ChildOGI;
            E3GO.ChildAnimation = GO.ChildAnimation;
            E3GO.ChildScript = GO.ChildScript;
            E3GO.ChildID4 = GO.ChildID4;
            E3GO.ChildSound = GO.ChildSound;
            E3GO.ChildUnknown = GO.ChildUnknown;
            E3GO.ScriptLength = GO.ScriptLength;
            E3GO.ScriptParameters = GO.ScriptParameters;
            E3GO.ScriptArray = GO.ScriptArray;
        }
        private void ConvertINST(ref DemoInstance E3INST, ref Instance INST)
        {
            E3INST.ID = INST.ID;
            E3INST.X = INST.X;
            E3INST.Y = INST.Y;
            E3INST.Z = INST.Z;
            E3INST.W = INST.W;
            E3INST.RX = INST.RX;
            E3INST.RY = INST.RY;
            E3INST.RZ = INST.RZ;
            E3INST.COMRX = INST.COMRX;
            E3INST.COMRY = INST.COMRY;
            E3INST.COMRZ = INST.COMRZ;
            E3INST.Size1 = INST.Size1;
            E3INST.Size2 = INST.Size2;
            E3INST.Size3 = INST.Size3;
            E3INST.SomeNum1 = INST.SomeNum1;
            E3INST.SomeNum2 = INST.SomeNum2;
            E3INST.SomeNum3 = INST.SomeNum3;
            E3INST.Something1 = INST.Something1;
            E3INST.Something2 = INST.Something2;
            E3INST.Something3 = INST.Something3;
            E3INST.ObjectID = INST.ObjectID;
            E3INST.AfterOID = INST.AfterOID;
            E3INST.ParametersHeader = INST.ParametersHeader;
            E3INST.UnkI32 = INST.UnkI32;
            E3INST.UnkI321Number = INST.UnkI321Number;
            E3INST.UnkI322Number = INST.UnkI322Number;
            E3INST.UnkI323Number = INST.UnkI323Number;
            E3INST.UnkI321 = INST.UnkI321;
            E3INST.UnkI322 = INST.UnkI322;
            E3INST.UnkI323 = INST.UnkI323;
        }
        public struct Libr
        {
            public uint ID;
            public string Name;
            public int InstReferSize;
            public int PosReferSize;
            public int PathReferSize;
            public UInt16[] InstRefer;
            public UInt16[] PosRefer;
            public UInt16[] PathRefer;
            public uint ParamNumber;
            public int SomeParamSize;
            public int FloatSize;
            public int IntSize;
            public uint[] SomeParams;
            public float[] Floats;
            public uint[] Ints;
        }
        public void LoadLibrary(ref Libr[] Librar)
        {
            if (System.IO.File.Exists(Application.StartupPath + @"\Library.txt"))
            {
                System.IO.FileStream File = new System.IO.FileStream(Application.StartupPath + @"\Library.txt", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader FileReader = new System.IO.StreamReader(File);
                while (!FileReader.EndOfStream)
                {
                    string str = FileReader.ReadLine();
                    string[] arrstr = str.Split('|');
                    Array.Resize(ref Librar, Librar.Length + 1);
                    Librar[Librar.Length - 1].ID = uint.Parse(arrstr[0]);
                    Librar[Librar.Length - 1].Name = arrstr[1];
                    Librar[Librar.Length - 1].InstReferSize = int.Parse(arrstr[2]);
                    Array.Resize(ref Librar[Librar.Length - 1].InstRefer, Librar[Librar.Length - 1].InstReferSize);
                    int offset = 0;
                    for (int i = 0; i <= Librar[Librar.Length - 1].InstReferSize - 1; i++)
                        Librar[Librar.Length - 1].InstRefer[i] = UInt16.Parse(arrstr[2 + i + offset]);
                    offset += Librar[Librar.Length - 1].InstReferSize;
                    Librar[Librar.Length - 1].PosReferSize = int.Parse(arrstr[3 + offset]);
                    Array.Resize(ref Librar[Librar.Length - 1].PosRefer, Librar[Librar.Length - 1].PosReferSize);
                    for (int i = 0; i <= Librar[Librar.Length - 1].PosReferSize - 1; i++)
                        Librar[Librar.Length - 1].PosRefer[i] = UInt16.Parse(arrstr[3 + i + offset]);
                    offset += Librar[Librar.Length - 1].PosReferSize;
                    Librar[Librar.Length - 1].PathReferSize = int.Parse(arrstr[4 + offset]);
                    Array.Resize(ref Librar[Librar.Length - 1].PathRefer, Librar[Librar.Length - 1].PathReferSize);
                    for (int i = 0; i <= Librar[Librar.Length - 1].PosReferSize - 1; i++)
                        Librar[Librar.Length - 1].PathRefer[i] = UInt16.Parse(arrstr[4 + i + offset]);
                    offset += Librar[Librar.Length - 1].PathReferSize;
                    Librar[Librar.Length - 1].ParamNumber = uint.Parse(arrstr[5 + offset]);
                    Librar[Librar.Length - 1].SomeParamSize = int.Parse(arrstr[6 + offset]);
                    Array.Resize(ref Librar[Librar.Length - 1].SomeParams, Librar[Librar.Length - 1].SomeParamSize);
                    for (int i = 0; i <= Librar[Librar.Length - 1].PosReferSize - 1; i++)
                        Librar[Librar.Length - 1].SomeParams[i] = uint.Parse(arrstr[6 + i + offset]);
                    offset += Librar[Librar.Length - 1].SomeParamSize;
                    Librar[Librar.Length - 1].FloatSize = int.Parse(arrstr[7 + offset]);
                    Array.Resize(ref Librar[Librar.Length - 1].Floats, Librar[Librar.Length - 1].FloatSize);
                    for (int i = 0; i <= Librar[Librar.Length - 1].FloatSize - 1; i++)
                        Librar[Librar.Length - 1].Floats[i] = float.Parse(arrstr[8 + i + offset]);
                    offset += Librar[Librar.Length - 1].FloatSize;
                    Librar[Librar.Length - 1].IntSize = int.Parse(arrstr[8 + offset]);
                    Array.Resize(ref Librar[Librar.Length - 1].Ints, Librar[Librar.Length - 1].IntSize);
                    for (int i = 0; i <= Librar[Librar.Length - 1].IntSize - 1; i++)
                        Librar[Librar.Length - 1].Ints[i] = uint.Parse(arrstr[9 + i + offset]);
                    offset += Librar[Librar.Length - 1].IntSize;
                }
            }
            else
                Array.Resize(ref Librar, 0);
        }
        public Libr[] Library = new Libr[] { };
        private void RefresLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLibrary(ref Library);
        }
        private void GeoDataVisualiserToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < fileData.Records; i++)
            {
                if (fileData.Item[i].ID == 9)
                {
                    Form frm = new Form();
                    RMViewer viewer = new RMViewer((GeoData)fileData.Item[i]);
                    viewer.Dock = DockStyle.Fill;
                    frm.Controls.Add(viewer);
                    frm.Show();
                    //_viewGeoData.GD = (GeoData)fileData.Item[i];
                    //_viewGeoData.Show();
                    //_viewGeoData.Init();
                    break;
                }
            }
        }

        private void ClearGeoDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fileData.Records; i++)
            {
                if (fileData.Item[i].ID == 9)
                {
                    GeoData GD = (GeoData)fileData.Item[i];
                    GD.Triggers.Clear();
                    GD.Groups.Clear();
                    GD.Tris.Clear();
                    GD.Vertices.Clear();
                    fileData.Item[i] = GD;
                    RefreshSummary();
                    fileData.Put_Item(GD, CalculateIndexes(TreeView1.Nodes[0].Nodes[i]));
                    break;
                }
            }
        }

        private void ExportSingleOBJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ObjSingleSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream ObjFile = new System.IO.FileStream(ObjSingleSave.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.StreamWriter ObjWriter = new System.IO.StreamWriter(ObjFile);
                GeoData GD = null;
                for (int i = 0; i < fileData.Records; i++)
                {
                    if (fileData.Item[i].ID == 9)
                    {
                        GD = (GeoData)fileData.Item[i];
                        break;
                    }
                }
                if (GD == null) return;
                ObjWriter.WriteLine("# Converted by TwinsanityEditor, made by Neo_Kesha");
                ObjWriter.WriteLine("# All rights on game assets belongs to their respective authors");
                ObjWriter.WriteLine("o TwinsanityMap");
                for (int i = 0; i < GD.Vertices.Count; i++)
                    ObjWriter.WriteLine("v " + GD.Vertices[i].X.ToString() + " " + GD.Vertices[i].Y.ToString() + " " + GD.Vertices[i].Z.ToString() + " 1,0");
                for (int i = 0; i < GD.Tris.Count; i++)
                {
                    /*Microsoft.DirectX.Vector3 vert1 = new Microsoft.DirectX.Vector3(GD.Vertex[GD.Indexes[i].Vert1].X, GD.Vertex[GD.Indexes[i].Vert1].Y, GD.Vertex[GD.Indexes[i].Vert1].Z);
                    Microsoft.DirectX.Vector3 vert2 = new Microsoft.DirectX.Vector3(GD.Vertex[GD.Indexes[i].Vert2].X, GD.Vertex[GD.Indexes[i].Vert2].Y, GD.Vertex[GD.Indexes[i].Vert2].Z);
                    Microsoft.DirectX.Vector3 vert3 = new Microsoft.DirectX.Vector3(GD.Vertex[GD.Indexes[i].Vert3].X, GD.Vertex[GD.Indexes[i].Vert3].Y, GD.Vertex[GD.Indexes[i].Vert3].Z);
                    Microsoft.DirectX.Vector3 normal = CalcNormal(vert1, vert2, vert3);*/
                    OpenTK.Vector3 vert1 = new OpenTK.Vector3(GD.Vertices[GD.Tris[i].Vert1].X, GD.Vertices[GD.Tris[i].Vert1].Y, GD.Vertices[GD.Tris[i].Vert1].Z);
                    OpenTK.Vector3 vert2 = new OpenTK.Vector3(GD.Vertices[GD.Tris[i].Vert2].X, GD.Vertices[GD.Tris[i].Vert2].Y, GD.Vertices[GD.Tris[i].Vert2].Z);
                    OpenTK.Vector3 vert3 = new OpenTK.Vector3(GD.Vertices[GD.Tris[i].Vert3].X, GD.Vertices[GD.Tris[i].Vert3].Y, GD.Vertices[GD.Tris[i].Vert3].Z);
                    OpenTK.Vector3 normal = CalcNormal(vert1, vert2, vert3);
                    ObjWriter.WriteLine("vn " + normal.X.ToString() + " " + normal.Y.ToString() + " " + normal.Z.ToString());
                }
                for (int i = 0; i < GD.Tris.Count; i++)
                    ObjWriter.WriteLine("f " + (GD.Tris[i].Vert1 + 1).ToString() + "//" + (i + 1).ToString() + " " + (GD.Tris[i].Vert2 + 1).ToString() + "//" + (i + 1).ToString() + " " + (GD.Tris[i].Vert3 + 1).ToString() + "//" + (i + 1).ToString());
                ObjWriter.Close();
                ObjFile.Close();
                Interaction.MsgBox("Done.");
            }
        }
        public static OpenTK.Vector3 CalcNormal(OpenTK.Vector3 Vert1, OpenTK.Vector3 Vert2, OpenTK.Vector3 Vert3)
        {
            float x1 = Vert1.X;
            float y1 = Vert1.Y;
            float z1 = Vert1.Z;
            float x2 = Vert2.X;
            float y2 = Vert2.Y;
            float z2 = Vert2.Z;
            float x3 = Vert3.X;
            float y3 = Vert3.Y;
            float z3 = Vert3.Z;
            float nx = ((y1 - y2) * (z1 - z3)) - ((z1 - z2) * (y1 - y3));
            float ny = ((z1 - z2) * (x1 - x3)) - ((x1 - x2) * (z1 - z3));
            float nz = ((x1 - x2) * (y1 - y3)) - ((y1 - y2) * (x1 - x3));
            return new OpenTK.Vector3(nx, ny, nz);
        }

        private void ExportOBJLayeredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ObjSingleSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<int> surfaces = new List<int>();
                GeoData GD = null;
                for (int i = 0; i < fileData.Records; i++)
                {
                    if (fileData.Item[i].ID == 9)
                    {
                        GD = (GeoData)fileData.Item[i];
                        break;
                    }
                }
                if (GD == null) return;
                for (int i = 0; i < GD.Tris.Count; i++)
                {
                    while (GD.Tris[i].Surface > surfaces.Count)
                        surfaces.Add(0);
                    surfaces[GD.Tris[i].Surface]++;
                }
                string Name = ObjSingleSave.FileName.Split('.')[0];
                for (int i = 0; i < surfaces.Count && surfaces[i] > 0; i++)
                {
                    System.IO.FileStream ObjFile = new System.IO.FileStream(Name + "ID" + i.ToString() + ".obj", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    System.IO.StreamWriter ObjWriter = new System.IO.StreamWriter(ObjFile);
                    HashSet<int> vertexHash = new HashSet<int>(); //we create a hashset to prevent duplicate entries
                    uint cnt = 0;
                    ObjWriter.WriteLine("# Map Surface ID " + i.ToString());
                    ObjWriter.WriteLine("# Converted by TwinsanityEditor, made by Neo_Kesha");
                    ObjWriter.WriteLine("# All rights on game assets belongs to their respective owners");
                    for (int j = 0; j < GD.Tris.Count; j++)
                        if (GD.Tris[j].Surface == i)
                        {
                            vertexHash.Add(GD.Tris[j].Vert1);
                            vertexHash.Add(GD.Tris[j].Vert2);
                            vertexHash.Add(GD.Tris[j].Vert3);
                        }
                    List<int> vertexList = new List<int>(vertexHash); //we convert the hash to a usable list, this is MUCH faster
                    for (int j = 0; j < vertexList.Count; j++)
                        ObjWriter.WriteLine("v " + GD.Vertices[vertexList[j]].X.ToString() + " " + GD.Vertices[vertexList[j]].Y.ToString() + " " + GD.Vertices[vertexList[j]].Z.ToString() + " 1,0");
                    for (int j = 0; j < GD.Groups.Count; j++)
                    {
                        bool flag = false;
                        for (int k = (int)GD.Groups[j].Offset; k < GD.Groups[j].Offset + GD.Groups[j].Size; k++)
                        {
                            if (GD.Tris[k].Surface == i)
                            {
                                if (!flag)
                                {
                                    flag = true;
                                    ObjWriter.WriteLine("o G" + j.ToString());
                                }
                                cnt++;
                                /*Microsoft.DirectX.Vector3 V1 = new Microsoft.DirectX.Vector3(GD.Vertex[GD.Indexes[k].Vert1].X, GD.Vertex[GD.Indexes[k].Vert1].Y, GD.Vertex[GD.Indexes[k].Vert1].Z);
                                Microsoft.DirectX.Vector3 V2 = new Microsoft.DirectX.Vector3(GD.Vertex[GD.Indexes[k].Vert2].X, GD.Vertex[GD.Indexes[k].Vert2].Y, GD.Vertex[GD.Indexes[k].Vert2].Z);
                                Microsoft.DirectX.Vector3 V3 = new Microsoft.DirectX.Vector3(GD.Vertex[GD.Indexes[k].Vert3].X, GD.Vertex[GD.Indexes[k].Vert3].Y, GD.Vertex[GD.Indexes[k].Vert3].Z);
                                Microsoft.DirectX.Vector3 Normal = CalcNormal(V1, V2, V3);*/
                                OpenTK.Vector3 vert1 = new OpenTK.Vector3(GD.Vertices[GD.Tris[k].Vert1].X, GD.Vertices[GD.Tris[k].Vert1].Y, GD.Vertices[GD.Tris[k].Vert1].Z);
                                OpenTK.Vector3 vert2 = new OpenTK.Vector3(GD.Vertices[GD.Tris[k].Vert2].X, GD.Vertices[GD.Tris[k].Vert2].Y, GD.Vertices[GD.Tris[k].Vert2].Z);
                                OpenTK.Vector3 vert3 = new OpenTK.Vector3(GD.Vertices[GD.Tris[k].Vert3].X, GD.Vertices[GD.Tris[k].Vert3].Y, GD.Vertices[GD.Tris[k].Vert3].Z);
                                OpenTK.Vector3 normal = CalcNormal(vert1, vert2, vert3);
                                ObjWriter.WriteLine("vn " + normal.X.ToString() + " " + normal.Y.ToString() + " " + normal.Z.ToString());
                                ObjWriter.WriteLine("f " + GD.Vertices[GD.Tris[k].Vert1].ToString() + "//" + cnt.ToString() + " " + GD.Vertices[GD.Tris[k].Vert2].ToString() + "//" + cnt.ToString() + " " + GD.Vertices[GD.Tris[k].Vert3].ToString() + "//" + cnt.ToString());
                            }
                        }
                    }
                    ObjWriter.Close();
                    ObjFile.Close();
                }
                Interaction.MsgBox("Done");
            }
        }
        private struct Face
        {
            public int Vert1;
            public int Vert2;
            public int Vert3;
        }
        private struct Group
        {
            public uint Offset;
            public uint Size;
        }
        private void AddLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenObj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _viewGeoData.Close();
                GeoData GD = new GeoData();
                for (int i = 0; i <= fileData.Records - 1; i++)
                {
                    if (fileData.Item[i].ID == 9)
                    {
                        GD = (GeoData)fileData.Item[i];
                        break;
                    }
                }
                int SID = int.Parse(ToolStripMenuItem3.Text);
                List<OpenTK.Vector3> verts = new List<OpenTK.Vector3>();
                List<Face> faces = new List<Face>();
                List<Group> groups = new List<Group>();
                int Shift = GD.Vertices.Count;
                int IndShift = GD.Tris.Count;
                int GroupShift = GD.Groups.Count;
                System.IO.FileStream ObjFile = new System.IO.FileStream(OpenObj.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader ObjReader = new System.IO.StreamReader(ObjFile);
                while (!ObjReader.EndOfStream)
                {
                    string[] str = ObjReader.ReadLine().Split(' ');
                    switch (str[0])
                    {
                        case "v":
                            verts.Add(new OpenTK.Vector3(float.Parse(str[1].Replace(".", ",")), float.Parse(str[2].Replace(".", ",")), float.Parse(str[3].Replace(".", ","))));
                            break;
                        case "f":
                            {
                                faces.Add(new Face { Vert1 = int.Parse(str[1].Split('/')[0]), Vert2 = int.Parse(str[2].Split('/')[0]), Vert3 = int.Parse(str[3].Split('/')[0]) });
                                Group grp = groups[groups.Count - 1];
                                grp.Size += 1;
                                break;
                            }
                        case "o":
                            groups.Add(new Group { Offset = groups.Count > 1 ? (groups[groups.Count - 2].Offset + groups[groups.Count - 2].Size) : 0 });
                            break;
                    }
                }
                for (int i = 0; i < verts.Count; i++)
                {
                    GD.Vertices.Add(new Pos { X = verts[i].X, Y = verts[i].Y, Z = verts[i].Z });
                }
                for (int i = 0; i < faces.Count; i++)
                {
                    GD.Tris.Add(new GeoData.ColTri
                    {
                        Vert1 = faces[i].Vert1 - 1 + Shift,
                        Vert2 = faces[i].Vert2 - 1 + Shift,
                        Vert3 = faces[i].Vert3 - 1 + Shift,
                        Surface = SID
                    });
                }
                for (int i = 0; i < groups.Count; i++)
                {
                    GD.Groups.Add(new GeoData.GroupInfo
                    {
                        Offset = (uint)(groups[i].Offset + IndShift),
                        Size = groups[i].Size
                    });
                }
                //GD.VertexSize = GD.Vertex.Length;
                //GD.IndexSize = GD.Indexes.Length;
                //GD.GroupSize = GD.Groups.Length;
                //GD.TriggerSize = GD.GroupSize * 2 - 1;
                List<GeoData.Trigger> NewTriggers = new List<GeoData.Trigger>(GD.Groups.Count * 2);
                TreeView TriggerTree = new TreeView();
                TriggerTree.Nodes.Add("E", "0");
                int x = (int)Math.Truncate(Math.Log(GD.Groups.Count, 2));
                for (int i = 0; i < x; i++)
                    ExpandLevel(TriggerTree.Nodes[0]);
                ExpandEngings(TriggerTree, (uint)(GD.Groups.Count - Math.Pow(2, x)));
                TreeTemp = 1;
                CalcIDs(TriggerTree.Nodes[0]);
                Tree2Trigger(TriggerTree.Nodes[0], NewTriggers);
                TriggerRecalculate(NewTriggers, GD.Groups, GD.Tris, GD.Vertices, 0);
                GD.Triggers = NewTriggers;
                for (int i = 0; i <= fileData.Records - 1; i++)
                {
                    if (fileData.Item[i].ID == 9)
                    {
                        fileData.Put_Item(GD, CalculateIndexes(TreeView1.Nodes[0].Nodes[i]));
                        break;
                    }
                }
                ObjReader.Close();
                ObjFile.Close();
                Interaction.MsgBox("Done");
            }
        }
        private void DoubleExpand(TreeNode Node)
        {
            Node.Nodes.Add("E", "Node");
            Node.Nodes.Add("E", "Node");
        }
        private void ExpandLevel(TreeNode Root)
        {
            TreeNode[] Nodes = Root.Nodes.Find("E", true);
            if (Root.Name == "E")
            {
                Array.Resize(ref Nodes, Nodes.Length + 1);
                Nodes[Nodes.Length - 1] = Root;
            }
            for (int i = 0; i <= Nodes.Length - 1; i++)
            {
                Nodes[i].Name = "P";
                DoubleExpand(Nodes[i]);
            }
        }
        private void ExpandEngings(TreeView Tree, uint d)
        {
            TreeNode[] Nodes = Tree.Nodes.Find("E", true);
            for (int i = 0; i <= d - 1; i++)
            {
                Nodes[i].Name = "P";
                DoubleExpand(Nodes[i]);
            }
        }
        public uint TreeTemp = 1;
        private void CalcIDs(TreeNode Node)
        {
            if (Node.Name == "P")
            {
                uint x = (uint)Node.Nodes[0].GetNodeCount(true);
                Node.Nodes[0].Text = (int.Parse(Node.Text) + 1).ToString();
                Node.Nodes[1].Text = (int.Parse(Node.Nodes[0].Text) + x + 1).ToString();
                CalcIDs(Node.Nodes[0]);
                CalcIDs(Node.Nodes[1]);
            }
            else if (Node.Name == "E")
            {
                Node.Nodes.Add("Ptr", TreeTemp.ToString());
                TreeTemp += 1;
            }
        }
        private void Tree2Trigger(TreeNode Node, List<GeoData.Trigger> Triggers)
        {
            if (Node.Name == "P")
            {
                GeoData.Trigger trg = Triggers[int.Parse(Node.Text)];
                trg.Flag1 = int.Parse(Node.Nodes[0].Text);
                trg.Flag2 = int.Parse(Node.Nodes[1].Text);
                Tree2Trigger(Node.Nodes[0], Triggers);
                Tree2Trigger(Node.Nodes[1], Triggers);
            }
            else if (Node.Name == "E")
            {
                GeoData.Trigger trg = Triggers[int.Parse(Node.Text)];
                trg.Flag1 = -int.Parse(Node.Nodes[0].Text);
                trg.Flag2 = -int.Parse(Node.Nodes[0].Text);
            }
        }
        private void TriggerRecalculate(List<GeoData.Trigger> Triggers, List<GeoData.GroupInfo> Groups, List<GeoData.ColTri> Indexes, List<Pos> Vertexes, int index)
        {
            GeoData.Trigger trg = Triggers[index];
            if (trg.Flag1 >= 0)
            {
                TriggerRecalculate(Triggers, Groups, Indexes, Vertexes, trg.Flag1);
                TriggerRecalculate(Triggers, Groups, Indexes, Vertexes, trg.Flag2);
                trg.X1 = Math.Min(Triggers[trg.Flag1].X1, Triggers[trg.Flag2].X1);
                trg.Y1 = Math.Min(Triggers[trg.Flag1].Y1, Triggers[trg.Flag2].Y1);
                trg.Z1 = Math.Min(Triggers[trg.Flag1].Z1, Triggers[trg.Flag2].Z1);
                trg.X2 = Math.Max(Triggers[trg.Flag1].X2, Triggers[trg.Flag2].X2);
                trg.Y2 = Math.Max(Triggers[trg.Flag1].Y2, Triggers[trg.Flag2].Y2);
                trg.Z2 = Math.Max(Triggers[trg.Flag1].Z2, Triggers[trg.Flag2].Z2);
            }
            else
            {
                float x1 = 0f, x2 = 0f, y1 = 0f, y2 = 0f, z1 = 0f, z2 = 0f;
                for (int i = (int)Groups[-trg.Flag1 - 1].Offset; i <= Groups[-trg.Flag1 - 1].Offset + Groups[-trg.Flag1 - 1].Size - 1; i++)
                {
                    OpenTK.Vector3 a, b, c;
                    a.X = Vertexes[Indexes[i].Vert1].X;
                    a.Y = Vertexes[Indexes[i].Vert1].Y;
                    a.Z = Vertexes[Indexes[i].Vert1].Z;
                    b.X = Vertexes[Indexes[i].Vert2].X;
                    b.Y = Vertexes[Indexes[i].Vert2].Y;
                    b.Z = Vertexes[Indexes[i].Vert2].Z;
                    c.X = Vertexes[Indexes[i].Vert3].X;
                    c.Y = Vertexes[Indexes[i].Vert3].Y;
                    c.Z = Vertexes[Indexes[i].Vert3].Z;
                    if (x1 == 0)
                        x1 = Math.Min(a.X, Math.Min(b.X, c.X));
                    else
                        x1 = Math.Min(x1, Math.Min(a.X, Math.Min(b.X, c.X)));
                    if (y1 == 0)
                        y1 = Math.Min(a.Y, Math.Min(b.Y, c.Y));
                    else
                        y1 = Math.Min(y1, Math.Min(a.Y, Math.Min(b.Y, c.Y)));
                    if (z1 == 0)
                        z1 = Math.Min(a.Z, Math.Min(b.Z, c.Z));
                    else
                        z1 = Math.Min(z1, Math.Min(a.Z, Math.Min(b.Z, c.Z)));
                    if (x2 == 0)
                        x2 = Math.Max(a.X, Math.Max(b.X, c.X));
                    else
                        x2 = Math.Max(x2, Math.Max(a.X, Math.Min(b.X, c.X)));
                    if (y2 == 0)
                        y2 = Math.Max(a.Y, Math.Max(b.Y, c.Y));
                    else
                        y2 = Math.Max(y2, Math.Max(a.Y, Math.Min(b.Y, c.Y)));
                    if (z2 == 0)
                        z2 = Math.Max(a.Z, Math.Max(b.Z, c.Z));
                    else
                        z2 = Math.Max(z2, Math.Max(a.Z, Math.Min(b.Z, c.Z)));
                }
                trg.X1 = x1;
                trg.Y1 = y1;
                trg.Z1 = z1;
                trg.X2 = x2;
                trg.Y2 = y2;
                trg.Z2 = z2;
            }
        }
        private void TexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] indexes = CalculateIndexes(TreeView1.SelectedNode);
            BaseObject Obj = fileData.Get_Item(indexes);
            if (Obj is Texture)
            {
                _viewTexture.Hide();
                _viewTexture.Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                _viewTexture.Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                _viewTexture.Mat = false;
                _viewTexture.CurTex = (uint)TreeView1.SelectedNode.Index;
                _viewTexture.Show();
            }
            else if (Obj is Material)
            {
                _viewTexture.Hide();
                Material MAT = (Material)Obj;
                uint TexID = MAT.Texture;
                _viewTexture.Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                _viewTexture.Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                _viewTexture.Mat = true;
                _viewTexture.CurTex = (uint)TreeView1.SelectedNode.Index;
                _viewTexture.Show();
            }
            else if (Obj is Model)
            {
                _viewModel.Model = (Model)Obj;
                _viewModel.Show();
            }
            else if (Obj is Twinsanity.GC)
            {
                Twinsanity.GC GC = (Twinsanity.GC)Obj;
                Texture[] Texture = new Texture[] { };
                Materials Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                Textures Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                Models Models = (Models)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[2]));
                Material[] MAT = new Material[] { };
                for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                {
                    for (int j = 0; j <= Materials._Item.Length - 1; j++)
                    {
                        if (GC.Material[i] == Materials._Item[j].ID)
                        {
                            Array.Resize(ref MAT, MAT.Length + 1);
                            MAT[MAT.Length - 1] = (Material)Materials._Item[j];
                            break;
                        }
                    }
                }
                for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                {
                    for (int j = 0; j <= Textures._Item.Length - 1; j++)
                    {
                        if (MAT[i].Texture == Textures._Item[j].ID)
                        {
                            Array.Resize(ref Texture, Texture.Length + 1);
                            Texture[Texture.Length - 1] = (Texture)Textures._Item[j];
                            break;
                        }
                    }
                }
                for (int i = 0; i <= Models._Item.Length - 1; i++)
                {
                    if (Models._Item[i].ID == GC.Model)
                    {
                        _viewModel.Model = (Model)Models._Item[i];
                        break;
                    }
                }
                Texture[] twinTexs = _viewModel.TwinTextures;
                Array.Resize(ref twinTexs, Texture.Length);
                _viewModel.TwinTextures = twinTexs;
                Texture.CopyTo(_viewModel.TwinTextures, 0);
                _viewModel.Show();
            }
            else if (Obj is Scenery)
            {
                Scenery SCEN = (Scenery)Obj;
                GCs GCs = (GCs)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[6]));
                Terrains Ts = (Terrains)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[7]));
                Twinsanity.GC[] SGC = new Twinsanity.GC[] { };
                Scenery.Matrix4[] M = new Scenery.Matrix4[] { };
                float[] Kf = new float[] { };
                for (int Si = 0; Si <= SCEN.E3.Length - 1; Si++)
                {
                    for (int Sj = 0; Sj <= SCEN.E3[Si].GCCount - 1; Sj++)
                    {
                        for (int Sk = 0; Sk <= GCs.Records - 1; Sk++)
                        {
                            if (GCs._Item[Sk].ID == SCEN.E3[Si].GCID[Sj])
                            {
                                Array.Resize(ref SGC, SGC.Length + 1);
                                Array.Resize(ref M, M.Length + 1);
                                Array.Resize(ref Kf, Kf.Length + 1);
                                SGC[SGC.Length - 1] = (Twinsanity.GC)GCs._Item[Sk];
                                M[M.Length - 1] = SCEN.E3[Si].ChunkMatrix[Sj];
                                Kf[Kf.Length - 1] = 1;
                                break;
                            }
                        }
                    }
                    for (int Sj = 0; Sj <= SCEN.E3[Si].SBCount - 1; Sj++)
                    {
                        for (int Sk = 0; Sk <= Ts.Records - 1; Sk++)
                        {
                            if (Ts._Item[Sk].ID == SCEN.E3[Si].SBID[Sj])
                            {
                                Terrain T = (Terrain)Ts._Item[Sk];
                                for (int Sn = 0; Sn <= T.Num - 1; Sn++)
                                {
                                    for (int Tk = 0; Tk <= GCs.Records - 1; Tk++)
                                    {
                                        if (GCs._Item[Tk].ID == T.IDS[3 - Sn])
                                        {
                                            Array.Resize(ref SGC, SGC.Length + 1);
                                            Array.Resize(ref M, M.Length + 1);
                                            Array.Resize(ref Kf, Kf.Length + 1);
                                            SGC[SGC.Length - 1] = (Twinsanity.GC)GCs._Item[Tk];
                                            M[M.Length - 1] = SCEN.E3[Si].ChunkMatrix[SCEN.E3[Si].GCCount + Sj];
                                            Kf[Kf.Length - 1] = T.K[Sn];
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                int n = 0;
                Texture[] TotalTextures = new Texture[] { };
                Model TotalModel = new Model();
                TotalModel.SubModels = 0;
                TotalModel.SubModel = new Model._SubModel[] { };
                foreach (Twinsanity.GC GC in SGC)
                {
                    Texture[] Texture = new Twinsanity.Texture[] { };
                    Model Model = new Model();
                    Materials Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                    Textures Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                    Models Models = (Models)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[2]));
                    Material[] MAT = new Material[] { };
                    for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                    {
                        for (int j = 0; j <= Materials._Item.Length - 1; j++)
                        {
                            if (GC.Material[i] == Materials._Item[j].ID)
                            {
                                Array.Resize(ref MAT, MAT.Length + 1);
                                MAT[MAT.Length - 1] = (Material)Materials._Item[j];
                                break;
                            }
                        }
                    }
                    for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                    {
                        for (int j = 0; j <= Textures._Item.Length - 1; j++)
                        {
                            if (MAT[i].Texture == Textures._Item[j].ID)
                            {
                                Array.Resize(ref Texture, Texture.Length + 1);
                                Texture[Texture.Length - 1] = (Texture)Textures._Item[j];
                                break;
                            }
                        }
                    }
                    for (int i = 0; i <= Models._Item.Length - 1; i++)
                    {
                        if (Models._Item[i].ID == GC.Model)
                        {
                            var stream = new System.IO.MemoryStream(((Model)Models._Item[i]).ByteStream.ToArray());
                            Model.Put_Stream(stream, 0, null);
                            break;
                        }
                    }
                    Texture[] twinTexs = _viewModel.TwinTextures;
                    Array.Resize(ref twinTexs, Texture.Length);
                    _viewModel.TwinTextures = twinTexs;
                    Texture.CopyTo(_viewModel.TwinTextures, 0);
                    OpenTK.Matrix4 MATRIX = new OpenTK.Matrix4();
                    MATRIX.M11 = M[n].x1;
                    MATRIX.M12 = M[n].y1;
                    MATRIX.M13 = M[n].z1;
                    MATRIX.M14 = M[n].w1;
                    MATRIX.M21 = M[n].x2;
                    MATRIX.M22 = M[n].y2;
                    MATRIX.M23 = M[n].z2;
                    MATRIX.M24 = M[n].w2;
                    MATRIX.M31 = M[n].x3;
                    MATRIX.M32 = M[n].y3;
                    MATRIX.M33 = M[n].z3;
                    MATRIX.M34 = M[n].w3;
                    MATRIX.M41 = M[n].x4;
                    MATRIX.M42 = M[n].y4;
                    MATRIX.M43 = M[n].z4;
                    MATRIX.M44 = M[n].w4;
                    for (int i = 0; i <= Model.SubModels - 1; i++)
                    {
                        for (int j = 0; j <= Model.SubModel[i].Group.Length - 1; j++)
                        {
                            OpenTK.Vector4 v;
                            for (int k = 0; k <= Model.SubModel[i].Group[j].Vertexes - 1; k++)
                            {
                                v = new OpenTK.Vector4(Model.SubModel[i].Group[j].Vertex[k].X, Model.SubModel[i].Group[j].Vertex[k].Y, Model.SubModel[i].Group[j].Vertex[k].Z, 1);
                                v = OpenTK.Vector4.Transform(v,MATRIX);
                                Model.SubModel[i].Group[j].Vertex[k].X = v.X;
                                Model.SubModel[i].Group[j].Vertex[k].Y = v.Y;
                                Model.SubModel[i].Group[j].Vertex[k].Z = v.Z;
                            }
                        }
                    }
                    n += 1;
                    int p = TotalTextures.Length;
                    Array.Resize(ref TotalTextures, p + Texture.Length);
                    Texture.CopyTo(TotalTextures, p);
                    p = TotalModel.SubModels;
                    Array.Resize(ref TotalModel.SubModel, TotalModel.SubModels + Model.SubModels);
                    TotalModel.SubModels += Model.SubModels;
                    Model.SubModel.CopyTo(TotalModel.SubModel, p);
                }
                _viewModel.TwinTextures = (Texture[])TotalTextures.Clone();
                _viewModel.Model = TotalModel;
                _viewModel.Show();
            }
        }
        private void InitDS()
        {
            
        }
        private void ManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseObject Obj = fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
            if (Obj is SoundDescription)
            {
                SoundDescription SD = (SoundDescription)Obj;
                BaseObject ParObj = fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                System.IO.MemoryStream SoundBank = new System.IO.MemoryStream();
                if (ParObj is SoundDescriptions)
                {
                    System.IO.MemoryStream PSoundBank = fileData.Get_Stream(CalculateIndexes(TreeView1.SelectedNode.Parent.Parent.Nodes[TreeView1.SelectedNode.Parent.Index + 1]));
                    System.IO.BinaryReader Reader = new System.IO.BinaryReader(PSoundBank);
                    System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(SoundBank);
                    PSoundBank.Position = SD.SoundOffset;
                    Writer.Write(Reader.ReadBytes((int)SD.SoundSize));
                }
                else
                {
                    SoundbankDescriptions SbD = (SoundbankDescriptions)ParObj;
                    System.IO.MemoryStream PSoundBank = SbD.SoundBank;
                    System.IO.BinaryReader Reader = new System.IO.BinaryReader(PSoundBank);
                    System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(SoundBank);
                    PSoundBank.Position = SD.SoundOffset;
                    Writer.Write(Reader.ReadBytes((int)SD.SoundSize));
                }
                System.IO.MemoryStream WAVEStream = new System.IO.MemoryStream();
                Twinsanity.ADPCM ADPCM_Device = new Twinsanity.ADPCM();
                ADPCM_Device.Twin2WAV(SoundBank, ref WAVEStream, SD.Frequency);
                WAVEStream.Position = 0;

                var src = AL.GenSource();
                var buffer = AL.GenBuffer();
                AL.BindBufferToSource(src, buffer);
                AL.BufferData(buffer, ALFormat.Stereo16, WAVEStream.GetBuffer(), WAVEStream.GetBuffer().Length, SD.Frequency);
                AL.SourcePlay(src);
            }
        }

        private void MHWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseObject Obj = fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
            if (Obj is SoundDescription)
            {
                if (SaveWave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SoundDescription SD = (SoundDescription)Obj;
                    BaseObject ParObj = fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                    System.IO.MemoryStream SoundBank = new System.IO.MemoryStream();
                    if (ParObj is SoundDescriptions)
                    {
                        System.IO.MemoryStream PSoundBank = fileData.Get_Stream(CalculateIndexes(TreeView1.SelectedNode.Parent.Parent.Nodes[TreeView1.SelectedNode.Parent.Index + 1]));
                        System.IO.BinaryReader Reader = new System.IO.BinaryReader(PSoundBank);
                        System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(SoundBank);
                        PSoundBank.Position = SD.SoundOffset;
                        Writer.Write(Reader.ReadBytes((int)SD.SoundSize));
                    }
                    else
                    {
                        SoundbankDescriptions SbD = (SoundbankDescriptions)ParObj;
                        System.IO.MemoryStream PSoundBank = SbD.SoundBank;
                        System.IO.BinaryReader Reader = new System.IO.BinaryReader(PSoundBank);
                        System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(SoundBank);
                        PSoundBank.Position = SD.SoundOffset;
                        Writer.Write(Reader.ReadBytes((int)SD.SoundSize));
                    }
                    System.IO.MemoryStream WAVEStream = new System.IO.MemoryStream();
                    Twinsanity.ADPCM ADPCM_Device = new Twinsanity.ADPCM();
                    SoundBank.Position = 0;
                    WAVEStream.Position = 0;
                    ADPCM_Device.Twin2WAV(SoundBank, ref WAVEStream, SD.Frequency);
                    WAVEStream.Position = 0;
                    System.IO.FileStream File = new System.IO.FileStream(SaveWave.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    System.IO.BinaryWriter FileWriter = new System.IO.BinaryWriter(File);
                    FileWriter.Write(WAVEStream.ToArray());
                    FileWriter.Close();
                    File.Close();
                }
            }
        }

        private void WAVToTwinSNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADPCM ADPCM_Device = new ADPCM();
            if (OpenWave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i <= OpenWave.FileNames.Length - 1; i++)
                {
                    System.IO.FileStream WAV = new System.IO.FileStream(OpenWave.FileNames[i], System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    System.IO.BinaryReader WAVReader = new System.IO.BinaryReader(WAV);
                    string[] FilePath = OpenWave.FileNames[i].Split('.');
                    string Name = "";
                    for (int j = 0; j <= FilePath.Length - 2; j++)
                        Name += FilePath[j] + ".";
                    System.IO.FileStream ADPCM = new System.IO.FileStream(Name, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    System.IO.BinaryWriter ADPCMWriter = new System.IO.BinaryWriter(ADPCM);
                    System.IO.MemoryStream WAVStream = new System.IO.MemoryStream();
                    System.IO.BinaryWriter WAVStreamWriter = new System.IO.BinaryWriter(WAVStream);
                    WAVStreamWriter.Write(WAVReader.ReadBytes((int)WAV.Length));
                    System.IO.MemoryStream ADPCMStream = new System.IO.MemoryStream();
                    System.IO.BinaryReader ADPCMStreamReader = new System.IO.BinaryReader(ADPCMStream);
                    WAVStream.Position = 0;
                    ADPCMStream.Position = 0;
                    ADPCM_Device.WAV2Twin(WAVStream, ref ADPCMStream);
                    ADPCM.Position = 0;
                    ADPCMStream.Position = 0;
                    ADPCMWriter.Write(ADPCMStreamReader.ReadBytes((int)ADPCMStream.Length));
                    WAVReader.Close();
                    WAV.Close();
                    ADPCMWriter.Close();
                    ADPCM.Close();
                }
            }
        }


        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenMusic.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _workMH.Hide();
                string MH = OpenMusic.FileName;
                string MB = "";
                string[] strarr = MH.Split('.');
                for (int i = 0; i <= strarr.Length - 2; i++)
                    MB += strarr[i] + ".";
                MB += "MB";
                _workMH.Init(MH, MB);
                _workMH.Show();
            }
        }


        private void TriggerTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _utilTriggerTree.Hide();
            GeoData GD = new GeoData();
            for (int i = 0; i <= fileData.Records - 1; i++)
            {
                if (fileData.Item[i].ID == 9)
                {
                    GD = (GeoData)fileData.Item[i];
                    break;
                }
            }
            _utilTriggerTree.Trigg = GD.Triggers;
            _utilTriggerTree.Show();
        }

        private void PSMWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _workPSM.Show();
        }

        private void ImportTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _workTextureImport.Show();
        }

        private void ExportModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseObject Obj = fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
            if (Obj is Model)
            {
                if (ObjSingleSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    ExportModel((Model)Obj, ObjSingleSave.FileName);
            }
            else if (Obj is Models)
            {
                if (ExtractBunch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Models MDLs = (Models)Obj;
                    for (int i = 0; i <= MDLs._Item.Length - 1; i++)
                    {
                        string Name = TreeView1.SelectedNode.Nodes[i].Text + ".obj";
                        Name = Name.Replace(":", " ");
                        ExportModel((Model)MDLs._Item[i], ExtractBunch.SelectedPath + @"\" + Name);
                    }
                }
            }
            else if (Obj is Twinsanity.GC)
            {
                if (ExtractBunch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Twinsanity.GC GC = (Twinsanity.GC)Obj;
                    Texture[] Texture = new Twinsanity.Texture[] { };
                    Model Model = null;
                    Materials Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                    Textures Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                    Models Models = (Models)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[2]));
                    Material[] MAT = new Material[] { };
                    for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                    {
                        for (int j = 0; j <= Materials._Item.Length - 1; j++)
                        {
                            if (GC.Material[i] == Materials._Item[j].ID)
                            {
                                Array.Resize(ref MAT, MAT.Length + 1);
                                MAT[MAT.Length - 1] = (Material)Materials._Item[j];
                                break;
                            }
                        }
                    }
                    for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                    {
                        for (int j = 0; j <= Textures._Item.Length - 1; j++)
                        {
                            if (MAT[i].Texture == Textures._Item[j].ID)
                            {
                                Array.Resize(ref Texture, Texture.Length + 1);
                                Texture[Texture.Length - 1] = (Texture)Textures._Item[j];
                                break;
                            }
                        }
                    }
                    for (int i = 0; i <= Models._Item.Length - 1; i++)
                    {
                        if (Models._Item[i].ID == GC.Model)
                        {
                            Model = (Model)Models._Item[i];
                            break;
                        }
                    }
                    List<Bitmap> BMP = new List<Bitmap>(Texture.Length);
                    for (int i = 0; i < BMP.Count; i++)
                    {
                        BMP[i] = new Bitmap(System.Convert.ToInt32(Texture[i].Width), System.Convert.ToInt32(Texture[i].Height));
                        for (int j = 0; j <= Texture[i].RawData.Length - 1; j++)
                            BMP[i].SetPixel((int)(j % Texture[i].Width), (int)(j / Texture[i].Width), Texture[i].RawData[j]);
                    }
                    ExportModel(Model, BMP, null, ExtractBunch.SelectedPath);
                }
            }
            else if (Obj is GCs)
            {
                if (ExtractBunch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    for (int n = 0; n <= Obj._Item.Length - 1; n++)
                    {
                        Twinsanity.GC GC = (Twinsanity.GC)Obj._Item[n];
                        Texture[] Texture = new Texture[] { };
                        Model Model = null;
                        Materials Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                        Textures Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                        Models Models = (Models)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[2]));
                        Material[] MAT = new Material[] { };
                        for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                        {
                            for (int j = 0; j <= Materials._Item.Length - 1; j++)
                            {
                                if (GC.Material[i] == Materials._Item[j].ID)
                                {
                                    Array.Resize(ref MAT, MAT.Length + 1);
                                    MAT[MAT.Length - 1] = (Material)Materials._Item[j];
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                        {
                            for (int j = 0; j <= Textures._Item.Length - 1; j++)
                            {
                                if (MAT[i].Texture == Textures._Item[j].ID)
                                {
                                    Array.Resize(ref Texture, Texture.Length + 1);
                                    Texture[Texture.Length - 1] = (Texture)Textures._Item[j];
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i <= Models._Item.Length - 1; i++)
                        {
                            if (Models._Item[i].ID == GC.Model)
                            {
                                Model = (Model)Models._Item[i];
                                break;
                            }
                        }
                        List<Bitmap> BMP = new List<Bitmap>(Texture.Length);
                        for (int i = 0; i < BMP.Count; i++)
                        {
                            BMP[i] = new Bitmap(System.Convert.ToInt32(Texture[i].Width), System.Convert.ToInt32(Texture[i].Height));
                            for (int j = 0; j <= Texture[i].RawData.Length - 1; j++)
                                BMP[i].SetPixel((int)(j % Texture[i].Width), (int)(j / Texture[i].Width), Texture[i].RawData[j]);
                        }
                        System.IO.Directory.CreateDirectory(ExtractBunch.SelectedPath + @"\" + n.ToString());
                        ExportModel(Model, BMP, null, ExtractBunch.SelectedPath + @"\" + n.ToString());
                    }
                }
            }
            else if (Obj is Scenery)
            {
                if (ExtractBunch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Scenery SCEN = (Scenery)Obj;
                    GCs GCs = (GCs)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[6]));
                    Terrains Ts = (Terrains)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[7]));
                    List<Twinsanity.GC> SGC = new List<Twinsanity.GC>();
                    List<Scenery.Matrix4> M = new List<Scenery.Matrix4>();
                    List<float> Kf = new List<float>();
                    for (int Si = 0; Si < SCEN.E3.Length; Si++)
                    {
                        for (int Sj = 0; Sj < SCEN.E3[Si].GCCount; Sj++)
                        {
                            for (int Sk = 0; Sk < GCs.Records; Sk++)
                            {
                                if (GCs._Item[Sk].ID == SCEN.E3[Si].GCID[Sj])
                                {
                                    SGC.Add((Twinsanity.GC)GCs._Item[Sk]);
                                    M.Add(SCEN.E3[Si].ChunkMatrix[Sj]);
                                    Kf.Add(1);
                                    break;
                                }
                            }
                        }
                        for (int Sj = 0; Sj < SCEN.E3[Si].SBCount; Sj++)
                        {
                            for (int Sk = 0; Sk < Ts.Records; Sk++)
                            {
                                if (Ts._Item[Sk].ID == SCEN.E3[Si].SBID[Sj])
                                {
                                    Terrain T = (Terrain)Ts._Item[Sk];
                                    for (int Sn = 0; Sn < T.Num; Sn++)
                                    {
                                        for (int Tk = 0; Tk < GCs.Records; Tk++)
                                        {
                                            if (GCs._Item[Tk].ID == T.IDS[3 - Sn])
                                            {
                                                SGC.Add((Twinsanity.GC)GCs._Item[Tk]);
                                                M.Add(SCEN.E3[Si].ChunkMatrix[SCEN.E3[Si].GCCount + Sj]);
                                                Kf.Add(T.K[Sn]);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    int n = 0;
                    HashSet<Texture> TotalTex = new HashSet<Texture>();
                    List<Bitmap> TotalBMP = new List<Bitmap>();
                    List<int> texArray = new List<int>();
                    Model TotalModel = new Model();
                    TotalModel.SubModels = 0;
                    TotalModel.SubModel = new Model._SubModel[] { };
                    foreach (Twinsanity.GC GC in SGC)
                    {
                        Model Model = new Model();
                        Materials Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                        Textures Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                        Models Models = (Models)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[2]));
                        HashSet<Texture> Texture = new HashSet<Texture>();
                        HashSet<Material> MAT = new HashSet<Material>();
                        for (int i = 0; i < GC.MaterialNumber; i++)
                        {
                            for (int j = 0; j < Materials._Item.Length; j++)
                            {
                                if (GC.Material[i] == Materials._Item[j].ID)
                                {
                                    MAT.Add((Material)Materials._Item[j]);
                                    break;
                                }
                            }
                        }
                        List<Material> MatList = new List<Material>(MAT);
                        for (int i = 0; i < MatList.Count; i++)
                        {
                            for (int j = 0; j < Textures._Item.Length; j++)
                            {
                                if (MatList[i].Texture == Textures._Item[j].ID)
                                {
                                    if (!TotalTex.Contains((Texture)Textures._Item[j]))
                                    {
                                        Texture.Add((Texture)Textures._Item[j]);
                                        TotalTex.Add((Texture)Textures._Item[j]);
                                    }
                                    break;
                                }
                            }
                        }
                        List<Texture> TexList = new List<Texture>(Texture);
                        for (int i = 0; i < MatList.Count; i++)
                        {
                            for (int j = 0; j < Textures._Item.Length; j++)
                            {
                                if (MatList[i].Texture == Textures._Item[j].ID)
                                {
                                    texArray.Add(TexList.IndexOf((Texture)Textures._Item[j]) + TotalBMP.Count);
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i < Models._Item.Length; i++)
                        {
                            if (Models._Item[i].ID == GC.Model)
                            {
                                var stream = new System.IO.MemoryStream(((Model)Models._Item[i]).ByteStream.ToArray());
                                Model.Put_Stream(stream, 0, null);
                                break;
                            }
                        }
                        List<Bitmap> BMP = new List<Bitmap>();
                        for (int i = 0; i < TexList.Count; i++)
                        {
                            Bitmap new_bmp = new Bitmap(System.Convert.ToInt32(TexList[i].Width), System.Convert.ToInt32(TexList[i].Height));
                            for (int j = 0; j < TexList[i].RawData.Length; j++)
                                new_bmp.SetPixel((int)(j % TexList[i].Width), (int)(j / TexList[i].Width), TexList[i].RawData[j]);
                            BMP.Add(new_bmp);
                        }
                        OpenTK.Matrix4 MATRIX = new OpenTK.Matrix4(M[n].x1, M[n].y1, M[n].z1, M[n].w1,
                            M[n].x2, M[n].y2, M[n].z2, M[n].w2,
                            M[n].x3, M[n].y3, M[n].z3, M[n].w3,
                            M[n].x4, M[n].y4, M[n].z4, M[n].w4);
                        for (int i = 0; i < Model.SubModels; i++)
                        {
                            for (int j = 0; j < Model.SubModel[i].Group.Length; j++)
                            {
                                OpenTK.Vector4 v;
                                for (int k = 0; k < Model.SubModel[i].Group[j].Vertexes; k++)
                                {
                                    v = new OpenTK.Vector4(Model.SubModel[i].Group[j].Vertex[k].X, Model.SubModel[i].Group[j].Vertex[k].Y, Model.SubModel[i].Group[j].Vertex[k].Z, 1);
                                    v = OpenTK.Vector4.Transform(v,MATRIX);
                                    Model.SubModel[i].Group[j].Vertex[k].X = v.X;
                                    Model.SubModel[i].Group[j].Vertex[k].Y = v.Y;
                                    Model.SubModel[i].Group[j].Vertex[k].Z = v.Z;
                                }
                            }
                        }
                        n++;
                        TotalBMP.AddRange(BMP);
                        int p = TotalModel.SubModels;
                        Array.Resize(ref TotalModel.SubModel, TotalModel.SubModels + Model.SubModels);
                        TotalModel.SubModels += Model.SubModels;
                        Model.SubModel.CopyTo(TotalModel.SubModel, p);
                    }
                    ExportModel(TotalModel, TotalBMP, texArray.ToArray(), ExtractBunch.SelectedPath + @"\");
                }
            }
            else if (Obj is OGI)
            {
                if (ExtractBunch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OGI OGI = (OGI)Obj;
                    GCs GCs = (GCs)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[3]));
                    Twinsanity.GC[] OGC = new Twinsanity.GC[] { };
                    for (int nn = 0; nn <= OGI.GCNumber - 1; nn++)
                    {
                        for (int i = 0; i <= GCs.Records - 1; i++)
                        {
                            if (GCs._Item[i].ID == OGI.GCI[nn].GCID)
                            {
                                Array.Resize(ref OGC, OGC.Length + 1);
                                OGC[OGC.Length - 1] = (Twinsanity.GC)GCs._Item[i];
                            }
                        }
                    }
                    int n = 0;
                    List<Bitmap> TotalBMP = new List<Bitmap>();
                    Model TotalModel = new Model();
                    TotalModel.SubModels = 0;
                    TotalModel.SubModel = new Model._SubModel[] { };
                    foreach (Twinsanity.GC GC in OGC)
                    {
                        Texture[] Texture = new Texture[] { };
                        Model Model = new Model();
                        Materials Materials = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                        Textures Textures = (Textures)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                        Models Models = (Models)fileData.Get_Item(CalculateIndexes(TreeView1.Nodes[0].Nodes[0].Nodes[2]));
                        Material[] MAT = new Material[] { };
                        for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                        {
                            for (int j = 0; j <= Materials._Item.Length - 1; j++)
                            {
                                if (GC.Material[i] == Materials._Item[j].ID)
                                {
                                    Array.Resize(ref MAT, MAT.Length + 1);
                                    MAT[MAT.Length - 1] = (Material)Materials._Item[j];
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                        {
                            for (int j = 0; j <= Textures._Item.Length - 1; j++)
                            {
                                if (MAT[i].Texture == Textures._Item[j].ID)
                                {
                                    Array.Resize(ref Texture, Texture.Length + 1);
                                    Texture[Texture.Length - 1] = (Texture)Textures._Item[j];
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i <= Models._Item.Length - 1; i++)
                        {
                            if (Models._Item[i].ID == GC.Model)
                            {
                                var stream = new System.IO.MemoryStream(((Model)Models._Item[i]).ByteStream.ToArray());
                                Model.Put_Stream(stream, 0, null);
                                break;
                            }
                        }
                        List<Bitmap> BMP = new List<Bitmap>(Texture.Length);
                        for (int i = 0; i < BMP.Count; i++)
                        {
                            BMP[i] = new Bitmap(System.Convert.ToInt32(Texture[i].Width), System.Convert.ToInt32(Texture[i].Height));
                            for (int j = 0; j < Texture[i].RawData.Length; j++)
                                BMP[i].SetPixel((int)(j % Texture[i].Width), (int)(j / Texture[i].Width), Texture[i].RawData[j]);
                        }
                        OpenTK.Matrix4 MATRIX = new OpenTK.Matrix4();
                        int gcid = 0;
                        for (int i = 0; i <= OGI.GCNumber - 1; i++)
                        {
                            if (OGI.GCI[i].GCID == GC.ID)
                                gcid = i + 1;
                        }
                        MATRIX.M11 = OGI.T3[gcid].Coordinate1.X;
                        MATRIX.M12 = OGI.T3[gcid].Coordinate1.Y;
                        MATRIX.M13 = OGI.T3[gcid].Coordinate1.Z;
                        MATRIX.M14 = OGI.T3[gcid].Coordinate1.W;
                        MATRIX.M21 = OGI.T3[gcid].Coordinate2.X;
                        MATRIX.M22 = OGI.T3[gcid].Coordinate2.Y;
                        MATRIX.M23 = OGI.T3[gcid].Coordinate2.Z;
                        MATRIX.M24 = OGI.T3[gcid].Coordinate2.W;
                        MATRIX.M31 = OGI.T3[gcid].Coordinate3.X;
                        MATRIX.M32 = OGI.T3[gcid].Coordinate3.Y;
                        MATRIX.M33 = OGI.T3[gcid].Coordinate3.Z;
                        MATRIX.M34 = OGI.T3[gcid].Coordinate3.W;
                        MATRIX.M41 = OGI.T3[gcid].Coordinate4.X;
                        MATRIX.M42 = OGI.T3[gcid].Coordinate4.Y;
                        MATRIX.M43 = OGI.T3[gcid].Coordinate4.Z;
                        MATRIX.M44 = OGI.T3[gcid].Coordinate4.W;
                        for (int i = 0; i <= Model.SubModels - 1; i++)
                        {
                            for (int j = 0; j <= Model.SubModel[i].Group.Length - 1; j++)
                            {
                                OpenTK.Vector4 v;
                                for (int k = 0; k <= Model.SubModel[i].Group[j].Vertexes - 1; k++)
                                {
                                    v = new OpenTK.Vector4(Model.SubModel[i].Group[j].Vertex[k].X, Model.SubModel[i].Group[j].Vertex[k].Y, Model.SubModel[i].Group[j].Vertex[k].Z, 1);
                                    v = OpenTK.Vector4.Transform(v,MATRIX);
                                    Model.SubModel[i].Group[j].Vertex[k].X = v.X;
                                    Model.SubModel[i].Group[j].Vertex[k].Y = v.Y;
                                    Model.SubModel[i].Group[j].Vertex[k].Z = v.Z;
                                }
                            }
                        }
                        n += 1;
                        TotalBMP.AddRange(BMP);
                        int p = TotalModel.SubModels;
                        Array.Resize(ref TotalModel.SubModel, TotalModel.SubModels + Model.SubModels);
                        TotalModel.SubModels += Model.SubModels;
                        Model.SubModel.CopyTo(TotalModel.SubModel, p);
                    }
                    ExportModel(TotalModel, TotalBMP, null, ExtractBunch.SelectedPath + @"\");
                }
            }
        }
        private new void ExportModel(Model MDL, string Path)
        {
            System.IO.FileStream ObjFile = new System.IO.FileStream(Path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter ObjWriter = new System.IO.StreamWriter(ObjFile);
            ObjWriter.WriteLine("# Model extracted with Twinsanity Editor by Neo_Kesha");
            ObjWriter.WriteLine("# Crash Twinsanity made by Travellers Tales");
            uint shift = 0;
            for (int i = 0; i <= MDL.SubModels - 1; i++)
            {
                ObjWriter.WriteLine("o SubModel" + i.ToString());
                for (int j = 0; j <= MDL.SubModel[i].Group.Length - 1; j++)
                {
                    // ObjWriter.WriteLine("g Group" + j.ToString)
                    for (int k = 0; k <= MDL.SubModel[i].Group[j].Vertexes - 1; k++)
                    {
                        {
                            var withBlock = MDL.SubModel[i].Group[j];
                            ObjWriter.WriteLine("v " + withBlock.Vertex[k].X.ToString() + " " + withBlock.Vertex[k].Y.ToString() + " " + withBlock.Vertex[k].Z.ToString());
                            if (withBlock.Weight.Length > 0)
                                ObjWriter.WriteLine("vt " + (1 - withBlock.Weight[k].X).ToString() + " " + withBlock.Weight[k].Y.ToString() + " " + withBlock.Weight[k].Z.ToString());
                            else
                                ObjWriter.WriteLine("vt 0 0 0");
                            if (withBlock.UV.Length > 0)
                                ObjWriter.WriteLine("vn " + withBlock.UV[k].X.ToString() + " " + withBlock.UV[k].Y.ToString() + " " + withBlock.UV[k].Z.ToString());
                            else
                                ObjWriter.WriteLine("vn 0 0 0");
                        }
                    }
                    for (int k = 0; k <= MDL.SubModel[i].Group[j].Vertexes - 3; k++)
                    {
                        {
                            var withBlock = MDL.SubModel[i].Group[j];
                            if (!(withBlock.Weight[k + 2].CONN == 128))
                            {
                                string v1 = (k + 1 + shift).ToString() + "/" + (k + 1 + shift).ToString() + "/" + (k + 1 + shift).ToString();
                                string v2 = (k + 2 + shift).ToString() + "/" + (k + 2 + shift).ToString() + "/" + (k + 2 + shift).ToString();
                                string v3 = (k + 3 + shift).ToString() + "/" + (k + 3 + shift).ToString() + "/" + (k + 3 + shift).ToString();
                                ObjWriter.WriteLine("f " + v1 + " " + v2 + " " + v3);
                            }
                        }
                    }
                    shift += MDL.SubModel[i].Group[j].Vertexes;
                }
            }
            ObjWriter.Close();
            ObjFile.Close();
        }
        private new void ExportModel(Model MDL, List<Bitmap> TEX, int[] TEX_index, string Path)
        {
            System.IO.FileStream ObjFile = new System.IO.FileStream(Path + @"\mdl.obj", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter ObjWriter = new System.IO.StreamWriter(ObjFile);
            System.IO.FileStream MTLFile = new System.IO.FileStream(Path + @"\mtl.mtl", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter MTLWriter = new System.IO.StreamWriter(MTLFile);
            for (int i = 0; i < TEX.Count; i++)
            {
                MTLWriter.WriteLine("newmtl mtl" + i.ToString());
                MTLWriter.WriteLine("map_Kd mtl" + i.ToString() + ".png");
                MTLWriter.WriteLine("map_d mtl" + i.ToString() + ".png");
                TEX[i].Save(Path + @"\mtl" + i.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
            ObjWriter.WriteLine("# Model extracted with Twinsanity Editor by Neo_Kesha");
            ObjWriter.WriteLine("# Crash Twinsanity made by Travellers Tales");
            uint shift = 0;
            if (TEX.Count > 0)
                ObjWriter.WriteLine("mtllib mtl.mtl");
            for (int i = 0; i < MDL.SubModels; i++)
            {
                ObjWriter.WriteLine("o SubModel" + i.ToString());
                if (TEX.Count > 0)
                    ObjWriter.WriteLine("usemtl mtl" + TEX_index[i].ToString());
                for (int j = 0; j < MDL.SubModel[i].Group.Length; j++)
                {
                    // ObjWriter.WriteLine("g Group" + j.ToString)
                    for (int k = 0; k < MDL.SubModel[i].Group[j].Vertexes; k++)
                    {
                        {
                            var withBlock = MDL.SubModel[i].Group[j];
                            ObjWriter.WriteLine("v " + (-withBlock.Vertex[k].X).ToString() + " " + withBlock.Vertex[k].Y.ToString() + " " + withBlock.Vertex[k].Z.ToString());
                            if (withBlock.Weight.Length > 0)
                                ObjWriter.WriteLine("vt " + (withBlock.Weight[k].X).ToString() + " " + (withBlock.Weight[k].Y).ToString() + " " + withBlock.Weight[k].Z.ToString());
                            else
                                ObjWriter.WriteLine("vt 0 0 0");
                            if (withBlock.UV.Length > 0)
                                ObjWriter.WriteLine("vn " + withBlock.UV[k].X.ToString() + " " + withBlock.UV[k].Y.ToString() + " " + withBlock.UV[k].Z.ToString());
                            else
                                ObjWriter.WriteLine("vn 0 0 0");
                        }
                    }
                    for (int k = 0; k < MDL.SubModel[i].Group[j].Vertexes - 2; k++)
                    {
                        {
                            var withBlock = MDL.SubModel[i].Group[j];
                            if (!(withBlock.Weight[k + 2].CONN == 128))
                            {
                                string v1 = (k + 1 + shift).ToString() + "/" + (k + 1 + shift).ToString() + "/" + (k + 1 + shift).ToString();
                                string v2 = (k + 2 + shift).ToString() + "/" + (k + 2 + shift).ToString() + "/" + (k + 2 + shift).ToString();
                                string v3 = (k + 3 + shift).ToString() + "/" + (k + 3 + shift).ToString() + "/" + (k + 3 + shift).ToString();
                                ObjWriter.WriteLine("f " + v1 + " " + v2 + " " + v3);
                            }
                        }
                    }
                    shift += MDL.SubModel[i].Group[j].Vertexes;
                }
            }
            ObjWriter.Close();
            ObjFile.Close();
            MTLWriter.Close();
            MTLFile.Close();
        }

        private void ImportModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenObj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream Obj = new System.IO.FileStream(OpenObj.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader ObjReader = new System.IO.StreamReader(Obj);
                ObjSubmodel[] SubModels = new ObjSubmodel[] { };
                Twinsanity.Model.RawData[][] RawData = new Model.RawData[][] { };
                Model Model = new Model();
                while (!ObjReader.EndOfStream)
                {
                    string[] str = ObjReader.ReadLine().Split(' ');
                    if (str[0] == "o")
                    {
                        Array.Resize(ref SubModels, SubModels.Length + 1);
                        Array.Resize(ref SubModels[SubModels.Length - 1].Face, 0);
                        Array.Resize(ref SubModels[SubModels.Length - 1].VertexData, 0);
                        Array.Resize(ref SubModels[SubModels.Length - 1].UVWData, 0);
                        Array.Resize(ref SubModels[SubModels.Length - 1].NormalData, 0);
                    }
                    else if (SubModels.Length > 0)
                    {
                        switch (str[0])
                        {
                            case "v":
                                {
                                    float x = float.Parse(str[1].Replace(".", ","));
                                    float y = float.Parse(str[2].Replace(".", ","));
                                    float z = float.Parse(str[3].Replace(".", ","));
                                    Array.Resize(ref SubModels[SubModels.Length - 1].VertexData, SubModels[SubModels.Length - 1].VertexData.Length + 1);
                                    SubModels[SubModels.Length - 1].VertexData[SubModels[SubModels.Length - 1].VertexData.Length - 1] = new OpenTK.Vector3(x, y, z);
                                    break;
                                }

                            case "vt":
                                {
                                    float u = float.Parse(str[1].Replace(".", ","));
                                    float v = float.Parse(str[2].Replace(".", ","));
                                    float w = str.Length > 3 ? float.Parse(str[3].Replace(".", ",")) : 0.0F;
                                    Array.Resize(ref SubModels[SubModels.Length - 1].UVWData, SubModels[SubModels.Length - 1].UVWData.Length + 1);
                                    SubModels[SubModels.Length - 1].UVWData[SubModels[SubModels.Length - 1].UVWData.Length - 1] = new OpenTK.Vector3(u, v, w);
                                    break;
                                }

                            case "vn":
                                {
                                    float Nx = float.Parse(str[1].Replace(".", ","));
                                    float Ny = float.Parse(str[2].Replace(".", ","));
                                    float Nz = float.Parse(str[3].Replace(".", ","));
                                    Array.Resize(ref SubModels[SubModels.Length - 1].NormalData, SubModels[SubModels.Length - 1].NormalData.Length + 1);
                                    SubModels[SubModels.Length - 1].NormalData[SubModels[SubModels.Length - 1].NormalData.Length - 1] = new OpenTK.Vector3(Nx, Ny, Nz);
                                    break;
                                }

                            case "f":
                                {
                                    uint i1v = uint.Parse(str[1].Split('/')[0]);
                                    uint i2v = uint.Parse(str[2].Split('/')[0]);
                                    uint i3v = uint.Parse(str[3].Split('/')[0]);
                                    uint i1t = uint.Parse(str[1].Split('/')[1]);
                                    uint i2t = uint.Parse(str[2].Split('/')[1]);
                                    uint i3t = uint.Parse(str[3].Split('/')[1]);
                                    uint i1n = uint.Parse(str[1].Split('/')[2]);
                                    uint i2n = uint.Parse(str[2].Split('/')[2]);
                                    uint i3n = uint.Parse(str[3].Split('/')[2]);
                                    // Array.Resize(SubModels(SubModels.Length - 1).FacesV, SubModels(SubModels.Length - 1).FacesV.Length + 1)
                                    // SubModels(SubModels.Length - 1).FacesV(SubModels(SubModels.Length - 1).FacesV.Length - 1) = New Microsoft.DirectX.Vector3(i1v, i2v, i3v)
                                    // Array.Resize(SubModels(SubModels.Length - 1).FacesT, SubModels(SubModels.Length - 1).FacesT.Length + 1)
                                    // SubModels(SubModels.Length - 1).FacesT(SubModels(SubModels.Length - 1).FacesT.Length - 1) = New Microsoft.DirectX.Vector3(i1t, i2t, i3t)
                                    // Array.Resize(SubModels(SubModels.Length - 1).FacesN, SubModels(SubModels.Length - 1).FacesN.Length + 1)
                                    // SubModels(SubModels.Length - 1).FacesN(SubModels(SubModels.Length - 1).FacesN.Length - 1) = New Microsoft.DirectX.Vector3(i1n, i2n, i3n)
                                    Array.Resize(ref SubModels[SubModels.Length - 1].Face, SubModels[SubModels.Length - 1].Face.Length + 3);
                                    SubModels[SubModels.Length - 1].Face[SubModels[SubModels.Length - 1].Face.Length - 3] = new OpenTK.Vector3(i1v, i1t, i1n);
                                    SubModels[SubModels.Length - 1].Face[SubModels[SubModels.Length - 1].Face.Length - 2] = new OpenTK.Vector3(i2v, i2t, i2n);
                                    SubModels[SubModels.Length - 1].Face[SubModels[SubModels.Length - 1].Face.Length - 1] = new OpenTK.Vector3(i3v, i3t, i3n);
                                    break;
                                }
                        }
                    }
                }
                ObjReader.Close();
                Obj.Close();
                Array.Resize(ref RawData, SubModels.Length);
                int shift = 0;
                int peek = 1;
                int normal_shift = 0;
                bool CW = true;
                for (int i = 0; i <= SubModels.Length - 1; i++)
                {
                    Array.Resize(ref RawData[i], 0);
                    bool start = true;
                    byte side = 0;
                    int j = 0;
                    int a, b, c;
                    OpenTK.Vector3 center = calc_center(SubModels[i].VertexData);
                    while (SubModels[i].Face.Length > 0)
                    {
                        if (start)
                        {
                            add_vertex(j + 2, ref SubModels[i], ref RawData[i], false, shift, normal_shift);
                            add_vertex(j + 1, ref SubModels[i], ref RawData[i], false, shift, normal_shift);
                            add_vertex(j, ref SubModels[i], ref RawData[i], true, shift, normal_shift);
                        }
                        else
                            add_vertex(j + peek, ref SubModels[i], ref RawData[i], true, shift, normal_shift);
                        // If is_CW(RawData(i).Length - 3, RawData(i), center) = CW Then
                        // swap_vertex(RawData(i))
                        // End If
                        // CW = Not CW
                        a = (int)SubModels[i].Face[j].X;
                        b = (int)SubModels[i].Face[j + 1].X;
                        c = (int)SubModels[i].Face[j + 2].X;
                        del_face(j, ref SubModels[i].Face);
                        j = find_face(a, b, c, ref SubModels[i].Face, ref peek);
                        if (j == -1)
                        {
                            peek = 1;
                            start = true;
                            j = 0;
                            side = 0;
                        }
                        else
                            start = false;
                    }
                    shift += SubModels[i].VertexData.Length;
                    normal_shift += SubModels[i].NormalData.Length;
                }
                Model.Import(RawData);
                System.IO.FileStream File = new System.IO.FileStream(OpenObj.FileName.Remove(OpenObj.FileName.Length - 4), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.BinaryWriter FileWriter = new System.IO.BinaryWriter(File);
                FileWriter.Write(Model.ByteStream.ToArray());
                FileWriter.Close();
                File.Close();
            }
        }
        private bool is_CW(int j, Model.RawData[] RawData, OpenTK.Vector3 center)
        {
            OpenTK.Vector3 v, u, w, d;
            v = new OpenTK.Vector3(RawData[j + 1].X - RawData[j].X, RawData[j + 1].Y - RawData[j].Y, RawData[j + 1].Z - RawData[j].Z);
            u = new OpenTK.Vector3(RawData[j + 2].X - RawData[j + 1].X, RawData[j + 2].Y - RawData[j + 1].Y, RawData[j + 2].Z - RawData[j + 1].Z);
            w = OpenTK.Vector3.Cross(v, u);
            OpenTK.Vector3[] vert = new OpenTK.Vector3[3];
            vert[0] = new OpenTK.Vector3(RawData[j].X, RawData[j].Y, RawData[j].Z);
            vert[1] = new OpenTK.Vector3(RawData[j].X, RawData[j].Y, RawData[j].Z);
            vert[2] = new OpenTK.Vector3(RawData[j].X, RawData[j].Y, RawData[j].Z);
            d = calc_center(vert);
            d -= center;
            if ((w.X * d.X + w.Y * d.Y + w.Z * d.Z) >= 0)
                return true;
            else
                return false;
        }

        private OpenTK.Vector3 calc_center(OpenTK.Vector3[] VertexData)
        {
            OpenTK.Vector3 v = new OpenTK.Vector3(0, 0, 0);
            for (int i = 0; i <= VertexData.Length - 1; i++)
                v += VertexData[i];
            v = OpenTK.Vector3.Multiply(v,(float)(1 / (double)VertexData.Length));
            return v;
        }
        private void swap_vertex(ref Model.RawData[] RawData)
        {
            Model.RawData t;
            t = RawData[RawData.Length - 1];
            RawData[RawData.Length - 1] = RawData[RawData.Length - 2];
            RawData[RawData.Length - 2] = t;
        }
        private void add_vertex(int i, ref ObjSubmodel SubModel, ref Model.RawData[] RawData, bool conn, int shift, int normal_shift)
        {
            Array.Resize(ref RawData, RawData.Length + 1);
            RawData[RawData.Length - 1].X = SubModel.VertexData[System.Convert.ToInt32(SubModel.Face[i].X) - 1 - shift].X;
            RawData[RawData.Length - 1].Y = SubModel.VertexData[System.Convert.ToInt32(SubModel.Face[i].X) - 1 - shift].Y;
            RawData[RawData.Length - 1].Z = SubModel.VertexData[System.Convert.ToInt32(SubModel.Face[i].X) - 1 - shift].Z;
            RawData[RawData.Length - 1].U = SubModel.UVWData[System.Convert.ToInt32(SubModel.Face[i].Y) - 1 - shift].X;
            RawData[RawData.Length - 1].V = SubModel.UVWData[System.Convert.ToInt32(SubModel.Face[i].Y) - 1 - shift].Y;
            RawData[RawData.Length - 1].W = 1;
            RawData[RawData.Length - 1].Nx = SubModel.NormalData[System.Convert.ToInt32(SubModel.Face[i].Z) - 1 - normal_shift].X;
            RawData[RawData.Length - 1].Ny = SubModel.NormalData[System.Convert.ToInt32(SubModel.Face[i].Z) - 1 - normal_shift].Y;
            RawData[RawData.Length - 1].Nz = SubModel.NormalData[System.Convert.ToInt32(SubModel.Face[i].Z) - 1 - normal_shift].Z;
            RawData[RawData.Length - 1].CONN = conn;
            RawData[RawData.Length - 1].Emission = 2004318071;
        }
        private int find_face(int a, int b, int c, ref OpenTK.Vector3[] Face, ref int peek)
        {
            for (int i = 0; i <= Face.Length - 1; i += 3)
            {
                if ((Face[i].X == a & Face[i + 2].X == b) | (Face[i].X == b & Face[i + 2].X == a))
                {
                    peek = 1;
                    return i;
                }
                else if ((Face[i].X == a & Face[i + 1].X == c))
                {
                    peek = 2;
                    return i;
                }
                else if ((Face[i + 1].X == a & Face[i + 2].X == c))
                {
                    peek = 0;
                    return i;
                }
            }
            return -1;
        }
        private int find_face_rev(int a, int b, ref OpenTK.Vector3[] Face, int side)
        {
            for (int i = 0; i <= Face.Length - 1; i += 3)
            {
                if (side == 0)
                {
                    if (Face[i].X == a & Face[i + 1].X == b)
                        return i;
                }
                else if (Face[i].X == a & Face[i + 2].X == b)
                    return i;
            }
            return -1;
        }
        private void del_face(int i, ref OpenTK.Vector3[] Face)
        {
            for (int j = i + 3; j <= Face.Length - 1; j++)
                Face[j - 3] = Face[j];
            Array.Resize(ref Face, Face.Length - 3);
        }
        private struct ObjSubmodel
        {
            public OpenTK.Vector3[] VertexData;
            public OpenTK.Vector3[] UVWData;
            public OpenTK.Vector3[] NormalData;
            public OpenTK.Vector3[] Face;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            BaseItem Obj = (BaseItem)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
            if (Obj is Instance)
            {
                Instances INSTS = (Instances)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editInstance.Hide();
                _editInstance.UpdateTree(ref INSTS, TreeView1.SelectedNode.Parent.Parent.Index);
                _editInstance.UpdateInstance(TreeView1.SelectedNode.Index);
                _editInstance.InstanceTree.SelectedNode = _editInstance.InstanceTree.Nodes[TreeView1.SelectedNode.Index];
                _editInstance.Show();
            }
            else if (Obj is Trigger)
            {
                Triggers TRIGS = (Triggers)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editTrigger.Hide();
                _editTrigger.UpdateTree(ref TRIGS, TreeView1.SelectedNode.Parent.Parent.Index);
                _editTrigger.UpdateTrigger(TreeView1.SelectedNode.Index);
                _editTrigger.TriggerTree.SelectedNode = _editTrigger.TriggerTree.Nodes[TreeView1.SelectedNode.Index];
                _editTrigger.Show();
            }
            else if (Obj is Twinsanity.GC)
            {
                GCs GCS = (GCs)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editGC.Hide();
                _editGC.UpdateTree(ref GCS);
                _editGC.UpdateGC(TreeView1.SelectedNode.Index);
                _editGC.GCTree.SelectedNode = _editGC.GCTree.Nodes[TreeView1.SelectedNode.Index];
                _editGC.Show();
            }
            else if (Obj is Position)
            {
                Positions POSs = (Positions)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editPosition.Hide();
                _editPosition.UpdateTree(ref POSs, (uint)TreeView1.SelectedNode.Parent.Parent.Index);
                _editPosition.UpdatePos(TreeView1.SelectedNode.Index);
                _editPosition.PosTree.SelectedNode = _editPosition.PosTree.Nodes[TreeView1.SelectedNode.Index];
                _editPosition.Show();
            }
            else if (Obj is Path)
            {
                Paths PATHs = (Paths)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editPath.Hide();
                _editPath.UpdateTree(ref PATHs, (uint)TreeView1.SelectedNode.Parent.Parent.Index);
                _editPath.Pathes.SelectedNode = _editPath.Pathes.Nodes[TreeView1.SelectedNode.Index];
                _editPath.UpdatePath(TreeView1.SelectedNode.Index);
                _editPath.Show();
            }
            else if (Obj is FuckingShit)
            {
                FuckingShits FSs = (FuckingShits)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editFuckingShit.Hide();
                _editFuckingShit.UpdateTree(ref FSs, (uint)TreeView1.SelectedNode.Parent.Parent.Index);
                _editFuckingShit.UpdateFS(TreeView1.SelectedNode.Index);
                _editFuckingShit.FSTree.SelectedNode = _editFuckingShit.FSTree.Nodes[TreeView1.SelectedNode.Index];
                _editFuckingShit.Show();
            }
            else if (Obj is Behavior)
            {
                Behaviors BHs = (Behaviors)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editBehavior.Hide();
                _editBehavior.UpdateTree(ref BHs, (uint)TreeView1.SelectedNode.Parent.Parent.Index);
                _editBehavior.UpdateBeh(TreeView1.SelectedNode.Index);
                _editBehavior.BehTree.SelectedNode = _editBehavior.BehTree.Nodes[TreeView1.SelectedNode.Index];
                _editBehavior.Show();
            }
            else if (Obj is SurfaceBehaviour)
            {
                SurfaceBehaviours SBs = (SurfaceBehaviours)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editSurfBehavior.Hide();
                _editSurfBehavior.UpdateTree(ref SBs, (uint)TreeView1.SelectedNode.Parent.Parent.Index);
                _editSurfBehavior.UpdateSB(TreeView1.SelectedNode.Index);
                _editSurfBehavior.SBTree.SelectedNode = _editSurfBehavior.SBTree.Nodes[TreeView1.SelectedNode.Index];
                _editSurfBehavior.Show();
            }
            else if (Obj is GameObject)
            {
                GameObjects GOs = (GameObjects)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editGameObject.Hide();
                _editGameObject.UpdateTree(ref GOs, TreeView1.SelectedNode.Parent.Parent.Index);
                _editGameObject.UpdateGO(TreeView1.SelectedNode.Index);
                _editGameObject.GOTree.SelectedNode = _editGameObject.GOTree.Nodes[TreeView1.SelectedNode.Index];
                _editGameObject.Show();
            }
            else if (Obj is OGI)
            {
                OGIs OGIs = (OGIs)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editOGI.Hide();
                _editOGI.UpdateTree(ref OGIs, TreeView1.SelectedNode.Parent.Parent.Index);
                _editOGI.UpdateOGI(TreeView1.SelectedNode.Index);
                _editOGI.OGITree.SelectedNode = _editOGI.OGITree.Nodes[TreeView1.SelectedNode.Index];
                _editOGI.Show();
            }
            else if (Obj is Material)
            {
                Materials Mtls = (Materials)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editMaterial.Hide();
                _editMaterial.UpdateTree(ref Mtls);
                _editMaterial.UpdateMtl(TreeView1.SelectedNode.Index);
                _editMaterial.MtlTree.SelectedNode = _editMaterial.MtlTree.Nodes[TreeView1.SelectedNode.Index];
                _editMaterial.Show();
            }
            else if (Obj is Terrain)
            {
                Terrains Ts = (Terrains)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode.Parent));
                _editTerrain.Hide();
                _editTerrain.UpdateTree(ref Ts);
                _editTerrain.UpdatePos(TreeView1.SelectedNode.Index);
                _editTerrain.TerTree.SelectedNode = _editTerrain.TerTree.Nodes[TreeView1.SelectedNode.Index];
                _editTerrain.Show();
            }
            else if (Obj is ID4Model)
            {
                ID4Model ID4 = (ID4Model)fileData.Get_Item(CalculateIndexes(TreeView1.SelectedNode));
                System.IO.FileStream File = new System.IO.FileStream(@"C:\mdl.obj", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.StreamWriter Writer = new System.IO.StreamWriter(File);
                int cnt = 0;
                for (int i = 0; i <= ID4.SubModels - 1; i++)
                {
                    for (int j = 0; j <= ID4.SubModel[i].Group.Length - 1; j++)
                    {
                        for (int k = 0; k <= ID4.SubModel[i].Group[j].Struct3.Length - 1; k++)
                        {
                            float a, b, c, d;
                            a = (ID4.SubModel[i].Group[j].Struct3[k].ID1 >> 16);
                            b = (ID4.SubModel[i].Group[j].Struct3[k].ID1 % 65536);
                            c = (ID4.SubModel[i].Group[j].Struct3[k].ID2 >> 16);
                            d = (ID4.SubModel[i].Group[j].Struct3[k].ID2 % 65536);
                            Writer.WriteLine("v " + Decode((ushort)a).ToString() + " " + Decode((ushort)b).ToString() + " " + Decode((ushort)c).ToString() + " " + Decode((ushort)d).ToString());
                            Writer.WriteLine("f " + cnt.ToString());
                            cnt += 1;
                        }
                    }
                }
                Writer.Flush();
                Writer.Close();
            }
        }
        public double Decode(UInt16 value)
        {
            uint cnt = (uint)(value >> 12);
            double result = value + 0xFFF;
            while (cnt > 0)
            {
                result /= 10.0;
                cnt -= 1;
            }
            return result;
        }
    }
}
