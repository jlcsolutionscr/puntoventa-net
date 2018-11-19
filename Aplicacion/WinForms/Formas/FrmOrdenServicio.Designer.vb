<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOrdenServicio
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
    Public WithEvents cmdBusProd As System.Windows.Forms.Button
    Public WithEvents CmdAnular As System.Windows.Forms.Button
    Public WithEvents CmdAgregar As System.Windows.Forms.Button
    Public WithEvents CmdBuscar As System.Windows.Forms.Button
    Public WithEvents CmdImprimir As System.Windows.Forms.Button
    Public WithEvents CmdGuardar As System.Windows.Forms.Button
    Public WithEvents cmdEliminar As System.Windows.Forms.Button
    Public WithEvents cmdInsertar As System.Windows.Forms.Button
    Public WithEvents txtImpuesto As System.Windows.Forms.TextBox
    Public WithEvents txtIdOrdenServicio As System.Windows.Forms.TextBox
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents txtDescuento As System.Windows.Forms.TextBox
    Public WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents _LblImpuesto_0 As System.Windows.Forms.Label
    Public WithEvents _LblTotal_6 As System.Windows.Forms.Label
    Public WithEvents _LblDescuento_1 As System.Windows.Forms.Label
    Public WithEvents _lblSubTotal_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents grdDetalleOrdenServicio As System.Windows.Forms.DataGridView
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOrdenServicio))
        Me.cmdBusProd = New System.Windows.Forms.Button()
        Me.CmdAnular = New System.Windows.Forms.Button()
        Me.CmdAgregar = New System.Windows.Forms.Button()
        Me.CmdBuscar = New System.Windows.Forms.Button()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        Me.CmdGuardar = New System.Windows.Forms.Button()
        Me.cmdEliminar = New System.Windows.Forms.Button()
        Me.cmdInsertar = New System.Windows.Forms.Button()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me.txtIdOrdenServicio = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me._LblImpuesto_0 = New System.Windows.Forms.Label()
        Me._LblTotal_6 = New System.Windows.Forms.Label()
        Me._LblDescuento_1 = New System.Windows.Forms.Label()
        Me._lblSubTotal_5 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.grdDetalleOrdenServicio = New System.Windows.Forms.DataGridView()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.txtNombreCliente = New System.Windows.Forms.TextBox()
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
        Me.txtOperarios = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me.btnBuscaVendedor = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtVendedor = New System.Windows.Forms.TextBox()
        Me.txtHoraEntrada = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtHoraSalida = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMarca = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtModelo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPlaca = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEstadoActual = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.grdDetalleOrdenServicio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBusProd
        '
        Me.cmdBusProd.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdBusProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBusProd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBusProd.Location = New System.Drawing.Point(168, 443)
        Me.cmdBusProd.Name = "cmdBusProd"
        Me.cmdBusProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBusProd.Size = New System.Drawing.Size(73, 25)
        Me.cmdBusProd.TabIndex = 21
        Me.cmdBusProd.TabStop = False
        Me.cmdBusProd.Text = "&Buscar"
        Me.cmdBusProd.UseVisualStyleBackColor = False
        '
        'CmdAnular
        '
        Me.CmdAnular.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAnular.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAnular.Enabled = False
        Me.CmdAnular.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAnular.Location = New System.Drawing.Point(200, 8)
        Me.CmdAnular.Name = "CmdAnular"
        Me.CmdAnular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAnular.Size = New System.Drawing.Size(64, 21)
        Me.CmdAnular.TabIndex = 36
        Me.CmdAnular.TabStop = False
        Me.CmdAnular.Text = "&Anular"
        Me.CmdAnular.UseVisualStyleBackColor = False
        '
        'CmdAgregar
        '
        Me.CmdAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAgregar.Location = New System.Drawing.Point(264, 8)
        Me.CmdAgregar.Name = "CmdAgregar"
        Me.CmdAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAgregar.Size = New System.Drawing.Size(64, 21)
        Me.CmdAgregar.TabIndex = 35
        Me.CmdAgregar.TabStop = False
        Me.CmdAgregar.Text = "&Nuevo"
        Me.CmdAgregar.UseVisualStyleBackColor = False
        '
        'CmdBuscar
        '
        Me.CmdBuscar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdBuscar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdBuscar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdBuscar.Location = New System.Drawing.Point(136, 8)
        Me.CmdBuscar.Name = "CmdBuscar"
        Me.CmdBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdBuscar.Size = New System.Drawing.Size(64, 21)
        Me.CmdBuscar.TabIndex = 34
        Me.CmdBuscar.TabStop = False
        Me.CmdBuscar.Text = "B&uscar"
        Me.CmdBuscar.UseVisualStyleBackColor = False
        '
        'CmdImprimir
        '
        Me.CmdImprimir.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdImprimir.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdImprimir.Enabled = False
        Me.CmdImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdImprimir.Location = New System.Drawing.Point(72, 8)
        Me.CmdImprimir.Name = "CmdImprimir"
        Me.CmdImprimir.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdImprimir.Size = New System.Drawing.Size(64, 21)
        Me.CmdImprimir.TabIndex = 33
        Me.CmdImprimir.TabStop = False
        Me.CmdImprimir.Text = "&Imprimir"
        Me.CmdImprimir.UseVisualStyleBackColor = False
        '
        'CmdGuardar
        '
        Me.CmdGuardar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdGuardar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdGuardar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdGuardar.Location = New System.Drawing.Point(8, 8)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdGuardar.Size = New System.Drawing.Size(64, 21)
        Me.CmdGuardar.TabIndex = 32
        Me.CmdGuardar.TabStop = False
        Me.CmdGuardar.Text = "&Guardar"
        Me.CmdGuardar.UseVisualStyleBackColor = False
        '
        'cmdEliminar
        '
        Me.cmdEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEliminar.Location = New System.Drawing.Point(88, 443)
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEliminar.Size = New System.Drawing.Size(73, 25)
        Me.cmdEliminar.TabIndex = 20
        Me.cmdEliminar.TabStop = False
        Me.cmdEliminar.Text = "&Eliminar"
        Me.cmdEliminar.UseVisualStyleBackColor = False
        '
        'cmdInsertar
        '
        Me.cmdInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInsertar.Location = New System.Drawing.Point(8, 443)
        Me.cmdInsertar.Name = "cmdInsertar"
        Me.cmdInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInsertar.Size = New System.Drawing.Size(73, 25)
        Me.cmdInsertar.TabIndex = 19
        Me.cmdInsertar.TabStop = False
        Me.cmdInsertar.Text = "Insertar"
        Me.cmdInsertar.UseVisualStyleBackColor = False
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(735, 491)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.ReadOnly = True
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 24
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIdOrdenServicio
        '
        Me.txtIdOrdenServicio.AcceptsReturn = True
        Me.txtIdOrdenServicio.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdOrdenServicio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdOrdenServicio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdOrdenServicio.Location = New System.Drawing.Point(82, 36)
        Me.txtIdOrdenServicio.MaxLength = 0
        Me.txtIdOrdenServicio.Name = "txtIdOrdenServicio"
        Me.txtIdOrdenServicio.ReadOnly = True
        Me.txtIdOrdenServicio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdOrdenServicio.Size = New System.Drawing.Size(81, 20)
        Me.txtIdOrdenServicio.TabIndex = 0
        Me.txtIdOrdenServicio.TabStop = False
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(735, 515)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 25
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescuento
        '
        Me.txtDescuento.AcceptsReturn = True
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescuento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescuento.Location = New System.Drawing.Point(735, 467)
        Me.txtDescuento.MaxLength = 0
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescuento.Size = New System.Drawing.Size(73, 20)
        Me.txtDescuento.TabIndex = 23
        Me.txtDescuento.TabStop = False
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(735, 443)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 22
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(82, 89)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 4
        Me.txtFecha.TabStop = False
        '
        '_LblImpuesto_0
        '
        Me._LblImpuesto_0.BackColor = System.Drawing.Color.Transparent
        Me._LblImpuesto_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblImpuesto_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblImpuesto_0.Location = New System.Drawing.Point(664, 491)
        Me._LblImpuesto_0.Name = "_LblImpuesto_0"
        Me._LblImpuesto_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblImpuesto_0.Size = New System.Drawing.Size(65, 19)
        Me._LblImpuesto_0.TabIndex = 23
        Me._LblImpuesto_0.Text = "Impuesto:"
        Me._LblImpuesto_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_LblTotal_6
        '
        Me._LblTotal_6.BackColor = System.Drawing.Color.Transparent
        Me._LblTotal_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblTotal_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblTotal_6.Location = New System.Drawing.Point(664, 515)
        Me._LblTotal_6.Name = "_LblTotal_6"
        Me._LblTotal_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblTotal_6.Size = New System.Drawing.Size(65, 19)
        Me._LblTotal_6.TabIndex = 21
        Me._LblTotal_6.Text = "Total:"
        Me._LblTotal_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_LblDescuento_1
        '
        Me._LblDescuento_1.BackColor = System.Drawing.Color.Transparent
        Me._LblDescuento_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblDescuento_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblDescuento_1.Location = New System.Drawing.Point(664, 467)
        Me._LblDescuento_1.Name = "_LblDescuento_1"
        Me._LblDescuento_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblDescuento_1.Size = New System.Drawing.Size(65, 19)
        Me._LblDescuento_1.TabIndex = 20
        Me._LblDescuento_1.Text = "Descuento:"
        Me._LblDescuento_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblSubTotal_5
        '
        Me._lblSubTotal_5.BackColor = System.Drawing.Color.Transparent
        Me._lblSubTotal_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblSubTotal_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblSubTotal_5.Location = New System.Drawing.Point(664, 443)
        Me._lblSubTotal_5.Name = "_lblSubTotal_5"
        Me._lblSubTotal_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblSubTotal_5.Size = New System.Drawing.Size(65, 19)
        Me._lblSubTotal_5.TabIndex = 17
        Me._lblSubTotal_5.Text = "Sub-Total:"
        Me._lblSubTotal_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(19, 89)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(57, 19)
        Me._lblLabels_3.TabIndex = 15
        Me._lblLabels_3.Text = "Fecha:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(11, 64)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_2.TabIndex = 14
        Me._lblLabels_2.Text = "Cliente:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(11, 36)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_0.TabIndex = 13
        Me._lblLabels_0.Text = "Orden No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleOrdenServicio
        '
        Me.grdDetalleOrdenServicio.AllowUserToAddRows = False
        Me.grdDetalleOrdenServicio.AllowUserToDeleteRows = False
        Me.grdDetalleOrdenServicio.AllowUserToResizeColumns = False
        Me.grdDetalleOrdenServicio.AllowUserToResizeRows = False
        Me.grdDetalleOrdenServicio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleOrdenServicio.Location = New System.Drawing.Point(8, 237)
        Me.grdDetalleOrdenServicio.MultiSelect = False
        Me.grdDetalleOrdenServicio.Name = "grdDetalleOrdenServicio"
        Me.grdDetalleOrdenServicio.ReadOnly = True
        Me.grdDetalleOrdenServicio.RowHeadersVisible = False
        Me.grdDetalleOrdenServicio.RowHeadersWidth = 30
        Me.grdDetalleOrdenServicio.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleOrdenServicio.TabIndex = 18
        Me.grdDetalleOrdenServicio.TabStop = False
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Image = CType(resources.GetObject("btnBuscarCliente.Image"), System.Drawing.Image)
        Me.btnBuscarCliente.Location = New System.Drawing.Point(634, 63)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarCliente.TabIndex = 3
        Me.btnBuscarCliente.TabStop = False
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'txtNombreCliente
        '
        Me.txtNombreCliente.AcceptsReturn = True
        Me.txtNombreCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreCliente.Location = New System.Drawing.Point(82, 64)
        Me.txtNombreCliente.MaxLength = 100
        Me.txtNombreCliente.Name = "txtNombreCliente"
        Me.txtNombreCliente.ReadOnly = True
        Me.txtNombreCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreCliente.Size = New System.Drawing.Size(553, 20)
        Me.txtNombreCliente.TabIndex = 2
        Me.txtNombreCliente.TabStop = False
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(51, 211)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(252, 20)
        Me.txtCodigo.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(303, 191)
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
        Me.txtDescripcion.Location = New System.Drawing.Point(303, 211)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(345, 20)
        Me.txtDescripcion.TabIndex = 15
        Me.txtDescripcion.TabStop = False
        '
        'txtUnidad
        '
        Me.txtUnidad.AcceptsReturn = True
        Me.txtUnidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnidad.Location = New System.Drawing.Point(648, 211)
        Me.txtUnidad.MaxLength = 0
        Me.txtUnidad.Name = "txtUnidad"
        Me.txtUnidad.ReadOnly = True
        Me.txtUnidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnidad.Size = New System.Drawing.Size(60, 20)
        Me.txtUnidad.TabIndex = 16
        Me.txtUnidad.TabStop = False
        Me.txtUnidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPrecio
        '
        Me.txtPrecio.AcceptsReturn = True
        Me.txtPrecio.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecio.Location = New System.Drawing.Point(708, 211)
        Me.txtPrecio.MaxLength = 0
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecio.Size = New System.Drawing.Size(100, 20)
        Me.txtPrecio.TabIndex = 17
        Me.txtPrecio.TabStop = False
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(8, 211)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(43, 20)
        Me.txtCantidad.TabIndex = 13
        Me.txtCantidad.Text = "1"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_8.Location = New System.Drawing.Point(648, 191)
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
        Me._lblLabels_7.Location = New System.Drawing.Point(708, 191)
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
        Me._lblLabels_6.Location = New System.Drawing.Point(8, 191)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(58, 19)
        Me._lblLabels_6.TabIndex = 71
        Me._lblLabels_6.Text = "Cant"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(51, 191)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(252, 19)
        Me._lblLabels_1.TabIndex = 70
        Me._lblLabels_1.Text = "Código"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtOperarios
        '
        Me.txtOperarios.AcceptsReturn = True
        Me.txtOperarios.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperarios.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOperarios.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOperarios.Location = New System.Drawing.Point(239, 89)
        Me.txtOperarios.MaxLength = 200
        Me.txtOperarios.Name = "txtOperarios"
        Me.txtOperarios.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOperarios.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOperarios.Size = New System.Drawing.Size(549, 20)
        Me.txtOperarios.TabIndex = 5
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(176, 89)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(57, 19)
        Me._lblLabels_11.TabIndex = 76
        Me._lblLabels_11.Text = "Operarios:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBuscaVendedor
        '
        Me.btnBuscaVendedor.Image = CType(resources.GetObject("btnBuscaVendedor.Image"), System.Drawing.Image)
        Me.btnBuscaVendedor.Location = New System.Drawing.Point(782, 8)
        Me.btnBuscaVendedor.Name = "btnBuscaVendedor"
        Me.btnBuscaVendedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscaVendedor.TabIndex = 2
        Me.btnBuscaVendedor.TabStop = False
        Me.btnBuscaVendedor.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(462, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 19)
        Me.Label2.TabIndex = 82
        Me.Label2.Text = "Vendedor:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVendedor
        '
        Me.txtVendedor.AcceptsReturn = True
        Me.txtVendedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtVendedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVendedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVendedor.Location = New System.Drawing.Point(533, 9)
        Me.txtVendedor.MaxLength = 0
        Me.txtVendedor.Name = "txtVendedor"
        Me.txtVendedor.ReadOnly = True
        Me.txtVendedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendedor.Size = New System.Drawing.Size(250, 20)
        Me.txtVendedor.TabIndex = 1
        Me.txtVendedor.TabStop = False
        '
        'txtHoraEntrada
        '
        Me.txtHoraEntrada.AcceptsReturn = True
        Me.txtHoraEntrada.BackColor = System.Drawing.SystemColors.Window
        Me.txtHoraEntrada.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHoraEntrada.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHoraEntrada.Location = New System.Drawing.Point(82, 115)
        Me.txtHoraEntrada.MaxLength = 15
        Me.txtHoraEntrada.Name = "txtHoraEntrada"
        Me.txtHoraEntrada.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHoraEntrada.Size = New System.Drawing.Size(46, 20)
        Me.txtHoraEntrada.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(72, 19)
        Me.Label3.TabIndex = 84
        Me.Label3.Text = "Hora entrada:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHoraSalida
        '
        Me.txtHoraSalida.AcceptsReturn = True
        Me.txtHoraSalida.BackColor = System.Drawing.SystemColors.Window
        Me.txtHoraSalida.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHoraSalida.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHoraSalida.Location = New System.Drawing.Point(203, 115)
        Me.txtHoraSalida.MaxLength = 15
        Me.txtHoraSalida.Name = "txtHoraSalida"
        Me.txtHoraSalida.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHoraSalida.Size = New System.Drawing.Size(46, 20)
        Me.txtHoraSalida.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(134, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(63, 19)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Hora salida:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMarca
        '
        Me.txtMarca.AcceptsReturn = True
        Me.txtMarca.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarca.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMarca.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMarca.Location = New System.Drawing.Point(309, 115)
        Me.txtMarca.MaxLength = 20
        Me.txtMarca.Name = "txtMarca"
        Me.txtMarca.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMarca.Size = New System.Drawing.Size(73, 20)
        Me.txtMarca.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(261, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(42, 19)
        Me.Label5.TabIndex = 88
        Me.Label5.Text = "Marca:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtModelo
        '
        Me.txtModelo.AcceptsReturn = True
        Me.txtModelo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModelo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModelo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModelo.Location = New System.Drawing.Point(448, 115)
        Me.txtModelo.MaxLength = 30
        Me.txtModelo.Name = "txtModelo"
        Me.txtModelo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModelo.Size = New System.Drawing.Size(104, 20)
        Me.txtModelo.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(394, 115)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(48, 19)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "Modelo:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlaca
        '
        Me.txtPlaca.AcceptsReturn = True
        Me.txtPlaca.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlaca.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlaca.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlaca.Location = New System.Drawing.Point(606, 115)
        Me.txtPlaca.MaxLength = 10
        Me.txtPlaca.Name = "txtPlaca"
        Me.txtPlaca.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlaca.Size = New System.Drawing.Size(46, 20)
        Me.txtPlaca.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(563, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(37, 19)
        Me.Label7.TabIndex = 92
        Me.Label7.Text = "Placa:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtColor
        '
        Me.txtColor.AcceptsReturn = True
        Me.txtColor.BackColor = System.Drawing.SystemColors.Window
        Me.txtColor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtColor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtColor.Location = New System.Drawing.Point(715, 115)
        Me.txtColor.MaxLength = 20
        Me.txtColor.Name = "txtColor"
        Me.txtColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtColor.Size = New System.Drawing.Size(73, 20)
        Me.txtColor.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(667, 115)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(42, 19)
        Me.Label8.TabIndex = 94
        Me.Label8.Text = "Color:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEstadoActual
        '
        Me.txtEstadoActual.AcceptsReturn = True
        Me.txtEstadoActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtEstadoActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEstadoActual.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEstadoActual.Location = New System.Drawing.Point(82, 141)
        Me.txtEstadoActual.MaxLength = 500
        Me.txtEstadoActual.Multiline = True
        Me.txtEstadoActual.Name = "txtEstadoActual"
        Me.txtEstadoActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEstadoActual.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEstadoActual.Size = New System.Drawing.Size(706, 40)
        Me.txtEstadoActual.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(-1, 141)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(77, 40)
        Me.Label9.TabIndex = 96
        Me.Label9.Text = "Estado actual del vehículo:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmOrdenServicio
        '
        Me.AcceptButton = Me.cmdInsertar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(814, 543)
        Me.Controls.Add(Me.txtEstadoActual)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtColor)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtPlaca)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtModelo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtMarca)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtHoraSalida)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtHoraEntrada)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtVendedor)
        Me.Controls.Add(Me.btnBuscaVendedor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtOperarios)
        Me.Controls.Add(Me._lblLabels_11)
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
        Me.Controls.Add(Me.txtNombreCliente)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.grdDetalleOrdenServicio)
        Me.Controls.Add(Me.cmdBusProd)
        Me.Controls.Add(Me.CmdAnular)
        Me.Controls.Add(Me.CmdAgregar)
        Me.Controls.Add(Me.CmdBuscar)
        Me.Controls.Add(Me.CmdImprimir)
        Me.Controls.Add(Me.CmdGuardar)
        Me.Controls.Add(Me.cmdEliminar)
        Me.Controls.Add(Me.cmdInsertar)
        Me.Controls.Add(Me.txtImpuesto)
        Me.Controls.Add(Me.txtIdOrdenServicio)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtDescuento)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me._LblImpuesto_0)
        Me.Controls.Add(Me._LblTotal_6)
        Me.Controls.Add(Me._LblDescuento_1)
        Me.Controls.Add(Me._lblSubTotal_5)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmOrdenServicio"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Ordenes de Servicio"
        CType(Me.grdDetalleOrdenServicio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Public WithEvents txtNombreCliente As System.Windows.Forms.TextBox
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtUnidad As System.Windows.Forms.TextBox
    Public WithEvents txtPrecio As System.Windows.Forms.TextBox
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents txtOperarios As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Friend WithEvents btnBuscaVendedor As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtVendedor As System.Windows.Forms.TextBox
    Public WithEvents txtHoraEntrada As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtHoraSalida As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents txtMarca As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtModelo As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtPlaca As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents txtColor As System.Windows.Forms.TextBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtEstadoActual As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
End Class