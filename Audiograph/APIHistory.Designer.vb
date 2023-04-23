<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAPIHistory
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAPIHistory))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ltvStatus = New System.Windows.Forms.ListView()
        Me.Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Method = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Latency = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Time = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Thread = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tmrListUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.btnClear = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(697, 526)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'ltvStatus
        '
        Me.ltvStatus.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Type, Me.Method, Me.Status, Me.Latency, Me.Time, Me.Thread})
        Me.ltvStatus.FullRowSelect = True
        Me.ltvStatus.GridLines = True
        Me.ltvStatus.HideSelection = False
        Me.ltvStatus.Location = New System.Drawing.Point(12, 12)
        Me.ltvStatus.Name = "ltvStatus"
        Me.ltvStatus.Size = New System.Drawing.Size(760, 508)
        Me.ltvStatus.TabIndex = 1
        Me.ltvStatus.UseCompatibleStateImageBehavior = False
        Me.ltvStatus.View = System.Windows.Forms.View.Details
        '
        'Type
        '
        Me.Type.Text = "Type"
        '
        'Method
        '
        Me.Method.Text = "Method"
        Me.Method.Width = 120
        '
        'Status
        '
        Me.Status.Text = "Status"
        Me.Status.Width = 240
        '
        'Latency
        '
        Me.Latency.Text = "Latency"
        '
        'Time
        '
        Me.Time.Text = "Time"
        Me.Time.Width = 150
        '
        'Thread
        '
        Me.Thread.Text = "Thread"
        Me.Thread.Width = 90
        '
        'tmrListUpdate
        '
        Me.tmrListUpdate.Enabled = True
        Me.tmrListUpdate.Interval = 15
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(12, 526)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmAPIHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.ltvStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAPIHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "API Call History"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Button
    Friend WithEvents ltvStatus As ListView
    Friend WithEvents Type As ColumnHeader
    Friend WithEvents Method As ColumnHeader
    Friend WithEvents Status As ColumnHeader
    Friend WithEvents Latency As ColumnHeader
    Friend WithEvents Time As ColumnHeader
    Friend WithEvents Thread As ColumnHeader
    Friend WithEvents tmrListUpdate As Timer
    Friend WithEvents btnClear As Button
End Class
