Imports MySql.Data.MySqlClient

Public Class FormRegist

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Label6.Visible = False
        txtkodekonfirmasi.Visible = False
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Label6.Visible = True
        txtkodekonfirmasi.Visible = True
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Close()
        clean()
        FormLogin.Show()
    End Sub

    Private Sub btnregister_Click(sender As Object, e As EventArgs) Handles btnregister.Click
        koneksi()
        Dim response As Integer
        response = MessageBox.Show("Apakah Data Anda Sudah Benar dan Terisi Lengkap?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = vbYes Then

            Dim penulis As String
            For Each ctrl As Control In GroupBox1.Controls
                If TypeOf ctrl Is RadioButton Then
                    If (DirectCast(ctrl, RadioButton).Checked) Then
                        penulis = DirectCast(ctrl, RadioButton).Text
                    End If
                End If
            Next
            If txtusername.Text <> Nothing And txtpassword.Text <> Nothing And txtkpassword.Text <> Nothing And penulis <> Nothing Then

                If cekUsername(txtusername.Text) = False Then

                    koneksi()

                    If txtpassword.TextLength = 6 And txtkpassword.TextLength = 6 Then

                        If txtpassword.Text = txtkpassword.Text Then

                            If penulis = "Iya" Then

                                If txtkodekonfirmasi.Text = "1112" Then
                                    Dim username, password As String
                                    Dim pen As Integer

                                    username = txtusername.Text
                                    password = txtpassword.Text
                                    pen = 1

                                    CMD = New MySqlCommand("INSERT INTO akun VALUES (NULL,'" & username & "','" & password & "','" & pen & "')", CONN)
                                    Dim rowAffected As Integer = CMD.ExecuteNonQuery()
                                    If rowAffected > 0 Then
                                        MessageBox.Show("Berhasil Registrasi!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Close()
                                        clean()
                                        FormLogin.Show()
                                    Else
                                        MessageBox.Show("Gagal Registrasi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    End If
                                ElseIf txtkodekonfirmasi.Text <> Nothing And txtkodekonfirmasi.Text <> "1112" Then
                                    MessageBox.Show("Kode Konfirmasi Salah!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Else
                                    MessageBox.Show("Harap Isi Kode Konfirmasi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

                            Else

                                Dim username, password As String
                                Dim pen As Integer

                                username = txtusername.Text
                                password = txtpassword.Text
                                pen = 0

                                CMD = New MySqlCommand("INSERT INTO akun VALUES (NULL,'" & username & "','" & password & "','" & pen & "')", CONN)
                                Dim rowAffected As Integer = CMD.ExecuteNonQuery()
                                If rowAffected > 0 Then
                                    MessageBox.Show("Berhasil Registrasi!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Me.Close()
                                    clean()
                                    FormLogin.Show()
                                Else
                                    MessageBox.Show("Gagal Registrasi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

                            End If

                        Else
                            MessageBox.Show("Konfirmasi Password dan Password Tidak Sama!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    Else
                        MessageBox.Show("Harap Masukkan Password Dengan 6 Karakter!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                Else
                    MessageBox.Show("Harap Ganti Username Karena Username Sudah Terdaftar!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                End If
            Else
                MessageBox.Show("Harap Isi Semua Data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Else
            MessageBox.Show("Harap Periksa Kembali Data Anda Dengan Benar!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Sub clean()
        txtusername.Clear()
        txtpassword.Clear()
        txtkodekonfirmasi.Clear()
        txtkpassword.Clear()
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is RadioButton Then
                DirectCast(ctrl, RadioButton).Checked = False
            End If
        Next
    End Sub

    Function cekUsername(ByRef usn)
        CMD = New MySqlCommand("SELECT * FROM akun WHERE username = '" & usn & "'", CONN)
        RD = CMD.ExecuteReader()

        If RD.HasRows = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

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

    Private Sub txtkpassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkpassword.KeyPress
        txtkpassword.MaxLength = 6
    End Sub

    Private Sub FormRegist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtusername.Focus()
    End Sub
End Class
