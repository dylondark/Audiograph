using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmScrobbleSearch : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScrobbleSearch));
            btnOK = new Button();
            btnOK.Click += new EventHandler(OK);
            txtSearchArtist = new TextBox();
            Label5 = new Label();
            txtMBID = new TextBox();
            btnMBID = new Button();
            btnMBID.Click += new EventHandler(MBID);
            Label2 = new Label();
            txtSearchTrack = new TextBox();
            Label1 = new Label();
            ltvResults = new ListView();
            ltvResults.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(SelectSearchItem);
            ltvResults.ItemActivate += new EventHandler(DoubleClickItem);
            ColumnHeader1 = new ColumnHeader();
            ColumnHeader2 = new ColumnHeader();
            ColumnHeader3 = new ColumnHeader();
            btnSearch = new Button();
            btnSearch.Click += new EventHandler(Search);
            Label3 = new Label();
            txtTrack = new TextBox();
            txtTrack.KeyDown += new KeyEventHandler(UserType);
            txtArtist = new TextBox();
            txtArtist.KeyDown += new KeyEventHandler(UserType);
            Label4 = new Label();
            txtAlbum = new TextBox();
            Label6 = new Label();
            Label7 = new Label();
            txtPlaycount = new TextBox();
            Label8 = new Label();
            btnCancel = new Button();
            btnCancel.Click += new EventHandler(Cancel);
            picArt = new PictureBox();
            picArt.Click += new EventHandler(ArtClicked);
            ((System.ComponentModel.ISupportInitialize)picArt).BeginInit();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(555, 428);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(67, 23);
            btnOK.TabIndex = 10;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // txtSearchArtist
            // 
            txtSearchArtist.Location = new Point(215, 7);
            txtSearchArtist.Name = "txtSearchArtist";
            txtSearchArtist.Size = new Size(136, 20);
            txtSearchArtist.TabIndex = 3;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(185, 11);
            Label5.Name = "Label5";
            Label5.Size = new Size(36, 13);
            Label5.TabIndex = 2;
            Label5.Text = "Artist: ";
            // 
            // txtMBID
            // 
            txtMBID.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtMBID.Location = new Point(46, 429);
            txtMBID.Name = "txtMBID";
            txtMBID.Size = new Size(293, 20);
            txtMBID.TabIndex = 7;
            // 
            // btnMBID
            // 
            btnMBID.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnMBID.Location = new Point(345, 428);
            btnMBID.Name = "btnMBID";
            btnMBID.Size = new Size(76, 23);
            btnMBID.TabIndex = 8;
            btnMBID.Text = "&Lookup";
            btnMBID.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            Label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Label2.AutoSize = true;
            Label2.Location = new Point(12, 432);
            Label2.Name = "Label2";
            Label2.Size = new Size(40, 13);
            Label2.TabIndex = 6;
            Label2.Text = "MBID: ";
            // 
            // txtSearchTrack
            // 
            txtSearchTrack.Location = new Point(46, 7);
            txtSearchTrack.Name = "txtSearchTrack";
            txtSearchTrack.Size = new Size(136, 20);
            txtSearchTrack.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(11, 10);
            Label1.Name = "Label1";
            Label1.Size = new Size(38, 13);
            Label1.TabIndex = 0;
            Label1.Text = "Track:";
            // 
            // ltvResults
            // 
            ltvResults.Columns.AddRange(new ColumnHeader[] { ColumnHeader1, ColumnHeader2, ColumnHeader3 });
            ltvResults.FullRowSelect = true;
            ltvResults.HideSelection = false;
            ltvResults.Location = new Point(12, 35);
            ltvResults.Name = "ltvResults";
            ltvResults.Size = new Size(408, 387);
            ltvResults.TabIndex = 5;
            ltvResults.UseCompatibleStateImageBehavior = false;
            ltvResults.View = View.Details;
            // 
            // ColumnHeader1
            // 
            ColumnHeader1.Text = "Title";
            ColumnHeader1.Width = 190;
            // 
            // ColumnHeader2
            // 
            ColumnHeader2.Text = "Artist";
            ColumnHeader2.Width = 117;
            // 
            // ColumnHeader3
            // 
            ColumnHeader3.Text = "Listeners";
            ColumnHeader3.Width = 76;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(357, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(63, 23);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "&Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(432, 6);
            Label3.Name = "Label3";
            Label3.Size = new Size(39, 21);
            Label3.TabIndex = 11;
            Label3.Text = "Title";
            // 
            // txtTrack
            // 
            txtTrack.Location = new Point(435, 31);
            txtTrack.Name = "txtTrack";
            txtTrack.Size = new Size(185, 20);
            txtTrack.TabIndex = 12;
            txtTrack.Text = "N/A";
            txtTrack.TextAlign = HorizontalAlignment.Center;
            // 
            // txtArtist
            // 
            txtArtist.Location = new Point(436, 82);
            txtArtist.Name = "txtArtist";
            txtArtist.Size = new Size(185, 20);
            txtArtist.TabIndex = 14;
            txtArtist.Text = "N/A";
            txtArtist.TextAlign = HorizontalAlignment.Center;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(433, 57);
            Label4.Name = "Label4";
            Label4.Size = new Size(47, 21);
            Label4.TabIndex = 13;
            Label4.Text = "Artist";
            // 
            // txtAlbum
            // 
            txtAlbum.Location = new Point(437, 134);
            txtAlbum.Name = "txtAlbum";
            txtAlbum.ReadOnly = true;
            txtAlbum.Size = new Size(185, 20);
            txtAlbum.TabIndex = 16;
            txtAlbum.Text = "N/A";
            txtAlbum.TextAlign = HorizontalAlignment.Center;
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.Location = new Point(434, 109);
            Label6.Name = "Label6";
            Label6.Size = new Size(56, 21);
            Label6.TabIndex = 15;
            Label6.Text = "Album";
            // 
            // Label7
            // 
            Label7.AutoSize = true;
            Label7.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.Location = new Point(436, 212);
            Label7.Name = "Label7";
            Label7.Size = new Size(31, 21);
            Label7.TabIndex = 19;
            Label7.Text = "Art";
            // 
            // txtPlaycount
            // 
            txtPlaycount.Location = new Point(437, 186);
            txtPlaycount.Name = "txtPlaycount";
            txtPlaycount.ReadOnly = true;
            txtPlaycount.Size = new Size(185, 20);
            txtPlaycount.TabIndex = 18;
            txtPlaycount.Text = "N/A";
            txtPlaycount.TextAlign = HorizontalAlignment.Center;
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8.Location = new Point(434, 161);
            Label8.Name = "Label8";
            Label8.Size = new Size(114, 21);
            Label8.TabIndex = 17;
            Label8.Text = "Your Playcount";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(464, 428);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 23);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // picArt
            // 
            picArt.Cursor = Cursors.Hand;
            picArt.ErrorImage = My.Resources.Resources.imageerror;
            picArt.Image = My.Resources.Resources.imageunavailable;
            picArt.ImageLocation = "";
            picArt.InitialImage = My.Resources.Resources.imageunavailable;
            picArt.Location = new Point(438, 236);
            picArt.Name = "picArt";
            picArt.Size = new Size(186, 186);
            picArt.SizeMode = PictureBoxSizeMode.StretchImage;
            picArt.TabIndex = 25;
            picArt.TabStop = false;
            // 
            // frmScrobbleSearch
            // 
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(634, 461);
            Controls.Add(btnCancel);
            Controls.Add(txtPlaycount);
            Controls.Add(Label8);
            Controls.Add(picArt);
            Controls.Add(Label7);
            Controls.Add(txtAlbum);
            Controls.Add(Label6);
            Controls.Add(txtArtist);
            Controls.Add(Label4);
            Controls.Add(txtTrack);
            Controls.Add(Label3);
            Controls.Add(txtSearchArtist);
            Controls.Add(Label5);
            Controls.Add(txtMBID);
            Controls.Add(btnMBID);
            Controls.Add(Label2);
            Controls.Add(txtSearchTrack);
            Controls.Add(Label1);
            Controls.Add(ltvResults);
            Controls.Add(btnSearch);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(650, 500);
            MinimumSize = new Size(650, 500);
            Name = "frmScrobbleSearch";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Search for a Track";
            ((System.ComponentModel.ISupportInitialize)picArt).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        internal Button btnOK;
        internal TextBox txtSearchArtist;
        internal Label Label5;
        internal TextBox txtMBID;
        internal Button btnMBID;
        internal Label Label2;
        internal TextBox txtSearchTrack;
        internal Label Label1;
        internal ListView ltvResults;
        internal ColumnHeader ColumnHeader1;
        internal ColumnHeader ColumnHeader2;
        internal ColumnHeader ColumnHeader3;
        internal Button btnSearch;
        internal Label Label3;
        internal TextBox txtTrack;
        internal TextBox txtArtist;
        internal Label Label4;
        internal TextBox txtAlbum;
        internal Label Label6;
        internal Label Label7;
        internal PictureBox picArt;
        internal TextBox txtPlaycount;
        internal Label Label8;
        internal Button btnCancel;
    }
}