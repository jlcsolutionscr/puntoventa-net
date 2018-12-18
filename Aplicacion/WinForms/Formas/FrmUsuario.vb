Imports System.Collections.Generic
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmUsuario
#Region "Variables"
    Private dtbRolePorUsuario As DataTable
    Private dtrRolePorUsuario As DataRow
    Private I As Short
    Private datos As Usuario
    Private role As Role
    Private rolePorUsuario As RolePorUsuario
    Private bolInit As Boolean = True
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

    Private Sub CargarLineaDetalleRole(ByVal role As Role)
        If dtbRolePorUsuario.Rows.Contains(role.IdRole) Then
            MessageBox.Show("El role seleccionado ya esta asignado. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            dtrRolePorUsuario = dtbRolePorUsuario.NewRow
            dtrRolePorUsuario.Item(0) = role.IdRole
            dtrRolePorUsuario.Item(1) = role.Descripcion
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

    Private Sub CargarCombos()
        Try
            cboRole.ValueMember = "IdRole"
            cboRole.DisplayMember = "Nombre"
            'cboRole.DataSource = servicioMantenimiento.ObtenerListaRoles()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmUsuario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IniciaDetalleRole()
        EstablecerPropiedadesDataGridView()
        CargarCombos()
        If intIdUsuario > 0 Then
            Try
                'datos = servicioMantenimiento.ObtenerUsuario(intIdUsuario, FrmMenuPrincipal.strThumbprint)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El usuario seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdUsuario.Text = datos.IdUsuario
            txtUsuario.Text = datos.CodigoUsuario
            txtPassword.Text = datos.ClaveSinEncriptar
            chkModifica.Checked = datos.Modifica
            chkAutoriza.Checked = datos.AutorizaCredito
            CargarDetalleRole(datos)
        Else
            datos = New Usuario
        End If
        bolInit = False
        'role = servicioMantenimiento.ObtenerRole(cboRole.SelectedValue)
        txtDescripción.Text = role.Descripcion
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If datos.IdUsuario = 0 Then
            Dim empresaUsuario As EmpresaPorUsuario = New EmpresaPorUsuario()
            empresaUsuario.IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa
            Dim detalleEmpresa As List(Of EmpresaPorUsuario) = New List(Of EmpresaPorUsuario)()
            detalleEmpresa.Add(empresaUsuario)
            datos.EmpresaPorUsuario = detalleEmpresa
        End If
        datos.CodigoUsuario = txtUsuario.Text
        datos.ClaveSinEncriptar = txtPassword.Text
        datos.Modifica = chkModifica.Checked
        datos.AutorizaCredito = chkAutoriza.Checked
        datos.RolePorUsuario.Clear()
        For I = 0 To dtbRolePorUsuario.Rows.Count - 1
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
                'datos = servicioMantenimiento.AgregarUsuario(datos, FrmMenuPrincipal.strThumbprint)
                txtIdUsuario.Text = datos.IdUsuario
            Else
                'servicioMantenimiento.ActualizarUsuario(datos, FrmMenuPrincipal.strThumbprint)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub cboRole_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboRole.SelectedValueChanged
        If Not bolInit Then
            Try
                'role = servicioMantenimiento.ObtenerRole(cboRole.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End Try
            txtDescripción.Text = role.Descripcion
        End If
    End Sub

    Private Sub btnInsertarRole_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertarRole.Click
        If cboRole.SelectedValue IsNot Nothing Then
            CargarLineaDetalleRole(role)
        Else
            MessageBox.Show("Debe selecionar el Permiso para asignar al usuario", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub btnEliminarRole_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarRole.Click
        If dtbRolePorUsuario.Rows.Count > 0 Then
            dtbRolePorUsuario.Rows.Remove(dtbRolePorUsuario.Rows.Find(dgvRoleXUsuario.CurrentRow.Cells(0).Value))
        End If
    End Sub
#End Region
End Class