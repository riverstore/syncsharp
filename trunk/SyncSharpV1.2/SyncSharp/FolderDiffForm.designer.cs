namespace SyncSharp.GUI
{
    partial class FolderDiffForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderDiffForm));
            this.lvMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.keepBothMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delFrmSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delFrmTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noActionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.lvCompare = new DoubleBufferedListView();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.colSourceSize = new System.Windows.Forms.ColumnHeader();
            this.colSourceDate = new System.Windows.Forms.ColumnHeader();
            this.colSyncAction = new System.Windows.Forms.ColumnHeader();
            this.colTarget = new System.Windows.Forms.ColumnHeader();
            this.colTargetSize = new System.Windows.Forms.ColumnHeader();
            this.colTargetDate = new System.Windows.Forms.ColumnHeader();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTargetCreate = new System.Windows.Forms.Label();
            this.lblTargetCpy = new System.Windows.Forms.Label();
            this.lblSourceCreate = new System.Windows.Forms.Label();
            this.lblSourceCpy = new System.Windows.Forms.Label();
            this.lblTargetTotal = new System.Windows.Forms.Label();
            this.lblTargetRemove = new System.Windows.Forms.Label();
            this.lblSourceTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSourceRemove = new System.Windows.Forms.Label();
            this.lblTargetOW = new System.Windows.Forms.Label();
            this.lblTargetDel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSourceOW = new System.Windows.Forms.Label();
            this.lblSourceDel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSynchronize = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSource = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTarget = new System.Windows.Forms.ToolStripStatusLabel();
            this.lvMenu.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMenu
            // 
            this.lvMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.openFolderMenuItem,
            this.toolStripSeparator1,
            this.keepBothMenuItem,
            this.copyToSourceMenuItem,
            this.copyToTargetMenuItem,
            this.createSourceMenuItem,
            this.createTargetMenuItem,
            this.deleteMenuItem,
            this.delFrmSourceMenuItem,
            this.delFrmTargetMenuItem,
            this.noActionMenuItem,
            this.toolStripSeparator2,
            this.propertiesMenuItem});
            this.lvMenu.Name = "lvMenu";
            this.lvMenu.Size = new System.Drawing.Size(196, 302);
            this.lvMenu.Opening += new System.ComponentModel.CancelEventHandler(this.lvMenu_Opening);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openMenuItem.Text = "Open";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // openFolderMenuItem
            // 
            this.openFolderMenuItem.Name = "openFolderMenuItem";
            this.openFolderMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openFolderMenuItem.Text = "Open Folder";
            this.openFolderMenuItem.Click += new System.EventHandler(this.openFolderMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // keepBothMenuItem
            // 
            this.keepBothMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("keepBothMenuItem.Image")));
            this.keepBothMenuItem.Name = "keepBothMenuItem";
            this.keepBothMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.keepBothMenuItem.Size = new System.Drawing.Size(195, 22);
            this.keepBothMenuItem.Text = "Keep Both";
            this.keepBothMenuItem.Click += new System.EventHandler(this.keepBothMenuItem_Click);
            // 
            // copyToSourceMenuItem
            // 
            this.copyToSourceMenuItem.Image = global::SyncSharp.Properties.Resources.left_copy;
            this.copyToSourceMenuItem.Name = "copyToSourceMenuItem";
            this.copyToSourceMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.copyToSourceMenuItem.Size = new System.Drawing.Size(195, 22);
            this.copyToSourceMenuItem.Text = "Copy to Source";
            this.copyToSourceMenuItem.Click += new System.EventHandler(this.copyToSourceMenuItem_Click);
            // 
            // copyToTargetMenuItem
            // 
            this.copyToTargetMenuItem.Image = global::SyncSharp.Properties.Resources.right_copy;
            this.copyToTargetMenuItem.Name = "copyToTargetMenuItem";
            this.copyToTargetMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.copyToTargetMenuItem.Size = new System.Drawing.Size(195, 22);
            this.copyToTargetMenuItem.Text = "Copy to Target";
            this.copyToTargetMenuItem.Click += new System.EventHandler(this.copyToTargetMenuItem_Click);
            // 
            // createSourceMenuItem
            // 
            this.createSourceMenuItem.Image = global::SyncSharp.Properties.Resources.left_copy;
            this.createSourceMenuItem.Name = "createSourceMenuItem";
            this.createSourceMenuItem.Size = new System.Drawing.Size(195, 22);
            this.createSourceMenuItem.Text = "Create Source";
            this.createSourceMenuItem.Click += new System.EventHandler(this.createSourceMenuItem_Click);
            // 
            // createTargetMenuItem
            // 
            this.createTargetMenuItem.Image = global::SyncSharp.Properties.Resources.right_copy;
            this.createTargetMenuItem.Name = "createTargetMenuItem";
            this.createTargetMenuItem.Size = new System.Drawing.Size(195, 22);
            this.createTargetMenuItem.Text = "Create Target";
            this.createTargetMenuItem.Click += new System.EventHandler(this.createTargetMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Image = global::SyncSharp.Properties.Resources.remove;
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteMenuItem.Size = new System.Drawing.Size(195, 22);
            this.deleteMenuItem.Text = "Delete from Both";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // delFrmSourceMenuItem
            // 
            this.delFrmSourceMenuItem.Image = global::SyncSharp.Properties.Resources.delete_left;
            this.delFrmSourceMenuItem.Name = "delFrmSourceMenuItem";
            this.delFrmSourceMenuItem.Size = new System.Drawing.Size(195, 22);
            this.delFrmSourceMenuItem.Text = "Delete from Source";
            this.delFrmSourceMenuItem.Click += new System.EventHandler(this.delSourceMenuItem_Click);
            // 
            // delFrmTargetMenuItem
            // 
            this.delFrmTargetMenuItem.Image = global::SyncSharp.Properties.Resources.delete_right;
            this.delFrmTargetMenuItem.Name = "delFrmTargetMenuItem";
            this.delFrmTargetMenuItem.Size = new System.Drawing.Size(195, 22);
            this.delFrmTargetMenuItem.Text = "Delete from Target";
            this.delFrmTargetMenuItem.Click += new System.EventHandler(this.delTargetMenuItem_Click);
            // 
            // noActionMenuItem
            // 
            this.noActionMenuItem.Image = global::SyncSharp.Properties.Resources.noAction;
            this.noActionMenuItem.Name = "excludeMenuItem";
            this.noActionMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.noActionMenuItem.Size = new System.Drawing.Size(195, 22);
            this.noActionMenuItem.Text = "No Action";
            this.noActionMenuItem.Click += new System.EventHandler(this.excludeMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // propertiesMenuItem
            // 
            this.propertiesMenuItem.Name = "propertiesMenuItem";
            this.propertiesMenuItem.Size = new System.Drawing.Size(195, 22);
            this.propertiesMenuItem.Text = "Properties";
            this.propertiesMenuItem.Click += new System.EventHandler(this.propertiesMenuItem_Click);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.lvCompare);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scBottom);
            this.scMain.Panel2.Controls.Add(this.statusBar);
            this.scMain.Size = new System.Drawing.Size(650, 374);
            this.scMain.SplitterDistance = 195;
            this.scMain.TabIndex = 0;
            // 
            // lvCompare
            // 
            this.lvCompare.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSource,
            this.colSourceSize,
            this.colSourceDate,
            this.colSyncAction,
            this.colTarget,
            this.colTargetSize,
            this.colTargetDate});
            this.lvCompare.ContextMenuStrip = this.lvMenu;
            this.lvCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCompare.FullRowSelect = true;
            this.lvCompare.GridLines = true;
            this.lvCompare.Location = new System.Drawing.Point(0, 0);
            this.lvCompare.MultiSelect = false;
            this.lvCompare.Name = "lvCompare";
            this.lvCompare.OwnerDraw = true;
            this.lvCompare.ShowItemToolTips = true;
            this.lvCompare.Size = new System.Drawing.Size(650, 195);
            this.lvCompare.SmallImageList = this.imageList;
            this.lvCompare.TabIndex = 1;
            this.lvCompare.UseCompatibleStateImageBehavior = false;
            this.lvCompare.View = System.Windows.Forms.View.Details;
            this.lvCompare.VirtualMode = true;
            this.lvCompare.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvCompare_DrawColumnHeader);
            this.lvCompare.DoubleClick += new System.EventHandler(this.lvCompare_DoubleClick);
            this.lvCompare.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvCompare_ColumnClick);
            this.lvCompare.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvCompare_RetrieveVirtualItem);
            this.lvCompare.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvCompare_DrawSubItem);
            // 
            // colSource
            // 
            this.colSource.Text = "Source";
            this.colSource.Width = 180;
            // 
            // colSourceSize
            // 
            this.colSourceSize.Text = "Size (bytes)";
            this.colSourceSize.Width = 90;
            // 
            // colSourceDate
            // 
            this.colSourceDate.Text = "Date & Time";
            this.colSourceDate.Width = 125;
            // 
            // colSyncAction
            // 
            this.colSyncAction.Text = "Sync Action";
            this.colSyncAction.Width = 130;
            // 
            // colTarget
            // 
            this.colTarget.Text = "Target";
            this.colTarget.Width = 180;
            // 
            // colTargetSize
            // 
            this.colTargetSize.Text = "Size (bytes)";
            this.colTargetSize.Width = 90;
            // 
            // colTargetDate
            // 
            this.colTargetDate.Text = "Date & Time";
            this.colTargetDate.Width = 125;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // scBottom
            // 
            this.scBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scBottom.IsSplitterFixed = true;
            this.scBottom.Location = new System.Drawing.Point(0, 0);
            this.scBottom.Name = "scBottom";
            // 
            // scBottom.Panel1
            // 
            this.scBottom.Panel1.Controls.Add(this.groupBox1);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.groupBox2);
            this.scBottom.Panel2MinSize = 18;
            this.scBottom.Size = new System.Drawing.Size(650, 153);
            this.scBottom.SplitterDistance = 510;
            this.scBottom.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.lblTargetCreate);
            this.groupBox1.Controls.Add(this.lblTargetCpy);
            this.groupBox1.Controls.Add(this.lblSourceCreate);
            this.groupBox1.Controls.Add(this.lblSourceCpy);
            this.groupBox1.Controls.Add(this.lblTargetTotal);
            this.groupBox1.Controls.Add(this.lblTargetRemove);
            this.groupBox1.Controls.Add(this.lblSourceTotal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblSourceRemove);
            this.groupBox1.Controls.Add(this.lblTargetOW);
            this.groupBox1.Controls.Add(this.lblTargetDel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblSourceOW);
            this.groupBox1.Controls.Add(this.lblSourceDel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 153);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // lblTargetCreate
            // 
            this.lblTargetCreate.AutoSize = true;
            this.lblTargetCreate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTargetCreate.Location = new System.Drawing.Point(341, 88);
            this.lblTargetCreate.Name = "lblTargetCreate";
            this.lblTargetCreate.Size = new System.Drawing.Size(31, 13);
            this.lblTargetCreate.TabIndex = 0;
            this.lblTargetCreate.Text = "none";
            // 
            // lblTargetCpy
            // 
            this.lblTargetCpy.AutoSize = true;
            this.lblTargetCpy.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTargetCpy.Location = new System.Drawing.Point(341, 31);
            this.lblTargetCpy.Name = "lblTargetCpy";
            this.lblTargetCpy.Size = new System.Drawing.Size(99, 13);
            this.lblTargetCpy.TabIndex = 0;
            this.lblTargetCpy.Text = "40 [500 123 bytes]";
            // 
            // lblSourceCreate
            // 
            this.lblSourceCreate.AutoSize = true;
            this.lblSourceCreate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSourceCreate.Location = new System.Drawing.Point(164, 88);
            this.lblSourceCreate.Name = "lblSourceCreate";
            this.lblSourceCreate.Size = new System.Drawing.Size(66, 13);
            this.lblSourceCreate.TabIndex = 0;
            this.lblSourceCreate.Text = "1 [12 bytes]";
            // 
            // lblSourceCpy
            // 
            this.lblSourceCpy.AutoSize = true;
            this.lblSourceCpy.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSourceCpy.Location = new System.Drawing.Point(164, 31);
            this.lblSourceCpy.Name = "lblSourceCpy";
            this.lblSourceCpy.Size = new System.Drawing.Size(129, 13);
            this.lblSourceCpy.TabIndex = 0;
            this.lblSourceCpy.Text = "10 120 [1 245 345 bytes]";
            // 
            // lblTargetTotal
            // 
            this.lblTargetTotal.AutoSize = true;
            this.lblTargetTotal.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTargetTotal.Location = new System.Drawing.Point(340, 126);
            this.lblTargetTotal.Name = "lblTargetTotal";
            this.lblTargetTotal.Size = new System.Drawing.Size(87, 13);
            this.lblTargetTotal.TabIndex = 0;
            this.lblTargetTotal.Text = "2 [23 123 bytes]";
            // 
            // lblTargetRemove
            // 
            this.lblTargetRemove.AutoSize = true;
            this.lblTargetRemove.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTargetRemove.Location = new System.Drawing.Point(341, 107);
            this.lblTargetRemove.Name = "lblTargetRemove";
            this.lblTargetRemove.Size = new System.Drawing.Size(87, 13);
            this.lblTargetRemove.TabIndex = 0;
            this.lblTargetRemove.Text = "2 [23 123 bytes]";
            // 
            // lblSourceTotal
            // 
            this.lblSourceTotal.AutoSize = true;
            this.lblSourceTotal.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSourceTotal.Location = new System.Drawing.Point(164, 126);
            this.lblSourceTotal.Name = "lblSourceTotal";
            this.lblSourceTotal.Size = new System.Drawing.Size(87, 13);
            this.lblSourceTotal.TabIndex = 0;
            this.lblSourceTotal.Text = "23 [1 234 bytes]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(21, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Folders to create:";
            // 
            // lblSourceRemove
            // 
            this.lblSourceRemove.AutoSize = true;
            this.lblSourceRemove.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSourceRemove.Location = new System.Drawing.Point(164, 107);
            this.lblSourceRemove.Name = "lblSourceRemove";
            this.lblSourceRemove.Size = new System.Drawing.Size(31, 13);
            this.lblSourceRemove.TabIndex = 0;
            this.lblSourceRemove.Text = "none";
            // 
            // lblTargetOW
            // 
            this.lblTargetOW.AutoSize = true;
            this.lblTargetOW.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTargetOW.Location = new System.Drawing.Point(341, 69);
            this.lblTargetOW.Name = "lblTargetOW";
            this.lblTargetOW.Size = new System.Drawing.Size(87, 13);
            this.lblTargetOW.TabIndex = 0;
            this.lblTargetOW.Text = "10 [9 500 bytes]";
            // 
            // lblTargetDel
            // 
            this.lblTargetDel.AutoSize = true;
            this.lblTargetDel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTargetDel.Location = new System.Drawing.Point(341, 50);
            this.lblTargetDel.Name = "lblTargetDel";
            this.lblTargetDel.Size = new System.Drawing.Size(87, 13);
            this.lblTargetDel.TabIndex = 0;
            this.lblTargetDel.Text = "7 [10 200 bytes]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.label7.Location = new System.Drawing.Point(341, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Target folder";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.label6.Location = new System.Drawing.Point(164, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Source folder";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(21, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Files to copy:";
            // 
            // lblSourceOW
            // 
            this.lblSourceOW.AutoSize = true;
            this.lblSourceOW.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSourceOW.Location = new System.Drawing.Point(164, 69);
            this.lblSourceOW.Name = "lblSourceOW";
            this.lblSourceOW.Size = new System.Drawing.Size(81, 13);
            this.lblSourceOW.TabIndex = 0;
            this.lblSourceOW.Text = "7 [9 010 bytes]";
            // 
            // lblSourceDel
            // 
            this.lblSourceDel.AutoSize = true;
            this.lblSourceDel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSourceDel.Location = new System.Drawing.Point(164, 50);
            this.lblSourceDel.Name = "lblSourceDel";
            this.lblSourceDel.Size = new System.Drawing.Size(93, 13);
            this.lblSourceDel.TabIndex = 0;
            this.lblSourceDel.Text = "10 [10 000 bytes]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Files to be overwritten:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(21, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Total:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(21, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Folders to remove:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(21, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Files to delete:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHelp);
            this.groupBox2.Controls.Add(this.btnSynchronize);
            this.groupBox2.Controls.Add(this.btnAbort);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 153);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnHelp
            // 
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(20, 84);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(99, 25);
            this.btnHelp.TabIndex = 0;
            this.btnHelp.Text = "Help";
            this.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnSynchronize
            // 
            this.btnSynchronize.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSynchronize.Image = ((System.Drawing.Image)(resources.GetObject("btnSynchronize.Image")));
            this.btnSynchronize.Location = new System.Drawing.Point(20, 22);
            this.btnSynchronize.Name = "btnSynchronize";
            this.btnSynchronize.Size = new System.Drawing.Size(99, 25);
            this.btnSynchronize.TabIndex = 0;
            this.btnSynchronize.Text = "Synchronize";
            this.btnSynchronize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSynchronize.UseVisualStyleBackColor = true;
            this.btnSynchronize.Click += new System.EventHandler(this.btnSynchronize_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnAbort.Image = ((System.Drawing.Image)(resources.GetObject("btnAbort.Image")));
            this.btnAbort.Location = new System.Drawing.Point(20, 53);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(99, 25);
            this.btnAbort.TabIndex = 0;
            this.btnAbort.Text = "Abort";
            this.btnAbort.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblName,
            this.lblSource,
            this.lblTarget});
            this.statusBar.Location = new System.Drawing.Point(0, 153);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(650, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(4, 17);
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSource
            // 
            this.lblSource.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(315, 17);
            this.lblSource.Spring = true;
            this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTarget
            // 
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(315, 17);
            this.lblTarget.Spring = true;
            this.lblTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FolderDiffForm
            // 
            this.AcceptButton = this.btnSynchronize;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnAbort;
            this.ClientSize = new System.Drawing.Size(650, 374);
            this.Controls.Add(this.scMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FolderDiffForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Synchronization Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FolderDiffForm_Load);
            this.lvMenu.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.Panel2.PerformLayout();
            this.scMain.ResumeLayout(false);
            this.scBottom.Panel1.ResumeLayout(false);
            this.scBottom.Panel2.ResumeLayout(false);
            this.scBottom.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ContextMenuStrip lvMenu;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnSynchronize;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolStripMenuItem openFolderMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTargetCreate;
        private System.Windows.Forms.Label lblTargetCpy;
        private System.Windows.Forms.Label lblSourceCreate;
        private System.Windows.Forms.Label lblSourceCpy;
        private System.Windows.Forms.Label lblTargetRemove;
        private System.Windows.Forms.Label lblSourceRemove;
        private System.Windows.Forms.Label lblTargetDel;
        private System.Windows.Forms.Label lblSourceDel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem propertiesMenuItem;
        private System.Windows.Forms.SplitContainer scBottom;
        private System.Windows.Forms.Label lblTargetOW;
        private System.Windows.Forms.Label lblSourceOW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem copyToSourceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToTargetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delFrmSourceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delFrmTargetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noActionMenuItem;
        private DoubleBufferedListView lvCompare;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colSourceSize;
        private System.Windows.Forms.ColumnHeader colSourceDate;
        private System.Windows.Forms.ColumnHeader colSyncAction;
        private System.Windows.Forms.ColumnHeader colTarget;
        private System.Windows.Forms.ColumnHeader colTargetSize;
        private System.Windows.Forms.ColumnHeader colTargetDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTargetTotal;
        private System.Windows.Forms.Label lblSourceTotal;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblName;
        private System.Windows.Forms.ToolStripStatusLabel lblSource;
        private System.Windows.Forms.ToolStripStatusLabel lblTarget;
        private System.Windows.Forms.ToolStripMenuItem createSourceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTargetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepBothMenuItem;
    }
}

