Imports LeandroSoftware.Puntoventa.Servicios
Imports Unity

Public Class FrmClienteListado
#Region "Variables"
    Private servicioFacturacion As IFacturacionService
    Private servicioMantenimiento As IMantenimientoService
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 16
    Private intCantidadDePaginas As Integer
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn
        Dim dvcDireccion As New DataGridViewTextBoxColumn
        Dim dvcTelefono As New DataGridViewTextBoxColumn
        Dim dvcPermiteCredito As New DataGridViewCheckBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdCliente"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcNombre.HeaderText = "Nombre"
        dvcNombre.DataPropertyName = "Nombre"
        dvcNombre.Width = 200
        dgvDatos.Columns.Add(dvcNombre)
        dvcDireccion.HeaderText = "Dirección"
        dvcDireccion.DataPropertyName = "Direccion"
        dvcDireccion.Width = 300
        dgvDatos.Columns.Add(dvcDireccion)
        dvcTelefono.HeaderText = "Teléfono"
        dvcTelefono.DataPropertyName = "Telefono"
        dvcTelefono.Width = 70
        dgvDatos.Columns.Add(dvcTelefono)
        dvcPermiteCredito.HeaderText = "Crédito"
        dvcPermiteCredito.DataPropertyName = "PermiteCredito"
        dvcPermiteCredito.Width = 50
        dgvDatos.Columns.Add(dvcPermiteCredito)
    End Sub

    Private Sub ActualizarDatos(ByVal intNumeroPagina As Integer)
        Try
            Dim listado As IList = servicioFacturacion.ObtenerListaClientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, txtNombre.Text)
            dgvDatos.DataSource = listado
            If listado.Count() > 0 Then
                btnEditar.Enabled = True
                btnEliminar.Enabled = True
            Else
                btnEditar.Enabled = False
                btnEliminar.Enabled = False
            End If
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        dgvDatos.Refresh()
    End Sub

    Private Sub ValidarCantidadClientes()
        Try
            intTotalEmpresas = servicioFacturacion.ObtenerTotalListaClientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtNombre.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        intCantidadDePaginas = Math.Truncate(intTotalEmpresas / intFilasPorPagina) + IIf((intTotalEmpresas Mod intFilasPorPagina) = 0, 0, 1)

        If intCantidadDePaginas > 1 Then
            btnLast.Enabled = True
            btnNext.Enabled = True
            btnPrevious.Enabled = True
            btnFirst.Enabled = True
        Else
            btnLast.Enabled = False
            btnNext.Enabled = False
            btnPrevious.Enabled = False
            btnFirst.Enabled = False
        End If
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub BtnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FrmClienteListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Try
            servicioFacturacion = FrmMenuPrincipal.unityContainer.Resolve(Of IFacturacionService)()
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        EstablecerPropiedadesDataGridView()
        ValidarCantidadClientes()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub BtnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmCliente With {
            .intIdCliente = 0,
            .servicioFacturacion = servicioFacturacion,
            .servicioMantenimiento = servicioMantenimiento
        }
        formMant.ShowDialog()
        ValidarCantidadClientes()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub BtnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmCliente With {
            .intIdCliente = dgvDatos.CurrentRow.Cells(0).Value,
            .servicioFacturacion = servicioFacturacion,
            .servicioMantenimiento = servicioMantenimiento
        }
        formMant.ShowDialog()
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub BtnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                servicioFacturacion.EliminarCliente(dgvDatos.CurrentRow.Cells(0).Value)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            ValidarCantidadClientes()
            intIndiceDePagina = 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        ValidarCantidadClientes()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class