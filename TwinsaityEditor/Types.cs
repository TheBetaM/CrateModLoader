using OpenTK;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TwinsaityEditor
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        private Vector3 pos, nor;
        private Vector2 tex;
        private uint col;

        public Vertex(Vector3 pos)
        {
            this.pos = pos;
            col = 0;
            nor = new Vector3();
            tex = new Vector2();
        }

        public Vertex(Vector3 pos, Color col)
        {
            this.pos = pos;
            this.col = ColorToABGR(col);
            nor = new Vector3();
            tex = new Vector2();
        }

        public Vertex(Vector3 pos, Vector3 nor, Color col)
        {
            this.pos = pos;
            this.nor = nor;
            this.col = ColorToABGR(col);
            tex = new Vector2();
        }

        public Vertex(Vector3 pos, Vector2 tex, Color col)
        {
            this.pos = pos;
            this.tex = tex;
            this.col = ColorToABGR(col);
            nor = new Vector3();
        }

        public Vertex(Vector3 pos, Vector3 nor, Vector2 tex, Color col)
        {
            this.pos = pos;
            this.nor = nor;
            this.tex = tex;
            this.col = ColorToABGR(col);
        }

        public static uint ColorToABGR(Color col)
        {
            return col.R | (uint)col.G << 8 | (uint)col.B << 16 | (uint)col.A << 24;
        }

        public static int SizeOf { get; } = Marshal.SizeOf(typeof(Vertex));
        public static System.IntPtr OffsetOfPos { get; } = Marshal.OffsetOf(typeof(Vertex), "pos");
        public static System.IntPtr OffsetOfNor { get; } = Marshal.OffsetOf(typeof(Vertex), "nor");
        public static System.IntPtr OffsetOfTex { get; } = Marshal.OffsetOf(typeof(Vertex), "tex");
        public static System.IntPtr OffsetOfCol { get; } = Marshal.OffsetOf(typeof(Vertex), "col");

        public Vector3 Pos { get => pos; set => pos = value; }
        public Vector3 Nor { get => nor; set => nor = value; }
        public Vector2 Tex { get => tex; set => tex = value; }
        public uint Col { get => col; set => col = value; }
    }

    public enum TextAnchor { TopLeft, TopMiddle, TopRight, BotLeft, BotMiddle, BotRight };
    public enum Editors
    {
        ColData, //<-- not an actual editor!
        ChunkLinks,
        Position,
        Path,
        Instance,
        Trigger,
        Script,
        Object,
        Animation,
        InstanceMB,
    };
}
