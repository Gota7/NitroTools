using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NitroFileLoader;
using System.IO;
using System.Diagnostics;

namespace NitroStudio
{
    public partial class SwarEditor : Form
    {

        swarFile file;

        public SwarEditor(byte[] b, string name)
        {
            InitializeComponent();

            //Load swar.
            file = new swarFile();
            file.load(b);

            //Change nodes.
            this.Text = name + " (DON'T TRUST THIS!!! INCOMPLETE!)";
            tree.Nodes[0].Text = name;

            //Update nodes.
            updateNodes();

        }


        //Update nodes.
        #region updateNodes

        public void updateNodes() {

            tree.BeginUpdate();
            foreach (TreeNode e in tree.Nodes[0].Nodes) {
                tree.Nodes[0].Nodes.RemoveAt(0);
            }
            tree.SelectedNode = tree.Nodes[0];
            for (int i = 0; i < file.data.Length; i++) {

                //Add nodes.
                tree.Nodes[0].Nodes.Add("Block " + i);

                for (int j = 0; j < file.data[i].files.Length; j++) {

                    tree.Nodes[0].Nodes[i].Nodes.Add("Sound " + j, "Sound " + j, 1, 1);
                    tree.Nodes[0].Nodes[i].LastNode.ContextMenuStrip = soundMenu;

                }

            }

            tree.EndUpdate();

        }

        #endregion


        //Node shit.
        #region nodeShit

        //Expand node and parents.
        void expandNodePath(TreeNode node)
        {
            if (node == null)
                return;
            if (node.Level != 0) //check if it is not root
            {
                node.Expand();
                expandNodePath(node.Parent);
            }
            else
            {
                node.Expand(); // this is root 
            }



        }

        //Make right click actually select, and show infoViewer.
        void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Select the clicked node
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            }

        }

        #endregion


        //Sound menu.
        #region soundMenu

        private void addAbove_Click(object sender, EventArgs e)
        {

        }

        private void addBelow_Click(object sender, EventArgs e)
        {

        }

        private void Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Swav File|*.swav|Wave|*.wav";
            f.Title = "Export the file";
            f.ShowDialog();

            if (f.FileName != "")
            {
                if (f.FilterIndex == 1)
                {
                    File.WriteAllBytes(f.FileName, file.data[tree.SelectedNode.Parent.Index].files[tree.SelectedNode.Index]);
                }
                else {
                    File.WriteAllBytes("Data\\Tools\\tmp.swav", file.data[tree.SelectedNode.Parent.Index].files[tree.SelectedNode.Index]);

                    //Convert file.
                    Process p = new Process();
                    p.StartInfo.FileName = "Data\\Tools\\swav2wav.exe";
                    p.StartInfo.Arguments = "Data\\Tools\\tmp.swav";
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();

                    if (File.Exists(f.FileName)) { File.Delete(f.FileName); }
                    File.Delete("Data\\Tools\\tmp.swav");
                    File.Move("Data\\Tools\\tmp.wav", f.FileName);
                }
            }
        }

        private void Import_Click(object sender, EventArgs e)
        {

            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Swav File|*.swav|Wave|*.wav";
            f.Title = "Import the file";
            f.ShowDialog();

            if (f.FileName != "") {

                if (f.FilterIndex == 1)
                {

                    file.data[tree.SelectedNode.Parent.Index].files[tree.SelectedNode.Index] = File.ReadAllBytes(f.FileName);

                }
                else {

                    //Copy file.
                    File.Copy(f.FileName, "Data\\Tools\\tmp.swav", true);

                    //Convert file.
                    Process p = new Process();
                    p.StartInfo.FileName = "Data\\Tools\\wav2swav.exe";
                    p.StartInfo.Arguments = "Data\\Tools\\tmp.swav";
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();

                    file.data[tree.SelectedNode.Parent.Index].files[tree.SelectedNode.Index] = File.ReadAllBytes("Data\\Tools\\tmp.wav");
                    File.Delete("Data\\Tools\\tmp.swav");
                    File.Delete("Data\\Tools\\tmp.wav");

                }

            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            List<byte[]> f = file.data[tree.SelectedNode.Parent.Index].files.ToList();
            f.RemoveAt(tree.SelectedNode.Index);
            file.data[tree.SelectedNode.Parent.Index].files = f.ToArray();
            file.fixOffsets();
            updateNodes();
        }


        #endregion


        //Import Export
        #region importExportFolder

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            DialogResult d = f.ShowDialog();

            if (d == DialogResult.OK)
            {
                file.compress(f.SelectedPath);
                file.fixOffsets();
            }
            updateNodes();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            DialogResult d = f.ShowDialog();

            if (d == DialogResult.OK)
            {
                file.extract(f.SelectedPath); 
            }

        }
        #endregion

        
    }
}
