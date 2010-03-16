namespace SyncSharp.GUI
{
    partial class AutoRunForm
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoRunForm));
					this.scMain = new System.Windows.Forms.SplitContainer();
					this.groupBox1 = new System.Windows.Forms.GroupBox();
					this.lvTaskList = new System.Windows.Forms.ListView();
					this.colTaskName = new System.Windows.Forms.ColumnHeader();
					this.colSource = new System.Windows.Forms.ColumnHeader();
					this.colTarget = new System.Windows.Forms.ColumnHeader();
					this.btnDown = new System.Windows.Forms.Button();
					this.btnRemove = new System.Windows.Forms.Button();
					this.btnCancel = new System.Windows.Forms.Button();
					this.btnUp = new System.Windows.Forms.Button();
					this.statusBar = new System.Windows.Forms.StatusStrip();
					this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
					this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
					this.toolTip = new System.Windows.Forms.ToolTip(this.components);
					this.scMain.Panel1.SuspendLayout();
					this.scMain.Panel2.SuspendLayout();
					this.scMain.SuspendLayout();
					this.groupBox1.SuspendLayout();
					this.statusBar.SuspendLayout();
					this.SuspendLayout();
					// 
					// scMain
					// 
					this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
					this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
					this.scMain.IsSplitterFixed = true;
					this.scMain.Location = new System.Drawing.Point(0, 0);
					this.scMain.Name = "scMain";
					// 
					// scMain.Panel1
					// 
					this.scMain.Panel1.Controls.Add(this.groupBox1);
					// 
					// scMain.Panel2
					// 
					this.scMain.Panel2.Controls.Add(this.btnDown);
					this.scMain.Panel2.Controls.Add(this.btnRemove);
					this.scMain.Panel2.Controls.Add(this.btnCancel);
					this.scMain.Panel2.Controls.Add(this.btnUp);
					this.scMain.Size = new System.Drawing.Size(524, 302);
					this.scMain.SplitterDistance = 477;
					this.scMain.SplitterWidth = 2;
					this.scMain.TabIndex = 0;
					// 
					// groupBox1
					// 
					this.groupBox1.Controls.Add(this.lvTaskList);
					this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
					this.groupBox1.Location = new System.Drawing.Point(0, 0);
					this.groupBox1.Name = "groupBox1";
					this.groupBox1.Size = new System.Drawing.Size(477, 302);
					this.groupBox1.TabIndex = 0;
					this.groupBox1.TabStop = false;
					this.groupBox1.Text = "Task List";
					// 
					// lvTaskList
					// 
					this.lvTaskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTaskName,
            this.colSource,
            this.colTarget});
					this.lvTaskList.Dock = System.Windows.Forms.DockStyle.Fill;
					this.lvTaskList.ForeColor = System.Drawing.SystemColors.WindowText;
					this.lvTaskList.FullRowSelect = true;
					this.lvTaskList.Location = new System.Drawing.Point(3, 17);
					this.lvTaskList.MultiSelect = false;
					this.lvTaskList.Name = "lvTaskList";
					this.lvTaskList.Size = new System.Drawing.Size(471, 282);
					this.lvTaskList.TabIndex = 0;
					this.lvTaskList.UseCompatibleStateImageBehavior = false;
					this.lvTaskList.View = System.Windows.Forms.View.Details;
					// 
					// colTaskName
					// 
					this.colTaskName.Text = "Task Name";
					this.colTaskName.Width = 130;
					// 
					// colSource
					// 
					this.colSource.Text = "Source";
					this.colSource.Width = 165;
					// 
					// colTarget
					// 
					this.colTarget.Text = "Target";
					this.colTarget.Width = 165;
					// 
					// btnDown
					// 
					this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
					this.btnDown.Location = new System.Drawing.Point(1, 102);
					this.btnDown.Name = "btnDown";
					this.btnDown.Size = new System.Drawing.Size(39, 25);
					this.btnDown.TabIndex = 0;
					this.btnDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.toolTip.SetToolTip(this.btnDown, "Move down");
					this.btnDown.UseVisualStyleBackColor = true;
					this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
					// 
					// btnRemove
					// 
					this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
					this.btnRemove.Location = new System.Drawing.Point(1, 133);
					this.btnRemove.Name = "btnRemove";
					this.btnRemove.Size = new System.Drawing.Size(39, 25);
					this.btnRemove.TabIndex = 0;
					this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.toolTip.SetToolTip(this.btnRemove, "Remove item");
					this.btnRemove.UseVisualStyleBackColor = true;
					this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
					// 
					// btnCancel
					// 
					this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
					this.btnCancel.Location = new System.Drawing.Point(1, 17);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.Size = new System.Drawing.Size(39, 25);
					this.btnCancel.TabIndex = 0;
					this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.toolTip.SetToolTip(this.btnCancel, "Return to main window");
					this.btnCancel.UseVisualStyleBackColor = true;
					this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
					// 
					// btnUp
					// 
					this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
					this.btnUp.Location = new System.Drawing.Point(1, 71);
					this.btnUp.Name = "btnUp";
					this.btnUp.Size = new System.Drawing.Size(39, 25);
					this.btnUp.TabIndex = 0;
					this.btnUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.toolTip.SetToolTip(this.btnUp, "Move up");
					this.btnUp.UseVisualStyleBackColor = true;
					this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
					// 
					// statusBar
					// 
					this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
					this.statusBar.Location = new System.Drawing.Point(0, 302);
					this.statusBar.Name = "statusBar";
					this.statusBar.Size = new System.Drawing.Size(524, 22);
					this.statusBar.SizingGrip = false;
					this.statusBar.TabIndex = 1;
					this.statusBar.Text = "statusBar";
					// 
					// lblStatus
					// 
					this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F);
					this.lblStatus.Name = "lblStatus";
					this.lblStatus.Size = new System.Drawing.Size(509, 17);
					this.lblStatus.Spring = true;
					this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
					// 
					// progressBar
					// 
					this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
					this.progressBar.MarqueeAnimationSpeed = 20;
					this.progressBar.Name = "progressBar";
					this.progressBar.Size = new System.Drawing.Size(100, 16);
					this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
					this.progressBar.Visible = false;
					// 
					// AutoRunForm
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(524, 324);
					this.Controls.Add(this.scMain);
					this.Controls.Add(this.statusBar);
					this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.MaximizeBox = false;
					this.MinimizeBox = false;
					this.Name = "AutoRunForm";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "SyncSharp PlugSync";
					this.Load += new System.EventHandler(this.AutoRunForm_Load);
					this.scMain.Panel1.ResumeLayout(false);
					this.scMain.Panel2.ResumeLayout(false);
					this.scMain.ResumeLayout(false);
					this.groupBox1.ResumeLayout(false);
					this.statusBar.ResumeLayout(false);
					this.statusBar.PerformLayout();
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ListView lvTaskList;
        private System.Windows.Forms.ColumnHeader colTaskName;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colTarget;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
    }
}