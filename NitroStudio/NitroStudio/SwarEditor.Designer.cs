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
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.samplingBox = new System.Windows.Forms.NumericUpDown();
            this.loopBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.samplingLabel = new System.Windows.Forms.Label();
            this.loopsLabel = new System.Windows.Forms.Label();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.tree = new System.Windows.Forms.TreeView();
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newBetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Export = new System.Windows.Forms.ToolStripMenuItem();
            this.Import = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.addAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.addBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.swavGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplingBox)).BeginInit();
            this.menu.SuspendLayout();
            this.soundMenu.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.swavGroup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tree);
            this.splitContainer1.Size = new System.Drawing.Size(583, 325);
            this.splitContainer1.SplitterDistance = 194;
            this.splitContainer1.TabIndex = 0;
            // 
            // swavGroup
            // 
            this.swavGroup.Controls.Add(this.numericUpDown4);
            this.swavGroup.Controls.Add(this.numericUpDown3);
            this.swavGroup.Controls.Add(this.numericUpDown2);
            this.swavGroup.Controls.Add(this.samplingBox);
            this.swavGroup.Controls.Add(this.loopBox);
            this.swavGroup.Controls.Add(this.label6);
            this.swavGroup.Controls.Add(this.label5);
            this.swavGroup.Controls.Add(this.label4);
            this.swavGroup.Controls.Add(this.samplingLabel);
            this.swavGroup.Controls.Add(this.loopsLabel);
            this.swavGroup.Controls.Add(this.typeBox);
            this.swavGroup.Controls.Add(this.typeLabel);
            this.swavGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.swavGroup.Location = new System.Drawing.Point(0, 0);
            this.swavGroup.Name = "swavGroup";
            this.swavGroup.Size = new System.Drawing.Size(192, 323);
            this.swavGroup.TabIndex = 0;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(44, 275);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown4.TabIndex = 11;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(30, 221);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown3.TabIndex = 10;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(44, 173);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 9;
            // 
            // samplingBox
            // 
            this.samplingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.samplingBox.Location = new System.Drawing.Point(3, 102);
            this.samplingBox.Name = "samplingBox";
            this.samplingBox.Size = new System.Drawing.Size(186, 20);
            this.samplingBox.TabIndex = 8;
            // 
            // loopBox
            // 
            this.loopBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loopBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loopBox.Location = new System.Drawing.Point(3, 66);
            this.loopBox.Name = "loopBox";
            this.loopBox.Size = new System.Drawing.Size(189, 17);
            this.loopBox.TabIndex = 7;
            this.loopBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loopBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // samplingLabel
            // 
            this.samplingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.samplingLabel.Location = new System.Drawing.Point(6, 86);
            this.samplingLabel.Name = "samplingLabel";
            this.samplingLabel.Size = new System.Drawing.Size(183, 13);
            this.samplingLabel.TabIndex = 3;
            this.samplingLabel.Text = "Sampling Rate:";
            this.samplingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loopsLabel
            // 
            this.loopsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loopsLabel.Location = new System.Drawing.Point(3, 40);
            this.loopsLabel.Name = "loopsLabel";
            this.loopsLabel.Size = new System.Drawing.Size(189, 23);
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
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Location = new System.Drawing.Point(3, 16);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(186, 21);
            this.typeBox.TabIndex = 1;
            // 
            // typeLabel
            // 
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.typeLabel.Location = new System.Drawing.Point(0, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(192, 13);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "Type:";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tree.Size = new System.Drawing.Size(383, 323);
            this.tree.TabIndex = 0;
            this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
            // 
            // icons
            // 
            this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
            this.icons.TransparentColor = System.Drawing.Color.Transparent;
            this.icons.Images.SetKeyName(0, "FILES.png");
            this.icons.Images.SetKeyName(1, "wave.png");
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(583, 24);
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
            this.newBetaToolStripMenuItem.Name = "newBetaToolStripMenuItem";
            this.newBetaToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newBetaToolStripMenuItem.Text = "New (Beta)";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.quitToolStripMenuItem.Text = "Quit";
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
            this.soundMenu.Size = new System.Drawing.Size(153, 136);
            // 
            // Export
            // 
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(152, 22);
            this.Export.Text = "Export";
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Import
            // 
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(152, 22);
            this.Import.Text = "Import";
            this.Import.Click += new System.EventHandler(this.Import_Click);
            // 
            // Delete
            // 
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(152, 22);
            this.Delete.Text = "Delete";
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // addAbove
            // 
            this.addAbove.Name = "addAbove";
            this.addAbove.Size = new System.Drawing.Size(152, 22);
            this.addAbove.Text = "Add Above";
            this.addAbove.Click += new System.EventHandler(this.addAbove_Click);
            // 
            // addBelow
            // 
            this.addBelow.Name = "addBelow";
            this.addBelow.Size = new System.Drawing.Size(152, 22);
            this.addBelow.Text = "Add Below";
            this.addBelow.Click += new System.EventHandler(this.addBelow_Click);
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
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.importToolStripMenuItem.Text = "Import From Folder";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exportToolStripMenuItem.Text = "Export To Folder";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // SwarEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 349);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SwarEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Swar Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.swavGroup.ResumeLayout(false);
            this.swavGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplingBox)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.soundMenu.ResumeLayout(false);
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
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown samplingBox;
        private System.Windows.Forms.CheckBox loopBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label samplingLabel;
        private System.Windows.Forms.Label loopsLabel;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newBetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.ContextMenuStrip soundMenu;
        private System.Windows.Forms.ToolStripMenuItem Export;
        private System.Windows.Forms.ToolStripMenuItem Import;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private System.Windows.Forms.ToolStripMenuItem addAbove;
        private System.Windows.Forms.ToolStripMenuItem addBelow;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}