﻿Imports MySql.Data.MySqlClient
Module Module1
    Public CONN As MySqlConnection
    Public CMD As MySqlCommand
    Public RD As MySqlDataReader
    Public DA As MySqlDataAdapter
    Public DS As DataSet
    Public STR As String

    Sub koneksi()
        Try
            Dim STR As String =
            "server=localhost;userid=root;password=;database=db_ashariinfo"
            'Ganti nama database sesuaikan dengan nama database kalian
            CONN = New MySqlConnection(STR)
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module