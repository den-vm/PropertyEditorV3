﻿namespace PropertyEditor
{
    partial class PropertyEditorView
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyEditorView));
            this.msTool = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desbloquearScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treePefFolders = new System.Windows.Forms.TreeView();
            this.cmsEditTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.infosPef_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailComponentPef = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ItemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsEditListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lbPath = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox_ResultFind = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pbTotal = new System.Windows.Forms.ToolStripProgressBar();
            this.lbNation = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbEncrypted = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbFoundNodes = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addItemPef_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msTool.SuspendLayout();
            this.cmsEditTreeView.SuspendLayout();
            this.cmsEditListView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // msTool
            // 
            this.msTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.ferramentasToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.msTool.Location = new System.Drawing.Point(0, 0);
            this.msTool.Name = "msTool";
            this.msTool.Size = new System.Drawing.Size(1101, 24);
            this.msTool.TabIndex = 0;
            this.msTool.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.openToolStripMenuItem.Text = "Open .pef";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ferramentasToolStripMenuItem
            // 
            this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desbloquearScriptToolStripMenuItem1});
            this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
            this.ferramentasToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.ferramentasToolStripMenuItem.Text = "Tools";
            // 
            // desbloquearScriptToolStripMenuItem1
            // 
            this.desbloquearScriptToolStripMenuItem1.Name = "desbloquearScriptToolStripMenuItem1";
            this.desbloquearScriptToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.desbloquearScriptToolStripMenuItem1.Text = "Unlock Script";
            this.desbloquearScriptToolStripMenuItem1.Click += new System.EventHandler(this.desbloquearScriptToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.creditsToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.aboutToolStripMenuItem.Text = "Info";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.creditsToolStripMenuItem.Text = "More Info";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // treePefFolders
            // 
            this.treePefFolders.ContextMenuStrip = this.cmsEditTreeView;
            this.treePefFolders.Enabled = false;
            this.treePefFolders.FullRowSelect = true;
            this.treePefFolders.HotTracking = true;
            this.treePefFolders.Location = new System.Drawing.Point(6, 41);
            this.treePefFolders.Name = "treePefFolders";
            this.treePefFolders.Size = new System.Drawing.Size(418, 286);
            this.treePefFolders.TabIndex = 1;
            this.treePefFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFolders_AfterSelect);
            // 
            // cmsEditTreeView
            // 
            this.cmsEditTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infosPef_ToolStripMenuItem,
            this.addItemPef_ToolStripMenuItem});
            this.cmsEditTreeView.Name = "cmsEditTreeView";
            this.cmsEditTreeView.Size = new System.Drawing.Size(149, 48);
            // 
            // infosPef_ToolStripMenuItem
            // 
            this.infosPef_ToolStripMenuItem.Name = "infosPef_ToolStripMenuItem";
            this.infosPef_ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.infosPef_ToolStripMenuItem.Text = "Info item";
            this.infosPef_ToolStripMenuItem.Click += new System.EventHandler(this.infosToolStripMenuItem_Click);
            // 
            // detailComponentPef
            // 
            this.detailComponentPef.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.detailComponentPef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailComponentPef.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.ItemName,
            this.Type,
            this.ValueType,
            this.Value});
            this.detailComponentPef.ContextMenuStrip = this.cmsEditListView;
            this.detailComponentPef.Enabled = false;
            this.detailComponentPef.FullRowSelect = true;
            this.detailComponentPef.HideSelection = false;
            this.detailComponentPef.Location = new System.Drawing.Point(430, 15);
            this.detailComponentPef.Name = "detailComponentPef";
            this.detailComponentPef.ShowItemToolTips = true;
            this.detailComponentPef.Size = new System.Drawing.Size(662, 312);
            this.detailComponentPef.TabIndex = 2;
            this.detailComponentPef.UseCompatibleStateImageBehavior = false;
            this.detailComponentPef.View = System.Windows.Forms.View.Details;
            this.detailComponentPef.SelectedIndexChanged += new System.EventHandler(this.lvDataItems_SelectedIndexChanged);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            this.Id.Width = 98;
            // 
            // ItemName
            // 
            this.ItemName.Text = "ItemName";
            this.ItemName.Width = 113;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 124;
            // 
            // ValueType
            // 
            this.ValueType.Text = "ValueType";
            this.ValueType.Width = 107;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 137;
            // 
            // cmsEditListView
            // 
            this.cmsEditListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.cmsEditListView.Name = "cmsEdit";
            this.cmsEditListView.Size = new System.Drawing.Size(95, 26);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
            this.fileToolStripMenuItem1.Text = "Edit";
            this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            // 
            // lbPath
            // 
            this.lbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(6, 16);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(33, 13);
            this.lbPath.TabIndex = 3;
            this.lbPath.Text = "None";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listBox_ResultFind);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.treePefFolders);
            this.groupBox1.Controls.Add(this.detailComponentPef);
            this.groupBox1.Location = new System.Drawing.Point(0, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1098, 438);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // listBox_ResultFind
            // 
            this.listBox_ResultFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_ResultFind.FormattingEnabled = true;
            this.listBox_ResultFind.Location = new System.Drawing.Point(6, 333);
            this.listBox_ResultFind.Name = "listBox_ResultFind";
            this.listBox_ResultFind.Size = new System.Drawing.Size(1086, 95);
            this.listBox_ResultFind.TabIndex = 5;
            this.listBox_ResultFind.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(284, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 20);
            this.button1.TabIndex = 4;
            this.button1.Text = "Find";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(6, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(272, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbPath);
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1098, 35);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "i3PackFile";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbTotal,
            this.lbNation,
            this.lbEncrypted,
            this.lbFoundNodes});
            this.statusStrip1.Location = new System.Drawing.Point(0, 503);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1101, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pbTotal
            // 
            this.pbTotal.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pbTotal.Margin = new System.Windows.Forms.Padding(5, 3, 1, 3);
            this.pbTotal.Name = "pbTotal";
            this.pbTotal.Size = new System.Drawing.Size(130, 16);
            this.pbTotal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lbNation
            // 
            this.lbNation.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.lbNation.Name = "lbNation";
            this.lbNation.Size = new System.Drawing.Size(100, 17);
            this.lbNation.Text = "Lang client: None";
            this.lbNation.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // lbEncrypted
            // 
            this.lbEncrypted.Margin = new System.Windows.Forms.Padding(60, 3, 0, 2);
            this.lbEncrypted.Name = "lbEncrypted";
            this.lbEncrypted.Size = new System.Drawing.Size(95, 17);
            this.lbEncrypted.Text = "Encrypted: None";
            // 
            // lbFoundNodes
            // 
            this.lbFoundNodes.Margin = new System.Windows.Forms.Padding(40, 3, 0, 2);
            this.lbFoundNodes.Name = "lbFoundNodes";
            this.lbFoundNodes.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(839, 497);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 31);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Copyright © Exploit Network 2021 and SITD 2022";
            // 
            // addItemPef_ToolStripMenuItem
            // 
            this.addItemPef_ToolStripMenuItem.Name = "addItemPef_ToolStripMenuItem";
            this.addItemPef_ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addItemPef_ToolStripMenuItem.Text = "Add new item";
            this.addItemPef_ToolStripMenuItem.Click += new System.EventHandler(this.addItemPef_ToolStripMenuItem_Click);
            // 
            // PropertyEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 525);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.msTool);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msTool;
            this.Name = "PropertyEditorView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Property Editor V3";
            this.Load += new System.EventHandler(this.PropertyEditor_Load);
            this.Resize += new System.EventHandler(this.PropertyEditorView_Resize);
            this.msTool.ResumeLayout(false);
            this.msTool.PerformLayout();
            this.cmsEditTreeView.ResumeLayout(false);
            this.cmsEditListView.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msTool;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label lbPath;
        public System.Windows.Forms.TreeView treePefFolders;
        public System.Windows.Forms.ListView detailComponentPef;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader ItemName;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader ValueType;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.ContextMenuStrip cmsEditListView;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip cmsEditTreeView;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infosPef_ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel lbNation;
        private System.Windows.Forms.ToolStripStatusLabel lbEncrypted;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ToolStripProgressBar pbTotal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripStatusLabel lbFoundNodes;
        private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desbloquearScriptToolStripMenuItem1;
        private System.Windows.Forms.ListBox listBox_ResultFind;
        private System.Windows.Forms.ToolStripMenuItem addItemPef_ToolStripMenuItem;
    }
}

