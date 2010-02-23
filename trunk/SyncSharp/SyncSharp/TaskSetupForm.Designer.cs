namespace SyncSharp
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
					this.tpFolderPair = new System.Windows.Forms.TabPage();
					this.btnDelete = new System.Windows.Forms.Button();
					this.btnSwitch = new System.Windows.Forms.Button();
					this.btnTarget = new System.Windows.Forms.Button();
					this.btnSource = new System.Windows.Forms.Button();
					this.label3 = new System.Windows.Forms.Label();
					this.label2 = new System.Windows.Forms.Label();
					this.txtTarget = new System.Windows.Forms.TextBox();
					this.txtSource = new System.Windows.Forms.TextBox();
					this.tpComparison = new System.Windows.Forms.TabPage();
					this.groupBox8 = new System.Windows.Forms.GroupBox();
					this.chkAttribute = new System.Windows.Forms.CheckBox();
					this.chkTime = new System.Windows.Forms.CheckBox();
					this.chkHash = new System.Windows.Forms.CheckBox();
					this.chkSize = new System.Windows.Forms.CheckBox();
					this.groupBox7 = new System.Windows.Forms.GroupBox();
					this.chkSkipSystem = new System.Windows.Forms.CheckBox();
					this.chkSkipTemp = new System.Windows.Forms.CheckBox();
					this.chkSkipRO = new System.Windows.Forms.CheckBox();
					this.chkSkipHidden = new System.Windows.Forms.CheckBox();
					this.chkSkipWindow = new System.Windows.Forms.CheckBox();
					this.groupBox6 = new System.Windows.Forms.GroupBox();
					this.cbUnits = new System.Windows.Forms.ComboBox();
					this.label5 = new System.Windows.Forms.Label();
					this.ndNum = new System.Windows.Forms.NumericUpDown();
					this.ndTime = new System.Windows.Forms.NumericUpDown();
					this.label6 = new System.Windows.Forms.Label();
					this.label4 = new System.Windows.Forms.Label();
					this.chkIgnoreDST = new System.Windows.Forms.CheckBox();
					this.tpFilter = new System.Windows.Forms.TabPage();
					this.groupBox4 = new System.Windows.Forms.GroupBox();
					this.btnSubDir = new System.Windows.Forms.Button();
					this.txtFilter = new System.Windows.Forms.TextBox();
					this.btnDeleteEx = new System.Windows.Forms.Button();
					this.btnAddEx = new System.Windows.Forms.Button();
					this.lstExclude = new System.Windows.Forms.ListBox();
					this.tpCopyDel = new System.Windows.Forms.TabPage();
					this.chkDelTarget = new System.Windows.Forms.CheckBox();
					this.chkConfirm = new System.Windows.Forms.CheckBox();
					this.label1 = new System.Windows.Forms.Label();
					this.chkSafe = new System.Windows.Forms.CheckBox();
					this.chkMove = new System.Windows.Forms.CheckBox();
					this.chkDelSource = new System.Windows.Forms.CheckBox();
					this.chkVerify = new System.Windows.Forms.CheckBox();
					this.chkReset = new System.Windows.Forms.CheckBox();
					this.chkPrompt = new System.Windows.Forms.CheckBox();
					this.tpLogSettings = new System.Windows.Forms.TabPage();
					this.btnDeleteLog = new System.Windows.Forms.Button();
					this.chkError = new System.Windows.Forms.CheckBox();
					this.chkFailed = new System.Windows.Forms.CheckBox();
					this.chkDrive = new System.Windows.Forms.CheckBox();
					this.chkFilter = new System.Windows.Forms.CheckBox();
					this.chkReason = new System.Windows.Forms.CheckBox();
					this.chkDisplay = new System.Windows.Forms.CheckBox();
					this.tcAdvanced = new System.Windows.Forms.TabPage();
					this.chkAutoSync = new System.Windows.Forms.CheckBox();
					this.groupBox3 = new System.Windows.Forms.GroupBox();
					this.radPromptInBoth = new System.Windows.Forms.RadioButton();
					this.radSkipInBoth = new System.Windows.Forms.RadioButton();
					this.radTargetSrc = new System.Windows.Forms.RadioButton();
					this.radSrcTarget = new System.Windows.Forms.RadioButton();
					this.radSmallLarge = new System.Windows.Forms.RadioButton();
					this.radLargeSmall = new System.Windows.Forms.RadioButton();
					this.radOldNew = new System.Windows.Forms.RadioButton();
					this.radNewOld = new System.Windows.Forms.RadioButton();
					this.groupBox2 = new System.Windows.Forms.GroupBox();
					this.radPromptInTarget = new System.Windows.Forms.RadioButton();
					this.radSkipInTarget = new System.Windows.Forms.RadioButton();
					this.radToSource = new System.Windows.Forms.RadioButton();
					this.radDelTarget = new System.Windows.Forms.RadioButton();
					this.groupBox1 = new System.Windows.Forms.GroupBox();
					this.radPromptInSrc = new System.Windows.Forms.RadioButton();
					this.radSkipInSrc = new System.Windows.Forms.RadioButton();
					this.radDelSource = new System.Windows.Forms.RadioButton();
					this.radToTarget = new System.Windows.Forms.RadioButton();
					this.imageList = new System.Windows.Forms.ImageList(this.components);
					this.btnApply = new System.Windows.Forms.Button();
					this.btnCancel = new System.Windows.Forms.Button();
					this.btnAccept = new System.Windows.Forms.Button();
					this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
					this.splitContainer1.Panel1.SuspendLayout();
					this.splitContainer1.Panel2.SuspendLayout();
					this.splitContainer1.SuspendLayout();
					this.tcTaskSetup.SuspendLayout();
					this.tpFolderPair.SuspendLayout();
					this.tpComparison.SuspendLayout();
					this.groupBox8.SuspendLayout();
					this.groupBox7.SuspendLayout();
					this.groupBox6.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.ndNum)).BeginInit();
					((System.ComponentModel.ISupportInitialize)(this.ndTime)).BeginInit();
					this.tpFilter.SuspendLayout();
					this.groupBox4.SuspendLayout();
					this.tpCopyDel.SuspendLayout();
					this.tpLogSettings.SuspendLayout();
					this.tcAdvanced.SuspendLayout();
					this.groupBox3.SuspendLayout();
					this.groupBox2.SuspendLayout();
					this.groupBox1.SuspendLayout();
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
					this.tcTaskSetup.Controls.Add(this.tpComparison);
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
					this.tpGeneral.ImageIndex = 6;
					this.tpGeneral.Location = new System.Drawing.Point(4, 44);
					this.tpGeneral.Name = "tpGeneral";
					this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
					this.tpGeneral.Size = new System.Drawing.Size(464, 364);
					this.tpGeneral.TabIndex = 7;
					this.tpGeneral.Text = "General";
					this.tpGeneral.UseVisualStyleBackColor = true;
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
					this.tpFolderPair.Location = new System.Drawing.Point(4, 44);
					this.tpFolderPair.Name = "tpFolderPair";
					this.tpFolderPair.Padding = new System.Windows.Forms.Padding(3);
					this.tpFolderPair.Size = new System.Drawing.Size(464, 364);
					this.tpFolderPair.TabIndex = 0;
					this.tpFolderPair.Text = "Folder Pair";
					this.tpFolderPair.UseVisualStyleBackColor = true;
					// 
					// btnDelete
					// 
					this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
					this.btnDelete.Location = new System.Drawing.Point(146, 87);
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
					this.btnSwitch.Location = new System.Drawing.Point(53, 87);
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
					this.btnTarget.Location = new System.Drawing.Point(409, 49);
					this.btnTarget.Name = "btnTarget";
					this.btnTarget.Size = new System.Drawing.Size(30, 23);
					this.btnTarget.TabIndex = 2;
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
					this.btnSource.UseVisualStyleBackColor = true;
					this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
					// 
					// label3
					// 
					this.label3.AutoSize = true;
					this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					this.label3.Location = new System.Drawing.Point(6, 50);
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
					this.txtTarget.Location = new System.Drawing.Point(53, 50);
					this.txtTarget.Name = "txtTarget";
					this.txtTarget.Size = new System.Drawing.Size(356, 21);
					this.txtTarget.TabIndex = 0;
					// 
					// txtSource
					// 
					this.txtSource.Location = new System.Drawing.Point(53, 18);
					this.txtSource.Name = "txtSource";
					this.txtSource.Size = new System.Drawing.Size(356, 21);
					this.txtSource.TabIndex = 0;
					// 
					// tpComparison
					// 
					this.tpComparison.Controls.Add(this.groupBox8);
					this.tpComparison.Controls.Add(this.groupBox7);
					this.tpComparison.Controls.Add(this.chkSkipWindow);
					this.tpComparison.Controls.Add(this.groupBox6);
					this.tpComparison.ImageIndex = 0;
					this.tpComparison.Location = new System.Drawing.Point(4, 44);
					this.tpComparison.Name = "tpComparison";
					this.tpComparison.Padding = new System.Windows.Forms.Padding(3);
					this.tpComparison.Size = new System.Drawing.Size(464, 364);
					this.tpComparison.TabIndex = 1;
					this.tpComparison.Text = "Comparison rules";
					this.tpComparison.UseVisualStyleBackColor = true;
					// 
					// groupBox8
					// 
					this.groupBox8.Controls.Add(this.chkAttribute);
					this.groupBox8.Controls.Add(this.chkTime);
					this.groupBox8.Controls.Add(this.chkHash);
					this.groupBox8.Controls.Add(this.chkSize);
					this.groupBox8.Location = new System.Drawing.Point(6, 34);
					this.groupBox8.Name = "groupBox8";
					this.groupBox8.Size = new System.Drawing.Size(448, 70);
					this.groupBox8.TabIndex = 3;
					this.groupBox8.TabStop = false;
					this.groupBox8.Text = "Files are considered equal if they have an equal";
					// 
					// chkAttribute
					// 
					this.chkAttribute.AutoSize = true;
					this.chkAttribute.Location = new System.Drawing.Point(197, 43);
					this.chkAttribute.Name = "chkAttribute";
					this.chkAttribute.Size = new System.Drawing.Size(74, 17);
					this.chkAttribute.TabIndex = 0;
					this.chkAttribute.Text = "Attributes";
					this.chkAttribute.UseVisualStyleBackColor = true;
					// 
					// chkTime
					// 
					this.chkTime.AutoSize = true;
					this.chkTime.Location = new System.Drawing.Point(6, 43);
					this.chkTime.Name = "chkTime";
					this.chkTime.Size = new System.Drawing.Size(82, 17);
					this.chkTime.TabIndex = 0;
					this.chkTime.Text = "Timestamps";
					this.chkTime.UseVisualStyleBackColor = true;
					// 
					// chkHash
					// 
					this.chkHash.AutoSize = true;
					this.chkHash.Location = new System.Drawing.Point(197, 20);
					this.chkHash.Name = "chkHash";
					this.chkHash.Size = new System.Drawing.Size(207, 17);
					this.chkHash.TabIndex = 0;
					this.chkHash.Text = "Hash code (may reduce performance)";
					this.chkHash.UseVisualStyleBackColor = true;
					// 
					// chkSize
					// 
					this.chkSize.AutoSize = true;
					this.chkSize.Location = new System.Drawing.Point(6, 20);
					this.chkSize.Name = "chkSize";
					this.chkSize.Size = new System.Drawing.Size(45, 17);
					this.chkSize.TabIndex = 0;
					this.chkSize.Text = "Size";
					this.chkSize.UseVisualStyleBackColor = true;
					// 
					// groupBox7
					// 
					this.groupBox7.Controls.Add(this.chkSkipSystem);
					this.groupBox7.Controls.Add(this.chkSkipTemp);
					this.groupBox7.Controls.Add(this.chkSkipRO);
					this.groupBox7.Controls.Add(this.chkSkipHidden);
					this.groupBox7.Location = new System.Drawing.Point(6, 219);
					this.groupBox7.Name = "groupBox7";
					this.groupBox7.Size = new System.Drawing.Size(448, 77);
					this.groupBox7.TabIndex = 2;
					this.groupBox7.TabStop = false;
					this.groupBox7.Text = "File attributes";
					// 
					// chkSkipSystem
					// 
					this.chkSkipSystem.AutoSize = true;
					this.chkSkipSystem.Checked = true;
					this.chkSkipSystem.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkSkipSystem.Location = new System.Drawing.Point(6, 43);
					this.chkSkipSystem.Name = "chkSkipSystem";
					this.chkSkipSystem.Size = new System.Drawing.Size(172, 17);
					this.chkSkipSystem.TabIndex = 1;
					this.chkSkipSystem.Text = "Skip files with system attribute";
					this.chkSkipSystem.UseVisualStyleBackColor = true;
					// 
					// chkSkipTemp
					// 
					this.chkSkipTemp.AutoSize = true;
					this.chkSkipTemp.Location = new System.Drawing.Point(197, 43);
					this.chkSkipTemp.Name = "chkSkipTemp";
					this.chkSkipTemp.Size = new System.Drawing.Size(188, 17);
					this.chkSkipTemp.TabIndex = 1;
					this.chkSkipTemp.Text = "Skip files with temporary attribute";
					this.chkSkipTemp.UseVisualStyleBackColor = true;
					// 
					// chkSkipRO
					// 
					this.chkSkipRO.AutoSize = true;
					this.chkSkipRO.Location = new System.Drawing.Point(197, 20);
					this.chkSkipRO.Name = "chkSkipRO";
					this.chkSkipRO.Size = new System.Drawing.Size(184, 17);
					this.chkSkipRO.TabIndex = 1;
					this.chkSkipRO.Text = "Skip files with read-only attribute";
					this.chkSkipRO.UseVisualStyleBackColor = true;
					// 
					// chkSkipHidden
					// 
					this.chkSkipHidden.AutoSize = true;
					this.chkSkipHidden.Location = new System.Drawing.Point(6, 20);
					this.chkSkipHidden.Name = "chkSkipHidden";
					this.chkSkipHidden.Size = new System.Drawing.Size(170, 17);
					this.chkSkipHidden.TabIndex = 1;
					this.chkSkipHidden.Text = "Skip files with hidden attribute";
					this.chkSkipHidden.UseVisualStyleBackColor = true;
					// 
					// chkSkipWindow
					// 
					this.chkSkipWindow.AutoSize = true;
					this.chkSkipWindow.Location = new System.Drawing.Point(6, 11);
					this.chkSkipWindow.Name = "chkSkipWindow";
					this.chkSkipWindow.Size = new System.Drawing.Size(252, 17);
					this.chkSkipWindow.TabIndex = 1;
					this.chkSkipWindow.Text = "Skip differences window when this profile is run";
					this.chkSkipWindow.UseVisualStyleBackColor = true;
					// 
					// groupBox6
					// 
					this.groupBox6.Controls.Add(this.cbUnits);
					this.groupBox6.Controls.Add(this.label5);
					this.groupBox6.Controls.Add(this.ndNum);
					this.groupBox6.Controls.Add(this.ndTime);
					this.groupBox6.Controls.Add(this.label6);
					this.groupBox6.Controls.Add(this.label4);
					this.groupBox6.Controls.Add(this.chkIgnoreDST);
					this.groupBox6.Location = new System.Drawing.Point(6, 110);
					this.groupBox6.Name = "groupBox6";
					this.groupBox6.Size = new System.Drawing.Size(448, 103);
					this.groupBox6.TabIndex = 0;
					this.groupBox6.TabStop = false;
					this.groupBox6.Text = "File time differences";
					// 
					// cbUnits
					// 
					this.cbUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
					this.cbUnits.FormattingEnabled = true;
					this.cbUnits.Items.AddRange(new object[] {
            "seconds",
            "minutes",
            "hours",
            "days"});
					this.cbUnits.Location = new System.Drawing.Point(333, 73);
					this.cbUnits.Name = "cbUnits";
					this.cbUnits.Size = new System.Drawing.Size(94, 21);
					this.cbUnits.TabIndex = 4;
					// 
					// label5
					// 
					this.label5.AutoSize = true;
					this.label5.Location = new System.Drawing.Point(217, 49);
					this.label5.Name = "label5";
					this.label5.Size = new System.Drawing.Size(88, 13);
					this.label5.TabIndex = 3;
					this.label5.Text = "second(s) or less";
					// 
					// ndNum
					// 
					this.ndNum.Location = new System.Drawing.Point(274, 73);
					this.ndNum.Name = "ndNum";
					this.ndNum.Size = new System.Drawing.Size(57, 21);
					this.ndNum.TabIndex = 2;
					// 
					// ndTime
					// 
					this.ndTime.Location = new System.Drawing.Point(158, 47);
					this.ndTime.Name = "ndTime";
					this.ndTime.Size = new System.Drawing.Size(53, 21);
					this.ndTime.TabIndex = 2;
					// 
					// label6
					// 
					this.label6.AutoSize = true;
					this.label6.Location = new System.Drawing.Point(3, 77);
					this.label6.Name = "label6";
					this.label6.Size = new System.Drawing.Size(270, 13);
					this.label6.TabIndex = 1;
					this.label6.Text = "Ignore files that have not been modified within the last";
					// 
					// label4
					// 
					this.label4.AutoSize = true;
					this.label4.Location = new System.Drawing.Point(3, 49);
					this.label4.Name = "label4";
					this.label4.Size = new System.Drawing.Size(156, 13);
					this.label4.TabIndex = 1;
					this.label4.Text = "Ignore date && time changes of ";
					// 
					// chkIgnoreDST
					// 
					this.chkIgnoreDST.AutoSize = true;
					this.chkIgnoreDST.Checked = true;
					this.chkIgnoreDST.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkIgnoreDST.Location = new System.Drawing.Point(6, 20);
					this.chkIgnoreDST.Name = "chkIgnoreDST";
					this.chkIgnoreDST.Size = new System.Drawing.Size(404, 17);
					this.chkIgnoreDST.TabIndex = 0;
					this.chkIgnoreDST.Text = "Ignore date && time differences because of Daylight Saving Time (DST) changes";
					this.chkIgnoreDST.UseVisualStyleBackColor = true;
					// 
					// tpFilter
					// 
					this.tpFilter.Controls.Add(this.groupBox4);
					this.tpFilter.ImageIndex = 4;
					this.tpFilter.Location = new System.Drawing.Point(4, 44);
					this.tpFilter.Name = "tpFilter";
					this.tpFilter.Size = new System.Drawing.Size(464, 364);
					this.tpFilter.TabIndex = 5;
					this.tpFilter.Text = "Filters";
					this.tpFilter.UseVisualStyleBackColor = true;
					// 
					// groupBox4
					// 
					this.groupBox4.Controls.Add(this.btnSubDir);
					this.groupBox4.Controls.Add(this.txtFilter);
					this.groupBox4.Controls.Add(this.btnDeleteEx);
					this.groupBox4.Controls.Add(this.btnAddEx);
					this.groupBox4.Controls.Add(this.lstExclude);
					this.groupBox4.Location = new System.Drawing.Point(8, 6);
					this.groupBox4.Name = "groupBox4";
					this.groupBox4.Size = new System.Drawing.Size(448, 345);
					this.groupBox4.TabIndex = 0;
					this.groupBox4.TabStop = false;
					this.groupBox4.Text = "Files/Folders to exclude";
					// 
					// btnSubDir
					// 
					this.btnSubDir.Location = new System.Drawing.Point(344, 50);
					this.btnSubDir.Name = "btnSubDir";
					this.btnSubDir.Size = new System.Drawing.Size(98, 25);
					this.btnSubDir.TabIndex = 3;
					this.btnSubDir.Text = "Sub-directory...";
					this.btnSubDir.UseVisualStyleBackColor = true;
					// 
					// txtFilter
					// 
					this.txtFilter.Location = new System.Drawing.Point(6, 20);
					this.txtFilter.Name = "txtFilter";
					this.txtFilter.Size = new System.Drawing.Size(332, 21);
					this.txtFilter.TabIndex = 2;
					// 
					// btnDeleteEx
					// 
					this.btnDeleteEx.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteEx.Image")));
					this.btnDeleteEx.Location = new System.Drawing.Point(345, 313);
					this.btnDeleteEx.Name = "btnDeleteEx";
					this.btnDeleteEx.Size = new System.Drawing.Size(97, 25);
					this.btnDeleteEx.TabIndex = 1;
					this.btnDeleteEx.Text = "Delete";
					this.btnDeleteEx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					this.btnDeleteEx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.btnDeleteEx.UseVisualStyleBackColor = true;
					// 
					// btnAddEx
					// 
					this.btnAddEx.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEx.Image")));
					this.btnAddEx.Location = new System.Drawing.Point(344, 18);
					this.btnAddEx.Name = "btnAddEx";
					this.btnAddEx.Size = new System.Drawing.Size(97, 25);
					this.btnAddEx.TabIndex = 1;
					this.btnAddEx.Text = "Add";
					this.btnAddEx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					this.btnAddEx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.btnAddEx.UseVisualStyleBackColor = true;
					// 
					// lstExclude
					// 
					this.lstExclude.FormattingEnabled = true;
					this.lstExclude.Location = new System.Drawing.Point(6, 48);
					this.lstExclude.Name = "lstExclude";
					this.lstExclude.Size = new System.Drawing.Size(333, 290);
					this.lstExclude.TabIndex = 0;
					// 
					// tpCopyDel
					// 
					this.tpCopyDel.Controls.Add(this.chkDelTarget);
					this.tpCopyDel.Controls.Add(this.chkConfirm);
					this.tpCopyDel.Controls.Add(this.label1);
					this.tpCopyDel.Controls.Add(this.chkSafe);
					this.tpCopyDel.Controls.Add(this.chkMove);
					this.tpCopyDel.Controls.Add(this.chkDelSource);
					this.tpCopyDel.Controls.Add(this.chkVerify);
					this.tpCopyDel.Controls.Add(this.chkReset);
					this.tpCopyDel.Controls.Add(this.chkPrompt);
					this.tpCopyDel.ImageIndex = 2;
					this.tpCopyDel.Location = new System.Drawing.Point(4, 44);
					this.tpCopyDel.Name = "tpCopyDel";
					this.tpCopyDel.Size = new System.Drawing.Size(464, 364);
					this.tpCopyDel.TabIndex = 3;
					this.tpCopyDel.Text = "Copy/Delete";
					this.tpCopyDel.UseVisualStyleBackColor = true;
					// 
					// chkDelTarget
					// 
					this.chkDelTarget.AutoSize = true;
					this.chkDelTarget.Location = new System.Drawing.Point(8, 109);
					this.chkDelTarget.Name = "chkDelTarget";
					this.chkDelTarget.Size = new System.Drawing.Size(184, 17);
					this.chkDelTarget.TabIndex = 9;
					this.chkDelTarget.Text = "Delete empty folders from target";
					this.chkDelTarget.UseVisualStyleBackColor = true;
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
					this.chkSafe.Location = new System.Drawing.Point(8, 201);
					this.chkSafe.Name = "chkSafe";
					this.chkSafe.Size = new System.Drawing.Size(357, 17);
					this.chkSafe.TabIndex = 3;
					this.chkSafe.Text = "Make safe copies (protect overwriting file until file replication is done)";
					this.chkSafe.UseVisualStyleBackColor = true;
					// 
					// chkMove
					// 
					this.chkMove.AutoSize = true;
					this.chkMove.Checked = true;
					this.chkMove.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkMove.Location = new System.Drawing.Point(8, 40);
					this.chkMove.Name = "chkMove";
					this.chkMove.Size = new System.Drawing.Size(180, 17);
					this.chkMove.TabIndex = 5;
					this.chkMove.Text = "Move deleted files to recycle bin";
					this.chkMove.UseVisualStyleBackColor = true;
					// 
					// chkDelSource
					// 
					this.chkDelSource.AutoSize = true;
					this.chkDelSource.Location = new System.Drawing.Point(8, 86);
					this.chkDelSource.Name = "chkDelSource";
					this.chkDelSource.Size = new System.Drawing.Size(186, 17);
					this.chkDelSource.TabIndex = 8;
					this.chkDelSource.Text = "Delete empty folders from source";
					this.chkDelSource.UseVisualStyleBackColor = true;
					// 
					// chkVerify
					// 
					this.chkVerify.AutoSize = true;
					this.chkVerify.Location = new System.Drawing.Point(8, 132);
					this.chkVerify.Name = "chkVerify";
					this.chkVerify.Size = new System.Drawing.Size(197, 17);
					this.chkVerify.TabIndex = 0;
					this.chkVerify.Text = "Verify that files are copied correctly";
					this.chkVerify.UseVisualStyleBackColor = true;
					// 
					// chkReset
					// 
					this.chkReset.AutoSize = true;
					this.chkReset.Location = new System.Drawing.Point(8, 178);
					this.chkReset.Name = "chkReset";
					this.chkReset.Size = new System.Drawing.Size(349, 17);
					this.chkReset.TabIndex = 2;
					this.chkReset.Text = "Reset the archive file attribute on files once they have been copied";
					this.chkReset.UseVisualStyleBackColor = true;
					// 
					// chkPrompt
					// 
					this.chkPrompt.AutoSize = true;
					this.chkPrompt.Location = new System.Drawing.Point(8, 155);
					this.chkPrompt.Name = "chkPrompt";
					this.chkPrompt.Size = new System.Drawing.Size(276, 17);
					this.chkPrompt.TabIndex = 1;
					this.chkPrompt.Text = "Prompt to retry if a file is locked or cannot be copied";
					this.chkPrompt.UseVisualStyleBackColor = true;
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
					// chkAutoSync
					// 
					this.chkAutoSync.AutoSize = true;
					this.chkAutoSync.Checked = true;
					this.chkAutoSync.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkAutoSync.Location = new System.Drawing.Point(6, 16);
					this.chkAutoSync.Name = "chkAutoSync";
					this.chkAutoSync.Size = new System.Drawing.Size(171, 17);
					this.chkAutoSync.TabIndex = 2;
					this.chkAutoSync.Text = "Enable auto-sync upon plug-in";
					this.chkAutoSync.UseVisualStyleBackColor = true;
					// 
					// groupBox3
					// 
					this.groupBox3.Controls.Add(this.radPromptInBoth);
					this.groupBox3.Controls.Add(this.radSkipInBoth);
					this.groupBox3.Controls.Add(this.radTargetSrc);
					this.groupBox3.Controls.Add(this.radSrcTarget);
					this.groupBox3.Controls.Add(this.radSmallLarge);
					this.groupBox3.Controls.Add(this.radLargeSmall);
					this.groupBox3.Controls.Add(this.radOldNew);
					this.groupBox3.Controls.Add(this.radNewOld);
					this.groupBox3.Location = new System.Drawing.Point(6, 191);
					this.groupBox3.Name = "groupBox3";
					this.groupBox3.Size = new System.Drawing.Size(448, 123);
					this.groupBox3.TabIndex = 1;
					this.groupBox3.TabStop = false;
					this.groupBox3.Text = "What to do if the same file has been changed in source and target";
					// 
					// radPromptInBoth
					// 
					this.radPromptInBoth.AutoSize = true;
					this.radPromptInBoth.Location = new System.Drawing.Point(202, 71);
					this.radPromptInBoth.Name = "radPromptInBoth";
					this.radPromptInBoth.Size = new System.Drawing.Size(76, 17);
					this.radPromptInBoth.TabIndex = 3;
					this.radPromptInBoth.Text = "Prompt me";
					this.radPromptInBoth.UseVisualStyleBackColor = true;
					// 
					// radSkipInBoth
					// 
					this.radSkipInBoth.AutoSize = true;
					this.radSkipInBoth.Location = new System.Drawing.Point(202, 94);
					this.radSkipInBoth.Name = "radSkipInBoth";
					this.radSkipInBoth.Size = new System.Drawing.Size(138, 17);
					this.radSkipInBoth.TabIndex = 4;
					this.radSkipInBoth.Text = "Do nothing, skip the file";
					this.radSkipInBoth.UseVisualStyleBackColor = true;
					// 
					// radTargetSrc
					// 
					this.radTargetSrc.AutoSize = true;
					this.radTargetSrc.Location = new System.Drawing.Point(6, 94);
					this.radTargetSrc.Name = "radTargetSrc";
					this.radTargetSrc.Size = new System.Drawing.Size(182, 17);
					this.radTargetSrc.TabIndex = 1;
					this.radTargetSrc.Text = "Target overwrites source always";
					this.radTargetSrc.UseVisualStyleBackColor = true;
					// 
					// radSrcTarget
					// 
					this.radSrcTarget.AutoSize = true;
					this.radSrcTarget.Location = new System.Drawing.Point(6, 71);
					this.radSrcTarget.Name = "radSrcTarget";
					this.radSrcTarget.Size = new System.Drawing.Size(181, 17);
					this.radSrcTarget.TabIndex = 2;
					this.radSrcTarget.Text = "Source overwrites target always";
					this.radSrcTarget.UseVisualStyleBackColor = true;
					// 
					// radSmallLarge
					// 
					this.radSmallLarge.AutoSize = true;
					this.radSmallLarge.Location = new System.Drawing.Point(202, 48);
					this.radSmallLarge.Name = "radSmallLarge";
					this.radSmallLarge.Size = new System.Drawing.Size(178, 17);
					this.radSmallLarge.TabIndex = 1;
					this.radSmallLarge.Text = "Smaller file overwrites larger file";
					this.radSmallLarge.UseVisualStyleBackColor = true;
					// 
					// radLargeSmall
					// 
					this.radLargeSmall.AutoSize = true;
					this.radLargeSmall.Location = new System.Drawing.Point(202, 25);
					this.radLargeSmall.Name = "radLargeSmall";
					this.radLargeSmall.Size = new System.Drawing.Size(180, 17);
					this.radLargeSmall.TabIndex = 2;
					this.radLargeSmall.Text = "Larger file overwrites smaller file";
					this.radLargeSmall.UseVisualStyleBackColor = true;
					// 
					// radOldNew
					// 
					this.radOldNew.AutoSize = true;
					this.radOldNew.Location = new System.Drawing.Point(6, 48);
					this.radOldNew.Name = "radOldNew";
					this.radOldNew.Size = new System.Drawing.Size(172, 17);
					this.radOldNew.TabIndex = 1;
					this.radOldNew.Text = "Older file overwrites newer file";
					this.radOldNew.UseVisualStyleBackColor = true;
					// 
					// radNewOld
					// 
					this.radNewOld.AutoSize = true;
					this.radNewOld.Checked = true;
					this.radNewOld.Location = new System.Drawing.Point(6, 25);
					this.radNewOld.Name = "radNewOld";
					this.radNewOld.Size = new System.Drawing.Size(171, 17);
					this.radNewOld.TabIndex = 2;
					this.radNewOld.TabStop = true;
					this.radNewOld.Text = "Newer file overwrites older file";
					this.radNewOld.UseVisualStyleBackColor = true;
					// 
					// groupBox2
					// 
					this.groupBox2.Controls.Add(this.radPromptInTarget);
					this.groupBox2.Controls.Add(this.radSkipInTarget);
					this.groupBox2.Controls.Add(this.radToSource);
					this.groupBox2.Controls.Add(this.radDelTarget);
					this.groupBox2.Location = new System.Drawing.Point(6, 115);
					this.groupBox2.Name = "groupBox2";
					this.groupBox2.Size = new System.Drawing.Size(448, 70);
					this.groupBox2.TabIndex = 1;
					this.groupBox2.TabStop = false;
					this.groupBox2.Text = "What to do if a file is in target but not in source";
					// 
					// radPromptInTarget
					// 
					this.radPromptInTarget.AutoSize = true;
					this.radPromptInTarget.Location = new System.Drawing.Point(202, 23);
					this.radPromptInTarget.Name = "radPromptInTarget";
					this.radPromptInTarget.Size = new System.Drawing.Size(76, 17);
					this.radPromptInTarget.TabIndex = 3;
					this.radPromptInTarget.Text = "Prompt me";
					this.radPromptInTarget.UseVisualStyleBackColor = true;
					// 
					// radSkipInTarget
					// 
					this.radSkipInTarget.AutoSize = true;
					this.radSkipInTarget.Location = new System.Drawing.Point(202, 46);
					this.radSkipInTarget.Name = "radSkipInTarget";
					this.radSkipInTarget.Size = new System.Drawing.Size(138, 17);
					this.radSkipInTarget.TabIndex = 4;
					this.radSkipInTarget.Text = "Do nothing, skip the file";
					this.radSkipInTarget.UseVisualStyleBackColor = true;
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
					this.radDelTarget.Location = new System.Drawing.Point(7, 46);
					this.radDelTarget.Name = "radDelTarget";
					this.radDelTarget.Size = new System.Drawing.Size(131, 17);
					this.radDelTarget.TabIndex = 1;
					this.radDelTarget.Text = "Delete file from target";
					this.radDelTarget.UseVisualStyleBackColor = true;
					// 
					// groupBox1
					// 
					this.groupBox1.Controls.Add(this.radPromptInSrc);
					this.groupBox1.Controls.Add(this.radSkipInSrc);
					this.groupBox1.Controls.Add(this.radDelSource);
					this.groupBox1.Controls.Add(this.radToTarget);
					this.groupBox1.Location = new System.Drawing.Point(6, 39);
					this.groupBox1.Name = "groupBox1";
					this.groupBox1.Size = new System.Drawing.Size(448, 70);
					this.groupBox1.TabIndex = 0;
					this.groupBox1.TabStop = false;
					this.groupBox1.Text = "What to do if a file is in source but not in target";
					// 
					// radPromptInSrc
					// 
					this.radPromptInSrc.AutoSize = true;
					this.radPromptInSrc.Location = new System.Drawing.Point(202, 22);
					this.radPromptInSrc.Name = "radPromptInSrc";
					this.radPromptInSrc.Size = new System.Drawing.Size(76, 17);
					this.radPromptInSrc.TabIndex = 0;
					this.radPromptInSrc.Text = "Prompt me";
					this.radPromptInSrc.UseVisualStyleBackColor = true;
					// 
					// radSkipInSrc
					// 
					this.radSkipInSrc.AutoSize = true;
					this.radSkipInSrc.Location = new System.Drawing.Point(202, 45);
					this.radSkipInSrc.Name = "radSkipInSrc";
					this.radSkipInSrc.Size = new System.Drawing.Size(138, 17);
					this.radSkipInSrc.TabIndex = 0;
					this.radSkipInSrc.Text = "Do nothing, skip the file";
					this.radSkipInSrc.UseVisualStyleBackColor = true;
					// 
					// radDelSource
					// 
					this.radDelSource.AutoSize = true;
					this.radDelSource.Location = new System.Drawing.Point(7, 45);
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
					this.btnApply.Location = new System.Drawing.Point(392, 5);
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
					this.btnCancel.Location = new System.Drawing.Point(311, 5);
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
					this.btnAccept.Location = new System.Drawing.Point(230, 5);
					this.btnAccept.Name = "btnAccept";
					this.btnAccept.Size = new System.Drawing.Size(75, 25);
					this.btnAccept.TabIndex = 0;
					this.btnAccept.Text = "OK";
					this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					this.btnAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.btnAccept.UseVisualStyleBackColor = true;
					this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
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
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "Task Setup";
					this.splitContainer1.Panel1.ResumeLayout(false);
					this.splitContainer1.Panel2.ResumeLayout(false);
					this.splitContainer1.ResumeLayout(false);
					this.tcTaskSetup.ResumeLayout(false);
					this.tpFolderPair.ResumeLayout(false);
					this.tpFolderPair.PerformLayout();
					this.tpComparison.ResumeLayout(false);
					this.tpComparison.PerformLayout();
					this.groupBox8.ResumeLayout(false);
					this.groupBox8.PerformLayout();
					this.groupBox7.ResumeLayout(false);
					this.groupBox7.PerformLayout();
					this.groupBox6.ResumeLayout(false);
					this.groupBox6.PerformLayout();
					((System.ComponentModel.ISupportInitialize)(this.ndNum)).EndInit();
					((System.ComponentModel.ISupportInitialize)(this.ndTime)).EndInit();
					this.tpFilter.ResumeLayout(false);
					this.groupBox4.ResumeLayout(false);
					this.groupBox4.PerformLayout();
					this.tpCopyDel.ResumeLayout(false);
					this.tpCopyDel.PerformLayout();
					this.tpLogSettings.ResumeLayout(false);
					this.tpLogSettings.PerformLayout();
					this.tcAdvanced.ResumeLayout(false);
					this.tcAdvanced.PerformLayout();
					this.groupBox3.ResumeLayout(false);
					this.groupBox3.PerformLayout();
					this.groupBox2.ResumeLayout(false);
					this.groupBox2.PerformLayout();
					this.groupBox1.ResumeLayout(false);
					this.groupBox1.PerformLayout();
					this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.TabControl tcTaskSetup;
        private System.Windows.Forms.TabPage tpFolderPair;
        private System.Windows.Forms.TabPage tpComparison;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tpCopyDel;
        private System.Windows.Forms.TabPage tpFilter;
        private System.Windows.Forms.TabPage tpLogSettings;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.CheckBox chkVerify;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.CheckBox chkPrompt;
        private System.Windows.Forms.CheckBox chkSafe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkConfirm;
        private System.Windows.Forms.CheckBox chkMove;
        private System.Windows.Forms.CheckBox chkDelSource;
        private System.Windows.Forms.CheckBox chkDelTarget;
        private System.Windows.Forms.TabPage tcAdvanced;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radPromptInSrc;
        private System.Windows.Forms.RadioButton radSkipInSrc;
        private System.Windows.Forms.RadioButton radDelSource;
        private System.Windows.Forms.RadioButton radToTarget;
        private System.Windows.Forms.RadioButton radPromptInTarget;
        private System.Windows.Forms.RadioButton radSkipInTarget;
        private System.Windows.Forms.RadioButton radToSource;
        private System.Windows.Forms.RadioButton radDelTarget;
        private System.Windows.Forms.RadioButton radPromptInBoth;
        private System.Windows.Forms.RadioButton radSkipInBoth;
        private System.Windows.Forms.RadioButton radTargetSrc;
        private System.Windows.Forms.RadioButton radSrcTarget;
        private System.Windows.Forms.RadioButton radSmallLarge;
        private System.Windows.Forms.RadioButton radLargeSmall;
        private System.Windows.Forms.RadioButton radOldNew;
        private System.Windows.Forms.RadioButton radNewOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnDeleteEx;
        private System.Windows.Forms.Button btnAddEx;
        private System.Windows.Forms.ListBox lstExclude;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ndTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIgnoreDST;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbUnits;
        private System.Windows.Forms.NumericUpDown ndNum;
        private System.Windows.Forms.CheckBox chkSkipWindow;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkSkipHidden;
        private System.Windows.Forms.CheckBox chkSkipSystem;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chkSize;
        private System.Windows.Forms.CheckBox chkSkipRO;
        private System.Windows.Forms.CheckBox chkAttribute;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.CheckBox chkHash;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.CheckBox chkReason;
        private System.Windows.Forms.CheckBox chkDisplay;
        private System.Windows.Forms.Button btnDeleteLog;
        private System.Windows.Forms.CheckBox chkError;
        private System.Windows.Forms.CheckBox chkDrive;
        private System.Windows.Forms.CheckBox chkSkipTemp;
        private System.Windows.Forms.CheckBox chkFailed;
        private System.Windows.Forms.CheckBox chkAutoSync;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnSubDir;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;

    }
}