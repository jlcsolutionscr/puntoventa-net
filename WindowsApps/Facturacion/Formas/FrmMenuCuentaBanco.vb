Imports LeandroSoftware.ClienteWCF

Public Class FrmMenuCuentaBanco
#Region "Variables"
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        cboIdCuentaBanco.ValueMember = "IdCuenta"
        cboIdCuentaBanco.DisplayMember = "Descripcion"
        cboIdCuentaBanco.DataSource = Await Puntoventa.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
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
            MessageBox.Show("Debe seleccionar una cuenta bancaria para continuar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
End Class