namespace SyncSharp.GUI
{
    partial class TaskWizardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskWizardForm));
            this.scContent = new System.Windows.Forms.SplitContainer();
            this.pTaskName = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pSelectType = new System.Windows.Forms.Panel();
            this.radBackup = new System.Windows.Forms.RadioButton();
            this.radSync = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.pSetFolderPair = new System.Windows.Forms.Panel();
            this.btnTarget = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.scContent.Panel1.SuspendLayout();
            this.scContent.Panel2.SuspendLayout();
            this.scContent.SuspendLayout();
            this.pTaskName.SuspendLayout();
            this.pSelectType.SuspendLayout();
            this.pSetFolderPair.SuspendLayout();
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
            this.scContent.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scContent.Panel1.Controls.Add(this.pTaskName);
            this.scContent.Panel1.Controls.Add(this.pSelectType);
            this.scContent.Panel1.Controls.Add(this.pSetFolderPair);
            // 
            // scContent.Panel2
            // 
            this.scContent.Panel2.Controls.Add(this.btnAbort);
            this.scContent.Panel2.Controls.Add(this.btnBack);
            this.scContent.Panel2.Controls.Add(this.btnNext);
            this.scContent.Size = new System.Drawing.Size(512, 346);
            this.scContent.SplitterDistance = 290;
            this.scContent.TabIndex = 0;
            // 
            // pTaskName
            // 
            this.pTaskName.Controls.Add(this.label3);
            this.pTaskName.Controls.Add(this.label2);
            this.pTaskName.Controls.Add(this.txtName);
            this.pTaskName.Controls.Add(this.label1);
            this.pTaskName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTaskName.Location = new System.Drawing.Point(0, 0);
            this.pTaskName.Name = "pTaskName";
            this.pTaskName.Size = new System.Drawing.Size(512, 290);
            this.pTaskName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(404, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "You will be asked a series of questions on what you want the task to do.\r\nTo cont" +
                "inue onto the next step,  press the Next button or simply hit the Enter key.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter a name to describe the task you want to perform. For eg. Sync project work." +
                "";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(33, 136);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(437, 21);
            this.txtName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Step 1: Creating A New Profile";
            // 
            // pSelectType
            // 
            this.pSelectType.Controls.Add(this.radBackup);
            this.pSelectType.Controls.Add(this.radSync);
            this.pSelectType.Controls.Add(this.label5);
            this.pSelectType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSelectType.Location = new System.Drawing.Point(0, 0);
            this.pSelectType.Name = "pSelectType";
            this.pSelectType.Size = new System.Drawing.Size(512, 290);
            this.pSelectType.TabIndex = 4;
            // 
            // radBackup
            // 
            this.radBackup.AutoSize = true;
            this.radBackup.Location = new System.Drawing.Point(36, 110);
            this.radBackup.Name = "radBackup";
            this.radBackup.Size = new System.Drawing.Size(438, 17);
            this.radBackup.TabIndex = 5;
            this.radBackup.TabStop = true;
            this.radBackup.Text = "Backup: To copy files to another directory, drive. To keep a backup copy of your " +
                "files.";
            this.radBackup.UseVisualStyleBackColor = true;
            // 
            // radSync
            // 
            this.radSync.AutoSize = true;
            this.radSync.Location = new System.Drawing.Point(36, 76);
            this.radSync.Name = "radSync";
            this.radSync.Size = new System.Drawing.Size(395, 17);
            this.radSync.TabIndex = 5;
            this.radSync.TabStop = true;
            this.radSync.Text = "Synchronize: To keep two folders contents synchronize in different locations.";
            this.radSync.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(283, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Step 2: What type of task do you want to create?";
            // 
            // pSetFolderPair
            // 
            this.pSetFolderPair.Controls.Add(this.btnTarget);
            this.pSetFolderPair.Controls.Add(this.btnSource);
            this.pSetFolderPair.Controls.Add(this.label6);
            this.pSetFolderPair.Controls.Add(this.label7);
            this.pSetFolderPair.Controls.Add(this.txtTarget);
            this.pSetFolderPair.Controls.Add(this.txtSource);
            this.pSetFolderPair.Controls.Add(this.label4);
            this.pSetFolderPair.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSetFolderPair.Location = new System.Drawing.Point(0, 0);
            this.pSetFolderPair.Name = "pSetFolderPair";
            this.pSetFolderPair.Size = new System.Drawing.Size(512, 290);
            this.pSetFolderPair.TabIndex = 6;
            // 
            // btnTarget
            // 
            this.btnTarget.Image = ((System.Drawing.Image)(resources.GetObject("btnTarget.Image")));
            this.btnTarget.Location = new System.Drawing.Point(421, 136);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(30, 23);
            this.btnTarget.TabIndex = 10;
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // btnSource
            // 
            this.btnSource.Image = ((System.Drawing.Image)(resources.GetObject("btnSource.Image")));
            this.btnSource.Location = new System.Drawing.Point(421, 82);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(30, 23);
            this.btnSource.TabIndex = 11;
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Select Target folder";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Select Source folder";
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(36, 138);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(383, 21);
            this.txtTarget.TabIndex = 6;
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(36, 83);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(383, 21);
            this.txtSource.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(281, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Step 3: Select your source and target directories";
            // 
            // btnAbort
            // 
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAbort.Image = ((System.Drawing.Image)(resources.GetObject("btnAbort.Image")));
            this.btnAbort.Location = new System.Drawing.Point(263, 3);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 0;
            this.btnAbort.Text = "Abort";
            this.btnAbort.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(344, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 0;
            this.btnNext.ImageList = this.imgList;
            this.btnNext.Location = new System.Drawing.Point(425, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "next.png");
            this.imgList.Images.SetKeyName(1, "done.png");
            // 
            // TaskWizardForm
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnAbort;
            this.ClientSize = new System.Drawing.Size(512, 346);
            this.Controls.Add(this.scContent);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TaskWizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskWizard";
            this.scContent.Panel1.ResumeLayout(false);
            this.scContent.Panel2.ResumeLayout(false);
            this.scContent.ResumeLayout(false);
            this.pTaskName.ResumeLayout(false);
            this.pTaskName.PerformLayout();
            this.pSelectType.ResumeLayout(false);
            this.pSelectType.PerformLayout();
            this.pSetFolderPair.ResumeLayout(false);
            this.pSetFolderPair.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scContent;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnBack;
				private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel pSelectType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radBackup;
        private System.Windows.Forms.RadioButton radSync;
        private System.Windows.Forms.Panel pSetFolderPair;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.TextBox txtSource;
				private System.Windows.Forms.Panel pTaskName;
				private System.Windows.Forms.Label label3;
				private System.Windows.Forms.Label label2;
				private System.Windows.Forms.TextBox txtName;
				private System.Windows.Forms.Label label1;
                private System.Windows.Forms.ImageList imgList;
    }
}