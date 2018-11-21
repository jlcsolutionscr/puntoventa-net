Imports LeandroSoftware.Core

Public Class FrmMDIMenu
    Public strCadenaConexion, strMySQLDumpOptions, strThumbprint, strSubjectName, strApplicationKey, strDatabase, strUser, strPassword, strHost As String
    Public bolSeguridad As Boolean = True
    Private bolArchivoConfig As Boolean = True

    Private Sub TsRegistrarEquipo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsRegistrarEquipo.Click
        Dim formaEmpresa As New FrmEmpresaListado() With {
            .MdiParent = Me
        }
        formaEmpresa.Show()
    End Sub

    Private Sub TsActualizaConfig_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsActualizaConfig.Click
        Dim formaArchivoConfig As New FrmArchivoConfig With {
            .MdiParent = Me
        }
        formaArchivoConfig.Show()
    End Sub

    Private Sub TsRespaldo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsRespaldo.Click
        Dim formaRespaldo As New FrmRespaldo With {
            .MdiParent = Me
        }
        formaRespaldo.Show()
    End Sub

    Private Sub TsRestaura_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsRestaura.Click
        Dim formaRestaura As New FrmDesencriptar With {
            .MdiParent = Me
        }
        formaRestaura.Show()
    End Sub

    Private Sub TsSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsSalir.Click
        Me.Close()
    End Sub

    Private Sub TsActivacion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsActivacion.Click
        Dim formActivacion As New FrmActivacion With {
            .MdiParent = Me
        }
        formActivacion.Show()
    End Sub

    Private Sub FrmMDIMenu_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim appSettings As Specialized.NameValueCollection = Nothing
        Try
            appSettings = Configuration.ConfigurationManager.AppSettings
        Catch ex As Exception
            MessageBox.Show("Error al cargar el archivo de configuración del sistema de activación: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Exit Sub
        End Try
        Try
            strThumbprint = Utilitario.VerificarCertificadoPorNombre(appSettings.Get("SubjectName"))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Exit Sub
        End Try
        Try
            strHost = appSettings.Get("Host")
            strDatabase = appSettings.Get("Database")
            Try
                strUser = Utilitario.DesencriptarDatos(strThumbprint, appSettings.Get("User"))
                strPassword = Utilitario.DesencriptarDatos(strThumbprint, appSettings.Get("Password"))
            Catch ex As Exception
                bolSeguridad = False
                MessageBox.Show("Error al tratar de desencriptar valores del archivo de configuración del sistema de activación: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
            strCadenaConexion = "Database = " & strDatabase & ";User Id = " & strUser & ";Password = " & strPassword & "; data Source = " & strHost & "; SslMode = none"
            strMySQLDumpOptions = appSettings.Get("MySQLDumpOptions")
            strSubjectName = appSettings.Get("SubjectName")
            strApplicationKey = appSettings.Get("ApplicationKey")
        Catch ex As Exception
            bolArchivoConfig = False
            MessageBox.Show("Error al obtener los valores de configuración de la activación: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        If bolArchivoConfig Then
            tsActualizaConfig.Visible = True
        End If

        If bolSeguridad Then
            bolSeguridad = False
            Dim formSeguridad As New FrmSeguridad()
            FrmSeguridad.ShowDialog()
            If bolSeguridad Then
                tsActivacion.Visible = True
                tsRegistrarEquipo.Visible = True
                tsRespaldo.Visible = True
                tsRestaura.Visible = True
            End If
        End If
    End Sub
End Class
