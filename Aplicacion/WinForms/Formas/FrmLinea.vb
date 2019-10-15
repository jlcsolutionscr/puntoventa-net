Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmLinea
#Region "Variables"
    Public intIdLinea As Integer
    Private datos As Linea
    Private bolInit As Boolean = True
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If cboTipoProducto.SelectedValue Is Nothing Then
            pCampo = "Tipo de Línea"
            Return False
        ElseIf txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        Else
            Return True
        End If
    End Function

    Private Async Sub CargarTipoLinea()
        Try
            cboTipoProducto.ValueMember = "Id"
            cboTipoProducto.DisplayMember = "Descripcion"
            cboTipoProducto.DataSource = Await Puntoventa.ObtenerListadoTipoProducto(FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmLinea_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            CargarTipoLinea()
            If intIdLinea > 0 Then
                datos = Await Puntoventa.ObtenerLinea(intIdLinea, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    MessageBox.Show("La Línea seleccionada no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                    Exit Sub
                End If
                txtIdLinea.Text = datos.IdLinea
                cboTipoProducto.SelectedValue = datos.IdTipoProducto
                txtDescripcion.Text = datos.Descripcion
            Else
                datos = New Linea
            End If
            bolInit = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
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
        If datos.IdLinea = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.IdTipoProducto = cboTipoProducto.SelectedValue
        datos.Descripcion = txtDescripcion.Text
        Try
            If datos.IdLinea = 0 Then
                Await Puntoventa.AgregarLinea(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarLinea(datos, FrmPrincipal.usuarioGlobal.Token)
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