using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TwinsaityEditor.Handlers
{
    public static class DisposeHandler
    {
        public static event EventHandler OnControlDisposal;

        public static void AddObject(ref Control control)
        {
            _controlList.Add(control);
            control.Disposed += Control_Disposed;
        }

        private static void Control_Disposed(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            _controlList.Remove(ctrl);
            if (OnControlDisposal != null)
            {
                EventArgs args = new EventArgs();
                OnControlDisposal(null, args);
            }
        }

        public static void AddObjects(params Control[] controls)
        {
            for(var i = 0; i < controls.Length; ++i)
            {
                _controlList.Add(controls[i]);
                controls[i].Disposed += Control_Disposed;
            }
        }


        public static void Clear()
        {
            _controlList.RemoveAll((ctrl) => { return true; });
        }





        private static List<Control> _controlList = new List<Control>();
    }
}
