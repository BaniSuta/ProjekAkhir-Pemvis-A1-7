Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class FormMenuUser
    Dim pageWidth As Integer = 827
    Dim pageHeight As Integer = 1169
    Dim currentPage, totalPage, marginPixels, y, x, marginRight As Integer
    Dim marginInch As Single
    Public Hberita, tanggal, penulis, isi As String


    Sub aturGrid()
        DataGridView1.Columns(0).Width = 60
        DataGridView1.Columns(1).Width = 218
        DataGridView1.Columns(2).Width = 218
        DataGridView1.Columns(3).Width = 218
        DataGridView1.Columns(0).HeaderText = "Kode Berita"
        DataGridView1.Columns(1).HeaderText = "Penulis Berita"
        DataGridView1.Columns(2).HeaderText = "Judul Berita"
        DataGridView1.Columns(3).HeaderText = "Jenis Berita"
    End Sub

    Sub tampilJenis()
        DA = New MySqlDataAdapter("SELECT kode_berita, akun.username, judul, jenis FROM berita JOIN akun ON berita.id_penulis = akun.id_user", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "berita")
        DataGridView1.DataSource = DS.Tables("berita")
        DataGridView1.Refresh()
    End Sub

    Private Sub FormMenuUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tampilJenis()
        aturGrid()
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        koneksi()
        If e.KeyChar = Chr(13) Then
            CMD = New MySqlCommand("SELECT kode_berita, akun.username, judul, jenis FROM berita JOIN akun ON berita.id_penulis = akun.id_user where username like '%" & txtSearch.Text & "%' OR jenis like '%" & txtSearch.Text & "%' OR judul like '%" & txtSearch.Text & "%' OR isi like '%" & txtSearch.Text & "%'", CONN)
            RD = CMD.ExecuteReader()
            RD.Read()

            If RD.HasRows Then
                RD.Close()
                DA = New MySqlDataAdapter("SELECT kode_berita, akun.username, judul, jenis FROM berita JOIN akun ON berita.id_penulis = akun.id_user where username like '%" & txtSearch.Text & "%' OR jenis like '%" & txtSearch.Text & "%' OR judul like '%" & txtSearch.Text & "%' OR isi like '%" & txtSearch.Text & "%'", CONN)
                DS = New DataSet
                DA.Fill(DS, "Dapat")
                DataGridView1.DataSource = DS.Tables("Dapat")
                DataGridView1.ReadOnly = True
            Else
                MessageBox.Show("Data Tidak Ditemukan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub ManajemenBeritaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManajemenBeritaToolStripMenuItem.Click
        Me.Close()
        FormLogin.Show()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
            If row.Index < DataGridView1.RowCount - 1 And row.Index >= 0 Then
                readBerita(row.Cells(0).Value)
                currentPage = 1
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()
            End If
        End If
    End Sub

    Private Sub readBerita(ByVal kode)
        koneksi()
        CMD = New MySqlCommand("SELECT judul, tgl_berita, akun.username, isi FROM berita JOIN akun ON berita.id_penulis = akun.id_user WHERE kode_berita = '" & kode & "'", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        Hberita = RD.GetString(0)
        Dim tgl As Date = Convert.ToDateTime(RD.GetString(1))
        tanggal = tgl.ToString("dd-MM-yyyy")
        penulis = RD.GetString(2)
        isi = RD.GetString(3)
        RD.Close()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim Fheader As New Font("Times New Roman", 24, FontStyle.Bold)
        'font header
        Dim FBodyB As New Font("Times New Roman", 14, FontStyle.Bold) 'font body
        Dim FBody As New Font("Times New Roman", 14, FontStyle.Regular)
        'font body
        Dim FDate As New Font("Times New Roman", 12, FontStyle.Italic)
        Dim black As SolidBrush = New SolidBrush(Color.Black) 'tipe dan warna teks
        Dim blue As SolidBrush = New SolidBrush(Color.SteelBlue)
        ' alignment
        Dim center As New StringFormat()
        Dim right As New StringFormat()
        right.Alignment = StringAlignment.Near
        center.Alignment = StringAlignment.Center
        Dim posY As Integer
        Dim rect As New RectangleF(0, 0, e.MarginBounds.Width, e.MarginBounds.Height)
        If currentPage <= 1 Then
            marginInch = 2.54F 'margin 1 inci/2.54 cm
            marginPixels = CInt(e.PageSettings.PrinterResolution.X *
            marginInch) 'convert margin ke pixel
            e.PageSettings.Margins = New Margins(marginPixels,
            marginPixels, marginPixels, marginPixels) 'inisiasi margin koordinat awal
            x = e.MarginBounds.Left
            y = e.MarginBounds.Top
            marginRight = e.MarginBounds.Right 'judul

            Dim Hwords As String() = Hberita.Split(" "c)
            Dim Hline As New System.Text.StringBuilder()

            Dim Hpoint As New PointF(pageWidth / 2, y)

            For Each word As String In Hwords
                If e.Graphics.MeasureString(Hline.ToString() & " " & word, Fheader).Width > e.MarginBounds.Width Then
                    e.Graphics.DrawString(Hline.ToString(), Fheader, blue, Hpoint, center)
                    Hline.Clear()
                    Hpoint.Y += Fheader.Height + 13
                End If
                Hline.Append(word & " ")
            Next

            If Hline.Length > 0 Then
                e.Graphics.DrawString(Hline.ToString(), Fheader, blue, Hpoint, center)
            End If
            posY = y + 70
        Else
            posY = y
        End If
        e.Graphics.DrawString(tanggal, FDate, black, x + 20, posY +
            30)
        e.Graphics.DrawString(penulis, FDate, black, x + 500, posY + 30, right)

        Dim words As String() = isi.Split(" "c)
        Dim line As New System.Text.StringBuilder()

        Dim point As New PointF(x, posY + 80)

        For Each word As String In words
            If e.Graphics.MeasureString(line.ToString() & " " & word, FBody).Width > e.MarginBounds.Width Then
                e.Graphics.DrawString(line.ToString(), FBody, black, point)
                line.Clear()
                point.Y += FBody.Height + 18
            End If
            line.Append(word & " ")
        Next

        If line.Length > 0 Then
            e.Graphics.DrawString(line.ToString(), FBody, black, point)
        End If

        currentPage += 1
        e.HasMorePages = currentPage <= totalPage
    End Sub
End Class