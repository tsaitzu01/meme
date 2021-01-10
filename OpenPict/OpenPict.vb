Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Image
Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports Newtonsoft.Json

Public Class OpenPict
    Dim opendialog As New OpenFileDialog()
    Dim picName As String

    '-------------------------------------------Configure FireSharp
    Public fcon As New FirebaseConfig() With
        {
        .AuthSecret = "wIyDJxCoubhdJj1M5gByeRJdGt5PHmFvWkAlzFnu",
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            opendialog.Filter = "Images |*.png; *.bmp; *.jpg; *.jpeg; *.gif; *.ico;"
            If (opendialog.ShowDialog() = DialogResult.OK) Then
                inputPic.ImageLocation = opendialog.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
        If SelectMode.RB_v1.Checked Then
            EditPic.pbPreview.ImageLocation = inputPic.ImageLocation
            EditPic.pbPreview.Location = New Point(17, 90)
            EditPic.pbPreview.BackColor = Color.White
            EditPic.pbPreview.Height = 430
            EditPic.pbPreview.Width = 430
        ElseIf SelectMode.RB_v2_1.Checked Then
            If GroupBox1.Text = "圖片一" Then
                UserPic2_1.PictureBox1.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片二" Then
                UserPic2_1.PictureBox2.Image = FromFile(inputPic.ImageLocation)
            End If
        ElseIf SelectMode.RB_v2_2.Checked Then
            If GroupBox1.Text = "圖片一" Then
                UserPic2_2.PictureBox1.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片二" Then
                UserPic2_2.PictureBox2.Image = FromFile(inputPic.ImageLocation)
            End If
        ElseIf SelectMode.RB_v3_1.Checked Then
            If GroupBox1.Text = "圖片一" Then
                UserPic3_1.PictureBox1.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片二" Then
                UserPic3_1.PictureBox2.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片三" Then
                UserPic3_1.PictureBox3.Image = FromFile(inputPic.ImageLocation)
            End If
        ElseIf SelectMode.RB_v3_2.Checked Then
            If GroupBox1.Text = "圖片一" Then
                UserPic3_2.PictureBox1.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片二" Then
                UserPic3_2.PictureBox2.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片三" Then
                UserPic3_2.PictureBox3.Image = FromFile(inputPic.ImageLocation)
            End If
        ElseIf SelectMode.RB_v3_3.Checked Then
            If GroupBox1.Text = "圖片一" Then
                UserPic3_3.PictureBox1.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片二" Then
                UserPic3_3.PictureBox2.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片三" Then
                UserPic3_3.PictureBox3.Image = FromFile(inputPic.ImageLocation)
            End If
        ElseIf SelectMode.RB_v4_1.Checked Then
            If GroupBox1.Text = "圖片一" Then
                UserPic4_1.PictureBox1.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片二" Then
                UserPic4_1.PictureBox2.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片三" Then
                UserPic4_1.PictureBox3.Image = FromFile(inputPic.ImageLocation)
            ElseIf GroupBox1.Text = "圖片四" Then
                UserPic4_1.PictureBox4.Image = FromFile(inputPic.ImageLocation)
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim res As FirebaseResponse = client.Get("Sample")
        Dim data As Dictionary(Of String, ImageData) = JsonConvert.DeserializeObject(Of Dictionary(Of String, ImageData))(res.Body.ToString())

        inputPic.Image = Nothing
        '一張圖
        If (SelectMode.RB_v1.Checked And inputPic.ImageLocation = "") Then
            getRB_Id()
            If picName = "" Then
                MessageBox.Show("請選擇或上傳一張圖片", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Else
                EditPic.pbPreview.Image = Base64ToImage(data.Item(picName).img)
                EditPic.pbPreview.Location = New Point(17, 90)
                EditPic.pbPreview.Height = 430
                EditPic.pbPreview.Width = 430
                EditPic.Show()
                Me.Close()
            End If
        ElseIf SelectMode.RB_v1.Checked Then
            EditPic.Show()
            Me.Close()
        End If
        '兩張直圖
        If (SelectMode.RB_v2_1.Checked And inputPic.ImageLocation = "") Then
            getRB_Id()
            If picName = "" Then
                MessageBox.Show("請選擇或上傳一張圖片", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Else
                If GroupBox1.Text = "圖片一" Then
                    UserPic2_1.PictureBox1.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片二"
                    pbPic1.Image = UserPic2_1.PictureBox1.Image
                    lblPic1.Visible = True
                ElseIf GroupBox1.Text = "圖片二" Then
                    UserPic2_1.PictureBox2.Image = Base64ToImage(data.Item(picName).img)
                    loadPic()
                End If
            End If
        ElseIf SelectMode.RB_v2_1.Checked Then
            UserPic2()
        End If

        '兩張橫圖
        If (SelectMode.RB_v2_2.Checked And inputPic.ImageLocation = "") Then
            getRB_Id()
            If picName = "" Then
                MessageBox.Show("請選擇或上傳一張圖片", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Else
                If GroupBox1.Text = "圖片一" Then
                    UserPic2_2.PictureBox1.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片二"
                    pbPic1.Image = UserPic2_2.PictureBox1.Image
                    lblPic1.Visible = True
                ElseIf GroupBox1.Text = "圖片二" Then
                    UserPic2_2.PictureBox2.Image = Base64ToImage(data.Item(picName).img)
                    loadPic()
                End If
            End If
        ElseIf SelectMode.RB_v2_2.Checked Then
            UserPic2()
        End If
        '三張圖附文字
        If (SelectMode.RB_v3_1.Checked And inputPic.ImageLocation = "") Then
            getRB_Id()
            If picName = "" Then
                MessageBox.Show("請選擇或上傳一張圖片", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Else
                If GroupBox1.Text = "圖片一" Then
                    UserPic3_1.PictureBox1.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片二"
                    pbPic1.Image = UserPic3_1.PictureBox1.Image
                    lblPic1.Visible = True
                ElseIf GroupBox1.Text = "圖片二" Then
                    UserPic3_1.PictureBox2.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片三"
                    pbPic2.Image = UserPic3_1.PictureBox2.Image
                    lblPic2.Visible = True
                ElseIf GroupBox1.Text = "圖片三" Then
                    UserPic3_1.PictureBox3.Image = Base64ToImage(data.Item(picName).img)
                    loadPic()
                End If
            End If
        ElseIf SelectMode.RB_v3_1.Checked Then
            UserPic3()
        End If
        '三張圖交錯附文字
        If (SelectMode.RB_v3_2.Checked And inputPic.ImageLocation = "") Then
            getRB_Id()
            If picName = "" Then
                MessageBox.Show("請選擇或上傳一張圖片", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Else
                If GroupBox1.Text = "圖片一" Then
                    UserPic3_2.PictureBox1.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片二"
                    pbPic1.Image = UserPic3_2.PictureBox1.Image
                    lblPic1.Visible = True
                ElseIf GroupBox1.Text = "圖片二" Then
                    UserPic3_2.PictureBox2.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片三"
                    pbPic2.Image = UserPic3_2.PictureBox2.Image
                    lblPic2.Visible = True
                ElseIf GroupBox1.Text = "圖片三" Then
                    UserPic3_2.PictureBox3.Image = Base64ToImage(data.Item(picName).img)
                    loadPic()
                End If
            End If
        ElseIf SelectMode.RB_v3_2.Checked Then
            UserPic3()
        End If
        '三張橫圖
        If (SelectMode.RB_v3_3.Checked And inputPic.ImageLocation = "") Then
            getRB_Id()
            If picName = "" Then
                MessageBox.Show("請選擇或上傳一張圖片", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Else
                If GroupBox1.Text = "圖片一" Then
                    UserPic3_3.PictureBox1.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片二"
                    pbPic1.Image = UserPic3_3.PictureBox1.Image
                    lblPic1.Visible = True
                ElseIf GroupBox1.Text = "圖片二" Then
                    UserPic3_3.PictureBox2.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片三"
                    pbPic2.Image = UserPic3_3.PictureBox2.Image
                    lblPic2.Visible = True
                ElseIf GroupBox1.Text = "圖片三" Then
                    UserPic3_3.PictureBox3.Image = Base64ToImage(data.Item(picName).img)
                    loadPic()
                End If
            End If
        ElseIf SelectMode.RB_v3_3.Checked Then
            UserPic3()
        End If
        '四張圖
        If (SelectMode.RB_v4_1.Checked And inputPic.ImageLocation = "") Then
            getRB_Id()
            If picName = "" Then
                MessageBox.Show("請選擇或上傳一張圖片", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Else
                If GroupBox1.Text = "圖片一" Then
                    UserPic4_1.PictureBox1.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片二"
                    pbPic1.Image = UserPic4_1.PictureBox1.Image
                    lblPic1.Visible = True
                ElseIf GroupBox1.Text = "圖片二" Then
                    UserPic4_1.PictureBox2.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片三"
                    pbPic2.Image = UserPic4_1.PictureBox2.Image
                    lblPic2.Visible = True
                ElseIf GroupBox1.Text = "圖片三" Then
                    UserPic4_1.PictureBox3.Image = Base64ToImage(data.Item(picName).img)
                    GroupBox1.Text = "圖片四"
                    pbPic3.Image = UserPic4_1.PictureBox3.Image
                    lblPic3.Visible = True
                ElseIf GroupBox1.Text = "圖片四" Then
                    UserPic4_1.PictureBox4.Image = Base64ToImage(data.Item(picName).img)
                    loadPic()
                End If
            End If
        ElseIf SelectMode.RB_v4_1.Checked Then
            UserPic4()
        End If
    End Sub

    Function getRB_Id()
        picName = ""
        For Each ctl As Control In Me.GroupBox1.Controls
            If TypeOf (ctl) Is RadioButton Then
                Dim rb As RadioButton = CType(ctl, RadioButton)
                If rb.Checked Then
                    picName = rb.Name
                    rb.Checked = False
                End If
            End If
        Next
    End Function

    Function UserPic2()
        If GroupBox1.Text = "圖片一" Then
            GroupBox1.Text = "圖片二"
            pbPic1.ImageLocation = inputPic.ImageLocation
            lblPic1.Visible = True
        ElseIf GroupBox1.Text = "圖片二" Then
            loadPic()
        End If
        inputPic.ImageLocation = Nothing
    End Function

    Function UserPic3()
        If GroupBox1.Text = "圖片一" Then
            GroupBox1.Text = "圖片二"
            pbPic1.ImageLocation = inputPic.ImageLocation
            lblPic1.Visible = True
        ElseIf GroupBox1.Text = "圖片二" Then
            GroupBox1.Text = "圖片三"
            pbPic2.ImageLocation = inputPic.ImageLocation
            lblPic2.Visible = True
        ElseIf GroupBox1.Text = "圖片三" Then
            loadPic()
        End If
        inputPic.ImageLocation = Nothing
    End Function

    Function UserPic4()
        If GroupBox1.Text = "圖片一" Then
            GroupBox1.Text = "圖片二"
            pbPic1.ImageLocation = inputPic.ImageLocation
            lblPic1.Visible = True
        ElseIf GroupBox1.Text = "圖片二" Then
            GroupBox1.Text = "圖片三"
            pbPic2.ImageLocation = inputPic.ImageLocation
            lblPic2.Visible = True
        ElseIf GroupBox1.Text = "圖片三" Then
            GroupBox1.Text = "圖片四"
            pbPic3.ImageLocation = inputPic.ImageLocation
            lblPic3.Visible = True
        ElseIf GroupBox1.Text = "圖片四" Then
            loadPic()
        End If
        inputPic.ImageLocation = Nothing
    End Function

    Function loadPic()
        Me.Close()
        If SelectMode.RB_v2_1.Checked Then
            UserPic2_1.Show()
            EditPic.pbPreview.Location = New Point(17, 100)
            EditPic.pbPreview.Height = 350
            EditPic.pbPreview.Width = 440
            Using b As New Bitmap(UserPic2_1.Width, UserPic2_1.Height)
                UserPic2_1.DrawToBitmap(b, New Rectangle(0, 0, UserPic2_1.Width, UserPic2_1.Height))
                b.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png)
                EditPic.pbPreview.ImageLocation = "temp.png"
                UserPic2_1.Close()
            End Using
        ElseIf SelectMode.RB_v2_2.Checked Then
            UserPic2_2.Show()
            Using b As New Bitmap(UserPic2_2.Width, UserPic2_2.Height)
                UserPic2_2.DrawToBitmap(b, New Rectangle(0, 0, UserPic2_2.Width, UserPic2_2.Height))
                b.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png)
                EditPic.pbPreview.ImageLocation = "temp.png"
                UserPic2_2.Close()
            End Using
        ElseIf SelectMode.RB_v3_1.Checked Then
            UserPic3_1.Show()
            Using b As New Bitmap(UserPic3_1.Width, UserPic3_1.Height)
                UserPic3_1.DrawToBitmap(b, New Rectangle(0, 0, UserPic3_1.Width, UserPic3_1.Height))
                b.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png)
                EditPic.pbPreview.ImageLocation = "temp.png"
                UserPic3_1.Close()
            End Using
        ElseIf SelectMode.RB_v3_2.Checked Then
            UserPic3_2.Show()
            Using b As New Bitmap(UserPic3_2.Width, UserPic3_2.Height)
                UserPic3_2.DrawToBitmap(b, New Rectangle(0, 0, UserPic3_2.Width, UserPic3_2.Height))
                b.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png)
                EditPic.pbPreview.ImageLocation = "temp.png"
                UserPic3_2.Close()
            End Using
        ElseIf SelectMode.RB_v3_3.Checked Then
            UserPic3_3.Show()
            Using b As New Bitmap(UserPic3_3.Width, UserPic3_3.Height)
                UserPic3_3.DrawToBitmap(b, New Rectangle(0, 0, UserPic3_3.Width, UserPic3_3.Height))
                b.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png)
                EditPic.pbPreview.ImageLocation = "temp.png"
                UserPic3_3.Close()
            End Using
        ElseIf SelectMode.RB_v4_1.Checked Then
            EditPic.pbPreview.Location = New Point(17, 90)
            EditPic.pbPreview.Height = 430
            EditPic.pbPreview.Width = 430
            UserPic4_1.Show()
            Using b As New Bitmap(UserPic4_1.Width, UserPic4_1.Height)
                UserPic4_1.DrawToBitmap(b, New Rectangle(0, 0, UserPic4_1.Width, UserPic4_1.Height))
                b.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png)
                EditPic.pbPreview.ImageLocation = "temp.png"
                UserPic4_1.Close()
            End Using
        End If
        EditPic.Show()
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SelectMode.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim choice As DialogResult
        choice = MessageBox.Show("是否確定要離開?", "提醒",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1)
        If choice = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub OpenPict_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            client = New FireSharp.FirebaseClient(fcon)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class