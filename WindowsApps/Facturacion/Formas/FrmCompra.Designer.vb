<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCompra
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
    Public WithEvents btnBusProd As System.Windows.Forms.Button
    Public WithEvents btnAnular As System.Windows.Forms.Button
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnBuscar As System.Windows.Forms.Button
    Public WithEvents btnImprimir As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents btnEliminar As System.Windows.Forms.Button
    Public WithEvents btnInsertar As System.Windows.Forms.Button
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents txtImpuesto As System.Windows.Forms.TextBox
    Public WithEvents txtIdCompra As System.Windows.Forms.TextBox
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents txtDescuento As System.Windows.Forms.TextBox
    Public WithEvents txtFactura As System.Windows.Forms.TextBox
    Public WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents lblLabel6 As System.Windows.Forms.Label
    Public WithEvents lblLabel1 As System.Windows.Forms.Label
    Public WithEvents LblImpuesto As System.Windows.Forms.Label
    Public WithEvents LblTotal As System.Windows.Forms.Label
    Public WithEvents LblDescuento As System.Windows.Forms.Label
    Public WithEvents lblSubTotal As System.Windows.Forms.Label
    Public WithEvents lblLabel4 As System.Windows.Forms.Label
    Public WithEvents lblLabel3 As System.Windows.Forms.Label
    Public WithEvents lblLabel2 As System.Windows.Forms.Label
    Public WithEvents lblLabel0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCompra))
        Me.btnBusProd = New System.Windows.Forms.Button()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me.txtIdCompra = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.txtFactura = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.lblLabel6 = New System.Windows.Forms.Label()
        Me.lblLabel1 = New System.Windows.Forms.Label()
        Me.LblImpuesto = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.LblDescuento = New System.Windows.Forms.Label()
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel2 = New System.Windows.Forms.Label()
        Me.lblLabel0 = New System.Windows.Forms.Label()
        Me.grdDetalleCompra = New System.Windows.Forms.DataGridView()
        Me.txtPrecioCosto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCuentaBanco = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grdDesglosePago = New System.Windows.Forms.DataGridView()
        Me.btnEliminarPago = New System.Windows.Forms.Button()
        Me.btnInsertarPago = New System.Windows.Forms.Button()
        Me.txtMontoPago = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSaldoPorPagar = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCodigoProveedor = New System.Windows.Forms.TextBox()
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.txtIdOrdenCompra = New System.Windows.Forms.TextBox()
        Me.btnOrdenCompra = New System.Windows.Forms.Button()
        Me.txtPlazoCredito = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboCondicionVenta = New System.Windows.Forms.ComboBox()
        Me.txtReferencia = New System.Windows.Forms.TextBox()
        Me.lblAutorizacion = New System.Windows.Forms.Label()
        Me.cboFormaPago = New System.Windows.Forms.ComboBox()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPrecioVenta = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtUtilidad = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtExistencias = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnGenerarPDF = New System.Windows.Forms.Button()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        CType(Me.grdDetalleCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBusProd
        '
        Me.btnBusProd.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBusProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBusProd.Enabled = False
        Me.btnBusProd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBusProd.Location = New System.Drawing.Point(168, 371)
        Me.btnBusProd.Name = "btnBusProd"
        Me.btnBusProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBusProd.Size = New System.Drawing.Size(73, 25)
        Me.btnBusProd.TabIndex = 27
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
        Me.btnAnular.TabIndex = 0
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
        Me.btnAgregar.TabIndex = 0
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
        Me.btnBuscar.TabIndex = 0
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
        Me.btnImprimir.TabIndex = 0
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
        Me.btnGuardar.TabIndex = 0
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminar.Location = New System.Drawing.Point(88, 371)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminar.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminar.TabIndex = 26
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "&Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnInsertar
        '
        Me.btnInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertar.Location = New System.Drawing.Point(8, 371)
        Me.btnInsertar.Name = "btnInsertar"
        Me.btnInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertar.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertar.TabIndex = 25
        Me.btnInsertar.TabStop = False
        Me.btnInsertar.Text = "Insertar"
        Me.btnInsertar.UseVisualStyleBackColor = False
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(679, 142)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(41, 20)
        Me.txtCantidad.TabIndex = 24
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(744, 373)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 30
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIdCompra
        '
        Me.txtIdCompra.AcceptsReturn = True
        Me.txtIdCompra.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCompra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCompra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCompra.Location = New System.Drawing.Point(108, 40)
        Me.txtIdCompra.MaxLength = 0
        Me.txtIdCompra.Name = "txtIdCompra"
        Me.txtIdCompra.ReadOnly = True
        Me.txtIdCompra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdCompra.Size = New System.Drawing.Size(73, 20)
        Me.txtIdCompra.TabIndex = 0
        Me.txtIdCompra.TabStop = False
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(869, 374)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 31
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescuento
        '
        Me.txtDescuento.AcceptsReturn = True
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescuento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescuento.Location = New System.Drawing.Point(600, 373)
        Me.txtDescuento.MaxLength = 0
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescuento.Size = New System.Drawing.Size(73, 20)
        Me.txtDescuento.TabIndex = 29
        Me.txtDescuento.TabStop = False
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFactura
        '
        Me.txtFactura.AcceptsReturn = True
        Me.txtFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtFactura.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFactura.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFactura.Location = New System.Drawing.Point(256, 66)
        Me.txtFactura.MaxLength = 20
        Me.txtFactura.Name = "txtFactura"
        Me.txtFactura.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFactura.Size = New System.Drawing.Size(141, 20)
        Me.txtFactura.TabIndex = 4
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(450, 373)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 28
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(108, 66)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 3
        Me.txtFecha.TabStop = False
        '
        'lblLabel6
        '
        Me.lblLabel6.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel6.Location = New System.Drawing.Point(679, 122)
        Me.lblLabel6.Name = "lblLabel6"
        Me.lblLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel6.Size = New System.Drawing.Size(41, 19)
        Me.lblLabel6.TabIndex = 27
        Me.lblLabel6.Text = "Cant"
        Me.lblLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLabel1
        '
        Me.lblLabel1.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel1.Location = New System.Drawing.Point(8, 122)
        Me.lblLabel1.Name = "lblLabel1"
        Me.lblLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel1.Size = New System.Drawing.Size(135, 19)
        Me.lblLabel1.TabIndex = 24
        Me.lblLabel1.Text = "Código"
        Me.lblLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblImpuesto
        '
        Me.LblImpuesto.BackColor = System.Drawing.Color.Transparent
        Me.LblImpuesto.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblImpuesto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblImpuesto.Location = New System.Drawing.Point(684, 373)
        Me.LblImpuesto.Name = "LblImpuesto"
        Me.LblImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblImpuesto.Size = New System.Drawing.Size(53, 19)
        Me.LblImpuesto.TabIndex = 23
        Me.LblImpuesto.Text = "Impuesto:"
        Me.LblImpuesto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotal
        '
        Me.LblTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTotal.Location = New System.Drawing.Point(828, 374)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(34, 19)
        Me.LblTotal.TabIndex = 19
        Me.LblTotal.Text = "Total:"
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblDescuento
        '
        Me.LblDescuento.BackColor = System.Drawing.Color.Transparent
        Me.LblDescuento.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDescuento.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDescuento.Location = New System.Drawing.Point(528, 373)
        Me.LblDescuento.Name = "LblDescuento"
        Me.LblDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDescuento.Size = New System.Drawing.Size(65, 19)
        Me.LblDescuento.TabIndex = 18
        Me.LblDescuento.Text = "Descuento:"
        Me.LblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubTotal
        '
        Me.lblSubTotal.BackColor = System.Drawing.Color.Transparent
        Me.lblSubTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSubTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSubTotal.Location = New System.Drawing.Point(379, 373)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSubTotal.Size = New System.Drawing.Size(65, 19)
        Me.lblSubTotal.TabIndex = 14
        Me.lblSubTotal.Text = "Sub-Total:"
        Me.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(180, 66)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(70, 18)
        Me.lblLabel4.TabIndex = 13
        Me.lblLabel4.Text = "Factura No:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(45, 66)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel3.Size = New System.Drawing.Size(57, 19)
        Me.lblLabel3.TabIndex = 12
        Me.lblLabel3.Text = "Fecha:"
        Me.lblLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel2
        '
        Me.lblLabel2.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel2.Location = New System.Drawing.Point(187, 40)
        Me.lblLabel2.Name = "lblLabel2"
        Me.lblLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel2.Size = New System.Drawing.Size(63, 19)
        Me.lblLabel2.TabIndex = 11
        Me.lblLabel2.Text = "Proveedor:"
        Me.lblLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel0
        '
        Me.lblLabel0.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel0.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel0.Location = New System.Drawing.Point(28, 40)
        Me.lblLabel0.Name = "lblLabel0"
        Me.lblLabel0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel0.Size = New System.Drawing.Size(74, 19)
        Me.lblLabel0.TabIndex = 10
        Me.lblLabel0.Text = "Compra No:"
        Me.lblLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleCompra
        '
        Me.grdDetalleCompra.AllowUserToAddRows = False
        Me.grdDetalleCompra.AllowUserToDeleteRows = False
        Me.grdDetalleCompra.AllowUserToResizeColumns = False
        Me.grdDetalleCompra.AllowUserToResizeRows = False
        Me.grdDetalleCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleCompra.Location = New System.Drawing.Point(8, 168)
        Me.grdDetalleCompra.MultiSelect = False
        Me.grdDetalleCompra.Name = "grdDetalleCompra"
        Me.grdDetalleCompra.ReadOnly = True
        Me.grdDetalleCompra.RowHeadersVisible = False
        Me.grdDetalleCompra.Size = New System.Drawing.Size(934, 200)
        Me.grdDetalleCompra.TabIndex = 24
        Me.grdDetalleCompra.TabStop = False
        '
        'txtPrecioCosto
        '
        Me.txtPrecioCosto.AcceptsReturn = True
        Me.txtPrecioCosto.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioCosto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioCosto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioCosto.Location = New System.Drawing.Point(720, 142)
        Me.txtPrecioCosto.MaxLength = 0
        Me.txtPrecioCosto.Name = "txtPrecioCosto"
        Me.txtPrecioCosto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioCosto.Size = New System.Drawing.Size(88, 20)
        Me.txtPrecioCosto.TabIndex = 25
        Me.txtPrecioCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(721, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(87, 19)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Precio Costo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(278, 142)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(351, 20)
        Me.txtDescripcion.TabIndex = 22
        Me.txtDescripcion.TabStop = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(278, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(401, 19)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Descripción"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCuentaBanco
        '
        Me.cboCuentaBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCuentaBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCuentaBanco.BackColor = System.Drawing.SystemColors.Window
        Me.cboCuentaBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCuentaBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCuentaBanco.Enabled = False
        Me.cboCuentaBanco.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCuentaBanco.IntegralHeight = False
        Me.cboCuentaBanco.ItemHeight = 13
        Me.cboCuentaBanco.Location = New System.Drawing.Point(180, 425)
        Me.cboCuentaBanco.Name = "cboCuentaBanco"
        Me.cboCuentaBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCuentaBanco.Size = New System.Drawing.Size(528, 21)
        Me.cboCuentaBanco.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(180, 405)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(528, 19)
        Me.Label3.TabIndex = 126
        Me.Label3.Text = "Cuenta Bancaria"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdDesglosePago
        '
        Me.grdDesglosePago.AllowUserToAddRows = False
        Me.grdDesglosePago.AllowUserToDeleteRows = False
        Me.grdDesglosePago.AllowUserToResizeColumns = False
        Me.grdDesglosePago.AllowUserToResizeRows = False
        Me.grdDesglosePago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDesglosePago.Location = New System.Drawing.Point(8, 452)
        Me.grdDesglosePago.MultiSelect = False
        Me.grdDesglosePago.Name = "grdDesglosePago"
        Me.grdDesglosePago.ReadOnly = True
        Me.grdDesglosePago.RowHeadersVisible = False
        Me.grdDesglosePago.RowHeadersWidth = 30
        Me.grdDesglosePago.Size = New System.Drawing.Size(934, 89)
        Me.grdDesglosePago.TabIndex = 46
        Me.grdDesglosePago.TabStop = False
        '
        'btnEliminarPago
        '
        Me.btnEliminarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminarPago.Location = New System.Drawing.Point(88, 547)
        Me.btnEliminarPago.Name = "btnEliminarPago"
        Me.btnEliminarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminarPago.TabIndex = 48
        Me.btnEliminarPago.TabStop = False
        Me.btnEliminarPago.Text = "&Eliminar"
        Me.btnEliminarPago.UseVisualStyleBackColor = False
        '
        'btnInsertarPago
        '
        Me.btnInsertarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertarPago.Location = New System.Drawing.Point(8, 547)
        Me.btnInsertarPago.Name = "btnInsertarPago"
        Me.btnInsertarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertarPago.TabIndex = 47
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
        Me.txtMontoPago.Location = New System.Drawing.Point(833, 425)
        Me.txtMontoPago.MaxLength = 0
        Me.txtMontoPago.Name = "txtMontoPago"
        Me.txtMontoPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMontoPago.Size = New System.Drawing.Size(109, 20)
        Me.txtMontoPago.TabIndex = 45
        Me.txtMontoPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(833, 405)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(109, 19)
        Me.Label5.TabIndex = 123
        Me.Label5.Text = "Monto"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(8, 405)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(100, 19)
        Me.Label8.TabIndex = 122
        Me.Label8.Text = "Forma de Pago"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSaldoPorPagar
        '
        Me.txtSaldoPorPagar.AcceptsReturn = True
        Me.txtSaldoPorPagar.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoPorPagar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoPorPagar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoPorPagar.Location = New System.Drawing.Point(869, 547)
        Me.txtSaldoPorPagar.MaxLength = 0
        Me.txtSaldoPorPagar.Name = "txtSaldoPorPagar"
        Me.txtSaldoPorPagar.ReadOnly = True
        Me.txtSaldoPorPagar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoPorPagar.Size = New System.Drawing.Size(73, 20)
        Me.txtSaldoPorPagar.TabIndex = 49
        Me.txtSaldoPorPagar.TabStop = False
        Me.txtSaldoPorPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(771, 547)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(91, 19)
        Me.Label10.TabIndex = 132
        Me.Label10.Text = "Saldo por Pagar:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigoProveedor
        '
        Me.txtCodigoProveedor.Location = New System.Drawing.Point(8, 142)
        Me.txtCodigoProveedor.Name = "txtCodigoProveedor"
        Me.txtCodigoProveedor.Size = New System.Drawing.Size(135, 20)
        Me.txtCodigoProveedor.TabIndex = 20
        '
        'txtProveedor
        '
        Me.txtProveedor.AcceptsReturn = True
        Me.txtProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProveedor.Location = New System.Drawing.Point(256, 40)
        Me.txtProveedor.MaxLength = 0
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProveedor.Size = New System.Drawing.Size(386, 20)
        Me.txtProveedor.TabIndex = 1
        Me.txtProveedor.TabStop = False
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Image = CType(resources.GetObject("btnBuscarProveedor.Image"), System.Drawing.Image)
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(643, 39)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarProveedor.TabIndex = 2
        Me.btnBuscarProveedor.TabStop = False
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'txtIdOrdenCompra
        '
        Me.txtIdOrdenCompra.AcceptsReturn = True
        Me.txtIdOrdenCompra.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdOrdenCompra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdOrdenCompra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdOrdenCompra.Location = New System.Drawing.Point(800, 40)
        Me.txtIdOrdenCompra.MaxLength = 0
        Me.txtIdOrdenCompra.Name = "txtIdOrdenCompra"
        Me.txtIdOrdenCompra.ReadOnly = True
        Me.txtIdOrdenCompra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdOrdenCompra.Size = New System.Drawing.Size(54, 20)
        Me.txtIdOrdenCompra.TabIndex = 5
        Me.txtIdOrdenCompra.TabStop = False
        Me.txtIdOrdenCompra.Visible = False
        '
        'btnOrdenCompra
        '
        Me.btnOrdenCompra.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnOrdenCompra.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnOrdenCompra.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOrdenCompra.Location = New System.Drawing.Point(860, 39)
        Me.btnOrdenCompra.Name = "btnOrdenCompra"
        Me.btnOrdenCompra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnOrdenCompra.Size = New System.Drawing.Size(80, 21)
        Me.btnOrdenCompra.TabIndex = 6
        Me.btnOrdenCompra.TabStop = False
        Me.btnOrdenCompra.Text = "Cargar Orden"
        Me.btnOrdenCompra.UseVisualStyleBackColor = False
        Me.btnOrdenCompra.Visible = False
        '
        'txtPlazoCredito
        '
        Me.txtPlazoCredito.AcceptsReturn = True
        Me.txtPlazoCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlazoCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlazoCredito.Enabled = False
        Me.txtPlazoCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlazoCredito.Location = New System.Drawing.Point(739, 66)
        Me.txtPlazoCredito.MaxLength = 300
        Me.txtPlazoCredito.Name = "txtPlazoCredito"
        Me.txtPlazoCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlazoCredito.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlazoCredito.Size = New System.Drawing.Size(42, 20)
        Me.txtPlazoCredito.TabIndex = 8
        Me.txtPlazoCredito.TabStop = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(646, 66)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(87, 19)
        Me.Label12.TabIndex = 140
        Me.Label12.Text = "Plazo de crédito:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(403, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(89, 19)
        Me.Label11.TabIndex = 139
        Me.Label11.Text = "Condición venta:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.cboCondicionVenta.Location = New System.Drawing.Point(498, 66)
        Me.cboCondicionVenta.Name = "cboCondicionVenta"
        Me.cboCondicionVenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCondicionVenta.Size = New System.Drawing.Size(129, 21)
        Me.cboCondicionVenta.TabIndex = 7
        '
        'txtReferencia
        '
        Me.txtReferencia.AcceptsReturn = True
        Me.txtReferencia.BackColor = System.Drawing.SystemColors.Window
        Me.txtReferencia.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReferencia.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReferencia.Location = New System.Drawing.Point(708, 425)
        Me.txtReferencia.MaxLength = 0
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.ReadOnly = True
        Me.txtReferencia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReferencia.Size = New System.Drawing.Size(125, 20)
        Me.txtReferencia.TabIndex = 42
        '
        'lblAutorizacion
        '
        Me.lblAutorizacion.BackColor = System.Drawing.Color.Transparent
        Me.lblAutorizacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAutorizacion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAutorizacion.Location = New System.Drawing.Point(708, 405)
        Me.lblAutorizacion.Name = "lblAutorizacion"
        Me.lblAutorizacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAutorizacion.Size = New System.Drawing.Size(125, 19)
        Me.lblAutorizacion.TabIndex = 144
        Me.lblAutorizacion.Text = "Referencia"
        Me.lblAutorizacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.cboFormaPago.Location = New System.Drawing.Point(8, 425)
        Me.cboFormaPago.Name = "cboFormaPago"
        Me.cboFormaPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFormaPago.Size = New System.Drawing.Size(171, 21)
        Me.cboFormaPago.TabIndex = 40
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(108, 92)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(289, 21)
        Me.cboSucursal.TabIndex = 145
        Me.cboSucursal.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(41, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(61, 19)
        Me.Label6.TabIndex = 146
        Me.Label6.Text = "Sucursal:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(143, 142)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(135, 20)
        Me.txtCodigo.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(143, 122)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(135, 19)
        Me.Label7.TabIndex = 148
        Me.Label7.Text = "Código Interno"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPrecioVenta
        '
        Me.txtPrecioVenta.AcceptsReturn = True
        Me.txtPrecioVenta.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta.Location = New System.Drawing.Point(808, 142)
        Me.txtPrecioVenta.MaxLength = 0
        Me.txtPrecioVenta.Name = "txtPrecioVenta"
        Me.txtPrecioVenta.ReadOnly = True
        Me.txtPrecioVenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta.Size = New System.Drawing.Size(88, 20)
        Me.txtPrecioVenta.TabIndex = 26
        Me.txtPrecioVenta.TabStop = False
        Me.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(809, 122)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(87, 19)
        Me.Label13.TabIndex = 150
        Me.Label13.Text = "Precio Venta"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUtilidad
        '
        Me.txtUtilidad.AcceptsReturn = True
        Me.txtUtilidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtUtilidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUtilidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUtilidad.Location = New System.Drawing.Point(896, 142)
        Me.txtUtilidad.MaxLength = 0
        Me.txtUtilidad.Name = "txtUtilidad"
        Me.txtUtilidad.ReadOnly = True
        Me.txtUtilidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUtilidad.Size = New System.Drawing.Size(46, 20)
        Me.txtUtilidad.TabIndex = 27
        Me.txtUtilidad.TabStop = False
        Me.txtUtilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(896, 122)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(46, 19)
        Me.Label14.TabIndex = 152
        Me.Label14.Text = "Utilidad"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtExistencias
        '
        Me.txtExistencias.AcceptsReturn = True
        Me.txtExistencias.BackColor = System.Drawing.SystemColors.Window
        Me.txtExistencias.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExistencias.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExistencias.Location = New System.Drawing.Point(629, 142)
        Me.txtExistencias.MaxLength = 0
        Me.txtExistencias.Name = "txtExistencias"
        Me.txtExistencias.ReadOnly = True
        Me.txtExistencias.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExistencias.Size = New System.Drawing.Size(50, 20)
        Me.txtExistencias.TabIndex = 23
        Me.txtExistencias.TabStop = False
        Me.txtExistencias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(629, 122)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(50, 19)
        Me.Label15.TabIndex = 154
        Me.Label15.Text = "Stock"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.cboTipoMoneda.Location = New System.Drawing.Point(498, 92)
        Me.cboTipoMoneda.Name = "cboTipoMoneda"
        Me.cboTipoMoneda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoMoneda.Size = New System.Drawing.Size(129, 21)
        Me.cboTipoMoneda.TabIndex = 191
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(432, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(60, 19)
        Me.Label4.TabIndex = 194
        Me.Label4.Text = "Moneda:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.AcceptsReturn = True
        Me.txtTipoCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoCambio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoCambio.Location = New System.Drawing.Point(739, 92)
        Me.txtTipoCambio.MaxLength = 0
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.ReadOnly = True
        Me.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoCambio.Size = New System.Drawing.Size(73, 20)
        Me.txtTipoCambio.TabIndex = 192
        Me.txtTipoCambio.TabStop = False
        Me.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(657, 92)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(76, 19)
        Me.Label9.TabIndex = 193
        Me.Label9.Text = "Tipo Cambio:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.btnGenerarPDF.TabIndex = 195
        Me.btnGenerarPDF.TabStop = False
        Me.btnGenerarPDF.Text = "A&brir PDF"
        Me.btnGenerarPDF.UseVisualStyleBackColor = False
        '
        'txtObservaciones
        '
        Me.txtObservaciones.AcceptsReturn = True
        Me.txtObservaciones.BackColor = System.Drawing.SystemColors.Window
        Me.txtObservaciones.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtObservaciones.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtObservaciones.Location = New System.Drawing.Point(104, 578)
        Me.txtObservaciones.MaxLength = 500
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservaciones.Size = New System.Drawing.Size(836, 45)
        Me.txtObservaciones.TabIndex = 197
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(12, 578)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(86, 19)
        Me._lblLabels_11.TabIndex = 196
        Me._lblLabels_11.Text = "Observaciones:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmCompra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(952, 633)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me.btnGenerarPDF)
        Me.Controls.Add(Me.cboTipoMoneda)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtTipoCambio)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtExistencias)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtUtilidad)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtPrecioVenta)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboSucursal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboFormaPago)
        Me.Controls.Add(Me.txtReferencia)
        Me.Controls.Add(Me.lblAutorizacion)
        Me.Controls.Add(Me.txtPlazoCredito)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboCondicionVenta)
        Me.Controls.Add(Me.txtIdOrdenCompra)
        Me.Controls.Add(Me.btnOrdenCompra)
        Me.Controls.Add(Me.btnBuscarProveedor)
        Me.Controls.Add(Me.txtProveedor)
        Me.Controls.Add(Me.txtCodigoProveedor)
        Me.Controls.Add(Me.txtSaldoPorPagar)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboCuentaBanco)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.grdDesglosePago)
        Me.Controls.Add(Me.btnEliminarPago)
        Me.Controls.Add(Me.btnInsertarPago)
        Me.Controls.Add(Me.txtMontoPago)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtPrecioCosto)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdDetalleCompra)
        Me.Controls.Add(Me.btnBusProd)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnInsertar)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.txtImpuesto)
        Me.Controls.Add(Me.txtIdCompra)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtDescuento)
        Me.Controls.Add(Me.txtFactura)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.lblLabel6)
        Me.Controls.Add(Me.lblLabel1)
        Me.Controls.Add(Me.LblImpuesto)
        Me.Controls.Add(Me.LblTotal)
        Me.Controls.Add(Me.LblDescuento)
        Me.Controls.Add(Me.lblSubTotal)
        Me.Controls.Add(Me.lblLabel4)
        Me.Controls.Add(Me.lblLabel3)
        Me.Controls.Add(Me.lblLabel2)
        Me.Controls.Add(Me.lblLabel0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(968, 672)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(968, 672)
        Me.Name = "FrmCompra"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Compra"
        CType(Me.grdDetalleCompra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleCompra As System.Windows.Forms.DataGridView
    Public WithEvents txtPrecioCosto As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cboCuentaBanco As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents grdDesglosePago As System.Windows.Forms.DataGridView
    Public WithEvents btnEliminarPago As System.Windows.Forms.Button
    Public WithEvents btnInsertarPago As System.Windows.Forms.Button
    Public WithEvents txtMontoPago As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtSaldoPorPagar As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoProveedor As System.Windows.Forms.TextBox
    Public WithEvents txtProveedor As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarProveedor As System.Windows.Forms.Button
    Public WithEvents txtIdOrdenCompra As System.Windows.Forms.TextBox
    Public WithEvents btnOrdenCompra As System.Windows.Forms.Button
    Public WithEvents txtPlazoCredito As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents Label11 As Label
    Public WithEvents cboCondicionVenta As ComboBox
    Public WithEvents txtReferencia As TextBox
    Public WithEvents lblAutorizacion As Label
    Public WithEvents cboFormaPago As ComboBox
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCodigo As TextBox
    Public WithEvents Label7 As Label
    Public WithEvents txtPrecioVenta As TextBox
    Public WithEvents Label13 As Label
    Public WithEvents txtUtilidad As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents txtExistencias As TextBox
    Public WithEvents Label15 As Label
    Public WithEvents cboTipoMoneda As ComboBox
    Public WithEvents Label4 As Label
    Public WithEvents txtTipoCambio As TextBox
    Public WithEvents Label9 As Label
    Public WithEvents btnGenerarPDF As Button
    Public WithEvents txtObservaciones As TextBox
    Public WithEvents _lblLabels_11 As Label
End Class