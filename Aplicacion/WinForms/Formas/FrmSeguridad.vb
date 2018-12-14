Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports LeandroSoftware.AccesoDatos.TiposDatos
Imports LeandroSoftware.Core
Imports Unity

Public Class FrmSeguridad
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
    Private usuario As Usuario
    Private empresa As Empresa
#End Region

#Region "Eventos Controles"
    Private Async Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Try
            CmdAceptar.Enabled = False
            Dim peticion As RequestDTO = New RequestDTO With {
                .NombreMetodo = "ValidarCredenciales",
                .DatosPeticion = "{Identificacion: '" + txtIdentificacion.Text + "', Usuario: '" + TxtUsuario.Text + "', Clave: '" + TxtClave.Text + "'}"
            }
            Dim strPeticion As String = New JavaScriptSerializer().Serialize(peticion)
            Dim strRespuesta As String = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
            strRespuesta = New JavaScriptSerializer().Deserialize(Of String)(strRespuesta)
            If strRespuesta <> "" Then
                usuario = New JavaScriptSerializer().Deserialize(Of Usuario)(strRespuesta)
            End If
            peticion = New RequestDTO With {
                .NombreMetodo = "ObtenerEmpresa",
                .DatosPeticion = "{IdEmpresa: " + usuario.IdEmpresa.ToString() + "}"
            }
            strPeticion = New JavaScriptSerializer().Serialize(peticion)
            strRespuesta = Await Utilitario.EjecutarConsulta(strPeticion, FrmMenuPrincipal.strServicioPuntoventaURL, "")
            strRespuesta = New JavaScriptSerializer().Deserialize(Of String)(strRespuesta)
            If strRespuesta <> "" Then
                empresa = New JavaScriptSerializer().Deserialize(Of Empresa)(strRespuesta)
            End If
        Catch ex As Exception
            CmdAceptar.Enabled = True
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        FrmMenuPrincipal.usuarioGlobal = usuario
        FrmMenuPrincipal.empresaGlobal = empresa
        Close()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Close()
    End Sub
#End Region
End Class