using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmAbout : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            PictureBox1 = new PictureBox();
            Label1 = new Label();
            Label2 = new Label();
            btnOK = new Button();
            btnOK.Click += new EventHandler(ExitForm);
            txtAbout = new RichTextBox();
            txtAbout.LinkClicked += new LinkClickedEventHandler(LinkClicked);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(12, 12);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(75, 75);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Nirmala UI", 27.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.Location = new Point(89, 10);
            Label1.Name = "Label1";
            Label1.Size = new Size(217, 50);
            Label1.TabIndex = 1;
            Label1.Text = "Audiograph";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.Location = new Point(93, 60);
            Label2.Name = "Label2";
            Label2.Size = new Size(282, 21);
            Label2.TabIndex = 2;
            Label2.Text = "Release 1.0.0 - Dylan Miller (dylondark)";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(377, 286);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 4;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // txtAbout
            // 
            txtAbout.BackColor = SystemColors.ControlLightLight;
            txtAbout.BorderStyle = BorderStyle.FixedSingle;
            txtAbout.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAbout.Location = new Point(97, 98);
            txtAbout.Name = "txtAbout";
            txtAbout.ReadOnly = true;
            txtAbout.Size = new Size(355, 182);
            txtAbout.TabIndex = 5;
            txtAbout.Text = "";
            // 
            // frmAbout
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 321);
            Controls.Add(txtAbout);
            Controls.Add(btnOK);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(PictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(480, 360);
            MinimumSize = new Size(480, 360);
            Name = "frmAbout";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About Audiograph";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Load += new EventHandler(FormLoad);
            ResumeLayout(false);
            PerformLayout();

        }

        internal PictureBox PictureBox1;
        internal Label Label1;
        internal Label Label2;
        internal Button btnOK;
        internal RichTextBox txtAbout;
    }
}