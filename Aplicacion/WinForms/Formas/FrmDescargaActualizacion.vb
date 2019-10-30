Imports System.ComponentModel
Imports System.Configuration
Imports System.IO
Imports System.Net

Public Class FrmDescargaActualizacion
#Region "Variables"
    Private WithEvents downloader As WebClient
    Private appSettings As Specialized.NameValueCollection
    Private strServicioURL As String
    Private strMSIFilePath As String = Path.GetTempPath() + "puntoventaJLC.msi"
#End Region

#Region "Métodos"
    Private Sub ShowDownloadProgress(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles downloader.DownloadProgressChanged
        pgbProgresoDescarga.Value = e.ProgressPercentage
    End Sub

    Private Sub OnDownloadComplete(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs) Handles downloader.DownloadFileCompleted
        Dim procStartInfo As New ProcessStartInfo
        Try
            procStartInfo.Arguments = "/i " + strMSIFilePath
            procStartInfo.FileName = "msiexec"
            Process.Start(procStartInfo)
        Catch ex As Exception
            MessageBox.Show("Error al ejecutar la actualización. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Application.Exit()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmDescargaActualizacion_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            appSettings = ConfigurationManager.AppSettings
            strServicioURL = appSettings.Get("ServicioURL")
        Catch ex As Exception
            MessageBox.Show("Error al cargar el archivo de configuración del sistema. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
        downloader = New WebClient
        File.Delete(strMSIFilePath)
        downloader.DownloadFileAsync(New Uri(strServicioURL + "/descargaractualizacion"), strMSIFilePath)
    End Sub
#End Region
End Class