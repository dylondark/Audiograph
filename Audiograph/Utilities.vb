' Audiograph Utilities module
' Contains general methods/variables for Audiograph

Imports System.Net
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Threading
Module Utilities

#Region "Vars"
    Public apihistory As New List(Of String())  ' used for api call history
    Public scrobblehistory As New List(Of String()) ' used for scrobble history
    Public progress As UShort          ' progress bar percentage
    Public progressmultiplier As Byte  ' progress bar
    Public stoploadexecution As Boolean = True  ' stops things from executing on form load
    Public userlookup As String        ' current user in user lookup
    Public tracklookup(2) As String    ' 0 = track, 1 = artist, 2 = album
    Public artistlookup As String      ' current artist in artist lookup
    Public albumlookup(1) As String    ' 0 = album, 1 = artist
    Public originalqueue As New List(Of String())   ' original queue for media
    Public addingqueue As Boolean      ' true when something is currently being added to the queue
    Public queueremoved As New List(Of String())    ' list of removed queue items when shuffle is activated
    Public timezoneoffset As Integer   ' unix time offset for time zone
    Public scrobbleindexdata As New List(Of String())   ' 0 = filename, 1 = title, 2 = artist, 3 = album
    Public newsong As Boolean = True    ' true when mediaplayer is playing a new song, false when the song has already been scrobbled
#End Region

#Region "Methods"
    ' callapi get request
    Public Function CallAPI(method As String, Optional user As String = "", Optional param1 As String = "", Optional param2 As String = "",
                            Optional param3 As String = "", Optional param4 As String = "") As String
        ' url formatting
        Dim urldata As New StringBuilder
        urldata.Append("http://ws.audioscrobbler.com/2.0/?method=" & method & "&")    ' url and method
        If user <> String.Empty Then
            user = user.Replace("&", "%26")         ' encode ampersands
            urldata.Append("user=" & user & "&")    ' user
        End If
        If param1 <> String.Empty Then
            param1 = param1.Replace("&", "%26")     ' encode ampersands
            urldata.Append(param1 & "&")            ' param1
        End If
        If param2 <> String.Empty Then
            param2 = param2.Replace("&", "%26")     ' encode ampersands
            urldata.Append(param2 & "&")            ' param2
        End If
        If param3 <> String.Empty Then
            param3 = param3.Replace("&", "%26")     ' encode ampersands
            urldata.Append(param3 & "&")            ' param3
        End If
        If param4 <> String.Empty Then
            param4 = param4.Replace("&", "%26")     ' encode ampersands
            urldata.Append(param4 & "&")            ' param3
        End If
        urldata.Append("api_key=" & APIkey)         ' api key

        ' initialization
        Dim API As New WebClient()      ' webclient
        API.Headers.Add("user-agent", "Audiograph/INDEV")   ' add user agent
        Dim starttime, endtime As New DateTime  ' timekeeping
        Dim milliseconds As New TimeSpan
        Dim utf8 As New UTF8Encoding    ' encode
        Dim response As String          ' response holder
        Dim errorholder As String       ' holds ex.message

        ' make request
        Try
            starttime = Now        ' starting time
            response = utf8.GetString(API.DownloadData(urldata.ToString))
            endtime = Now          ' ending time
        Catch ex As Exception
            endtime = Now
            response = "CallAPI ERROR: " & ex.Message
            errorholder = ex.Message
        End Try

        ' put in apilist
        milliseconds = endtime.Subtract(starttime)
        If errorholder = String.Empty Then
            ' if no error
            AddAPIHistory(False, method, ParseMetadata(response, "lfm status=").ToUpper, milliseconds.Milliseconds, starttime, Thread.CurrentThread.Name)
        Else
            ' if error
            AddAPIHistory(False, method, errorholder, milliseconds.Milliseconds, starttime, Thread.CurrentThread.Name)
        End If

        Return response
    End Function

    ' callapi post request
    Public Function CallAPIAuth(method As String, Optional param1 As String = "", Optional param2 As String = "",
                            Optional param3 As String = "", Optional param4 As String = "") As String
        ' data formatting
        Dim postdata As New StringBuilder
        postdata.Append("method=" & method & "&")   ' method
        If param1 <> String.Empty Then
            postdata.Append(param1 & "&")           ' param1
        End If
        If param2 <> String.Empty Then
            postdata.Append(param2 & "&")           ' param2
        End If
        If param3 <> String.Empty Then
            postdata.Append(param3 & "&")           ' param3
        End If
        If param4 <> String.Empty Then
            postdata.Append(param4 & "&")           ' param3
        End If
        postdata.Append("sk=" & My.Settings.SessionKey & "&")       ' session key
        postdata.Append("api_key=" & APIkey & "&")  ' api key
        postdata.Append("api_sig=" & CreateSignature(method, param1, param2, param3, param4))    ' api signature
        ' encode as utf8
        Dim encoding As New UTF8Encoding
        Dim postbytes As Byte() = encoding.GetBytes(postdata.ToString)

        ' initialization
        Dim starttime, endtime As DateTime   ' timekeeping
        Dim milliseconds As New TimeSpan    ' latency
        Dim responsestring As String
        Dim errorholder As String

        ' configure request
        Dim API As HttpWebRequest = DirectCast(WebRequest.Create("http://ws.audioscrobbler.com/2.0/"), HttpWebRequest)
        API.Method = "POST"
        API.KeepAlive = True
        API.UserAgent = "Audiograph/INDEV"
        API.ContentLength = postbytes.Length
        API.ContentType = "application/x-www-form-urlencoded"

        Try
            ' start time
            starttime = Now

            ' send request
            Dim reqstream As Stream = API.GetRequestStream
            reqstream.Write(postbytes, 0, postbytes.Length)
            reqstream.Close()

            ' get response
            Dim response As HttpWebResponse = DirectCast(API.GetResponse(), HttpWebResponse)
            Dim responsereader As New StreamReader(response.GetResponseStream())
            responsestring = responsereader.ReadToEnd

            ' end time
            endtime = Now
        Catch ex As Exception
            endtime = Now
            responsestring = "CallAPIAuth ERROR: " & ex.Message
            errorholder = ex.Message
        End Try

        ' put in apilist
        milliseconds = endtime.Subtract(starttime)
        If errorholder = String.Empty Then
            ' if no error
            AddAPIHistory(True, method, ParseMetadata(responsestring, "lfm status=").ToUpper, milliseconds.Milliseconds, starttime, Thread.CurrentThread.Name)
        Else
            ' if error
            AddAPIHistory(True, method, errorholder, milliseconds.Milliseconds, starttime, Thread.CurrentThread.Name)
        End If

        Return responsestring
    End Function

    ' converts an array of node names into their node text
    Public Sub ParseXML(ByVal xml As String, ByVal directory As String, ByVal num As UInteger, ByRef nodes() As String)
        ' initialize xml parser
        Dim xmlParse As New XmlDocument()
        ' check for errors which would be likely caused by invalid input
        Try
            xmlParse.LoadXml(xml)
        Catch ex As Exception
            nodes(0) = "ERROR: Likely caused by invalid input into ParseXML, check CallAPI. Message: " & ex.Message
            Exit Sub
        End Try
        Dim nodelist As XmlNodeList = xmlParse.DocumentElement.SelectNodes(directory)

        ' gets nodes from number down in list
        Dim node As XmlNode = nodelist(num)

        ' get node text
        For nodecount As Byte = 0 To nodes.GetUpperBound(0)
            Try    ' error detection
                nodes(nodecount) = node.SelectSingleNode(nodes(nodecount)).InnerText    ' get node text
            Catch ex As Exception
                nodes(nodecount) = "ERROR: " & ex.Message    ' display error
            End Try
        Next nodecount
    End Sub

    ' parse xml to find image by quality, regular parsexml is not set up to do this
    Public Function ParseImage(ByVal xml As String, ByVal directory As String, ByVal quality As Byte) As String
        ' initialize xml parser
        Dim xmlParse As New XmlDocument()
        ' check for errors which would be likely caused by invalid input
        Try
            xmlParse.LoadXml(xml)
        Catch ex As Exception
            Return "ERROR: Likely caused by invalid input into ParseXML, check CallAPI. Message: " & ex.Message
            Exit Function
        End Try
        Dim nodelist As XmlNodeList = xmlParse.DocumentElement.SelectNodes(directory)

        ' gets nodes from number down in list
        Dim node As XmlNode = nodelist(quality)

        Dim out As String
        Try
            out = node.InnerText
        Catch ex As Exception
            out = "ERROR: " & ex.Message
        End Try
        Return out
    End Function

    ' alternate parseimage for multiple tracks with multiple nodes
    Public Function ParseImage(ByVal xml As String, ByVal directory As String, ByVal count As Byte, ByVal quality As Byte) As String
        ' initialize xml parser
        Dim xmlParse As New XmlDocument()
        ' check for errors which would be likely caused by invalid input
        Try
            xmlParse.LoadXml(xml)
        Catch ex As Exception
            Return "ERROR: Likely caused by invalid input into ParseXML, check CallAPI. Message: " & ex.Message
            Exit Function
        End Try
        Dim nodelist As XmlNodeList = xmlParse.DocumentElement.SelectNodes(directory)

        ' gets nodes from number down in list
        Dim imagelist As XmlNodeList
        Try
            imagelist = nodelist(count).ChildNodes
        Catch ex As Exception
            Return "ERROR: " & ex.Message
            Exit Function
        End Try

        ' gets image node from number down
        Dim imagenode As XmlNode = imagelist(quality)

        Dim out As String
        Try
            out = imagenode.InnerText
        Catch ex As Exception
            out = "ERROR: " & ex.Message
        End Try
        Return out
    End Function

    ' manually parse xml to return metadata
    Public Function ParseMetadata(ByVal xml As String, ByVal metadata As String, Optional count As UInteger = 1) As String
        If xml.Contains(metadata) = True Then                                                               ' proceed if xml input contains specified metadata
            Dim instances As New List(Of String)
            Dim start As Integer = 1
            Dim metadatapos As Integer
            Dim firstquotpos As UInteger
            Dim secondquotpos As UInteger

            Try
                Do Until instances.Count = count
                    metadatapos = InStr(start, xml, metadata)                                              ' get the pos of the beginning of the metadata
                    If metadatapos > -1 Then    ' if instr has found new instance
                        firstquotpos = InStr(metadatapos, xml, Chr(34))                                 ' get the pos of the first quotation mark
                        secondquotpos = InStr(firstquotpos + 1, xml, Chr(34))                           ' get the pos of the second quotation mark
                        instances.Add(xml.Substring(firstquotpos, secondquotpos - firstquotpos - 1))    ' add metadata to list
                        start = secondquotpos
                    End If
                Loop
                Return instances(count - 1)
            Catch ex As Exception
                Return "ERROR: ParseMetadata cannot locate specified metadata."
            End Try
        Else
            Return "ERROR: ParseMetadata cannot locate specified metadata."
        End If
    End Function

    ' convert date to unix time
    Public Function DateToUnix(ByVal datein As Date) As UInteger
        Return (datein - New Date(1970, 1, 1, 0, 0, 0)).TotalSeconds
    End Function

    Public Function UnixToDate(ByVal unixtime As UInteger) As Date
        Return New Date(1970, 1, 1, 0, 0, 0).AddSeconds(unixtime)
    End Function

    Public Function GetCurrentUTC() As UInteger
        Return DateToUnix(Now) - timezoneoffset
    End Function

    Public Function Hash(ByVal input As String) As String
        ' dim stuff
        Dim md5 As New MD5CryptoServiceProvider
        Dim encoding As New UTF8Encoding
        Dim bytestohash() As Byte = encoding.GetBytes(input)
        Dim result As String

        ' hash
        bytestohash = md5.ComputeHash(bytestohash)
        For Each b As Byte In bytestohash
            result &= b.ToString("x2")
        Next

        Return result
    End Function

    ' create api_sig
    Public Function CreateSignature(ByVal method As String, Optional ByVal param1 As String = "", Optional ByVal param2 As String = "", Optional ByVal param3 As String = "", Optional param4 As String = "", Optional sk As Boolean = True) As String
        ' formatting
        param1 = param1.Replace("&", String.Empty)
        param2 = param2.Replace("&", String.Empty)
        param3 = param3.Replace("&", String.Empty)
        param4 = param4.Replace("&", String.Empty)
        param1 = param1.Replace("=", String.Empty)
        param2 = param2.Replace("=", String.Empty)
        param3 = param3.Replace("=", String.Empty)
        param4 = param4.Replace("=", String.Empty)

        ' put params in list for sorting
        Dim params As New List(Of String)
        params.Add("method" & method)   ' method
        params.Add("api_key" & APIkey)  ' api key
        ' sk
        If sk = True Then
            params.Add("sk" & My.Settings.SessionKey)
        End If
        ' param1
        If param1 <> String.Empty Then
            params.Add(param1)
        End If
        ' param2
        If param2 <> String.Empty Then
            params.Add(param2)
        End If
        ' param3
        If param3 <> String.Empty Then
            params.Add(param3)
        End If
        ' param4
        If param4 <> String.Empty Then
            params.Add(param4)
        End If

        ' add params to single string
        params.Sort()                   ' sort alphabetically
        Dim parambuilder As New StringBuilder
        ' append params
        For Each param As String In params
            parambuilder.Append(param)  ' append parameters
        Next
        parambuilder.Append(APIsecret)  ' append secret

        Return Hash(parambuilder.ToString)
    End Function

    ' finds the amount of substrings in a string
    Public Function StrCount(ByVal input As String, ByVal substring As String) As UInteger
        Dim loopend As Boolean = False
        Dim startpos As Integer = 1
        Dim count As UInteger
        Do
            startpos = InStr(startpos, input, substring)
            If startpos <= 0 Then
                loopend = True  ' end loop if no more can be found
            Else
                startpos += substring.Length   ' increment startpos to the end of substring
                count += 1                 ' increment usercount
            End If
        Loop Until loopend = True
        Return count
    End Function

    ' creates a larger array out of a smaller array with empty elements set to string.empty
    Public Function FillArray(ByVal commands() As String, ByVal maxindex As Byte) As String()
        Dim newcommands(maxindex) As String
        Dim upperbound As Byte

        ' set upperbound
        If commands.GetUpperBound(0) > maxindex Then
            upperbound = maxindex
        Else
            upperbound = commands.GetUpperBound(0)
        End If

        ' make new array
        For count As Byte = 0 To upperbound
            newcommands(count) = commands(count)
        Next

        ' make empty maxindex string.empty
        For count As Byte = 0 To maxindex
            If newcommands(count) = Nothing Then
                newcommands(count) = String.Empty
            End If
        Next

        Return newcommands
    End Function

    Public Sub GoToTrack(ByVal track As String, ByVal artist As String)
        ' check for errors
        If track.Contains("ERROR: ") = True OrElse artist.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmMain.Invoke(Sub() frmMain.txtTrackTitle.Text = track)
        frmMain.Invoke(Sub() frmMain.txtTrackArtist.Text = artist)
        frmMain.Invoke(Sub() frmMain.tabControl.SelectTab(2))
        frmMain.Invoke(Sub() frmMain.btnTrackGo.Select())
        frmMain.btnTrackGo.PerformClick()
    End Sub

    Public Sub GoToArtist(ByVal artist As String)
        ' check for errors
        If artist.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmMain.Invoke(Sub() frmMain.txtArtistName.Text = artist)
        frmMain.Invoke(Sub() frmMain.tabControl.SelectTab(3))
        frmMain.Invoke(Sub() frmMain.btnArtistGo.Select())
        frmMain.btnArtistGo.PerformClick()
    End Sub

    Public Sub GoToAlbum(ByVal album As String, ByVal artist As String)
        ' check for errors
        If album.Contains("ERROR: ") = True OrElse artist.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmMain.Invoke(Sub() frmMain.txtAlbumTitle.Text = album)
        frmMain.Invoke(Sub() frmMain.txtAlbumArtist.Text = artist)
        frmMain.Invoke(Sub() frmMain.tabControl.SelectTab(4))
        frmMain.Invoke(Sub() frmMain.btnAlbumGo.Select())
        frmMain.btnAlbumGo.PerformClick()
    End Sub

    Public Sub BackupTag(ByVal tag As String)
        ' check for errors
        If tag.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmBackupTool.Show()
        frmBackupTool.Activate()
        frmBackupTool.cmbContents.SelectedIndex = 1
        frmBackupTool.txtTagTag.Text = tag
    End Sub

    Public Sub BackupTrack(ByVal track As String, ByVal artist As String)
        ' check for errors
        If track.Contains("ERROR: ") = True OrElse artist.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmBackupTool.Show()
        frmBackupTool.Activate()
        frmBackupTool.cmbContents.SelectedIndex = 2
        frmBackupTool.txtTrackTitle.Text = track
        frmBackupTool.txtTrackArtist.Text = artist
    End Sub

    Public Sub BackupArtist(ByVal artist As String)
        ' check for errors
        If artist.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmBackupTool.Show()
        frmBackupTool.Activate()
        frmBackupTool.cmbContents.SelectedIndex = 3
        frmBackupTool.txtArtistArtist.Text = artist
    End Sub

    Public Sub BackupAlbum(ByVal album As String, ByVal artist As String)
        ' check for errors
        If album.Contains("ERROR: ") = True OrElse artist.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmBackupTool.Show()
        frmBackupTool.Activate()
        frmBackupTool.cmbContents.SelectedIndex = 4
        frmBackupTool.txtAlbumAlbum.Text = album
        frmBackupTool.txtAlbumArtist.Text = artist
    End Sub

    Public Sub BackupUser(ByVal user As String)
        ' check for errors
        If user.Contains("ERROR: ") = True Then
            Exit Sub
        End If

        frmBackupTool.Show()
        frmBackupTool.Activate()
        frmBackupTool.cmbContents.SelectedIndex = 5
        frmBackupTool.txtUserUser.Text = user
    End Sub

    Public Sub AddAPIHistory(ByVal post As Boolean, ByVal method As String, ByVal status As String, ByVal latency As Integer, time As DateTime, thread As String)
        Dim poststring As String
        If post = False Then
            poststring = "GET"
        Else
            poststring = "POST"
        End If
        Dim listarray() As String = {poststring, method, status, latency.ToString("N0") & "ms", time.ToString("G"), thread}

        Try
            apihistory.Add(listarray)
        Catch ex As IndexOutOfRangeException
        End Try
    End Sub

    ' provides the autocorrected name of an entered tag, also can be used to verify if a tag exists on the lfm servers
    Public Function VerifyTag(tag As String) As String
        ' strings
        Dim response As String = CallAPI("tag.getInfo", "", "tag=" + tag, "autocorrect=1")
        Dim tagnodes As String() = {"name", "total"}

        ' parse
        ParseXML(response, "/lfm/tag", 0, tagnodes)

        ' any tag the user types in will come up with the name but will not have any data. filter out unverifiable tags by checking if taggings = 0
        If tagnodes(1) = "0" Then
            tagnodes(0) = "ERROR: Tag unable to be verified"
        End If

        Return tagnodes(0)
    End Function

    ' provides the autocorrected name and artist of an entered track, also can be used to verify if a track exists on the lfm servers
    Public Function VerifyTrack(track As String, artist As String) As String()
        ' strings
        Dim response As String = CallAPI("track.getInfo", "", "track=" + track, "artist=" + artist, "autocorrect=1")
        Dim tracknodes As String() = {"name", "artist/name", "album/title"}

        ' parse
        ParseXML(response, "/lfm/track", 0, tracknodes)

        Return tracknodes
    End Function

    ' provides the autocorrected name of an artist, also can be used to verify if an artist exists on the lfm servers
    Public Function VerifyArtist(artist As String) As String
        ' strings
        Dim response As String = CallAPI("artist.getInfo", "", "artist=" + artist, "autocorrect=1")
        Dim artistnodes As String() = {"name"}

        ' parse
        ParseXML(response, "/lfm/artist", 0, artistnodes)

        Return artistnodes(0)
    End Function

    ' provides the autocorrected name and artist of an entered album, also can be used to verify if an album exists on the lfm servers
    Public Function VerifyAlbum(album As String, artist As String) As String()
        ' strings
        Dim response As String = CallAPI("album.getInfo", "", "album=" + album, "artist=" + artist, "autocorrect=1")
        Dim albumnodes As String() = {"name", "artist"}

        ' parse
        ParseXML(response, "/lfm/album", 0, albumnodes)

        Return albumnodes
    End Function

    ' scrobbles a track and adds the data to the scrobble history listviews
    ' return = track, artist, album, timestamp, status
    Public Function Scrobble(track As String, artist As String, timestamp As UInteger, source As String, Optional album As String = "") As String()
        ' make request
        Dim response As String
        If album = String.Empty Then
            response = CallAPIAuth("track.scrobble", "track=" & track, "artist=" & artist, "timestamp=" & timestamp.ToString)
        Else
            response = CallAPIAuth("track.scrobble", "track=" & track, "artist=" & artist, "timestamp=" & timestamp.ToString, "album=" & album)
        End If

        ' check for error
        If response.Contains("ERROR: ") = True Then
            Return {track, artist, album, timestamp, "ERROR: Scrobble did not succeed."}
        End If

        ' accepted true/false?
        Dim acceptedstring As String = ParseMetadata(response, "accepted=")
        Dim accepted As Boolean
        If CShort(acceptedstring) > 0 Then
            accepted = True
        Else
            accepted = False
        End If
        ' parse response
        Dim responsenodes As String() = {"track", "artist", "album", "timestamp", "ignoredMessage"}
        ParseXML(response, "/lfm/scrobbles/scrobble", 0, responsenodes)

        ' status
        ' check for error
        If track.Contains("ERROR: ") = True Then
            responsenodes = {track, artist, timestamp, album, "ERROR: Scrobble did not succeed."}
        End If
        ' check for accepted/ignored
        If accepted = True Then
            responsenodes(4) = "Success"
        Else    ' ignored
            responsenodes(4) = responsenodes(4).Insert(0, "Ignored: ")
        End If

        ' add to listviews
        frmMain.ltvMediaHistory.Items.Insert(0, responsenodes(0)).SubItems.AddRange({responsenodes(1), responsenodes(4)})
        scrobblehistory.Add({responsenodes(0), responsenodes(1), responsenodes(2), UnixToDate(CUInt(responsenodes(3) + timezoneoffset)).ToString("G"), UnixToDate(GetCurrentUTC() + timezoneoffset).ToString("G"), source, responsenodes(4)})

        Return responsenodes
    End Function

    ' returns just the filename from a full path
    Public Function GetFilename(path) As String
        Dim startIndex As Integer = InStrRev(path, "\")
        Dim endIndex As Integer = InStr(path, ".") - 1
        Return path.Substring(startIndex, endIndex - startIndex)
    End Function

    ' searches current scrobble index for a file and returns the 0 = track, 1 = artist, 2 = album
    ' returns null if not found
    Public Function SearchIndex(filename As String) As String()
        For Each row In scrobbleindexdata
            If row(0) = filename Then
                Return {row(1), row(2), row(3)}
            End If
        Next
        Return Nothing
    End Function
#End Region

End Module
