Imports LeandroSoftware.ClienteWCF

Public Class FrmMenuLinea
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        cboIdLinea.ValueMember = "Id"
        cboIdLinea.DisplayMember = "Descripcion"
        cboIdLinea.DataSource = Await Puntoventa.ObtenerListadoLineas(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        cboIdLinea.SelectedValue = 0
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
        If cboIdLinea.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboIdLinea.SelectedValue
            Close()
        Else
            FrmPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class