Public Class FormMenuAdmin

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Me.Close()
        FormLogin.id_pen = Nothing
        FormLogin.Show()
    End Sub

    Private Sub TambahBeritaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TambahBeritaToolStripMenuItem.Click
        Me.Close()
        AddBerita.Show()
    End Sub

    Private Sub LihatBeritaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LihatBeritaToolStripMenuItem.Click
        Me.Close()
        LihatBerita.Show()
    End Sub
End Class