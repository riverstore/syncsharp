namespace SyncSharp.GUI
{
    partial class ViewLog
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLog));
					this.splitContainer1 = new System.Windows.Forms.SplitContainer();
					this.txtLog = new System.Windows.Forms.TextBox();
					this.splitContainer2 = new System.Windows.Forms.SplitContainer();
					this.btnClear = new System.Windows.Forms.Button();
					this.btnCancel = new System.Windows.Forms.Button();
					this.splitContainer1.Panel1.SuspendLayout();
					this.splitContainer1.Panel2.SuspendLayout();
					this.splitContainer1.SuspendLayout();
					this.splitContainer2.Panel2.SuspendLayout();
					this.splitContainer2.SuspendLayout();
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
					this.splitContainer1.Panel1.Controls.Add(this.txtLog);
					// 
					// splitContainer1.Panel2
					// 
					this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
					this.splitContainer1.Size = new System.Drawing.Size(534, 414);
					this.splitContainer1.SplitterDistance = 360;
					this.splitContainer1.TabIndex = 0;
					// 
					// txtLog
					// 
					this.txtLog.BackColor = System.Drawing.SystemColors.Window;
					this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
					this.txtLog.Font = new System.Drawing.Font("Arial", 8.5F);
					this.txtLog.ForeColor = System.Drawing.Color.Black;
					this.txtLog.Location = new System.Drawing.Point(0, 0);
					this.txtLog.Multiline = true;
					this.txtLog.Name = "txtLog";
					this.txtLog.ReadOnly = true;
					this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
					this.txtLog.Size = new System.Drawing.Size(534, 360);
					this.txtLog.TabIndex = 0;
					this.txtLog.WordWrap = false;
					// 
					// splitContainer2
					// 
					this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
					this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
					this.splitContainer2.IsSplitterFixed = true;
					this.splitContainer2.Location = new System.Drawing.Point(0, 0);
					this.splitContainer2.Name = "splitContainer2";
					// 
					// splitContainer2.Panel2
					// 
					this.splitContainer2.Panel2.Controls.Add(this.btnClear);
					this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
					this.splitContainer2.Size = new System.Drawing.Size(534, 50);
					this.splitContainer2.SplitterDistance = 330;
					this.splitContainer2.TabIndex = 0;
					// 
					// btnClear
					// 
					this.btnClear.Location = new System.Drawing.Point(32, 3);
					this.btnClear.Name = "btnClear";
					this.btnClear.Size = new System.Drawing.Size(75, 25);
					this.btnClear.TabIndex = 0;
					this.btnClear.Text = "Clear Log";
					this.btnClear.UseVisualStyleBackColor = true;
					this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
					// 
					// btnCancel
					// 
					this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.btnCancel.Location = new System.Drawing.Point(113, 3);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.Size = new System.Drawing.Size(75, 25);
					this.btnCancel.TabIndex = 0;
					this.btnCancel.Text = "Cancel";
					this.btnCancel.UseVisualStyleBackColor = true;
					// 
					// ViewLog
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.CancelButton = this.btnCancel;
					this.ClientSize = new System.Drawing.Size(534, 414);
					this.Controls.Add(this.splitContainer1);
					this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.Name = "ViewLog";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
					this.Text = "View Log";
					this.Load += new System.EventHandler(this.ViewLog_Load);
					this.splitContainer1.Panel1.ResumeLayout(false);
					this.splitContainer1.Panel1.PerformLayout();
					this.splitContainer1.Panel2.ResumeLayout(false);
					this.splitContainer1.ResumeLayout(false);
					this.splitContainer2.Panel2.ResumeLayout(false);
					this.splitContainer2.ResumeLayout(false);
					this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}