Public Class FrmCierreContable
#Region "Variables"
#End Region

#Region "Eventos Controles"
    Private Sub CmdProcesar_Click(sender As Object, e As EventArgs) Handles CmdProcesar.Click
        If MessageBox.Show("Desea procesar el Cierre Contable Mensual", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                'servicioContabilidad.ProcesarCierreMensual(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("Proceso ejecutado exitosamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmdProcesar.Enabled = False
        Else
            MessageBox.Show("Proceso cancelado por el usuario", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
#End Region
End Class