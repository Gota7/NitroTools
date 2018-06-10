using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SymbTool2;
using InfoTool2;
using NitroFileLoader;
using LibNitro;

namespace NitroStudio
{
    public partial class MainWindow : Form
    {

        //Init
        #region Init

        //Variable if file is open
        bool fileOpen = false;

        //Directory on file open dialogs
        string acc_path = "";
        string previousPath = "";

        //Info file and symb file path.
        string sdatPath;
        string[] fatFiles;

        //The SDAT.
        public sdatFile sdat;
        LibNitro.SND.Player.SimpleSequencePlayer sseqPlayer;

        //Application path
        public string nitroPath = Application.StartupPath;
        #endregion Init


        //Main window functions
        #region MainWindowFunctions

        public MainWindow()
        {
            InitializeComponent();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Show dialog only if file is opened.
            if (fileOpen)
            {

                SaveQuitDialog m = new SaveQuitDialog();
                m.ShowDialog();

            }
            else
            {
                Application.Exit();
            }
        }

        private void aboutNitroComposerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Make a new window popout, with about information.
            AboutWindow m = new AboutWindow();
            m.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //Load status text.
            status.Text = "Ready!";

            //Load progress bar.
            progressBar.Value = progressBar.Maximum;

        }

        /// <summary>
        /// Close the file.
        /// </summary>
        public void closeFile()
        {

            //Progress and status.
            status.Text = "Closing Files...";
            progressBar.Maximum = 2;
            progressBar.Value = 1;

            //Remove all children of nodes, and remove context menu.
            tree.BeginUpdate();
            for (int i = 0; i < tree.Nodes.Count; i++)
            {

                RemoveChildNodes(tree.Nodes[i]);
                tree.Nodes[i].ContextMenuStrip = null;

            }
            tree.EndUpdate();

            //Close File.
            fileOpen = false;

            progressBar.PerformStep();
            status.Text = "Ready!";

        }

        #endregion MainWindowFunctions



        //Open the file.
        #region OpenFile

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Dialog if you want to close file.
            SaveCloseDialog m = new SaveCloseDialog();
            int closeValue = 2;
            if (fileOpen) closeValue = m.getValue();
            Console.WriteLine(m);
            if (!fileOpen) closeValue = 1;

            //Save if m is 0.
            if (closeValue == 0) {
                save();
            }

            if (closeValue != 2) {


                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Supported Files|*.sdat;symb.bin|Nitro Sound Data|*.sdat|Symbol Binary|symb.bin";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.InitialDirectory = previousPath;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {

                                //Get previously opened path.
                                previousPath = Path.GetDirectoryName(openFileDialog1.FileName);
                                acc_path = openFileDialog1.FileName;

                                if (openFileDialog1.FilterIndex == 1) {

                                    if (openFileDialog1.FileName.EndsWith(".sdat")) { openFileDialog1.FilterIndex = 2; }
                                    if (openFileDialog1.FileName.EndsWith(".bin")) { openFileDialog1.FilterIndex = 3; }

                                }

                                if (openFileDialog1.FilterIndex == 2)
                                {

                                    //Get path.
                                    sdatPath = openFileDialog1.FileName;

                                    this.Text = "Nitro Studio - " + Path.GetFileName(sdatPath);

                                    //Get sdat.
                                    sdat = new sdatFile();
                                    sdat.load(File.ReadAllBytes(sdatPath));

                                }
                                if (openFileDialog1.FilterIndex == 3)
                                {

                                    //Get sdat from the folder.
                                    if (File.Exists(Path.GetDirectoryName(openFileDialog1.FileName) + "\\info.bin"))
                                    {
                                        this.Text = "Nitro Studio - New_File.sdat";

                                        sdatPath = Path.GetDirectoryName(openFileDialog1.FileName);
                                        sdat = new sdatFile();
                                        sdat.compress(sdatPath);
                                        sdat.fixOffsets();
                                        sdatPath = "%BLANK%";
                                    }
                                    else { sdatPath = ""; }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                } else { sdatPath = ""; }



                //Do the info and symb conversion stuff if they exist.
                if (sdatPath != "")
                {

                    //Close existing file.
                    closeFile();

                    //First control status bar.
                    progressBar.Minimum = 0;
                    progressBar.Maximum = 2;
                    progressBar.Enabled = true;
                    progressBar.Step = 1;
                    progressBar.Value = 1;

                    //Update nodes.
                    updateNodes();

                    //Load node menus.
                    for (int i = 0; i < tree.Nodes.Count - 1; i++)
                    {
                        tree.Nodes[i].ContextMenuStrip = bigNodeMenu;
                    }

                    //Open file is true.
                    fileOpen = true;

                    //Reset
                    progressBar.PerformStep();
                    status.Text = "Ready!";



                }


            }

        }

        #endregion OpenFile



        //Useless functions?
        #region UselessFunctions
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        #endregion UselessFunctions



        //Folder menus.
        #region FoldersMenuShit

        //Add ability to add files to folders.
        private void Add2_Click(object sender, EventArgs e)
        {
            //Get the new file.
            string folderName = tree.SelectedNode.Text;

            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select " + folderName + " file.";
            o.Filter = "Sequence File|*.sseq";
            if (folderName == "Sequence Archive") { o.Filter = "Sequence Archive|*.ssar"; }
            if (folderName == "Bank") { o.Filter = "Sound Bank|*.sbnk"; }
            if (folderName == "Wave Archive") { o.Filter = "Wave Archive|*.swar"; }
            if (folderName == "Stream") { o.Filter = "Stream|*.strm"; }
            o.ShowDialog();

            //If not retarded name.
            if (o.FileName != "")
            {

                if (o.FilterIndex == 1)
                {

                    //Add the file.
                    if (folderName == "Sequence") { sdat.files.sseqFiles.Add(File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Sequence Archive") { sdat.files.seqArcFiles.Add(File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Bank") { sdat.files.bankFiles.Add(File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Wave Archive") { sdat.files.waveFiles.Add(File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Stream") { sdat.files.strmFiles.Add(File.ReadAllBytes(o.FileName)); }
                    sdat.fixOffsets();



                }
                else {



                }

                //Fix the file IDs.
                if (folderName == "Sequence")
                {

                    foreach (SeqArcData s in sdat.infoFile.seqArcData)
                    {
                        s.fileId += 1;
                    }
                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId += 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                    symbStringName s2 = new symbStringName();
                    s2.isPlaceHolder = false;
                    s2.name = "New File";
                    sdat.symbFile.sseqStrings.Add(s2);

                    SseqData q = new SseqData();
                    q.bank = 0;
                    q.fileId = (UInt16)(sdat.files.sseqFiles.Count - 1);
                    q.channelPriority = 64;
                    q.isPlaceHolder = false;
                    q.playerPriority = 64;
                    q.playerNumber = 0;
                    q.unknown1 = 0;
                    q.unknown2 = 0;
                    q.volume = 100;
                    sdat.infoFile.sseqData.Add(q);

                }

                if (folderName == "Sequence Archive")
                {

                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId += 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                    symbStringName s2 = new symbStringName();
                    s2.isPlaceHolder = false;
                    s2.name = "New File";
                    sdat.symbFile.seqArcStrings.Add(s2);

                    SeqArcData q = new SeqArcData();
                    q.fileId = (UInt16)(sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count - 1);
                    q.isPlaceHolder = false;
                    sdat.infoFile.seqArcData.Add(q);

                }

                if (folderName == "Bank")
                {

                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                    symbStringName s2 = new symbStringName();
                    s2.isPlaceHolder = false;
                    s2.name = "New File";
                    sdat.symbFile.bankStrings.Add(s2);

                    BankData q = new BankData();
                    q.fileId = (UInt16)(sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count - 1);
                    q.isPlaceHolder = false;
                    q.wave0 = 0;
                    q.wave1 = 0;
                    q.wave2 = 0;
                    q.wave3 = 0;
                    sdat.infoFile.bankData.Add(q);

                }

                if (folderName == "Wave Archive")
                {

                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                    symbStringName s2 = new symbStringName();
                    s2.isPlaceHolder = false;
                    s2.name = "New File";
                    sdat.symbFile.waveStrings.Add(s2);

                    WaveData q = new WaveData();
                    q.fileId = (UInt16)(sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count + sdat.files.waveFiles.Count - 1);
                    q.isPlaceHolder = false;
                    sdat.infoFile.waveData.Add(q);

                }

                if (folderName == "Stream")
                {

                    symbStringName s2 = new symbStringName();
                    s2.isPlaceHolder = false;
                    s2.name = "New File";
                    sdat.symbFile.strmStrings.Add(s2);

                    StrmData q = new StrmData();
                    q.fileId = (UInt16)(sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count + sdat.files.waveFiles.Count + sdat.files.strmFiles.Count - 1);
                    q.isPlaceHolder = false;
                    q.player = 0;
                    q.priority = 64;
                    byte[] reserved = { 0, 0, 0, 0, 0 };
                    q.reserved = reserved;
                    q.volume = 100;
                    sdat.infoFile.strmData.Add(q);

                }

                //Update nodes.
                updateNodes();

            }

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Check if user wants to close, if file is open.
            if (fileOpen)
            {
                SaveCloseDialog m = new SaveCloseDialog();
                int result = 2;
                result = m.getValue();

                //Close the file.
                if (result == 0)
                {
                    save();
                    closeFile();
                    this.Text = "Nitro Studio";
                }
                else if (result == 1)
                {
                    closeFile();
                    this.Text = "Nitro Studio";
                }
            }
            //If no file open
            else {
                MessageBox.Show("You can't close a file with nothing open!", "Notice:");
            }

        }

        //Close a folder that is open.
        private void closeTree2_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse(false);
        }

        //Open a folder that is closed.
        private void openTree2_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        //Big FILES node stuff.
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse(false);
        }

        #endregion FoldersMenuShit



        //Save the file.
        #region Saving
        public void save() {

            if (sdatPath != "%BLANK%")
            {
                File.WriteAllBytes(sdatPath, sdat.toBytes());
            }
            else {

                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "Nitro Sound Data|*.sdat";
                s.Title = "Save sound data";
                s.ShowDialog();

                if (s.FileName != "") {

                    sdatPath = s.FileName;
                    this.Text = "Nitro Studio - " + Path.GetFileName(sdatPath);
                    File.WriteAllBytes(sdatPath, sdat.toBytes());

                }

            }

        }

        //Save As.
        public void saveAs() {

            sdatPath = "%BLANK%";
            save();

        }

        #endregion Saving



        //Shit for nodes.
        #region NodeShit


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
            else if (e.Button == MouseButtons.Left) {
                // Select the clicked node
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            }

            doInfoPanelStuff();


            //See if file is selected, if so, show byte length.
            if (tree.SelectedNode != null) {

                if (tree.SelectedNode.Parent != null) {

                    if (tree.SelectedNode.Parent.Parent != null) {

                        if (tree.SelectedNode.Parent.Parent.Index == 8)
                        {

                            if (tree.SelectedNode.Parent.Index == 0) { byteSelect.Text = sdat.files.sseqFiles[tree.SelectedNode.Index].Length + " bytes."; }
                            else if (tree.SelectedNode.Parent.Index == 1) { byteSelect.Text = sdat.files.seqArcFiles[tree.SelectedNode.Index].Length + " bytes."; }
                            else if (tree.SelectedNode.Parent.Index == 2) { byteSelect.Text = sdat.files.bankFiles[tree.SelectedNode.Index].Length + " bytes."; }
                            else if (tree.SelectedNode.Parent.Index == 3) { byteSelect.Text = sdat.files.waveFiles[tree.SelectedNode.Index].Length + " bytes."; }
                            else if (tree.SelectedNode.Parent.Index == 4) { byteSelect.Text = sdat.files.strmFiles[tree.SelectedNode.Index].Length + " bytes."; }
                            else { byteSelect.Text = "No bytes selected!"; }

                        }
                        else { byteSelect.Text = "No bytes selected!"; }

                    } else { byteSelect.Text = "No bytes selected!"; }

                } else { byteSelect.Text = "No bytes selected!"; }

            } else { byteSelect.Text = "No bytes selected!"; }

        }

        void treeArrowKey(object sender, KeyEventArgs e) {

            doInfoPanelStuff();

        }

        #endregion



        //Info panel.
        #region infoPanelStuff

        public void doInfoPanelStuff() {

            if (fileOpen)
            {
                if (tree.SelectedNode != null)
                {
                    nodeSelected.Text = "Node: " + tree.SelectedNode.Text + ";";
                    if (tree.SelectedNode.Parent != null) {
                        parentNodeSelected.Text = "Parent Node: " + tree.SelectedNode.Parent.Text;
                    }
                    else
                    {
                        parentNodeSelected.Text = "Node's parent is null!";
                    }
                }
                else
                {
                    nodeSelected.Text = "No node selected!";
                    parentNodeSelected.Text = "Node's parent is null!";
                }
            }
            else {
                nodeSelected.Text = "No node selected!";
                parentNodeSelected.Text = "Node's parent is null!";
            }

            if (tree.SelectedNode.Parent != null)
            {

                //Menus per node.
                togglePlaceholderButton.Enabled = true;

                //Double parent nodes.
                if (tree.SelectedNode.Parent.Parent != null)
                {

                    //Group
                    if (tree.SelectedNode.Parent.Parent.Index == 5)
                    {

                        //Hide panel stuff.
                        hideAllPanelStuff();

                        //Show group sub panel.
                        groupSubPanel.Show();

                        //Show type.
                        if (sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].type > 3)
                        {
                            typeGroupBox.SelectedIndex = 4;
                        }
                        else
                        {
                            typeGroupBox.SelectedIndex = sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].type;
                        }

                        //Show nEntry.
                        /*
                            0 - SSEQ
                            1 - SBNK
                            2 - SWAR
                            3 - SSAR
                            WTF - Other
                        */
                        if (typeGroupBox.SelectedIndex == 0)
                        {
                            foreach (TreeNode n in tree.Nodes[0].Nodes)
                            {
                                nEntryBox.Items.Add(n.Text);
                            }
                            nEntryBox.SelectedIndex = (int)sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].nEntry;
                            ssarBoxLoad.Enabled = false;
                            sseqBoxLoad.Enabled = true;
                            sbnkBoxLoad.Enabled = true;
                            swarBoxLoad.Enabled = true;
                        }
                        if (typeGroupBox.SelectedIndex == 1)
                        {
                            foreach (TreeNode n in tree.Nodes[2].Nodes)
                            {
                                nEntryBox.Items.Add(n.Text);
                            }
                            nEntryBox.SelectedIndex = (int)sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].nEntry;
                            ssarBoxLoad.Enabled = false;
                            sseqBoxLoad.Enabled = false;
                            sbnkBoxLoad.Enabled = true;
                            swarBoxLoad.Enabled = true;
                        }
                        if (typeGroupBox.SelectedIndex == 2)
                        {
                            foreach (TreeNode n in tree.Nodes[3].Nodes)
                            {
                                nEntryBox.Items.Add(n.Text);
                            }
                            nEntryBox.SelectedIndex = (int)sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].nEntry;
                            ssarBoxLoad.Enabled = false;
                            sseqBoxLoad.Enabled = false;
                            sbnkBoxLoad.Enabled = false;
                            swarBoxLoad.Enabled = true;
                        }
                        if (typeGroupBox.SelectedIndex == 3)
                        {
                            foreach (TreeNode n in tree.Nodes[1].Nodes)
                            {
                                nEntryBox.Items.Add(n.Text);
                            }
                            nEntryBox.SelectedIndex = (int)sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].nEntry;
                            ssarBoxLoad.Enabled = true;
                            sseqBoxLoad.Enabled = false;
                            sbnkBoxLoad.Enabled = false;
                            swarBoxLoad.Enabled = false;
                        }
                        if (typeGroupBox.SelectedIndex == 4) {
                            ssarBoxLoad.Enabled = true;
                            sseqBoxLoad.Enabled = true;
                            sbnkBoxLoad.Enabled = true;
                            swarBoxLoad.Enabled = true;
                        }

                        //Load flag.
                        int loadFlag = sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag;
                        if ((loadFlag & 0b1) == 0b1) { sseqBoxLoad.Checked = true; } else { sseqBoxLoad.Checked = false; }
                        if ((loadFlag & 0b10) == 0b10) { sbnkBoxLoad.Checked = true; } else { sbnkBoxLoad.Checked = false; }
                        if ((loadFlag & 0b100) == 0b100) { swarBoxLoad.Checked = true; } else { swarBoxLoad.Checked = false; }
                        if ((loadFlag & 0b1000) == 0b1000) { ssarBoxLoad.Checked = true; } else { ssarBoxLoad.Checked = false; }

                    }

                    //SeqArc Sub.
                    else if (tree.SelectedNode.Parent.Parent.Index == 1)
                    {

                        //Hide panel stuff.
                        hideAllPanelStuff();
                        placeHolderLayout.Show();

                        if (sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index][tree.SelectedNode.Index].isPlaceHolder)
                        {
                            placeholderBox.Checked = true;
                        }
                        else {
                            placeholderBox.Checked = false;
                        }

                    }
                    else
                    {

                        //Show no select.
                        hideAllPanelStuff();
                        noSelectLabel.Show();

                    }


                }

                //Sseq
                else if (tree.SelectedNode.Parent.Index == 0)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();

                    //Sseq.
                    sseqGroup.Show();
                    placeHolderLayout.Show();

                    //Check if placeholder.
                    if (!sdat.infoFile.sseqData[tree.SelectedNode.Index].isPlaceHolder && !sdat.symbFile.sseqStrings[tree.SelectedNode.Index].isPlaceHolder)
                    {

                        placeholderBox.Checked = false;

                        //Show Gericom.
                        gericomPause.Show();
                        gericomPlay.Show();
                        gericomStop.Show();
                        gericomLabel.Show();

                        //Add player.
                        sdat.fixOffsets();
                        try
                        {
                            if (sseqPlayer != null) sseqPlayer.Stop();
                            sseqPlayer = new LibNitro.SND.Player.SimpleSequencePlayer(new LibNitro.SND.SDAT(sdat.toBytes()), tree.SelectedNode.Index);
                        }
                        catch { MessageBox.Show("You can only play a .sseq!", "Notice:"); }

                        //FileId
                        fileIdLabel.Show();
                        for (int i = 0; i < fatFiles.Length; i++)
                        {
                            fileIdBox.Items.Add(fatFiles[i]);
                        }
                        fileIdBox.SelectedIndex = (int)sdat.infoFile.sseqData[tree.SelectedNode.Index].fileId;
                        fileIdBox.Show();

                        //Add banks.
                        bankIDbox.Items.Add("FFFF - Blank.");
                        foreach (TreeNode t in tree.Nodes[2].Nodes)
                        {

                            bankIDbox.Items.Add(t.Name);

                        }

                        if (sdat.infoFile.sseqData[tree.SelectedNode.Index].bank == (UInt16)0xFFFF)
                        {
                            bankIDbox.SelectedIndex = 0;
                        }
                        else
                        {
                            bankIDbox.SelectedIndex = (int)(sdat.infoFile.sseqData[tree.SelectedNode.Index].bank + 1);
                        }


                        //The boxes.
                        volumeSseqBox.Value = (int)sdat.infoFile.sseqData[tree.SelectedNode.Index].volume;
                        channelPrioritySseqBox.Value = (int)sdat.infoFile.sseqData[tree.SelectedNode.Index].channelPriority;
                        playerPrioritySseqBox.Value = (int)sdat.infoFile.sseqData[tree.SelectedNode.Index].playerPriority;
                        playerNumberSseqBox.Value = (int)sdat.infoFile.sseqData[tree.SelectedNode.Index].playerNumber;

                    }
                    else
                    {

                        //Hide Gericom.
                        gericomPause.Hide();
                        gericomPlay.Hide();
                        gericomStop.Hide();
                        gericomLabel.Hide();

                        //FileID.
                        fileIdLabel.Show();
                        fileIdBox.Items.Add("Null");
                        fileIdBox.Show();
                        fileIdBox.SelectedIndex = 0;

                        //Bank.
                        bankIDbox.Items.Add("Null");
                        bankIDbox.SelectedIndex = 0;

                        //The boxes.
                        volumeSseqBox.Value = 100;
                        channelPrioritySseqBox.Value = 64;
                        playerPrioritySseqBox.Value = 64;
                        playerNumberSseqBox.Value = 0;

                        placeholderBox.Checked = true;

                        

                    }



                }

                //SeqArc
                else if (tree.SelectedNode.Parent.Index == 1)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();

                    //Check if placeholder.
                    if (!sdat.infoFile.seqArcData[tree.SelectedNode.Index].isPlaceHolder && !sdat.symbFile.seqArcStrings[tree.SelectedNode.Index].isPlaceHolder)
                    {

                        placeholderBox.Checked = false;

                        //FileId
                        fileIdLabel.Show();
                        for (int i = 0; i < fatFiles.Length; i++)
                        {
                            fileIdBox.Items.Add(fatFiles[i]);
                        }
                        fileIdBox.SelectedIndex = (int)sdat.infoFile.seqArcData[tree.SelectedNode.Index].fileId;
                        fileIdBox.Show();

                    }
                    else
                    {

                        placeholderBox.Checked = true;

                        //File ID.
                        fileIdLabel.Show();
                        fileIdBox.Items.Add("Null");
                        fileIdBox.Show();
                        fileIdBox.SelectedIndex = 0;

                    }


                }

                //Bank
                else if (tree.SelectedNode.Parent.Index == 2)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();

                    //Show bank group.
                    bankGroup.Show();
                    placeHolderLayout.Show();

                    //Check if placeholder.
                    if (!sdat.infoFile.bankData[tree.SelectedNode.Index].isPlaceHolder && !sdat.symbFile.bankStrings[tree.SelectedNode.Index].isPlaceHolder)
                    {

                        placeholderBox.Checked = false;

                        //Show corresponding waves.
                        wave0Box.Items.Add("FFFF - Blank.");
                        wave1Box.Items.Add("FFFF - Blank.");
                        wave2Box.Items.Add("FFFF - Blank.");
                        wave3Box.Items.Add("FFFF - Blank.");
                        foreach (TreeNode t in tree.Nodes[3].Nodes)
                        {

                            wave0Box.Items.Add(t.Name);
                            wave1Box.Items.Add(t.Name);
                            wave2Box.Items.Add(t.Name);
                            wave3Box.Items.Add(t.Name);

                        }

                        //Set wave indexes.
                        if ((int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave0 == 0xFFFF)
                        {
                            wave0Box.SelectedIndex = 0;
                        }
                        else
                        {
                            wave0Box.SelectedIndex = (int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave0 + 1;
                        }

                        if ((int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave1 == 0xFFFF)
                        {
                            wave1Box.SelectedIndex = 0;
                        }
                        else
                        {
                            wave1Box.SelectedIndex = (int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave1 + 1;
                        }

                        if ((int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave2 == 0xFFFF)
                        {
                            wave2Box.SelectedIndex = 0;
                        }
                        else
                        {
                            wave2Box.SelectedIndex = (int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave2 + 1;
                        }

                        if ((int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave3 == 0xFFFF)
                        {
                            wave3Box.SelectedIndex = 0;
                        }
                        else
                        {
                            wave3Box.SelectedIndex = (int)sdat.infoFile.bankData[tree.SelectedNode.Index].wave3 + 1;
                        }

                        //FileId
                        fileIdLabel.Show();
                        for (int i = 0; i < fatFiles.Length; i++)
                        {
                            fileIdBox.Items.Add(fatFiles[i]);
                        }
                        fileIdBox.SelectedIndex = (int)sdat.infoFile.bankData[tree.SelectedNode.Index].fileId;
                        fileIdBox.Show();

                    }
                    else
                    {


                        placeholderBox.Checked = true;

                        //Show corresponding waves.
                        wave0Box.Items.Add("Null");
                        wave1Box.Items.Add("Null");
                        wave2Box.Items.Add("Null");
                        wave3Box.Items.Add("Null");
                        wave0Box.SelectedIndex = 0;
                        wave1Box.SelectedIndex = 0;
                        wave2Box.SelectedIndex = 0;
                        wave3Box.SelectedIndex = 0;

                        //FileId
                        fileIdLabel.Show();
                        fileIdBox.Items.Add("Null");
                        fileIdBox.SelectedIndex = 0;
                        fileIdBox.Show();

                    }



                }

                //Wave
                else if (tree.SelectedNode.Parent.Index == 3)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();
                    placeHolderLayout.Show();

                    //Check if placeholder.
                    if (!sdat.infoFile.waveData[tree.SelectedNode.Index].isPlaceHolder && !sdat.symbFile.waveStrings[tree.SelectedNode.Index].isPlaceHolder)
                    {
                        placeholderBox.Checked = false;

                        //FileId
                        fileIdLabel.Show();
                        for (int i = 0; i < fatFiles.Length; i++)
                        {
                            fileIdBox.Items.Add(fatFiles[i]);
                        }
                        fileIdBox.SelectedIndex = (int)sdat.infoFile.waveData[tree.SelectedNode.Index].fileId;
                        fileIdBox.Show();

                    }
                    else
                    {

                        placeholderBox.Checked = true;

                        //FileId
                        fileIdLabel.Show();
                        fileIdBox.Items.Add("Null");
                        fileIdBox.SelectedIndex = 0;
                        fileIdBox.Show();

                    }


                }

                //Player
                else if (tree.SelectedNode.Parent.Index == 4)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();

                    //Show player panel.
                    playerGroup.Show();
                    placeHolderLayout.Show();

                    //Check if placeholder.
                    if (!sdat.infoFile.playerData[tree.SelectedNode.Index].isPlaceHolder && !sdat.symbFile.playerStrings[tree.SelectedNode.Index].isPlaceHolder)
                    {

                        placeholderBox.Checked = false;

                        //Update player viewer.
                        sequenceMaxBox.Value = sdat.infoFile.playerData[tree.SelectedNode.Index].seqMax;
                        channelFlagBox.Value = sdat.infoFile.playerData[tree.SelectedNode.Index].channelFlag;
                        heapSizeBox.Value = sdat.infoFile.playerData[tree.SelectedNode.Index].heapSize;

                    }
                    else
                    {

                        placeholderBox.Checked = true;

                        //Update player viewer.
                        sequenceMaxBox.Value = 0;
                        channelFlagBox.Value = 0;
                        heapSizeBox.Value = 0;

                    }


                }

                //Group
                else if (tree.SelectedNode.Parent.Index == 5)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();
                    placeHolderLayout.Show();

                    //Check if placeholder.
                    if (!sdat.infoFile.groupData[tree.SelectedNode.Index].isPlaceHolder)
                    {

                        placeholderBox.Checked = false;

                    }
                    else
                    {

                        placeholderBox.Checked = true;

                    }
                }

                //Player2
                else if (tree.SelectedNode.Parent.Index == 6)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();

                    //Show player2group.
                    player2Group.Show();
                    placeHolderLayout.Show();

                    //Check if placeholder.
                    if (!sdat.infoFile.player2Data[tree.SelectedNode.Index].isPlaceHolder && !sdat.symbFile.player2Strings[tree.SelectedNode.Index].isPlaceHolder)
                    {

                        placeholderBox.Checked = false;
                        typeBox.Items.Clear();
                        typeBox.Items.Add("Mono");
                        typeBox.Items.Add("Stereo");
                        channel0Box.Enabled = true;
                        channel0.Enabled = true;

                        //Set values.
                        if (sdat.infoFile.player2Data[tree.SelectedNode.Index].count == 1)
                        {
                            typeBox.SelectedIndex = 0;
                            channel0.Text = "Channel:";
                            channel1.Text = "NULL:";
                            channel0Box.Value = sdat.infoFile.player2Data[tree.SelectedNode.Index].v0;
                            channel1Box.Value = sdat.infoFile.player2Data[tree.SelectedNode.Index].v1;
                            channel1.Enabled = false;
                            channel1Box.Enabled = false;
                        }
                        else {
                            typeBox.SelectedIndex = 1;
                            channel0.Text = "Left Channel:";
                            channel1.Text = "Right Channel:";
                            channel0Box.Value = sdat.infoFile.player2Data[tree.SelectedNode.Index].v0;
                            channel1Box.Value = sdat.infoFile.player2Data[tree.SelectedNode.Index].v1;
                            channel1.Enabled = true;
                            channel1Box.Enabled = true;
                        }
                    }
                    else {

                        placeholderBox.Checked = true;
                        typeBox.Items.Clear();
                        typeBox.Items.Add("NULL");
                        typeBox.SelectedIndex = 0;

                        //Set values.
                        channel0Box.Enabled = false;
                        channel0.Enabled = false;
                        channel0.Text = "NULL";
                        channel0Box.Value = 0xFF;
                        channel1Box.Value = 0xFF;

                    }
                }

                //Strm
                else if (tree.SelectedNode.Parent.Index == 7)
                {

                    //Hide panel stuff.
                    hideAllPanelStuff();
                    placeHolderLayout.Show();

                    //Show strm group.
                    strmGroup.Show();

                    //Show fileID.
                    fileIdLabel.Show();
                    fileIdBox.Show();
                    fileIdBox.BringToFront();

                    //Check if placeholder.
                    if (!sdat.infoFile.strmData[tree.SelectedNode.Index].isPlaceHolder && !sdat.symbFile.strmStrings[tree.SelectedNode.Index].isPlaceHolder)
                    {

                        placeholderBox.Checked = false;

                        for (int i = 0; i < fatFiles.Length; i++)
                        {
                            fileIdBox.Items.Add(fatFiles[i]);
                        }
                        fileIdBox.SelectedIndex = (int)sdat.infoFile.strmData[tree.SelectedNode.Index].fileId;
                        fileIdBox.Show();
                        
                        //Show the stuff.
                        volumeBoxMushrooms.Value = (int)sdat.infoFile.strmData[tree.SelectedNode.Index].volume;
                        priorityBoxBlack.Value = (int)sdat.infoFile.strmData[tree.SelectedNode.Index].priority;
                        playerBoxMagic.Value = (int)sdat.infoFile.strmData[tree.SelectedNode.Index].player;

                    }
                    else {

                        placeholderBox.Checked = true;

                        fileIdBox.Items.Add("Null");
                        fileIdBox.SelectedIndex = 0;
                        fileIdBox.Show();

                        //Show the stuff.
                        volumeBoxMushrooms.Value = 0;
                        priorityBoxBlack.Value = 0;
                        playerBoxMagic.Value = 0;

                    }

                }


                else
                {

                    //Show no select.
                    hideAllPanelStuff();
                    noSelectLabel.Show();

                }
            }

            else
            {

                //Show no select.
                hideAllPanelStuff();
                noSelectLabel.Show();

            }

        }

        //Wipe clean the panel.
        public void hideAllPanelStuff()
        {

            noSelectLabel.Hide();
            fileIdBox.Hide();
            fileIdLabel.Hide();
            fileIdBox.Enabled = true;
            fileIdBox.Items.Clear();
            bankGroup.Hide();
            placeHolderLayout.Hide();
            placeHolderLayout.Hide();
            placeholderBox.Checked = false;
            wave0Box.Items.Clear();
            wave1Box.Items.Clear();
            wave2Box.Items.Clear();
            wave3Box.Items.Clear();
            sseqGroup.Hide();
            bankIDbox.Items.Clear();
            playerGroup.Hide();
            groupSubPanel.Hide();
            nEntryBox.Items.Clear();
            player2Group.Hide();
            strmGroup.Hide();

        }

        //Toggle placeholders.
        private void togglePlaceholderButton_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Parent != null && placeholderBox.Checked == false)
            {

                if (tree.SelectedNode.Parent.Parent != null)
                {
                    if (tree.SelectedNode.Parent.Parent.Index == 1)
                    {
                        sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index][tree.SelectedNode.Index].isPlaceHolder = true;
                    }
                }
                else
                {

                    //Sseq
                    if (tree.SelectedNode.Parent.Index == 0)
                    {
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.sseqStrings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }

                    //SeqArc
                    if (tree.SelectedNode.Parent.Index == 1)
                    {
                        sdat.infoFile.seqArcData[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.seqArcStrings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }

                    //Bank
                    if (tree.SelectedNode.Parent.Index == 2)
                    {
                        sdat.infoFile.bankData[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.bankStrings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }

                    //Wave
                    if (tree.SelectedNode.Parent.Index == 3)
                    {
                        sdat.infoFile.waveData[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.waveStrings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }

                    //Player
                    if (tree.SelectedNode.Parent.Index == 4)
                    {
                        sdat.infoFile.playerData[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.playerStrings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }

                    //Group
                    if (tree.SelectedNode.Parent.Index == 5)
                    {
                        sdat.infoFile.groupData[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.groupStrings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }

                    //Player2
                    if (tree.SelectedNode.Parent.Index == 6)
                    {
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.player2Strings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }

                    //Stream
                    if (tree.SelectedNode.Parent.Index == 7)
                    {
                        sdat.infoFile.strmData[tree.SelectedNode.Index].isPlaceHolder = true;
                        sdat.symbFile.strmStrings[tree.SelectedNode.Index].isPlaceHolder = true;
                    }
                }

                updateNodes();

            } else if (tree.SelectedNode.Parent != null && placeholderBox.Checked == true)
            {

                if (tree.SelectedNode.Parent.Parent != null)
                {
                    if (tree.SelectedNode.Parent.Parent.Index == 1)
                    {
                        sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index][tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index][tree.SelectedNode.Index].name = "Unknown_Name";
                    }
                }
                else
                {

                    //Sseq
                    if (tree.SelectedNode.Parent.Index == 0)
                    {
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.sseqStrings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.sseqStrings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.sseqData[tree.SelectedNode.Index].bank = 0;
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].channelPriority = 64;
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].fileId = 0;
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].playerNumber = 0;
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].playerPriority = 64;
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].volume = 100;
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].unknown1 = 0;
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].unknown2 = 0;
                    }

                    //SeqArc
                    if (tree.SelectedNode.Parent.Index == 1)
                    {
                        sdat.infoFile.seqArcData[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.seqArcStrings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.seqArcStrings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.seqArcData[tree.SelectedNode.Index].fileId = 0;
                    }

                    //Bank
                    if (tree.SelectedNode.Parent.Index == 2)
                    {
                        sdat.infoFile.bankData[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.bankStrings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.bankStrings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.bankData[tree.SelectedNode.Index].wave0 = 0;
                        sdat.infoFile.bankData[tree.SelectedNode.Index].wave1 = 0;
                        sdat.infoFile.bankData[tree.SelectedNode.Index].wave2 = 0;
                        sdat.infoFile.bankData[tree.SelectedNode.Index].wave3 = 0;
                        sdat.infoFile.bankData[tree.SelectedNode.Index].fileId = 0;
                    }

                    //Wave
                    if (tree.SelectedNode.Parent.Index == 3)
                    {
                        sdat.infoFile.waveData[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.waveStrings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.waveStrings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.waveData[tree.SelectedNode.Index].fileId = 0;
                    }

                    //Player
                    if (tree.SelectedNode.Parent.Index == 4)
                    {
                        sdat.infoFile.playerData[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.playerStrings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.playerStrings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.playerData[tree.SelectedNode.Index].channelFlag = 0;
                        sdat.infoFile.playerData[tree.SelectedNode.Index].heapSize = 0;
                        sdat.infoFile.playerData[tree.SelectedNode.Index].seqMax = 0;

                    }

                    //Group
                    if (tree.SelectedNode.Parent.Index == 5)
                    {
                        sdat.infoFile.groupData[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.groupStrings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.groupStrings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.groupData[tree.SelectedNode.Index].count = 0;
                        sdat.infoFile.groupData[tree.SelectedNode.Index].subInfo = new List<GroupSubData>();
                    }

                    //Player2
                    if (tree.SelectedNode.Parent.Index == 6)
                    {
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.player2Strings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.player2Strings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.player2Data[tree.SelectedNode.Index].count = 1;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v0 = 0;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v1 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v2 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v3 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v4 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v5 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v6 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v7 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v8 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v9 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v10 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v11 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v12 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v13 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v14 = 255;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v15 = 255;
                    }

                    //Stream
                    if (tree.SelectedNode.Parent.Index == 7)
                    {
                        sdat.infoFile.strmData[tree.SelectedNode.Index].isPlaceHolder = false;
                        sdat.symbFile.strmStrings[tree.SelectedNode.Index].isPlaceHolder = false;

                        sdat.symbFile.strmStrings[tree.SelectedNode.Index].name = "Unknown_Name";

                        sdat.infoFile.strmData[tree.SelectedNode.Index].fileId = 0;
                        sdat.infoFile.strmData[tree.SelectedNode.Index].player = 0;
                        sdat.infoFile.strmData[tree.SelectedNode.Index].priority = 64;
                        sdat.infoFile.strmData[tree.SelectedNode.Index].volume = 100;
                    }
                }

                updateNodes();

            }
        }

        /// <summary>
        /// Placeholder stuff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onCheckedChange(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent != null)
            {

                //Sseq
                if (tree.SelectedNode.Parent.Index == 0)
                {
                    sdat.infoFile.sseqData[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

                //SeqArc
                if (tree.SelectedNode.Parent.Index == 1)
                {
                    sdat.infoFile.seqArcData[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

                //Bank
                if (tree.SelectedNode.Parent.Index == 2)
                {
                    sdat.infoFile.bankData[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

                //Wave
                if (tree.SelectedNode.Parent.Index == 3)
                {
                    sdat.infoFile.waveData[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

                //Player
                if (tree.SelectedNode.Parent.Index == 4)
                {
                    sdat.infoFile.playerData[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

                //Group
                if (tree.SelectedNode.Parent.Index == 5)
                {
                    sdat.infoFile.groupData[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

                //Player2
                if (tree.SelectedNode.Parent.Index == 6)
                {
                    sdat.infoFile.player2Data[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

                //Stream
                if (tree.SelectedNode.Parent.Index == 7)
                {
                    sdat.infoFile.strmData[tree.SelectedNode.Index].isPlaceHolder = placeholderBox.Checked;
                }

            }
            

        }


        public void onTypeChanged(object sender, EventArgs e) {
            /*
                            0 - SSEQ
                            1 - SBNK
                            2 - SWAR
                            3 - SSAR
                            WTF - Other
            */
            if (tree.SelectedNode.Parent != null && tree.SelectedNode.Parent.Parent != null)
            {
                if (tree.SelectedNode.Parent.Parent.Index == 5)
                {
                    sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].type = (byte)typeGroupBox.SelectedIndex;

                    int imageIndex = 0;
                    int flags = sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag;

                    switch (sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].type) {

                        case 0:
                            imageIndex = 0;
                            if ((flags & 0b1000) == 0b1000) { flags -= 0b1000; }
                            break;

                        case 1:
                            imageIndex = 2;
                            if ((flags & 0b1000) == 0b1000) { flags -= 0b1000; }
                            if ((flags & 0b1) == 0b1) { flags -= 0b1; }
                            break;

                        case 2:
                            imageIndex = 3;
                            if ((flags & 0b1000) == 0b1000) { flags -= 0b1000; }
                            if ((flags & 0b1) == 0b1) { flags -= 0b1; }
                            if ((flags & 0b10) == 0b10) { flags -= 0b10; }
                            break;

                        case 3:
                            imageIndex = 1;
                            if ((flags & 0b100) == 0b100) { flags -= 0b100; }
                            if ((flags & 0b1) == 0b1) { flags -= 0b1; }
                            if ((flags & 0b10) == 0b10) { flags -= 0b10; }
                            break;

                        case 4:
                            imageIndex = 9;
                            break;

                    }
                    sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag = (byte)flags;

                    tree.Nodes[5].Nodes[tree.SelectedNode.Parent.Index].Nodes[tree.SelectedNode.Index].ImageIndex = imageIndex;
                    tree.Nodes[5].Nodes[tree.SelectedNode.Parent.Index].Nodes[tree.SelectedNode.Index].SelectedImageIndex = imageIndex;
                }
            }

        }

        public void onNEntryChanged(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent != null && tree.SelectedNode.Parent.Parent != null)
            {
                if (tree.SelectedNode.Parent.Parent.Index == 5)
                {
                    sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].nEntry = (UInt32)nEntryBox.SelectedIndex;
                }
            }
        }

        public void onSequenceMaxChanged(object sender, EventArgs e) {

            if (tree.SelectedNode.Parent != null)
            {
                if (tree.SelectedNode.Parent.Index == 4)
                {
                    sdat.infoFile.playerData[tree.SelectedNode.Index].seqMax = (UInt16)sequenceMaxBox.Value;
                }
            }
        }

        public void onChannelFlagChanged(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent != null)
            {
                if (tree.SelectedNode.Parent.Index == 4)
                {
                    sdat.infoFile.playerData[tree.SelectedNode.Index].channelFlag = (UInt16)channelFlagBox.Value;
                }
            }
        }

        public void onheapSizeChanged(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent != null)
            {
                if (tree.SelectedNode.Parent.Index == 4)
                {
                    sdat.infoFile.playerData[tree.SelectedNode.Index].heapSize = (UInt32)heapSizeBox.Value;
                }
            }
        }


        /// <summary>
        /// On changed bank ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onBankIdChanged(object sender, EventArgs e) {

            if (tree.SelectedNode.Parent != null)
            {

                //Sseq
                if (tree.SelectedNode.Parent.Index == 0)
                {

                    if (bankIDbox.SelectedIndex == 0)
                    {
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].bank = (UInt16)0xFFFF;
                    }
                    else
                    {
                        sdat.infoFile.sseqData[tree.SelectedNode.Index].bank = (UInt16)(bankIDbox.SelectedIndex - 1);
                    }

                    //Add player.
                    if (!sdat.infoFile.sseqData[tree.SelectedNode.Index].isPlaceHolder)
                    {
                        try
                        {
                            sdat.fixOffsets();
                            if (sseqPlayer != null) sseqPlayer.Stop();
                            sseqPlayer = new LibNitro.SND.Player.SimpleSequencePlayer(new LibNitro.SND.SDAT(sdat.toBytes()), tree.SelectedNode.Index);
                        }
                        catch { }
                    }

                }


            }

        }

        /// <summary>
        /// If volume changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onVolumeChanged(object sender, EventArgs e) {

            if (tree.SelectedNode.Parent != null)
            {

                //Sseq
                if (tree.SelectedNode.Parent.Index == 0)
                {

                    sdat.infoFile.sseqData[tree.SelectedNode.Index].volume = (byte)volumeSseqBox.Value;

                }


            }

        }

        public void onChannelPriorityChanged(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent != null)
            {

                //Sseq
                if (tree.SelectedNode.Parent.Index == 0)
                {

                    sdat.infoFile.sseqData[tree.SelectedNode.Index].channelPriority = (byte)channelPrioritySseqBox.Value;

                }


            }

        }

        public void onPlayerPriorityChanged(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent != null)
            {

                //Sseq
                if (tree.SelectedNode.Parent.Index == 0)
                {

                    sdat.infoFile.sseqData[tree.SelectedNode.Index].playerPriority = (byte)playerPrioritySseqBox.Value;

                }


            }

        }

        public void onPlayerNumberChanged(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent != null)
            {

                //Sseq
                if (tree.SelectedNode.Parent.Index == 0)
                {

                    sdat.infoFile.sseqData[tree.SelectedNode.Index].playerNumber = (byte)playerNumberSseqBox.Value;

                }


            }

        }





        /// <summary>
        /// On file ID change.
        /// </summary>
        public void onFileIdChange(object sender, EventArgs e) {

            //Sseq
            if (tree.SelectedNode.Parent.Index == 0) {
                sdat.infoFile.sseqData[tree.SelectedNode.Index].fileId = (UInt32)fileIdBox.SelectedIndex;

                //Add player.
                if (!sdat.infoFile.sseqData[tree.SelectedNode.Index].isPlaceHolder)
                {
                    try
                    {
                        sdat.fixOffsets();
                        if (sseqPlayer != null) sseqPlayer.Stop();
                        sseqPlayer = new LibNitro.SND.Player.SimpleSequencePlayer(new LibNitro.SND.SDAT(sdat.toBytes()), tree.SelectedNode.Index);
                    }
                    catch { }
                }
            }

            //SeqArc
            else if (tree.SelectedNode.Parent.Index == 1) {
                sdat.infoFile.seqArcData[tree.SelectedNode.Index].fileId = (UInt32)fileIdBox.SelectedIndex;
            }

            //Bank
            else if (tree.SelectedNode.Parent.Index == 2)
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].fileId = (UInt32)fileIdBox.SelectedIndex;

            }

            //Wave
            else if (tree.SelectedNode.Parent.Index == 3)
            {
                sdat.infoFile.waveData[tree.SelectedNode.Index].fileId = (UInt32)fileIdBox.SelectedIndex;
            }

            //Stream
            else if (tree.SelectedNode.Parent.Index == 7)
            {
                sdat.infoFile.strmData[tree.SelectedNode.Index].fileId = (UInt32)fileIdBox.SelectedIndex;
            }

        }





        //Update the Bank Wave ID.
        public void wave0updated(object sender, EventArgs e) {
            if (wave0Box.SelectedIndex == 0)
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave0 = (UInt16)0xFFFF;
            }
            else {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave0 = (UInt16)(wave0Box.SelectedIndex - 1);
            }
        }
        public void wave1updated(object sender, EventArgs e)
        {
            if (wave1Box.SelectedIndex == 0)
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave1 = (UInt16)0xFFFF;
            }
            else
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave1 = (UInt16)(wave1Box.SelectedIndex - 1);
            }
        }
        public void wave2updated(object sender, EventArgs e)
        {
            if (wave2Box.SelectedIndex == 0)
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave2 = (UInt16)0xFFFF;
            }
            else
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave2 = (UInt16)(wave2Box.SelectedIndex - 1);
            }
        }
        public void wave3updated(object sender, EventArgs e)
        {
            if (wave3Box.SelectedIndex == 0)
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave3 = (UInt16)0xFFFF;
            }
            else
            {
                sdat.infoFile.bankData[tree.SelectedNode.Index].wave3 = (UInt16)(wave3Box.SelectedIndex - 1);
            }
        }

        private void priorityLabelPig_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void v15_ValueChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Parent != null)
            {
                if (tree.SelectedNode.Parent.Index == 6)
                {
                    sdat.infoFile.player2Data[tree.SelectedNode.Index].v0 = (byte)channel0Box.Value;
                }
            }
        }

        private void count_ValueChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Parent != null)
            {
                if (tree.SelectedNode.Parent.Index == 6)
                {
                    if (sdat.infoFile.player2Data[tree.SelectedNode.Index].count > 1)
                    {
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v1 = (byte)channel1Box.Value;
                    }
                    else {
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v1 = 0xFF;
                    }
                }
            }
        }



        private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Parent != null)
            {
                if (tree.SelectedNode.Parent.Index == 6)
                {
                    sdat.infoFile.player2Data[tree.SelectedNode.Index].count = (byte)(typeBox.SelectedIndex + 1);
                    if (typeBox.SelectedIndex == 0)
                    {
                        //sdat.infoFile.player2Data[tree.SelectedNode.Index].v0 = (byte)channel0Box.Value;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v1 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v2 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v3 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v4 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v5 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v6 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v7 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v8 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v9 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v10 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v11 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v12 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v13 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v14 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v15 = 0xFF;
                        channel0.Text = "Channel:";
                        channel1.Text = "NULL:";
                        channel1Box.Value = 0xFF;
                        channel1Box.Enabled = false;
                        channel1.Enabled = false;
                    }
                    else
                    {
                        //sdat.infoFile.player2Data[tree.SelectedNode.Index].v0 = (byte)channel0Box.Value;
                        if (sdat.infoFile.player2Data[tree.SelectedNode.Index].v1 == 0xFF) { sdat.infoFile.player2Data[tree.SelectedNode.Index].v1 = (byte)(sdat.infoFile.player2Data[tree.SelectedNode.Index].v0+1); channel1Box.Value = sdat.infoFile.player2Data[tree.SelectedNode.Index].v1; }
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v2 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v3 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v4 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v5 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v6 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v7 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v8 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v9 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v10 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v11 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v12 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v13 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v14 = 0xFF;
                        sdat.infoFile.player2Data[tree.SelectedNode.Index].v15 = 0xFF;
                        channel0.Text = "Left Channel:";
                        channel1.Text = "Right Channel:";
                        channel1Box.Enabled = true;
                        channel1.Enabled = true;
                    }
                }
            }
        }

        //SWAR Load.
        private void swarBoxLoad_CheckedChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNode != null)
            {
                if (tree.SelectedNode.Parent != null)
                {
                    if (tree.SelectedNode.Parent.Parent != null)
                    {
                        if (swarBoxLoad.Checked)
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b100) != 0b100) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag += 0b100; }
                        }
                        else
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b100) == 0b100) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag -= 0b100; }
                        }
                    }
                }
            }
        }

        //SBNK Load.
        private void sbnkBoxLoad_CheckedChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNode != null)
            {
                if (tree.SelectedNode.Parent != null)
                {
                    if (tree.SelectedNode.Parent.Parent != null)
                    {
                        if (sbnkBoxLoad.Checked)
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b10) != 0b10) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag += 0b10; }
                        }
                        else
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b10) == 0b10) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag -= 0b10; }
                        }
                    }
                }
            }
        }

        //SSEQ Load.
        private void sseqBoxLoad_CheckedChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNode != null)
            {
                if (tree.SelectedNode.Parent != null)
                {
                    if (tree.SelectedNode.Parent.Parent != null)
                    {
                        if (sseqBoxLoad.Checked)
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b1) != 0b1) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag += 0b1; }
                        }
                        else
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b1) == 0b1) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag -= 0b1; }
                        }
                    }
                }
            }
        }

        //SSAR Load.
        private void ssarBoxLoad_CheckedChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNode != null)
            {
                if (tree.SelectedNode.Parent != null)
                {
                    if (tree.SelectedNode.Parent.Parent != null)
                    {
                        if (ssarBoxLoad.Checked)
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b1000) != 0b1000) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag += 0b1000; }
                        }
                        else
                        {
                            if ((sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag & 0b1000) == 0b1000) { sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo[tree.SelectedNode.Index].loadFlag -= 0b1000; }
                        }
                    }
                }
            }
        }

        #endregion



        //More node stuff
        #region moreNodesStuff

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




        private void RemoveChildNodes(TreeNode aNode)
        {
            if (aNode.Nodes.Count > 0)
            {
                for (int i = aNode.Nodes.Count - 1; i >= 0; i--)
                {
                    aNode.Nodes[i].Remove();
                }
            }

        }

        //Update nodes to show from info binary, then rename them from symbol dictionary.
        public void updateNodes() {

            //Update sdat offsets.
            sdat.fixOffsets();

            //Get nodes that are currently expanded.
            List<string> expandedNodes = collectExpandedNodes(tree.Nodes);

            //First remove all nodes.
            tree.SelectedNode = tree.Nodes[0];
            tree.BeginUpdate();
            tree.SelectedNode = tree.Nodes[0];
            for (int i = 0; i < tree.Nodes.Count; i++)
            {

                RemoveChildNodes(tree.Nodes[i]);

            }

            //Add the folder nodes.
            tree.Nodes[8].Nodes.Add("Sequence", "Sequence", 8, 8);
            tree.Nodes[8].Nodes.Add("Sequence Archive", "Sequence Archive", 8, 8);
            tree.Nodes[8].Nodes.Add("Bank", "Bank", 8, 8);
            tree.Nodes[8].Nodes.Add("Wave Archive", "Wave Archive", 8, 8);
            tree.Nodes[8].Nodes.Add("Stream", "Stream", 8, 8);

            //Add context menus of node.
            for (int i = 0; i < tree.Nodes[8].Nodes.Count; i++) {
                tree.Nodes[8].Nodes[i].ContextMenuStrip = foldersMenu;
            }


            //Add the FAT files.
            int fileId = 0;

            //FIX FILE LOADING!!!!!
            List<string> files = new List<string>();
            for (int i = 0; i < sdat.files.sseqFiles.Count; i++) {

                tree.Nodes[8].Nodes[0].Nodes.Add("[" + fileId + "] " + "UNKNOWN_NAME.sseq", "[" + fileId + "] " +"UNKNOWN_NAME.sseq", 0, 0);
                files.Add("[" + fileId + "] " + "UNKNOWN_NAME.sseq");
                
                for (int j = 0; j < sdat.infoFile.sseqData.Count; j++) {
                    if ((int)sdat.infoFile.sseqData[j].fileId == i) {
                        tree.Nodes[8].Nodes[0].Nodes[i].Text = "[" + fileId + "] " + sdat.symbFile.sseqStrings[j].name + ".sseq";
                        files[fileId] = "[" + fileId + "] " + sdat.symbFile.sseqStrings[j].name + ".sseq";
                        break;
                    }
                }
                fileId++;

            }
            for (int i = 0; i < sdat.files.seqArcFiles.Count; i++)
            {

                tree.Nodes[8].Nodes[1].Nodes.Add("[" + fileId + "] " + "UNKNOWN_NAME.ssar", "[" + fileId + "] " + "UNKNOWN_NAME.ssar", 1, 1);
                files.Add("[" + fileId + "] " + "UNKNOWN_NAME.ssar");

                for (int j = 0; j < sdat.infoFile.seqArcData.Count; j++)
                {
                    if ((int)sdat.infoFile.seqArcData[j].fileId == i + sdat.files.sseqFiles.Count)
                    {
                        tree.Nodes[8].Nodes[1].Nodes[i].Text = "[" + fileId + "] " + sdat.symbFile.seqArcStrings[j].name + ".ssar";
                        files[fileId] = "[" + fileId + "] " + sdat.symbFile.seqArcStrings[j].name + ".ssar";
                        break;
                    }
                }
                fileId++;

            }
            for (int i = 0; i < sdat.files.bankFiles.Count; i++)
            {

                tree.Nodes[8].Nodes[2].Nodes.Add("[" + fileId + "] " + "UNKNOWN_NAME.sbnk", "[" + fileId + "] " + "UNKNOWN_NAME.sbnk", 2, 2);
                files.Add("[" + fileId + "] " + "UNKNOWN_NAME.sbnk");

                for (int j = 0; j < sdat.infoFile.bankData.Count; j++)
                {
                    if ((int)sdat.infoFile.bankData[j].fileId == i + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count)
                    {
                        tree.Nodes[8].Nodes[2].Nodes[i].Text = "[" + fileId + "] " + sdat.symbFile.bankStrings[j].name + ".sbnk";
                        files[fileId] = "[" + fileId + "] " + sdat.symbFile.bankStrings[j].name + ".sbnk";
                        break;
                    }
                }
                fileId++;

            }
            for (int i = 0; i < sdat.files.waveFiles.Count; i++)
            {

                tree.Nodes[8].Nodes[3].Nodes.Add("[" + fileId + "] " + "UNKNOWN_NAME.swar", "[" + fileId + "] " + "UNKNOWN_NAME.swar", 3, 3);
                files.Add("[" + fileId + "] " + "UNKNOWN_NAME.swar");

                for (int j = 0; j < sdat.infoFile.waveData.Count; j++)
                {
                    if ((int)sdat.infoFile.waveData[j].fileId == i + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count)
                    {
                        tree.Nodes[8].Nodes[3].Nodes[i].Text = "[" + fileId + "] " + sdat.symbFile.waveStrings[j].name + ".swar";
                        files[fileId] = "[" + fileId + "] " + sdat.symbFile.waveStrings[j].name + ".swar";
                        break;
                    }
                }
                fileId++;

            }
            for (int i = 0; i < sdat.files.strmFiles.Count; i++)
            {

                tree.Nodes[8].Nodes[4].Nodes.Add("[" + fileId + "] " + "UNKNOWN_NAME.strm", "[" + fileId + "] " + "UNKNOWN_NAME.strm", 7, 7);
                files.Add("[" + fileId + "] " + "UNKNOWN_NAME.strm");

                for (int j = 0; j < sdat.infoFile.strmData.Count; j++)
                {
                    if ((int)sdat.infoFile.strmData[j].fileId == i + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count + sdat.files.waveFiles.Count)
                    {
                        tree.Nodes[8].Nodes[4].Nodes[i].Text = "[" + fileId + "] " + sdat.symbFile.strmStrings[j].name + ".strm";
                        files[fileId] = "[" + fileId + "] " + sdat.symbFile.strmStrings[j].name + ".strm";
                        break;
                    }
                }
                fileId++;

            }
            fatFiles = files.ToArray();

            /*
            List<string> files = new List<string>();
            for (int i = 0; i < sdat.files.files.Count(); i++) {

                //Get correct info, sseq.
                for (int j = 0; j < sdat.infoFile.sseqData.Count(); j++)
                {
                    if ((int)sdat.infoFile.sseqData[j].fileId == i)
                    {
                        tree.Nodes[8].Nodes[0].Nodes.Add("[" + fileId + "] " + sdat.symbFile.sseqStrings[j].name + ".sseq", "[" + fileId + "] " + sdat.symbFile.sseqStrings[j].name + ".sseq", 0, 0);
                        files.Add("[" + fileId + "] " + sdat.symbFile.sseqStrings[j].name + ".sseq");
                        fileId++;
                        break;
                    }
                }

                //SeqArc
                for (int j = 0; j < sdat.infoFile.seqArcData.Count(); j++)
                {
                    if ((int)sdat.infoFile.seqArcData[j].fileId == i)
                    {
                        tree.Nodes[8].Nodes[1].Nodes.Add("[" + fileId + "] " + sdat.symbFile.seqArcStrings[j].name + ".ssar", "[" + fileId + "] " + sdat.symbFile.seqArcStrings[j].name + ".ssar", 1, 1);
                        files.Add("[" + fileId + "] " + sdat.symbFile.seqArcStrings[j].name + ".ssar");
                        fileId++;
                        break;
                    }
                }

                //Bank
                for (int j = 0; j < sdat.infoFile.bankData.Count(); j++)
                {
                    if ((int)sdat.infoFile.bankData[j].fileId == i)
                    {
                        tree.Nodes[8].Nodes[2].Nodes.Add("[" + fileId + "] " + sdat.symbFile.bankStrings[j].name + ".sbnk", "[" + fileId + "] " + sdat.symbFile.bankStrings[j].name + ".sbnk", 2, 2);
                        files.Add("[" + fileId + "] " + sdat.symbFile.bankStrings[j].name + ".sbnk");
                        fileId++;
                        break;
                    }
                }

                //WavArc
                for (int j = 0; j < sdat.infoFile.waveData.Count(); j++)
                {
                    if ((int)sdat.infoFile.waveData[j].fileId == i)
                    {
                        tree.Nodes[8].Nodes[3].Nodes.Add("[" + fileId + "] " + sdat.symbFile.waveStrings[j].name + ".swar", "[" + fileId + "] " + sdat.symbFile.waveStrings[j].name + ".swar", 3, 3);
                        files.Add("[" + fileId + "] " + sdat.symbFile.waveStrings[j].name + ".swar");
                        fileId++;
                        break;
                    }
                }

                //Stream
                for (int j = 0; j < sdat.infoFile.strmData.Count(); j++)
                {
                    if ((int)sdat.infoFile.strmData[j].fileId == i)
                    {
                        tree.Nodes[8].Nodes[4].Nodes.Add("[" + fileId + "] " + sdat.symbFile.strmStrings[j].name + ".strm", "[" + fileId + "] " + sdat.symbFile.strmStrings[j].name + ".strm", 4, 4);
                        files.Add("[" + fileId + "] " + sdat.symbFile.strmStrings[j].name + ".strm");
                        fileId++;
                        break;
                    }
                }

            }

            fatFiles = files.ToArray();
            */


            //Add file menu to each node.
            for (int i = 0; i < tree.Nodes[8].Nodes[0].Nodes.Count; i++) {
                tree.Nodes[8].Nodes[0].Nodes[i].ContextMenuStrip = filesMenu;
            }
            for (int i = 0; i < tree.Nodes[8].Nodes[1].Nodes.Count; i++)
            {
                tree.Nodes[8].Nodes[1].Nodes[i].ContextMenuStrip = filesMenu;
            }
            for (int i = 0; i < tree.Nodes[8].Nodes[2].Nodes.Count; i++)
            {
                tree.Nodes[8].Nodes[2].Nodes[i].ContextMenuStrip = filesMenu;
            }
            for (int i = 0; i < tree.Nodes[8].Nodes[3].Nodes.Count; i++)
            {
                tree.Nodes[8].Nodes[3].Nodes[i].ContextMenuStrip = filesMenu;
            }
            for (int i = 0; i < tree.Nodes[8].Nodes[4].Nodes.Count; i++)
            {
                tree.Nodes[8].Nodes[4].Nodes[i].ContextMenuStrip = filesMenu;
            }

            //Load info block.
            for (int i = 0; i < sdat.infoFile.sseqData.Count(); i++) {
                if (sdat.symbFile.sseqStrings[i].isPlaceHolder)
                {
                    tree.Nodes[0].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%");
                }
                else
                {
                    tree.Nodes[0].Nodes.Add("[" + i + "] " + sdat.symbFile.sseqStrings[i].name);
                }
            }
            for (int i = 0; i < sdat.infoFile.seqArcData.Count(); i++)
            {
                if (sdat.symbFile.seqArcStrings[i].isPlaceHolder)
                {
                    tree.Nodes[1].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%", "[" + i + "] " + "%PLACEHOLDER%", 1, 1);
                }
                else
                {
                    tree.Nodes[1].Nodes.Add("[" + i + "] " + sdat.symbFile.seqArcStrings[i].name, "[" + i + "] " + sdat.symbFile.seqArcStrings[i].name, 1, 1);
                }

                //Sub strings.
                for (int j = 0; j < sdat.symbFile.seqArcSubStrings[i].Count(); j++) {
                    if (sdat.symbFile.seqArcSubStrings[i][j].isPlaceHolder)
                    {
                        tree.Nodes[1].Nodes[i].Nodes.Add("[" + j + "] " + "%PLACEHOLDER%");
                    }
                    else
                    {
                        tree.Nodes[1].Nodes[i].Nodes.Add("[" + j + "] " + sdat.symbFile.seqArcSubStrings[i][j].name);
                    }
                }
            }
            for (int i = 0; i < sdat.infoFile.bankData.Count(); i++)
            {
                if (sdat.symbFile.bankStrings[i].isPlaceHolder)
                {
                    tree.Nodes[2].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%", "[" + i + "] " + "%PLACEHOLDER%", 2, 2);
                }
                else
                {
                    tree.Nodes[2].Nodes.Add("[" + i + "] " + sdat.symbFile.bankStrings[i].name, "[" + i + "] " + sdat.symbFile.bankStrings[i].name, 2, 2);
                }
            }
            for (int i = 0; i < sdat.infoFile.waveData.Count(); i++)
            {
                if (sdat.symbFile.waveStrings[i].isPlaceHolder)
                {
                    tree.Nodes[3].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%", "[" + i + "] " + "%PLACEHOLDER%", 3, 3);
                }
                else
                {
                    tree.Nodes[3].Nodes.Add("[" + i + "] " + sdat.symbFile.waveStrings[i].name, "[" + i + "] " + sdat.symbFile.waveStrings[i].name, 3, 3);
                }
            }
            for (int i = 0; i < sdat.infoFile.playerData.Count(); i++)
            {
                if (sdat.symbFile.playerStrings[i].isPlaceHolder)
                {
                    tree.Nodes[4].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%", "[" + i + "] " + "%PLACEHOLDER%", 4, 4);
                }
                else
                {
                    tree.Nodes[4].Nodes.Add("[" + i + "] " + sdat.symbFile.playerStrings[i].name, "[" + i + "] " + sdat.symbFile.playerStrings[i].name, 4, 4);
                }
            }
            //Groups.
            for (int i = 0; i < sdat.infoFile.groupData.Count(); i++) {

                if(sdat.symbFile.groupStrings[i].isPlaceHolder)
                {
                    tree.Nodes[5].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%", "[" + i + "] " + "%PLACEHOLDER%", 5, 5);
                }
                else
                {
                    tree.Nodes[5].Nodes.Add("[" + i + "] " + sdat.symbFile.groupStrings[i].name, "[" + i + "] " + sdat.symbFile.groupStrings[i].name, 5, 5);
                    for (int j = 0; j < sdat.infoFile.groupData[i].count; j++) {
                        switch (sdat.infoFile.groupData[i].subInfo[j].type) {

                            case 0:
                                tree.Nodes[5].Nodes[i].Nodes.Add("Entry " + j, "Entry " + j, 0, 0);
                                break;

                            case 1:
                                tree.Nodes[5].Nodes[i].Nodes.Add("Entry " + j, "Entry " + j, 2, 2);
                                break;

                            case 2:
                                tree.Nodes[5].Nodes[i].Nodes.Add("Entry " + j, "Entry " + j, 3, 3);
                                break;

                            case 3:
                                tree.Nodes[5].Nodes[i].Nodes.Add("Entry " + j, "Entry " + j, 1, 1);
                                break;

                            default:
                                tree.Nodes[5].Nodes[i].Nodes.Add("Entry " + j, "Entry " + j, 9, 9);
                                break;

                        }
                        
                    }
                }

            }
            for (int i = 0; i < sdat.infoFile.player2Data.Count(); i++)
            {
                if (sdat.symbFile.player2Strings[i].isPlaceHolder)
                {
                    tree.Nodes[6].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%", "[" + i + "] " + "%PLACEHOLDER%", 6, 6);
                }
                else
                {
                    tree.Nodes[6].Nodes.Add("[" + i + "] " + sdat.symbFile.player2Strings[i].name, "[" + i + "] " + sdat.symbFile.player2Strings[i].name, 6, 6);
                }
            }
            for (int i = 0; i < sdat.infoFile.strmData.Count(); i++)
            {
                if (sdat.symbFile.strmStrings[i].isPlaceHolder)
                {
                    tree.Nodes[7].Nodes.Add("[" + i + "] " + "%PLACEHOLDER%", "[" + i + "] " + "%PLACEHOLDER%", 7, 7);
                }
                else
                {
                    tree.Nodes[7].Nodes.Add("[" + i + "] " + sdat.symbFile.strmStrings[i].name, "[" + i + "] " + sdat.symbFile.strmStrings[i].name, 7, 7);
                }
            }

            //Set FILES node.
            tree.Nodes[8].ContextMenuStrip = bigFolderMenu;


            //Set the menus for the data.
            foreach (TreeNode n in tree.Nodes) {

                if (n.Index != 8) {
                    foreach (TreeNode n2 in n.Nodes) {
                        n2.ContextMenuStrip = nodeMenu;
                    }
                }

            }

            //Add sub menus.
            for (int i = 0; i < tree.Nodes[1].Nodes.Count; i++) {

                for (int j = 0; j < tree.Nodes[1].Nodes[i].Nodes.Count; j++) {
                    tree.Nodes[1].Nodes[i].Nodes[j].ContextMenuStrip = subNodeMenu;
                }

            }
            for (int i = 0; i < tree.Nodes[5].Nodes.Count; i++)
            {

                for (int j = 0; j < tree.Nodes[5].Nodes[i].Nodes.Count; j++)
                {
                    tree.Nodes[5].Nodes[i].Nodes[j].ContextMenuStrip = subNodeMenu;
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

            tree.SelectedNode = tree.Nodes[0];
            tree.EndUpdate();

        }


        #endregion NodeShit



        //Edit certain files.
        #region fileOpenerNodes
        public void doubleClickNode(object sender, EventArgs e) {

            if (tree.SelectedNode != null) {
                if (tree.SelectedNode.Parent != null) {

                    if (tree.SelectedNode.Parent.Parent == null)
                    {

                        if (tree.SelectedNode.Parent.Text == "Instrument Bank")
                        {

                            TreeNode n = tree.Nodes[8].Nodes[2].Nodes[(int)sdat.infoFile.bankData[tree.SelectedNode.Index].fileId - (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count())];

                            if (!sdat.infoFile.bankData[tree.SelectedNode.Index].isPlaceHolder)
                            {
                                sdat.fixOffsets();
                                SbnkEditor s = new SbnkEditor(this, sdat.files.files[(int)sdat.infoFile.bankData[tree.SelectedNode.Index].fileId], n.Text.Split(' ')[1], (int)(sdat.infoFile.bankData[tree.SelectedNode.Index].fileId - (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count())), tree.SelectedNode.Index);
                                s.Show();
                            }

                        }

                        else if (tree.SelectedNode.Parent.Text == "Wave")
                        {

                            TreeNode n = tree.Nodes[8].Nodes[3].Nodes[(int)sdat.infoFile.waveData[tree.SelectedNode.Index].fileId - (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count() + sdat.files.bankFiles.Count())];

                            if (!sdat.infoFile.waveData[tree.SelectedNode.Index].isPlaceHolder)
                            {
                                sdat.fixOffsets();
                                SwarEditor s = new SwarEditor(this, sdat.files.files[(int)sdat.infoFile.waveData[tree.SelectedNode.Index].fileId], n.Text.Split(' ')[1], (int)(sdat.infoFile.waveData[tree.SelectedNode.Index].fileId - (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count() + sdat.files.bankFiles.Count())));
                                s.Show();
                            }

                        }

                        else if (tree.SelectedNode.Parent.Text == "Stream")
                        {

                            if (!sdat.infoFile.strmData[tree.SelectedNode.Index].isPlaceHolder)
                            {
                                //Convert STRM to .wav
                                sdat.fixOffsets();
                                File.WriteAllBytes(nitroPath + "/Data/Tools/tmp.strm", sdat.files.files[(int)sdat.infoFile.strmData[tree.SelectedNode.Index].fileId]);
                                string infoArguments = "tmp.strm";
                                Process p2 = new Process();
                                p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\strm2wav.exe\"";
                                p2.StartInfo.Arguments = infoArguments;
                                p2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools");
                                p2.Start();
                                p2.WaitForExit();
                                Directory.SetCurrentDirectory(nitroPath);

                                StrmPlayer s = new StrmPlayer(nitroPath + "/Data/Tools/tmp.wav");
                                s.Show();
                            }

                        }

                    }

                    if (tree.SelectedNode.Parent.Parent != null) {

                        if (tree.SelectedNode.Parent.Parent.Name == "FILES" && tree.SelectedNode.Parent.Name == "Sequence")
                        {

                            ByteViewerForm f = new ByteViewerForm(sdat.files.sseqFiles[tree.SelectedNode.Index], tree.SelectedNode.Text);
                            f.Show();

                        }

                        if (tree.SelectedNode.Parent.Parent.Name == "FILES" && tree.SelectedNode.Parent.Name == "Sequence Archive")
                        {

                            SsarEditor s = new SsarEditor(this, sdat.files.seqArcFiles[tree.SelectedNode.Index], tree.Nodes[2], tree.SelectedNode.Text.Split(' ')[1]);
                            s.Show();

                        }

                        if (tree.SelectedNode.Parent.Parent.Name == "FILES" && tree.SelectedNode.Parent.Name == "Bank")
                        {

                            SbnkEditor s = new SbnkEditor(this, sdat.files.bankFiles[tree.SelectedNode.Index], tree.SelectedNode.Text.Split(' ')[1], tree.SelectedNode.Index, 0);
                            s.Show();

                        }
                        

                        if (tree.SelectedNode.Parent.Parent.Name == "FILES" && tree.SelectedNode.Parent.Name == "Wave Archive") {

                            SwarEditor s = new SwarEditor(this, sdat.files.waveFiles[tree.SelectedNode.Index], tree.SelectedNode.Text.Split(' ')[1], tree.SelectedNode.Index);
                            s.Show();

                        }

                        if (tree.SelectedNode.Parent.Parent.Name == "FILES" && tree.SelectedNode.Parent.Name == "Stream")
                        {

                            //Convert STRM to .wav
                            File.WriteAllBytes(nitroPath+"/Data/Tools/tmp.strm", sdat.files.strmFiles[tree.SelectedNode.Index]);
                            string infoArguments = "tmp.strm";
                            Process p2 = new Process();
                            p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\strm2wav.exe\"";
                            p2.StartInfo.Arguments = infoArguments;
                            p2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools");
                            p2.Start();
                            p2.WaitForExit();
                            Directory.SetCurrentDirectory(nitroPath);

                            StrmPlayer s = new StrmPlayer(nitroPath+"/Data/Tools/tmp.wav");
                            s.Show();

                        }


                    }
                }
            }

        }
        #endregion



        //Main Node Menus.
        #region NodeMenu

        //Insert node.
        private void nodeMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        //Expand node.
        private void openTree3_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        //Collapse nodes.
        private void closeTree3_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse(false);
        }


        //Enable adding things to the thing. (Add above).
        private void Add3_Click(object sender, EventArgs e)
        {
            //Type of thing to add.
            string name = tree.SelectedNode.Parent.Text;
            int index = tree.SelectedNode.Index;

            if (name == "Sound Sequence")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.sseqStrings.Insert(index, s);

                SseqData i = new SseqData();
                i.fileId = 0;
                i.bank = 0;
                i.channelPriority = 64;
                i.isPlaceHolder = false;
                i.playerNumber = 0;
                i.playerPriority = 64;
                i.unknown1 = 0;
                i.unknown2 = 0;
                i.volume = 100;
                sdat.infoFile.sseqData.Insert(index, i);

            }

            if (name == "Sequence Archive")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.seqArcStrings.Insert(index, s);
                List<symbStringName> t = new List<symbStringName>();
                sdat.symbFile.seqArcSubStrings.Insert(index, t);

                SeqArcData i = new SeqArcData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.seqArcData.Insert(index, i);

            }

            if (name == "Instrument Bank")
            {

                //Fix sseq.
                foreach (SseqData t in sdat.infoFile.sseqData) {

                    if (t.bank >= tree.SelectedNode.Index && t.bank != 0xFFFF) { t.bank += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.bankStrings.Insert(index, s);

                BankData i = new BankData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                i.wave0 = 0;
                i.wave1 = 0;
                i.wave2 = 0;
                i.wave3 = 0;
                sdat.infoFile.bankData.Insert(index, i);

            }


            if (name == "Wave")
            {

                //Fix bank.
                foreach (BankData t in sdat.infoFile.bankData)
                {

                    if (t.wave0 >= tree.SelectedNode.Index && t.wave0 != 0xFFFF) { t.wave0 += 1; }
                    if (t.wave1 >= tree.SelectedNode.Index && t.wave1 != 0xFFFF) { t.wave1 += 1; }
                    if (t.wave2 >= tree.SelectedNode.Index && t.wave2 != 0xFFFF) { t.wave2 += 1; }
                    if (t.wave3 >= tree.SelectedNode.Index && t.wave3 != 0xFFFF) { t.wave3 += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.waveStrings.Insert(index, s);

                WaveData i = new WaveData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.waveData.Insert(index, i);

            }

            if (name == "Player")
            {

                //Fix sseq.
                foreach (SseqData t in sdat.infoFile.sseqData)
                {

                    if (t.playerNumber >= tree.SelectedNode.Index && t.playerNumber != 0xFF) { t.playerNumber += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.playerStrings.Insert(index, s);

                PlayerData i = new PlayerData();
                i.channelFlag = 0;
                i.heapSize = 0;
                i.seqMax = 1;
                i.isPlaceHolder = false;
                sdat.infoFile.playerData.Insert(index, i);

            }

            if (name == "Group")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.groupStrings.Insert(index, s);

                GroupData i = new GroupData();
                i.count = 0;
                i.subInfo = new List<GroupSubData>();
                i.isPlaceHolder = false;
                sdat.infoFile.groupData.Insert(index, i);


            }


            if (name == "Stream Player")
            {

                //Fix strm.
                foreach (StrmData t in sdat.infoFile.strmData)
                {

                    if (t.player >= tree.SelectedNode.Index && t.player != 0xFF) { t.player += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.player2Strings.Insert(index, s);

                Player2Data i = new Player2Data();
                i.count = 0;
                byte[] reserved = { 0, 0, 0, 0, 0, 0, 0 };
                i.reserved = reserved;
                i.v0 = 0;
                i.v1 = 0;
                i.v2 = 0;
                i.v3 = 0;
                i.v4 = 0;
                i.v5 = 0;
                i.v6 = 0;
                i.v7 = 0;
                i.v8 = 0;
                i.v9 = 0;
                i.v10 = 0;
                i.v11 = 0;
                i.v12 = 0;
                i.v13 = 0;
                i.v14 = 0;
                i.v15 = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.player2Data.Insert(index, i);

            }


            if (name == "Stream")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.strmStrings.Insert(index, s);

                StrmData i = new StrmData();
                i.fileId = 0;
                i.volume = 100;
                i.player = 0;
                i.priority = 64;
                byte[] reserved = { 0, 0, 0, 0, 0 };
                i.reserved = reserved;
                i.isPlaceHolder = false;
                sdat.infoFile.strmData.Insert(index, i);

            }

            updateNodes();
        }

        //Add below.
        private void Add32_Click(object sender, EventArgs e)
        {
            //Type of thing to add.
            string name = tree.SelectedNode.Parent.Text;
            int index = tree.SelectedNode.Index + 1;

            if (name == "Sound Sequence")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.sseqStrings.Insert(index, s);

                SseqData i = new SseqData();
                i.fileId = 0;
                i.bank = 0;
                i.channelPriority = 64;
                i.isPlaceHolder = false;
                i.playerNumber = 0;
                i.playerPriority = 64;
                i.unknown1 = 0;
                i.unknown2 = 0;
                i.volume = 100;
                sdat.infoFile.sseqData.Insert(index, i);

            }

            if (name == "Sequence Archive")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.seqArcStrings.Insert(index, s);
                List<symbStringName> t = new List<symbStringName>();
                sdat.symbFile.seqArcSubStrings.Insert(index, t);

                SeqArcData i = new SeqArcData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.seqArcData.Insert(index, i);

            }

            if (name == "Instrument Bank")
            {

                //Fix sseq.
                foreach (SseqData t in sdat.infoFile.sseqData)
                {

                    if (t.bank > tree.SelectedNode.Index && t.bank != 0xFFFF) { t.bank += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.bankStrings.Insert(index, s);

                BankData i = new BankData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                i.wave0 = 0;
                i.wave1 = 0;
                i.wave2 = 0;
                i.wave3 = 0;
                sdat.infoFile.bankData.Insert(index, i);

            }


            if (name == "Wave")
            {

                //Fix bank.
                foreach (BankData t in sdat.infoFile.bankData)
                {

                    if (t.wave0 > tree.SelectedNode.Index && t.wave0 != 0xFFFF) { t.wave0 += 1; }
                    if (t.wave1 > tree.SelectedNode.Index && t.wave1 != 0xFFFF) { t.wave1 += 1; }
                    if (t.wave2 > tree.SelectedNode.Index && t.wave2 != 0xFFFF) { t.wave2 += 1; }
                    if (t.wave3 > tree.SelectedNode.Index && t.wave3 != 0xFFFF) { t.wave3 += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.waveStrings.Insert(index, s);

                WaveData i = new WaveData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.waveData.Insert(index, i);

            }

            if (name == "Player")
            {

                //Fix sseq.
                foreach (SseqData t in sdat.infoFile.sseqData)
                {

                    if (t.playerNumber > tree.SelectedNode.Index && t.playerNumber != 0xFF) { t.playerNumber += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.playerStrings.Insert(index, s);

                PlayerData i = new PlayerData();
                i.channelFlag = 0;
                i.heapSize = 0;
                i.seqMax = 1;
                i.isPlaceHolder = false;
                sdat.infoFile.playerData.Insert(index, i);

            }

            if (name == "Group")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.groupStrings.Insert(index, s);

                GroupData i = new GroupData();
                i.count = 0;
                i.subInfo = new List<GroupSubData>();
                i.isPlaceHolder = false;
                sdat.infoFile.groupData.Insert(index, i);


            }


            if (name == "Stream Player")
            {

                //Fix strm.
                foreach (StrmData t in sdat.infoFile.strmData)
                {

                    if (t.player > tree.SelectedNode.Index && t.player != 0xFF) { t.player += 1; }

                }

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.player2Strings.Insert(index, s);

                Player2Data i = new Player2Data();
                i.count = 0;
                byte[] reserved = { 0, 0, 0, 0, 0, 0, 0 };
                i.reserved = reserved;
                i.v0 = 0;
                i.v1 = 0;
                i.v2 = 0;
                i.v3 = 0;
                i.v4 = 0;
                i.v5 = 0;
                i.v6 = 0;
                i.v7 = 0;
                i.v8 = 0;
                i.v9 = 0;
                i.v10 = 0;
                i.v11 = 0;
                i.v12 = 0;
                i.v13 = 0;
                i.v14 = 0;
                i.v15 = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.player2Data.Insert(index, i);

            }


            if (name == "Stream")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.strmStrings.Insert(index, s);

                StrmData i = new StrmData();
                i.fileId = 0;
                i.volume = 100;
                i.player = 0;
                i.priority = 64;
                byte[] reserved = { 0, 0, 0, 0, 0 };
                i.reserved = reserved;
                i.isPlaceHolder = false;
                sdat.infoFile.strmData.Insert(index, i);

            }

            updateNodes();
        }

        //Add inside.
        private void addInside_Click(object sender, EventArgs e)
        {

            //If SeqArc.
            if (tree.SelectedNode.Parent.Index == 1) {

                //Get name.
                string newName = Interaction.InputBox("Name the entry:", "Namer");
                if (newName != "")
                {

                    //Rename the file.
                    symbStringName s = new symbStringName();
                    s.isPlaceHolder = false;
                    s.name = newName;
                    sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Index].Add(s);
                    updateNodes();

                }

            }

            //Groups.
            else if (tree.SelectedNode.Parent.Index == 5)
            {

                //Add entry.
                GroupSubData d = new GroupSubData();
                d.loadFlag = 0;
                d.nEntry = 0;
                d.type = 0;
                d.padding = 0;
                sdat.infoFile.groupData[tree.SelectedNode.Index].subInfo.Add(d);
                updateNodes();

            }

            //None of the above.
            else {

                MessageBox.Show("You can't insert anything in here!", "Notice:");

            }

        }

        //Rename.
        private void rename3_Click(object sender, EventArgs e)
        {

            string newName = Interaction.InputBox("Rename the entry:", "Renamer");
            if (newName != "")
            {
                if (tree.SelectedNode.Parent.Text == "Sound Sequence")
                {
                    sdat.symbFile.sseqStrings[tree.SelectedNode.Index].name = newName;
                }
                if (tree.SelectedNode.Parent.Text == "Sequence Archive")
                {
                    sdat.symbFile.seqArcStrings[tree.SelectedNode.Index].name = newName;
                }
                if (tree.SelectedNode.Parent.Text == "Instrument Bank")
                {
                    sdat.symbFile.bankStrings[tree.SelectedNode.Index].name = newName;
                }
                if (tree.SelectedNode.Parent.Text == "Wave")
                {
                    sdat.symbFile.waveStrings[tree.SelectedNode.Index].name = newName;
                }
                if (tree.SelectedNode.Parent.Text == "Player")
                {
                    sdat.symbFile.playerStrings[tree.SelectedNode.Index].name = newName;
                }
                if (tree.SelectedNode.Parent.Text == "Group")
                {
                    sdat.symbFile.groupStrings[tree.SelectedNode.Index].name = newName;
                }
                if (tree.SelectedNode.Parent.Text == "Stream Player")
                {
                    sdat.symbFile.player2Strings[tree.SelectedNode.Index].name = newName;
                }
                if (tree.SelectedNode.Parent.Text == "Stream")
                {
                    sdat.symbFile.strmStrings[tree.SelectedNode.Index].name = newName;
                }

                updateNodes();
            }

        }

        //Delete.
        private void deleteMeh_Click(object sender, EventArgs e)
        {

            //Get name.
            string name = tree.SelectedNode.Parent.Text;

            if (name == "Sound Sequence") {

                sdat.symbFile.sseqStrings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.sseqData.RemoveAt(tree.SelectedNode.Index);

            }

            if (name == "Sequence Archive")
            {

                sdat.symbFile.seqArcStrings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.seqArcData.RemoveAt(tree.SelectedNode.Index);
                sdat.symbFile.seqArcSubStrings.RemoveAt(tree.SelectedNode.Index);

            }

            if (name == "Instrument Bank")
            {

                //Decrement the sseq entries.
                foreach (SseqData s in sdat.infoFile.sseqData) {

                    if (s.bank > tree.SelectedNode.Index && s.bank != 0xFFFF) { s.bank -= 1; }

                }

                sdat.symbFile.bankStrings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.bankData.RemoveAt(tree.SelectedNode.Index);

            }

            if (name == "Wave")
            {

                //Decrement the wave entries.
                foreach (BankData s in sdat.infoFile.bankData)
                {

                    if (s.wave0 > tree.SelectedNode.Index && s.wave0 != 0xFFFF) { s.wave0 -= 1; }
                    if (s.wave1 > tree.SelectedNode.Index && s.wave1 != 0xFFFF) { s.wave1 -= 1; }
                    if (s.wave2 > tree.SelectedNode.Index && s.wave2 != 0xFFFF) { s.wave2 -= 1; }
                    if (s.wave3 > tree.SelectedNode.Index && s.wave3 != 0xFFFF) { s.wave3 -= 1; }

                }
                sdat.symbFile.waveStrings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.waveData.RemoveAt(tree.SelectedNode.Index);

            }

            if (name == "Player")
            {

                //Decrement the player entries.
                foreach (SseqData s in sdat.infoFile.sseqData)
                {

                    if (s.playerNumber > tree.SelectedNode.Index && s.playerNumber != 0xFF) { s.playerNumber -= 1; }

                }
                sdat.symbFile.playerStrings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.playerData.RemoveAt(tree.SelectedNode.Index);

            }

            if (name == "Group")
            {

                sdat.symbFile.groupStrings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.groupData.RemoveAt(tree.SelectedNode.Index);

            }

            if (name == "Stream Player")
            {

                //Decrement the player2 entries.
                foreach (StrmData s in sdat.infoFile.strmData)
                {

                    if (s.player > tree.SelectedNode.Index && s.player != 0xFF) { s.player -= 1; }

                }

                sdat.symbFile.player2Strings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.player2Data.RemoveAt(tree.SelectedNode.Index);

            }

            if (name == "Stream")
            {

                sdat.symbFile.strmStrings.RemoveAt(tree.SelectedNode.Index);
                sdat.infoFile.strmData.RemoveAt(tree.SelectedNode.Index);

            }


            //Update nodes.
            updateNodes();

        }


        //Add a new info or symb entry.
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

            //Type of thing to add.
            string name = tree.SelectedNode.Text;

            if (name == "Sound Sequence") {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.sseqStrings.Add(s);

                SseqData i = new SseqData();
                i.fileId = 0;
                i.bank = 0;
                i.channelPriority = 64;
                i.isPlaceHolder = false;
                i.playerNumber = 0;
                i.playerPriority = 64;
                i.unknown1 = 0;
                i.unknown2 = 0;
                i.volume = 100;
                sdat.infoFile.sseqData.Add(i);

            }

            if (name == "Sequence Archive")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.seqArcStrings.Add(s);
                List<symbStringName> t = new List<symbStringName>();
                sdat.symbFile.seqArcSubStrings.Add(t);

                SeqArcData i = new SeqArcData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.seqArcData.Add(i);

            }

            if (name == "Instrument Bank")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.bankStrings.Add(s);

                BankData i = new BankData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                i.wave0 = 0;
                i.wave1 = 0;
                i.wave2 = 0;
                i.wave3 = 0;
                sdat.infoFile.bankData.Add(i);

            }


            if (name == "Wave")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.waveStrings.Add(s);

                WaveData i = new WaveData();
                i.fileId = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.waveData.Add(i);

            }

            if (name == "Player")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.playerStrings.Add(s);

                PlayerData i = new PlayerData();
                i.channelFlag = 0;
                i.heapSize = 0;
                i.seqMax = 1;
                i.isPlaceHolder = false;
                sdat.infoFile.playerData.Add(i);

            }

            if (name == "Group")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.groupStrings.Add(s);

                GroupData i = new GroupData();
                i.count = 0;
                i.subInfo = new List<GroupSubData>();
                i.isPlaceHolder = false;
                sdat.infoFile.groupData.Add(i);


            }


            if (name == "Stream Player")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.player2Strings.Add(s);

                Player2Data i = new Player2Data();
                i.count = 0;
                byte[] reserved = { 0, 0, 0, 0, 0, 0, 0 };
                i.reserved = reserved;
                i.v0 = 0;
                i.v1 = 0;
                i.v2 = 0;
                i.v3 = 0;
                i.v4 = 0;
                i.v5 = 0;
                i.v6 = 0;
                i.v7 = 0;
                i.v8 = 0;
                i.v9 = 0;
                i.v10 = 0;
                i.v11 = 0;
                i.v12 = 0;
                i.v13 = 0;
                i.v14 = 0;
                i.v15 = 0;
                i.isPlaceHolder = false;
                sdat.infoFile.player2Data.Add(i);

            }


            if (name == "Stream")
            {

                //Add entry.
                symbStringName s = new symbStringName();
                s.name = "New entry";
                s.isPlaceHolder = false;
                sdat.symbFile.strmStrings.Add(s);

                StrmData i = new StrmData();
                i.fileId = 0;
                i.volume = 100;
                i.player = 0;
                i.priority = 64;
                byte[] reserved = { 0, 0, 0, 0, 0};
                i.reserved = reserved;
                i.isPlaceHolder = false;
                sdat.infoFile.strmData.Add(i);

            }

            updateNodes();

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse();
        }

        //Export
        private void exportToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            int fileId = -1;
            string ending = "";
            if (tree.SelectedNode != null) {
                if (tree.SelectedNode.Parent != null) {
                    switch (tree.SelectedNode.Parent.Index) {
                        case 0:
                            fileId = (int)sdat.infoFile.sseqData[tree.SelectedNode.Index].fileId;
                            ending = ".sseq";
                            break;
                        case 1:
                            fileId = (int)sdat.infoFile.seqArcData[tree.SelectedNode.Index].fileId;
                            fileId -= sdat.files.sseqFiles.Count();
                            ending = ".ssar";
                            break;
                        case 2:
                            fileId = (int)sdat.infoFile.bankData[tree.SelectedNode.Index].fileId;
                            fileId -= (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count());
                            ending = ".sbnk";
                            break;
                        case 3:
                            fileId = (int)sdat.infoFile.waveData[tree.SelectedNode.Index].fileId;
                            fileId -= (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count() + sdat.files.bankFiles.Count());
                            ending = ".swar";
                            break;
                        case 7:
                            fileId = (int)sdat.infoFile.strmData[tree.SelectedNode.Index].fileId;
                            fileId -= (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count() + sdat.files.bankFiles.Count() + sdat.files.waveFiles.Count());
                            ending = ".strm";
                            break;
                        default:
                            MessageBox.Show("This has no file!");
                            break;
                    }
                }
            }

            if (fileId != -1)
            {
                //Strings for exporting files.
                string sseq2midi = "MID Sequence File |*.mid";
                string strm2wav = "PCM Wave |*.wav";

                //Node title.
                string name = tree.SelectedNode.Text + ending;
                string rootName = Path.GetFileNameWithoutExtension(name);

                //Ending.       
                string newFileEnding = ".mid";

                //Export Any File.
                string fileExportString = "Misc. Nitro Sound File |*" + ending;
                int filterIndex = 1;

                if (ending == ".sseq") { fileExportString += "|" + sseq2midi; filterIndex++; newFileEnding = ".mid"; }
                if (ending == ".strm") { fileExportString += "|" + strm2wav; filterIndex++; newFileEnding = ".wav"; }



                //Save file dialog.
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = previousPath;
                saveFileDialog1.Title = "Export File";
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.Filter = fileExportString;
                saveFileDialog1.FilterIndex = filterIndex;
                saveFileDialog1.FileName = rootName.Split(' ')[1];

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Normal export.
                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        if (ending == ".sseq") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.sseqFiles[fileId]);
                        if (ending == ".ssar") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.seqArcFiles[fileId]);
                        if (ending == ".sbnk") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.bankFiles[fileId]);
                        if (ending == ".swar") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.waveFiles[fileId]);
                        if (ending == ".strm") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.strmFiles[fileId]);
                    }
                    else
                    {

                        //Get the file.
                        if (ending == ".sseq") File.WriteAllBytes(nitroPath + "\\Data\\Tools\\tmp", sdat.files.sseqFiles[fileId]);
                        if (ending == ".strm") File.WriteAllBytes(nitroPath + "\\Data\\Tools\\tmp", sdat.files.strmFiles[fileId]);

                        string infoArguments = "tmp";
                        Process p2 = new Process();
                        if (ending == ".sseq") p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\sseq2midi.exe\"";
                        if (ending == ".strm") p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\strm2wav.exe\"";
                        p2.StartInfo.Arguments = infoArguments;
                        p2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools");
                        p2.Start();
                        p2.WaitForExit();
                        Directory.SetCurrentDirectory(nitroPath);

                        //Export the new file.
                        if (ending == ".sseq") File.Copy(nitroPath + "\\Data\\Tools\\tmp.mid", saveFileDialog1.FileName, true);
                        if (ending == ".strm") File.Copy(nitroPath + "\\Data\\Tools\\tmp.wav", saveFileDialog1.FileName, true);

                        //Delete useless files.
                        Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools\\");
                        File.Delete("tmp");
                        if (ending == ".sseq") { File.Delete("tmp.mid"); }
                        if (ending == ".strm") { File.Delete("tmp.wav"); }
                        Directory.SetCurrentDirectory(nitroPath);

                    }
                }
            }
        }

        //Replace
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int fileId = -1;
            string ending = "";
            if (tree.SelectedNode != null)
            {
                if (tree.SelectedNode.Parent != null)
                {
                    switch (tree.SelectedNode.Parent.Index)
                    {
                        case 0:
                            fileId = (int)sdat.infoFile.sseqData[tree.SelectedNode.Index].fileId;
                            ending = ".sseq";
                            break;
                        case 1:
                            fileId = (int)sdat.infoFile.seqArcData[tree.SelectedNode.Index].fileId;
                            fileId -= sdat.files.sseqFiles.Count();
                            ending = ".ssar";
                            break;
                        case 2:
                            fileId = (int)sdat.infoFile.bankData[tree.SelectedNode.Index].fileId;
                            fileId -= (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count());
                            ending = ".sbnk";
                            break;
                        case 3:
                            fileId = (int)sdat.infoFile.waveData[tree.SelectedNode.Index].fileId;
                            fileId -= (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count() + sdat.files.bankFiles.Count());
                            ending = ".swar";
                            break;
                        case 7:
                            fileId = (int)sdat.infoFile.strmData[tree.SelectedNode.Index].fileId;
                            fileId -= (sdat.files.sseqFiles.Count() + sdat.files.seqArcFiles.Count() + sdat.files.bankFiles.Count() + sdat.files.waveFiles.Count());
                            ending = ".strm";
                            break;
                        default:
                            MessageBox.Show("This has no file!");
                            break;
                    }
                }
            }

            //Node title.
            string name = tree.SelectedNode.Text + ending;
            string rootName = Path.GetFileNameWithoutExtension(name);

            string filter = "Supported Files|*.sseq;*.mid|Sound Sequence|*.sseq|MIDI Sequence|*.mid";

            //Change ending.
            if (ending == ".ssar") { filter = "Sequence Archive|*.ssar"; }
            if (ending == ".sbnk") { filter = "Sound Bank|*.sbnk"; }
            if (ending == ".swar") { filter = "Wave Archive|*.swar"; }
            if (ending == ".strm") { filter = "Supported Files|*.strm;*.wav|Stream|*.strm|PCM Wave|*.wav"; }

            if (fileId != -1) {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = filter;
            f.Title = "Open A Sound File";
            f.RestoreDirectory = true;
            f.ShowDialog();

                if (f.FileName != "")
                {

                    if (ending == ".sseq" || ending == ".strm")
                    {

                        if (ending == ".sseq")
                        {

                            if (f.FileName.EndsWith(".mid")) { f.FilterIndex = 2; }
                            if (f.FileName.EndsWith(".sseq")) { f.FilterIndex = 1; }

                        }
                        else
                        {

                            if (f.FileName.EndsWith(".wav")) { f.FilterIndex = 2; }
                            if (f.FileName.EndsWith(".strm")) { f.FilterIndex = 1; }

                        }

                    }

                    if (f.FilterIndex == 1)
                    {

                        if (ending == ".sseq") { sdat.files.sseqFiles[fileId] = File.ReadAllBytes(f.FileName); }
                        if (ending == ".seqArc") { sdat.files.seqArcFiles[fileId] = File.ReadAllBytes(f.FileName); }
                        if (ending == ".sbnk") { sdat.files.bankFiles[fileId] = File.ReadAllBytes(f.FileName); }
                        if (ending == ".swar") { sdat.files.waveFiles[fileId] = File.ReadAllBytes(f.FileName); }
                        if (ending == ".strm") { sdat.files.strmFiles[fileId] = File.ReadAllBytes(f.FileName); }

                    }
                    else
                    {

                        //Copy file to tmp.
                        File.Copy(f.FileName, nitroPath + "\\Data\\Tools\\tmp", true);

                        //New process conversion.
                        Process p2 = new Process();
                        string infoArguments = "";
                        if (ending == ".sseq") { p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\midi2sseq.exe\""; infoArguments = "tmp tmp.sseq"; }
                        if (ending == ".strm") { p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\wav2strm.exe\""; infoArguments = "tmp"; }
                        p2.StartInfo.Arguments = infoArguments;
                        if (ending == ".strm") { p2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; }
                        Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools");
                        p2.Start();
                        p2.WaitForExit();

                        if (ending == ".sseq") { sdat.files.sseqFiles[fileId] = File.ReadAllBytes("tmp.sseq"); }
                        if (ending == ".strm") { sdat.files.strmFiles[fileId] = File.ReadAllBytes("tmp.strm"); }

                        //Delete the files.
                        File.Delete("tmp");
                        if (ending == ".sseq") { File.Delete("tmp.sseq"); }
                        if (ending == ".strm") { File.Delete("tmp.strm"); }


                        Directory.SetCurrentDirectory(nitroPath);


                    }
                }

            }
        }
        #endregion NodeMenu



        //Get a path of a file selection.
        #region browseForFile
        public string browseForFile() {

            string returnValue = "";

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.InitialDirectory = previousPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            //Get file.
                            returnValue = openFileDialog1.FileName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            return returnValue;

        }
        #endregion BrowseForFile



        //Region for file right click menu.
        #region FilesNodes

        //Exporting files.
        private void Export_Click(object sender, EventArgs e)
        {

            //Strings for exporting files.
            string sseq2midi = "MID Sequence File |*.mid";
            string strm2wav = "PCM Wave |*.wav";

            //Node title.
            string name = tree.SelectedNode.Text;
            string rootName = Path.GetFileNameWithoutExtension(name);

            //Ending.
            string ending = name.Substring(name.IndexOf('.'));

            string newFileEnding = ".mid";

            //Export Any File.
            string fileExportString = "Misc. Nitro Sound File |*" + ending;
            int filterIndex = 1;

            if (ending == ".sseq") { fileExportString += "|" + sseq2midi; filterIndex++; newFileEnding = ".mid"; }
            if (ending == ".strm") { fileExportString += "|" + strm2wav; filterIndex++; newFileEnding = ".wav"; }



            //Save file dialog.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = previousPath;
            saveFileDialog1.Title = "Export File";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.Filter = fileExportString;
            saveFileDialog1.FilterIndex = filterIndex;
            saveFileDialog1.FileName = rootName.Split(' ')[1];

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Normal export.
                if (saveFileDialog1.FilterIndex == 1)
                {
                    if (ending == ".sseq") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.sseqFiles[tree.SelectedNode.Index]);
                    if (ending == ".ssar") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.seqArcFiles[tree.SelectedNode.Index]);
                    if (ending == ".sbnk") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.bankFiles[tree.SelectedNode.Index]);
                    if (ending == ".swar") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.waveFiles[tree.SelectedNode.Index]);
                    if (ending == ".strm") File.WriteAllBytes(saveFileDialog1.FileName, sdat.files.strmFiles[tree.SelectedNode.Index]);
                }
                else {
                    
                    //Get the file.
                    if (ending == ".sseq") File.WriteAllBytes(nitroPath + "\\Data\\Tools\\tmp", sdat.files.sseqFiles[tree.SelectedNode.Index]);
                    if (ending == ".strm") File.WriteAllBytes(nitroPath + "\\Data\\Tools\\tmp", sdat.files.strmFiles[tree.SelectedNode.Index]);

                    string infoArguments = "tmp";
                    Process p2 = new Process();
                    if (ending == ".sseq") p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\sseq2midi.exe\"";
                    if (ending == ".strm") p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\strm2wav.exe\"";
                    p2.StartInfo.Arguments = infoArguments;
                    p2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools");
                    p2.Start();
                    p2.WaitForExit();
                    Directory.SetCurrentDirectory(nitroPath);

                    //Export the new file.
                    if(ending == ".sseq") File.Copy(nitroPath + "\\Data\\Tools\\tmp.mid", saveFileDialog1.FileName, true);
                    if(ending == ".strm") File.Copy(nitroPath + "\\Data\\Tools\\tmp.wav", saveFileDialog1.FileName, true);

                    //Delete useless files.
                    Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools\\");
                    File.Delete("tmp");
                    if (ending == ".sseq") { File.Delete("tmp.mid"); }
                    if (ending == ".strm") { File.Delete("tmp.wav"); }
                    Directory.SetCurrentDirectory(nitroPath);
                    
                }
            }

        }

        //Delete file in files data.
        private void Delete_Click(object sender, EventArgs e)
        {
            //Show confirmation dialog.
            var result = MessageBox.Show("Are you sure you want to delete the file? This can cause big problems!", "Warning:", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                //Node title.
                string name = tree.SelectedNode.Text;
                string rootName = Path.GetFileNameWithoutExtension(name);

                //Ending.
                string ending = name.Substring(name.IndexOf('.'));



                //Delete the file.
                if (ending == ".sseq") { sdat.files.sseqFiles.RemoveAt(tree.SelectedNode.Index); }
                if (ending == ".ssar") { sdat.files.seqArcFiles.RemoveAt(tree.SelectedNode.Index); }
                if (ending == ".sbnk") { sdat.files.bankFiles.RemoveAt(tree.SelectedNode.Index); }
                if (ending == ".swar") { sdat.files.waveFiles.RemoveAt(tree.SelectedNode.Index); }
                if (ending == ".strm") { sdat.files.strmFiles.RemoveAt(tree.SelectedNode.Index); }


                //Lower the file IDs in info.
                if (ending == ".sseq")
                {
                    foreach (SseqData s in sdat.infoFile.sseqData)
                    {
                        if (s.fileId > tree.SelectedNode.Index) { s.fileId -= 1; }
                    }
                    foreach (SeqArcData s in sdat.infoFile.seqArcData)
                    {
                        s.fileId -= 1;
                    }
                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId -= 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId -= 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId -= 1;
                    }
                }
                else if (ending == ".ssar")
                {
                    foreach (SeqArcData s in sdat.infoFile.seqArcData)
                    {
                        if (s.fileId > tree.SelectedNode.Index + tree.Nodes[8].Nodes[0].Nodes.Count) { s.fileId -= 1; }
                    }
                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId -= 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId -= 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId -= 1;
                    }
                }
                else if (ending == ".sbnk")
                {
                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        if (s.fileId > tree.SelectedNode.Index + tree.Nodes[8].Nodes[0].Nodes.Count + tree.Nodes[8].Nodes[1].Nodes.Count) { s.fileId -= 1; }
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId -= 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId -= 1;
                    }
                }
                else if (ending == ".swar")
                {
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        if (s.fileId > tree.SelectedNode.Index + tree.Nodes[8].Nodes[0].Nodes.Count + tree.Nodes[8].Nodes[1].Nodes.Count + tree.Nodes[8].Nodes[2].Nodes.Count) { s.fileId -= 1; }
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId -= 1;
                    }
                }
                else if (ending == ".strm") {
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        if (s.fileId > tree.SelectedNode.Index + tree.Nodes[8].Nodes[0].Nodes.Count + tree.Nodes[8].Nodes[1].Nodes.Count + tree.Nodes[8].Nodes[2].Nodes.Count + tree.Nodes[8].Nodes[3].Nodes.Count) { s.fileId -= 1; }
                    }
                }

                    //Update nodes
                    updateNodes();
            }
        }

        //Replace a file in the files.
        private void Replace_Click(object sender, EventArgs e)
        {
            //Node title.
            string name = tree.SelectedNode.Text;
            string rootName = Path.GetFileNameWithoutExtension(name);

            //Ending.
            string ending = name.Substring(name.IndexOf('.'));

            string filter = "Supported Files|*.sseq;*.mid|Sound Sequence|*.sseq|MIDI Sequence|*.mid";

            //Change ending.
            if (ending == ".ssar") { filter = "Sequence Archive|*.ssar"; }
            if (ending == ".sbnk") { filter = "Sound Bank|*.sbnk"; }
            if (ending == ".swar") { filter = "Wave Archive|*.swar"; }
            if (ending == ".strm") { filter = "Supported Files|*.strm;*.wav|Stream|*.strm|PCM Wave|*.wav"; }

            OpenFileDialog f = new OpenFileDialog();
            f.Filter = filter;
            f.Title = "Open A Sound File";
            f.RestoreDirectory = true;
            f.ShowDialog();

            if (f.FileName != "") {

                if (ending == ".sseq" || ending == ".strm") {

                    if (ending == ".sseq")
                    {

                        if (f.FileName.EndsWith(".mid")) { f.FilterIndex = 2; }
                        if (f.FileName.EndsWith(".sseq")) { f.FilterIndex = 1; }

                    }
                    else {

                        if (f.FileName.EndsWith(".wav")) { f.FilterIndex = 2; }
                        if (f.FileName.EndsWith(".strm")) { f.FilterIndex = 1; }

                    }

                }

                if (f.FilterIndex == 1)
                {

                    if (ending == ".sseq") { sdat.files.sseqFiles[tree.SelectedNode.Index] = File.ReadAllBytes(f.FileName); }
                    if (ending == ".seqArc") { sdat.files.seqArcFiles[tree.SelectedNode.Index] = File.ReadAllBytes(f.FileName); }
                    if (ending == ".sbnk") { sdat.files.bankFiles[tree.SelectedNode.Index] = File.ReadAllBytes(f.FileName); }
                    if (ending == ".swar") { sdat.files.waveFiles[tree.SelectedNode.Index] = File.ReadAllBytes(f.FileName); }
                    if (ending == ".strm") { sdat.files.strmFiles[tree.SelectedNode.Index] = File.ReadAllBytes(f.FileName); }

                }
                else {

                    //Copy file to tmp.
                    File.Copy(f.FileName, nitroPath + "\\Data\\Tools\\tmp", true);

                    //New process conversion.
                    Process p2 = new Process();
                    string infoArguments = "";
                    if (ending == ".sseq") { p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\midi2sseq.exe\""; infoArguments = "tmp tmp.sseq"; }
                    if (ending == ".strm") { p2.StartInfo.FileName = "\"" + nitroPath + "\\Data\\Tools\\wav2strm.exe\""; infoArguments = "tmp"; }
                    p2.StartInfo.Arguments = infoArguments;
                    if (ending == ".strm") { p2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; }
                    Directory.SetCurrentDirectory(nitroPath + "\\Data\\Tools");
                    p2.Start();
                    p2.WaitForExit();

                    if (ending == ".sseq") { sdat.files.sseqFiles[tree.SelectedNode.Index] = File.ReadAllBytes("tmp.sseq"); }
                    if (ending == ".strm") { sdat.files.strmFiles[tree.SelectedNode.Index] = File.ReadAllBytes("tmp.strm"); }

                    //Delete the files.
                    File.Delete("tmp");
                    if (ending == ".sseq") { File.Delete("tmp.sseq"); }
                    if (ending == ".strm") { File.Delete("tmp.strm"); }

                    
                    Directory.SetCurrentDirectory(nitroPath);

                    
                }

            }

        }

        //Add above.
        private void addAbove_Click(object sender, EventArgs e)
        {

            //Get indexes.
            int parentIndex = tree.SelectedNode.Parent.Index;
            int index = tree.SelectedNode.Index;

            //Get the new file.
            string folderName = tree.SelectedNode.Parent.Text;

            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select " + folderName + " file.";
            o.Filter = "Sequence File|*.sseq";
            if (folderName == "Sequence Archive") { o.Filter = "Sequence Archive|*.ssar"; }
            if (folderName == "Bank") { o.Filter = "Sound Bank|*.sbnk"; }
            if (folderName == "Wave Archive") { o.Filter = "Wave Archive|*.swar"; }
            if (folderName == "Stream") { o.Filter = "Stream|*.strm"; }
            o.ShowDialog();

            //If not retarded name.
            if (o.FileName != "")
            {

                if (o.FilterIndex == 1)
                {

                    //Add the file.
                    if (folderName == "Sequence") { sdat.files.sseqFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Sequence Archive") { sdat.files.seqArcFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Bank") { sdat.files.bankFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Wave Archive") { sdat.files.waveFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Stream") { sdat.files.strmFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    sdat.fixOffsets();



                }
                else
                {



                }

                //Fix the file IDs.
                if (folderName == "Sequence")
                {

                    foreach (SseqData s in sdat.infoFile.sseqData) {

                        if (s.fileId >= index) { s.fileId += 1; }

                    }

                    foreach (SeqArcData s in sdat.infoFile.seqArcData)
                    {
                        s.fileId += 1;
                    }
                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId += 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                }

                if (folderName == "Sequence Archive")
                {
                    foreach (SeqArcData s in sdat.infoFile.seqArcData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count) { s.fileId += 1; }

                    }

                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId += 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                }

                if (folderName == "Bank")
                {

                    foreach (BankData s in sdat.infoFile.bankData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count) { s.fileId += 1; }

                    }

                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }


                }

                if (folderName == "Wave Archive")
                {

                    foreach (WaveData s in sdat.infoFile.waveData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count) { s.fileId += 1; }

                    }

                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }


                }

                if (folderName == "Stream")
                {
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count + sdat.files.strmFiles.Count) { s.fileId += 1; }

                    }
                }

                //Update nodes.
                updateNodes();

            }
        }

        private void addBelow_Click(object sender, EventArgs e)
        {
            //Get indexes.
            int parentIndex = tree.SelectedNode.Parent.Index;
            int index = tree.SelectedNode.Index+1;

            //Get the new file.
            string folderName = tree.SelectedNode.Parent.Text;

            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select " + folderName + " file.";
            o.Filter = "Sequence File|*.sseq";
            if (folderName == "Sequence Archive") { o.Filter = "Sequence Archive|*.ssar"; }
            if (folderName == "Bank") { o.Filter = "Sound Bank|*.sbnk"; }
            if (folderName == "Wave Archive") { o.Filter = "Wave Archive|*.swar"; }
            if (folderName == "Stream") { o.Filter = "Stream|*.strm"; }
            o.ShowDialog();

            //If not retarded name.
            if (o.FileName != "")
            {

                if (o.FilterIndex == 1)
                {

                    //Add the file.
                    if (folderName == "Sequence") { sdat.files.sseqFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Sequence Archive") { sdat.files.seqArcFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Bank") { sdat.files.bankFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Wave Archive") { sdat.files.waveFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    if (folderName == "Stream") { sdat.files.strmFiles.Insert(index, File.ReadAllBytes(o.FileName)); }
                    sdat.fixOffsets();



                }
                else
                {



                }

                //Fix the file IDs.
                if (folderName == "Sequence")
                {

                    foreach (SseqData s in sdat.infoFile.sseqData)
                    {

                        if (s.fileId >= index) { s.fileId += 1; }

                    }

                    foreach (SeqArcData s in sdat.infoFile.seqArcData)
                    {
                        s.fileId += 1;
                    }
                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId += 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                }

                if (folderName == "Sequence Archive")
                {
                    foreach (SeqArcData s in sdat.infoFile.seqArcData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count) { s.fileId += 1; }

                    }

                    foreach (BankData s in sdat.infoFile.bankData)
                    {
                        s.fileId += 1;
                    }
                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }

                }

                if (folderName == "Bank")
                {

                    foreach (BankData s in sdat.infoFile.bankData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count) { s.fileId += 1; }

                    }

                    foreach (WaveData s in sdat.infoFile.waveData)
                    {
                        s.fileId += 1;
                    }
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }


                }

                if (folderName == "Wave Archive")
                {

                    foreach (WaveData s in sdat.infoFile.waveData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count) { s.fileId += 1; }

                    }

                    foreach (StrmData s in sdat.infoFile.strmData)
                    {
                        s.fileId += 1;
                    }


                }

                if (folderName == "Stream")
                {
                    foreach (StrmData s in sdat.infoFile.strmData)
                    {

                        if (s.fileId >= index + sdat.files.sseqFiles.Count + sdat.files.seqArcFiles.Count + sdat.files.bankFiles.Count + sdat.files.strmFiles.Count) { s.fileId += 1; }

                    }
                }

                //Update nodes.
                updateNodes();

            }
        }

        #endregion FilesNodes



        //Region for each entry right click menu.
        #region EntryMenu

        //Rename a file.
        private void Rename4_Click(object sender, EventArgs e)
                {

                    //Get the good data.
                    int nodeID = tree.SelectedNode.Index;
                    int parentID = tree.SelectedNode.Parent.Index;

                    string oldName = tree.SelectedNode.Text;


                    //Get the string.
                    string newName = Microsoft.VisualBasic.Interaction.InputBox("Please input the new name:", "Input:", oldName);

                    //Rename the actual file.
                    if (newName != oldName && newName != "")
                    {

                        //symbStrings[parentID][0][nodeID] = newName;

                        //Update nodes.
                        updateNodes();

                    }

                }

                //Delete the file.
                private void Delete4_Click(object sender, EventArgs e)
                {
                    //Show confirmation dialog.
                    var result = MessageBox.Show("Are you sure you want to delete the entry? This can cause big problems!", "Warning:", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {

                        //Get the good data.
                        int nodeID = tree.SelectedNode.Index;
                        int parentID = tree.SelectedNode.Parent.Index;

                        //Delete the actual folder.
                        //symbStrings[parentID][0].Remove(nodeID);

                        //Update nodes
                        updateNodes();
                    }

                }



        #endregion EntryMenu



        //Actual node saving file.
        #region nodeSavesFile
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileOpen) { save(); } else { MessageBox.Show("You need to have a file open to save!", "Notice:"); }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileOpen) { saveAs(); } else { MessageBox.Show("You need to have a file open to save!", "Notice:"); }
        }

        #endregion



        //Export and import SDATs.
        #region exportImportSDAT
   
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (fileOpen)
            {

                FolderBrowserDialog f = new FolderBrowserDialog();
                DialogResult d = f.ShowDialog();

                if (d == DialogResult.OK)
                {
                    sdat.extract(f.SelectedPath);
                }

            }

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (fileOpen)
            {

                FolderBrowserDialog f = new FolderBrowserDialog();
                DialogResult d = f.ShowDialog();

                if (d == DialogResult.OK)
                {
                    sdat.compress(f.SelectedPath);
                    sdat.fixOffsets();
                }



                updateNodes();
            }

        }







        #endregion



        //Sub Node Menu.
        #region subNodeMenu
        
        //Add above.
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Parent != null)
            {

                //If seqArc.
                if (tree.SelectedNode.Parent.Parent.Index == 1)
                {

                    //Get name.
                    string newName = Interaction.InputBox("Name the entry:", "Namer");
                    if (newName != "")
                    {

                        //Save indexes.
                        int parentIndex = tree.SelectedNode.Parent.Index;
                        int index = tree.SelectedNode.Index;

                        //Rename the file.
                        symbStringName s = new symbStringName();
                        s.isPlaceHolder = false;
                        s.name = newName;
                        sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index].Insert(tree.SelectedNode.Index, s);
                        updateNodes();

                        tree.SelectedNode = tree.Nodes[1].Nodes[parentIndex].Nodes[index];

                    }

                }

                //If group.
                if (tree.SelectedNode.Parent.Parent.Index == 5)
                {

                    //Get node.
                    int parentIndex = tree.SelectedNode.Parent.Index;
                    int index = tree.SelectedNode.Index;

                    //Add entry.
                    GroupSubData d = new GroupSubData();
                    d.loadFlag = 0;
                    d.nEntry = 0;
                    d.type = 0;
                    d.padding = 0;
                    sdat.infoFile.groupData[parentIndex].subInfo.Insert(index, d);
                    updateNodes();

                    tree.SelectedNode = tree.Nodes[5].Nodes[parentIndex].Nodes[index];

                }
            }
        }

        //Add below.
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Parent != null)
            {

                //If seqArc.
                if (tree.SelectedNode.Parent.Parent.Index == 1)
                {

                    //Get name.
                    string newName = Interaction.InputBox("Name the entry:", "Namer");
                    if (newName != "")
                    {

                        //Save indexes.
                        int parentIndex = tree.SelectedNode.Parent.Index;
                        int index = tree.SelectedNode.Index+1;

                        //Rename the file.
                        symbStringName s = new symbStringName();
                        s.isPlaceHolder = false;
                        s.name = newName;
                        sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index].Insert(index, s);
                        updateNodes();

                        tree.SelectedNode = tree.Nodes[1].Nodes[parentIndex].Nodes[index];

                    }

                }

                //If group.
                if (tree.SelectedNode.Parent.Parent.Index == 5)
                {

                    //Get node.
                    int parentIndex = tree.SelectedNode.Parent.Index;
                    int index = tree.SelectedNode.Index+1;

                    //Add entry.
                    GroupSubData d = new GroupSubData();
                    d.loadFlag = 0;
                    d.nEntry = 0;
                    d.type = 0;
                    d.padding = 0;
                    sdat.infoFile.groupData[parentIndex].subInfo.Insert(index, d);
                    updateNodes();

                    tree.SelectedNode = tree.Nodes[5].Nodes[parentIndex].Nodes[index];

                }
            }
        }

        //Rename
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {

            if (tree.SelectedNode.Parent.Parent.Index == 1)
            {

                string newName = Interaction.InputBox("Rename the entry:", "Renamer");
                if (newName != "")
                {

                    //Rename the file.
                    sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index][tree.SelectedNode.Index].name = newName;
                    updateNodes();

                }

            }
            else {
                MessageBox.Show("Group Entries don't have names!", "Notice:");
            }

        }

        //Delete
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode.Parent.Parent.Index == 1)
            {

                sdat.symbFile.seqArcSubStrings[tree.SelectedNode.Parent.Index].RemoveAt(tree.SelectedNode.Index);
                updateNodes();

            }
            else if (tree.SelectedNode.Parent.Parent.Index == 5) {

                sdat.infoFile.groupData[tree.SelectedNode.Parent.Index].subInfo.RemoveAt(tree.SelectedNode.Index);
                sdat.fixOffsets();
                updateNodes();

            }
        }


        #endregion



        //Help
        #region help
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=_WtYuP4NF1I&index=17&list=PLqLbHe4NpIb5T7evcOl9dll23HC8-i9QT");
            }
            catch { }
        }
        #endregion



        //New file.
        #region newFile
        private void newBetaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Make new file.
            makeNewFile();

        }

        public void makeNewFile() {

            //If file not open.
            if (!fileOpen)
            {

                //Make a new sdat.
                sdat = new sdatFile();

                sdat.files = new sdatFile.fileBlock();
                sdat.symbFile = new symbData();
                sdat.infoFile = new infoData();

                //Fix strings.
                sdat.symbFile.sseqStrings = new List<symbStringName>();
                sdat.symbFile.seqArcStrings = new List<symbStringName>();
                sdat.symbFile.bankStrings = new List<symbStringName>();
                sdat.symbFile.waveStrings = new List<symbStringName>();
                sdat.symbFile.groupStrings = new List<symbStringName>();
                sdat.symbFile.playerStrings = new List<symbStringName>();
                sdat.symbFile.player2Strings = new List<symbStringName>();
                sdat.symbFile.strmStrings = new List<symbStringName>();
                sdat.symbFile.seqArcSubStrings = new List<List<symbStringName>>();

                //Fix info.
                sdat.infoFile.sseqData = new List<SseqData>();
                sdat.infoFile.seqArcData = new List<SeqArcData>();
                sdat.infoFile.bankData = new List<BankData>();
                sdat.infoFile.waveData = new List<WaveData>();
                sdat.infoFile.playerData = new List<PlayerData>();
                sdat.infoFile.groupData = new List<GroupData>();
                sdat.infoFile.player2Data = new List<Player2Data>();
                sdat.infoFile.strmData = new List<StrmData>();

                //Fix files.
                sdat.files.files = new List<byte[]>();
                sdat.files.sseqFiles = new List<byte[]>();
                sdat.files.seqArcFiles = new List<byte[]>();
                sdat.files.bankFiles = new List<byte[]>();
                sdat.files.waveFiles = new List<byte[]>();
                sdat.files.strmFiles = new List<byte[]>();

                //Load node menus.
                for (int i = 0; i < tree.Nodes.Count - 1; i++)
                {
                    tree.Nodes[i].ContextMenuStrip = bigNodeMenu;
                }

                sdatPath = "%BLANK%";

                sdat.fixOffsets();

                fileOpen = true;

                //Update nodes.
                updateNodes();

                this.Text = "Nitro Studio - New_File.sdat";

            }
            else
            {

                //Show warning close menu.
                SaveCloseDialog c = new SaveCloseDialog();
                int h = c.getValue();

                if (h == 0)
                {
                    sdatPath = "%BLANK%";
                    save();
                    closeFile();
                    makeNewFile();
                }
                else if (h == 1) {
                    closeFile();
                    makeNewFile();
                }

            }

        }







        #endregion



        //Gericom player.
        #region gericomPlayer

        private void gericomPlay_Click(object sender, EventArgs e)
        {
            sseqPlayer.Play();
        }

        private void gericomPause_Click(object sender, EventArgs e)
        {
            sseqPlayer.Pause();
        }

        private void gericomStop_Click(object sender, EventArgs e)
        {
            sseqPlayer.Stop();
        }

        private void onClosingPlayer(object sender, FormClosingEventArgs e)
        {

            if (sseqPlayer != null) { sseqPlayer.Stop();}

        }









        #endregion

        
    }
}
