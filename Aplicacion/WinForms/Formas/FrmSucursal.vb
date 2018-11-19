Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmSucursal
#Region "Variables"
    Public servicioTraslados As ITrasladoService
    Public intIdSucursal As Integer
    Private datos As Sucursal
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtNombre.Text = "" Then
            pCampo = "Nombre"
            Return False
        ElseIf txtDireccion.Text = "" Then
            pCampo = "Dirección"
            Return False
        ElseIf txtTelefono.Text = "" Then
            pCampo = "Teléfono"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmCuentaBanco_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdSucursal > 0 Then
            Try
                datos = servicioTraslados.ObtenerSucursal(intIdSucursal)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("La cuenta bancaria seleccionada no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdSucursal.Text = datos.IdSucursal
            txtNombre.Text = datos.Nombre
            txtDireccion.Text = datos.Direccion
            txtTelefono.Text = datos.Telefono
        Else
            datos = New Sucursal
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
        If datos.IdSucursal = 0 Then
            datos.IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Nombre = txtNombre.Text
        datos.Direccion = txtDireccion.Text
        datos.Telefono = txtTelefono.Text
        Try
            If datos.IdSucursal = 0 Then
                servicioTraslados.AgregarSucursal(datos)
                txtIdSucursal.Text = datos.IdSucursal
            Else
                servicioTraslados.ActualizarSucursal(datos)
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