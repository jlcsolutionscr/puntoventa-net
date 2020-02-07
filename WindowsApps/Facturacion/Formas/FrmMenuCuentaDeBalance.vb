Public Class FrmMenuCuentaDeBalance
#Region "Variables"
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        cboIdCuentaBanco.ValueMember = "IdCuenta"
        cboIdCuentaBanco.DisplayMember = "Descripcion"
        'cboIdCuentaBanco.DataSource = servicioContabilidad.ObtenerListaCuentasDeBalance(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboIdCuentaBanco.SelectedValue = 0
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
        If cboIdCuentaBanco.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdCuentaBanco.SelectedValue
            Close()
        Else
            MessageBox.Show("Debe seleccionar una cuenta contable para continuar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
End Class