namespace NitroStudio
{
    partial class SsarEditor
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("SSAR", 1, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SsarEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.channelPrioritySseqBox = new System.Windows.Forms.NumericUpDown();
            this.volumeSseqBox = new System.Windows.Forms.NumericUpDown();
            this.playerPrioritySseqBox = new System.Windows.Forms.NumericUpDown();
            this.bankIDbox = new System.Windows.Forms.ComboBox();
            this.playerNumberSseqBox = new System.Windows.Forms.NumericUpDown();
            this.bankIdLabel = new System.Windows.Forms.Label();
            this.volumeSseqLabel = new System.Windows.Forms.Label();
            this.channelPrioritySseqLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.gericomLabel = new System.Windows.Forms.Label();
            this.gericomPlay = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gericomPause = new System.Windows.Forms.Button();
            this.gericomStop = new System.Windows.Forms.Button();
            this.playerPrioritySseqLabel = new System.Windows.Forms.Label();
            this.playerNumberSseqLabel = new System.Windows.Forms.Label();
            this.noInfoPanel = new System.Windows.Forms.Panel();
            this.noInfoLabel = new System.Windows.Forms.Label();
            this.tree = new System.Windows.Forms.TreeView();
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixSDATSYMBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSEATYOUROWNRISKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.channelPrioritySseqBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeSseqBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPrioritySseqBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerNumberSseqBox)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.noInfoPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.infoPanel);
            this.splitContainer1.Panel1.Controls.Add(this.noInfoPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tree);
            this.splitContainer1.Size = new System.Drawing.Size(610, 372);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 0;
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.channelPrioritySseqBox);
            this.infoPanel.Controls.Add(this.volumeSseqBox);
            this.infoPanel.Controls.Add(this.playerPrioritySseqBox);
            this.infoPanel.Controls.Add(this.bankIDbox);
            this.infoPanel.Controls.Add(this.playerNumberSseqBox);
            this.infoPanel.Controls.Add(this.bankIdLabel);
            this.infoPanel.Controls.Add(this.volumeSseqLabel);
            this.infoPanel.Controls.Add(this.channelPrioritySseqLabel);
            this.infoPanel.Controls.Add(this.tableLayoutPanel6);
            this.infoPanel.Controls.Add(this.tableLayoutPanel5);
            this.infoPanel.Controls.Add(this.playerPrioritySseqLabel);
            this.infoPanel.Controls.Add(this.playerNumberSseqLabel);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(201, 370);
            this.infoPanel.TabIndex = 25;
            this.infoPanel.Visible = false;
            // 
            // channelPrioritySseqBox
            // 
            this.channelPrioritySseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelPrioritySseqBox.Location = new System.Drawing.Point(3, 116);
            this.channelPrioritySseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.channelPrioritySseqBox.Name = "channelPrioritySseqBox";
            this.channelPrioritySseqBox.Size = new System.Drawing.Size(195, 20);
            this.channelPrioritySseqBox.TabIndex = 35;
            // 
            // volumeSseqBox
            // 
            this.volumeSseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeSseqBox.Location = new System.Drawing.Point(3, 70);
            this.volumeSseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.volumeSseqBox.Name = "volumeSseqBox";
            this.volumeSseqBox.Size = new System.Drawing.Size(195, 20);
            this.volumeSseqBox.TabIndex = 34;
            // 
            // playerPrioritySseqBox
            // 
            this.playerPrioritySseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerPrioritySseqBox.Location = new System.Drawing.Point(3, 162);
            this.playerPrioritySseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.playerPrioritySseqBox.Name = "playerPrioritySseqBox";
            this.playerPrioritySseqBox.Size = new System.Drawing.Size(195, 20);
            this.playerPrioritySseqBox.TabIndex = 33;
            // 
            // bankIDbox
            // 
            this.bankIDbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bankIDbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bankIDbox.FormattingEnabled = true;
            this.bankIDbox.Location = new System.Drawing.Point(3, 23);
            this.bankIDbox.Name = "bankIDbox";
            this.bankIDbox.Size = new System.Drawing.Size(195, 21);
            this.bankIDbox.TabIndex = 31;
            // 
            // playerNumberSseqBox
            // 
            this.playerNumberSseqBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerNumberSseqBox.Location = new System.Drawing.Point(3, 208);
            this.playerNumberSseqBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.playerNumberSseqBox.Name = "playerNumberSseqBox";
            this.playerNumberSseqBox.Size = new System.Drawing.Size(195, 20);
            this.playerNumberSseqBox.TabIndex = 30;
            // 
            // bankIdLabel
            // 
            this.bankIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bankIdLabel.Location = new System.Drawing.Point(3, 0);
            this.bankIdLabel.Name = "bankIdLabel";
            this.bankIdLabel.Size = new System.Drawing.Size(195, 20);
            this.bankIdLabel.TabIndex = 29;
            this.bankIdLabel.Text = "Bank ID:";
            this.bankIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // volumeSseqLabel
            // 
            this.volumeSseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeSseqLabel.Location = new System.Drawing.Point(3, 47);
            this.volumeSseqLabel.Name = "volumeSseqLabel";
            this.volumeSseqLabel.Size = new System.Drawing.Size(195, 20);
            this.volumeSseqLabel.TabIndex = 28;
            this.volumeSseqLabel.Text = "Volume:";
            this.volumeSseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // channelPrioritySseqLabel
            // 
            this.channelPrioritySseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelPrioritySseqLabel.Location = new System.Drawing.Point(3, 93);
            this.channelPrioritySseqLabel.Name = "channelPrioritySseqLabel";
            this.channelPrioritySseqLabel.Size = new System.Drawing.Size(195, 20);
            this.channelPrioritySseqLabel.TabIndex = 27;
            this.channelPrioritySseqLabel.Text = "Channel Priority:";
            this.channelPrioritySseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.gericomLabel, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.gericomPlay, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel6.Enabled = false;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 284);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(201, 57);
            this.tableLayoutPanel6.TabIndex = 25;
            // 
            // gericomLabel
            // 
            this.gericomLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gericomLabel.Location = new System.Drawing.Point(3, 0);
            this.gericomLabel.Name = "gericomLabel";
            this.gericomLabel.Size = new System.Drawing.Size(195, 28);
            this.gericomLabel.TabIndex = 12;
            this.gericomLabel.Text = "Gericom Player:";
            this.gericomLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // gericomPlay
            // 
            this.gericomPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gericomPlay.Location = new System.Drawing.Point(3, 31);
            this.gericomPlay.Name = "gericomPlay";
            this.gericomPlay.Size = new System.Drawing.Size(195, 23);
            this.gericomPlay.TabIndex = 11;
            this.gericomPlay.Text = "Play";
            this.gericomPlay.UseVisualStyleBackColor = true;
            this.gericomPlay.Click += new System.EventHandler(this.gericomPlay_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.gericomPause, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.gericomStop, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel5.Enabled = false;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 341);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(201, 29);
            this.tableLayoutPanel5.TabIndex = 26;
            // 
            // gericomPause
            // 
            this.gericomPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gericomPause.Location = new System.Drawing.Point(3, 3);
            this.gericomPause.Name = "gericomPause";
            this.gericomPause.Size = new System.Drawing.Size(94, 23);
            this.gericomPause.TabIndex = 13;
            this.gericomPause.Text = "Pause";
            this.gericomPause.UseVisualStyleBackColor = true;
            // 
            // gericomStop
            // 
            this.gericomStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gericomStop.Location = new System.Drawing.Point(103, 3);
            this.gericomStop.Name = "gericomStop";
            this.gericomStop.Size = new System.Drawing.Size(95, 23);
            this.gericomStop.TabIndex = 10;
            this.gericomStop.Text = "Stop";
            this.gericomStop.UseVisualStyleBackColor = true;
            // 
            // playerPrioritySseqLabel
            // 
            this.playerPrioritySseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerPrioritySseqLabel.Location = new System.Drawing.Point(3, 139);
            this.playerPrioritySseqLabel.Name = "playerPrioritySseqLabel";
            this.playerPrioritySseqLabel.Size = new System.Drawing.Size(195, 20);
            this.playerPrioritySseqLabel.TabIndex = 24;
            this.playerPrioritySseqLabel.Text = "Player Priority:";
            this.playerPrioritySseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerNumberSseqLabel
            // 
            this.playerNumberSseqLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerNumberSseqLabel.Location = new System.Drawing.Point(3, 185);
            this.playerNumberSseqLabel.Name = "playerNumberSseqLabel";
            this.playerNumberSseqLabel.Size = new System.Drawing.Size(195, 20);
            this.playerNumberSseqLabel.TabIndex = 23;
            this.playerNumberSseqLabel.Text = "Player Number:";
            this.playerNumberSseqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // noInfoPanel
            // 
            this.noInfoPanel.Controls.Add(this.noInfoLabel);
            this.noInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noInfoPanel.Location = new System.Drawing.Point(0, 0);
            this.noInfoPanel.Name = "noInfoPanel";
            this.noInfoPanel.Size = new System.Drawing.Size(201, 370);
            this.noInfoPanel.TabIndex = 33;
            // 
            // noInfoLabel
            // 
            this.noInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.noInfoLabel.Name = "noInfoLabel";
            this.noInfoLabel.Size = new System.Drawing.Size(201, 370);
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
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "SSAR";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "SSAR";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tree.SelectedImageIndex = 0;
            this.tree.Size = new System.Drawing.Size(401, 370);
            this.tree.TabIndex = 0;
            this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
            this.tree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tree_NodeKey);
            // 
            // icons
            // 
            this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
            this.icons.TransparentColor = System.Drawing.Color.Transparent;
            this.icons.Images.SetKeyName(0, "sseq.ico");
            this.icons.Images.SetKeyName(1, "seqArc.ico");
            this.icons.Images.SetKeyName(2, "FILES.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.uSEATYOUROWNRISKToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(610, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.importFromFileToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
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
            // importFromFileToolStripMenuItem
            // 
            this.importFromFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importFromFileToolStripMenuItem.Image")));
            this.importFromFileToolStripMenuItem.Name = "importFromFileToolStripMenuItem";
            this.importFromFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.importFromFileToolStripMenuItem.Text = "Import From File";
            this.importFromFileToolStripMenuItem.Click += new System.EventHandler(this.importFromFileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Enabled = false;
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixSDATSYMBToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // fixSDATSYMBToolStripMenuItem
            // 
            this.fixSDATSYMBToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fixSDATSYMBToolStripMenuItem.Image")));
            this.fixSDATSYMBToolStripMenuItem.Name = "fixSDATSYMBToolStripMenuItem";
            this.fixSDATSYMBToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.fixSDATSYMBToolStripMenuItem.Text = "Fix SDAT SYMB";
            // 
            // uSEATYOUROWNRISKToolStripMenuItem
            // 
            this.uSEATYOUROWNRISKToolStripMenuItem.Name = "uSEATYOUROWNRISKToolStripMenuItem";
            this.uSEATYOUROWNRISKToolStripMenuItem.Size = new System.Drawing.Size(151, 20);
            this.uSEATYOUROWNRISKToolStripMenuItem.Text = "USE AT YOUR OWN RISK!";
            // 
            // SsarEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 396);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SsarEditor";
            this.Text = "Sequence Archive Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.channelPrioritySseqBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeSseqBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPrioritySseqBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerNumberSseqBox)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.noInfoPanel.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.ImageList icons;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label gericomLabel;
        private System.Windows.Forms.Button gericomPlay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button gericomPause;
        private System.Windows.Forms.Button gericomStop;
        private System.Windows.Forms.Label playerPrioritySseqLabel;
        private System.Windows.Forms.Label playerNumberSseqLabel;
        private System.Windows.Forms.NumericUpDown channelPrioritySseqBox;
        private System.Windows.Forms.NumericUpDown volumeSseqBox;
        private System.Windows.Forms.NumericUpDown playerPrioritySseqBox;
        private System.Windows.Forms.ComboBox bankIDbox;
        private System.Windows.Forms.NumericUpDown playerNumberSseqBox;
        private System.Windows.Forms.Label bankIdLabel;
        private System.Windows.Forms.Label volumeSseqLabel;
        private System.Windows.Forms.Label channelPrioritySseqLabel;
        private System.Windows.Forms.Panel noInfoPanel;
        private System.Windows.Forms.Label noInfoLabel;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixSDATSYMBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSEATYOUROWNRISKToolStripMenuItem;
    }
}