Imports System.IO

Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports Newtonsoft.Json

Public Class PreviewPic
    '-------------------------------------------Configure FireSharp
    Public fcon As New FirebaseConfig() With
        {
        .AuthSecret = "(Input your own Auth Secret)",
        .BasePath = "https://vbnetdatabase-default-rtdb.firebaseio.com/"
        }
    Public client As IFirebaseClient
    '-------------------------------------------

    Public Sub PreviewPic_Show(ByVal num As Integer)
        Try
            client = New FireSharp.FirebaseClient(fcon)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim res As FirebaseResponse = client.Get("ImageDB")
        Dim data As Dictionary(Of String, ImageData) = JsonConvert.DeserializeObject(Of Dictionary(Of String, ImageData))(res.Body.ToString())

        Select Case num
            Case 1
                PictureBox1.Image = MainForm.PictureBox1.Image
                Label1.Text = data.Values(data.Count - 1).Time
            Case 2
                PictureBox1.Image = MainForm.PictureBox2.Image
                Label1.Text = data.Values(data.Count - 2).Time
            Case 3
                PictureBox1.Image = MainForm.PictureBox3.Image
                Label1.Text = data.Values(data.Count - 3).Time
            Case 4
                PictureBox1.Image = MainForm.PictureBox4.Image
                Label1.Text = data.Values(data.Count - 4).Time
            Case 5
                PictureBox1.Image = MainForm.PictureBox5.Image
                Label1.Text = data.Values(data.Count - 5).Time
            Case 6
                PictureBox1.Image = MainForm.PictureBox6.Image
                Label1.Text = data.Values(data.Count - 6).Time
            Case 7
                PictureBox1.Image = MainForm.PictureBox7.Image
                Label1.Text = data.Values(data.Count - 7).Time
            Case 8
                PictureBox1.Image = MainForm.PictureBox8.Image
                Label1.Text = data.Values(data.Count - 8).Time
        End Select
    End Sub

    Private Sub Preview_Picture_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label1.Font = New Font("Lucida Fax", Label1.Font.Size, Label1.Font.Style)
        Label1.Location = New Point(0, PictureBox1.Height)
        Label1.Width = PictureBox1.Width
        Me.Width = PictureBox1.Width + 15
        Me.Height = PictureBox1.Height + Label1.Height + 40
    End Sub
End Class