namespace SyncSharp.GUI
{
    partial class TaskSetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskSetupForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tcTaskSetup = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radBackup = new System.Windows.Forms.RadioButton();
            this.radSync = new System.Windows.Forms.RadioButton();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lblChange = new System.Windows.Forms.LinkLabel();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblFileExclude = new System.Windows.Forms.Label();
            this.lblFileInclude = new System.Windows.Forms.Label();
            this.lblLastRun = new System.Windows.Forms.Label();
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblTaskName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.tpFolderPair = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btnTarget = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.tpFilter = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInclude = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtExclude = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkSkipSystem = new System.Windows.Forms.CheckBox();
            this.chkSkipTemp = new System.Windows.Forms.CheckBox();
            this.chkSkipRO = new System.Windows.Forms.CheckBox();
            this.chkSkipHidden = new System.Windows.Forms.CheckBox();
            this.btnSubFolders = new System.Windows.Forms.Button();
            this.tpCopyDel = new System.Windows.Forms.TabPage();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.chkConfirm = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSafe = new System.Windows.Forms.CheckBox();
            this.chkMove = new System.Windows.Forms.CheckBox();
            this.chkVerify = new System.Windows.Forms.CheckBox();
            this.tpLogSettings = new System.Windows.Forms.TabPage();
            this.btnDeleteLog = new System.Windows.Forms.Button();
            this.chkError = new System.Windows.Forms.CheckBox();
            this.chkFailed = new System.Windows.Forms.CheckBox();
            this.chkDrive = new System.Windows.Forms.CheckBox();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.chkReason = new System.Windows.Forms.CheckBox();
            this.chkDisplay = new System.Windows.Forms.CheckBox();
            this.tcAdvanced = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ndTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAutoSync = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radTargetSrc = new System.Windows.Forms.RadioButton();
            this.radSrcTarget = new System.Windows.Forms.RadioButton();
            this.radKeepBoth = new System.Windows.Forms.RadioButton();
            this.radNewOld = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radToSource = new System.Windows.Forms.RadioButton();
            this.radDelTarget = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radDelSource = new System.Windows.Forms.RadioButton();
            this.radToTarget = new System.Windows.Forms.RadioButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radRenameSrc = new System.Windows.Forms.RadioButton();
            this.radRenameTgt = new System.Windows.Forms.RadioButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tcTaskSetup.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tpFolderPair.SuspendLayout();
            this.tpFilter.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tpCopyDel.SuspendLayout();
            this.tpLogSettings.SuspendLayout();
            this.tcAdvanced.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndTime)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tcTaskSetup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnApply);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnAccept);
            this.splitContainer1.Size = new System.Drawing.Size(472, 466);
            this.splitContainer1.SplitterDistance = 412;
            this.splitContainer1.TabIndex = 0;
            // 
            // tcTaskSetup
            // 
            this.tcTaskSetup.Controls.Add(this.tpGeneral);
            this.tcTaskSetup.Controls.Add(this.tpFolderPair);
            this.tcTaskSetup.Controls.Add(this.tpFilter);
            this.tcTaskSetup.Controls.Add(this.tpCopyDel);
            this.tcTaskSetup.Controls.Add(this.tpLogSettings);
            this.tcTaskSetup.Controls.Add(this.tcAdvanced);
            this.tcTaskSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTaskSetup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcTaskSetup.ImageList = this.imageList;
            this.tcTaskSetup.ItemSize = new System.Drawing.Size(115, 20);
            this.tcTaskSetup.Location = new System.Drawing.Point(0, 0);
            this.tcTaskSetup.Multiline = true;
            this.tcTaskSetup.Name = "tcTaskSetup";
            this.tcTaskSetup.SelectedIndex = 0;
            this.tcTaskSetup.Size = new System.Drawing.Size(472, 412);
            this.tcTaskSetup.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tcTaskSetup.TabIndex = 0;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.groupBox5);
            this.tpGeneral.Controls.Add(this.groupBox9);
            this.tpGeneral.ImageIndex = 6;
            this.tpGeneral.Location = new System.Drawing.Point(4, 44);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(464, 364);
            this.tpGeneral.TabIndex = 7;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radBackup);
            this.groupBox5.Controls.Add(this.radSync);
            this.groupBox5.Location = new System.Drawing.Point(8, 196);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(448, 87);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Current Task Type";
            // 
            // radBackup
            // 
            this.radBackup.AutoSize = true;
            this.radBackup.Location = new System.Drawing.Point(9, 51);
            this.radBackup.Name = "radBackup";
            this.radBackup.Size = new System.Drawing.Size(258, 17);
            this.radBackup.TabIndex = 7;
            this.radBackup.Text = "Backup contents in source folder to target folder";
            this.radBackup.UseVisualStyleBackColor = true;
            // 
            // radSync
            // 
            this.radSync.AutoSize = true;
            this.radSync.Checked = true;
            this.radSync.Location = new System.Drawing.Point(9, 27);
            this.radSync.Name = "radSync";
            this.radSync.Size = new System.Drawing.Size(253, 17);
            this.radSync.TabIndex = 6;
            this.radSync.TabStop = true;
            this.radSync.Text = "Synchronize source and target folders contents";
            this.radSync.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.lblChange);
            this.groupBox9.Controls.Add(this.label21);
            this.groupBox9.Controls.Add(this.label19);
            this.groupBox9.Controls.Add(this.label16);
            this.groupBox9.Controls.Add(this.label13);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Controls.Add(this.lblFileExclude);
            this.groupBox9.Controls.Add(this.lblFileInclude);
            this.groupBox9.Controls.Add(this.lblLastRun);
            this.groupBox9.Controls.Add(this.lblTarget);
            this.groupBox9.Controls.Add(this.lblTaskName);
            this.groupBox9.Controls.Add(this.label11);
            this.groupBox9.Controls.Add(this.lblSource);
            this.groupBox9.Location = new System.Drawing.Point(8, 13);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(448, 177);
            this.groupBox9.TabIndex = 10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Task Information";
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Location = new System.Drawing.Point(359, 22);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(83, 13);
            this.lblChange.TabIndex = 2;
            this.lblChange.TabStop = true;
            this.lblChange.Text = "Rename Task...";
            this.lblChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblChange_LinkClicked);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 145);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Files exclusion:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 119);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Files inclusion:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 93);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Last run time:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Target folder:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Task Name:";
            // 
            // lblFileExclude
            // 
            this.lblFileExclude.AutoSize = true;
            this.lblFileExclude.Location = new System.Drawing.Point(87, 145);
            this.lblFileExclude.Name = "lblFileExclude";
            this.lblFileExclude.Size = new System.Drawing.Size(106, 13);
            this.lblFileExclude.TabIndex = 1;
            this.lblFileExclude.Text = "*.tmp *.txt impt.doc";
            // 
            // lblFileInclude
            // 
            this.lblFileInclude.AutoSize = true;
            this.lblFileInclude.Location = new System.Drawing.Point(87, 119);
            this.lblFileInclude.Name = "lblFileInclude";
            this.lblFileInclude.Size = new System.Drawing.Size(23, 13);
            this.lblFileInclude.TabIndex = 1;
            this.lblFileInclude.Text = "*.*";
            // 
            // lblLastRun
            // 
            this.lblLastRun.AutoSize = true;
            this.lblLastRun.Location = new System.Drawing.Point(87, 93);
            this.lblLastRun.Name = "lblLastRun";
            this.lblLastRun.Size = new System.Drawing.Size(127, 13);
            this.lblLastRun.TabIndex = 1;
            this.lblLastRun.Text = "29/02/2010 11:23:10 PM";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoEllipsis = true;
            this.lblTarget.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarget.Location = new System.Drawing.Point(87, 68);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(355, 13);
            this.lblTarget.TabIndex = 1;
            this.lblTarget.Text = "C:\\Document && Settings\\users\\Desktop\\NUS\\Test2\\\r\n";
            // 
            // lblTaskName
            // 
            this.lblTaskName.AutoEllipsis = true;
            this.lblTaskName.Location = new System.Drawing.Point(87, 22);
            this.lblTaskName.Name = "lblTaskName";
            this.lblTaskName.Size = new System.Drawing.Size(266, 13);
            this.lblTaskName.TabIndex = 1;
            this.lblTaskName.Text = "NUS Year3 Sem 2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Source folder:";
            // 
            // lblSource
            // 
            this.lblSource.AutoEllipsis = true;
            this.lblSource.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSource.Location = new System.Drawing.Point(87, 45);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(355, 13);
            this.lblSource.TabIndex = 1;
            this.lblSource.Text = "C:\\Document && Settings\\users\\Desktop\\NUS\\Test1\\";
            // 
            // tpFolderPair
            // 
            this.tpFolderPair.Controls.Add(this.btnDelete);
            this.tpFolderPair.Controls.Add(this.btnSwitch);
            this.tpFolderPair.Controls.Add(this.btnTarget);
            this.tpFolderPair.Controls.Add(this.btnSource);
            this.tpFolderPair.Controls.Add(this.label3);
            this.tpFolderPair.Controls.Add(this.label2);
            this.tpFolderPair.Controls.Add(this.txtTarget);
            this.tpFolderPair.Controls.Add(this.txtSource);
            this.tpFolderPair.ImageIndex = 1;
            this.tpFolderPair.Location = new System.Drawing.Point(4, 24);
            this.tpFolderPair.Name = "tpFolderPair";
            this.tpFolderPair.Padding = new System.Windows.Forms.Padding(3);
            this.tpFolderPair.Size = new System.Drawing.Size(464, 384);
            this.tpFolderPair.TabIndex = 0;
            this.tpFolderPair.Text = "Folder Pair";
            this.tpFolderPair.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(146, 89);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete pair";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitch.Image")));
            this.btnSwitch.Location = new System.Drawing.Point(53, 89);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(87, 25);
            this.btnSwitch.TabIndex = 3;
            this.btnSwitch.Text = "Switch pair";
            this.btnSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSwitch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btnTarget
            // 
            this.btnTarget.Image = ((System.Drawing.Image)(resources.GetObject("btnTarget.Image")));
            this.btnTarget.Location = new System.Drawing.Point(409, 51);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(30, 23);
            this.btnTarget.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnTarget, "Choose target folder");
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // btnSource
            // 
            this.btnSource.Image = ((System.Drawing.Image)(resources.GetObject("btnSource.Image")));
            this.btnSource.Location = new System.Drawing.Point(409, 17);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(30, 23);
            this.btnSource.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnSource, "Choose source folder");
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Target";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source";
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(53, 52);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(356, 21);
            this.txtTarget.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtTarget, "Enter your target directory here. You can use environment variables like %HOMEPAT" +
                    "H%.");
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(53, 18);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(356, 21);
            this.txtSource.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtSource, "Enter your source directory here. You can use environment variables like %HOMEPAT" +
                    "H%.");
            // 
            // tpFilter
            // 
            this.tpFilter.Controls.Add(this.groupBox4);
            this.tpFilter.Controls.Add(this.groupBox7);
            this.tpFilter.Controls.Add(this.btnSubFolders);
            this.tpFilter.ImageIndex = 4;
            this.tpFilter.Location = new System.Drawing.Point(4, 24);
            this.tpFilter.Name = "tpFilter";
            this.tpFilter.Size = new System.Drawing.Size(464, 384);
            this.tpFilter.TabIndex = 5;
            this.tpFilter.Text = "Filters";
            this.tpFilter.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtInclude);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtExclude);
            this.groupBox4.Location = new System.Drawing.Point(8, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(448, 123);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Files Inclusion/Exclusion";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Files to include: eg. *.doc; *.txt; *.*";
            // 
            // txtInclude
            // 
            this.txtInclude.Location = new System.Drawing.Point(6, 85);
            this.txtInclude.Name = "txtInclude";
            this.txtInclude.Size = new System.Drawing.Size(437, 21);
            this.txtInclude.TabIndex = 2;
            this.txtInclude.Text = "*.*";
            this.toolTip.SetToolTip(this.txtInclude, "Enter mask eg. *.doc or filename.\r\nSeparate each pattern by a space.");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Files to exclude: eg. *.exe; filename";
            // 
            // txtExclude
            // 
            this.txtExclude.Location = new System.Drawing.Point(6, 36);
            this.txtExclude.Name = "txtExclude";
            this.txtExclude.Size = new System.Drawing.Size(437, 21);
            this.txtExclude.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtExclude, "Enter mask eg. *.doc or filename.\r\nSeparate each pattern by a space.");
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkSkipSystem);
            this.groupBox7.Controls.Add(this.chkSkipTemp);
            this.groupBox7.Controls.Add(this.chkSkipRO);
            this.groupBox7.Controls.Add(this.chkSkipHidden);
            this.groupBox7.Location = new System.Drawing.Point(8, 142);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(448, 77);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Exclude files with";
            // 
            // chkSkipSystem
            // 
            this.chkSkipSystem.AutoSize = true;
            this.chkSkipSystem.Checked = true;
            this.chkSkipSystem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSkipSystem.Location = new System.Drawing.Point(6, 43);
            this.chkSkipSystem.Name = "chkSkipSystem";
            this.chkSkipSystem.Size = new System.Drawing.Size(105, 17);
            this.chkSkipSystem.TabIndex = 1;
            this.chkSkipSystem.Text = "system attribute";
            this.chkSkipSystem.UseVisualStyleBackColor = true;
            // 
            // chkSkipTemp
            // 
            this.chkSkipTemp.AutoSize = true;
            this.chkSkipTemp.Checked = true;
            this.chkSkipTemp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSkipTemp.Location = new System.Drawing.Point(197, 43);
            this.chkSkipTemp.Name = "chkSkipTemp";
            this.chkSkipTemp.Size = new System.Drawing.Size(121, 17);
            this.chkSkipTemp.TabIndex = 1;
            this.chkSkipTemp.Text = "temporary attribute";
            this.chkSkipTemp.UseVisualStyleBackColor = true;
            // 
            // chkSkipRO
            // 
            this.chkSkipRO.AutoSize = true;
            this.chkSkipRO.Location = new System.Drawing.Point(197, 20);
            this.chkSkipRO.Name = "chkSkipRO";
            this.chkSkipRO.Size = new System.Drawing.Size(117, 17);
            this.chkSkipRO.TabIndex = 1;
            this.chkSkipRO.Text = "read-only attribute";
            this.chkSkipRO.UseVisualStyleBackColor = true;
            // 
            // chkSkipHidden
            // 
            this.chkSkipHidden.AutoSize = true;
            this.chkSkipHidden.Location = new System.Drawing.Point(6, 20);
            this.chkSkipHidden.Name = "chkSkipHidden";
            this.chkSkipHidden.Size = new System.Drawing.Size(103, 17);
            this.chkSkipHidden.TabIndex = 1;
            this.chkSkipHidden.Text = "hidden attribute";
            this.chkSkipHidden.UseVisualStyleBackColor = true;
            // 
            // btnSubFolders
            // 
            this.btnSubFolders.Image = ((System.Drawing.Image)(resources.GetObject("btnSubFolders.Image")));
            this.btnSubFolders.Location = new System.Drawing.Point(8, 235);
            this.btnSubFolders.Name = "btnSubFolders";
            this.btnSubFolders.Size = new System.Drawing.Size(150, 25);
            this.btnSubFolders.TabIndex = 7;
            this.btnSubFolders.Text = "Select sub folders...";
            this.btnSubFolders.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSubFolders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.btnSubFolders, "Filter by sub directories.");
            this.btnSubFolders.UseVisualStyleBackColor = true;
            this.btnSubFolders.Click += new System.EventHandler(this.btnSubFolders_Click);
            // 
            // tpCopyDel
            // 
            this.tpCopyDel.Controls.Add(this.chkReset);
            this.tpCopyDel.Controls.Add(this.chkConfirm);
            this.tpCopyDel.Controls.Add(this.label1);
            this.tpCopyDel.Controls.Add(this.chkSafe);
            this.tpCopyDel.Controls.Add(this.chkMove);
            this.tpCopyDel.Controls.Add(this.chkVerify);
            this.tpCopyDel.ImageIndex = 2;
            this.tpCopyDel.Location = new System.Drawing.Point(4, 44);
            this.tpCopyDel.Name = "tpCopyDel";
            this.tpCopyDel.Size = new System.Drawing.Size(464, 364);
            this.tpCopyDel.TabIndex = 3;
            this.tpCopyDel.Text = "Copy/Delete";
            this.tpCopyDel.UseVisualStyleBackColor = true;
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Location = new System.Drawing.Point(8, 132);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(349, 17);
            this.chkReset.TabIndex = 7;
            this.chkReset.Text = "Reset the archive file attribute on files once they have been copied";
            this.chkReset.UseVisualStyleBackColor = true;
            // 
            // chkConfirm
            // 
            this.chkConfirm.AutoSize = true;
            this.chkConfirm.Location = new System.Drawing.Point(8, 63);
            this.chkConfirm.Name = "chkConfirm";
            this.chkConfirm.Size = new System.Drawing.Size(410, 17);
            this.chkConfirm.TabIndex = 6;
            this.chkConfirm.Text = "Confirm file deletions, replacing files, etc. (Prompt is disabled if recycle bin " +
                "used)";
            this.chkConfirm.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Note: Windows shell will be used for all copying and deleting operations.";
            // 
            // chkSafe
            // 
            this.chkSafe.AutoSize = true;
            this.chkSafe.Location = new System.Drawing.Point(8, 109);
            this.chkSafe.Name = "chkSafe";
            this.chkSafe.Size = new System.Drawing.Size(357, 17);
            this.chkSafe.TabIndex = 3;
            this.chkSafe.Text = "Make safe copies (protect overwriting file until file replication is done)";
            this.chkSafe.UseVisualStyleBackColor = true;
            // 
            // chkMove
            // 
            this.chkMove.AutoSize = true;
            this.chkMove.Location = new System.Drawing.Point(8, 40);
            this.chkMove.Name = "chkMove";
            this.chkMove.Size = new System.Drawing.Size(180, 17);
            this.chkMove.TabIndex = 5;
            this.chkMove.Text = "Move deleted files to recycle bin";
            this.chkMove.UseVisualStyleBackColor = true;
            // 
            // chkVerify
            // 
            this.chkVerify.AutoSize = true;
            this.chkVerify.Location = new System.Drawing.Point(8, 86);
            this.chkVerify.Name = "chkVerify";
            this.chkVerify.Size = new System.Drawing.Size(197, 17);
            this.chkVerify.TabIndex = 0;
            this.chkVerify.Text = "Verify that files are copied correctly";
            this.chkVerify.UseVisualStyleBackColor = true;
            // 
            // tpLogSettings
            // 
            this.tpLogSettings.Controls.Add(this.btnDeleteLog);
            this.tpLogSettings.Controls.Add(this.chkError);
            this.tpLogSettings.Controls.Add(this.chkFailed);
            this.tpLogSettings.Controls.Add(this.chkDrive);
            this.tpLogSettings.Controls.Add(this.chkFilter);
            this.tpLogSettings.Controls.Add(this.chkReason);
            this.tpLogSettings.Controls.Add(this.chkDisplay);
            this.tpLogSettings.ImageIndex = 3;
            this.tpLogSettings.Location = new System.Drawing.Point(4, 44);
            this.tpLogSettings.Name = "tpLogSettings";
            this.tpLogSettings.Size = new System.Drawing.Size(464, 364);
            this.tpLogSettings.TabIndex = 4;
            this.tpLogSettings.Text = "Log Settings";
            this.tpLogSettings.UseVisualStyleBackColor = true;
            // 
            // btnDeleteLog
            // 
            this.btnDeleteLog.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteLog.Image")));
            this.btnDeleteLog.Location = new System.Drawing.Point(8, 162);
            this.btnDeleteLog.Name = "btnDeleteLog";
            this.btnDeleteLog.Size = new System.Drawing.Size(111, 25);
            this.btnDeleteLog.TabIndex = 4;
            this.btnDeleteLog.Text = "Delete Log files";
            this.btnDeleteLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteLog.UseVisualStyleBackColor = true;
            // 
            // chkError
            // 
            this.chkError.AutoSize = true;
            this.chkError.Checked = true;
            this.chkError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkError.Location = new System.Drawing.Point(23, 36);
            this.chkError.Name = "chkError";
            this.chkError.Size = new System.Drawing.Size(138, 17);
            this.chkError.TabIndex = 1;
            this.chkError.Text = "Only when errors occur";
            this.chkError.UseVisualStyleBackColor = true;
            // 
            // chkFailed
            // 
            this.chkFailed.AutoSize = true;
            this.chkFailed.Location = new System.Drawing.Point(8, 127);
            this.chkFailed.Name = "chkFailed";
            this.chkFailed.Size = new System.Drawing.Size(208, 17);
            this.chkFailed.TabIndex = 0;
            this.chkFailed.Text = "Log failed copying/deleting operations";
            this.chkFailed.UseVisualStyleBackColor = true;
            // 
            // chkDrive
            // 
            this.chkDrive.AutoSize = true;
            this.chkDrive.Location = new System.Drawing.Point(8, 104);
            this.chkDrive.Name = "chkDrive";
            this.chkDrive.Size = new System.Drawing.Size(161, 17);
            this.chkDrive.TabIndex = 0;
            this.chkDrive.Text = "Log the drive serial numbers";
            this.chkDrive.UseVisualStyleBackColor = true;
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(8, 81);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(171, 17);
            this.chkFilter.TabIndex = 0;
            this.chkFilter.Text = "Do not log filtered files/folders";
            this.chkFilter.UseVisualStyleBackColor = true;
            // 
            // chkReason
            // 
            this.chkReason.AutoSize = true;
            this.chkReason.Location = new System.Drawing.Point(8, 58);
            this.chkReason.Name = "chkReason";
            this.chkReason.Size = new System.Drawing.Size(278, 17);
            this.chkReason.TabIndex = 0;
            this.chkReason.Text = "Log the reason why files/folders are ignored/skipped";
            this.chkReason.UseVisualStyleBackColor = true;
            // 
            // chkDisplay
            // 
            this.chkDisplay.AutoSize = true;
            this.chkDisplay.Checked = true;
            this.chkDisplay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplay.Location = new System.Drawing.Point(8, 13);
            this.chkDisplay.Name = "chkDisplay";
            this.chkDisplay.Size = new System.Drawing.Size(175, 17);
            this.chkDisplay.TabIndex = 0;
            this.chkDisplay.Text = "Display log after running a task";
            this.chkDisplay.UseVisualStyleBackColor = true;
            // 
            // tcAdvanced
            // 
            this.tcAdvanced.Controls.Add(this.groupBox8);
            this.tcAdvanced.Controls.Add(this.groupBox6);
            this.tcAdvanced.Controls.Add(this.chkAutoSync);
            this.tcAdvanced.Controls.Add(this.groupBox3);
            this.tcAdvanced.Controls.Add(this.groupBox2);
            this.tcAdvanced.Controls.Add(this.groupBox1);
            this.tcAdvanced.ImageIndex = 5;
            this.tcAdvanced.Location = new System.Drawing.Point(4, 44);
            this.tcAdvanced.Name = "tcAdvanced";
            this.tcAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tcAdvanced.Size = new System.Drawing.Size(464, 364);
            this.tcAdvanced.TabIndex = 6;
            this.tcAdvanced.Text = "Advanced";
            this.tcAdvanced.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.ndTime);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Location = new System.Drawing.Point(6, 280);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(448, 58);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "File time differences";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "second(s) or less";
            // 
            // ndTime
            // 
            this.ndTime.Location = new System.Drawing.Point(161, 24);
            this.ndTime.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ndTime.Name = "ndTime";
            this.ndTime.Size = new System.Drawing.Size(53, 21);
            this.ndTime.TabIndex = 2;
            this.ndTime.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Ignore date && time changes of ";
            // 
            // chkAutoSync
            // 
            this.chkAutoSync.AutoSize = true;
            this.chkAutoSync.Checked = true;
            this.chkAutoSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoSync.Location = new System.Drawing.Point(7, 13);
            this.chkAutoSync.Name = "chkAutoSync";
            this.chkAutoSync.Size = new System.Drawing.Size(171, 17);
            this.chkAutoSync.TabIndex = 2;
            this.chkAutoSync.Text = "Enable auto-sync upon plug-in";
            this.chkAutoSync.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radTargetSrc);
            this.groupBox3.Controls.Add(this.radSrcTarget);
            this.groupBox3.Controls.Add(this.radKeepBoth);
            this.groupBox3.Controls.Add(this.radNewOld);
            this.groupBox3.Location = new System.Drawing.Point(6, 144);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(448, 74);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "What to do if the same file has been modified in source and target";
            // 
            // radTargetSrc
            // 
            this.radTargetSrc.AutoSize = true;
            this.radTargetSrc.Location = new System.Drawing.Point(192, 48);
            this.radTargetSrc.Name = "radTargetSrc";
            this.radTargetSrc.Size = new System.Drawing.Size(182, 17);
            this.radTargetSrc.TabIndex = 1;
            this.radTargetSrc.Text = "Target overwrites source always";
            this.radTargetSrc.UseVisualStyleBackColor = true;
            // 
            // radSrcTarget
            // 
            this.radSrcTarget.AutoSize = true;
            this.radSrcTarget.Location = new System.Drawing.Point(6, 48);
            this.radSrcTarget.Name = "radSrcTarget";
            this.radSrcTarget.Size = new System.Drawing.Size(181, 17);
            this.radSrcTarget.TabIndex = 2;
            this.radSrcTarget.Text = "Source overwrites target always";
            this.radSrcTarget.UseVisualStyleBackColor = true;
            // 
            // radKeepBoth
            // 
            this.radKeepBoth.AutoSize = true;
            this.radKeepBoth.Checked = true;
            this.radKeepBoth.Location = new System.Drawing.Point(192, 25);
            this.radKeepBoth.Name = "radKeepBoth";
            this.radKeepBoth.Size = new System.Drawing.Size(241, 17);
            this.radKeepBoth.TabIndex = 1;
            this.radKeepBoth.TabStop = true;
            this.radKeepBoth.Text = "Keep the original copies (rename copied files)";
            this.radKeepBoth.UseVisualStyleBackColor = true;
            // 
            // radNewOld
            // 
            this.radNewOld.AutoSize = true;
            this.radNewOld.Location = new System.Drawing.Point(6, 25);
            this.radNewOld.Name = "radNewOld";
            this.radNewOld.Size = new System.Drawing.Size(171, 17);
            this.radNewOld.TabIndex = 2;
            this.radNewOld.Text = "Newer file overwrites older file";
            this.radNewOld.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radToSource);
            this.groupBox2.Controls.Add(this.radDelTarget);
            this.groupBox2.Location = new System.Drawing.Point(6, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "What to do if a file has been modified in target but deleted in source";
            // 
            // radToSource
            // 
            this.radToSource.AutoSize = true;
            this.radToSource.Checked = true;
            this.radToSource.Location = new System.Drawing.Point(7, 23);
            this.radToSource.Name = "radToSource";
            this.radToSource.Size = new System.Drawing.Size(115, 17);
            this.radToSource.TabIndex = 2;
            this.radToSource.TabStop = true;
            this.radToSource.Text = "Copy file to source";
            this.radToSource.UseVisualStyleBackColor = true;
            // 
            // radDelTarget
            // 
            this.radDelTarget.AutoSize = true;
            this.radDelTarget.Location = new System.Drawing.Point(192, 23);
            this.radDelTarget.Name = "radDelTarget";
            this.radDelTarget.Size = new System.Drawing.Size(131, 17);
            this.radDelTarget.TabIndex = 1;
            this.radDelTarget.Text = "Delete file from target";
            this.radDelTarget.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radDelSource);
            this.groupBox1.Controls.Add(this.radToTarget);
            this.groupBox1.Location = new System.Drawing.Point(6, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "What to do if a file has been modified in source but deleted in target";
            // 
            // radDelSource
            // 
            this.radDelSource.AutoSize = true;
            this.radDelSource.Location = new System.Drawing.Point(192, 22);
            this.radDelSource.Name = "radDelSource";
            this.radDelSource.Size = new System.Drawing.Size(133, 17);
            this.radDelSource.TabIndex = 0;
            this.radDelSource.Text = "Delete file from source";
            this.radDelSource.UseVisualStyleBackColor = true;
            // 
            // radToTarget
            // 
            this.radToTarget.AutoSize = true;
            this.radToTarget.Checked = true;
            this.radToTarget.Location = new System.Drawing.Point(7, 22);
            this.radToTarget.Name = "radToTarget";
            this.radToTarget.Size = new System.Drawing.Size(113, 17);
            this.radToTarget.TabIndex = 0;
            this.radToTarget.TabStop = true;
            this.radToTarget.Text = "Copy file to target";
            this.radToTarget.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "compare_options.gif");
            this.imageList.Images.SetKeyName(1, "folder.png");
            this.imageList.Images.SetKeyName(2, "copy.png");
            this.imageList.Images.SetKeyName(3, "log.png");
            this.imageList.Images.SetKeyName(4, "filter.gif");
            this.imageList.Images.SetKeyName(5, "advanced.png");
            this.imageList.Images.SetKeyName(6, "general.png");
            // 
            // btnApply
            // 
            this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
            this.btnApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApply.Location = new System.Drawing.Point(400, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(68, 25);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(319, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.Location = new System.Drawing.Point(238, 3);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 25);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "OK";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radRenameTgt);
            this.groupBox8.Controls.Add(this.radRenameSrc);
            this.groupBox8.Location = new System.Drawing.Point(6, 224);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(448, 50);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "What to do if same folder has been renamed in source and target";
            // 
            // radRenameSrc
            // 
            this.radRenameSrc.AutoSize = true;
            this.radRenameSrc.Checked = true;
            this.radRenameSrc.Location = new System.Drawing.Point(7, 20);
            this.radRenameSrc.Name = "radRenameSrc";
            this.radRenameSrc.Size = new System.Drawing.Size(112, 17);
            this.radRenameSrc.TabIndex = 0;
            this.radRenameSrc.TabStop = true;
            this.radRenameSrc.Text = "Rename to source";
            this.radRenameSrc.UseVisualStyleBackColor = true;
            // 
            // radRenameTgt
            // 
            this.radRenameTgt.AutoSize = true;
            this.radRenameTgt.Location = new System.Drawing.Point(192, 20);
            this.radRenameTgt.Name = "radRenameTgt";
            this.radRenameTgt.Size = new System.Drawing.Size(110, 17);
            this.radRenameTgt.TabIndex = 0;
            this.radRenameTgt.Text = "Rename to target";
            this.radRenameTgt.UseVisualStyleBackColor = true;
            // 
            // TaskSetupForm
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(472, 466);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Task Setup";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tcTaskSetup.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tpFolderPair.ResumeLayout(false);
            this.tpFolderPair.PerformLayout();
            this.tpFilter.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tpCopyDel.ResumeLayout(false);
            this.tpCopyDel.PerformLayout();
            this.tpLogSettings.ResumeLayout(false);
            this.tpLogSettings.PerformLayout();
            this.tcAdvanced.ResumeLayout(false);
            this.tcAdvanced.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndTime)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.TabControl tcTaskSetup;
        private System.Windows.Forms.TabPage tpFolderPair;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tpCopyDel;
        private System.Windows.Forms.TabPage tpFilter;
        private System.Windows.Forms.TabPage tpLogSettings;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.CheckBox chkVerify;
        private System.Windows.Forms.CheckBox chkSafe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkConfirm;
        private System.Windows.Forms.CheckBox chkMove;
        private System.Windows.Forms.TabPage tcAdvanced;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radDelSource;
        private System.Windows.Forms.RadioButton radToTarget;
        private System.Windows.Forms.RadioButton radToSource;
        private System.Windows.Forms.RadioButton radDelTarget;
        private System.Windows.Forms.RadioButton radTargetSrc;
        private System.Windows.Forms.RadioButton radSrcTarget;
        private System.Windows.Forms.RadioButton radKeepBoth;
        private System.Windows.Forms.RadioButton radNewOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.CheckBox chkReason;
        private System.Windows.Forms.CheckBox chkDisplay;
        private System.Windows.Forms.Button btnDeleteLog;
        private System.Windows.Forms.CheckBox chkError;
        private System.Windows.Forms.CheckBox chkDrive;
        private System.Windows.Forms.CheckBox chkFailed;
        private System.Windows.Forms.CheckBox chkAutoSync;
        private System.Windows.Forms.TextBox txtExclude;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkSkipSystem;
        private System.Windows.Forms.CheckBox chkSkipTemp;
        private System.Windows.Forms.CheckBox chkSkipRO;
        private System.Windows.Forms.CheckBox chkSkipHidden;
        private System.Windows.Forms.TextBox txtInclude;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSubFolders;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblLastRun;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblFileInclude;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblFileExclude;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radBackup;
        private System.Windows.Forms.RadioButton radSync;
        private System.Windows.Forms.LinkLabel lblChange;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ndTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radRenameTgt;
        private System.Windows.Forms.RadioButton radRenameSrc;

    }
}