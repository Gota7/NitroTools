namespace NitroStudio
{
    partial class BankGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankGenerator));
            this.banks = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.instrumentBox = new System.Windows.Forms.TextBox();
            this.rBanks = new System.Windows.Forms.ComboBox();
            this.bankPanel = new System.Windows.Forms.Panel();
            this.genButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.banks.SuspendLayout();
            this.bankPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // banks
            // 
            this.banks.Controls.Add(this.tabPage1);
            this.banks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.banks.Location = new System.Drawing.Point(0, 0);
            this.banks.Name = "banks";
            this.banks.SelectedIndex = 0;
            this.banks.Size = new System.Drawing.Size(286, 202);
            this.banks.TabIndex = 0;
            this.banks.SelectedIndexChanged += new System.EventHandler(this.banksChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(278, 176);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bank";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // instrumentBox
            // 
            this.instrumentBox.Location = new System.Drawing.Point(5, 76);
            this.instrumentBox.Name = "instrumentBox";
            this.instrumentBox.Size = new System.Drawing.Size(262, 20);
            this.instrumentBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.instrumentBox, "Instruments used for the new bank seperated by commas, Ex: 0, 1, 77");
            this.instrumentBox.TextChanged += new System.EventHandler(this.instrumentBox_TextChanged);
            // 
            // rBanks
            // 
            this.rBanks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rBanks.FormattingEnabled = true;
            this.rBanks.Location = new System.Drawing.Point(5, 26);
            this.rBanks.Name = "rBanks";
            this.rBanks.Size = new System.Drawing.Size(262, 21);
            this.rBanks.TabIndex = 3;
            this.toolTip.SetToolTip(this.rBanks, "Bank to get instruments from.");
            this.rBanks.SelectedIndexChanged += new System.EventHandler(this.rBanksIndexChanged);
            // 
            // bankPanel
            // 
            this.bankPanel.Controls.Add(this.instrumentBox);
            this.bankPanel.Controls.Add(this.genButton);
            this.bankPanel.Controls.Add(this.label2);
            this.bankPanel.Controls.Add(this.rBanks);
            this.bankPanel.Controls.Add(this.label1);
            this.bankPanel.Controls.Add(this.addButton);
            this.bankPanel.Controls.Add(this.removeButton);
            this.bankPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bankPanel.Location = new System.Drawing.Point(0, 0);
            this.bankPanel.Name = "bankPanel";
            this.bankPanel.Size = new System.Drawing.Size(286, 202);
            this.bankPanel.TabIndex = 3;
            // 
            // genButton
            // 
            this.genButton.Location = new System.Drawing.Point(5, 142);
            this.genButton.Name = "genButton";
            this.genButton.Size = new System.Drawing.Size(262, 23);
            this.genButton.TabIndex = 5;
            this.genButton.Text = "Generate New Bank Using Selected Instruments";
            this.genButton.UseVisualStyleBackColor = true;
            this.genButton.Click += new System.EventHandler(this.genButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Instruments:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bank:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(139, 115);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(128, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add Source Bank";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(5, 115);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(128, 23);
            this.removeButton.TabIndex = 0;
            this.removeButton.Text = "Remove Source Bank";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // BankGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 202);
            this.Controls.Add(this.banks);
            this.Controls.Add(this.bankPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BankGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Generator";
            this.banks.ResumeLayout(false);
            this.bankPanel.ResumeLayout(false);
            this.bankPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl banks;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel bankPanel;
        private System.Windows.Forms.TextBox instrumentBox;
        private System.Windows.Forms.Button genButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox rBanks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
    }
}