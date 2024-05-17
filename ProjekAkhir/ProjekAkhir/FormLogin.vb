Imports MySql.Data.MySqlClient

Public Class FormLogin
    Public id_pen As Integer

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        FormRegist.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        koneksi()
        CMD = New MySqlCommand("SELECT * FROM akun WHERE username = '" & txtusername.Text & "' AND password = '" & txtpassword.Text & "'", CONN)
        RD = CMD.ExecuteReader()

        If RD.HasRows = 0 Then
            MessageBox.Show("Username atau Password Anda Salah!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            While RD.Read()
                If RD.GetString(3) = True Then
                    MessageBox.Show("Selamat Datang " & RD.GetString(1) & "!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Hide()
                    id_pen = RD.GetString(0)
                    clean()
                    FormMenuAdmin.Show()
                Else
                    MessageBox.Show("Selamat Datang " & RD.GetString(1) & ", Selamat Membaca!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Hide()
                    clean()
                    FormMenuUser.Show()
                End If
            End While
        End If
    End Sub

    Sub clean()
        txtusername.Clear()
        txtpassword.Clear()
    End Sub

    Private Sub txtusername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtusername.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[a-z, A-Z]" _
OrElse keyascii = Keys.Back _
OrElse keyascii = Keys.Space _
OrElse keyascii = Keys.Return _
OrElse keyascii = Keys.Delete) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub txtpassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpassword.KeyPress
        txtpassword.MaxLength = 6
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtusername.Focus()
    End Sub
End Class
