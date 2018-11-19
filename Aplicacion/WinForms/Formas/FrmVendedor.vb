Imports System.Collections.Generic
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmVendedor
#Region "Variables"
    Private I As Short
    Private datos As Vendedor
    Public servicioMantenimiento As IMantenimientoService
    Public intIdVendedor As Integer
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtNombre.Text = "" Then
            pCampo = "Usuario"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmUsuario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdVendedor > 0 Then
            Try
                datos = servicioMantenimiento.ObtenerVendedor(intIdVendedor)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El vendedor seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdVendedor.Text = datos.IdVendedor
            txtNombre.Text = datos.Nombre
        Else
            datos = New Vendedor
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If datos.IdVendedor = 0 Then
            datos.IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Nombre = txtNombre.Text
        Try
            If datos.IdVendedor = 0 Then
                datos = servicioMantenimiento.AgregarVendedor(datos)
                txtIdVendedor.Text = datos.IdVendedor
            Else
                servicioMantenimiento.ActualizarVendedor(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub
#End Region
End Class