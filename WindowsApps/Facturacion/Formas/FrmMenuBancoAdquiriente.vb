Imports LeandroSoftware.ClienteWCF

Public Class FrmMenuBancoAdquiriente
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        cboIdBancoAdquiriente.ValueMember = "IdBanco"
        cboIdBancoAdquiriente.DisplayMember = "Descripcion"
        cboIdBancoAdquiriente.DataSource = Await Puntoventa.ObtenerListadoBancoAdquiriente(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        cboIdBancoAdquiriente.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            CargarCombos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdBancoAdquiriente.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdBancoAdquiriente.SelectedValue
            Close()
        Else
            MessageBox.Show("Debe seleccionar una cuenta bancaria para continuar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
End Class