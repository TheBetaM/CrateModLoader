namespace CrateModLoader
{
    public class ModOption
    {
        // defaults to not enabled
        public ModOption(string name,bool enabled = false)
        {
            Name = name;
            Enabled = enabled;
        }

        public string Name { get; }
        public bool Enabled { get; set; }

        public override string ToString() => Name;
    }
}
