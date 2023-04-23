Public Class frmAuthentication
    Private Sub frmAuthentication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblInstruction.Text = "This window will allow you to authenticate your user account." & vbCrLf & "Click OK to begin the process."
        lblStatus.Text = ""
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Static authenticatestep As Byte
        Static token(0) As String

        Select Case authenticatestep
            Case 0  ' step 1 get token
                ' button stuff
                btnOK.Enabled = False
                btnOK.Text = "&OK"
                btnCancel.Text = "&Cancel"

                ' set labels
                lblInstruction.Text = ""
                lblStatus.Text = "Retrieving token..."

                ' get token
                Dim tokenXML As String = CallAPI("auth.getToken")
                token(0) = "token"
                ParseXML(tokenXML, "/lfm", 0, token)

                ' set labels
                lblInstruction.Text = "You will now be taken to the Last.fm authentication page." & vbCrLf & "Once you have authenticated, please come back to this window."
                lblStatus.Text = "Click OK to continue."

                ' enable button
                btnOK.Enabled = True
                btnOK.Select()

                ' go to next step
                authenticatestep = 1
            Case 1  ' step 2 authenticate with last
                ' set labels
                lblInstruction.Text = "Click " & Chr(34) & "Allow Access" & Chr(34) & " on the webpage." & vbCrLf & "Return here and click OK when finished."
                lblStatus.Text = ""

                ' open web browser for user to authenticate
                Process.Start("http://www.last.fm/api/auth/?api_key=27a6f7ec4ae4cd5bca77c7639a78abc0&token=" & token(0))

                ' go to next step
                authenticatestep = 2
            Case 2  ' step 3 get session
                ' set labels and stuff
                btnOK.Enabled = False
                lblInstruction.Text = ""
                lblStatus.Text = "Getting session key..."

                ' get session key
                Try
                    ' get session
                    Dim key As String = CallAPI("auth.getSession", "", "token=" & token(0), "api_sig=" & CreateSignature("auth.getSession", "token" & token(0), "", "", "", False))
                    Dim keynodes() As String = {"key", "name"}
                    ParseXML(key, "/lfm/session", 0, keynodes)

                    ' check for errors
                    If keynodes(0).Contains("ERROR: ") = True Then
                        lblInstruction.Text = "Authentication unsuccessful." & vbCrLf & "Please try again later."
                        lblStatus.Text = String.Empty

                        ' allow user to retry authentication
                        btnOK.Text = "&Retry"
                        btnOK.Enabled = True
                        btnOK.Select()
                        authenticatestep = 0
                        Exit Sub
                    End If

                    ' check for wrong account
                    If keynodes(1).ToLower <> My.Settings.User.ToLower Then
                        lblInstruction.Text = "Error: you have validated for the wrong account." & vbCrLf & "Make sure you are signed in to the correct account."
                        lblStatus.Text = String.Empty

                        ' allow user to retry authentication
                        btnOK.Text = "&Retry"
                        btnOK.Enabled = True
                        btnOK.Select()
                        authenticatestep = 0
                        Exit Sub
                    End If

                    ' save key
                    My.Settings.SessionKey = keynodes(0)
                    My.Settings.Save()

                    ' tell main form to allow authentication features
                    frmMain.AuthenticatedUI(True)

                    ' set labels and buttons
                    lblInstruction.Text = "Authentication successful!" & vbCrLf & "You can now close this window."
                    lblStatus.Text = ""
                    btnCancel.Text = "&Close"
                Catch ex As Exception   ' check for errors
                    lblInstruction.Text = "Authentication unsuccessful." & vbCrLf & "Please try again."
                    lblStatus.Text = "Message: " & ex.Message

                    ' allow user to retry authentication
                    btnOK.Text = "&Retry"
                    btnOK.Enabled = True
                    btnOK.Select()
                    authenticatestep = 0
                End Try
        End Select
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class