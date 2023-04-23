Imports WMPLib
Public Class frmAddToQueue
    Private Sub Cancel(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub OK(sender As Object, e As EventArgs) Handles btnOK.Click
        ' if there is nothing in box just close the form
        If txtLocation.Text = String.Empty Then
            Me.Close()
            Exit Sub
        End If

        ' get initial amount of queue items for later
        Dim queueitems As Integer = frmMain.ltvMediaQueue.Items.Count()

        ' get array of files
        Dim names() As String = txtLocation.Text.Split("|"c)
        ' loop through files to check for invalid
        For Each val As String In names
            If val.Contains(":\") = False AndAlso val.Contains("http://") = False AndAlso val.Contains("https://") = False Then
                MessageBox.Show("The location entered does not appear to be a valid filesystem location or URL.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Next

        ' add each element to queue
        For Each val As String In names
            ' only add file name if its a filesystem location, add entire url if its a url
            If val.Contains(":\") = True Then
                ' determine file type
                If val.ToLower.Contains(".mp3") = True OrElse val.ToLower.Contains(".aac") = True OrElse val.ToLower.Contains(".flac") = True OrElse val.ToLower.Contains(".wav") = True OrElse
                    val.ToLower.Contains(".wma") = True OrElse val.ToLower.Contains(".m4a") = True OrElse val.ToLower.Contains(".mid") = True Then    ' audio types
                    frmMain.ltvMediaQueue.Items.Add(" - " & val.Substring(InStrRev(val, "\"))).ImageIndex = 0
                ElseIf val.ToLower.Contains(".mp4") = True OrElse val.ToLower.Contains(".mov") = True OrElse val.ToLower.Contains(".mpeg") = True OrElse val.ToLower.Contains(".mpg") = True OrElse
                    val.ToLower.Contains(".avi") = True OrElse val.ToLower.Contains(".wmv") = True Then ' video types
                    frmMain.ltvMediaQueue.Items.Add(" - " & val.Substring(InStrRev(val, "\"))).ImageIndex = 1
                Else
                    ' anything else
                    frmMain.ltvMediaQueue.Items.Add(" - " & val.Substring(InStrRev(val, "\"))).ImageIndex = 2
                End If
            Else
                ' link
                frmMain.ltvMediaQueue.Items.Add((frmMain.ltvMediaQueue.Items.Count + 1).ToString & " - " & val).ImageIndex = 3
            End If
            ' add subitem with full location
            frmMain.ltvMediaQueue.Items(frmMain.ltvMediaQueue.Items.Count - 1).SubItems.Add(val)
        Next

        ' shuffle crap
        If frmMain.chkMediaShuffle.Checked = True Then
            addingqueue = True
            frmMain.chkMediaShuffle.Checked = False
        End If

        ' begin playing if nothing is in queue or in the player, only recount if not
        If queueitems < 1 AndAlso frmMain.MediaPlayer.playState <> WMPPlayState.wmppsPlaying AndAlso frmMain.MediaPlayer.playState <> WMPPlayState.wmppsPaused Then
            frmMain.QueuePlay(0)
        Else
            frmMain.QueueRecount()
        End If

        ' close form
        Me.Close()
    End Sub

    Private Sub Browse(sender As Object, e As EventArgs) Handles btnBrowse.Click
        OpenFileDialog.ShowDialog()
        Dim names() As String = OpenFileDialog.FileNames
        ' put item(s) in text box separated by quotation marks and a space
        Dim first As Boolean = False

        If names.Length > 1 Then    ' dont run the first time to prevent | from being inserted at the start
            For Each val As String In names
                If first = True Then
                    ' this will run after the first time
                    txtLocation.Text &= "|" & val
                Else
                    ' this will run the first time
                    txtLocation.Text = val
                End If
                first = True
            Next
        ElseIf names.Length = 1 Then
            ' just set the plain text if only one item was selected
            txtLocation.Text = names(0)
        End If
    End Sub
End Class