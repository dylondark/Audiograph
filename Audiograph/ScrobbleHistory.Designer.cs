using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmScrobbleHistory : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScrobbleHistory));
            btnClear = new Button();
            btnClear.Click += new EventHandler(ClearHistory);
            ltvHistory = new ListView();
            Title = new ColumnHeader();
            Artist = new ColumnHeader();
            Album = new ColumnHeader();
            Timestamp = new ColumnHeader();
            TimeSent = new ColumnHeader();
            Source = new ColumnHeader();
            Status = new ColumnHeader();
            btnClose = new Button();
            btnClose.Click += new EventHandler(ExitForm);
            tmrListUpdate = new Timer(components);
            tmrListUpdate.Tick += new EventHandler(AddListView);
            SuspendLayout();
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClear.Location = new Point(12, 526);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 5;
            btnClear.Text = "C&lear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // ltvHistory
            // 
            ltvHistory.Columns.AddRange(new ColumnHeader[] { Title, Artist, Album, Timestamp, TimeSent, Source, Status });
            ltvHistory.FullRowSelect = true;
            ltvHistory.GridLines = true;
            ltvHistory.HideSelection = false;
            ltvHistory.Location = new Point(12, 12);
            ltvHistory.Name = "ltvHistory";
            ltvHistory.Size = new Size(760, 508);
            ltvHistory.TabIndex = 4;
            ltvHistory.UseCompatibleStateImageBehavior = false;
            ltvHistory.View = View.Details;
            // 
            // Title
            // 
            Title.Text = "Title";
            Title.Width = 120;
            // 
            // Artist
            // 
            Artist.Text = "Artist";
            Artist.Width = 120;
            // 
            // Album
            // 
            Album.Text = "Album";
            Album.Width = 120;
            // 
            // Timestamp
            // 
            Timestamp.Text = "Timestamp";
            Timestamp.Width = 130;
            // 
            // TimeSent
            // 
            TimeSent.Text = "Time Sent";
            TimeSent.Width = 130;
            // 
            // Source
            // 
            Source.Text = "Source";
            // 
            // Status
            // 
            Status.Text = "Status";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(697, 526);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 3;
            btnClose.Text = "&Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // tmrListUpdate
            // 
            tmrListUpdate.Enabled = true;
            tmrListUpdate.Interval = 15;
            // 
            // frmScrobbleHistory
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(btnClear);
            Controls.Add(ltvHistory);
            Controls.Add(btnClose);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmScrobbleHistory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scrobble History";
            Resize += new EventHandler(ResizeOps);
            ResumeLayout(false);

        }

        internal Button btnClear;
        internal ListView ltvHistory;
        internal ColumnHeader Title;
        internal Button btnClose;
        internal ColumnHeader Artist;
        internal ColumnHeader Album;
        internal ColumnHeader Timestamp;
        internal ColumnHeader TimeSent;
        internal ColumnHeader Source;
        internal ColumnHeader Status;
        internal Timer tmrListUpdate;
    }
}