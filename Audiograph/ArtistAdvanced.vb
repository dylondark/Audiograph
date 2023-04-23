Public Class frmArtistAdvanced
    Private Sub Search(sender As Object, e As EventArgs) Handles btnSearch.Click
        ' end if no data has been entered in track box
        If txtSearchArtist.Text = String.Empty Then
            MessageBox.Show("Please make sure you have entered data in the artist search field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' get search results
        Dim results As String = CallAPI("artist.search", "", "artist=" & txtSearchArtist.Text.Trim.Replace(" ", "+"))

        ' remove opensearch garbage
        If results.Contains("opensearch") = True Then
            Dim startindex As UInteger = InStr(results, "<opensearch:Query") - 1            ' find starting index
            Dim endindex As UInteger = InStr(results, "</opensearch:itemsPerPage>") + 25    ' find ending index
            results = results.Remove(startindex, endindex - startindex)                     ' remove (get number of chars between start and end index)
        End If

        ' check for errors/populate listview
        ltvResults.Items.Clear()
        ' get status
        Dim status As String = ParseMetadata(results, "lfm status=")
        If status.Contains("ok") = True Then
            For count As Byte = 0 To 29
                ' parse for data
                Dim searchnodes() As String = {"name", "listeners"}
                ParseXML(results, "/lfm/results/artistmatches/artist", count, searchnodes)

                ' add to listview
                If searchnodes(0).Contains("ERROR: ") = False Then
                    ltvResults.Items.Add(searchnodes(0)) ' name
                    ltvResults.Items(count).SubItems.Add(CUInt(searchnodes(1)).ToString("N0"))  ' listeners
                End If
            Next count
        Else    ' if errors
            MessageBox.Show("API ERROR: Cannot retrieve search results.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' display message if no results found
        If ltvResults.Items.Count = 0 Then
            MessageBox.Show("No results found, please alter your search and try again.", "Search: " & txtSearchArtist.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSearchArtist.Select()
        End If
    End Sub

    ' put selected search item into final boxes
    Private Sub SelectSearchItem(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ltvResults.ItemSelectionChanged
        If ltvResults.SelectedItems.Count > 0 Then
            txtArtist.Text = ltvResults.SelectedItems(0).Text
            txtListeners.Text = ltvResults.SelectedItems(0).SubItems(1).Text
        End If
    End Sub

    Private Sub MBID(sender As Object, e As EventArgs) Handles btnMBID.Click
        ' call api
        Dim info As String = CallAPI("artist.getInfo", "", "mbid=" & txtMBID.Text.Trim.ToLower)

        ' get info or error
        Dim infonodes() As String = {"name", "stats/listeners"}
        ParseXML(info, "/lfm/artist", 0, infonodes)
        ' check for error
        If infonodes(0).Contains("ERROR: ") = False AndAlso infonodes(0).Contains("ERROR: ") = False Then
            txtArtist.Text = infonodes(0)
            Try
                txtListeners.Text = CInt(infonodes(1)).ToString("N0")
            Catch ex As Exception
                txtListeners.Text = "Number Error"
            End Try
        Else
            MessageBox.Show("No artist was able to be found from this MBID, please try again.", "MBID: " & txtMBID.Text.Trim.ToLower, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMBID.Select()
        End If
    End Sub

    ' put final box data into main form
    Private Sub OK(sender As Object, e As EventArgs) Handles btnOK.Click
        If txtArtist.Text <> String.Empty AndAlso txtArtist.Text <> "N/A" Then   ' if there is data in artist box
            GoToArtist(txtArtist.Text.Trim)
            Me.Close()                                      ' close window
        Else  ' if there is no data entered
            Me.Close()
        End If
    End Sub

    Private Sub Cancel(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub UserType(sender As TextBox, e As KeyEventArgs) Handles txtArtist.KeyDown
        If ltvResults.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        ' clear all info
        ltvResults.SelectedItems.Clear()
        txtListeners.Text = "N/A"
        sender.Clear()
    End Sub

    Private Sub DoubleClickItem(sender As Object, e As EventArgs) Handles ltvResults.ItemActivate
        btnOK.PerformClick()
    End Sub
End Class