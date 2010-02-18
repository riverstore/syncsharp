namespace SyncSharp
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
					this.tsbtnNew = new System.Windows.Forms.ToolStripButton();
					this.tsbtnEdit = new System.Windows.Forms.ToolStripButton();
					this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
					this.separator = new System.Windows.Forms.ToolStripSeparator();
					this.tsbtnSync = new System.Windows.Forms.ToolStripButton();
					this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
					this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
					this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
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
					this.preferenceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.hideToolBarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
					this.mainSplit = new System.Windows.Forms.SplitContainer();
					this.txtLog = new System.Windows.Forms.TextBox();
					this.tsSyncSharp.SuspendLayout();
					this.msSyncSharp.SuspendLayout();
					this.mainSplit.Panel1.SuspendLayout();
					this.mainSplit.Panel2.SuspendLayout();
					this.mainSplit.SuspendLayout();
					this.SuspendLayout();
					// 
					// tsSyncSharp
					// 
					this.tsSyncSharp.ImageScalingSize = new System.Drawing.Size(24, 24);
					this.tsSyncSharp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNew,
            this.tsbtnEdit,
            this.tsbtnDelete,
            this.separator,
            this.tsbtnSync,
            this.tsbtnExit,
            this.tsbtnHelp,
            this.stopToolStripButton});
					this.tsSyncSharp.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
					this.tsSyncSharp.Location = new System.Drawing.Point(0, 24);
					this.tsSyncSharp.Name = "tsSyncSharp";
					this.tsSyncSharp.Size = new System.Drawing.Size(632, 46);
					this.tsSyncSharp.TabIndex = 0;
					this.tsSyncSharp.Text = "toolStrip1";
					// 
					// tsbtnNew
					// 
					this.tsbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNew.Image")));
					this.tsbtnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.tsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.tsbtnNew.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
					this.tsbtnNew.Name = "tsbtnNew";
					this.tsbtnNew.Size = new System.Drawing.Size(35, 43);
					this.tsbtnNew.Text = "&New";
					this.tsbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.tsbtnNew.Click += new System.EventHandler(this.tsbtnNew_Click);
					// 
					// tsbtnEdit
					// 
					this.tsbtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnEdit.Image")));
					this.tsbtnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.tsbtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.tsbtnEdit.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
					this.tsbtnEdit.Name = "tsbtnEdit";
					this.tsbtnEdit.Size = new System.Drawing.Size(49, 43);
					this.tsbtnEdit.Text = "Modify";
					this.tsbtnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.tsbtnEdit.ToolTipText = "Modify";
					// 
					// tsbtnDelete
					// 
					this.tsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDelete.Image")));
					this.tsbtnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.tsbtnDelete.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
					this.tsbtnDelete.Name = "tsbtnDelete";
					this.tsbtnDelete.Size = new System.Drawing.Size(44, 43);
					this.tsbtnDelete.Text = "Delete";
					this.tsbtnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
					// 
					// separator
					// 
					this.separator.Name = "separator";
					this.separator.Size = new System.Drawing.Size(6, 46);
					// 
					// tsbtnSync
					// 
					this.tsbtnSync.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSync.Image")));
					this.tsbtnSync.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.tsbtnSync.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.tsbtnSync.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
					this.tsbtnSync.Name = "tsbtnSync";
					this.tsbtnSync.Size = new System.Drawing.Size(75, 43);
					this.tsbtnSync.Text = "&Synchronize";
					this.tsbtnSync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.tsbtnSync.Click += new System.EventHandler(this.tsbtnSync_Click);
					// 
					// tsbtnExit
					// 
					this.tsbtnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
					this.tsbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExit.Image")));
					this.tsbtnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.tsbtnExit.Margin = new System.Windows.Forms.Padding(4, 1, 6, 2);
					this.tsbtnExit.Name = "tsbtnExit";
					this.tsbtnExit.Size = new System.Drawing.Size(29, 43);
					this.tsbtnExit.Text = "E&xit";
					this.tsbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click);
					// 
					// tsbtnHelp
					// 
					this.tsbtnHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
					this.tsbtnHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnHelp.Image")));
					this.tsbtnHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.tsbtnHelp.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
					this.tsbtnHelp.Name = "tsbtnHelp";
					this.tsbtnHelp.Size = new System.Drawing.Size(36, 43);
					this.tsbtnHelp.Text = "He&lp";
					this.tsbtnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					// 
					// stopToolStripButton
					// 
					this.stopToolStripButton.Enabled = false;
					this.stopToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripButton.Image")));
					this.stopToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
					this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
					this.stopToolStripButton.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
					this.stopToolStripButton.Name = "stopToolStripButton";
					this.stopToolStripButton.Size = new System.Drawing.Size(35, 43);
					this.stopToolStripButton.Text = "Stop";
					this.stopToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					// 
					// msSyncSharp
					// 
					this.msSyncSharp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskMenuItem,
            this.actionMenuItem,
            this.preferenceMenuItem,
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
					// 
					// renameMenuItem
					// 
					this.renameMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("renameMenuItem.Image")));
					this.renameMenuItem.Name = "renameMenuItem";
					this.renameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
					this.renameMenuItem.Size = new System.Drawing.Size(184, 22);
					this.renameMenuItem.Text = "Rename";
					// 
					// copyMenuItem
					// 
					this.copyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyMenuItem.Image")));
					this.copyMenuItem.Name = "copyMenuItem";
					this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
					this.copyMenuItem.Size = new System.Drawing.Size(184, 22);
					this.copyMenuItem.Text = "Copy";
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
					// 
					// exportMenuItem
					// 
					this.exportMenuItem.Name = "exportMenuItem";
					this.exportMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
					this.exportMenuItem.Size = new System.Drawing.Size(184, 22);
					this.exportMenuItem.Text = "Export Task...";
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
					this.analyzeMenuItem.Size = new System.Drawing.Size(180, 22);
					this.analyzeMenuItem.Text = "Analyze";
					this.analyzeMenuItem.Click += new System.EventHandler(this.analyzeMenuItem_Click);
					// 
					// syncMenuItem
					// 
					this.syncMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("syncMenuItem.Image")));
					this.syncMenuItem.Name = "syncMenuItem";
					this.syncMenuItem.Size = new System.Drawing.Size(180, 22);
					this.syncMenuItem.Text = "Synchronize";
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
					// 
					// openTargetMenuItem
					// 
					this.openTargetMenuItem.Name = "openTargetMenuItem";
					this.openTargetMenuItem.Size = new System.Drawing.Size(180, 22);
					this.openTargetMenuItem.Text = "Open Target Folder";
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
					// preferenceMenuItem
					// 
					this.preferenceMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolBarMenuItem,
            this.optionsMenuItem});
					this.preferenceMenuItem.Name = "preferenceMenuItem";
					this.preferenceMenuItem.Size = new System.Drawing.Size(80, 20);
					this.preferenceMenuItem.Text = "&Preferences";
					// 
					// hideToolBarMenuItem
					// 
					this.hideToolBarMenuItem.Name = "hideToolBarMenuItem";
					this.hideToolBarMenuItem.Size = new System.Drawing.Size(143, 22);
					this.hideToolBarMenuItem.Text = "&Hide Toolbar";
					// 
					// optionsMenuItem
					// 
					this.optionsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsMenuItem.Image")));
					this.optionsMenuItem.Name = "optionsMenuItem";
					this.optionsMenuItem.Size = new System.Drawing.Size(143, 22);
					this.optionsMenuItem.Text = "&Options...";
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
					this.ssSyncSharp.Location = new System.Drawing.Point(0, 384);
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
					this.taskListView.Location = new System.Drawing.Point(0, 0);
					this.taskListView.Name = "taskListView";
					this.taskListView.Size = new System.Drawing.Size(632, 188);
					this.taskListView.TabIndex = 3;
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
					// mainSplit
					// 
					this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
					this.mainSplit.Location = new System.Drawing.Point(0, 70);
					this.mainSplit.Name = "mainSplit";
					this.mainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
					// 
					// mainSplit.Panel1
					// 
					this.mainSplit.Panel1.Controls.Add(this.taskListView);
					// 
					// mainSplit.Panel2
					// 
					this.mainSplit.Panel2.Controls.Add(this.txtLog);
					this.mainSplit.Size = new System.Drawing.Size(632, 314);
					this.mainSplit.SplitterDistance = 188;
					this.mainSplit.TabIndex = 4;
					// 
					// txtLog
					// 
					this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
					this.txtLog.Location = new System.Drawing.Point(0, 0);
					this.txtLog.Multiline = true;
					this.txtLog.Name = "txtLog";
					this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
					this.txtLog.Size = new System.Drawing.Size(632, 122);
					this.txtLog.TabIndex = 0;
					// 
					// MainForm
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(632, 406);
					this.Controls.Add(this.mainSplit);
					this.Controls.Add(this.ssSyncSharp);
					this.Controls.Add(this.tsSyncSharp);
					this.Controls.Add(this.msSyncSharp);
					this.MainMenuStrip = this.msSyncSharp;
					this.Name = "MainForm";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "SyncSharp - Just plug and sync";
					this.Load += new System.EventHandler(this.MainForm_Load);
					this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
					this.tsSyncSharp.ResumeLayout(false);
					this.tsSyncSharp.PerformLayout();
					this.msSyncSharp.ResumeLayout(false);
					this.msSyncSharp.PerformLayout();
					this.mainSplit.Panel1.ResumeLayout(false);
					this.mainSplit.Panel2.ResumeLayout(false);
					this.mainSplit.Panel2.PerformLayout();
					this.mainSplit.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem preferenceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.ColumnHeader colTask;
        private System.Windows.Forms.ColumnHeader colLastRun;
        private System.Windows.Forms.ColumnHeader colResult;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colTarget;
        private System.Windows.Forms.ToolStripButton tsbtnNew;
        private System.Windows.Forms.ToolStripButton tsbtnEdit;
        private System.Windows.Forms.ToolStripButton tsbtnSync;
        private System.Windows.Forms.ToolStripButton tsbtnHelp;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.ToolStripSeparator separator;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.SplitContainer mainSplit;
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
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ToolStripMenuItem analyzeMenuItem;
    }
}

