using System;
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
using System.Media;
using System.Diagnostics;

namespace NitroStudio
{
    public partial class SbnkEditor : Form
    {

        public string nitroPath = Application.StartupPath;
        sbnkFile file;
        private MainWindow parent;
        int parentIndex;

        //Emulator variables.
        int bankId = 0;
        sbnkFile.basicInstrumentStuff emulatorInfo;
        SoundPlayer player = new SoundPlayer();

        public SbnkEditor(MainWindow parent, byte[] b, string name, int index, int emulationIndex)
        {
            InitializeComponent();

            //Load sbnk
            file = new sbnkFile();
            file.load(b);

            //Change nodes.
            this.Text = name;
            tree.Nodes[0].Text = name;

            //Get the bank IDs.
            bankEmulationBox.Items.Clear();

            this.parent = parent;
            parentIndex = index;

            //Bank Emulation.
            int count = 0;
            foreach (symbStringName sa in parent.sdat.symbFile.bankStrings)
            {
                if (sa.isPlaceHolder)
                {
                    bankEmulationBox.Items.Add("[" + count + "] %PLACEHOLDER%");
                }
                else
                {
                    bankEmulationBox.Items.Add("[" + count + "] " + sa.name);
                }
                count += 1;
            }
            bankEmulationBox.SelectedIndex = emulationIndex;
            

            //Update nodes.
            updateNodes();
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
            tree.Nodes[0].ContextMenuStrip = blockMenu;

            for (int i = 0; i < 1; i++)
            {

                //Add nodes.
                //tree.Nodes[0].Nodes.Add("Block " + i, "Block " + i, 1, 1);
                //tree.Nodes[0].Nodes[i].ContextMenuStrip = blockMenu;

                for (int j = 0; j < file.data[i].records.Length; j++)
                {

                    if (file.data[i].records[j].isPlaceholder)
                    {

                        tree.Nodes[0].Nodes.Add("[" + j + "] " + "%PLACEHOLDER%", "[" + j + "] " + "%PLACEHOLDER%", 5, 5);

                    }
                    else
                    {

                        //tree.Nodes[0].Nodes.Add("[" + j + "] " + "Record", "[" + j + "] " + "Record", 3, 3);
                        //tree.Nodes[0].Nodes[j].ContextMenuStrip = recordMenu;

                        //Add each of the other records due to type.
                        if (file.data[i].records[j].fRecord == 16)
                        {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "Ranged Instrument", "[" + j + "] " + "Ranged Instrument", 8, 8);

                            int count = (int)file.data[i].records[j].instrumentB.lowerNote;
                            for (int h = 0; h < file.data[i].records[j].instrumentB.stuff.Count(); h++)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Key " + count, "Key " + count, 4, 4);
                                count += 1;
                            }
                        }
                        else if (file.data[i].records[j].fRecord > 16)
                        {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "Regional Instrument", "[" + j + "] " + "Regional Instrument", 9, 9);

                            sbnkFile.sbnkInstrumentGreaterThan16 r = file.data[i].records[j].instrumentC;
                            if (r.stuff.Count > 0)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region 0-" + r.region0, "Region 0-" + r.region0, 2, 2);
                            }
                            if (r.stuff.Count > 1)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region " + (r.region0 + 1) + "-" + r.region1, "Region " + (r.region0 + 1) + "-" + r.region1, 2, 2);
                            }
                            if (r.stuff.Count > 2)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region " + (r.region1 + 1) + "-" + r.region2, "Region " + (r.region1 + 1) + "-" + r.region2, 2, 2);
                            }
                            if (r.stuff.Count > 3)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region " + (r.region2 + 1) + "-" + r.region3, "Region " + (r.region2 + 1) + "-" + r.region3, 2, 2);
                            }
                            if (r.stuff.Count > 4)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region " + (r.region3 + 1) + "-" + r.region4, "Region " + (r.region3 + 1) + "-" + r.region4, 2, 2);
                            }
                            if (r.stuff.Count > 5)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region " + (r.region4 + 1) + "-" + r.region5, "Region " + (r.region4 + 1) + "-" + r.region5, 2, 2);
                            }
                            if (r.stuff.Count > 6)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region " + (r.region5 + 1) + "-" + r.region6, "Region " + (r.region5 + 1) + "-" + r.region6, 2, 2);
                            }
                            if (r.stuff.Count > 7)
                            {
                                tree.Nodes[0].Nodes[j].Nodes.Add("Region " + (r.region6 + 1) + "-" + r.region7, "Region " + (r.region6 + 1) + "-" + r.region7, 2, 2);
                            }

                        }

                        else if (file.data[i].records[j].fRecord == 2)
                        {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "8-Bit Instrument", "[" + j + "] " + "8-Bit Instrument", 6, 6);
                        }

                        else if (file.data[i].records[j].fRecord == 3) {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "White Noise Instrument", "[" + j + "] " + "White Noise Instrument", 7, 7);
                        }

                        else if (file.data[i].records[j].fRecord == 4)
                        {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "Direct PCM Instrument", "[" + j + "] " + "Direct PCM Instrument", 10, 10);
                        }

                        else if (file.data[i].records[j].fRecord == 5)
                        {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "Blank Instrument", "[" + j + "] " + "Blank Instrument", 5, 5);
                        }

                        else if (file.data[i].records[j].fRecord == 1)
                        {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "Universal Instrument");
                        }

                        else
                        {
                            tree.Nodes[0].Nodes.Add("[" + j + "] " + "Invalid Instrument");
                        }

                    }
                    tree.Nodes[0].Nodes[j].ContextMenuStrip = recordMenu;

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

            //Do emulator stuff.
            doEmulatorStuff();
            hideAllThings();

            //See what node is selected.
            if (tree.SelectedNode != null)
            {

                nodeSelected.Text = "Node: " + tree.SelectedNode.Text + ";";

                if (tree.SelectedNode.Parent != null)
                {

                    nodeParent.Text = "Parent Node: " + tree.SelectedNode.Parent.Text;

                    //Instrument.
                    if (tree.SelectedNode.Parent.Parent == null)
                    {

                        int type = file.data[0].records[tree.SelectedNode.Index].fRecord;
                        fRecordBox.SelectedIndex = type;
                        universalPanel.Enabled = true;
                        switch (type)
                        {

                            case 0:
                                universalPanel.Enabled = false;
                                universalPanel.Show();
                                showInstrumentChanger();

                                swavNumberUniversal.Text = "Swav Number:";
                                swavNumberUniversal.Show();
                                swavNumberBoxUniversal.Show();
                                swarBoxUniversal.Value = 0;
                                attackRateBoxUniversal.Value = 0;
                                decayRateBoxUniversal.Value = 0;
                                noteNumberBoxUniversal.Value = 0;
                                panBoxUniversal.Value = 0;
                                releaseRateBoxUniversal.Value = 0;
                                sustainRateBoxUniversal.Value = 0;
                                swavNumberBoxUniversal.Value = 0;
                                break;

                            case 1:
                                universalPanel.Show();
                                showInstrumentChanger();

                                swavNumberUniversal.Text = "Swav Number:";
                                swavNumberUniversal.Enabled = true;
                                swavNumberBoxUniversal.Enabled = true;
                                swarBoxUniversal.Enabled = true;
                                swarLabelUniversal.Enabled = true;
                                swarBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swarNumber;
                                attackRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.attackRate;
                                decayRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.decayRate;
                                noteNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.noteNumber;
                                panBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.pan;
                                releaseRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.releaseRate;
                                sustainRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.sustainLevel;
                                swavNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swavNumber;
                                break;

                            case 2:
                                universalPanel.Show();
                                showInstrumentChanger();
                                swavNumberUniversal.Text = "Duty Cycle:";
                                swavNumberUniversal.Enabled = true;
                                swavNumberBoxUniversal.Enabled = true;
                                swarBoxUniversal.Enabled = true;
                                swarLabelUniversal.Enabled = true;
                                swarBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swarNumber;
                                attackRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.attackRate;
                                decayRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.decayRate;
                                noteNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.noteNumber;
                                panBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.pan;
                                releaseRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.releaseRate;
                                sustainRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.sustainLevel;
                                swavNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swavNumber;
                                break;

                            case 3:
                                universalPanel.Show();
                                showInstrumentChanger();
                                swavNumberUniversal.Enabled = false;
                                swavNumberBoxUniversal.Enabled = false;
                                swarBoxUniversal.Enabled = false;
                                swarLabelUniversal.Enabled = false;
                                swarBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swarNumber;
                                attackRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.attackRate;
                                decayRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.decayRate;
                                noteNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.noteNumber;
                                panBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.pan;
                                releaseRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.releaseRate;
                                sustainRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.sustainLevel;
                                break;

                            case 4:
                                universalPanel.Show();
                                showInstrumentChanger();

                                swavNumberUniversal.Text = "Swav Number:";
                                swavNumberUniversal.Enabled = true;
                                swavNumberBoxUniversal.Enabled = true;
                                swarBoxUniversal.Enabled = true;
                                swarLabelUniversal.Enabled = true;
                                swarBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swarNumber;
                                attackRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.attackRate;
                                decayRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.decayRate;
                                noteNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.noteNumber;
                                panBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.pan;
                                releaseRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.releaseRate;
                                sustainRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.sustainLevel;
                                swavNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swavNumber;
                                break;

                            case 16:
                                showInstrumentChanger();
                                rangedPanel.Show();

                                upperNoteBoxRanged.Value = file.data[0].records[tree.SelectedNode.Index].instrumentB.upperNote;
                                lowerNoteBoxRanged.Value = file.data[0].records[tree.SelectedNode.Index].instrumentB.lowerNote;
                                break;

                            case 17:
                                showInstrumentChanger();
                                regionedPanel.Show();

                                region0Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region0;
                                region1Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region1;
                                region2Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region2;
                                region3Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region3;
                                region4Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region4;
                                region5Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region5;
                                region6Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region6;
                                region7Box.Value = file.data[0].records[tree.SelectedNode.Index].instrumentC.region7;
                                break;

                            default:
                                universalPanel.Show();
                                showInstrumentChanger();

                                swavNumberUniversal.Text = "Swav Number:";
                                swavNumberUniversal.Show();
                                swavNumberBoxUniversal.Show();
                                swarBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swarNumber;
                                attackRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.attackRate;
                                decayRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.decayRate;
                                noteNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.noteNumber;
                                panBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.pan;
                                releaseRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.releaseRate;
                                sustainRateBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.sustainLevel;
                                swavNumberBoxUniversal.Value = file.data[0].records[tree.SelectedNode.Index].instrumentA.swavNumber;
                                break;

                        }

                    }
                    else {

                        //Show the instrument data.
                        int type = file.data[0].records[tree.SelectedNode.Parent.Index].fRecord;
                        fRecordBox.SelectedIndex = type;
                        switch (type) {

                            case 16:

                                hideAllThings();

                                basicInfoRangedPanel.SendToBack();
                                basicInfoRangedPanel.Show();
                                noInfoPanel.SendToBack();

                                attackRateBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].attackRate;
                                decayRateBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].decayRate;
                                panBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].pan;
                                sustainRateBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].sustainLevel;
                                releaseRateBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].releaseRate;
                                noteNumberBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].noteNumber;
                                unknownBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].unknown;
                                swarNumberBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].swarNumber;
                                swavNumberBoxRanged.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index].swavNumber;

                                break;

                            case 17:

                                hideAllThings();

                                basicInfoRegionalPanel.Show();

                                attackRateBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].attackRate;
                                decayRateBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].decayRate;
                                panBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].pan;
                                sustainRateBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].sustainLevel;
                                releaseRateBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].releaseRate;
                                noteNumberBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].noteNumber;
                                unknownBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].unknown;
                                swarNumberBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].swarNumber;
                                swavNumberBoxRegional.Value = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index].swavNumber;

                                break;

                        }

                    }

                }
                else
                {

                    nodeParent.Text = "Node's parent is null!";

                }

            }
            else {

                nodeSelected.Text = "No node selected!";

            }

        }

        //Hide all things
        public void hideAllThings() {

            universalPanel.Hide();
            rangedPanel.Hide();
            regionedPanel.Hide();
            basicInfoRangedPanel.Hide();
            basicInfoRegionalPanel.Hide();
            noInfoPanel.Show();
            changeInstrumentPanel.Hide();
            changeInstrumentPanel.SendToBack();
            noInfoPanel.SendToBack();

        }

        public void showInstrumentChanger() {

            changeInstrumentPanel.SendToBack();
            changeInstrumentPanel.Show();

        }

        #endregion


        //Emulator Stuff
        #region emulatorStuff

        //Do emulator stuff.
        public void doEmulatorStuff() {

            hideStuff();

            //See if instrument node.
            if (tree.SelectedNode != null) {

                //Universal Instrument.
                if (tree.SelectedNode.Text.Contains("Universal") || tree.SelectedNode.Text.Contains("Mystery")) 
                {

                    //Show stuff.
                    intrumentPanel.Show();

                    //Set basic instrument stuff.
                    emulatorInfo = new sbnkFile.basicInstrumentStuff();
                    emulatorInfo.attackRate = file.data[0].records[tree.SelectedNode.Index].instrumentA.attackRate;
                    emulatorInfo.decayRate = file.data[0].records[tree.SelectedNode.Index].instrumentA.decayRate;
                    emulatorInfo.swarNumber = file.data[0].records[tree.SelectedNode.Index].instrumentA.swarNumber;
                    emulatorInfo.swavNumber = file.data[0].records[tree.SelectedNode.Index].instrumentA.swavNumber;
                    emulatorInfo.sustainLevel = file.data[0].records[tree.SelectedNode.Index].instrumentA.sustainLevel;
                    emulatorInfo.releaseRate = file.data[0].records[tree.SelectedNode.Index].instrumentA.releaseRate;
                    emulatorInfo.pan = file.data[0].records[tree.SelectedNode.Index].instrumentA.pan;
                    emulatorInfo.noteNumber = file.data[0].records[tree.SelectedNode.Index].instrumentA.noteNumber;
                    emulatorInfo.unknown = 1;

                    doInstrumentStuff();

                }
                //Regional
                else if (tree.SelectedNode.Text.Contains("Region") && !tree.SelectedNode.Text.Contains("Regional"))
                {

                    //Show stuff.
                    intrumentPanel.Show();

                    //Get stuff.
                    emulatorInfo = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index];

                    doInstrumentStuff();

                }
                //Ranged
                else if (tree.SelectedNode.Text.Contains("Key"))
                {

                    //Show stuff.
                    intrumentPanel.Show();

                    //Get stuff.
                    emulatorInfo = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index];

                    doInstrumentStuff();

                }


            }

        }

        public void hideStuff() {
            noInstrumentPanel.Show();
            intrumentPanel.Hide();
        }

        #endregion


        //Instrument Stuff
        #region instrumentStuff

        public void doInstrumentStuff() {

            //Get the bank IDs.
            bankEmulationBox.Items.Clear();

            int count = 0;
            foreach (symbStringName b in parent.sdat.symbFile.bankStrings) {
                if (b.isPlaceHolder) {
                    bankEmulationBox.Items.Add("[" + count + "] %PLACEHOLDER%");
                }
                else
                {
                    bankEmulationBox.Items.Add("[" + count + "] " + b.name);
                }
                count += 1;
            }
            bankEmulationBox.SelectedIndex = bankId;

            //Edit SWAR list.
            swar1.Items.Clear();
            swar2.Items.Clear();
            swar3.Items.Clear();
            swar4.Items.Clear();

            //Add SWARs.
            count = 0;
            swar1.Items.Add("FFFF - None");
            swar2.Items.Add("FFFF - None");
            swar3.Items.Add("FFFF - None");
            swar4.Items.Add("FFFF - None");
            foreach (symbStringName b in parent.sdat.symbFile.waveStrings)
            {
                if (b.isPlaceHolder)
                {
                    swar1.Items.Add("[" + count + "] %PLACEHOLDER%");
                    swar2.Items.Add("[" + count + "] %PLACEHOLDER%");
                    swar3.Items.Add("[" + count + "] %PLACEHOLDER%");
                    swar4.Items.Add("[" + count + "] %PLACEHOLDER%");
                }
                else
                {
                    swar1.Items.Add("[" + count + "] " + b.name);
                    swar2.Items.Add("[" + count + "] " + b.name);
                    swar3.Items.Add("[" + count + "] " + b.name);
                    swar4.Items.Add("[" + count + "] " + b.name);
                }
                count += 1;
            }

            //Select waves.
            if (parent.sdat.infoFile.bankData[bankId].wave0 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar1.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave0 + 1; } else { swar1.SelectedIndex = 0; }
            if (parent.sdat.infoFile.bankData[bankId].wave1 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar2.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave1 + 1; } else { swar2.SelectedIndex = 0; }
            if (parent.sdat.infoFile.bankData[bankId].wave2 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar3.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave2 + 1; } else { swar3.SelectedIndex = 0; }
            if (parent.sdat.infoFile.bankData[bankId].wave3 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar4.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave3 + 1; } else { swar4.SelectedIndex = 0; }

        }

        //Bank Update.
        private void bankEmulationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bankId = bankEmulationBox.SelectedIndex;

            //Add SWARs.
            int count = 0;
            swar1.Items.Add("FFFF - None");
            swar2.Items.Add("FFFF - None");
            swar3.Items.Add("FFFF - None");
            swar4.Items.Add("FFFF - None");
            foreach (symbStringName b in parent.sdat.symbFile.waveStrings)
            {
                if (b.isPlaceHolder)
                {
                    swar1.Items.Add("[" + count + "] %PLACEHOLDER%");
                    swar2.Items.Add("[" + count + "] %PLACEHOLDER%");
                    swar3.Items.Add("[" + count + "] %PLACEHOLDER%");
                    swar4.Items.Add("[" + count + "] %PLACEHOLDER%");
                }
                else
                {
                    swar1.Items.Add("[" + count + "] " + b.name);
                    swar2.Items.Add("[" + count + "] " + b.name);
                    swar3.Items.Add("[" + count + "] " + b.name);
                    swar4.Items.Add("[" + count + "] " + b.name);
                }
                count += 1;
            }

            //Select waves.
            if (parent.sdat.infoFile.bankData[bankId].wave0 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar1.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave0 + 1; } else { swar1.SelectedIndex = 0; }
            if (parent.sdat.infoFile.bankData[bankId].wave1 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar2.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave1 + 1; } else { swar2.SelectedIndex = 0; }
            if (parent.sdat.infoFile.bankData[bankId].wave2 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar3.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave2 + 1; } else { swar3.SelectedIndex = 0; }
            if (parent.sdat.infoFile.bankData[bankId].wave3 != 0xFFFF && !parent.sdat.infoFile.bankData[bankId].isPlaceHolder) { swar4.SelectedIndex = parent.sdat.infoFile.bankData[bankId].wave3 + 1; } else { swar4.SelectedIndex = 0; }

        }

        #endregion


        //Extract wav stuff
        #region waveButtons

        private void createTempButton_Click(object sender, EventArgs e)
        {

            //Change Directory.
            Directory.SetCurrentDirectory(nitroPath + "/Data/Temp");

            //Create Folders.
            Directory.CreateDirectory("0");
            Directory.CreateDirectory("1");
            Directory.CreateDirectory("2");
            Directory.CreateDirectory("3");


            //Extract the swavs.
            Directory.SetCurrentDirectory(nitroPath);

            //Dump swavs.
            for (int i = 0; i < 4; i++) {
                swarFile f = new swarFile();

                if (i == 0 && parent.sdat.infoFile.bankData[bankId].wave0 != 0xFFFF)
                {
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave0]);
                    for (int j = 0; j < f.data[0].files.Count(); j++) {
                        dumpSwav(i, j);
                    }
                }
                if (i == 1 && parent.sdat.infoFile.bankData[bankId].wave1 != 0xFFFF)
                {
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave1]);
                    for (int j = 0; j < f.data[0].files.Count(); j++)
                    {
                        dumpSwav(i, j);
                    }
                }
                if (i == 2 && parent.sdat.infoFile.bankData[bankId].wave2 != 0xFFFF)
                {
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave2]);
                    for (int j = 0; j < f.data[0].files.Count(); j++)
                    {
                        dumpSwav(i, j);
                    }
                }
                if (i == 3 && parent.sdat.infoFile.bankData[bankId].wave3 != 0xFFFF)
                {
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave3]);
                    for (int j = 0; j < f.data[0].files.Count(); j++)
                    {
                        dumpSwav(i, j);
                    }
                }

            }

            createTempButton.Enabled = false;
            deleteTempButton.Enabled = true;

            originalButton.Enabled = true;
            loopBox.Enabled = true;
            moddedButton.Enabled = false;
            stopButton.Enabled = true;
        }

        public void dumpSwav(int swar, int swav) {

            //See if placeholder.
            if (swar == 0) {
                if (parent.sdat.infoFile.bankData[bankId].wave0 != 0xFFFF) {
                    swarFile f = new swarFile();
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave0]);

                    File.WriteAllBytes(nitroPath + "/Data/Tools/tmp.swav", f.data[0].files[swav]);

                    //Convert.
                    Process p = new Process();
                    p.StartInfo.FileName = "Data\\Tools\\swav2wav.exe";
                    p.StartInfo.Arguments = "Data\\Tools\\tmp.swav";
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                    p.WaitForExit();

                    if (File.Exists("Data\\Temp\\0\\" + swav + ".wav")) { File.Delete("Data\\Temp\\0\\" + swav + ".wav"); }
                    File.Delete("Data\\Tools\\tmp.swav");
                    File.Move("Data\\Tools\\tmp.wav", "Data\\Temp\\0\\" + swav + ".wav");

                }
            }
            if (swar == 1)
            {
                if (parent.sdat.infoFile.bankData[bankId].wave1 != 0xFFFF)
                {
                    swarFile f = new swarFile();
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave1]);

                    File.WriteAllBytes(nitroPath + "/Data/Tools/tmp.swav", f.data[0].files[swav]);

                    //Convert.
                    Process p = new Process();
                    p.StartInfo.FileName = "Data\\Tools\\swav2wav.exe";
                    p.StartInfo.Arguments = "Data\\Tools\\tmp.swav";
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                    p.WaitForExit();

                    if (File.Exists("Data\\Temp\\1\\" + swav + ".wav")) { File.Delete("Data\\Temp\\1\\" + swav + ".wav"); }
                    File.Delete("Data\\Tools\\tmp.swav");
                    File.Move("Data\\Tools\\tmp.wav", "Data\\Temp\\1\\" + swav + ".wav");
                }
            }
            if (swar == 2)
            {
                if (parent.sdat.infoFile.bankData[bankId].wave2 != 0xFFFF)
                {
                    swarFile f = new swarFile();
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave2]);

                    File.WriteAllBytes(nitroPath + "/Data/Tools/tmp.swav", f.data[0].files[swav]);

                    //Convert.
                    Process p = new Process();
                    p.StartInfo.FileName = "Data\\Tools\\swav2wav.exe";
                    p.StartInfo.Arguments = "Data\\Tools\\tmp.swav";
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                    p.WaitForExit();

                    if (File.Exists("Data\\Temp\\2\\" + swav + ".wav")) { File.Delete("Data\\Temp\\2\\" + swav + ".wav"); }
                    File.Delete("Data\\Tools\\tmp.swav");
                    File.Move("Data\\Tools\\tmp.wav", "Data\\Temp\\2\\" + swav + ".wav");
                }
            }
            if (swar == 3)
            {
                if (parent.sdat.infoFile.bankData[bankId].wave3 != 0xFFFF)
                {
                    swarFile f = new swarFile();
                    f.load(parent.sdat.files.waveFiles[parent.sdat.infoFile.bankData[bankId].wave3]);

                    File.WriteAllBytes(nitroPath + "/Data/Tools/tmp.swav", f.data[0].files[swav]);

                    //Convert.
                    Process p = new Process();
                    p.StartInfo.FileName = "Data\\Tools\\swav2wav.exe";
                    p.StartInfo.Arguments = "Data\\Tools\\tmp.swav";
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();

                    if (File.Exists("Data\\Temp\\3\\" + swav + ".wav")) { File.Delete("Data\\Temp\\3\\" + swav + ".wav"); }
                    File.Delete("Data\\Tools\\tmp.swav");
                    File.Move("Data\\Tools\\tmp.wav", "Data\\Temp\\3\\" + swav + ".wav");
                }
            }

        }

        private void deleteTempButton_Click(object sender, EventArgs e)
        {
            //Delete everything.
            Directory.SetCurrentDirectory(nitroPath + "/Data/Temp");
            Directory.Delete("0", true);
            Directory.Delete("1", true);
            Directory.Delete("2", true);
            Directory.Delete("3", true);
            Directory.SetCurrentDirectory(nitroPath);

            createTempButton.Enabled = true;
            deleteTempButton.Enabled = false;

            originalButton.Enabled = false;
            loopBox.Enabled = false;
            moddedButton.Enabled = false;
            stopButton.Enabled = false;
        }

        #endregion


        //Sound Player Deluxe
        #region soundPlayerDeluxe

        private void moddedButton_Click(object sender, EventArgs e)
        {

        }

        private void originalButton_Click(object sender, EventArgs e)
        {
            try
            {
                player = new SoundPlayer("Data/Temp/" + emulatorInfo.swarNumber + "/" + emulatorInfo.swavNumber + ".wav");
                if (loopBox.Checked) { player.PlayLooping(); } else { player.Play(); }
            }
            catch {
                MessageBox.Show("File not found! Are you emulating the wrong bank?", "Error!");
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            player.Stop();
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
            if (tree.SelectedNode != null)
            {
                if (tree.SelectedNode.Parent != null)
                {
                    if (tree.SelectedNode.Parent.Parent == null)
                    {
                        file.data[0].records[tree.SelectedNode.Index].fRecord = (byte)fRecordBox.SelectedIndex;

                        if (file.data[0].records[tree.SelectedNode.Index].fRecord < 16 && file.data[0].records[tree.SelectedNode.Index].fRecord != 0)
                        {

                            /*
                            file.data[0].records[tree.SelectedNode.Index].instrumentA = new sbnkFile.sbnkInstrumentLessThan16();
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.swarNumber = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.swavNumber = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.attackRate = 127;
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.decayRate = 127;
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.sustainLevel = 127;
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.releaseRate = 127;
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.noteNumber = 64;
                            file.data[0].records[tree.SelectedNode.Index].instrumentA.pan = 64;
                            */

                            file.data[0].records[tree.SelectedNode.Index].isPlaceholder = false;
                            updateNodes();

                        }
                        else if (file.data[0].records[tree.SelectedNode.Index].fRecord == 0)
                        {

                            foreach (TreeNode n in tree.SelectedNode.Nodes)
                            {
                                tree.SelectedNode.Nodes.RemoveAt(0);
                            }

                            file.data[0].records[tree.SelectedNode.Index].isPlaceholder = true;

                            updateNodes();

                        }
                        else if (file.data[0].records[tree.SelectedNode.Index].fRecord == 16)
                        {

                            foreach (TreeNode n in tree.SelectedNode.Nodes)
                            {
                                tree.SelectedNode.Nodes.RemoveAt(0);
                            }

                            file.data[0].records[tree.SelectedNode.Index].isPlaceholder = false;
                            file.data[0].records[tree.SelectedNode.Index].instrumentB = new sbnkFile.sbnkInstrumentEquals16();
                            file.data[0].records[tree.SelectedNode.Index].instrumentB.lowerNote = 64;
                            file.data[0].records[tree.SelectedNode.Index].instrumentB.upperNote = 64;
                            file.data[0].records[tree.SelectedNode.Index].instrumentB.stuff = new List<sbnkFile.basicInstrumentStuff>();

                            //tree.SelectedNode.Nodes.Add("Ranged Instrument");

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

                            file.data[0].records[tree.SelectedNode.Index].instrumentB.stuff.Add(b);
                            updateNodes();

                        }
                        else
                        {

                            foreach (TreeNode n in tree.SelectedNode.Nodes)
                            {
                                tree.SelectedNode.Nodes.RemoveAt(0);
                            }

                            file.data[0].records[tree.SelectedNode.Index].isPlaceholder = false;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC = new sbnkFile.sbnkInstrumentGreaterThan16();
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region0 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region1 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region2 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region3 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region4 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region5 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region6 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.region7 = 0;
                            file.data[0].records[tree.SelectedNode.Index].instrumentC.stuff = new List<sbnkFile.basicInstrumentStuff>();

                            updateNodes();

                        }

                    }
                }
            }

        }

        //Range creation.
        private void setRangedButton_Click(object sender, EventArgs e)
        {
            //See if correct.
            if (tree.SelectedNode.Text.Contains("Ranged Instrument")) {

                file.data[0].records[tree.SelectedNode.Index].instrumentB.lowerNote = (byte)lowerNoteBoxRanged.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentB.upperNote = (byte)upperNoteBoxRanged.Value;

                file.data[0].records[tree.SelectedNode.Index].instrumentB.stuff = new List<sbnkFile.basicInstrumentStuff>();

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

                    file.data[0].records[tree.SelectedNode.Index].instrumentB.stuff.Add(b);
                }

                updateNodes();

            }

        }

        //Regional creation.
        private void createNewRegionsButton_Click(object sender, EventArgs e)
        {
            //See if correct.
            if (tree.SelectedNode.Text.Contains("Regional Instrument"))
            {

                file.data[0].records[tree.SelectedNode.Index].instrumentC.region0 = (byte)region0Box.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentC.region1 = (byte)region1Box.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentC.region2 = (byte)region2Box.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentC.region3 = (byte)region3Box.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentC.region4 = (byte)region4Box.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentC.region5 = (byte)region5Box.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentC.region6 = (byte)region6Box.Value;
                file.data[0].records[tree.SelectedNode.Index].instrumentC.region7 = (byte)region7Box.Value;

                file.data[0].records[tree.SelectedNode.Index].instrumentC.stuff = new List<sbnkFile.basicInstrumentStuff>();

                sbnkFile.sbnkInstrumentGreaterThan16 a = file.data[0].records[tree.SelectedNode.Index].instrumentC;


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

                    file.data[0].records[tree.SelectedNode.Index].instrumentC.stuff.Add(b);
                }

                updateNodes();

            }
        }

        //Update Ranged Data.
        private void updateRangedButton_Click(object sender, EventArgs e)
        {
            doEmulatorStuff();

            sbnkFile.basicInstrumentStuff f = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index];

            f.attackRate = (byte)attackRateBoxRanged.Value;
            f.decayRate = (byte)decayRateBoxRanged.Value;
            f.swarNumber = (UInt16)swarNumberBoxRanged.Value;
            f.swavNumber = (UInt16)swavNumberBoxRanged.Value;
            f.sustainLevel = (byte)sustainRateBoxRanged.Value;
            f.releaseRate = (byte)releaseRateBoxRanged.Value;
            f.pan = (byte)panBoxRanged.Value;
            f.noteNumber = (byte)noteNumberBoxRanged.Value;
            f.unknown = (byte)unknownBoxRanged.Value;

            file.data[0].records[tree.SelectedNode.Parent.Index].instrumentB.stuff[tree.SelectedNode.Index] = f;
        }

        //Update Regional Data.
        private void updateButtonRegional_Click(object sender, EventArgs e)
        {
            doEmulatorStuff();

            sbnkFile.basicInstrumentStuff f = file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index];

            f.attackRate = (byte)attackRateBoxRegional.Value;
            f.decayRate = (byte)decayRateBoxRegional.Value;
            f.swarNumber = (UInt16)swarNumberBoxRegional.Value;
            f.swavNumber = (UInt16)swavNumberBoxRegional.Value;
            f.sustainLevel = (byte)sustainRateBoxRegional.Value;
            f.releaseRate = (byte)releaseRateBoxRegional.Value;
            f.pan = (byte)panBoxRegional.Value;
            f.noteNumber = (byte)noteNumberBoxRegional.Value;
            f.unknown = (byte)unknownBoxRegional.Value;

            file.data[0].records[tree.SelectedNode.Parent.Index].instrumentC.stuff[tree.SelectedNode.Index] = f;
        }

        //Update Universal Data.
        private void updateDataUniversal_Click(object sender, EventArgs e)
        {
            doEmulatorStuff();

            if (!file.data[0].records[tree.SelectedNode.Index].isPlaceholder)
            {
                sbnkFile.sbnkInstrumentLessThan16 f = file.data[0].records[tree.SelectedNode.Index].instrumentA;

                f.attackRate = (byte)attackRateBoxUniversal.Value;
                f.decayRate = (byte)decayRateBoxUniversal.Value;
                f.swarNumber = (UInt16)swarBoxUniversal.Value;
                f.swavNumber = (UInt16)swavNumberBoxUniversal.Value;
                f.sustainLevel = (byte)sustainRateBoxUniversal.Value;
                f.releaseRate = (byte)releaseRateBoxUniversal.Value;
                f.pan = (byte)panBoxUniversal.Value;
                f.noteNumber = (byte)noteNumberBoxUniversal.Value;

                file.data[0].records[tree.SelectedNode.Index].instrumentA = f;
            }

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
            List<sbnkFile.sbnkInstrumentRecord> r = file.data[0].records.ToList();
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

            List<sbnkFile.sbnkInstrumentRecord> r = file.data[0].records.ToList();

            sbnkFile.sbnkInstrumentRecord s = new sbnkFile.sbnkInstrumentRecord();
            s.fRecord = 0;
            s.isPlaceholder = true;

            for (int i = 0; i < count; i++)
            {
                r.Insert(index, s);
            }

            file.data[0].records = r.ToArray();
            updateNodes();
        }

        private void addBelowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumberSelectionDialog d = new NumberSelectionDialog();
            int count = d.getValue();

            int parentIndex = tree.SelectedNode.Parent.Index;
            int index = tree.SelectedNode.Index;

            List<sbnkFile.sbnkInstrumentRecord> r = file.data[0].records.ToList();

            sbnkFile.sbnkInstrumentRecord s = new sbnkFile.sbnkInstrumentRecord();
            s.fRecord = 0;
            s.isPlaceholder = true;

            for (int i = 0; i < count; i++)
            {
                r.Insert(index + 1, s);
            }

            file.data[0].records = r.ToArray();
            updateNodes();
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Index != 0) {
                sbnkFile.sbnkInstrumentRecord current = file.data[0].records[tree.SelectedNode.Index];
                sbnkFile.sbnkInstrumentRecord above = file.data[0].records[tree.SelectedNode.Index - 1];

                file.data[0].records[tree.SelectedNode.Index - 1] = current;
                file.data[0].records[tree.SelectedNode.Index] = above;
                updateNodes();
            }
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Index != file.data[0].records.Count() - 1)
            {
                sbnkFile.sbnkInstrumentRecord current = file.data[0].records[tree.SelectedNode.Index];
                sbnkFile.sbnkInstrumentRecord below = file.data[0].records[tree.SelectedNode.Index + 1];

                file.data[0].records[tree.SelectedNode.Index + 1] = current;
                file.data[0].records[tree.SelectedNode.Index] = below;
                updateNodes();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nistFile n = new nistFile();
            n.loadRecord(file.data[0].records[tree.SelectedNode.Index]);

            SaveFileDialog s = new SaveFileDialog();
            s.RestoreDirectory = true;
            s.Filter = "Nitro Instrument|*.nist";
            s.FilterIndex = 1;
            s.ShowDialog();

            if (s.FileName != "") {
                File.WriteAllBytes(s.FileName, n.toBytes());
            }

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.RestoreDirectory = true;
            o.Filter = "Nitro Instrument|*.nist";
            o.FilterIndex = 1;
            o.ShowDialog();

            if (o.FileName != "")
            {
                nistFile n = new nistFile();
                n.load(File.ReadAllBytes(o.FileName));
                file.data[0].records[tree.SelectedNode.Index] = n.toRecord();
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
            List<sbnkFile.sbnkInstrumentRecord> s = file.data[0].records.ToList();
            s.RemoveAt(tree.SelectedNode.Index);
            file.data[0].records = s.ToArray();
            updateNodes();
        }

        #endregion


        //Nitro Instrument (*.nist) Specification.
        #region nitroInstrument

        public class nistFile {

            public char[] magic; //NIST.
            public byte fRecord; //Type.
            public byte isPlaceHolder; //0 No. 1 Yes.

            //Instrument - Only one of these types.
			public sbnkFile.sbnkInstrumentLessThan16 instrumentA;
			public sbnkFile.sbnkInstrumentEquals16 instrumentB;
			public sbnkFile.sbnkInstrumentGreaterThan16 instrumentC;


            //Load NIST.
            public void load(byte[] b) {

                MemoryStream src = new MemoryStream(b);
                BinaryReader br = new BinaryReader(src);

                magic = br.ReadChars(4);
                fRecord = br.ReadByte();
                isPlaceHolder = br.ReadByte();

                if (isPlaceHolder == 0 && fRecord != 0) {

                    if (fRecord < 16)
                    {

                        instrumentA = new sbnkFile.sbnkInstrumentLessThan16();
                        instrumentA.swavNumber = br.ReadUInt16();
                        instrumentA.swarNumber = br.ReadUInt16();
                        instrumentA.noteNumber = br.ReadByte();
                        instrumentA.attackRate = br.ReadByte();
                        instrumentA.decayRate = br.ReadByte();
                        instrumentA.sustainLevel = br.ReadByte();
                        instrumentA.releaseRate = br.ReadByte();
                        instrumentA.pan = br.ReadByte();

                    }
                    else if (fRecord == 16)
                    {

                        instrumentB = new sbnkFile.sbnkInstrumentEquals16();
                        instrumentB.lowerNote = br.ReadByte();
                        instrumentB.upperNote = br.ReadByte();
                        instrumentB.stuff = new List<sbnkFile.basicInstrumentStuff>();

                        //Add stuff.
                        int count = instrumentB.upperNote - instrumentB.lowerNote + 1;
                        for (int i = 0; i < count; i++) {

                            sbnkFile.basicInstrumentStuff c = new sbnkFile.basicInstrumentStuff();

                            c.unknown = br.ReadUInt16();
                            c.swavNumber = br.ReadUInt16();
                            c.swarNumber = br.ReadUInt16();
                            c.noteNumber = br.ReadByte();
                            c.attackRate = br.ReadByte();
                            c.decayRate = br.ReadByte();
                            c.sustainLevel = br.ReadByte();
                            c.releaseRate = br.ReadByte();
                            c.pan = br.ReadByte();

                            instrumentB.stuff.Add(c);

                        }

                    }
                    else {

                        instrumentC = new sbnkFile.sbnkInstrumentGreaterThan16();
                        instrumentC.region0 = br.ReadByte();
                        instrumentC.region1 = br.ReadByte();
                        instrumentC.region2 = br.ReadByte();
                        instrumentC.region3 = br.ReadByte();
                        instrumentC.region4 = br.ReadByte();
                        instrumentC.region5 = br.ReadByte();
                        instrumentC.region6 = br.ReadByte();
                        instrumentC.region7 = br.ReadByte();

                        //New list.
                        instrumentC.stuff = new List<sbnkFile.basicInstrumentStuff>();

                        //Get count.
                        int count = 0;
                        if (instrumentC.region0 == 0)
                        {
                            count = 0;
                        }
                        else
                        {
                            if (instrumentC.region1 == 0)
                            {
                                count = 1;
                            }
                            else
                            {
                                if (instrumentC.region2 == 0)
                                {
                                    count = 2;
                                }
                                else
                                {
                                    if (instrumentC.region3 == 0)
                                    {
                                        count = 3;
                                    }
                                    else
                                    {
                                        if (instrumentC.region4 == 0)
                                        {
                                            count = 4;
                                        }
                                        else
                                        {
                                            if (instrumentC.region5 == 0)
                                            {
                                                count = 5;
                                            }
                                            else
                                            {
                                                if (instrumentC.region6 == 0)
                                                {
                                                    count = 6;
                                                }
                                                else
                                                {
                                                    if (instrumentC.region7 == 0)
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
                        for (int i = 0; i < count; i++)
                        {

                            sbnkFile.basicInstrumentStuff c = new sbnkFile.basicInstrumentStuff();

                            c.unknown = br.ReadUInt16();
                            c.swavNumber = br.ReadUInt16();
                            c.swarNumber = br.ReadUInt16();
                            c.noteNumber = br.ReadByte();
                            c.attackRate = br.ReadByte();
                            c.decayRate = br.ReadByte();
                            c.sustainLevel = br.ReadByte();
                            c.releaseRate = br.ReadByte();
                            c.pan = br.ReadByte();

                            instrumentC.stuff.Add(c);

                        }

                    }

                }

            }

            //Write NIST.
            public byte[] toBytes()
            {

                MemoryStream o = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(o);

                bw.Write(magic);
                bw.Write(fRecord);
                bw.Write(isPlaceHolder);

                if (isPlaceHolder == 0 && fRecord != 0)
                {

                    if (fRecord < 16)
                    {

                        bw.Write(instrumentA.swavNumber);
                        bw.Write(instrumentA.swarNumber);
                        bw.Write(instrumentA.noteNumber);
                        bw.Write(instrumentA.attackRate);
                        bw.Write(instrumentA.decayRate);
                        bw.Write(instrumentA.sustainLevel);
                        bw.Write(instrumentA.releaseRate);
                        bw.Write(instrumentA.pan);

                    }
                    else if (fRecord == 16)
                    {

                        bw.Write(instrumentB.lowerNote);
                        bw.Write(instrumentB.upperNote);

                        //Write stuff.
                        int count = instrumentB.upperNote - instrumentB.lowerNote + 1;
                        for (int i = 0; i < count; i++)
                        {

                            bw.Write(instrumentB.stuff[i].unknown);
                            bw.Write(instrumentB.stuff[i].swavNumber);
                            bw.Write(instrumentB.stuff[i].swarNumber);
                            bw.Write(instrumentB.stuff[i].noteNumber);
                            bw.Write(instrumentB.stuff[i].attackRate);
                            bw.Write(instrumentB.stuff[i].decayRate);
                            bw.Write(instrumentB.stuff[i].sustainLevel);
                            bw.Write(instrumentB.stuff[i].releaseRate);
                            bw.Write(instrumentB.stuff[i].pan);

                        }

                    }
                    else
                    {

                        bw.Write(instrumentC.region0);
                        bw.Write(instrumentC.region1);
                        bw.Write(instrumentC.region2);
                        bw.Write(instrumentC.region3);
                        bw.Write(instrumentC.region4);
                        bw.Write(instrumentC.region5);
                        bw.Write(instrumentC.region6);
                        bw.Write(instrumentC.region7);

                        //Get count.
                        int count = 0;
                        if (instrumentC.region0 == 0)
                        {
                            count = 0;
                        }
                        else
                        {
                            if (instrumentC.region1 == 0)
                            {
                                count = 1;
                            }
                            else
                            {
                                if (instrumentC.region2 == 0)
                                {
                                    count = 2;
                                }
                                else
                                {
                                    if (instrumentC.region3 == 0)
                                    {
                                        count = 3;
                                    }
                                    else
                                    {
                                        if (instrumentC.region4 == 0)
                                        {
                                            count = 4;
                                        }
                                        else
                                        {
                                            if (instrumentC.region5 == 0)
                                            {
                                                count = 5;
                                            }
                                            else
                                            {
                                                if (instrumentC.region6 == 0)
                                                {
                                                    count = 6;
                                                }
                                                else
                                                {
                                                    if (instrumentC.region7 == 0)
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
                        for (int i = 0; i < count; i++)
                        {

                            bw.Write(instrumentC.stuff[i].unknown);
                            bw.Write(instrumentC.stuff[i].swavNumber);
                            bw.Write(instrumentC.stuff[i].swarNumber);
                            bw.Write(instrumentC.stuff[i].noteNumber);
                            bw.Write(instrumentC.stuff[i].attackRate);
                            bw.Write(instrumentC.stuff[i].decayRate);
                            bw.Write(instrumentC.stuff[i].sustainLevel);
                            bw.Write(instrumentC.stuff[i].releaseRate);
                            bw.Write(instrumentC.stuff[i].pan);

                        }

                    }

                }

                return o.ToArray();

            }

            //To Record.
            public sbnkFile.sbnkInstrumentRecord toRecord() {

                sbnkFile.sbnkInstrumentRecord r = new sbnkFile.sbnkInstrumentRecord();

                r.fRecord = fRecord;
                r.instrumentA = instrumentA;
                r.instrumentB = instrumentB;
                r.instrumentC = instrumentC;
                if (isPlaceHolder == 0) { r.isPlaceholder = false; } else { r.isPlaceholder = true; }
                r.reserved = 0;
                r.instrumentOffset = 0;

                return r;

            }

            //Load Record.
            public void loadRecord(sbnkFile.sbnkInstrumentRecord r) {

                magic = "NIST".ToCharArray();
                fRecord = r.fRecord;
                instrumentA = r.instrumentA;
                instrumentB = r.instrumentB;
                instrumentC = r.instrumentC;
                if (r.isPlaceholder) { isPlaceHolder = 1; } else { isPlaceHolder = 0; }

            }

        }

        #endregion




        private void SbnkEditor_Load(object sender, EventArgs e)
        {

        }

        private void changeInstrument_Click_1(object sender, EventArgs e)
        {
            changeInstrument_Click(sender, e);
        }

        private void resetToDefaultsUniversal_Click(object sender, EventArgs e)
        {
            sbnkFile.sbnkInstrumentLessThan16 f = file.data[0].records[tree.SelectedNode.Index].instrumentA;
            f.swarNumber = 0;
            f.swavNumber = 0;
            f.attackRate = 127;
            f.decayRate = 127;
            f.sustainLevel = 127;
            f.releaseRate = 127;
            f.noteNumber = 64;
            f.pan = 64;

            swarBoxUniversal.Value = 0;
            swavNumberBoxUniversal.Value = 0;
            attackRateBoxUniversal.Value = 127;
            decayRateBoxUniversal.Value = 127;
            sustainRateBoxUniversal.Value = 127;
            releaseRateBoxUniversal.Value = 127;
            noteNumberBoxUniversal.Value = 64;
            panBoxUniversal.Value = 64;
            file.data[0].records[tree.SelectedNode.Index].instrumentA = f;

        }
    }
}
