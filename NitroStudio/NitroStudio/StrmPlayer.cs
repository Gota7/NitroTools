using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NitroStudio
{
    public partial class StrmPlayer : Form
    {

        public string nitroPath = Application.StartupPath;
        string path;

        public StrmPlayer(string path2)
        {
            InitializeComponent();
            axWindowsMediaPlayer1.URL = path2;
            path = path2;
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void onClose(object sender, EventArgs e) {
            //Delete useless files.
            Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools\\");
            File.Delete(path);
            Directory.SetCurrentDirectory(nitroPath);
        }

    }
}
