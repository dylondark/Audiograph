Imports System.ComponentModel

Public Class frmScrobbleIndexAddRow
    Private Sub Browse(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim response As DialogResult = ofdBrowse.ShowDialog()
        Dim file As String = ofdBrowse.FileName

        If response = DialogResult.OK Then
            If file.Contains(":\") = True Then
                txtFilename.Text = GetFilename(file)
            Else
                MessageBox.Show("Valid file not detected", "Browse for File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub Search(sender As Object, e As EventArgs) Handles btnSearch.Click
        frmScrobbleIndexTrackAdvanced.Show()
    End Sub

    Private Sub Verify(sender As Object, e As EventArgs) Handles btnVerify.Click
        ' check that there is something in the boxes
        If txtTitle.Text = String.Empty OrElse txtArtist.Text = String.Empty Then
            MessageBox.Show("Valid data must be entered in both the Track and Artist fields", "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' verify
        Dim info As String() = VerifyTrack(txtTitle.Text.Trim, txtArtist.Text.Trim)
        ' if cannot be found
        If info(0).Contains("ERROR: ") = True Then
            MessageBox.Show("Track was unable to be verified", "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else ' if was found
            MessageBox.Show("Track verified as " + info(0) + " by " + info(1), "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtTitle.Text = info(0)
            txtArtist.Text = info(1)
            txtAlbum.Text = info(2)
        End If
    End Sub

    Private Sub OK(sender As Object, e As EventArgs) Handles btnOK.Click
        ' check for missing data
        If txtFilename.Text = String.Empty OrElse txtTitle.Text = String.Empty OrElse txtArtist.Text = String.Empty Then
            MessageBox.Show("Valid data must be entered into the File, Title, and Artist fields", "Add Row", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        frmScrobbleIndexEditor.dgvData.Rows.Add({txtFilename.Text.Trim, txtTitle.Text.Trim, txtArtist.Text.Trim, txtAlbum.Text.Trim})
        frmScrobbleIndexEditor.Saved(False)
        Me.Close()
    End Sub

    Private Sub Cancel(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub FormClose(sender As Object, e As CancelEventArgs) Handles Me.Closing
        frmScrobbleIndexTrackAdvanced.Close()
    End Sub
End Class