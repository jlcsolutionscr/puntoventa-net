Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmLinea
#Region "Variables"
    Public intIdLinea As Integer
    Private datos As Linea
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmLinea_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmLinea_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            If intIdLinea > 0 Then
                datos = Await Puntoventa.ObtenerLinea(intIdLinea, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    MessageBox.Show("La Línea seleccionada no existe", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                    Exit Sub
                End If
                txtIdLinea.Text = datos.IdLinea
                txtDescripcion.Text = datos.Descripcion
            Else
                datos = New Linea
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            btnGuardar.Enabled = True
            Exit Sub
        End If
        If datos.IdLinea = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Descripcion = txtDescripcion.Text
        Try
            If datos.IdLinea = 0 Then
                Await Puntoventa.AgregarLinea(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarLinea(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub
#End Region
End Class