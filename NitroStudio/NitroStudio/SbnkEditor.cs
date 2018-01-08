﻿using System;
using System.IO;
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
    public partial class SbnkEditor : Form
    {

        public string nitroPath = Application.StartupPath;
        sbnkFile file;
        private MainWindow parent;
        int parentIndex;

        public SbnkEditor(MainWindow parent, byte[] b, string name, int index)
        {
            InitializeComponent();

            //Load sbnk
            file = new sbnkFile();
            file.load(b);

            //Change nodes.
            this.Text = name;
            tree.Nodes[0].Text = name;

            //Update nodes.
            updateNodes();

            this.parent = parent;
            parentIndex = index;
        }

        //Update nodes
        #region updateNodes
        public void updateNodes() {
            tree.BeginUpdate();

            List<string> expandedNodes = collectExpandedNodes(tree.Nodes);

            foreach (TreeNode e in tree.Nodes[0].Nodes)
            {
                tree.Nodes[0].Nodes.RemoveAt(0);
            }
            tree.SelectedNode = tree.Nodes[0];
            tree.Nodes[0].ContextMenuStrip = bigMenu;

            for (int i = 0; i < file.data.Length; i++)
            {

                //Add nodes.
                tree.Nodes[0].Nodes.Add("Block " + i, "Block " + i, 1, 1);
                tree.Nodes[0].Nodes[i].ContextMenuStrip = blockMenu;

                for (int j = 0; j < file.data[i].records.Length; j++)
                {

                    if (file.data[i].records[j].isPlaceholder)
                    {

                        tree.Nodes[0].Nodes[i].Nodes.Add("[" + j + "] " + "%PLACEHOLDER%");
                        tree.Nodes[0].Nodes[i].Nodes[j].ContextMenuStrip = recordMenu;

                    }
                    else
                    {

                        tree.Nodes[0].Nodes[i].Nodes.Add("[" + j + "] " + "Record", "[" + j + "] " + "Record", 3, 3);
                        tree.Nodes[0].Nodes[i].Nodes[j].ContextMenuStrip = recordMenu;

                        //Add each of the other records due to type.
                        if (file.data[i].records[j].fRecord == 16)
                        {
                            tree.Nodes[0].Nodes[i].Nodes[j].Nodes.Add("Ranged Instrument");

                            int count = (int)file.data[i].records[j].instrumentB.lowerNote;
                            for (int h = 0; h < file.data[i].records[j].instrumentB.stuff.Count(); h++)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Key " + count, "Key " + count, 4, 4);
                                count += 1;
                            }
                        }
                        else if (file.data[i].records[j].fRecord > 16)
                        {
                            tree.Nodes[0].Nodes[i].Nodes[j].Nodes.Add("Regional Instrument");

                            sbnkFile.sbnkInstrumentGreaterThan16 r = file.data[i].records[j].instrumentC;
                            if (r.stuff.Count > 0)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region 0-" + r.region0, "Region 0-" + r.region0, 2, 2);
                            }
                            if (r.stuff.Count > 1)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region " + (r.region0 + 1) + "-" + r.region1, "Region " + (r.region0 + 1) + "-" + r.region1, 2, 2);
                            }
                            if (r.stuff.Count > 2)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region " + (r.region1 + 1) + "-" + r.region2, "Region " + (r.region1 + 1) + "-" + r.region2, 2, 2);
                            }
                            if (r.stuff.Count > 3)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region " + (r.region2 + 1) + "-" + r.region3, "Region " + (r.region2 + 1) + "-" + r.region3, 2, 2);
                            }
                            if (r.stuff.Count > 4)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region " + (r.region3 + 1) + "-" + r.region4, "Region " + (r.region3 + 1) + "-" + r.region4, 2, 2);
                            }
                            if (r.stuff.Count > 5)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region " + (r.region4 + 1) + "-" + r.region5, "Region " + (r.region4 + 1) + "-" + r.region5, 2, 2);
                            }
                            if (r.stuff.Count > 6)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region " + (r.region5 + 1) + "-" + r.region6, "Region " + (r.region5 + 1) + "-" + r.region6, 2, 2);
                            }
                            if (r.stuff.Count > 7)
                            {
                                tree.Nodes[0].Nodes[i].Nodes[j].Nodes[0].Nodes.Add("Region " + (r.region6 + 1) + "-" + r.region7, "Region " + (r.region6 + 1) + "-" + r.region7, 2, 2);
                            }

                        }
                        else {

                            tree.Nodes[0].Nodes[i].Nodes[j].Nodes.Add("Universal Intrument");

                        }

                    }

                }


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


        //Node shit
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


        //Info Stuff
        #region infoStuff

        //Do Info Panel Stuff.
        public void doInfoStuff() {

            if (tree.SelectedNode.Parent != null)
            {

                if (tree.SelectedNode.Parent.Parent != null) {

                    //fRecord stuff.
                    if (tree.SelectedNode.Parent.Parent.Parent == null) {

                        hideAllThings();
                        fRecordPanel.Show();
                        fRecordBox.SelectedIndex = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].fRecord;

                    } else {

                        if (tree.SelectedNode.Parent.Parent.Parent.Parent == null)
                        {

                            //Universal record.
                            if (tree.SelectedNode.Text == "Universal Intrument")
                            {

                                hideAllThings();

                                if (file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].fRecord < 16)
                                {

                                    universalPanel.Show();

                                    //Set values.
                                    swarBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.swarNumber;
                                    swavNumberBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.swavNumber;
                                    attackRateBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.attackRate;
                                    decayRateBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.decayRate;
                                    releaseRateBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.releaseRate;
                                    sustainRateBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.sustainLevel;
                                    decayRateBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.decayRate;
                                    noteNumberBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.noteNumber;
                                    panBoxUniversal.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA.pan;

                                }
                                else
                                {

                                    //Show not instrument type box.
                                    MessageBox.Show("You can only edit the type of instrument selected!", "Notice:");

                                }

                            }
                            //Ranged record.
                            else if (tree.SelectedNode.Text == "Ranged Instrument")
                            {

                                hideAllThings();

                                if (file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].fRecord == 16)
                                {

                                    rangedPanel.Show();

                                    //Set things.
                                    lowerNoteBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentB.lowerNote;
                                    upperNoteBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentB.upperNote;

                                }
                                else
                                {

                                    //Show not instrument type box.
                                    MessageBox.Show("You can only edit the type of instrument selected!", "Notice:");

                                }

                            }
                            //Regional Record.
                            else if (tree.SelectedNode.Text == "Regional Instrument")
                            {
                                hideAllThings();

                                if (file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].fRecord > 16)
                                {
                                    regionedPanel.Show();


                                    //Set things.
                                    region0Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region0;
                                    region1Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region1;
                                    region2Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region2;
                                    region3Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region3;
                                    region4Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region4;
                                    region5Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region5;
                                    region6Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region6;
                                    region7Box.Value = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region7;

                                }
                                else
                                {

                                    //Show not instrument type box.
                                    MessageBox.Show("You can only edit the type of instrument selected!", "Notice:");

                                }

                            }
                            else { hideAllThings(); }

                        }
                        else {

                            hideAllThings();

                            //Ranged Instrument.
                            if (tree.SelectedNode.Parent.Text == "Ranged Instrument") {

                                //Show the basic info.
                                basicInfoRangedPanel.Show();

                                //Set values.
                                swarNumberBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].swarNumber;
                                swavNumberBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].swavNumber;
                                attackRateBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].attackRate;
                                decayRateBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].decayRate;
                                sustainRateBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].sustainLevel;
                                releaseRateBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].releaseRate;
                                panBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].pan;
                                noteNumberBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].noteNumber;
                                unknownBoxRanged.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].unknown;

                            } else if (tree.SelectedNode.Parent.Text == "Regional Instrument") {

                                //Show the basic info.
                                basicInfoRegionalPanel.Show();

                                //Set values.
                                swarNumberBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].swarNumber;
                                swavNumberBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].swavNumber;
                                attackRateBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].attackRate;
                                decayRateBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].decayRate;
                                sustainRateBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].sustainLevel;
                                releaseRateBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].releaseRate;
                                panBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].pan;
                                noteNumberBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].noteNumber;
                                unknownBoxRegional.Value = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].unknown;

                            }

                        }

                    } 

                } else {hideAllThings();}

            }
            else {

                hideAllThings();

            }

        }

        //Hide all things
        public void hideAllThings() {

            fRecordPanel.Hide();
            universalPanel.Hide();
            rangedPanel.Hide();
            regionedPanel.Hide();
            basicInfoRangedPanel.Hide();
            basicInfoRegionalPanel.Hide();
            noInfoPanel.Show();

        }

        #endregion


        //File Menu
        #region fileMenu

        //New
        private void newBetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file = new sbnkFile();
            file.data = new sbnkFile.sbnkData[0];
            updateNodes();
        }

        //Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            o.Filter = "Sound Bank|*.sbnk";
            o.ShowDialog();

            if (o.FileName != "") {

                file = new sbnkFile();
                file.load(File.ReadAllBytes(o.FileName));
                updateNodes();

            }

        }

        //Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file.fixOffsets();
            parent.sdat.files.bankFiles[parentIndex] = file.toBytes();
        }

        //Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.RestoreDirectory = true;
            s.Filter = "Sound Bank|*.sbnk";
            s.ShowDialog();

            if (s.FileName != "") {
                File.WriteAllBytes(s.FileName, file.toBytes());
            }
        }

        //Quit.
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Commit suicide.
            this.Close();
        }

        #endregion


        //Changed info
        #region changedInfo

        //fRecord changing.
        private void changeInstrument_Click(object sender, EventArgs e)
        {

            //See if correct node.
            if (tree.SelectedNode.Text.EndsWith("%PLACEHOLDER%") || tree.SelectedNode.Text.EndsWith("Record"))
            {
                file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].fRecord = (byte)fRecordBox.SelectedIndex;

                if (file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].fRecord < 16 && file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].fRecord != 0)
                {
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA = new sbnkFile.sbnkInstrumentLessThan16();
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.swarNumber = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.swavNumber = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.attackRate = 127;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.decayRate = 127;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.sustainLevel = 127;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.releaseRate = 127;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.noteNumber = 64;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentA.pan = 64;


                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].isPlaceholder = false;
                    try
                    {
                        tree.SelectedNode.Nodes.RemoveAt(0);
                        tree.SelectedNode.Nodes.Add("Universal Instrument");
                        updateNodes();
                    }
                    catch
                    {
                        tree.SelectedNode.Nodes.Add("Universal Instrument");
                        updateNodes();
                    }
                }
                else if (file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].fRecord == 0)
                {

                    foreach (TreeNode n in tree.SelectedNode.Nodes)
                    {
                        tree.SelectedNode.Nodes.RemoveAt(0);
                    }

                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].isPlaceholder = true;

                    updateNodes();

                }
                else if (file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].fRecord == 16)
                {

                    foreach (TreeNode n in tree.SelectedNode.Nodes)
                    {
                        tree.SelectedNode.Nodes.RemoveAt(0);
                    }

                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].isPlaceholder = false;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentB = new sbnkFile.sbnkInstrumentEquals16();
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentB.lowerNote = 64;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentB.upperNote = 64;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentB.stuff = new List<sbnkFile.basicInstrumentStuff>();

                    tree.SelectedNode.Nodes.Add("Ranged Instrument");

                    sbnkFile.basicInstrumentStuff b = new sbnkFile.basicInstrumentStuff();
                    b.attackRate = 127;
                    b.decayRate = 127;
                    b.noteNumber = 64;
                    b.pan = 64;
                    b.sustainLevel = 127;
                    b.releaseRate = 127;
                    b.unknown = 1;
                    b.swarNumber = 0;
                    b.swavNumber = 0;

                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentB.stuff.Add(b);
                    updateNodes();

                }
                else {

                    foreach (TreeNode n in tree.SelectedNode.Nodes)
                    {
                        tree.SelectedNode.Nodes.RemoveAt(0);
                    }

                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].isPlaceholder = false;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC = new sbnkFile.sbnkInstrumentGreaterThan16();
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region0 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region1 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region2 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region3 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region4 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region5 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region6 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.region7 = 0;
                    file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index].instrumentC.stuff = new List<sbnkFile.basicInstrumentStuff>();

                    updateNodes();

                }

            }

        }

        //Range creation.
        private void setRangedButton_Click(object sender, EventArgs e)
        {
            //See if correct.
            if (tree.SelectedNode.Text.EndsWith("Ranged Instrument")) {

                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentB.lowerNote = (byte)lowerNoteBoxRanged.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentB.upperNote = (byte)upperNoteBoxRanged.Value;

                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentB.stuff = new List<sbnkFile.basicInstrumentStuff>();

                for (int i = (int)lowerNoteBoxRanged.Value; i <= (int)upperNoteBoxRanged.Value; i++) {
                    sbnkFile.basicInstrumentStuff b = new sbnkFile.basicInstrumentStuff();

                    b.attackRate = 127;
                    b.decayRate = 127;
                    b.noteNumber = (byte)i;
                    b.pan = 64;
                    b.sustainLevel = 127;
                    b.releaseRate = 127;
                    b.unknown = 1;
                    b.swarNumber = 0;
                    b.swavNumber = 0;

                    file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentB.stuff.Add(b);
                }

                updateNodes();

            }

        }

        //Regional creation.
        private void createNewRegionsButton_Click(object sender, EventArgs e)
        {
            //See if correct.
            if (tree.SelectedNode.Text.EndsWith("Regional Instrument"))
            {

                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region0 = (byte)region0Box.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region1 = (byte)region1Box.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region2 = (byte)region2Box.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region3 = (byte)region3Box.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region4 = (byte)region4Box.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region5 = (byte)region5Box.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region6 = (byte)region6Box.Value;
                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.region7 = (byte)region7Box.Value;

                file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.stuff = new List<sbnkFile.basicInstrumentStuff>();

                sbnkFile.sbnkInstrumentGreaterThan16 a = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC;


                //Get count.
                int count = 0;
                #region getCount
                if (a.region0 == 0)
                {
                    count = 0;
                }
                else
                {
                    if (a.region1 == 0)
                    {
                        count = 1;
                    }
                    else
                    {
                        if (a.region2 == 0)
                        {
                            count = 2;
                        }
                        else
                        {
                            if (a.region3 == 0)
                            {
                                count = 3;
                            }
                            else
                            {
                                if (a.region4 == 0)
                                {
                                    count = 4;
                                }
                                else
                                {
                                    if (a.region5 == 0)
                                    {
                                        count = 5;
                                    }
                                    else
                                    {
                                        if (a.region6 == 0)
                                        {
                                            count = 6;
                                        }
                                        else
                                        {
                                            if (a.region7 == 0)
                                            {
                                                count = 7;
                                            }
                                            else
                                            {
                                                count = 8;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                for (int i = 0; i < count; i++) {
                    sbnkFile.basicInstrumentStuff b = new sbnkFile.basicInstrumentStuff();

                    b.attackRate = 127;
                    b.decayRate = 127;
                    b.noteNumber = 64;
                    b.pan = 64;
                    b.sustainLevel = 127;
                    b.releaseRate = 127;
                    b.unknown = 1;
                    b.swarNumber = 0;
                    b.swavNumber = 0;

                    file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentC.stuff.Add(b);
                }

                updateNodes();

            }
        }

        //Update Ranged Data.
        private void updateRangedButton_Click(object sender, EventArgs e)
        {
            sbnkFile.basicInstrumentStuff f = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index];

            f.attackRate = (byte)attackRateBoxRanged.Value;
            f.decayRate = (byte)decayRateBoxRanged.Value;
            f.swarNumber = (UInt16)swarNumberBoxRanged.Value;
            f.swavNumber = (UInt16)swavNumberBoxRanged.Value;
            f.sustainLevel = (byte)sustainRateBoxRanged.Value;
            f.releaseRate = (byte)releaseRateBoxRanged.Value;
            f.pan = (byte)panBoxRanged.Value;
            f.noteNumber = (byte)noteNumberBoxRanged.Value;
            f.unknown = (byte)unknownBoxRanged.Value;

            file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index] = f;
        }

        //Update Regional Data.
        private void updateButtonRegional_Click(object sender, EventArgs e)
        {
            sbnkFile.basicInstrumentStuff f = file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index];

            f.attackRate = (byte)attackRateBoxRegional.Value;
            f.decayRate = (byte)decayRateBoxRegional.Value;
            f.swarNumber = (UInt16)swarNumberBoxRegional.Value;
            f.swavNumber = (UInt16)swavNumberBoxRegional.Value;
            f.sustainLevel = (byte)sustainRateBoxRegional.Value;
            f.releaseRate = (byte)releaseRateBoxRegional.Value;
            f.pan = (byte)panBoxRegional.Value;
            f.noteNumber = (byte)noteNumberBoxRegional.Value;
            f.unknown = (byte)unknownBoxRegional.Value;

            file.data[tree.SelectedNode.Parent.Parent.Parent.Index].records[tree.SelectedNode.Parent.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index] = f;
        }

        //Update Universal Data.
        private void updateDataUniversal_Click(object sender, EventArgs e)
        {
            sbnkFile.sbnkInstrumentLessThan16 f = file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA;

            f.attackRate = (byte)attackRateBoxUniversal.Value;
            f.decayRate = (byte)decayRateBoxUniversal.Value;
            f.swarNumber = (UInt16)swarBoxUniversal.Value;
            f.swavNumber = (UInt16)swavNumberBoxUniversal.Value;
            f.sustainLevel = (byte)sustainRateBoxUniversal.Value;
            f.releaseRate = (byte)releaseRateBoxUniversal.Value;
            f.pan = (byte)panBoxUniversal.Value;
            f.noteNumber = (byte)noteNumberBoxUniversal.Value;

            file.data[tree.SelectedNode.Parent.Parent.Index].records[tree.SelectedNode.Parent.Index].instrumentA = f;

        }

        #endregion


        //Big menu
        #region bigMenu

        private void addBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<sbnkFile.sbnkData> f = file.data.ToList();
            sbnkFile.sbnkData d = new sbnkFile.sbnkData();
            d.records = new sbnkFile.sbnkInstrumentRecord[0];
            f.Add(d);
            file.data = f.ToArray();
            updateNodes();
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse();
        }

        #endregion


        //Block menu
        #region blockMenu

        private void addRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<sbnkFile.sbnkInstrumentRecord> r = file.data[tree.SelectedNode.Index].records.ToList();
            sbnkFile.sbnkInstrumentRecord s = new sbnkFile.sbnkInstrumentRecord();
            s.fRecord = 0;
            s.isPlaceholder = true;
            r.Add(s);
            file.data[tree.SelectedNode.Index].records = r.ToArray();
            updateNodes();
        }

        private void expandToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        private void collapseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<sbnkFile.sbnkData> s = file.data.ToList();
            s.RemoveAt(tree.SelectedNode.Index);
            file.data = s.ToArray();
            updateNodes();
        }

        #endregion


        //Record menu
        #region recordMenu

        private void addAboveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumberSelectionDialog d = new NumberSelectionDialog();
            int count = d.getValue();

            int parentIndex = tree.SelectedNode.Parent.Index;
            int index = tree.SelectedNode.Index;

            List<sbnkFile.sbnkInstrumentRecord> r = file.data[parentIndex].records.ToList();

            sbnkFile.sbnkInstrumentRecord s = new sbnkFile.sbnkInstrumentRecord();
            s.fRecord = 0;
            s.isPlaceholder = true;

            for (int i = 0; i < count; i++)
            {
                r.Insert(index, s);
            }

            file.data[parentIndex].records = r.ToArray();
            updateNodes();
        }

        private void addBelowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumberSelectionDialog d = new NumberSelectionDialog();
            int count = d.getValue();

            int parentIndex = tree.SelectedNode.Parent.Index;
            int index = tree.SelectedNode.Index;

            List<sbnkFile.sbnkInstrumentRecord> r = file.data[parentIndex].records.ToList();

            sbnkFile.sbnkInstrumentRecord s = new sbnkFile.sbnkInstrumentRecord();
            s.fRecord = 0;
            s.isPlaceholder = true;

            for (int i = 0; i < count; i++)
            {
                r.Insert(index+1, s);
            }

            file.data[parentIndex].records = r.ToArray();
            updateNodes();
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Index != 0) {
                sbnkFile.sbnkInstrumentRecord current = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index];
                sbnkFile.sbnkInstrumentRecord above = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index-1];

                file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index - 1] = current;
                file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index] = above;
                updateNodes();
            }
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Index != file.data[tree.SelectedNode.Parent.Index].records.Count()-1)
            {
                sbnkFile.sbnkInstrumentRecord current = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index];
                sbnkFile.sbnkInstrumentRecord below = file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index + 1];

                file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index + 1] = current;
                file.data[tree.SelectedNode.Parent.Index].records[tree.SelectedNode.Index] = below;
                updateNodes();
            }
        }

        private void expandToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        private void collapseToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<sbnkFile.sbnkInstrumentRecord> s = file.data[tree.SelectedNode.Parent.Index].records.ToList();
            s.RemoveAt(tree.SelectedNode.Index);
            file.data[tree.SelectedNode.Parent.Index].records = s.ToArray();
            updateNodes();
        }

        #endregion







        private void SbnkEditor_Load(object sender, EventArgs e)
        {

        }

        
    }
}