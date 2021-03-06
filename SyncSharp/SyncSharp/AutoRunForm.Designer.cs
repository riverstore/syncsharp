﻿namespace SyncSharp.GUI
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
            this.countDownTimer = new System.Windows.Forms.Timer(this.components);
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvTaskList = new System.Windows.Forms.ListView();
            this.colTaskName = new System.Windows.Forms.ColumnHeader();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.colTarget = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // countDownTimer
            // 
            this.countDownTimer.Enabled = true;
            this.countDownTimer.Interval = 1000;
            this.countDownTimer.Tick += new System.EventHandler(this.countDownTimer_Tick);
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
            this.scMain.Panel2.Controls.Add(this.groupBox2);
            this.scMain.Size = new System.Drawing.Size(514, 306);
            this.scMain.SplitterDistance = 400;
            this.scMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvTaskList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 306);
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
            this.lvTaskList.FullRowSelect = true;
            this.lvTaskList.Location = new System.Drawing.Point(3, 17);
            this.lvTaskList.Name = "lvTaskList";
            this.lvTaskList.Size = new System.Drawing.Size(394, 286);
            this.lvTaskList.TabIndex = 0;
            this.lvTaskList.UseCompatibleStateImageBehavior = false;
            this.lvTaskList.View = System.Windows.Forms.View.Details;
            // 
            // colTaskName
            // 
            this.colTaskName.Text = "Task Name";
            this.colTaskName.Width = 120;
            // 
            // colSource
            // 
            this.colSource.Text = "Source";
            this.colSource.Width = 150;
            // 
            // colTarget
            // 
            this.colTarget.Text = "Target";
            this.colTarget.Width = 150;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnDown);
            this.groupBox2.Controls.Add(this.btnUp);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(110, 306);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(6, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Back to Main";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(6, 140);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(96, 25);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(6, 109);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(96, 25);
            this.btnDown.TabIndex = 0;
            this.btnDown.Text = "Move Down";
            this.btnDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(6, 78);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(96, 25);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "Move Up";
            this.btnUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTimer});
            this.statusBar.Location = new System.Drawing.Point(0, 306);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(514, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusBar";
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(499, 17);
            this.lblTimer.Spring = true;
            this.lblTimer.Text = "Performing synchronization in 5 seconds...";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AutoRunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 328);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.statusBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoRunForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoRunForm";
            this.Load += new System.EventHandler(this.AutoRunForm_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer countDownTimer;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ListView lvTaskList;
        private System.Windows.Forms.ColumnHeader colTaskName;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colTarget;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblTimer;
    }
}