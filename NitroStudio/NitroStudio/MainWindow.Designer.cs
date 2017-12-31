namespace NitroStudio
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Sound Sequence");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Sequence Archive", 1, 1);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Instrument Bank", 2, 2);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Wave", 3, 3);
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Player", 4, 4);
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Group", 5, 5);
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Stream Player", 6, 6);
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Stream", 7, 7);
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Sequence", 8, 8);
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Sequence Archive", 8, 8);
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Bank", 8, 8);
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Wave Archive", 8, 8);
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Stream", 8, 8);
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("FILES", 8, 8, new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26,
            treeNode27});
            this.bigFolderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newBetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutNitroComposerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.byteSelect = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileIdBox = new System.Windows.Forms.ComboBox();
            this.fileIdLabel = new System.Windows.Forms.Label();
            this.player2Group = new System.Windows.Forms.Panel();
            this.groupSubPanel = new System.Windows.Forms.Panel();
            this.loadFlagGroupBox = new System.Windows.Forms.NumericUpDown();
            this.loadFlagGroupLabel = new System.Windows.Forms.Label();
            this.nEntryBox = new System.Windows.Forms.ComboBox();
            this.nEntryLabel = new System.Windows.Forms.Label();
            this.typeGroupLabel = new System.Windows.Forms.Label();
            this.typeGroupBox = new System.Windows.Forms.ComboBox();
            this.playerGroup = new System.Windows.Forms.Panel();
            this.sequenceMaxLabel = new System.Windows.Forms.Label();
            this.heapSizeBox = new System.Windows.Forms.NumericUpDown();
            this.heapSizeLabel = new System.Windows.Forms.Label();
            this.channelFlagBox = new System.Windows.Forms.NumericUpDown();
            this.channelFlagLabel = new System.Windows.Forms.Label();
            this.sequenceMaxBox = new System.Windows.Forms.NumericUpDown();
            this.bankGroup = new System.Windows.Forms.Panel();
            this.wave3Box = new System.Windows.Forms.ComboBox();
            this.wave2Box = new System.Windows.Forms.ComboBox();
            this.wave1Box = new System.Windows.Forms.ComboBox();
            this.wave0Box = new System.Windows.Forms.ComboBox();
            this.wave3Label = new System.Windows.Forms.Label();
            this.wave2Label = new System.Windows.Forms.Label();
            this.wave1Label = new System.Windows.Forms.Label();
            this.wave0Label = new System.Windows.Forms.Label();
            this.noSelectLabel = new System.Windows.Forms.Label();
            this.placeholderBox = new System.Windows.Forms.CheckBox();
            this.sseqGroup = new System.Windows.Forms.Panel();
            this.playerNumberSseqLabel = new System.Windows.Forms.Label();
            this.playerPrioritySseqLabel = new System.Windows.Forms.Label();
            this.channelPrioritySseqLabel = new System.Windows.Forms.Label();
            this.volumeSseqLabel = new System.Windows.Forms.Label();
            this.playerNumberSseqBox = new System.Windows.Forms.NumericUpDown();
            this.playerPrioritySseqBox = new System.Windows.Forms.NumericUpDown();
            this.channelPrioritySseqBox = new System.Windows.Forms.NumericUpDown();
            this.volumeSseqBox = new System.Windows.Forms.NumericUpDown();
            this.bankIDbox = new System.Windows.Forms.ComboBox();
            this.bankIdLabel = new System.Windows.Forms.Label();
            this.strmGroup = new System.Windows.Forms.Panel();
            this.tree = new System.Windows.Forms.TreeView();
            this.nodeImages = new System.Windows.Forms.ImageList(this.components);
            this.filesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.addBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.Export = new System.Windows.Forms.ToolStripMenuItem();
            this.Replace = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.foldersMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Add2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openTree2 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTree2 = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Add3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Add32 = new System.Windows.Forms.ToolStripMenuItem();
            this.addInside = new System.Windows.Forms.ToolStripMenuItem();
            this.openTree3 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTree3 = new System.Windows.Forms.ToolStripMenuItem();
            this.rename3 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMeh = new System.Windows.Forms.ToolStripMenuItem();
            this.entryMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Rename4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete4 = new System.Windows.Forms.ToolStripMenuItem();
            this.bigNodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.subNodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.bigFolderMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupSubPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadFlagGroupBox)).BeginInit();
            this.playerGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heapSizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelFlagBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sequenceMaxBox)).BeginInit();
            this.bankGroup.SuspendLayout();
            this.sseqGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerNumberSseqBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPrioritySseqBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelPrioritySseqBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeSseqBox)).BeginInit();
            this.filesMenu.SuspendLayout();
            this.foldersMenu.SuspendLayout();
            this.nodeMenu.SuspendLayout();
            this.entryMenu.SuspendLayout();
            this.bigNodeMenu.SuspendLayout();
            this.subNodeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bigFolderMenu
            // 
            this.bigFolderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.bigFolderMenu.Name = "filesMenu";
            this.bigFolderMenu.Size = new System.Drawing.Size(120, 48);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem2.Text = "Expand";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem3.Text = "Collapse";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBetaToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newBetaToolStripMenuItem
            // 
            this.newBetaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newBetaToolStripMenuItem.Image")));
            this.newBetaToolStripMenuItem.Name = "newBetaToolStripMenuItem";
            this.newBetaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newBetaToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.newBetaToolStripMenuItem.Text = "New";
            this.newBetaToolStripMenuItem.Click += new System.EventHandler(this.newBetaToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripMenuItem.Image")));
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.closeToolStripMenuItem.Text = "Close File";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quitToolStripMenuItem.Image")));
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportToolStripMenuItem.Image")));
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.exportToolStripMenuItem.Text = "Export To Folder";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importToolStripMenuItem.Image")));
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.importToolStripMenuItem.Text = "Import From Folder";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.H)));
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewHelpToolStripMenuItem.Image")));
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutNitroComposerToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutNitroComposerToolStripMenuItem
            // 
            this.aboutNitroComposerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutNitroComposerToolStripMenuItem.Image")));
            this.aboutNitroComposerToolStripMenuItem.Name = "aboutNitroComposerToolStripMenuItem";
            this.aboutNitroComposerToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.aboutNitroComposerToolStripMenuItem.Text = "About Nitro Studio";
            this.aboutNitroComposerToolStripMenuItem.Click += new System.EventHandler(this.aboutNitroComposerToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.status,
            this.byteSelect});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "status1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(41, 17);
            this.status.Text = "status!";
            this.status.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // byteSelect
            // 
            this.byteSelect.Name = "byteSelect";
            this.byteSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.byteSelect.Size = new System.Drawing.Size(103, 17);
            this.byteSelect.Text = "No bytes selected!";
            this.byteSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.placeholderBox);
            this.splitContainer1.Panel1.Controls.Add(this.fileIdBox);
            this.splitContainer1.Panel1.Controls.Add(this.fileIdLabel);
            this.splitContainer1.Panel1.Controls.Add(this.player2Group);
            this.splitContainer1.Panel1.Controls.Add(this.groupSubPanel);
            this.splitContainer1.Panel1.Controls.Add(this.playerGroup);
            this.splitContainer1.Panel1.Controls.Add(this.bankGroup);
            this.splitContainer1.Panel1.Controls.Add(this.noSelectLabel);
            this.splitContainer1.Panel1.Controls.Add(this.sseqGroup);
            this.splitContainer1.Panel1.Controls.Add(this.strmGroup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tree);
            this.splitContainer1.Size = new System.Drawing.Size(684, 365);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 2;
            // 
            // fileIdBox
            // 
            this.fileIdBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileIdBox.FormattingEnabled = true;
            this.fileIdBox.Location = new System.Drawing.Point(4, 26);
            this.fileIdBox.Name = "fileIdBox";
            this.fileIdBox.Size = new System.Drawing.Size(187, 21);
            this.fileIdBox.TabIndex = 1;
            this.fileIdBox.Visible = false;
            this.fileIdBox.SelectedIndexChanged += new System.EventHandler(this.onFileIdChange);
            // 
            // fileIdLabel
            // 
            this.fileIdLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.fileIdLabel.Location = new System.Drawing.Point(0, 0);
            this.fileIdLabel.Name = "fileIdLabel";
            this.fileIdLabel.Size = new System.Drawing.Size(194, 23);
            this.fileIdLabel.TabIndex = 2;
            this.fileIdLabel.Text = "File ID:";
            this.fileIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fileIdLabel.Visible = false;
            // 
            // player2Group
            // 
            this.player2Group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player2Group.Location = new System.Drawing.Point(0, 0);
            this.player2Group.Name = "player2Group";
            this.player2Group.Size = new System.Drawing.Size(194, 363);
            this.player2Group.TabIndex = 14;
            this.player2Group.Visible = false;
            // 
            // groupSubPanel
            // 
            this.groupSubPanel.Controls.Add(this.loadFlagGroupBox);
            this.groupSubPanel.Controls.Add(this.loadFlagGroupLabel);
            this.groupSubPanel.Controls.Add(this.nEntryBox);
            this.groupSubPanel.Controls.Add(this.nEntryLabel);
            this.groupSubPanel.Controls.Add(this.typeGroupLabel);
            this.groupSubPanel.Controls.Add(this.typeGroupBox);
            this.groupSubPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSubPanel.Location = new System.Drawing.Point(0, 0);
            this.groupSubPanel.Name = "groupSubPanel";
            this.groupSubPanel.Size = new System.Drawing.Size(194, 363);
            this.groupSubPanel.TabIndex = 12;
            this.groupSubPanel.Visible = false;
            // 
            // loadFlagGroupBox
            // 
            this.loadFlagGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadFlagGroupBox.Location = new System.Drawing.Point(5, 124);
            this.loadFlagGroupBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.loadFlagGroupBox.Name = "loadFlagGroupBox";
            this.loadFlagGroupBox.Size = new System.Drawing.Size(185, 20);
            this.loadFlagGroupBox.TabIndex = 5;
            this.loadFlagGroupBox.ValueChanged += new System.EventHandler(this.onLoadFlagChanged);
            // 
            // loadFlagGroupLabel
            // 
            this.loadFlagGroupLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadFlagGroupLabel.Location = new System.Drawing.Point(1, 100);
            this.loadFlagGroupLabel.Name = "loadFlagGroupLabel";
            this.loadFlagGroupLabel.Size = new System.Drawing.Size(192, 23);
            this.loadFlagGroupLabel.TabIndex = 4;
            this.loadFlagGroupLabel.Text = "Load Flag:";
            this.loadFlagGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nEntryBox
            // 
            this.nEntryBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nEntryBox.FormattingEnabled = true;
            this.nEntryBox.Items.AddRange(new object[] {
            "0 - SSEQ",
            "1 - SBNK",
            "2 - SWAR",
            "3 - SSAR",
            "WTF - Other"});
            this.nEntryBox.Location = new System.Drawing.Point(5, 76);
            this.nEntryBox.Name = "nEntryBox";
            this.nEntryBox.Size = new System.Drawing.Size(186, 21);
            this.nEntryBox.TabIndex = 3;
            this.nEntryBox.SelectedIndexChanged += new System.EventHandler(this.onNEntryChanged);
            // 
            // nEntryLabel
            // 
            this.nEntryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nEntryLabel.Location = new System.Drawing.Point(1, 50);
            this.nEntryLabel.Name = "nEntryLabel";
            this.nEntryLabel.Size = new System.Drawing.Size(192, 23);
            this.nEntryLabel.TabIndex = 2;
            this.nEntryLabel.Text = "Entry Number:";
            this.nEntryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // typeGroupLabel
            // 
            this.typeGroupLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeGroupLabel.Location = new System.Drawing.Point(1, 0);
            this.typeGroupLabel.Name = "typeGroupLabel";
            this.typeGroupLabel.Size = new System.Drawing.Size(192, 23);
            this.typeGroupLabel.TabIndex = 1;
            this.typeGroupLabel.Text = "Type:";
            this.typeGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // typeGroupBox
            // 
            this.typeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeGroupBox.FormattingEnabled = true;
            this.typeGroupBox.Items.AddRange(new object[] {
            "0 - SSEQ",
            "1 - SBNK",
            "2 - SWAR",
            "3 - SSAR",
            "WTF - Other"});
            this.typeGroupBox.Location = new System.Drawing.Point(4, 26);
            this.typeGroupBox.Name = "typeGroupBox";
            this.typeGroupBox.Size = new System.Drawing.Size(186, 21);
            this.typeGroupBox.TabIndex = 0;
            this.typeGroupBox.SelectedIndexChanged += new System.EventHandler(this.onTypeChanged);
            // 
            // playerGroup
            // 
            this.playerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerGroup.Controls.Add(this.sequenceMaxLabel);
            this.playerGroup.Controls.Add(this.heapSizeBox);
            this.playerGroup.Controls.Add(this.heapSizeLabel);
            this.playerGroup.Controls.Add(this.channelFlagBox);
            this.playerGroup.Controls.Add(this.channelFlagLabel);
            this.playerGroup.Controls.Add(this.sequenceMaxBox);
            this.playerGroup.Location = new System.Drawing.Point(0, 14);
            this.playerGroup.Name = "playerGroup";
            this.playerGroup.Size = new System.Drawing.Size(195, 334);
            this.playerGroup.TabIndex = 11;
            this.playerGroup.Visible = false;
            // 
            // sequenceMaxLabel
            // 
            this.sequenceMaxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sequenceMaxLabel.Location = new System.Drawing.Point(0, 1);
            this.sequenceMaxLabel.Name = "sequenceMaxLabel";
            this.sequenceMaxLabel.Size = new System.Drawing.Size(195, 23);
            this.sequenceMaxLabel.TabIndex = 0;
            this.sequenceMaxLabel.Text = "Sequence Max:";
            this.sequenceMaxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // heapSizeBox
            // 
            this.heapSizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heapSizeBox.Location = new System.Drawing.Point(5, 129);
            this.heapSizeBox.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.heapSizeBox.Name = "heapSizeBox";
            this.heapSizeBox.Size = new System.Drawing.Size(187, 20);
            this.heapSizeBox.TabIndex = 5;
            this.heapSizeBox.ValueChanged += new System.EventHandler(this.onheapSizeChanged);
            // 
            // heapSizeLabel
            // 
            this.heapSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heapSizeLabel.Location = new System.Drawing.Point(0, 104);
            this.heapSizeLabel.Name = "heapSizeLabel";
            this.heapSizeLabel.Size = new System.Drawing.Size(195, 23);
            this.heapSizeLabel.TabIndex = 4;
            this.heapSizeLabel.Text = "Heap Size (Bytes):";
            this.heapSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // channelFlagBox
            // 
            this.channelFlagBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelFlagBox.Location = new System.Drawing.Point(5, 77);
            this.channelFlagBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.channelFlagBox.Name = "channelFlagBox";
            this.channelFlagBox.Size = new System.Drawing.Size(187, 20);
            this.channelFlagBox.TabIndex = 3;
            this.channelFlagBox.ValueChanged += new System.EventHandler(this.onChannelFlagChanged);
            // 
            // channelFlagLabel
            // 
            this.channelFlagLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelFlagLabel.Location = new System.Drawing.Point(-2, 51);
            this.channelFlagLabel.Name = "channelFlagLabel";
            this.channelFlagLabel.Size = new System.Drawing.Size(195, 23);
            this.channelFlagLabel.TabIndex = 2;
            this.channelFlagLabel.Text = "Channel Flag:";
            this.channelFlagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sequenceMaxBox
            // 
            this.sequenceMaxBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sequenceMaxBox.Location = new System.Drawing.Point(5, 27);
            this.sequenceMaxBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.sequenceMaxBox.Name = "sequenceMaxBox";
            this.sequenceMaxBox.Size = new System.Drawing.Size(187, 20);
            this.sequenceMaxBox.TabIndex = 1;
            this.sequenceMaxBox.ValueChanged += new System.EventHandler(this.onSequenceMaxChanged);
            // 
            // bankGroup
            // 
            this.bankGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bankGroup.Controls.Add(this.wave3Box);
            this.bankGroup.Controls.Add(this.wave2Box);
            this.bankGroup.Controls.Add(this.wave1Box);
            this.bankGroup.Controls.Add(this.wave0Box);
            this.bankGroup.Controls.Add(this.wave3Label);
            this.bankGroup.Controls.Add(this.wave2Label);
            this.bankGroup.Controls.Add(this.wave1Label);
            this.bankGroup.Controls.Add(this.wave0Label);
            this.bankGroup.Location = new System.Drawing.Point(0, 53);
            this.bankGroup.Name = "bankGroup";
            this.bankGroup.Size = new System.Drawing.Size(194, 280);
            this.bankGroup.TabIndex = 4;
            this.bankGroup.Visible = false;
            // 
            // wave3Box
            // 
            this.wave3Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave3Box.FormattingEnabled = true;
            this.wave3Box.Location = new System.Drawing.Point(3, 184);
            this.wave3Box.Name = "wave3Box";
            this.wave3Box.Size = new System.Drawing.Size(188, 21);
            this.wave3Box.TabIndex = 7;
            this.wave3Box.SelectedIndexChanged += new System.EventHandler(this.wave3updated);
            // 
            // wave2Box
            // 
            this.wave2Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave2Box.FormattingEnabled = true;
            this.wave2Box.Location = new System.Drawing.Point(3, 128);
            this.wave2Box.Name = "wave2Box";
            this.wave2Box.Size = new System.Drawing.Size(188, 21);
            this.wave2Box.TabIndex = 6;
            this.wave2Box.SelectedIndexChanged += new System.EventHandler(this.wave2updated);
            // 
            // wave1Box
            // 
            this.wave1Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave1Box.FormattingEnabled = true;
            this.wave1Box.Location = new System.Drawing.Point(3, 75);
            this.wave1Box.Name = "wave1Box";
            this.wave1Box.Size = new System.Drawing.Size(188, 21);
            this.wave1Box.TabIndex = 5;
            this.wave1Box.SelectedIndexChanged += new System.EventHandler(this.wave1updated);
            // 
            // wave0Box
            // 
            this.wave0Box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave0Box.FormattingEnabled = true;
            this.wave0Box.Location = new System.Drawing.Point(3, 26);
            this.wave0Box.Name = "wave0Box";
            this.wave0Box.Size = new System.Drawing.Size(188, 21);
            this.wave0Box.TabIndex = 4;
            this.wave0Box.SelectedIndexChanged += new System.EventHandler(this.wave0updated);
            // 
            // wave3Label
            // 
            this.wave3Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave3Label.Location = new System.Drawing.Point(3, 152);
            this.wave3Label.Name = "wave3Label";
            this.wave3Label.Size = new System.Drawing.Size(188, 29);
            this.wave3Label.TabIndex = 3;
            this.wave3Label.Text = "Wave 3:";
            this.wave3Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wave2Label
            // 
            this.wave2Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave2Label.Location = new System.Drawing.Point(3, 99);
            this.wave2Label.Name = "wave2Label";
            this.wave2Label.Size = new System.Drawing.Size(188, 26);
            this.wave2Label.TabIndex = 2;
            this.wave2Label.Text = "Wave 2:";
            this.wave2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wave1Label
            // 
            this.wave1Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave1Label.Location = new System.Drawing.Point(3, 50);
            this.wave1Label.Name = "wave1Label";
            this.wave1Label.Size = new System.Drawing.Size(188, 22);
            this.wave1Label.TabIndex = 1;
            this.wave1Label.Text = "Wave 1:";
            this.wave1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wave0Label
            // 
            this.wave0Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wave0Label.Location = new System.Drawing.Point(3, 0);
            this.wave0Label.Name = "wave0Label";
            this.wave0Label.Size = new System.Drawing.Size(188, 23);
            this.wave0Label.TabIndex = 0;
            this.wave0Label.Text = "Wave 0:";
            this.wave0Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // noSelectLabel
            // 
            this.noSelectLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noSelectLabel.Location = new System.Drawing.Point(0, 0);
            this.noSelectLabel.Name = "noSelectLabel";
            this.noSelectLabel.Size = new System.Drawing.Size(194, 363);
            this.noSelectLabel.TabIndex = 0;
            this.noSelectLabel.Text = "No Valid Info Selected!";
            this.noSelectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // placeholderBox
            // 
            this.placeholderBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.placeholderBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.placeholderBox.Enabled = false;
            this.placeholderBox.Location = new System.Drawing.Point(0, 339);
            this.placeholderBox.Name = "placeholderBox";
            this.placeholderBox.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.placeholderBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.placeholderBox.Size = new System.Drawing.Size(194, 24);
            this.placeholderBox.TabIndex = 3;
            this.placeholderBox.Text = "Placeholder:";
            this.placeholderBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.placeholderBox.UseVisualStyleBackColor = true;
            this.placeholderBox.Visible = false;
            this.placeholderBox.CheckedChanged += new System.EventHandler(this.onCheckedChange);
            // 
            // sseqGroup
            // 
            this.sseqGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sseqGroup.Controls.Add(this.playerNumberSseqLabel);
            this.sseqGroup.Controls.Add(this.playerPrioritySseqLabel);
            this.sseqGroup.Controls.Add(this.channelPrioritySseqLabel);
            this.sseqGroup.Controls.Add(this.volumeSseqLabel);
            this.sseqGroup.Controls.Add(this.playerNumberSseqBox);
            this.sseqGroup.Controls.Add(this.playerPrioritySseqBox);
            this.sseqGroup.Controls.Add(this.channelPrioritySseqBox);
            this.sseqGroup.Controls.Add(this.volumeSseqBox);
            this.sseqGroup.Controls.Add(this.bankIDbox);
            this.sseqGroup.Controls.Add(this.bankIdLabel);
            this.sseqGroup.Location = new System.Drawing.Point(0, 53);
            this.sseqGroup.Name = "sseqGroup";
            this.sseqGroup.Size = new System.Drawing.Size(194, 280);
            this.sseqGroup.TabIndex = 8;
            this.sseqGroup.Visible = false;
            // 
            // playerNumberSseqLabel
            // 
            this.playerNumberSseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerNumberSseqLabel.Location = new System.Drawing.Point(4, 196);
            this.playerNumberSseqLabel.Name = "playerNumberSseqLabel";
            this.playerNumberSseqLabel.Size = new System.Drawing.Size(187, 23);
            this.playerNumberSseqLabel.TabIndex = 9;
            this.playerNumberSseqLabel.Text = "Player Number:";
            this.playerNumberSseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerPrioritySseqLabel
            // 
            this.playerPrioritySseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerPrioritySseqLabel.Location = new System.Drawing.Point(3, 147);
            this.playerPrioritySseqLabel.Name = "playerPrioritySseqLabel";
            this.playerPrioritySseqLabel.Size = new System.Drawing.Size(188, 23);
            this.playerPrioritySseqLabel.TabIndex = 8;
            this.playerPrioritySseqLabel.Text = "Player Priority:";
            this.playerPrioritySseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // channelPrioritySseqLabel
            // 
            this.channelPrioritySseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelPrioritySseqLabel.Location = new System.Drawing.Point(4, 98);
            this.channelPrioritySseqLabel.Name = "channelPrioritySseqLabel";
            this.channelPrioritySseqLabel.Size = new System.Drawing.Size(187, 23);
            this.channelPrioritySseqLabel.TabIndex = 7;
            this.channelPrioritySseqLabel.Text = "Channel Priority:";
            this.channelPrioritySseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // volumeSseqLabel
            // 
            this.volumeSseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeSseqLabel.Location = new System.Drawing.Point(3, 49);
            this.volumeSseqLabel.Name = "volumeSseqLabel";
            this.volumeSseqLabel.Size = new System.Drawing.Size(189, 23);
            this.volumeSseqLabel.TabIndex = 6;
            this.volumeSseqLabel.Text = "Volume:";
            this.volumeSseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerNumberSseqBox
            // 
            this.playerNumberSseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerNumberSseqBox.Location = new System.Drawing.Point(4, 222);
            this.playerNumberSseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.playerNumberSseqBox.Name = "playerNumberSseqBox";
            this.playerNumberSseqBox.Size = new System.Drawing.Size(187, 20);
            this.playerNumberSseqBox.TabIndex = 5;
            this.playerNumberSseqBox.ValueChanged += new System.EventHandler(this.onPlayerNumberChanged);
            // 
            // playerPrioritySseqBox
            // 
            this.playerPrioritySseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerPrioritySseqBox.Location = new System.Drawing.Point(4, 173);
            this.playerPrioritySseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.playerPrioritySseqBox.Name = "playerPrioritySseqBox";
            this.playerPrioritySseqBox.Size = new System.Drawing.Size(188, 20);
            this.playerPrioritySseqBox.TabIndex = 4;
            this.playerPrioritySseqBox.ValueChanged += new System.EventHandler(this.onPlayerPriorityChanged);
            // 
            // channelPrioritySseqBox
            // 
            this.channelPrioritySseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelPrioritySseqBox.Location = new System.Drawing.Point(3, 124);
            this.channelPrioritySseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.channelPrioritySseqBox.Name = "channelPrioritySseqBox";
            this.channelPrioritySseqBox.Size = new System.Drawing.Size(189, 20);
            this.channelPrioritySseqBox.TabIndex = 3;
            this.channelPrioritySseqBox.ValueChanged += new System.EventHandler(this.onChannelPriorityChanged);
            // 
            // volumeSseqBox
            // 
            this.volumeSseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeSseqBox.Location = new System.Drawing.Point(4, 75);
            this.volumeSseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.volumeSseqBox.Name = "volumeSseqBox";
            this.volumeSseqBox.Size = new System.Drawing.Size(188, 20);
            this.volumeSseqBox.TabIndex = 2;
            this.volumeSseqBox.ValueChanged += new System.EventHandler(this.onVolumeChanged);
            // 
            // bankIDbox
            // 
            this.bankIDbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bankIDbox.FormattingEnabled = true;
            this.bankIDbox.Location = new System.Drawing.Point(4, 26);
            this.bankIDbox.Name = "bankIDbox";
            this.bankIDbox.Size = new System.Drawing.Size(188, 21);
            this.bankIDbox.TabIndex = 1;
            this.bankIDbox.SelectedIndexChanged += new System.EventHandler(this.onBankIdChanged);
            // 
            // bankIdLabel
            // 
            this.bankIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bankIdLabel.Location = new System.Drawing.Point(48, 0);
            this.bankIdLabel.Name = "bankIdLabel";
            this.bankIdLabel.Size = new System.Drawing.Size(100, 23);
            this.bankIdLabel.TabIndex = 0;
            this.bankIdLabel.Text = "Bank ID:";
            this.bankIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // strmGroup
            // 
            this.strmGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strmGroup.Location = new System.Drawing.Point(0, 0);
            this.strmGroup.Name = "strmGroup";
            this.strmGroup.Size = new System.Drawing.Size(194, 363);
            this.strmGroup.TabIndex = 15;
            this.strmGroup.Visible = false;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.ImageIndex = 0;
            this.tree.ImageList = this.nodeImages;
            this.tree.Indent = 15;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            treeNode15.Name = "Sseq";
            treeNode15.Tag = "sseq";
            treeNode15.Text = "Sound Sequence";
            treeNode16.ImageIndex = 1;
            treeNode16.Name = "SeqArc";
            treeNode16.SelectedImageIndex = 1;
            treeNode16.Tag = "seqarc";
            treeNode16.Text = "Sequence Archive";
            treeNode17.ImageIndex = 2;
            treeNode17.Name = "Bank";
            treeNode17.SelectedImageIndex = 2;
            treeNode17.Tag = "bank";
            treeNode17.Text = "Instrument Bank";
            treeNode18.ImageIndex = 3;
            treeNode18.Name = "Wave";
            treeNode18.SelectedImageIndex = 3;
            treeNode18.Tag = "wave";
            treeNode18.Text = "Wave";
            treeNode19.ImageIndex = 4;
            treeNode19.Name = "Player";
            treeNode19.SelectedImageIndex = 4;
            treeNode19.Tag = "player";
            treeNode19.Text = "Player";
            treeNode20.ImageIndex = 5;
            treeNode20.Name = "Group";
            treeNode20.SelectedImageIndex = 5;
            treeNode20.Tag = "group";
            treeNode20.Text = "Group";
            treeNode21.ImageIndex = 6;
            treeNode21.Name = "Strm Player";
            treeNode21.SelectedImageIndex = 6;
            treeNode21.Tag = "strmplayer";
            treeNode21.Text = "Stream Player";
            treeNode22.ImageIndex = 7;
            treeNode22.Name = "strm";
            treeNode22.SelectedImageIndex = 7;
            treeNode22.Tag = "strm";
            treeNode22.Text = "Stream";
            treeNode23.ImageIndex = 8;
            treeNode23.Name = "sequenceFiles";
            treeNode23.SelectedImageIndex = 8;
            treeNode23.Text = "Sequence";
            treeNode24.ImageIndex = 8;
            treeNode24.Name = "sequenceArchive";
            treeNode24.SelectedImageIndex = 8;
            treeNode24.Text = "Sequence Archive";
            treeNode25.ImageIndex = 8;
            treeNode25.Name = "bankFiles";
            treeNode25.SelectedImageIndex = 8;
            treeNode25.Text = "Bank";
            treeNode26.ImageIndex = 8;
            treeNode26.Name = "waveFiles";
            treeNode26.SelectedImageIndex = 8;
            treeNode26.Text = "Wave Archive";
            treeNode27.ImageIndex = 8;
            treeNode27.Name = "strmFiles";
            treeNode27.SelectedImageIndex = 8;
            treeNode27.Text = "Stream";
            treeNode28.ContextMenuStrip = this.bigFolderMenu;
            treeNode28.ImageIndex = 8;
            treeNode28.Name = "FILES";
            treeNode28.SelectedImageIndex = 8;
            treeNode28.Tag = "files";
            treeNode28.Text = "FILES";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode28});
            this.tree.SelectedImageIndex = 0;
            this.tree.ShowLines = false;
            this.tree.Size = new System.Drawing.Size(484, 363);
            this.tree.TabIndex = 0;
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
            this.tree.DoubleClick += new System.EventHandler(this.doubleClickNode);
            this.tree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeArrowKey);
            // 
            // nodeImages
            // 
            this.nodeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("nodeImages.ImageStream")));
            this.nodeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.nodeImages.Images.SetKeyName(0, "sseq.png");
            this.nodeImages.Images.SetKeyName(1, "seqArc.png");
            this.nodeImages.Images.SetKeyName(2, "bank.png");
            this.nodeImages.Images.SetKeyName(3, "wave.png");
            this.nodeImages.Images.SetKeyName(4, "player.png");
            this.nodeImages.Images.SetKeyName(5, "group.png");
            this.nodeImages.Images.SetKeyName(6, "player2.png");
            this.nodeImages.Images.SetKeyName(7, "strm.png");
            this.nodeImages.Images.SetKeyName(8, "FILES.png");
            this.nodeImages.Images.SetKeyName(9, "blank.png");
            // 
            // filesMenu
            // 
            this.filesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAbove,
            this.addBelow,
            this.Export,
            this.Replace,
            this.Delete});
            this.filesMenu.Name = "filesMenu";
            this.filesMenu.Size = new System.Drawing.Size(159, 114);
            // 
            // addAbove
            // 
            this.addAbove.Image = ((System.Drawing.Image)(resources.GetObject("addAbove.Image")));
            this.addAbove.Name = "addAbove";
            this.addAbove.Size = new System.Drawing.Size(158, 22);
            this.addAbove.Text = "Add Above This";
            this.addAbove.Click += new System.EventHandler(this.addAbove_Click);
            // 
            // addBelow
            // 
            this.addBelow.Image = ((System.Drawing.Image)(resources.GetObject("addBelow.Image")));
            this.addBelow.Name = "addBelow";
            this.addBelow.Size = new System.Drawing.Size(158, 22);
            this.addBelow.Text = "Add Below This";
            this.addBelow.Click += new System.EventHandler(this.addBelow_Click);
            // 
            // Export
            // 
            this.Export.Image = ((System.Drawing.Image)(resources.GetObject("Export.Image")));
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(158, 22);
            this.Export.Text = "Export";
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Replace
            // 
            this.Replace.Image = ((System.Drawing.Image)(resources.GetObject("Replace.Image")));
            this.Replace.Name = "Replace";
            this.Replace.Size = new System.Drawing.Size(158, 22);
            this.Replace.Text = "Replace";
            this.Replace.Click += new System.EventHandler(this.Replace_Click);
            // 
            // Delete
            // 
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(158, 22);
            this.Delete.Text = "Delete";
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // foldersMenu
            // 
            this.foldersMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add2,
            this.openTree2,
            this.closeTree2});
            this.foldersMenu.Name = "filesMenu";
            this.foldersMenu.Size = new System.Drawing.Size(120, 70);
            // 
            // Add2
            // 
            this.Add2.Image = ((System.Drawing.Image)(resources.GetObject("Add2.Image")));
            this.Add2.Name = "Add2";
            this.Add2.Size = new System.Drawing.Size(119, 22);
            this.Add2.Text = "Add";
            this.Add2.Click += new System.EventHandler(this.Add2_Click);
            // 
            // openTree2
            // 
            this.openTree2.Image = ((System.Drawing.Image)(resources.GetObject("openTree2.Image")));
            this.openTree2.Name = "openTree2";
            this.openTree2.Size = new System.Drawing.Size(119, 22);
            this.openTree2.Text = "Expand";
            this.openTree2.Click += new System.EventHandler(this.openTree2_Click);
            // 
            // closeTree2
            // 
            this.closeTree2.Image = ((System.Drawing.Image)(resources.GetObject("closeTree2.Image")));
            this.closeTree2.Name = "closeTree2";
            this.closeTree2.Size = new System.Drawing.Size(119, 22);
            this.closeTree2.Text = "Collapse";
            this.closeTree2.Click += new System.EventHandler(this.closeTree2_Click);
            // 
            // nodeMenu
            // 
            this.nodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add3,
            this.Add32,
            this.addInside,
            this.openTree3,
            this.closeTree3,
            this.rename3,
            this.deleteMeh});
            this.nodeMenu.Name = "nodeMenu";
            this.nodeMenu.Size = new System.Drawing.Size(134, 158);
            this.nodeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.nodeMenu_Opening);
            // 
            // Add3
            // 
            this.Add3.Image = ((System.Drawing.Image)(resources.GetObject("Add3.Image")));
            this.Add3.Name = "Add3";
            this.Add3.Size = new System.Drawing.Size(133, 22);
            this.Add3.Text = "Add Above";
            this.Add3.Click += new System.EventHandler(this.Add3_Click);
            // 
            // Add32
            // 
            this.Add32.Image = ((System.Drawing.Image)(resources.GetObject("Add32.Image")));
            this.Add32.Name = "Add32";
            this.Add32.Size = new System.Drawing.Size(133, 22);
            this.Add32.Text = "Add Below";
            this.Add32.Click += new System.EventHandler(this.Add32_Click);
            // 
            // addInside
            // 
            this.addInside.Image = ((System.Drawing.Image)(resources.GetObject("addInside.Image")));
            this.addInside.Name = "addInside";
            this.addInside.Size = new System.Drawing.Size(133, 22);
            this.addInside.Text = "Add Inside";
            this.addInside.Click += new System.EventHandler(this.addInside_Click);
            // 
            // openTree3
            // 
            this.openTree3.Image = ((System.Drawing.Image)(resources.GetObject("openTree3.Image")));
            this.openTree3.Name = "openTree3";
            this.openTree3.Size = new System.Drawing.Size(133, 22);
            this.openTree3.Text = "Expand";
            this.openTree3.Click += new System.EventHandler(this.openTree3_Click);
            // 
            // closeTree3
            // 
            this.closeTree3.Image = ((System.Drawing.Image)(resources.GetObject("closeTree3.Image")));
            this.closeTree3.Name = "closeTree3";
            this.closeTree3.Size = new System.Drawing.Size(133, 22);
            this.closeTree3.Text = "Collapse";
            this.closeTree3.Click += new System.EventHandler(this.closeTree3_Click);
            // 
            // rename3
            // 
            this.rename3.Image = ((System.Drawing.Image)(resources.GetObject("rename3.Image")));
            this.rename3.Name = "rename3";
            this.rename3.Size = new System.Drawing.Size(133, 22);
            this.rename3.Text = "Rename";
            this.rename3.Click += new System.EventHandler(this.rename3_Click);
            // 
            // deleteMeh
            // 
            this.deleteMeh.Image = ((System.Drawing.Image)(resources.GetObject("deleteMeh.Image")));
            this.deleteMeh.Name = "deleteMeh";
            this.deleteMeh.Size = new System.Drawing.Size(133, 22);
            this.deleteMeh.Text = "Delete";
            this.deleteMeh.Click += new System.EventHandler(this.deleteMeh_Click);
            // 
            // entryMenu
            // 
            this.entryMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Rename4,
            this.Delete4});
            this.entryMenu.Name = "entryMenu";
            this.entryMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // Rename4
            // 
            this.Rename4.Image = ((System.Drawing.Image)(resources.GetObject("Rename4.Image")));
            this.Rename4.Name = "Rename4";
            this.Rename4.Size = new System.Drawing.Size(117, 22);
            this.Rename4.Text = "Rename";
            this.Rename4.Click += new System.EventHandler(this.Rename4_Click);
            // 
            // Delete4
            // 
            this.Delete4.Image = ((System.Drawing.Image)(resources.GetObject("Delete4.Image")));
            this.Delete4.Name = "Delete4";
            this.Delete4.Size = new System.Drawing.Size(117, 22);
            this.Delete4.Text = "Delete";
            this.Delete4.Click += new System.EventHandler(this.Delete4_Click);
            // 
            // bigNodeMenu
            // 
            this.bigNodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.bigNodeMenu.Name = "nodeMenu";
            this.bigNodeMenu.Size = new System.Drawing.Size(120, 70);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem5.Image")));
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem5.Text = "Add";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem6.Text = "Expand";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem7.Image")));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem7.Text = "Collapse";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // subNodeMenu
            // 
            this.subNodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12});
            this.subNodeMenu.Name = "nodeMenu";
            this.subNodeMenu.Size = new System.Drawing.Size(134, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem1.Text = "Add Above";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem4.Text = "Add Below";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem11.Image")));
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem11.Text = "Rename";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem11_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem12.Image")));
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem12.Text = "Delete";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Nitro Studio";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.bigFolderMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupSubPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadFlagGroupBox)).EndInit();
            this.playerGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.heapSizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelFlagBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sequenceMaxBox)).EndInit();
            this.bankGroup.ResumeLayout(false);
            this.sseqGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerNumberSseqBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPrioritySseqBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelPrioritySseqBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeSseqBox)).EndInit();
            this.filesMenu.ResumeLayout(false);
            this.foldersMenu.ResumeLayout(false);
            this.nodeMenu.ResumeLayout(false);
            this.entryMenu.ResumeLayout(false);
            this.bigNodeMenu.ResumeLayout(false);
            this.subNodeMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newBetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutNitroComposerToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.ImageList nodeImages;
        private System.Windows.Forms.ContextMenuStrip filesMenu;
        private System.Windows.Forms.ToolStripMenuItem Replace;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private System.Windows.Forms.ContextMenuStrip foldersMenu;
        private System.Windows.Forms.ToolStripMenuItem Add2;
        private System.Windows.Forms.ToolStripMenuItem openTree2;
        private System.Windows.Forms.ToolStripMenuItem closeTree2;
        private System.Windows.Forms.ContextMenuStrip nodeMenu;
        private System.Windows.Forms.ToolStripMenuItem Add3;
        private System.Windows.Forms.ToolStripMenuItem openTree3;
        private System.Windows.Forms.ToolStripMenuItem closeTree3;
        private System.Windows.Forms.ContextMenuStrip entryMenu;
        private System.Windows.Forms.ToolStripMenuItem Rename4;
        private System.Windows.Forms.ToolStripMenuItem Delete4;
        private System.Windows.Forms.ToolStripMenuItem Export;
        private System.Windows.Forms.Label noSelectLabel;
        private System.Windows.Forms.ComboBox fileIdBox;
        private System.Windows.Forms.Label fileIdLabel;
        private System.Windows.Forms.CheckBox placeholderBox;
        private System.Windows.Forms.Panel bankGroup;
        private System.Windows.Forms.ComboBox wave0Box;
        private System.Windows.Forms.Label wave3Label;
        private System.Windows.Forms.Label wave2Label;
        private System.Windows.Forms.Label wave1Label;
        private System.Windows.Forms.Label wave0Label;
        private System.Windows.Forms.ComboBox wave3Box;
        private System.Windows.Forms.ComboBox wave2Box;
        private System.Windows.Forms.ComboBox wave1Box;
        private System.Windows.Forms.Panel sseqGroup;
        private System.Windows.Forms.Label bankIdLabel;
        private System.Windows.Forms.ComboBox bankIDbox;
        private System.Windows.Forms.NumericUpDown volumeSseqBox;
        private System.Windows.Forms.NumericUpDown playerNumberSseqBox;
        private System.Windows.Forms.NumericUpDown playerPrioritySseqBox;
        private System.Windows.Forms.NumericUpDown channelPrioritySseqBox;
        private System.Windows.Forms.Label playerNumberSseqLabel;
        private System.Windows.Forms.Label playerPrioritySseqLabel;
        private System.Windows.Forms.Label channelPrioritySseqLabel;
        private System.Windows.Forms.Label volumeSseqLabel;
        private System.Windows.Forms.Panel groupSubPanel;
        private System.Windows.Forms.Panel playerGroup;
        private System.Windows.Forms.NumericUpDown heapSizeBox;
        private System.Windows.Forms.Label heapSizeLabel;
        private System.Windows.Forms.NumericUpDown channelFlagBox;
        private System.Windows.Forms.Label channelFlagLabel;
        private System.Windows.Forms.NumericUpDown sequenceMaxBox;
        private System.Windows.Forms.Label sequenceMaxLabel;
        private System.Windows.Forms.Label typeGroupLabel;
        private System.Windows.Forms.ComboBox typeGroupBox;
        private System.Windows.Forms.Label nEntryLabel;
        private System.Windows.Forms.ComboBox nEntryBox;
        private System.Windows.Forms.NumericUpDown loadFlagGroupBox;
        private System.Windows.Forms.Label loadFlagGroupLabel;
        private System.Windows.Forms.ToolStripStatusLabel byteSelect;
        private System.Windows.Forms.ToolStripMenuItem addAbove;
        private System.Windows.Forms.ToolStripMenuItem addBelow;
        private System.Windows.Forms.ContextMenuStrip bigFolderMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem Add32;
        private System.Windows.Forms.ToolStripMenuItem addInside;
        private System.Windows.Forms.ToolStripMenuItem rename3;
        private System.Windows.Forms.ToolStripMenuItem deleteMeh;
        private System.Windows.Forms.ContextMenuStrip bigNodeMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ContextMenuStrip subNodeMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.Panel player2Group;
        private System.Windows.Forms.Panel strmGroup;
    }
}

