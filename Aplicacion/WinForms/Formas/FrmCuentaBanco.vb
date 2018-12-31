Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmCuentaBanco
#Region "Variables"
    Public intIdCuenta As Integer
    Private datos As CuentaBanco
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        ElseIf txtCodigo.Text = "" Then
            pCampo = "Código"
            Return False
        ElseIf txtSaldo.Text = "" Then
            pCampo = "Saldo"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmCuentaBanco_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdCuenta > 0 Then
            Try
                datos = Await ClienteWCF.ObtenerCuentaBanco(intIdCuenta)
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
            txtIdCuenta.Text = datos.IdCuenta
            txtCodigo.Text = datos.Codigo
            txtDescripcion.Text = datos.Descripcion
            txtSaldo.Text = FormatNumber(datos.Saldo, 2)
        Else
            datos = New CuentaBanco
            txtSaldo.Text = FormatNumber(0, 2)
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
            datos.IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Codigo = txtCodigo.Text
        datos.Descripcion = txtDescripcion.Text
        datos.Saldo = txtSaldo.Text
        Try
            If datos.IdCuenta = 0 Then
                Dim strIdCuenta = Await ClienteWCF.AgregarCuentaBanco(datos)
                txtIdCuenta.Text = strIdCuenta
            Else
                Await ClienteWCF.ActualizarCuentaBanco(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub Saldo_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtSaldo.Validated
        If txtSaldo.Text = "" Then txtSaldo.Text = "0"
        txtSaldo.Text = FormatNumber(txtSaldo.Text, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSaldo.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class