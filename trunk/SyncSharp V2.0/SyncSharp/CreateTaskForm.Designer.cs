namespace SyncSharp.GUI
{
    partial class CreateTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTaskForm));
            this.scContent = new System.Windows.Forms.SplitContainer();
            this.grpBEnterST = new System.Windows.Forms.GroupBox();
            this.btnTarget = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.grpBSelType = new System.Windows.Forms.GroupBox();
            this.radBackup = new System.Windows.Forms.RadioButton();
            this.radSync = new System.Windows.Forms.RadioButton();
            this.grpBName = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.scContent.Panel1.SuspendLayout();
            this.scContent.Panel2.SuspendLayout();
            this.scContent.SuspendLayout();
            this.grpBEnterST.SuspendLayout();
            this.grpBSelType.SuspendLayout();
            this.grpBName.SuspendLayout();
            this.SuspendLayout();
            // 
            // scContent
            // 
            this.scContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContent.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scContent.IsSplitterFixed = true;
            this.scContent.Location = new System.Drawing.Point(0, 0);
            this.scContent.Name = "scContent";
            this.scContent.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContent.Panel1
            // 
            this.scContent.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.scContent.Panel1.Controls.Add(this.grpBEnterST);
            this.scContent.Panel1.Controls.Add(this.grpBSelType);
            this.scContent.Panel1.Controls.Add(this.grpBName);
            // 
            // scContent.Panel2
            // 
            this.scContent.Panel2.Controls.Add(this.btnAbort);
            this.scContent.Panel2.Controls.Add(this.btnAccept);
            this.scContent.Size = new System.Drawing.Size(482, 394);
            this.scContent.SplitterDistance = 350;
            this.scContent.TabIndex = 0;
            // 
            // grpBEnterST
            // 
            this.grpBEnterST.Controls.Add(this.btnTarget);
            this.grpBEnterST.Controls.Add(this.btnSource);
            this.grpBEnterST.Controls.Add(this.label6);
            this.grpBEnterST.Controls.Add(this.txtTarget);
            this.grpBEnterST.Controls.Add(this.label7);
            this.grpBEnterST.Controls.Add(this.txtSource);
            this.grpBEnterST.Location = new System.Drawing.Point(12, 12);
            this.grpBEnterST.Name = "grpBEnterST";
            this.grpBEnterST.Size = new System.Drawing.Size(458, 139);
            this.grpBEnterST.TabIndex = 1;
            this.grpBEnterST.TabStop = false;
            this.grpBEnterST.Text = "Enter Source && Target Directories";
            // 
            // btnTarget
            // 
            this.btnTarget.Image = ((System.Drawing.Image)(resources.GetObject("btnTarget.Image")));
            this.btnTarget.Location = new System.Drawing.Point(417, 90);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(30, 23);
            this.btnTarget.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnTarget, "Choose target folder");
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // btnSource
            // 
            this.btnSource.Image = ((System.Drawing.Image)(resources.GetObject("btnSource.Image")));
            this.btnSource.Location = new System.Drawing.Point(417, 41);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(30, 23);
            this.btnSource.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnSource, "Choose source folder");
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Target folder";
            // 
            // txtTarget
            // 
            this.txtTarget.BackColor = System.Drawing.SystemColors.Window;
            this.txtTarget.Location = new System.Drawing.Point(9, 92);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(406, 21);
            this.txtTarget.TabIndex = 4;
            this.toolTip.SetToolTip(this.txtTarget, "Enter your target directory here. You can use environment variables like %HOMEPAT" +
                    "H%.");
            this.txtTarget.TextChanged += new System.EventHandler(this.txtTarget_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Source folder";
            // 
            // txtSource
            // 
            this.txtSource.BackColor = System.Drawing.SystemColors.Window;
            this.txtSource.Location = new System.Drawing.Point(9, 43);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(406, 21);
            this.txtSource.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtSource, "Enter your source directory here. You can use environment variables like %HOMEPAT" +
                    "H%.");
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
            // 
            // grpBSelType
            // 
            this.grpBSelType.Controls.Add(this.radBackup);
            this.grpBSelType.Controls.Add(this.radSync);
            this.grpBSelType.Location = new System.Drawing.Point(12, 157);
            this.grpBSelType.Name = "grpBSelType";
            this.grpBSelType.Size = new System.Drawing.Size(458, 87);
            this.grpBSelType.TabIndex = 6;
            this.grpBSelType.TabStop = false;
            this.grpBSelType.Text = "Select Task Type";
            // 
            // radBackup
            // 
            this.radBackup.AutoSize = true;
            this.radBackup.Location = new System.Drawing.Point(9, 51);
            this.radBackup.Name = "radBackup";
            this.radBackup.Size = new System.Drawing.Size(332, 17);
            this.radBackup.TabIndex = 8;
            this.radBackup.Text = "Backup: To keep a backup copy of your files in another location.";
            this.radBackup.UseVisualStyleBackColor = true;
            // 
            // radSync
            // 
            this.radSync.AutoSize = true;
            this.radSync.Checked = true;
            this.radSync.Location = new System.Drawing.Point(9, 27);
            this.radSync.Name = "radSync";
            this.radSync.Size = new System.Drawing.Size(395, 17);
            this.radSync.TabIndex = 7;
            this.radSync.TabStop = true;
            this.radSync.Text = "Synchronize: To keep two folders contents synchronize in different locations.";
            this.radSync.UseVisualStyleBackColor = true;
            this.radSync.CheckedChanged += new System.EventHandler(this.radSync_CheckedChanged);
            // 
            // grpBName
            // 
            this.grpBName.Controls.Add(this.label2);
            this.grpBName.Controls.Add(this.txtName);
            this.grpBName.Location = new System.Drawing.Point(12, 250);
            this.grpBName.Name = "grpBName";
            this.grpBName.Size = new System.Drawing.Size(458, 82);
            this.grpBName.TabIndex = 9;
            this.grpBName.TabStop = false;
            this.grpBName.Text = "Enter Task Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Enter a name to describe the task you want to perform. For eg. Sync project work." +
                "";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(9, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(435, 21);
            this.txtName.TabIndex = 10;
            // 
            // btnAbort
            // 
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAbort.Image = ((System.Drawing.Image)(resources.GetObject("btnAbort.Image")));
            this.btnAbort.Location = new System.Drawing.Point(395, 3);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 25);
            this.btnAbort.TabIndex = 0;
            this.btnAbort.Text = "Abort";
            this.btnAbort.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.Location = new System.Drawing.Point(314, 3);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 25);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "OK";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // CreateTaskForm
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnAbort;
            this.ClientSize = new System.Drawing.Size(482, 394);
            this.Controls.Add(this.scContent);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New Task";
            this.scContent.Panel1.ResumeLayout(false);
            this.scContent.Panel2.ResumeLayout(false);
            this.scContent.ResumeLayout(false);
            this.grpBEnterST.ResumeLayout(false);
            this.grpBEnterST.PerformLayout();
            this.grpBSelType.ResumeLayout(false);
            this.grpBSelType.PerformLayout();
            this.grpBName.ResumeLayout(false);
            this.grpBName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scContent;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnAccept;
                private System.Windows.Forms.GroupBox grpBSelType;
                private System.Windows.Forms.GroupBox grpBName;
                private System.Windows.Forms.Label label2;
                private System.Windows.Forms.TextBox txtName;
                private System.Windows.Forms.GroupBox grpBEnterST;
                private System.Windows.Forms.RadioButton radBackup;
                private System.Windows.Forms.RadioButton radSync;
                private System.Windows.Forms.TextBox txtTarget;
                private System.Windows.Forms.Label label7;
                private System.Windows.Forms.TextBox txtSource;
                private System.Windows.Forms.Button btnTarget;
                private System.Windows.Forms.Button btnSource;
                private System.Windows.Forms.Label label6;
                private System.Windows.Forms.ToolTip toolTip;
    }
}