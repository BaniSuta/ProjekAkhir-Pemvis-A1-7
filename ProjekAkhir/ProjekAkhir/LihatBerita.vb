Imports MySql.Data.MySqlClient

Public Class LihatBerita
    Public kode As Integer

    Sub aturGrid()
        DataGridView1.Columns(0).Width = 60
        DataGridView1.Columns(1).Width = 200
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(3).Width = 200
        DataGridView1.Columns(4).Width = 200
        DataGridView1.Columns(5).Width = 200
        DataGridView1.Columns(0).HeaderText = "Kode Berita"
        DataGridView1.Columns(1).HeaderText = "Penulis Berita"
        DataGridView1.Columns(2).HeaderText = "Judul Berita"
        DataGridView1.Columns(3).HeaderText = "Jenis Berita"
        DataGridView1.Columns(4).HeaderText = "Tanggal Berita"
        DataGridView1.Columns(5).HeaderText = "Isi Berita"
    End Sub

    Sub tampilJenis()
        DA = New MySqlDataAdapter("SELECT kode_berita, akun.username, judul, jenis, tgl_berita, isi FROM berita JOIN akun ON berita.id_penulis = akun.id_user", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "berita")
        DataGridView1.DataSource = DS.Tables("berita")
        DataGridView1.Refresh()
    End Sub

    Private Sub LihatBerita_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tampilJenis()
        aturGrid()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If kode = Nothing Then
            MessageBox.Show("Harap Pilih Berita Yang Ingin Dihapus!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim response As Integer
            response = MessageBox.Show("Yakin Ingin Menghapus Berita Dengan Kode " & kode & " ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If response = vbYes Then
                Dim ubah As String = "delete from berita where kode_berita = '" & kode & "'"
                CMD = New MySqlCommand(ubah, CONN)
                CMD.ExecuteNonQuery()
                MessageBox.Show("Data Berhasil Dihapus!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                kode = Nothing
                tampilJenis()
            Else
                kode = Nothing
                MessageBox.Show("Yaudah Deh", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If kode <> Nothing Then
            Me.Hide()
            EditBerita.Show()
        Else
            MessageBox.Show("Harap Pilih Data Terlebih Dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
            If row.Index < DataGridView1.RowCount - 1 And row.Index >= 0 Then
                kode = row.Cells(0).Value
            End If
        End If
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        koneksi()
        If e.KeyChar = Chr(13) Then
            CMD = New MySqlCommand("SELECT kode_berita, akun.username, judul, jenis, tgl_berita, isi FROM berita JOIN akun ON berita.id_penulis = akun.id_user where username like '%" & txtSearch.Text & "%' OR jenis like '%" & txtSearch.Text & "%' OR judul like '%" & txtSearch.Text & "%' OR isi like '%" & txtSearch.Text & "%'", CONN)
            RD = CMD.ExecuteReader()
            RD.Read()

            If RD.HasRows Then
                RD.Close()
                DA = New MySqlDataAdapter("SELECT kode_berita, akun.username, judul, jenis, tgl_berita, isi FROM berita JOIN akun ON berita.id_penulis = akun.id_user where username like '%" & txtSearch.Text & "%' OR jenis like '%" & txtSearch.Text & "%' OR judul like '%" & txtSearch.Text & "%' OR isi like '%" & txtSearch.Text & "%'", CONN)
                DS = New DataSet
                DA.Fill(DS, "Dapat")
                DataGridView1.DataSource = DS.Tables("Dapat")
                DataGridView1.ReadOnly = True
            Else
                MessageBox.Show("Data Tidak Ditemukan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub btnkembali_Click(sender As Object, e As EventArgs) Handles btnkembali.Click
        Me.Close()
        FormMenuAdmin.Show()
    End Sub
End Class