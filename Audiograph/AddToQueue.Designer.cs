using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmAddToQueue : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddToQueue));
            Label1 = new Label();
            btnOK = new Button();
            btnOK.Click += new EventHandler(OK);
            btnCancel = new Button();
            btnCancel.Click += new EventHandler(Cancel);
            txtLocation = new TextBox();
            btnBrowse = new Button();
            btnBrowse.Click += new EventHandler(Browse);
            PictureBox1 = new PictureBox();
            Label2 = new Label();
            OpenFileDialog = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(12, 67);
            Label1.Name = "Label1";
            Label1.Size = new Size(364, 13);
            Label1.TabIndex = 1;
            Label1.Text = "Please enter the location of a media file. Online and local files are accepted.";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(297, 126);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 5;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 126);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(12, 85);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(294, 20);
            txtLocation.TabIndex = 2;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(312, 83);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(60, 23);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "&Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = My.Resources.Resources.icon;
            PictureBox1.Location = new Point(12, 12);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(40, 40);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 6;
            PictureBox1.TabStop = false;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(58, 21);
            Label2.Name = "Label2";
            Label2.Size = new Size(150, 21);
            Label2.TabIndex = 0;
            Label2.Text = "Add media to queue";
            // 
            // OpenFileDialog
            // 
            OpenFileDialog.Filter = "Audio files|*.mp3;*.aac;*.flac;*.wav;*.wma;*.m4a;*.mid|Video files|*.mp4;*.mov;*." + "mpeg;*.mpg;*.avi;*.wmv|All files|*.*";
            OpenFileDialog.Multiselect = true;
            OpenFileDialog.Title = "Select file...";
            // 
            // frmAddToQueue
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 161);
            Controls.Add(PictureBox1);
            Controls.Add(Label2);
            Controls.Add(btnBrowse);
            Controls.Add(txtLocation);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(Label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(400, 200);
            MinimizeBox = false;
            MinimumSize = new Size(400, 200);
            Name = "frmAddToQueue";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add Media";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label Label1;
        internal Button btnOK;
        internal Button btnCancel;
        internal TextBox txtLocation;
        internal Button btnBrowse;
        internal PictureBox PictureBox1;
        internal Label Label2;
        internal OpenFileDialog OpenFileDialog;
    }
}