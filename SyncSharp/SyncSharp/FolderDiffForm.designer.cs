﻿namespace SyncSharp
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
            this.selectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.open = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAction = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToSource = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToTarget = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.excludeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showFilesNotInSrc = new System.Windows.Forms.ToolStripMenuItem();
            this.showFilesNotInTarget = new System.Windows.Forms.ToolStripMenuItem();
            this.showChangedFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.hideFolderDiff = new System.Windows.Forms.ToolStripMenuItem();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.lvCompare = new System.Windows.Forms.ListView();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.colSourceSize = new System.Windows.Forms.ColumnHeader();
            this.colSourceDate = new System.Windows.Forms.ColumnHeader();
            this.colSyncAction = new System.Windows.Forms.ColumnHeader();
            this.colTarget = new System.Windows.Forms.ColumnHeader();
            this.colTargetSize = new System.Windows.Forms.ColumnHeader();
            this.colTargetDate = new System.Windows.Forms.ColumnHeader();
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.scView = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDeletedTarget = new System.Windows.Forms.Label();
            this.lblSingleTarget = new System.Windows.Forms.Label();
            this.lblDeletedSrc = new System.Windows.Forms.Label();
            this.lblSingleSrc = new System.Windows.Forms.Label();
            this.lblExcludeTarget = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblExcludeSrc = new System.Windows.Forms.Label();
            this.lblOlderTarget = new System.Windows.Forms.Label();
            this.lblNewerTarget = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblOlderSrc = new System.Windows.Forms.Label();
            this.lblNewerSrc = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSynchronize = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.syncProgressBar = new System.Windows.Forms.ProgressBar();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.lvMenu.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.scView.Panel1.SuspendLayout();
            this.scView.Panel2.SuspendLayout();
            this.scView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMenu
            // 
            this.lvMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAll,
            this.open,
            this.changeAction,
            this.toolStripSeparator2,
            this.showFilesNotInSrc,
            this.showFilesNotInTarget,
            this.showChangedFiles,
            this.toolStripSeparator1,
            this.hideFolderDiff});
            this.lvMenu.Name = "listMenu";
            this.lvMenu.Size = new System.Drawing.Size(223, 170);
            // 
            // selectAll
            // 
            this.selectAll.Name = "selectAll";
            this.selectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAll.Size = new System.Drawing.Size(222, 22);
            this.selectAll.Text = "Select all";
            // 
            // open
            // 
            this.open.Name = "open";
            this.open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.open.Size = new System.Drawing.Size(222, 22);
            this.open.Text = "Open";
            // 
            // changeAction
            // 
            this.changeAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToSource,
            this.copyToTarget,
            this.deleteAll,
            this.excludeAll});
            this.changeAction.Name = "changeAction";
            this.changeAction.Size = new System.Drawing.Size(222, 22);
            this.changeAction.Text = "Change action";
            // 
            // copyToSource
            // 
            this.copyToSource.Image = ((System.Drawing.Image)(resources.GetObject("copyToSource.Image")));
            this.copyToSource.Name = "copyToSource";
            this.copyToSource.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.copyToSource.Size = new System.Drawing.Size(185, 22);
            this.copyToSource.Text = "Copy to source";
            // 
            // copyToTarget
            // 
            this.copyToTarget.Image = ((System.Drawing.Image)(resources.GetObject("copyToTarget.Image")));
            this.copyToTarget.Name = "copyToTarget";
            this.copyToTarget.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.copyToTarget.Size = new System.Drawing.Size(185, 22);
            this.copyToTarget.Text = "Copy to target";
            // 
            // deleteAll
            // 
            this.deleteAll.Image = ((System.Drawing.Image)(resources.GetObject("deleteAll.Image")));
            this.deleteAll.Name = "deleteAll";
            this.deleteAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deleteAll.Size = new System.Drawing.Size(185, 22);
            this.deleteAll.Text = "Delete";
            // 
            // excludeAll
            // 
            this.excludeAll.Image = ((System.Drawing.Image)(resources.GetObject("excludeAll.Image")));
            this.excludeAll.Name = "excludeAll";
            this.excludeAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.excludeAll.Size = new System.Drawing.Size(185, 22);
            this.excludeAll.Text = "Exclude";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // showFilesNotInSrc
            // 
            this.showFilesNotInSrc.Checked = true;
            this.showFilesNotInSrc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFilesNotInSrc.Name = "showFilesNotInSrc";
            this.showFilesNotInSrc.Size = new System.Drawing.Size(222, 22);
            this.showFilesNotInSrc.Text = "Show files not in source";
            // 
            // showFilesNotInTarget
            // 
            this.showFilesNotInTarget.Checked = true;
            this.showFilesNotInTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFilesNotInTarget.Name = "showFilesNotInTarget";
            this.showFilesNotInTarget.Size = new System.Drawing.Size(222, 22);
            this.showFilesNotInTarget.Text = "Show files not in target";
            // 
            // showChangedFiles
            // 
            this.showChangedFiles.Checked = true;
            this.showChangedFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showChangedFiles.Name = "showChangedFiles";
            this.showChangedFiles.Size = new System.Drawing.Size(222, 22);
            this.showChangedFiles.Text = "Show changed files";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // hideFolderDiff
            // 
            this.hideFolderDiff.Name = "hideFolderDiff";
            this.hideFolderDiff.Size = new System.Drawing.Size(222, 22);
            this.hideFolderDiff.Text = "Do not show this window again";
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
            this.scMain.Size = new System.Drawing.Size(650, 374);
            this.scMain.SplitterDistance = 217;
            this.scMain.SplitterWidth = 5;
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
            this.lvCompare.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.lvCompare.FullRowSelect = true;
            this.lvCompare.GridLines = true;
            this.lvCompare.Location = new System.Drawing.Point(0, 0);
            this.lvCompare.Name = "lvCompare";
            this.lvCompare.OwnerDraw = true;
            this.lvCompare.Size = new System.Drawing.Size(650, 217);
            this.lvCompare.TabIndex = 3;
            this.lvCompare.UseCompatibleStateImageBehavior = false;
            this.lvCompare.View = System.Windows.Forms.View.Details;
            this.lvCompare.VirtualMode = true;
            this.lvCompare.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvDiff_DrawColumnHeader);
            this.lvCompare.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvDiff_ColumnClick);
            this.lvCompare.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvDiff_RetrieveVirtualItem);
            this.lvCompare.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvDiff_DrawSubItem);
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
            this.colSourceDate.Width = 120;
            // 
            // colSyncAction
            // 
            this.colSyncAction.Text = "Sync Action";
            this.colSyncAction.Width = 120;
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
            this.colTargetDate.Width = 120;
            // 
            // scBottom
            // 
            this.scBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scBottom.IsSplitterFixed = true;
            this.scBottom.Location = new System.Drawing.Point(0, 0);
            this.scBottom.Name = "scBottom";
            this.scBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scBottom.Panel1
            // 
            this.scBottom.Panel1.Controls.Add(this.scView);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.syncProgressBar);
            this.scBottom.Panel2MinSize = 18;
            this.scBottom.Size = new System.Drawing.Size(650, 152);
            this.scBottom.SplitterDistance = 130;
            this.scBottom.TabIndex = 0;
            // 
            // scView
            // 
            this.scView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scView.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scView.IsSplitterFixed = true;
            this.scView.Location = new System.Drawing.Point(0, 0);
            this.scView.Name = "scView";
            // 
            // scView.Panel1
            // 
            this.scView.Panel1.Controls.Add(this.groupBox1);
            // 
            // scView.Panel2
            // 
            this.scView.Panel2.Controls.Add(this.groupBox2);
            this.scView.Size = new System.Drawing.Size(650, 130);
            this.scView.SplitterDistance = 510;
            this.scView.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDeletedTarget);
            this.groupBox1.Controls.Add(this.lblSingleTarget);
            this.groupBox1.Controls.Add(this.lblDeletedSrc);
            this.groupBox1.Controls.Add(this.lblSingleSrc);
            this.groupBox1.Controls.Add(this.lblExcludeTarget);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblExcludeSrc);
            this.groupBox1.Controls.Add(this.lblOlderTarget);
            this.groupBox1.Controls.Add(this.lblNewerTarget);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblOlderSrc);
            this.groupBox1.Controls.Add(this.lblNewerSrc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 130);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // lblDeletedTarget
            // 
            this.lblDeletedTarget.AutoSize = true;
            this.lblDeletedTarget.Location = new System.Drawing.Point(327, 88);
            this.lblDeletedTarget.Name = "lblDeletedTarget";
            this.lblDeletedTarget.Size = new System.Drawing.Size(31, 13);
            this.lblDeletedTarget.TabIndex = 0;
            this.lblDeletedTarget.Text = "none";
            // 
            // lblSingleTarget
            // 
            this.lblSingleTarget.AutoSize = true;
            this.lblSingleTarget.Location = new System.Drawing.Point(327, 31);
            this.lblSingleTarget.Name = "lblSingleTarget";
            this.lblSingleTarget.Size = new System.Drawing.Size(99, 13);
            this.lblSingleTarget.TabIndex = 0;
            this.lblSingleTarget.Text = "40 [500 123 bytes]";
            // 
            // lblDeletedSrc
            // 
            this.lblDeletedSrc.AutoSize = true;
            this.lblDeletedSrc.Location = new System.Drawing.Point(157, 88);
            this.lblDeletedSrc.Name = "lblDeletedSrc";
            this.lblDeletedSrc.Size = new System.Drawing.Size(66, 13);
            this.lblDeletedSrc.TabIndex = 0;
            this.lblDeletedSrc.Text = "1 [12 bytes]";
            // 
            // lblSingleSrc
            // 
            this.lblSingleSrc.AutoSize = true;
            this.lblSingleSrc.Location = new System.Drawing.Point(157, 31);
            this.lblSingleSrc.Name = "lblSingleSrc";
            this.lblSingleSrc.Size = new System.Drawing.Size(129, 13);
            this.lblSingleSrc.TabIndex = 0;
            this.lblSingleSrc.Text = "10 120 [1 245 345 bytes]";
            // 
            // lblExcludeTarget
            // 
            this.lblExcludeTarget.AutoSize = true;
            this.lblExcludeTarget.Location = new System.Drawing.Point(327, 107);
            this.lblExcludeTarget.Name = "lblExcludeTarget";
            this.lblExcludeTarget.Size = new System.Drawing.Size(87, 13);
            this.lblExcludeTarget.TabIndex = 0;
            this.lblExcludeTarget.Text = "2 [23 123 bytes]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Deleted files:";
            // 
            // lblExcludeSrc
            // 
            this.lblExcludeSrc.AutoSize = true;
            this.lblExcludeSrc.Location = new System.Drawing.Point(157, 107);
            this.lblExcludeSrc.Name = "lblExcludeSrc";
            this.lblExcludeSrc.Size = new System.Drawing.Size(31, 13);
            this.lblExcludeSrc.TabIndex = 0;
            this.lblExcludeSrc.Text = "none";
            // 
            // lblOlderTarget
            // 
            this.lblOlderTarget.AutoSize = true;
            this.lblOlderTarget.Location = new System.Drawing.Point(327, 69);
            this.lblOlderTarget.Name = "lblOlderTarget";
            this.lblOlderTarget.Size = new System.Drawing.Size(87, 13);
            this.lblOlderTarget.TabIndex = 0;
            this.lblOlderTarget.Text = "10 [9 500 bytes]";
            // 
            // lblNewerTarget
            // 
            this.lblNewerTarget.AutoSize = true;
            this.lblNewerTarget.Location = new System.Drawing.Point(327, 50);
            this.lblNewerTarget.Name = "lblNewerTarget";
            this.lblNewerTarget.Size = new System.Drawing.Size(87, 13);
            this.lblNewerTarget.TabIndex = 0;
            this.lblNewerTarget.Text = "7 [10 200 bytes]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(327, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Target folder";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(157, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Source folder";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Single files:";
            // 
            // lblOlderSrc
            // 
            this.lblOlderSrc.AutoSize = true;
            this.lblOlderSrc.Location = new System.Drawing.Point(157, 69);
            this.lblOlderSrc.Name = "lblOlderSrc";
            this.lblOlderSrc.Size = new System.Drawing.Size(81, 13);
            this.lblOlderSrc.TabIndex = 0;
            this.lblOlderSrc.Text = "7 [9 010 bytes]";
            // 
            // lblNewerSrc
            // 
            this.lblNewerSrc.AutoSize = true;
            this.lblNewerSrc.Location = new System.Drawing.Point(157, 50);
            this.lblNewerSrc.Name = "lblNewerSrc";
            this.lblNewerSrc.Size = new System.Drawing.Size(93, 13);
            this.lblNewerSrc.TabIndex = 0;
            this.lblNewerSrc.Text = "10 [10 000 bytes]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Older files:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Excluded files:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Newer files:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHelp);
            this.groupBox2.Controls.Add(this.btnSynchronize);
            this.groupBox2.Controls.Add(this.btnAbort);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 130);
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
            this.btnSynchronize.Image = ((System.Drawing.Image)(resources.GetObject("btnSynchronize.Image")));
            this.btnSynchronize.Location = new System.Drawing.Point(20, 22);
            this.btnSynchronize.Name = "btnSynchronize";
            this.btnSynchronize.Size = new System.Drawing.Size(99, 25);
            this.btnSynchronize.TabIndex = 0;
            this.btnSynchronize.Text = "Synchronize";
            this.btnSynchronize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSynchronize.UseVisualStyleBackColor = true;
            // 
            // btnAbort
            // 
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
            // syncProgressBar
            // 
            this.syncProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syncProgressBar.Location = new System.Drawing.Point(0, 0);
            this.syncProgressBar.Name = "syncProgressBar";
            this.syncProgressBar.Size = new System.Drawing.Size(650, 18);
            this.syncProgressBar.TabIndex = 0;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FolderDiffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(650, 374);
            this.Controls.Add(this.scMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.Name = "FolderDiffForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FolderDiff";
            this.Load += new System.EventHandler(this.FolderDiffForm_Load);
            this.lvMenu.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.scBottom.Panel1.ResumeLayout(false);
            this.scBottom.Panel2.ResumeLayout(false);
            this.scBottom.ResumeLayout(false);
            this.scView.Panel1.ResumeLayout(false);
            this.scView.Panel2.ResumeLayout(false);
            this.scView.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ContextMenuStrip lvMenu;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ListView lvCompare;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colSourceSize;
        private System.Windows.Forms.ColumnHeader colSourceDate;
        private System.Windows.Forms.ColumnHeader colSyncAction;
        private System.Windows.Forms.ColumnHeader colTarget;
        private System.Windows.Forms.ColumnHeader colTargetSize;
        private System.Windows.Forms.ColumnHeader colTargetDate;
        private System.Windows.Forms.ToolStripMenuItem open;
        private System.Windows.Forms.ToolStripMenuItem changeAction;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyToSource;
        private System.Windows.Forms.ToolStripMenuItem copyToTarget;
        private System.Windows.Forms.ToolStripMenuItem deleteAll;
        private System.Windows.Forms.ToolStripMenuItem excludeAll;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnSynchronize;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolStripMenuItem selectAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer scView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem showFilesNotInSrc;
        private System.Windows.Forms.ToolStripMenuItem showFilesNotInTarget;
        private System.Windows.Forms.ToolStripMenuItem showChangedFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDeletedTarget;
        private System.Windows.Forms.Label lblSingleTarget;
        private System.Windows.Forms.Label lblDeletedSrc;
        private System.Windows.Forms.Label lblSingleSrc;
        private System.Windows.Forms.Label lblExcludeTarget;
        private System.Windows.Forms.Label lblExcludeSrc;
        private System.Windows.Forms.Label lblNewerTarget;
        private System.Windows.Forms.Label lblNewerSrc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem hideFolderDiff;
        private System.Windows.Forms.SplitContainer scBottom;
        private System.Windows.Forms.ProgressBar syncProgressBar;
        private System.Windows.Forms.Label lblOlderTarget;
        private System.Windows.Forms.Label lblOlderSrc;
        private System.Windows.Forms.Label label1;
    }
}

