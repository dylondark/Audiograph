Imports Microsoft.VisualBasic.FileIO
Imports System.Text
Imports System.IO
Imports System.ComponentModel

Public Class frmScrobbleIndexEditor
    Public currentIndexLocation As String
    Private currentIndexData As New List(Of String())
    Private currentSaved As Boolean = True
    Private newState As Boolean = True
    Private searched As Boolean = False

#Region "UI"
    Private Sub ResizeOps(sender As Object, e As EventArgs) Handles Me.Resize
        dgvData.Height = Me.Height - 67
        dgvData.Width = Me.Width - 16
    End Sub

    Private Sub FrmLoad(sender As Object, e As EventArgs) Handles Me.Load
        Me.KeyPreview = True
    End Sub

    Private Sub ShortcutKeys(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control Then
            If e.KeyCode = Keys.N Then
                btnNew.PerformClick()
            ElseIf e.KeyCode = Keys.O Then
                btnOpen.PerformClick()
            ElseIf e.Shift = False AndAlso e.KeyCode = Keys.S Then
                btnSave.PerformClick()
            ElseIf e.Shift = True AndAlso e.KeyCode = Keys.S Then
                btnSaveAs.PerformClick()
            ElseIf e.Shift = True AndAlso e.KeyCode = Keys.A Then
                btnAddRow.PerformClick()
            ElseIf e.KeyCode = Keys.R Then
                btnReload.PerformClick()
            ElseIf e.KeyCode = Keys.F Then
                btnFind.PerformClick()
            End If
        End If
    End Sub

    Public Sub Saved(yesno As Boolean)
        ' disable/enable button
        If currentIndexLocation = My.Settings.CurrentScrobbleIndex Then
            btnSetIndex.Enabled = False
        Else
            btnSetIndex.Enabled = True
        End If

        ' only run when needed
        If yesno = currentSaved Then
            Exit Sub
        End If

        If yesno = True Then
            ' remove asterisk
            Me.Text = Me.Text.Replace("*", String.Empty)
            btnSave.Enabled = False
            currentSaved = True
            newState = False
        Else
            ' insert asterisk after "- "
            Me.Text = Me.Text.Insert(InStr(Me.Text, "-") + 1, "*")
            btnSave.Enabled = True
            currentSaved = False
        End If
    End Sub

    Private Sub Modified(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellEndEdit
        Saved(False)
    End Sub

    Private Sub Enable(sender As Object, e As EventArgs) Handles dgvData.SelectionChanged
        ' check that there is something selected
        If dgvData.SelectedCells.Count <= 0 AndAlso dgvData.SelectedRows.Count <= 0 AndAlso dgvData.SelectedColumns.Count <= 0 Then
            btnEdit.Enabled = False
            btnVerifyFile.Enabled = False
        Else
            btnEdit.Enabled = True
            btnVerifyFile.Enabled = True
            If dgvData.SelectedRows.Count > 0 Then
                btnCopyRow.Enabled = True
            End If
        End If

        ' check that a row is selected
        If dgvData.SelectedRows.Count > 0 Then
            btnCopyRow.Enabled = True
            btnCutRow.Enabled = True
            btnDeleteRow.Enabled = True
            btnVerifyRow.Enabled = True
        Else
            btnCutRow.Enabled = False
            btnCopyRow.Enabled = False
            btnDeleteRow.Enabled = False
            btnVerifyRow.Enabled = False
        End If
    End Sub

    Private Sub DisableFind(sender As Object, e As EventArgs) Handles dgvData.Click
        txtFind.Visible = False
        lblFind.Visible = False
        searched = False
    End Sub

    Private Sub VerifyRows()
        Dim failedRows As New List(Of Integer)
        Dim rows As New List(Of Integer)
        Dim progress As Double
        Dim currentResponse As String()

        Invoke(Sub()
                   prgVerify.Value = 0
                   prgVerify.Visible = True
                   lblVerify.Text = "Verifying"
                   lblVerify.Visible = True
               End Sub)

        ' get rows to verify
        For selectedRow As Integer = 0 To dgvData.SelectedRows.Count - 1
            rows.Add(dgvData.SelectedRows(selectedRow).Index)
        Next

        For row As Integer = 0 To rows.Count - 2
            ' verify
            currentResponse = VerifyTrack(CStr(dgvData.Rows(rows(row)).Cells(1).Value), CStr(dgvData.Rows(rows(row)).Cells(2).Value))

            ' add if failed
            If currentResponse(0).Contains("ERROR: ") = True Then
                failedRows.Add(rows(row))
            End If

            ' update progress
            progress = (row + 1) / rows.Count * 100
            Invoke(Sub()
                       prgVerify.Value = Math.Round(progress)
                       lblVerify.Text = "Verifying: " & (row + 2).ToString & " of " & rows.Count.ToString
                   End Sub)
        Next

        Invoke(Sub() prgVerify.Value = 100)

        ' deal with results
        If failedRows.Count = 0 Then
            Invoke(Sub() MessageBox.Show("All rows were successfully verified!", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Information))
        Else
            Invoke(Sub() MessageBox.Show("Some rows were not able to be verified" & vbCrLf & "Unverified rows will be highlighted", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation))

            Invoke(Sub() dgvData.ClearSelection())
            For Each row In failedRows
                Invoke(Sub() dgvData.Rows(row).Selected = True)
            Next
        End If

        Invoke(Sub()
                   prgVerify.Visible = False
                   lblVerify.Visible = False
               End Sub)
    End Sub

    Private Sub VerifyFile()
        Dim failedRows As New List(Of Integer)
        Dim rows As Integer = dgvData.Rows.Count
        Dim progress As Double
        Dim currentResponse As String()

        Invoke(Sub()
                   prgVerify.Value = 0
                   prgVerify.Visible = True
                   lblVerify.Text = "Verifying"
                   lblVerify.Visible = True
               End Sub)

        For row As Integer = 0 To rows - 2
            ' verify
            currentResponse = VerifyTrack(CStr(dgvData.Rows(row).Cells(1).Value), CStr(dgvData.Rows(row).Cells(2).Value))

            ' add if failed
            If currentResponse(0).Contains("ERROR: ") = True Then
                failedRows.Add(row)
            End If

            ' update progress
            progress = (row + 1) / rows * 100
            Invoke(Sub()
                       prgVerify.Value = Math.Round(progress)
                       lblVerify.Text = "Verifying: " & (row + 2).ToString & " of " & rows.ToString
                   End Sub)
        Next

        Invoke(Sub() prgVerify.Value = 100)

        ' deal with results
        If failedRows.Count = 0 Then
            Invoke(Sub() MessageBox.Show("All rows were successfully verified!", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Information))
        Else
            Invoke(Sub() MessageBox.Show("Some rows were not able to be verified" & vbCrLf & "Unverified rows will be highlighted", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation))

            Invoke(Sub() dgvData.ClearSelection())
            For Each row In failedRows
                Invoke(Sub() dgvData.Rows(row).Selected = True)
            Next
        End If

        Invoke(Sub()
                   prgVerify.Visible = False
                   lblVerify.Visible = False
               End Sub)
    End Sub
#End Region

#Region "Buttons"
    Private Sub ClickNew(sender As Object, e As EventArgs) Handles btnNew.Click
        NewFile()
    End Sub

    Private Sub ClickOpen(sender As Object, e As EventArgs) Handles btnOpen.Click
        If ofdOpen.ShowDialog() = DialogResult.OK Then
            Open(ofdOpen.FileName)
        End If
    End Sub

    Private Sub ClickSave(sender As Object, e As EventArgs) Handles btnSave.Click
        If newState = True Then
            ' open save as instead
            btnSaveAs.PerformClick()
            Exit Sub
        End If

        Save(currentIndexLocation)
    End Sub

    Private Sub ClickSaveAs(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        sfdSaveAs.InitialDirectory = Application.StartupPath

        If sfdSaveAs.ShowDialog() = DialogResult.OK Then
            Save(sfdSaveAs.FileName)
        End If
    End Sub

    Private Sub SetIndexClick(sender As Object, e As EventArgs) Handles btnSetIndex.Click
        frmMain.LoadScrobbleIndex(currentIndexLocation)
        btnSetIndex.Enabled = False
    End Sub

    Private Sub ClickReload(sender As Object, e As EventArgs) Handles btnReload.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to reload index from file?" & vbCrLf & "All unsaved changes will be lost", "Confirm Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            Open(currentIndexLocation)
        End If
    End Sub

    Private Sub ClickFind(sender As Object, e As EventArgs) Handles btnFind.Click
        Static cells As New List(Of Integer())
        Static currentCell As Integer
        Static lastSearchTerm As String

        If txtFind.Visible = False Then
            ' step 1 - show text box and label 
            cells.Clear()
            currentCell = 0
            txtFind.Text = String.Empty
            lblFind.Text = "Find"
            txtFind.Visible = True
            lblFind.Visible = True
            txtFind.Select()
        ElseIf searched = False OrElse lastSearchTerm <> txtFind.Text OrElse (txtFind.Text = String.Empty AndAlso searched = False) Then
            ' step 2 - do initial search and highlight first item
            cells.Clear()
            lastSearchTerm = txtFind.Text
            Dim searchTerm As String = lastSearchTerm.Trim.ToLower

            ' search
            For row As Integer = 0 To dgvData.Rows.Count - 2
                For column As Integer = 0 To 3
                    If CStr(dgvData.Rows(row).Cells(column).Value).ToLower.Contains(searchTerm) = True Then
                        cells.Add({row, column})
                    End If
                Next
            Next

            ' if nothing was found
            If cells.Count = 0 Then
                lblFind.Text = "Nothing found"
                Exit Sub
            End If

            searched = True

            ' display first
            currentCell = 1
            lblFind.Text = currentCell.ToString & " of " & cells.Count.ToString
            dgvData.CurrentCell = dgvData.Rows(cells(currentCell - 1)(0)).Cells(cells(currentCell - 1)(1))
        Else
            ' step 3 - loop between rest of items
            If currentCell < cells.Count Then
                currentCell += 1
            Else
                currentCell = 1
            End If

            ' display
            lblFind.Text = currentCell.ToString & " of " & cells.Count.ToString
            dgvData.CurrentCell = dgvData.Rows(cells(currentCell - 1)(0)).Cells(cells(currentCell - 1)(1))
        End If
    End Sub

    Private Sub ClickCopyCell(sender As Object, e As EventArgs) Handles btnCopyCell.Click
        ' if theres only one cell selected dont add quotations and commas and crap
        If dgvData.SelectedCells.Count <= 1 Then
            My.Computer.Clipboard.SetText(CStr(dgvData.SelectedCells(0).Value))
        Else
            Dim str As New StringBuilder()

            For selectedCell As Integer = dgvData.SelectedCells.Count - 1 To 0 Step -1
                str.Append(Chr(34) & CStr(dgvData.SelectedCells(selectedCell).Value) & Chr(34))
                If selectedCell > 0 Then
                    str.Append(",")
                End If
            Next

            My.Computer.Clipboard.SetText(str.ToString)
        End If
    End Sub

    Private Sub ClickCopyRow(sender As Object, e As EventArgs) Handles btnCopyRow.Click
        Dim str As New StringBuilder()
        Dim currentRow As Integer

        For selectedRow As Integer = dgvData.SelectedRows.Count - 1 To 0 Step -1
            currentRow = dgvData.SelectedRows(selectedRow).Index
            str.Append(Chr(34) & dgvData.Rows(currentRow).Cells(0).Value & Chr(34) & ",")
            str.Append(Chr(34) & dgvData.Rows(currentRow).Cells(1).Value & Chr(34) & ",")
            str.Append(Chr(34) & dgvData.Rows(currentRow).Cells(2).Value & Chr(34) & ",")
            str.Append(Chr(34) & dgvData.Rows(currentRow).Cells(3).Value & Chr(34))
            If selectedRow > 0 Then
                str.AppendLine()    ' next line
            End If
        Next

        My.Computer.Clipboard.SetText(str.ToString)
    End Sub

    Private Sub ClickCutCell(sender As Object, e As EventArgs) Handles btnCutCell.Click
        btnCopyCell.PerformClick()
        btnDeleteCell.PerformClick()
    End Sub

    Private Sub ClickCutRow(sender As Object, e As EventArgs) Handles btnCutRow.Click
        btnCopyRow.PerformClick()
        btnDeleteRow.PerformClick()
    End Sub

    Private Sub ClickPasteCell(sender As Object, e As EventArgs) Handles btnPasteCell.Click
        For selectedCell As Integer = dgvData.SelectedCells.Count - 1 To 0 Step -1
            dgvData.SelectedCells(selectedCell).Value = My.Computer.Clipboard.GetText
        Next
    End Sub

    Private Sub ClickDeleteCell(sender As Object, e As EventArgs) Handles btnDeleteCell.Click
        For selectedCell As Integer = dgvData.SelectedCells.Count - 1 To 0 Step -1
            dgvData.SelectedCells(selectedCell).Value = String.Empty
        Next
        Saved(False)
    End Sub

    Private Sub ClickDeleteRow(sender As Object, e As EventArgs) Handles btnDeleteRow.Click
        Dim currentRow As Integer

        For selectedRow As Integer = dgvData.SelectedRows.Count - 1 To 0 Step -1
            currentRow = dgvData.SelectedRows(selectedRow).Index
            dgvData.Rows.RemoveAt(currentRow)
        Next
        Saved(False)
    End Sub

    Private Sub ClickAddRow(sender As Object, e As EventArgs) Handles btnAddRow.Click
        frmScrobbleIndexAddRow.Show()
        frmScrobbleIndexAddRow.Activate()
    End Sub

    Private Sub ClickVerifyRow(sender As Object, e As EventArgs) Handles btnVerifyRow.Click
        Dim th As New Threading.Thread(AddressOf VerifyRows)
        th.Name = "VerifyIndex"
        th.Start()
    End Sub

    Private Sub ClickVerifyFile(sender As Object, e As EventArgs) Handles btnVerifyFile.Click
        Dim th As New Threading.Thread(AddressOf VerifyFile)
        th.Name = "VerifyIndex"
        th.Start()
    End Sub
#End Region

#Region "Ops"
    Public Sub NewFile()
        Saved(False)
        Me.Text = "Scrobble Index Editor - *Untitled"
        newState = True
        btnSetIndex.Enabled = False
        btnReload.Enabled = False
        dgvData.Rows.Clear()
    End Sub

    Public Sub Open(location As String)
        ' init reader
        Using reader As New TextFieldParser(location)
            reader.TextFieldType = FieldType.Delimited
            reader.SetDelimiters(",")

            ' read file and gather errors if any
            currentIndexData.Clear()
            Dim currentRow As String()
            Dim badLines As New List(Of UInteger)
            Dim currentLine As UInteger
            Dim blank As Boolean = True ' if this remains true after the loop that means the file is blank
            While Not reader.EndOfData
                blank = False
                currentLine += 1
                Try
                    currentRow = reader.ReadFields()
                    ' row must have 4 fields
                    If currentRow.Length <> 4 Then
                        badLines.Add(currentLine)
                    Else
                        ' add line to contents
                        currentIndexData.Add(currentRow)
                    End If
                Catch ex As MalformedLineException
                    badLines.Add(currentLine)
                End Try
            End While

            ' handle errors
            If blank = False Then
                ' if entire file is unusable
                If badLines.Count >= currentLine Then
                    MessageBox.Show("Scrobble index was unable to be parsed", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                ' if only some lines are usuable
                If badLines.Count >= 1 Then
                    MessageBox.Show("Scrobble index contains errors on some lines, these lines will be ignored by the editor and overwritten if you save the file", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            ' save file location
            currentIndexLocation = location

            ' add file to form title
            Me.Text = "Scrobble Index Editor - " & location

            ' enter data into dgv
            dgvData.Rows.Clear()
            If blank = False Then
                For count As UInteger = 0 To currentIndexData.Count - 1
                    dgvData.Rows.Add(currentIndexData(count))
                Next
            End If
        End Using
        btnReload.Enabled = True
        newState = False
        Saved(True)
    End Sub

    Public Sub Save(location As String)
        newState = False

        Dim fi As New FileInfo(location)

        ' delete file if it already exists
        If fi.Exists AndAlso dgvData.Rows.Count > 1 Then
            fi.Delete()
        End If

        ' create new file and filestream
        Dim fs As FileStream = fi.Create()

        ' only attempt to write if there is data to write
        If dgvData.Rows.Count > 1 Then
            ' compile data to string
            Dim str As New StringBuilder()
            For row As UInteger = 0 To dgvData.Rows.Count - 2
                str.Append(Chr(34) & dgvData.Rows(row).Cells(0).Value & Chr(34) & ",")
                str.Append(Chr(34) & dgvData.Rows(row).Cells(1).Value & Chr(34) & ",")
                str.Append(Chr(34) & dgvData.Rows(row).Cells(2).Value & Chr(34) & ",")
                str.Append(Chr(34) & dgvData.Rows(row).Cells(3).Value & Chr(34))
                str.AppendLine()    ' next line
            Next

            ' convert to bytearray and write
            Dim bytes As Byte() = New UTF8Encoding(True).GetBytes(str.ToString)
            fs.Write(bytes, 0, bytes.Length)
        End If
        fs.Close()

        ' tell main to reload scrobble index if this is the currently loaded scrobble index
        If location = My.Settings.CurrentScrobbleIndex Then
            frmMain.LoadScrobbleIndex(location)
        End If

        ' final stuff
        Saved(True)
        btnReload.Enabled = True
        currentIndexLocation = location
        Me.Text = "Scrobble Index Editor - " & location
    End Sub

    Private Sub FormClose(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If currentSaved = False Then
            Dim result As DialogResult = MessageBox.Show("You have made unsaved changes" & vbCrLf & "Are you sure you want to quit without saving?", "Add Row", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            ' cancel exit if not yes
            If result <> DialogResult.Yes Then
                e.Cancel = True
                Exit Sub
            End If
        End If

        frmScrobbleIndexAddRow.Close()
    End Sub
#End Region
End Class