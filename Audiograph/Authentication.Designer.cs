using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmAuthentication : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuthentication));
            Label1 = new Label();
            PictureBox1 = new PictureBox();
            btnCancel = new Button();
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnOK = new Button();
            btnOK.Click += new EventHandler(btnOK_Click);
            lblInstruction = new Label();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(58, 21);
            Label1.Name = "Label1";
            Label1.Size = new Size(225, 21);
            Label1.TabIndex = 0;
            Label1.Text = "Authenticate your user account";
            // 
            // PictureBox1
            // 
            PictureBox1.Image = My.Resources.Resources.icon;
            PictureBox1.Location = new Point(12, 12);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(40, 40);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 1;
            PictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(12, 126);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(297, 126);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 3;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // lblInstruction
            // 
            lblInstruction.AutoSize = true;
            lblInstruction.Location = new Point(59, 60);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(56, 26);
            lblInstruction.TabIndex = 1;
            lblInstruction.Text = "Instruction" + '\r' + '\n' + "Line 2";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(59, 99);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(37, 13);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status" + '\r' + '\n';
            // 
            // frmAuthentication
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(384, 161);
            Controls.Add(lblStatus);
            Controls.Add(lblInstruction);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(PictureBox1);
            Controls.Add(Label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(400, 200);
            MinimizeBox = false;
            MinimumSize = new Size(400, 200);
            Name = "frmAuthentication";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Authentication";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(frmAuthentication_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label Label1;
        internal PictureBox PictureBox1;
        internal Button btnCancel;
        internal Button btnOK;
        internal Label lblInstruction;
        internal Label lblStatus;
    }
}