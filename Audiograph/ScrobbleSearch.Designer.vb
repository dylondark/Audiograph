<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmScrobbleSearch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScrobbleSearch))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtSearchArtist = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMBID = New System.Windows.Forms.TextBox()
        Me.btnMBID = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSearchTrack = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ltvResults = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTrack = New System.Windows.Forms.TextBox()
        Me.txtArtist = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAlbum = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPlaycount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.picArt = New System.Windows.Forms.PictureBox()
        CType(Me.picArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(555, 428)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(67, 23)
        Me.btnOK.TabIndex = 10
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtSearchArtist
        '
        Me.txtSearchArtist.Location = New System.Drawing.Point(215, 7)
        Me.txtSearchArtist.Name = "txtSearchArtist"
        Me.txtSearchArtist.Size = New System.Drawing.Size(136, 20)
        Me.txtSearchArtist.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(185, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Artist: "
        '
        'txtMBID
        '
        Me.txtMBID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMBID.Location = New System.Drawing.Point(46, 429)
        Me.txtMBID.Name = "txtMBID"
        Me.txtMBID.Size = New System.Drawing.Size(293, 20)
        Me.txtMBID.TabIndex = 7
        '
        'btnMBID
        '
        Me.btnMBID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMBID.Location = New System.Drawing.Point(345, 428)
        Me.btnMBID.Name = "btnMBID"
        Me.btnMBID.Size = New System.Drawing.Size(76, 23)
        Me.btnMBID.TabIndex = 8
        Me.btnMBID.Text = "&Lookup"
        Me.btnMBID.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 432)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "MBID: "
        '
        'txtSearchTrack
        '
        Me.txtSearchTrack.Location = New System.Drawing.Point(46, 7)
        Me.txtSearchTrack.Name = "txtSearchTrack"
        Me.txtSearchTrack.Size = New System.Drawing.Size(136, 20)
        Me.txtSearchTrack.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Track:"
        '
        'ltvResults
        '
        Me.ltvResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ltvResults.FullRowSelect = True
        Me.ltvResults.HideSelection = False
        Me.ltvResults.Location = New System.Drawing.Point(12, 35)
        Me.ltvResults.Name = "ltvResults"
        Me.ltvResults.Size = New System.Drawing.Size(408, 387)
        Me.ltvResults.TabIndex = 5
        Me.ltvResults.UseCompatibleStateImageBehavior = False
        Me.ltvResults.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Title"
        Me.ColumnHeader1.Width = 190
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Artist"
        Me.ColumnHeader2.Width = 117
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Listeners"
        Me.ColumnHeader3.Width = 76
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(357, 6)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(63, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(432, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 21)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Title"
        '
        'txtTrack
        '
        Me.txtTrack.Location = New System.Drawing.Point(435, 31)
        Me.txtTrack.Name = "txtTrack"
        Me.txtTrack.Size = New System.Drawing.Size(185, 20)
        Me.txtTrack.TabIndex = 12
        Me.txtTrack.Text = "N/A"
        Me.txtTrack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtArtist
        '
        Me.txtArtist.Location = New System.Drawing.Point(436, 82)
        Me.txtArtist.Name = "txtArtist"
        Me.txtArtist.Size = New System.Drawing.Size(185, 20)
        Me.txtArtist.TabIndex = 14
        Me.txtArtist.Text = "N/A"
        Me.txtArtist.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(433, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 21)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Artist"
        '
        'txtAlbum
        '
        Me.txtAlbum.Location = New System.Drawing.Point(437, 134)
        Me.txtAlbum.Name = "txtAlbum"
        Me.txtAlbum.ReadOnly = True
        Me.txtAlbum.Size = New System.Drawing.Size(185, 20)
        Me.txtAlbum.TabIndex = 16
        Me.txtAlbum.Text = "N/A"
        Me.txtAlbum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(434, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 21)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Album"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(436, 212)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 21)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Art"
        '
        'txtPlaycount
        '
        Me.txtPlaycount.Location = New System.Drawing.Point(437, 186)
        Me.txtPlaycount.Name = "txtPlaycount"
        Me.txtPlaycount.ReadOnly = True
        Me.txtPlaycount.Size = New System.Drawing.Size(185, 20)
        Me.txtPlaycount.TabIndex = 18
        Me.txtPlaycount.Text = "N/A"
        Me.txtPlaycount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(434, 161)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(114, 21)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Your Playcount"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(464, 428)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(86, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'picArt
        '
        Me.picArt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picArt.ErrorImage = Global.Audiograph.My.Resources.Resources.imageerror
        Me.picArt.Image = Global.Audiograph.My.Resources.Resources.imageunavailable
        Me.picArt.ImageLocation = ""
        Me.picArt.InitialImage = Global.Audiograph.My.Resources.Resources.imageunavailable
        Me.picArt.Location = New System.Drawing.Point(438, 236)
        Me.picArt.Name = "picArt"
        Me.picArt.Size = New System.Drawing.Size(186, 186)
        Me.picArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picArt.TabIndex = 25
        Me.picArt.TabStop = False
        '
        'frmScrobbleSearch
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(634, 461)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtPlaycount)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.picArt)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtAlbum)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtArtist)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtTrack)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSearchArtist)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtMBID)
        Me.Controls.Add(Me.btnMBID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSearchTrack)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ltvResults)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(650, 500)
        Me.MinimumSize = New System.Drawing.Size(650, 500)
        Me.Name = "frmScrobbleSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search for a Track"
        CType(Me.picArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As Button
    Friend WithEvents txtSearchArtist As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtMBID As TextBox
    Friend WithEvents btnMBID As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtSearchTrack As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ltvResults As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents btnSearch As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtTrack As TextBox
    Friend WithEvents txtArtist As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtAlbum As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents picArt As PictureBox
    Friend WithEvents txtPlaycount As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents btnCancel As Button
End Class
