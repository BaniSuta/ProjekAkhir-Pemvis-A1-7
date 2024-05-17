Imports MySql.Data.MySqlClient

Public Class EditBerita
    Dim jenis As String

    Private Sub EditBerita_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        DateTimePicker1.MaxDate = Now
        ' Set up the SQL query
        Dim queryString As String = "SELECT judul, jenis, tgl_berita, isi FROM berita WHERE kode_berita = '" & LihatBerita.kode & "'"

        ' Create a new SQL connection' Create a new SQL command
        Dim command As New MySqlCommand(queryString, CONN)

        ' Execute the SQL command and read the result
        Dim reader As MySqlDataReader = command.ExecuteReader()
        reader.Read()

        ' Display the result in the TextBox
        txtjudul.Text = reader.GetString(0)

        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is RadioButton Then
                If (DirectCast(ctrl, RadioButton).Text = reader.GetString(1)) Then
                    DirectCast(ctrl, RadioButton).Checked = True
                End If
            End If
        Next

        DateTimePicker1.Value = Convert.ToDateTime(reader.GetString(2))

        txtisi.Text = reader.GetString(3)



        ' Close the reader
        reader.Close()

        txtjudul.Focus()
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        cekRadio()
        If jenis <> "" And txtjudul.Text <> "" And txtisi.Text <> "" Then
            CMD = New MySqlCommand("UPDATE berita SET jenis = '" & jenis & "',judul = '" & txtjudul.Text & "',tgl_berita = '" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "', isi = '" & txtisi.Text & "' WHERE kode_berita = " & LihatBerita.kode, CONN)
            Dim rowAffected As Integer = CMD.ExecuteNonQuery()
            If rowAffected > 0 Then
                MessageBox.Show("Berhasil Mengubah Berita!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                LihatBerita.kode = Nothing
                LihatBerita.tampilJenis()
                LihatBerita.Show()
            Else
                MessageBox.Show("Gagal Mengubah Berita!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
        LihatBerita.kode = Nothing
        LihatBerita.Show()
    End Sub
End Class