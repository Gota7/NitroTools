namespace NitroStudio
{
    partial class SwarEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("SWAR");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SwarEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.swavGroup = new System.Windows.Forms.Panel();
            this.updateDataButton = new System.Windows.Forms.Button();
            this.soundPlayerDeluxeTM = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.playSoundPlaybackBox = new System.Windows.Forms.Button();
            this.stopSoundPlaybackBox = new System.Windows.Forms.Button();
            this.nonLoopLengthBox = new System.Windows.Forms.NumericUpDown();
            this.loopOffsetBox = new System.Windows.Forms.NumericUpDown();
            this.nTimeBox = new System.Windows.Forms.NumericUpDown();
            this.samplingBox = new System.Windows.Forms.NumericUpDown();
            this.loopBox = new System.Windows.Forms.CheckBox();
            this.nonLoopLengthLabel = new System.Windows.Forms.Label();
            this.loopOffsetLabel = new System.Windows.Forms.Label();
            this.nTimeLabel = new System.Windows.Forms.Label();
            this.samplingLabel = new System.Windows.Forms.Label();
            this.loopsLabel = new System.Windows.Forms.Label();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.noInfoLabelPanel = new System.Windows.Forms.Panel();
            this.noInfoLabel = new System.Windows.Forms.Label();
            this.tree = new System.Windows.Forms.TreeView();
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newBetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.addBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.Export = new System.Windows.Forms.ToolStripMenuItem();
            this.Import = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.blockMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.expandToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.bytesSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.soundSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.volume = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.swavGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nonLoopLengthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopOffsetBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTimeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplingBox)).BeginInit();
            this.noInfoLabelPanel.SuspendLayout();
            this.menu.SuspendLayout();
            this.soundMenu.SuspendLayout();
            this.blockMenu.SuspendLayout();
            this.bigMenu.SuspendLayout();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.swavGroup);
            this.splitContainer1.Panel1.Controls.Add(this.noInfoLabelPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tree);
            this.splitContainer1.Size = new System.Drawing.Size(726, 416);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 0;
            // 
            // swavGroup
            // 
            this.swavGroup.Controls.Add(this.label1);
            this.swavGroup.Controls.Add(this.volume);
            this.swavGroup.Controls.Add(this.updateDataButton);
            this.swavGroup.Controls.Add(this.soundPlayerDeluxeTM);
            this.swavGroup.Controls.Add(this.tableLayoutPanel1);
            this.swavGroup.Controls.Add(this.nonLoopLengthBox);
            this.swavGroup.Controls.Add(this.loopOffsetBox);
            this.swavGroup.Controls.Add(this.nTimeBox);
            this.swavGroup.Controls.Add(this.samplingBox);
            this.swavGroup.Controls.Add(this.loopBox);
            this.swavGroup.Controls.Add(this.nonLoopLengthLabel);
            this.swavGroup.Controls.Add(this.loopOffsetLabel);
            this.swavGroup.Controls.Add(this.nTimeLabel);
            this.swavGroup.Controls.Add(this.samplingLabel);
            this.swavGroup.Controls.Add(this.loopsLabel);
            this.swavGroup.Controls.Add(this.typeBox);
            this.swavGroup.Controls.Add(this.typeLabel);
            this.swavGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.swavGroup.Location = new System.Drawing.Point(0, 0);
            this.swavGroup.Name = "swavGroup";
            this.swavGroup.Size = new System.Drawing.Size(210, 414);
            this.swavGroup.TabIndex = 0;
            this.swavGroup.Visible = false;
            // 
            // updateDataButton
            // 
            this.updateDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateDataButton.Location = new System.Drawing.Point(3, 243);
            this.updateDataButton.Name = "updateDataButton";
            this.updateDataButton.Size = new System.Drawing.Size(204, 23);
            this.updateDataButton.TabIndex = 19;
            this.updateDataButton.Text = "Update Data:";
            this.updateDataButton.UseVisualStyleBackColor = true;
            this.updateDataButton.Click += new System.EventHandler(this.updateDataButton_Click);
            // 
            // soundPlayerDeluxeTM
            // 
            this.soundPlayerDeluxeTM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundPlayerDeluxeTM.Location = new System.Drawing.Point(5, 358);
            this.soundPlayerDeluxeTM.Name = "soundPlayerDeluxeTM";
            this.soundPlayerDeluxeTM.Size = new System.Drawing.Size(202, 19);
            this.soundPlayerDeluxeTM.TabIndex = 16;
            this.soundPlayerDeluxeTM.Text = "Sound Player Deluxe™";
            this.soundPlayerDeluxeTM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.playSoundPlaybackBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.stopSoundPlaybackBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 380);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(210, 34);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // playSoundPlaybackBox
            // 
            this.playSoundPlaybackBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playSoundPlaybackBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playSoundPlaybackBox.Location = new System.Drawing.Point(3, 3);
            this.playSoundPlaybackBox.Name = "playSoundPlaybackBox";
            this.playSoundPlaybackBox.Size = new System.Drawing.Size(99, 28);
            this.playSoundPlaybackBox.TabIndex = 13;
            this.playSoundPlaybackBox.Text = "Play Sound";
            this.playSoundPlaybackBox.UseVisualStyleBackColor = true;
            this.playSoundPlaybackBox.Click += new System.EventHandler(this.playSoundPlaybackBox_Click);
            // 
            // stopSoundPlaybackBox
            // 
            this.stopSoundPlaybackBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.stopSoundPlaybackBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopSoundPlaybackBox.Location = new System.Drawing.Point(108, 3);
            this.stopSoundPlaybackBox.Name = "stopSoundPlaybackBox";
            this.stopSoundPlaybackBox.Size = new System.Drawing.Size(99, 28);
            this.stopSoundPlaybackBox.TabIndex = 14;
            this.stopSoundPlaybackBox.Text = "Stop Sound";
            this.stopSoundPlaybackBox.UseVisualStyleBackColor = true;
            this.stopSoundPlaybackBox.Click += new System.EventHandler(this.stopSoundPlaybackBox_Click);
            // 
            // nonLoopLengthBox
            // 
            this.nonLoopLengthBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nonLoopLengthBox.Location = new System.Drawing.Point(3, 221);
            this.nonLoopLengthBox.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nonLoopLengthBox.Name = "nonLoopLengthBox";
            this.nonLoopLengthBox.Size = new System.Drawing.Size(204, 20);
            this.nonLoopLengthBox.TabIndex = 11;
            this.nonLoopLengthBox.ValueChanged += new System.EventHandler(this.onValueBoxChanged);
            // 
            // loopOffsetBox
            // 
            this.loopOffsetBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loopOffsetBox.Location = new System.Drawing.Point(3, 179);
            this.loopOffsetBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.loopOffsetBox.Name = "loopOffsetBox";
            this.loopOffsetBox.Size = new System.Drawing.Size(204, 20);
            this.loopOffsetBox.TabIndex = 10;
            this.loopOffsetBox.ValueChanged += new System.EventHandler(this.onValueBoxChanged);
            // 
            // nTimeBox
            // 
            this.nTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nTimeBox.Location = new System.Drawing.Point(3, 141);
            this.nTimeBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nTimeBox.Name = "nTimeBox";
            this.nTimeBox.Size = new System.Drawing.Size(204, 20);
            this.nTimeBox.TabIndex = 9;
            this.nTimeBox.ValueChanged += new System.EventHandler(this.onValueBoxChanged);
            // 
            // samplingBox
            // 
            this.samplingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.samplingBox.Location = new System.Drawing.Point(3, 102);
            this.samplingBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.samplingBox.Name = "samplingBox";
            this.samplingBox.Size = new System.Drawing.Size(204, 20);
            this.samplingBox.TabIndex = 8;
            this.samplingBox.ValueChanged += new System.EventHandler(this.onValueBoxChanged);
            // 
            // loopBox
            // 
            this.loopBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loopBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loopBox.Location = new System.Drawing.Point(3, 66);
            this.loopBox.Name = "loopBox";
            this.loopBox.Size = new System.Drawing.Size(204, 17);
            this.loopBox.TabIndex = 7;
            this.loopBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loopBox.UseVisualStyleBackColor = true;
            this.loopBox.CheckedChanged += new System.EventHandler(this.onCheckBoxChanged);
            // 
            // nonLoopLengthLabel
            // 
            this.nonLoopLengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nonLoopLengthLabel.Location = new System.Drawing.Point(3, 202);
            this.nonLoopLengthLabel.Name = "nonLoopLengthLabel";
            this.nonLoopLengthLabel.Size = new System.Drawing.Size(204, 16);
            this.nonLoopLengthLabel.TabIndex = 6;
            this.nonLoopLengthLabel.Text = "Non Loop Length";
            this.nonLoopLengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nonLoopLengthLabel.Click += new System.EventHandler(this.label6_Click);
            // 
            // loopOffsetLabel
            // 
            this.loopOffsetLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loopOffsetLabel.Location = new System.Drawing.Point(3, 164);
            this.loopOffsetLabel.Name = "loopOffsetLabel";
            this.loopOffsetLabel.Size = new System.Drawing.Size(204, 12);
            this.loopOffsetLabel.TabIndex = 5;
            this.loopOffsetLabel.Text = "Loop Offset";
            this.loopOffsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nTimeLabel
            // 
            this.nTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nTimeLabel.Location = new System.Drawing.Point(2, 125);
            this.nTimeLabel.Name = "nTimeLabel";
            this.nTimeLabel.Size = new System.Drawing.Size(205, 13);
            this.nTimeLabel.TabIndex = 4;
            this.nTimeLabel.Text = "nTime";
            this.nTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // samplingLabel
            // 
            this.samplingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.samplingLabel.Location = new System.Drawing.Point(3, 86);
            this.samplingLabel.Name = "samplingLabel";
            this.samplingLabel.Size = new System.Drawing.Size(204, 13);
            this.samplingLabel.TabIndex = 3;
            this.samplingLabel.Text = "Sampling Rate:";
            this.samplingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.samplingLabel.Click += new System.EventHandler(this.samplingLabel_Click);
            // 
            // loopsLabel
            // 
            this.loopsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loopsLabel.Location = new System.Drawing.Point(3, 40);
            this.loopsLabel.Name = "loopsLabel";
            this.loopsLabel.Size = new System.Drawing.Size(204, 23);
            this.loopsLabel.TabIndex = 2;
            this.loopsLabel.Text = "Loop Flag:";
            this.loopsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // typeBox
            // 
            this.typeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeBox.AutoCompleteCustomSource.AddRange(new string[] {
            "PCM8",
            "PCM16",
            "(IMA-)ADPCM"});
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.Enabled = false;
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {
            "0 - PCM8",
            "1 - PCM16",
            "2- (IMA-)ADPCM"});
            this.typeBox.Location = new System.Drawing.Point(3, 16);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(204, 21);
            this.typeBox.TabIndex = 1;
            this.typeBox.SelectedIndexChanged += new System.EventHandler(this.onDropBoxChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeLabel.Location = new System.Drawing.Point(3, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(204, 13);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "Type:";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // noInfoLabelPanel
            // 
            this.noInfoLabelPanel.Controls.Add(this.noInfoLabel);
            this.noInfoLabelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noInfoLabelPanel.Location = new System.Drawing.Point(0, 0);
            this.noInfoLabelPanel.Name = "noInfoLabelPanel";
            this.noInfoLabelPanel.Size = new System.Drawing.Size(210, 414);
            this.noInfoLabelPanel.TabIndex = 13;
            // 
            // noInfoLabel
            // 
            this.noInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noInfoLabel.Location = new System.Drawing.Point(-1, 0);
            this.noInfoLabel.Name = "noInfoLabel";
            this.noInfoLabel.Size = new System.Drawing.Size(235, 414);
            this.noInfoLabel.TabIndex = 0;
            this.noInfoLabel.Text = "No Valid Info Selected!";
            this.noInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.ImageIndex = 0;
            this.tree.ImageList = this.icons;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            treeNode1.Name = "SWAR";
            treeNode1.Text = "SWAR";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tree.SelectedImageIndex = 0;
            this.tree.Size = new System.Drawing.Size(508, 414);
            this.tree.TabIndex = 0;
            this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
            this.tree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.onNodeDoubleClick);
            this.tree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tree_NodeKey);
            // 
            // icons
            // 
            this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
            this.icons.TransparentColor = System.Drawing.Color.Transparent;
            this.icons.Images.SetKeyName(0, "waveArchive.png");
            this.icons.Images.SetKeyName(1, "wave.png");
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(726, 24);
            this.menu.TabIndex = 11;
            this.menu.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBetaToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newBetaToolStripMenuItem
            // 
            this.newBetaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newBetaToolStripMenuItem.Image")));
            this.newBetaToolStripMenuItem.Name = "newBetaToolStripMenuItem";
            this.newBetaToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.newBetaToolStripMenuItem.Text = "New";
            this.newBetaToolStripMenuItem.Click += new System.EventHandler(this.newBetaToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openToolStripMenuItem.Text = "Import From File";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quitToolStripMenuItem.Image")));
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importToolStripMenuItem.Image")));
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.importToolStripMenuItem.Text = "Import From Folder";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportToolStripMenuItem.Image")));
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exportToolStripMenuItem.Text = "Export To Folder";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // soundMenu
            // 
            this.soundMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAbove,
            this.addBelow,
            this.Export,
            this.Import,
            this.Delete});
            this.soundMenu.Name = "soundMenu";
            this.soundMenu.Size = new System.Drawing.Size(134, 114);
            // 
            // addAbove
            // 
            this.addAbove.Image = ((System.Drawing.Image)(resources.GetObject("addAbove.Image")));
            this.addAbove.Name = "addAbove";
            this.addAbove.Size = new System.Drawing.Size(133, 22);
            this.addAbove.Text = "Add Above";
            this.addAbove.Click += new System.EventHandler(this.addAbove_Click);
            // 
            // addBelow
            // 
            this.addBelow.Image = ((System.Drawing.Image)(resources.GetObject("addBelow.Image")));
            this.addBelow.Name = "addBelow";
            this.addBelow.Size = new System.Drawing.Size(133, 22);
            this.addBelow.Text = "Add Below";
            this.addBelow.Click += new System.EventHandler(this.addBelow_Click);
            // 
            // Export
            // 
            this.Export.Image = ((System.Drawing.Image)(resources.GetObject("Export.Image")));
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(133, 22);
            this.Export.Text = "Export";
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Import
            // 
            this.Import.Image = ((System.Drawing.Image)(resources.GetObject("Import.Image")));
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(133, 22);
            this.Import.Text = "Import";
            this.Import.Click += new System.EventHandler(this.Import_Click);
            // 
            // Delete
            // 
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(133, 22);
            this.Delete.Text = "Delete";
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // blockMenu
            // 
            this.blockMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.expandToolStripMenuItem,
            this.collapseToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.blockMenu.Name = "blockMenu";
            this.blockMenu.Size = new System.Drawing.Size(120, 92);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addToolStripMenuItem.Image")));
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("expandToolStripMenuItem.Image")));
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.expandToolStripMenuItem.Text = "Expand";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("collapseToolStripMenuItem.Image")));
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.collapseToolStripMenuItem.Text = "Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // bigMenu
            // 
            this.bigMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.expandToolStripMenuItem1,
            this.collapseToolStripMenuItem1});
            this.bigMenu.Name = "bigMenu";
            this.bigMenu.Size = new System.Drawing.Size(120, 70);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("addToolStripMenuItem1.Image")));
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // expandToolStripMenuItem1
            // 
            this.expandToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("expandToolStripMenuItem1.Image")));
            this.expandToolStripMenuItem1.Name = "expandToolStripMenuItem1";
            this.expandToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.expandToolStripMenuItem1.Text = "Expand";
            this.expandToolStripMenuItem1.Click += new System.EventHandler(this.expandToolStripMenuItem1_Click);
            // 
            // collapseToolStripMenuItem1
            // 
            this.collapseToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("collapseToolStripMenuItem1.Image")));
            this.collapseToolStripMenuItem1.Name = "collapseToolStripMenuItem1";
            this.collapseToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.collapseToolStripMenuItem1.Text = "Collapse";
            this.collapseToolStripMenuItem1.Click += new System.EventHandler(this.collapseToolStripMenuItem1_Click);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bytesSelected,
            this.soundSelected});
            this.status.Location = new System.Drawing.Point(0, 440);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(726, 22);
            this.status.TabIndex = 12;
            this.status.Text = "status";
            // 
            // bytesSelected
            // 
            this.bytesSelected.Name = "bytesSelected";
            this.bytesSelected.Size = new System.Drawing.Size(103, 17);
            this.bytesSelected.Text = "No bytes selected!";
            // 
            // soundSelected
            // 
            this.soundSelected.Name = "soundSelected";
            this.soundSelected.Size = new System.Drawing.Size(108, 17);
            this.soundSelected.Text = "No sound selected!";
            // 
            // volume
            // 
            this.volume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volume.Location = new System.Drawing.Point(3, 310);
            this.volume.Maximum = 100;
            this.volume.Name = "volume";
            this.volume.Size = new System.Drawing.Size(204, 45);
            this.volume.TabIndex = 20;
            this.volume.TickFrequency = 10;
            this.volume.Value = 50;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(5, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "Volume:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SwarEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 462);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.status);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SwarEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Swar Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.onClose);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.swavGroup.ResumeLayout(false);
            this.swavGroup.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nonLoopLengthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopOffsetBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTimeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplingBox)).EndInit();
            this.noInfoLabelPanel.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.soundMenu.ResumeLayout(false);
            this.blockMenu.ResumeLayout(false);
            this.bigMenu.ResumeLayout(false);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.ImageList icons;
        private System.Windows.Forms.Panel swavGroup;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.NumericUpDown nTimeBox;
        private System.Windows.Forms.NumericUpDown samplingBox;
        private System.Windows.Forms.CheckBox loopBox;
        private System.Windows.Forms.Label nonLoopLengthLabel;
        private System.Windows.Forms.Label loopOffsetLabel;
        private System.Windows.Forms.Label nTimeLabel;
        private System.Windows.Forms.Label samplingLabel;
        private System.Windows.Forms.Label loopsLabel;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newBetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown nonLoopLengthBox;
        private System.Windows.Forms.NumericUpDown loopOffsetBox;
        private System.Windows.Forms.ContextMenuStrip soundMenu;
        private System.Windows.Forms.ToolStripMenuItem Export;
        private System.Windows.Forms.ToolStripMenuItem Import;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private System.Windows.Forms.ToolStripMenuItem addAbove;
        private System.Windows.Forms.ToolStripMenuItem addBelow;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Panel noInfoLabelPanel;
        private System.Windows.Forms.Label noInfoLabel;
        private System.Windows.Forms.ContextMenuStrip blockMenu;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button stopSoundPlaybackBox;
        private System.Windows.Forms.Button playSoundPlaybackBox;
        private System.Windows.Forms.Label soundPlayerDeluxeTM;
        private System.Windows.Forms.ContextMenuStrip bigMenu;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem1;
        private System.Windows.Forms.Button updateDataButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel bytesSelected;
        private System.Windows.Forms.ToolStripStatusLabel soundSelected;
        private System.Windows.Forms.TrackBar volume;
        private System.Windows.Forms.Label label1;
    }
}