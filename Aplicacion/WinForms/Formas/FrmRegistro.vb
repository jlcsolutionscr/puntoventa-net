Imports System.Collections.Generic
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Utilities

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
        dvcTipo.Width = 300
        dgvDatos.Columns.Add(dvcTipo)
        dvcDescripcion.HeaderText = "Terminal"
        dvcDescripcion.DataPropertyName = "IdTerminal"
        dvcDescripcion.Width = 50
        dgvDatos.Columns.Add(dvcDescripcion)
        dgvDatos.DataSource = listado
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmLineaListado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            dgvDatos.DataSource = listado
            strDispositivoId = Utilitario.ObtenerIdentificadorEquipo()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub
    Private Async Sub BtnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Try
            Await Puntoventa.RegistrarTerminal(txtUsuario.Text, strEncryptedPassword, txtIdentificacion.Text, intIdSucursal, intIdTerminal, StaticTipoDispisitivo.AppEscritorio, strDispositivoId)
            MessageBox.Show("Equipo registrado satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        FrmPrincipal.bolSalir = True
        Close()
    End Sub

    Private Async Sub BtnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            strEncryptedPassword = Utilitario.EncriptarDatos(txtClave.Text, FrmPrincipal.strKey)
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
                MessageBox.Show("No existen terminales disponibles para la empresa consultada. Por favor, pongase en contacto con su proveedor del servicio.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvDatos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDatos.CellContentClick
        intIdSucursal = dgvDatos.CurrentRow.Cells(0).Value
        intIdTerminal = dgvDatos.CurrentRow.Cells(2).Value
    End Sub
#End Region
End Class