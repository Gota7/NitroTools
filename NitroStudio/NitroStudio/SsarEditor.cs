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

namespace NitroStudio
{
    public partial class SsarEditor : Form
    {

        ssarFile file = new ssarFile();
        MainWindow parent;
        LibNitro.SND.SSAR nitroFile;
        LibNitro.SND.Player.DSSoundContext dsc = new LibNitro.SND.Player.DSSoundContext();
        TreeNode bankNode;

        public SsarEditor(MainWindow p, byte[] b, TreeNode ban, string name)
        {
            InitializeComponent();
            parent = p;
            file.load(b);
            nitroFile = new LibNitro.SND.SSAR(b);
            bankNode = ban;
            tree.Nodes[0].Text = name;
            this.Text = name;
            updateNodes();

        }

        //TODO: ADD NAMES!!!

        //Do info stuff.
        #region infoStuff

        public void doInfoStuff() {

            if (tree.SelectedNode != null) {

                if (tree.SelectedNode.Parent != null) {

                    //Code here.
                    if (tree.SelectedNode.Parent.Parent != null)
                    {
                        if (!file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].isPlaceholder)
                        {

                            //Show.
                            infoPanel.Show();
                            noInfoPanel.Hide();

                            //Get banks.
                            bankIDbox.Items.Clear();
                            for (int i = 0; i < bankNode.Nodes.Count; i++)
                            {
                                bankIDbox.Items.Add(bankNode.Nodes[i].Text);
                            }
                            bankIDbox.SelectedIndex = (int)file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].bank;

                            //Other stuff.
                            volumeSseqBox.Value = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].volume;
                            channelPrioritySseqBox.Value = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].cpr;
                            playerPrioritySseqBox.Value = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].ppr;
                            playerNumberSseqBox.Value = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].player;

                        }
                        else
                        {
                            //Hide.
                            infoPanel.Hide();
                            noInfoPanel.Show();
                        }


                    }
                    else {
                        //Hide.
                        infoPanel.Hide();
                        noInfoPanel.Show();
                    }

                }
                else
                {
                    //Hide.
                    infoPanel.Hide();
                    noInfoPanel.Show();
                }

            }
            else
            {
                //Hide.
                infoPanel.Hide();
                noInfoPanel.Show();
            }

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

            doInfoStuff();

        }

        void tree_NodeKey(object sender, KeyEventArgs e)
        {

            doInfoStuff();

        }

        //Get expanded nodes.
        List<string> collectExpandedNodes(TreeNodeCollection Nodes)
        {
            List<string> _lst = new List<string>();
            foreach (TreeNode checknode in Nodes)
            {
                if (checknode.IsExpanded)
                    _lst.Add(checknode.Name);
                if (checknode.Nodes.Count > 0)
                    _lst.AddRange(collectExpandedNodes(checknode.Nodes));
            }
            return _lst;
        }


        /// <summary>
        /// Find nodes by name.
        /// </summary>
        /// <param name="NodesCollection"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        TreeNode FindNodeByName(TreeNodeCollection NodesCollection, string Name)
        {
            TreeNode returnNode = null; // Default value to return
            foreach (TreeNode checkNode in NodesCollection)
            {
                if (checkNode.Name == Name)  //checks if this node name is correct
                    returnNode = checkNode;
                else if (checkNode.Nodes.Count > 0) //node has child
                {
                    returnNode = FindNodeByName(checkNode.Nodes, Name);
                }

                if (returnNode != null) //check if founded do not continue and break
                {
                    return returnNode;
                }

            }
            //not found
            return returnNode;
        }

        #endregion


        //Update nodes.
        #region updateNodes

        public void updateNodes() {

            tree.BeginUpdate();

            List<string> expandedNodes = collectExpandedNodes(tree.Nodes);

            foreach (TreeNode e in tree.Nodes[0].Nodes)
            {
                tree.Nodes[0].Nodes.RemoveAt(0);
            }
            tree.SelectedNode = tree.Nodes[0];


            //Data.
            int count = 0;
            foreach (ssarFile.ssarData data in file.data) {

                tree.Nodes[0].Nodes.Add("Block " + count, "Block " + count, 2, 2);

                //Add SSEQs.
                int count2 = 0;
                foreach (ssarFile.ssarRecord s in data.records) {

                    if (!s.isPlaceholder)
                    {
                        tree.Nodes[0].Nodes[count].Nodes.Add("[" + count2 + "] Sequence");
                    }
                    else {
                        tree.Nodes[0].Nodes[count].Nodes.Add("[" + count2 + "] %PLACEHOLDER%");
                    }
                    count2 += 1;

                }

                count += 1;
            }

            //Restore the nodes if they exist.
            if (expandedNodes.Count > 0)
            {
                TreeNode IamExpandedNode;
                for (int i = 0; i < expandedNodes.Count; i++)
                {
                    IamExpandedNode = FindNodeByName(tree.Nodes, expandedNodes[i]);
                    expandNodePath(IamExpandedNode);
                }

            }

            tree.EndUpdate();

        }

        #endregion



    }
}
