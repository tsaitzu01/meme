Imports System.IO
'-------------------------------------------Importing FireSharp. Make sure you have FireSharp installed on this project.
'-------------------------------------------For more information see here : https://github.com/ziyasal/FireSharp
Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports Newtonsoft.Json
'-------------------------------------------

Public Class EditPic
    Dim IDnum As String = Nothing
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

    Dim pos As MouseEventArgs = Nothing

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            client = New FireSharp.FirebaseClient(fcon)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        lbPWord1.Parent = pbPreview
        lbPWord2.Parent = pbPreview
        lbPWord3.Parent = pbPreview
        lbPWord4.Parent = pbPreview

        lbPWord1.Width = pbPreview.Width / 2
        lbPWord2.Width = pbPreview.Width / 2
        lbPWord3.Width = pbPreview.Width / 2
        lbPWord4.Width = pbPreview.Width / 2
        lbPWord1.Height = 30
        lbPWord2.Height = 30
        lbPWord3.Height = 30
        lbPWord4.Height = 30
        cbSize1.SelectedItem = "12"
        cbSize2.SelectedItem = "12"
        cbSize3.SelectedItem = "12"
        cbSize4.SelectedItem = "12"
        cbFont1.SelectedItem = "微軟正黑體"
        cbFont2.SelectedItem = "微軟正黑體"
        cbFont3.SelectedItem = "微軟正黑體"
        cbFont4.SelectedItem = "微軟正黑體"

        lbPWord1.Location = New Point(0, 0)
        lbPWord2.Location = New Point(0, pbPreview.Height / 4)
        lbPWord3.Location = New Point(0, pbPreview.Height / 2)
        lbPWord4.Location = New Point(0, pbPreview.Height * 3 / 4)

        tbWHSize1.Minimum = 0
        tbWHSize1.Maximum = pbPreview.Height
        tbWHSize1.TickStyle = TickStyle.TopLeft
        tbWHSize1.Value = 40

        tbWHSize2.Minimum = 0
        tbWHSize2.Maximum = pbPreview.Height
        tbWHSize2.TickStyle = TickStyle.TopLeft
        tbWHSize2.Value = 40

        tbWHSize3.Minimum = 0
        tbWHSize3.Maximum = pbPreview.Height
        tbWHSize3.TickStyle = TickStyle.TopLeft
        tbWHSize3.Value = 40

        tbWHSize4.Minimum = 0
        tbWHSize4.Maximum = pbPreview.Height
        tbWHSize4.TickStyle = TickStyle.TopLeft
        tbWHSize4.Value = 40

        tbWWSize1.Minimum = 0
        tbWWSize1.Maximum = pbPreview.Width
        tbWWSize1.TickStyle = TickStyle.TopLeft
        tbWWSize1.Value = pbPreview.Width / 2

        tbWWSize2.Minimum = 0
        tbWWSize2.Maximum = pbPreview.Width
        tbWWSize2.TickStyle = TickStyle.TopLeft
        tbWWSize2.Value = pbPreview.Width / 2

        tbWWSize3.Minimum = 0
        tbWWSize3.Maximum = pbPreview.Width
        tbWWSize3.TickStyle = TickStyle.TopLeft
        tbWWSize3.Value = pbPreview.Width / 2

        tbWWSize4.Minimum = 0
        tbWWSize4.Maximum = pbPreview.Width
        tbWWSize4.TickStyle = TickStyle.TopLeft
        tbWWSize4.Value = pbPreview.Width / 2

        'AddHandler 陳述式 : 執行階段使事件與事件處理常式產生關聯。
        AddHandler lbPWord1.MouseMove, AddressOf lbl_MouseMove
        AddHandler lbPWord1.MouseDown, AddressOf lbl_MouseDown
        AddHandler lbPWord2.MouseMove, AddressOf lbl_MouseMove
        AddHandler lbPWord2.MouseDown, AddressOf lbl_MouseDown
        AddHandler lbPWord3.MouseMove, AddressOf lbl_MouseMove
        AddHandler lbPWord3.MouseDown, AddressOf lbl_MouseDown
        AddHandler lbPWord4.MouseMove, AddressOf lbl_MouseMove
        AddHandler lbPWord4.MouseDown, AddressOf lbl_MouseDown
    End Sub

    Private Sub lbl_MouseMove(ByVal s As Object, ByVal e As MouseEventArgs)
        Dim c As Control = DirectCast(s, Control) ' 型別轉換作業
        If c.Capture Then ' 控制項是否 Capture Mouse
            ' 指定控制項新位置
            c.Location = New Point(e.X + c.Location.X - pos.X, e.Y + c.Location.Y - pos.Y)
        End If
    End Sub

    Private Sub lbl_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs)
        pos = e ' 記錄控制項舊位置
    End Sub

    Private Sub txtWord1_TextChanged(sender As Object, e As EventArgs) Handles txtWord1.TextChanged
        lbPWord1.Text = txtWord1.Text
    End Sub
    Private Sub cbFont1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFont1.SelectedIndexChanged
        lbPWord1.Font = New Font(cbFont1.Text, lbPWord1.Font.Size, lbPWord1.Font.Style)
    End Sub
    Private Sub cbFont1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbFont1.KeyPress
        lbPWord1.Font = New Font(cbFont1.Text, lbPWord1.Font.Size, lbPWord1.Font.Style)
    End Sub
    Private Sub cbSize1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSize1.SelectedIndexChanged
        lbPWord1.Font = New Font(lbPWord1.Font.Name, cbSize1.SelectedItem, lbPWord1.Font.Style)
    End Sub
    Private Sub cbSize1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbSize1.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
            Dim size As Integer
            size = Convert.ToInt32(cbSize1.Text)
            lbPWord1.Font = New Font(lbPWord1.Font.Name, size, lbPWord1.Font.Style)
        End If
    End Sub
    Private Sub tbWHSize1_Scroll(sender As Object, e As EventArgs) Handles tbWHSize1.Scroll
        lbPWord1.TextAlign = ContentAlignment.MiddleCenter
        lbPWord1.Height = tbWHSize1.Value
    End Sub
    Private Sub tbWWSize1_Scroll(sender As Object, e As EventArgs) Handles tbWWSize1.Scroll
        lbPWord1.TextAlign = ContentAlignment.MiddleCenter
        lbPWord1.Width = tbWWSize1.Value
    End Sub
    Private Sub lbWColor1_Click(sender As Object, e As EventArgs) Handles lbWColor1.Click
        If cdWordColor1.ShowDialog() <> DialogResult.Cancel Then
            lbPWord1.ForeColor = cdWordColor1.Color
        End If
    End Sub
    Private Sub rbBlack1_CheckedChanged(sender As Object, e As EventArgs) Handles rbBlack1.CheckedChanged
        lbPWord1.BackColor = Color.Black
    End Sub
    Private Sub rbWhite1_CheckedChanged(sender As Object, e As EventArgs) Handles rbWhite1.CheckedChanged
        lbPWord1.BackColor = Color.White
    End Sub
    Private Sub rbTransparent1_CheckedChanged(sender As Object, e As EventArgs) Handles rbTransparent1.CheckedChanged
        lbPWord1.BackColor = Color.Transparent
    End Sub

    Private Sub txtWord2_TextChanged(sender As Object, e As EventArgs) Handles txtWord2.TextChanged
        lbPWord2.Text = txtWord2.Text
    End Sub
    Private Sub cbFont2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFont2.SelectedIndexChanged
        lbPWord2.Font = New Font(cbFont2.Text, lbPWord2.Font.Size, lbPWord2.Font.Style)
    End Sub
    Private Sub cbFont2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbFont2.KeyPress
        lbPWord2.Font = New Font(cbFont2.Text, lbPWord2.Font.Size, lbPWord2.Font.Style)
    End Sub
    Private Sub cbSize2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSize2.SelectedIndexChanged
        lbPWord2.Font = New Font(lbPWord2.Font.Name, cbSize2.SelectedItem, lbPWord2.Font.Style)
    End Sub
    Private Sub cbSize2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbSize2.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
            Dim size As Integer
            size = Convert.ToInt32(cbSize2.Text)
            lbPWord2.Font = New Font(lbPWord2.Font.Name, size, lbPWord2.Font.Style)
        End If
    End Sub
    Private Sub tbWHSize2_Scroll(sender As Object, e As EventArgs) Handles tbWHSize2.Scroll
        lbPWord2.TextAlign = ContentAlignment.MiddleCenter
        lbPWord2.Height = tbWHSize2.Value
    End Sub
    Private Sub tbWWSize2_Scroll(sender As Object, e As EventArgs) Handles tbWWSize2.Scroll
        lbPWord2.TextAlign = ContentAlignment.MiddleCenter
        lbPWord2.Width = tbWWSize2.Value
    End Sub
    Private Sub lbWColor2_Click(sender As Object, e As EventArgs) Handles lbWColor2.Click
        If cdWordColor2.ShowDialog() <> DialogResult.Cancel Then
            lbPWord2.ForeColor = cdWordColor2.Color
        End If
    End Sub
    Private Sub rbBlack2_CheckedChanged(sender As Object, e As EventArgs) Handles rbBlack2.CheckedChanged
        lbPWord2.BackColor = Color.Black
    End Sub
    Private Sub rbWhite2_CheckedChanged(sender As Object, e As EventArgs) Handles rbWhite2.CheckedChanged
        lbPWord2.BackColor = Color.White
    End Sub
    Private Sub rbTransparent2_CheckedChanged(sender As Object, e As EventArgs) Handles rbTransparent2.CheckedChanged
        lbPWord2.BackColor = Color.Transparent
    End Sub

    Private Sub txtWord3_TextChanged(sender As Object, e As EventArgs) Handles txtWord3.TextChanged
        lbPWord3.Text = txtWord3.Text
    End Sub
    Private Sub cbFont3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFont3.SelectedIndexChanged
        lbPWord3.Font = New Font(cbFont3.Text, lbPWord3.Font.Size, lbPWord3.Font.Style)
    End Sub
    Private Sub cbFont3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbFont3.KeyPress
        lbPWord3.Font = New Font(cbFont3.Text, lbPWord3.Font.Size, lbPWord3.Font.Style)
    End Sub
    Private Sub cbSize3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSize3.SelectedIndexChanged
        lbPWord3.Font = New Font(lbPWord3.Font.Name, cbSize3.SelectedItem, lbPWord3.Font.Style)
    End Sub
    Private Sub cbSize3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbSize3.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
            Dim size As Integer
            size = Convert.ToInt32(cbSize3.Text)
            lbPWord3.Font = New Font(lbPWord3.Font.Name, size, lbPWord3.Font.Style)
        End If
    End Sub
    Private Sub tbWHSize3_Scroll(sender As Object, e As EventArgs) Handles tbWHSize3.Scroll
        lbPWord3.TextAlign = ContentAlignment.MiddleCenter
        lbPWord3.Height = tbWHSize3.Value
    End Sub
    Private Sub tbWWSize3_Scroll(sender As Object, e As EventArgs) Handles tbWWSize3.Scroll
        lbPWord3.TextAlign = ContentAlignment.MiddleCenter
        lbPWord3.Width = tbWWSize3.Value
    End Sub
    Private Sub lbWColor3_Click(sender As Object, e As EventArgs) Handles lbWColor3.Click
        If cdWordColor3.ShowDialog() <> DialogResult.Cancel Then
            lbPWord3.ForeColor = cdWordColor3.Color
        End If
    End Sub
    Private Sub rbBlack3_CheckedChanged(sender As Object, e As EventArgs) Handles rbBlack3.CheckedChanged
        lbPWord3.BackColor = Color.Black
    End Sub
    Private Sub rbWhite3_CheckedChanged(sender As Object, e As EventArgs) Handles rbWhite3.CheckedChanged
        lbPWord3.BackColor = Color.White
    End Sub
    Private Sub rbTransparent3_CheckedChanged(sender As Object, e As EventArgs) Handles rbTransparent3.CheckedChanged
        lbPWord3.BackColor = Color.Transparent
    End Sub

    Private Sub txtWord4_TextChanged(sender As Object, e As EventArgs) Handles txtWord4.TextChanged
        lbPWord4.Text = txtWord4.Text
    End Sub
    Private Sub cbFont4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFont4.SelectedIndexChanged
        lbPWord4.Font = New Font(cbFont4.Text, lbPWord4.Font.Size, lbPWord4.Font.Style)
    End Sub
    Private Sub cbFont4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbFont4.KeyPress
        lbPWord4.Font = New Font(cbFont4.Text, lbPWord4.Font.Size, lbPWord4.Font.Style)
    End Sub
    Private Sub cbSize4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSize4.SelectedIndexChanged
        lbPWord4.Font = New Font(lbPWord4.Font.Name, cbSize4.SelectedItem, lbPWord4.Font.Style)
    End Sub
    Private Sub cbSize4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbSize4.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
            Dim size As Integer
            size = Convert.ToInt32(cbSize4.Text)
            lbPWord4.Font = New Font(lbPWord4.Font.Name, size, lbPWord4.Font.Style)
        End If
    End Sub
    Private Sub tbWHSize4_Scroll(sender As Object, e As EventArgs) Handles tbWHSize4.Scroll
        lbPWord4.TextAlign = ContentAlignment.MiddleCenter
        lbPWord4.Height = tbWHSize4.Value
    End Sub
    Private Sub tbWWSize4_Scroll(sender As Object, e As EventArgs) Handles tbWWSize4.Scroll
        lbPWord4.TextAlign = ContentAlignment.MiddleCenter
        lbPWord4.Width = tbWWSize4.Value
    End Sub
    Private Sub lbWColor4_Click(sender As Object, e As EventArgs) Handles lbWColor4.Click
        If cdWordColor4.ShowDialog() <> DialogResult.Cancel Then
            lbPWord4.ForeColor = cdWordColor4.Color
        End If
    End Sub
    Private Sub rbBlack4_CheckedChanged(sender As Object, e As EventArgs) Handles rbBlack4.CheckedChanged
        lbPWord4.BackColor = Color.Black
    End Sub
    Private Sub rbWhite4_CheckedChanged(sender As Object, e As EventArgs) Handles rbWhite4.CheckedChanged
        lbPWord4.BackColor = Color.White
    End Sub
    Private Sub rbTransparent4_CheckedChanged(sender As Object, e As EventArgs) Handles rbTransparent4.CheckedChanged
        lbPWord4.BackColor = Color.Transparent
    End Sub

    Private Sub btnStore_Click(sender As Object, e As EventArgs) Handles btnStore.Click
        Dim choice As DialogResult
        choice = MessageBox.Show("是否確定要儲存圖片?", "儲存",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1)
        If choice = DialogResult.Yes Then
            Try
                Me.Text = "MEME—編輯圖片 (Saving. Please wait...)"

                Try
                    Dim res As FirebaseResponse = client.Get("ImageDB")
                    Dim data As Dictionary(Of String, ImageData) = JsonConvert.DeserializeObject(Of Dictionary(Of String, ImageData))(res.Body.ToString())
                    Dim num As Integer = data.Count + 1
                    Dim IDresults As String = Strings.Right("00000" & num.ToString(), 5)

                    Dim Check_ID = client.Get("ImageDB/" + IDresults)

                    '-------------------------------------------Conditions to check whether the ID has been used.
                    If Check_ID.Body <> "null" Then
                        MessageBox.Show("The same ID is found, create another ID by pressing the Create ID button.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        IDnum = IDresults
                    End If
                    '-------------------------------------------
                Catch ex As Exception
                    If ex.Message = "One or more errors occurred." Then
                        IDnum = Nothing
                        MessageBox.Show("Cannot connect to firebase, check your network !", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        IDnum = Nothing
                        MessageBox.Show(ex.Message, "Error Messageee", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Try

                Using b As New Bitmap(pbPreview.Width, pbPreview.Height)
                    pbPreview.DrawToBitmap(b, New Rectangle(0, 0, pbPreview.Width, pbPreview.Height))
                    b.Save("temp.png")
                    Dim img As New Bitmap("temp.png")
                    Dim img1 As Image = DirectCast(img, Image)
                    Dim ImgData As String = ImageToBase64(img1)

                    Dim ID As New ImageData() With
                    {
                    .ID = IDnum,
                    .Image = ImgData,
                    .Time = Now
                    }
                    Dim save = client.Set("ImageDB/" + IDnum, ID) 'To save data to Firebase Database.
                End Using
                Me.Text = "MEME—編輯圖片"

                MessageBox.Show("Data saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If choice = DialogResult.Yes Then
                    Final.Show()
                    Me.Close()
                End If
            Catch ex As Exception
                If ex.Message = "One or more errors occurred." Then
                    MessageBox.Show("Cannot connect to firebase, check your network !", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Me.Text = "MEME—編輯圖片"
            End Try
        End If
    End Sub

    Private Sub btnBefore_Click(sender As Object, e As EventArgs) Handles btnBefore.Click
        OpenPict.Show()
        Me.Close()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim choice As DialogResult
        choice = MessageBox.Show("是否確定要離開?", "提醒",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1)
        If choice = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub lbPWord1_Click(sender As Object, e As EventArgs) Handles lbPWord1.Click
        Me.tcEWB1.SelectedTab = Me.TabPage1
    End Sub

    Private Sub lbPWord2_Click(sender As Object, e As EventArgs) Handles lbPWord2.Click
        Me.tcEWB1.SelectedTab = Me.tcEWB2
    End Sub

    Private Sub lbPWord3_Click(sender As Object, e As EventArgs) Handles lbPWord3.Click
        Me.tcEWB1.SelectedTab = Me.tcEWB3
    End Sub

    Private Sub lbPWord4_Click(sender As Object, e As EventArgs) Handles lbPWord4.Click
        Me.tcEWB1.SelectedTab = Me.tcEWB4
    End Sub
End Class
