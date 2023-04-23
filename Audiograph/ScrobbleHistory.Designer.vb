<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScrobbleHistory
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScrobbleHistory))
        Me.btnClear = New System.Windows.Forms.Button()
        Me.ltvHistory = New System.Windows.Forms.ListView()
        Me.Title = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Artist = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Album = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Timestamp = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TimeSent = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Source = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.tmrListUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(12, 526)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'ltvHistory
        '
        Me.ltvHistory.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Title, Me.Artist, Me.Album, Me.Timestamp, Me.TimeSent, Me.Source, Me.Status})
        Me.ltvHistory.FullRowSelect = True
        Me.ltvHistory.GridLines = True
        Me.ltvHistory.HideSelection = False
        Me.ltvHistory.Location = New System.Drawing.Point(12, 12)
        Me.ltvHistory.Name = "ltvHistory"
        Me.ltvHistory.Size = New System.Drawing.Size(760, 508)
        Me.ltvHistory.TabIndex = 4
        Me.ltvHistory.UseCompatibleStateImageBehavior = False
        Me.ltvHistory.View = System.Windows.Forms.View.Details
        '
        'Title
        '
        Me.Title.Text = "Title"
        Me.Title.Width = 120
        '
        'Artist
        '
        Me.Artist.Text = "Artist"
        Me.Artist.Width = 120
        '
        'Album
        '
        Me.Album.Text = "Album"
        Me.Album.Width = 120
        '
        'Timestamp
        '
        Me.Timestamp.Text = "Timestamp"
        Me.Timestamp.Width = 130
        '
        'TimeSent
        '
        Me.TimeSent.Text = "Time Sent"
        Me.TimeSent.Width = 130
        '
        'Source
        '
        Me.Source.Text = "Source"
        '
        'Status
        '
        Me.Status.Text = "Status"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(697, 526)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'tmrListUpdate
        '
        Me.tmrListUpdate.Enabled = True
        Me.tmrListUpdate.Interval = 15
        '
        'frmScrobbleHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.ltvHistory)
        Me.Controls.Add(Me.btnClose)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmScrobbleHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scrobble History"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnClear As Button
    Friend WithEvents ltvHistory As ListView
    Friend WithEvents Title As ColumnHeader
    Friend WithEvents btnClose As Button
    Friend WithEvents Artist As ColumnHeader
    Friend WithEvents Album As ColumnHeader
    Friend WithEvents Timestamp As ColumnHeader
    Friend WithEvents TimeSent As ColumnHeader
    Friend WithEvents Source As ColumnHeader
    Friend WithEvents Status As ColumnHeader
    Friend WithEvents tmrListUpdate As Timer
End Class
