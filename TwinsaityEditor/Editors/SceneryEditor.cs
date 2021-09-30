namespace TwinsaityEditor
{
    public partial class SceneryEditor
    {
        private TwinsanityEditorForm twinsanityEditorForm;

        public SceneryEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public void Update()
        {
            Twinsanity.Scenery SCEN = (Twinsanity.Scenery)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[1]));
        }
    }
}
