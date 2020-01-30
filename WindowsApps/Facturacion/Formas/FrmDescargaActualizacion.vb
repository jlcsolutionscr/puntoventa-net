Imports System.ComponentModel
Imports System.Configuration
Imports System.IO
Imports System.Net

Public Class FrmDescargaActualizacion
#Region "Variables"
    Private WithEvents DownloaderClient As WebClient
    Private appSettings As Specialized.NameValueCollection
    Private strServicioURL As String
    Private bolCancelaDescarga As Boolean = False
    Private bolDescargaCompleta As Boolean = False
    Private ReadOnly strMSIFilePath As String = Path.GetTempPath() + "puntoventaJLC.msi"
#End Region

#Region "Métodos"
    Private Sub ShowDownloadProgress(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles DownloaderClient.DownloadProgressChanged
        pgbProgresoDescarga.Value = e.ProgressPercentage
    End Sub

    Private Sub OnDownloadComplete(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs) Handles DownloaderClient.DownloadFileCompleted
        Dim procStartInfo As New ProcessStartInfo
        If bolCancelaDescarga = False Then
            Try
                bolDescargaCompleta = True
                FrmPrincipal.bolDescargaFinalizada = True
                procStartInfo.Arguments = "/i " + strMSIFilePath
                procStartInfo.FileName = "msiexec"
                Process.Start(procStartInfo)
                Close()
            Catch ex As Exception
                MessageBox.Show("Error al ejecutar la actualización. Por favor contacte con su proveedor del servicio. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmDescargaActualizacion_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            appSettings = ConfigurationManager.AppSettings
            strServicioURL = appSettings.Get("ServicioURL")
        Catch ex As Exception
            MessageBox.Show("Error al cargar el archivo de configuración del sistema. Por favor contacte con su proveedor del servicio. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
        DownloaderClient = New WebClient
        File.Delete(strMSIFilePath)
        DownloaderClient.DownloadFileAsync(New Uri(strServicioURL + "/descargaractualizacion"), strMSIFilePath)
    End Sub

    Private Sub FrmDescargaActualizacion_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If bolDescargaCompleta = False Then
            If MessageBox.Show("Esta seguro que desea cancelar la actualización?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                bolCancelaDescarga = True
                FrmPrincipal.bolDescargaFinalizada = True
                DownloaderClient.CancelAsync()
            Else
                e.Cancel = True
            End If
        End If
    End Sub
#End Region
End Class