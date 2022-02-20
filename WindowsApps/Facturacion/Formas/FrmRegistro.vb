Imports System.Collections.Generic
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Constantes
Imports LeandroSoftware.Common.Seguridad

Public Class FrmRegistro
#Region "Variables"
    Private listado As List(Of EquipoRegistrado) = New List(Of EquipoRegistrado)()
    Private intIdSucursal As Integer
    Private intIdTerminal As Integer
    Private strDispositivoId As String
    Private strEncryptedPassword As String
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcTipo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Sucursal"
        dvcId.DataPropertyName = "IdSucursal"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcTipo.HeaderText = "Nombre Sucursal"
        dvcTipo.DataPropertyName = "NombreSucursal"
        dvcTipo.Width = 250
        dgvDatos.Columns.Add(dvcTipo)
        dvcDescripcion.HeaderText = "Terminal"
        dvcDescripcion.DataPropertyName = "IdTerminal"
        dvcDescripcion.Width = 50
        dgvDatos.Columns.Add(dvcDescripcion)
        dgvDatos.DataSource = listado
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRegistro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmRegistro_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            dgvDatos.DataSource = listado
            strDispositivoId = Puntoventa.ObtenerIdentificadorEquipo()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub
    Private Async Sub BtnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Try
            Await Puntoventa.RegistrarTerminal(txtUsuario.Text, strEncryptedPassword, txtIdentificacion.Text, intIdSucursal, intIdTerminal, StaticTipoDispisitivo.AppEscritorio, strDispositivoId)
            MessageBox.Show("Equipo registrado satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        FrmPrincipal.bolSalir = True
        Close()
    End Sub

    Private Async Sub BtnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            strEncryptedPassword = Encriptador.EncriptarDatos(txtClave.Text)
            listado = Await Puntoventa.ObtenerListadoTerminalesDisponibles(txtUsuario.Text, strEncryptedPassword, txtIdentificacion.Text, StaticTipoDispisitivo.AppEscritorio)
            dgvDatos.DataSource = listado
            If listado.Count > 0 Then
                txtIdentificacion.Enabled = False
                txtUsuario.Enabled = False
                txtClave.Enabled = False
                intIdSucursal = dgvDatos.Rows(0).Cells(0).Value
                intIdTerminal = dgvDatos.Rows(0).Cells(2).Value
                btnRegistrar.Enabled = True
            Else
                MessageBox.Show("No existen terminales disponibles para la empresa consultada. Por favor, pongase en contacto con su proveedor del servicio.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvDatos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDatos.CellContentClick
        intIdSucursal = dgvDatos.CurrentRow.Cells(0).Value
        intIdTerminal = dgvDatos.CurrentRow.Cells(2).Value
    End Sub

    Private Sub TxtUsuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsuario.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtClave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClave.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtIdentificacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdentificacion.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            e.Handled = True
        End If
    End Sub
#End Region
End Class