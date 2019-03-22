Imports LeandroSoftware.AccesoDatos.ClienteWCF

Public Class FrmMenuCuentaEgreso
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        Try
            cboIdCuentaEgreso.ValueMember = "IdCuenta"
            cboIdCuentaEgreso.DisplayMember = "Descripcion"
            cboIdCuentaEgreso.DataSource = Await PuntoventaWCF.ObtenerListaCuentasEgreso(FrmPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboIdCuentaEgreso.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarCombos()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdCuentaEgreso.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdCuentaEgreso.SelectedValue
            Close()
        Else
            FrmPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class