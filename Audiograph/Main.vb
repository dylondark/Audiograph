' Audiograph by Dylan Miller (dylondark)
' Project started on 11/11/2020

Imports System.ComponentModel
Imports System.Text
Imports System.Threading
Imports AxWMPLib
Imports Microsoft.VisualBasic.FileIO
Imports WMPLib

Public Class frmMain

#Region "Charts"
    Private Sub UpdateCharts()
        progressmultiplier += 1

#Region "Fatty Arrays"
        Dim TrackLabel(,) As Label = {{lblTopTrackTitle1, lblTopTrackArtist1, lblTopTrackAlbum1, lblTopTracksListeners1},
                                      {lblTopTrackTitle2, lblTopTrackArtist2, lblTopTrackAlbum2, lblTopTracksListeners2},
                                      {lblTopTrackTitle3, lblTopTrackArtist3, lblTopTrackAlbum3, lblTopTracksListeners3},
                                      {lblTopTrackTitle4, lblTopTrackArtist4, lblTopTrackAlbum4, lblTopTracksListeners4},
                                      {lblTopTrackTitle5, lblTopTrackArtist5, lblTopTrackAlbum5, lblTopTracksListeners5},
                                      {lblTopTrackTitle6, lblTopTrackArtist6, lblTopTrackAlbum6, lblTopTracksListeners6},
                                      {lblTopTrackTitle7, lblTopTrackArtist7, lblTopTrackAlbum7, lblTopTracksListeners7},
                                      {lblTopTrackTitle8, lblTopTrackArtist8, lblTopTrackAlbum8, lblTopTracksListeners8},
                                      {lblTopTrackTitle9, lblTopTrackArtist9, lblTopTrackAlbum9, lblTopTracksListeners9},
                                      {lblTopTrackTitle10, lblTopTrackArtist10, lblTopTrackAlbum10, lblTopTracksListeners10},
                                      {lblTopTrackTitle11, lblTopTrackArtist11, lblTopTrackAlbum11, lblTopTracksListeners11},
                                      {lblTopTrackTitle12, lblTopTrackArtist12, lblTopTrackAlbum12, lblTopTracksListeners12},
                                      {lblTopTrackTitle13, lblTopTrackArtist13, lblTopTrackAlbum13, lblTopTracksListeners13},
                                      {lblTopTrackTitle14, lblTopTrackArtist14, lblTopTrackAlbum14, lblTopTracksListeners14},
                                      {lblTopTrackTitle15, lblTopTrackArtist15, lblTopTrackAlbum15, lblTopTracksListeners15},
                                      {lblTopTrackTitle16, lblTopTrackArtist16, lblTopTrackAlbum16, lblTopTracksListeners16},
                                      {lblTopTrackTitle17, lblTopTrackArtist17, lblTopTrackAlbum17, lblTopTracksListeners17},
                                      {lblTopTrackTitle18, lblTopTrackArtist18, lblTopTrackAlbum18, lblTopTracksListeners18},
                                      {lblTopTrackTitle19, lblTopTrackArtist19, lblTopTrackAlbum19, lblTopTracksListeners19},
                                      {lblTopTrackTitle20, lblTopTrackArtist20, lblTopTrackAlbum20, lblTopTracksListeners20}}

        Dim TrackArt() As PictureBox = {picTrack1, picTrack2, picTrack3, picTrack4, picTrack5, picTrack6, picTrack7, picTrack8, picTrack9, picTrack10,
                                        picTrack11, picTrack12, picTrack13, picTrack14, picTrack15, picTrack16, picTrack17, picTrack18, picTrack19, picTrack20}

        Dim ArtistLabel(,) As Label = {{lblTopArtist1, lblTopArtistListeners1, lblTopArtistPlaycount1},
                                       {lblTopArtist2, lblTopArtistListeners2, lblTopArtistPlaycount2},
                                       {lblTopArtist3, lblTopArtistListeners3, lblTopArtistPlaycount3},
                                       {lblTopArtist4, lblTopArtistListeners4, lblTopArtistPlaycount4},
                                       {lblTopArtist5, lblTopArtistListeners5, lblTopArtistPlaycount5},
                                       {lblTopArtist6, lblTopArtistListeners6, lblTopArtistPlaycount6},
                                       {lblTopArtist7, lblTopArtistListeners7, lblTopArtistPlaycount7},
                                       {lblTopArtist8, lblTopArtistListeners8, lblTopArtistPlaycount8},
                                       {lblTopArtist9, lblTopArtistListeners9, lblTopArtistPlaycount9},
                                       {lblTopArtist10, lblTopArtistListeners10, lblTopArtistPlaycount10},
                                       {lblTopArtist11, lblTopArtistListeners11, lblTopArtistPlaycount11},
                                       {lblTopArtist12, lblTopArtistListeners12, lblTopArtistPlaycount12},
                                       {lblTopArtist13, lblTopArtistListeners13, lblTopArtistPlaycount13},
                                       {lblTopArtist14, lblTopArtistListeners14, lblTopArtistPlaycount14},
                                       {lblTopArtist15, lblTopArtistListeners15, lblTopArtistPlaycount15},
                                       {lblTopArtist16, lblTopArtistListeners16, lblTopArtistPlaycount16},
                                       {lblTopArtist17, lblTopArtistListeners17, lblTopArtistPlaycount17},
                                       {lblTopArtist18, lblTopArtistListeners18, lblTopArtistPlaycount18},
                                       {lblTopArtist19, lblTopArtistListeners19, lblTopArtistPlaycount19},
                                       {lblTopArtist20, lblTopArtistListeners20, lblTopArtistPlaycount20}}
#End Region

#Region "Top Tracks"
        ' dim stuff
        Dim TopTrackXML As String
        Dim country As String
        Invoke(Sub() country = cmbChartCountry.Text.Trim)
        If radChartWorldwide.Checked = True Then
            TopTrackXML = CallAPI("chart.getTopTracks")
        Else
            TopTrackXML = CallAPI("geo.getTopTracks", "", "country=" & country.Replace(" ", "+"))
        End If
        Dim TopTrackNodes() As String = {"name", "artist/name"}
        Dim TrackLookupNodes() As String = {"name", "artist/name", "album/title", "listeners"}
        Dim numberholder As UInteger

        ' report 20% progress
        progress += 20
        UpdateProgressChange()

        ' populate data loop
        For counter As Byte = 0 To 19
            ' get initial track data (title and artist) from toptracks
            ParseXML(TopTrackXML, "/lfm/tracks/track", counter, TopTrackNodes)

            ' replace spaces with + for proper apicall formatting
            TopTrackNodes(0) = TopTrackNodes(0).Replace(" ", "+")
            TopTrackNodes(1) = TopTrackNodes(1).Replace(" ", "+")

            ' call api for track data and set as variable
            Dim TrackXML As String = CallAPI("track.getInfo", "", "track=" & TopTrackNodes(0), "artist=" & TopTrackNodes(1), "autocorrect=1")

            ' get advanced track data (title, artist, album, listeners) and parse
            ParseXML(TrackXML, "/lfm/track", 0, TrackLookupNodes)

            ' detect errors and set to "(Unavailable)"
            For counter2 As Byte = 0 To 3
                If TrackLookupNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TrackLookupNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TrackLabel(counter, 0).Text = TrackLookupNodes(0))
            Invoke(Sub() TrackLabel(counter, 1).Text = TrackLookupNodes(1))
            Invoke(Sub() TrackLabel(counter, 2).Text = TrackLookupNodes(2))
            ' apply formatting to playcount
            UInteger.TryParse(TrackLookupNodes(3), numberholder)
            Invoke(Sub() TrackLabel(counter, 3).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TrackArt(counter).Load(ParseImage(TrackXML, "/lfm/track/album/image", 1))
            Catch ex As Exception
                TrackArt(counter).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopTrackNodes = {"name", "artist/name"}
            TrackLookupNodes = {"name", "artist/name", "album/title", "listeners"}

            ' report progress 20-60%
            progress += 2
            UpdateProgressChange()
        Next
#End Region

#Region "Top Artists"
        ' dim stuff
        Dim TopArtistXML As String
        Dim directory As String
        If radChartWorldwide.Checked = True Then
            TopArtistXML = CallAPI("chart.getTopArtists")
            directory = "/lfm/artists/artist"
        Else
            TopArtistXML = CallAPI("geo.getTopArtists", "", "country=" & country.Replace(" ", "+"))
            directory = "/lfm/topartists/artist"
        End If
        Dim TopArtistNodes() As String = {"name", "listeners", "playcount"}

        For counter As Byte = 0 To 19
            ' get initial artist data (name) from topartists
            ParseXML(TopArtistXML, directory, counter, TopArtistNodes)

            ' detect errors and set to "(Unavailable)"
            For counter2 As Byte = 0 To 2
                If TopArtistNodes(counter2).Contains("Object reference not set to an instance of an object") = True OrElse TopArtistNodes(counter2) = "0" Then
                    TopArtistNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() ArtistLabel(counter, 0).Text = TopArtistNodes(0))
            UInteger.TryParse(TopArtistNodes(1), numberholder)
            Invoke(Sub() ArtistLabel(counter, 1).Text = numberholder.ToString("N0"))
            UInteger.TryParse(TopArtistNodes(2), numberholder)
            Invoke(Sub() ArtistLabel(counter, 2).Text = numberholder.ToString("N0"))

            ' reset node arrays
            TopArtistNodes = {"name", "listeners", "playcount"}

            ' report progress 60-100
            progress += 2
            UpdateProgressChange()
        Next
#End Region
    End Sub
#End Region

#Region "Search"
    Private Sub UpdateSearch()
        ' check that there is something in the search box or that the user is not typing
        If txtSearch.Text = String.Empty OrElse Me.ActiveControl.Name = txtSearch.Name Then
            Exit Sub
        End If

        progressmultiplier += 1

        ' params
        Dim tag As String = "tag=" & txtSearch.Text.Trim.ToLower

        ' info
        Dim infoXML As String = CallAPI("tag.getInfo", String.Empty, tag)
        Dim infonodes() As String = {"name", "total", "reach", "wiki/content"}
        ParseXML(infoXML, "/lfm/tag", 0, infonodes)
        ' check for errors
        For Each element As String In infonodes
            If element.Contains("ERROR: ") = True Then
                element = "(Unavailable)"
            End If
        Next
        ' set name in search box
        If infonodes(0) <> "(Unavailable)" Then
            txtSearch.Text = infonodes(0)
        End If
        ' add to box
        Dim builder As New StringBuilder
        Dim doublepar As String = "\par\par "
        BeginInvoke(Sub() txtSearchInfo.Clear())
        builder.Append("{\rtf1\ansi ")      ' append rtf declaration
        builder.Append("\b Name\b0\par ")
        builder.Append(infonodes(0))
        builder.Append(doublepar)         ' double new line
        builder.Append("\b Total Times Used\b0\par ")
        builder.Append(CInt(infonodes(1)).ToString("N0"))
        builder.Append(doublepar)
        builder.Append("\b Reach\b0\par ")
        builder.Append(CInt(infonodes(2)).ToString("N0"))
        builder.Append(doublepar)
        builder.Append("\b Wiki\b0\par ")
        builder.Append(infonodes(3))
        builder.Append("}") ' rtf closing
        BeginInvoke(Sub() txtSearchInfo.Rtf = builder.ToString)

        ' 25%
        progress += 25
        UpdateProgressChange()

        ' tracks
        Invoke(Sub() ltvSearchTracks.Items.Clear())
        Dim trackXML As String = CallAPI("tag.getTopTracks", String.Empty, tag)
        Dim tracknodes() As String
        ' parse and add to listview
        For count As Byte = 0 To 49
            tracknodes = {"name", "artist/name"}
            ParseXML(trackXML, "/lfm/tracks/track", count, tracknodes)
            ' set errors to unavailable
            If tracknodes(0).Contains("ERROR: ") = False Then   ' dont add item if there is an error
                Invoke(Sub() ltvSearchTracks.Items.Add((count + 1).ToString))
                Invoke(Sub() ltvSearchTracks.Items(count).SubItems.Add(tracknodes(0)))
                If tracknodes(1).Contains("ERROR: ") = False Then   ' dont add subitem if there is an error
                    Invoke(Sub() ltvSearchTracks.Items(count).SubItems.Add(tracknodes(1)))
                End If
            End If
        Next

        ' 50%
        progress += 25
        UpdateProgressChange()

        ' artists
        Invoke(Sub() ltvSearchArtists.Items.Clear())
        Dim artistXML As String = CallAPI("tag.getTopArtists", String.Empty, tag)
        Dim artistnodes() As String
        ' parse and add to listview
        For count As Byte = 0 To 49
            artistnodes = {"name"}
            ParseXML(artistXML, "/lfm/topartists/artist", count, artistnodes)
            ' set errors to unavailable
            If artistnodes(0).Contains("ERROR: ") = False Then  ' dont add item if there is an error
                Invoke(Sub() ltvSearchArtists.Items.Add((count + 1).ToString))
                Invoke(Sub() ltvSearchArtists.Items(count).SubItems.Add(artistnodes(0)))
            End If
        Next

        ' 75%
        progress += 25
        UpdateProgressChange()

        ' albums
        Invoke(Sub() ltvSearchAlbums.Items.Clear())
        Dim albumXML As String = CallAPI("tag.getTopAlbums", String.Empty, tag)
        Dim albumnodes() As String
        ' parse and add to listview
        For count As Byte = 0 To 49
            albumnodes = {"name", "artist/name"}
            ParseXML(albumXML, "/lfm/albums/album", count, albumnodes)
            ' set errors to unavailable
            If albumnodes(0).Contains("ERROR: ") = False Then   ' dont add item if there is an error
                Invoke(Sub() ltvSearchAlbums.Items.Add((count + 1).ToString))
                Invoke(Sub() ltvSearchAlbums.Items(count).SubItems.Add(albumnodes(0)))
                If albumnodes(1).Contains("ERROR: ") = False Then   ' dont add subitem if there is an error
                    Invoke(Sub() ltvSearchAlbums.Items(count).SubItems.Add(albumnodes(1)))
                End If
            End If
        Next

        ' 100%
        progress += 25
        UpdateProgressChange()
    End Sub
#End Region

#Region "Track"
    Public Sub UpdateTrack()
        ' check that something is in the boxes or that the user is not typing
        If txtTrackTitle.Text = String.Empty OrElse txtTrackArtist.Text = String.Empty OrElse Me.ActiveControl.Name = txtTrackTitle.Name OrElse Me.ActiveControl.Name = txtTrackArtist.Name Then
            Exit Sub
        End If

        progressmultiplier += 1

        ' dim stuff
        Dim trackXML As String
        Dim tracknodes() As String
        ' check for whether response should include user or not
        If My.Settings.User = String.Empty Then
            trackXML = CallAPI("track.getInfo", "", "track=" & txtTrackTitle.Text.Trim, "artist=" & txtTrackArtist.Text.Trim, "autocorrect=1")
            tracknodes = {"name", "artist/name", "album/title", "mbid", "url", "duration", "listeners", "playcount", "wiki/content"}
        Else
            trackXML = CallAPI("track.getInfo", My.Settings.User, "track=" & txtTrackTitle.Text.Trim, "artist=" & txtTrackArtist.Text.Trim, "autocorrect=1")
            tracknodes = {"name", "artist/name", "album/title", "mbid", "url", "duration", "listeners", "playcount", "userplaycount", "userloved", "wiki/content"}
        End If

        ' parse for track info
        ParseXML(trackXML, "/lfm/track", 0, tracknodes)

        ' check that track actually exists
        If tracknodes(0).Contains("ERROR: ") = True Then
            BeginInvoke(Sub() MessageBox.Show("Track data unable to be retrieved" & vbCrLf & "Check that you have spelled your search terms correctly", "Track Lookup",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error))
            progressmultiplier -= 1
            Exit Sub
        End If

        ' 20%
        progress += 20
        UpdateProgressChange()

        ' set correct names into search boxes
        BeginInvoke(Sub() txtTrackTitle.Text = tracknodes(0))
        BeginInvoke(Sub() txtTrackArtist.Text = tracknodes(1))

        ' set tracklookup info
        tracklookup(0) = tracknodes(0)  ' track
        tracklookup(1) = tracknodes(1)  ' artist
        tracklookup(2) = tracknodes(2)  ' album

        ' convert duration into time
        Dim ts As TimeSpan
        ' error checking in case duration wasnt able to be retrieved
        Try
            ts = TimeSpan.FromMilliseconds(CUInt(tracknodes(5)))
        Catch ex As Exception
            ts = TimeSpan.Zero
        End Try

        ' find amount of toptags
        Dim toptagcount As Byte = StrCount(trackXML, "<tag>")
        ' parse for toptags
        Dim toptagnode() As String
        Dim toptaglist As New List(Of String)
        For count As Byte = 0 To toptagcount
            toptagnode = {"name"}
            ParseXML(trackXML, "/lfm/track/toptags/tag", count, toptagnode)
            ' add to list if no errors
            If toptagnode(0).Contains("ERROR: ") = False Then
                toptaglist.Add(toptagnode(0))
            End If
        Next

        ' 40%
        progress += 20
        UpdateProgressChange()

        ' format tags into one string
        Dim toptags As String
        For Each tag As String In toptaglist
            toptags &= tag & ", "
        Next
        ' get rid of extra comma at end
        If toptags <> String.Empty Then
            toptags = toptags.Remove(toptags.Length - 2, 2)
        End If

        ' set any errors to (Unavailable)
        For count As Byte = 0 To tracknodes.Count - 1
            If tracknodes(count).Contains("ERROR: ") = True Then
                tracknodes(count) = "(Unavailable)"
            End If
        Next
        If toptags = String.Empty OrElse toptags.Contains("ERROR: ") = True Then
            toptags = "(Unavailable)"
        End If

        ' format numbers
        ' global listeners
        tracknodes(6) = CUInt(tracknodes(6)).ToString("N0")
        ' global playcount
        tracknodes(7) = CUInt(tracknodes(7)).ToString("N0")
        ' user playcount
        If My.Settings.User <> String.Empty Then
            tracknodes(8) = CUInt(tracknodes(8)).ToString("N0")
        End If

        ' remove link from wiki
        If tracknodes(10).Contains("<a href") = True OrElse tracknodes(8).Contains("<a href") = True Then
            If My.Settings.User <> String.Empty Then
                tracknodes(10) = tracknodes(10).Substring(0, tracknodes(10).Count - (tracknodes(10).Count - InStr(tracknodes(10), "<a href") + 1))
            Else
                tracknodes(8) = tracknodes(8).Substring(0, tracknodes(8).Count - (tracknodes(8).Count - InStr(tracknodes(8), "<a href") + 1))
            End If
        End If

        ' set text box
        BeginInvoke(Sub() txtTrackInfo.Clear())
        Dim builder As New StringBuilder
        builder.Append("{\rtf1\ansi ")   ' append rtf declaration
        builder.Append("\b Title\b0\par ")
        builder.Append(tracknodes(0))
        builder.Append("\par\par ")      ' double new line
        builder.Append("\b Artist\b0\par ")
        builder.Append(tracknodes(1))
        builder.Append("\par\par ")
        builder.Append("\b Album\b0\par ")
        builder.Append(tracknodes(2))
        builder.Append("\par\par ")
        builder.Append("\b Musicbrainz ID (MBID)\b0\par ")
        builder.Append(tracknodes(3))
        builder.Append("\par\par ")
        builder.Append("\b View on Last.fm\b0\par ")
        builder.Append(tracknodes(4))
        builder.Append("\par\par ")
        builder.Append("\b Duration\b0\par ")
        builder.Append(ts.Minutes.ToString & ":" & ts.Seconds.ToString("0#"))
        builder.Append("\par\par ")
        builder.Append("\b Global Listeners\b0\par ")
        builder.Append(tracknodes(6))
        builder.Append("\par\par ")
        builder.Append("\b Global Playcount\b0\par ")
        builder.Append(tracknodes(7))
        builder.Append("\par\par ")
        ' add user playcount
        If My.Settings.User <> String.Empty Then
            builder.Append("\b User Playcount\b0\par ")
            builder.Append(tracknodes(8))
            builder.Append("\par\par ")
        End If
        builder.Append("\b Top Tags\b0\par ")
        builder.Append(toptags)
        builder.Append("\par\par ")
        builder.Append("\b Wiki\b0\par ")
        If My.Settings.User <> String.Empty Then
            builder.Append(tracknodes(10))
        Else
            builder.Append(tracknodes(8))
        End If
        builder.Append("}")
        BeginInvoke(Sub() txtTrackInfo.Rtf = builder.ToString) ' set box

        ' 60%
        progress += 20
        UpdateProgressChange()

        ' set track art
        Try
            picTrackArt.Load(ParseImage(trackXML, "/lfm/track/album/image", 3))
        Catch ex As Exception
            BeginInvoke(Sub() picTrackArt.Image = My.Resources.imageunavailable)
        End Try

        ' set loved button
        If tracknodes(9) = "1" Then
            BeginInvoke(Sub() btnTrackLove.Text = "Unlove")
        Else
            BeginInvoke(Sub() btnTrackLove.Text = "Love")
        End If

        ' avoid trying to parse tags if there is no user set
        If My.Settings.User <> String.Empty Then
            ' get tags
            Dim tagXML As String = CallAPI("track.getTags", My.Settings.User, "track=" & tracknodes(0), "artist=" & tracknodes(1))

            ' put tags into list box
            BeginInvoke(Sub() lstTrackUserTags.Items.Clear())
            ' find number of tags
            Dim tagcount As UInteger = StrCount(tagXML, "<tag>")
            ' parse for tags
            Dim tagnode() As String
            For count As Byte = 0 To tagcount
                tagnode = {"name"}
                ParseXML(tagXML, "/lfm/tags/tag", count, tagnode)
                ' add to listbox if no errors
                If tagnode(0).Contains("ERROR: ") = False Then
                    Invoke(Sub() lstTrackUserTags.Items.Add(tagnode(0)))
                End If
            Next
        End If

        ' 80%
        progress += 20
        UpdateProgressChange()

        ' get similar tracks
        Dim similarXML As String = CallAPI("track.getSimilar", "", "track=" & tracknodes(0), "artist=" & tracknodes(1), "limit=50")

        ' put similar tracks into listview
        BeginInvoke(Sub() ltvTrackSimilar.Items.Clear())
        ' parse for tracks
        Dim similarnodes() As String
        For count As Byte = 0 To 49
            similarnodes = {"name", "artist/name", "match"}
            ParseXML(similarXML, "/lfm/similartracks/track", count, similarnodes)
            ' change errors to (Unavailable)
            If similarnodes(1).Contains("ERROR: ") = True Then
                similarnodes(1) = "(Unavailable)"
            End If
            If similarnodes(2).Contains("ERROR: ") = True Then
                similarnodes(2) = "(Unavailable)"
            Else    ' format match as %
                similarnodes(2) = CSng(similarnodes(2)).ToString("P1")
            End If
            ' add to listview if no errors
            If similarnodes(0).Contains("ERROR: ") = False Then
                Invoke(Sub() ltvTrackSimilar.Items.Add(similarnodes(0)))
                Invoke(Sub() ltvTrackSimilar.Items(count).SubItems.Add(similarnodes(1)))
                Invoke(Sub() ltvTrackSimilar.Items(count).SubItems.Add(similarnodes(2)))
            End If
        Next

        ' 100%
        progress += 20
        UpdateProgressChange()
    End Sub
#End Region

#Region "Artist"
    Public Sub UpdateArtist()
        ' check that something is in the box or that the user is not typing
        If txtArtistName.Text = String.Empty OrElse Me.ActiveControl.Name = txtArtistName.Name Then
            Exit Sub
        End If

        progressmultiplier += 1

        ' dim stuff
        Dim artistXML As String
        Dim artistnodes() As String
        ' check for whether response should include user or not
        If My.Settings.User = String.Empty Then
            artistXML = CallAPI("artist.getInfo", "", "artist=" & txtArtistName.Text.Trim, "autocorrect=1")
            artistnodes = {"name", "mbid", "url", "stats/listeners", "stats/playcount", "bio/content"}
        Else
            artistXML = CallAPI("artist.getInfo", My.Settings.User, "artist=" & txtArtistName.Text.Trim, "autocorrect=1")
            artistnodes = {"name", "mbid", "url", "stats/listeners", "stats/playcount", "stats/userplaycount", "bio/content"}
        End If

        ' parse for artist info
        ParseXML(artistXML, "/lfm/artist", 0, artistnodes)

        ' check that the artist actually exists
        If artistnodes(0).Contains("ERROR: ") = True Then
            Invoke(Sub() MessageBox.Show("Artist data unable to be retrieved" & vbCrLf & "Check that you have spelled your search terms correctly", "Artist Lookup",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error))
            progressmultiplier -= 1
            Exit Sub
        End If

        ' 20%
        progress += 20
        UpdateProgressChange()

        ' set correct name into search box
        Invoke(Sub() txtArtistName.Text = artistnodes(0))

        ' set artistlookup
        artistlookup = artistnodes(0)

        ' set image
        Dim topalbumXML As String = CallAPI("artist.getTopAlbums", "", "artist=" & artistnodes(0), "limit=20")
        Try
            picArtistArt.Load(ParseImage(topalbumXML, "/lfm/topalbums/album/image", 3))
        Catch ex As Exception
            picArtistArt.Image = My.Resources.imageunavailable
        End Try

        ' find amount of toptags
        Dim toptagcount As Byte = StrCount(artistXML, "<tag>")
        ' parse for toptags
        Dim toptagnode() As String
        Dim toptaglist As New List(Of String)
        For count As Byte = 0 To toptagcount
            toptagnode = {"name"}
            ParseXML(artistXML, "/lfm/artist/tags/tag", count, toptagnode)
            ' add to list if no errors
            If toptagnode(0).Contains("ERROR: ") = False Then
                toptaglist.Add(toptagnode(0))
            End If
        Next

        ' format tags into one string
        Dim toptags As String
        For Each tag As String In toptaglist
            toptags &= tag & ", "
        Next
        ' get rid of extra comma at end
        If toptags <> String.Empty Then
            toptags = toptags.Remove(toptags.Length - 2, 2)
        End If

        ' set any errors to (Unavailable)
        For count As Byte = 0 To artistnodes.Count - 1
            If artistnodes(count).Contains("ERROR: ") = True Then
                artistnodes(count) = "(Unavailable)"
            End If
        Next
        If toptags = String.Empty OrElse toptags.Contains("ERROR: ") = True Then
            toptags = "(Unavailable)"
        End If

        ' 40%
        progress += 20
        UpdateProgressChange()

        ' format numbers
        Dim numberholder As UInteger
        ' global listeners
        UInteger.TryParse(artistnodes(3), numberholder)
        artistnodes(3) = numberholder.ToString("N0")
        ' global playcount
        UInteger.TryParse(artistnodes(4), numberholder)
        artistnodes(4) = numberholder.ToString("N0")
        ' user playcount
        If My.Settings.User <> String.Empty Then
            UInteger.TryParse(artistnodes(5), numberholder)
            artistnodes(5) = numberholder.ToString("N0")
        End If

        ' remove link from wiki
        If artistnodes(6).Contains("<a href") = True OrElse artistnodes(5).Contains("<a href") = True Then
            If My.Settings.User <> String.Empty Then
                artistnodes(6) = artistnodes(6).Substring(0, artistnodes(6).Count - (artistnodes(6).Count - InStr(artistnodes(6), "<a href") + 1))
            Else
                artistnodes(5) = artistnodes(5).Substring(0, artistnodes(5).Count - (artistnodes(5).Count - InStr(artistnodes(5), "<a href") + 1))
            End If
        End If

        ' set text box
        Invoke(Sub() txtArtistInfo.Clear())
        Dim builder As New StringBuilder
        builder.Append("{\rtf1\ansi ")   ' append rtf declaration
        builder.Append("\b Artist\b0\par ")
        builder.Append(artistnodes(0))
        builder.Append("\par\par ")      ' double new line
        builder.Append("\b Musicbrainz ID (MBID)\b0\par ")
        builder.Append(artistnodes(1))
        builder.Append("\par\par ")
        builder.Append("\b View on Last.fm\b0\par ")
        builder.Append(artistnodes(2))
        builder.Append("\par\par ")
        builder.Append("\b Global Listeners\b0\par ")
        builder.Append(artistnodes(3))
        builder.Append("\par\par ")
        builder.Append("\b Global Playcount\b0\par ")
        builder.Append(artistnodes(4))
        builder.Append("\par\par ")
        ' add user playcount
        If My.Settings.User <> String.Empty Then
            builder.Append("\b User Playcount\b0\par ")
            builder.Append(artistnodes(5))
            builder.Append("\par\par ")
        End If
        builder.Append("\b Top Tags\b0\par ")
        builder.Append(toptags)
        builder.Append("\par\par ")
        builder.Append("\b Wiki\b0\par ")
        If My.Settings.User <> String.Empty Then
            builder.Append(artistnodes(6))
        Else
            builder.Append(artistnodes(5))
        End If
        builder.Append("}")
        Invoke(Sub() txtArtistInfo.Rtf = builder.ToString) ' set box

        ' 60%
        progress += 20
        UpdateProgressChange()

        ' avoid trying to parse tags if there is no user set
        If My.Settings.User <> String.Empty Then
            ' get tags
            Dim tagXML As String = CallAPI("artist.getTags", My.Settings.User, "artist=" & artistnodes(0))

            ' put tags into list box
            Invoke(Sub() lstArtistUserTags.Items.Clear())
            ' find number of tags
            Dim tagcount As Byte = StrCount(tagXML, "<tag>")
            If tagcount > 0 Then
                tagcount -= 1
            End If
            ' parse for tags
            Dim tagnode() As String
            For count As Byte = 0 To tagcount
                tagnode = {"name"}
                ParseXML(tagXML, "/lfm/tags/tag", count, tagnode)
                ' add to listbox if no errors
                If tagnode(0).Contains("ERROR: ") = False Then
                    Invoke(Sub() lstArtistUserTags.Items.Add(tagnode(0)))
                End If
            Next
        End If

        ' get similar artists
        Dim similarXML As String = CallAPI("artist.getSimilar", "", "artist=" & artistnodes(0), "limit=50")

        ' put similar tracks into listview
        Invoke(Sub() ltvArtistSimilar.Items.Clear())
        ' parse for tracks
        Dim similarnodes() As String
        Dim matchholder As Single
        For count As Byte = 0 To 49
            similarnodes = {"name", "match"}
            ParseXML(similarXML, "/lfm/similarartists/artist", count, similarnodes)
            ' change errors to (Unavailable)
            If similarnodes(1).Contains("ERROR: ") = True Then
                similarnodes(1) = "(Unavailable)"
            Else    ' format match as %
                Single.TryParse(similarnodes(1), matchholder)
                similarnodes(1) = matchholder.ToString("P1")
            End If
            ' add to listview if no errors
            If similarnodes(0).Contains("ERROR: ") = False Then
                Invoke(Sub() ltvArtistSimilar.Items.Add(similarnodes(0)))
                Invoke(Sub() ltvArtistSimilar.Items(count).SubItems.Add(similarnodes(1)))
            End If
        Next

        ' 80%
        progress += 20
        UpdateProgressChange()

#Region "Fatty Arrays"
        Dim TopTrackLabel(,) As Label = {{lblArtistTopTrackTitle1, lblArtistTopTrackListeners1},
                                         {lblArtistTopTrackTitle2, lblArtistTopTrackListeners2},
                                         {lblArtistTopTrackTitle3, lblArtistTopTrackListeners3},
                                         {lblArtistTopTrackTitle4, lblArtistTopTrackListeners4},
                                         {lblArtistTopTrackTitle5, lblArtistTopTrackListeners5},
                                         {lblArtistTopTrackTitle6, lblArtistTopTrackListeners6},
                                         {lblArtistTopTrackTitle7, lblArtistTopTrackListeners7},
                                         {lblArtistTopTrackTitle8, lblArtistTopTrackListeners8},
                                         {lblArtistTopTrackTitle9, lblArtistTopTrackListeners9},
                                         {lblArtistTopTrackTitle10, lblArtistTopTrackListeners10},
                                         {lblArtistTopTrackTitle11, lblArtistTopTrackListeners11},
                                         {lblArtistTopTrackTitle12, lblArtistTopTrackListeners12},
                                         {lblArtistTopTrackTitle13, lblArtistTopTrackListeners13},
                                         {lblArtistTopTrackTitle14, lblArtistTopTrackListeners14},
                                         {lblArtistTopTrackTitle15, lblArtistTopTrackListeners15},
                                         {lblArtistTopTrackTitle16, lblArtistTopTrackListeners16},
                                         {lblArtistTopTrackTitle17, lblArtistTopTrackListeners17},
                                         {lblArtistTopTrackTitle18, lblArtistTopTrackListeners18},
                                         {lblArtistTopTrackTitle19, lblArtistTopTrackListeners19},
                                         {lblArtistTopTrackTitle20, lblArtistTopTrackListeners20}}

        Dim TopAlbumLabel() As Label = {lblArtistTopAlbum1, lblArtistTopAlbum2, lblArtistTopAlbum3, lblArtistTopAlbum4, lblArtistTopAlbum5, lblArtistTopAlbum6, lblArtistTopAlbum7, lblArtistTopAlbum8, lblArtistTopAlbum9, lblArtistTopAlbum10}
        Dim TopAlbumPic() As PictureBox = {picArtistTopAlbum1, picArtistTopAlbum2, picArtistTopAlbum3, picArtistTopAlbum4, picArtistTopAlbum5, picArtistTopAlbum6, picArtistTopAlbum7, picArtistTopAlbum8, picArtistTopAlbum9, picArtistTopAlbum10}
#End Region

        ' top tracks
        Dim toptrackXML As String = CallAPI("artist.getTopTracks", "", "artist=" & artistnodes(0), "limit=20")
        Dim toptracknodes() As String
        ' put tracks into tlp
        For count As Byte = 0 To 19
            ' parse
            toptracknodes = {"name", "listeners"}
            ParseXML(toptrackXML, "/lfm/toptracks/track", count, toptracknodes)

            ' set
            Invoke(Sub() TopTrackLabel(count, 0).Text = toptracknodes(0))
            Try
                Invoke(Sub() TopTrackLabel(count, 1).Text = CUInt(toptracknodes(1)).ToString("N0"))
            Catch ex As Exception
                Invoke(Sub() TopTrackLabel(count, 1).Text = toptracknodes(1))
            End Try
        Next

        ' top albums
        Dim topalbumnodes() As String
        ' put albums into tlp
        For count As Byte = 0 To 9
            ' parse
            topalbumnodes = {"name"}
            ParseXML(topalbumXML, "/lfm/topalbums/album", count, topalbumnodes)

            ' set
            Invoke(Sub() TopAlbumLabel(count).Text = topalbumnodes(0))
            Try
                TopAlbumPic(count).Load(ParseImage(topalbumXML, "/lfm/topalbums/album", count, 6))
            Catch ex As Exception
                TopAlbumPic(count).Image = My.Resources.imageunavailable
            End Try
        Next

        ' 100%
        progress += 20
        UpdateProgressChange()
    End Sub
#End Region

#Region "Album"
    Public Sub UpdateAlbum()
        ' check that something is in the boxes or that the user is not typing
        If txtAlbumTitle.Text = String.Empty OrElse txtAlbumArtist.Text = String.Empty OrElse Me.ActiveControl.Name = txtAlbumTitle.Name OrElse Me.ActiveControl.Name = txtAlbumArtist.Name Then
            Exit Sub
        End If

        progressmultiplier += 1

        ' dim stuff
        Dim albumXML As String
        Dim albumnodes() As String
        ' check for whether response should include user or not
        If My.Settings.User = String.Empty Then
            albumXML = CallAPI("album.getInfo", "", "album=" & txtAlbumTitle.Text.Trim, "artist=" & txtAlbumArtist.Text.Trim, "autocorrect=1")
            albumnodes = {"name", "artist", "mbid", "url", "listeners", "playcount", "wiki/content"}
        Else
            albumXML = CallAPI("album.getInfo", My.Settings.User, "album=" & txtAlbumTitle.Text.Trim, "artist=" & txtAlbumArtist.Text.Trim, "autocorrect=1")
            albumnodes = {"name", "artist", "mbid", "url", "listeners", "playcount", "userplaycount", "wiki/content"}
        End If

        ' parse for album info
        ParseXML(albumXML, "/lfm/album", 0, albumnodes)

        ' check that album actually exists
        If albumnodes(0).Contains("ERROR: ") = True Then
            BeginInvoke(Sub() MessageBox.Show("Album data unable to be retrieved" & vbCrLf & "Check that you have spelled your search terms correctly", "Album Lookup",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error))
            progressmultiplier -= 1
            Exit Sub
        End If

        ' 20%
        progress += 20
        UpdateProgressChange()

        ' set correct names into search boxes
        BeginInvoke(Sub() txtAlbumTitle.Text = albumnodes(0))
        BeginInvoke(Sub() txtAlbumArtist.Text = albumnodes(1))

        ' set albumlookup info
        albumlookup(0) = albumnodes(0)  ' album
        albumlookup(1) = albumnodes(1)  ' artist

        ' find amount of toptags
        Dim toptagcount As Byte = StrCount(albumXML, "<tag>")
        ' parse for toptags
        Dim toptagnode() As String
        Dim toptaglist As New List(Of String)
        For count As Byte = 0 To toptagcount
            toptagnode = {"name"}
            ParseXML(albumXML, "/lfm/album/tags/tag", count, toptagnode)
            ' add to list if no errors
            If toptagnode(0).Contains("ERROR: ") = False Then
                toptaglist.Add(toptagnode(0))
            End If
        Next

        ' 40%
        progress += 20
        UpdateProgressChange()

        ' format tags into one string
        Dim toptags As String
        For Each tag As String In toptaglist
            toptags &= tag & ", "
        Next
        ' get rid of extra comma at end
        If toptags <> String.Empty Then
            toptags = toptags.Remove(toptags.Length - 2, 2)
        End If

        ' set any errors to (Unavailable)
        For count As Byte = 0 To albumnodes.Count - 1
            If albumnodes(count).Contains("ERROR: ") = True Then
                albumnodes(count) = "(Unavailable)"
            End If
        Next
        If toptags = String.Empty OrElse toptags.Contains("ERROR: ") = True Then
            toptags = "(Unavailable)"
        End If

        ' format numbers
        ' global listeners
        albumnodes(4) = CUInt(albumnodes(4)).ToString("N0")
        ' global playcount
        albumnodes(5) = CUInt(albumnodes(5)).ToString("N0")
        ' user playcount
        If My.Settings.User <> String.Empty Then
            albumnodes(6) = CUInt(albumnodes(6)).ToString("N0")
        End If

        ' remove link from wiki
        If albumnodes(7).Contains("<a href") = True OrElse albumnodes(6).Contains("<a href") = True Then
            If My.Settings.User <> String.Empty Then
                albumnodes(7) = albumnodes(7).Substring(0, albumnodes(7).Count - (albumnodes(7).Count - InStr(albumnodes(7), "<a href") + 1))
            Else
                albumnodes(6) = albumnodes(6).Substring(0, albumnodes(6).Count - (albumnodes(6).Count - InStr(albumnodes(6), "<a href") + 1))
            End If
        End If

        ' set text box
        BeginInvoke(Sub() txtAlbumInfo.Clear())
        Dim builder As New StringBuilder
        builder.Append("{\rtf1\ansi ")   ' append rtf declaration
        builder.Append("\b Title\b0\par ")
        builder.Append(albumnodes(0))
        builder.Append("\par\par ")      ' double new line
        builder.Append("\b Artist\b0\par ")
        builder.Append(albumnodes(1))
        builder.Append("\par\par ")
        builder.Append("\b Musicbrainz ID (MBID)\b0\par ")
        builder.Append(albumnodes(2))
        builder.Append("\par\par ")
        builder.Append("\b View on Last.fm\b0\par ")
        builder.Append(albumnodes(3))
        builder.Append("\par\par ")
        builder.Append("\b Global Listeners\b0\par ")
        builder.Append(albumnodes(4))
        builder.Append("\par\par ")
        builder.Append("\b Global Playcount\b0\par ")
        builder.Append(albumnodes(5))
        builder.Append("\par\par ")
        ' add user playcount
        If My.Settings.User <> String.Empty Then
            builder.Append("\b User Playcount\b0\par ")
            builder.Append(albumnodes(6))
            builder.Append("\par\par ")
        End If
        builder.Append("\b Top Tags\b0\par ")
        builder.Append(toptags)
        builder.Append("\par\par ")
        builder.Append("\b Wiki\b0\par ")
        If My.Settings.User <> String.Empty Then
            builder.Append(albumnodes(7))
        Else
            builder.Append(albumnodes(6))
        End If
        builder.Append("}")
        BeginInvoke(Sub() txtAlbumInfo.Rtf = builder.ToString) ' set box

        ' 60%
        progress += 20
        UpdateProgressChange()

        ' set album art
        Try
            picAlbumArt.Load(ParseImage(albumXML, "/lfm/album/image", 3))
        Catch ex As Exception
            BeginInvoke(Sub() picAlbumArt.Image = My.Resources.imageunavailable)
        End Try

        ' avoid trying to parse tags if there is no user set
        If My.Settings.User <> String.Empty Then
            ' get tags
            Dim tagXML As String = CallAPI("album.getTags", My.Settings.User, "album=" & albumnodes(0), "artist=" & albumnodes(1))

            ' put tags into list box
            Invoke(Sub() lstAlbumUserTags.Items.Clear())
            ' find number of tags
            Dim tagcount As Byte = StrCount(tagXML, "<tag>")
            ' parse for tags
            Dim tagnode() As String
            For count As Byte = 0 To tagcount
                tagnode = {"name"}
                ParseXML(tagXML, "/lfm/tags/tag", count, tagnode)
                ' add to listbox if no errors
                If tagnode(0).Contains("ERROR: ") = False Then
                    Invoke(Sub() lstAlbumUserTags.Items.Add(tagnode(0)))
                End If
            Next
        End If

        ' 80%
        progress += 20
        UpdateProgressChange()

        ' track list
        BeginInvoke(Sub() ltvAlbumTrackList.Items.Clear())
        ' get amount of tracks
        Dim tracknum As UShort = StrCount(albumXML, "<track rank")
        If tracknum = 0 Then
            tracknum = 1
        End If
        ' init
        Dim ts As New TimeSpan
        Dim currentnum As UShort = 0
        ' parse for albums
        Dim tracknodes() As String
        For count As Byte = 0 To tracknum - 1
            tracknodes = {"name", "duration"}
            ParseXML(albumXML, "/lfm/album/tracks/track", count, tracknodes)
            ' change errors to (Unavailable)
            If tracknodes(0).Contains("ERROR: ") = True Then
                tracknodes(0) = "(Unavailable)"
            End If
            ' set to 0 if duration cannot be parsed
            Try
                ts = TimeSpan.FromSeconds(CUInt(tracknodes(1)))
            Catch ex As Exception
                ts = TimeSpan.Zero
            End Try
            ' add to listview if no errors
            If tracknodes(0) <> "(Unavailable)" Then
                currentnum += 1
                Invoke(Sub() ltvAlbumTrackList.Items.Add(currentnum))   ' #
                Invoke(Sub() ltvAlbumTrackList.Items(count).SubItems.Add(tracknodes(0)))    ' title
                Invoke(Sub() ltvAlbumTrackList.Items(count).SubItems.Add(ts.Minutes.ToString & ":" & ts.Seconds.ToString("0#")))  ' duration    ' duration
            End If
        Next

        ' 100%
        progress += 20
        UpdateProgressChange()
    End Sub
#End Region

#Region "User"
    Public Sub UpdateUser()
        progressmultiplier += 1

#Region "Info"
        ' xml stuff
        Dim UserInfoXML As String = CallAPI("user.getInfo", My.Settings.User)
        Dim UserInfoNodes() As String = {"name", "realname", "url", "country", "age", "gender", "playcount", "playlists", "registered"}

        ' parse xml
        ParseXML(UserInfoXML, "/lfm/user", 0, UserInfoNodes)
        Try
            picUser.Load(ParseImage(UserInfoXML, "/lfm/user/image", 3))
        Catch ex As Exception
            picUser.Image = My.Resources.imageunavailable
        End Try

        ' convert registered date to datetime
        Dim unixtime As UInteger
        UInteger.TryParse(UserInfoNodes(8), unixtime)
        Dim RegisteredDateTime As Date = UnixToDate(unixtime + timezoneoffset)

        ' gender formatting
        Select Case UserInfoNodes(5)
            Case "m"
                UserInfoNodes(5) = "Male"
            Case "f"
                UserInfoNodes(5) = "Female"
            Case Else
                UserInfoNodes(5) = "Not Specified"
        End Select

        ' age formatting
        If UserInfoNodes(4) = "0" Then
            UserInfoNodes(4) = "Not Specified"
        End If

        ' playcount formatting
        UserInfoNodes(6) = CInt(UserInfoNodes(6)).ToString("N0")

        ' create textbox data
        Invoke(Sub() txtUserInfo.Clear())
        Invoke(Sub() txtUserInfo.Text = "Name" & vbCrLf & UserInfoNodes(0) & vbCrLf & vbCrLf & "Real Name" & vbCrLf & UserInfoNodes(1) & vbCrLf & vbCrLf &
                   "URL" & vbCrLf & UserInfoNodes(2) & vbCrLf & vbCrLf & "Country" & vbCrLf & UserInfoNodes(3) & vbCrLf & vbCrLf &
                   "Age" & vbCrLf & UserInfoNodes(4) & vbCrLf & vbCrLf & "Gender" & vbCrLf & UserInfoNodes(5) & vbCrLf & vbCrLf & "Playcount" & vbCrLf & UserInfoNodes(6) & vbCrLf & vbCrLf &
                   "Playlists" & vbCrLf & UserInfoNodes(7) & vbCrLf & vbCrLf & "Registered" & vbCrLf & RegisteredDateTime.ToString)

        ' format textbox
        Dim BoldFont As New Font("Segoe UI", 10, FontStyle.Bold)
        ' name
        Invoke(Sub() txtUserInfo.SelectionStart = 0)
        Invoke(Sub() txtUserInfo.SelectionLength = "Text".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' real name
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "Real Name") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "Real Name".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' URL
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "URL") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "URL".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' country
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "Country") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "Country".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' age
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "Age") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "Age".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' gender
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "Gender") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "Gender".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' playcount
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "Playcount") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "Playcount".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' playlists
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "Playlists") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "Playlists".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)
        ' registered
        Invoke(Sub() txtUserInfo.SelectionStart = InStr(txtUserInfo.Text, "Registered") - 1)
        Invoke(Sub() txtUserInfo.SelectionLength = "Registered".Length)
        Invoke(Sub() txtUserInfo.SelectionFont = BoldFont)

        ' report 34% progress
        progress += 34
        UpdateProgressChange()
#End Region

#Region "Friends"
        ' parse for total friends
        Dim FriendsTotalXML As String = CallAPI("user.getFriends", My.Settings.User, "limit=1")
        Dim totalfriends As String = ParseMetadata(FriendsTotalXML, "total=")
        If totalfriends.Contains("ERROR:") = True Then
            totalfriends = "max"
        End If

        ' xml stuff
        Dim FriendsXML As String = CallAPI("user.getFriends", My.Settings.User, "limit=" & totalfriends)
        Invoke(Sub() lblUserFriendTotal.Text = "Friends: " & ParseMetadata(FriendsXML, "total="))
        If lblUserFriendTotal.Text.Contains("ParseMetadata") = True Then
            Invoke(Sub() lblUserFriendTotal.Text = "Friends: 0")
        End If
        Dim FriendsNodes() As String = {"name", "realname", "url", "registered"}

        ' find number of users in xml
        Dim usercount As UInteger = StrCount(FriendsXML, "<user>")

        ' add each friend to list view
        Invoke(Sub() ltvUserFriends.Items.Clear())
        If usercount > 0 Then
            For count As UShort = 0 To usercount - 1
                ParseXML(FriendsXML, "/lfm/friends/user", count, FriendsNodes)              ' get data from xml
                Invoke(Sub() ltvUserFriends.Items.Add(FriendsNodes(0)))                     ' add listview item
                Invoke(Sub() ltvUserFriends.Items(count).SubItems.Add(FriendsNodes(1)))     ' add subitem 1
                Invoke(Sub() ltvUserFriends.Items(count).SubItems.Add(FriendsNodes(2)))     ' add subitem 2
                Invoke(Sub() ltvUserFriends.Items(count).SubItems.Add(FriendsNodes(3)))     ' add subitem 3
                FriendsNodes = {"name", "realname", "url", "registered"}                    ' reset nodes
            Next
        End If

        ' report 67% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Recent Tracks"
#Region "Fatty Arrays"
        Dim TrackLabel(,) = {{lblUserRecentTitle1, lblUserRecentArtist1, lblUserRecentAlbum1},
                             {lblUserRecentTitle2, lblUserRecentArtist2, lblUserRecentAlbum2},
                             {lblUserRecentTitle3, lblUserRecentArtist3, lblUserRecentAlbum3},
                             {lblUserRecentTitle4, lblUserRecentArtist4, lblUserRecentAlbum4},
                             {lblUserRecentTitle5, lblUserRecentArtist5, lblUserRecentAlbum5},
                             {lblUserRecentTitle6, lblUserRecentArtist6, lblUserRecentAlbum6},
                             {lblUserRecentTitle7, lblUserRecentArtist7, lblUserRecentAlbum7},
                             {lblUserRecentTitle8, lblUserRecentArtist8, lblUserRecentAlbum8},
                             {lblUserRecentTitle9, lblUserRecentArtist9, lblUserRecentAlbum9},
                             {lblUserRecentTitle10, lblUserRecentArtist10, lblUserRecentAlbum10},
                             {lblUserRecentTitle11, lblUserRecentArtist11, lblUserRecentAlbum11},
                             {lblUserRecentTitle12, lblUserRecentArtist12, lblUserRecentAlbum12},
                             {lblUserRecentTitle13, lblUserRecentArtist13, lblUserRecentAlbum13},
                             {lblUserRecentTitle14, lblUserRecentArtist14, lblUserRecentAlbum14},
                             {lblUserRecentTitle15, lblUserRecentArtist15, lblUserRecentAlbum15},
                             {lblUserRecentTitle16, lblUserRecentArtist16, lblUserRecentAlbum16},
                             {lblUserRecentTitle17, lblUserRecentArtist17, lblUserRecentAlbum17},
                             {lblUserRecentTitle18, lblUserRecentArtist18, lblUserRecentAlbum18},
                             {lblUserRecentTitle19, lblUserRecentArtist19, lblUserRecentAlbum19},
                             {lblUserRecentTitle20, lblUserRecentArtist20, lblUserRecentAlbum20}}

        Dim TrackArt() = {picUserRecentArt1, picUserRecentArt2, picUserRecentArt3, picUserRecentArt4, picUserRecentArt5,
                          picUserRecentArt6, picUserRecentArt7, picUserRecentArt8, picUserRecentArt9, picUserRecentArt10,
                          picUserRecentArt11, picUserRecentArt12, picUserRecentArt13, picUserRecentArt14, picUserRecentArt15,
                          picUserRecentArt16, picUserRecentArt17, picUserRecentArt18, picUserRecentArt19, picUserRecentArt20}
#End Region

        ' xml stuff
        Dim recenttrackxml As String = CallAPI("user.getRecentTracks", My.Settings.User, "extended=1")
        Dim recenttracknodes() As String = {"name", "artist/name", "album"}

        For count As Byte = 0 To 19
            ' parse xml
            ParseXML(recenttrackxml, "/lfm/recenttracks/track", count, recenttracknodes)

            ' set labels
            Invoke(Sub() TrackLabel(count, 0).Text = recenttracknodes(0))
            Invoke(Sub() TrackLabel(count, 1).Text = recenttracknodes(1))
            Invoke(Sub() TrackLabel(count, 2).Text = recenttracknodes(2))

            ' set art
            Dim loadurl As String = ParseImage(recenttrackxml, "/lfm/recenttracks/track", count, 8)
            Try
                TrackArt(count).Load(loadurl)
            Catch ex As Exception
                TrackArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset nodes
            recenttracknodes = {"name", "artist/name", "album"}
        Next

        ' report 100% progress
        progress += 33
        UpdateProgressChange()
#End Region

    End Sub

    ' user loved tracks (separate because of page control)
    Public Sub UserLovedTracksUpdate(ByVal page As String)
        progressmultiplier += 1

        ' get xml
        Dim xml As String = CallAPI("user.getLovedTracks", My.Settings.User, "page=" & page)

        ' report 20% progress
        progress += 20
        UpdateProgressChange()

        ' set labels
        Invoke(Sub() lblUserLovedTotalPages.Text = "Total Pages: " & ParseMetadata(xml, "totalPages="))  ' set total pages label
        Invoke(Sub() lblUserLovedTotalTracks.Text = "Total Tracks: " & ParseMetadata(xml, "total="))     ' set total tracks label
        Invoke(Sub() ltvUserLovedTracks.Items.Clear())

        ' report 30% progress
        progress += 10
        UpdateProgressChange()

        ' set max numeric box value
        Dim totalpages As UShort
        UShort.TryParse(ParseMetadata(xml, "totalPages="), totalpages)
        Invoke(Sub() nudUserLovedPage.Maximum = totalpages)

        ' report 40% progress
        progress += 10
        UpdateProgressChange()

        ' parse information to put in listview
        Dim xmlnodes() As String = {"name", "artist/name", "date"}
        For count As Byte = 0 To 49
            ParseXML(xml, "/lfm/lovedtracks/track", count, xmlnodes)

            ' add items if no error
            If xmlnodes(0).Contains("ERROR:") = False Then
                Invoke(Sub() ltvUserLovedTracks.Items.Add(xmlnodes(0)))
                Invoke(Sub() ltvUserLovedTracks.Items(count).SubItems.Add(xmlnodes(1)))
                Invoke(Sub() ltvUserLovedTracks.Items(count).SubItems.Add(xmlnodes(2)))
            End If
            ' reset nodes
            xmlnodes = {"name", "artist/name", "date"}

            ' report 40-90% progress
            progress += 1
            UpdateProgressChange()
        Next

        ' set numeric box value back to current page
        Dim pagenum As UShort
        Dim metadata As String = ParseMetadata(xml, "page=")
        UShort.TryParse(metadata, pagenum)
        If nudUserLovedPage.Maximum > 0 Then
            Invoke(Sub() nudUserLovedPage.Value = pagenum)
        Else
            Invoke(Sub() nudUserLovedPage.Value = 0)
        End If

        ' report 100% progress
        progress += 10
        UpdateProgressChange()
    End Sub

    ' user charts (separate because of date control)
    Public Sub UserChartsUpdate()
        progressmultiplier += 1

#Region "Fatty Arrays"
        Dim TopTrackLabels(,) As Label = {{lblUserTopTrackTitle1, lblUserTopTrackArtist1, lblUserTopTrackAlbum1, lblUserTopTrackPlaycount1},
                                          {lblUserTopTrackTitle2, lblUserTopTrackArtist2, lblUserTopTrackAlbum2, lblUserTopTrackPlaycount2},
                                          {lblUserTopTrackTitle3, lblUserTopTrackArtist3, lblUserTopTrackAlbum3, lblUserTopTrackPlaycount3},
                                          {lblUserTopTrackTitle4, lblUserTopTrackArtist4, lblUserTopTrackAlbum4, lblUserTopTrackPlaycount4},
                                          {lblUserTopTrackTitle5, lblUserTopTrackArtist5, lblUserTopTrackAlbum5, lblUserTopTrackPlaycount5},
                                          {lblUserTopTrackTitle6, lblUserTopTrackArtist6, lblUserTopTrackAlbum6, lblUserTopTrackPlaycount6},
                                          {lblUserTopTrackTitle7, lblUserTopTrackArtist7, lblUserTopTrackAlbum7, lblUserTopTrackPlaycount7},
                                          {lblUserTopTrackTitle8, lblUserTopTrackArtist8, lblUserTopTrackAlbum8, lblUserTopTrackPlaycount8},
                                          {lblUserTopTrackTitle9, lblUserTopTrackArtist9, lblUserTopTrackAlbum9, lblUserTopTrackPlaycount9},
                                          {lblUserTopTrackTitle10, lblUserTopTrackArtist10, lblUserTopTrackAlbum10, lblUserTopTrackPlaycount10},
                                          {lblUserTopTrackTitle11, lblUserTopTrackArtist11, lblUserTopTrackAlbum11, lblUserTopTrackPlaycount11},
                                          {lblUserTopTrackTitle12, lblUserTopTrackArtist12, lblUserTopTrackAlbum12, lblUserTopTrackPlaycount12},
                                          {lblUserTopTrackTitle13, lblUserTopTrackArtist13, lblUserTopTrackAlbum13, lblUserTopTrackPlaycount13},
                                          {lblUserTopTrackTitle14, lblUserTopTrackArtist14, lblUserTopTrackAlbum14, lblUserTopTrackPlaycount14},
                                          {lblUserTopTrackTitle15, lblUserTopTrackArtist15, lblUserTopTrackAlbum15, lblUserTopTrackPlaycount15},
                                          {lblUserTopTrackTitle16, lblUserTopTrackArtist16, lblUserTopTrackAlbum16, lblUserTopTrackPlaycount16},
                                          {lblUserTopTrackTitle17, lblUserTopTrackArtist17, lblUserTopTrackAlbum17, lblUserTopTrackPlaycount17},
                                          {lblUserTopTrackTitle18, lblUserTopTrackArtist18, lblUserTopTrackAlbum18, lblUserTopTrackPlaycount18},
                                          {lblUserTopTrackTitle19, lblUserTopTrackArtist19, lblUserTopTrackAlbum19, lblUserTopTrackPlaycount19},
                                          {lblUserTopTrackTitle20, lblUserTopTrackArtist20, lblUserTopTrackAlbum20, lblUserTopTrackPlaycount20}}

        Dim TopTrackArt() As PictureBox = {picUserTopTrackArt1, picUserTopTrackArt2, picUserTopTrackArt3, picUserTopTrackArt4, picUserTopTrackArt5,
                                           picUserTopTrackArt6, picUserTopTrackArt7, picUserTopTrackArt8, picUserTopTrackArt9, picUserTopTrackArt10,
                                           picUserTopTrackArt11, picUserTopTrackArt12, picUserTopTrackArt13, picUserTopTrackArt14, picUserTopTrackArt15,
                                           picUserTopTrackArt16, picUserTopTrackArt17, picUserTopTrackArt18, picUserTopTrackArt19, picUserTopTrackArt20}

        Dim TopArtistLabels(,) As Label = {{lblUserTopArtist1, lblUserTopArtistPlaycount1},
                                          {lblUserTopArtist2, lblUserTopArtistPlaycount2},
                                          {lblUserTopArtist3, lblUserTopArtistPlaycount3},
                                          {lblUserTopArtist4, lblUserTopArtistPlaycount4},
                                          {lblUserTopArtist5, lblUserTopArtistPlaycount5},
                                          {lblUserTopArtist6, lblUserTopArtistPlaycount6},
                                          {lblUserTopArtist7, lblUserTopArtistPlaycount7},
                                          {lblUserTopArtist8, lblUserTopArtistPlaycount8},
                                          {lblUserTopArtist9, lblUserTopArtistPlaycount9},
                                          {lblUserTopArtist10, lblUserTopArtistPlaycount10},
                                          {lblUserTopArtist11, lblUserTopArtistPlaycount11},
                                          {lblUserTopArtist12, lblUserTopArtistPlaycount12},
                                          {lblUserTopArtist13, lblUserTopArtistPlaycount13},
                                          {lblUserTopArtist14, lblUserTopArtistPlaycount14},
                                          {lblUserTopArtist15, lblUserTopArtistPlaycount15},
                                          {lblUserTopArtist16, lblUserTopArtistPlaycount16},
                                          {lblUserTopArtist17, lblUserTopArtistPlaycount17},
                                          {lblUserTopArtist18, lblUserTopArtistPlaycount18},
                                          {lblUserTopArtist19, lblUserTopArtistPlaycount19},
                                          {lblUserTopArtist20, lblUserTopArtistPlaycount20}}

        Dim TopAlbumLabels(,) As Label = {{lblUserTopAlbum1, lblUserTopAlbumArtist1, lblUserTopAlbumPlaycount1},
                                          {lblUserTopAlbum2, lblUserTopAlbumArtist2, lblUserTopAlbumPlaycount2},
                                          {lblUserTopAlbum3, lblUserTopAlbumArtist3, lblUserTopAlbumPlaycount3},
                                          {lblUserTopAlbum4, lblUserTopAlbumArtist4, lblUserTopAlbumPlaycount4},
                                          {lblUserTopAlbum5, lblUserTopAlbumArtist5, lblUserTopAlbumPlaycount5},
                                          {lblUserTopAlbum6, lblUserTopAlbumArtist6, lblUserTopAlbumPlaycount6},
                                          {lblUserTopAlbum7, lblUserTopAlbumArtist7, lblUserTopAlbumPlaycount7},
                                          {lblUserTopAlbum8, lblUserTopAlbumArtist8, lblUserTopAlbumPlaycount8},
                                          {lblUserTopAlbum9, lblUserTopAlbumArtist9, lblUserTopAlbumPlaycount9},
                                          {lblUserTopAlbum10, lblUserTopAlbumArtist10, lblUserTopAlbumPlaycount10},
                                          {lblUserTopAlbum11, lblUserTopAlbumArtist11, lblUserTopAlbumPlaycount11},
                                          {lblUserTopAlbum12, lblUserTopAlbumArtist12, lblUserTopAlbumPlaycount12},
                                          {lblUserTopAlbum13, lblUserTopAlbumArtist13, lblUserTopAlbumPlaycount13},
                                          {lblUserTopAlbum14, lblUserTopAlbumArtist14, lblUserTopAlbumPlaycount14},
                                          {lblUserTopAlbum15, lblUserTopAlbumArtist15, lblUserTopAlbumPlaycount15},
                                          {lblUserTopAlbum16, lblUserTopAlbumArtist16, lblUserTopAlbumPlaycount16},
                                          {lblUserTopAlbum17, lblUserTopAlbumArtist17, lblUserTopAlbumPlaycount17},
                                          {lblUserTopAlbum18, lblUserTopAlbumArtist18, lblUserTopAlbumPlaycount18},
                                          {lblUserTopAlbum19, lblUserTopAlbumArtist19, lblUserTopAlbumPlaycount19},
                                          {lblUserTopAlbum20, lblUserTopAlbumArtist20, lblUserTopAlbumPlaycount20}}

        Dim TopAlbumArt() As PictureBox = {picUserTopAlbumArt1, picUserTopAlbumArt2, picUserTopAlbumArt3, picUserTopAlbumArt4, picUserTopAlbumArt5,
                                           picUserTopAlbumArt6, picUserTopAlbumArt7, picUserTopAlbumArt8, picUserTopAlbumArt9, picUserTopAlbumArt10,
                                           picUserTopAlbumArt11, picUserTopAlbumArt12, picUserTopAlbumArt13, picUserTopAlbumArt14, picUserTopAlbumArt15,
                                           picUserTopAlbumArt16, picUserTopAlbumArt17, picUserTopAlbumArt18, picUserTopAlbumArt19, picUserTopAlbumArt20}

        ' report 1% progress
        progress += 1
        UpdateProgressChange()
#End Region

#Region "Tracks"
        ' dim stuff
        Dim TopTrackXML As String = CallAPI("user.getTopTracks", My.Settings.User, "limit=20")
        Dim TopTrackNodes() As String = {"name", "artist/name", "playcount"}
        Dim TrackInfoXML As String
        Dim TrackInfoNodes() As String = {"title"}
        Dim numberholder As UInteger

        ' get track info for 20 tracks
        For count As Byte = 0 To 19
            ' get initial track data (title and artist) from toptracks
            ParseXML(TopTrackXML, "/lfm/toptracks/track", count, TopTrackNodes)

            ' get track info xml
            TrackInfoXML = CallAPI("track.getInfo", "", "track=" & TopTrackNodes(0).Replace(" ", "+"), "artist=" & TopTrackNodes(1).Replace(" ", "+"))

            ' get track album from trackinfoxml
            ParseXML(TrackInfoXML, "/lfm/track/album", 0, TrackInfoNodes)

            ' detect errors and set to "(Unavailable)"
            For counter2 As Byte = 0 To 2
                If TopTrackNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopTrackNodes(counter2) = "(Unavailable)"
                End If
            Next counter2
            If TrackInfoNodes(0).Contains("Object reference not set to an instance of an object") = True Then
                TrackInfoNodes(0) = "(Unavailable)"
            End If

            ' set labels
            Invoke(Sub() TopTrackLabels(count, 0).Text = TopTrackNodes(0))     ' title
            Invoke(Sub() TopTrackLabels(count, 1).Text = TopTrackNodes(1))     ' artist
            Invoke(Sub() TopTrackLabels(count, 2).Text = TrackInfoNodes(0))    ' album
            ' apply formatting to playcount
            UInteger.TryParse(TopTrackNodes(2), numberholder)
            Invoke(Sub() TopTrackLabels(count, 3).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopTrackArt(count).Load(ParseImage(TrackInfoXML, "/lfm/track/album/image", 1))
            Catch ex As Exception
                TopTrackArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopTrackNodes = {"name", "artist/name", "playcount"}
            TrackInfoNodes = {"title"}
        Next

        ' report 34% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Artists"
        ' dim stuff
        Dim TopArtistXML As String = CallAPI("user.getTopArtists", My.Settings.User, "limit=20")
        Dim TopArtistNodes() As String = {"name", "playcount"}

        For count As Byte = 0 To 19
            ' get artist data (name and playcount)
            ParseXML(TopArtistXML, "/lfm/topartists/artist", count, TopArtistNodes)

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 1
                If TopArtistNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopArtistNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopArtistLabels(count, 0).Text = TopArtistNodes(0))     ' title
            ' apply formatting to playcount
            UInteger.TryParse(TopArtistNodes(1), numberholder)
            Invoke(Sub() TopArtistLabels(count, 1).Text = numberholder.ToString("N0"))

            ' reset node arrays
            TopArtistNodes = {"name", "playcount"}
        Next

        ' report 67% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Albums"
        ' dim stuff
        Dim TopAlbumXML As String = CallAPI("user.getTopAlbums", My.Settings.User, "limit=20")
        Dim TopAlbumNodes() As String = {"name", "artist/name", "playcount"}

        For count As Byte = 0 To 19
            ' get album data (name, artist, playcount)
            ParseXML(TopAlbumXML, "/lfm/topalbums/album", count, TopAlbumNodes)

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 2
                If TopAlbumNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopAlbumNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopAlbumLabels(count, 0).Text = TopAlbumNodes(0))     ' album
            Invoke(Sub() TopAlbumLabels(count, 1).Text = TopAlbumNodes(1))     ' artist
            ' apply formatting to playcount
            UInteger.TryParse(TopAlbumNodes(2), numberholder)
            Invoke(Sub() TopAlbumLabels(count, 2).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopAlbumArt(count).Load(ParseImage(TopAlbumXML, "/lfm/topalbums/album", count, 6))
            Catch ex As Exception
                TopAlbumArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopAlbumNodes = {"name", "artist/name", "playcount"}
        Next

        ' report 100% progress
        progress += 33
        UpdateProgressChange()
#End Region

    End Sub

    Public Sub UserChartsUpdate(ByVal unixfrom As UInteger, ByVal unixto As UInteger)
        progressmultiplier += 1

#Region "To>From Check"
        If unixfrom > unixto Then
            Invoke(Sub() MessageBox.Show("From date must be before to date", "User Charts", MessageBoxButtons.OK, MessageBoxIcon.Error))
            progress += 100
            UpdateProgressChange()
            Exit Sub
        End If
#End Region

#Region "Fatty Arrays"
        Dim TopTrackLabels(,) As Label = {{lblUserTopTrackTitle1, lblUserTopTrackArtist1, lblUserTopTrackAlbum1, lblUserTopTrackPlaycount1},
                                          {lblUserTopTrackTitle2, lblUserTopTrackArtist2, lblUserTopTrackAlbum2, lblUserTopTrackPlaycount2},
                                          {lblUserTopTrackTitle3, lblUserTopTrackArtist3, lblUserTopTrackAlbum3, lblUserTopTrackPlaycount3},
                                          {lblUserTopTrackTitle4, lblUserTopTrackArtist4, lblUserTopTrackAlbum4, lblUserTopTrackPlaycount4},
                                          {lblUserTopTrackTitle5, lblUserTopTrackArtist5, lblUserTopTrackAlbum5, lblUserTopTrackPlaycount5},
                                          {lblUserTopTrackTitle6, lblUserTopTrackArtist6, lblUserTopTrackAlbum6, lblUserTopTrackPlaycount6},
                                          {lblUserTopTrackTitle7, lblUserTopTrackArtist7, lblUserTopTrackAlbum7, lblUserTopTrackPlaycount7},
                                          {lblUserTopTrackTitle8, lblUserTopTrackArtist8, lblUserTopTrackAlbum8, lblUserTopTrackPlaycount8},
                                          {lblUserTopTrackTitle9, lblUserTopTrackArtist9, lblUserTopTrackAlbum9, lblUserTopTrackPlaycount9},
                                          {lblUserTopTrackTitle10, lblUserTopTrackArtist10, lblUserTopTrackAlbum10, lblUserTopTrackPlaycount10},
                                          {lblUserTopTrackTitle11, lblUserTopTrackArtist11, lblUserTopTrackAlbum11, lblUserTopTrackPlaycount11},
                                          {lblUserTopTrackTitle12, lblUserTopTrackArtist12, lblUserTopTrackAlbum12, lblUserTopTrackPlaycount12},
                                          {lblUserTopTrackTitle13, lblUserTopTrackArtist13, lblUserTopTrackAlbum13, lblUserTopTrackPlaycount13},
                                          {lblUserTopTrackTitle14, lblUserTopTrackArtist14, lblUserTopTrackAlbum14, lblUserTopTrackPlaycount14},
                                          {lblUserTopTrackTitle15, lblUserTopTrackArtist15, lblUserTopTrackAlbum15, lblUserTopTrackPlaycount15},
                                          {lblUserTopTrackTitle16, lblUserTopTrackArtist16, lblUserTopTrackAlbum16, lblUserTopTrackPlaycount16},
                                          {lblUserTopTrackTitle17, lblUserTopTrackArtist17, lblUserTopTrackAlbum17, lblUserTopTrackPlaycount17},
                                          {lblUserTopTrackTitle18, lblUserTopTrackArtist18, lblUserTopTrackAlbum18, lblUserTopTrackPlaycount18},
                                          {lblUserTopTrackTitle19, lblUserTopTrackArtist19, lblUserTopTrackAlbum19, lblUserTopTrackPlaycount19},
                                          {lblUserTopTrackTitle20, lblUserTopTrackArtist20, lblUserTopTrackAlbum20, lblUserTopTrackPlaycount20}}

        Dim TopTrackArt() As PictureBox = {picUserTopTrackArt1, picUserTopTrackArt2, picUserTopTrackArt3, picUserTopTrackArt4, picUserTopTrackArt5,
                                           picUserTopTrackArt6, picUserTopTrackArt7, picUserTopTrackArt8, picUserTopTrackArt9, picUserTopTrackArt10,
                                           picUserTopTrackArt11, picUserTopTrackArt12, picUserTopTrackArt13, picUserTopTrackArt14, picUserTopTrackArt15,
                                           picUserTopTrackArt16, picUserTopTrackArt17, picUserTopTrackArt18, picUserTopTrackArt19, picUserTopTrackArt20}

        Dim TopArtistLabels(,) As Label = {{lblUserTopArtist1, lblUserTopArtistPlaycount1},
                                          {lblUserTopArtist2, lblUserTopArtistPlaycount2},
                                          {lblUserTopArtist3, lblUserTopArtistPlaycount3},
                                          {lblUserTopArtist4, lblUserTopArtistPlaycount4},
                                          {lblUserTopArtist5, lblUserTopArtistPlaycount5},
                                          {lblUserTopArtist6, lblUserTopArtistPlaycount6},
                                          {lblUserTopArtist7, lblUserTopArtistPlaycount7},
                                          {lblUserTopArtist8, lblUserTopArtistPlaycount8},
                                          {lblUserTopArtist9, lblUserTopArtistPlaycount9},
                                          {lblUserTopArtist10, lblUserTopArtistPlaycount10},
                                          {lblUserTopArtist11, lblUserTopArtistPlaycount11},
                                          {lblUserTopArtist12, lblUserTopArtistPlaycount12},
                                          {lblUserTopArtist13, lblUserTopArtistPlaycount13},
                                          {lblUserTopArtist14, lblUserTopArtistPlaycount14},
                                          {lblUserTopArtist15, lblUserTopArtistPlaycount15},
                                          {lblUserTopArtist16, lblUserTopArtistPlaycount16},
                                          {lblUserTopArtist17, lblUserTopArtistPlaycount17},
                                          {lblUserTopArtist18, lblUserTopArtistPlaycount18},
                                          {lblUserTopArtist19, lblUserTopArtistPlaycount19},
                                          {lblUserTopArtist20, lblUserTopArtistPlaycount20}}

        Dim TopAlbumLabels(,) As Label = {{lblUserTopAlbum1, lblUserTopAlbumArtist1, lblUserTopAlbumPlaycount1},
                                          {lblUserTopAlbum2, lblUserTopAlbumArtist2, lblUserTopAlbumPlaycount2},
                                          {lblUserTopAlbum3, lblUserTopAlbumArtist3, lblUserTopAlbumPlaycount3},
                                          {lblUserTopAlbum4, lblUserTopAlbumArtist4, lblUserTopAlbumPlaycount4},
                                          {lblUserTopAlbum5, lblUserTopAlbumArtist5, lblUserTopAlbumPlaycount5},
                                          {lblUserTopAlbum6, lblUserTopAlbumArtist6, lblUserTopAlbumPlaycount6},
                                          {lblUserTopAlbum7, lblUserTopAlbumArtist7, lblUserTopAlbumPlaycount7},
                                          {lblUserTopAlbum8, lblUserTopAlbumArtist8, lblUserTopAlbumPlaycount8},
                                          {lblUserTopAlbum9, lblUserTopAlbumArtist9, lblUserTopAlbumPlaycount9},
                                          {lblUserTopAlbum10, lblUserTopAlbumArtist10, lblUserTopAlbumPlaycount10},
                                          {lblUserTopAlbum11, lblUserTopAlbumArtist11, lblUserTopAlbumPlaycount11},
                                          {lblUserTopAlbum12, lblUserTopAlbumArtist12, lblUserTopAlbumPlaycount12},
                                          {lblUserTopAlbum13, lblUserTopAlbumArtist13, lblUserTopAlbumPlaycount13},
                                          {lblUserTopAlbum14, lblUserTopAlbumArtist14, lblUserTopAlbumPlaycount14},
                                          {lblUserTopAlbum15, lblUserTopAlbumArtist15, lblUserTopAlbumPlaycount15},
                                          {lblUserTopAlbum16, lblUserTopAlbumArtist16, lblUserTopAlbumPlaycount16},
                                          {lblUserTopAlbum17, lblUserTopAlbumArtist17, lblUserTopAlbumPlaycount17},
                                          {lblUserTopAlbum18, lblUserTopAlbumArtist18, lblUserTopAlbumPlaycount18},
                                          {lblUserTopAlbum19, lblUserTopAlbumArtist19, lblUserTopAlbumPlaycount19},
                                          {lblUserTopAlbum20, lblUserTopAlbumArtist20, lblUserTopAlbumPlaycount20}}

        Dim TopAlbumArt() As PictureBox = {picUserTopAlbumArt1, picUserTopAlbumArt2, picUserTopAlbumArt3, picUserTopAlbumArt4, picUserTopAlbumArt5,
                                           picUserTopAlbumArt6, picUserTopAlbumArt7, picUserTopAlbumArt8, picUserTopAlbumArt9, picUserTopAlbumArt10,
                                           picUserTopAlbumArt11, picUserTopAlbumArt12, picUserTopAlbumArt13, picUserTopAlbumArt14, picUserTopAlbumArt15,
                                           picUserTopAlbumArt16, picUserTopAlbumArt17, picUserTopAlbumArt18, picUserTopAlbumArt19, picUserTopAlbumArt20}

        ' report 1% progress
        progress += 1
        UpdateProgressChange()
#End Region

#Region "Tracks"
        ' dim stuff
        Dim TopTrackXML As String = CallAPI("user.getWeeklyTrackChart", My.Settings.User, "from=" & unixfrom.ToString, "to=" & unixto.ToString)
        Dim TopTrackNodes() As String = {"name", "artist", "playcount"}
        Dim TrackInfoXML As String
        Dim TrackInfoNodes() As String = {"title"}
        Dim numberholder As UInteger

        ' get track info for 20 tracks
        For count As Byte = 0 To 19
            ' get initial track data (title and artist) from toptracks
            ParseXML(TopTrackXML, "/lfm/weeklytrackchart/track", count, TopTrackNodes)

            ' get track info xml
            TrackInfoXML = CallAPI("track.getInfo", "", "track=" & TopTrackNodes(0).Replace(" ", "+"), "artist=" & TopTrackNodes(1).Replace(" ", "+"))

            ' get track album from trackinfoxml
            ParseXML(TrackInfoXML, "/lfm/track/album", 0, TrackInfoNodes)

            ' detect errors and set to "(Unavailable)"
            For counter2 As Byte = 0 To 2
                If TopTrackNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopTrackNodes(counter2) = "(Unavailable)"
                End If
            Next counter2
            If TrackInfoNodes(0).Contains("Object reference not set to an instance of an object") = True Then
                TrackInfoNodes(0) = "(Unavailable)"
            End If

            ' set labels
            Invoke(Sub() TopTrackLabels(count, 0).Text = TopTrackNodes(0))     ' title
            Invoke(Sub() TopTrackLabels(count, 1).Text = TopTrackNodes(1))     ' artist
            Invoke(Sub() TopTrackLabels(count, 2).Text = TrackInfoNodes(0))    ' album
            ' apply formatting to playcount
            UInteger.TryParse(TopTrackNodes(2), numberholder)
            Invoke(Sub() TopTrackLabels(count, 3).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopTrackArt(count).Load(ParseImage(TrackInfoXML, "/lfm/track/album/image", 1))
            Catch ex As Exception
                TopTrackArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopTrackNodes = {"name", "artist", "playcount"}
            TrackInfoNodes = {"title"}
        Next

        ' report 34% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Artists"
        ' dim stuff
        Dim TopArtistXML As String = CallAPI("user.getWeeklyArtistChart", My.Settings.User, "from=" & unixfrom.ToString, "to=" & unixto.ToString)
        Dim TopArtistNodes() As String = {"name", "playcount"}

        For count As Byte = 0 To 19
            ' get artist data (name and playcount)
            ParseXML(TopArtistXML, "/lfm/weeklyartistchart/artist", count, TopArtistNodes)

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 1
                If TopArtistNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopArtistNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopArtistLabels(count, 0).Text = TopArtistNodes(0))     ' title
            ' apply formatting to playcount
            UInteger.TryParse(TopArtistNodes(1), numberholder)
            Invoke(Sub() TopArtistLabels(count, 1).Text = numberholder.ToString("N0"))

            ' reset node arrays
            TopArtistNodes = {"name", "playcount"}
        Next

        ' report 67% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Albums"
        ' dim stuff
        Dim TopAlbumXML As String = CallAPI("user.getWeeklyAlbumChart", My.Settings.User, "from=" & unixfrom.ToString, "to=" & unixto.ToString)
        Dim TopAlbumNodes() As String = {"name", "artist", "playcount"}
        Dim AlbumInfoXML As String

        For count As Byte = 0 To 19
            ' get album data (name, artist, playcount)
            ParseXML(TopAlbumXML, "/lfm/weeklyalbumchart/album", count, TopAlbumNodes)

            ' get album info for image
            AlbumInfoXML = CallAPI("album.getInfo", My.Settings.User, "album=" & TopAlbumNodes(0).Replace(" ", "+"), "artist=" & TopAlbumNodes(1).Replace(" ", "+"))

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 2
                If TopAlbumNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopAlbumNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopAlbumLabels(count, 0).Text = TopAlbumNodes(0))     ' album
            Invoke(Sub() TopAlbumLabels(count, 1).Text = TopAlbumNodes(1))     ' artist
            ' apply formatting to playcount
            UInteger.TryParse(TopAlbumNodes(2), numberholder)
            Invoke(Sub() TopAlbumLabels(count, 2).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopAlbumArt(count).Load(ParseImage(AlbumInfoXML, "/lfm/album/image", 1))
            Catch ex As Exception
                TopAlbumArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopAlbumNodes = {"name", "artist", "playcount"}
        Next

        ' report 100% progress
        progress += 33
        UpdateProgressChange()
#End Region

    End Sub

    Public Sub UserHistoryUpdate()
        progressmultiplier += 1

        ' determine whether date is used or not
        Dim historyXML As String
        If radUserAllTime.Checked = True Then
            historyXML = CallAPI("user.getRecentTracks", My.Settings.User, "page=" & nudUserHistoryPage.Value.ToString())
        Else
            historyXML = CallAPI("user.getRecentTracks", My.Settings.User, "page=" & nudUserHistoryPage.Value.ToString(), "from=" & DateToUnix(dtpUserFrom.Value.Date) - timezoneoffset, "to=" & DateToUnix(dtpUserTo.Value.Date) - timezoneoffset)
        End If

        ' determine whether something is now playing 
        Dim metadataOffset As Byte = 1
        If StrCount(historyXML, "</track>") > StrCount(historyXML, "date uts=") Then
            metadataOffset = 0
        End If

        ' report 20% progress
        progress += 20
        UpdateProgressChange()

        ' set labels
        Invoke(Sub() lblUserHistoryTotalPages.Text = "Total Pages: " & ParseMetadata(historyXML, "totalPages="))  ' set total pages label
        Invoke(Sub() lblUserHistoryTotalTracks.Text = "Total Tracks: " & ParseMetadata(historyXML, "total="))    ' set total tracks label
        Invoke(Sub() ltvUserHistory.Items.Clear())

        ' report 30% progress
        progress += 10
        UpdateProgressChange()

        ' set max numeric box value
        Dim totalpages As UShort
        UShort.TryParse(ParseMetadata(historyXML, "totalPages="), totalpages)
        Invoke(Sub() nudUserHistoryPage.Maximum = totalpages)

        ' report 40% progress
        progress += 10
        UpdateProgressChange()

        ' parse xml and add to list
        Dim historynodes() As String = {"name", "artist", "album", "date"}
        For count As Byte = 0 To 50
            ' parse xml
            ParseXML(historyXML, "/lfm/recenttracks/track", count, historynodes)

            ' add items if no error
            Dim counterrors As Integer = 0
            If historynodes(0).Contains("ERROR:") = False Then
                Invoke(Sub() ltvUserHistory.Items.Add(historynodes(0)))
                Invoke(Sub() ltvUserHistory.Items(count - counterrors).SubItems.Add(historynodes(1)))
                Invoke(Sub() ltvUserHistory.Items(count - counterrors).SubItems.Add(historynodes(2)))
                ' check for date error due to now playing
                If historynodes(3).Contains("ERROR: ") = False Then
                    Invoke(Sub() ltvUserHistory.Items(count).SubItems.Add(UnixToDate(CUInt(ParseMetadata(historyXML, "date uts=", (count - counterrors + metadataOffset)) + timezoneoffset)).ToString("G")))
                Else
                    Invoke(Sub() ltvUserHistory.Items(count).SubItems.Add("Now Playing"))
                End If
            Else
                counterrors += 1
            End If
            ' reset nodes
            historynodes = {"name", "artist", "album", "date"}

            ' report 40-91% progress
            progress += 1
            UpdateProgressChange()
        Next

        ' set numeric box value back to current page
        Dim pagenum As UShort
        Dim metadata As String = ParseMetadata(historyXML, "page=")
        UShort.TryParse(metadata, pagenum)
        If nudUserHistoryPage.Maximum > 0 AndAlso nudUserHistoryPage.Minimum > 0 Then
            Invoke(Sub() nudUserHistoryPage.Value = pagenum)
        Else
            Invoke(Sub() nudUserHistoryPage.Minimum = 0)
            Invoke(Sub() nudUserHistoryPage.Value = 0)
        End If

        ' report 100% progress
        progress += 9
        UpdateProgressChange()
    End Sub
#End Region

#Region "User Lookup"
    Public Sub UpdateUserL()
        progressmultiplier += 1

#Region "Info"
        ' xml stuff
        Dim UserInfoXML As String = CallAPI("user.getInfo", userlookup)
        Dim UserInfoNodes() As String = {"name", "realname", "url", "country", "age", "gender", "playcount", "playlists", "registered"}

        ' parse xml
        ParseXML(UserInfoXML, "/lfm/user", 0, UserInfoNodes)
        Try
            picUserL.Load(ParseImage(UserInfoXML, "/lfm/user/image", 3))
        Catch ex As Exception
            picUserL.Image = My.Resources.imageunavailable
        End Try

        ' convert registered date to datetime
        Dim unixtime As UInteger
        UInteger.TryParse(UserInfoNodes(8), unixtime)
        Dim RegisteredDateTime As Date = UnixToDate(unixtime + timezoneoffset)

        ' gender formatting
        Select Case UserInfoNodes(5)
            Case "m"
                UserInfoNodes(5) = "Male"
            Case "f"
                UserInfoNodes(5) = "Female"
            Case Else
                UserInfoNodes(5) = "Not Specified"
        End Select

        ' age formatting
        If UserInfoNodes(4) = "0" Then
            UserInfoNodes(4) = "Not Specified"
        End If

        ' playcount formatting
        UserInfoNodes(6) = CInt(UserInfoNodes(6)).ToString("N0")

        ' create textbox data
        Invoke(Sub() txtUserLInfo.Clear())
        Invoke(Sub() txtUserLInfo.Text = "Name" & vbCrLf & UserInfoNodes(0) & vbCrLf & vbCrLf & "Real Name" & vbCrLf & UserInfoNodes(1) & vbCrLf & vbCrLf &
                   "URL" & vbCrLf & UserInfoNodes(2) & vbCrLf & vbCrLf & "Country" & vbCrLf & UserInfoNodes(3) & vbCrLf & vbCrLf &
                   "Age" & vbCrLf & UserInfoNodes(4) & vbCrLf & vbCrLf & "Gender" & vbCrLf & UserInfoNodes(5) & vbCrLf & vbCrLf & "Playcount" & vbCrLf & UserInfoNodes(6) & vbCrLf & vbCrLf &
                   "Playlists" & vbCrLf & UserInfoNodes(7) & vbCrLf & vbCrLf & "Registered" & vbCrLf & RegisteredDateTime.ToString)

        ' format textbox
        Dim BoldFont As New Font("Segoe UI", 10, FontStyle.Bold)
        ' name
        Invoke(Sub() txtUserLInfo.SelectionStart = 0)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Text".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' real name
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "Real Name") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Real Name".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' URL
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "URL") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "URL".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' country
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "Country") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Country".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' age
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "Age") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Age".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' gender
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "Gender") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Gender".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' playcount
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "Playcount") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Playcount".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' playlists
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "Playlists") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Playlists".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)
        ' registered
        Invoke(Sub() txtUserLInfo.SelectionStart = InStr(txtUserLInfo.Text, "Registered") - 1)
        Invoke(Sub() txtUserLInfo.SelectionLength = "Registered".Length)
        Invoke(Sub() txtUserLInfo.SelectionFont = BoldFont)

        ' report 34% progress
        progress += 34
        UpdateProgressChange()
#End Region

#Region "Friends"
        ' parse for total friends
        Dim FriendsTotalXML As String = CallAPI("user.getFriends", userlookup, "limit=1")
        Dim totalfriends As String = ParseMetadata(FriendsTotalXML, "total=")
        If totalfriends.Contains("ERROR:") = True Then
            totalfriends = "0"
        End If

        ' xml stuff
        Dim FriendsXML As String = CallAPI("user.getFriends", userlookup, "limit=" & totalfriends)
        Invoke(Sub() lblUserLFriendTotal.Text = "Friends: " & totalfriends)
        If lblUserLFriendTotal.Text.Contains("ParseMetadata") = True Then
            Invoke(Sub() lblUserLFriendTotal.Text = "Friends: 0")
        End If
        Dim FriendsNodes() As String = {"name", "realname", "url", "registered"}

        ' find number of users in xml
        Dim loopend As Boolean = False
        Dim startpos As Integer = 1
        Dim usercount As UShort
        Do
            startpos = InStr(startpos, FriendsXML, "<user>")
            If startpos <= 0 Then
                loopend = True  ' end loop if no more can be found
            Else
                startpos += 6   ' increment startpos to the end of <user>
                usercount += 1  ' increment usercount
            End If
        Loop Until loopend = True

        ' add each friend to list view
        Invoke(Sub() ltvUserLFriends.Items.Clear())
        If usercount > 0 Then
            For count As UShort = 0 To usercount - 1
                ParseXML(FriendsXML, "/lfm/friends/user", count, FriendsNodes)              ' get data from xml
                Invoke(Sub() ltvUserLFriends.Items.Add(FriendsNodes(0)))                     ' add listview item
                Invoke(Sub() ltvUserLFriends.Items(count).SubItems.Add(FriendsNodes(1)))     ' add subitem 1
                Invoke(Sub() ltvUserLFriends.Items(count).SubItems.Add(FriendsNodes(2)))     ' add subitem 2
                Invoke(Sub() ltvUserLFriends.Items(count).SubItems.Add(FriendsNodes(3)))     ' add subitem 3
                FriendsNodes = {"name", "realname", "url", "registered"}                    ' reset nodes
            Next
        End If

        ' report 67% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Recent Tracks"
#Region "Fatty Arrays"
        Dim TrackLabel(,) = {{lblUserLRecentTitle1, lblUserLRecentArtist1, lblUserLRecentAlbum1},
                             {lblUserLRecentTitle2, lblUserLRecentArtist2, lblUserLRecentAlbum2},
                             {lblUserLRecentTitle3, lblUserLRecentArtist3, lblUserLRecentAlbum3},
                             {lblUserLRecentTitle4, lblUserLRecentArtist4, lblUserLRecentAlbum4},
                             {lblUserLRecentTitle5, lblUserLRecentArtist5, lblUserLRecentAlbum5},
                             {lblUserLRecentTitle6, lblUserLRecentArtist6, lblUserLRecentAlbum6},
                             {lblUserLRecentTitle7, lblUserLRecentArtist7, lblUserLRecentAlbum7},
                             {lblUserLRecentTitle8, lblUserLRecentArtist8, lblUserLRecentAlbum8},
                             {lblUserLRecentTitle9, lblUserLRecentArtist9, lblUserLRecentAlbum9},
                             {lblUserLRecentTitle10, lblUserLRecentArtist10, lblUserLRecentAlbum10},
                             {lblUserLRecentTitle11, lblUserLRecentArtist11, lblUserLRecentAlbum11},
                             {lblUserLRecentTitle12, lblUserLRecentArtist12, lblUserLRecentAlbum12},
                             {lblUserLRecentTitle13, lblUserLRecentArtist13, lblUserLRecentAlbum13},
                             {lblUserLRecentTitle14, lblUserLRecentArtist14, lblUserLRecentAlbum14},
                             {lblUserLRecentTitle15, lblUserLRecentArtist15, lblUserLRecentAlbum15},
                             {lblUserLRecentTitle16, lblUserLRecentArtist16, lblUserLRecentAlbum16},
                             {lblUserLRecentTitle17, lblUserLRecentArtist17, lblUserLRecentAlbum17},
                             {lblUserLRecentTitle18, lblUserLRecentArtist18, lblUserLRecentAlbum18},
                             {lblUserLRecentTitle19, lblUserLRecentArtist19, lblUserLRecentAlbum19},
                             {lblUserLRecentTitle20, lblUserLRecentArtist20, lblUserLRecentAlbum20}}

        Dim TrackArt() = {picUserLRecentArt1, picUserLRecentArt2, picUserLRecentArt3, picUserLRecentArt4, picUserLRecentArt5,
                          picUserLRecentArt6, picUserLRecentArt7, picUserLRecentArt8, picUserLRecentArt9, picUserLRecentArt10,
                          picUserLRecentArt11, picUserLRecentArt12, picUserLRecentArt13, picUserLRecentArt14, picUserLRecentArt15,
                          picUserLRecentArt16, picUserLRecentArt17, picUserLRecentArt18, picUserLRecentArt19, picUserLRecentArt20}
#End Region

        ' xml stuff
        Dim recenttrackxml As String = CallAPI("user.getRecentTracks", userlookup, "extended=1")
        Dim recenttracknodes() As String = {"name", "artist/name", "album"}

        For count As Byte = 0 To 19
            ' parse xml
            ParseXML(recenttrackxml, "/lfm/recenttracks/track", count, recenttracknodes)

            ' set labels
            Invoke(Sub() TrackLabel(count, 0).Text = recenttracknodes(0))
            Invoke(Sub() TrackLabel(count, 1).Text = recenttracknodes(1))
            Invoke(Sub() TrackLabel(count, 2).Text = recenttracknodes(2))

            ' set art
            Dim loadurl As String = ParseImage(recenttrackxml, "/lfm/recenttracks/track", count, 8)
            Try
                TrackArt(count).Load(loadurl)
            Catch ex As Exception
                TrackArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset nodes
            recenttracknodes = {"name", "artist/name", "album"}
        Next

        ' report 100% progress
        progress += 33
        UpdateProgressChange()
#End Region

    End Sub

    Public Sub UserLLovedTracksUpdate(ByVal page As String)
        progressmultiplier += 1

        ' get xml
        Dim xml As String = CallAPI("user.getLovedTracks", userlookup, "page=" & page)

        ' report 20% progress
        progress += 20
        UpdateProgressChange()

        ' set labels
        Invoke(Sub() lblUserLLovedTotalPages.Text = "Total Pages: " & ParseMetadata(xml, "totalPages="))  ' set total pages label
        Invoke(Sub() lblUserLLovedTotalTracks.Text = "Total Tracks: " & ParseMetadata(xml, "total="))     ' set total tracks label
        Invoke(Sub() ltvUserLLovedTracks.Items.Clear())

        ' report 30% progress
        progress += 10
        UpdateProgressChange()

        ' set max numeric box value
        Dim totalpages As UShort
        UShort.TryParse(ParseMetadata(xml, "totalPages="), totalpages)
        nudUserLovedPage.Maximum = totalpages

        ' report 40% progress
        progress += 10
        UpdateProgressChange()

        ' parse information to put in listview
        Dim xmlnodes() As String = {"name", "artist/name", "date"}
        For count As Byte = 0 To 49
            ParseXML(xml, "/lfm/lovedtracks/track", count, xmlnodes)

            ' add items if no error
            If xmlnodes(0).Contains("ERROR:") = False Then
                Invoke(Sub() ltvUserLLovedTracks.Items.Add(xmlnodes(0)))
                Invoke(Sub() ltvUserLLovedTracks.Items(count).SubItems.Add(xmlnodes(1)))
                Invoke(Sub() ltvUserLLovedTracks.Items(count).SubItems.Add(xmlnodes(2)))
            End If
            ' reset nodes
            xmlnodes = {"name", "artist/name", "date"}

            ' report 40-90% progress
            progress += 1
            UpdateProgressChange()
        Next

        ' set numeric box value back to current page
        Dim pagenum As UShort
        Dim metadata As String = ParseMetadata(xml, "page=")
        UShort.TryParse(metadata, pagenum)
        If nudUserLLovedPage.Maximum > 0 AndAlso nudUserLLovedPage.Minimum > 0 Then
            Invoke(Sub() nudUserLLovedPage.Value = pagenum)
        Else
            Invoke(Sub() nudUserLLovedPage.Minimum = 0)
            Invoke(Sub() nudUserLLovedPage.Value = 0)
        End If

        ' report 100% progress
        progress += 10
        UpdateProgressChange()
    End Sub

    Public Sub UserLChartsUpdate()
        progressmultiplier += 1

#Region "Fatty Arrays"
        Dim TopTrackLabels(,) As Label = {{lblUserLTopTrackTitle1, lblUserLTopTrackArtist1, lblUserLTopTrackAlbum1, lblUserLTopTrackPlaycount1},
                                          {lblUserLTopTrackTitle2, lblUserLTopTrackArtist2, lblUserLTopTrackAlbum2, lblUserLTopTrackPlaycount2},
                                          {lblUserLTopTrackTitle3, lblUserLTopTrackArtist3, lblUserLTopTrackAlbum3, lblUserLTopTrackPlaycount3},
                                          {lblUserLTopTrackTitle4, lblUserLTopTrackArtist4, lblUserLTopTrackAlbum4, lblUserLTopTrackPlaycount4},
                                          {lblUserLTopTrackTitle5, lblUserLTopTrackArtist5, lblUserLTopTrackAlbum5, lblUserLTopTrackPlaycount5},
                                          {lblUserLTopTrackTitle6, lblUserLTopTrackArtist6, lblUserLTopTrackAlbum6, lblUserLTopTrackPlaycount6},
                                          {lblUserLTopTrackTitle7, lblUserLTopTrackArtist7, lblUserLTopTrackAlbum7, lblUserLTopTrackPlaycount7},
                                          {lblUserLTopTrackTitle8, lblUserLTopTrackArtist8, lblUserLTopTrackAlbum8, lblUserLTopTrackPlaycount8},
                                          {lblUserLTopTrackTitle9, lblUserLTopTrackArtist9, lblUserLTopTrackAlbum9, lblUserLTopTrackPlaycount9},
                                          {lblUserLTopTrackTitle10, lblUserLTopTrackArtist10, lblUserLTopTrackAlbum10, lblUserLTopTrackPlaycount10},
                                          {lblUserLTopTrackTitle11, lblUserLTopTrackArtist11, lblUserLTopTrackAlbum11, lblUserLTopTrackPlaycount11},
                                          {lblUserLTopTrackTitle12, lblUserLTopTrackArtist12, lblUserLTopTrackAlbum12, lblUserLTopTrackPlaycount12},
                                          {lblUserLTopTrackTitle13, lblUserLTopTrackArtist13, lblUserLTopTrackAlbum13, lblUserLTopTrackPlaycount13},
                                          {lblUserLTopTrackTitle14, lblUserLTopTrackArtist14, lblUserLTopTrackAlbum14, lblUserLTopTrackPlaycount14},
                                          {lblUserLTopTrackTitle15, lblUserLTopTrackArtist15, lblUserLTopTrackAlbum15, lblUserLTopTrackPlaycount15},
                                          {lblUserLTopTrackTitle16, lblUserLTopTrackArtist16, lblUserLTopTrackAlbum16, lblUserLTopTrackPlaycount16},
                                          {lblUserLTopTrackTitle17, lblUserLTopTrackArtist17, lblUserLTopTrackAlbum17, lblUserLTopTrackPlaycount17},
                                          {lblUserLTopTrackTitle18, lblUserLTopTrackArtist18, lblUserLTopTrackAlbum18, lblUserLTopTrackPlaycount18},
                                          {lblUserLTopTrackTitle19, lblUserLTopTrackArtist19, lblUserLTopTrackAlbum19, lblUserLTopTrackPlaycount19},
                                          {lblUserLTopTrackTitle20, lblUserLTopTrackArtist20, lblUserLTopTrackAlbum20, lblUserLTopTrackPlaycount20}}

        Dim TopTrackArt() As PictureBox = {picUserLTopTrackArt1, picUserLTopTrackArt2, picUserLTopTrackArt3, picUserLTopTrackArt4, picUserLTopTrackArt5,
                                           picUserLTopTrackArt6, picUserLTopTrackArt7, picUserLTopTrackArt8, picUserLTopTrackArt9, picUserLTopTrackArt10,
                                           picUserLTopTrackArt11, picUserLTopTrackArt12, picUserLTopTrackArt13, picUserLTopTrackArt14, picUserLTopTrackArt15,
                                           picUserLTopTrackArt16, picUserLTopTrackArt17, picUserLTopTrackArt18, picUserLTopTrackArt19, picUserLTopTrackArt20}

        Dim TopArtistLabels(,) As Label = {{lblUserLTopArtist1, lblUserLTopArtistPlaycount1},
                                          {lblUserLTopArtist2, lblUserLTopArtistPlaycount2},
                                          {lblUserLTopArtist3, lblUserLTopArtistPlaycount3},
                                          {lblUserLTopArtist4, lblUserLTopArtistPlaycount4},
                                          {lblUserLTopArtist5, lblUserLTopArtistPlaycount5},
                                          {lblUserLTopArtist6, lblUserLTopArtistPlaycount6},
                                          {lblUserLTopArtist7, lblUserLTopArtistPlaycount7},
                                          {lblUserLTopArtist8, lblUserLTopArtistPlaycount8},
                                          {lblUserLTopArtist9, lblUserLTopArtistPlaycount9},
                                          {lblUserLTopArtist10, lblUserLTopArtistPlaycount10},
                                          {lblUserLTopArtist11, lblUserLTopArtistPlaycount11},
                                          {lblUserLTopArtist12, lblUserLTopArtistPlaycount12},
                                          {lblUserLTopArtist13, lblUserLTopArtistPlaycount13},
                                          {lblUserLTopArtist14, lblUserLTopArtistPlaycount14},
                                          {lblUserLTopArtist15, lblUserLTopArtistPlaycount15},
                                          {lblUserLTopArtist16, lblUserLTopArtistPlaycount16},
                                          {lblUserLTopArtist17, lblUserLTopArtistPlaycount17},
                                          {lblUserLTopArtist18, lblUserLTopArtistPlaycount18},
                                          {lblUserLTopArtist19, lblUserLTopArtistPlaycount19},
                                          {lblUserLTopArtist20, lblUserLTopArtistPlaycount20}}

        Dim TopAlbumLabels(,) As Label = {{lblUserLTopAlbum1, lblUserLTopAlbumArtist1, lblUserLTopAlbumPlaycount1},
                                          {lblUserLTopAlbum2, lblUserLTopAlbumArtist2, lblUserLTopAlbumPlaycount2},
                                          {lblUserLTopAlbum3, lblUserLTopAlbumArtist3, lblUserLTopAlbumPlaycount3},
                                          {lblUserLTopAlbum4, lblUserLTopAlbumArtist4, lblUserLTopAlbumPlaycount4},
                                          {lblUserLTopAlbum5, lblUserLTopAlbumArtist5, lblUserLTopAlbumPlaycount5},
                                          {lblUserLTopAlbum6, lblUserLTopAlbumArtist6, lblUserLTopAlbumPlaycount6},
                                          {lblUserLTopAlbum7, lblUserLTopAlbumArtist7, lblUserLTopAlbumPlaycount7},
                                          {lblUserLTopAlbum8, lblUserLTopAlbumArtist8, lblUserLTopAlbumPlaycount8},
                                          {lblUserLTopAlbum9, lblUserLTopAlbumArtist9, lblUserLTopAlbumPlaycount9},
                                          {lblUserLTopAlbum10, lblUserLTopAlbumArtist10, lblUserLTopAlbumPlaycount10},
                                          {lblUserLTopAlbum11, lblUserLTopAlbumArtist11, lblUserLTopAlbumPlaycount11},
                                          {lblUserLTopAlbum12, lblUserLTopAlbumArtist12, lblUserLTopAlbumPlaycount12},
                                          {lblUserLTopAlbum13, lblUserLTopAlbumArtist13, lblUserLTopAlbumPlaycount13},
                                          {lblUserLTopAlbum14, lblUserLTopAlbumArtist14, lblUserLTopAlbumPlaycount14},
                                          {lblUserLTopAlbum15, lblUserLTopAlbumArtist15, lblUserLTopAlbumPlaycount15},
                                          {lblUserLTopAlbum16, lblUserLTopAlbumArtist16, lblUserLTopAlbumPlaycount16},
                                          {lblUserLTopAlbum17, lblUserLTopAlbumArtist17, lblUserLTopAlbumPlaycount17},
                                          {lblUserLTopAlbum18, lblUserLTopAlbumArtist18, lblUserLTopAlbumPlaycount18},
                                          {lblUserLTopAlbum19, lblUserLTopAlbumArtist19, lblUserLTopAlbumPlaycount19},
                                          {lblUserLTopAlbum20, lblUserLTopAlbumArtist20, lblUserLTopAlbumPlaycount20}}

        Dim TopAlbumArt() As PictureBox = {picUserLTopAlbumArt1, picUserLTopAlbumArt2, picUserLTopAlbumArt3, picUserLTopAlbumArt4, picUserLTopAlbumArt5,
                                           picUserLTopAlbumArt6, picUserLTopAlbumArt7, picUserLTopAlbumArt8, picUserLTopAlbumArt9, picUserLTopAlbumArt10,
                                           picUserLTopAlbumArt11, picUserLTopAlbumArt12, picUserLTopAlbumArt13, picUserLTopAlbumArt14, picUserLTopAlbumArt15,
                                           picUserLTopAlbumArt16, picUserLTopAlbumArt17, picUserLTopAlbumArt18, picUserLTopAlbumArt19, picUserLTopAlbumArt20}

        ' report 1% progress
        progress += 1
        UpdateProgressChange()
#End Region

#Region "Tracks"
        ' dim stuff
        Dim TopTrackXML As String = CallAPI("user.getTopTracks", userlookup, "limit=20")
        Dim TopTrackNodes() As String = {"name", "artist/name", "playcount"}
        Dim TrackInfoXML As String
        Dim TrackInfoNodes() As String = {"title"}
        Dim numberholder As UInteger

        ' get track info for 20 tracks
        For count As Byte = 0 To 19
            ' get initial track data (title and artist) from toptracks
            ParseXML(TopTrackXML, "/lfm/toptracks/track", count, TopTrackNodes)

            ' get track info xml
            TrackInfoXML = CallAPI("track.getInfo", "", "track=" & TopTrackNodes(0).Replace(" ", "+"), "artist=" & TopTrackNodes(1).Replace(" ", "+"))

            ' get track album from trackinfoxml
            ParseXML(TrackInfoXML, "/lfm/track/album", 0, TrackInfoNodes)

            ' detect errors and set to "(Unavailable)"
            For counter2 As Byte = 0 To 2
                If TopTrackNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopTrackNodes(counter2) = "(Unavailable)"
                End If
            Next counter2
            If TrackInfoNodes(0).Contains("Object reference not set to an instance of an object") = True Then
                TrackInfoNodes(0) = "(Unavailable)"
            End If

            ' set labels
            Invoke(Sub() TopTrackLabels(count, 0).Text = TopTrackNodes(0))     ' title
            Invoke(Sub() TopTrackLabels(count, 1).Text = TopTrackNodes(1))     ' artist
            Invoke(Sub() TopTrackLabels(count, 2).Text = TrackInfoNodes(0))    ' album
            ' apply formatting to playcount
            UInteger.TryParse(TopTrackNodes(2), numberholder)
            Invoke(Sub() TopTrackLabels(count, 3).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopTrackArt(count).Load(ParseImage(TrackInfoXML, "/lfm/track/album/image", 1))
            Catch ex As Exception
                TopTrackArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopTrackNodes = {"name", "artist/name", "playcount"}
            TrackInfoNodes = {"title"}
        Next

        ' report 34% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Artists"
        ' dim stuff
        Dim TopArtistXML As String = CallAPI("user.getTopArtists", userlookup, "limit=20")
        Dim TopArtistNodes() As String = {"name", "playcount"}

        For count As Byte = 0 To 19
            ' get artist data (name and playcount)
            ParseXML(TopArtistXML, "/lfm/topartists/artist", count, TopArtistNodes)

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 1
                If TopArtistNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopArtistNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopArtistLabels(count, 0).Text = TopArtistNodes(0))     ' title
            ' apply formatting to playcount
            UInteger.TryParse(TopArtistNodes(1), numberholder)
            Invoke(Sub() TopArtistLabels(count, 1).Text = numberholder.ToString("N0"))

            ' reset node arrays
            TopArtistNodes = {"name", "playcount"}
        Next

        ' report 67% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Albums"
        ' dim stuff
        Dim TopAlbumXML As String = CallAPI("user.getTopAlbums", userlookup, "limit=20")
        Dim TopAlbumNodes() As String = {"name", "artist/name", "playcount"}

        For count As Byte = 0 To 19
            ' get album data (name, artist, playcount)
            ParseXML(TopAlbumXML, "/lfm/topalbums/album", count, TopAlbumNodes)

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 2
                If TopAlbumNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopAlbumNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopAlbumLabels(count, 0).Text = TopAlbumNodes(0))     ' album
            Invoke(Sub() TopAlbumLabels(count, 1).Text = TopAlbumNodes(1))     ' artist
            ' apply formatting to playcount
            UInteger.TryParse(TopAlbumNodes(2), numberholder)
            Invoke(Sub() TopAlbumLabels(count, 2).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopAlbumArt(count).Load(ParseImage(TopAlbumXML, "/lfm/topalbums/album", count, 6))
            Catch ex As Exception
                TopAlbumArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopAlbumNodes = {"name", "artist/name", "playcount"}
        Next

        ' report 100% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "History"
        'Invoke(Sub() nudUserLHistoryPage.Value = nudUserLHistoryPage.Minimum)
        'UserLHistoryUpdate()
#End Region

    End Sub

    Public Sub UserLChartsUpdate(ByVal unixfrom As UInteger, ByVal unixto As UInteger)
        progressmultiplier += 1

#Region "To>From Check"
        If unixfrom > unixto Then
            Invoke(Sub() MessageBox.Show("From date must be before to date", "User Lookup Charts", MessageBoxButtons.OK, MessageBoxIcon.Error))
            progress += 100
            UpdateProgressChange()
            Exit Sub
        End If
#End Region

#Region "Fatty Arrays"
        Dim TopTrackLabels(,) As Label = {{lblUserLTopTrackTitle1, lblUserLTopTrackArtist1, lblUserLTopTrackAlbum1, lblUserLTopTrackPlaycount1},
                                          {lblUserLTopTrackTitle2, lblUserLTopTrackArtist2, lblUserLTopTrackAlbum2, lblUserLTopTrackPlaycount2},
                                          {lblUserLTopTrackTitle3, lblUserLTopTrackArtist3, lblUserLTopTrackAlbum3, lblUserLTopTrackPlaycount3},
                                          {lblUserLTopTrackTitle4, lblUserLTopTrackArtist4, lblUserLTopTrackAlbum4, lblUserLTopTrackPlaycount4},
                                          {lblUserLTopTrackTitle5, lblUserLTopTrackArtist5, lblUserLTopTrackAlbum5, lblUserLTopTrackPlaycount5},
                                          {lblUserLTopTrackTitle6, lblUserLTopTrackArtist6, lblUserLTopTrackAlbum6, lblUserLTopTrackPlaycount6},
                                          {lblUserLTopTrackTitle7, lblUserLTopTrackArtist7, lblUserLTopTrackAlbum7, lblUserLTopTrackPlaycount7},
                                          {lblUserLTopTrackTitle8, lblUserLTopTrackArtist8, lblUserLTopTrackAlbum8, lblUserLTopTrackPlaycount8},
                                          {lblUserLTopTrackTitle9, lblUserLTopTrackArtist9, lblUserLTopTrackAlbum9, lblUserLTopTrackPlaycount9},
                                          {lblUserLTopTrackTitle10, lblUserLTopTrackArtist10, lblUserLTopTrackAlbum10, lblUserLTopTrackPlaycount10},
                                          {lblUserLTopTrackTitle11, lblUserLTopTrackArtist11, lblUserLTopTrackAlbum11, lblUserLTopTrackPlaycount11},
                                          {lblUserLTopTrackTitle12, lblUserLTopTrackArtist12, lblUserLTopTrackAlbum12, lblUserLTopTrackPlaycount12},
                                          {lblUserLTopTrackTitle13, lblUserLTopTrackArtist13, lblUserLTopTrackAlbum13, lblUserLTopTrackPlaycount13},
                                          {lblUserLTopTrackTitle14, lblUserLTopTrackArtist14, lblUserLTopTrackAlbum14, lblUserLTopTrackPlaycount14},
                                          {lblUserLTopTrackTitle15, lblUserLTopTrackArtist15, lblUserLTopTrackAlbum15, lblUserLTopTrackPlaycount15},
                                          {lblUserLTopTrackTitle16, lblUserLTopTrackArtist16, lblUserLTopTrackAlbum16, lblUserLTopTrackPlaycount16},
                                          {lblUserLTopTrackTitle17, lblUserLTopTrackArtist17, lblUserLTopTrackAlbum17, lblUserLTopTrackPlaycount17},
                                          {lblUserLTopTrackTitle18, lblUserLTopTrackArtist18, lblUserLTopTrackAlbum18, lblUserLTopTrackPlaycount18},
                                          {lblUserLTopTrackTitle19, lblUserLTopTrackArtist19, lblUserLTopTrackAlbum19, lblUserLTopTrackPlaycount19},
                                          {lblUserLTopTrackTitle20, lblUserLTopTrackArtist20, lblUserLTopTrackAlbum20, lblUserLTopTrackPlaycount20}}

        Dim TopTrackArt() As PictureBox = {picUserLTopTrackArt1, picUserLTopTrackArt2, picUserLTopTrackArt3, picUserLTopTrackArt4, picUserLTopTrackArt5,
                                           picUserLTopTrackArt6, picUserLTopTrackArt7, picUserLTopTrackArt8, picUserLTopTrackArt9, picUserLTopTrackArt10,
                                           picUserLTopTrackArt11, picUserLTopTrackArt12, picUserLTopTrackArt13, picUserLTopTrackArt14, picUserLTopTrackArt15,
                                           picUserLTopTrackArt16, picUserLTopTrackArt17, picUserLTopTrackArt18, picUserLTopTrackArt19, picUserLTopTrackArt20}

        Dim TopArtistLabels(,) As Label = {{lblUserLTopArtist1, lblUserLTopArtistPlaycount1},
                                          {lblUserLTopArtist2, lblUserLTopArtistPlaycount2},
                                          {lblUserLTopArtist3, lblUserLTopArtistPlaycount3},
                                          {lblUserLTopArtist4, lblUserLTopArtistPlaycount4},
                                          {lblUserLTopArtist5, lblUserLTopArtistPlaycount5},
                                          {lblUserLTopArtist6, lblUserLTopArtistPlaycount6},
                                          {lblUserLTopArtist7, lblUserLTopArtistPlaycount7},
                                          {lblUserLTopArtist8, lblUserLTopArtistPlaycount8},
                                          {lblUserLTopArtist9, lblUserLTopArtistPlaycount9},
                                          {lblUserLTopArtist10, lblUserLTopArtistPlaycount10},
                                          {lblUserLTopArtist11, lblUserLTopArtistPlaycount11},
                                          {lblUserLTopArtist12, lblUserLTopArtistPlaycount12},
                                          {lblUserLTopArtist13, lblUserLTopArtistPlaycount13},
                                          {lblUserLTopArtist14, lblUserLTopArtistPlaycount14},
                                          {lblUserLTopArtist15, lblUserLTopArtistPlaycount15},
                                          {lblUserLTopArtist16, lblUserLTopArtistPlaycount16},
                                          {lblUserLTopArtist17, lblUserLTopArtistPlaycount17},
                                          {lblUserLTopArtist18, lblUserLTopArtistPlaycount18},
                                          {lblUserLTopArtist19, lblUserLTopArtistPlaycount19},
                                          {lblUserLTopArtist20, lblUserLTopArtistPlaycount20}}

        Dim TopAlbumLabels(,) As Label = {{lblUserLTopAlbum1, lblUserLTopAlbumArtist1, lblUserLTopAlbumPlaycount1},
                                          {lblUserLTopAlbum2, lblUserLTopAlbumArtist2, lblUserLTopAlbumPlaycount2},
                                          {lblUserLTopAlbum3, lblUserLTopAlbumArtist3, lblUserLTopAlbumPlaycount3},
                                          {lblUserLTopAlbum4, lblUserLTopAlbumArtist4, lblUserLTopAlbumPlaycount4},
                                          {lblUserLTopAlbum5, lblUserLTopAlbumArtist5, lblUserLTopAlbumPlaycount5},
                                          {lblUserLTopAlbum6, lblUserLTopAlbumArtist6, lblUserLTopAlbumPlaycount6},
                                          {lblUserLTopAlbum7, lblUserLTopAlbumArtist7, lblUserLTopAlbumPlaycount7},
                                          {lblUserLTopAlbum8, lblUserLTopAlbumArtist8, lblUserLTopAlbumPlaycount8},
                                          {lblUserLTopAlbum9, lblUserLTopAlbumArtist9, lblUserLTopAlbumPlaycount9},
                                          {lblUserLTopAlbum10, lblUserLTopAlbumArtist10, lblUserLTopAlbumPlaycount10},
                                          {lblUserLTopAlbum11, lblUserLTopAlbumArtist11, lblUserLTopAlbumPlaycount11},
                                          {lblUserLTopAlbum12, lblUserLTopAlbumArtist12, lblUserLTopAlbumPlaycount12},
                                          {lblUserLTopAlbum13, lblUserLTopAlbumArtist13, lblUserLTopAlbumPlaycount13},
                                          {lblUserLTopAlbum14, lblUserLTopAlbumArtist14, lblUserLTopAlbumPlaycount14},
                                          {lblUserLTopAlbum15, lblUserLTopAlbumArtist15, lblUserLTopAlbumPlaycount15},
                                          {lblUserLTopAlbum16, lblUserLTopAlbumArtist16, lblUserLTopAlbumPlaycount16},
                                          {lblUserLTopAlbum17, lblUserLTopAlbumArtist17, lblUserLTopAlbumPlaycount17},
                                          {lblUserLTopAlbum18, lblUserLTopAlbumArtist18, lblUserLTopAlbumPlaycount18},
                                          {lblUserLTopAlbum19, lblUserLTopAlbumArtist19, lblUserLTopAlbumPlaycount19},
                                          {lblUserLTopAlbum20, lblUserLTopAlbumArtist20, lblUserLTopAlbumPlaycount20}}

        Dim TopAlbumArt() As PictureBox = {picUserLTopAlbumArt1, picUserLTopAlbumArt2, picUserLTopAlbumArt3, picUserLTopAlbumArt4, picUserLTopAlbumArt5,
                                           picUserLTopAlbumArt6, picUserLTopAlbumArt7, picUserLTopAlbumArt8, picUserLTopAlbumArt9, picUserLTopAlbumArt10,
                                           picUserLTopAlbumArt11, picUserLTopAlbumArt12, picUserLTopAlbumArt13, picUserLTopAlbumArt14, picUserLTopAlbumArt15,
                                           picUserLTopAlbumArt16, picUserLTopAlbumArt17, picUserLTopAlbumArt18, picUserLTopAlbumArt19, picUserLTopAlbumArt20}

        ' report 1% progress
        progress += 1
        UpdateProgressChange()
#End Region

#Region "Tracks"
        ' dim stuff
        Dim TopTrackXML As String = CallAPI("user.getWeeklyTrackChart", userlookup, "from=" & unixfrom.ToString, "to=" & unixto.ToString)
        Dim TopTrackNodes() As String = {"name", "artist", "playcount"}
        Dim TrackInfoXML As String
        Dim TrackInfoNodes() As String = {"title"}
        Dim numberholder As UInteger

        ' get track info for 20 tracks
        For count As Byte = 0 To 19
            ' get initial track data (title and artist) from toptracks
            ParseXML(TopTrackXML, "/lfm/weeklytrackchart/track", count, TopTrackNodes)

            ' get track info xml
            TrackInfoXML = CallAPI("track.getInfo", "", "track=" & TopTrackNodes(0).Replace(" ", "+"), "artist=" & TopTrackNodes(1).Replace(" ", "+"))

            ' get track album from trackinfoxml
            ParseXML(TrackInfoXML, "/lfm/track/album", 0, TrackInfoNodes)

            ' detect errors and set to "(Unavailable)"
            For counter2 As Byte = 0 To 2
                If TopTrackNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopTrackNodes(counter2) = "(Unavailable)"
                End If
            Next counter2
            If TrackInfoNodes(0).Contains("Object reference not set to an instance of an object") = True Then
                TrackInfoNodes(0) = "(Unavailable)"
            End If

            ' set labels
            Invoke(Sub() TopTrackLabels(count, 0).Text = TopTrackNodes(0))     ' title
            Invoke(Sub() TopTrackLabels(count, 1).Text = TopTrackNodes(1))     ' artist
            Invoke(Sub() TopTrackLabels(count, 2).Text = TrackInfoNodes(0))    ' album
            ' apply formatting to playcount
            UInteger.TryParse(TopTrackNodes(2), numberholder)
            Invoke(Sub() TopTrackLabels(count, 3).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopTrackArt(count).Load(ParseImage(TrackInfoXML, "/lfm/track/album/image", 1))
            Catch ex As Exception
                TopTrackArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopTrackNodes = {"name", "artist", "playcount"}
            TrackInfoNodes = {"title"}
        Next

        ' report 34% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Artists"
        ' dim stuff
        Dim TopArtistXML As String = CallAPI("user.getWeeklyArtistChart", userlookup, "from=" & unixfrom.ToString, "to=" & unixto.ToString)
        Dim TopArtistNodes() As String = {"name", "playcount"}

        For count As Byte = 0 To 19
            ' get artist data (name and playcount)
            ParseXML(TopArtistXML, "/lfm/weeklyartistchart/artist", count, TopArtistNodes)

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 1
                If TopArtistNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopArtistNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopArtistLabels(count, 0).Text = TopArtistNodes(0))     ' title
            ' apply formatting to playcount
            UInteger.TryParse(TopArtistNodes(1), numberholder)
            Invoke(Sub() TopArtistLabels(count, 1).Text = numberholder.ToString("N0"))

            ' reset node arrays
            TopArtistNodes = {"name", "playcount"}
        Next

        ' report 67% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "Albums"
        ' dim stuff
        Dim TopAlbumXML As String = CallAPI("user.getWeeklyAlbumChart", userlookup, "from=" & unixfrom.ToString, "to=" & unixto.ToString)
        Dim TopAlbumNodes() As String = {"name", "artist", "playcount"}
        Dim AlbumInfoXML As String

        For count As Byte = 0 To 19
            ' get album data (name, artist, playcount)
            ParseXML(TopAlbumXML, "/lfm/weeklyalbumchart/album", count, TopAlbumNodes)

            ' get album info for image
            AlbumInfoXML = CallAPI("album.getInfo", userlookup, "album=" & TopAlbumNodes(0).Replace(" ", "+"), "artist=" & TopAlbumNodes(1).Replace(" ", "+"))

            ' detect errors and set to unavailable
            For counter2 As Byte = 0 To 2
                If TopAlbumNodes(counter2).Contains("Object reference not set to an instance of an object") = True Then
                    TopAlbumNodes(counter2) = "(Unavailable)"
                End If
            Next counter2

            ' set labels
            Invoke(Sub() TopAlbumLabels(count, 0).Text = TopAlbumNodes(0))     ' album
            Invoke(Sub() TopAlbumLabels(count, 1).Text = TopAlbumNodes(1))     ' artist
            ' apply formatting to playcount
            UInteger.TryParse(TopAlbumNodes(2), numberholder)
            Invoke(Sub() TopAlbumLabels(count, 2).Text = numberholder.ToString("N0"))

            ' set picturebox
            Try
                TopAlbumArt(count).Load(ParseImage(AlbumInfoXML, "/lfm/album/image", 1))
            Catch ex As Exception
                TopAlbumArt(count).Image = My.Resources.imageunavailable
            End Try

            ' reset node arrays
            TopAlbumNodes = {"name", "artist", "playcount"}
        Next

        ' report 100% progress
        progress += 33
        UpdateProgressChange()
#End Region

#Region "History"
        Invoke(Sub() nudUserLHistoryPage.Value = nudUserLHistoryPage.Minimum)
        UserLHistoryUpdate()
#End Region

    End Sub

    Public Sub UserLHistoryUpdate()
        progressmultiplier += 1

        ' determine whether date is used or not
        Dim historyXML As String
        If radUserLAllTime.Checked = True Then
            historyXML = CallAPI("user.getRecentTracks", userlookup, "page=" & nudUserLHistoryPage.Value.ToString())
        Else
            historyXML = CallAPI("user.getRecentTracks", userlookup, "page=" & nudUserLHistoryPage.Value.ToString(), "from=" & DateToUnix(dtpUserLFrom.Value.Date) - timezoneoffset, "to=" & DateToUnix(dtpUserLTo.Value.Date) - timezoneoffset)
        End If

        ' determine whether something is now playing 
        Dim metadataOffset As Byte = 1
        If StrCount(historyXML, "</track>") > StrCount(historyXML, "date uts=") Then
            metadataOffset = 0
        End If

        ' report 20% progress
        progress += 20
        UpdateProgressChange()

        ' set labels
        Invoke(Sub() lblUserLHistoryTotalPages.Text = "Total Pages: " & ParseMetadata(historyXML, "totalPages="))  ' set total pages label
        Invoke(Sub() lblUserLHistoryTotalTracks.Text = "Total Tracks: " & ParseMetadata(historyXML, "total="))     ' set total tracks label
        Invoke(Sub() ltvUserLHistory.Items.Clear())

        ' report 30% progress
        progress += 10
        UpdateProgressChange()

        ' set max numeric box value
        Dim totalpages As UShort
        UShort.TryParse(ParseMetadata(historyXML, "totalPages="), totalpages)
        Invoke(Sub() nudUserLHistoryPage.Maximum = totalpages)

        ' report 40% progress
        progress += 10
        UpdateProgressChange()

        ' parse xml and add to list
        Dim historynodes() As String = {"name", "artist", "album", "date"}
        For count As Byte = 0 To 50
            ' parse xml
            ParseXML(historyXML, "/lfm/recenttracks/track", count, historynodes)

            ' add items if no error
            Dim counterrors As Integer = 0
            If historynodes(0).Contains("ERROR:") = False Then
                Invoke(Sub() ltvUserLHistory.Items.Add(historynodes(0)))
                Invoke(Sub() ltvUserLHistory.Items(count - counterrors).SubItems.Add(historynodes(1)))
                Invoke(Sub() ltvUserLHistory.Items(count - counterrors).SubItems.Add(historynodes(2)))
                ' check for date error due to now playing
                If historynodes(3).Contains("ERROR: ") = False Then
                    Invoke(Sub() ltvUserLHistory.Items(count).SubItems.Add(UnixToDate(CUInt(ParseMetadata(historyXML, "date uts=", (count - counterrors + metadataOffset)) + timezoneoffset)).ToString("G")))
                Else
                    Invoke(Sub() ltvUserLHistory.Items(count).SubItems.Add("Now Playing"))
                End If
            Else
                counterrors += 1
            End If
            ' reset nodes
            historynodes = {"name", "artist", "album", "date"}

            ' report 40-91% progress
            progress += 1
            UpdateProgressChange()
        Next

        ' set numeric box value back to current page
        Dim pagenum As UShort
        Dim metadata As String = ParseMetadata(historyXML, "page=")
        UShort.TryParse(metadata, pagenum)
        If nudUserLHistoryPage.Maximum > 0 AndAlso nudUserLHistoryPage.Minimum > 0 Then
            ' check if pagenum is higher than maximum
            If pagenum <= nudUserLHistoryPage.Maximum Then
                Invoke(Sub() nudUserLHistoryPage.Value = pagenum)
            Else
                Invoke(Sub() nudUserLHistoryPage.Value = nudUserLHistoryPage.Maximum)
            End If
        Else
            Invoke(Sub() nudUserLHistoryPage.Minimum = 0)
            Invoke(Sub() nudUserLHistoryPage.Value = 0)
        End If

        ' report 100% progress
        progress += 9
        UpdateProgressChange()
    End Sub
#End Region

#Region "Other Update"
    Public Sub LastPlayedUpdate()
        ' get recent track
        Dim recenttracknodes() As String = {"artist", "name"}
        Dim lastplayedxml As String = CallAPI("user.getRecentTracks", My.Settings.User, "limit=1")
        ParseXML(lastplayedxml, "/lfm/recenttracks/track", 0, recenttracknodes)

        ' determine whether track is now playing or last played
        Dim playingstatus As String
        If lastplayedxml.Contains("nowplaying=" & Chr(34) & "true" & Chr(34)) = True Then
            playingstatus = "Now Playing: "
        Else
            playingstatus = "Last Played: "
        End If

        ' if artist and title are both errors then dont display anything
        If recenttracknodes(0).Contains("ERROR:") = True AndAlso recenttracknodes(1).Contains("ERROR:") = True Then
            lblLastPlayed.Text = "Last Played: N/A"
        Else
            ' check if artist has an error, set to (Unavailable)
            If recenttracknodes(0).Contains("ERROR:") = True Then
                recenttracknodes(0) = "(Unavailable)"
            ElseIf recenttracknodes(1).Contains("ERROR:") = True Then
                recenttracknodes(1) = "(Unavailable)"
            End If
            Invoke(Sub() lblLastPlayed.Text = playingstatus & recenttracknodes(0).Replace("&", "&&") & " - " & recenttracknodes(1).Replace("&", "&&"))
        End If

        ' hide label if error
        If lblLastPlayed.Text = "Last Played: (Unavailable) - name" Then
            lblLastPlayed.Visible = False
            separator1.Visible = False
        Else
            lblLastPlayed.Visible = True
            separator1.Visible = True
        End If

        ' set tags for label
        Invoke(Sub() lblLastPlayed.Tag = recenttracknodes(0) & vbCrLf & recenttracknodes(1))
    End Sub

    Public Sub UpdateProgressChange()
        ' update progress bar
        Dim progressvalue As Single
        Try     ' error catching for possible overflow
            progressvalue = progress / progressmultiplier
        Catch ex As Exception
            Invoke(Sub() MessageBox.Show("UpdateProgressChange has encountered an error. Message: " & ex.Message, "Audiograph Error", MessageBoxButtons.OK, MessageBoxIcon.Error))
            Exit Sub
        End Try
        If progressvalue >= 100 Then
            progressmultiplier = 0
            progress = 0
            Invoke(Sub() Cursor = Cursors.Default)
            Invoke(Sub() UpdateProgress.Visible = False)
            Invoke(Sub() UpdateProgress.Value = 100)

        Else
            Invoke(Sub() Cursor = Cursors.AppStarting)
            Invoke(Sub() UpdateProgress.Visible = True)
            Invoke(Sub() UpdateProgress.Value = Math.Floor(progressvalue))
        End If
    End Sub

    ' update all tabs
    Public Sub UpdateAll()
        ' resetprog
        progress = 0
        progressmultiplier = 0
        UpdateProgress.Value = 100
        UpdateProgress.Visible = False

        If bgwChartUpdater.IsBusy = False Then
            bgwChartUpdater.RunWorkerAsync()
        End If
        If bgwTrackUpdater.IsBusy = False Then
            bgwTrackUpdater.RunWorkerAsync()
        End If
        If bgwArtistUpdater.IsBusy = False Then
            bgwArtistUpdater.RunWorkerAsync()
        End If
        If bgwAlbumUpdater.IsBusy = False Then
            bgwAlbumUpdater.RunWorkerAsync()
        End If
        If bgwSearchUpdater.IsBusy = False Then
            bgwSearchUpdater.RunWorkerAsync()
        End If
        If bgwUserUpdater.IsBusy = False Then
            bgwUserUpdater.RunWorkerAsync()
        End If
        If bgwUserLookupUpdater.IsBusy = False Then
            bgwUserLookupUpdater.RunWorkerAsync()
        End If
        LastPlayedUpdate()
    End Sub

    ' update current tab
    Private Sub UpdateCurrentTab(sender As Object, e As EventArgs) Handles mnuUpdateCurrent.Click
        ' resetprog
        progress = 0
        progressmultiplier = 0
        UpdateProgress.Value = 100
        UpdateProgress.Visible = False

        Select Case tabControl.SelectedIndex
            Case 0
                If bgwChartUpdater.IsBusy = False Then
                    bgwChartUpdater.RunWorkerAsync()
                End If
            Case 1
                If bgwTrackUpdater.IsBusy = False Then
                    bgwTrackUpdater.RunWorkerAsync()
                End If
            Case 2
                If bgwArtistUpdater.IsBusy = False Then
                    bgwArtistUpdater.RunWorkerAsync()
                End If
            Case 3
                If bgwAlbumUpdater.IsBusy = False Then
                    bgwAlbumUpdater.RunWorkerAsync()
                End If
            Case 4
                If bgwSearchUpdater.IsBusy = False Then
                    bgwSearchUpdater.RunWorkerAsync()
                End If
            Case 5
                If bgwUserUpdater.IsBusy = False Then
                    bgwUserUpdater.RunWorkerAsync()
                End If
            Case 6
                If bgwUserLookupUpdater.IsBusy = False Then
                    bgwUserLookupUpdater.RunWorkerAsync()
                End If
        End Select
        LastPlayedUpdate()
    End Sub

    Private Sub AutoRefresh(sender As Object, e As EventArgs) Handles tmrAutoRefresh.Tick
        UpdateAll()
    End Sub
#End Region

#Region "Update Threads"
    Private Sub ChartThread(sender As Object, e As DoWorkEventArgs) Handles bgwChartUpdater.DoWork
        Thread.CurrentThread.Name = "Chart"
        UpdateCharts()
    End Sub

    Private Sub SearchThread(sender As Object, e As DoWorkEventArgs) Handles bgwSearchUpdater.DoWork
        Thread.CurrentThread.Name = "Search"
        UpdateSearch()
    End Sub

    Private Sub TrackThread(sender As Object, e As DoWorkEventArgs) Handles bgwTrackUpdater.DoWork
        Thread.CurrentThread.Name = "Track"
        UpdateTrack()
    End Sub

    Private Sub ArtistThread(sender As Object, e As DoWorkEventArgs) Handles bgwArtistUpdater.DoWork
        Thread.CurrentThread.Name = "Artist"
        UpdateArtist()
    End Sub

    Private Sub AlbumThread(sender As Object, e As DoWorkEventArgs) Handles bgwAlbumUpdater.DoWork
        Thread.CurrentThread.Name = "Album"
        UpdateAlbum()
    End Sub

    Private Sub UserThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserUpdater.DoWork
        Thread.CurrentThread.Name = "User"
        If My.Settings.User <> String.Empty Then
            ' start other threads
            If bgwUserChartUpdater.IsBusy = False Then
                bgwUserChartUpdater.RunWorkerAsync()
            End If
            If bgwUserHistoryUpdater.IsBusy = False Then
                bgwUserHistoryUpdater.RunWorkerAsync()
            End If
            If bgwUserLovedUpdater.IsBusy = False Then
                bgwUserLovedUpdater.RunWorkerAsync()
            End If
            UpdateUser()
        End If
    End Sub

    Private Sub UserLovedThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserLovedUpdater.DoWork
        Thread.CurrentThread.Name = "UserLoved"
        If My.Settings.User <> String.Empty Then
            UserLovedTracksUpdate(nudUserLovedPage.Value)
        End If
    End Sub

    Private Sub UserChartThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserChartUpdater.DoWork
        Thread.CurrentThread.Name = "UserChart"
        If My.Settings.User <> String.Empty Then
            If radUserAllTime.Checked = True Then
                UserChartsUpdate()
            Else
                UserChartsUpdate(DateToUnix(dtpUserFrom.Value), DateToUnix(dtpUserTo.Value))
            End If
        End If
    End Sub

    Private Sub UserHistoryThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserHistoryUpdater.DoWork
        Thread.CurrentThread.Name = "UserHistory"
        If My.Settings.User <> String.Empty Then
            UserHistoryUpdate()
        End If
    End Sub

    Private Sub UserLThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserLookupUpdater.DoWork
        Thread.CurrentThread.Name = "UserLookup"
        If userlookup <> String.Empty Then
            LastPlayedUpdate()
            If bgwUserLChartUpdater.IsBusy = False Then
                bgwUserLChartUpdater.RunWorkerAsync()
            End If
            If bgwUserLHistoryUpdater.IsBusy = False Then
                bgwUserLHistoryUpdater.RunWorkerAsync()
            End If
            If bgwUserLLovedUpdater.IsBusy = False Then
                bgwUserLLovedUpdater.RunWorkerAsync()
            End If
            UpdateUserL()
        End If
    End Sub

    Private Sub UserLLovedThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserLLovedUpdater.DoWork
        Thread.CurrentThread.Name = "UserLLoved"
        If userlookup <> String.Empty Then
            UserLLovedTracksUpdate(nudUserLLovedPage.Value)
        End If
    End Sub

    Private Sub UserLChartThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserLChartUpdater.DoWork
        Thread.CurrentThread.Name = "UserLChart"
        If userlookup <> String.Empty Then
            If radUserLAllTime.Checked = True Then
                UserLChartsUpdate()
            Else
                UserLChartsUpdate(DateToUnix(dtpUserLFrom.Value), DateToUnix(dtpUserLTo.Value))
            End If
        End If
    End Sub

    Private Sub UserLHistoryThread(sender As Object, e As DoWorkEventArgs) Handles bgwUserLHistoryUpdater.DoWork
        Thread.CurrentThread.Name = "UserLHistory"
        If userlookup <> String.Empty Then
            UserLHistoryUpdate()
        End If
    End Sub
#End Region

#Region "General UI"
    Private Sub CustomScaling(sender As Object, e As EventArgs) Handles Me.Resize, tabControl.SelectedIndexChanged, tbcUserCharts.SelectedIndexChanged,
            tbcUserLCharts.SelectedIndexChanged
        Static statecontrol(6) As Byte   ' ensures operations arent needlessly run multiple times to reduce lag

        ' tabcontrol
        tabControl.Height = Me.Height - 72
        tabControl.Width = Me.Width - 24

#Region "Chart"
        ' chart tlp scaling
        tlpCharts.Width = pgCharts.Width - 6
        tlpCharts.Height = pgCharts.Height - 35

        ' user tlp scaling
        tlpUser.Width = pgUser.Width - 6
        tlpUser.Height = pgUser.Height - 35
#End Region

#Region "Search"
        tlpSearch.Width = pgSearch.Width - 6
        tlpSearch.Height = pgSearch.Height - 35
#End Region

#Region "Track"
        ' track tlp
        tlpTrack.Width = pgTrack.Width - 6
        tlpTrack.Height = pgTrack.Height - 35

        ' left tlp
        If picTrackArt.Width >= 25 Then
            spcTrack.SplitterDistance = picTrackArt.Width
        End If

        ' add/remove buttons
        If Me.Width < 1110 Then
            If statecontrol(0) <> 1 Then   ' perf
                btnTrackTagAdd.Width = 22
                btnTrackTagRemove.Width = 22
                btnTrackTagAdd.Text = "+"
                btnTrackTagRemove.Text = "-"
                btnTrackTagRemove.Left = 62
                statecontrol(0) = 1
            End If
        Else
            If statecontrol(0) <> 2 Then   ' perf
                btnTrackTagAdd.Width = 62
                btnTrackTagRemove.Width = 62
                btnTrackTagAdd.Text = "Add"
                btnTrackTagRemove.Text = "Remove"
                btnTrackTagRemove.Left = 102
                statecontrol(0) = 2
            End If
        End If

        ' top bar
        If Me.Width <= 737 AndAlso Me.Width > 694 Then
            If statecontrol(1) <> 1 Then
                btnTrackGo.Width = 41
                btnTrackAdvanced.Width = 68
                btnTrackAdvanced.Left = 424
                statecontrol(1) = 1
            End If
        ElseIf Me.Width <= 694 Then
            If statecontrol(1) <> 2 Then
                txtTrackTitle.Width = 125
                txtTrackArtist.Width = 125
                txtTrackArtist.Left = 186
                lblTrackArtist.Left = 156
                btnTrackGo.Width = 41
                btnTrackGo.Left = 317
                btnTrackAdvanced.Width = 68
                btnTrackAdvanced.Left = 364
                statecontrol(1) = 2
            End If
        Else
            If statecontrol(1) <> 3 Then
                txtTrackArtist.Left = 216
                txtTrackTitle.Width = 155
                txtTrackArtist.Width = 155
                btnTrackGo.Width = 75
                btnTrackGo.Left = 377
                btnTrackAdvanced.Width = 75
                btnTrackAdvanced.Left = 458
                lblTrackArtist.Left = 186
                statecontrol(1) = 3
            End If
        End If

        ' track spc2
        If Me.Width <= 1917 AndAlso Me.Width > 1153 Then
            If spcTrack2.SplitterDistance <> 26 Then    ' perf
                spcTrack2.SplitterDistance = 26
            End If
        ElseIf Me.Width <= 1153 AndAlso Me.Width > 769 Then
            If spcTrack2.SplitterDistance <> 40 Then    ' perf
                spcTrack2.SplitterDistance = 40
            End If
        ElseIf Me.Width <= 769 AndAlso Me.Width > 682 Then
            If spcTrack2.SplitterDistance <> 54 Then    ' perf
                spcTrack2.SplitterDistance = 54
            End If
        ElseIf Me.Width <= 682 Then
            If spcTrack2.SplitterDistance <> 66 Then    ' perf
                spcTrack2.SplitterDistance = 66
            End If
        Else
            If spcTrack2.SplitterDistance <> 14 Then    ' perf
                spcTrack2.SplitterDistance = 14
            End If
        End If
#End Region

#Region "Artist"
        ' artist tlp
        tlpArtist.Width = pgArtist.Width - 6
        tlpArtist.Height = pgArtist.Height - 35

        ' left tlp
        If picArtistArt.Width >= 25 Then
            spcArtist.SplitterDistance = picArtistArt.Width
        End If

        ' user tag listview
        lstArtistUserTags.Height = gpbArtistUser.Height - 80
        lstArtistUserTags.Width = gpbArtistUser.Width - 12

        ' add/remove buttons
        If Me.Width < 1110 Then
            If statecontrol(5) <> 1 Then   ' perf
                btnArtistTagAdd.Width = 22
                btnArtistTagRemove.Width = 22
                btnArtistTagAdd.Text = "+"
                btnArtistTagRemove.Text = "-"
                btnArtistTagRemove.Left = 62
                statecontrol(5) = 1
            End If
        Else
            If statecontrol(5) <> 2 Then   ' perf
                btnArtistTagAdd.Width = 62
                btnArtistTagRemove.Width = 62
                btnArtistTagAdd.Text = "Add"
                btnArtistTagRemove.Text = "Remove"
                btnArtistTagRemove.Left = 102
                statecontrol(5) = 2
            End If
        End If

        ' artist spc2
        If Me.Width <= 1917 AndAlso Me.Width > 1153 Then
            If spcArtist2.SplitterDistance <> 26 Then    ' perf
                spcArtist2.SplitterDistance = 26
            End If
        ElseIf Me.Width <= 1153 AndAlso Me.Width > 769 Then
            If spcArtist2.SplitterDistance <> 40 Then    ' perf
                spcArtist2.SplitterDistance = 40
            End If
        ElseIf Me.Width <= 769 AndAlso Me.Width > 682 Then
            If spcArtist2.SplitterDistance <> 54 Then    ' perf
                spcArtist2.SplitterDistance = 54
            End If
        ElseIf Me.Width <= 682 Then
            If spcArtist2.SplitterDistance <> 66 Then    ' perf
                spcArtist2.SplitterDistance = 66
            End If
        Else
            If spcArtist2.SplitterDistance <> 14 Then    ' perf
                spcArtist2.SplitterDistance = 14
            End If
        End If
#End Region

#Region "Album"
        ' album tlp
        tlpAlbum.Width = pgAlbum.Width - 6
        tlpAlbum.Height = pgAlbum.Height - 35

        ' left tlp
        If picAlbumArt.Width >= 25 Then
            spcAlbum.SplitterDistance = picAlbumArt.Width
        End If

        ' user tag listview
        lstAlbumUserTags.Height = gpbAlbumUser.Height - 80
        lstAlbumUserTags.Width = gpbAlbumUser.Width - 12

        ' add/remove buttons
        If Me.Width < 1110 Then
            If statecontrol(4) <> 1 Then   ' perf
                btnAlbumTagAdd.Width = 22
                btnAlbumTagRemove.Width = 22
                btnAlbumTagAdd.Text = "+"
                btnAlbumTagRemove.Text = "-"
                btnAlbumTagRemove.Left = 62
                statecontrol(4) = 1
            End If
        Else
            If statecontrol(4) <> 2 Then   ' perf
                btnAlbumTagAdd.Width = 62
                btnAlbumTagRemove.Width = 62
                btnAlbumTagAdd.Text = "Add"
                btnAlbumTagRemove.Text = "Remove"
                btnAlbumTagRemove.Left = 102
                statecontrol(4) = 2
            End If
        End If

        ' Album spc2
        If Me.Width <= 1917 AndAlso Me.Width > 1153 Then
            If spcAlbum2.SplitterDistance <> 26 Then    ' perf
                spcAlbum2.SplitterDistance = 26
            End If
        ElseIf Me.Width <= 1153 AndAlso Me.Width > 769 Then
            If spcAlbum2.SplitterDistance <> 40 Then    ' perf
                spcAlbum2.SplitterDistance = 40
            End If
        ElseIf Me.Width <= 769 AndAlso Me.Width > 682 Then
            If spcAlbum2.SplitterDistance <> 54 Then    ' perf
                spcAlbum2.SplitterDistance = 54
            End If
        ElseIf Me.Width <= 682 Then
            If spcAlbum2.SplitterDistance <> 66 Then    ' perf
                spcAlbum2.SplitterDistance = 66
            End If
        Else
            If spcAlbum2.SplitterDistance <> 14 Then    ' perf
                spcAlbum2.SplitterDistance = 14
            End If
        End If
#End Region

#Region "User"
        ' user tlp
        tlpUser.Width = pgUser.Width - 6
        tlpUser.Height = pgUser.Height - 35

        ' user chart
        tbcUserCharts.Width = gpbUserCharts.Width - 6
        If Me.Width > 940 Then
            tbcUserCharts.Height = gpbUserCharts.Height - 50
            If statecontrol(2) <> 1 Then
                tbcUserCharts.Location = New Point(3, 45)
                btnUserChartGo.Location = New Point(386, 18)
                dtpUserTo.Location = New Point(283, 19)
                lblUserTo.Location = New Point(263, 22)
                statecontrol(2) = 1
            End If
        ElseIf Me.Width <= 940 AndAlso Me.Width > 815 Then
            tbcUserCharts.Height = gpbUserCharts.Height - 75
            If statecontrol(2) <> 2 Then
                tbcUserCharts.Location = New Point(3, 70)
                btnUserChartGo.Location = New Point(7, 43)
                dtpUserTo.Location = New Point(283, 19)
                lblUserTo.Location = New Point(263, 22)
                statecontrol(2) = 2
            End If
        ElseIf Me.Width <= 815 Then
            tbcUserCharts.Height = gpbUserCharts.Height - 75
            If statecontrol(2) <> 3 Then
                tbcUserCharts.Location = New Point(3, 70)
                btnUserChartGo.Location = New Point(129, 43)
                dtpUserTo.Location = New Point(26, 44)
                lblUserTo.Location = New Point(6, 47)
                statecontrol(2) = 3
            End If
        End If
        ltvUserHistory.Width = pgUserHistory.Width - 7
        ltvUserHistory.Height = pgUserHistory.Height - 35

        ' user friends
        ltvUserFriends.Width = pgUserFriends.Width - 6
        ltvUserFriends.Height = pgUserFriends.Height - 35
#End Region

#Region "User Lookup"
        ' user loved tracks
        ltvUserLovedTracks.Width = pgUserLovedTracks.Width - 6
        ltvUserLovedTracks.Height = pgUserLovedTracks.Height - 35

        ' user lookup tlp
        tlpUserL.Width = pgUserLookup.Width - 6
        tlpUserL.Height = pgUserLookup.Height - 35

        ' user lookup chart
        tbcUserLCharts.Width = gpbUserLCharts.Width - 6
        If Me.Width > 940 Then
            tbcUserLCharts.Height = gpbUserLCharts.Height - 50
            If statecontrol(3) <> 1 Then
                tbcUserLCharts.Location = New Point(3, 45)
                btnUserLChartGo.Location = New Point(386, 18)
                dtpUserLTo.Location = New Point(283, 19)
                lblUserLTo.Location = New Point(263, 22)
                statecontrol(3) = 1
            End If
        ElseIf Me.Width <= 940 AndAlso Me.Width > 815 Then
            tbcUserLCharts.Height = gpbUserLCharts.Height - 75
            If statecontrol(3) <> 2 Then
                tbcUserLCharts.Location = New Point(3, 70)
                btnUserLChartGo.Location = New Point(7, 43)
                dtpUserLTo.Location = New Point(283, 19)
                lblUserLTo.Location = New Point(263, 22)
                statecontrol(3) = 2
            End If
        ElseIf Me.Width <= 815 Then
            tbcUserLCharts.Height = gpbUserLCharts.Height - 75
            If statecontrol(3) <> 3 Then
                tbcUserLCharts.Location = New Point(3, 70)
                btnUserLChartGo.Location = New Point(129, 43)
                dtpUserLTo.Location = New Point(26, 44)
                lblUserLTo.Location = New Point(6, 47)
                statecontrol(3) = 3
            End If
        End If
        ltvUserLHistory.Width = pgUserLHistory.Width - 7
        ltvUserLHistory.Height = pgUserLHistory.Height - 35

        ' user lookup friends
        ltvUserLFriends.Width = pgUserLFriends.Width - 6
        ltvUserLFriends.Height = pgUserLFriends.Height - 35

        ' user lookup loved tracks
        ltvUserLLovedTracks.Width = pgUserLLovedTracks.Width - 6
        ltvUserLLovedTracks.Height = pgUserLLovedTracks.Height - 35
#End Region

#Region "Media"
        ' scrobble textboxes
        ' title
        If Me.Width > 940 Then
            txtMediaTitle.Width = pnlMediaScrobble.Width - 37
        End If
        ' artist
        If Me.Width > 940 Then
            txtMediaArtist.Width = pnlMediaScrobble.Width - 40
        End If
        ' album
        If Me.Width > 940 Then
            txtMediaAlbum.Width = pnlMediaScrobble.Width - 42
        End If
        ' time
        If Me.Width > 940 Then
            cmbMediaTime.Left = pnlMediaScrobble.Width - 59
            nudMediaMinute.Left = cmbMediaTime.Left - 42
            lblMediaDivider.Left = nudMediaMinute.Left - 8
            nudMediaHour.Left = lblMediaDivider.Left - 31
            dtpMediaScrobble.Width = pnlMediaScrobble.Width - (pnlMediaScrobble.Width - nudMediaHour.Left) - 42
        End If

        ' history listview
        ' height
        If pnlMediaScrobble.Height > 450 Then
            ltvMediaHistory.Height = pnlMediaScrobble.Height - 338
        End If
        ' width
        If Me.Width > 940 Then
            ltvMediaHistory.Width = pnlMediaScrobble.Width
        End If
#End Region

#Region "Easter Egg"
        If Me.Size = Me.MinimumSize Then
            Dim rng As New Random
            Dim random As Byte = rng.Next(0, 99)
            If random = 69 Then
                Me.Text = Me.Text.Remove(0, 10)
                Me.Text = Me.Text.Insert(0, "I HATE YOU")
            End If
        End If
#End Region

    End Sub

    Private Sub FormLoad(sender As Object, e As EventArgs) Handles Me.Load
        Thread.CurrentThread.Name = "Main"

        ' set form title
        If My.Settings.User = String.Empty Then
            lblUser.Text = "Current User: N/A (Please Set User)"
            Me.Text = "Audiograph"
        Else
            lblUser.Text = "Current User: " & My.Settings.User
            Me.Text = "Audiograph - " & My.Settings.User
        End If

        ' remove session key if no user
        If My.Settings.User = String.Empty Then
            My.Settings.SessionKey = String.Empty
            My.Settings.Save()
        End If

        ' enable or disable authentication ui
        If My.Settings.SessionKey <> String.Empty AndAlso My.Settings.User <> String.Empty Then
            AuthenticatedUI(True)
        Else
            AuthenticatedUI(False)
        End If

        ' set timezone offset
        Dim offsetspan As New TimeSpan
        Dim timezone As TimeZone = TimeZone.CurrentTimeZone
        offsetspan = timezone.GetUtcOffset(Now)
        timezoneoffset = offsetspan.TotalSeconds

        ' scrobble index
        If My.Settings.CurrentScrobbleIndex <> String.Empty Then
            LoadScrobbleIndex(My.Settings.CurrentScrobbleIndex)
        Else
            radMediaEnable.Enabled = False
            radMediaDisable.Enabled = False
            radMediaDisable.Checked = True
            btnMediaEditIndex.Enabled = False
        End If

        ' set scrobble time
        cmbMediaTime.SelectedIndex = 0

        ' set auto refresh time
        Select Case My.Settings.AutoRefresh
            Case 1
                mnuAuto1Min.PerformClick()
            Case 2
                mnuAuto2Min.PerformClick()
            Case 3
                mnuAuto3Min.PerformClick()
            Case 4
                mnuAuto4Min.PerformClick()
            Case 5
                mnuAuto5Min.PerformClick()
            Case 10
                mnuAuto10Min.PerformClick()
            Case Else
                mnuAutoOff.PerformClick()
        End Select

        ' update all
        UpdateAll()

        ' turn off load execution prevention
        stoploadexecution = False
    End Sub

    Private Sub LayoutSuspend(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        tlpCharts.SuspendLayout()
    End Sub

    Private Sub LayoutResume(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        tlpCharts.ResumeLayout()
    End Sub

    ' reset user text box and user status label when opening user tab
    Private Sub TabChanged(sender As Object, e As EventArgs) Handles tabControl.SelectedIndexChanged
        If tabControl.SelectedIndex = 5 Then
            txtUser.Text = My.Settings.User
            My.Settings.Save()
            If My.Settings.User <> String.Empty Then
                If My.Settings.SessionKey = String.Empty Then
                    lblUserStatus.Text = "Welcome, " & My.Settings.User
                Else
                    lblUserStatus.Text = "Welcome, " & My.Settings.User & " (Authenticated)"
                End If
                Me.Text = "Audiograph - " & My.Settings.User
            Else
                lblUserStatus.Text = "User Not Set"
            End If
        End If
    End Sub

    ' opens clicked image in browser
    Private Sub ArtClicked(sender As PictureBox, e As MouseEventArgs) Handles picTrack1.MouseDown, picTrack2.MouseDown, picTrack3.MouseDown, picTrack4.MouseDown, picTrack5.MouseDown,
            picTrack6.MouseDown, picTrack7.MouseDown, picTrack8.MouseDown, picTrack9.MouseDown, picTrack10.MouseDown, picTrack11.MouseDown, picTrack12.MouseDown, picTrack13.MouseDown,
            picTrack14.MouseDown, picTrack15.MouseDown, picTrack16.MouseDown, picTrack17.MouseDown, picTrack18.MouseDown, picTrack19.MouseDown, picTrack20.MouseDown,
            picUserRecentArt1.MouseDown, picUserRecentArt2.MouseDown, picUserRecentArt3.MouseDown, picUserRecentArt4.MouseDown, picUserRecentArt5.MouseDown, picUserRecentArt6.MouseDown,
            picUserRecentArt7.MouseDown, picUserRecentArt8.MouseDown, picUserRecentArt9.MouseDown, picUserRecentArt10.MouseDown, picUserRecentArt11.MouseDown, picUserRecentArt12.MouseDown,
            picUserRecentArt15.MouseDown, picUserRecentArt16.MouseDown, picUserRecentArt13.MouseDown, picUserRecentArt14.MouseDown, picUserRecentArt17.MouseDown, picUserRecentArt18.MouseDown,
            picUserRecentArt19.MouseDown, picUserRecentArt20.MouseDown, picUserTopTrackArt1.MouseDown, picUserTopTrackArt2.MouseDown, picUserTopTrackArt3.MouseDown, picUserTopTrackArt4.MouseDown,
            picUserTopTrackArt5.MouseDown, picUserTopTrackArt6.MouseDown, picUserTopTrackArt7.MouseDown, picUserTopTrackArt8.MouseDown, picUserTopTrackArt9.MouseDown, picUserTopTrackArt10.MouseDown,
            picUserTopTrackArt11.MouseDown, picUserTopTrackArt12.MouseDown, picUserTopTrackArt13.MouseDown, picUserTopTrackArt14.MouseDown, picUserTopTrackArt15.MouseDown,
            picUserTopTrackArt16.MouseDown, picUserTopTrackArt17.MouseDown, picUserTopTrackArt18.MouseDown, picUserTopTrackArt19.MouseDown, picUserTopTrackArt20.MouseDown,
            picUserTopAlbumArt1.MouseDown, picUserTopAlbumArt2.MouseDown, picUserTopAlbumArt3.MouseDown, picUserTopAlbumArt4.MouseDown, picUserTopAlbumArt5.MouseDown, picUserTopAlbumArt6.MouseDown,
            picUserTopAlbumArt7.MouseDown, picUserTopAlbumArt8.MouseDown, picUserTopAlbumArt9.MouseDown, picUserTopAlbumArt10.MouseDown, picUserTopAlbumArt11.MouseDown, picUserTopAlbumArt12.MouseDown,
            picUserTopAlbumArt13.MouseDown, picUserTopAlbumArt14.MouseDown, picUserTopAlbumArt15.MouseDown, picUserTopAlbumArt16.MouseDown, picUserTopAlbumArt17.MouseDown, picUserTopAlbumArt18.MouseDown,
            picUserTopAlbumArt19.MouseDown, picUserTopAlbumArt20.MouseDown, picUserRecentArt1.MouseDown, picUserRecentArt2.MouseDown, picUserRecentArt3.MouseDown, picUserRecentArt4.MouseDown, picUserRecentArt5.MouseDown, picUserRecentArt6.MouseDown,
            picUserRecentArt7.MouseDown, picUserRecentArt8.MouseDown, picUserLRecentArt9.MouseDown, picUserLRecentArt10.MouseDown, picUserLRecentArt11.MouseDown, picUserLRecentArt12.MouseDown,
            picUserLRecentArt15.MouseDown, picUserLRecentArt16.MouseDown, picUserLRecentArt13.MouseDown, picUserLRecentArt14.MouseDown, picUserLRecentArt17.MouseDown, picUserLRecentArt18.MouseDown,
            picUserLRecentArt19.MouseDown, picUserLRecentArt20.MouseDown, picUserLTopTrackArt1.MouseDown, picUserLTopTrackArt2.MouseDown, picUserLTopTrackArt3.MouseDown, picUserLTopTrackArt4.MouseDown,
            picUserLTopTrackArt5.MouseDown, picUserLTopTrackArt6.MouseDown, picUserLTopTrackArt7.MouseDown, picUserLTopTrackArt8.MouseDown, picUserLTopTrackArt9.MouseDown, picUserLTopTrackArt10.MouseDown,
            picUserLTopTrackArt11.MouseDown, picUserLTopTrackArt12.MouseDown, picUserLTopTrackArt13.MouseDown, picUserLTopTrackArt14.MouseDown, picUserLTopTrackArt15.MouseDown,
            picUserLTopTrackArt16.MouseDown, picUserLTopTrackArt17.MouseDown, picUserLTopTrackArt18.MouseDown, picUserLTopTrackArt19.MouseDown, picUserLTopTrackArt20.MouseDown,
            picUserLTopAlbumArt1.MouseDown, picUserLTopAlbumArt2.MouseDown, picUserLTopAlbumArt3.MouseDown, picUserLTopAlbumArt4.MouseDown, picUserLTopAlbumArt5.MouseDown, picUserLTopAlbumArt6.MouseDown,
            picUserLTopAlbumArt7.MouseDown, picUserLTopAlbumArt8.MouseDown, picUserLTopAlbumArt9.MouseDown, picUserLTopAlbumArt10.MouseDown, picUserLTopAlbumArt11.MouseDown, picUserLTopAlbumArt12.MouseDown,
            picUserLTopAlbumArt13.MouseDown, picUserLTopAlbumArt14.MouseDown, picUserLTopAlbumArt15.MouseDown, picUserLTopAlbumArt16.MouseDown, picUserLTopAlbumArt17.MouseDown, picUserLTopAlbumArt18.MouseDown,
            picUserLTopAlbumArt19.MouseDown, picUserLTopAlbumArt20.MouseDown, picTrackArt.MouseDown, picArtistArt.MouseDown, picAlbumArt.MouseDown

        If e.Button = MouseButtons.Left AndAlso sender.ImageLocation.Contains("http") = True Then
            Process.Start(sender.ImageLocation)
        End If
    End Sub

    ' makes ui changes when user is authenticated
    Public Sub AuthenticatedUI(ByVal yesno As Boolean)
        If yesno = True Then
            btnUserAuthenticate.Enabled = False
            ' user labels
            If lblUser.Text.Contains("(Authenticated)") = False Then
                lblUser.Text &= " (Authenticated)"
                lblUserStatus.Text &= " (Authenticated)"
            End If
            ' track love
            btnTrackLove.Enabled = True
            ' add/remove tags
            btnTrackTagAdd.Enabled = True
            btnTrackTagRemove.Enabled = True
            btnArtistTagAdd.Enabled = True
            btnArtistTagRemove.Enabled = True
            btnAlbumTagAdd.Enabled = True
            btnAlbumTagRemove.Enabled = True
            ' auth warning
            lblTrackAuthWarning.Enabled = False
            lblArtistAuthWarning.Enabled = False
            lblAlbumAuthWarning.Enabled = False
            ' ui labels
            lblTrackLove.Enabled = True
            lblTrackTags.Enabled = True
            lblArtistTags.Enabled = True
            lblAlbumTags.Enabled = True
            ' media
            txtMediaTitle.Enabled = True
            txtMediaArtist.Enabled = True
            txtMediaAlbum.Enabled = True
            btnMediaScrobble.Enabled = True
            btnMediaVerify.Enabled = True
            btnMediaSearch.Enabled = True
            dtpMediaScrobble.Enabled = True
            nudMediaHour.Enabled = True
            nudMediaMinute.Enabled = True
            lblMediaTitle.Enabled = True
            lblMediaArtist.Enabled = True
            lblMediaAlbum.Enabled = True
            lblMediaTime.Enabled = True
            lblMediaDivider.Enabled = True
            radMediaDisable.Enabled = True
            cmbMediaTime.Enabled = True
            radMediaEnable.Enabled = True
            radMediaEnable.Checked = True
            lblMediaScrobble.Text = "Nothing scrobbled yet."
        Else
            btnUserAuthenticate.Enabled = True
            ' user labels
            lblUser.Text = lblUser.Text.Replace(" (Authenticated)", String.Empty)
            lblUserStatus.Text = lblUser.Text.Replace(" (Authenticated)", String.Empty)
            ' track love
            btnTrackLove.Enabled = False
            ' add/remove tags
            btnTrackTagAdd.Enabled = False
            btnTrackTagRemove.Enabled = False
            btnArtistTagAdd.Enabled = False
            btnArtistTagRemove.Enabled = False
            btnAlbumTagAdd.Enabled = False
            btnAlbumTagRemove.Enabled = False
            ' auth warning
            lblTrackAuthWarning.Enabled = True
            lblArtistAuthWarning.Enabled = True
            lblAlbumAuthWarning.Enabled = True
            ' ui labels
            lblTrackLove.Enabled = False
            lblTrackTags.Enabled = False
            lblArtistTags.Enabled = False
            lblAlbumTags.Enabled = False
            ' media
            txtMediaTitle.Enabled = False
            txtMediaArtist.Enabled = False
            txtMediaAlbum.Enabled = False
            btnMediaScrobble.Enabled = False
            btnMediaVerify.Enabled = False
            btnMediaSearch.Enabled = False
            dtpMediaScrobble.Enabled = False
            nudMediaHour.Enabled = False
            nudMediaMinute.Enabled = False
            lblMediaTitle.Enabled = False
            lblMediaArtist.Enabled = False
            lblMediaAlbum.Enabled = False
            lblMediaTime.Enabled = False
            lblMediaDivider.Enabled = False
            radMediaDisable.Enabled = False
            cmbMediaTime.Enabled = False
            radMediaEnable.Enabled = False
            radMediaDisable.Checked = True
            lblMediaScrobble.Text = "Authenticate account to begin scrobbling."
        End If
    End Sub

    Private Sub AutoUncheckAll()
        mnuAuto1Min.Checked = False
        mnuAuto2Min.Checked = False
        mnuAuto3Min.Checked = False
        mnuAuto4Min.Checked = False
        mnuAuto5Min.Checked = False
        mnuAuto10Min.Checked = False
        mnuAutoOff.Checked = False
    End Sub
#End Region

#Region "Menu Items"
    Private Sub UpdateAllButton(sender As Object, e As EventArgs) Handles mnuUpdateAll.Click
        UpdateAll()
    End Sub

    Private Sub OpenAPIHistory(sender As Object, e As EventArgs) Handles mnuAPIHistory.Click
        frmAPIHistory.Show()
        frmAPIHistory.Activate()
    End Sub

    Private Sub LFMWebsite(sender As Object, e As EventArgs) Handles mnuLFMSite.Click
        Process.Start("https://www.last.fm")
    End Sub

    Private Sub About(sender As Object, e As EventArgs) Handles mnuAbout.Click
        frmAbout.Show()
        frmAbout.Activate()
    End Sub

    Private Sub AltF4(sender As Object, e As EventArgs) Handles mnuExit.Click
        Application.Exit()
    End Sub

    Private Sub Reload(sender As Object, e As EventArgs) Handles mnuReload.Click
        Application.Restart()
    End Sub

    Private Sub OpenConsole(sender As Object, e As EventArgs) Handles mnuConsole.Click
        frmConsole.Show()
        frmConsole.Activate()
    End Sub

    Private Sub OpenScrobbleHistory(sender As Object, e As EventArgs) Handles mnuScrobbleHistory.Click
        frmScrobbleHistory.Show()
        frmScrobbleHistory.Activate()
    End Sub

    Private Sub OpenScrobbleIndexEditor(sender As Object, e As EventArgs) Handles mnuIndexEditor.Click
        frmScrobbleIndexEditor.Show()
        frmScrobbleIndexEditor.Activate()
        frmScrobbleIndexEditor.NewFile()
    End Sub

    Private Sub OpenBackupTool(sender As Object, e As EventArgs) Handles mnuBackupTool.Click
        frmBackupTool.Show()
        frmBackupTool.Activate()
    End Sub

    ' takes user to user tab and selects the user box
    Private Sub SetUserButton(sender As Object, e As EventArgs) Handles mnuSetUser.Click
        tabControl.SelectTab(5)
        txtUser.Select()
        txtUser.SelectAll()
    End Sub

    ' switch to user tab
    Private Sub UserClicked(sender As Object, e As EventArgs) Handles lblUser.Click
        tabControl.SelectTab(5)
    End Sub

    ' go to track page with last played track
    Private Sub LastPlayedClicked(sender As Object, e As EventArgs) Handles lblLastPlayed.Click
        Dim trackinfo As String() = lblLastPlayed.Tag.ToString.Split(vbCrLf)    ' split lines into artist and track
        trackinfo(1).Replace(vbCrLf, String.Empty)  ' remove vbcrlf
        ' do not proceed if unavailble
        If trackinfo(0) <> "(Unavailable)" AndAlso trackinfo(1) <> "(Unavailable)" Then
            GoToTrack(trackinfo(1), trackinfo(0))
        End If
    End Sub

    Private Sub ProgressBarClicked(sender As Object, e As EventArgs) Handles UpdateProgress.Click
        frmAPIHistory.Show()
        frmAPIHistory.Activate()
    End Sub

    Private Sub Auto1Minute(sender As Object, e As EventArgs) Handles mnuAuto1Min.Click
        ' check
        AutoUncheckAll()
        mnuAuto1Min.Checked = True

        tmrAutoRefresh.Stop()
        tmrAutoRefresh.Interval = 60000
        tmrAutoRefresh.Start()

        My.Settings.AutoRefresh = 1
    End Sub

    Private Sub Auto2Minute(sender As Object, e As EventArgs) Handles mnuAuto2Min.Click
        ' check
        AutoUncheckAll()
        mnuAuto2Min.Checked = True

        tmrAutoRefresh.Stop()
        tmrAutoRefresh.Interval = 120000
        tmrAutoRefresh.Start()

        My.Settings.AutoRefresh = 2
    End Sub

    Private Sub Auto3Minute(sender As Object, e As EventArgs) Handles mnuAuto3Min.Click
        ' check
        AutoUncheckAll()
        mnuAuto3Min.Checked = True

        tmrAutoRefresh.Stop()
        tmrAutoRefresh.Interval = 180000
        tmrAutoRefresh.Start()

        My.Settings.AutoRefresh = 3
    End Sub

    Private Sub Auto4Minute(sender As Object, e As EventArgs) Handles mnuAuto4Min.Click
        ' check
        AutoUncheckAll()
        mnuAuto4Min.Checked = True

        tmrAutoRefresh.Stop()
        tmrAutoRefresh.Interval = 240000
        tmrAutoRefresh.Start()

        My.Settings.AutoRefresh = 4
    End Sub

    Private Sub Auto5Minute(sender As Object, e As EventArgs) Handles mnuAuto5Min.Click
        ' check
        AutoUncheckAll()
        mnuAuto5Min.Checked = True

        tmrAutoRefresh.Stop()
        tmrAutoRefresh.Interval = 300000
        tmrAutoRefresh.Start()

        My.Settings.AutoRefresh = 5
    End Sub

    Private Sub Auto10Minute(sender As Object, e As EventArgs) Handles mnuAuto10Min.Click
        ' check
        AutoUncheckAll()
        mnuAuto10Min.Checked = True

        tmrAutoRefresh.Stop()
        tmrAutoRefresh.Interval = 600000
        tmrAutoRefresh.Start()

        My.Settings.AutoRefresh = 10
    End Sub

    Private Sub AutoOff(sender As Object, e As EventArgs) Handles mnuAutoOff.Click
        ' check
        AutoUncheckAll()
        mnuAutoOff.Checked = True

        tmrAutoRefresh.Stop()

        My.Settings.AutoRefresh = 0
    End Sub

    Private Sub ViewCharts(sender As Object, e As EventArgs) Handles mnuViewCharts.Click
        tabControl.SelectedIndex = 0
    End Sub

    Private Sub ViewSearch(sender As Object, e As EventArgs) Handles mnuViewSearch.Click
        tabControl.SelectedIndex = 1
    End Sub

    Private Sub ViewTrack(sender As Object, e As EventArgs) Handles mnuViewTrack.Click
        tabControl.SelectedIndex = 2
    End Sub

    Private Sub ViewArtist(sender As Object, e As EventArgs) Handles mnuViewArtist.Click
        tabControl.SelectedIndex = 3
    End Sub

    Private Sub ViewAlbum(sender As Object, e As EventArgs) Handles mnuViewAlbum.Click
        tabControl.SelectedIndex = 4
    End Sub

    Private Sub ViewUser(sender As Object, e As EventArgs) Handles mnuViewUser.Click
        tabControl.SelectedIndex = 5
    End Sub

    Private Sub ViewUserL(sender As Object, e As EventArgs) Handles mnuViewUserL.Click
        tabControl.SelectedIndex = 6
    End Sub

    Private Sub ViewMedia(sender As Object, e As EventArgs) Handles mnuViewMedia.Click
        tabControl.SelectedIndex = 7
    End Sub
#End Region

#Region "Context Menu"
    Private Sub CmsArtOpen(sender As ContextMenuStrip, e As EventArgs) Handles cmsArt.Opening
        ' get parent
        Dim parent As PictureBox = CType(sender.SourceControl, PictureBox)

        If parent.ImageLocation.Contains("http") = True Then
            mnuArtOpenImage.Enabled = True
            mnuArtCopyImage.Enabled = True
            mnuArtCopyImageLink.Enabled = True
            mnuArtSaveImage.Enabled = True
        Else
            mnuArtOpenImage.Enabled = False
            mnuArtCopyImage.Enabled = False
            mnuArtCopyImageLink.Enabled = False
            mnuArtSaveImage.Enabled = False
        End If
    End Sub

    Private Sub ArtOpenImage(sender As ToolStripMenuItem, e As EventArgs) Handles mnuArtOpenImage.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As PictureBox = parent2.SourceControl

        If finalParent.ImageLocation.Contains("http") = True Then
            Process.Start(finalParent.ImageLocation)
        End If
    End Sub

    Private Sub ArtCopyImage(sender As ToolStripMenuItem, e As EventArgs) Handles mnuArtCopyImage.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As PictureBox = parent2.SourceControl

        Try
            My.Computer.Clipboard.SetImage(finalParent.Image)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ArtCopyImageLink(sender As ToolStripMenuItem, e As EventArgs) Handles mnuArtCopyImageLink.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As PictureBox = parent2.SourceControl

        Try
            My.Computer.Clipboard.SetText(finalParent.ImageLocation)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ArtSaveImage(sender As ToolStripMenuItem, e As EventArgs) Handles mnuArtSaveImage.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As PictureBox = parent2.SourceControl

        Dim result As DialogResult = sfdSaveImage.ShowDialog()

        If result = DialogResult.OK Then
            Try
                Select Case sfdSaveImage.FilterIndex
                    Case 0
                        ' jpeg
                        finalParent.Image.Save(sfdSaveImage.FileName, Imaging.ImageFormat.Jpeg)
                    Case 1
                        ' png
                        finalParent.Image.Save(sfdSaveImage.FileName, Imaging.ImageFormat.Png)
                    Case 2
                        ' bmp
                        finalParent.Image.Save(sfdSaveImage.FileName, Imaging.ImageFormat.Bmp)
                    Case Else
                        ' all files
                        finalParent.Image.Save(sfdSaveImage.FileName, Imaging.ImageFormat.Jpeg)
                End Select
            Catch ex As Exception
                MessageBox.Show("Program was unable to save image" & vbCrLf & "Message: " & ex.Message, "Image Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CmsChartTrackOpen(sender As ContextMenuStrip, e As EventArgs) Handles cmsChartTrack.Opening
        Dim row As Byte = GetChartRowIndex(CType(sender.SourceControl, Label))

        ' disable track if needed
        If ChartTrackLabel(row, 0).Text.Contains("(Unavailable)") = True OrElse ChartTrackLabel(row, 0).Text.Contains("ERROR: ") = True OrElse ChartTrackLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse ChartTrackLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuChartTrackGoToTrack.Enabled = False
            mnuChartTrackBackupTrack.Enabled = False
        Else
            mnuChartTrackGoToTrack.Enabled = True
            mnuChartTrackBackupTrack.Enabled = True
        End If

        ' disable artist if needed
        If ChartTrackLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse ChartTrackLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuChartTrackGoToArtist.Enabled = False
            mnuChartTrackBackupArtist.Enabled = False
        Else
            mnuChartTrackGoToArtist.Enabled = True
            mnuChartTrackBackupArtist.Enabled = True
        End If

        ' disable album if needed
        If ChartTrackLabel(row, 2).Text.Contains("(Unavailable)") = True OrElse ChartTrackLabel(row, 2).Text.Contains("ERROR: ") = True OrElse ChartTrackLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse ChartTrackLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuChartTrackGoToAlbum.Enabled = False
            mnuChartTrackBackupAlbum.Enabled = False
        Else
            mnuChartTrackGoToAlbum.Enabled = True
            mnuChartTrackBackupAlbum.Enabled = True
        End If
    End Sub

    Private Sub ChartTrackGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartTrackGoToTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        GoToTrack(ChartTrackLabel(row, 0).Text, ChartTrackLabel(row, 1).Text)
    End Sub

    Private Sub ChartTrackGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartTrackGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        GoToArtist(ChartTrackLabel(row, 1).Text)
    End Sub

    Private Sub ChartTrackGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartTrackGoToAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        GoToAlbum(ChartTrackLabel(row, 2).Text, ChartTrackLabel(row, 1).Text)
    End Sub

    Private Sub ChartTrackBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartTrackBackupTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        BackupTrack(ChartTrackLabel(row, 0).Text, ChartTrackLabel(row, 1).Text)
    End Sub

    Private Sub ChartTrackBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartTrackBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        BackupArtist(ChartTrackLabel(row, 1).Text)
    End Sub

    Private Sub ChartTrackBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartTrackBackupAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        BackupAlbum(ChartTrackLabel(row, 2).Text, ChartTrackLabel(row, 1).Text)
    End Sub

    Private Sub CmsChartArtistOpen(sender As ContextMenuStrip, e As EventArgs) Handles cmsChartArtist.Opening
        Dim row As Byte = GetChartRowIndex(CType(sender.SourceControl, Label))

        ' disable artist if needed
        If ChartArtistLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse ChartArtistLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuChartArtistGoToArtist.Enabled = False
            mnuChartArtistBackupArtist.Enabled = False
        Else
            mnuChartArtistGoToArtist.Enabled = True
            mnuChartArtistBackupArtist.Enabled = True
        End If
    End Sub

    Private Sub ChartArtistGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartArtistGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        GoToArtist(ChartArtistLabel(row, 0).Text)
    End Sub

    Private Sub ChartArtistBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuChartArtistBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetChartRowIndex(finalParent)

        BackupArtist(ChartArtistLabel(row, 0).Text)
    End Sub

    Private Sub CmsSearchOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsSearch.Opening
        If txtSearchInfo.Text = String.Empty Then
            e.Cancel = True
            Exit Sub
        End If

        If txtSearchInfo.SelectedText = Nothing Then
            mnuSearchCopy.Enabled = False
        Else
            mnuSearchCopy.Enabled = True
        End If
    End Sub

    Private Sub SearchBackupTag(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchBackup.Click
        BackupTag(txtSearchInfo.Text.Split(vbLf)(1))
    End Sub

    Private Sub SearchSelectAll(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchSelectAll.Click
        txtSearchInfo.SelectAll()
    End Sub

    Private Sub SearchCopy(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchCopy.Click
        Try
            My.Computer.Clipboard.SetText(txtSearchInfo.SelectedText)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsSearchListsOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsSearchTrack.Opening, cmsSearchArtist.Opening, cmsSearchAlbum.Opening
        If CType(sender.SourceControl, ListView).SelectedItems.Count <= 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub SearchGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchGoToTrack.Click
        GoToTrack(ltvSearchTracks.SelectedItems(0).SubItems(1).Text, ltvSearchTracks.SelectedItems(0).SubItems(2).Text)
    End Sub

    Private Sub SearchGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchGoToArtist.Click
        GoToArtist(ltvSearchArtists.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub SearchGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchGoToAlbum.Click
        GoToAlbum(ltvSearchAlbums.SelectedItems(0).SubItems(1).Text, ltvSearchAlbums.SelectedItems(0).SubItems(2).Text)
    End Sub

    Private Sub SearchBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchBackupTrack.Click
        BackupTrack(ltvSearchTracks.SelectedItems(0).SubItems(1).Text, ltvSearchTracks.SelectedItems(0).SubItems(2).Text)
    End Sub

    Private Sub SearchBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchBackupArtist.Click
        BackupArtist(ltvSearchArtists.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub SearchBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuSearchBackupAlbum.Click
        BackupAlbum(ltvSearchAlbums.SelectedItems(0).SubItems(1).Text, ltvSearchAlbums.SelectedItems(0).SubItems(2).Text)
    End Sub

    Private Sub CmsTrackOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsTrack.Opening
        ' do not open if nothing is in the text box
        If txtTrackInfo.Text.Trim = String.Empty Then
            e.Cancel = True
            Exit Sub
        End If

        If txtTrackInfo.SelectedText = String.Empty Then
            mnuTrackCopy.Enabled = False
        Else
            mnuTrackCopy.Enabled = True
        End If
    End Sub

    Private Sub TrackBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuTrackBackupTrack.Click
        BackupTrack(txtTrackInfo.Text.Split(vbLf)(1), txtTrackInfo.Text.Split(vbLf)(4))
    End Sub

    Private Sub TrackSelectAll(sender As ToolStripMenuItem, e As EventArgs) Handles mnuTrackSelectAll.Click
        txtTrackInfo.SelectAll()
    End Sub

    Private Sub TrackCopy(sender As ToolStripMenuItem, e As EventArgs) Handles mnuTrackCopy.Click
        Try
            My.Computer.Clipboard.SetText(txtTrackInfo.SelectedText)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsArtistOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsArtist.Opening
        ' do not open if nothing is in the text box
        If txtArtistInfo.Text.Trim = String.Empty Then
            e.Cancel = True
            Exit Sub
        End If

        If txtArtistInfo.SelectedText = String.Empty Then
            mnuArtistCopy.Enabled = False
        Else
            mnuArtistCopy.Enabled = True
        End If
    End Sub

    Private Sub ArtistBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuArtistBackupArtist.Click
        BackupArtist(txtArtistInfo.Text.Split(vbLf)(1))
    End Sub

    Private Sub ArtistSelectAll(sender As ToolStripMenuItem, e As EventArgs) Handles mnuArtistSelectAll.Click
        txtArtistInfo.SelectAll()
    End Sub

    Private Sub ArtistCopy(sender As ToolStripMenuItem, e As EventArgs) Handles mnuArtistCopy.Click
        Try
            My.Computer.Clipboard.SetText(txtArtistInfo.SelectedText)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsAlbumOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsAlbum.Opening
        ' do not open if nothing is in the text box
        If txtAlbumInfo.Text.Trim = String.Empty Then
            e.Cancel = True
            Exit Sub
        End If

        If txtAlbumInfo.SelectedText = String.Empty Then
            mnuAlbumCopy.Enabled = False
        Else
            mnuAlbumCopy.Enabled = True
        End If
    End Sub

    Private Sub AlbumBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuAlbumBackupAlbum.Click
        BackupAlbum(txtAlbumInfo.Text.Split(vbLf)(1), txtAlbumInfo.Text.Split(vbLf)(4))
    End Sub

    Private Sub AlbumSelectAll(sender As ToolStripMenuItem, e As EventArgs) Handles mnuAlbumSelectAll.Click
        txtAlbumInfo.SelectAll()
    End Sub

    Private Sub AlbumCopy(sender As ToolStripMenuItem, e As EventArgs) Handles mnuAlbumCopy.Click
        Try
            My.Computer.Clipboard.SetText(txtAlbumInfo.SelectedText)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsUserOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUser.Opening
        ' do not open if nothing is in the text box
        If txtUserInfo.Text.Trim = String.Empty Then
            e.Cancel = True
            Exit Sub
        End If

        If txtUserInfo.SelectedText = String.Empty Then
            mnuUserCopy.Enabled = False
        Else
            mnuUserCopy.Enabled = True
        End If
    End Sub

    Private Sub UserBackupUser(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserBackupUser.Click
        BackupUser(txtUserInfo.Text.Split(vbLf)(1))
    End Sub

    Private Sub UserSelectAll(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserSelectAll.Click
        txtUserInfo.SelectAll()
    End Sub

    Private Sub UserCopy(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserCopy.Click
        Try
            My.Computer.Clipboard.SetText(txtUserInfo.SelectedText)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsUserFriendsOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserFriends.Opening
        ' do not open if nothing is selected
        If ltvUserFriends.SelectedItems.Count <= 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub UserFriendGoToUser(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserGoToFriend.Click
        txtUserL.Text = ltvUserFriends.SelectedItems(0).SubItems(0).Text
        tabControl.SelectedIndex = 6
        btnUserLSet.PerformClick()
    End Sub

    Private Sub UserFriendOpenLink(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserOpenFriendLink.Click
        Process.Start(ltvUserFriends.SelectedItems(0).SubItems(2).Text)
    End Sub

    Private Sub UserFriendBackupUser(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserBackupFriend.Click
        BackupUser(ltvUserFriends.SelectedItems(0).SubItems(0).Text)
    End Sub

    Private Sub UserFriendCopyUsername(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserCopyFriendName.Click
        Try
            My.Computer.Clipboard.SetText(ltvUserFriends.SelectedItems(0).SubItems(0).Text)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsUserLovedTracksOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserLovedTracks.Opening
        ' do not open if nothing is selected
        If ltvUserLovedTracks.SelectedItems.Count <= 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub UserLovedTrackGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserGoToLovedTrack.Click
        GoToTrack(ltvUserLovedTracks.SelectedItems(0).SubItems(0).Text, ltvUserLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLovedTrackGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserGoToLovedArtist.Click
        GoToArtist(ltvUserLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLovedTrackBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserBackupLovedTrack.Click
        BackupTrack(ltvUserLovedTracks.SelectedItems(0).SubItems(0).Text, ltvUserLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLovedTrackBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserBackupLovedArtist.Click
        BackupArtist(ltvUserLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub CmsUserRecentOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserRecent.Opening
        Dim row As Byte = GetUserRecentRowIndex(CType(sender.SourceControl, Label))

        ' disable track if needed
        If UserRecentLabel(row, 0).Text.Contains("(Unavailable)") = True OrElse UserRecentLabel(row, 0).Text.Contains("ERROR: ") = True OrElse UserRecentLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserRecentLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserGoToRecentTrack.Enabled = False
            mnuUserBackupRecentTrack.Enabled = False
        Else
            mnuUserGoToRecentTrack.Enabled = True
            mnuUserBackupRecentTrack.Enabled = True
        End If

        ' disable artist if needed
        If UserRecentLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserRecentLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserGoToRecentArtist.Enabled = False
            mnuUserBackupRecentArtist.Enabled = False
        Else
            mnuUserGoToRecentArtist.Enabled = True
            mnuUserBackupRecentArtist.Enabled = True
        End If

        ' disable album if needed
        If UserRecentLabel(row, 2).Text.Contains("(Unavailable)") = True OrElse UserRecentLabel(row, 2).Text.Contains("ERROR: ") = True OrElse UserRecentLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserRecentLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserGoToRecentAlbum.Enabled = False
            mnuUserBackupRecentAlbum.Enabled = False
        Else
            mnuUserGoToRecentAlbum.Enabled = True
            mnuUserBackupRecentAlbum.Enabled = True
        End If
    End Sub

    Private Sub UserRecentGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserGoToRecentTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        GoToTrack(UserRecentLabel(row, 0).Text, UserRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserRecentGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserGoToRecentArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        GoToArtist(UserRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserRecentGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserGoToRecentAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        GoToAlbum(UserRecentLabel(row, 2).Text, UserRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserRecentBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserBackupRecentTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        BackupTrack(UserRecentLabel(row, 0).Text, UserRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserRecentBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserBackupRecentArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        BackupArtist(UserRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserRecentBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserBackupRecentAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        BackupAlbum(UserRecentLabel(row, 2).Text, UserRecentLabel(row, 1).Text)
    End Sub

    Private Sub CmsUserTopTrackOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserTopTracks.Opening
        Dim row As Byte = GetUserTopTrackRowIndex(CType(sender.SourceControl, Label))

        ' disable track if needed
        If UserTopTracksLabel(row, 0).Text.Contains("(Unavailable)") = True OrElse UserTopTracksLabel(row, 0).Text.Contains("ERROR: ") = True OrElse UserTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserTopTracksLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserTopTrackGoToTrack.Enabled = False
            mnuUserTopTrackBackupTrack.Enabled = False
        Else
            mnuUserTopTrackGoToTrack.Enabled = True
            mnuUserTopTrackBackupTrack.Enabled = True
        End If

        ' disable artist if needed
        If UserTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserTopTracksLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserTopTrackGoToTrack.Enabled = False
            mnuUserTopTrackBackupTrack.Enabled = False
        Else
            mnuUserTopTrackGoToTrack.Enabled = True
            mnuUserTopTrackBackupTrack.Enabled = True
        End If

        ' disable album if needed
        If UserTopTracksLabel(row, 2).Text.Contains("(Unavailable)") = True OrElse UserTopTracksLabel(row, 2).Text.Contains("ERROR: ") = True OrElse UserTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserTopTracksLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserTopTrackGoToAlbum.Enabled = False
            mnuUserTopTrackBackupAlbum.Enabled = False
        Else
            mnuUserTopTrackGoToAlbum.Enabled = True
            mnuUserTopTrackBackupAlbum.Enabled = True
        End If
    End Sub

    Private Sub UserTopTrackGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopTrackGoToTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        GoToTrack(UserTopTracksLabel(row, 0).Text, UserTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserTopTrackGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopTrackGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        GoToArtist(UserTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserTopTrackGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopTrackGoToAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        GoToAlbum(UserTopTracksLabel(row, 2).Text, UserTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserTopTrackBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopTrackBackupTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        BackupTrack(UserTopTracksLabel(row, 0).Text, UserTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserTopTrackBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopTrackBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        BackupArtist(UserTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserTopTrackBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopTrackBackupAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        BackupAlbum(UserTopTracksLabel(row, 2).Text, UserTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserTopArtistGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopArtistGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopArtistRowIndex(finalParent)

        GoToArtist(UserTopArtistsLabel(row, 0).Text)
    End Sub

    Private Sub UserTopArtistBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopArtistBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopArtistRowIndex(finalParent)

        BackupArtist(UserTopArtistsLabel(row, 0).Text)
    End Sub

    Private Sub UserTopAlbumGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopAlbumGoToAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        GoToAlbum(UserTopAlbumsLabel(row, 0).Text, UserTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub UserTopAlbumGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopAlbumGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        GoToArtist(UserTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub UserTopAlbumBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopAlbumBackupAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        BackupAlbum(UserTopAlbumsLabel(row, 0).Text, UserTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub UserTopAlbumBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserTopAlbumBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        BackupArtist(UserTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub CmsUserHistoryOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserHistory.Opening
        ' do not open if nothing is selected
        If ltvUserHistory.SelectedItems.Count <= 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub UserHistoryGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserHistoryGoToTrack.Click
        GoToTrack(ltvUserHistory.SelectedItems(0).SubItems(0).Text, ltvUserHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserHistoryGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserHistoryGoToArtist.Click
        GoToArtist(ltvUserHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserHistoryGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserHistoryGoToAlbum.Click
        GoToAlbum(ltvUserHistory.SelectedItems(0).SubItems(2).Text, ltvUserHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserHistoryBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserHistoryBackupTrack.Click
        BackupTrack(ltvUserHistory.SelectedItems(0).SubItems(0).Text, ltvUserHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserHistoryBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserHistoryBackupArtist.Click
        BackupArtist(ltvUserHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserHistoryBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserHistoryBackupAlbum.Click
        BackupAlbum(ltvUserHistory.SelectedItems(0).SubItems(2).Text, ltvUserHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub CmsUserLOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserL.Opening
        ' do not open if nothing is in the text box
        If txtUserLInfo.Text.Trim = String.Empty Then
            e.Cancel = True
            Exit Sub
        End If

        If txtUserLInfo.SelectedText = String.Empty Then
            mnuUserLCopy.Enabled = False
        Else
            mnuUserLCopy.Enabled = True
        End If
    End Sub

    Private Sub UserLBackupUserL(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLBackupUser.Click
        BackupUser(txtUserLInfo.Text.Split(vbLf)(1))
    End Sub

    Private Sub UserLSelectAll(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLSelectAll.Click
        txtUserLInfo.SelectAll()
    End Sub

    Private Sub UserLCopy(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLCopy.Click
        Try
            My.Computer.Clipboard.SetText(txtUserLInfo.SelectedText)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsUserLFriendsOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserLFriends.Opening
        ' do not open if nothing is selected
        If ltvUserLFriends.SelectedItems.Count <= 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub UserLFriendGoToUserL(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLGoToFriend.Click
        txtUserL.Text = ltvUserLFriends.SelectedItems(0).SubItems(0).Text
        tabControl.SelectedIndex = 6
        btnUserLSet.PerformClick()
    End Sub

    Private Sub UserLFriendOpenLink(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLOpenFriendLink.Click
        Process.Start(ltvUserLFriends.SelectedItems(0).SubItems(2).Text)
    End Sub

    Private Sub UserLFriendBackupUserL(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLBackupFriend.Click
        BackupUser(ltvUserLFriends.SelectedItems(0).SubItems(0).Text)
    End Sub

    Private Sub UserLFriendCopyUserLname(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLCopyFriendName.Click
        Try
            My.Computer.Clipboard.SetText(ltvUserLFriends.SelectedItems(0).SubItems(0).Text)
        Catch ex As Exception
            MessageBox.Show("Program was unable to write to the clipboard" & vbCrLf & "Check that another program is not using the clipboard", "Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmsUserLLovedTracksOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserLLovedTracks.Opening
        ' do not open if nothing is selected
        If ltvUserLLovedTracks.SelectedItems.Count <= 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub UserLLovedTrackGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLGoToLovedTrack.Click
        GoToTrack(ltvUserLLovedTracks.SelectedItems(0).SubItems(0).Text, ltvUserLLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLLovedTrackGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLGoToLovedArtist.Click
        GoToArtist(ltvUserLLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLLovedTrackBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLBackupLovedTrack.Click
        BackupTrack(ltvUserLLovedTracks.SelectedItems(0).SubItems(0).Text, ltvUserLLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLLovedTrackBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLBackupLovedArtist.Click
        BackupArtist(ltvUserLLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub CmsUserLRecentOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserLRecent.Opening
        Dim row As Byte = GetUserRecentRowIndex(CType(sender.SourceControl, Label))

        ' disable track if needed
        If UserLRecentLabel(row, 0).Text.Contains("(Unavailable)") = True OrElse UserLRecentLabel(row, 0).Text.Contains("ERROR: ") = True OrElse UserLRecentLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserLRecentLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserLGoToRecentTrack.Enabled = False
            mnuUserLBackupRecentTrack.Enabled = False
        Else
            mnuUserLGoToRecentTrack.Enabled = True
            mnuUserLBackupRecentTrack.Enabled = True
        End If

        ' disable artist if needed
        If UserLRecentLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserLRecentLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserLGoToRecentArtist.Enabled = False
            mnuUserLBackupRecentArtist.Enabled = False
        Else
            mnuUserLGoToRecentArtist.Enabled = True
            mnuUserLBackupRecentArtist.Enabled = True
        End If

        ' disable album if needed
        If UserLRecentLabel(row, 2).Text.Contains("(Unavailable)") = True OrElse UserLRecentLabel(row, 2).Text.Contains("ERROR: ") = True OrElse UserLRecentLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserLRecentLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserLGoToRecentAlbum.Enabled = False
            mnuUserLBackupRecentAlbum.Enabled = False
        Else
            mnuUserLGoToRecentAlbum.Enabled = True
            mnuUserLBackupRecentAlbum.Enabled = True
        End If
    End Sub

    Private Sub UserLRecentGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLGoToRecentTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        GoToTrack(UserLRecentLabel(row, 0).Text, UserLRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserLRecentGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLGoToRecentArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        GoToArtist(UserLRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserLRecentGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLGoToRecentAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        GoToAlbum(UserLRecentLabel(row, 2).Text, UserLRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserLRecentBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLBackupRecentTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        BackupTrack(UserLRecentLabel(row, 0).Text, UserLRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserLRecentBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLBackupRecentArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        BackupArtist(UserLRecentLabel(row, 1).Text)
    End Sub

    Private Sub UserLRecentBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLBackupRecentAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserRecentRowIndex(finalParent)

        BackupAlbum(UserLRecentLabel(row, 2).Text, UserLRecentLabel(row, 1).Text)
    End Sub

    Private Sub CmsUserLTopTrackOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserLTopTracks.Opening
        Dim row As Byte = GetUserTopTrackRowIndex(CType(sender.SourceControl, Label))

        ' disable track if needed
        If UserLTopTracksLabel(row, 0).Text.Contains("(Unavailable)") = True OrElse UserLTopTracksLabel(row, 0).Text.Contains("ERROR: ") = True OrElse UserLTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserLTopTracksLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserLTopTrackGoToTrack.Enabled = False
            mnuUserLTopTrackBackupTrack.Enabled = False
        Else
            mnuUserLTopTrackGoToTrack.Enabled = True
            mnuUserLTopTrackBackupTrack.Enabled = True
        End If

        ' disable artist if needed
        If UserLTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserLTopTracksLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserLTopTrackGoToTrack.Enabled = False
            mnuUserLTopTrackBackupTrack.Enabled = False
        Else
            mnuUserLTopTrackGoToTrack.Enabled = True
            mnuUserLTopTrackBackupTrack.Enabled = True
        End If

        ' disable album if needed
        If UserLTopTracksLabel(row, 2).Text.Contains("(Unavailable)") = True OrElse UserLTopTracksLabel(row, 2).Text.Contains("ERROR: ") = True OrElse UserLTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserLTopTracksLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserLTopTrackGoToAlbum.Enabled = False
            mnuUserLTopTrackBackupAlbum.Enabled = False
        Else
            mnuUserLTopTrackGoToAlbum.Enabled = True
            mnuUserLTopTrackBackupAlbum.Enabled = True
        End If
    End Sub

    Private Sub UserLTopTrackGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopTrackGoToTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        GoToTrack(UserLTopTracksLabel(row, 0).Text, UserLTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopTrackGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopTrackGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        GoToArtist(UserLTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopTrackGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopTrackGoToAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        GoToAlbum(UserLTopTracksLabel(row, 2).Text, UserLTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopTrackBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopTrackBackupTrack.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        BackupTrack(UserLTopTracksLabel(row, 0).Text, UserLTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopTrackBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopTrackBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        BackupArtist(UserLTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopTrackBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopTrackBackupAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopTrackRowIndex(finalParent)

        BackupAlbum(UserLTopTracksLabel(row, 2).Text, UserLTopTracksLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopArtistGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopArtistGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopArtistRowIndex(finalParent)

        GoToArtist(UserLTopArtistsLabel(row, 0).Text)
    End Sub

    Private Sub UserLTopArtistBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopArtistBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopArtistRowIndex(finalParent)

        BackupArtist(UserLTopArtistsLabel(row, 0).Text)
    End Sub

    Private Sub UserLTopAlbumGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopAlbumGoToAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        GoToAlbum(UserLTopAlbumsLabel(row, 0).Text, UserLTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopAlbumGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopAlbumGoToArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        GoToArtist(UserLTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopAlbumBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopAlbumBackupAlbum.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        BackupAlbum(UserLTopAlbumsLabel(row, 0).Text, UserLTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub UserLTopAlbumBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLTopAlbumBackupArtist.Click
        Dim parent1 As ToolStrip = sender.GetCurrentParent()
        Dim parent2 As ContextMenuStrip = CType(parent1, ContextMenuStrip)
        Dim finalParent As Label = parent2.SourceControl
        Dim row As Byte = GetUserTopAlbumRowIndex(finalParent)

        BackupArtist(UserLTopAlbumsLabel(row, 1).Text)
    End Sub

    Private Sub CmsUserLHistoryOpen(sender As ContextMenuStrip, e As CancelEventArgs) Handles cmsUserLHistory.Opening
        ' do not open if nothing is selected
        If ltvUserLHistory.SelectedItems.Count <= 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub UserLHistoryGoToTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLHistoryGoToTrack.Click
        GoToTrack(ltvUserLHistory.SelectedItems(0).SubItems(0).Text, ltvUserLHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLHistoryGoToArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLHistoryGoToArtist.Click
        GoToArtist(ltvUserLHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLHistoryGoToAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLHistoryGoToAlbum.Click
        GoToAlbum(ltvUserLHistory.SelectedItems(0).SubItems(2).Text, ltvUserLHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLHistoryBackupTrack(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLHistoryBackupTrack.Click
        BackupTrack(ltvUserLHistory.SelectedItems(0).SubItems(0).Text, ltvUserLHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLHistoryBackupArtist(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLHistoryBackupArtist.Click
        BackupArtist(ltvUserLHistory.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLHistoryBackupAlbum(sender As ToolStripMenuItem, e As EventArgs) Handles mnuUserLHistoryBackupAlbum.Click
        BackupAlbum(ltvUserLHistory.SelectedItems(0).SubItems(2).Text, ltvUserLHistory.SelectedItems(0).SubItems(1).Text)
    End Sub
#End Region

#Region "Chart UI"
    Private Sub ChartRad(sender As Object, e As EventArgs) Handles radChartWorldwide.CheckedChanged
        If radChartWorldwide.Checked = True Then
            cmbChartCountry.Enabled = False
        Else
            cmbChartCountry.Enabled = True
        End If
    End Sub

    Private Sub ChartGo(sender As Object, e As EventArgs) Handles btnChartGo.Click
        If bgwChartUpdater.IsBusy = False Then
            bgwChartUpdater.RunWorkerAsync()
        End If
    End Sub

    Private Sub ChartTrackTrackClick(sender As Label, e As MouseEventArgs) Handles lblTopTrackTitle1.Click, lblTopTrackTitle2.Click, lblTopTrackTitle3.Click, lblTopTrackTitle4.Click, lblTopTrackTitle5.Click, lblTopTrackTitle6.Click, lblTopTrackTitle7.Click, lblTopTrackTitle8.Click,
            lblTopTrackTitle9.Click, lblTopTrackTitle10.Click, lblTopTrackTitle11.Click, lblTopTrackTitle12.Click, lblTopTrackTitle13.Click, lblTopTrackTitle14.Click, lblTopTrackTitle15.Click, lblTopTrackTitle16.Click, lblTopTrackTitle17.Click, lblTopTrackTitle18.Click, lblTopTrackTitle19.Click, lblTopTrackTitle20.Click
        Dim row As Byte = GetChartRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False AndAlso UserRecentLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserRecentLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToTrack(ChartTrackLabel(row, 0).Text, ChartTrackLabel(row, 1).Text)
        End If
    End Sub

    Private Sub ChartTrackArtistClick(sender As Label, e As MouseEventArgs) Handles lblTopTrackArtist1.Click, lblTopTrackArtist2.Click, lblTopTrackArtist3.Click, lblTopTrackArtist4.Click, lblTopTrackArtist5.Click, lblTopTrackArtist6.Click, lblTopTrackArtist7.Click, lblTopTrackArtist8.Click,
            lblTopTrackArtist9.Click, lblTopTrackArtist10.Click, lblTopTrackArtist11.Click, lblTopTrackArtist12.Click, lblTopTrackArtist13.Click, lblTopTrackArtist14.Click, lblTopTrackArtist15.Click, lblTopTrackArtist16.Click, lblTopTrackArtist17.Click, lblTopTrackArtist18.Click, lblTopTrackArtist19.Click, lblTopTrackArtist20.Click
        Dim row As Byte = GetChartRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False Then
            GoToArtist(ChartTrackLabel(row, 1).Text)
        End If
    End Sub

    Private Sub ChartTrackAlbumClick(sender As Label, e As MouseEventArgs) Handles lblTopTrackAlbum1.Click, lblTopTrackAlbum2.Click, lblTopTrackAlbum3.Click, lblTopTrackAlbum4.Click, lblTopTrackAlbum5.Click, lblTopTrackAlbum6.Click, lblTopTrackAlbum7.Click, lblTopTrackAlbum8.Click,
            lblTopTrackAlbum9.Click, lblTopTrackAlbum10.Click, lblTopTrackAlbum11.Click, lblTopTrackAlbum12.Click, lblTopTrackAlbum13.Click, lblTopTrackAlbum14.Click, lblTopTrackAlbum15.Click, lblTopTrackAlbum16.Click, lblTopTrackAlbum17.Click, lblTopTrackAlbum18.Click, lblTopTrackAlbum19.Click, lblTopTrackAlbum20.Click
        Dim row As Byte = GetChartRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False AndAlso UserRecentLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserRecentLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToAlbum(ChartTrackLabel(row, 2).Text, ChartTrackLabel(row, 1).Text)
        End If
    End Sub

    Private Sub ChartArtistClick(sender As Label, e As MouseEventArgs) Handles lblTopArtist1.Click, lblTopArtist2.Click, lblTopArtist3.Click, lblTopArtist4.Click, lblTopArtist5.Click, lblTopArtist6.Click, lblTopArtist7.Click, lblTopArtist8.Click,
            lblTopArtist9.Click, lblTopArtist10.Click, lblTopArtist11.Click, lblTopArtist12.Click, lblTopArtist13.Click, lblTopArtist14.Click, lblTopArtist15.Click, lblTopArtist16.Click, lblTopArtist17.Click, lblTopArtist18.Click, lblTopArtist19.Click, lblTopArtist20.Click
        Dim row As Byte = GetChartRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False Then
            GoToArtist(ChartArtistLabel(row, 0).Text)
        End If
    End Sub

    Private Sub CmsUserTopArtistOpen(sender As ContextMenuStrip, e As EventArgs) Handles cmsUserTopArtists.Opening
        Dim row As Byte = GetUserTopArtistRowIndex(CType(sender.SourceControl, Label))

        ' disable artist if needed
        If UserTopArtistsLabel(row, 1).Text.Contains("(Unavailable)") = True OrElse UserTopArtistsLabel(row, 1).Text.Contains("ERROR: ") = True Then
            mnuUserTopArtistGoToArtist.Enabled = False
            mnuUserTopArtistBackupArtist.Enabled = False
        Else
            mnuUserTopArtistGoToArtist.Enabled = True
            mnuUserTopArtistBackupArtist.Enabled = True
        End If
    End Sub
#End Region

#Region "Search UI"
    Private Sub GoSearch(sender As Object, e As EventArgs) Handles btnSearchGo.Click
        If bgwSearchUpdater.IsBusy = False Then
            bgwSearchUpdater.RunWorkerAsync()
        End If
    End Sub

    Private Sub GoSearchTrack(sender As Object, e As EventArgs) Handles ltvSearchTracks.ItemActivate
        GoToTrack(ltvSearchTracks.SelectedItems(0).SubItems(1).Text, ltvSearchTracks.SelectedItems(0).SubItems(2).Text)
    End Sub

    Private Sub GoSearchArtist(sender As Object, e As EventArgs) Handles ltvSearchArtists.ItemActivate
        GoToArtist(ltvSearchArtists.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub GoSearchAlbum(sender As Object, e As EventArgs) Handles ltvSearchAlbums.ItemActivate
        GoToAlbum(ltvSearchAlbums.SelectedItems(0).SubItems(1).Text, ltvSearchAlbums.SelectedItems(0).SubItems(2).Text)
    End Sub
#End Region

#Region "Track UI"
    Private Sub TrackGo(sender As Object, e As EventArgs) Handles btnTrackGo.Click
        If txtTrackTitle.Text = String.Empty OrElse txtTrackArtist.Text = String.Empty Then
            MessageBox.Show("You must enter data into both the Title and Artist fields in order to use this", "Track Lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            If bgwTrackUpdater.IsBusy = False Then
                bgwTrackUpdater.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub TrackLove(sender As Object, e As EventArgs) Handles btnTrackLove.Click
        ' stop if no track is loaded
        If tracklookup(0) = Nothing Then
            Exit Sub
        End If

        ' stop user from breaking crap by spamming the button
        Static working As Boolean = False
        If working = False Then
            ' if track not loved
            If btnTrackLove.Text = "Love" Then
                ' set working flag to true
                working = True

                ' call api
                Dim lovedresponse As String = CallAPIAuth("track.love", "track=" & tracklookup(0), "artist=" & tracklookup(1))

                ' determine if love was successful or not
                If lovedresponse.Contains("ok") = True Then
                    btnTrackLove.Text = "Unlove"
                Else
                    btnTrackLove.Text = "Love"
                End If

                ' set working flag back to false
                working = False
            Else    ' if track is loved already
                ' set working flag to true
                working = True

                ' call api
                Dim lovedresponse As String = CallAPIAuth("track.unlove", "track=" & tracklookup(0), "artist=" & tracklookup(1))

                ' determine if love was successful or not
                If lovedresponse.Contains("ok") = True Then
                    btnTrackLove.Text = "Love"
                Else
                    btnTrackLove.Text = "Unlove"
                End If

                ' set working flag back to false
                working = False
            End If
        End If
    End Sub

    Private Sub TrackAddTag(sender As Object, e As EventArgs) Handles btnTrackTagAdd.Click
        ' stop if no track is loaded
        If tracklookup(0) = Nothing Then
            Exit Sub
        End If

        ' get input
        Dim tags As String = InputBox("Please enter tag(s), multiple can be added by separating them with commas (10 max).", "Enter Tags")

        ' halt if no input
        If tags = String.Empty Then
            Exit Sub
        End If

        ' remove spaces after commas
        If tags.Contains(",") = True Then
            Dim commaindex As UShort = 1
            For count As Byte = 1 To StrCount(tags, ",")
                ' find comma index
                commaindex = InStr(commaindex + 1, tags, ",")

                ' get rid of spaces
                If tags(commaindex) = " "c Then
                    tags = tags.Remove(commaindex, 1)
                End If
            Next
        End If

        ' call api
        Dim tagresponse As String = CallAPIAuth("track.addTags", "track=" & tracklookup(0), "artist=" & tracklookup(1), "tags=" & tags)

        ' check for success
        If tagresponse.Contains("ok") = True Then
            btnTrackGo.PerformClick()
        Else
            MessageBox.Show("Error while attempting to add tags. Please make sure you are using commas to separate tags and you are not using any special characters.",
                            "Add Tags", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TrackRemoveTag(sender As Object, e As EventArgs) Handles btnTrackTagRemove.Click
        ' stop if no track is loaded or no tag selected
        If lstTrackUserTags.SelectedIndex = -1 OrElse tracklookup(0) = Nothing Then
            Exit Sub
        End If

        ' call api
        Dim tagresponse As String = CallAPIAuth("track.removeTag", "track=" & tracklookup(0), "artist=" & tracklookup(1), "tag=" & lstTrackUserTags.SelectedItem.ToString)

        ' check for success
        If tagresponse.Contains("ok") = True Then
            lstTrackUserTags.Items.RemoveAt(lstTrackUserTags.SelectedIndex) ' remove from list box
        Else
            MessageBox.Show("Error while attempting to remove tag. Please try again later.", "Remove Tag", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TrackGoArtist(sender As Object, e As EventArgs) Handles btnTrackGoArtist.Click
        GoToArtist(tracklookup(1))
    End Sub

    Private Sub TrackGoAlbum(sender As Object, e As EventArgs) Handles btnTrackGoAlbum.Click
        GoToAlbum(tracklookup(2), tracklookup(1))
    End Sub

    Private Sub TrackAdvancedSearch(sender As Object, e As EventArgs) Handles btnTrackAdvanced.Click
        frmTrackAdvanced.Show()
        frmTrackAdvanced.Activate()
    End Sub

    Private Sub TrackSimilarClicked(sender As Object, e As EventArgs) Handles ltvTrackSimilar.ItemActivate
        txtTrackTitle.Text = ltvTrackSimilar.SelectedItems(0).Text              ' set title
        txtTrackArtist.Text = ltvTrackSimilar.SelectedItems(0).SubItems(1).Text ' set artist
        btnTrackGo.PerformClick()
    End Sub

    Private Sub TrackLinkClicked(sender As Object, e As LinkClickedEventArgs) Handles txtTrackInfo.LinkClicked
        Process.Start(e.LinkText)
    End Sub
#End Region

#Region "Artist UI"
    Private Sub ArtistAddTag(sender As Object, e As EventArgs) Handles btnArtistTagAdd.Click
        ' stop if no track is loaded
        If artistlookup = Nothing Then
            Exit Sub
        End If

        ' get input
        Dim tags As String = InputBox("Please enter tag(s), multiple can be added by separating them with commas (10 max).", "Enter Tags")

        ' halt if no input
        If tags = String.Empty Then
            Exit Sub
        End If

        ' remove spaces after commas
        If tags.Contains(",") = True Then
            Dim commaindex As UShort = 1
            For count As Byte = 1 To StrCount(tags, ",")
                ' find comma index
                commaindex = InStr(commaindex + 1, tags, ",")

                ' get rid of spaces
                If tags(commaindex) = " "c Then
                    tags = tags.Remove(commaindex, 1)
                End If
            Next
        End If

        ' call api
        Dim tagresponse As String = CallAPIAuth("artist.addTags", "artist=" & artistlookup, "tags=" & tags)

        ' check for success
        If tagresponse.Contains("ok") = True Then
            btnArtistGo.PerformClick()
        Else
            MessageBox.Show("Error while attempting to add tags. Please make sure you are using commas to separate tags and you are not using any special characters.",
                            "Add Tags", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ArtistRemoveTag(sender As Object, e As EventArgs) Handles btnArtistTagRemove.Click
        ' stop if no track is loaded or no tag is selected
        If lstArtistUserTags.SelectedIndex = -1 OrElse artistlookup = String.Empty Then
            Exit Sub
        End If

        ' call api
        Dim tagresponse As String = CallAPIAuth("artist.removeTag", "artist=" & artistlookup, "tag=" & lstArtistUserTags.SelectedItem.ToString)

        ' check for success
        If tagresponse.Contains("ok") = True Then
            lstArtistUserTags.Items.RemoveAt(lstArtistUserTags.SelectedIndex) ' remove from list box
        Else
            MessageBox.Show("Error while attempting to remove tag. Please try again later.", "Remove Tag", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ArtistGo(sender As Object, e As EventArgs) Handles btnArtistGo.Click
        If bgwArtistUpdater.IsBusy = False Then
            bgwArtistUpdater.RunWorkerAsync()
        End If
    End Sub

    Private Sub ArtistSimilarClicked(sender As Object, e As EventArgs) Handles ltvArtistSimilar.ItemActivate
        txtArtistName.Text = ltvArtistSimilar.SelectedItems(0).Text              ' set title
        btnArtistGo.PerformClick()
    End Sub

    Private Sub ArtistAdvancedSearch(sender As Object, e As EventArgs) Handles btnArtistAdvanced.Click
        frmArtistAdvanced.Show()
        frmArtistAdvanced.Activate()
    End Sub

    Private Sub ArtistLinkClicked(sender As Object, e As LinkClickedEventArgs) Handles txtArtistInfo.LinkClicked
        Process.Start(e.LinkText)
    End Sub

    Private Sub ArtistTopTrackClick(sender As Label, e As MouseEventArgs) Handles lblArtistTopTrackTitle1.Click, lblArtistTopTrackTitle2.Click, lblArtistTopTrackTitle3.Click, lblArtistTopTrackTitle4.Click, lblArtistTopTrackTitle5.Click, lblArtistTopTrackTitle6.Click, lblArtistTopTrackTitle7.Click, lblArtistTopTrackTitle8.Click, lblArtistTopTrackTitle9.Click, lblArtistTopTrackTitle10.Click,
            lblArtistTopTrackTitle11.Click, lblArtistTopTrackTitle12.Click, lblArtistTopTrackTitle13.Click, lblArtistTopTrackTitle14.Click, lblArtistTopTrackTitle15.Click, lblArtistTopTrackTitle16.Click, lblArtistTopTrackTitle17.Click, lblArtistTopTrackTitle18.Click, lblArtistTopTrackTitle19.Click, lblArtistTopTrackTitle20.Click
        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False Then
            GoToTrack(sender.Text, txtArtistInfo.Text.Split(vbLf)(1))
        End If
    End Sub

    Private Sub ArtistTopAlbumClick(sender As Label, e As MouseEventArgs) Handles lblArtistTopAlbum1.Click, lblArtistTopAlbum2.Click, lblArtistTopAlbum3.Click, lblArtistTopAlbum4.Click, lblArtistTopAlbum5.Click, lblArtistTopAlbum6.Click, lblArtistTopAlbum7.Click, lblArtistTopAlbum8.Click, lblArtistTopAlbum9.Click, lblArtistTopAlbum10.Click
        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False Then
            GoToAlbum(sender.Text, txtArtistInfo.Text.Split(vbLf)(1))
        End If
    End Sub
#End Region

#Region "Album UI"
    Private Sub AlbumGo(sender As Object, e As EventArgs) Handles btnAlbumGo.Click
        If txtAlbumTitle.Text = String.Empty OrElse txtAlbumArtist.Text = String.Empty Then
            MessageBox.Show("You must enter data into both the Title and Artist fields in order to use this", "album Lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            If bgwAlbumUpdater.IsBusy = False Then
                bgwAlbumUpdater.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub AlbumAddTag(sender As Object, e As EventArgs) Handles btnAlbumTagAdd.Click
        ' stop if no album is loaded
        If albumlookup(0) = Nothing Then
            Exit Sub
        End If

        ' get input
        Dim tags As String = InputBox("Please enter tag(s), multiple can be added by separating them with commas (10 max).", "Enter Tags")

        ' halt if no input
        If tags = String.Empty Then
            Exit Sub
        End If

        ' remove spaces after commas
        If tags.Contains(",") = True Then
            Dim commaindex As UShort = 1
            For count As Byte = 1 To StrCount(tags, ",")
                ' find comma index
                commaindex = InStr(commaindex + 1, tags, ",")

                ' get rid of spaces
                If tags(commaindex) = " "c Then
                    tags = tags.Remove(commaindex, 1)
                End If
            Next
        End If

        ' call api
        Dim tagresponse As String = CallAPIAuth("album.addTags", "album=" & albumlookup(0), "artist=" & albumlookup(1), "tags=" & tags)

        ' check for success
        If tagresponse.Contains("ok") = True Then
            btnAlbumGo.PerformClick()
        Else
            MessageBox.Show("Error while attempting to add tags. Please make sure you are using commas to separate tags and you are not using any special characters.",
                            "Add Tags", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub AlbumRemoveTag(sender As Object, e As EventArgs) Handles btnAlbumTagRemove.Click
        ' stop if no album is loaded or no tag selected
        If lstAlbumUserTags.SelectedIndex = -1 OrElse albumlookup(0) = Nothing Then
            Exit Sub
        End If

        ' call api
        Dim tagresponse As String = CallAPIAuth("album.removeTag", "album=" & albumlookup(0), "artist=" & albumlookup(1), "tag=" & lstAlbumUserTags.SelectedItem.ToString)

        ' check for success
        If tagresponse.Contains("ok") = True Then
            lstAlbumUserTags.Items.RemoveAt(lstAlbumUserTags.SelectedIndex) ' remove from list box
        Else
            MessageBox.Show("Error while attempting to remove tag. Please try again later.", "Remove Tag", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub AlbumAdvancedClicked(sender As Object, e As EventArgs) Handles btnAlbumAdvanced.Click
        frmAlbumAdvanced.Show()
        frmAlbumAdvanced.Activate()
    End Sub

    Private Sub AlbumTrackClicked(sender As Object, e As EventArgs) Handles ltvAlbumTrackList.ItemActivate
        GoToTrack(ltvAlbumTrackList.SelectedItems(0).SubItems(1).Text, albumlookup(1))
    End Sub

    Private Sub AlbumGoArtist(sender As Object, e As EventArgs) Handles btnAlbumGoArtist.Click
        GoToArtist(albumlookup(1))
    End Sub

    Private Sub AlbumLinkClicked(sender As Object, e As LinkClickedEventArgs) Handles txtAlbumInfo.LinkClicked
        Process.Start(e.LinkText)
    End Sub
#End Region

#Region "User UI"
    ' set user
    Private Sub SetUser(sender As Object, e As EventArgs) Handles btnUserSet.Click
        ' set user var
        Dim userinput As String = txtUser.Text.Trim

        ' validation process
        lblUserStatus.Text = "Validating..."
        ' attempt to get user info to check if user exists
        If CallAPI("user.getInfo", userinput).Contains("ERROR:") = False Then
            ' if validation succeeded
            My.Settings.User = userinput                          ' set user variable
            lblUserStatus.Text = "Welcome, " & My.Settings.User   ' update user status label
            lblUser.Text = "Current User: " & My.Settings.User    ' update current user label
            Me.Text = "Audiograph - " & My.Settings.User          ' update form name

            ' get rid of previous session key and authenticated state if applicable
            My.Settings.SessionKey = String.Empty
            My.Settings.Save()
            AuthenticatedUI(False)

            ' update all
            UpdateAll()

            ' enable authentication button
            btnUserAuthenticate.Enabled = True
        Else
            ' if validation failed
            lblUserStatus.Text = "User Cannot Be Found"
            txtUser.SelectAll()
        End If
    End Sub

    ' keep user pic square when splitter is moved
    Private Sub PicUserScaling(sender As Object, e As EventArgs) Handles picUser.Resize
        picUser.Height = picUser.Width
    End Sub

    ' change user loved tracks page
    Private Sub UserLovedTracksPageChanged(sender As Object, e As EventArgs) Handles nudUserLovedPage.ValueChanged
        ' stop this from running on startup
        Static first As Boolean = True
        If stoploadexecution = False AndAlso first = False AndAlso bgwUserLovedUpdater.IsBusy = False Then
            bgwUserLovedUpdater.RunWorkerAsync()
        End If
        first = False
    End Sub

    Private Sub UserHistoryPageChanged(sender As Object, e As EventArgs) Handles nudUserHistoryPage.ValueChanged
        ' stop this from running on startup
        Static first As Boolean = True
        If stoploadexecution = False AndAlso first = False AndAlso bgwUserHistoryUpdater.IsBusy = False Then
            bgwUserHistoryUpdater.RunWorkerAsync()
        End If
        first = False
    End Sub

    Private Sub UserInfoTabScaling(sender As Object, e As EventArgs) Handles tbcUserInfo.SelectedIndexChanged
        ' user friends
        ltvUserFriends.Width = pgUserFriends.Width - 6
        ltvUserFriends.Height = pgUserFriends.Height - 35

        ' user loved tracks
        ltvUserLovedTracks.Width = pgUserLovedTracks.Width - 6
        ltvUserLovedTracks.Height = pgUserLovedTracks.Height - 35
    End Sub

    Private Sub UserLinkClicked(sender As Object, e As LinkClickedEventArgs) Handles txtUserInfo.LinkClicked
        Process.Start(e.LinkText)
    End Sub

    Private Sub UserPictureClicked(sender As Object, e As MouseEventArgs) Handles picUser.MouseDown
        If e.Button = MouseButtons.Left AndAlso picUser.ImageLocation.Contains("http") = True Then
            Process.Start(picUser.ImageLocation)
        End If
    End Sub

    Private Sub UserLovedSongClick(sender As Object, e As EventArgs) Handles ltvUserLovedTracks.ItemActivate
        GoToTrack(ltvUserLovedTracks.SelectedItems(0).SubItems(0).Text, ltvUserLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserFriendClick(sender As Object, e As EventArgs) Handles ltvUserFriends.ItemActivate
        tabControl.SelectedIndex = 6
        txtUserL.Text = ltvUserFriends.SelectedItems(0).Text
        btnUserLSet.PerformClick()
    End Sub

    Private Sub UserChartRad(sender As Object, e As EventArgs) Handles radUserAllTime.CheckedChanged
        If radUserAllTime.Checked = True Then
            dtpUserFrom.Enabled = False
            dtpUserTo.Enabled = False
            lblUserFrom.Enabled = False
            lblUserTo.Enabled = False
        Else
            dtpUserFrom.Enabled = True
            dtpUserTo.Enabled = True
            lblUserFrom.Enabled = True
            lblUserTo.Enabled = True
        End If
    End Sub

    Private Sub UserChartGo(sender As Object, e As EventArgs) Handles btnUserChartGo.Click
        ' notify user and do nothing if there is no user set
        If My.Settings.User = String.Empty Then
            MessageBox.Show("You must set user before attempting to use this", "User Charts", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            nudUserHistoryPage.Value = nudUserHistoryPage.Minimum
            If stoploadexecution = False AndAlso bgwUserChartUpdater.IsBusy = False Then
                bgwUserChartUpdater.RunWorkerAsync()
                If bgwUserHistoryUpdater.IsBusy = False Then
                    bgwUserHistoryUpdater.RunWorkerAsync()
                End If
            End If
        End If
    End Sub

    Private Sub UserAuthenticate(sender As Object, e As EventArgs) Handles btnUserAuthenticate.Click
        frmAuthentication.Show()
    End Sub

    Private Sub UserRecentTrackClick(sender As Label, e As MouseEventArgs) Handles lblUserRecentTitle1.Click, lblUserRecentTitle2.Click, lblUserRecentTitle3.Click, lblUserRecentTitle4.Click, lblUserRecentTitle5.Click, lblUserRecentTitle6.Click, lblUserRecentTitle7.Click, lblUserRecentTitle8.Click,
            lblUserRecentTitle9.Click, lblUserRecentTitle10.Click, lblUserRecentTitle11.Click, lblUserRecentTitle12.Click, lblUserRecentTitle13.Click, lblUserRecentTitle14.Click, lblUserRecentTitle15.Click, lblUserRecentTitle16.Click, lblUserRecentTitle17.Click, lblUserRecentTitle18.Click, lblUserRecentTitle19.Click, lblUserRecentTitle20.Click
        Dim row As Byte = GetUserRecentRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserRecentLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserRecentLabel(row, 0).Text.Contains("ERROR: ") = False AndAlso UserRecentLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserRecentLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToTrack(UserRecentLabel(row, 0).Text, UserRecentLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserRecentArtistClick(sender As Label, e As MouseEventArgs) Handles lblUserRecentArtist1.Click, lblUserRecentArtist2.Click, lblUserRecentArtist3.Click, lblUserRecentArtist4.Click, lblUserRecentArtist5.Click, lblUserRecentArtist6.Click, lblUserRecentArtist7.Click, lblUserRecentArtist8.Click,
            lblUserRecentArtist9.Click, lblUserRecentArtist10.Click, lblUserRecentArtist11.Click, lblUserRecentArtist12.Click, lblUserRecentArtist13.Click, lblUserRecentArtist14.Click, lblUserRecentArtist15.Click, lblUserRecentArtist16.Click, lblUserRecentArtist17.Click, lblUserRecentArtist18.Click, lblUserRecentArtist19.Click, lblUserRecentArtist20.Click
        Dim row As Byte = GetUserRecentRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False Then
            GoToArtist(UserRecentLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserRecentAlbumClick(sender As Label, e As MouseEventArgs) Handles lblUserRecentAlbum1.Click, lblUserRecentAlbum2.Click, lblUserRecentAlbum3.Click, lblUserRecentAlbum4.Click, lblUserRecentAlbum5.Click, lblUserRecentAlbum6.Click, lblUserRecentAlbum7.Click, lblUserRecentAlbum8.Click,
            lblUserRecentAlbum9.Click, lblUserRecentAlbum10.Click, lblUserRecentAlbum11.Click, lblUserRecentAlbum12.Click, lblUserRecentAlbum13.Click, lblUserRecentAlbum14.Click, lblUserRecentAlbum15.Click, lblUserRecentAlbum16.Click, lblUserRecentAlbum17.Click, lblUserRecentAlbum18.Click, lblUserRecentAlbum19.Click, lblUserRecentAlbum20.Click
        Dim row As Byte = GetUserRecentRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserRecentLabel(row, 2).Text.Contains("(Unavailable)") = False AndAlso UserRecentLabel(row, 2).Text.Contains("ERROR: ") = False AndAlso UserRecentLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserRecentLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToAlbum(UserRecentLabel(row, 2).Text, UserRecentLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserTopTrackTrackClicked(sender As Label, e As MouseEventArgs) Handles lblUserTopTrackTitle1.Click, lblUserTopTrackTitle2.Click, lblUserTopTrackTitle3.Click, lblUserTopTrackTitle4.Click, lblUserTopTrackTitle5.Click, lblUserTopTrackTitle6.Click, lblUserTopTrackTitle7.Click, lblUserTopTrackTitle8.Click,
            lblUserTopTrackTitle9.Click, lblUserTopTrackTitle10.Click, lblUserTopTrackTitle11.Click, lblUserTopTrackTitle12.Click, lblUserTopTrackTitle13.Click, lblUserTopTrackTitle14.Click, lblUserTopTrackTitle15.Click, lblUserTopTrackTitle16.Click, lblUserTopTrackTitle17.Click, lblUserTopTrackTitle18.Click, lblUserTopTrackTitle19.Click, lblUserTopTrackTitle20.Click
        Dim row As Byte = GetUserTopTrackRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserTopTracksLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserTopTracksLabel(row, 0).Text.Contains("ERROR: ") = False AndAlso UserTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserTopTracksLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToTrack(UserTopTracksLabel(row, 0).Text, UserTopTracksLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserTopTrackArtistClicked(sender As Label, e As MouseEventArgs) Handles lblUserTopTrackArtist1.Click, lblUserTopTrackArtist2.Click, lblUserTopTrackArtist3.Click, lblUserTopTrackArtist4.Click, lblUserTopTrackArtist5.Click, lblUserTopTrackArtist6.Click, lblUserTopTrackArtist7.Click, lblUserTopTrackArtist8.Click,
            lblUserTopTrackArtist9.Click, lblUserTopTrackArtist10.Click, lblUserTopTrackArtist11.Click, lblUserTopTrackArtist12.Click, lblUserTopTrackArtist13.Click, lblUserTopTrackArtist14.Click, lblUserTopTrackArtist15.Click, lblUserTopTrackArtist16.Click, lblUserTopTrackArtist17.Click, lblUserTopTrackArtist18.Click, lblUserTopTrackArtist19.Click, lblUserTopTrackArtist20.Click
        Dim row As Byte = GetUserTopTrackRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserTopTracksLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToArtist(UserTopTracksLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserTopTrackAlbumClicked(sender As Label, e As MouseEventArgs) Handles lblUserTopTrackAlbum1.Click, lblUserTopTrackAlbum2.Click, lblUserTopTrackAlbum3.Click, lblUserTopTrackAlbum4.Click, lblUserTopTrackAlbum5.Click, lblUserTopTrackAlbum6.Click, lblUserTopTrackAlbum7.Click, lblUserTopTrackAlbum8.Click,
            lblUserTopTrackAlbum9.Click, lblUserTopTrackAlbum10.Click, lblUserTopTrackAlbum11.Click, lblUserTopTrackAlbum12.Click, lblUserTopTrackAlbum13.Click, lblUserTopTrackAlbum14.Click, lblUserTopTrackAlbum15.Click, lblUserTopTrackAlbum16.Click, lblUserTopTrackAlbum17.Click, lblUserTopTrackAlbum18.Click, lblUserTopTrackAlbum19.Click, lblUserTopTrackAlbum20.Click
        Dim row As Byte = GetUserTopTrackRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserTopTracksLabel(row, 2).Text.Contains("(Unavailable)") = False AndAlso UserTopTracksLabel(row, 2).Text.Contains("ERROR: ") = False AndAlso UserTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserTopTracksLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToAlbum(UserTopTracksLabel(row, 2).Text, UserTopTracksLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserTopArtistClicked(sender As Label, e As MouseEventArgs) Handles lblUserTopArtist1.Click, lblUserTopArtist2.Click, lblUserTopArtist3.Click, lblUserTopArtist4.Click, lblUserTopArtist5.Click, lblUserTopArtist6.Click, lblUserTopArtist7.Click, lblUserTopArtist8.Click,
            lblUserTopArtist9.Click, lblUserTopArtist10.Click, lblUserTopArtist11.Click, lblUserTopArtist12.Click, lblUserTopArtist13.Click, lblUserTopArtist14.Click, lblUserTopArtist15.Click, lblUserTopArtist16.Click, lblUserTopArtist17.Click, lblUserTopArtist18.Click, lblUserTopArtist19.Click, lblUserTopArtist20.Click
        Dim row As Byte = GetUserTopArtistRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserTopArtistsLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserTopArtistsLabel(row, 0).Text.Contains("ERROR: ") = False Then
            GoToArtist(UserTopArtistsLabel(row, 0).Text)
        End If
    End Sub

    Private Sub UserTopAlbumAlbumClicked(sender As Label, e As MouseEventArgs) Handles lblUserTopAlbum1.Click, lblUserTopAlbum2.Click, lblUserTopAlbum3.Click, lblUserTopAlbum4.Click, lblUserTopAlbum5.Click, lblUserTopAlbum6.Click, lblUserTopAlbum7.Click, lblUserTopAlbum8.Click,
            lblUserTopAlbum9.Click, lblUserTopAlbum10.Click, lblUserTopAlbum11.Click, lblUserTopAlbum12.Click, lblUserTopAlbum13.Click, lblUserTopAlbum14.Click, lblUserTopAlbum15.Click, lblUserTopAlbum16.Click, lblUserTopAlbum17.Click, lblUserTopAlbum18.Click, lblUserTopAlbum19.Click, lblUserTopAlbum20.Click
        Dim row As Byte = GetUserTopAlbumRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserTopAlbumsLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserTopAlbumsLabel(row, 0).Text.Contains("ERROR: ") = False AndAlso UserTopAlbumsLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserTopAlbumsLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToAlbum(UserTopAlbumsLabel(row, 0).Text, UserTopAlbumsLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserTopAlbumArtistClicked(sender As Label, e As MouseEventArgs) Handles lblUserTopAlbumArtist1.Click, lblUserTopAlbumArtist2.Click, lblUserTopAlbumArtist3.Click, lblUserTopAlbumArtist4.Click, lblUserTopAlbumArtist5.Click, lblUserTopAlbumArtist6.Click, lblUserTopAlbumArtist7.Click, lblUserTopAlbumArtist8.Click,
            lblUserTopAlbum9.Click, lblUserTopAlbum10.Click, lblUserTopAlbum11.Click, lblUserTopAlbum12.Click, lblUserTopAlbum13.Click, lblUserTopAlbum14.Click, lblUserTopAlbum15.Click, lblUserTopAlbum16.Click, lblUserTopAlbum17.Click, lblUserTopAlbum18.Click, lblUserTopAlbum19.Click, lblUserTopAlbum20.Click
        Dim row As Byte = GetUserTopAlbumRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserTopAlbumsLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserTopAlbumsLabel(row, 0).Text.Contains("ERROR: ") = False Then
            GoToArtist(UserTopAlbumsLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserHistoryClicked(sender As Object, e As EventArgs) Handles ltvUserHistory.ItemActivate
        GoToTrack(ltvUserHistory.SelectedItems(0).SubItems(0).Text, ltvUserHistory.SelectedItems(0).SubItems(1).Text)
    End Sub
#End Region

#Region "UserL UI"
    ' set user
    Private Sub SetUserL(sender As Object, e As EventArgs) Handles btnUserLSet.Click
        ' set user var
        Dim userinput As String = txtUserL.Text.Trim

        ' validation process
        lblUserLStatus.Text = "Validating..."
        ' attempt to get user info to check if user exists
        If CallAPI("user.getInfo", userinput).Contains("ERROR:") = False Then
            ' if validation succeeded
            userlookup = userinput                          ' set user variable
            lblUserLStatus.Text = "Lookup: " & userinput    ' update user status label

            ' update userlookup tab
            If bgwUserLookupUpdater.IsBusy = False Then
                bgwUserLookupUpdater.RunWorkerAsync()
            End If
        Else
            ' if validation failed
            lblUserLStatus.Text = "User Cannot Be Found"
            txtUser.SelectAll()
        End If
    End Sub

    Private Sub PicUserLScaling(sender As Object, e As EventArgs) Handles picUserL.Resize
        picUserL.Height = picUserL.Width
    End Sub

    ' change user loved tracks page
    Private Sub UserLLovedTracksPageChanged(sender As Object, e As EventArgs) Handles nudUserLLovedPage.ValueChanged
        If stoploadexecution = False AndAlso bgwUserLLovedUpdater.IsBusy = False Then
            bgwUserLLovedUpdater.RunWorkerAsync()
        End If
    End Sub

    Private Sub UserLHistoryPageChanged(sender As Object, e As EventArgs) Handles nudUserLHistoryPage.ValueChanged
        ' stop this from running on startup
        Static first As Boolean = True
        If stoploadexecution = False AndAlso first = False AndAlso bgwUserLHistoryUpdater.IsBusy = False Then
            bgwUserLHistoryUpdater.RunWorkerAsync()
        End If
        first = False
    End Sub

    Private Sub UserLInfoTabScaling(sender As Object, e As EventArgs) Handles tbcUserLInfo.SelectedIndexChanged
        ' user loved tracks
        ltvUserLLovedTracks.Width = pgUserLLovedTracks.Width - 6
        ltvUserLLovedTracks.Height = pgUserLLovedTracks.Height - 35

        'user friends
        ltvUserLFriends.Width = pgUserLFriends.Width - 6
        ltvUserLFriends.Height = pgUserLFriends.Height - 35
    End Sub

    ' userl profile link clicked
    Private Sub UserLLinkClicked(sender As Object, e As LinkClickedEventArgs) Handles txtUserLInfo.LinkClicked
        Process.Start(e.LinkText)
    End Sub

    Private Sub UserLPictureClicked(sender As Object, e As MouseEventArgs) Handles picUserL.MouseDown
        If e.Button = MouseButtons.Left AndAlso picUserL.ImageLocation.Contains("http") = True Then
            Process.Start(picUserL.ImageLocation)
        End If
    End Sub

    Private Sub UserLLovedSongClick(sender As Object, e As EventArgs) Handles ltvUserLLovedTracks.ItemActivate
        GoToTrack(ltvUserLLovedTracks.SelectedItems(0).SubItems(0).Text, ltvUserLLovedTracks.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UserLFriendClick(sender As Object, e As EventArgs) Handles ltvUserLFriends.ItemActivate
        txtUserL.Text = ltvUserLFriends.SelectedItems(0).Text
        btnUserLSet.PerformClick()
    End Sub

    Private Sub UserLChartRad(sender As Object, e As EventArgs) Handles radUserLAllTime.CheckedChanged
        If radUserLAllTime.Checked = True Then
            dtpUserLFrom.Enabled = False
            dtpUserLTo.Enabled = False
            lblUserLFrom.Enabled = False
            lblUserLTo.Enabled = False
        Else
            dtpUserLFrom.Enabled = True
            dtpUserLTo.Enabled = True
            lblUserLFrom.Enabled = True
            lblUserLTo.Enabled = True
        End If
    End Sub

    Private Sub UserLChartGo(sender As Object, e As EventArgs) Handles btnUserLChartGo.Click
        ' notify user and do nothing if there is no user set
        If userlookup = String.Empty Then
            MessageBox.Show("You must set user before attempting to use this", "User Lookup Charts", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If bgwUserLChartUpdater.IsBusy = False Then
                bgwUserLChartUpdater.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub UserLRecentTrackClick(sender As Label, e As MouseEventArgs) Handles lblUserLRecentTitle1.Click, lblUserLRecentTitle2.Click, lblUserLRecentTitle3.Click, lblUserLRecentTitle4.Click, lblUserLRecentTitle5.Click, lblUserLRecentTitle6.Click, lblUserLRecentTitle7.Click, lblUserLRecentTitle8.Click,
            lblUserLRecentTitle9.Click, lblUserLRecentTitle10.Click, lblUserLRecentTitle11.Click, lblUserLRecentTitle12.Click, lblUserLRecentTitle13.Click, lblUserLRecentTitle14.Click, lblUserLRecentTitle15.Click, lblUserLRecentTitle16.Click, lblUserLRecentTitle17.Click, lblUserLRecentTitle18.Click, lblUserLRecentTitle19.Click, lblUserLRecentTitle20.Click
        Dim row As Byte = GetUserRecentRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLRecentLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserLRecentLabel(row, 0).Text.Contains("ERROR: ") = False AndAlso UserLRecentLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserLRecentLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToTrack(UserLRecentLabel(row, 0).Text, UserLRecentLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLRecentArtistClick(sender As Label, e As MouseEventArgs) Handles lblUserLRecentArtist1.Click, lblUserLRecentArtist2.Click, lblUserLRecentArtist3.Click, lblUserLRecentArtist4.Click, lblUserLRecentArtist5.Click, lblUserLRecentArtist6.Click, lblUserLRecentArtist7.Click, lblUserLRecentArtist8.Click,
            lblUserLRecentArtist9.Click, lblUserLRecentArtist10.Click, lblUserLRecentArtist11.Click, lblUserLRecentArtist12.Click, lblUserLRecentArtist13.Click, lblUserLRecentArtist14.Click, lblUserLRecentArtist15.Click, lblUserLRecentArtist16.Click, lblUserLRecentArtist17.Click, lblUserLRecentArtist18.Click, lblUserLRecentArtist19.Click, lblUserLRecentArtist20.Click
        Dim row As Byte = GetUserRecentRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso sender.Text.Contains("(Unavailable)") = False AndAlso sender.Text.Contains("ERROR: ") = False Then
            GoToArtist(UserLRecentLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLRecentAlbumClick(sender As Label, e As MouseEventArgs) Handles lblUserLRecentAlbum1.Click, lblUserLRecentAlbum2.Click, lblUserLRecentAlbum3.Click, lblUserLRecentAlbum4.Click, lblUserLRecentAlbum5.Click, lblUserLRecentAlbum6.Click, lblUserLRecentAlbum7.Click, lblUserLRecentAlbum8.Click,
            lblUserLRecentAlbum9.Click, lblUserLRecentAlbum10.Click, lblUserLRecentAlbum11.Click, lblUserLRecentAlbum12.Click, lblUserLRecentAlbum13.Click, lblUserLRecentAlbum14.Click, lblUserLRecentAlbum15.Click, lblUserLRecentAlbum16.Click, lblUserLRecentAlbum17.Click, lblUserLRecentAlbum18.Click, lblUserLRecentAlbum19.Click, lblUserLRecentAlbum20.Click
        Dim row As Byte = GetUserRecentRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLRecentLabel(row, 2).Text.Contains("(Unavailable)") = False AndAlso UserLRecentLabel(row, 2).Text.Contains("ERROR: ") = False AndAlso UserLRecentLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserLRecentLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToAlbum(UserLRecentLabel(row, 2).Text, UserLRecentLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLTopTrackTrackClicked(sender As Label, e As MouseEventArgs) Handles lblUserLTopTrackTitle1.Click, lblUserLTopTrackTitle2.Click, lblUserLTopTrackTitle3.Click, lblUserLTopTrackTitle4.Click, lblUserLTopTrackTitle5.Click, lblUserLTopTrackTitle6.Click, lblUserLTopTrackTitle7.Click, lblUserLTopTrackTitle8.Click,
            lblUserLTopTrackTitle9.Click, lblUserLTopTrackTitle10.Click, lblUserLTopTrackTitle11.Click, lblUserLTopTrackTitle12.Click, lblUserLTopTrackTitle13.Click, lblUserLTopTrackTitle14.Click, lblUserLTopTrackTitle15.Click, lblUserLTopTrackTitle16.Click, lblUserLTopTrackTitle17.Click, lblUserLTopTrackTitle18.Click, lblUserLTopTrackTitle19.Click, lblUserLTopTrackTitle20.Click
        Dim row As Byte = GetUserTopTrackRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLTopTracksLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserLTopTracksLabel(row, 0).Text.Contains("ERROR: ") = False AndAlso UserLTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserLTopTracksLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToTrack(UserLTopTracksLabel(row, 0).Text, UserLTopTracksLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLTopTrackArtistClicked(sender As Label, e As MouseEventArgs) Handles lblUserLTopTrackArtist1.Click, lblUserLTopTrackArtist2.Click, lblUserLTopTrackArtist3.Click, lblUserLTopTrackArtist4.Click, lblUserLTopTrackArtist5.Click, lblUserLTopTrackArtist6.Click, lblUserLTopTrackArtist7.Click, lblUserLTopTrackArtist8.Click,
            lblUserLTopTrackArtist9.Click, lblUserLTopTrackArtist10.Click, lblUserLTopTrackArtist11.Click, lblUserLTopTrackArtist12.Click, lblUserLTopTrackArtist13.Click, lblUserLTopTrackArtist14.Click, lblUserLTopTrackArtist15.Click, lblUserLTopTrackArtist16.Click, lblUserLTopTrackArtist17.Click, lblUserLTopTrackArtist18.Click, lblUserLTopTrackArtist19.Click, lblUserLTopTrackArtist20.Click
        Dim row As Byte = GetUserTopTrackRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserLTopTracksLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToArtist(UserLTopTracksLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLTopTrackAlbumClicked(sender As Label, e As MouseEventArgs) Handles lblUserLTopTrackAlbum1.Click, lblUserLTopTrackAlbum2.Click, lblUserLTopTrackAlbum3.Click, lblUserLTopTrackAlbum4.Click, lblUserLTopTrackAlbum5.Click, lblUserLTopTrackAlbum6.Click, lblUserLTopTrackAlbum7.Click, lblUserLTopTrackAlbum8.Click,
            lblUserLTopTrackAlbum9.Click, lblUserLTopTrackAlbum10.Click, lblUserLTopTrackAlbum11.Click, lblUserLTopTrackAlbum12.Click, lblUserLTopTrackAlbum13.Click, lblUserLTopTrackAlbum14.Click, lblUserLTopTrackAlbum15.Click, lblUserLTopTrackAlbum16.Click, lblUserLTopTrackAlbum17.Click, lblUserLTopTrackAlbum18.Click, lblUserLTopTrackAlbum19.Click, lblUserLTopTrackAlbum20.Click
        Dim row As Byte = GetUserTopTrackRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLTopTracksLabel(row, 2).Text.Contains("(Unavailable)") = False AndAlso UserLTopTracksLabel(row, 2).Text.Contains("ERROR: ") = False AndAlso UserLTopTracksLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserLTopTracksLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToAlbum(UserLTopTracksLabel(row, 2).Text, UserLTopTracksLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLTopArtistClicked(sender As Label, e As MouseEventArgs) Handles lblUserLTopArtist1.Click, lblUserLTopArtist2.Click, lblUserLTopArtist3.Click, lblUserLTopArtist4.Click, lblUserLTopArtist5.Click, lblUserLTopArtist6.Click, lblUserLTopArtist7.Click, lblUserLTopArtist8.Click,
            lblUserLTopArtist9.Click, lblUserLTopArtist10.Click, lblUserLTopArtist11.Click, lblUserLTopArtist12.Click, lblUserLTopArtist13.Click, lblUserLTopArtist14.Click, lblUserLTopArtist15.Click, lblUserLTopArtist16.Click, lblUserLTopArtist17.Click, lblUserLTopArtist18.Click, lblUserLTopArtist19.Click, lblUserLTopArtist20.Click
        Dim row As Byte = GetUserTopArtistRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLTopArtistsLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserLTopArtistsLabel(row, 0).Text.Contains("ERROR: ") = False Then
            GoToArtist(UserLTopArtistsLabel(row, 0).Text)
        End If
    End Sub

    Private Sub UserLTopAlbumAlbumClicked(sender As Label, e As MouseEventArgs) Handles lblUserLTopAlbum1.Click, lblUserLTopAlbum2.Click, lblUserLTopAlbum3.Click, lblUserLTopAlbum4.Click, lblUserLTopAlbum5.Click, lblUserLTopAlbum6.Click, lblUserLTopAlbum7.Click, lblUserLTopAlbum8.Click,
            lblUserLTopAlbum9.Click, lblUserLTopAlbum10.Click, lblUserLTopAlbum11.Click, lblUserLTopAlbum12.Click, lblUserLTopAlbum13.Click, lblUserLTopAlbum14.Click, lblUserLTopAlbum15.Click, lblUserLTopAlbum16.Click, lblUserLTopAlbum17.Click, lblUserLTopAlbum18.Click, lblUserLTopAlbum19.Click, lblUserLTopAlbum20.Click
        Dim row As Byte = GetUserTopAlbumRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLTopAlbumsLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserLTopAlbumsLabel(row, 0).Text.Contains("ERROR: ") = False AndAlso UserLTopAlbumsLabel(row, 1).Text.Contains("(Unavailable)") = False AndAlso UserLTopAlbumsLabel(row, 1).Text.Contains("ERROR: ") = False Then
            GoToAlbum(UserLTopAlbumsLabel(row, 0).Text, UserLTopAlbumsLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLTopAlbumArtistClicked(sender As Label, e As MouseEventArgs) Handles lblUserLTopAlbumArtist1.Click, lblUserLTopAlbumArtist2.Click, lblUserLTopAlbumArtist3.Click, lblUserLTopAlbumArtist4.Click, lblUserLTopAlbumArtist5.Click, lblUserLTopAlbumArtist6.Click, lblUserLTopAlbumArtist7.Click, lblUserLTopAlbumArtist8.Click,
            lblUserLTopAlbum9.Click, lblUserLTopAlbum10.Click, lblUserLTopAlbum11.Click, lblUserLTopAlbum12.Click, lblUserLTopAlbum13.Click, lblUserLTopAlbum14.Click, lblUserLTopAlbum15.Click, lblUserLTopAlbum16.Click, lblUserLTopAlbum17.Click, lblUserLTopAlbum18.Click, lblUserLTopAlbum19.Click, lblUserLTopAlbum20.Click
        Dim row As Byte = GetUserTopAlbumRowIndex(sender)

        If e.Button = MouseButtons.Left AndAlso UserLTopAlbumsLabel(row, 0).Text.Contains("(Unavailable)") = False AndAlso UserLTopAlbumsLabel(row, 0).Text.Contains("ERROR: ") = False Then
            GoToArtist(UserLTopAlbumsLabel(row, 1).Text)
        End If
    End Sub

    Private Sub UserLHistoryClicked(sender As Object, e As EventArgs) Handles ltvUserLHistory.ItemActivate
        GoToTrack(ltvUserLHistory.SelectedItems(0).SubItems(0).Text, ltvUserLHistory.SelectedItems(0).SubItems(1).Text)
    End Sub
#End Region

#Region "Media UI"
    Private Sub MediaFrmLoad(sender As Object, e As EventArgs) Handles Me.Load
        lblMediaVersion.Text = "v." & MediaPlayer.versionInfo
    End Sub

    Private Sub AddQueue(sender As Object, e As EventArgs) Handles btnMediaAddQ.Click
        frmAddToQueue.Show()
        frmAddToQueue.Activate()
    End Sub

    Private Sub NextQueue(sender As Object, e As EventArgs) Handles btnMediaNext.Click
        QueuePlay(0)
    End Sub

    Public Sub QueuePlay(item As Integer)
        ' check that there are things to play
        If ltvMediaQueue.Items.Count < 1 Then
            Exit Sub
        End If

        MediaPlayer.URL = ltvMediaQueue.Items(item).SubItems(1).Text
        QueueRemove({item})
        ' check if player is playing, if its not then turn on the timer
        If MediaPlayer.playState <> WMPPlayState.wmppsPlaying Then
            tmrMediaPlayer.Enabled = True
        End If
    End Sub

    Public Sub QueueRemove(index() As Integer)
        Array.Sort(index)           ' sort ascending

        ' if shuffle is on add item to list
        For count As Integer = 0 To index.Length - 1
            Dim originalindex As Byte
            If chkMediaShuffle.Checked = True Then
                ' filter out the number before adding to queue
                originalindex = InStr(ltvMediaQueue.Items(count).Text, " ") - 1
                queueremoved.Add({ltvMediaQueue.Items(count).Text.Substring(originalindex), ltvMediaQueue.Items(count).SubItems(1).Text})
            End If
        Next

        For count As Integer = 0 To index.Length - 1
            ltvMediaQueue.Items(index(count) - count).Remove()  ' remove at index minus count to adjust for deletion
        Next
        QueueRecount()
    End Sub

    ' re-orders the numbers of the queue items
    Public Sub QueueRecount()
        ' check that there is items in the queue, exit if none
        If ltvMediaQueue.Items.Count = 0 Then
            Exit Sub
        End If

        For count As Integer = 0 To ltvMediaQueue.Items.Count - 1
            Dim firstpos As Byte = InStr(ltvMediaQueue.Items(count).Text, " ")                        ' get pos of first space
            Dim newstring As String = ltvMediaQueue.Items(count).Text.Remove(0, firstpos)             ' remove old number
            ltvMediaQueue.Items(count).Text = newstring.Insert(0, (count + 1).ToString("N0") & " ")   ' add new number and space
        Next
    End Sub

    Public Sub QueueRecountImg()
        ' check that there is items in the queue, exit if none
        If ltvMediaQueue.Items.Count = 0 Then
            Exit Sub
        End If

        For count As Integer = 0 To ltvMediaQueue.Items.Count - 1
            Dim firstpos As Byte = InStr(ltvMediaQueue.Items(count).Text, " ")                        ' get pos of first space
            Dim newstring As String = ltvMediaQueue.Items(count).Text.Remove(0, firstpos)             ' remove old number
            ltvMediaQueue.Items(count).Text = newstring.Insert(0, (count + 1).ToString("N0") & " ")   ' add new number and space
            ' set image
            If ltvMediaQueue.Items(count).Text.ToLower.Contains(".mp3") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".aac") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".flac") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".wav") = True OrElse
                    ltvMediaQueue.Items(count).Text.ToLower.Contains(".wma") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".m4a") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".mid") = True Then    ' audio types
                ltvMediaQueue.Items(count).ImageIndex = 0
            ElseIf ltvMediaQueue.Items(count).Text.ToLower.Contains(".mp4") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".mov") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".mpeg") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".mpg") = True OrElse
                ltvMediaQueue.Items(count).Text.ToLower.Contains(".avi") = True OrElse ltvMediaQueue.Items(count).Text.ToLower.Contains(".wmv") = True Then ' video types
                ltvMediaQueue.Items(count).ImageIndex = 1
            Else
                ' anything else
                ltvMediaQueue.Items(count).ImageIndex = 2
            End If
        Next
    End Sub

    Private Sub DoubleClickQueue(sender As Object, e As EventArgs) Handles ltvMediaQueue.ItemActivate
        QueuePlay(ltvMediaQueue.SelectedItems(0).Index)
    End Sub

    Private Sub RemoveQueueBtn(sender As Object, e As EventArgs) Handles btnMediaRemoveQ.Click
        ' check that something is selected to remove, if not then exit sub
        If ltvMediaQueue.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Dim indexes As New List(Of Integer)
        ' get indexes of selected items
        For Each item As ListViewItem In ltvMediaQueue.SelectedItems
            indexes.Add(item.Index)
        Next
        QueueRemove(indexes.ToArray)
    End Sub

    Private Sub PlayStateChange(sender As Object, e As _WMPOCXEvents_PlayStateChangeEvent) Handles MediaPlayer.PlayStateChange
        Static ended As Boolean = False

        ' media ended
        If e.newState = WMPPlayState.wmppsMediaEnded Then
            ended = True
            ' hide play button
            separator2.Visible = False
            btnMediaPlay.Visible = False
            tmrMediaScrobble.Enabled = False
            newsong = True
            btnMediaPlay.Visible = False
            btnMediaPlay.Image = My.Resources.play
        End If

        ' repeat current
        If ended = True AndAlso e.newState = WMPPlayState.wmppsStopped AndAlso chkMediaRepeat.Checked = True Then
            MediaPlayer.Ctlcontrols.play()
            ended = False
            newsong = True
        End If

        ' song end
        If ended = True AndAlso e.newState = WMPPlayState.wmppsStopped AndAlso chkMediaRepeat.Checked = False Then
            QueuePlay(0)
            ended = False
            tmrMediaScrobble.Enabled = False
            newsong = True
            btnMediaPlay.Visible = False
            btnMediaPlay.Image = My.Resources.play
        End If

        ' user hits stop
        If ended = False AndAlso e.newState = WMPPlayState.wmppsStopped AndAlso chkMediaRepeat.Checked = False Then
            ended = True
            tmrMediaScrobble.Enabled = False
            newsong = True
            btnMediaPlay.Visible = False
            btnMediaPlay.Image = My.Resources.play
        End If

        ' paused
        If e.newState = WMPPlayState.wmppsPaused Then
            tmrMediaScrobble.Enabled = False
            btnMediaPlay.Image = My.Resources.play
        End If

        ' playing
        If e.newState = WMPPlayState.wmppsPlaying Then
            tmrMediaPlayer.Enabled = False
            ' show play button
            separator2.Visible = True
            btnMediaPlay.Visible = True
            btnMediaPlay.Image = My.Resources.pause
            ' enable scrobble check timer
            tmrMediaScrobble.Enabled = True
            ' update now playing
            If radMediaEnable.Checked = True AndAlso SearchIndex(GetFilename(MediaPlayer.URL)) IsNot Nothing AndAlso scrobbleindexdata.Count > 0 Then
                Dim data As String() = SearchIndex(GetFilename(MediaPlayer.URL))

                Dim th As New Thread(Sub()
                                         Dim results As String() = SearchIndex(GetFilename(MediaPlayer.URL))

                                         If results IsNot Nothing AndAlso scrobbleindexdata.Count > 0 Then
                                             CallAPIAuth("track.updateNowPlaying", "track=" & results(0), "artist=" & results(1), "album=" & results(2), "duration=" & Math.Round(MediaPlayer.currentMedia.duration - MediaPlayer.Ctlcontrols.currentPosition).ToString())
                                         End If
                                     End Sub)
                th.Name = "Media"
                th.Start()
            End If
        End If
    End Sub

    ' timer that repeatedly tells the media player to play after its stopped
    Private Sub TimerPlay(sender As Object, e As EventArgs) Handles tmrMediaPlayer.Tick
        MediaPlayer.Ctlcontrols.play()
    End Sub

    Private Sub MediaShuffle(sender As Object, e As EventArgs) Handles chkMediaShuffle.CheckedChanged
        Dim workingqueue As New List(Of String())       ' list to use for shuffle
        Dim shuffledqueue As New List(Of String())      ' final shuffled list
        Dim RNG As New Random
        Dim RNGnum As UInteger

        If addingqueue = True Then
            addingqueue = False
            Exit Sub
        End If

        If chkMediaShuffle.Checked = True Then  ' when shuffle is turned on
            originalqueue.Clear()
            For count As Integer = 0 To ltvMediaQueue.Items.Count - 1
                originalqueue.Add({ltvMediaQueue.Items(count).Text, ltvMediaQueue.Items(count).SubItems(1).Text}) ' save original queue
                workingqueue.Add({ltvMediaQueue.Items(count).Text, ltvMediaQueue.Items(count).SubItems(1).Text})  ' also add to working queue
            Next

            ' shuffle
            For Each item In ltvMediaQueue.Items
                RNGnum = RNG.Next(0, workingqueue.Count)
                shuffledqueue.Add(workingqueue(RNGnum))
                workingqueue.RemoveAt(RNGnum)
            Next

            ' add to listview
            ltvMediaQueue.Items.Clear()
            For Each item As String() In shuffledqueue
                ltvMediaQueue.Items.Add(item(0)).SubItems.Add(item(1))
            Next

            QueueRecountImg()
        Else    ' when shuffle is turned off
            ' replace listview items with original items
            ltvMediaQueue.Items.Clear()

            ' remove originalqueue items that correspond with queueremoved items
            If queueremoved.Count > 0 Then
                Dim originalindex As UShort    ' stores the index of the text after the number of the removed element
                Dim moditem() As String        ' array storing the modified item
                For Each item As String() In originalqueue.ToList()
                    originalindex = InStr(item(0), " ") - 1     ' get original index for substring
                    moditem = {item(0).Substring(originalindex), item(1)}

                    ' check if item is in queueremoved, remove it if it is
                    For Each removeditem As String() In queueremoved.ToList()   ' has to be tolist because of another dumb vb error
                        If removeditem(0) = moditem(0) Then
                            originalqueue.Remove(item)
                        End If
                    Next
                Next

                queueremoved.Clear()
            End If

            ' add to listview
            For Each item As String() In originalqueue
                ltvMediaQueue.Items.Add(item(0)).SubItems.Add(item(1))
            Next

            QueueRecountImg()
        End If
    End Sub

    Private Sub MediaPlayButton(sender As Object, e As EventArgs) Handles btnMediaPlay.Click
        If MediaPlayer.playState = WMPPlayState.wmppsPlaying Then
            btnMediaPlay.Image = My.Resources.play
            MediaPlayer.Ctlcontrols.pause()
        Else
            btnMediaPlay.Image = My.Resources.pause
            MediaPlayer.Ctlcontrols.play()
        End If
    End Sub

    Private Sub ButtonVerifyTrack(sender As Object, e As EventArgs) Handles btnMediaVerify.Click
        ' check that there is something in the boxes
        If txtMediaTitle.Text = String.Empty OrElse txtMediaArtist.Text = String.Empty Then
            lblMediaScrobble.Text = "Valid data must be entered in both the Track and Artist fields!"
            Exit Sub
        End If

        ' verify
        Dim info As String() = VerifyTrack(txtMediaTitle.Text.Trim, txtMediaArtist.Text.Trim)
        ' if cannot be found
        If info(0).Contains("ERROR: ") = True Then
            lblMediaScrobble.Text = "Track was unable to be verified."
        Else ' if was found
            lblMediaScrobble.Text = "Track verified as " + info(0) + " by " + info(1) + "."
            txtMediaTitle.Text = info(0)
            txtMediaArtist.Text = info(1)
            If info(2).Contains("ERROR:") = False Then
                txtMediaAlbum.Text = info(2)
            Else
                txtMediaAlbum.Text = String.Empty
            End If
        End If
    End Sub

    Private Sub ClickScrobble(sender As Object, e As EventArgs) Handles btnMediaScrobble.Click
        ' check that there is something in the boxes
        If txtMediaTitle.Text.Trim = String.Empty OrElse txtMediaArtist.Text.Trim = String.Empty Then
            MessageBox.Show("Valid data must be entered into the Title and Artist fields!", "Scrobble Track", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' set label
        lblMediaScrobble.Text = "Scrobbling..."

        ' get time
        Dim workingdate As Date = dtpMediaScrobble.Value.Date
        If cmbMediaTime.SelectedIndex = 0 Then
            ' am
            If nudMediaHour.Value < 12 Then
                workingdate = workingdate.AddHours(nudMediaHour.Value)
            End If
        Else
            ' pm
            If nudMediaHour.Value < 12 Then
                workingdate = workingdate.AddHours(nudMediaHour.Value + 12)
            Else
                workingdate = workingdate.AddHours(12)
            End If
        End If
        workingdate = workingdate.AddMinutes(nudMediaMinute.Value)

        ' scrobble
        Dim response As String() = Scrobble(txtMediaTitle.Text.Trim, txtMediaArtist.Text.Trim, DateToUnix(workingdate) - timezoneoffset, "User", txtMediaAlbum.Text.Trim)
        lblMediaScrobble.Text = response(4)
    End Sub

    Private Sub ScrobbleTimerTick(sender As Object, e As EventArgs) Handles tmrMediaScrobble.Tick
        ' analyze the current position of the playing media and decide if it must be scrobbled according to lfm protocol
        Dim totalDuration As Integer = MediaPlayer.currentMedia.duration
        Dim currentDuration As Integer = MediaPlayer.Ctlcontrols.currentPosition

        If radMediaEnable.Checked = True AndAlso newsong = True AndAlso (currentDuration > totalDuration / 2 OrElse currentDuration > 240) AndAlso totalDuration > 30 Then
            newsong = False
            Dim results As String() = SearchIndex(GetFilename(MediaPlayer.URL))

            If results IsNot Nothing Then
                ' update now playing
                If scrobbleindexdata.Count > 0 Then
                    CallAPIAuth("track.updateNowPlaying", "track=" & results(0), "artist=" & results(1), "album=" & results(2), "duration=" & Math.Round(MediaPlayer.currentMedia.duration - MediaPlayer.Ctlcontrols.currentPosition).ToString())
                End If

                ' scrobble
                Scrobble(results(0), results(1), GetCurrentUTC(), "Auto", results(2))
            End If
        End If
    End Sub

    Private Sub ExpandScrobbleHistory(sender As Object, e As EventArgs) Handles btnMediaExpand.Click
        frmScrobbleHistory.Show()
        frmScrobbleHistory.Activate()
    End Sub

    Private Sub ScrobbleSearchButton(sender As Object, e As EventArgs) Handles btnMediaSearch.Click
        frmScrobbleSearch.Show()
        frmScrobbleSearch.Activate()
    End Sub

    Private Sub ScrobbleIndexEditorMediaButton(sender As Object, e As EventArgs) Handles btnMediaEditIndex.Click
        frmScrobbleIndexEditor.Show()
        frmScrobbleIndexEditor.Activate()
        frmScrobbleIndexEditor.Open(My.Settings.CurrentScrobbleIndex)
    End Sub

    Public Sub LoadScrobbleIndex(location As String)
        ' init reader
        Try
            Using reader As New TextFieldParser(location)
                reader.TextFieldType = FieldType.Delimited
                reader.SetDelimiters(",")

                ' read file and gather errors if any

                scrobbleindexdata.Clear()
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
                            scrobbleindexdata.Add(currentRow)
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
                        MessageBox.Show("Scrobble index contains errors on some lines, these lines will be ignored by the program", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

                ' save file location
                My.Settings.CurrentScrobbleIndex = location

                ' change label
                lblMediaIndex.Text = "Using index " & Chr(34) & GetFilename(My.Settings.CurrentScrobbleIndex) & ".agsi" & Chr(34)

                ' enable button
                btnMediaEditIndex.Enabled = True

                ' change editor button if open
                If frmScrobbleIndexEditor.Visible = True Then
                    If location = frmScrobbleIndexEditor.currentIndexLocation Then
                        frmScrobbleIndexEditor.btnSetIndex.Enabled = False
                    Else
                        frmScrobbleIndexEditor.btnSetIndex.Enabled = True
                    End If
                End If
            End Using
        Catch ex As Exception
            ' if there was an error loading
            MessageBox.Show("Scrobble index " & Chr(34) & GetFilename(My.Settings.CurrentScrobbleIndex) & Chr(34) & " was unable to be loaded." & vbCrLf & "Please load a new scrobble index.",
                                "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' clear file location
            My.Settings.CurrentScrobbleIndex = String.Empty
            ' change label
            lblMediaIndex.Text = "No scrobble index loaded."
            ' disable button
            btnMediaEditIndex.Enabled = False
        End Try
    End Sub

    Private Sub IndexLoadClick(sender As Object, e As EventArgs) Handles btnMediaLoad.Click
        If ofdIndexLoad.ShowDialog() = DialogResult.OK Then
            LoadScrobbleIndex(ofdIndexLoad.FileName)
        End If
    End Sub

    Private Sub IndexCreateClick(sender As Object, e As EventArgs) Handles btnMediaCreate.Click
        frmScrobbleIndexEditor.Show()
        frmScrobbleIndexEditor.NewFile()
    End Sub

    Private Sub ChartArtistClick(sender As Object, e As EventArgs) Handles lblTopArtist9.Click, lblTopArtist8.Click, lblTopArtist7.Click, lblTopArtist6.Click, lblTopArtist5.Click, lblTopArtist4.Click, lblTopArtist3.Click, lblTopArtist20.Click, lblTopArtist2.Click, lblTopArtist19.Click, lblTopArtist18.Click, lblTopArtist17.Click, lblTopArtist16.Click, lblTopArtist15.Click, lblTopArtist14.Click, lblTopArtist13.Click, lblTopArtist12.Click, lblTopArtist11.Click, lblTopArtist10.Click, lblTopArtist1.Click

    End Sub

    Private Sub ChartTrackAlbumClick(sender As Object, e As EventArgs) Handles lblTopTrackAlbum9.Click, lblTopTrackAlbum8.Click, lblTopTrackAlbum7.Click, lblTopTrackAlbum6.Click, lblTopTrackAlbum5.Click, lblTopTrackAlbum4.Click, lblTopTrackAlbum3.Click, lblTopTrackAlbum20.Click, lblTopTrackAlbum2.Click, lblTopTrackAlbum19.Click, lblTopTrackAlbum18.Click, lblTopTrackAlbum17.Click, lblTopTrackAlbum16.Click, lblTopTrackAlbum15.Click, lblTopTrackAlbum14.Click, lblTopTrackAlbum13.Click, lblTopTrackAlbum12.Click, lblTopTrackAlbum11.Click, lblTopTrackAlbum10.Click, lblTopTrackAlbum1.Click

    End Sub

    Private Sub ChartTrackArtistClick(sender As Object, e As EventArgs) Handles lblTopTrackArtist9.Click, lblTopTrackArtist8.Click, lblTopTrackArtist7.Click, lblTopTrackArtist6.Click, lblTopTrackArtist5.Click, lblTopTrackArtist4.Click, lblTopTrackArtist3.Click, lblTopTrackArtist20.Click, lblTopTrackArtist2.Click, lblTopTrackArtist19.Click, lblTopTrackArtist18.Click, lblTopTrackArtist17.Click, lblTopTrackArtist16.Click, lblTopTrackArtist15.Click, lblTopTrackArtist14.Click, lblTopTrackArtist13.Click, lblTopTrackArtist12.Click, lblTopTrackArtist11.Click, lblTopTrackArtist10.Click, lblTopTrackArtist1.Click

    End Sub

    Private Sub ChartTrackTrackClick(sender As Object, e As EventArgs) Handles lblTopTrackTitle9.Click, lblTopTrackTitle8.Click, lblTopTrackTitle7.Click, lblTopTrackTitle6.Click, lblTopTrackTitle5.Click, lblTopTrackTitle4.Click, lblTopTrackTitle3.Click, lblTopTrackTitle20.Click, lblTopTrackTitle2.Click, lblTopTrackTitle19.Click, lblTopTrackTitle18.Click, lblTopTrackTitle17.Click, lblTopTrackTitle16.Click, lblTopTrackTitle15.Click, lblTopTrackTitle14.Click, lblTopTrackTitle13.Click, lblTopTrackTitle12.Click, lblTopTrackTitle11.Click, lblTopTrackTitle10.Click, lblTopTrackTitle1.Click

    End Sub

    Private Sub ArtClicked(sender As Object, e As MouseEventArgs) Handles picUserTopTrackArt9.MouseDown, picUserTopTrackArt8.MouseDown, picUserTopTrackArt7.MouseDown, picUserTopTrackArt6.MouseDown, picUserTopTrackArt5.MouseDown, picUserTopTrackArt4.MouseDown, picUserTopTrackArt3.MouseDown, picUserTopTrackArt20.MouseDown, picUserTopTrackArt2.MouseDown, picUserTopTrackArt19.MouseDown, picUserTopTrackArt18.MouseDown, picUserTopTrackArt17.MouseDown, picUserTopTrackArt16.MouseDown, picUserTopTrackArt15.MouseDown, picUserTopTrackArt14.MouseDown, picUserTopTrackArt13.MouseDown, picUserTopTrackArt12.MouseDown, picUserTopTrackArt11.MouseDown, picUserTopTrackArt10.MouseDown, picUserTopTrackArt1.MouseDown, picUserTopAlbumArt9.MouseDown, picUserTopAlbumArt8.MouseDown, picUserTopAlbumArt7.MouseDown, picUserTopAlbumArt6.MouseDown, picUserTopAlbumArt5.MouseDown, picUserTopAlbumArt4.MouseDown, picUserTopAlbumArt3.MouseDown, picUserTopAlbumArt20.MouseDown, picUserTopAlbumArt2.MouseDown, picUserTopAlbumArt19.MouseDown, picUserTopAlbumArt18.MouseDown, picUserTopAlbumArt17.MouseDown, picUserTopAlbumArt16.MouseDown, picUserTopAlbumArt15.MouseDown, picUserTopAlbumArt14.MouseDown, picUserTopAlbumArt13.MouseDown, picUserTopAlbumArt12.MouseDown, picUserTopAlbumArt11.MouseDown, picUserTopAlbumArt10.MouseDown, picUserTopAlbumArt1.MouseDown, picUserRecentArt9.MouseDown, picUserRecentArt8.MouseDown, picUserRecentArt7.MouseDown, picUserRecentArt6.MouseDown, picUserRecentArt5.MouseDown, picUserRecentArt4.MouseDown, picUserRecentArt3.MouseDown, picUserRecentArt20.MouseDown, picUserRecentArt2.MouseDown, picUserRecentArt19.MouseDown, picUserRecentArt18.MouseDown, picUserRecentArt17.MouseDown, picUserRecentArt16.MouseDown, picUserRecentArt15.MouseDown, picUserRecentArt14.MouseDown, picUserRecentArt13.MouseDown, picUserRecentArt12.MouseDown, picUserRecentArt11.MouseDown, picUserRecentArt10.MouseDown, picUserRecentArt1.MouseDown, picUserLTopTrackArt9.MouseDown, picUserLTopTrackArt8.MouseDown, picUserLTopTrackArt7.MouseDown, picUserLTopTrackArt6.MouseDown, picUserLTopTrackArt5.MouseDown, picUserLTopTrackArt4.MouseDown, picUserLTopTrackArt3.MouseDown, picUserLTopTrackArt20.MouseDown, picUserLTopTrackArt2.MouseDown, picUserLTopTrackArt19.MouseDown, picUserLTopTrackArt18.MouseDown, picUserLTopTrackArt17.MouseDown, picUserLTopTrackArt16.MouseDown, picUserLTopTrackArt15.MouseDown, picUserLTopTrackArt14.MouseDown, picUserLTopTrackArt13.MouseDown, picUserLTopTrackArt12.MouseDown, picUserLTopTrackArt11.MouseDown, picUserLTopTrackArt10.MouseDown, picUserLTopTrackArt1.MouseDown, picUserLTopAlbumArt9.MouseDown, picUserLTopAlbumArt8.MouseDown, picUserLTopAlbumArt7.MouseDown, picUserLTopAlbumArt6.MouseDown, picUserLTopAlbumArt5.MouseDown, picUserLTopAlbumArt4.MouseDown, picUserLTopAlbumArt3.MouseDown, picUserLTopAlbumArt20.MouseDown, picUserLTopAlbumArt2.MouseDown, picUserLTopAlbumArt19.MouseDown, picUserLTopAlbumArt18.MouseDown, picUserLTopAlbumArt17.MouseDown, picUserLTopAlbumArt16.MouseDown, picUserLTopAlbumArt15.MouseDown, picUserLTopAlbumArt14.MouseDown, picUserLTopAlbumArt13.MouseDown, picUserLTopAlbumArt12.MouseDown, picUserLTopAlbumArt11.MouseDown, picUserLTopAlbumArt10.MouseDown, picUserLTopAlbumArt1.MouseDown, picUserLRecentArt9.MouseDown, picUserLRecentArt20.MouseDown, picUserLRecentArt19.MouseDown, picUserLRecentArt18.MouseDown, picUserLRecentArt17.MouseDown, picUserLRecentArt16.MouseDown, picUserLRecentArt15.MouseDown, picUserLRecentArt14.MouseDown, picUserLRecentArt13.MouseDown, picUserLRecentArt12.MouseDown, picUserLRecentArt11.MouseDown, picUserLRecentArt10.MouseDown, picTrackArt.MouseDown, picTrack9.MouseDown, picTrack8.MouseDown, picTrack7.MouseDown, picTrack6.MouseDown, picTrack5.MouseDown, picTrack4.MouseDown, picTrack3.MouseDown, picTrack20.MouseDown, picTrack2.MouseDown, picTrack19.MouseDown, picTrack18.MouseDown, picTrack17.MouseDown, picTrack16.MouseDown, picTrack15.MouseDown, picTrack14.MouseDown, picTrack13.MouseDown, picTrack12.MouseDown, picTrack11.MouseDown, picTrack10.MouseDown, picTrack1.MouseDown, picArtistArt.MouseDown, picAlbumArt.MouseDown

    End Sub

    Private Sub ArtistTopTrackClick(sender As Object, e As EventArgs) Handles lblArtistTopTrackTitle9.Click, lblArtistTopTrackTitle8.Click, lblArtistTopTrackTitle7.Click, lblArtistTopTrackTitle6.Click, lblArtistTopTrackTitle5.Click, lblArtistTopTrackTitle4.Click, lblArtistTopTrackTitle3.Click, lblArtistTopTrackTitle20.Click, lblArtistTopTrackTitle2.Click, lblArtistTopTrackTitle19.Click, lblArtistTopTrackTitle18.Click, lblArtistTopTrackTitle17.Click, lblArtistTopTrackTitle16.Click, lblArtistTopTrackTitle15.Click, lblArtistTopTrackTitle14.Click, lblArtistTopTrackTitle13.Click, lblArtistTopTrackTitle12.Click, lblArtistTopTrackTitle11.Click, lblArtistTopTrackTitle10.Click, lblArtistTopTrackTitle1.Click

    End Sub

    Private Sub ArtistTopAlbumClick(sender As Object, e As EventArgs) Handles lblArtistTopAlbum9.Click, lblArtistTopAlbum8.Click, lblArtistTopAlbum7.Click, lblArtistTopAlbum6.Click, lblArtistTopAlbum5.Click, lblArtistTopAlbum4.Click, lblArtistTopAlbum3.Click, lblArtistTopAlbum2.Click, lblArtistTopAlbum10.Click, lblArtistTopAlbum1.Click

    End Sub

    Private Sub UserTopTrackAlbumClicked(sender As Object, e As EventArgs) Handles lblUserTopTrackAlbum9.Click, lblUserTopTrackAlbum8.Click, lblUserTopTrackAlbum7.Click, lblUserTopTrackAlbum6.Click, lblUserTopTrackAlbum5.Click, lblUserTopTrackAlbum4.Click, lblUserTopTrackAlbum3.Click, lblUserTopTrackAlbum20.Click, lblUserTopTrackAlbum2.Click, lblUserTopTrackAlbum19.Click, lblUserTopTrackAlbum18.Click, lblUserTopTrackAlbum17.Click, lblUserTopTrackAlbum16.Click, lblUserTopTrackAlbum15.Click, lblUserTopTrackAlbum14.Click, lblUserTopTrackAlbum13.Click, lblUserTopTrackAlbum12.Click, lblUserTopTrackAlbum11.Click, lblUserTopTrackAlbum10.Click, lblUserTopTrackAlbum1.Click

    End Sub

    Private Sub UserTopTrackArtistClicked(sender As Object, e As EventArgs) Handles lblUserTopTrackArtist9.Click, lblUserTopTrackArtist8.Click, lblUserTopTrackArtist7.Click, lblUserTopTrackArtist6.Click, lblUserTopTrackArtist5.Click, lblUserTopTrackArtist4.Click, lblUserTopTrackArtist3.Click, lblUserTopTrackArtist20.Click, lblUserTopTrackArtist2.Click, lblUserTopTrackArtist19.Click, lblUserTopTrackArtist18.Click, lblUserTopTrackArtist17.Click, lblUserTopTrackArtist16.Click, lblUserTopTrackArtist15.Click, lblUserTopTrackArtist14.Click, lblUserTopTrackArtist13.Click, lblUserTopTrackArtist12.Click, lblUserTopTrackArtist11.Click, lblUserTopTrackArtist10.Click, lblUserTopTrackArtist1.Click

    End Sub

    Private Sub UserTopTrackTrackClicked(sender As Object, e As EventArgs) Handles lblUserTopTrackTitle9.Click, lblUserTopTrackTitle8.Click, lblUserTopTrackTitle7.Click, lblUserTopTrackTitle6.Click, lblUserTopTrackTitle5.Click, lblUserTopTrackTitle4.Click, lblUserTopTrackTitle3.Click, lblUserTopTrackTitle20.Click, lblUserTopTrackTitle2.Click, lblUserTopTrackTitle19.Click, lblUserTopTrackTitle18.Click, lblUserTopTrackTitle17.Click, lblUserTopTrackTitle16.Click, lblUserTopTrackTitle15.Click, lblUserTopTrackTitle14.Click, lblUserTopTrackTitle13.Click, lblUserTopTrackTitle12.Click, lblUserTopTrackTitle11.Click, lblUserTopTrackTitle10.Click, lblUserTopTrackTitle1.Click

    End Sub

    Private Sub UserTopArtistClicked(sender As Object, e As EventArgs) Handles lblUserTopArtist9.Click, lblUserTopArtist8.Click, lblUserTopArtist7.Click, lblUserTopArtist6.Click, lblUserTopArtist5.Click, lblUserTopArtist4.Click, lblUserTopArtist3.Click, lblUserTopArtist20.Click, lblUserTopArtist2.Click, lblUserTopArtist19.Click, lblUserTopArtist18.Click, lblUserTopArtist17.Click, lblUserTopArtist16.Click, lblUserTopArtist15.Click, lblUserTopArtist14.Click, lblUserTopArtist13.Click, lblUserTopArtist12.Click, lblUserTopArtist11.Click, lblUserTopArtist10.Click, lblUserTopArtist1.Click

    End Sub

    Private Sub UserTopAlbumAlbumClicked(sender As Object, e As EventArgs) Handles lblUserTopAlbum9.Click, lblUserTopAlbum8.Click, lblUserTopAlbum7.Click, lblUserTopAlbum6.Click, lblUserTopAlbum5.Click, lblUserTopAlbum4.Click, lblUserTopAlbum3.Click, lblUserTopAlbum20.Click, lblUserTopAlbum2.Click, lblUserTopAlbum19.Click, lblUserTopAlbum18.Click, lblUserTopAlbum17.Click, lblUserTopAlbum16.Click, lblUserTopAlbum15.Click, lblUserTopAlbum14.Click, lblUserTopAlbum13.Click, lblUserTopAlbum12.Click, lblUserTopAlbum11.Click, lblUserTopAlbum10.Click, lblUserTopAlbum1.Click

    End Sub

    Private Sub UserTopAlbumArtistClicked(sender As Object, e As EventArgs) Handles lblUserTopAlbumArtist8.Click, lblUserTopAlbumArtist7.Click, lblUserTopAlbumArtist6.Click, lblUserTopAlbumArtist5.Click, lblUserTopAlbumArtist4.Click, lblUserTopAlbumArtist3.Click, lblUserTopAlbumArtist2.Click, lblUserTopAlbumArtist1.Click

    End Sub

    Private Sub UserRecentAlbumClick(sender As Object, e As EventArgs) Handles lblUserRecentAlbum9.Click, lblUserRecentAlbum8.Click, lblUserRecentAlbum7.Click, lblUserRecentAlbum6.Click, lblUserRecentAlbum5.Click, lblUserRecentAlbum4.Click, lblUserRecentAlbum3.Click, lblUserRecentAlbum20.Click, lblUserRecentAlbum2.Click, lblUserRecentAlbum19.Click, lblUserRecentAlbum18.Click, lblUserRecentAlbum17.Click, lblUserRecentAlbum16.Click, lblUserRecentAlbum15.Click, lblUserRecentAlbum14.Click, lblUserRecentAlbum13.Click, lblUserRecentAlbum12.Click, lblUserRecentAlbum11.Click, lblUserRecentAlbum10.Click, lblUserRecentAlbum1.Click

    End Sub

    Private Sub UserRecentArtistClick(sender As Object, e As EventArgs) Handles lblUserRecentArtist9.Click, lblUserRecentArtist8.Click, lblUserRecentArtist7.Click, lblUserRecentArtist6.Click, lblUserRecentArtist5.Click, lblUserRecentArtist4.Click, lblUserRecentArtist3.Click, lblUserRecentArtist20.Click, lblUserRecentArtist2.Click, lblUserRecentArtist19.Click, lblUserRecentArtist18.Click, lblUserRecentArtist17.Click, lblUserRecentArtist16.Click, lblUserRecentArtist15.Click, lblUserRecentArtist14.Click, lblUserRecentArtist13.Click, lblUserRecentArtist12.Click, lblUserRecentArtist11.Click, lblUserRecentArtist10.Click, lblUserRecentArtist1.Click

    End Sub

    Private Sub UserRecentTrackClick(sender As Object, e As EventArgs) Handles lblUserRecentTitle9.Click, lblUserRecentTitle8.Click, lblUserRecentTitle7.Click, lblUserRecentTitle6.Click, lblUserRecentTitle5.Click, lblUserRecentTitle4.Click, lblUserRecentTitle3.Click, lblUserRecentTitle20.Click, lblUserRecentTitle2.Click, lblUserRecentTitle19.Click, lblUserRecentTitle18.Click, lblUserRecentTitle17.Click, lblUserRecentTitle16.Click, lblUserRecentTitle15.Click, lblUserRecentTitle14.Click, lblUserRecentTitle13.Click, lblUserRecentTitle12.Click, lblUserRecentTitle11.Click, lblUserRecentTitle10.Click, lblUserRecentTitle1.Click

    End Sub

    Private Sub UserLTopTrackAlbumClicked(sender As Object, e As EventArgs) Handles lblUserLTopTrackAlbum9.Click, lblUserLTopTrackAlbum8.Click, lblUserLTopTrackAlbum7.Click, lblUserLTopTrackAlbum6.Click, lblUserLTopTrackAlbum5.Click, lblUserLTopTrackAlbum4.Click, lblUserLTopTrackAlbum3.Click, lblUserLTopTrackAlbum20.Click, lblUserLTopTrackAlbum2.Click, lblUserLTopTrackAlbum19.Click, lblUserLTopTrackAlbum18.Click, lblUserLTopTrackAlbum17.Click, lblUserLTopTrackAlbum16.Click, lblUserLTopTrackAlbum15.Click, lblUserLTopTrackAlbum14.Click, lblUserLTopTrackAlbum13.Click, lblUserLTopTrackAlbum12.Click, lblUserLTopTrackAlbum11.Click, lblUserLTopTrackAlbum10.Click, lblUserLTopTrackAlbum1.Click

    End Sub

    Private Sub UserLTopTrackArtistClicked(sender As Object, e As EventArgs) Handles lblUserLTopTrackArtist9.Click, lblUserLTopTrackArtist8.Click, lblUserLTopTrackArtist7.Click, lblUserLTopTrackArtist6.Click, lblUserLTopTrackArtist5.Click, lblUserLTopTrackArtist4.Click, lblUserLTopTrackArtist3.Click, lblUserLTopTrackArtist20.Click, lblUserLTopTrackArtist2.Click, lblUserLTopTrackArtist19.Click, lblUserLTopTrackArtist18.Click, lblUserLTopTrackArtist17.Click, lblUserLTopTrackArtist16.Click, lblUserLTopTrackArtist15.Click, lblUserLTopTrackArtist14.Click, lblUserLTopTrackArtist13.Click, lblUserLTopTrackArtist12.Click, lblUserLTopTrackArtist11.Click, lblUserLTopTrackArtist10.Click, lblUserLTopTrackArtist1.Click

    End Sub

    Private Sub UserLTopTrackTrackClicked(sender As Object, e As EventArgs) Handles lblUserLTopTrackTitle9.Click, lblUserLTopTrackTitle8.Click, lblUserLTopTrackTitle7.Click, lblUserLTopTrackTitle6.Click, lblUserLTopTrackTitle5.Click, lblUserLTopTrackTitle4.Click, lblUserLTopTrackTitle3.Click, lblUserLTopTrackTitle20.Click, lblUserLTopTrackTitle2.Click, lblUserLTopTrackTitle19.Click, lblUserLTopTrackTitle18.Click, lblUserLTopTrackTitle17.Click, lblUserLTopTrackTitle16.Click, lblUserLTopTrackTitle15.Click, lblUserLTopTrackTitle14.Click, lblUserLTopTrackTitle13.Click, lblUserLTopTrackTitle12.Click, lblUserLTopTrackTitle11.Click, lblUserLTopTrackTitle10.Click, lblUserLTopTrackTitle1.Click

    End Sub

    Private Sub UserLTopArtistClicked(sender As Object, e As EventArgs) Handles lblUserLTopArtist9.Click, lblUserLTopArtist8.Click, lblUserLTopArtist7.Click, lblUserLTopArtist6.Click, lblUserLTopArtist5.Click, lblUserLTopArtist4.Click, lblUserLTopArtist3.Click, lblUserLTopArtist20.Click, lblUserLTopArtist2.Click, lblUserLTopArtist19.Click, lblUserLTopArtist18.Click, lblUserLTopArtist17.Click, lblUserLTopArtist16.Click, lblUserLTopArtist15.Click, lblUserLTopArtist14.Click, lblUserLTopArtist13.Click, lblUserLTopArtist12.Click, lblUserLTopArtist11.Click, lblUserLTopArtist10.Click, lblUserLTopArtist1.Click

    End Sub

    Private Sub UserLTopAlbumAlbumClicked(sender As Object, e As EventArgs) Handles lblUserLTopAlbum9.Click, lblUserLTopAlbum8.Click, lblUserLTopAlbum7.Click, lblUserLTopAlbum6.Click, lblUserLTopAlbum5.Click, lblUserLTopAlbum4.Click, lblUserLTopAlbum3.Click, lblUserLTopAlbum20.Click, lblUserLTopAlbum2.Click, lblUserLTopAlbum19.Click, lblUserLTopAlbum18.Click, lblUserLTopAlbum17.Click, lblUserLTopAlbum16.Click, lblUserLTopAlbum15.Click, lblUserLTopAlbum14.Click, lblUserLTopAlbum13.Click, lblUserLTopAlbum12.Click, lblUserLTopAlbum11.Click, lblUserLTopAlbum10.Click, lblUserLTopAlbum1.Click

    End Sub

    Private Sub UserLTopAlbumArtistClicked(sender As Object, e As EventArgs) Handles lblUserLTopAlbumArtist8.Click, lblUserLTopAlbumArtist7.Click, lblUserLTopAlbumArtist6.Click, lblUserLTopAlbumArtist5.Click, lblUserLTopAlbumArtist4.Click, lblUserLTopAlbumArtist3.Click, lblUserLTopAlbumArtist2.Click, lblUserLTopAlbumArtist1.Click

    End Sub

    Private Sub UserLRecentAlbumClick(sender As Object, e As EventArgs) Handles lblUserLRecentAlbum9.Click, lblUserLRecentAlbum8.Click, lblUserLRecentAlbum7.Click, lblUserLRecentAlbum6.Click, lblUserLRecentAlbum5.Click, lblUserLRecentAlbum4.Click, lblUserLRecentAlbum3.Click, lblUserLRecentAlbum20.Click, lblUserLRecentAlbum2.Click, lblUserLRecentAlbum19.Click, lblUserLRecentAlbum18.Click, lblUserLRecentAlbum17.Click, lblUserLRecentAlbum16.Click, lblUserLRecentAlbum15.Click, lblUserLRecentAlbum14.Click, lblUserLRecentAlbum13.Click, lblUserLRecentAlbum12.Click, lblUserLRecentAlbum11.Click, lblUserLRecentAlbum10.Click, lblUserLRecentAlbum1.Click

    End Sub

    Private Sub UserLRecentArtistClick(sender As Object, e As EventArgs) Handles lblUserLRecentArtist9.Click, lblUserLRecentArtist8.Click, lblUserLRecentArtist7.Click, lblUserLRecentArtist6.Click, lblUserLRecentArtist5.Click, lblUserLRecentArtist4.Click, lblUserLRecentArtist3.Click, lblUserLRecentArtist20.Click, lblUserLRecentArtist2.Click, lblUserLRecentArtist19.Click, lblUserLRecentArtist18.Click, lblUserLRecentArtist17.Click, lblUserLRecentArtist16.Click, lblUserLRecentArtist15.Click, lblUserLRecentArtist14.Click, lblUserLRecentArtist13.Click, lblUserLRecentArtist12.Click, lblUserLRecentArtist11.Click, lblUserLRecentArtist10.Click, lblUserLRecentArtist1.Click

    End Sub

    Private Sub UserLRecentTrackClick(sender As Object, e As EventArgs) Handles lblUserLRecentTitle9.Click, lblUserLRecentTitle8.Click, lblUserLRecentTitle7.Click, lblUserLRecentTitle6.Click, lblUserLRecentTitle5.Click, lblUserLRecentTitle4.Click, lblUserLRecentTitle3.Click, lblUserLRecentTitle20.Click, lblUserLRecentTitle2.Click, lblUserLRecentTitle19.Click, lblUserLRecentTitle18.Click, lblUserLRecentTitle17.Click, lblUserLRecentTitle16.Click, lblUserLRecentTitle15.Click, lblUserLRecentTitle14.Click, lblUserLRecentTitle13.Click, lblUserLRecentTitle12.Click, lblUserLRecentTitle11.Click, lblUserLRecentTitle10.Click, lblUserLRecentTitle1.Click

    End Sub
#End Region

End Class