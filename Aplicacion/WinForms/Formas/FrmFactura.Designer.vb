<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFactura
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents txtDocumento As System.Windows.Forms.TextBox
    Public WithEvents txtUnidad As System.Windows.Forms.TextBox
    Public WithEvents btnBusProd As System.Windows.Forms.Button
    Public WithEvents btnAnular As System.Windows.Forms.Button
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnBuscar As System.Windows.Forms.Button
    Public WithEvents btnImprimir As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents btnEliminar As System.Windows.Forms.Button
    Public WithEvents btnInsertar As System.Windows.Forms.Button
    Public WithEvents txtPrecio As System.Windows.Forms.TextBox
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents txtImpuesto As System.Windows.Forms.TextBox
    Public WithEvents txtIdFactura As System.Windows.Forms.TextBox
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _LblImpuesto_0 As System.Windows.Forms.Label
    Public WithEvents _LblTotal_6 As System.Windows.Forms.Label
    Public WithEvents _lblSubTotal_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents grdDetalleFactura As System.Windows.Forms.DataGridView
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFactura))
        Me.txtDocumento = New System.Windows.Forms.TextBox()
        Me.txtUnidad = New System.Windows.Forms.TextBox()
        Me.btnBusProd = New System.Windows.Forms.Button()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me.txtIdFactura = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._LblImpuesto_0 = New System.Windows.Forms.Label()
        Me._LblTotal_6 = New System.Windows.Forms.Label()
        Me._lblSubTotal_5 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.grdDetalleFactura = New System.Windows.Forms.DataGridView()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNombreCliente = New System.Windows.Forms.TextBox()
        Me.txtAutorizacion = New System.Windows.Forms.TextBox()
        Me.lblAutorizacion = New System.Windows.Forms.Label()
        Me.txtTipoTarjeta = New System.Windows.Forms.TextBox()
        Me.lblTipoTarjeta = New System.Windows.Forms.Label()
        Me.cboTipoBanco = New System.Windows.Forms.ComboBox()
        Me.lblBanco = New System.Windows.Forms.Label()
        Me.cboTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdDesglosePago = New System.Windows.Forms.DataGridView()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.btnEliminarPago = New System.Windows.Forms.Button()
        Me.btnInsertarPago = New System.Windows.Forms.Button()
        Me.txtMontoPago = New System.Windows.Forms.TextBox()
        Me.cboFormaPago = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSaldoPorPagar = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.btnOrdenServicio = New System.Windows.Forms.Button()
        Me.txtIdOrdenServicio = New System.Windows.Forms.TextBox()
        Me.txtVendedor = New System.Windows.Forms.TextBox()
        Me.btnBuscaVendedor = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtIdProforma = New System.Windows.Forms.TextBox()
        Me.btnProforma = New System.Windows.Forms.Button()
        Me.txtPagoDelCliente = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCambio = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCondicionVenta = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPlazoCredito = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnGenerarPDF = New System.Windows.Forms.Button()
        Me.gpbExoneracion = New System.Windows.Forms.GroupBox()
        Me.txtFechaExoneracion = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboTipoExoneracion = New System.Windows.Forms.ComboBox()
        Me.txtPorcentajeExoneracion = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNombreInstExoneracion = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNumDocExoneracion = New System.Windows.Forms.TextBox()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        CType(Me.grdDetalleFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpbExoneracion.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDocumento
        '
        Me.txtDocumento.AcceptsReturn = True
        Me.txtDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocumento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocumento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocumento.Location = New System.Drawing.Point(103, 593)
        Me.txtDocumento.MaxLength = 500
        Me.txtDocumento.Multiline = True
        Me.txtDocumento.Name = "txtDocumento"
        Me.txtDocumento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocumento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDocumento.Size = New System.Drawing.Size(704, 45)
        Me.txtDocumento.TabIndex = 81
        '
        'txtUnidad
        '
        Me.txtUnidad.AcceptsReturn = True
        Me.txtUnidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnidad.Location = New System.Drawing.Point(648, 175)
        Me.txtUnidad.MaxLength = 0
        Me.txtUnidad.Name = "txtUnidad"
        Me.txtUnidad.ReadOnly = True
        Me.txtUnidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnidad.Size = New System.Drawing.Size(60, 20)
        Me.txtUnidad.TabIndex = 53
        Me.txtUnidad.TabStop = False
        Me.txtUnidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnBusProd
        '
        Me.btnBusProd.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBusProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBusProd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBusProd.Location = New System.Drawing.Point(167, 407)
        Me.btnBusProd.Name = "btnBusProd"
        Me.btnBusProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBusProd.Size = New System.Drawing.Size(73, 25)
        Me.btnBusProd.TabIndex = 58
        Me.btnBusProd.TabStop = False
        Me.btnBusProd.Text = "&Buscar"
        Me.btnBusProd.UseVisualStyleBackColor = False
        '
        'btnAnular
        '
        Me.btnAnular.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAnular.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAnular.Enabled = False
        Me.btnAnular.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAnular.Location = New System.Drawing.Point(200, 8)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAnular.Size = New System.Drawing.Size(64, 21)
        Me.btnAnular.TabIndex = 83
        Me.btnAnular.TabStop = False
        Me.btnAnular.Text = "&Anular"
        Me.btnAnular.UseVisualStyleBackColor = False
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAgregar.Location = New System.Drawing.Point(264, 8)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAgregar.Size = New System.Drawing.Size(64, 21)
        Me.btnAgregar.TabIndex = 84
        Me.btnAgregar.TabStop = False
        Me.btnAgregar.Text = "&Nuevo"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBuscar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBuscar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBuscar.Location = New System.Drawing.Point(136, 8)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBuscar.Size = New System.Drawing.Size(64, 21)
        Me.btnBuscar.TabIndex = 82
        Me.btnBuscar.TabStop = False
        Me.btnBuscar.Text = "B&uscar"
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'btnImprimir
        '
        Me.btnImprimir.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnImprimir.Location = New System.Drawing.Point(72, 8)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnImprimir.Size = New System.Drawing.Size(64, 21)
        Me.btnImprimir.TabIndex = 81
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.Text = "&Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = False
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGuardar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnGuardar.Location = New System.Drawing.Point(8, 8)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnGuardar.Size = New System.Drawing.Size(64, 21)
        Me.btnGuardar.TabIndex = 80
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminar.Location = New System.Drawing.Point(88, 407)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminar.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminar.TabIndex = 57
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "&Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnInsertar
        '
        Me.btnInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertar.Location = New System.Drawing.Point(9, 407)
        Me.btnInsertar.Name = "btnInsertar"
        Me.btnInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertar.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertar.TabIndex = 56
        Me.btnInsertar.TabStop = False
        Me.btnInsertar.Text = "Insertar"
        Me.btnInsertar.UseVisualStyleBackColor = False
        '
        'txtPrecio
        '
        Me.txtPrecio.AcceptsReturn = True
        Me.txtPrecio.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecio.Location = New System.Drawing.Point(708, 175)
        Me.txtPrecio.MaxLength = 0
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecio.Size = New System.Drawing.Size(100, 20)
        Me.txtPrecio.TabIndex = 54
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(8, 175)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(43, 20)
        Me.txtCantidad.TabIndex = 50
        Me.txtCantidad.Text = "1"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(627, 410)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.ReadOnly = True
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 62
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIdFactura
        '
        Me.txtIdFactura.AcceptsReturn = True
        Me.txtIdFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdFactura.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdFactura.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdFactura.Location = New System.Drawing.Point(102, 39)
        Me.txtIdFactura.MaxLength = 0
        Me.txtIdFactura.Name = "txtIdFactura"
        Me.txtIdFactura.ReadOnly = True
        Me.txtIdFactura.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdFactura.Size = New System.Drawing.Size(73, 20)
        Me.txtIdFactura.TabIndex = 0
        Me.txtIdFactura.TabStop = False
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(735, 410)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 63
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(527, 410)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 59
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(-3, 593)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(100, 19)
        Me._lblLabels_11.TabIndex = 44
        Me._lblLabels_11.Text = "Otras referencias:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_8.Location = New System.Drawing.Point(648, 155)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(60, 19)
        Me._lblLabels_8.TabIndex = 38
        Me._lblLabels_8.Text = "U/M"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_7.Location = New System.Drawing.Point(708, 155)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(99, 19)
        Me._lblLabels_7.TabIndex = 28
        Me._lblLabels_7.Text = "Precio"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(8, 155)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(43, 19)
        Me._lblLabels_6.TabIndex = 27
        Me._lblLabels_6.Text = "Cant"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(51, 155)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(252, 19)
        Me._lblLabels_1.TabIndex = 24
        Me._lblLabels_1.Text = "Código"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_LblImpuesto_0
        '
        Me._LblImpuesto_0.BackColor = System.Drawing.Color.Transparent
        Me._LblImpuesto_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblImpuesto_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblImpuesto_0.Location = New System.Drawing.Point(596, 410)
        Me._LblImpuesto_0.Name = "_LblImpuesto_0"
        Me._LblImpuesto_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblImpuesto_0.Size = New System.Drawing.Size(32, 19)
        Me._LblImpuesto_0.TabIndex = 23
        Me._LblImpuesto_0.Text = "Imp:"
        Me._LblImpuesto_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_LblTotal_6
        '
        Me._LblTotal_6.BackColor = System.Drawing.Color.Transparent
        Me._LblTotal_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblTotal_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblTotal_6.Location = New System.Drawing.Point(695, 410)
        Me._LblTotal_6.Name = "_LblTotal_6"
        Me._LblTotal_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblTotal_6.Size = New System.Drawing.Size(42, 19)
        Me._LblTotal_6.TabIndex = 21
        Me._LblTotal_6.Text = "Total:"
        Me._LblTotal_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblSubTotal_5
        '
        Me._lblSubTotal_5.BackColor = System.Drawing.Color.Transparent
        Me._lblSubTotal_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblSubTotal_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblSubTotal_5.Location = New System.Drawing.Point(463, 410)
        Me._lblSubTotal_5.Name = "_lblSubTotal_5"
        Me._lblSubTotal_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblSubTotal_5.Size = New System.Drawing.Size(65, 19)
        Me._lblSubTotal_5.TabIndex = 17
        Me._lblSubTotal_5.Text = "Sub-Total:"
        Me._lblSubTotal_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(227, 39)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(44, 19)
        Me._lblLabels_2.TabIndex = 14
        Me._lblLabels_2.Text = "Cliente:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(25, 40)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(71, 19)
        Me._lblLabels_0.TabIndex = 13
        Me._lblLabels_0.Text = "Factura No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleFactura
        '
        Me.grdDetalleFactura.AllowUserToAddRows = False
        Me.grdDetalleFactura.AllowUserToDeleteRows = False
        Me.grdDetalleFactura.AllowUserToResizeColumns = False
        Me.grdDetalleFactura.AllowUserToResizeRows = False
        Me.grdDetalleFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleFactura.Location = New System.Drawing.Point(8, 201)
        Me.grdDetalleFactura.MultiSelect = False
        Me.grdDetalleFactura.Name = "grdDetalleFactura"
        Me.grdDetalleFactura.ReadOnly = True
        Me.grdDetalleFactura.RowHeadersVisible = False
        Me.grdDetalleFactura.RowHeadersWidth = 30
        Me.grdDetalleFactura.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleFactura.TabIndex = 55
        Me.grdDetalleFactura.TabStop = False
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Image = CType(resources.GetObject("btnBuscarCliente.Image"), System.Drawing.Image)
        Me.btnBuscarCliente.Location = New System.Drawing.Point(615, 39)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarCliente.TabIndex = 4
        Me.btnBuscarCliente.TabStop = False
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(303, 175)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(345, 20)
        Me.txtDescripcion.TabIndex = 52
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(303, 155)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(345, 19)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "Descripción"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNombreCliente
        '
        Me.txtNombreCliente.AcceptsReturn = True
        Me.txtNombreCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreCliente.Location = New System.Drawing.Point(277, 39)
        Me.txtNombreCliente.MaxLength = 0
        Me.txtNombreCliente.Name = "txtNombreCliente"
        Me.txtNombreCliente.ReadOnly = True
        Me.txtNombreCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreCliente.Size = New System.Drawing.Size(332, 20)
        Me.txtNombreCliente.TabIndex = 3
        Me.txtNombreCliente.TabStop = False
        '
        'txtAutorizacion
        '
        Me.txtAutorizacion.AcceptsReturn = True
        Me.txtAutorizacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtAutorizacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAutorizacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAutorizacion.Location = New System.Drawing.Point(372, 460)
        Me.txtAutorizacion.MaxLength = 0
        Me.txtAutorizacion.Name = "txtAutorizacion"
        Me.txtAutorizacion.ReadOnly = True
        Me.txtAutorizacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAutorizacion.Size = New System.Drawing.Size(125, 20)
        Me.txtAutorizacion.TabIndex = 73
        '
        'lblAutorizacion
        '
        Me.lblAutorizacion.BackColor = System.Drawing.Color.Transparent
        Me.lblAutorizacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAutorizacion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAutorizacion.Location = New System.Drawing.Point(372, 440)
        Me.lblAutorizacion.Name = "lblAutorizacion"
        Me.lblAutorizacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAutorizacion.Size = New System.Drawing.Size(125, 19)
        Me.lblAutorizacion.TabIndex = 90
        Me.lblAutorizacion.Text = "Autorización"
        Me.lblAutorizacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTipoTarjeta
        '
        Me.txtTipoTarjeta.AcceptsReturn = True
        Me.txtTipoTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoTarjeta.Location = New System.Drawing.Point(302, 460)
        Me.txtTipoTarjeta.MaxLength = 0
        Me.txtTipoTarjeta.Name = "txtTipoTarjeta"
        Me.txtTipoTarjeta.ReadOnly = True
        Me.txtTipoTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoTarjeta.Size = New System.Drawing.Size(70, 20)
        Me.txtTipoTarjeta.TabIndex = 72
        '
        'lblTipoTarjeta
        '
        Me.lblTipoTarjeta.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoTarjeta.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTipoTarjeta.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTipoTarjeta.Location = New System.Drawing.Point(302, 440)
        Me.lblTipoTarjeta.Name = "lblTipoTarjeta"
        Me.lblTipoTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTipoTarjeta.Size = New System.Drawing.Size(70, 19)
        Me.lblTipoTarjeta.TabIndex = 89
        Me.lblTipoTarjeta.Text = "Tipo Tarjeta"
        Me.lblTipoTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTipoBanco
        '
        Me.cboTipoBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoBanco.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoBanco.Enabled = False
        Me.cboTipoBanco.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoBanco.IntegralHeight = False
        Me.cboTipoBanco.ItemHeight = 13
        Me.cboTipoBanco.Location = New System.Drawing.Point(179, 460)
        Me.cboTipoBanco.Name = "cboTipoBanco"
        Me.cboTipoBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoBanco.Size = New System.Drawing.Size(123, 21)
        Me.cboTipoBanco.TabIndex = 71
        '
        'lblBanco
        '
        Me.lblBanco.BackColor = System.Drawing.Color.Transparent
        Me.lblBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBanco.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBanco.Location = New System.Drawing.Point(179, 440)
        Me.lblBanco.Name = "lblBanco"
        Me.lblBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBanco.Size = New System.Drawing.Size(123, 19)
        Me.lblBanco.TabIndex = 88
        Me.lblBanco.Text = "Banco Adquiriente"
        Me.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTipoMoneda
        '
        Me.cboTipoMoneda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoMoneda.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoMoneda.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoMoneda.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoMoneda.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoMoneda.IntegralHeight = False
        Me.cboTipoMoneda.ItemHeight = 13
        Me.cboTipoMoneda.Location = New System.Drawing.Point(498, 460)
        Me.cboTipoMoneda.Name = "cboTipoMoneda"
        Me.cboTipoMoneda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoMoneda.Size = New System.Drawing.Size(129, 21)
        Me.cboTipoMoneda.TabIndex = 74
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(498, 440)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(129, 19)
        Me.Label2.TabIndex = 87
        Me.Label2.Text = "Moneda"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdDesglosePago
        '
        Me.grdDesglosePago.AllowUserToAddRows = False
        Me.grdDesglosePago.AllowUserToDeleteRows = False
        Me.grdDesglosePago.AllowUserToResizeColumns = False
        Me.grdDesglosePago.AllowUserToResizeRows = False
        Me.grdDesglosePago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDesglosePago.Location = New System.Drawing.Point(8, 487)
        Me.grdDesglosePago.MultiSelect = False
        Me.grdDesglosePago.Name = "grdDesglosePago"
        Me.grdDesglosePago.ReadOnly = True
        Me.grdDesglosePago.RowHeadersVisible = False
        Me.grdDesglosePago.RowHeadersWidth = 30
        Me.grdDesglosePago.Size = New System.Drawing.Size(800, 67)
        Me.grdDesglosePago.TabIndex = 77
        Me.grdDesglosePago.TabStop = False
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.AcceptsReturn = True
        Me.txtTipoCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoCambio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoCambio.Location = New System.Drawing.Point(627, 460)
        Me.txtTipoCambio.MaxLength = 0
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.ReadOnly = True
        Me.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoCambio.Size = New System.Drawing.Size(73, 20)
        Me.txtTipoCambio.TabIndex = 75
        Me.txtTipoCambio.TabStop = False
        Me.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnEliminarPago
        '
        Me.btnEliminarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminarPago.Location = New System.Drawing.Point(87, 560)
        Me.btnEliminarPago.Name = "btnEliminarPago"
        Me.btnEliminarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminarPago.TabIndex = 79
        Me.btnEliminarPago.TabStop = False
        Me.btnEliminarPago.Text = "&Eliminar"
        Me.btnEliminarPago.UseVisualStyleBackColor = False
        '
        'btnInsertarPago
        '
        Me.btnInsertarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertarPago.Location = New System.Drawing.Point(7, 560)
        Me.btnInsertarPago.Name = "btnInsertarPago"
        Me.btnInsertarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertarPago.TabIndex = 78
        Me.btnInsertarPago.TabStop = False
        Me.btnInsertarPago.Text = "Insertar"
        Me.btnInsertarPago.UseVisualStyleBackColor = False
        '
        'txtMontoPago
        '
        Me.txtMontoPago.AcceptsReturn = True
        Me.txtMontoPago.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoPago.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMontoPago.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMontoPago.Location = New System.Drawing.Point(699, 460)
        Me.txtMontoPago.MaxLength = 0
        Me.txtMontoPago.Name = "txtMontoPago"
        Me.txtMontoPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMontoPago.Size = New System.Drawing.Size(109, 20)
        Me.txtMontoPago.TabIndex = 76
        Me.txtMontoPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboFormaPago
        '
        Me.cboFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFormaPago.BackColor = System.Drawing.SystemColors.Window
        Me.cboFormaPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormaPago.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFormaPago.IntegralHeight = False
        Me.cboFormaPago.ItemHeight = 13
        Me.cboFormaPago.Location = New System.Drawing.Point(8, 460)
        Me.cboFormaPago.Name = "cboFormaPago"
        Me.cboFormaPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFormaPago.Size = New System.Drawing.Size(171, 21)
        Me.cboFormaPago.TabIndex = 70
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(699, 440)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(109, 19)
        Me.Label4.TabIndex = 85
        Me.Label4.Text = "Monto"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 440)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(171, 19)
        Me.Label5.TabIndex = 84
        Me.Label5.Text = "Forma de Pago"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(624, 440)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(76, 19)
        Me.Label9.TabIndex = 86
        Me.Label9.Text = "Tipo Cambio"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSaldoPorPagar
        '
        Me.txtSaldoPorPagar.AcceptsReturn = True
        Me.txtSaldoPorPagar.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoPorPagar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoPorPagar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoPorPagar.Location = New System.Drawing.Point(734, 563)
        Me.txtSaldoPorPagar.MaxLength = 0
        Me.txtSaldoPorPagar.Name = "txtSaldoPorPagar"
        Me.txtSaldoPorPagar.ReadOnly = True
        Me.txtSaldoPorPagar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoPorPagar.Size = New System.Drawing.Size(73, 20)
        Me.txtSaldoPorPagar.TabIndex = 80
        Me.txtSaldoPorPagar.TabStop = False
        Me.txtSaldoPorPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(637, 563)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(91, 17)
        Me.Label10.TabIndex = 92
        Me.Label10.Text = "Saldo por Pagar:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(51, 175)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(252, 20)
        Me.txtCodigo.TabIndex = 51
        '
        'btnOrdenServicio
        '
        Me.btnOrdenServicio.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnOrdenServicio.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnOrdenServicio.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOrdenServicio.Location = New System.Drawing.Point(710, 38)
        Me.btnOrdenServicio.Name = "btnOrdenServicio"
        Me.btnOrdenServicio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnOrdenServicio.Size = New System.Drawing.Size(96, 21)
        Me.btnOrdenServicio.TabIndex = 6
        Me.btnOrdenServicio.TabStop = False
        Me.btnOrdenServicio.Text = "Cargar Orden"
        Me.btnOrdenServicio.UseVisualStyleBackColor = False
        Me.btnOrdenServicio.Visible = False
        '
        'txtIdOrdenServicio
        '
        Me.txtIdOrdenServicio.AcceptsReturn = True
        Me.txtIdOrdenServicio.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdOrdenServicio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdOrdenServicio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdOrdenServicio.Location = New System.Drawing.Point(653, 39)
        Me.txtIdOrdenServicio.MaxLength = 0
        Me.txtIdOrdenServicio.Name = "txtIdOrdenServicio"
        Me.txtIdOrdenServicio.ReadOnly = True
        Me.txtIdOrdenServicio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdOrdenServicio.Size = New System.Drawing.Size(54, 20)
        Me.txtIdOrdenServicio.TabIndex = 5
        Me.txtIdOrdenServicio.TabStop = False
        Me.txtIdOrdenServicio.Visible = False
        '
        'txtVendedor
        '
        Me.txtVendedor.AcceptsReturn = True
        Me.txtVendedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtVendedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVendedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVendedor.Location = New System.Drawing.Point(537, 9)
        Me.txtVendedor.MaxLength = 0
        Me.txtVendedor.Name = "txtVendedor"
        Me.txtVendedor.ReadOnly = True
        Me.txtVendedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendedor.Size = New System.Drawing.Size(250, 20)
        Me.txtVendedor.TabIndex = 1
        Me.txtVendedor.TabStop = False
        '
        'btnBuscaVendedor
        '
        Me.btnBuscaVendedor.Image = CType(resources.GetObject("btnBuscaVendedor.Image"), System.Drawing.Image)
        Me.btnBuscaVendedor.Location = New System.Drawing.Point(786, 8)
        Me.btnBuscaVendedor.Name = "btnBuscaVendedor"
        Me.btnBuscaVendedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscaVendedor.TabIndex = 2
        Me.btnBuscaVendedor.TabStop = False
        Me.btnBuscaVendedor.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(466, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(65, 19)
        Me.Label11.TabIndex = 96
        Me.Label11.Text = "Vendedor:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtIdProforma
        '
        Me.txtIdProforma.AcceptsReturn = True
        Me.txtIdProforma.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdProforma.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdProforma.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdProforma.Location = New System.Drawing.Point(653, 64)
        Me.txtIdProforma.MaxLength = 0
        Me.txtIdProforma.Name = "txtIdProforma"
        Me.txtIdProforma.ReadOnly = True
        Me.txtIdProforma.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdProforma.Size = New System.Drawing.Size(54, 20)
        Me.txtIdProforma.TabIndex = 9
        Me.txtIdProforma.TabStop = False
        Me.txtIdProforma.Visible = False
        '
        'btnProforma
        '
        Me.btnProforma.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnProforma.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnProforma.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnProforma.Location = New System.Drawing.Point(710, 64)
        Me.btnProforma.Name = "btnProforma"
        Me.btnProforma.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnProforma.Size = New System.Drawing.Size(96, 21)
        Me.btnProforma.TabIndex = 10
        Me.btnProforma.TabStop = False
        Me.btnProforma.Text = "Cargar Proforma"
        Me.btnProforma.UseVisualStyleBackColor = False
        Me.btnProforma.Visible = False
        '
        'txtPagoDelCliente
        '
        Me.txtPagoDelCliente.AcceptsReturn = True
        Me.txtPagoDelCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtPagoDelCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPagoDelCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPagoDelCliente.Location = New System.Drawing.Point(291, 410)
        Me.txtPagoDelCliente.MaxLength = 0
        Me.txtPagoDelCliente.Name = "txtPagoDelCliente"
        Me.txtPagoDelCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPagoDelCliente.Size = New System.Drawing.Size(73, 20)
        Me.txtPagoDelCliente.TabIndex = 64
        Me.txtPagoDelCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(246, 410)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(46, 17)
        Me.Label3.TabIndex = 98
        Me.Label3.Text = "Pago:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCambio
        '
        Me.txtCambio.AcceptsReturn = True
        Me.txtCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtCambio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCambio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCambio.Location = New System.Drawing.Point(408, 410)
        Me.txtCambio.MaxLength = 0
        Me.txtCambio.Name = "txtCambio"
        Me.txtCambio.ReadOnly = True
        Me.txtCambio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCambio.Size = New System.Drawing.Size(64, 20)
        Me.txtCambio.TabIndex = 65
        Me.txtCambio.TabStop = False
        Me.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(365, 411)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(45, 17)
        Me.Label6.TabIndex = 100
        Me.Label6.Text = "Cambio:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCondicionVenta
        '
        Me.cboCondicionVenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCondicionVenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCondicionVenta.BackColor = System.Drawing.SystemColors.Window
        Me.cboCondicionVenta.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCondicionVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondicionVenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCondicionVenta.IntegralHeight = False
        Me.cboCondicionVenta.ItemHeight = 13
        Me.cboCondicionVenta.Location = New System.Drawing.Point(277, 65)
        Me.cboCondicionVenta.Name = "cboCondicionVenta"
        Me.cboCondicionVenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCondicionVenta.Size = New System.Drawing.Size(238, 21)
        Me.cboCondicionVenta.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(182, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(89, 19)
        Me.Label7.TabIndex = 102
        Me.Label7.Text = "Condición venta:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlazoCredito
        '
        Me.txtPlazoCredito.AcceptsReturn = True
        Me.txtPlazoCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlazoCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlazoCredito.Enabled = False
        Me.txtPlazoCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlazoCredito.Location = New System.Drawing.Point(605, 64)
        Me.txtPlazoCredito.MaxLength = 300
        Me.txtPlazoCredito.Name = "txtPlazoCredito"
        Me.txtPlazoCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlazoCredito.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlazoCredito.Size = New System.Drawing.Size(42, 20)
        Me.txtPlazoCredito.TabIndex = 12
        Me.txtPlazoCredito.TabStop = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(521, 65)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(78, 19)
        Me.Label12.TabIndex = 104
        Me.Label12.Text = "Plazo crédito:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGenerarPDF
        '
        Me.btnGenerarPDF.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnGenerarPDF.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGenerarPDF.Enabled = False
        Me.btnGenerarPDF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnGenerarPDF.Location = New System.Drawing.Point(328, 8)
        Me.btnGenerarPDF.Name = "btnGenerarPDF"
        Me.btnGenerarPDF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnGenerarPDF.Size = New System.Drawing.Size(64, 21)
        Me.btnGenerarPDF.TabIndex = 105
        Me.btnGenerarPDF.TabStop = False
        Me.btnGenerarPDF.Text = "A&brir PDF"
        Me.btnGenerarPDF.UseVisualStyleBackColor = False
        '
        'gpbExoneracion
        '
        Me.gpbExoneracion.Controls.Add(Me.txtFechaExoneracion)
        Me.gpbExoneracion.Controls.Add(Me.Label16)
        Me.gpbExoneracion.Controls.Add(Me.cboTipoExoneracion)
        Me.gpbExoneracion.Controls.Add(Me.txtPorcentajeExoneracion)
        Me.gpbExoneracion.Controls.Add(Me.Label14)
        Me.gpbExoneracion.Controls.Add(Me.Label13)
        Me.gpbExoneracion.Controls.Add(Me.txtNombreInstExoneracion)
        Me.gpbExoneracion.Controls.Add(Me.Label8)
        Me.gpbExoneracion.Controls.Add(Me.txtNumDocExoneracion)
        Me.gpbExoneracion.Controls.Add(Me._lblLabels_3)
        Me.gpbExoneracion.Enabled = False
        Me.gpbExoneracion.Location = New System.Drawing.Point(109, 91)
        Me.gpbExoneracion.Name = "gpbExoneracion"
        Me.gpbExoneracion.Size = New System.Drawing.Size(699, 65)
        Me.gpbExoneracion.TabIndex = 13
        Me.gpbExoneracion.TabStop = False
        Me.gpbExoneracion.Text = "Registro de Exoneración"
        '
        'txtFechaExoneracion
        '
        Me.txtFechaExoneracion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFechaExoneracion.Location = New System.Drawing.Point(575, 38)
        Me.txtFechaExoneracion.Name = "txtFechaExoneracion"
        Me.txtFechaExoneracion.Size = New System.Drawing.Size(84, 20)
        Me.txtFechaExoneracion.TabIndex = 16
        Me.txtFechaExoneracion.Value = New Date(2013, 6, 9, 0, 0, 0, 0)
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(267, 19)
        Me.Label16.TabIndex = 117
        Me.Label16.Text = "Tipo exoneración"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTipoExoneracion
        '
        Me.cboTipoExoneracion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoExoneracion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoExoneracion.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoExoneracion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoExoneracion.IntegralHeight = False
        Me.cboTipoExoneracion.ItemHeight = 13
        Me.cboTipoExoneracion.Location = New System.Drawing.Point(6, 38)
        Me.cboTipoExoneracion.Name = "cboTipoExoneracion"
        Me.cboTipoExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoExoneracion.Size = New System.Drawing.Size(267, 21)
        Me.cboTipoExoneracion.TabIndex = 116
        '
        'txtPorcentajeExoneracion
        '
        Me.txtPorcentajeExoneracion.AcceptsReturn = True
        Me.txtPorcentajeExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentajeExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentajeExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentajeExoneracion.Location = New System.Drawing.Point(665, 38)
        Me.txtPorcentajeExoneracion.MaxLength = 0
        Me.txtPorcentajeExoneracion.Name = "txtPorcentajeExoneracion"
        Me.txtPorcentajeExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentajeExoneracion.Size = New System.Drawing.Size(26, 20)
        Me.txtPorcentajeExoneracion.TabIndex = 17
        Me.txtPorcentajeExoneracion.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(665, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(26, 19)
        Me.Label14.TabIndex = 115
        Me.Label14.Text = "%"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(575, 17)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(70, 19)
        Me.Label13.TabIndex = 113
        Me.Label13.Text = "Fecha"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNombreInstExoneracion
        '
        Me.txtNombreInstExoneracion.AcceptsReturn = True
        Me.txtNombreInstExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreInstExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreInstExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreInstExoneracion.Location = New System.Drawing.Point(388, 39)
        Me.txtNombreInstExoneracion.MaxLength = 0
        Me.txtNombreInstExoneracion.Name = "txtNombreInstExoneracion"
        Me.txtNombreInstExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreInstExoneracion.Size = New System.Drawing.Size(181, 20)
        Me.txtNombreInstExoneracion.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(388, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(181, 19)
        Me.Label8.TabIndex = 111
        Me.Label8.Text = "Institución"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNumDocExoneracion
        '
        Me.txtNumDocExoneracion.AcceptsReturn = True
        Me.txtNumDocExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumDocExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumDocExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumDocExoneracion.Location = New System.Drawing.Point(279, 39)
        Me.txtNumDocExoneracion.MaxLength = 0
        Me.txtNumDocExoneracion.Name = "txtNumDocExoneracion"
        Me.txtNumDocExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumDocExoneracion.Size = New System.Drawing.Size(103, 20)
        Me.txtNumDocExoneracion.TabIndex = 14
        Me.txtNumDocExoneracion.TabStop = False
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(279, 17)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(103, 19)
        Me._lblLabels_3.TabIndex = 109
        Me._lblLabels_3.Text = "Nro. Documento"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(102, 65)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 4
        Me.txtFecha.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(41, 65)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(55, 19)
        Me.Label15.TabIndex = 111
        Me.Label15.Text = "Fecha:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmFactura
        '
        Me.AcceptButton = Me.btnInsertar
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(817, 646)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.gpbExoneracion)
        Me.Controls.Add(Me.btnGenerarPDF)
        Me.Controls.Add(Me.txtPlazoCredito)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboCondicionVenta)
        Me.Controls.Add(Me.txtCambio)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPagoDelCliente)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtIdProforma)
        Me.Controls.Add(Me.btnProforma)
        Me.Controls.Add(Me.txtVendedor)
        Me.Controls.Add(Me.btnBuscaVendedor)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtIdOrdenServicio)
        Me.Controls.Add(Me.btnOrdenServicio)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.txtSaldoPorPagar)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtAutorizacion)
        Me.Controls.Add(Me.lblAutorizacion)
        Me.Controls.Add(Me.txtTipoTarjeta)
        Me.Controls.Add(Me.lblTipoTarjeta)
        Me.Controls.Add(Me.cboTipoBanco)
        Me.Controls.Add(Me.lblBanco)
        Me.Controls.Add(Me.cboTipoMoneda)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdDesglosePago)
        Me.Controls.Add(Me.txtTipoCambio)
        Me.Controls.Add(Me.btnEliminarPago)
        Me.Controls.Add(Me.btnInsertarPago)
        Me.Controls.Add(Me.txtMontoPago)
        Me.Controls.Add(Me.cboFormaPago)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtNombreCliente)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.grdDetalleFactura)
        Me.Controls.Add(Me.txtDocumento)
        Me.Controls.Add(Me.txtUnidad)
        Me.Controls.Add(Me.btnBusProd)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnInsertar)
        Me.Controls.Add(Me.txtPrecio)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.txtImpuesto)
        Me.Controls.Add(Me.txtIdFactura)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me._lblLabels_8)
        Me.Controls.Add(Me._lblLabels_7)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me._LblImpuesto_0)
        Me.Controls.Add(Me._LblTotal_6)
        Me.Controls.Add(Me._lblSubTotal_5)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFactura"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Facturación"
        CType(Me.grdDetalleFactura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpbExoneracion.ResumeLayout(False)
        Me.gpbExoneracion.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtNombreCliente As System.Windows.Forms.TextBox
    Public WithEvents txtAutorizacion As System.Windows.Forms.TextBox
    Public WithEvents lblAutorizacion As System.Windows.Forms.Label
    Public WithEvents txtTipoTarjeta As System.Windows.Forms.TextBox
    Public WithEvents lblTipoTarjeta As System.Windows.Forms.Label
    Public WithEvents cboTipoBanco As System.Windows.Forms.ComboBox
    Public WithEvents lblBanco As System.Windows.Forms.Label
    Public WithEvents cboTipoMoneda As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents grdDesglosePago As System.Windows.Forms.DataGridView
    Public WithEvents txtTipoCambio As System.Windows.Forms.TextBox
    Public WithEvents btnEliminarPago As System.Windows.Forms.Button
    Public WithEvents btnInsertarPago As System.Windows.Forms.Button
    Public WithEvents txtMontoPago As System.Windows.Forms.TextBox
    Public WithEvents cboFormaPago As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents txtSaldoPorPagar As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents btnOrdenServicio As System.Windows.Forms.Button
    Public WithEvents txtIdOrdenServicio As System.Windows.Forms.TextBox
    Public WithEvents txtVendedor As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscaVendedor As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents txtIdProforma As System.Windows.Forms.TextBox
    Public WithEvents btnProforma As System.Windows.Forms.Button
    Public WithEvents txtPagoDelCliente As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtCambio As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents cboCondicionVenta As ComboBox
    Public WithEvents Label7 As Label
    Public WithEvents txtPlazoCredito As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents btnGenerarPDF As Button
    Friend WithEvents gpbExoneracion As GroupBox
    Public WithEvents txtPorcentajeExoneracion As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents Label13 As Label
    Public WithEvents txtNombreInstExoneracion As TextBox
    Public WithEvents Label8 As Label
    Public WithEvents txtNumDocExoneracion As TextBox
    Public WithEvents _lblLabels_3 As Label
    Public WithEvents txtFecha As TextBox
    Public WithEvents Label15 As Label
    Public WithEvents Label16 As Label
    Public WithEvents cboTipoExoneracion As ComboBox
    Friend WithEvents txtFechaExoneracion As DateTimePicker
End Class