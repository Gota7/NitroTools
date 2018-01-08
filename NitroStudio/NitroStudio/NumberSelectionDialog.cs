using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NitroStudio
{
    public partial class NumberSelectionDialog : Form
    {

        int returnValue = 0;

        public NumberSelectionDialog()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            returnValue = (int)numericUpDown1.Value;
            this.Close();
        }

        public int getValue() {
            this.ShowDialog();
            return returnValue;
        }

    }
}
