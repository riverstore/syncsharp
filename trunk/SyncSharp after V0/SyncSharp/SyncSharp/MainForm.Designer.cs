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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
					this.tsSyncSharp = new System.Windows.Forms.ToolStrip();
					this.btnNew = new System.Windows.Forms.ToolStripButton();
					this.btnEdit = new System.Windows.Forms.ToolStripButton();
					this.btnDelete = new System.Windows.Forms.ToolStripButton();
					this.separator1 = new System.Windows.Forms.ToolStripSeparator();
					this.btnSync = new System.Windows.Forms.ToolStripButton();
					this.btnExit = new System.Windows.Forms.ToolStripButton();
					this.btnHelp = new System.Windows.Forms.ToolStripButton();
					this.btnAnalyze = new System.Windows.Forms.ToolStripButton();
					this.msSyncSharp = new System.Windows.Forms.MenuStrip();
					this.taskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.renameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
					this.importMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.actionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.analyzeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.syncMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.syncAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
					this.openSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.openTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
					this.viewLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.hideToolBarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.showBackupTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.showSyncTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.showHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.ssSyncSharp = new System.Windows.Forms.StatusStrip();
					this.taskListView = new System.Windows.Forms.ListView();
					this.colTask = new System.Windows.Forms.ColumnHeader();
					this.colLastRun = new System.Windows.Forms.ColumnHeader();
					this.colResult = new System.Windows.Forms.ColumnHeader();
					this.colSource = new System.Windows.Forms.ColumnHeader();
					this.colTarget = new System.Windows.Forms.ColumnHeader();
					this.tsSyncSharp.SuspendLayout();
					this.msSyncSharp.SuspendLayout();
					this.SuspendLayout();
					// 
					// tsSyncSharp
					// 
					this.tsSyncSharp.ImageScalingSize = new System.Drawing.Size(24, 24);
					this.tsSyncSharp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnEdit,
            this.btnDelete,
            this.separator1,
            this.btnSync,
            this.btnExit,
            this.btnHelp,
            this.btnAnalyze});
					this.tsSyncSharp.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
					this.tsSyncSharp.Location = new System.Drawing.Point(0, 24);
					this.tsSyncSharp.Name = "tsSyncSharp";
					this.tsSyncSharp.Size = new System.Drawing.Size(632, 46);
					this.tsSyncSharp.TabIndex = 0;
					this.tsSyncSharp.Text = "toolbar";
					// 
					// btnNew
					// 
					this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
					this.btnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.btnNew.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
					this.btnNew.Name = "btnNew";
					this.btnNew.Size = new System.Drawing.Size(35, 43);
					this.btnNew.Text = "&New";
					this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
					// 
					// btnEdit
					// 
					this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
					this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
					this.btnEdit.Name = "btnEdit";
					this.btnEdit.Size = new System.Drawing.Size(49, 43);
					this.btnEdit.Text = "&Modify";
					this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.btnEdit.ToolTipText = "Modify";
					this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
					// 
					// btnDelete
					// 
					this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
					this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
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
					this.btnSync.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
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
					this.btnExit.Margin = new System.Windows.Forms.Padding(3, 1, 6, 2);
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
					this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
					this.btnHelp.Name = "btnHelp";
					this.btnHelp.Size = new System.Drawing.Size(36, 43);
					this.btnHelp.Text = "He&lp";
					this.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					// 
					// btnAnalyze
					// 
					this.btnAnalyze.Image = ((System.Drawing.Image)(resources.GetObject("btnAnalyze.Image")));
					this.btnAnalyze.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.btnAnalyze.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
					this.btnAnalyze.Name = "btnAnalyze";
					this.btnAnalyze.Size = new System.Drawing.Size(52, 43);
					this.btnAnalyze.Text = "&Analyze";
					this.btnAnalyze.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
					// 
					// msSyncSharp
					// 
					this.msSyncSharp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskMenuItem,
            this.actionMenuItem,
            this.viewMenuItem,
            this.helpMenuItem});
					this.msSyncSharp.Location = new System.Drawing.Point(0, 0);
					this.msSyncSharp.Name = "msSyncSharp";
					this.msSyncSharp.Size = new System.Drawing.Size(632, 24);
					this.msSyncSharp.TabIndex = 1;
					this.msSyncSharp.Text = "menuStrip1";
					// 
					// taskMenuItem
					// 
					this.taskMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.editMenuItem,
            this.deleteMenuItem,
            this.renameMenuItem,
            this.copyMenuItem,
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
					this.newMenuItem.Size = new System.Drawing.Size(184, 22);
					this.newMenuItem.Text = "&New";
					this.newMenuItem.Click += new System.EventHandler(this.newMenuItem_Click);
					// 
					// editMenuItem
					// 
					this.editMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editMenuItem.Image")));
					this.editMenuItem.Name = "editMenuItem";
					this.editMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
					this.editMenuItem.Size = new System.Drawing.Size(184, 22);
					this.editMenuItem.Text = "Modify";
					this.editMenuItem.Click += new System.EventHandler(this.editMenuItem_Click);
					// 
					// deleteMenuItem
					// 
					this.deleteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteMenuItem.Image")));
					this.deleteMenuItem.Name = "deleteMenuItem";
					this.deleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
					this.deleteMenuItem.Size = new System.Drawing.Size(184, 22);
					this.deleteMenuItem.Text = "Delete";
					this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
					// 
					// renameMenuItem
					// 
					this.renameMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("renameMenuItem.Image")));
					this.renameMenuItem.Name = "renameMenuItem";
					this.renameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
					this.renameMenuItem.Size = new System.Drawing.Size(184, 22);
					this.renameMenuItem.Text = "Rename";
					this.renameMenuItem.Click += new System.EventHandler(this.renameMenuItem_Click);
					// 
					// copyMenuItem
					// 
					this.copyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyMenuItem.Image")));
					this.copyMenuItem.Name = "copyMenuItem";
					this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
					this.copyMenuItem.Size = new System.Drawing.Size(184, 22);
					this.copyMenuItem.Text = "Copy";
					this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
					// 
					// toolStripSeparator1
					// 
					this.toolStripSeparator1.Name = "toolStripSeparator1";
					this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
					// 
					// importMenuItem
					// 
					this.importMenuItem.Name = "importMenuItem";
					this.importMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
					this.importMenuItem.Size = new System.Drawing.Size(184, 22);
					this.importMenuItem.Text = "Import Task...";
					this.importMenuItem.Click += new System.EventHandler(this.importMenuItem_Click);
					// 
					// exportMenuItem
					// 
					this.exportMenuItem.Name = "exportMenuItem";
					this.exportMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
					this.exportMenuItem.Size = new System.Drawing.Size(184, 22);
					this.exportMenuItem.Text = "Export Task...";
					this.exportMenuItem.Click += new System.EventHandler(this.exportMenuItem_Click);
					// 
					// actionMenuItem
					// 
					this.actionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeMenuItem,
            this.syncMenuItem,
            this.syncAllMenuItem,
            this.toolStripSeparator2,
            this.openSourceMenuItem,
            this.openTargetMenuItem,
            this.toolStripSeparator3,
            this.viewLogMenuItem});
					this.actionMenuItem.Name = "actionMenuItem";
					this.actionMenuItem.Size = new System.Drawing.Size(54, 20);
					this.actionMenuItem.Text = "&Action";
					// 
					// analyzeMenuItem
					// 
					this.analyzeMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("analyzeMenuItem.Image")));
					this.analyzeMenuItem.Name = "analyzeMenuItem";
					this.analyzeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
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
					// syncAllMenuItem
					// 
					this.syncAllMenuItem.Name = "syncAllMenuItem";
					this.syncAllMenuItem.Size = new System.Drawing.Size(180, 22);
					this.syncAllMenuItem.Text = "Sync All Folder Pairs";
					// 
					// toolStripSeparator2
					// 
					this.toolStripSeparator2.Name = "toolStripSeparator2";
					this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
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
					// toolStripSeparator3
					// 
					this.toolStripSeparator3.Name = "toolStripSeparator3";
					this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
					// 
					// viewLogMenuItem
					// 
					this.viewLogMenuItem.Name = "viewLogMenuItem";
					this.viewLogMenuItem.Size = new System.Drawing.Size(180, 22);
					this.viewLogMenuItem.Text = "View Log";
					// 
					// viewMenuItem
					// 
					this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolBarMenuItem,
            this.showBackupTaskMenuItem,
            this.showSyncTaskMenuItem});
					this.viewMenuItem.Name = "viewMenuItem";
					this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
					this.viewMenuItem.Text = "&View";
					// 
					// hideToolBarMenuItem
					// 
					this.hideToolBarMenuItem.Name = "hideToolBarMenuItem";
					this.hideToolBarMenuItem.Size = new System.Drawing.Size(211, 22);
					this.hideToolBarMenuItem.Text = "&Hide Toolbar";
					// 
					// showBackupTaskMenuItem
					// 
					this.showBackupTaskMenuItem.Name = "showBackupTaskMenuItem";
					this.showBackupTaskMenuItem.Size = new System.Drawing.Size(211, 22);
					this.showBackupTaskMenuItem.Text = "Show Backup Task(s) only";
					// 
					// showSyncTaskMenuItem
					// 
					this.showSyncTaskMenuItem.Name = "showSyncTaskMenuItem";
					this.showSyncTaskMenuItem.Size = new System.Drawing.Size(211, 22);
					this.showSyncTaskMenuItem.Text = "Show Sync Task(s) only";
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
					// 
					// aboutMenuItem
					// 
					this.aboutMenuItem.Name = "aboutMenuItem";
					this.aboutMenuItem.Size = new System.Drawing.Size(118, 22);
					this.aboutMenuItem.Text = "&About...";
					// 
					// ssSyncSharp
					// 
					this.ssSyncSharp.Location = new System.Drawing.Point(0, 372);
					this.ssSyncSharp.Name = "ssSyncSharp";
					this.ssSyncSharp.Size = new System.Drawing.Size(632, 22);
					this.ssSyncSharp.TabIndex = 2;
					this.ssSyncSharp.Text = "statusStrip1";
					// 
					// taskListView
					// 
					this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTask,
            this.colLastRun,
            this.colResult,
            this.colSource,
            this.colTarget});
					this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
					this.taskListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					this.taskListView.FullRowSelect = true;
					this.taskListView.Location = new System.Drawing.Point(0, 70);
					this.taskListView.Name = "taskListView";
					this.taskListView.Size = new System.Drawing.Size(632, 302);
					this.taskListView.TabIndex = 4;
					this.taskListView.UseCompatibleStateImageBehavior = false;
					this.taskListView.View = System.Windows.Forms.View.Details;
					// 
					// colTask
					// 
					this.colTask.Text = "Task";
					this.colTask.Width = 200;
					// 
					// colLastRun
					// 
					this.colLastRun.Text = "Last Run";
					this.colLastRun.Width = 140;
					// 
					// colResult
					// 
					this.colResult.Text = "Result";
					this.colResult.Width = 140;
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
					// MainForm
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(632, 394);
					this.Controls.Add(this.taskListView);
					this.Controls.Add(this.ssSyncSharp);
					this.Controls.Add(this.tsSyncSharp);
					this.Controls.Add(this.msSyncSharp);
					this.MainMenuStrip = this.msSyncSharp;
					this.Name = "MainForm";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "SyncSharp - Just plug it in!";
					this.Load += new System.EventHandler(this.MainForm_Load);
					this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
					this.tsSyncSharp.ResumeLayout(false);
					this.tsSyncSharp.PerformLayout();
					this.msSyncSharp.ResumeLayout(false);
					this.msSyncSharp.PerformLayout();
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsSyncSharp;
        private System.Windows.Forms.MenuStrip msSyncSharp;
        private System.Windows.Forms.StatusStrip ssSyncSharp;
        private System.Windows.Forms.ToolStripMenuItem taskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem actionMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnSync;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem showBackupTaskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSyncTaskMenuItem;
        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.ColumnHeader colTask;
        private System.Windows.Forms.ColumnHeader colLastRun;
        private System.Windows.Forms.ColumnHeader colResult;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colTarget;
    }
}

