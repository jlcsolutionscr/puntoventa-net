Imports System.Configuration
Imports LeandroSoftware.PuntoVenta.Core

Public Class FrmActivacion
    Private config As Configuration

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Try
            config.AppSettings.Settings("LoginUser").Value = Utilitario.EncriptarDatos(FrmMDIMenu.strThumbprint, txtUsuarioLogin.Text)
            config.AppSettings.Settings("LoginPassword").Value = Utilitario.EncriptarDatos(FrmMDIMenu.strThumbprint, txtClaveLogin.Text)
            config.AppSettings.Settings("BackupUser").Value = Utilitario.EncriptarDatos(FrmMDIMenu.strThumbprint, txtUsuarioRespaldo.Text)
            config.AppSettings.Settings("BackupPassword").Value = Utilitario.EncriptarDatos(FrmMDIMenu.strThumbprint, txtClaveRespaldo.Text)
            config.AppSettings.Settings("DatabaseHost").Value = txtHostRespaldo.Text
            config.AppSettings.Settings("DatabaseName").Value = txtNombreBaseDatos.Text
            config.AppSettings.Settings("MySQLDumpOptions").Value = txtParametrosRespaldo.Text
            config.AppSettings.Settings("BackupServer").Value = txtServidorRespaldo.Text
            config.AppSettings.Settings("Sucursal").Value = txtSucursal.Text
            config.AppSettings.Settings("Caja").Value = txtCaja.Text
            config.Save(ConfigurationSaveMode.Modified)
            MessageBox.Show("Configuración guardada satisfactoriamente. . .")
        Catch ex As Exception
            MessageBox.Show("Error al guardar el archivo de configuración: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click
        Dim openFileDialog1 As New OpenFileDialog With {
            .InitialDirectory = "c:\\",
            .Filter = "Config files | *.config",
            .FilterIndex = 2,
            .RestoreDirectory = True
        }
        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            txtRutaArchivoConfiguracion.Text = openFileDialog1.FileName
            If txtRutaArchivoConfiguracion.Text <> "" Then
                Dim configFileMap As ExeConfigurationFileMap = New ExeConfigurationFileMap With {
                    .ExeConfigFilename = txtRutaArchivoConfiguracion.Text
                }
                config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None)
                Try
                    txtUsuarioLogin.Text = Utilitario.DesencriptarDatos(FrmMDIMenu.strThumbprint, config.AppSettings.Settings("LoginUser").Value)
                    txtClaveLogin.Text = Utilitario.DesencriptarDatos(FrmMDIMenu.strThumbprint, config.AppSettings.Settings("LoginPassword").Value)
                    txtUsuarioRespaldo.Text = Utilitario.DesencriptarDatos(FrmMDIMenu.strThumbprint, config.AppSettings.Settings("BackupUser").Value)
                    txtClaveRespaldo.Text = Utilitario.DesencriptarDatos(FrmMDIMenu.strThumbprint, config.AppSettings.Settings("BackupPassword").Value)
                    txtHostRespaldo.Text = config.AppSettings.Settings("DatabaseHost").Value
                    txtNombreBaseDatos.Text = config.AppSettings.Settings("DatabaseName").Value
                    txtParametrosRespaldo.Text = config.AppSettings.Settings("MySQLDumpOptions").Value
                    txtServidorRespaldo.Text = config.AppSettings.Settings("BackupServer").Value
                    txtSucursal.Text = config.AppSettings.Settings("Sucursal").Value
                    txtCaja.Text = config.AppSettings.Settings("Caja").Value
                Catch ex As Exception
                    MessageBox.Show("Error al desencriptar valores del archivo de configuración seleccionado: " & ex.Message & " Se utilizarán los valores por defecto.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtUsuarioLogin.Text = "usuarioConn"
                    txtClaveLogin.Text = ""
                    txtUsuarioRespaldo.Text = "root"
                    txtClaveRespaldo.Text = ""
                    txtHostRespaldo.Text = "localhost"
                    txtNombreBaseDatos.Text = "puntoventa"
                    txtParametrosRespaldo.Text = "--complete-insert --no-create-info --default-character-set=utf8"
                    txtServidorRespaldo.Text = ""
                    txtSucursal.Text = "001"
                    txtCaja.Text = "00001"

                End Try
                MessageBox.Show("Archivo de configuración de la aplicación seleccionado correctamente.")
            Else
                MessageBox.Show("Debe seleccionar un archivo de configuración antes de continuar.")
            End If
        End If
    End Sub

    Private Sub BtnPAramPorDefecto_Click(sender As Object, e As EventArgs) Handles btnPAramPorDefecto.Click
        txtUsuarioLogin.Text = "usuarioConn"
        txtClaveLogin.Text = ""
        txtUsuarioRespaldo.Text = "root"
        txtClaveRespaldo.Text = ""
        txtHostRespaldo.Text = "localhost"
        txtNombreBaseDatos.Text = "puntoventa"
        txtParametrosRespaldo.Text = "--default-character-set=utf8"
        txtServidorRespaldo.Text = ""
        txtSucursal.Text = "001"
        txtCaja.Text = "00001"
    End Sub
End Class