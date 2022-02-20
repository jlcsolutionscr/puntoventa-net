Imports System.Collections.Generic
Imports System.Linq
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Seguridad

Public Class FrmUsuario
#Region "Variables"
    Private dtbRolePorUsuario As DataTable
    Private dtrRolePorUsuario As DataRow
    Private datos As Usuario
    Private rolePorUsuario As RolePorUsuario
    Private sucursalPorUsuario As SucursalPorUsuario
    Public intIdUsuario As Integer
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleRole()
        dtbRolePorUsuario = New DataTable()
        dtbRolePorUsuario.Columns.Add("IDROLE", GetType(String))
        dtbRolePorUsuario.Columns.Add("NOMBRE", GetType(String))
        dtbRolePorUsuario.PrimaryKey = {dtbRolePorUsuario.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        dgvRoleXUsuario.Columns.Clear()
        dgvRoleXUsuario.AutoGenerateColumns = False

        Dim dvcIdRole As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn

        dvcIdRole.DataPropertyName = "IDROLE"
        dvcIdRole.HeaderText = "IdRole"
        dvcIdRole.Visible = False
        dgvRoleXUsuario.Columns.Add(dvcIdRole)

        dvcNombre.DataPropertyName = "NOMBRE"
        dvcNombre.HeaderText = "Nombre del Role Asignado"
        dvcNombre.Width = 425
        dgvRoleXUsuario.Columns.Add(dvcNombre)
    End Sub

    Private Sub CargarDetalleRole(ByVal usuario As Usuario)
        For Each detalle As RolePorUsuario In usuario.RolePorUsuario
            dtrRolePorUsuario = dtbRolePorUsuario.NewRow
            dtrRolePorUsuario.Item(0) = detalle.IdRole
            dtrRolePorUsuario.Item(1) = detalle.Role.Descripcion
            dtbRolePorUsuario.Rows.Add(dtrRolePorUsuario)
        Next
        dgvRoleXUsuario.DataSource = dtbRolePorUsuario
        dgvRoleXUsuario.Refresh()
    End Sub

    Private Sub CargarLineaDetalleRole(ByVal intIdRole As Integer, ByVal strDescripcion As String)
        If dtbRolePorUsuario.Rows.Contains(intIdRole) Then
            MessageBox.Show("El role seleccionado ya esta asignado. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            dtrRolePorUsuario = dtbRolePorUsuario.NewRow
            dtrRolePorUsuario.Item(0) = intIdRole
            dtrRolePorUsuario.Item(1) = strDescripcion
            dtbRolePorUsuario.Rows.Add(dtrRolePorUsuario)
            dgvRoleXUsuario.DataSource = dtbRolePorUsuario
            dgvRoleXUsuario.Refresh()
        End If
    End Sub

    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtUsuario.Text = "" Then
            pCampo = "Usuario"
            Return False
        ElseIf txtPassword.Text = "" Then
            pCampo = "Contraseña"
            Return False
        Else
            Return True
        End If
    End Function

    Private Async Sub CargarCombos()
        cboRole.ValueMember = "Id"
        cboRole.DisplayMember = "Descripcion"
        cboRole.DataSource = Await Puntoventa.ObtenerListadoRolesPorEmpresa(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = FrmPrincipal.listaSucursales
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmUsuario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmUsuario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleRole()
            EstablecerPropiedadesDataGridView()
            CargarCombos()
            If intIdUsuario > 0 Then
                txtUsuario.ReadOnly = True
                Dim strDecryptedPassword As String
                Try
                    datos = Await Puntoventa.ObtenerUsuario(intIdUsuario, FrmPrincipal.usuarioGlobal.Token)
                    strDecryptedPassword = Encriptador.DesencriptarDatos(datos.Clave)
                Catch ex As Exception
                    Throw ex
                End Try
                If datos Is Nothing Then
                    Throw New Exception("El usuario seleccionado no existe")
                End If
                txtIdUsuario.Text = datos.IdUsuario
                txtUsuario.Text = datos.CodigoUsuario
                txtPassword.Text = strDecryptedPassword
                txtPorcMaxDescuento.Text = datos.PorcMaxDescuento
                chkRegistraDispositivo.Checked = datos.PermiteRegistrarDispositivo
                cboSucursal.SelectedValue = datos.SucursalPorUsuario.ToList()(0).IdSucursal
                CargarDetalleRole(datos)
            Else
                txtUsuario.ReadOnly = False
                datos = New Usuario
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
        Dim strEncryptedPassword As String
        If datos.IdUsuario = 0 Then
            sucursalPorUsuario = New SucursalPorUsuario With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal
            }
            Dim listaSucursalPorUsuario As List(Of SucursalPorUsuario) = New List(Of SucursalPorUsuario) From {
                sucursalPorUsuario
            }
            datos.SucursalPorUsuario = listaSucursalPorUsuario
        End If
        Try
            strEncryptedPassword = Encriptador.EncriptarDatos(txtPassword.Text)
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End Try
        datos.CodigoUsuario = txtUsuario.Text
        datos.Clave = strEncryptedPassword
        datos.PorcMaxDescuento = txtPorcMaxDescuento.Text
        datos.PermiteRegistrarDispositivo = chkRegistraDispositivo.Checked
        datos.RolePorUsuario.Clear()
        For I As Short = 0 To dtbRolePorUsuario.Rows.Count - 1
            rolePorUsuario = New RolePorUsuario With {
                .IdRole = dtbRolePorUsuario.Rows(I).Item(0)
            }
            If datos.IdUsuario > 0 Then
                rolePorUsuario.IdUsuario = datos.IdUsuario
            End If
            datos.RolePorUsuario.Add(rolePorUsuario)
        Next
        Try
            If datos.IdUsuario = 0 Then
                Await Puntoventa.AgregarUsuario(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarUsuario(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub BtnInsertarRole_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertarRole.Click
        If cboRole.SelectedValue IsNot Nothing Then
            CargarLineaDetalleRole(cboRole.SelectedValue, cboRole.Text)
        Else
            MessageBox.Show("Debe selecionar el Permiso para asignar al usuario", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub BtnEliminarRole_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarRole.Click
        If dtbRolePorUsuario.Rows.Count > 0 Then
            dtbRolePorUsuario.Rows.Remove(dtbRolePorUsuario.Rows.Find(dgvRoleXUsuario.CurrentRow.Cells(0).Value))
        End If
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtPorcMaxDescuento.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub txtPorcMaxDescuento_TextChanged(sender As Object, e As EventArgs) Handles txtPorcMaxDescuento.TextChanged
        If txtPorcMaxDescuento.Text = "" Then txtPorcMaxDescuento.Text = "0.00"
    End Sub
#End Region
End Class