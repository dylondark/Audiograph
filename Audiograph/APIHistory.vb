Public Class frmAPIHistory
    Private Sub ResizeOps(sender As Object, e As EventArgs) Handles Me.Resize
        ltvStatus.Width = Me.Width - 40
        ltvStatus.Height = Me.Height - 92
    End Sub

    Private Sub ExitForm(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' updates list view ~60 times per second, i hate that i have to do this but vb sucks
    Public Sub AddListView(sender As Object, e As EventArgs) Handles tmrListUpdate.Tick
        ' compare count of list and count of listview
        If apihistory.Count > ltvStatus.Items.Count Then
            ' add every item that is not already added to the listview
            For count As Integer = ltvStatus.Items.Count To apihistory.Count - 1
                ltvStatus.Items.Insert(0, apihistory(count)(0)).SubItems.AddRange({apihistory(count)(1), apihistory(count)(2), apihistory(count)(3), apihistory(count)(4), apihistory(count)(5)})
            Next
        End If

        ' check for max items
        If ltvStatus.Items.Count > 100000 Then
            ltvStatus.Items.Clear()
        End If
    End Sub

    Private Sub ClearHistory(sender As Object, e As EventArgs) Handles btnClear.Click
        apihistory.Clear()
        ltvStatus.Items.Clear()
    End Sub
End Class