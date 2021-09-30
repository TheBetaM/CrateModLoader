using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public static class ControlExt
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(this Control c)
        {
            SendMessage(c.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(this Control c)
        {
            SendMessage(c.Handle, WM_SETREDRAW, true, 0);
            c.Refresh();
        }
    }
}
