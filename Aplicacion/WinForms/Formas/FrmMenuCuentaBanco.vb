Imports LeandroSoftware.Core.ClienteWCF

Public Class FrmMenuCuentaBanco
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        Try
            cboIdCuentaBanco.ValueMember = "IdCuenta"
            cboIdCuentaBanco.DisplayMember = "Descripcion"
            cboIdCuentaBanco.DataSource = Await ClienteFEWCF.ObtenerListaCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        cboIdCuentaBanco.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarCombos()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdCuentaBanco.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdCuentaBanco.SelectedValue
            Close()
        Else
            MessageBox.Show("Debe seleccionar una cuenta bancaria para continuar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
End Class