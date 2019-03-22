Imports LeandroSoftware.AccesoDatos.ClienteWCF

Public Class FrmMenuBancoAdquiriente
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        Try
            cboIdBancoAdquiriente.ValueMember = "IdBanco"
            cboIdBancoAdquiriente.DisplayMember = "Descripcion"
            cboIdBancoAdquiriente.DataSource = Await PuntoventaWCF.ObtenerListaBancoAdquiriente(FrmPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboIdBancoAdquiriente.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarCombos()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdBancoAdquiriente.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdBancoAdquiriente.SelectedValue
            Close()
        Else
            MessageBox.Show("Debe seleccionar una cuenta bancaria para continuar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
End Class