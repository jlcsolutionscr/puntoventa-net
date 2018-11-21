Imports System.IO
Imports LeandroSoftware.Core

Public Class FrmDesencriptar
    Private ProcID As Integer
    Private strCadena, strOutputFilaname As String
    Private process As Process

    Private Sub btnDesencriptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDesencriptar.Click
        If MessageBox.Show("Desea realizar la desencriptaci�n del archivo de respaldo", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            If txtArchivo.Text = "" Then
                MessageBox.Show("No se encontr� el archivo de aplicaci�n mysql.exe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Try
                strOutputFilaname = txtArchivo.Text.Substring(0, txtArchivo.Text.LastIndexOf(".")) + "Decrypted.sql"
                Utilitario.DesencriptarArchivo(FrmMDIMenu.strThumbprint, FrmMDIMenu.strApplicationKey, txtArchivo.Text, strOutputFilaname)
            Catch ex As Exception
                MessageBox.Show("Error al desencriptar: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End Try
            MessageBox.Show("Proceso de desencriptado finalizado con �xito", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnFileLoader_Click(sender As Object, e As EventArgs) Handles btnFileLoader.Click
        ofdAbrirArchivo.ShowDialog()
        txtArchivo.Text = ofdAbrirArchivo.FileName
    End Sub
End Class