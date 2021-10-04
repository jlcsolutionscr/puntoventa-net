Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmPuntoDeServicio
#Region "Variables"
    Public intIdPunto As Integer
    Private datos As PuntoDeServicio
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
    Private Sub FrmPuntoDeServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmPuntoDeServicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdPunto > 0 Then
            Try
                datos = Await Puntoventa.ObtenerPuntoDeServicio(intIdPunto, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El banco adquiriente seleccionado no existe", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdPunto.Text = datos.IdPunto
            txtDescripcion.Text = datos.Descripcion
            chkActivo.Checked = datos.Activo
        Else
            datos = New PuntoDeServicio
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
        If datos.IdPunto = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
            datos.IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal
        End If
        datos.Descripcion = txtDescripcion.Text
        datos.Activo = chkActivo.Checked
        Try
            If datos.IdPunto = 0 Then
                Await Puntoventa.AgregarPuntoDeServicio(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarPuntoDeServicio(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
            MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Close()
    End Sub
#End Region
End Class