Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity

Public Class FrmTipoMoneda
#Region "Variables"
    Public servicioMantenimiento As IMantenimientoService
    Public intIdTipoMoneda As Integer
    Private datos As TipoMoneda
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        ElseIf txtTipoCambioCompra.Text = "" Then
            pCampo = "Tipo de Cambio"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmTipoMoneda_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdTipoMoneda > 0 Then
            Try
                datos = servicioMantenimiento.ObtenerTipoMoneda(intIdTipoMoneda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El parámetro contable seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdTipoMoneda.Text = datos.IdTipoMoneda
            txtDescripcion.Text = datos.Descripcion
            txtTipoCambioCompra.Text = datos.TipoCambioCompra
            txtTipoCambioVenta.Text = datos.TipoCambioVenta
        Else
            datos = New TipoMoneda
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
        datos.Descripcion = txtDescripcion.Text
        datos.TipoCambioCompra = txtTipoCambioCompra.Text
        datos.TipoCambioVenta = txtTipoCambioVenta.Text
        Try
            If datos.IdTipoMoneda = 0 Then
                datos = servicioMantenimiento.AgregarTipoMoneda(datos)
                txtIdTipoMoneda.Text = datos.IdTipoMoneda
            Else
                servicioMantenimiento.ActualizarTipoMoneda(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub txtTipoCambioCompra_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtTipoCambioCompra.Validated
        txtTipoCambioCompra.Text = FormatNumber(IIf(txtTipoCambioCompra.Text <> "", txtTipoCambioCompra.Text, 1), 2)
    End Sub

    Private Sub txtTipoCambioVenta_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtTipoCambioVenta.Validated
        txtTipoCambioVenta.Text = FormatNumber(IIf(txtTipoCambioVenta.Text <> "", txtTipoCambioVenta.Text, 1), 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTipoCambioCompra.KeyPress, txtTipoCambioVenta.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class