using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Audiograph
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmScrobbleIndexEditor : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScrobbleIndexEditor));
            dgvData = new DataGridView();
            dgvData.CellEndEdit += new DataGridViewCellEventHandler(Modified);
            dgvData.SelectionChanged += new EventHandler(Enable);
            dgvData.Click += new EventHandler(DisableFind);
            Filename = new DataGridViewTextBoxColumn();
            Title = new DataGridViewTextBoxColumn();
            Artist = new DataGridViewTextBoxColumn();
            Album = new DataGridViewTextBoxColumn();
            ToolStrip = new ToolStrip();
            btnNew = new ToolStripButton();
            btnNew.Click += new EventHandler(ClickNew);
            btnOpen = new ToolStripButton();
            btnOpen.Click += new EventHandler(ClickOpen);
            btnSave = new ToolStripButton();
            btnSave.Click += new EventHandler(ClickSave);
            btnSaveAs = new ToolStripButton();
            btnSaveAs.Click += new EventHandler(ClickSaveAs);
            btnSetIndex = new ToolStripButton();
            btnSetIndex.Click += new EventHandler(SetIndexClick);
            ToolStripSeparator1 = new ToolStripSeparator();
            btnAddRow = new ToolStripButton();
            btnAddRow.Click += new EventHandler(ClickAddRow);
            ToolStripSeparator7 = new ToolStripSeparator();
            btnReload = new ToolStripButton();
            btnReload.Click += new EventHandler(ClickReload);
            btnEdit = new ToolStripDropDownButton();
            btnCopyCell = new ToolStripMenuItem();
            btnCopyCell.Click += new EventHandler(ClickCopyCell);
            btnCopyRow = new ToolStripMenuItem();
            btnCopyRow.Click += new EventHandler(ClickCopyRow);
            ToolStripSeparator8 = new ToolStripSeparator();
            btnCutCell = new ToolStripMenuItem();
            btnCutCell.Click += new EventHandler(ClickCutCell);
            btnCutRow = new ToolStripMenuItem();
            btnCutRow.Click += new EventHandler(ClickCutRow);
            ToolStripSeparator9 = new ToolStripSeparator();
            btnPasteCell = new ToolStripMenuItem();
            btnPasteCell.Click += new EventHandler(ClickPasteCell);
            ToolStripSeparator10 = new ToolStripSeparator();
            btnDeleteCell = new ToolStripMenuItem();
            btnDeleteCell.Click += new EventHandler(ClickDeleteCell);
            btnDeleteRow = new ToolStripMenuItem();
            btnDeleteRow.Click += new EventHandler(ClickDeleteRow);
            ToolStripSeparator3 = new ToolStripSeparator();
            btnFind = new ToolStripButton();
            btnFind.Click += new EventHandler(ClickFind);
            txtFind = new ToolStripTextBox();
            lblFind = new ToolStripLabel();
            ToolStripSeparator2 = new ToolStripSeparator();
            btnVerifyRow = new ToolStripButton();
            btnVerifyRow.Click += new EventHandler(ClickVerifyRow);
            btnVerifyFile = new ToolStripButton();
            btnVerifyFile.Click += new EventHandler(ClickVerifyFile);
            prgVerify = new ToolStripProgressBar();
            lblVerify = new ToolStripLabel();
            ofdOpen = new OpenFileDialog();
            sfdSaveAs = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            ToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Filename, Title, Artist, Album });
            dgvData.Location = new Point(0, 28);
            dgvData.Name = "dgvData";
            dgvData.Size = new Size(800, 422);
            dgvData.TabIndex = 0;
            // 
            // Filename
            // 
            Filename.HeaderText = "Filename";
            Filename.Name = "Filename";
            Filename.Width = 74;
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.Name = "Title";
            Title.Width = 52;
            // 
            // Artist
            // 
            Artist.HeaderText = "Artist";
            Artist.Name = "Artist";
            Artist.Width = 55;
            // 
            // Album
            // 
            Album.HeaderText = "Album";
            Album.Name = "Album";
            Album.Width = 61;
            // 
            // ToolStrip
            // 
            ToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            ToolStrip.Items.AddRange(new ToolStripItem[] { btnNew, btnOpen, btnSave, btnSaveAs, btnSetIndex, ToolStripSeparator1, btnAddRow, ToolStripSeparator7, btnReload, btnEdit, ToolStripSeparator3, btnFind, txtFind, lblFind, ToolStripSeparator2, btnVerifyRow, btnVerifyFile, prgVerify, lblVerify });
            ToolStrip.Location = new Point(0, 0);
            ToolStrip.Name = "ToolStrip";
            ToolStrip.Size = new Size(800, 25);
            ToolStrip.TabIndex = 1;
            ToolStrip.Text = "ToolStrip1";
            // 
            // btnNew
            // 
            btnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNew.Image = My.Resources.Resources.New_File;
            btnNew.ImageTransparentColor = Color.Magenta;
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(23, 22);
            btnNew.Text = "ToolStripButton1";
            btnNew.ToolTipText = "New File (Ctrl+N)";
            // 
            // btnOpen
            // 
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpen.Image = My.Resources.Resources.Open_File;
            btnOpen.ImageTransparentColor = Color.Magenta;
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(23, 22);
            btnOpen.Text = "ToolStripButton1";
            btnOpen.ToolTipText = "Open File (Ctrl+O)";
            // 
            // btnSave
            // 
            btnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSave.Enabled = false;
            btnSave.Image = My.Resources.Resources.Save_File;
            btnSave.ImageTransparentColor = Color.Magenta;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(23, 22);
            btnSave.Text = "ToolStripButton1";
            btnSave.ToolTipText = "Save File (Ctrl+S)";
            // 
            // btnSaveAs
            // 
            btnSaveAs.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSaveAs.Image = My.Resources.Resources.Save_As;
            btnSaveAs.ImageTransparentColor = Color.Magenta;
            btnSaveAs.Name = "btnSaveAs";
            btnSaveAs.Size = new Size(23, 22);
            btnSaveAs.Text = "ToolStripButton1";
            btnSaveAs.ToolTipText = "Save File As (Ctrl+Shift+S)";
            // 
            // btnSetIndex
            // 
            btnSetIndex.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSetIndex.Image = My.Resources.Resources.Set_Index;
            btnSetIndex.ImageTransparentColor = Color.Magenta;
            btnSetIndex.Name = "btnSetIndex";
            btnSetIndex.Size = new Size(23, 22);
            btnSetIndex.Text = "ToolStripButton1";
            btnSetIndex.ToolTipText = "Set As Current Scrobble Index";
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(6, 25);
            // 
            // btnAddRow
            // 
            btnAddRow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddRow.Image = My.Resources.Resources.Add_Row;
            btnAddRow.ImageTransparentColor = Color.Magenta;
            btnAddRow.Name = "btnAddRow";
            btnAddRow.Size = new Size(23, 22);
            btnAddRow.Text = "ToolStripButton1";
            btnAddRow.ToolTipText = "Add Row From File (Ctrl+Shift+A)";
            // 
            // ToolStripSeparator7
            // 
            ToolStripSeparator7.Name = "ToolStripSeparator7";
            ToolStripSeparator7.Size = new Size(6, 25);
            // 
            // btnReload
            // 
            btnReload.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnReload.Image = My.Resources.Resources.reload;
            btnReload.ImageTransparentColor = Color.Magenta;
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(23, 22);
            btnReload.Text = "ToolStripButton1";
            btnReload.ToolTipText = "Reload From File (Ctrl+R)";
            // 
            // btnEdit
            // 
            btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEdit.DropDownItems.AddRange(new ToolStripItem[] { btnCopyCell, btnCopyRow, ToolStripSeparator8, btnCutCell, btnCutRow, ToolStripSeparator9, btnPasteCell, ToolStripSeparator10, btnDeleteCell, btnDeleteRow });
            btnEdit.Image = My.Resources.Resources.Pencil;
            btnEdit.ImageTransparentColor = Color.Magenta;
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(29, 22);
            btnEdit.Text = "ToolStripDropDownButton4";
            btnEdit.ToolTipText = "Edit";
            // 
            // btnCopyCell
            // 
            btnCopyCell.Name = "btnCopyCell";
            btnCopyCell.Size = new Size(149, 22);
            btnCopyCell.Text = "Copy Cell(s)";
            // 
            // btnCopyRow
            // 
            btnCopyRow.Name = "btnCopyRow";
            btnCopyRow.Size = new Size(149, 22);
            btnCopyRow.Text = "Copy Row(s)";
            // 
            // ToolStripSeparator8
            // 
            ToolStripSeparator8.Name = "ToolStripSeparator8";
            ToolStripSeparator8.Size = new Size(146, 6);
            // 
            // btnCutCell
            // 
            btnCutCell.Name = "btnCutCell";
            btnCutCell.Size = new Size(149, 22);
            btnCutCell.Text = "Cut Cell(s)";
            // 
            // btnCutRow
            // 
            btnCutRow.Name = "btnCutRow";
            btnCutRow.Size = new Size(149, 22);
            btnCutRow.Text = "Cut Row(s)";
            // 
            // ToolStripSeparator9
            // 
            ToolStripSeparator9.Name = "ToolStripSeparator9";
            ToolStripSeparator9.Size = new Size(146, 6);
            // 
            // btnPasteCell
            // 
            btnPasteCell.Name = "btnPasteCell";
            btnPasteCell.Size = new Size(149, 22);
            btnPasteCell.Text = "Paste Into Cell";
            // 
            // ToolStripSeparator10
            // 
            ToolStripSeparator10.Name = "ToolStripSeparator10";
            ToolStripSeparator10.Size = new Size(146, 6);
            // 
            // btnDeleteCell
            // 
            btnDeleteCell.Name = "btnDeleteCell";
            btnDeleteCell.Size = new Size(149, 22);
            btnDeleteCell.Text = "Delete Cell(s)";
            // 
            // btnDeleteRow
            // 
            btnDeleteRow.Name = "btnDeleteRow";
            btnDeleteRow.Size = new Size(149, 22);
            btnDeleteRow.Text = "Delete Row(s)";
            // 
            // ToolStripSeparator3
            // 
            ToolStripSeparator3.Name = "ToolStripSeparator3";
            ToolStripSeparator3.Size = new Size(6, 25);
            // 
            // btnFind
            // 
            btnFind.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFind.Image = My.Resources.Resources.Find;
            btnFind.ImageTransparentColor = Color.Magenta;
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(23, 22);
            btnFind.Text = "ToolStripButton1";
            btnFind.ToolTipText = "Find (Ctrl+F)";
            // 
            // txtFind
            // 
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(100, 25);
            txtFind.ToolTipText = "Type text to find...";
            txtFind.Visible = false;
            // 
            // lblFind
            // 
            lblFind.Name = "lblFind";
            lblFind.Size = new Size(30, 22);
            lblFind.Text = "Find";
            lblFind.Visible = false;
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(6, 25);
            // 
            // btnVerifyRow
            // 
            btnVerifyRow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnVerifyRow.Image = (Image)resources.GetObject("btnVerifyRow.Image");
            btnVerifyRow.ImageTransparentColor = Color.Magenta;
            btnVerifyRow.Name = "btnVerifyRow";
            btnVerifyRow.Size = new Size(23, 22);
            btnVerifyRow.Text = "ToolStripButton1";
            btnVerifyRow.ToolTipText = "Verify Selected Row With LFM";
            // 
            // btnVerifyFile
            // 
            btnVerifyFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnVerifyFile.Image = My.Resources.Resources.Verify_All;
            btnVerifyFile.ImageTransparentColor = Color.Magenta;
            btnVerifyFile.Name = "btnVerifyFile";
            btnVerifyFile.Size = new Size(23, 22);
            btnVerifyFile.ToolTipText = "Verify Entire File With LFM";
            // 
            // prgVerify
            // 
            prgVerify.Name = "prgVerify";
            prgVerify.Size = new Size(100, 22);
            prgVerify.Style = ProgressBarStyle.Continuous;
            prgVerify.Visible = false;
            // 
            // lblVerify
            // 
            lblVerify.Name = "lblVerify";
            lblVerify.Size = new Size(53, 22);
            lblVerify.Text = "Verifying";
            lblVerify.Visible = false;
            // 
            // ofdOpen
            // 
            ofdOpen.Filter = "Audiograph Scrobble Index|*.agsi";
            ofdOpen.Title = "Select file...";
            // 
            // sfdSaveAs
            // 
            sfdSaveAs.Filter = "Audiograph Scrobble Index|*.agsi";
            sfdSaveAs.Title = "Save file as...";
            // 
            // frmScrobbleIndexEditor
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ToolStrip);
            Controls.Add(dgvData);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmScrobbleIndexEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scrobble Index Editor";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ToolStrip.ResumeLayout(false);
            ToolStrip.PerformLayout();
            Resize += new EventHandler(ResizeOps);
            Load += new EventHandler(FrmLoad);
            KeyDown += new KeyEventHandler(ShortcutKeys);
            Closing += new System.ComponentModel.CancelEventHandler(FormClose);
            ResumeLayout(false);
            PerformLayout();

        }

        internal DataGridView dgvData;
        internal DataGridViewTextBoxColumn Filename;
        internal DataGridViewTextBoxColumn Title;
        internal DataGridViewTextBoxColumn Artist;
        internal DataGridViewTextBoxColumn Album;
        internal ToolStrip ToolStrip;
        internal OpenFileDialog ofdOpen;
        internal ToolStripButton btnNew;
        internal ToolStripButton btnOpen;
        internal ToolStripButton btnSave;
        internal ToolStripButton btnSaveAs;
        internal ToolStripSeparator ToolStripSeparator7;
        internal ToolStripButton btnReload;
        internal ToolStripButton btnFind;
        internal ToolStripDropDownButton btnEdit;
        internal ToolStripMenuItem btnCopyCell;
        internal ToolStripMenuItem btnCopyRow;
        internal ToolStripSeparator ToolStripSeparator8;
        internal ToolStripMenuItem btnCutCell;
        internal ToolStripMenuItem btnCutRow;
        internal ToolStripSeparator ToolStripSeparator9;
        internal ToolStripMenuItem btnPasteCell;
        internal ToolStripSeparator ToolStripSeparator10;
        internal ToolStripMenuItem btnDeleteCell;
        internal ToolStripMenuItem btnDeleteRow;
        internal ToolStripButton btnAddRow;
        internal ToolStripSeparator ToolStripSeparator2;
        internal ToolStripButton btnVerifyRow;
        internal ToolStripButton btnVerifyFile;
        internal SaveFileDialog sfdSaveAs;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripTextBox txtFind;
        internal ToolStripLabel lblFind;
        internal ToolStripProgressBar prgVerify;
        internal ToolStripLabel lblVerify;
        internal ToolStripButton btnSetIndex;
        internal ToolStripSeparator ToolStripSeparator1;
    }
}