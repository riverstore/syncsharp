namespace SyncSharp.GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnModify = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSync = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.btnAnalyze = new System.Windows.Forms.ToolStripButton();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.taskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.analyzeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.openSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.viewLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolBarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBackupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSyncMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.taskListView = new System.Windows.Forms.ListView();
            this.colTask = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colLastRun = new System.Windows.Forms.ColumnHeader();
            this.colResult = new System.Windows.Forms.ColumnHeader();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.colTarget = new System.Windows.Forms.ColumnHeader();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newTaskCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.analyzeCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.viewLogCMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnModify,
            this.btnDelete,
            this.separator1,
            this.btnSync,
            this.btnExit,
            this.btnHelp,
            this.btnAnalyze});
            this.toolBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolBar.Location = new System.Drawing.Point(0, 24);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(630, 46);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolBar";
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(35, 43);
            this.btnNew.Text = "&New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnModify
            // 
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModify.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(49, 43);
            this.btnModify.Text = "&Modify";
            this.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModify.ToolTipText = "Modify";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(44, 43);
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(6, 46);
            // 
            // btnSync
            // 
            this.btnSync.Image = ((System.Drawing.Image)(resources.GetObject("btnSync.Image")));
            this.btnSync.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSync.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(75, 43);
            this.btnSync.Text = "&Synchronize";
            this.btnSync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnExit
            // 
            this.btnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 1, 4, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(29, 43);
            this.btnExit.Text = "E&xit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(36, 43);
            this.btnHelp.Text = "He&lp";
            this.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Image = ((System.Drawing.Image)(resources.GetObject("btnAnalyze.Image")));
            this.btnAnalyze.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(52, 43);
            this.btnAnalyze.Text = "&Analyze";
            this.btnAnalyze.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskMenuItem,
            this.actionMenuItem,
            this.viewMenuItem,
            this.helpMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(630, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "mainMenu";
            // 
            // taskMenuItem
            // 
            this.taskMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.modifyMenuItem,
            this.deleteMenuItem,
            this.renameMenuItem,
            this.toolStripSeparator1,
            this.importMenuItem,
            this.exportMenuItem});
            this.taskMenuItem.Name = "taskMenuItem";
            this.taskMenuItem.Size = new System.Drawing.Size(43, 20);
            this.taskMenuItem.Text = "T&ask";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newMenuItem.Image")));
            this.newMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newMenuItem.Size = new System.Drawing.Size(193, 22);
            this.newMenuItem.Text = "&New";
            this.newMenuItem.Click += new System.EventHandler(this.newMenuItem_Click);
            // 
            // modifyMenuItem
            // 
            this.modifyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("modifyMenuItem.Image")));
            this.modifyMenuItem.Name = "modifyMenuItem";
            this.modifyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.modifyMenuItem.Size = new System.Drawing.Size(193, 22);
            this.modifyMenuItem.Text = "Modify";
            this.modifyMenuItem.Click += new System.EventHandler(this.modifyMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteMenuItem.Image")));
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteMenuItem.Size = new System.Drawing.Size(193, 22);
            this.deleteMenuItem.Text = "Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // renameMenuItem
            // 
            this.renameMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("renameMenuItem.Image")));
            this.renameMenuItem.Name = "renameMenuItem";
            this.renameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameMenuItem.Size = new System.Drawing.Size(193, 22);
            this.renameMenuItem.Text = "Rename";
            this.renameMenuItem.Click += new System.EventHandler(this.renameMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // importMenuItem
            // 
            this.importMenuItem.Name = "importMenuItem";
            this.importMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importMenuItem.Size = new System.Drawing.Size(193, 22);
            this.importMenuItem.Text = "Import Profile...";
            this.importMenuItem.Click += new System.EventHandler(this.importMenuItem_Click);
            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Name = "exportMenuItem";
            this.exportMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportMenuItem.Size = new System.Drawing.Size(193, 22);
            this.exportMenuItem.Text = "Export Profile...";
            this.exportMenuItem.Click += new System.EventHandler(this.exportMenuItem_Click);
            // 
            // actionMenuItem
            // 
            this.actionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllMenuItem,
            this.toolStripSeparator2,
            this.analyzeMenuItem,
            this.syncMenuItem,
            this.restoreMenuItem,
            this.syncAllMenuItem,
            this.toolStripSeparator3,
            this.openSourceMenuItem,
            this.openTargetMenuItem,
            this.toolStripSeparator5,
            this.viewLogMenuItem});
            this.actionMenuItem.Name = "actionMenuItem";
            this.actionMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionMenuItem.Text = "&Action";
            // 
            // selectAllMenuItem
            // 
            this.selectAllMenuItem.Name = "selectAllMenuItem";
            this.selectAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectAllMenuItem.Text = "Select all";
            this.selectAllMenuItem.Click += new System.EventHandler(this.selectAllMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // analyzeMenuItem
            // 
            this.analyzeMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("analyzeMenuItem.Image")));
            this.analyzeMenuItem.Name = "analyzeMenuItem";
            this.analyzeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.analyzeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.analyzeMenuItem.Text = "Analyze";
            this.analyzeMenuItem.Click += new System.EventHandler(this.analyzeMenuItem_Click);
            // 
            // syncMenuItem
            // 
            this.syncMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("syncMenuItem.Image")));
            this.syncMenuItem.Name = "syncMenuItem";
            this.syncMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.syncMenuItem.Size = new System.Drawing.Size(180, 22);
            this.syncMenuItem.Text = "Synchronize";
            this.syncMenuItem.Click += new System.EventHandler(this.syncMenuItem_Click);
            // 
            // restoreMenuItem
            // 
            this.restoreMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restoreMenuItem.Image")));
            this.restoreMenuItem.Name = "restoreMenuItem";
            this.restoreMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.restoreMenuItem.Size = new System.Drawing.Size(180, 22);
            this.restoreMenuItem.Text = "Restore";
            this.restoreMenuItem.Click += new System.EventHandler(this.restoreMenuItem_Click);
            // 
            // syncAllMenuItem
            // 
            this.syncAllMenuItem.Name = "syncAllMenuItem";
            this.syncAllMenuItem.Size = new System.Drawing.Size(180, 22);
            this.syncAllMenuItem.Text = "Sync All Folder Pairs";
            this.syncAllMenuItem.Click += new System.EventHandler(this.syncAllMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // openSourceMenuItem
            // 
            this.openSourceMenuItem.Name = "openSourceMenuItem";
            this.openSourceMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openSourceMenuItem.Text = "Open Source Folder";
            this.openSourceMenuItem.Click += new System.EventHandler(this.openSourceMenuItem_Click);
            // 
            // openTargetMenuItem
            // 
            this.openTargetMenuItem.Name = "openTargetMenuItem";
            this.openTargetMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openTargetMenuItem.Text = "Open Target Folder";
            this.openTargetMenuItem.Click += new System.EventHandler(this.openTargetMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // viewLogMenuItem
            // 
            this.viewLogMenuItem.Name = "viewLogMenuItem";
            this.viewLogMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.viewLogMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewLogMenuItem.Text = "View Log";
            this.viewLogMenuItem.Click += new System.EventHandler(this.viewLogMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolBarMenuItem,
            this.showBackupMenuItem,
            this.showSyncMenuItem,
            this.toolStripSeparator4,
            this.optionsMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "&View";
            // 
            // hideToolBarMenuItem
            // 
            this.hideToolBarMenuItem.Name = "hideToolBarMenuItem";
            this.hideToolBarMenuItem.Size = new System.Drawing.Size(188, 22);
            this.hideToolBarMenuItem.Text = "&Hide Toolbar";
            this.hideToolBarMenuItem.Click += new System.EventHandler(this.hideToolBarMenuItem_Click);
            // 
            // showBackupMenuItem
            // 
            this.showBackupMenuItem.Checked = true;
            this.showBackupMenuItem.CheckOnClick = true;
            this.showBackupMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBackupMenuItem.Name = "showBackupMenuItem";
            this.showBackupMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showBackupMenuItem.Text = "Show Backup Task(s) ";
            this.showBackupMenuItem.Click += new System.EventHandler(this.showBackupMenuItem_Click);
            // 
            // showSyncMenuItem
            // 
            this.showSyncMenuItem.Checked = true;
            this.showSyncMenuItem.CheckOnClick = true;
            this.showSyncMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSyncMenuItem.Name = "showSyncMenuItem";
            this.showSyncMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showSyncMenuItem.Text = "Show Sync Task(s) ";
            this.showSyncMenuItem.Click += new System.EventHandler(this.showSyncMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(185, 6);
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(188, 22);
            this.optionsMenuItem.Text = "&Options...";
            this.optionsMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpMenuItem,
            this.aboutMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // showHelpMenuItem
            // 
            this.showHelpMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showHelpMenuItem.Image")));
            this.showHelpMenuItem.Name = "showHelpMenuItem";
            this.showHelpMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.showHelpMenuItem.Size = new System.Drawing.Size(118, 22);
            this.showHelpMenuItem.Text = "Help";
            this.showHelpMenuItem.Click += new System.EventHandler(this.showHelpMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(118, 22);
            this.aboutMenuItem.Text = "&About...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
            this.statusBar.Location = new System.Drawing.Point(0, 366);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(630, 24);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusBar";
            // 
            // lblStatus
            // 
            this.lblStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(482, 19);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 18);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // taskListView
            // 
            this.taskListView.AllowDrop = true;
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTask,
            this.colType,
            this.colLastRun,
            this.colResult,
            this.colSource,
            this.colTarget});
            this.taskListView.ContextMenuStrip = this.contextMenu;
            this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskListView.FullRowSelect = true;
            this.taskListView.Location = new System.Drawing.Point(0, 70);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(630, 296);
            this.taskListView.TabIndex = 4;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Details;
            this.taskListView.SelectedIndexChanged += new System.EventHandler(this.taskListView_SelectedIndexChanged);
            this.taskListView.DoubleClick += new System.EventHandler(this.taskListView_DoubleClick);
            this.taskListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.taskListView_DragDrop);
            this.taskListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.taskListView_DragEnter);
            this.taskListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.taskListView_ColumnWidthChanging);
            // 
            // colTask
            // 
            this.colTask.Text = "Task";
            this.colTask.Width = 200;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 100;
            // 
            // colLastRun
            // 
            this.colLastRun.Text = "Last Run";
            this.colLastRun.Width = 140;
            // 
            // colResult
            // 
            this.colResult.Text = "Result";
            this.colResult.Width = 120;
            // 
            // colSource
            // 
            this.colSource.Text = "Source";
            this.colSource.Width = 120;
            // 
            // colTarget
            // 
            this.colTarget.Text = "Target";
            this.colTarget.Width = 120;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTaskCMItem,
            this.toolStripSeparator6,
            this.analyzeCMItem,
            this.syncCMItem,
            this.restoreCMItem,
            this.modifyCMItem,
            this.deleteCMItem,
            this.renameCMItem,
            this.toolStripSeparator7,
            this.viewLogCMItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(179, 192);
            // 
            // newTaskCMItem
            // 
            this.newTaskCMItem.Image = ((System.Drawing.Image)(resources.GetObject("newTaskCMItem.Image")));
            this.newTaskCMItem.Name = "newTaskCMItem";
            this.newTaskCMItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newTaskCMItem.Size = new System.Drawing.Size(178, 22);
            this.newTaskCMItem.Text = "New Task";
            this.newTaskCMItem.Click += new System.EventHandler(this.newTaskCMItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(175, 6);
            // 
            // analyzeCMItem
            // 
            this.analyzeCMItem.Image = ((System.Drawing.Image)(resources.GetObject("analyzeCMItem.Image")));
            this.analyzeCMItem.Name = "analyzeCMItem";
            this.analyzeCMItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.analyzeCMItem.Size = new System.Drawing.Size(178, 22);
            this.analyzeCMItem.Text = "Analyze";
            this.analyzeCMItem.Click += new System.EventHandler(this.analyzeCMItem_Click);
            // 
            // syncCMItem
            // 
            this.syncCMItem.Image = ((System.Drawing.Image)(resources.GetObject("syncCMItem.Image")));
            this.syncCMItem.Name = "syncCMItem";
            this.syncCMItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.syncCMItem.Size = new System.Drawing.Size(178, 22);
            this.syncCMItem.Text = "Synchronize";
            this.syncCMItem.Click += new System.EventHandler(this.syncCMItem_Click);
            // 
            // restoreCMItem
            // 
            this.restoreCMItem.Image = ((System.Drawing.Image)(resources.GetObject("restoreCMItem.Image")));
            this.restoreCMItem.Name = "restoreCMItem";
            this.restoreCMItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.restoreCMItem.Size = new System.Drawing.Size(178, 22);
            this.restoreCMItem.Text = "Restore";
            this.restoreCMItem.Click += new System.EventHandler(this.restoreCMItem_Click);
            // 
            // modifyCMItem
            // 
            this.modifyCMItem.Image = ((System.Drawing.Image)(resources.GetObject("modifyCMItem.Image")));
            this.modifyCMItem.Name = "modifyCMItem";
            this.modifyCMItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.modifyCMItem.Size = new System.Drawing.Size(178, 22);
            this.modifyCMItem.Text = "Modify";
            this.modifyCMItem.Click += new System.EventHandler(this.modifyCMItem_Click);
            // 
            // deleteCMItem
            // 
            this.deleteCMItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteCMItem.Image")));
            this.deleteCMItem.Name = "deleteCMItem";
            this.deleteCMItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteCMItem.Size = new System.Drawing.Size(178, 22);
            this.deleteCMItem.Text = "Delete";
            this.deleteCMItem.Click += new System.EventHandler(this.deleteCMItem_Click);
            // 
            // renameCMItem
            // 
            this.renameCMItem.Image = ((System.Drawing.Image)(resources.GetObject("renameCMItem.Image")));
            this.renameCMItem.Name = "renameCMItem";
            this.renameCMItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameCMItem.Size = new System.Drawing.Size(178, 22);
            this.renameCMItem.Text = "Rename";
            this.renameCMItem.Click += new System.EventHandler(this.renameCMItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(175, 6);
            // 
            // viewLogCMItem
            // 
            this.viewLogCMItem.Name = "viewLogCMItem";
            this.viewLogCMItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.viewLogCMItem.Size = new System.Drawing.Size(178, 22);
            this.viewLogCMItem.Text = "View Log";
            this.viewLogCMItem.Click += new System.EventHandler(this.viewLogCMItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 390);
            this.Controls.Add(this.taskListView);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SyncSharp - Plug and Sync";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripMenuItem taskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem actionMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnModify;
        private System.Windows.Forms.ToolStripButton btnSync;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripMenuItem modifyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSourceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTargetMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem viewLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolBarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeMenuItem;
        private System.Windows.Forms.ToolStripButton btnAnalyze;
        private System.Windows.Forms.ToolStripMenuItem showBackupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSyncMenuItem;
        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.ColumnHeader colTask;
        private System.Windows.Forms.ColumnHeader colLastRun;
        private System.Windows.Forms.ColumnHeader colResult;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colTarget;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem newTaskCMItem;
        private System.Windows.Forms.ToolStripMenuItem modifyCMItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCMItem;
        private System.Windows.Forms.ToolStripMenuItem renameCMItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogCMItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem syncCMItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeCMItem;
        private System.Windows.Forms.ToolStripMenuItem restoreMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreCMItem;
    }
}

