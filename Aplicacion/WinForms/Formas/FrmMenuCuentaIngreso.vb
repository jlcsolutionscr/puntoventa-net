Public Class FrmMenuCuentaIngreso
#Region "Variables"
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        Try
            cboIdCuentaIngreso.ValueMember = "IdCuenta"
            cboIdCuentaIngreso.DisplayMember = "Descripcion"
            'cboIdCuentaIngreso.DataSource = servicioIngreso.ObtenerListaCuentasIngreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboIdCuentaIngreso.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarCombos()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdCuentaIngreso.SelectedValue IsNot Nothing Then
            FrmMenuPrincipal.intBusqueda = cboIdCuentaIngreso.SelectedValue
            Close()
        Else
            FrmMenuPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class