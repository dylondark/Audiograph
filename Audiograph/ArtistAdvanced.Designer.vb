<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArtistAdvanced
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArtistAdvanced))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtListeners = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtArtist = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSearchArtist = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMBID = New System.Windows.Forms.TextBox()
        Me.btnMBID = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ltvResults = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(464, 428)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(86, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtListeners
        '
        Me.txtListeners.Location = New System.Drawing.Point(435, 81)
        Me.txtListeners.Name = "txtListeners"
        Me.txtListeners.ReadOnly = True
        Me.txtListeners.Size = New System.Drawing.Size(185, 20)
        Me.txtListeners.TabIndex = 12
        Me.txtListeners.Text = "N/A"
        Me.txtListeners.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(433, 57)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 21)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Listeners"
        '
        'txtArtist
        '
        Me.txtArtist.Location = New System.Drawing.Point(435, 31)
        Me.txtArtist.Name = "txtArtist"
        Me.txtArtist.Size = New System.Drawing.Size(185, 20)
        Me.txtArtist.TabIndex = 10
        Me.txtArtist.Text = "N/A"
        Me.txtArtist.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(432, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 21)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Artist"
        '
        'txtSearchArtist
        '
        Me.txtSearchArtist.Location = New System.Drawing.Point(41, 7)
        Me.txtSearchArtist.Name = "txtSearchArtist"
        Me.txtSearchArtist.Size = New System.Drawing.Size(310, 20)
        Me.txtSearchArtist.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Artist: "
        '
        'txtMBID
        '
        Me.txtMBID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMBID.Location = New System.Drawing.Point(46, 429)
        Me.txtMBID.Name = "txtMBID"
        Me.txtMBID.Size = New System.Drawing.Size(293, 20)
        Me.txtMBID.TabIndex = 5
        '
        'btnMBID
        '
        Me.btnMBID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMBID.Location = New System.Drawing.Point(345, 428)
        Me.btnMBID.Name = "btnMBID"
        Me.btnMBID.Size = New System.Drawing.Size(76, 23)
        Me.btnMBID.TabIndex = 6
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
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "MBID: "
        '
        'ltvResults
        '
        Me.ltvResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ltvResults.FullRowSelect = True
        Me.ltvResults.HideSelection = False
        Me.ltvResults.Location = New System.Drawing.Point(12, 35)
        Me.ltvResults.Name = "ltvResults"
        Me.ltvResults.Size = New System.Drawing.Size(408, 387)
        Me.ltvResults.TabIndex = 3
        Me.ltvResults.UseCompatibleStateImageBehavior = False
        Me.ltvResults.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Artist"
        Me.ColumnHeader2.Width = 280
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Listeners"
        Me.ColumnHeader3.Width = 100
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(357, 6)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(63, 23)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(555, 428)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(67, 23)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmArtistAdvanced
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 461)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtListeners)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtArtist)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtSearchArtist)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtMBID)
        Me.Controls.Add(Me.btnMBID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ltvResults)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmArtistAdvanced"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Advanced Search"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCancel As Button
    Friend WithEvents txtListeners As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtArtist As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtSearchArtist As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtMBID As TextBox
    Friend WithEvents btnMBID As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents ltvResults As ListView
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnOK As Button
End Class
