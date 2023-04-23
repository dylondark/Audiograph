Imports System.Text
Public Class frmConsole
    Public working As Boolean = False
    Private authorized As Boolean = False
    Private cmdlist As New List(Of String)
    Private cmdlistcount As UShort

    ' step 1 - interprets commands and sends them to their respective methods
    Public Sub CommandInterpreter(ByVal command As String)
        ' stop if working
        If working = True Then
            Exit Sub
        End If
        working = True

        ' append command
        txtOutput.AppendText(command)

        ' add command to cmdlist
        cmdlist.Add(command)

        ' detect for no input
        If command = String.Empty Then
            DisplayOut("Please enter a command.")
            Exit Sub
        End If
        ' check that spaceholder character is not present
        If command.Contains("ˍ") = True Then
            DisplayOut("ERROR: Char 'ˍ' is an invalid character.")
        End If

        ' parse for backticks
        Dim backtick As Boolean = False
        For count As UInteger = 0 To command.Count - 1
            ' backtick switch
            If command(count) = "`"c Then
                If backtick = False Then
                    backtick = True
                Else
                    backtick = False
                End If
            End If
            ' space replace
            If command(count) = " "c AndAlso backtick = False Then
                command = command.Remove(count, 1)
                command = command.Insert(count, "ˍ")
            End If
        Next

        ' split command into array
        Dim commandarray() As String = command.Trim.Split("ˍ"c)

        ' tolower/null check
        For count As Byte = 0 To commandarray.Count() - 1
            ' make tolower if no backticks
            If commandarray(count).Contains("`") = False Then
                commandarray(count) = commandarray(count).ToLower
            End If

            ' make null nothing
            If commandarray(count) = "null" Then
                commandarray(count) = String.Empty
            End If
        Next

        ' get rid of any backticks in array
        For count As Byte = 0 To commandarray.Count() - 1
            commandarray(count) = commandarray(count).Replace("`", String.Empty)
        Next

        ' send command to proper method
        Select Case commandarray(0)
            Case "help"
                CmdHelp(commandarray)
            Case "hash"
                CmdHash(commandarray)
            Case "lfm"
                CmdLfm(commandarray)
            Case "auth", "authorize"
                CmdAuthorize(commandarray)
            Case "var"
                CmdVar(commandarray)
            Case "createreq"
                CmdCreatereq(commandarray)
            Case "createsig"
                CmdCreatesig(commandarray)
            Case "frm"
                CmdFrm(commandarray)
            Case "setsk"
                CmdSetsk(commandarray)
            Case "threads"
                CmdThreads()
            Case "resetprog"
                CmdResetprog()
            Case "time"
                CmdTime()
            Case "viewsk"
                CmdViewsk()
            Case "secret"
                CmdSecret()
            Case "removeuser"
                My.Settings.User = String.Empty
                My.Settings.SessionKey = String.Empty
                DisplayOut("User removed.")
            Case "removesk"
                My.Settings.SessionKey = String.Empty
                frmMain.AuthenticatedUI(False)
                DisplayOut("Session key removed.")
            Case "root"
                Process.Start(Application.StartupPath)
                DisplayOut(Application.ExecutablePath)
            Case "clr", "clear", "cls"
                txtOutput.Text = "user>"
                working = False
            Case "exit"
                Application.Exit()
            Case "restart"
                Application.Restart()
            Case "ram"
                Dim x As Process = Process.GetCurrentProcess()
                DisplayOut((x.WorkingSet64 / 1024 / 1024).ToString("N2") & "MB currently in use with " & x.Threads.Count.ToString & " threads by the Audiograph process.")
            Case "apiinfo"
                ' check for authorization
                If authorized = True Then
                    DisplayOut("Key".PadRight(8) & APIkey & vbCrLf & "Secret".PadRight(8) & APIsecret)
                Else
                    DisplayOut("ERROR: apiinfo command requires authorization. Please use the 'authorize' command.")
                End If
            Case "clearindex"
                My.Settings.CurrentScrobbleIndex = String.Empty
                DisplayOut("Current index cleared.")
            Case Else
                DisplayOut("ERROR: Command '" & commandarray(0) & "' unrecognized.")
        End Select
    End Sub

#Region "Step 2 - command methods"
    Private Sub CmdHelp(ByVal commands() As String)
        Dim output As New StringBuilder
        If commands.Count = 1 Then
            ' general help
            output.AppendLine("Type 'help {command}' to display information about a specific command.")
            output.AppendLine("() = required, {} = optional")
            output.AppendLine("Use backticks `` to pass an argument with spaces/capitalization and 'null' to pass an argument with nothing.")
            output.AppendLine(String.Empty)
            output.AppendLine("help".PadRight(12) & "Displays list of commands or information about a specific command.")
            output.AppendLine("clr".PadRight(12) & "Clears the screen.")
            output.AppendLine("lfm".PadRight(12) & "Returns the raw XML from requests made with the CallAPI and CallAPIAuth functions.")
            output.AppendLine("var".PadRight(12) & "Read the value of a variable.")
            output.AppendLine("hash".PadRight(12) & "Returns the MD5 hash of a string.")
            output.AppendLine("createreq".PadRight(12) & "Returns the url used for a get request to the LFM server.")
            output.AppendLine("createsig".PadRight(12) & "Create an LFM API signature.")
            output.AppendLine("frm".PadRight(12) & "Open, close or display information about a form.")
            output.AppendLine("removeuser".PadRight(12) & "Remove user (can cause errors).")
            output.AppendLine("removesk".PadRight(12) & "Remove session key (you will have to reauthenticate, can cause errors).")
            output.AppendLine("ram".PadRight(12) & "Returns the current memory usage from the Audiograph process.")
            output.AppendLine("exit".PadRight(12) & "Exits the Audiograph application, including all forms.")
            output.AppendLine("restart".PadRight(12) & "Restarts the Audiograph application, closing all forms.")
            output.AppendLine("root".PadRight(12) & "Opens a file explorer window with the root folder for this Audiograph application.")
            output.AppendLine("threads".PadRight(12) & "Shows current running threads.")
            output.AppendLine("resetprog".PadRight(12) & "Resets the progress and progressmultiplier variables.")
            output.AppendLine("time".PadRight(12) & "Displays various time variables.")
            output.AppendLine("clearindex".PadRight(12) & "Unloads the currently loaded scrobble index.")
            output.AppendLine("auth".PadRight(12) & "Allows you to authorize as a developer in order to use restricted commands. Unavailable in release builds.")
            output.AppendLine("viewsk".PadRight(12) & "Displays the current user session key (requires authorization).")
            output.AppendLine("setsk".PadRight(12) & "Sets the current user session key to a provided session key (requires authorization).")
            output.Append("apiinfo".PadRight(12) & "Display the Last.fm API key and secret used by this application (requires authorization).")
        Else
            ' command help
            Select Case commands(1)
                Case "help"
                    output.AppendLine("Usage: help {command}")
                    output.Append("Displays general information about commands (no params) or information about a specific command.")
                Case "clr"
                    output.AppendLine("Usage: clr")
                    output.Append("Clears the screen.")
                Case "lfm"
                    output.AppendLine("Usage: lfm (get/post) (method) {user if get} {param1} {param2} {param3}")
                    output.AppendLine("Uses the CallAPI (get) or CallAPIAuth (post) functions to make a request to the Last.fm API. Returns the output from these functions.")
                    output.Append("'post' argument requires authorization, see 'help auth' for more information.")
                Case "var"
                    output.AppendLine("Usage: var (variablename)")
                    output.AppendLine("Displays the value of a specifed variable during runtime.")
                    output.Append("Variables: progress, progressmultiplier, stoploadexecution, userlookup, tracklookup, cmdlist, cmdlistcount, settings.user, settings.sessionkey")
                Case "hash"
                    output.AppendLine("Usage: hash (string)")
                    output.Append("Returns the MD5 hash of a specified string.")
                Case "createreq"
                    output.AppendLine("Usage: createreq (method) {user} {param1} {param2} {param3}")
                    output.Append("Creates a url used for an HTTP GET request to the Last.fm server, can be loaded into an XML viewer.")
                Case "createsig"
                    output.AppendLine("Usage: createsig (method) {param1} {param2} {param3}")
                    output.AppendLine("Creates a Last.fm API signature used for HTTP POST requests.")
                    output.Append("Command requires authorization, see 'help auth' for more information.")
                Case "frm"
                    output.AppendLine("Usage: frm (open/close/info) (formname)")
                    output.AppendLine("Opens, closes, or retrieves information about a form.")
                    output.Append("Forms: main, about, authentication, console, apihistory, trackadvanced, artistadvanced, albumadvanced, addqueue, scrobblehistory, backuptool, scrobbleindexeditor, scrobbleindexaddrow, scrobbleindextrackadvanced, scrobblesearch")
                Case "removeuser"
                    output.AppendLine("Usage: removeuser")
                    output.Append("Removes the set user from the application, along with their session key if applicable. Can cause errors, and requires a restart of the application to fully take effect.")
                Case "removesk"
                    output.AppendLine("Usage: removesk")
                    output.Append("Removes the user's session key from the application if they have authenticated, effectively taking away their authentication.")
                Case "ram"
                    output.AppendLine("Usage: ram")
                    output.Append("Displays the current memory usage from the Audiograph process.")
                Case "exit"
                    output.AppendLine("Usage: exit")
                    output.Append("Exits the Audiograph application, including all forms.")
                Case "restart"
                    output.AppendLine("Usage: restart")
                    output.Append("Restarts the Audiograph application, closing all forms.")
                Case "root"
                    output.AppendLine("Usage: root")
                    output.Append("Opens a file explorer window with the root folder for this Audiograph executable.")
                Case "auth", "authorize"
                    output.AppendLine("Usage: auth (password)")
                    output.AppendLine("Allows you to authorize as a developer using a special password in order to use developer-restricted commands.")
                    output.Append("This feature is not available in release builds.")
                Case "threads"
                    output.AppendLine("Usage: threads")
                    output.Append("Displays current running active threads/operations.")
                Case "resetprog"
                    output.AppendLine("Usage: resetprog")
                    output.Append("Resets the progress and progressmultiplier variables to 0. Useful when the progress bar has become stuck.")
                Case "time"
                    output.AppendLine("Usage: time")
                    output.Append("Displays the current time, current UTC time (using GetCurrentUTC()), and the current time zone offset.")
                Case "apiinfo"
                    output.AppendLine("Usage: apiinfo")
                    output.AppendLine("Display the Last.fm API key and secret used by this application.")
                    output.Append("Command requires authorization, see 'help auth' for more information.")
                Case "viewsk"
                    output.AppendLine("Usage: viewsk")
                    output.AppendLine("Display the current user session key if the user is authorized.")
                    output.Append("Command requires authorization, see 'help auth' for more information.")
                Case "setsk"
                    output.AppendLine("Usage: setsk (sessionkey)")
                    output.AppendLine("Sets the current user session key to a provided session key. Validates that the session key is usable.")
                    output.Append("Command requires authorization, see 'help auth' for more information.")
                Case "clearindex"
                    output.AppendLine("Usage: clearindex")
                    output.Append("Unloads the currently loaded scrobble index. Requires a restart to take effect.")
                Case Else
                    output.Append("ERROR: Help command '" & commands(1) & "' not found.")
            End Select
        End If

        ' final output
        DisplayOut(output.ToString)
    End Sub

    Private Sub CmdHash(ByVal commands() As String)
        ' make sure method has been passed required params
        If commands.Count > 1 Then
            DisplayOut(Hash(commands(1)))
        Else
            DisplayOut("ERROR: Missing required parameter 'string'. Type 'help hash' for proper command usage.")
        End If
    End Sub

    Private Sub CmdLfm(ByVal commands() As String)
        ' make sure method has been passed required params
        If commands.Count >= 3 Then ' has required parameters
            ' check that get/post is proper
            If commands(1) = "get" Then
                ' get start time
                Dim starttime As New DateTime
                starttime = DateTime.Now

                ' call api
                Dim newcommands() As String = FillArray(commands, 6)
                Dim returnXML As String = CallAPI(newcommands(2), newcommands(3), newcommands(4), newcommands(5), newcommands(6))

                ' get end time
                Dim endtime As New DateTime
                endtime = DateTime.Now

                ' get milliseconds
                Dim calltime As New TimeSpan
                calltime = endtime.Subtract(starttime)

                ' display data
                Dim output As New StringBuilder
                output.AppendLine("-----BEGIN RESPONSE-----")
                output.AppendLine(returnXML)
                output.AppendLine("------END RESPONSE------")
                output.AppendLine(String.Empty)
                ' status message
                If returnXML.Contains("ERROR: ") = False Then
                    output.Append("Call returned successfully in " & calltime.Milliseconds.ToString & "ms.")
                Else
                    output.Append("Call returned unsuccessfully.")
                End If
                DisplayOut(output.ToString)
            ElseIf commands(1) = "post" Then
                ' check for authorization to use post
                If authorized = True Then
                    ' call api
                    Dim newcommands() As String = FillArray(commands, 5)
                    Dim returnXML As String = CallAPIAuth(newcommands(2), newcommands(3), newcommands(4), newcommands(5))

                    ' display data
                    Dim output As New StringBuilder
                    output.AppendLine("-----BEGIN RESPONSE-----")
                    output.AppendLine(returnXML)
                    output.AppendLine("------END RESPONSE------")
                    output.AppendLine(String.Empty)
                    ' status message
                    If returnXML.Contains("ERROR: ") = False Then
                        output.Append("Post made successfully.")
                    Else
                        output.Append("Post unsuccessful.")
                    End If
                    DisplayOut(output.ToString)
                Else
                    DisplayOut("ERROR: 'post' parameter requires authorization. Please use the 'auth' command.")
                End If
            Else
                ' if getpost not recognized
                DisplayOut("ERROR: '" & commands(1) & "' parameter not recognized. Type 'help lfm' for proper command usage.")
            End If
        ElseIf commands.Count = 2 Then  ' missing method parameter
            DisplayOut("ERROR: Missing required parameter 'method'. Type 'help lfm' for proper command usage.")
        Else    ' missing getpost and method parameters
            DisplayOut("ERROR: Missing required parameters 'get/post' and 'method'. Type 'help lfm' for proper command usage.")
        End If
    End Sub

    Private Sub CmdAuthorize(ByVal commands() As String)
#If DEBUG Then
        ' make sure method has been passed required params
        If commands.Count >= 2 Then
            ' check that password is correct
            If commands(1) = ConsolePass Then
                authorized = True
                DisplayOut("Authorization success!")
            Else
                DisplayOut("Password incorrect, authorization unsuccessful.")
            End If
        Else
            DisplayOut("ERROR: Missing required parameter 'code'. Type 'help authorize' for proper command usage.")
        End If
#Else
        DisplayOut("ERROR: This function is not present in release builds.")
#End If
    End Sub

    Private Sub CmdVar(ByVal commands() As String)
        ' make sure method has been passed required params
        If commands.Count >= 2 Then
            ' determine variable
            Dim output As New StringBuilder
            Select Case commands(1)
                Case "apihistory"
                    output.AppendLine("Displaying variable 'apihistory' (frmMain) as List of String with " & apihistory.Count.ToString & " elements.")
                    output.Append(String.Empty)
                    ' display values
                    For count As UInteger = 0 To apihistory.Count - 1
                        output.Append(vbCrLf & "apihistory (" & count & ") = " & apihistory(count)(0) & ", " & apihistory(count)(1) & ", " & apihistory(count)(2) & ", " & apihistory(count)(3) & ", " & apihistory(count)(4) & ", " & apihistory(count)(5))
                    Next
                    DisplayOut(output.ToString)
                Case "tracklookup"
                    output.AppendLine("Displaying variable 'tracklookup' (frmMain) as Array of String with " & tracklookup.Count.ToString & " elements.")
                    output.Append(String.Empty)
                    ' display values
                    For count As UInteger = 0 To tracklookup.Count - 1
                        output.Append(vbCrLf & "tracklookup(" & count & ") = " & tracklookup(count))
                    Next
                    DisplayOut(output.ToString)
                Case "cmdlist"
                    output.AppendLine("Displaying variable 'cmdlist' (frmConsole) as List of String with " & cmdlist.Count.ToString & " elements.")
                    output.Append(String.Empty)
                    ' display values
                    For count As UInteger = 0 To cmdlist.Count - 1
                        output.Append(vbCrLf & "cmdlist(" & count & ") = " & cmdlist(count))
                    Next
                    DisplayOut(output.ToString)
                Case "cmdlistcount"
                    DisplayOut("Displaying variable 'cmdlistcount' (frmConsole) as UShort." & vbCrLf & cmdlistcount.ToString)
                Case "progress"
                    DisplayOut("Displaying variable 'progress' (frmMain) as UShort." & vbCrLf & progress.ToString)
                Case "progressmultiplier"
                    DisplayOut("Displaying variable 'progressmultiplier' (frmMain) as Byte." & vbCrLf & progressmultiplier.ToString)
                Case "stoploadexecution"
                    DisplayOut("Displaying variable 'stoploadexecution' (frmMain) as Boolean." & vbCrLf & stoploadexecution.ToString)
                Case "userlookup"
                    DisplayOut("Displaying variable 'userlookup' (frmMain) as String." & vbCrLf & userlookup)
                Case "settings.user"
                    DisplayOut("Displaying settings variable 'user' as String." & vbCrLf & My.Settings.User)
                Case "settings.sessionkey"
                    ' check for authorization
                    If authorized = True Then
                        DisplayOut("Displaying settings variable 'sessionkey' as String." & vbCrLf & My.Settings.SessionKey)
                    Else
                        DisplayOut("ERROR: Variable 'settings.sessionkey' requires authorization. Please use the 'authorize' command.")
                    End If
                Case Else
                    DisplayOut("ERROR: Variable '" & commands(1) & "' not found.")
            End Select
        Else
            DisplayOut("ERROR: Missing required parameter 'variablename'. Type 'help authorize' for proper command usage.")
        End If
    End Sub

    Private Sub CmdCreatereq(ByVal commands() As String)
        ' make sure method has been passed required params
        If commands.Count >= 2 Then
            ' check for authorization
            If authorized = True Then
                Dim newcommands As String() = FillArray(commands, 5)

                ' url formatting
                Dim urldata As New StringBuilder
                urldata.Append("http://ws.audioscrobbler.com/2.0/?method=" & newcommands(1) & "&")    ' url and method
                If newcommands(2) <> String.Empty Then
                    urldata.Append("user=" & newcommands(2) & "&")  ' user
                End If
                If newcommands(3) <> String.Empty Then
                    urldata.Append(newcommands(3) & "&")            ' param1
                End If
                If newcommands(4) <> String.Empty Then
                    urldata.Append(newcommands(4) & "&")            ' param2
                End If
                If newcommands(5) <> String.Empty Then
                    urldata.Append(newcommands(5) & "&")            ' param3
                End If
                urldata.Append("api_key=" & APIkey)         ' api key
                urldata = urldata.Replace(" ", "+")
                DisplayOut(urldata.ToString)
            Else
                DisplayOut("ERROR: createreq command requires authorization. Please use the 'auth' command.")
            End If
        Else
            DisplayOut("ERROR: Missing required parameter 'method'. Type 'help createreq' for proper usage.")
        End If
    End Sub

    Private Sub CmdCreatesig(ByVal commands() As String)
        ' make sure method has been passed required params
        If commands.Count >= 2 Then
            ' check auth
            If authorized = True Then
                Dim newcommands() = FillArray(commands, 4)

                DisplayOut(CreateSignature(newcommands(1), newcommands(2), newcommands(3), newcommands(4)))
            Else
                DisplayOut("ERROR: createsig command requires authorization. Please use the 'auth' command.")
            End If
        Else
                DisplayOut("ERROR: Missing required parameter 'method'. Type 'help createsig' for proper usage.")
            End If
    End Sub

    Private Sub CmdFrm(ByVal commands() As String)
        ' make sure method has been passed required params
        If commands.Count >= 3 Then
            ' open/close/info
            Select Case commands(1)
                Case "open"
                    ' form name
                    Select Case commands(2).ToLower
                        Case "main"
                            Show()
                            Activate()
                            DisplayOut(String.Empty)
                        Case "about"
                            frmAbout.Show()
                            frmAbout.Activate()
                            DisplayOut(String.Empty)
                        Case "authentication"
                            frmAuthentication.Show()
                            frmAuthentication.Activate()
                            DisplayOut(String.Empty)
                        Case "apihistory"
                            frmAPIHistory.Show()
                            frmAPIHistory.Activate()
                            DisplayOut(String.Empty)
                        Case "trackadvanced"
                            frmTrackAdvanced.Show()
                            frmTrackAdvanced.Activate()
                            DisplayOut(String.Empty)
                        Case "artistadvanced"
                            frmArtistAdvanced.Show()
                            frmArtistAdvanced.Activate()
                            DisplayOut(String.Empty)
                        Case "albumadvanced"
                            frmAlbumAdvanced.Show()
                            frmAlbumAdvanced.Activate()
                            DisplayOut(String.Empty)
                        Case "addqueue"
                            frmAddToQueue.Show()
                            frmAddToQueue.Activate()
                            DisplayOut(String.Empty)
                        Case "console"
                            DisplayOut(String.Empty)
                        Case "scrobblehistory"
                            frmScrobbleHistory.Show()
                            frmScrobbleHistory.Activate()
                            DisplayOut(String.Empty)
                        Case "backuptool"
                            frmBackupTool.Show()
                            frmBackupTool.Activate()
                            DisplayOut(String.Empty)
                        Case "scrobbleindexeditor"
                            frmScrobbleIndexEditor.Show()
                            frmScrobbleIndexEditor.Activate()
                            DisplayOut(String.Empty)
                        Case "scrobbleindexaddrow"
                            frmScrobbleIndexAddRow.Show()
                            frmScrobbleIndexAddRow.Activate()
                            DisplayOut(String.Empty)
                        Case "scrobbleindextrackadvanced"
                            frmScrobbleIndexTrackAdvanced.Show()
                            frmScrobbleIndexTrackAdvanced.Activate()
                            DisplayOut(String.Empty)
                        Case "scrobblesearch"
                            frmScrobbleSearch.Show()
                            frmScrobbleSearch.Activate()
                            DisplayOut(String.Empty)
                        Case Else
                            DisplayOut("ERROR: Form '" & commands(2) & "' not found. Type 'help frm' for proper command usage.")
                    End Select
                Case "close"
                    Select Case commands(2).ToLower
                        Case "main"
                            Close()
                            DisplayOut(String.Empty)
                        Case "about"
                            frmAbout.Close()
                            DisplayOut(String.Empty)
                        Case "authentication"
                            frmAuthentication.Close()
                            DisplayOut(String.Empty)
                        Case "apihistory"
                            frmAPIHistory.Close()
                            DisplayOut(String.Empty)
                        Case "trackadvanced"
                            frmTrackAdvanced.Close()
                            DisplayOut(String.Empty)
                        Case "artistadvanced"
                            frmArtistAdvanced.Close()
                            DisplayOut(String.Empty)
                        Case "albumadvanced"
                            frmAlbumAdvanced.Close()
                            DisplayOut(String.Empty)
                        Case "adddqueue"
                            frmAddToQueue.Close()
                            DisplayOut(String.Empty)
                        Case "console"
                            Close()
                        Case "scrobblehistory"
                            frmScrobbleHistory.Close()
                            DisplayOut(String.Empty)
                        Case "backuptool"
                            frmBackupTool.Close()
                            DisplayOut(String.Empty)
                        Case "scrobbleindexeditor"
                            frmScrobbleIndexEditor.Close()
                            DisplayOut(String.Empty)
                        Case "scrobbleindexaddrow"
                            frmScrobbleIndexAddRow.Close()
                            DisplayOut(String.Empty)
                        Case "scrobbleindextrackadvanced"
                            frmScrobbleIndexTrackAdvanced.Close()
                            DisplayOut(String.Empty)
                        Case "scrobblesearch"
                            frmScrobbleSearch.Close()
                            DisplayOut(String.Empty)
                        Case Else
                            DisplayOut("ERROR: Form '" & commands(2) & "' not found. Type 'help frm' for proper command usage.")
                    End Select
                Case "info"
                    Dim output As New StringBuilder
                    Select Case commands(2).ToLower
                        Case "main"
                            output.AppendLine("Displaying information for form frmMain")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmMain.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmMain.Width.ToString & ", " & Height)
                            output.AppendLine("Position".PadRight(12) & frmMain.Left.ToString & ", " & frmMain.Top.ToString)
                            output.Append("WindowState".PadRight(12) & frmMain.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "about"
                            output.AppendLine("Displaying information for form frmAbout.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmAbout.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmAbout.Width.ToString & ", " & frmAbout.Height)
                            output.AppendLine("Position".PadRight(12) & frmAbout.Left.ToString & ", " & frmAbout.Top.ToString)
                            output.Append("WindowState".PadRight(12) & frmAbout.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "authentication"
                            output.AppendLine("Displaying information for form frmAuthentication.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmAuthentication.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmAuthentication.Width.ToString & ", " & frmAuthentication.Height)
                            output.AppendLine("Position".PadRight(12) & frmAuthentication.Left.ToString & ", " & frmAuthentication.Top.ToString)
                            output.Append("WindowState".PadRight(12) & frmAuthentication.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "apihistory"
                            output.AppendLine("Displaying information for form frmAPIHistory.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmAPIHistory.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmAPIHistory.Width.ToString & ", " & frmAPIHistory.Height)
                            output.AppendLine("Position".PadRight(12) & frmAPIHistory.Left.ToString & ", " & frmAPIHistory.Top.ToString)
                            output.Append("WindowState".PadRight(12) & frmAPIHistory.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "trackadvanced"
                            output.AppendLine("Displaying information for form frmTrackAdvanced.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmTrackAdvanced.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmTrackAdvanced.Width.ToString & ", " & frmTrackAdvanced.Height)
                            output.AppendLine("Position".PadRight(12) & frmTrackAdvanced.Left.ToString & ", " & frmTrackAdvanced.Top.ToString)
                            output.Append("WindowState".PadRight(12) & frmTrackAdvanced.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "artistadvanced"
                            output.AppendLine("Displaying information for form frmArtistAdvanced.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmArtistAdvanced.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmArtistAdvanced.Width.ToString & ", " & frmArtistAdvanced.Height)
                            output.AppendLine("Position".PadRight(12) & frmArtistAdvanced.Left.ToString & ", " & frmArtistAdvanced.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmArtistAdvanced.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "albumadvanced"
                            output.AppendLine("Displaying information for form frmAlbumAdvanced.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmAlbumAdvanced.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmAlbumAdvanced.Width.ToString & ", " & frmAlbumAdvanced.Height)
                            output.AppendLine("Position".PadRight(12) & frmAlbumAdvanced.Left.ToString & ", " & frmAlbumAdvanced.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmAlbumAdvanced.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "addqueue"
                            output.AppendLine("Displaying information for form frmAddToQueue.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmAddToQueue.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmAddToQueue.Width.ToString & ", " & frmAddToQueue.Height)
                            output.AppendLine("Position".PadRight(12) & frmAddToQueue.Left.ToString & ", " & frmAddToQueue.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmAddToQueue.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "console"
                            output.AppendLine("Displaying information for form frmConsole.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & Width.ToString & ", " & Height)
                            output.AppendLine("Position".PadRight(12) & Left.ToString & ", " & Top.ToString)
                            output.Append("WindowState".PadRight(12) & WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "scrobblehistory"
                            output.AppendLine("Displaying information for form frmscrobblehistory.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmScrobbleHistory.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmScrobbleHistory.Width.ToString & ", " & frmScrobbleHistory.Height)
                            output.AppendLine("Position".PadRight(12) & frmScrobbleHistory.Left.ToString & ", " & frmScrobbleHistory.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmScrobbleHistory.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "backuptool"
                            output.AppendLine("Displaying information for form frmbackuptool.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmBackupTool.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmBackupTool.Width.ToString & ", " & frmBackupTool.Height)
                            output.AppendLine("Position".PadRight(12) & frmBackupTool.Left.ToString & ", " & frmBackupTool.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmBackupTool.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "scrobbleindexeditor"
                            output.AppendLine("Displaying information for form frmscrobbleindexeditor.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmScrobbleIndexEditor.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmScrobbleIndexEditor.Width.ToString & ", " & frmScrobbleIndexEditor.Height)
                            output.AppendLine("Position".PadRight(12) & frmScrobbleIndexEditor.Left.ToString & ", " & frmScrobbleIndexEditor.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmScrobbleIndexEditor.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "scrobbleindexaddrow"
                            output.AppendLine("Displaying information for form frmscrobbleindexaddrow.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmScrobbleIndexAddRow.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmScrobbleIndexAddRow.Width.ToString & ", " & frmScrobbleIndexAddRow.Height)
                            output.AppendLine("Position".PadRight(12) & frmScrobbleIndexAddRow.Left.ToString & ", " & frmScrobbleIndexAddRow.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmScrobbleIndexAddRow.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "scrobbleindextrackadvanced"
                            output.AppendLine("Displaying information for form frmscrobbleindextrackadvanced.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmScrobbleIndexTrackAdvanced.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmScrobbleIndexTrackAdvanced.Width.ToString & ", " & frmScrobbleIndexTrackAdvanced.Height)
                            output.AppendLine("Position".PadRight(12) & frmScrobbleIndexTrackAdvanced.Left.ToString & ", " & frmScrobbleIndexTrackAdvanced.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmScrobbleIndexTrackAdvanced.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case "scrobblesearch"
                            output.AppendLine("Displaying information for form frmscrobblesearch.")
                            output.AppendLine(String.Empty)
                            output.AppendLine("Open".PadRight(12) & frmScrobbleSearch.Visible.ToString)
                            output.AppendLine("Size".PadRight(12) & frmScrobbleSearch.Width.ToString & ", " & frmScrobbleSearch.Height)
                            output.AppendLine("Position".PadRight(12) & frmScrobbleSearch.Left.ToString & ", " & frmScrobbleSearch.Top.ToString)
                            output.AppendLine("WindowState".PadRight(12) & frmScrobbleSearch.WindowState.ToString)
                            DisplayOut(output.ToString)
                        Case Else
                            DisplayOut("ERROR: Form '" & commands(2) & "' not found. Type 'help frm' for proper command usage.")
                    End Select
                Case Else
                    DisplayOut("ERROR: '" & commands(1) & "' parameter not recognized. Type 'help frm' for proper command usage.")
            End Select
        ElseIf commands.Count = 2 Then
            DisplayOut("ERROR: Missing required parameter 'formname'. Type 'help frm' for proper usage.")
        Else
            DisplayOut("ERROR: Missing required parameter 'open/close/info'. Type 'help frm' for proper usage.")
        End If
    End Sub

    Private Sub CmdSetsk(ByVal commands() As String)
        ' check commands
        If commands.Count >= 2 Then
            ' check auth
            If authorized = True Then
                ' remember old session key
                Dim oldsk As String = My.Settings.SessionKey

                ' set session key
                My.Settings.SessionKey = commands(1)

                ' test session key
                Dim response1 As String = CallAPIAuth("artist.addTags", "artist=test", "tag=test")

                If response1 = ("CallAPIAuth ERROR: The remote server returned an error: (403) Forbidden.") = True Then
                    My.Settings.SessionKey = oldsk
                    DisplayOut("ERROR: Invalid session key provided.")
                ElseIf response1.Contains("ERROR: ") = True Then
                    My.Settings.SessionKey = oldsk
                    DisplayOut("ERROR: Session key was unable to complete test.")
                Else
                    ' second step
                    Dim response2 As String = CallAPI("artist.getTags", My.Settings.User, "artist=test")
                    If response2.Contains("test") = True Then
                        CallAPIAuth("artist.removeTag", "artist=test", "tag=test")
                        frmMain.AuthenticatedUI(True)
                        DisplayOut("Session key has been successfully verified and set.")
                    Else
                        My.Settings.SessionKey = oldsk
                        DisplayOut("ERROR: Session key does not match user.")
                    End If
                End If
            Else
                DisplayOut("ERROR: setsk command requires authorization. Please use the 'auth' command.")
            End If
        Else
            DisplayOut("ERROR: Missing required parameter 'sessionkey'. Type 'help setsk' for proper usage.")
        End If
    End Sub

    Private Sub CmdThreads()
        Dim output As New StringBuilder
        ' charts
        If frmMain.bgwChartUpdater.IsBusy = True Then
            output.AppendLine("Chart update thread Is currently active. (bgwChartUpdater)")
        End If
        ' track
        If frmMain.bgwTrackUpdater.IsBusy = True Then
            output.AppendLine("Track update thread Is currently active. (bgwTrackUpdater)")
        End If
        ' artist
        If frmMain.bgwArtistUpdater.IsBusy = True Then
            output.AppendLine("Artist update thread Is currently active. (bgwArtistUpdater)")
        End If
        ' album
        If frmMain.bgwAlbumUpdater.IsBusy = True Then
            output.AppendLine("Album update thread Is currently active. (bgwAlbumUpdater)")
        End If
        ' tag
        If frmMain.bgwSearchUpdater.IsBusy = True Then
            output.AppendLine("Tag update thread Is currently active. (bgwTagUpdater)")
        End If
        ' user
        If frmMain.bgwUserUpdater.IsBusy = True Then
            output.AppendLine("User update thread Is currently active. (bgwUserUpdater)")
        End If
        ' user loved
        If frmMain.bgwUserLovedUpdater.IsBusy = True Then
            output.AppendLine("User loved tracks update thread Is currently active. (bgwUserLovedUpdater)")
        End If
        ' user chart
        If frmMain.bgwUserChartUpdater.IsBusy = True Then
            output.AppendLine("User charts update thread Is currently active. (bgwUserChartUpdater)")
        End If
        ' user history
        If frmMain.bgwUserHistoryUpdater.IsBusy = True Then
            output.AppendLine("User history update thread Is currently active. (bgwUserHistoryUpdater)")
        End If
        ' user lookup
        If frmMain.bgwUserLookupUpdater.IsBusy = True Then
            output.AppendLine("User lookup update thread Is currently active. (bgwUserLookupUpdater)")
        End If
        ' userl loved
        If frmMain.bgwUserLLovedUpdater.IsBusy = True Then
            output.AppendLine("User lookup loved tracks update thread Is currently active. (bgwUserLLovedUpdater)")
        End If
        ' userl chart
        If frmMain.bgwUserLChartUpdater.IsBusy = True Then
            output.AppendLine("User lookup charts update thread Is currently active. (bgwUserLChartUpdater)")
        End If
        ' userl history
        If frmMain.bgwUserLHistoryUpdater.IsBusy = True Then
            output.AppendLine("User lookup history update thread Is currently active. (bgwUserLHistoryUpdater)")
        End If
        ' check if no threads were active
        If output.ToString = String.Empty Then
            output.Append("No threads are currently active.")
        End If

        DisplayOut(output.ToString)
    End Sub

    Private Sub CmdResetprog()
        progress = 0
        progressmultiplier = 0
        frmMain.UpdateProgress.Value = 100
        frmMain.UpdateProgress.Visible = False
        DisplayOut(String.Empty)
    End Sub

    Private Sub CmdTime()
        Dim output As New StringBuilder
        output.AppendLine("Current Time: " & Now.ToString("G") & " (" & DateToUnix(Now).ToString() & ")")
        output.AppendLine("Current Time (UTC): " & UnixToDate(GetCurrentUTC()).ToString("G") & " (" & GetCurrentUTC.ToString() & ")")
        output.Append("Time Zone Offset: " & (timezoneoffset / 3600).ToString() & ":00 (" & timezoneoffset.ToString() & ")")
        DisplayOut(output.ToString)
    End Sub

    Private Sub CmdViewsk()
        ' check authenticated
        If My.Settings.SessionKey = String.Empty Then
            DisplayOut("Current user is not authenticated.")
        Else
            ' check authorized
            If authorized = True Then
                DisplayOut("Current session key is '" & My.Settings.SessionKey & "' for user '" & My.Settings.User & "'")
            Else
                DisplayOut("ERROR: viewsk command requires authorization. Please use the 'auth' command.")
            End If
        End If
    End Sub

    Private Sub CmdSecret()
        If tmrSecret.Enabled = False Then
            tmrSecret.Start()
        Else
            tmrSecret.Stop()
            Me.BackColor = SystemColors.Control
        End If
        DisplayOut(String.Empty)
    End Sub
#End Region

    ' step 3 - display output
    Public Sub DisplayOut(ByVal output As String)
        txtOutput.AppendText(vbCrLf & output & vbCrLf & vbCrLf & "user>")
        working = False
    End Sub

    ' switches between command history on up/down arrow press
    Private Sub History(sender As Object, e As KeyEventArgs) Handles txtInput.KeyDown
        ' check that there is at least one command entered
        If cmdlist.Count = 0 Then
            Exit Sub
        End If

        ' var for checking whether a command has been entered since the last time this sub was ran
        Static checkvalue As UShort = cmdlist.Count

        ' up
        If e.KeyCode = Keys.Up Then
            e.Handled = True
            If checkvalue <> cmdlist.Count Then  ' new command check (if checkvalue is different than cmdlist.count then a new command has been entered)
                cmdlistcount = cmdlist.Count - 1
                txtInput.Text = cmdlist(cmdlistcount)
                checkvalue = cmdlist.Count
            Else
                If cmdlistcount > 0 Then
                    cmdlistcount -= 1
                End If
                txtInput.Text = cmdlist(cmdlistcount)
            End If
        End If

        ' down
        If e.KeyCode = Keys.Down Then
            e.Handled = True
            If checkvalue <> cmdlist.Count Then
                cmdlistcount = 0
                txtInput.Text = cmdlist(cmdlistcount)
                checkvalue = cmdlist.Count
            Else
                If cmdlistcount < cmdlist.Count - 1 Then
                    cmdlistcount += 1
                End If
                txtInput.Text = cmdlist(cmdlistcount)
            End If
        End If
    End Sub

    ' sends input
    Private Sub Send(sender As Object, e As EventArgs) Handles btnSend.Click
        If working = False Then
            CommandInterpreter(txtInput.Text)
            txtInput.Clear()
        End If
    End Sub

    ' resize controls
    Private Sub ResizeOps(sender As Object, e As EventArgs) Handles Me.Resize
        txtOutput.Width = Width - 40
        txtOutput.Height = Height - 90
        txtInput.Width = Width - 122
    End Sub

    Private Sub tmrSecret_Tick(sender As Object, e As EventArgs) Handles tmrSecret.Tick
        Static colors() As Color = {Color.Red, Color.Green, Color.Blue, Color.Yellow}
        Static count As Byte

        count += 1
        If count > 3 Then
            count = 0
        End If
        BackColor = colors(count)
    End Sub
End Class