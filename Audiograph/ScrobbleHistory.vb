Public Class frmScrobbleHistory
    Private Sub ResizeOps(sender As Object, e As EventArgs) Handles Me.Resize
        ltvHistory.Width = Me.Width - 40
        ltvHistory.Height = Me.Height - 92
    End Sub

    Private Sub ExitForm(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' updates list view ~60 times per second, i hate that i have to do this but vb sucks
    Public Sub AddListView(sender As Object, e As EventArgs) Handles tmrListUpdate.Tick
        ' compare count of list and count of listview
        If scrobblehistory.Count > ltvHistory.Items.Count Then
            ' add every item that is not already added to the listview
            For count As Integer = ltvHistory.Items.Count To scrobblehistory.Count - 1
                ltvHistory.Items.Insert(0, scrobblehistory(count)(0)).SubItems.AddRange({scrobblehistory(count)(1), scrobblehistory(count)(2), scrobblehistory(count)(3), scrobblehistory(count)(4), scrobblehistory(count)(5), scrobblehistory(count)(6)})
            Next
        End If

        ' check for max items
        If ltvHistory.Items.Count > 100000 Then
            ltvHistory.Items.Clear()
        End If
    End Sub

    Private Sub ClearHistory(sender As Object, e As EventArgs) Handles btnClear.Click
        scrobblehistory.Clear()
        ltvHistory.Items.Clear()
    End Sub
End Class