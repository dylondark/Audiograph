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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScrobbleIndexEditor));
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.Filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.btnSetIndex = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddRow = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnCopyCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCutCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCutRow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPasteCell = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.lblFind = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVerifyRow = new System.Windows.Forms.ToolStripButton();
            this.btnVerifyFile = new System.Windows.Forms.ToolStripButton();
            this.prgVerify = new System.Windows.Forms.ToolStripProgressBar();
            this.lblVerify = new System.Windows.Forms.ToolStripLabel();
            this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
            this.sfdSaveAs = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Filename,
            this.Title,
            this.Artist,
            this.Album});
            this.dgvData.Location = new System.Drawing.Point(0, 28);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(800, 422);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Modified);
            this.dgvData.SelectionChanged += new System.EventHandler(this.Enable);
            this.dgvData.Click += new System.EventHandler(this.DisableFind);
            // 
            // Filename
            // 
            this.Filename.HeaderText = "Filename";
            this.Filename.Name = "Filename";
            this.Filename.Width = 74;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.Width = 52;
            // 
            // Artist
            // 
            this.Artist.HeaderText = "Artist";
            this.Artist.Name = "Artist";
            this.Artist.Width = 55;
            // 
            // Album
            // 
            this.Album.HeaderText = "Album";
            this.Album.Name = "Album";
            this.Album.Width = 61;
            // 
            // ToolStrip
            // 
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.btnSetIndex,
            this.ToolStripSeparator1,
            this.btnAddRow,
            this.ToolStripSeparator7,
            this.btnReload,
            this.btnEdit,
            this.ToolStripSeparator3,
            this.btnFind,
            this.txtFind,
            this.lblFind,
            this.ToolStripSeparator2,
            this.btnVerifyRow,
            this.btnVerifyFile,
            this.prgVerify,
            this.lblVerify});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(800, 25);
            this.ToolStrip.TabIndex = 1;
            this.ToolStrip.Text = "ToolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Audiograph.My.Resources.Resources.New_File;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "ToolStripButton1";
            this.btnNew.ToolTipText = "New File (Ctrl+N)";
            this.btnNew.Click += new System.EventHandler(this.ClickNew);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Audiograph.My.Resources.Resources.Open_File;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "ToolStripButton1";
            this.btnOpen.ToolTipText = "Open File (Ctrl+O)";
            this.btnOpen.Click += new System.EventHandler(this.ClickOpen);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Enabled = false;
            this.btnSave.Image = global::Audiograph.My.Resources.Resources.Save_File;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "ToolStripButton1";
            this.btnSave.ToolTipText = "Save File (Ctrl+S)";
            this.btnSave.Click += new System.EventHandler(this.ClickSave);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = global::Audiograph.My.Resources.Resources.Save_As;
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(23, 22);
            this.btnSaveAs.Text = "ToolStripButton1";
            this.btnSaveAs.ToolTipText = "Save File As (Ctrl+Shift+S)";
            this.btnSaveAs.Click += new System.EventHandler(this.ClickSaveAs);
            // 
            // btnSetIndex
            // 
            this.btnSetIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetIndex.Image = global::Audiograph.My.Resources.Resources.Set_Index;
            this.btnSetIndex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetIndex.Name = "btnSetIndex";
            this.btnSetIndex.Size = new System.Drawing.Size(23, 22);
            this.btnSetIndex.Text = "ToolStripButton1";
            this.btnSetIndex.ToolTipText = "Set As Current Scrobble Index";
            this.btnSetIndex.Click += new System.EventHandler(this.SetIndexClick);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAddRow
            // 
            this.btnAddRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddRow.Image = global::Audiograph.My.Resources.Resources.Add_Row;
            this.btnAddRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(23, 22);
            this.btnAddRow.Text = "ToolStripButton1";
            this.btnAddRow.ToolTipText = "Add Row From File (Ctrl+Shift+A)";
            this.btnAddRow.Click += new System.EventHandler(this.ClickAddRow);
            // 
            // ToolStripSeparator7
            // 
            this.ToolStripSeparator7.Name = "ToolStripSeparator7";
            this.ToolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnReload
            // 
            this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReload.Image = global::Audiograph.My.Resources.Resources.reload;
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(23, 22);
            this.btnReload.Text = "ToolStripButton1";
            this.btnReload.ToolTipText = "Reload From File (Ctrl+R)";
            this.btnReload.Click += new System.EventHandler(this.ClickReload);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCopyCell,
            this.btnCopyRow,
            this.ToolStripSeparator8,
            this.btnCutCell,
            this.btnCutRow,
            this.ToolStripSeparator9,
            this.btnPasteCell,
            this.ToolStripSeparator10,
            this.btnDeleteCell,
            this.btnDeleteRow});
            this.btnEdit.Image = global::Audiograph.My.Resources.Resources.Pencil;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(29, 22);
            this.btnEdit.Text = "ToolStripDropDownButton4";
            this.btnEdit.ToolTipText = "Edit";
            // 
            // btnCopyCell
            // 
            this.btnCopyCell.Name = "btnCopyCell";
            this.btnCopyCell.Size = new System.Drawing.Size(149, 22);
            this.btnCopyCell.Text = "Copy Cell(s)";
            this.btnCopyCell.Click += new System.EventHandler(this.ClickCopyCell);
            // 
            // btnCopyRow
            // 
            this.btnCopyRow.Name = "btnCopyRow";
            this.btnCopyRow.Size = new System.Drawing.Size(149, 22);
            this.btnCopyRow.Text = "Copy Row(s)";
            this.btnCopyRow.Click += new System.EventHandler(this.ClickCopyRow);
            // 
            // ToolStripSeparator8
            // 
            this.ToolStripSeparator8.Name = "ToolStripSeparator8";
            this.ToolStripSeparator8.Size = new System.Drawing.Size(146, 6);
            // 
            // btnCutCell
            // 
            this.btnCutCell.Name = "btnCutCell";
            this.btnCutCell.Size = new System.Drawing.Size(149, 22);
            this.btnCutCell.Text = "Cut Cell(s)";
            this.btnCutCell.Click += new System.EventHandler(this.ClickCutCell);
            // 
            // btnCutRow
            // 
            this.btnCutRow.Name = "btnCutRow";
            this.btnCutRow.Size = new System.Drawing.Size(149, 22);
            this.btnCutRow.Text = "Cut Row(s)";
            this.btnCutRow.Click += new System.EventHandler(this.ClickCutRow);
            // 
            // ToolStripSeparator9
            // 
            this.ToolStripSeparator9.Name = "ToolStripSeparator9";
            this.ToolStripSeparator9.Size = new System.Drawing.Size(146, 6);
            // 
            // btnPasteCell
            // 
            this.btnPasteCell.Name = "btnPasteCell";
            this.btnPasteCell.Size = new System.Drawing.Size(149, 22);
            this.btnPasteCell.Text = "Paste Into Cell";
            this.btnPasteCell.Click += new System.EventHandler(this.ClickPasteCell);
            // 
            // ToolStripSeparator10
            // 
            this.ToolStripSeparator10.Name = "ToolStripSeparator10";
            this.ToolStripSeparator10.Size = new System.Drawing.Size(146, 6);
            // 
            // btnDeleteCell
            // 
            this.btnDeleteCell.Name = "btnDeleteCell";
            this.btnDeleteCell.Size = new System.Drawing.Size(149, 22);
            this.btnDeleteCell.Text = "Delete Cell(s)";
            this.btnDeleteCell.Click += new System.EventHandler(this.ClickDeleteCell);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(149, 22);
            this.btnDeleteRow.Text = "Delete Row(s)";
            this.btnDeleteRow.Click += new System.EventHandler(this.ClickDeleteRow);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFind
            // 
            this.btnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFind.Image = global::Audiograph.My.Resources.Resources.Find;
            this.btnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(23, 22);
            this.btnFind.Text = "ToolStripButton1";
            this.btnFind.ToolTipText = "Find (Ctrl+F)";
            this.btnFind.Click += new System.EventHandler(this.ClickFind);
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(100, 25);
            this.txtFind.ToolTipText = "Type text to find...";
            this.txtFind.Visible = false;
            // 
            // lblFind
            // 
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(30, 22);
            this.lblFind.Text = "Find";
            this.lblFind.Visible = false;
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnVerifyRow
            // 
            this.btnVerifyRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVerifyRow.Image = ((System.Drawing.Image)(resources.GetObject("btnVerifyRow.Image")));
            this.btnVerifyRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVerifyRow.Name = "btnVerifyRow";
            this.btnVerifyRow.Size = new System.Drawing.Size(23, 22);
            this.btnVerifyRow.Text = "ToolStripButton1";
            this.btnVerifyRow.ToolTipText = "Verify Selected Row With LFM";
            this.btnVerifyRow.Click += new System.EventHandler(this.ClickVerifyRow);
            // 
            // btnVerifyFile
            // 
            this.btnVerifyFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVerifyFile.Image = global::Audiograph.My.Resources.Resources.Verify_All;
            this.btnVerifyFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVerifyFile.Name = "btnVerifyFile";
            this.btnVerifyFile.Size = new System.Drawing.Size(23, 22);
            this.btnVerifyFile.ToolTipText = "Verify Entire File With LFM";
            this.btnVerifyFile.Click += new System.EventHandler(this.ClickVerifyFile);
            // 
            // prgVerify
            // 
            this.prgVerify.Name = "prgVerify";
            this.prgVerify.Size = new System.Drawing.Size(100, 22);
            this.prgVerify.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgVerify.Visible = false;
            // 
            // lblVerify
            // 
            this.lblVerify.Name = "lblVerify";
            this.lblVerify.Size = new System.Drawing.Size(53, 22);
            this.lblVerify.Text = "Verifying";
            this.lblVerify.Visible = false;
            // 
            // ofdOpen
            // 
            this.ofdOpen.Filter = "Audiograph Scrobble Index|*.agsi";
            this.ofdOpen.Title = "Select file...";
            // 
            // sfdSaveAs
            // 
            this.sfdSaveAs.Filter = "Audiograph Scrobble Index|*.agsi";
            this.sfdSaveAs.Title = "Save file as...";
            // 
            // frmScrobbleIndexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.dgvData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScrobbleIndexEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scrobble Index Editor";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClose);
            this.Load += new System.EventHandler(this.FrmLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShortcutKeys);
            this.Resize += new System.EventHandler(this.ResizeOps);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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