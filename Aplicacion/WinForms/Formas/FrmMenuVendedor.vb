Imports LeandroSoftware.ClienteWCF

Public Class FrmMenuVendedor
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        cboIdVendedor.ValueMember = "Id"
        cboIdVendedor.DisplayMember = "Descripcion"
        cboIdVendedor.DataSource = Await Puntoventa.ObtenerListadoVendedores(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboIdVendedor.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            CargarCombos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdVendedor.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdVendedor.SelectedValue
            Close()
        Else
            FrmPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class