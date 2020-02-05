Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Utilitario

Public Class FrmFacturaCompra
#Region "Variables"
    Private decExcento, decGravado, decExonerado, decImpuesto, decTotal, decSubTotal, decPrecioVenta As Decimal
    Private I As Short
    Private dtbDetalleProforma As DataTable
    Private dtrRowDetProforma As DataRow
    Private facturaCompra As FacturaCompra
    Private detalleFacturaCompra As DetalleFacturaCompra
    Private bolInit As Boolean = True
#End Region

#Region "M�todos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleProforma = New DataTable()
        dtbDetalleProforma.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("CODIGO", GetType(String))
        dtbDetalleProforma.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleProforma.Columns.Add("IDIMPUESTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("UNIDADMEDIDA", GetType(String))
        dtbDetalleProforma.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleProforma.PrimaryKey = {dtbDetalleProforma.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleProforma.Columns.Clear()
        grdDetalleProforma.AutoGenerateColumns = False

        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcTipoImpuesto As New DataGridViewCheckBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn
        Dim dvcUnidadMedida As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.Visible = True
        dvcCantidad.ReadOnly = True
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcCantidad)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "C�digo"
        dvcCodigo.Width = 100
        dvcCodigo.Visible = True
        dvcCodigo.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripci�n"
        dvcDescripcion.Width = 370
        dvcDescripcion.Visible = True
        dvcDescripcion.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcDescripcion)

        dvcTipoImpuesto.DataPropertyName = "IDIMPUESTO"
        dvcTipoImpuesto.HeaderText = "Exc"
        dvcTipoImpuesto.Width = 0
        dvcTipoImpuesto.Visible = False
        dvcTipoImpuesto.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcTipoImpuesto)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        dvcTipoImpuesto.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcPorcentajeIVA)

        dvcUnidadMedida.DataPropertyName = "UNIDADMEDIDA"
        dvcUnidadMedida.HeaderText = "Unid"
        dvcUnidadMedida.Width = 50
        dvcUnidadMedida.Visible = True
        dvcUnidadMedida.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcUnidadMedida)

        dvcPrecio.DataPropertyName = "PRECIO"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 100
        dvcPrecio.Visible = True
        dvcPrecio.ReadOnly = True
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.Visible = True
        dvcTotal.ReadOnly = True
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcTotal)
    End Sub

    Private Sub CargarLineaDetalleFacturaCompra(decCantidad As Decimal, strCodigo As String, strDescripcion As String, intIdImpuesto As Integer, intPorcetajeIVA As Integer, strUnidadMedida As String, decPrecio As Decimal)
        dtrRowDetProforma = dtbDetalleProforma.NewRow
        dtrRowDetProforma.Item(0) = decCantidad
        dtrRowDetProforma.Item(1) = strCodigo
        dtrRowDetProforma.Item(2) = strDescripcion
        dtrRowDetProforma.Item(3) = intIdImpuesto
        dtrRowDetProforma.Item(4) = intPorcetajeIVA
        dtrRowDetProforma.Item(5) = strUnidadMedida
        dtrRowDetProforma.Item(6) = decPrecio
        dtrRowDetProforma.Item(7) = decCantidad * decPrecio
        dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        grdDetalleProforma.Refresh()
        CargarTotales()
    End Sub

    Private Sub CargarTotales()
        decSubTotal = 0
        decGravado = 0
        decExonerado = 0
        decExcento = 0
        decImpuesto = 0
        Dim intPorcentajeExoneracion As Integer = 0
        If txtPorcentajeExoneracion.Text <> "" Then intPorcentajeExoneracion = CInt(txtPorcentajeExoneracion.Text)
        For I = 0 To dtbDetalleProforma.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleProforma.Rows(I).Item(4)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleProforma.Rows(I).Item(6) * decTasaImpuesto / 100
                If intPorcentajeExoneracion > 0 Then
                    Dim decGravadoPorcentual = dtbDetalleProforma.Rows(I).Item(6) * (1 - (intPorcentajeExoneracion / 100))
                    decGravado += Math.Round(decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
                    decExonerado += Math.Round(dtbDetalleProforma.Rows(I).Item(6) - decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
                    decImpuestoProducto = decGravadoPorcentual * decTasaImpuesto / 100
                Else
                    decGravado += Math.Round(dtbDetalleProforma.Rows(I).Item(6), 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
                End If
                decImpuesto += Math.Round(decImpuestoProducto, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
            Else
                decExcento += Math.Round(dtbDetalleProforma.Rows(I).Item(6), 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
            End If
        Next
        decSubTotal = decGravado + decExcento + decExonerado
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        decExonerado = Math.Round(decExonerado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decSubTotal + decImpuesto, 2, MidpointRounding.AwayFromZero)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
    End Sub

    Private Async Function CargarListadoBarrios(IdProvincia As Integer, IdCanton As Integer, IdDistrito As Integer) As Task
        cboCanton.ValueMember = "Id"
        cboCanton.DisplayMember = "Descripcion"
        cboCanton.DataSource = Await Puntoventa.ObtenerListadoCantones(IdProvincia, FrmPrincipal.usuarioGlobal.Token)
        cboDistrito.ValueMember = "Id"
        cboDistrito.DisplayMember = "Descripcion"
        cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(IdProvincia, IdCanton, FrmPrincipal.usuarioGlobal.Token)
        cboBarrio.ValueMember = "Id"
        cboBarrio.DisplayMember = "Descripcion"
        cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(IdProvincia, IdCanton, IdDistrito, FrmPrincipal.usuarioGlobal.Token)
        cboTipoImpuesto.ValueMember = "Id"
        cboTipoImpuesto.DisplayMember = "Descripcion"
        cboTipoImpuesto.DataSource = Await Puntoventa.ObtenerListadoTipoImpuesto(FrmPrincipal.usuarioGlobal.Token)
    End Function

    Private Async Function CargarCombos() As Task
        Dim columns() As String = {"Und", "Sp", "Spe", "St", "Os"}
        cboUnidadMedida.MaxDropDownItems = columns.Length
        For i As Integer = 0 To (columns.Length - 1)
            cboUnidadMedida.Items.Add(columns(i))
        Next
        cboTipoIdentificacion.ValueMember = "Id"
        cboTipoIdentificacion.DisplayMember = "Descripcion"
        cboTipoIdentificacion.DataSource = Await Puntoventa.ObtenerListadoTipoIdentificacion(FrmPrincipal.usuarioGlobal.Token)
        cboProvincia.ValueMember = "Id"
        cboProvincia.DisplayMember = "Descripcion"
        cboProvincia.DataSource = Await Puntoventa.ObtenerListadoProvincias(FrmPrincipal.usuarioGlobal.Token)
        cboTipoExoneracion.ValueMember = "Id"
        cboTipoExoneracion.DisplayMember = "Descripcion"
        cboTipoExoneracion.DataSource = Await Puntoventa.ObtenerListadoTipoExoneracion(FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmFacturaCompra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
    End Sub

    Private Async Sub FrmFacturaCompra_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
            Await CargarListadoBarrios(1, 1, 1)
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            grdDetalleProforma.DataSource = dtbDetalleProforma
            txtFechaExoneracion.Text = "01/01/2019"
            txtPorcentajeExoneracion.Text = "0"
            bolInit = False
            txtCantidad.Text = "1"
            cboTipoImpuesto.SelectedValue = 8
            cboUnidadMedida.SelectedIndex = 0
            txtPrecio.Text = "0.00"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            cboTipoIdentificacion.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub CboProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincia.SelectedIndexChanged
        If Not bolInit Then
            bolInit = True
            Await CargarListadoBarrios(cboProvincia.SelectedValue, 1, 1)
            bolInit = False
        End If
    End Sub

    Private Async Sub CboCanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCanton.SelectedIndexChanged
        If Not bolInit Then
            bolInit = True
            cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(cboProvincia.SelectedValue, cboCanton.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, 1, FrmPrincipal.usuarioGlobal.Token)
            bolInit = False
        End If
    End Sub

    Private Async Sub CboDistrito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrito.SelectedIndexChanged
        If Not bolInit Then
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, cboDistrito.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
        End If
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdFactCompra.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtIdentificacion.Text = ""
        txtDireccion.Text = ""
        txtNombre.Text = ""
        txtNombreComercial.Text = ""
        txtTelefono.Text = ""
        txtCorreoElectronico.Text = ""
        cboTipoIdentificacion.SelectedIndex = 0
        Await CargarListadoBarrios(1, 1, 1)
        cboTipoExoneracion.SelectedIndex = 0
        txtNumDocExoneracion.Text = ""
        txtNombreInstExoneracion.Text = ""
        txtFechaExoneracion.Text = "01/01/2019"
        txtPorcentajeExoneracion.Text = "0"
        dtbDetalleProforma.Rows.Clear()
        grdDetalleProforma.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCantidad.Text = "1"
        txtCodigo.Text = ""
        cboTipoImpuesto.SelectedValue = 8
        txtDescripcion.Text = ""
        cboUnidadMedida.SelectedIndex = 0
        txtPrecio.Text = "0.00"
        txtTextoAdicional.Text = ""
        decTotal = 0
        bolInit = False
        cboTipoIdentificacion.Focus()
    End Sub

    Private Async Sub txtIdentificacion_Validated(sender As Object, e As EventArgs) Handles txtIdentificacion.Validated
        If cboTipoIdentificacion.SelectedValue = 0 Then
            Dim cliente As Cliente = Nothing
            cliente = Await Puntoventa.ValidaIdentificacionCliente(FrmPrincipal.empresaGlobal.IdEmpresa, txtIdentificacion.Text, FrmPrincipal.usuarioGlobal.Token)
            If cliente IsNot Nothing Then
                bolInit = True
                Await CargarListadoBarrios(cliente.IdProvincia, cliente.IdCanton, cliente.IdDistrito)
                cboProvincia.SelectedValue = cliente.IdProvincia
                cboCanton.SelectedValue = cliente.IdCanton
                cboDistrito.SelectedValue = cliente.IdDistrito
                cboBarrio.SelectedValue = 0
                txtNombre.Text = cliente.Nombre
                bolInit = False
            End If
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtFecha.Text = "" Or decTotal = 0 Then
            MessageBox.Show("Informaci�n incompleta.  Favor verificar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnAgregar.Focus()
        btnGuardar.Enabled = False
        If txtIdFactCompra.Text = "" Then
            facturaCompra = New FacturaCompra With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdTerminal = FrmPrincipal.equipoGlobal.IdTerminal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdTipoMoneda = 1,
                .Fecha = Now(),
                .IdCondicionVenta = 1,
                .PlazoCredito = 0,
                .NombreEmisor = txtNombre.Text,
                .IdTipoIdentificacion = cboTipoIdentificacion.SelectedValue,
                .IdentificacionEmisor = txtIdentificacion.Text,
                .NombreComercialEmisor = txtNombreComercial.Text,
                .TelefonoEmisor = txtTelefono.Text,
                .IdProvinciaEmisor = cboProvincia.SelectedValue,
                .IdCantonEmisor = cboCanton.SelectedValue,
                .IdDistritoEmisor = cboDistrito.SelectedValue,
                .IdBarrioEmisor = cboBarrio.SelectedValue,
                .DireccionEmisor = txtDireccion.Text,
                .CorreoElectronicoEmisor = txtCorreoElectronico.Text,
                .IdTipoExoneracion = cboTipoExoneracion.SelectedValue,
                .NumDocExoneracion = txtNumDocExoneracion.Text,
                .NombreInstExoneracion = txtNombreInstExoneracion.Text,
                .FechaEmisionDoc = txtFechaExoneracion.Value,
                .PorcentajeExoneracion = txtPorcentajeExoneracion.Text,
                .TextoAdicional = txtTextoAdicional.Text,
                .Excento = decExcento,
                .Gravado = decGravado,
                .Exonerado = decExonerado,
                .Descuento = 0,
                .Impuesto = decImpuesto
            }
            For I = 0 To dtbDetalleProforma.Rows.Count - 1
                detalleFacturaCompra = New DetalleFacturaCompra With {
                    .Linea = I + 1,
                    .Cantidad = dtbDetalleProforma.Rows(I).Item(0),
                    .Codigo = dtbDetalleProforma.Rows(I).Item(1),
                    .Descripcion = dtbDetalleProforma.Rows(I).Item(2),
                    .IdImpuesto = dtbDetalleProforma.Rows(I).Item(3),
                    .PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(4),
                    .UnidadMedida = dtbDetalleProforma.Rows(I).Item(5),
                    .PrecioVenta = dtbDetalleProforma.Rows(I).Item(6)
                }
                facturaCompra.DetalleFacturaCompra.Add(detalleFacturaCompra)
            Next
            Try
                txtIdFactCompra.Text = Await Puntoventa.AgregarFacturaCompra(facturaCompra, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdFactCompra.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacci�n efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        BtnAgregar_Click(btnAgregar, New EventArgs())
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If txtCantidad.Text <> "" And txtCodigo.Text <> "" And txtDescripcion.Text <> "" And txtPrecio.Text <> "" Then
            If txtCantidad.Text = "0" Then
                MessageBox.Show("La cantidad no puede ser 0. Por favor verifique la informaci�n. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            CargarLineaDetalleFacturaCompra(txtCantidad.Text, txtCodigo.Text, txtDescripcion.Text, cboTipoImpuesto.SelectedValue, 13, cboUnidadMedida.Text, txtPrecio.Text)
        Else
            MessageBox.Show("Debe ingresar la informaci�n requerida para la l�nea de la factura de compra. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleProforma.Rows.Count > 0 Then
            dtbDetalleProforma.Rows.Clear()
            grdDetalleProforma.Refresh()
            CargarTotales()
            txtCantidad.Focus()
        End If
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtPorcentajeExoneracion.KeyPress, txtIdentificacion.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecio.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class