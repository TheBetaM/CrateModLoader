using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public delegate void ControllerAddMenuDelegate();

    public abstract class Controller
    {
        protected MainForm TopForm { get; private set; }
        public bool Selected { get; set; }
        public string[] TextPrev { get; set; }
        public TreeNode Node { get; set; }
        public ContextMenu ContextMenu { get; set; }
        
        public Controller(MainForm topform)
        {
            TopForm = topform;
            ContextMenu = new ContextMenu();
            Node = new TreeNode { Tag = this, ContextMenu = ContextMenu };
        }

        public void AddNode(Controller controller)
        {
            Node.Nodes.Add(controller.Node);
        }

        protected void AddMenu(string text, ControllerAddMenuDelegate func)
        {
            void handler(object sender, EventArgs e)
            {
                func();
            }
            ContextMenu.MenuItems.Add(text, handler);
        }

        protected abstract string GetName();
        protected abstract void GenText();

        public void UpdateText()
        {
            GenText();
            Node.Text = GetName();
            if (Selected)
                TopForm.ControllerNodeSelect(this);
        }

        public void UpdateTextBox()
        {
            GenText();
            if (Selected)
                TopForm.ControllerNodeSelect(this);
        }

        public void UpdateName()
        {
            Node.Text = GetName();
            if (Selected)
                TopForm.ControllerNodeSelect(this);
        }
    }
}
