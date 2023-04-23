Imports System.ComponentModel
Imports System.IO
Imports System.Text

Public Class frmBackupTool
    ' 0 = chart, 1 = tag, 2 = track, etc
    Private section As Byte
    ' contains text box contents for the selected content page
    Private textContents As New List(Of String)
    ' contains values of checkboxes on the selected content page from top to bottom
    Private columnContents As New List(Of Boolean)
    ' contains value of the currently selected country on the chart page (empty for worldwide)
    Private chartCountry As String
    ' contains values of the datetimepickers on the user content page
    Private dateContents As New List(Of DateTime)
    ' contains value of the number of results (0 for entire)
    Private chartResults As Integer
    Private tagResults As Integer
    Private trackResults As Integer
    Private artistResults As Integer
    Private albumResults As Integer
    Private userResults As Integer
    Private progressMultiplier As Byte

#Region "UI"
    Private Sub FrmLoad(sender As Object, e As EventArgs) Handles Me.Load
        ' automatically switch to and fill in data from current tab
        Select Case frmMain.tabControl.SelectedIndex
            Case 0
                ' charts tab
                cmbContents.SelectedIndex = 0
                ' select worldwide or country
                If frmMain.radChartWorldwide.Checked = True Then
                    radChartWorldwide.Checked = True
                Else
                    radChartCountry.Checked = True
                End If
                ' fill in country
                cmbChartCountry.SelectedIndex = frmMain.cmbChartCountry.SelectedIndex
            Case 1
                ' tag tab
                cmbContents.SelectedIndex = 1
                txtTagTag.Text = frmMain.txtSearch.Text
            Case 2
                ' track tab
                cmbContents.SelectedIndex = 2
                txtTrackTitle.Text = frmMain.txtTrackTitle.Text
                txtTrackArtist.Text = frmMain.txtTrackArtist.Text
            Case 3
                ' artist tab
                cmbContents.SelectedIndex = 3
                txtArtistArtist.Text = frmMain.txtArtistName.Text
            Case 4
                ' album tab
                cmbContents.SelectedIndex = 4
                txtAlbumAlbum.Text = frmMain.txtAlbumTitle.Text
                txtAlbumArtist.Text = frmMain.txtAlbumArtist.Text
            Case 5
                ' user tab
                cmbContents.SelectedIndex = 5
                txtUserUser.Text = frmMain.txtUser.Text
            Case 6
                ' user lookup tab
                cmbContents.SelectedIndex = 5
                txtUserUser.Text = frmMain.txtUserL.Text
            Case Else
                cmbContents.SelectedIndex = 0
        End Select
    End Sub

    Private Sub ChangeContents(sender As Object, e As EventArgs) Handles cmbContents.SelectedIndexChanged
        InvisibleAllPanels()
        Select Case cmbContents.SelectedIndex
            Case 0
                pnlCharts.Visible = True
            Case 1
                pnlTag.Visible = True
            Case 2
                pnlTrack.Visible = True
            Case 3
                pnlArtist.Visible = True
            Case 4
                pnlAlbum.Visible = True
            Case 5
                pnlUser.Visible = True
        End Select
    End Sub

    Private Sub InvisibleAllPanels()
        pnlCharts.Visible = False
        pnlTag.Visible = False
        pnlTrack.Visible = False
        pnlArtist.Visible = False
        pnlAlbum.Visible = False
        pnlUser.Visible = False
    End Sub

    Private Sub Browse(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim result As DialogResult = sfdSave.ShowDialog()

        If result = DialogResult.OK Then
            txtSave.Text = sfdSave.FileName
        End If
    End Sub

    Private Sub StartButton(sender As Object, e As EventArgs) Handles btnStart.Click
        StartOp()
    End Sub

    Private Sub StopButton(sender As Object, e As EventArgs) Handles btnStop.Click
        StopOp()
    End Sub

    Private Sub FrmClose(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Ops"
    Private Sub UpdateProgress(finalStage As Boolean, percentage As Double, status As String)
        ' only add if the progressbar will not go over 95%
        If finalStage = False Then
            If percentage / progressMultiplier * 0.95 <= 95 Then
                Invoke(Sub() pbStatus.Value = percentage / progressMultiplier * 0.95)
            End If
        Else
            If percentage <> 100 Then
                Invoke(Sub() pbStatus.Value = 95 + percentage * 0.05)
            Else
                Invoke(Sub() pbStatus.Value = 100)
            End If
        End If

        Invoke(Sub() lblStatus.Text = status)
    End Sub

    Private Sub StartOp()
        ' ui
        btnStart.Enabled = False
        btnStop.Enabled = True
        progressMultiplier = 1
        UpdateProgress(False, 0, "Starting...")

        ' clear
        textContents.Clear()
        columnContents.Clear()
        dateContents.Clear()

        ' make sure something is entered in the browse field
        If txtSave.Text.Trim = String.Empty Then
            MessageBox.Show("Valid data must be entered in the Save Location field", "Backup Tool", MessageBoxButtons.OK, MessageBoxIcon.Error)
            StopOp()
            Exit Sub
        End If

        ' evaluate what needs to be backed up
        section = cmbContents.SelectedIndex
        Select Case section
            Case 0
                ' results
                chartResults = nudChartResults.Value

                ' country
                If radChartWorldwide.Checked = True Then
                    chartCountry = String.Empty
                Else
                    chartCountry = cmbChartCountry.Text
                End If

                ' columns
                columnContents.Add(chkChartTopTracks.Checked)
                columnContents.Add(chkChartTopArtists.Checked)
                columnContents.Add(chkChartTopTags.Checked)

                bgwChart.RunWorkerAsync()
            Case 1
                ' text
                If txtTagTag.Text.Trim = String.Empty Then
                    MessageBox.Show("Valid data must be entered in the Tag field", "Tag Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    StopOp()
                    Exit Sub
                End If
                textContents.Add(txtTagTag.Text)

                ' results
                tagResults = nudTagResults.Value

                ' columns
                columnContents.Add(chkTagInfo.Checked)
                columnContents.Add(chkTagTopTracks.Checked)
                columnContents.Add(chkTagTopArtists.Checked)
                columnContents.Add(chkTagTopAlbums.Checked)

                bgwTag.RunWorkerAsync()
            Case 2
                ' text
                If txtTrackTitle.Text.Trim = String.Empty OrElse txtTrackArtist.Text.Trim = String.Empty Then
                    MessageBox.Show("Valid data must be entered in the Track and Artist fields", "Track Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    StopOp()
                    Exit Sub
                End If
                textContents.Add(txtTrackTitle.Text)
                textContents.Add(txtTrackArtist.Text)

                ' columns
                columnContents.Add(chkTrackInfo.Checked)
                columnContents.Add(chkTrackStats.Checked)
                columnContents.Add(chkTrackTags.Checked)
                columnContents.Add(chkTrackSimilar.Checked)

                bgwTrack.RunWorkerAsync()
            Case 3
                ' text
                If txtArtistArtist.Text.Trim = String.Empty Then
                    MessageBox.Show("Valid data must be entered in the Artist field", "Artist Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    StopOp()
                    Exit Sub
                End If
                textContents.Add(txtArtistArtist.Text)

                ' columns
                columnContents.Add(chkArtistStats.Checked)
                columnContents.Add(chkArtistTags.Checked)
                columnContents.Add(chkArtistSimilar.Checked)
                columnContents.Add(chkArtistCharts.Checked)

                bgwArtist.RunWorkerAsync()
            Case 4
                ' text
                If txtAlbumAlbum.Text.Trim = String.Empty OrElse txtAlbumArtist.Text.Trim = String.Empty Then
                    MessageBox.Show("Valid data must be entered in the Album and Artist fields", "Album Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    StopOp()
                    Exit Sub
                End If
                textContents.Add(txtAlbumAlbum.Text)
                textContents.Add(txtAlbumArtist.Text)

                ' columns
                columnContents.Add(chkAlbumInfo.Checked)
                columnContents.Add(chkAlbumTracks.Checked)
                columnContents.Add(chkAlbumStats.Checked)
                columnContents.Add(chkAlbumTags.Checked)

                bgwAlbum.RunWorkerAsync()
            Case 5
                ' text
                If txtUserUser.Text.Trim = String.Empty Then
                    MessageBox.Show("Valid data must be entered in the User field", "User Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    StopOp()
                    Exit Sub
                End If
                textContents.Add(txtUserUser.Text)

                ' date
                If chkUserByDate.Checked = True Then
                    ' check that from is before to
                    If dtpUserFrom.Value > dtpUserTo.Value Then
                        MessageBox.Show("From date must be before to date", "User Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        StopOp()
                        Exit Sub
                    End If

                    dateContents.Add(dtpUserFrom.Value.Date)
                    dateContents.Add(dtpUserTo.Value.Date)
                End If

                ' results
                If radUserEntire.Checked = True Then
                    userResults = 0
                Else
                    userResults = nudUserNumber.Value
                End If

                ' columns
                columnContents.Add(chkUserInfo.Checked)
                columnContents.Add(chkUserFriends.Checked)
                columnContents.Add(chkUserLoved.Checked)
                columnContents.Add(chkUserHistory.Checked)
                columnContents.Add(chkUserCharts.Checked)

                bgwUser.RunWorkerAsync()
        End Select
    End Sub

    Private Sub StopOp()
        ' stop threads
        If bgwChart.IsBusy = True Then
            bgwChart.CancelAsync()
        End If
        If bgwTag.IsBusy = True Then
            bgwTag.CancelAsync()
        End If
        If bgwTrack.IsBusy = True Then
            bgwTrack.CancelAsync()
        End If
        If bgwArtist.IsBusy = True Then
            bgwArtist.CancelAsync()
        End If
        If bgwAlbum.IsBusy = True Then
            bgwAlbum.CancelAsync()
        End If
        If bgwUser.IsBusy = True Then
            bgwUser.CancelAsync()
        End If

        ' ui
        btnStart.Enabled = True
        btnStop.Enabled = False
        pbStatus.Value = 0
        lblStatus.Text = "Ready"
    End Sub

    Private Sub BackgroundStopOp(sender As BackgroundWorker, e As RunWorkerCompletedEventArgs) Handles bgwChart.RunWorkerCompleted, bgwTag.RunWorkerCompleted, bgwTrack.RunWorkerCompleted, bgwArtist.RunWorkerCompleted, bgwAlbum.RunWorkerCompleted, bgwUser.RunWorkerCompleted
        ' ui
        btnStart.Enabled = True
        btnStop.Enabled = False
        pbStatus.Value = 0
        lblStatus.Text = "Ready"
    End Sub

    ' the most retardedly confusing method ive ever written
    Private Sub Save(lists As List(Of String())())
        ' List of string()()    -1 contains all the categories
        '    List of string()	-2 contains all the lines in the category
        '        String()		-3 contains all the cells of a line
        '            String     -4 contains each individual cell of a line

        Dim outputList As New List(Of String())
        Dim currentLine As New List(Of String)
        Dim largestValue As Integer = 0

        UpdateProgress(True, 0, "Assembling...")

        ' get the largest list
        For Each list In lists ' 1 - finding the biggest category
            If list.Count - 1 > largestValue Then
                largestValue = list.Count - 1
            End If
        Next

        ' 3 - cycling through each line
        For line As Integer = 0 To largestValue
            ' 2 - cycling through the categories to add one line from each
            For list As Integer = 0 To lists.Count - 1
                ' attempt to add if there is more from the category, if not then add empty
                If lists(list).Count - 1 >= line Then
                    ' 4 - cycle through each cell of the line and add to current line
                    For Each item In lists(list)(line)
                        ' check for errors
                        If item.Contains("ERROR: ") = False Then
                            currentLine.Add(item.Replace(Chr(34), String.Empty))
                        Else
                            currentLine.Add(String.Empty)
                        End If
                    Next
                Else
                    ' add empty cells
                    For Each item In lists(list)(0)
                        currentLine.Add(String.Empty)
                    Next
                End If

                ' add separator only if not on the last list
                If list <> lists.Count - 1 Then
                    currentLine.Add("-")
                End If
            Next
            ' add and clear
            outputList.Add(currentLine.ToArray())
            currentLine.Clear()
        Next

        UpdateProgress(True, 50, "Writing...")

        ' save
        Dim fi As New FileInfo(txtSave.Text.Trim)

        ' delete file if it already exists
        If fi.Exists = True Then
            Try
                fi.Delete()
            Catch ex As IOException
                MessageBox.Show("Could not write to file" & vbCrLf & "Check that another program is not using the file", "Backup Tool", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Invoke(Sub() StopOp())
                Exit Sub
            End Try
        End If

        ' create new file and filestream
        Dim fs As FileStream
        Using fs
            fs = fi.Create()

            ' compile data to string
            Dim str As New StringBuilder()

            For Each line In outputList
                For cell As Integer = 0 To line.Count - 1
                    ' check if on the last cell
                    If cell < line.Count - 1 Then
                        str.Append(Chr(34) & line(cell) & Chr(34) & ",")

                    Else
                        ' if on last line dont add comma and add line
                        str.Append(Chr(34) & line(cell) & Chr(34))
                        str.AppendLine()
                    End If
                Next
            Next

            ' convert to bytearray and write
            Dim bytes As Byte() = New UTF32Encoding().GetBytes(str.ToString)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        End Using

        UpdateProgress(True, 100, "Success!")
    End Sub
#End Region

#Region "Charts"
    Private Sub ChartOp(sender As Object, e As DoWorkEventArgs) Handles bgwChart.DoWork
        Try
            ' init
            Threading.Thread.CurrentThread.Name = "BackupChart"
            Dim topTrackInfo As New List(Of String())
            Dim topArtistInfo As New List(Of String())
            Dim topTagInfo As New List(Of String())
            Dim lists As New List(Of List(Of String()))
            Dim progress As Integer = 0

            ' progress multiplier
            progressMultiplier = 0
            If columnContents(0) = True Then
                topTrackInfo.Add({"Track", "Artist", "Listeners", "Playcount"})
                progressMultiplier += 1
            End If
            If columnContents(1) = True Then
                topArtistInfo.Add({"Artist", "Listeners", "Playcount"})
                progressMultiplier += 1
            End If
            If columnContents(2) = True Then
                topTagInfo.Add({"Tag", "Reach", "Taggings"})
                progressMultiplier += 1
            End If

            ' top tracks
            If columnContents(0) = True Then
                lists.Add(topTrackInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(chartResults / 50)
                Dim xmlPages As New List(Of String)

                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting top tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If chartResults <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("chart.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & chartResults.ToString))
                        ElseIf chartResults Mod 50 = 0 Then
                            ' if no remainder
                            xmlPages.Add(CallAPI("chart.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50"))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("chart.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & (chartResults Mod 50).ToString()))
                        End If
                    Else
                        xmlPages.Add(CallAPI("chart.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50"))
                    End If

                    ' cancel check
                    If bgwChart.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing top tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For track As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "artist/name", "listeners", "playcount"}
                        ParseXML(xmlPages(currentPage), "/lfm/tracks/track", track, xmlNodes)
                        topTrackInfo.Add(xmlNodes)
                    Next

                    ' cancel check
                    If bgwChart.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 20
            End If

            ' top artists
            If columnContents(1) = True Then
                lists.Add(topArtistInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(chartResults / 50)
                Dim xmlPages As New List(Of String)
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting top artists... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If chartResults <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("chart.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & chartResults.ToString))
                        ElseIf chartResults Mod 50 = 0 Then
                            ' if no remainder
                            xmlPages.Add(CallAPI("chart.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50"))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("chart.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & (chartResults Mod 50).ToString()))
                        End If
                    Else
                        xmlPages.Add(CallAPI("chart.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50"))
                    End If

                    ' cancel check
                    If bgwChart.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing top artists... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For artist As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "listeners", "playcount"}
                        ParseXML(xmlPages(currentPage), "/lfm/artists/artist", artist, xmlNodes)
                        topArtistInfo.Add(xmlNodes)
                    Next

                    ' cancel check
                    If bgwChart.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 20
            End If

            ' top tags
            If columnContents(2) = True Then
                lists.Add(topTagInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(chartResults / 50)
                Dim xmlPages As New List(Of String)
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting top tags... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If chartResults <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("chart.getTopTags", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & chartResults.ToString))
                        ElseIf chartResults Mod 50 = 0 Then
                            ' if no remainder
                            xmlPages.Add(CallAPI("chart.getTopTags", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50"))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("chart.getTopTags", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & (chartResults Mod 50).ToString()))
                        End If
                    Else
                        xmlPages.Add(CallAPI("chart.getTopTags", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50"))
                    End If

                    ' cancel check
                    If bgwChart.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing top tags... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For tag As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "reach", "taggings"}
                        ParseXML(xmlPages(currentPage), "/lfm/tags/tag", tag, xmlNodes)
                        topTagInfo.Add(xmlNodes)
                    Next

                    ' cancel check
                    If bgwChart.CancellationPending = True Then
                        Exit Sub
                    End If
                Next
            End If

            Save(lists.ToArray)
            Invoke(Sub() StopOp())
        Catch ex As Exception
            Invoke(Sub() MessageBox.Show("ERROR: " & ex.Message), "Charts Backup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Tag"
    Private Sub TagOp(sender As Object, e As DoWorkEventArgs) Handles bgwTag.DoWork
        Try
            ' init
            Threading.Thread.CurrentThread.Name = "BackupTag"
            Dim tagInfo As New List(Of String())
            Dim topTrackInfo As New List(Of String())
            Dim topArtistInfo As New List(Of String())
            Dim topAlbumInfo As New List(Of String())
            Dim lists As New List(Of List(Of String()))
            Dim progress As Integer = 0

            ' verify
            If VerifyTag(textContents(0)).Contains("ERROR: ") = True Then
                MessageBox.Show("Tag data unable to be retrived" & vbCrLf & "Check that you have spelled your search terms correctly", "Backup Tag", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Invoke(Sub() StopOp())
                Exit Sub
            End If

            ' progress multiplier
            progressMultiplier = 0
            ' info
            If columnContents(0) = True Then
                tagInfo.Add({"Name", "Taggings", "Reach"})
                progressMultiplier += 1
            End If
            ' tracks
            If columnContents(1) = True Then
                topTrackInfo.Add({"Track", "Artist"})
                progressMultiplier += 1
            End If
            ' artists
            If columnContents(2) = True Then
                topArtistInfo.Add({"Artist"})
                progressMultiplier += 1
            End If
            ' albums
            If columnContents(3) = True Then
                topAlbumInfo.Add({"Album", "Artist"})
                progressMultiplier += 1
            End If

            ' info
            If columnContents(0) = True Then
                lists.Add(tagInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(tagResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting tag info...")

                xmlPage = CallAPI("tag.getInfo", String.Empty, "tag=" & textContents(0))

                ' cancel check
                If bgwTag.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing tag info...")

                ' parse
                xmlNodes = {"name", "total", "reach"}
                ParseXML(xmlPage, "/lfm/tag", 0, xmlNodes)
                tagInfo.Add(xmlNodes)

                ' cancel check
                If bgwTag.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' top tracks
            If columnContents(1) = True Then
                lists.Add(topTrackInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(tagResults / 50)
                Dim xmlPages As New List(Of String)
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting top tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If tagResults <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("tag.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & tagResults.ToString, "tag=" & textContents(0)))
                        ElseIf tagResults Mod 50 = 0 Then
                            ' if no remainder
                            xmlPages.Add(CallAPI("tag.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50", "tag=" & textContents(0)))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("tag.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & (tagResults Mod 50).ToString(), "tag=" & textContents(0)))
                        End If
                    Else
                        xmlPages.Add(CallAPI("tag.getTopTracks", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50", "tag=" & textContents(0)))
                    End If

                    ' cancel check
                    If bgwTag.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing top tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For artist As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "artist/name"}
                        ParseXML(xmlPages(currentPage), "/lfm/tracks/track", artist, xmlNodes)
                        topTrackInfo.Add(xmlNodes)
                    Next

                    ' cancel check
                    If bgwTag.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 20
            End If

            ' top artists
            If columnContents(1) = True Then
                lists.Add(topArtistInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(tagResults / 50)
                Dim xmlPages As New List(Of String)
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting top artists... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If tagResults <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("tag.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & tagResults.ToString, "tag=" & textContents(0)))
                        ElseIf tagResults Mod 50 = 0 Then
                            ' if no remainder
                            xmlPages.Add(CallAPI("tag.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50", "tag=" & textContents(0)))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("tag.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & (tagResults Mod 50).ToString, "tag=" & textContents(0)))
                        End If
                    Else
                        xmlPages.Add(CallAPI("tag.getTopArtists", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50", "tag=" & textContents(0)))
                    End If

                    ' cancel check
                    If bgwTag.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing top artists... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For artist As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name"}
                        ParseXML(xmlPages(currentPage), "/lfm/topartists/artist", artist, xmlNodes)
                        topArtistInfo.Add(xmlNodes)
                    Next

                    ' cancel check
                    If bgwTag.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 20
            End If

            ' top albums
            If columnContents(3) = True Then
                lists.Add(topAlbumInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(tagResults / 50)
                Dim xmlPages As New List(Of String)
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting top albums... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If tagResults <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("tag.getTopAlbums", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & tagResults.ToString, "tag=" & textContents(0)))
                        ElseIf tagResults Mod 50 = 0 Then
                            ' if no remainder
                            xmlPages.Add(CallAPI("tag.getTopAlbums", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50", "tag=" & textContents(0)))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("tag.getTopAlbums", String.Empty, "page=" & (currentPage + 1).ToString, "limit=" & (tagResults Mod 50).ToString, "tag=" & textContents(0)))
                        End If
                    Else
                        xmlPages.Add(CallAPI("tag.getTopAlbums", String.Empty, "page=" & (currentPage + 1).ToString, "limit=50", "tag=" & textContents(0)))
                    End If

                    ' cancel check
                    If bgwTag.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing top albums... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For album As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "artist/name"}
                        ParseXML(xmlPages(currentPage), "/lfm/albums/album", album, xmlNodes)
                        topAlbumInfo.Add(xmlNodes)
                    Next

                    ' cancel check
                    If bgwTag.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 20
            End If

            Save(lists.ToArray)
            Invoke(Sub() StopOp())
        Catch ex As Exception
            Invoke(Sub() MessageBox.Show("ERROR: " & ex.Message, "Tag Backup", MessageBoxButtons.OK, MessageBoxIcon.Error))
        End Try
    End Sub
#End Region

#Region "Track"
    Private Sub TrackOp(sender As Object, e As DoWorkEventArgs) Handles bgwTrack.DoWork
        Try
            ' init
            Threading.Thread.CurrentThread.Name = "BackupTrack"
            Dim trackInfo As New List(Of String())
            Dim statsInfo As New List(Of String())
            Dim tagsInfo As New List(Of String())
            Dim similarInfo As New List(Of String())
            Dim lists As New List(Of List(Of String()))
            Dim progress As Integer = 0

            ' verify
            If VerifyTrack(textContents(0), textContents(1))(0).Contains("ERROR: ") = True Then
                MessageBox.Show("Track data unable to be retrived" & vbCrLf & "Check that you have spelled your search terms correctly", "Backup Track", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Invoke(Sub() StopOp())
                Exit Sub
            End If

            ' progress multiplier
            progressMultiplier = 0
            ' info
            If columnContents(0) = True Then
                trackInfo.Add({"Name", "Artist", "Album", "Duration (s)"})
                progressMultiplier += 1
            End If
            ' stats
            If columnContents(1) = True Then
                statsInfo.Add({"Listeners", "Playcount"})
                progressMultiplier += 1
            End If
            ' tags
            If columnContents(2) = True Then
                tagsInfo.Add({"Tag", "Taggings"})
                progressMultiplier += 1
            End If
            ' similar
            If columnContents(3) = True Then
                similarInfo.Add({"Track", "Artist", "Match %"})
                progressMultiplier += 1
            End If

            ' info
            If columnContents(0) = True Then
                lists.Add(trackInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(trackResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting track info...")

                xmlPage = CallAPI("track.getInfo", String.Empty, "track=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing track info...")

                ' parse
                xmlNodes = {"name", "artist/name", "album/title", "duration"}
                ParseXML(xmlPage, "/lfm/track", 0, xmlNodes)

                ' fix album if not present
                If xmlNodes(2).Contains("ERROR: ") = True Then
                    xmlNodes(2) = String.Empty
                End If

                ' fix duration if not present
                If xmlNodes(3) = "0" Then
                    xmlNodes(3) = String.Empty
                Else
                    xmlNodes(3) = (CInt(xmlNodes(3)) / 1000).ToString("N0")
                End If

                trackInfo.Add(xmlNodes)

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' stats
            If columnContents(1) = True Then
                lists.Add(statsInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(trackResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting stats info...")

                xmlPage = CallAPI("track.getInfo", String.Empty, "track=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing stats info...")

                ' parse
                xmlNodes = {"listeners", "playcount"}
                ParseXML(xmlPage, "/lfm/track", 0, xmlNodes)

                statsInfo.Add(xmlNodes)

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' tags
            If columnContents(2) = True Then
                lists.Add(tagsInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(trackResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting tag info...")

                xmlPage = CallAPI("track.getTopTags", String.Empty, "track=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing tag info...")

                ' get amount of tags and handle if there are no tags
                Dim tagCount As Integer = StrCount(xmlPage, "<name>")
                If tagCount > 0 Then
                    ' parse
                    For tag As UInteger = 0 To tagCount - 1
                        xmlNodes = {"name", "count"}
                        ParseXML(xmlPage, "/lfm/toptags/tag", tag, xmlNodes)

                        tagsInfo.Add(xmlNodes)
                    Next
                Else
                    tagsInfo.Add({String.Empty})
                End If

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' similar
            If columnContents(3) = True Then
                lists.Add(similarInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(trackResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting similar tracks...")

                xmlPage = CallAPI("track.getSimilar", String.Empty, "track=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing similar tracks...")

                ' get amount of tracks and handle if there are no tracks
                Dim trackCount As Integer = StrCount(xmlPage, "<track>")
                If trackCount > 0 Then
                    ' parse
                    For track As UInteger = 0 To trackCount - 1
                        xmlNodes = {"name", "artist/name", "match"}
                        ParseXML(xmlPage, "/lfm/similartracks/track", track, xmlNodes)

                        ' convert match from decimal to percent
                        xmlNodes(2) = CDbl(xmlNodes(2)).ToString("P")

                        similarInfo.Add(xmlNodes)
                    Next
                Else
                    similarInfo.Add({String.Empty})
                End If

                ' cancel check
                If bgwTrack.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            Save(lists.ToArray)
            Invoke(Sub() StopOp())
        Catch ex As Exception
            Invoke(Sub() MessageBox.Show("ERROR: " & ex.Message, "Track Backup", MessageBoxButtons.OK, MessageBoxIcon.Error))
        End Try
    End Sub
#End Region

#Region "Artist"
    Private Sub ArtistOp(sender As Object, e As DoWorkEventArgs) Handles bgwArtist.DoWork
        Try
            ' init
            Threading.Thread.CurrentThread.Name = "BackupArtist"
            Dim statsInfo As New List(Of String())
            Dim tagsInfo As New List(Of String())
            Dim similarInfo As New List(Of String())
            Dim chartsInfo As New List(Of String())
            Dim lists As New List(Of List(Of String()))
            Dim progress As Integer = 0

            ' verify
            If VerifyArtist(textContents(0)).Contains("ERROR: ") = True Then
                MessageBox.Show("Artist data unable to be retrived" & vbCrLf & "Check that you have spelled your search terms correctly", "Backup Artist", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Invoke(Sub() StopOp())
                Exit Sub
            End If

            ' progress multiplier
            progressMultiplier = 0
            ' stats
            If columnContents(0) = True Then
                statsInfo.Add({"Artist", "Listeners", "Playcount"})
                progressMultiplier += 1
            End If
            ' tags
            If columnContents(1) = True Then
                tagsInfo.Add({"Tag", "Count"})
                progressMultiplier += 1
            End If
            ' similar
            If columnContents(2) = True Then
                similarInfo.Add({"Artist", "Match %"})
                progressMultiplier += 1
            End If
            ' charts
            If columnContents(3) = True Then
                chartsInfo.Add({"Top Track", "Top Album"})
                progressMultiplier += 1
            End If

            ' stats
            If columnContents(0) = True Then
                lists.Add(statsInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(artistResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting stats info...")

                xmlPage = CallAPI("artist.getInfo", String.Empty, "artist=" & textContents(0), "autocorrect=1")

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing stats info...")

                ' parse
                xmlNodes = {"name", "stats/listeners", "stats/playcount"}
                ParseXML(xmlPage, "/lfm/artist", 0, xmlNodes)

                statsInfo.Add(xmlNodes)

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' tags
            If columnContents(1) = True Then
                lists.Add(tagsInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(artistResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting tag info...")

                xmlPage = CallAPI("artist.getTopTags", String.Empty, "artist=" & textContents(0), "autocorrect=1")

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing tag info...")

                ' get amount of tags and handle if there are no tags
                Dim tagCount As Integer = StrCount(xmlPage, "<name>")
                If tagCount > 0 Then
                    ' parse
                    For tag As UInteger = 0 To tagCount - 1
                        xmlNodes = {"name", "count"}
                        ParseXML(xmlPage, "/lfm/toptags/tag", tag, xmlNodes)

                        tagsInfo.Add(xmlNodes)
                    Next
                End If

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' similar
            If columnContents(2) = True Then
                lists.Add(similarInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(artistResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting similar artists...")

                xmlPage = CallAPI("artist.getSimilar", String.Empty, "artist=" & textContents(0), "autocorrect=1")

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing similar artists...")

                ' get amount of tags and handle if there are no tags
                Dim artistCount As Integer = StrCount(xmlPage, "<artist>")
                If artistCount > 0 Then
                    ' parse
                    For artist As UInteger = 0 To artistCount - 1
                        xmlNodes = {"name", "match"}
                        ParseXML(xmlPage, "/lfm/similarartists/artist", artist, xmlNodes)

                        ' convert match from decimal to percent
                        xmlNodes(1) = CDbl(xmlNodes(1)).ToString("P")

                        similarInfo.Add(xmlNodes)
                    Next
                End If

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' charts
            If columnContents(3) = True Then
                lists.Add(chartsInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(artistResults / 50)
                Dim xmlPage(1) As String

                ' progress
                UpdateProgress(False, progress, "Getting artist charts...")

                xmlPage(0) = CallAPI("artist.getTopTracks", String.Empty, "artist=" & textContents(0), "autocorrect=1") ' tracks
                xmlPage(1) = CallAPI("artist.getTopAlbums", String.Empty, "artist=" & textContents(0), "autocorrect=1") ' albums

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()
                Dim xmlNodesFinal As New List(Of String)

                ' progress
                UpdateProgress(False, progress, "Parsing similar artists...")

                ' get amount of tracks and handle if there are no tracks
                Dim count As Integer = StrCount(xmlPage(0), "</track>")
                If count > 0 Then
                    ' parse
                    For artist As UInteger = 0 To count - 1
                        xmlNodes = {"name"}
                        ParseXML(xmlPage(0), "/lfm/toptracks/track", artist, xmlNodes)

                        ' add to final list
                        xmlNodesFinal.Add(xmlNodes(0))

                        xmlNodes = {"name"}
                        ParseXML(xmlPage(1), "/lfm/topalbums/album", artist, xmlNodes)

                        xmlNodesFinal.Add(xmlNodes(0))

                        chartsInfo.Add(xmlNodesFinal.ToArray())
                        xmlNodesFinal.Clear()
                    Next
                End If

                ' cancel check
                If bgwArtist.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            Save(lists.ToArray)
            Invoke(Sub() StopOp())
        Catch ex As Exception
            Invoke(Sub() MessageBox.Show("ERROR: " & ex.Message, "Artist Backup", MessageBoxButtons.OK, MessageBoxIcon.Error))
        End Try
    End Sub
#End Region

#Region "Album"
    Private Sub AlbumOp(sender As Object, e As DoWorkEventArgs) Handles bgwAlbum.DoWork
        Try
            ' init
            Threading.Thread.CurrentThread.Name = "BackupAlbum"
            Dim albumInfo As New List(Of String())
            Dim tracksInfo As New List(Of String())
            Dim statsInfo As New List(Of String())
            Dim tagsInfo As New List(Of String())
            Dim lists As New List(Of List(Of String()))
            Dim progress As Integer = 0

            ' verify
            If VerifyAlbum(textContents(0), textContents(1))(0).Contains("ERROR: ") = True Then
                MessageBox.Show("Album data unable to be retrived" & vbCrLf & "Check that you have spelled your search terms correctly", "Backup Album", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Invoke(Sub() StopOp())
                Exit Sub
            End If

            ' progress multiplier
            progressMultiplier = 0
            ' info
            If columnContents(0) = True Then
                albumInfo.Add({"Album", "Artist"})
                progressMultiplier += 1
            End If
            ' tracks
            If columnContents(1) = True Then
                tracksInfo.Add({"Track", "Duration (s)"})
                progressMultiplier += 1
            End If
            ' stats
            If columnContents(2) = True Then
                statsInfo.Add({"Listeners", "Playcount"})
                progressMultiplier += 1
            End If
            ' tags
            If columnContents(3) = True Then
                tagsInfo.Add({"Tag", "Taggings"})
                progressMultiplier += 1
            End If

            ' info
            If columnContents(0) = True Then
                lists.Add(albumInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(albumResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting album info...")

                xmlPage = CallAPI("album.getInfo", String.Empty, "album=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing album info...")

                ' parse
                xmlNodes = {"name", "artist"}
                ParseXML(xmlPage, "/lfm/album", 0, xmlNodes)

                albumInfo.Add(xmlNodes)

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' tracks
            If columnContents(1) = True Then
                lists.Add(tracksInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(albumResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting tracks info...")

                xmlPage = CallAPI("album.getInfo", String.Empty, "album=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing tracks info...")

                ' get amount of tags and handle if there are no tags
                Dim trackCount As Integer = StrCount(xmlPage, "</track>")
                If trackCount > 0 Then
                    ' parse
                    For track As UInteger = 0 To trackCount - 1
                        xmlNodes = {"name", "duration"}
                        ParseXML(xmlPage, "/lfm/album/tracks/track", track, xmlNodes)

                        tracksInfo.Add(xmlNodes)
                    Next
                End If

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' stats
            If columnContents(2) = True Then
                lists.Add(statsInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(albumResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting stats info...")

                xmlPage = CallAPI("album.getInfo", String.Empty, "album=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing stats info...")

                ' parse
                xmlNodes = {"listeners", "playcount"}
                ParseXML(xmlPage, "/lfm/album", 0, xmlNodes)

                statsInfo.Add(xmlNodes)

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' tags
            If columnContents(3) = True Then
                lists.Add(tagsInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(albumResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting tag info...")

                xmlPage = CallAPI("album.getTopTags", String.Empty, "album=" & textContents(0), "artist=" & textContents(1), "autocorrect=1")

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' add to list
                Dim xmlNodes As String()

                ' progress
                UpdateProgress(False, progress, "Parsing tag info...")

                ' get amount of tags and handle if there are no tags
                Dim tagCount As Integer = StrCount(xmlPage, "<name>")
                If tagCount > 0 Then
                    ' parse
                    For tag As UInteger = 0 To tagCount - 1
                        xmlNodes = {"name", "count"}
                        ParseXML(xmlPage, "/lfm/toptags/tag", tag, xmlNodes)

                        tagsInfo.Add(xmlNodes)
                    Next
                Else
                    tagsInfo.Add({String.Empty})
                End If

                ' cancel check
                If bgwAlbum.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            Save(lists.ToArray)
            Invoke(Sub() StopOp())
        Catch ex As Exception
            Invoke(Sub() MessageBox.Show("ERROR: " & ex.Message, "Album Backup", MessageBoxButtons.OK, MessageBoxIcon.Error))
        End Try
    End Sub
#End Region

#Region "User"
    Private Sub UserOp(sender As Object, e As DoWorkEventArgs) Handles bgwUser.DoWork
        Try
            ' init
            Threading.Thread.CurrentThread.Name = "UserBackup"
            Dim userInfo As New List(Of String())
            Dim friendsInfo As New List(Of String())
            Dim lovedInfo As New List(Of String())
            Dim historyInfo As New List(Of String())
            Dim topTrackInfo As New List(Of String())
            Dim topArtistInfo As New List(Of String())
            Dim topAlbumInfo As New List(Of String())
            Dim lists As New List(Of List(Of String()))
            Dim progress As Integer = 0

            ' progress multiplier
            progressMultiplier = 0
            ' info
            If columnContents(0) = True Then
                userInfo.Add({"Username", "Real Name", "Url", "Country", "Age", "Gender", "Playcount", "Playlists", "Date Registered"})
                progressMultiplier += 1
            End If
            ' friends
            If columnContents(1) = True Then
                friendsInfo.Add({"Name", "Real Name", "Url", "Date Registered"})
                progressMultiplier += 1
            End If
            ' loved songs
            If columnContents(2) = True Then
                lovedInfo.Add({"Loved Track", "Artist", "Date Loved (Unix)"})
                progressMultiplier += 1
            End If
            ' history
            If columnContents(3) = True Then
                historyInfo.Add({"Historical Track", "Artist", "Album", "Date Scrobbled"})
                progressMultiplier += 1
            End If
            ' charts
            If columnContents(4) = True Then
                topTrackInfo.Add({"Top Track", "Artist", "User Playcount"})
                topArtistInfo.Add({"Top Artist", "User Playcount"})
                topAlbumInfo.Add({"Top Album", "Artist", "User Playcount"})
                progressMultiplier += 1
            End If

            ' info
            If columnContents(0) = True Then
                lists.Add(userInfo)
                ' get all xml pages
                Dim pageAmount As Integer = Math.Ceiling(userResults / 50)
                Dim xmlPage As String

                ' progress
                UpdateProgress(False, progress, "Getting user info...")

                xmlPage = CallAPI("user.getInfo", textContents(0))

                ' cancel check
                If bgwUser.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 80

                ' progress
                UpdateProgress(False, progress, "Parsing user info...")

                ' parse
                Dim xmlNodes() As String = {"name", "realname", "url", "country", "age", "gender", "playcount", "playlists", "registered"}
                ParseXML(xmlPage, "/lfm/user", 0, xmlNodes)

                ' gender formatting
                Select Case xmlNodes(5)
                    Case "m"
                        xmlNodes(5) = "Male"
                    Case "f"
                        xmlNodes(5) = "Female"
                    Case Else
                        xmlNodes(5) = "Not Specified"
                End Select

                ' age formatting
                If xmlNodes(4) = "0" Then
                    xmlNodes(4) = "Not Specified"
                End If

                userInfo.Add(xmlNodes)

                ' cancel check
                If bgwUser.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 20
            End If

            ' friends
            If columnContents(1) = True Then
                lists.Add(friendsInfo)
                ' get total
                Dim userResults2 As Integer = CInt(ParseMetadata(CallAPI("user.getFriends", textContents(0)), "total="))
                Dim pageAmount As Integer = Math.Ceiling(userResults2 / 50)
                Dim xmlPages As New List(Of String)

                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting user friends... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If userResults2 <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("user.getFriends", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & userResults2.ToString))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("user.getFriends", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                        End If
                    Else
                        xmlPages.Add(CallAPI("user.getFriends", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                    End If

                    ' cancel check
                    If bgwUser.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing user friends... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For user As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "realname", "url", "registered"}
                        ParseXML(xmlPages(currentPage), "/lfm/friends/user", user, xmlNodes)
                        friendsInfo.Add(xmlNodes)

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Next

                progress += 20
            End If

            ' loved songs
            If columnContents(2) = True Then
                lists.Add(lovedInfo)
                ' get total
                Dim userResults2 As Integer = CInt(ParseMetadata(CallAPI("user.getLovedTracks", textContents(0)), "total="))
                Dim pageAmount As Integer = Math.Ceiling(userResults2 / 50)
                Dim xmlPages As New List(Of String)

                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting loved tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        ' last page, only request leftover
                        If userResults2 <= 50 Then
                            ' if results below 50
                            xmlPages.Add(CallAPI("user.getLovedTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & userResults2.ToString))
                        Else
                            ' if not below 50 get remainder
                            xmlPages.Add(CallAPI("user.getLovedTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                        End If
                    Else
                        xmlPages.Add(CallAPI("user.getLovedTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                    End If

                    ' cancel check
                    If bgwUser.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing loved tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                    currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                    ' parse
                    For track As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "artist/name", "date"}
                        ParseXML(xmlPages(currentPage), "/lfm/lovedtracks/track", track, xmlNodes)
                        lovedInfo.Add(xmlNodes)

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Next

                progress += 20
            End If

            ' play history
            If columnContents(3) = True Then
                lists.Add(historyInfo)
                ' get total
                Dim userResults2 As Integer
                If userResults = 0 Then
                    If dateContents.Count = 0 Then
                        userResults2 = CInt(ParseMetadata(CallAPI("user.getRecentTracks", textContents(0)), "total="))
                    Else
                        userResults2 = CInt(ParseMetadata(CallAPI("user.getRecentTracks", textContents(0), "from=" & DateToUnix(dateContents(0)).ToString(), "to=" & DateToUnix(dateContents(1)).ToString()), "total="))
                    End If
                Else
                    userResults2 = userResults
                End If
                Dim pageAmount As Integer = Math.Ceiling(userResults2 / 200)
                Dim xmlPages As New List(Of String)

                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 80) + progress, "Getting play history... (" & ((currentPage + 1) * 200).ToString() & " of " & (pageAmount * 200).ToString() & ")")

                    If dateContents.Count = 0 Then
                        If currentPage = pageAmount - 1 Then
                            ' last page, only request leftover
                            If userResults2 <= 200 Then
                                ' if results below 200
                                xmlPages.Add(CallAPI("user.getRecentTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & userResults2.ToString))
                            Else
                                ' if not below 200 get remainder
                                xmlPages.Add(CallAPI("user.getRecentTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=200"))
                            End If
                        Else
                            xmlPages.Add(CallAPI("user.getRecentTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=200"))
                        End If
                    Else
                        If currentPage = pageAmount - 1 Then
                            ' last page, only request leftover
                            If userResults2 <= 200 Then
                                ' if results below 200
                                xmlPages.Add(CallAPI("user.getRecentTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & userResults2.ToString, "from=" & DateToUnix(dateContents(0)).ToString(), "to=" & DateToUnix(dateContents(1)).ToString()))
                            Else
                                ' if not below 200 get remainder
                                xmlPages.Add(CallAPI("user.getRecentTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=200", "from=" & DateToUnix(dateContents(0)).ToString(), "to=" & DateToUnix(dateContents(1)).ToString()))
                            End If
                        Else
                            xmlPages.Add(CallAPI("user.getRecentTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=200", "from=" & DateToUnix(dateContents(0)).ToString(), "to=" & DateToUnix(dateContents(1)).ToString()))
                        End If
                    End If

                    ' cancel check
                    If bgwUser.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 80

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                For currentPage As Integer = 0 To pageAmount - 1
                    ' progress
                    UpdateProgress(False, (((currentPage + 1) / pageAmount) * 20) + progress, "Parsing play history... (" & ((currentPage + 1) * 200).ToString() & " of " & (pageAmount * 200).ToString() & ")")

                    If currentPage = pageAmount - 1 Then
                        currentPageAmount = userResults2 Mod 200
                    Else
                        Try
                            currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)
                        Catch ex As Exception
                            currentPageAmount = 200
                        End Try
                    End If


                    ' parse
                    For track As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "artist", "album", "date"}
                        ParseXML(xmlPages(currentPage), "/lfm/recenttracks/track", track, xmlNodes)

                        ' check for now playing
                        If track = 0 AndAlso currentPage = 0 AndAlso xmlNodes(3).Contains("Object reference not set to an instance of an object") = True Then
                            xmlNodes(3) = "Now Playing"
                        End If

                        historyInfo.Add(xmlNodes)

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next

                    ' cancel check
                    If bgwUser.CancellationPending = True Then
                        Exit Sub
                    End If
                Next

                progress += 20
            End If

            ' charts
            If columnContents(4) = True Then
                lists.Add(topTrackInfo)
                lists.Add(topArtistInfo)
                lists.Add(topAlbumInfo)
                ' results
                Dim topTrackResults, topArtistResults, topAlbumResults As Integer
                If userResults = 0 Then
                    If dateContents.Count = 0 Then
                        ' if entire but no date
                        topTrackResults = CInt(ParseMetadata(CallAPI("user.getTopTracks", textContents(0)), "total="))
                        topArtistResults = CInt(ParseMetadata(CallAPI("user.getTopArtists", textContents(0)), "total="))
                        topAlbumResults = CInt(ParseMetadata(CallAPI("user.getTopAlbums", textContents(0)), "total="))
                    End If
                Else
                    ' if not entire 
                    topTrackResults = userResults
                    topArtistResults = userResults
                    topAlbumResults = userResults
                End If
                Dim pageAmount As Integer
                Dim xmlPages As New List(Of String)
                Dim xmlPage As String

#Region "Top Tracks"
                If dateContents.Count = 0 Then
                    pageAmount = Math.Ceiling(topTrackResults / 50)

                    ' no date, needs mutliple pages
                    For currentPage As Integer = 0 To pageAmount - 1
                        ' progress
                        UpdateProgress(False, (((currentPage + 1) / pageAmount) * 26.66) + progress, "Getting user top tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                        If currentPage = pageAmount - 1 Then
                            ' last page, only request leftover
                            If topTrackResults <= 50 Then
                                ' if results below 50
                                xmlPages.Add(CallAPI("user.getTopTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & topTrackResults.ToString))
                            ElseIf topTrackResults Mod 50 = 0 Then
                                ' if no remainder
                                xmlPages.Add(CallAPI("user.getTopTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                            Else
                                ' if not below 50 get remainder
                                xmlPages.Add(CallAPI("user.getTopTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & (topTrackResults Mod 50).ToString))
                            End If
                        Else
                            xmlPages.Add(CallAPI("user.getTopTracks", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                        End If

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Else
                    ' date
                    ' progress
                    UpdateProgress(False, progress, "Getting user top tracks...")

                    xmlPage = CallAPI("user.getWeeklyTrackChart", textContents(0), "from=" & DateToUnix(dateContents(0)).ToString(), "to=" & DateToUnix(dateContents(1)).ToString())
                    ' results
                    If userResults = 0 Then
                        ' if entire
                        topTrackResults = StrCount(xmlPage, "</track>")
                    Else
                        ' if not entire
                        topTrackResults = userResults
                    End If
                    xmlPages.Add(xmlPage)
                End If

                ' cancel check
                If bgwUser.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 26.66

                ' add to list
                Dim currentPageAmount As Integer
                Dim xmlNodes As String()
                If dateContents.Count = 0 Then
                    For currentPage As Integer = 0 To pageAmount - 1
                        ' progress
                        UpdateProgress(False, (((currentPage + 1) / pageAmount) * 6.66) + progress, "Parsing user top tracks... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                        currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                        ' parse
                        For track As Integer = 0 To currentPageAmount - 1
                            xmlNodes = {"name", "artist/name", "playcount"}
                            ParseXML(xmlPages(currentPage), "/lfm/toptracks/track", track, xmlNodes)

                            topTrackInfo.Add(xmlNodes)
                        Next

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Else
                    ' progress
                    UpdateProgress(False, progress, "Parsing user top tracks...")

                    currentPageAmount = topTrackResults

                    For track As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "artist", "playcount"}
                        ParseXML(xmlPage, "/lfm/weeklytrackchart/track", track, xmlNodes)

                        topTrackInfo.Add(xmlNodes)

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                End If

                progress += 6.67
#End Region

#Region "Top Artists"
                xmlPages.Clear()
                If dateContents.Count = 0 Then
                    pageAmount = Math.Ceiling(topArtistResults / 50)

                    ' no date, needs mutliple pages
                    For currentPage As Integer = 0 To pageAmount - 1
                        ' progress
                        UpdateProgress(False, (((currentPage + 1) / pageAmount) * 26.66) + progress, "Getting user top artists... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                        If currentPage = pageAmount - 1 Then
                            ' last page, only request leftover
                            If topArtistResults <= 50 Then
                                ' if results below 50
                                xmlPages.Add(CallAPI("user.getTopArtists", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & topArtistResults.ToString))
                            ElseIf topArtistResults Mod 50 = 0 Then
                                ' if no remainder
                                xmlPages.Add(CallAPI("user.getTopArtists", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                            Else
                                ' if not below 50 get remainder
                                xmlPages.Add(CallAPI("user.getTopArtists", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & (topArtistResults Mod 50).ToString))
                            End If
                        Else
                            xmlPages.Add(CallAPI("user.getTopArtists", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                        End If

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Else
                    ' date
                    ' progress
                    UpdateProgress(False, progress, "Getting user top artists...")

                    xmlPage = CallAPI("user.getWeeklyArtistChart", textContents(0), "from=" & DateToUnix(dateContents(0)).ToString(), "to=" & DateToUnix(dateContents(1)).ToString())
                    ' results
                    If userResults = 0 Then
                        ' if entire
                        topArtistResults = StrCount(xmlPage, "</artist>")
                    Else
                        ' if not entire
                        topArtistResults = userResults
                    End If
                    xmlPages.Add(xmlPage)
                End If

                ' cancel check
                If bgwUser.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 26.66

                ' add to list
                If dateContents.Count = 0 Then
                    For currentPage As Integer = 0 To pageAmount - 1
                        ' progress
                        UpdateProgress(False, (((currentPage + 1) / pageAmount) * 6.66) + progress, "Parsing user top artists... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                        currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                        ' parse
                        For artist As Integer = 0 To currentPageAmount - 1
                            xmlNodes = {"name", "playcount"}
                            ParseXML(xmlPages(currentPage), "/lfm/topartists/artist", artist, xmlNodes)

                            topArtistInfo.Add(xmlNodes)
                        Next

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Else
                    ' progress
                    UpdateProgress(False, progress, "Parsing user top artists...")

                    currentPageAmount = topArtistResults

                    For artist As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "playcount"}
                        ParseXML(xmlPage, "/lfm/weeklyartistchart/artist", artist, xmlNodes)

                        topArtistInfo.Add(xmlNodes)

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                End If

                progress += 6.67
#End Region

#Region "Top Albums"
                xmlPages.Clear()
                If dateContents.Count = 0 Then
                    pageAmount = Math.Ceiling(topAlbumResults / 50)

                    ' no date, needs mutliple pages
                    For currentPage As Integer = 0 To pageAmount - 1
                        ' progress
                        UpdateProgress(False, (((currentPage + 1) / pageAmount) * 26.67) + progress, "Getting user top albums... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                        If currentPage = pageAmount - 1 Then
                            ' last page, only request leftover
                            If topAlbumResults <= 50 Then
                                ' if results below 50
                                xmlPages.Add(CallAPI("user.getTopAlbums", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & topAlbumResults.ToString))
                            ElseIf topAlbumResults Mod 50 = 0 Then
                                ' if no remainder
                                xmlPages.Add(CallAPI("user.getTopAlbums", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                            Else
                                ' if not below 50 get remainder
                                xmlPages.Add(CallAPI("user.getTopAlbums", textContents(0), "page=" & (currentPage + 1).ToString, "limit=" & (topAlbumResults Mod 50).ToString))
                            End If
                        Else
                            xmlPages.Add(CallAPI("user.getTopAlbums", textContents(0), "page=" & (currentPage + 1).ToString, "limit=50"))
                        End If

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Else
                    ' date
                    ' progress
                    UpdateProgress(False, progress, "Getting user top albums...")

                    xmlPage = CallAPI("user.getWeeklyAlbumChart", textContents(0), "from=" & DateToUnix(dateContents(0)).ToString(), "to=" & DateToUnix(dateContents(1)).ToString())
                    ' results
                    If userResults = 0 Then
                        ' if entire
                        topAlbumResults = StrCount(xmlPage, "</album>")
                    Else
                        ' if not entire
                        topAlbumResults = userResults
                    End If
                    xmlPages.Add(xmlPage)
                End If

                ' cancel check
                If bgwUser.CancellationPending = True Then
                    Exit Sub
                End If

                progress += 26.67

                ' add to list
                If dateContents.Count = 0 Then
                    For currentPage As Integer = 0 To pageAmount - 1
                        ' progress
                        UpdateProgress(False, (((currentPage + 1) / pageAmount) * 6.67) + progress, "Parsing user top albums... (" & ((currentPage + 1) * 50).ToString() & " of " & (pageAmount * 50).ToString() & ")")

                        currentPageAmount = CInt(ParseMetadata(xmlPages(currentPage), "perPage=").Trim)

                        ' parse
                        For album As Integer = 0 To currentPageAmount - 1
                            xmlNodes = {"name", "artist/name", "playcount"}
                            ParseXML(xmlPages(currentPage), "/lfm/topalbums/album", album, xmlNodes)

                            topAlbumInfo.Add(xmlNodes)

                            ' cancel check
                            If bgwUser.CancellationPending = True Then
                                Exit Sub
                            End If
                        Next

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                Else
                    ' progress
                    UpdateProgress(False, progress, "Parsing user top albums...")

                    currentPageAmount = topAlbumResults

                    For album As Integer = 0 To currentPageAmount - 1
                        xmlNodes = {"name", "artist", "playcount"}
                        ParseXML(xmlPage, "/lfm/weeklyalbumchart/album", album, xmlNodes)

                        topAlbumInfo.Add(xmlNodes)

                        ' cancel check
                        If bgwUser.CancellationPending = True Then
                            Exit Sub
                        End If
                    Next
                End If

                progress += 6.67
#End Region
            End If

            Save(lists.ToArray)
            Invoke(Sub() StopOp())
        Catch ex As Exception
            Invoke(Sub() MessageBox.Show("ERROR: " & ex.Message, "User Backup", MessageBoxButtons.OK, MessageBoxIcon.Error))
        End Try
    End Sub

    Private Sub UserUseCurrent(sender As Object, e As EventArgs) Handles btnUserUseCurrent.Click
        txtUserUser.Text = My.Settings.User
    End Sub

    Private Sub UserEnableDate(sender As Object, e As EventArgs) Handles chkUserByDate.CheckedChanged
        If chkUserByDate.Checked = True Then
            dtpUserFrom.Enabled = True
            dtpUserTo.Enabled = True
        Else
            dtpUserFrom.Enabled = False
            dtpUserTo.Enabled = False
        End If
    End Sub

    Private Sub UserEnableAmount(sender As Object, e As EventArgs) Handles chkUserHistory.CheckedChanged, chkUserCharts.CheckedChanged
        If chkUserHistory.Checked = True OrElse chkUserCharts.Checked = True Then
            radUserEntire.Enabled = True
            radUserNumber.Enabled = True
            nudUserNumber.Enabled = True
        Else
            radUserEntire.Enabled = False
            radUserNumber.Enabled = False
            nudUserNumber.Enabled = False
            radUserEntire.Checked = True
        End If
    End Sub
#End Region

End Class