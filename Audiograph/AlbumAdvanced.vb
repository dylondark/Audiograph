Imports System.Threading

Public Class frmAlbumAdvanced
    Private Sub Search(sender As Object, e As EventArgs) Handles btnSearch.Click
        ' end if no data has been entered in album box
        If txtSearchAlbum.Text = String.Empty Then
            MessageBox.Show("Please make sure you have entered data in the album search field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' get search results
        Dim results As String
        If txtSearchArtist.Text = String.Empty Then
            results = CallAPI("album.search", "", "album=" & txtSearchAlbum.Text.Trim.Replace(" ", "+"))
        Else
            results = CallAPI("album.search", "", "album=" & txtSearchAlbum.Text.Trim.Replace(" ", "+"), "artist=" & txtSearchArtist.Text.Trim.Replace(" ", "+"))
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
                Dim searchnodes() As String = {"name", "artist"}
                ParseXML(results, "/lfm/results/albummatches/album", count, searchnodes)

                ' add to listview
                If searchnodes(0).Contains("ERROR: ") = False Then
                    ltvResults.Items.Add(searchnodes(0))                    ' name
                    ltvResults.Items(count).SubItems.Add(searchnodes(1))    ' artist
                End If
            Next count
        Else    ' if errors
            MessageBox.Show("API ERROR: Cannot retrieve search results.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' display message if no results found
        If ltvResults.Items.Count = 0 Then
            MessageBox.Show("No results found, please alter your search and try again.", "Search: " & txtSearchArtist.Text & " - " & txtSearchAlbum.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSearchAlbum.Select()
        End If
    End Sub

    ' put selected search item into final boxes
    Private Sub SelectSearchItem(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ltvResults.ItemSelectionChanged
        If ltvResults.SelectedItems.Count > 0 Then
            txtAlbum.Text = ltvResults.SelectedItems(0).Text
            txtArtist.Text = ltvResults.SelectedItems(0).SubItems(1).Text
            txtTracks.Text = "Loading..."
            txtListeners.Text = "Loading..."
            picArt.Image = picArt.InitialImage

            ' start new thread and get album playcount and art
            Dim responseXML As String
            Dim th As New Thread(Sub()
                                     ' get xml
                                     Dim album, artist As String
                                     Invoke(Sub()
                                                album = ltvResults.SelectedItems(0).SubItems(0).Text
                                                artist = ltvResults.SelectedItems(0).SubItems(1).Text
                                            End Sub)
                                     responseXML = CallAPI("album.getInfo", My.Settings.User, "album=" & album, "artist=" & artist)

                                     ' parse data
                                     Dim albumnodes As String() = {"name", "artist", "listeners"}
                                     ParseXML(responseXML, "/lfm/album", 0, albumnodes)
                                     Try
                                         albumnodes(2) = CInt(albumnodes(2)).ToString("N0")
                                     Catch ex As Exception
                                         albumnodes(2) = "Number Error"
                                     End Try
                                     ' get tracks
                                     Dim tracks As UShort = StrCount(responseXML, "track rank=")

                                     ' prevent thread window mismatch garbage from people hitting ok too fast
                                     If Me.Visible = True Then
                                         ' set data
                                         Invoke(Sub()
                                                    ' album name
                                                    If albumnodes(0).Contains("ERROR: ") = False Then
                                                        txtAlbum.Text = albumnodes(0)
                                                    End If
                                                    ' artist name
                                                    If albumnodes(1).Contains("ERROR: ") = False Then
                                                        txtArtist.Text = albumnodes(1)
                                                    End If
                                                    ' listeners
                                                    txtListeners.Text = albumnodes(2)
                                                    ' tracks
                                                    If tracks > 0 Then
                                                        txtTracks.Text = tracks.ToString("N0")
                                                    Else
                                                        txtTracks.Text = "N/A"
                                                    End If
                                                End Sub)

                                         ' set image
                                         Dim imageURL As String = ParseImage(responseXML, "/lfm/album/image", 2)
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
                                     End If
                                 End Sub)
            th.Name = "AlbumSearch"
            th.Start()
        End If
    End Sub

    Private Sub MBID(sender As Object, e As EventArgs) Handles btnMBID.Click
        ' call api
        Dim info As String = CallAPI("album.getInfo", My.Settings.User, "mbid=" & txtMBID.Text.Trim.ToLower)

        ' get info or error
        Dim infonodes() As String = {"name", "artist", "listeners"}
        ParseXML(info, "/lfm/album", 0, infonodes)
        ' get tracks
        Dim tracks As UShort = StrCount(info, "track rank=")
        ' check for error
        If infonodes(0).Contains("ERROR: ") = False Then
            ' image
            picArt.Image = picArt.InitialImage
            picArt.LoadAsync(ParseImage(info, "/lfm/album/image", 2))
            ' album name
            txtAlbum.Text = infonodes(0)
            ' artist name
            txtArtist.Text = infonodes(1)
            ' listeners
            If infonodes(2).Contains("ERROR: ") = False Then
                Try
                    txtListeners.Text = CInt(infonodes(2)).ToString("N0")
                Catch ex As Exception
                    txtListeners.Text = "Number Error"
                End Try
            End If
            ' tracks
            If tracks > 0 Then
                txtTracks.Text = tracks.ToString("N0")
            Else
                txtTracks.Text = "N/A"
            End If
        Else
            MessageBox.Show("No album was able to be found from this MBID, please try again.", "MBID: " & txtMBID.Text.Trim.ToLower, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMBID.Select()
        End If
    End Sub

    ' put final box data into main form
    Private Sub OK(sender As Object, e As EventArgs) Handles btnOK.Click
        If txtAlbum.Text <> String.Empty AndAlso txtArtist.Text <> String.Empty AndAlso txtArtist.Text <> "N/A" AndAlso txtArtist.Text <> "N/A" Then    ' if there is data in both boxes
            GoToAlbum(txtAlbum.Text.Trim, txtArtist.Text.Trim)
            Me.Close()                                      ' close window
        ElseIf txtTracks.Text = String.Empty AndAlso txtArtist.Text <> String.Empty Then ' if there is only data in album
            MessageBox.Show("Please make sure you have entered data into the Album field.", "Advanced Search", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf txtArtist.Text = String.Empty AndAlso txtTracks.Text <> String.Empty Then ' if there is only data in artist
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

    Private Sub DoubleClickItem(sender As Object, e As EventArgs) Handles ltvResults.ItemActivate
        btnOK.PerformClick()
    End Sub
    Private Sub UserType(sender As TextBox, e As KeyEventArgs) Handles txtAlbum.KeyDown, txtArtist.KeyDown
        If ltvResults.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        ' clear all info
        ltvResults.SelectedItems.Clear()
        sender.Text = String.Empty
        If sender.Name = "txtAlbum" Then
            txtArtist.Clear()
        Else
            txtAlbum.Clear()
        End If
        txtListeners.Text = "N/A"
        txtTracks.Text = "N/A"
        picArt.Image = picArt.InitialImage
        picArt.ImageLocation = String.Empty
    End Sub
End Class