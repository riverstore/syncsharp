namespace SyncSharp
{
    partial class RenameTaskForm
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameTaskForm));
					this.txtNewName = new System.Windows.Forms.TextBox();
					this.label1 = new System.Windows.Forms.Label();
					this.btnCancel = new System.Windows.Forms.Button();
					this.btnAccept = new System.Windows.Forms.Button();
					this.SuspendLayout();
					// 
					// txtNewName
					// 
					this.txtNewName.Location = new System.Drawing.Point(12, 28);
					this.txtNewName.Name = "txtNewName";
					this.txtNewName.Size = new System.Drawing.Size(368, 21);
					this.txtNewName.TabIndex = 0;
					// 
					// label1
					// 
					this.label1.AutoSize = true;
					this.label1.Location = new System.Drawing.Point(12, 13);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(58, 13);
					this.label1.TabIndex = 1;
					this.label1.Text = "New Name";
					// 
					// btnCancel
					// 
					this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
					this.btnCancel.Location = new System.Drawing.Point(305, 55);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.Size = new System.Drawing.Size(75, 25);
					this.btnCancel.TabIndex = 3;
					this.btnCancel.Text = "Cancel";
					this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.btnCancel.UseVisualStyleBackColor = true;
					this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
					// 
					// btnAccept
					// 
					this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
					this.btnAccept.Location = new System.Drawing.Point(224, 55);
					this.btnAccept.Name = "btnAccept";
					this.btnAccept.Size = new System.Drawing.Size(75, 25);
					this.btnAccept.TabIndex = 2;
					this.btnAccept.Text = "OK";
					this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					this.btnAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
					this.btnAccept.UseVisualStyleBackColor = true;
					this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
					// 
					// RenameTaskForm
					// 
					this.AcceptButton = this.btnAccept;
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.CancelButton = this.btnCancel;
					this.ClientSize = new System.Drawing.Size(392, 91);
					this.Controls.Add(this.btnCancel);
					this.Controls.Add(this.btnAccept);
					this.Controls.Add(this.label1);
					this.Controls.Add(this.txtNewName);
					this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
					this.MaximizeBox = false;
					this.MinimizeBox = false;
					this.Name = "RenameTaskForm";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "Rename";
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;

    }
}