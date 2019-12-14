<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTrasladoMercaderia
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
    Public WithEvents txtIdTraslado As System.Windows.Forms.TextBox
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents txtReferencia As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents LblTotal As System.Windows.Forms.Label
    Public WithEvents lblLabel4 As System.Windows.Forms.Label
    Public WithEvents lblLabel3 As System.Windows.Forms.Label
    Public WithEvents lblLabel2 As System.Windows.Forms.Label
    Public WithEvents lblLabel0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnBusProd = New System.Windows.Forms.Button()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.txtIdTraslado = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtReferencia = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel2 = New System.Windows.Forms.Label()
        Me.lblLabel0 = New System.Windows.Forms.Label()
        Me.grdDetalleTraslado = New System.Windows.Forms.DataGridView()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtUnidad = New System.Windows.Forms.TextBox()
        Me.txtPrecioCosto = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.cboIdSucursalDestino = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNombreSucursalOrigen = New System.Windows.Forms.TextBox()
        CType(Me.grdDetalleTraslado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBusProd
        '
        Me.btnBusProd.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBusProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBusProd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBusProd.Location = New System.Drawing.Point(170, 430)
        Me.btnBusProd.Name = "btnBusProd"
        Me.btnBusProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBusProd.Size = New System.Drawing.Size(73, 25)
        Me.btnBusProd.TabIndex = 13
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
        Me.btnEliminar.Location = New System.Drawing.Point(90, 430)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminar.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminar.TabIndex = 12
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "&Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnInsertar
        '
        Me.btnInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertar.Location = New System.Drawing.Point(10, 430)
        Me.btnInsertar.Name = "btnInsertar"
        Me.btnInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertar.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertar.TabIndex = 11
        Me.btnInsertar.TabStop = False
        Me.btnInsertar.Text = "Insertar"
        Me.btnInsertar.UseVisualStyleBackColor = False
        '
        'txtIdTraslado
        '
        Me.txtIdTraslado.AcceptsReturn = True
        Me.txtIdTraslado.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdTraslado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdTraslado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdTraslado.Location = New System.Drawing.Point(113, 41)
        Me.txtIdTraslado.MaxLength = 0
        Me.txtIdTraslado.Name = "txtIdTraslado"
        Me.txtIdTraslado.ReadOnly = True
        Me.txtIdTraslado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdTraslado.Size = New System.Drawing.Size(73, 20)
        Me.txtIdTraslado.TabIndex = 0
        Me.txtIdTraslado.TabStop = False
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.Enabled = False
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(738, 430)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 16
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtReferencia
        '
        Me.txtReferencia.AcceptsReturn = True
        Me.txtReferencia.BackColor = System.Drawing.SystemColors.Window
        Me.txtReferencia.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReferencia.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReferencia.Location = New System.Drawing.Point(113, 146)
        Me.txtReferencia.MaxLength = 200
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReferencia.Size = New System.Drawing.Size(697, 20)
        Me.txtReferencia.TabIndex = 3
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(113, 67)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 2
        Me.txtFecha.TabStop = False
        '
        'LblTotal
        '
        Me.LblTotal.BackColor = System.Drawing.Color.Transparent
        Me.LblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTotal.Location = New System.Drawing.Point(667, 433)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(65, 19)
        Me.LblTotal.TabIndex = 19
        Me.LblTotal.Text = "Total:"
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(17, 147)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(90, 18)
        Me.lblLabel4.TabIndex = 13
        Me.lblLabel4.Text = "Referencia:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(60, 68)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel3.Size = New System.Drawing.Size(47, 19)
        Me.lblLabel3.TabIndex = 12
        Me.lblLabel3.Text = "Fecha:"
        Me.lblLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel2
        '
        Me.lblLabel2.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel2.Location = New System.Drawing.Point(14, 94)
        Me.lblLabel2.Name = "lblLabel2"
        Me.lblLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel2.Size = New System.Drawing.Size(93, 19)
        Me.lblLabel2.TabIndex = 11
        Me.lblLabel2.Text = "Sucursal origen:"
        Me.lblLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel0
        '
        Me.lblLabel0.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel0.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel0.Location = New System.Drawing.Point(41, 41)
        Me.lblLabel0.Name = "lblLabel0"
        Me.lblLabel0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel0.Size = New System.Drawing.Size(69, 19)
        Me.lblLabel0.TabIndex = 10
        Me.lblLabel0.Text = "Traslado No:"
        Me.lblLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleTraslado
        '
        Me.grdDetalleTraslado.AllowUserToAddRows = False
        Me.grdDetalleTraslado.AllowUserToDeleteRows = False
        Me.grdDetalleTraslado.AllowUserToResizeColumns = False
        Me.grdDetalleTraslado.AllowUserToResizeRows = False
        Me.grdDetalleTraslado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleTraslado.Location = New System.Drawing.Point(11, 224)
        Me.grdDetalleTraslado.MultiSelect = False
        Me.grdDetalleTraslado.Name = "grdDetalleTraslado"
        Me.grdDetalleTraslado.ReadOnly = True
        Me.grdDetalleTraslado.RowHeadersVisible = False
        Me.grdDetalleTraslado.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleTraslado.TabIndex = 10
        Me.grdDetalleTraslado.TabStop = False
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(54, 198)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(252, 20)
        Me.txtCodigo.TabIndex = 66
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(306, 178)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(345, 19)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Descripción"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(306, 198)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(345, 20)
        Me.txtDescripcion.TabIndex = 67
        Me.txtDescripcion.TabStop = False
        '
        'txtUnidad
        '
        Me.txtUnidad.AcceptsReturn = True
        Me.txtUnidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnidad.Location = New System.Drawing.Point(651, 198)
        Me.txtUnidad.MaxLength = 0
        Me.txtUnidad.Name = "txtUnidad"
        Me.txtUnidad.ReadOnly = True
        Me.txtUnidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnidad.Size = New System.Drawing.Size(60, 20)
        Me.txtUnidad.TabIndex = 68
        Me.txtUnidad.TabStop = False
        Me.txtUnidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPrecioCosto
        '
        Me.txtPrecioCosto.AcceptsReturn = True
        Me.txtPrecioCosto.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioCosto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioCosto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioCosto.Location = New System.Drawing.Point(711, 198)
        Me.txtPrecioCosto.MaxLength = 0
        Me.txtPrecioCosto.Name = "txtPrecioCosto"
        Me.txtPrecioCosto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioCosto.Size = New System.Drawing.Size(100, 20)
        Me.txtPrecioCosto.TabIndex = 69
        Me.txtPrecioCosto.TabStop = False
        Me.txtPrecioCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(11, 198)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(43, 20)
        Me.txtCantidad.TabIndex = 65
        Me.txtCantidad.Text = "1"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_8.Location = New System.Drawing.Point(651, 178)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(60, 19)
        Me._lblLabels_8.TabIndex = 73
        Me._lblLabels_8.Text = "U/M"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_7.Location = New System.Drawing.Point(711, 178)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(99, 19)
        Me._lblLabels_7.TabIndex = 72
        Me._lblLabels_7.Text = "Precio"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(11, 178)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(43, 19)
        Me._lblLabels_6.TabIndex = 71
        Me._lblLabels_6.Text = "Cant"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(54, 178)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(252, 19)
        Me._lblLabels_1.TabIndex = 70
        Me._lblLabels_1.Text = "Código"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboIdSucursalDestino
        '
        Me.cboIdSucursalDestino.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboIdSucursalDestino.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboIdSucursalDestino.BackColor = System.Drawing.SystemColors.Window
        Me.cboIdSucursalDestino.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIdSucursalDestino.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIdSucursalDestino.Location = New System.Drawing.Point(113, 119)
        Me.cboIdSucursalDestino.Name = "cboIdSucursalDestino"
        Me.cboIdSucursalDestino.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIdSucursalDestino.Size = New System.Drawing.Size(351, 21)
        Me.cboIdSucursalDestino.TabIndex = 78
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(14, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(93, 19)
        Me.Label2.TabIndex = 79
        Me.Label2.Text = "Sucursal destino:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNombreSucursalOrigen
        '
        Me.txtNombreSucursalOrigen.AcceptsReturn = True
        Me.txtNombreSucursalOrigen.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreSucursalOrigen.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreSucursalOrigen.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreSucursalOrigen.Location = New System.Drawing.Point(113, 93)
        Me.txtNombreSucursalOrigen.MaxLength = 0
        Me.txtNombreSucursalOrigen.Name = "txtNombreSucursalOrigen"
        Me.txtNombreSucursalOrigen.ReadOnly = True
        Me.txtNombreSucursalOrigen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreSucursalOrigen.Size = New System.Drawing.Size(351, 20)
        Me.txtNombreSucursalOrigen.TabIndex = 80
        Me.txtNombreSucursalOrigen.TabStop = False
        '
        'FrmTrasladoMercaderia
        '
        Me.AcceptButton = Me.btnInsertar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(823, 463)
        Me.Controls.Add(Me.txtNombreSucursalOrigen)
        Me.Controls.Add(Me.cboIdSucursalDestino)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtUnidad)
        Me.Controls.Add(Me.txtPrecioCosto)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me._lblLabels_8)
        Me.Controls.Add(Me._lblLabels_7)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me.grdDetalleTraslado)
        Me.Controls.Add(Me.btnBusProd)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnInsertar)
        Me.Controls.Add(Me.txtIdTraslado)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtReferencia)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.LblTotal)
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
        Me.MaximumSize = New System.Drawing.Size(839, 502)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(839, 502)
        Me.Name = "FrmTrasladoMercaderia"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Traslado de Mercancía"
        CType(Me.grdDetalleTraslado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleTraslado As System.Windows.Forms.DataGridView
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtUnidad As System.Windows.Forms.TextBox
    Public WithEvents txtPrecioCosto As System.Windows.Forms.TextBox
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents cboIdSucursalDestino As ComboBox
    Public WithEvents Label2 As Label
    Public WithEvents txtNombreSucursalOrigen As TextBox
End Class