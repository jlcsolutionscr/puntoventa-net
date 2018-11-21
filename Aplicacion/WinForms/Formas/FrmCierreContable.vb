Imports System.Collections.Generic
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity
Imports LeandroSoftware.Core.CommonTypes

Public Class FrmCierreContable
#Region "Variables"
    Private servicioContabilidad As IContabilidadService
#End Region

#Region "Eventos Controles"
    Private Sub FrmCierreContable_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Try
            servicioContabilidad = FrmMenuPrincipal.unityContainer.Resolve(Of IContabilidadService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub CmdProcesar_Click(sender As Object, e As EventArgs) Handles CmdProcesar.Click
        If MessageBox.Show("Desea procesar el Cierre Contable Mensual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                servicioContabilidad.ProcesarCierreMensual(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("Proceso ejecutado exitosamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmdProcesar.Enabled = False
        Else
            MessageBox.Show("Proceso cancelado por el usuario", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
#End Region
End Class