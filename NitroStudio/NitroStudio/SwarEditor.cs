﻿using System;
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
using Syroot.BinaryData;
using System.Media;
using System.ComponentModel.Design;
using NAudio.Wave;
using SoundNStream;
using NAudio.Wave.SampleProviders;
using System.Threading;

namespace NitroStudio
{
    public partial class SwarEditor : Form
    {

        public string nitroPath = Application.StartupPath;
        swarFile file;
        private MainWindow parent;
        int parentIndex;
        swavFile currentSwavFile;

        //Sound player.
        public WaveOutEvent player;
        public IWaveProvider playerFile;
        public WaveChannel32 waveChannel;
        public WaveStream waveStream;
        public byte[] swavFile;
        swav loopRef = new swav();

        public SwarEditor(MainWindow parent, byte[] b, string name, int index)
        {
            InitializeComponent();

            //Load swar.
            file = new swarFile();
            file.load(b);

            //Change nodes.
            this.Text = name;
            tree.Nodes[0].Text = name;

            //Update nodes.
            updateNodes();

            this.parent = parent;
            parentIndex = index;

        }


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
            tree.Nodes[0].ContextMenuStrip = blockMenu;

            for (int i = 0; i < 1; i++) {

                //Add nodes.
                //tree.Nodes[0].Nodes.Add("Block " + i);

                for (int j = 0; j < file.data[i].files.Length; j++) {

                    tree.Nodes[0].Nodes.Add("Sound " + j, "Sound " + j, 1, 1);
                    tree.Nodes[0].LastNode.ContextMenuStrip = soundMenu;

                }

            }

            //tree.Nodes[0].ContextMenuStrip = bigMenu;

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


        //Sound menu.
        #region soundMenu

        //Add above.
        private void addAbove_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Swav File|*.swav|Wave|*.wav";
            f.Title = "Import the file";
            f.ShowDialog();

            if (f.FileName != "")
            {

                if (f.FilterIndex == 1)
                {

                    List<byte[]> files = file.data[0].files.ToList();
                    files.Insert(tree.SelectedNode.Index, File.ReadAllBytes(f.FileName));
                    file.data[0].files = files.ToArray();

                    file.fixOffsets();
                    updateNodes();

                }
                else
                {

                    //Make new swav.
                    RIFF r = new RIFF();
                    r.load(File.ReadAllBytes(f.FileName));

                    List<byte[]> files = file.data[0].files.ToList();
                    files.Insert(tree.SelectedNode.Index, r.toSwav().toBytes());
                    file.data[0].files = files.ToArray();

                    file.fixOffsets();
                    updateNodes();

                }
            }
        }

        //Add below.
        private void addBelow_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Swav File|*.swav|Wave|*.wav";
            f.Title = "Import the file";
            f.ShowDialog();

            if (f.FileName != "")
            {

                if (f.FilterIndex == 1)
                {

                    List<byte[]> files = file.data[0].files.ToList();

                    files.Insert(tree.SelectedNode.Index + 1, File.ReadAllBytes(f.FileName));
                
                    file.data[0].files = files.ToArray();

                    file.fixOffsets();
                    updateNodes();

                }
                else
                {

                    //Make new swav.
                    RIFF r = new RIFF();
                    r.load(File.ReadAllBytes(f.FileName));

                    List<byte[]> files = file.data[0].files.ToList();
                    files.Insert(tree.SelectedNode.Index + 1, r.toSwav().toBytes());
                    file.data[0].files = files.ToArray();

                    file.fixOffsets();
                    updateNodes();

                }
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Supported Files|*.swav;*.wav|Swav File|*.swav|Wave|*.wav";
            f.Title = "Export the file";
            f.FileName = tree.SelectedNode.Text;
            f.ShowDialog();

            if (f.FileName != "")
            {

                if (f.FileName.EndsWith(".swav")) { f.FilterIndex = 1; }
                if (f.FileName.EndsWith(".wav")) { f.FilterIndex = 2; }

                if (f.FilterIndex == 1)
                {
                    File.WriteAllBytes(f.FileName, file.data[0].files[tree.SelectedNode.Index]);
                }
                else {
                    swav s = new swav();
                    s.load(file.data[0].files[tree.SelectedNode.Index]);
                    bool includeLoop = false;
                    if (s.data.info.loopFlag == 1) { includeLoop = true; }
                    File.WriteAllBytes(f.FileName, s.toRIFF().toBytes(true, includeLoop));
                }
            }
        }

        private void Import_Click(object sender, EventArgs e)
        {

            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Supported Files|*.swav;*.wav|Swav File|*.swav|Wave|*.wav";
            f.Title = "Import the file";
            f.ShowDialog();

            if (f.FileName != "") {


                if (f.FileName.EndsWith(".swav")) { f.FilterIndex = 1; }
                if (f.FileName.EndsWith(".wav")) { f.FilterIndex = 2; }

                if (f.FilterIndex == 1)
                {

                    file.data[0].files[tree.SelectedNode.Index] = File.ReadAllBytes(f.FileName);

                }
                else {

                    //Make new swav.
                    RIFF r = new RIFF();
                    r.load(File.ReadAllBytes(f.FileName));
                    file.data[0].files[tree.SelectedNode.Index] = r.toSwav().toBytes();

                }

            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            List<byte[]> f = file.data[0].files.ToList();
            f.RemoveAt(tree.SelectedNode.Index);
            file.data[0].files = f.ToArray();
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


        //Info Editor.
        #region infoEditor

        public void doInfoStuff() {

            if (tree.SelectedNode != null) {

                if (tree.SelectedNode.Parent != null) {

                    if (tree.SelectedNode.Text.StartsWith("Sound"))
                    {

                        soundSelected.Text = "Sound: " + tree.SelectedNode.Index;
                        bytesSelected.Text = file.data[0].files[tree.SelectedNode.Index].Length + " bytes.";

                        //Hide no info label.
                        noInfoLabel.Hide();

                        //Show swav group.
                        swavGroup.Show();

                        //Get info.
                        currentSwavFile = new swavFile();
                        currentSwavFile.load(file.data[0].files[tree.SelectedNode.Index]);

                        typeBox.SelectedIndex = currentSwavFile.data[0].info.waveType;
                        if ((int)currentSwavFile.data[0].info.loopFlag == 0) { loopBox.Checked = false;  } else { loopBox.Checked = true; }
                        samplingBox.Value = currentSwavFile.data[0].info.nSampleRate;
                        nTimeBox.Value = currentSwavFile.data[0].info.nTime;
                        loopOffsetBox.Value = currentSwavFile.data[0].info.loopOffset;
                        nonLoopLengthBox.Value = currentSwavFile.data[0].info.nonLoopLength;


                    }
                    else {

                        //Hide swav group.
                        swavGroup.Hide();

                        //Show no info label.
                        noInfoLabel.Show();

                        soundSelected.Text = "No sound selected!";
                        bytesSelected.Text = "No bytes selected!";

                    }

                }
                else
                {

                    //Hide swav group.
                    swavGroup.Hide();

                    //Show no info label.
                    noInfoLabel.Show();

                    soundSelected.Text = "No sound selected!";
                    bytesSelected.Text = "No bytes selected!";

                }

            }

        }

        #endregion


        //On info changed.
        #region infoChanged

        //Rewrite file.
        public void rewriteFile() {
            /*
            //Set info.
            currentSwavFile.data[0].info.waveType = (byte)typeBox.SelectedIndex;
            if (loopBox.Checked) { currentSwavFile.data[0].info.loopFlag = 1; } else { currentSwavFile.data[0].info.loopFlag = 0; }
            currentSwavFile.data[0].info.nSampleRate = (UInt16)samplingBox.Value;
            currentSwavFile.data[0].info.nTime = (UInt16)nTimeBox.Value;
            currentSwavFile.data[0].info.loopOffset = (UInt16)loopOffsetBox.Value;
            currentSwavFile.data[0].info.nonLoopLength = (UInt32)nonLoopLengthBox.Value;

            file.data[0].files[tree.SelectedNode.Index] = currentSwavFile.toBytes();
            tree.SelectedNode = tree.SelectedNode;
            */
        }

        public void onDropBoxChanged(object sender, EventArgs e) { rewriteFile(); }
        public void onCheckBoxChanged(object sender, EventArgs e) { rewriteFile(); }
        public void onValueBoxChanged(object sender, EventArgs e) { rewriteFile(); }


        private void updateDataButton_Click(object sender, EventArgs e)
        {
            //Set info.
            currentSwavFile = new swavFile();
            currentSwavFile.load(file.data[0].files[tree.SelectedNode.Index]);

            currentSwavFile.data[0].info.waveType = (byte)typeBox.SelectedIndex;
            if (loopBox.Checked) { currentSwavFile.data[0].info.loopFlag = 1; } else { currentSwavFile.data[0].info.loopFlag = 0; }
            currentSwavFile.data[0].info.nSampleRate = (UInt16)samplingBox.Value;
            currentSwavFile.data[0].info.nTime = (UInt16)nTimeBox.Value;
            currentSwavFile.data[0].info.loopOffset = (UInt16)loopOffsetBox.Value;
            currentSwavFile.data[0].info.nonLoopLength = (UInt32)nonLoopLengthBox.Value;

            file.data[0].files[tree.SelectedNode.Index] = currentSwavFile.toBytes();
            tree.SelectedNode = tree.SelectedNode;
            doInfoStuff();
        }

        #endregion


        //Save
        #region save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file.fixOffsets();
            parent.sdat.files.waveFiles[parentIndex] = file.toBytes();
        }
        #endregion


        //Play or stop sound.
        #region playSound
        
        private void loopPlaybackCheckbox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void playSoundPlaybackBox_Click(object sender, EventArgs e)
        {

            try
            {
                player.Stop();
                player.Dispose();
                
            }
            catch { }

            try
            {
                waveStream.Dispose();
                waveChannel.Dispose();

            }
            catch { }

            //Convert swav to wav.
            swav s = new swav();
            swavFile = file.data[0].files[tree.SelectedNode.Index];
            s.load(swavFile);
            player = new WaveOutEvent();
            waveStream = new WaveFileReader(new MemoryStream(s.toRIFF().toBytes()));
            waveChannel = new WaveChannel32(waveStream);
            waveChannel.Volume = (float)volume.Value * (float).01;
            player.Init(waveChannel);
            player.Play();
            //loopRef = s;
            s.data.info.loopFlag = 0;
            if (s.data.info.loopFlag == 1) { player.PlaybackStopped += new EventHandler(loopSound); } else { player.PlaybackStopped += new EventHandler(stopSoundPlaybackBox_Click); }
            if (s.data.info.loopFlag != 1) { Thread.Sleep(500); }
            

        }

        //Stop sound.
        private void stopSoundPlaybackBox_Click(object sender, EventArgs e)
        {

            try
            {
                //Stop sound.
                player.Stop();
                player.Dispose();
            }
            catch { }

            try
            {
                waveStream.Dispose();
                waveChannel.Dispose();

            }
            catch { }
        }


        public void loopSound(object sender, EventArgs e) {

            try
            {
                switch (loopRef.data.info.waveType)
                {

                    case 0:
                        waveChannel.Position = loopRef.data.info.nloopOffset * 4;
                        break;

                    case 1:
                        waveChannel.Position = loopRef.data.info.nloopOffset * 2;
                        break;

                    case 2:
                        waveChannel.Position = loopRef.data.info.nloopOffset * 8;
                        break;

                }
            }
            catch { }

            playSoundPlaybackBox_Click(sender, e);

        }

        #endregion


        //swavOpener
        #region swavOpener
        //Open SWAV in hex editor.
        public void onNodeDoubleClick(object sender, MouseEventArgs e) {

            if (tree.SelectedNode != null)
            {

                if (tree.SelectedNode.Parent != null)
                {

                    if (tree.SelectedNode.Parent.Parent != null)
                    {

                        int index = tree.SelectedNode.Index;

                        ByteViewerForm f = new ByteViewerForm(file.data[0].files[index], tree.SelectedNode.Text);
                        f.Show();

                    }
                }

            }
        }
        #endregion


        //Big node
        #region bigNode

        private void expandToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        private void collapseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            file.nBlocks += 1;

            List<swarFile.swarData> sList = file.data.ToList();

            swarFile.swarData f = new swarFile.swarData();
            f.files = new byte[0][];
            sList.Add(f);
            file.data = sList.ToArray();

            file.fixOffsets();

            updateNodes();

        }



        #endregion


        //Block nodes.
        #region blockNode
        
        //Expand.
        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Expand();
        }

        //Collapse.
        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tree.SelectedNode.Collapse();
        }

        //Add.
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Swav File|*.swav|Wave|*.wav";
            f.Title = "Import the file";
            f.ShowDialog();

            if (f.FileName != "")
            {

                if (f.FilterIndex == 1)
                {

                    List<byte[]> files = file.data[tree.SelectedNode.Index].files.ToList();
                    files.Add(File.ReadAllBytes(f.FileName));
                    file.data[tree.SelectedNode.Index].files = files.ToArray();

                    file.fixOffsets();
                    updateNodes();

                }
                else
                {

                    //Soundout.
                    RIFF r = new RIFF();
                    r.load(File.ReadAllBytes(f.FileName));

                    List<byte[]> files = file.data[tree.SelectedNode.Index].files.ToList();
                    files.Add(r.toSwav().toBytes());
                    file.data[tree.SelectedNode.Index].files = files.ToArray();

                    file.fixOffsets();
                    updateNodes();

                }
            }
        }

        //Delete the block of data.
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<swarFile.swarData> s = file.data.ToList();
            s.RemoveAt(tree.SelectedNode.Index);
            file.data = s.ToArray();

            file.fixOffsets();
            updateNodes();
        }




        #endregion


        //Everything else.
        #region everythingElse

        //New
        private void newBetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file = new swarFile();
            file.data = new swarFile.swarData[0];
            file.fixOffsets();
            updateNodes();
        }

        //Quit
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Swarchive|*.swar";
            f.Title = "Import the file";
            f.ShowDialog();

            if (f.FileName != "")
            {
                file = new swarFile();
                file.load(File.ReadAllBytes(f.FileName));

                file.fixOffsets();
                updateNodes();
            }
        }

        //Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();

            s.Filter = "Swarchive|*.swar";
            s.RestoreDirectory = true;

            if (s.ShowDialog() == DialogResult.OK)
            {
                file.fixOffsets();
                File.WriteAllBytes(s.FileName, file.toBytes());
            }
        }

        //On Close.
        public void onClose(object sender, System.EventArgs e) {

            try
            {
                player.Stop();
                player.Dispose();
            }
            catch { }

            try
            {
                waveStream.Dispose();
                waveChannel.Dispose();

            }
            catch { }

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void samplingLabel_Click(object sender, EventArgs e)
        {

        }

        #endregion

        
    }


    /// <summary>
    /// Swav file.
    /// </summary>
    public class swavFile {

        public char[] magic; //SWAV
        public UInt32 identifier; //0x0100feff
        public UInt32 fileSize; //File size.
        public UInt16 nSize; //16.
        public UInt16 nBlocks; //Usually 1.
        public swavData[] data; //Data

        //Data block.
        public struct swavData {

            public char[] magic; //DATA
            public UInt32 size; //Size of data.
            public swavInfo info; //Info.
            public byte[] data; //File data.

        }

        //Swav info.
        public struct swavInfo
        {
            public byte waveType; //Wave type.
            public byte loopFlag; //Loop Flag.
            public UInt16 nSampleRate; //Sample Rate.
            public UInt16 nTime; //nTime.
            public UInt16 loopOffset; //Loop Offset.
            public UInt32 nonLoopLength; //Non-loop length.
        }


        /// <summary>
        /// Load a file.
        /// </summary>
        /// <param name="b"></param>
        public void load(byte[] b) {

            MemoryStream src = new MemoryStream(b);
            BinaryDataReader br = new BinaryDataReader(src);

            magic = br.ReadChars(4);
            identifier = br.ReadUInt32();
            fileSize = br.ReadUInt32();
            nSize = br.ReadUInt16();
            nBlocks = br.ReadUInt16();

            data = new swavData[(int)nBlocks];

            for (int i = 0; i < data.Length; i++) {

                data[i].magic = br.ReadChars(4);
                data[i].size = br.ReadUInt32();
                data[i].info.waveType = br.ReadByte();
                data[i].info.loopFlag = br.ReadByte();
                data[i].info.nSampleRate = br.ReadUInt16();
                data[i].info.nTime = br.ReadUInt16();
                data[i].info.loopOffset = br.ReadUInt16();
                data[i].info.nonLoopLength = br.ReadUInt32();
                data[i].data = br.ReadBytes((int)(data[i].size - (8 + 12)));

            }

        }


        public byte[] toBytes() {

            MemoryStream src = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(src);

            bw.Write(magic);
            bw.Write(identifier);
            bw.Write(fileSize);
            bw.Write(nSize);
            bw.Write(nBlocks);

            foreach (swavData s in data) {

                bw.Write(s.magic);
                bw.Write(s.size);
                bw.Write(s.info.waveType);
                bw.Write(s.info.loopFlag);
                bw.Write(s.info.nSampleRate);
                bw.Write(s.info.nTime);
                bw.Write(s.info.loopOffset);
                bw.Write(s.info.nonLoopLength);
                bw.Write(s.data);

            }

            return src.ToArray();

        }

    }
}
