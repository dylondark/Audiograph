using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmConsole : Form
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsole));
            txtOutput = new TextBox();
            txtInput = new TextBox();
            txtInput.KeyDown += new KeyEventHandler(History);
            btnSend = new Button();
            btnSend.Click += new EventHandler(Send);
            tmrSecret = new Timer(components);
            tmrSecret.Tick += new EventHandler(tmrSecret_Tick);
            SuspendLayout();
            // 
            // txtOutput
            // 
            txtOutput.BackColor = SystemColors.MenuText;
            txtOutput.BorderStyle = BorderStyle.FixedSingle;
            txtOutput.Font = new Font("Consolas", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOutput.ForeColor = SystemColors.Window;
            txtOutput.Location = new Point(12, 12);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(680, 390);
            txtOutput.TabIndex = 2;
            txtOutput.Text = "Audiograph Release 1.0.1 ©2023 Dylan Miller (dylondark)" + '\r' + '\n' + "Type 'help' for command " + "list" + '\r' + '\n' + '\r' + '\n' + "user>";
            // 
            // txtInput
            // 
            txtInput.AcceptsReturn = true;
            txtInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtInput.Font = new Font("Consolas", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInput.Location = new Point(13, 409);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(598, 20);
            txtInput.TabIndex = 0;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSend.Location = new Point(617, 408);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 1;
            btnSend.Text = "&Send";
            btnSend.UseVisualStyleBackColor = true;
            // 
            // tmrSecret
            // 
            tmrSecret.Interval = 1000;
            // 
            // frmConsole
            // 
            AcceptButton = btnSend;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(704, 441);
            Controls.Add(btnSend);
            Controls.Add(txtInput);
            Controls.Add(txtOutput);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmConsole";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Developer Console";
            Resize += new EventHandler(ResizeOps);
            ResumeLayout(false);
            PerformLayout();

        }

        internal TextBox txtOutput;
        internal TextBox txtInput;
        internal Button btnSend;
        internal Timer tmrSecret;
    }
}