Public Class frmAbout
    Private Sub FormLoad(sender As Object, e As EventArgs) Handles Me.Load
        txtAbout.Rtf = "{\rtf1\ansi \b Description\b0 \par Music tracking program that utilizes the Last.fm service \par \par
\b Info\b0 \par .NET Target: 4.7.2 \par Language: VB.NET \par Code Lines: 11,905 \par Project Start Date: 11/11/2020 \par Build Date: 12/15/2021 \par \par
Uses Last.fm - https://www.last.fm \par Copyright" & Chr(169) & " 2022 - Dylan Miller \par Some icons created by Riley Schaefer}"
    End Sub

    Private Sub LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles txtAbout.LinkClicked
        Process.Start(e.LinkText)
    End Sub

    Private Sub ExitForm(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub
End Class