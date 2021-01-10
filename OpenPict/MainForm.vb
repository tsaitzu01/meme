Imports System.IO

Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports Newtonsoft.Json

Public Class MainForm
    '-------------------------------------------Configure FireSharp
    Public fcon As New FirebaseConfig() With
        {
        .AuthSecret = "(Input your own Auth Secret)",
        .BasePath = "https://vbnetdatabase-default-rtdb.firebaseio.com/"
        }
    Public client As IFirebaseClient
    '-------------------------------------------

    '-------------------------------------------Function to convert from images to Base64 and vice versa.
    Public Function ImageToBase64(image As Image) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]  
            Dim Format As System.Drawing.Imaging.ImageFormat = Imaging.ImageFormat.Jpeg
            image.Save(ms, Format)
            Dim imageBytes As Byte() = ms.ToArray()

            ' Convert byte[] to Base64 String  
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function

    Public Function Base64ToImage(base64String As String) As Image
        ' Convert Base64 String to byte[]  
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

        ' Convert byte[] to Image  
        ms.Write(imageBytes, 0, imageBytes.Length)
        Dim image__1 As Image = System.Drawing.Image.FromStream(ms, True)
        Return image__1
    End Function
    '-------------------------------------------

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        SelectMode.Show()
        Me.Close()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            client = New FireSharp.FirebaseClient(fcon)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim res As FirebaseResponse = client.Get("ImageDB")
        Dim data As Dictionary(Of String, ImageData) = JsonConvert.DeserializeObject(Of Dictionary(Of String, ImageData))(res.Body.ToString())
        PictureBox1.Image = Base64ToImage(data.Values(data.Count - 1).Image)
        PictureBox2.Image = Base64ToImage(data.Values(data.Count - 2).Image)
        PictureBox3.Image = Base64ToImage(data.Values(data.Count - 3).Image)
        PictureBox4.Image = Base64ToImage(data.Values(data.Count - 4).Image)
        PictureBox5.Image = Base64ToImage(data.Values(data.Count - 5).Image)
        PictureBox6.Image = Base64ToImage(data.Values(data.Count - 6).Image)
        PictureBox7.Image = Base64ToImage(data.Values(data.Count - 7).Image)
        PictureBox8.Image = Base64ToImage(data.Values(data.Count - 8).Image)

        For index As Integer = 0 To 5
            Debug.Write(index.ToString & " ")
        Next
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreviewP1.Click, btnPreviewP2.Click, btnPreviewP3.Click, btnPreviewP4.Click, btnPreviewP5.Click, btnPreviewP6.Click, btnPreviewP7.Click, btnPreviewP8.Click
        Dim num1 As Integer
        num1 = DirectCast((sender), Button).Tag
        PreviewPic.PreviewPic_Show(num1)
        PreviewPic.Show()
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload1.Click, btnDownload2.Click, btnDownload3.Click, btnDownload4.Click, btnDownload5.Click, btnDownload6.Click, btnDownload7.Click, btnDownload8.Click
        Try
            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "Image (*.png)|*.png|All files (*.*)|*.*"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = False

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    Select Case DirectCast((sender), Button).Tag
                        Case 1
                            PictureBox1.Image.Save(saveFileDialog1.FileName + ".png")
                        Case 2
                            PictureBox2.Image.Save(saveFileDialog1.FileName + ".png")
                        Case 3
                            PictureBox3.Image.Save(saveFileDialog1.FileName + ".png")
                        Case 4
                            PictureBox4.Image.Save(saveFileDialog1.FileName + ".png")
                        Case 5
                            PictureBox5.Image.Save(saveFileDialog1.FileName + ".png")
                        Case 6
                            PictureBox6.Image.Save(saveFileDialog1.FileName + ".png")
                        Case 7
                            PictureBox7.Image.Save(saveFileDialog1.FileName + ".png")
                        Case 8
                            PictureBox8.Image.Save(saveFileDialog1.FileName + ".png")
                    End Select
                    myStream.Close()
                    My.Computer.FileSystem.DeleteFile(saveFileDialog1.FileName)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDownload5.Click, btnDownload4.Click, btnDownload3.Click, btnDownload2.Click, btnDownload8.Click, btnDownload7.Click, btnDownload6.Click, btnDownload1.Click

    End Sub
End Class