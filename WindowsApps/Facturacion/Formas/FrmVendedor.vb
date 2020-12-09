Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmVendedor
#Region "Variables"
    Private datos As Vendedor
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
    Private Sub FrmVendedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmUsuario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            If intIdVendedor > 0 Then
                datos = Await Puntoventa.ObtenerVendedor(intIdVendedor, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    Throw New Exception("El vendedor seleccionado no existe")
                End If
                txtIdVendedor.Text = datos.IdVendedor
                txtNombre.Text = datos.Nombre
            Else
                datos = New Vendedor
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
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
        If datos.IdVendedor = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Nombre = txtNombre.Text
        Try
            If datos.IdVendedor = 0 Then
                Await Puntoventa.AgregarVendedor(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarVendedor(datos, FrmPrincipal.usuarioGlobal.Token)
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