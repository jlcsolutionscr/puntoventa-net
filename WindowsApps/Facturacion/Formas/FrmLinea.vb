Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF
Imports System.Threading.Tasks

Public Class FrmLinea
#Region "Variables"
    Public intIdLinea As Integer
    Private dtbLineaPorSucursal As DataTable
    Private dtrLineaPorSucursal As DataRow
    Private datos As Linea
    Private lineaPorSucursal As LineaPorSucursal
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub IniciaDetalleSucursal()
        dtbLineaPorSucursal = New DataTable()
        dtbLineaPorSucursal.Columns.Add("IdSucursal", GetType(String))
        dtbLineaPorSucursal.Columns.Add("Nombre", GetType(String))
        dtbLineaPorSucursal.PrimaryKey = {dtbLineaPorSucursal.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        dgvLineaPorSucursal.Columns.Clear()
        dgvLineaPorSucursal.AutoGenerateColumns = False

        Dim dvcIdRole As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn

        dvcIdRole.DataPropertyName = "IdSucursal"
        dvcIdRole.HeaderText = "Id"
        dvcIdRole.Visible = False
        dgvLineaPorSucursal.Columns.Add(dvcIdRole)

        dvcNombre.DataPropertyName = "Nombre"
        dvcNombre.HeaderText = "Nombre sucursal"
        dvcNombre.Width = 418
        dgvLineaPorSucursal.Columns.Add(dvcNombre)
    End Sub

    Private Sub CargarDetalleSucursal(ByVal linea As Linea)
        For Each detalle As LineaPorSucursal In linea.LineaPorSucursal
            dtrLineaPorSucursal = dtbLineaPorSucursal.NewRow
            dtrLineaPorSucursal.Item(0) = detalle.IdSucursal
            dtrLineaPorSucursal.Item(1) = detalle.SucursalPorEmpresa.NombreSucursal
            dtbLineaPorSucursal.Rows.Add(dtrLineaPorSucursal)
        Next
        dgvLineaPorSucursal.DataSource = dtbLineaPorSucursal
        dgvLineaPorSucursal.Refresh()
    End Sub

    Private Sub CargarLineaDetalleSucursal(ByVal intId As Integer, ByVal strDescripcion As String)
        If dtbLineaPorSucursal.Rows.Contains(intId) Then
            MessageBox.Show("La sucursal seleccionada ya se encuentra asignada. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            dtrLineaPorSucursal = dtbLineaPorSucursal.NewRow
            dtrLineaPorSucursal.Item(0) = intId
            dtrLineaPorSucursal.Item(1) = strDescripcion
            dtbLineaPorSucursal.Rows.Add(dtrLineaPorSucursal)
            dgvLineaPorSucursal.DataSource = dtbLineaPorSucursal
            dgvLineaPorSucursal.Refresh()
        End If
    End Sub

    Private Async Function CargarCombos() As Task
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = Await Puntoventa.ObtenerListadoSucursales(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmLinea_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmLinea_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleSucursal()
            EstablecerPropiedadesDataGridView()
            Await CargarCombos()
            If intIdLinea > 0 Then
                datos = Await Puntoventa.ObtenerLinea(intIdLinea, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    MessageBox.Show("La Línea seleccionada no existe", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                    Exit Sub
                End If
                txtIdLinea.Text = datos.IdLinea
                txtDescripcion.Text = datos.Descripcion
                CargarDetalleSucursal(datos)
            Else
                datos = New Linea
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            btnGuardar.Enabled = True
            Exit Sub
        End If
        If datos.IdLinea = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Descripcion = txtDescripcion.Text
        datos.LineaPorSucursal.Clear()
        For I As Short = 0 To dtbLineaPorSucursal.Rows.Count - 1
            lineaPorSucursal = New LineaPorSucursal With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = dtbLineaPorSucursal.Rows(I).Item(0)
            }
            If datos.IdLinea > 0 Then
                lineaPorSucursal.IdLinea = datos.IdLinea
            End If
            datos.LineaPorSucursal.Add(lineaPorSucursal)
        Next
        Try
            If datos.IdLinea = 0 Then
                Await Puntoventa.AgregarLinea(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarLinea(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub BtnInsertar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertar.Click
        If cboSucursal.SelectedValue IsNot Nothing Then
            CargarLineaDetalleSucursal(cboSucursal.SelectedValue, cboSucursal.Text)
        Else
            MessageBox.Show("Debe selecionar el Permiso para asignar al usuario", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub BtnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If dtbLineaPorSucursal.Rows.Count > 0 And dgvLineaPorSucursal.CurrentRow IsNot Nothing Then
            dtbLineaPorSucursal.Rows.Remove(dtbLineaPorSucursal.Rows.Find(dgvLineaPorSucursal.CurrentRow.Cells(0).Value))
        End If
    End Sub
#End Region
End Class