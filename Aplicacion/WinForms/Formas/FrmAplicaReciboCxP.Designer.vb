<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAplicaReciboCxP
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
    Public WithEvents CmdAgregar As System.Windows.Forms.Button
    Public WithEvents CmdGuardar As System.Windows.Forms.Button
    Public WithEvents txtMontoAbono As System.Windows.Forms.TextBox
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents Recibo As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAplicaReciboCxP))
        Me.CmdAgregar = New System.Windows.Forms.Button()
        Me.CmdGuardar = New System.Windows.Forms.Button()
        Me.txtMontoAbono = New System.Windows.Forms.TextBox()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Recibo = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        Me.txtDocumento = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboCuentaBanco = New System.Windows.Forms.ComboBox()
        Me.lblBanco = New System.Windows.Forms.Label()
        Me.cboTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdDesglosePago = New System.Windows.Forms.DataGridView()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.btnEliminarPago = New System.Windows.Forms.Button()
        Me.btnInsertarPago = New System.Windows.Forms.Button()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.cboFormaPago = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboCuentaPorPagar = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotalAbonado = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSaldoPorPagar = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNombreProveedor = New System.Windows.Forms.TextBox()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.grdDesgloseCuenta = New System.Windows.Forms.DataGridView()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.txtSaldoActual = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMontoOriginal = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDesgloseCuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdAgregar
        '
        Me.CmdAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAgregar.Location = New System.Drawing.Point(136, 8)
        Me.CmdAgregar.Name = "CmdAgregar"
        Me.CmdAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAgregar.Size = New System.Drawing.Size(64, 21)
        Me.CmdAgregar.TabIndex = 0
        Me.CmdAgregar.TabStop = False
        Me.CmdAgregar.Text = "&Nuevo"
        Me.CmdAgregar.UseVisualStyleBackColor = False
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
        Me.CmdGuardar.TabIndex = 0
        Me.CmdGuardar.TabStop = False
        Me.CmdGuardar.Text = "&Guardar"
        Me.CmdGuardar.UseVisualStyleBackColor = False
        '
        'txtMontoAbono
        '
        Me.txtMontoAbono.AcceptsReturn = True
        Me.txtMontoAbono.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoAbono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMontoAbono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMontoAbono.Location = New System.Drawing.Point(651, 147)
        Me.txtMontoAbono.MaxLength = 0
        Me.txtMontoAbono.Name = "txtMontoAbono"
        Me.txtMontoAbono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMontoAbono.Size = New System.Drawing.Size(107, 20)
        Me.txtMontoAbono.TabIndex = 8
        Me.txtMontoAbono.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(86, 103)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(322, 20)
        Me.txtDescripcion.TabIndex = 4
        '
        'Recibo
        '
        Me.Recibo.AcceptsReturn = True
        Me.Recibo.BackColor = System.Drawing.SystemColors.Window
        Me.Recibo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Recibo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Recibo.Location = New System.Drawing.Point(302, 76)
        Me.Recibo.MaxLength = 0
        Me.Recibo.Name = "Recibo"
        Me.Recibo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Recibo.Size = New System.Drawing.Size(106, 20)
        Me.Recibo.TabIndex = 3
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.Enabled = False
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(86, 77)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 2
        Me.txtFecha.TabStop = False
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(652, 126)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(106, 19)
        Me._lblLabels_6.TabIndex = 16
        Me._lblLabels_6.Text = "Monto Abono"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_5.Location = New System.Drawing.Point(6, 103)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(73, 19)
        Me._lblLabels_5.TabIndex = 15
        Me._lblLabels_5.Text = "Descripción:"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_4.Location = New System.Drawing.Point(230, 76)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(69, 19)
        Me._lblLabels_4.TabIndex = 11
        Me._lblLabels_4.Text = "Recibo No:"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(10, 77)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(69, 19)
        Me._lblLabels_3.TabIndex = 10
        Me._lblLabels_3.Text = "Fecha:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(10, 50)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(69, 19)
        Me._lblLabels_2.TabIndex = 9
        Me._lblLabels_2.Text = "Proveedor:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmdImprimir
        '
        Me.CmdImprimir.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdImprimir.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdImprimir.Location = New System.Drawing.Point(72, 8)
        Me.CmdImprimir.Name = "CmdImprimir"
        Me.CmdImprimir.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdImprimir.Size = New System.Drawing.Size(64, 21)
        Me.CmdImprimir.TabIndex = 0
        Me.CmdImprimir.TabStop = False
        Me.CmdImprimir.Text = "&Imprimir"
        Me.CmdImprimir.UseVisualStyleBackColor = False
        '
        'txtDocumento
        '
        Me.txtDocumento.AcceptsReturn = True
        Me.txtDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocumento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocumento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocumento.Location = New System.Drawing.Point(372, 384)
        Me.txtDocumento.MaxLength = 0
        Me.txtDocumento.Name = "txtDocumento"
        Me.txtDocumento.ReadOnly = True
        Me.txtDocumento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocumento.Size = New System.Drawing.Size(100, 20)
        Me.txtDocumento.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(372, 364)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(100, 19)
        Me.Label7.TabIndex = 111
        Me.Label7.Text = "Nro. Documento"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.cboCuentaBanco.Location = New System.Drawing.Point(108, 384)
        Me.cboCuentaBanco.Name = "cboCuentaBanco"
        Me.cboCuentaBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCuentaBanco.Size = New System.Drawing.Size(264, 21)
        Me.cboCuentaBanco.TabIndex = 10
        '
        'lblBanco
        '
        Me.lblBanco.BackColor = System.Drawing.Color.Transparent
        Me.lblBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBanco.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBanco.Location = New System.Drawing.Point(108, 364)
        Me.lblBanco.Name = "lblBanco"
        Me.lblBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBanco.Size = New System.Drawing.Size(264, 19)
        Me.lblBanco.TabIndex = 109
        Me.lblBanco.Text = "Banco"
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
        Me.cboTipoMoneda.Location = New System.Drawing.Point(472, 384)
        Me.cboTipoMoneda.Name = "cboTipoMoneda"
        Me.cboTipoMoneda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoMoneda.Size = New System.Drawing.Size(105, 21)
        Me.cboTipoMoneda.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(472, 364)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(105, 19)
        Me.Label2.TabIndex = 108
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
        Me.grdDesglosePago.Location = New System.Drawing.Point(8, 411)
        Me.grdDesglosePago.MultiSelect = False
        Me.grdDesglosePago.Name = "grdDesglosePago"
        Me.grdDesglosePago.ReadOnly = True
        Me.grdDesglosePago.RowHeadersVisible = False
        Me.grdDesglosePago.RowHeadersWidth = 30
        Me.grdDesglosePago.Size = New System.Drawing.Size(750, 89)
        Me.grdDesglosePago.TabIndex = 17
        Me.grdDesglosePago.TabStop = False
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.AcceptsReturn = True
        Me.txtTipoCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoCambio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoCambio.Location = New System.Drawing.Point(577, 384)
        Me.txtTipoCambio.MaxLength = 0
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.ReadOnly = True
        Me.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoCambio.Size = New System.Drawing.Size(73, 20)
        Me.txtTipoCambio.TabIndex = 15
        Me.txtTipoCambio.TabStop = False
        Me.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnEliminarPago
        '
        Me.btnEliminarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminarPago.Location = New System.Drawing.Point(88, 506)
        Me.btnEliminarPago.Name = "btnEliminarPago"
        Me.btnEliminarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminarPago.TabIndex = 19
        Me.btnEliminarPago.TabStop = False
        Me.btnEliminarPago.Text = "&Eliminar"
        Me.btnEliminarPago.UseVisualStyleBackColor = False
        '
        'btnInsertarPago
        '
        Me.btnInsertarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertarPago.Location = New System.Drawing.Point(8, 506)
        Me.btnInsertarPago.Name = "btnInsertarPago"
        Me.btnInsertarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertarPago.TabIndex = 18
        Me.btnInsertarPago.TabStop = False
        Me.btnInsertarPago.Text = "Insertar"
        Me.btnInsertarPago.UseVisualStyleBackColor = False
        '
        'txtMonto
        '
        Me.txtMonto.AcceptsReturn = True
        Me.txtMonto.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonto.Location = New System.Drawing.Point(649, 384)
        Me.txtMonto.MaxLength = 0
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonto.Size = New System.Drawing.Size(109, 20)
        Me.txtMonto.TabIndex = 16
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cboFormaPago.Location = New System.Drawing.Point(8, 384)
        Me.cboFormaPago.Name = "cboFormaPago"
        Me.cboFormaPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFormaPago.Size = New System.Drawing.Size(100, 21)
        Me.cboFormaPago.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(649, 364)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(109, 19)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "Monto"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 364)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(100, 19)
        Me.Label5.TabIndex = 105
        Me.Label5.Text = "Forma de Pago"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(574, 364)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(76, 19)
        Me.Label9.TabIndex = 107
        Me.Label9.Text = "Tipo Cambio"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCuentaPorPagar
        '
        Me.cboCuentaPorPagar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCuentaPorPagar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCuentaPorPagar.BackColor = System.Drawing.SystemColors.Window
        Me.cboCuentaPorPagar.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCuentaPorPagar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCuentaPorPagar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCuentaPorPagar.Location = New System.Drawing.Point(8, 147)
        Me.cboCuentaPorPagar.Name = "cboCuentaPorPagar"
        Me.cboCuentaPorPagar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCuentaPorPagar.Size = New System.Drawing.Size(321, 21)
        Me.cboCuentaPorPagar.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(9, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(320, 19)
        Me.Label1.TabIndex = 116
        Me.Label1.Text = "Cuenta por Pagar"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalAbonado
        '
        Me.txtTotalAbonado.AcceptsReturn = True
        Me.txtTotalAbonado.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalAbonado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalAbonado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalAbonado.Location = New System.Drawing.Point(437, 147)
        Me.txtTotalAbonado.MaxLength = 0
        Me.txtTotalAbonado.Name = "txtTotalAbonado"
        Me.txtTotalAbonado.ReadOnly = True
        Me.txtTotalAbonado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalAbonado.Size = New System.Drawing.Size(107, 20)
        Me.txtTotalAbonado.TabIndex = 7
        Me.txtTotalAbonado.TabStop = False
        Me.txtTotalAbonado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(437, 125)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(107, 19)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Total Abonado"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSaldoPorPagar
        '
        Me.txtSaldoPorPagar.AcceptsReturn = True
        Me.txtSaldoPorPagar.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoPorPagar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoPorPagar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoPorPagar.Location = New System.Drawing.Point(685, 506)
        Me.txtSaldoPorPagar.MaxLength = 0
        Me.txtSaldoPorPagar.Name = "txtSaldoPorPagar"
        Me.txtSaldoPorPagar.ReadOnly = True
        Me.txtSaldoPorPagar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoPorPagar.Size = New System.Drawing.Size(73, 20)
        Me.txtSaldoPorPagar.TabIndex = 20
        Me.txtSaldoPorPagar.TabStop = False
        Me.txtSaldoPorPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(587, 506)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(91, 19)
        Me.Label10.TabIndex = 149
        Me.Label10.Text = "Saldo por Pagar:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNombreProveedor
        '
        Me.txtNombreProveedor.AcceptsReturn = True
        Me.txtNombreProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreProveedor.Enabled = False
        Me.txtNombreProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreProveedor.Location = New System.Drawing.Point(85, 49)
        Me.txtNombreProveedor.MaxLength = 0
        Me.txtNombreProveedor.Name = "txtNombreProveedor"
        Me.txtNombreProveedor.ReadOnly = True
        Me.txtNombreProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreProveedor.Size = New System.Drawing.Size(405, 20)
        Me.txtNombreProveedor.TabIndex = 0
        Me.txtNombreProveedor.TabStop = False
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Image = CType(resources.GetObject("btnBuscarProveedor.Image"), System.Drawing.Image)
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(491, 49)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarProveedor.TabIndex = 0
        Me.btnBuscarProveedor.TabStop = False
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'grdDesgloseCuenta
        '
        Me.grdDesgloseCuenta.AllowUserToAddRows = False
        Me.grdDesgloseCuenta.AllowUserToDeleteRows = False
        Me.grdDesgloseCuenta.AllowUserToResizeColumns = False
        Me.grdDesgloseCuenta.AllowUserToResizeRows = False
        Me.grdDesgloseCuenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDesgloseCuenta.Location = New System.Drawing.Point(8, 174)
        Me.grdDesgloseCuenta.MultiSelect = False
        Me.grdDesgloseCuenta.Name = "grdDesgloseCuenta"
        Me.grdDesgloseCuenta.ReadOnly = True
        Me.grdDesgloseCuenta.RowHeadersVisible = False
        Me.grdDesgloseCuenta.RowHeadersWidth = 30
        Me.grdDesgloseCuenta.Size = New System.Drawing.Size(750, 149)
        Me.grdDesgloseCuenta.TabIndex = 150
        Me.grdDesgloseCuenta.TabStop = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminar.Location = New System.Drawing.Point(88, 329)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminar.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminar.TabIndex = 152
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "&Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnInsertar
        '
        Me.btnInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertar.Location = New System.Drawing.Point(8, 329)
        Me.btnInsertar.Name = "btnInsertar"
        Me.btnInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertar.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertar.TabIndex = 151
        Me.btnInsertar.TabStop = False
        Me.btnInsertar.Text = "Insertar"
        Me.btnInsertar.UseVisualStyleBackColor = False
        '
        'txtSaldoActual
        '
        Me.txtSaldoActual.AcceptsReturn = True
        Me.txtSaldoActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoActual.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoActual.Location = New System.Drawing.Point(544, 147)
        Me.txtSaldoActual.MaxLength = 0
        Me.txtSaldoActual.Name = "txtSaldoActual"
        Me.txtSaldoActual.ReadOnly = True
        Me.txtSaldoActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoActual.Size = New System.Drawing.Size(107, 20)
        Me.txtSaldoActual.TabIndex = 153
        Me.txtSaldoActual.TabStop = False
        Me.txtSaldoActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(544, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(107, 19)
        Me.Label3.TabIndex = 154
        Me.Label3.Text = "Saldo Actual"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMontoOriginal
        '
        Me.txtMontoOriginal.AcceptsReturn = True
        Me.txtMontoOriginal.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoOriginal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMontoOriginal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMontoOriginal.Location = New System.Drawing.Point(330, 147)
        Me.txtMontoOriginal.MaxLength = 0
        Me.txtMontoOriginal.Name = "txtMontoOriginal"
        Me.txtMontoOriginal.ReadOnly = True
        Me.txtMontoOriginal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMontoOriginal.Size = New System.Drawing.Size(107, 20)
        Me.txtMontoOriginal.TabIndex = 155
        Me.txtMontoOriginal.TabStop = False
        Me.txtMontoOriginal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(330, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(107, 19)
        Me.Label6.TabIndex = 156
        Me.Label6.Text = "Monto Original"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmAplicaReciboCxP
        '
        Me.AcceptButton = Me.btnInsertar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 539)
        Me.Controls.Add(Me.txtMontoOriginal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtSaldoActual)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.grdDesgloseCuenta)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnInsertar)
        Me.Controls.Add(Me.txtNombreProveedor)
        Me.Controls.Add(Me.btnBuscarProveedor)
        Me.Controls.Add(Me.txtSaldoPorPagar)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtTotalAbonado)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboCuentaPorPagar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDocumento)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboCuentaBanco)
        Me.Controls.Add(Me.lblBanco)
        Me.Controls.Add(Me.cboTipoMoneda)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdDesglosePago)
        Me.Controls.Add(Me.txtTipoCambio)
        Me.Controls.Add(Me.btnEliminarPago)
        Me.Controls.Add(Me.btnInsertarPago)
        Me.Controls.Add(Me.txtMonto)
        Me.Controls.Add(Me.cboFormaPago)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CmdImprimir)
        Me.Controls.Add(Me.CmdAgregar)
        Me.Controls.Add(Me.CmdGuardar)
        Me.Controls.Add(Me.txtMontoAbono)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.Recibo)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_5)
        Me.Controls.Add(Me._lblLabels_4)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAplicaReciboCxP"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aplicar recibo a Cuentas por Pagar"
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDesgloseCuenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents CmdImprimir As System.Windows.Forms.Button
    Public WithEvents txtDocumento As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents cboCuentaBanco As System.Windows.Forms.ComboBox
    Public WithEvents lblBanco As System.Windows.Forms.Label
    Public WithEvents cboTipoMoneda As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents grdDesglosePago As System.Windows.Forms.DataGridView
    Public WithEvents txtTipoCambio As System.Windows.Forms.TextBox
    Public WithEvents btnEliminarPago As System.Windows.Forms.Button
    Public WithEvents btnInsertarPago As System.Windows.Forms.Button
    Public WithEvents txtMonto As System.Windows.Forms.TextBox
    Public WithEvents cboFormaPago As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents cboCuentaPorPagar As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtTotalAbonado As System.Windows.Forms.TextBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtSaldoPorPagar As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents txtNombreProveedor As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarProveedor As System.Windows.Forms.Button
    Public WithEvents grdDesgloseCuenta As System.Windows.Forms.DataGridView
    Public WithEvents btnEliminar As System.Windows.Forms.Button
    Public WithEvents btnInsertar As System.Windows.Forms.Button
    Public WithEvents txtSaldoActual As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtMontoOriginal As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
End Class