Imports System.Collections.Generic
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.ClienteWCF

Public Class FrmUsuario
#Region "Variables"
    Private dtbRolePorUsuario As DataTable
    Private dtrRolePorUsuario As DataRow
    Private I As Short
    Private datos As Usuario
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

    Private Sub CargarLineaDetalleRole(ByVal intIdRole As Integer, ByVal strDescripcion As String)
        If dtbRolePorUsuario.Rows.Contains(intIdRole) Then
            MessageBox.Show("El role seleccionado ya esta asignado. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        Try
            cboRole.ValueMember = "IdRole"
            cboRole.DisplayMember = "Nombre"
            cboRole.DataSource = Await PuntoventaWCF.ObtenerListaRoles()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmUsuario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IniciaDetalleRole()
        EstablecerPropiedadesDataGridView()
        CargarCombos()
        If intIdUsuario > 0 Then
            Dim strDecryptedPassword As String
            Try
                datos = Await PuntoventaWCF.ObtenerUsuario(intIdUsuario)
                strDecryptedPassword = Core.Utilitario.DesencriptarDatos(datos.Clave, FrmPrincipal.strKey)
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
            txtPassword.Text = strDecryptedPassword
            chkModifica.Checked = datos.Modifica
            chkAutoriza.Checked = datos.AutorizaCredito
            CargarDetalleRole(datos)
        Else
            datos = New Usuario
        End If
        bolInit = False
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim strEncryptedPassword As String
        If datos.IdUsuario = 0 Then
            Dim empresaUsuario As UsuarioPorEmpresa = New UsuarioPorEmpresa With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
            }
            Dim detalleEmpresa As List(Of UsuarioPorEmpresa) = New List(Of UsuarioPorEmpresa) From {
                empresaUsuario
            }
            datos.UsuarioPorEmpresa = detalleEmpresa
        End If
        Try
            strEncryptedPassword = Core.Utilitario.EncriptarDatos(txtPassword.Text, FrmPrincipal.strKey)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End Try
        datos.CodigoUsuario = txtUsuario.Text
        datos.Clave = strEncryptedPassword
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
                Dim strIdUsuario = Await PuntoventaWCF.AgregarUsuario(datos)
                txtIdUsuario.Text = strIdUsuario
            Else
                Await PuntoventaWCF.ActualizarUsuario(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub BtnInsertarRole_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertarRole.Click
        If cboRole.SelectedValue IsNot Nothing Then
            CargarLineaDetalleRole(cboRole.SelectedValue, cboRole.Text)
        Else
            MessageBox.Show("Debe selecionar el Permiso para asignar al usuario", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub BtnEliminarRole_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarRole.Click
        If dtbRolePorUsuario.Rows.Count > 0 Then
            dtbRolePorUsuario.Rows.Remove(dtbRolePorUsuario.Rows.Find(dgvRoleXUsuario.CurrentRow.Cells(0).Value))
        End If
    End Sub
#End Region
End Class