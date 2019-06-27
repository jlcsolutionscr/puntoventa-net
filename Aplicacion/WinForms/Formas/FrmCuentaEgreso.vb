Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.ClienteWCF

Public Class FrmCuentaEgreso
#Region "Variables"
    Public intIdCuenta As Integer
    Private datos As CuentaEgreso
#End Region

#Region "M�todos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtDescripcion.Text = "" Then
            pCampo = "Descripci�n"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmCuentaEgreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdCuenta > 0 Then
            Try
                datos = Await PuntoventaWCF.ObtenerCuentaEgreso(intIdCuenta)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("La cuenta de egreso seleccionada no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdCuenta.Text = datos.IdCuenta
            txtDescripcion.Text = datos.Descripcion
        Else
            datos = New CuentaEgreso
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If datos.IdCuenta = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Descripcion = txtDescripcion.Text
        Try
            If datos.IdCuenta = 0 Then
                Dim strIdCuentaEgreso As String = Await PuntoventaWCF.AgregarCuentaEgreso(datos)
                txtIdCuenta.Text = strIdCuentaEgreso
            Else
                Await PuntoventaWCF.ActualizarCuentaEgreso(datos)
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