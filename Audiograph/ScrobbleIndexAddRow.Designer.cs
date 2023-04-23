using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmScrobbleIndexAddRow : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScrobbleIndexAddRow));
            GroupBox1 = new GroupBox();
            btnBrowse = new Button();
            btnBrowse.Click += new EventHandler(Browse);
            txtFilename = new TextBox();
            Label1 = new Label();
            GroupBox2 = new GroupBox();
            btnVerify = new Button();
            btnVerify.Click += new EventHandler(Verify);
            btnSearch = new Button();
            btnSearch.Click += new EventHandler(Search);
            txtAlbum = new TextBox();
            Label4 = new Label();
            txtArtist = new TextBox();
            Label3 = new Label();
            txtTitle = new TextBox();
            Label2 = new Label();
            btnOK = new Button();
            btnOK.Click += new EventHandler(OK);
            btnCancel = new Button();
            btnCancel.Click += new EventHandler(Cancel);
            ofdBrowse = new OpenFileDialog();
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(btnBrowse);
            GroupBox1.Controls.Add(txtFilename);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Location = new Point(3, 3);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(378, 50);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Filename";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(308, 17);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(61, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "&Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtFilename
            // 
            txtFilename.Location = new Point(59, 18);
            txtFilename.Name = "txtFilename";
            txtFilename.Size = new Size(243, 20);
            txtFilename.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(9, 21);
            Label1.Name = "Label1";
            Label1.Size = new Size(53, 13);
            Label1.TabIndex = 0;
            Label1.Text = "File/URL:";
            // 
            // GroupBox2
            // 
            GroupBox2.Controls.Add(btnVerify);
            GroupBox2.Controls.Add(btnSearch);
            GroupBox2.Controls.Add(txtAlbum);
            GroupBox2.Controls.Add(Label4);
            GroupBox2.Controls.Add(txtArtist);
            GroupBox2.Controls.Add(Label3);
            GroupBox2.Controls.Add(txtTitle);
            GroupBox2.Controls.Add(Label2);
            GroupBox2.Location = new Point(3, 59);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(378, 136);
            GroupBox2.TabIndex = 1;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "LFM";
            // 
            // btnVerify
            // 
            btnVerify.Location = new Point(278, 104);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new Size(91, 23);
            btnVerify.TabIndex = 7;
            btnVerify.Text = "&Verify with LFM";
            btnVerify.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(197, 104);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "&Search LFM";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtAlbum
            // 
            txtAlbum.Location = new Point(45, 69);
            txtAlbum.Name = "txtAlbum";
            txtAlbum.Size = new Size(324, 20);
            txtAlbum.TabIndex = 5;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(9, 72);
            Label4.Name = "Label4";
            Label4.Size = new Size(39, 13);
            Label4.TabIndex = 4;
            Label4.Text = "Album:";
            // 
            // txtArtist
            // 
            txtArtist.Location = new Point(39, 43);
            txtArtist.Name = "txtArtist";
            txtArtist.Size = new Size(330, 20);
            txtArtist.TabIndex = 3;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(9, 46);
            Label3.Name = "Label3";
            Label3.Size = new Size(33, 13);
            Label3.TabIndex = 2;
            Label3.Text = "Artist:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(36, 17);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(333, 20);
            txtTitle.TabIndex = 1;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(9, 20);
            Label2.Name = "Label2";
            Label2.Size = new Size(30, 13);
            Label2.TabIndex = 0;
            Label2.Text = "Title:";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(297, 205);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 3;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(12, 205);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ofdBrowse
            // 
            ofdBrowse.Filter = "All Files|*.*";
            ofdBrowse.Title = "Browse for file...";
            // 
            // frmScrobbleIndexAddRow
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(384, 240);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(400, 279);
            MinimizeBox = false;
            MinimumSize = new Size(400, 279);
            Name = "frmScrobbleIndexAddRow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scrobble Index Editor - Add Row";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            Closing += new System.ComponentModel.CancelEventHandler(FormClose);
            ResumeLayout(false);

        }

        internal GroupBox GroupBox1;
        internal Button btnBrowse;
        internal TextBox txtFilename;
        internal Label Label1;
        internal GroupBox GroupBox2;
        internal Button btnSearch;
        internal TextBox txtAlbum;
        internal Label Label4;
        internal TextBox txtArtist;
        internal Label Label3;
        internal TextBox txtTitle;
        internal Label Label2;
        internal Button btnOK;
        internal Button btnCancel;
        internal OpenFileDialog ofdBrowse;
        internal Button btnVerify;
    }
}