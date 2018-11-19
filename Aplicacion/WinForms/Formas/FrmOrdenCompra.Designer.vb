<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOrdenCompra
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
    Public WithEvents txtImpuesto As System.Windows.Forms.TextBox
    Public WithEvents txtIdOrdenCompra As System.Windows.Forms.TextBox
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents txtDescuento As System.Windows.Forms.TextBox
    Public WithEvents txtReferencia As System.Windows.Forms.TextBox
    Public WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOrdenCompra))
        Me.btnBusProd = New System.Windows.Forms.Button()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me.txtIdOrdenCompra = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.txtReferencia = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.LblImpuesto = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.LblDescuento = New System.Windows.Forms.Label()
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel2 = New System.Windows.Forms.Label()
        Me.lblLabel0 = New System.Windows.Forms.Label()
        Me.grdDetalleOrdenCompra = New System.Windows.Forms.DataGridView()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtPrecioCosto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.lblLabel6 = New System.Windows.Forms.Label()
        Me.lblLabel1 = New System.Windows.Forms.Label()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.txtPlazoCredito = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboIdCondicionVenta = New System.Windows.Forms.ComboBox()
        CType(Me.grdDetalleOrdenCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBusProd
        '
        Me.btnBusProd.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBusProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBusProd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBusProd.Location = New System.Drawing.Point(167, 374)
        Me.btnBusProd.Name = "btnBusProd"
        Me.btnBusProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBusProd.Size = New System.Drawing.Size(73, 25)
        Me.btnBusProd.TabIndex = 17
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
        Me.btnAnular.TabIndex = 35
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
        Me.btnAgregar.TabIndex = 34
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
        Me.btnBuscar.TabIndex = 33
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
        Me.btnImprimir.TabIndex = 32
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
        Me.btnGuardar.TabIndex = 31
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminar.Location = New System.Drawing.Point(87, 374)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminar.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminar.TabIndex = 16
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "&Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnInsertar
        '
        Me.btnInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertar.Location = New System.Drawing.Point(7, 374)
        Me.btnInsertar.Name = "btnInsertar"
        Me.btnInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertar.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertar.TabIndex = 15
        Me.btnInsertar.TabStop = False
        Me.btnInsertar.Text = "Insertar"
        Me.btnInsertar.UseVisualStyleBackColor = False
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(734, 422)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.ReadOnly = True
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 20
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIdOrdenCompra
        '
        Me.txtIdOrdenCompra.AcceptsReturn = True
        Me.txtIdOrdenCompra.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdOrdenCompra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdOrdenCompra.Enabled = False
        Me.txtIdOrdenCompra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdOrdenCompra.Location = New System.Drawing.Point(106, 40)
        Me.txtIdOrdenCompra.MaxLength = 0
        Me.txtIdOrdenCompra.Name = "txtIdOrdenCompra"
        Me.txtIdOrdenCompra.ReadOnly = True
        Me.txtIdOrdenCompra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdOrdenCompra.Size = New System.Drawing.Size(73, 20)
        Me.txtIdOrdenCompra.TabIndex = 0
        Me.txtIdOrdenCompra.TabStop = False
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(734, 446)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 21
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescuento
        '
        Me.txtDescuento.AcceptsReturn = True
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescuento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescuento.Location = New System.Drawing.Point(734, 398)
        Me.txtDescuento.MaxLength = 0
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescuento.Size = New System.Drawing.Size(73, 20)
        Me.txtDescuento.TabIndex = 19
        Me.txtDescuento.TabStop = False
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtReferencia
        '
        Me.txtReferencia.AcceptsReturn = True
        Me.txtReferencia.BackColor = System.Drawing.SystemColors.Window
        Me.txtReferencia.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReferencia.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReferencia.Location = New System.Drawing.Point(260, 66)
        Me.txtReferencia.MaxLength = 0
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReferencia.Size = New System.Drawing.Size(81, 20)
        Me.txtReferencia.TabIndex = 4
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(734, 374)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 18
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(106, 66)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 3
        Me.txtFecha.TabStop = False
        '
        'LblImpuesto
        '
        Me.LblImpuesto.BackColor = System.Drawing.Color.Transparent
        Me.LblImpuesto.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblImpuesto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblImpuesto.Location = New System.Drawing.Point(662, 422)
        Me.LblImpuesto.Name = "LblImpuesto"
        Me.LblImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblImpuesto.Size = New System.Drawing.Size(65, 19)
        Me.LblImpuesto.TabIndex = 23
        Me.LblImpuesto.Text = "Impuesto:"
        Me.LblImpuesto.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblTotal
        '
        Me.LblTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTotal.Location = New System.Drawing.Point(662, 446)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(65, 19)
        Me.LblTotal.TabIndex = 19
        Me.LblTotal.Text = "Total:"
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblDescuento
        '
        Me.LblDescuento.BackColor = System.Drawing.Color.Transparent
        Me.LblDescuento.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDescuento.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDescuento.Location = New System.Drawing.Point(662, 398)
        Me.LblDescuento.Name = "LblDescuento"
        Me.LblDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDescuento.Size = New System.Drawing.Size(65, 19)
        Me.LblDescuento.TabIndex = 18
        Me.LblDescuento.Text = "Descuento:"
        Me.LblDescuento.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSubTotal
        '
        Me.lblSubTotal.BackColor = System.Drawing.Color.Transparent
        Me.lblSubTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSubTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSubTotal.Location = New System.Drawing.Point(662, 374)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSubTotal.Size = New System.Drawing.Size(65, 19)
        Me.lblSubTotal.TabIndex = 14
        Me.lblSubTotal.Text = "Sub-Total:"
        Me.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(185, 67)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(69, 19)
        Me.lblLabel4.TabIndex = 13
        Me.lblLabel4.Text = "Referencia:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(43, 67)
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
        Me.lblLabel2.Location = New System.Drawing.Point(189, 40)
        Me.lblLabel2.Name = "lblLabel2"
        Me.lblLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel2.Size = New System.Drawing.Size(65, 19)
        Me.lblLabel2.TabIndex = 11
        Me.lblLabel2.Text = "Proveedor:"
        Me.lblLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel0
        '
        Me.lblLabel0.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel0.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel0.Location = New System.Drawing.Point(43, 40)
        Me.lblLabel0.Name = "lblLabel0"
        Me.lblLabel0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel0.Size = New System.Drawing.Size(57, 19)
        Me.lblLabel0.TabIndex = 10
        Me.lblLabel0.Text = "Orden No:"
        Me.lblLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleOrdenCompra
        '
        Me.grdDetalleOrdenCompra.AllowUserToAddRows = False
        Me.grdDetalleOrdenCompra.AllowUserToDeleteRows = False
        Me.grdDetalleOrdenCompra.AllowUserToResizeColumns = False
        Me.grdDetalleOrdenCompra.AllowUserToResizeRows = False
        Me.grdDetalleOrdenCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleOrdenCompra.Location = New System.Drawing.Point(7, 168)
        Me.grdDetalleOrdenCompra.MultiSelect = False
        Me.grdDetalleOrdenCompra.Name = "grdDetalleOrdenCompra"
        Me.grdDetalleOrdenCompra.ReadOnly = True
        Me.grdDetalleOrdenCompra.RowHeadersVisible = False
        Me.grdDetalleOrdenCompra.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleOrdenCompra.TabIndex = 14
        Me.grdDetalleOrdenCompra.TabStop = False
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(48, 142)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(270, 20)
        Me.txtCodigo.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(318, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(347, 19)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Descripción"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(318, 142)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(401, 20)
        Me.txtDescripcion.TabIndex = 12
        Me.txtDescripcion.TabStop = False
        '
        'txtPrecioCosto
        '
        Me.txtPrecioCosto.AcceptsReturn = True
        Me.txtPrecioCosto.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioCosto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioCosto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioCosto.Location = New System.Drawing.Point(719, 142)
        Me.txtPrecioCosto.MaxLength = 0
        Me.txtPrecioCosto.Name = "txtPrecioCosto"
        Me.txtPrecioCosto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioCosto.Size = New System.Drawing.Size(88, 20)
        Me.txtPrecioCosto.TabIndex = 13
        Me.txtPrecioCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(720, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(87, 19)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Precio Costo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(7, 142)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(41, 20)
        Me.txtCantidad.TabIndex = 10
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblLabel6
        '
        Me.lblLabel6.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel6.Location = New System.Drawing.Point(7, 122)
        Me.lblLabel6.Name = "lblLabel6"
        Me.lblLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel6.Size = New System.Drawing.Size(41, 19)
        Me.lblLabel6.TabIndex = 51
        Me.lblLabel6.Text = "Cant"
        Me.lblLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLabel1
        '
        Me.lblLabel1.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel1.Location = New System.Drawing.Point(48, 122)
        Me.lblLabel1.Name = "lblLabel1"
        Me.lblLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel1.Size = New System.Drawing.Size(270, 19)
        Me.lblLabel1.TabIndex = 50
        Me.lblLabel1.Text = "Código"
        Me.lblLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Image = CType(resources.GetObject("btnBuscarProveedor.Image"), System.Drawing.Image)
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(707, 39)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarProveedor.TabIndex = 2
        Me.btnBuscarProveedor.TabStop = False
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'txtProveedor
        '
        Me.txtProveedor.AcceptsReturn = True
        Me.txtProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProveedor.Location = New System.Drawing.Point(260, 40)
        Me.txtProveedor.MaxLength = 0
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProveedor.Size = New System.Drawing.Size(446, 20)
        Me.txtProveedor.TabIndex = 1
        Me.txtProveedor.TabStop = False
        '
        'txtPlazoCredito
        '
        Me.txtPlazoCredito.AcceptsReturn = True
        Me.txtPlazoCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlazoCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlazoCredito.Enabled = False
        Me.txtPlazoCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlazoCredito.Location = New System.Drawing.Point(501, 92)
        Me.txtPlazoCredito.MaxLength = 300
        Me.txtPlazoCredito.Name = "txtPlazoCredito"
        Me.txtPlazoCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlazoCredito.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlazoCredito.Size = New System.Drawing.Size(42, 20)
        Me.txtPlazoCredito.TabIndex = 6
        Me.txtPlazoCredito.TabStop = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(408, 93)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(87, 19)
        Me.Label12.TabIndex = 144
        Me.Label12.Text = "Plazo de crédito:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(11, 92)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(89, 19)
        Me.Label11.TabIndex = 143
        Me.Label11.Text = "Condición venta:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboIdCondicionVenta
        '
        Me.cboIdCondicionVenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboIdCondicionVenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboIdCondicionVenta.BackColor = System.Drawing.SystemColors.Window
        Me.cboIdCondicionVenta.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIdCondicionVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIdCondicionVenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIdCondicionVenta.IntegralHeight = False
        Me.cboIdCondicionVenta.ItemHeight = 13
        Me.cboIdCondicionVenta.Location = New System.Drawing.Point(106, 92)
        Me.cboIdCondicionVenta.Name = "cboIdCondicionVenta"
        Me.cboIdCondicionVenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIdCondicionVenta.Size = New System.Drawing.Size(295, 21)
        Me.cboIdCondicionVenta.TabIndex = 5
        '
        'FrmOrdenCompra
        '
        Me.AcceptButton = Me.btnInsertar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(814, 475)
        Me.Controls.Add(Me.txtPlazoCredito)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboIdCondicionVenta)
        Me.Controls.Add(Me.btnBuscarProveedor)
        Me.Controls.Add(Me.txtProveedor)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtPrecioCosto)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblLabel6)
        Me.Controls.Add(Me.lblLabel1)
        Me.Controls.Add(Me.grdDetalleOrdenCompra)
        Me.Controls.Add(Me.btnBusProd)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnInsertar)
        Me.Controls.Add(Me.txtImpuesto)
        Me.Controls.Add(Me.txtIdOrdenCompra)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtDescuento)
        Me.Controls.Add(Me.txtReferencia)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me.txtFecha)
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
        Me.MinimizeBox = False
        Me.Name = "FrmOrdenCompra"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Ordenes de Compra"
        CType(Me.grdDetalleOrdenCompra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleOrdenCompra As System.Windows.Forms.DataGridView
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtPrecioCosto As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents lblLabel6 As System.Windows.Forms.Label
    Public WithEvents lblLabel1 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarProveedor As System.Windows.Forms.Button
    Public WithEvents txtProveedor As System.Windows.Forms.TextBox
    Public WithEvents txtPlazoCredito As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents Label11 As Label
    Public WithEvents cboIdCondicionVenta As ComboBox
End Class