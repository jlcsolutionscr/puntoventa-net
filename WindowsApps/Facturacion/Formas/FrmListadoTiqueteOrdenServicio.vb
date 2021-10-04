Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmListadoTiqueteOrdenServicio
#Region "Variables"
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdTiquete"
        dvcId.Width = 50
        dgvListado.Columns.Add(dvcId)
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 600
        dgvListado.Columns.Add(dvcDescripcion)
    End Sub

    Private Async Function ActualizarDatos() As Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListadoTiqueteOrdenServicio(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, True, FrmPrincipal.usuarioGlobal.Token)
            btnActivar.Enabled = dgvListado.Rows.Count > 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmCierreDeCajaListado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctl As Control In Controls
            If TypeOf (ctl) Is TextBox Then
                AddHandler DirectCast(ctl, TextBox).Enter, AddressOf EnterTexboxHandler
                AddHandler DirectCast(ctl, TextBox).Leave, AddressOf LeaveTexboxHandler
            End If
        Next
    End Sub

    Private Sub EnterTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.PeachPuff
    End Sub

    Private Sub LeaveTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.White
    End Sub

    Private Async Sub BtnActivar_Click(sender As Object, e As EventArgs) Handles btnActivar.Click
        btnActivar.Enabled = False
        Try
            Await Puntoventa.ActualizarEstadoTiqueteOrdenServicio(dgvListado.CurrentRow.Cells(0).Value, False, FrmPrincipal.usuarioGlobal.Token)
            Await ActualizarDatos()
        Catch ex As Exception
        End Try
    End Sub

    Private Async Sub btnRecargar_Click(sender As Object, e As EventArgs) Handles btnRecargar.Click
        btnActivar.Enabled = False
        btnRecargar.Enabled = False
        Try
            Await ActualizarDatos()
        Catch ex As Exception
            btnRecargar.Enabled = True
        End Try
        btnRecargar.Enabled = True
    End Sub

    Private Async Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            Await ActualizarDatos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub
#End Region
End Class
