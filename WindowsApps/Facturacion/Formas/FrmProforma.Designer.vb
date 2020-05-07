<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProforma
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents txtTextoAdicional As System.Windows.Forms.TextBox
    Public WithEvents btnBusProd As System.Windows.Forms.Button
    Public WithEvents btnAnular As System.Windows.Forms.Button
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnBuscar As System.Windows.Forms.Button
    Public WithEvents btnImprimir As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents btnEliminar As System.Windows.Forms.Button
    Public WithEvents btnInsertar As System.Windows.Forms.Button
    Public WithEvents txtIdProforma As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents grdDetalleProforma As System.Windows.Forms.DataGridView
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProforma))
        Me.txtTextoAdicional = New System.Windows.Forms.TextBox()
        Me.btnBusProd = New System.Windows.Forms.Button()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.txtIdProforma = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.grdDetalleProforma = New System.Windows.Forms.DataGridView()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.txtNombreCliente = New System.Windows.Forms.TextBox()
        Me.txtVendedor = New System.Windows.Forms.TextBox()
        Me.btnBuscaVendedor = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPorcentajeExoneracion = New System.Windows.Forms.TextBox()
        Me.txtNombreInstExoneracion = New System.Windows.Forms.TextBox()
        Me.txtNumDocExoneracion = New System.Windows.Forms.TextBox()
        Me.txtTipoExoneracion = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFechaExoneracion = New System.Windows.Forms.TextBox()
        Me.txtPorcDesc = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtUnidad = New System.Windows.Forms.TextBox()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.btnGenerarPDF = New System.Windows.Forms.Button()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtExistencias = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me._LblImpuesto_0 = New System.Windows.Forms.Label()
        Me._LblTotal_6 = New System.Windows.Forms.Label()
        Me._lblSubTotal_5 = New System.Windows.Forms.Label()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.grdDetalleProforma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTextoAdicional
        '
        Me.txtTextoAdicional.AcceptsReturn = True
        Me.txtTextoAdicional.BackColor = System.Drawing.SystemColors.Window
        Me.txtTextoAdicional.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTextoAdicional.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTextoAdicional.Location = New System.Drawing.Point(104, 437)
        Me.txtTextoAdicional.MaxLength = 500
        Me.txtTextoAdicional.Multiline = True
        Me.txtTextoAdicional.Name = "txtTextoAdicional"
        Me.txtTextoAdicional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTextoAdicional.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTextoAdicional.Size = New System.Drawing.Size(704, 45)
        Me.txtTextoAdicional.TabIndex = 81
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
        Me.btnImprimir.Text = "&Tiquete"
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
        'txtIdProforma
        '
        Me.txtIdProforma.AcceptsReturn = True
        Me.txtIdProforma.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdProforma.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdProforma.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdProforma.Location = New System.Drawing.Point(102, 39)
        Me.txtIdProforma.MaxLength = 0
        Me.txtIdProforma.Name = "txtIdProforma"
        Me.txtIdProforma.ReadOnly = True
        Me.txtIdProforma.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdProforma.Size = New System.Drawing.Size(73, 20)
        Me.txtIdProforma.TabIndex = 0
        Me.txtIdProforma.TabStop = False
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(-2, 437)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(100, 19)
        Me._lblLabels_11.TabIndex = 44
        Me._lblLabels_11.Text = "Otras referencias:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(348, 40)
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
        Me._lblLabels_0.Location = New System.Drawing.Point(8, 40)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(88, 19)
        Me._lblLabels_0.TabIndex = 13
        Me._lblLabels_0.Text = "Proforma No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleProforma
        '
        Me.grdDetalleProforma.AllowUserToAddRows = False
        Me.grdDetalleProforma.AllowUserToDeleteRows = False
        Me.grdDetalleProforma.AllowUserToResizeColumns = False
        Me.grdDetalleProforma.AllowUserToResizeRows = False
        Me.grdDetalleProforma.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleProforma.Location = New System.Drawing.Point(8, 201)
        Me.grdDetalleProforma.MultiSelect = False
        Me.grdDetalleProforma.Name = "grdDetalleProforma"
        Me.grdDetalleProforma.RowHeadersVisible = False
        Me.grdDetalleProforma.RowHeadersWidth = 30
        Me.grdDetalleProforma.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleProforma.TabIndex = 55
        Me.grdDetalleProforma.TabStop = False
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Image = CType(resources.GetObject("btnBuscarCliente.Image"), System.Drawing.Image)
        Me.btnBuscarCliente.Location = New System.Drawing.Point(766, 40)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarCliente.TabIndex = 4
        Me.btnBuscarCliente.TabStop = False
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'txtNombreCliente
        '
        Me.txtNombreCliente.AcceptsReturn = True
        Me.txtNombreCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreCliente.Location = New System.Drawing.Point(398, 40)
        Me.txtNombreCliente.MaxLength = 0
        Me.txtNombreCliente.Name = "txtNombreCliente"
        Me.txtNombreCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreCliente.Size = New System.Drawing.Size(362, 20)
        Me.txtNombreCliente.TabIndex = 3
        Me.txtNombreCliente.TabStop = False
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
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(249, 39)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(65, 20)
        Me.txtFecha.TabIndex = 4
        Me.txtFecha.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(188, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(55, 19)
        Me.Label15.TabIndex = 111
        Me.Label15.Text = "Fecha:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPorcentajeExoneracion
        '
        Me.txtPorcentajeExoneracion.AcceptsReturn = True
        Me.txtPorcentajeExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentajeExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentajeExoneracion.Enabled = False
        Me.txtPorcentajeExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentajeExoneracion.Location = New System.Drawing.Point(748, 124)
        Me.txtPorcentajeExoneracion.MaxLength = 0
        Me.txtPorcentajeExoneracion.Name = "txtPorcentajeExoneracion"
        Me.txtPorcentajeExoneracion.ReadOnly = True
        Me.txtPorcentajeExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentajeExoneracion.Size = New System.Drawing.Size(39, 20)
        Me.txtPorcentajeExoneracion.TabIndex = 159
        Me.txtPorcentajeExoneracion.TabStop = False
        '
        'txtNombreInstExoneracion
        '
        Me.txtNombreInstExoneracion.AcceptsReturn = True
        Me.txtNombreInstExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreInstExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreInstExoneracion.Enabled = False
        Me.txtNombreInstExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreInstExoneracion.Location = New System.Drawing.Point(482, 124)
        Me.txtNombreInstExoneracion.MaxLength = 0
        Me.txtNombreInstExoneracion.Name = "txtNombreInstExoneracion"
        Me.txtNombreInstExoneracion.ReadOnly = True
        Me.txtNombreInstExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreInstExoneracion.Size = New System.Drawing.Size(181, 20)
        Me.txtNombreInstExoneracion.TabIndex = 157
        '
        'txtNumDocExoneracion
        '
        Me.txtNumDocExoneracion.AcceptsReturn = True
        Me.txtNumDocExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumDocExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumDocExoneracion.Enabled = False
        Me.txtNumDocExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumDocExoneracion.Location = New System.Drawing.Point(374, 124)
        Me.txtNumDocExoneracion.MaxLength = 0
        Me.txtNumDocExoneracion.Name = "txtNumDocExoneracion"
        Me.txtNumDocExoneracion.ReadOnly = True
        Me.txtNumDocExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumDocExoneracion.Size = New System.Drawing.Size(103, 20)
        Me.txtNumDocExoneracion.TabIndex = 156
        Me.txtNumDocExoneracion.TabStop = False
        '
        'txtTipoExoneracion
        '
        Me.txtTipoExoneracion.AcceptsReturn = True
        Me.txtTipoExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoExoneracion.Enabled = False
        Me.txtTipoExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoExoneracion.Location = New System.Drawing.Point(180, 124)
        Me.txtTipoExoneracion.MaxLength = 0
        Me.txtTipoExoneracion.Name = "txtTipoExoneracion"
        Me.txtTipoExoneracion.ReadOnly = True
        Me.txtTipoExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoExoneracion.Size = New System.Drawing.Size(188, 20)
        Me.txtTipoExoneracion.TabIndex = 160
        Me.txtTipoExoneracion.TabStop = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(72, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(102, 19)
        Me.Label8.TabIndex = 161
        Me.Label8.Text = "Datos exoneración:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFechaExoneracion
        '
        Me.txtFechaExoneracion.AcceptsReturn = True
        Me.txtFechaExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtFechaExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFechaExoneracion.Enabled = False
        Me.txtFechaExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFechaExoneracion.Location = New System.Drawing.Point(669, 124)
        Me.txtFechaExoneracion.MaxLength = 0
        Me.txtFechaExoneracion.Name = "txtFechaExoneracion"
        Me.txtFechaExoneracion.ReadOnly = True
        Me.txtFechaExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFechaExoneracion.Size = New System.Drawing.Size(73, 20)
        Me.txtFechaExoneracion.TabIndex = 162
        Me.txtFechaExoneracion.TabStop = False
        '
        'txtPorcDesc
        '
        Me.txtPorcDesc.AcceptsReturn = True
        Me.txtPorcDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcDesc.Location = New System.Drawing.Point(670, 176)
        Me.txtPorcDesc.MaxLength = 0
        Me.txtPorcDesc.Name = "txtPorcDesc"
        Me.txtPorcDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcDesc.Size = New System.Drawing.Size(38, 20)
        Me.txtPorcDesc.TabIndex = 9
        Me.txtPorcDesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(670, 156)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(38, 19)
        Me.Label13.TabIndex = 175
        Me.Label13.Text = "%Des"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(8, 176)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(216, 20)
        Me.txtCodigo.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(244, 156)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(345, 19)
        Me.Label1.TabIndex = 174
        Me.Label1.Text = "Descripción"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(224, 176)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(325, 20)
        Me.txtDescripcion.TabIndex = 6
        Me.txtDescripcion.TabStop = False
        '
        'txtUnidad
        '
        Me.txtUnidad.AcceptsReturn = True
        Me.txtUnidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnidad.Location = New System.Drawing.Point(632, 176)
        Me.txtUnidad.MaxLength = 0
        Me.txtUnidad.Name = "txtUnidad"
        Me.txtUnidad.ReadOnly = True
        Me.txtUnidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnidad.Size = New System.Drawing.Size(38, 20)
        Me.txtUnidad.TabIndex = 8
        Me.txtUnidad.TabStop = False
        Me.txtUnidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPrecio
        '
        Me.txtPrecio.AcceptsReturn = True
        Me.txtPrecio.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecio.Location = New System.Drawing.Point(708, 176)
        Me.txtPrecio.MaxLength = 0
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.ReadOnly = True
        Me.txtPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecio.Size = New System.Drawing.Size(100, 20)
        Me.txtPrecio.TabIndex = 10
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(589, 176)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(43, 20)
        Me.txtCantidad.TabIndex = 7
        Me.txtCantidad.Text = "1"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_8.Location = New System.Drawing.Point(632, 156)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(38, 19)
        Me._lblLabels_8.TabIndex = 168
        Me._lblLabels_8.Text = "U/M"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_7.Location = New System.Drawing.Point(708, 156)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(99, 19)
        Me._lblLabels_7.TabIndex = 167
        Me._lblLabels_7.Text = "Precio"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(589, 156)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(43, 19)
        Me._lblLabels_6.TabIndex = 166
        Me._lblLabels_6.Text = "Cant"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(8, 156)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(236, 19)
        Me._lblLabels_1.TabIndex = 165
        Me._lblLabels_1.Text = "Código"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.btnGenerarPDF.TabIndex = 189
        Me.btnGenerarPDF.TabStop = False
        Me.btnGenerarPDF.Text = "A&brir PDF"
        Me.btnGenerarPDF.UseVisualStyleBackColor = False
        '
        'txtTelefono
        '
        Me.txtTelefono.AcceptsReturn = True
        Me.txtTelefono.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefono.Location = New System.Drawing.Point(102, 65)
        Me.txtTelefono.MaxLength = 8
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefono.Size = New System.Drawing.Size(126, 20)
        Me.txtTelefono.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(88, 19)
        Me.Label2.TabIndex = 191
        Me.Label2.Text = "Teléfono:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.cboTipoMoneda.Location = New System.Drawing.Point(471, 91)
        Me.cboTipoMoneda.Name = "cboTipoMoneda"
        Me.cboTipoMoneda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoMoneda.Size = New System.Drawing.Size(129, 21)
        Me.cboTipoMoneda.TabIndex = 192
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(405, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 19)
        Me.Label3.TabIndex = 195
        Me.Label3.Text = "Moneda:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.AcceptsReturn = True
        Me.txtTipoCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoCambio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoCambio.Location = New System.Drawing.Point(699, 91)
        Me.txtTipoCambio.MaxLength = 0
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.ReadOnly = True
        Me.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoCambio.Size = New System.Drawing.Size(73, 20)
        Me.txtTipoCambio.TabIndex = 193
        Me.txtTipoCambio.TabStop = False
        Me.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(617, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(76, 19)
        Me.Label9.TabIndex = 194
        Me.Label9.Text = "Tipo Cambio:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtExistencias
        '
        Me.txtExistencias.AcceptsReturn = True
        Me.txtExistencias.BackColor = System.Drawing.SystemColors.Window
        Me.txtExistencias.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExistencias.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExistencias.Location = New System.Drawing.Point(549, 176)
        Me.txtExistencias.MaxLength = 0
        Me.txtExistencias.Name = "txtExistencias"
        Me.txtExistencias.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExistencias.Size = New System.Drawing.Size(40, 20)
        Me.txtExistencias.TabIndex = 200
        Me.txtExistencias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(549, 156)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(40, 19)
        Me.Label16.TabIndex = 199
        Me.Label16.Text = "Stock"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescuento
        '
        Me.txtDescuento.AcceptsReturn = True
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescuento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescuento.Location = New System.Drawing.Point(527, 407)
        Me.txtDescuento.MaxLength = 0
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.ReadOnly = True
        Me.txtDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescuento.Size = New System.Drawing.Size(73, 20)
        Me.txtDescuento.TabIndex = 208
        Me.txtDescuento.TabStop = False
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(492, 407)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(36, 19)
        Me.Label17.TabIndex = 211
        Me.Label17.Text = "Desc:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(627, 407)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.ReadOnly = True
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 209
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(735, 407)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 210
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(417, 407)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 207
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '_LblImpuesto_0
        '
        Me._LblImpuesto_0.BackColor = System.Drawing.Color.Transparent
        Me._LblImpuesto_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblImpuesto_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblImpuesto_0.Location = New System.Drawing.Point(596, 407)
        Me._LblImpuesto_0.Name = "_LblImpuesto_0"
        Me._LblImpuesto_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblImpuesto_0.Size = New System.Drawing.Size(32, 19)
        Me._LblImpuesto_0.TabIndex = 206
        Me._LblImpuesto_0.Text = "Imp:"
        Me._LblImpuesto_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_LblTotal_6
        '
        Me._LblTotal_6.BackColor = System.Drawing.Color.Transparent
        Me._LblTotal_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblTotal_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblTotal_6.Location = New System.Drawing.Point(695, 407)
        Me._LblTotal_6.Name = "_LblTotal_6"
        Me._LblTotal_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblTotal_6.Size = New System.Drawing.Size(42, 19)
        Me._LblTotal_6.TabIndex = 205
        Me._LblTotal_6.Text = "Total:"
        Me._LblTotal_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblSubTotal_5
        '
        Me._lblSubTotal_5.BackColor = System.Drawing.Color.Transparent
        Me._lblSubTotal_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblSubTotal_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblSubTotal_5.Location = New System.Drawing.Point(353, 407)
        Me._lblSubTotal_5.Name = "_lblSubTotal_5"
        Me._lblSubTotal_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblSubTotal_5.Size = New System.Drawing.Size(65, 19)
        Me._lblSubTotal_5.TabIndex = 204
        Me._lblSubTotal_5.Text = "Sub-Total:"
        Me._lblSubTotal_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(102, 91)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(289, 21)
        Me.cboSucursal.TabIndex = 212
        Me.cboSucursal.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(35, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(61, 19)
        Me.Label6.TabIndex = 213
        Me.Label6.Text = "Sucursal:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmProforma
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(817, 493)
        Me.Controls.Add(Me.cboSucursal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtDescuento)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtImpuesto)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me._LblImpuesto_0)
        Me.Controls.Add(Me._LblTotal_6)
        Me.Controls.Add(Me._lblSubTotal_5)
        Me.Controls.Add(Me.txtExistencias)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cboTipoMoneda)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTipoCambio)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTelefono)
        Me.Controls.Add(Me.btnGenerarPDF)
        Me.Controls.Add(Me.txtPorcDesc)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtUnidad)
        Me.Controls.Add(Me.txtPrecio)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me._lblLabels_8)
        Me.Controls.Add(Me._lblLabels_7)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me.txtFechaExoneracion)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTipoExoneracion)
        Me.Controls.Add(Me.txtPorcentajeExoneracion)
        Me.Controls.Add(Me.txtNombreInstExoneracion)
        Me.Controls.Add(Me.txtNumDocExoneracion)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtVendedor)
        Me.Controls.Add(Me.btnBuscaVendedor)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtNombreCliente)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.grdDetalleProforma)
        Me.Controls.Add(Me.txtTextoAdicional)
        Me.Controls.Add(Me.btnBusProd)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnInsertar)
        Me.Controls.Add(Me.txtIdProforma)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(833, 532)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(833, 532)
        Me.Name = "FrmProforma"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Proformas"
        CType(Me.grdDetalleProforma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Public WithEvents txtNombreCliente As System.Windows.Forms.TextBox
    Public WithEvents txtVendedor As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscaVendedor As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents txtFecha As TextBox
    Public WithEvents Label15 As Label
    Public WithEvents txtPorcentajeExoneracion As TextBox
    Public WithEvents txtNombreInstExoneracion As TextBox
    Public WithEvents txtNumDocExoneracion As TextBox
    Public WithEvents txtTipoExoneracion As TextBox
    Public WithEvents Label8 As Label
    Public WithEvents txtFechaExoneracion As TextBox
    Public WithEvents txtPorcDesc As TextBox
    Public WithEvents Label13 As Label
    Friend WithEvents txtCodigo As TextBox
    Public WithEvents Label1 As Label
    Public WithEvents txtDescripcion As TextBox
    Public WithEvents txtUnidad As TextBox
    Public WithEvents txtPrecio As TextBox
    Public WithEvents txtCantidad As TextBox
    Public WithEvents _lblLabels_8 As Label
    Public WithEvents _lblLabels_7 As Label
    Public WithEvents _lblLabels_6 As Label
    Public WithEvents _lblLabels_1 As Label
    Public WithEvents btnGenerarPDF As Button
    Public WithEvents txtTelefono As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents cboTipoMoneda As ComboBox
    Public WithEvents Label3 As Label
    Public WithEvents txtTipoCambio As TextBox
    Public WithEvents Label9 As Label
    Public WithEvents txtExistencias As TextBox
    Public WithEvents Label16 As Label
    Public WithEvents txtDescuento As TextBox
    Public WithEvents Label17 As Label
    Public WithEvents txtImpuesto As TextBox
    Public WithEvents txtTotal As TextBox
    Public WithEvents txtSubTotal As TextBox
    Public WithEvents _LblImpuesto_0 As Label
    Public WithEvents _LblTotal_6 As Label
    Public WithEvents _lblSubTotal_5 As Label
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents Label6 As Label
End Class