Imports MySql.Data.MySqlClient

Public Class AddBerita
    Dim jenis As String


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
        FormMenuAdmin.Show()
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        cekRadio()
        If jenis <> "" And txtjudul.Text <> "" And txtisi.Text <> "" Then
            CMD = New MySqlCommand("INSERT INTO berita VALUES (NULL," & FormLogin.id_pen & ",'" & txtjudul.Text & "','" & jenis & "','" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "','" & txtisi.Text & "')", CONN)
            Dim rowAffected As Integer = CMD.ExecuteNonQuery()
            If rowAffected > 0 Then
                MessageBox.Show("Berhasil Menambahkan Berita!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                clean()
                Me.Close()
                FormMenuAdmin.Show()
            Else
                MessageBox.Show("Gagal Menambahkan Berita!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Harap Inputkan Data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub cekRadio()
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is RadioButton Then
                If (DirectCast(ctrl, RadioButton).Checked) Then
                    jenis = DirectCast(ctrl, RadioButton).Text
                End If
            End If
        Next
    End Sub

    Sub clean()
        txtjudul.Clear()
        txtisi.Clear()
        DateTimePicker1.Value = Now
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is RadioButton Then
                DirectCast(ctrl, RadioButton).Checked = False
            End If
        Next
    End Sub

    Private Sub AddBerita_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        DateTimePicker1.MaxDate = Now
        txtjudul.Focus()
    End Sub
End Class