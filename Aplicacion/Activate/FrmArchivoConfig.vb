Imports System.Configuration
Imports LeandroSoftware.Core

Public Class FrmArchivoConfig
    Private config As Configuration

    Private Sub FrmArchivoConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath)
        txtHost.Text = config.AppSettings.Settings("Host").Value
        txtDatabase.Text = config.AppSettings.Settings("Database").Value
        txtUser.Text = Utilitario.DesencriptarDatos(FrmMDIMenu.strThumbprint, config.AppSettings.Settings("User").Value)
        txtPassword.Text = Utilitario.DesencriptarDatos(FrmMDIMenu.strThumbprint, config.AppSettings.Settings("Password").Value)
        txtSqlDumpParams.Text = config.AppSettings.Settings("MySQLDumpOptions").Value
        txtSubjectName.Text = config.AppSettings.Settings("SubjectName").Value
    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Try
            config.AppSettings.Settings("Host").Value = txtHost.Text
            config.AppSettings.Settings("Database").Value = txtDatabase.Text
            config.AppSettings.Settings("User").Value = Utilitario.EncriptarDatos(FrmMDIMenu.strThumbprint, txtUser.Text)
            config.AppSettings.Settings("Password").Value = Utilitario.EncriptarDatos(FrmMDIMenu.strThumbprint, txtPassword.Text)
            config.AppSettings.Settings("MySQLDumpOptions").Value = txtSqlDumpParams.Text
            config.AppSettings.Settings("SubjectName").Value = txtSubjectName.Text
            config.Save(ConfigurationSaveMode.Modified)
            MessageBox.Show("Configuración guardada satisfactoriamente. . .")
        Catch ex As Exception
            MessageBox.Show("Error al guardar el archivo de configuración")
        End Try
    End Sub

    Private Sub BtnParamPorDefecto_Click(sender As Object, e As EventArgs) Handles btnParamPorDefecto.Click
        txtUser.Text = "activator"
        txtPassword.Text = ""
        txtDatabase.Text = "puntoventa"
        txtSqlDumpParams.Text = "--default-character-set=utf8"
        txtSubjectName.Text = "leandro.software.net"
    End Sub
End Class