Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmAplicaTraslado
#Region "Variables"
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn
        Dim dvcTopeCredito As New DataGridViewTextBoxColumn

        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdTraslado"
        dvcId.Width = 50
        dgvListado.Columns.Add(dvcId)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 70
        dgvListado.Columns.Add(dvcFecha)
        dvcNombre.HeaderText = "Origen del traslado"
        dvcNombre.DataPropertyName = "NombreSucursal"
        dvcNombre.Width = 400
        dgvListado.Columns.Add(dvcNombre)
        dvcTopeCredito.HeaderText = "Total"
        dvcTopeCredito.DataPropertyName = "Total"
        dvcTopeCredito.Width = 100
        dvcTopeCredito.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcTopeCredito)
    End Sub

    Private Async Function ActualizarDatos(strId As String) As Task
        Dim intId As Integer = 0
        If strId <> "" Then intId = CInt(txtId.Text)
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListaTrasladosPorAplicar(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token, intId)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtId.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 0)
    End Sub

    Private Async Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            Await ActualizarDatos("")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        If dgvListado.RowCount > 0 Then
            btnAplicar_Click(sender, New EventArgs())
        End If
    End Sub

    Private Async Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        Await ActualizarDatos(txtId.Text)
    End Sub

    Private Async Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        If dgvListado.RowCount > 0 Then
            Dim intSelected As Integer = dgvListado.CurrentRow.Cells(0).Value
            If MessageBox.Show("Desea aplicar este registro de traslado?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AplicarTraslado(intSelected, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Await ActualizarDatos(txtId.Text)
            End If
        End If
    End Sub
#End Region
End Class
