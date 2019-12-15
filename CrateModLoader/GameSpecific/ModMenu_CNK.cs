using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrateModLoader.GameSpecific
{
    public partial class ModMenu_CNK : Form
    {
        public ModMenu_CNK()
        {
            InitializeComponent();
        }

        private void ModMenu_CNK_Load(object sender, EventArgs e)
        {
            Owner.Enabled = false;
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModMenu_CNK_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }
    }
}
