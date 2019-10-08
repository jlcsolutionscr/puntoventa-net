Imports LeandroSoftware.Core.ClienteWCF

Public Class FrmMenuLinea
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        Try
            cboIdLinea.ValueMember = "IdLinea"
            cboIdLinea.DisplayMember = "Descripcion"
            cboIdLinea.DataSource = Await ClienteFEWCF.ObtenerListadoLineas(FrmPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboIdLinea.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarCombos()
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