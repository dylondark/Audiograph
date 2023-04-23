using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmArtistAdvanced : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArtistAdvanced));
            btnCancel = new Button();
            btnCancel.Click += new EventHandler(Cancel);
            txtListeners = new TextBox();
            Label8 = new Label();
            txtArtist = new TextBox();
            txtArtist.KeyDown += new KeyEventHandler(UserType);
            Label4 = new Label();
            txtSearchArtist = new TextBox();
            Label5 = new Label();
            txtMBID = new TextBox();
            btnMBID = new Button();
            btnMBID.Click += new EventHandler(MBID);
            Label2 = new Label();
            ltvResults = new ListView();
            ltvResults.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(SelectSearchItem);
            ltvResults.ItemActivate += new EventHandler(DoubleClickItem);
            ColumnHeader2 = new ColumnHeader();
            ColumnHeader3 = new ColumnHeader();
            btnSearch = new Button();
            btnSearch.Click += new EventHandler(Search);
            btnOK = new Button();
            btnOK.Click += new EventHandler(OK);
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(464, 428);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 23);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtListeners
            // 
            txtListeners.Location = new Point(435, 81);
            txtListeners.Name = "txtListeners";
            txtListeners.ReadOnly = true;
            txtListeners.Size = new Size(185, 20);
            txtListeners.TabIndex = 12;
            txtListeners.Text = "N/A";
            txtListeners.TextAlign = HorizontalAlignment.Center;
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8.Location = new Point(433, 57);
            Label8.Name = "Label8";
            Label8.Size = new Size(72, 21);
            Label8.TabIndex = 11;
            Label8.Text = "Listeners";
            // 
            // txtArtist
            // 
            txtArtist.Location = new Point(435, 31);
            txtArtist.Name = "txtArtist";
            txtArtist.Size = new Size(185, 20);
            txtArtist.TabIndex = 10;
            txtArtist.Text = "N/A";
            txtArtist.TextAlign = HorizontalAlignment.Center;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.Location = new Point(432, 6);
            Label4.Name = "Label4";
            Label4.Size = new Size(47, 21);
            Label4.TabIndex = 9;
            Label4.Text = "Artist";
            // 
            // txtSearchArtist
            // 
            txtSearchArtist.Location = new Point(41, 7);
            txtSearchArtist.Name = "txtSearchArtist";
            txtSearchArtist.Size = new Size(310, 20);
            txtSearchArtist.TabIndex = 1;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(11, 10);
            Label5.Name = "Label5";
            Label5.Size = new Size(36, 13);
            Label5.TabIndex = 0;
            Label5.Text = "Artist: ";
            // 
            // txtMBID
            // 
            txtMBID.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtMBID.Location = new Point(46, 429);
            txtMBID.Name = "txtMBID";
            txtMBID.Size = new Size(293, 20);
            txtMBID.TabIndex = 5;
            // 
            // btnMBID
            // 
            btnMBID.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnMBID.Location = new Point(345, 428);
            btnMBID.Name = "btnMBID";
            btnMBID.Size = new Size(76, 23);
            btnMBID.TabIndex = 6;
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
            Label2.TabIndex = 4;
            Label2.Text = "MBID: ";
            // 
            // ltvResults
            // 
            ltvResults.Columns.AddRange(new ColumnHeader[] { ColumnHeader2, ColumnHeader3 });
            ltvResults.FullRowSelect = true;
            ltvResults.HideSelection = false;
            ltvResults.Location = new Point(12, 35);
            ltvResults.Name = "ltvResults";
            ltvResults.Size = new Size(408, 387);
            ltvResults.TabIndex = 3;
            ltvResults.UseCompatibleStateImageBehavior = false;
            ltvResults.View = View.Details;
            // 
            // ColumnHeader2
            // 
            ColumnHeader2.Text = "Artist";
            ColumnHeader2.Width = 280;
            // 
            // ColumnHeader3
            // 
            ColumnHeader3.Text = "Listeners";
            ColumnHeader3.Width = 100;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(357, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(63, 23);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "&Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(555, 428);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(67, 23);
            btnOK.TabIndex = 8;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // frmArtistAdvanced
            // 
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 461);
            Controls.Add(btnCancel);
            Controls.Add(txtListeners);
            Controls.Add(Label8);
            Controls.Add(txtArtist);
            Controls.Add(Label4);
            Controls.Add(txtSearchArtist);
            Controls.Add(Label5);
            Controls.Add(txtMBID);
            Controls.Add(btnMBID);
            Controls.Add(Label2);
            Controls.Add(ltvResults);
            Controls.Add(btnSearch);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmArtistAdvanced";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Advanced Search";
            ResumeLayout(false);
            PerformLayout();

        }

        internal Button btnCancel;
        internal TextBox txtListeners;
        internal Label Label8;
        internal TextBox txtArtist;
        internal Label Label4;
        internal TextBox txtSearchArtist;
        internal Label Label5;
        internal TextBox txtMBID;
        internal Button btnMBID;
        internal Label Label2;
        internal ListView ltvResults;
        internal ColumnHeader ColumnHeader2;
        internal ColumnHeader ColumnHeader3;
        internal Button btnSearch;
        internal Button btnOK;
    }
}