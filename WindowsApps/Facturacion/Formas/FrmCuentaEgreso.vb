Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

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
    Private Sub FrmCuentaEgreso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctl As Control In Controls
            If TypeOf (ctl) Is TextBox Then
                AddHandler DirectCast(ctl, TextBox).Enter, AddressOf EnterTexboxHandler
                AddHandler DirectCast(ctl, TextBox).Leave, AddressOf LeaveTexboxHandler
            End If
        Next
    End Sub

    Private Sub EnterTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.PeachPuff
    End Sub

    Private Sub LeaveTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.White
    End Sub

    Private Async Sub FrmCuentaEgreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdCuenta > 0 Then
            Try
                datos = Await Puntoventa.ObtenerCuentaEgreso(intIdCuenta, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    MessageBox.Show("La cuenta de egreso seleccionada no existe", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                    Exit Sub
                End If
                txtIdCuenta.Text = datos.IdCuenta
                txtDescripcion.Text = datos.Descripcion
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            End Try
        Else
            datos = New CuentaEgreso
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            btnGuardar.Enabled = True
            Exit Sub
        End If
        If datos.IdCuenta = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Descripcion = txtDescripcion.Text
        Try
            If datos.IdCuenta = 0 Then
                Await Puntoventa.AgregarCuentaEgreso(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarCuentaEgreso(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub
#End Region
End Class