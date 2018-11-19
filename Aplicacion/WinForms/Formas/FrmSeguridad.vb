Imports System.Collections.Generic
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmSeguridad
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
    Private usuario As Usuario
    Private empresa As Empresa
#End Region

#Region "Eventos Controles"
    Private Sub FrmSeguridad_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
            cboEmpresa.DataSource = servicioMantenimiento.ObtenerListaEmpresas()
            cboEmpresa.ValueMember = "IdEmpresa"
            cboEmpresa.DisplayMember = "NombreEmpresa"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        If cboEmpresa.SelectedIndex < 0 Then
            MessageBox.Show("Debe seleccionar una empresa para validar los datos", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Try
            usuario = servicioMantenimiento.ValidarUsuario(cboEmpresa.SelectedValue, TxtUsuario.Text, TxtClave.Text, FrmMenuPrincipal.strAppThumptPrint)
            empresa = servicioMantenimiento.ObtenerEmpresa(cboEmpresa.SelectedValue)
        Catch ex As Exception
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