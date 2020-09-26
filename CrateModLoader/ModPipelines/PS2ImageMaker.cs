using System;
using System.Runtime.InteropServices;

namespace CrateModLoader.Tools
{
    // Based on PS2ImageMaker by Smartkin (https://github.com/Smartkin/PS2ImageMaker)
    class PS2ImageMaker
    {
        public unsafe static Progress StartPacking(string gamePath, string imagePathName)
        {
            var ptr = start_packing(gamePath, imagePathName);
            ProgressC progress = new ProgressC();
            progress = (ProgressC)Marshal.PtrToStructure(ptr, typeof(ProgressC));
            Progress prog = new Progress
            {
                Finished = progress.finished != 0,
                NewFile = progress.new_file != 0,
                NewState = progress.new_state != 0,
                ProgressS = progress.state,
                ProgressPercentage = progress.progress,
                File = progress.file_name
            };
            return prog;
        }

        public unsafe static Progress PollProgress()
        {
            var ptr = poll_progress();
            ProgressC progress = new ProgressC();
            progress = (ProgressC)Marshal.PtrToStructure(ptr, typeof(ProgressC));
            Progress prog = new Progress
            {
                Finished = progress.finished != 0,
                NewFile = progress.new_file != 0,
                NewState = progress.new_state != 0,
                ProgressS = progress.state,
                ProgressPercentage = progress.progress,
                File = progress.file_name
            };
            return prog;
        }

        public enum ProgressState
        {
            FAILED = -1,
            ENUM_FILES,
            WRITE_SECTORS,
            WRITE_FILES,
            WRITE_END,
            FINISHED,
        }

        public class Progress
        {
            public string File;
            public ProgressState ProgressS;
            public float ProgressPercentage;
            public bool Finished;
            public bool NewState;
            public bool NewFile;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        unsafe struct ProgressC
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string file_name;
            public int size;
            public ProgressState state;
            public float progress;
            public byte finished;
            public byte new_state;
            public byte new_file;
        }

        [DllImport("Tools/PS2ImageMaker", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private unsafe static extern IntPtr start_packing([MarshalAs(UnmanagedType.LPStr)] string game_path, [MarshalAs(UnmanagedType.LPStr)] string dest_path);
        [DllImport("Tools/PS2ImageMaker", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern IntPtr poll_progress();
    }
}
