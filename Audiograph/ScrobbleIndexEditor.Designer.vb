<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScrobbleIndexEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScrobbleIndexEditor))
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.Filename = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Title = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Artist = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Album = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnOpen = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnSaveAs = New System.Windows.Forms.ToolStripButton()
        Me.btnSetIndex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddRow = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnReload = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripDropDownButton()
        Me.btnCopyCell = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCopyRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCutCell = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCutRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPasteCell = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDeleteCell = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnDeleteRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnFind = New System.Windows.Forms.ToolStripButton()
        Me.txtFind = New System.Windows.Forms.ToolStripTextBox()
        Me.lblFind = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnVerifyRow = New System.Windows.Forms.ToolStripButton()
        Me.btnVerifyFile = New System.Windows.Forms.ToolStripButton()
        Me.prgVerify = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblVerify = New System.Windows.Forms.ToolStripLabel()
        Me.ofdOpen = New System.Windows.Forms.OpenFileDialog()
        Me.sfdSaveAs = New System.Windows.Forms.SaveFileDialog()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvData
        '
        Me.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Filename, Me.Title, Me.Artist, Me.Album})
        Me.dgvData.Location = New System.Drawing.Point(0, 28)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.Size = New System.Drawing.Size(800, 422)
        Me.dgvData.TabIndex = 0
        '
        'Filename
        '
        Me.Filename.HeaderText = "Filename"
        Me.Filename.Name = "Filename"
        Me.Filename.Width = 74
        '
        'Title
        '
        Me.Title.HeaderText = "Title"
        Me.Title.Name = "Title"
        Me.Title.Width = 52
        '
        'Artist
        '
        Me.Artist.HeaderText = "Artist"
        Me.Artist.Name = "Artist"
        Me.Artist.Width = 55
        '
        'Album
        '
        Me.Album.HeaderText = "Album"
        Me.Album.Name = "Album"
        Me.Album.Width = 61
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnOpen, Me.btnSave, Me.btnSaveAs, Me.btnSetIndex, Me.ToolStripSeparator1, Me.btnAddRow, Me.ToolStripSeparator7, Me.btnReload, Me.btnEdit, Me.ToolStripSeparator3, Me.btnFind, Me.txtFind, Me.lblFind, Me.ToolStripSeparator2, Me.btnVerifyRow, Me.btnVerifyFile, Me.prgVerify, Me.lblVerify})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(800, 25)
        Me.ToolStrip.TabIndex = 1
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnNew.Image = Global.Audiograph.My.Resources.Resources.New_File
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(23, 22)
        Me.btnNew.Text = "ToolStripButton1"
        Me.btnNew.ToolTipText = "New File (Ctrl+N)"
        '
        'btnOpen
        '
        Me.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnOpen.Image = Global.Audiograph.My.Resources.Resources.Open_File
        Me.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(23, 22)
        Me.btnOpen.Text = "ToolStripButton1"
        Me.btnOpen.ToolTipText = "Open File (Ctrl+O)"
        '
        'btnSave
        '
        Me.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSave.Enabled = False
        Me.btnSave.Image = Global.Audiograph.My.Resources.Resources.Save_File
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(23, 22)
        Me.btnSave.Text = "ToolStripButton1"
        Me.btnSave.ToolTipText = "Save File (Ctrl+S)"
        '
        'btnSaveAs
        '
        Me.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSaveAs.Image = Global.Audiograph.My.Resources.Resources.Save_As
        Me.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(23, 22)
        Me.btnSaveAs.Text = "ToolStripButton1"
        Me.btnSaveAs.ToolTipText = "Save File As (Ctrl+Shift+S)"
        '
        'btnSetIndex
        '
        Me.btnSetIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSetIndex.Image = Global.Audiograph.My.Resources.Resources.Set_Index
        Me.btnSetIndex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSetIndex.Name = "btnSetIndex"
        Me.btnSetIndex.Size = New System.Drawing.Size(23, 22)
        Me.btnSetIndex.Text = "ToolStripButton1"
        Me.btnSetIndex.ToolTipText = "Set As Current Scrobble Index"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnAddRow
        '
        Me.btnAddRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAddRow.Image = Global.Audiograph.My.Resources.Resources.Add_Row
        Me.btnAddRow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(23, 22)
        Me.btnAddRow.Text = "ToolStripButton1"
        Me.btnAddRow.ToolTipText = "Add Row From File (Ctrl+Shift+A)"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'btnReload
        '
        Me.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnReload.Image = Global.Audiograph.My.Resources.Resources.reload
        Me.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(23, 22)
        Me.btnReload.Text = "ToolStripButton1"
        Me.btnReload.ToolTipText = "Reload From File (Ctrl+R)"
        '
        'btnEdit
        '
        Me.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCopyCell, Me.btnCopyRow, Me.ToolStripSeparator8, Me.btnCutCell, Me.btnCutRow, Me.ToolStripSeparator9, Me.btnPasteCell, Me.ToolStripSeparator10, Me.btnDeleteCell, Me.btnDeleteRow})
        Me.btnEdit.Image = Global.Audiograph.My.Resources.Resources.Pencil
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(29, 22)
        Me.btnEdit.Text = "ToolStripDropDownButton4"
        Me.btnEdit.ToolTipText = "Edit"
        '
        'btnCopyCell
        '
        Me.btnCopyCell.Name = "btnCopyCell"
        Me.btnCopyCell.Size = New System.Drawing.Size(149, 22)
        Me.btnCopyCell.Text = "Copy Cell(s)"
        '
        'btnCopyRow
        '
        Me.btnCopyRow.Name = "btnCopyRow"
        Me.btnCopyRow.Size = New System.Drawing.Size(149, 22)
        Me.btnCopyRow.Text = "Copy Row(s)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(146, 6)
        '
        'btnCutCell
        '
        Me.btnCutCell.Name = "btnCutCell"
        Me.btnCutCell.Size = New System.Drawing.Size(149, 22)
        Me.btnCutCell.Text = "Cut Cell(s)"
        '
        'btnCutRow
        '
        Me.btnCutRow.Name = "btnCutRow"
        Me.btnCutRow.Size = New System.Drawing.Size(149, 22)
        Me.btnCutRow.Text = "Cut Row(s)"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(146, 6)
        '
        'btnPasteCell
        '
        Me.btnPasteCell.Name = "btnPasteCell"
        Me.btnPasteCell.Size = New System.Drawing.Size(149, 22)
        Me.btnPasteCell.Text = "Paste Into Cell"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(146, 6)
        '
        'btnDeleteCell
        '
        Me.btnDeleteCell.Name = "btnDeleteCell"
        Me.btnDeleteCell.Size = New System.Drawing.Size(149, 22)
        Me.btnDeleteCell.Text = "Delete Cell(s)"
        '
        'btnDeleteRow
        '
        Me.btnDeleteRow.Name = "btnDeleteRow"
        Me.btnDeleteRow.Size = New System.Drawing.Size(149, 22)
        Me.btnDeleteRow.Text = "Delete Row(s)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnFind
        '
        Me.btnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnFind.Image = Global.Audiograph.My.Resources.Resources.Find
        Me.btnFind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(23, 22)
        Me.btnFind.Text = "ToolStripButton1"
        Me.btnFind.ToolTipText = "Find (Ctrl+F)"
        '
        'txtFind
        '
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(100, 25)
        Me.txtFind.ToolTipText = "Type text to find..."
        Me.txtFind.Visible = False
        '
        'lblFind
        '
        Me.lblFind.Name = "lblFind"
        Me.lblFind.Size = New System.Drawing.Size(30, 22)
        Me.lblFind.Text = "Find"
        Me.lblFind.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnVerifyRow
        '
        Me.btnVerifyRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnVerifyRow.Image = CType(resources.GetObject("btnVerifyRow.Image"), System.Drawing.Image)
        Me.btnVerifyRow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnVerifyRow.Name = "btnVerifyRow"
        Me.btnVerifyRow.Size = New System.Drawing.Size(23, 22)
        Me.btnVerifyRow.Text = "ToolStripButton1"
        Me.btnVerifyRow.ToolTipText = "Verify Selected Row With LFM"
        '
        'btnVerifyFile
        '
        Me.btnVerifyFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnVerifyFile.Image = Global.Audiograph.My.Resources.Resources.Verify_All
        Me.btnVerifyFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnVerifyFile.Name = "btnVerifyFile"
        Me.btnVerifyFile.Size = New System.Drawing.Size(23, 22)
        Me.btnVerifyFile.ToolTipText = "Verify Entire File With LFM"
        '
        'prgVerify
        '
        Me.prgVerify.Name = "prgVerify"
        Me.prgVerify.Size = New System.Drawing.Size(100, 22)
        Me.prgVerify.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgVerify.Visible = False
        '
        'lblVerify
        '
        Me.lblVerify.Name = "lblVerify"
        Me.lblVerify.Size = New System.Drawing.Size(53, 22)
        Me.lblVerify.Text = "Verifying"
        Me.lblVerify.Visible = False
        '
        'ofdOpen
        '
        Me.ofdOpen.Filter = "Audiograph Scrobble Index|*.agsi"
        Me.ofdOpen.Title = "Select file..."
        '
        'sfdSaveAs
        '
        Me.sfdSaveAs.Filter = "Audiograph Scrobble Index|*.agsi"
        Me.sfdSaveAs.Title = "Save file as..."
        '
        'frmScrobbleIndexEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.dgvData)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmScrobbleIndexEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scrobble Index Editor"
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvData As DataGridView
    Friend WithEvents Filename As DataGridViewTextBoxColumn
    Friend WithEvents Title As DataGridViewTextBoxColumn
    Friend WithEvents Artist As DataGridViewTextBoxColumn
    Friend WithEvents Album As DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents ofdOpen As OpenFileDialog
    Friend WithEvents btnNew As ToolStripButton
    Friend WithEvents btnOpen As ToolStripButton
    Friend WithEvents btnSave As ToolStripButton
    Friend WithEvents btnSaveAs As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents btnReload As ToolStripButton
    Friend WithEvents btnFind As ToolStripButton
    Friend WithEvents btnEdit As ToolStripDropDownButton
    Friend WithEvents btnCopyCell As ToolStripMenuItem
    Friend WithEvents btnCopyRow As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents btnCutCell As ToolStripMenuItem
    Friend WithEvents btnCutRow As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents btnPasteCell As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents btnDeleteCell As ToolStripMenuItem
    Friend WithEvents btnDeleteRow As ToolStripMenuItem
    Friend WithEvents btnAddRow As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnVerifyRow As ToolStripButton
    Friend WithEvents btnVerifyFile As ToolStripButton
    Friend WithEvents sfdSaveAs As SaveFileDialog
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents txtFind As ToolStripTextBox
    Friend WithEvents lblFind As ToolStripLabel
    Friend WithEvents prgVerify As ToolStripProgressBar
    Friend WithEvents lblVerify As ToolStripLabel
    Friend WithEvents btnSetIndex As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
End Class
