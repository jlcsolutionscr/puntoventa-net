Imports LeandroSoftware.ClienteWCF

Public Class FrmMenuCuentaEgreso
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        cboIdCuentaEgreso.ValueMember = "Id"
        cboIdCuentaEgreso.DisplayMember = "Descripcion"
        cboIdCuentaEgreso.DataSource = Await Puntoventa.ObtenerListadoCuentasEgreso(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboIdCuentaEgreso.SelectedValue = 0
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