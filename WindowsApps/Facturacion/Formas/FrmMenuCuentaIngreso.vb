Public Class FrmMenuCuentaIngreso
#Region "Variables"
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        cboIdCuentaIngreso.ValueMember = "IdCuenta"
        cboIdCuentaIngreso.DisplayMember = "Descripcion"
        'cboIdCuentaIngreso.DataSource = servicioIngreso.ObtenerListaCuentasIngreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboIdCuentaIngreso.SelectedValue = 0
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
        If cboIdCuentaIngreso.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdCuentaIngreso.SelectedValue
            Close()
        Else
            FrmPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class