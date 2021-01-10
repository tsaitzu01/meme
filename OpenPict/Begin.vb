Public Class Begin
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Me.Hide()
        Timer.Enabled = False
        MainForm.ShowDialog()
        Me.Close()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TransparencyKey = Color.White
        Me.BackColor = Color.White
    End Sub
End Class