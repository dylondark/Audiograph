Imports System.Threading

Public Class frmScrobbleIndexTrackAdvanced
    Private Sub Search(sender As Object, e As EventArgs) Handles btnSearch.Click
        ' end if no data has been entered in track box
        If txtSearchTrack.Text = String.Empty Then
            MessageBox.Show("Please make sure you have entered data in the track search field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' get search results
        Dim results As String
        If txtSearchArtist.Text = String.Empty Then
            results = CallAPI("track.search", "", "track=" & txtSearchTrack.Text.Trim.Replace(" ", "+"))
        Else
            results = CallAPI("track.search", "", "track=" & txtSearchTrack.Text.Trim.Replace(" ", "+"), "artist=" & txtSearchArtist.Text.Trim.Replace(" ", "+"))
        End If

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
                Dim searchnodes() As String = {"name", "artist", "listeners"}
                ParseXML(results, "/lfm/results/trackmatches/track", count, searchnodes)

                ' add to listview
                If searchnodes(0).Contains("ERROR: ") = False Then
                    ltvResults.Items.Add(searchnodes(0))                    ' name
                    ltvResults.Items(count).SubItems.Add(searchnodes(1))    ' artist
                    ltvResults.Items(count).SubItems.Add(CUInt(searchnodes(2)).ToString("N0"))  ' listeners
                End If
            Next count
        Else    ' if errors
            MessageBox.Show("API ERROR: Cannot retrieve search results.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' display message if no results found
        If ltvResults.Items.Count = 0 Then
            MessageBox.Show("No results found, please alter your search and try again.", "Search: " & txtSearchArtist.Text & " - " & txtSearchTrack.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSearchTrack.Select()
        End If
    End Sub

    ' put selected search item into final boxes
    Private Sub SelectSearchItem(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ltvResults.ItemSelectionChanged
        If ltvResults.SelectedItems.Count > 0 Then
            txtTrack.Text = ltvResults.SelectedItems(0).Text
            txtArtist.Text = ltvResults.SelectedItems(0).SubItems(1).Text
            txtAlbum.Text = "Loading..."
            txtListeners.Text = ltvResults.SelectedItems(0).SubItems(2).Text
            picArt.Image = picArt.InitialImage

            ' start new thread and get album playcount and art
            Dim responseXML As String
            Dim th As New Thread(Sub()
                                     ' get xml
                                     Dim track, artist As String
                                     Invoke(Sub()
                                                track = ltvResults.SelectedItems(0).SubItems(0).Text
                                                artist = ltvResults.SelectedItems(0).SubItems(1).Text
                                            End Sub)
                                     responseXML = CallAPI("track.getInfo", My.Settings.User, "track=" & track, "artist=" & artist)

                                     ' parse data
                                     Dim tracknodes As String() = {"name", "artist/name", "album/title", "listeners"}
                                     ParseXML(responseXML, "/lfm/track", 0, tracknodes)
                                     If tracknodes(2).Contains("ERROR: ") = True Then
                                         tracknodes(2) = "N/A"
                                     End If
                                     Try
                                         tracknodes(3) = CInt(tracknodes(3)).ToString("N0")
                                     Catch ex As Exception
                                         tracknodes(3) = "Number Error"
                                     End Try

                                     ' set data
                                     Invoke(Sub()
                                                    If tracknodes(0).Contains("ERROR: ") = False Then
                                                        txtTrack.Text = tracknodes(0)
                                                    End If
                                                    If tracknodes(1).Contains("ERROR: ") = False Then
                                                        txtArtist.Text = tracknodes(1)
                                                    End If
                                                    txtAlbum.Text = tracknodes(2)
                                                End Sub)

                                         ' set image
                                         Dim imageURL As String = ParseImage(responseXML, "/lfm/track/album/image", 2)
                                         If imageURL.Contains("ERROR: ") = False Then
                                             Invoke(Sub()
                                                        Try
                                                            picArt.LoadAsync(imageURL)
                                                        Catch ex As Exception
                                                            picArt.Image = picArt.InitialImage
                                                        End Try
                                                    End Sub)
                                         Else
                                             picArt.Image = picArt.ErrorImage
                                         End If
                                 End Sub)
            th.Name = "TrackSearch"
            th.Start()
        End If
    End Sub

    Private Sub MBID(sender As Object, e As EventArgs) Handles btnMBID.Click
        ' call api
        Dim info As String = CallAPI("track.getInfo", My.Settings.User, "mbid=" & txtMBID.Text.Trim.ToLower)

        ' get info or error
        Dim infonodes() As String = {"name", "artist/name", "album/title", "listeners"}
        ParseXML(info, "/lfm/track", 0, infonodes)
        ' check for error
        If infonodes(0).Contains("ERROR: ") = False Then
            picArt.Image = picArt.InitialImage
            picArt.LoadAsync(ParseImage(info, "/lfm/track/album/image", 2))
            txtTrack.Text = infonodes(0)
            txtArtist.Text = infonodes(1)
            If infonodes(2).Contains("ERROR: ") = False Then
                txtAlbum.Text = infonodes(2)
            Else
                txtAlbum.Text = "N/A"
            End If
            If infonodes(3).Contains("ERROR: ") = False Then
                Try
                    txtListeners.Text = CInt(infonodes(3)).ToString("N0")
                Catch ex As Exception
                    txtListeners.Text = "Number Error"
                End Try
            End If

        Else
            MessageBox.Show("No track was able to be found from this MBID, please try again.", "MBID: " & txtMBID.Text.Trim.ToLower, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMBID.Select()
        End If
    End Sub

    ' put final box data into main form
    Private Sub OK(sender As Object, e As EventArgs) Handles btnOK.Click
        ' make sure nothing is loading
        If txtAlbum.Text = "Loading..." Then
            Exit Sub
        End If

        If txtTrack.Text <> String.Empty AndAlso txtArtist.Text <> String.Empty AndAlso txtTrack.Text <> "N/A" AndAlso txtArtist.Text <> "N/A" Then    ' if there is data in both boxes
            frmScrobbleIndexAddRow.txtTitle.Text = txtTrack.Text.Trim
            frmScrobbleIndexAddRow.txtArtist.Text = txtArtist.Text.Trim
            If txtAlbum.Text <> "N/A" AndAlso txtAlbum.Text.Contains("ERROR: ") = False Then
                frmScrobbleIndexAddRow.txtAlbum.Text = txtAlbum.Text.Trim
            End If
            Me.Close()                                      ' close window
            ElseIf txtTrack.Text = String.Empty AndAlso txtArtist.Text <> String.Empty Then ' if there is only data in track
                MessageBox.Show("Please make sure you have entered data into the Track field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf txtArtist.Text = String.Empty AndAlso txtTrack.Text <> String.Empty Then ' if there is only data in artist
                MessageBox.Show("Please make sure you have entered data into the Artist field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else                                                                            ' if there is no data entered
                Me.Close()
        End If
    End Sub

    Private Sub Cancel(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub ArtClicked(sender As Object, e As EventArgs) Handles picArt.Click
        If sender.ImageLocation.Contains("http") = True Then
            Process.Start(sender.ImageLocation)
        End If
    End Sub

    Private Sub UserType(sender As TextBox, e As KeyEventArgs) Handles txtTrack.KeyDown, txtArtist.KeyDown
        If ltvResults.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        ' clear all info
        ltvResults.SelectedItems.Clear()
        sender.Text = String.Empty
        If sender.Name = "txtTrack" Then
            txtArtist.Clear()
        Else
            txtTrack.Clear()
        End If
        txtAlbum.Text = "N/A"
        txtListeners.Text = "N/A"
        picArt.Image = picArt.InitialImage
        picArt.ImageLocation = String.Empty
    End Sub

    Private Sub DoubleClickItem(sender As Object, e As EventArgs) Handles ltvResults.ItemActivate
        btnOK.PerformClick()
    End Sub
End Class