Public Class Form1
    Public Sub shifrator(ByVal Pass As String, ByVal InputFile As String, ByVal OutputFile As String, ByVal Crypt As Boolean)
        Dim ByteIN As Byte() = IO.File.ReadAllBytes(InputFile), ByteOut(ByteIN.Length - 1) As Byte, PL As Integer = Pass.Length, bPass() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(Pass), biPass(bPass.Length - 1) As Integer
        bPass.CopyTo(biPass, 0)
        If Crypt Then
            For i As Integer = 0 To ByteIN.Length - 1
                ByteOut(i) = (ByteIN(i) + biPass(i Mod PL)) Mod 256
            Next
        Else
            For i As Integer = 0 To ByteIN.Length - 1
                ByteOut(i) = (256 + ByteIN(i) - biPass(i Mod PL)) Mod 256
            Next
        End If
        IO.File.WriteAllBytes(OutputFile, ByteOut)
    End Sub
    Function B(ByVal S As Char, ByVal Inp As Byte, ByVal Crypt As Boolean) As Byte
        If Crypt Then
            Return IIf(Inp + Asc(S) > 255, Inp + Asc(S) - 256, Inp + Asc(S))
        Else
            Return IIf(Inp - Asc(S) < 0, 256 + Inp - Asc(S), Inp - Asc(S))
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SaveFileDialog1.ShowDialog()
        SaveFileDialog1.FileName = TextBox1.Text
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        TextBox2.Text = SaveFileDialog1.FileName
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        shifrator(TextBox3.Text, TextBox1.Text, TextBox2.Text, True)
        Kill(TextBox1.Text)
        MsgBox("Зашифровано!")
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        shifrator(TextBox3.Text, TextBox1.Text, TextBox2.Text, False)
        Kill(TextBox1.Text)
        MsgBox("Розшифровано!")
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
End Class
