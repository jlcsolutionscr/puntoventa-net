
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.Core.ClienteWCF
Imports LeandroSoftware.Core.Utilities

Public Class FrmSeguridad
#Region "Variables"
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmSeguridad_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If FrmPrincipal.strIdentificacion = "" Then
            MessageBox.Show("La identificación de la empresa no se encuentra parametrizada en el sistema. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End If
        Try
            cboEmpresa.DataSource = Await ClienteFEWCF.ObtenerListaEmpresasPorIdentificacion(FrmPrincipal.strIdentificacion)
            cboEmpresa.ValueMember = "Identificacion"
            cboEmpresa.DisplayMember = "NombreComercial"
            CmdAceptar.Enabled = True
            CmdCancelar.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try

    End Sub
    Private Async Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Dim usuario As Usuario = Nothing
        Dim strEncryptedPassword As String
        Try
            CmdAceptar.Enabled = False
            CmdCancelar.Enabled = False
            strEncryptedPassword = Utilitario.EncriptarDatos(TxtClave.Text, FrmPrincipal.strKey)
            usuario = Await ClienteFEWCF.ValidarCredenciales(cboEmpresa.SelectedValue.ToString(), TxtUsuario.Text, strEncryptedPassword)
        Catch ex As Exception
            CmdAceptar.Enabled = True
            CmdCancelar.Enabled = True
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        FrmPrincipal.usuarioGlobal = usuario
        FrmPrincipal.empresaGlobal = usuario.Empresa
        Close()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Close()
    End Sub

    Private Sub TxtUsuario_TextChanged(sender As Object, e As EventArgs) Handles TxtUsuario.TextChanged
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub

    Private Sub TxtClave_TextChanged(sender As Object, e As EventArgs) Handles TxtClave.TextChanged
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub

    Private Sub txtIdentificacion_TextChanged(sender As Object, e As EventArgs)
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub
#End Region
End Class