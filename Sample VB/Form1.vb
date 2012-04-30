Imports Clockwork
Imports System.Net

Public Class Form1

    Private Sub sendMessage_Click(sender As System.Object, e As System.EventArgs) Handles sendMessage.Click
        Try
            Dim api As Clockwork.API = New Clockwork.API(key.Text)
            Dim sms As Clockwork.SMS = New Clockwork.SMS()
            sms.To = number.Text
            sms.Message = message.Text

            Dim result As SMSResult = api.Send(sms)

            If (result.Success) Then
                MessageBox.Show("Sent - ID: " + result.ID)
            Else
                MessageBox.Show("Error: " + result.ErrorMessage)
            End If

        Catch ex As APIException
            ' You'll get an API exception for errors 
            ' such as wrong key
            MessageBox.Show("API Exception: " + ex.Message)
        Catch ex As WebException
            'Web exceptions mean you couldn't reach the mediaburst server
            MessageBox.Show("Web Exception: " + ex.Message)
        Catch ex As ArgumentException
            ' Argument exceptions are thrown for missing parameters,
            ' such as forgetting to set the username
            MessageBox.Show("Argument Exception: " + ex.Message)
        Catch ex As Exception
            ' Something else went wrong, the error message should help
            MessageBox.Show("Unknown Exception: " + ex.Message)
        End Try
    End Sub
End Class