<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmConsultaCierreCaja
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
    Public WithEvents txtFondoInicio As System.Windows.Forms.TextBox
    Public WithEvents txtTotalEfectivo As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtFondoInicio = New System.Windows.Forms.TextBox()
        Me.txtTotalEfectivo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtVentasEfectivo = New System.Windows.Forms.TextBox()
        Me.txtTotalIngresos = New System.Windows.Forms.TextBox()
        Me.txtIngresosEfectivo = New System.Windows.Forms.TextBox()
        Me.txtVentasTarjeta = New System.Windows.Forms.TextBox()
        Me.txtVentasCredito = New System.Windows.Forms.TextBox()
        Me.txtPagosCxCEfectivo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtComprasEfectivo = New System.Windows.Forms.TextBox()
        Me.txtTotalEgresos = New System.Windows.Forms.TextBox()
        Me.txtComprasCredito = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtEgresosEfectivo = New System.Windows.Forms.TextBox()
        Me.txtPagosCxPEfectivo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtComprasBancos = New System.Windows.Forms.TextBox()
        Me.txtTotalVentas = New System.Windows.Forms.TextBox()
        Me.txtTotalCompras = New System.Windows.Forms.TextBox()
        Me.txtRetencionIVA = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtComision = New System.Windows.Forms.TextBox()
        Me.txtLiquidacionTarjeta = New System.Windows.Forms.TextBox()
        Me.txtVentasBancos = New System.Windows.Forms.TextBox()
        Me.txtDevolucionesProveedores = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtDevolucionesClientes = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.btnReporte = New System.Windows.Forms.Button()
        Me.txtTotalIngresosTarjeta = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtAdelantosApartadoEfectivo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtAdelantosApartadoBancos = New System.Windows.Forms.TextBox()
        Me.txtAdelantosApartadoTarjeta = New System.Windows.Forms.TextBox()
        Me.txtAdelantosOrdenTarjeta = New System.Windows.Forms.TextBox()
        Me.txtAdelantosOrdenBancos = New System.Windows.Forms.TextBox()
        Me.txtAdelantosOrdenEfectivo = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtPagosCxCBancos = New System.Windows.Forms.TextBox()
        Me.txtPagosCxCTarjeta = New System.Windows.Forms.TextBox()
        Me.txtPagosCxPBancos = New System.Windows.Forms.TextBox()
        Me.txtTotalAdelantosApartado = New System.Windows.Forms.TextBox()
        Me.txtTotalAdelantosOrden = New System.Windows.Forms.TextBox()
        Me.btnTiquete = New System.Windows.Forms.Button()
        Me.txtEfectivoCaja = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtSaldo = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtTotalPagoCxC = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotalPagoCxP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCierreEfectivoProx = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtRetiroEfectivo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dgvDetalleEfectivoCierreCaja = New System.Windows.Forms.DataGridView()
        CType(Me.dgvDetalleEfectivoCierreCaja, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFondoInicio
        '
        Me.txtFondoInicio.AcceptsReturn = True
        Me.txtFondoInicio.BackColor = System.Drawing.SystemColors.Window
        Me.txtFondoInicio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFondoInicio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFondoInicio.Location = New System.Drawing.Point(205, 346)
        Me.txtFondoInicio.MaxLength = 0
        Me.txtFondoInicio.Name = "txtFondoInicio"
        Me.txtFondoInicio.ReadOnly = True
        Me.txtFondoInicio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFondoInicio.Size = New System.Drawing.Size(105, 20)
        Me.txtFondoInicio.TabIndex = 0
        Me.txtFondoInicio.TabStop = False
        Me.txtFondoInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalEfectivo
        '
        Me.txtTotalEfectivo.AcceptsReturn = True
        Me.txtTotalEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalEfectivo.Location = New System.Drawing.Point(205, 424)
        Me.txtTotalEfectivo.MaxLength = 0
        Me.txtTotalEfectivo.Name = "txtTotalEfectivo"
        Me.txtTotalEfectivo.ReadOnly = True
        Me.txtTotalEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalEfectivo.TabIndex = 13
        Me.txtTotalEfectivo.TabStop = False
        Me.txtTotalEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(91, 348)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(106, 17)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Inicio de Efectivo"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(85, 63)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(112, 17)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Ventas:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(65, 425)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(132, 17)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Cierre de Efectivo"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(41, 374)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(156, 17)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Total de Ingresos en Efectivo"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(85, 193)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(112, 17)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Ingresos varios:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVentasEfectivo
        '
        Me.txtVentasEfectivo.AcceptsReturn = True
        Me.txtVentasEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVentasEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVentasEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVentasEfectivo.Location = New System.Drawing.Point(205, 62)
        Me.txtVentasEfectivo.MaxLength = 0
        Me.txtVentasEfectivo.Name = "txtVentasEfectivo"
        Me.txtVentasEfectivo.ReadOnly = True
        Me.txtVentasEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVentasEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtVentasEfectivo.TabIndex = 1
        Me.txtVentasEfectivo.TabStop = False
        Me.txtVentasEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalIngresos
        '
        Me.txtTotalIngresos.AcceptsReturn = True
        Me.txtTotalIngresos.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalIngresos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalIngresos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalIngresos.Location = New System.Drawing.Point(205, 372)
        Me.txtTotalIngresos.MaxLength = 0
        Me.txtTotalIngresos.Name = "txtTotalIngresos"
        Me.txtTotalIngresos.ReadOnly = True
        Me.txtTotalIngresos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalIngresos.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalIngresos.TabIndex = 7
        Me.txtTotalIngresos.TabStop = False
        Me.txtTotalIngresos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIngresosEfectivo
        '
        Me.txtIngresosEfectivo.AcceptsReturn = True
        Me.txtIngresosEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIngresosEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIngresosEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIngresosEfectivo.Location = New System.Drawing.Point(205, 192)
        Me.txtIngresosEfectivo.MaxLength = 0
        Me.txtIngresosEfectivo.Name = "txtIngresosEfectivo"
        Me.txtIngresosEfectivo.ReadOnly = True
        Me.txtIngresosEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIngresosEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtIngresosEfectivo.TabIndex = 6
        Me.txtIngresosEfectivo.TabStop = False
        Me.txtIngresosEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtVentasTarjeta
        '
        Me.txtVentasTarjeta.AcceptsReturn = True
        Me.txtVentasTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtVentasTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVentasTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVentasTarjeta.Location = New System.Drawing.Point(438, 62)
        Me.txtVentasTarjeta.MaxLength = 0
        Me.txtVentasTarjeta.Name = "txtVentasTarjeta"
        Me.txtVentasTarjeta.ReadOnly = True
        Me.txtVentasTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVentasTarjeta.Size = New System.Drawing.Size(105, 20)
        Me.txtVentasTarjeta.TabIndex = 15
        Me.txtVentasTarjeta.TabStop = False
        Me.txtVentasTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtVentasCredito
        '
        Me.txtVentasCredito.AcceptsReturn = True
        Me.txtVentasCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtVentasCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVentasCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVentasCredito.Location = New System.Drawing.Point(556, 62)
        Me.txtVentasCredito.MaxLength = 0
        Me.txtVentasCredito.Name = "txtVentasCredito"
        Me.txtVentasCredito.ReadOnly = True
        Me.txtVentasCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVentasCredito.Size = New System.Drawing.Size(105, 20)
        Me.txtVentasCredito.TabIndex = 14
        Me.txtVentasCredito.TabStop = False
        Me.txtVentasCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPagosCxCEfectivo
        '
        Me.txtPagosCxCEfectivo.AcceptsReturn = True
        Me.txtPagosCxCEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPagosCxCEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPagosCxCEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPagosCxCEfectivo.Location = New System.Drawing.Point(205, 140)
        Me.txtPagosCxCEfectivo.MaxLength = 0
        Me.txtPagosCxCEfectivo.Name = "txtPagosCxCEfectivo"
        Me.txtPagosCxCEfectivo.ReadOnly = True
        Me.txtPagosCxCEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPagosCxCEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtPagosCxCEfectivo.TabIndex = 4
        Me.txtPagosCxCEfectivo.TabStop = False
        Me.txtPagosCxCEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(44, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(153, 17)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Pagos CXC:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(90, 231)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(107, 17)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Compras:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(44, 399)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(153, 17)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Total de Egresos en Efectivo"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComprasEfectivo
        '
        Me.txtComprasEfectivo.AcceptsReturn = True
        Me.txtComprasEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtComprasEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComprasEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComprasEfectivo.Location = New System.Drawing.Point(205, 230)
        Me.txtComprasEfectivo.MaxLength = 0
        Me.txtComprasEfectivo.Name = "txtComprasEfectivo"
        Me.txtComprasEfectivo.ReadOnly = True
        Me.txtComprasEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComprasEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtComprasEfectivo.TabIndex = 8
        Me.txtComprasEfectivo.TabStop = False
        Me.txtComprasEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalEgresos
        '
        Me.txtTotalEgresos.AcceptsReturn = True
        Me.txtTotalEgresos.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalEgresos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalEgresos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalEgresos.Location = New System.Drawing.Point(205, 398)
        Me.txtTotalEgresos.MaxLength = 0
        Me.txtTotalEgresos.Name = "txtTotalEgresos"
        Me.txtTotalEgresos.ReadOnly = True
        Me.txtTotalEgresos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalEgresos.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalEgresos.TabIndex = 12
        Me.txtTotalEgresos.TabStop = False
        Me.txtTotalEgresos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtComprasCredito
        '
        Me.txtComprasCredito.AcceptsReturn = True
        Me.txtComprasCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtComprasCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComprasCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComprasCredito.Location = New System.Drawing.Point(556, 230)
        Me.txtComprasCredito.MaxLength = 0
        Me.txtComprasCredito.Name = "txtComprasCredito"
        Me.txtComprasCredito.ReadOnly = True
        Me.txtComprasCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComprasCredito.Size = New System.Drawing.Size(105, 20)
        Me.txtComprasCredito.TabIndex = 22
        Me.txtComprasCredito.TabStop = False
        Me.txtComprasCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(76, 309)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(121, 17)
        Me.Label15.TabIndex = 37
        Me.Label15.Text = "Egresos varios:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEgresosEfectivo
        '
        Me.txtEgresosEfectivo.AcceptsReturn = True
        Me.txtEgresosEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEgresosEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEgresosEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEgresosEfectivo.Location = New System.Drawing.Point(205, 308)
        Me.txtEgresosEfectivo.MaxLength = 0
        Me.txtEgresosEfectivo.Name = "txtEgresosEfectivo"
        Me.txtEgresosEfectivo.ReadOnly = True
        Me.txtEgresosEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEgresosEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtEgresosEfectivo.TabIndex = 9
        Me.txtEgresosEfectivo.TabStop = False
        Me.txtEgresosEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPagosCxPEfectivo
        '
        Me.txtPagosCxPEfectivo.AcceptsReturn = True
        Me.txtPagosCxPEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPagosCxPEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPagosCxPEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPagosCxPEfectivo.Location = New System.Drawing.Point(205, 256)
        Me.txtPagosCxPEfectivo.MaxLength = 0
        Me.txtPagosCxPEfectivo.Name = "txtPagosCxPEfectivo"
        Me.txtPagosCxPEfectivo.ReadOnly = True
        Me.txtPagosCxPEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPagosCxPEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtPagosCxPEfectivo.TabIndex = 10
        Me.txtPagosCxPEfectivo.TabStop = False
        Me.txtPagosCxPEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(50, 257)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(147, 17)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "Pagos CxP:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComprasBancos
        '
        Me.txtComprasBancos.AcceptsReturn = True
        Me.txtComprasBancos.BackColor = System.Drawing.SystemColors.Window
        Me.txtComprasBancos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComprasBancos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComprasBancos.Location = New System.Drawing.Point(321, 230)
        Me.txtComprasBancos.MaxLength = 0
        Me.txtComprasBancos.Name = "txtComprasBancos"
        Me.txtComprasBancos.ReadOnly = True
        Me.txtComprasBancos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComprasBancos.Size = New System.Drawing.Size(105, 20)
        Me.txtComprasBancos.TabIndex = 23
        Me.txtComprasBancos.TabStop = False
        Me.txtComprasBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalVentas
        '
        Me.txtTotalVentas.AcceptsReturn = True
        Me.txtTotalVentas.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalVentas.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalVentas.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalVentas.Location = New System.Drawing.Point(674, 62)
        Me.txtTotalVentas.MaxLength = 0
        Me.txtTotalVentas.Name = "txtTotalVentas"
        Me.txtTotalVentas.ReadOnly = True
        Me.txtTotalVentas.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalVentas.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalVentas.TabIndex = 20
        Me.txtTotalVentas.TabStop = False
        Me.txtTotalVentas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalCompras
        '
        Me.txtTotalCompras.AcceptsReturn = True
        Me.txtTotalCompras.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalCompras.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalCompras.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalCompras.Location = New System.Drawing.Point(674, 230)
        Me.txtTotalCompras.MaxLength = 0
        Me.txtTotalCompras.Name = "txtTotalCompras"
        Me.txtTotalCompras.ReadOnly = True
        Me.txtTotalCompras.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalCompras.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalCompras.TabIndex = 24
        Me.txtTotalCompras.TabStop = False
        Me.txtTotalCompras.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRetencionIVA
        '
        Me.txtRetencionIVA.AcceptsReturn = True
        Me.txtRetencionIVA.BackColor = System.Drawing.SystemColors.Window
        Me.txtRetencionIVA.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRetencionIVA.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRetencionIVA.Location = New System.Drawing.Point(857, 88)
        Me.txtRetencionIVA.MaxLength = 0
        Me.txtRetencionIVA.Name = "txtRetencionIVA"
        Me.txtRetencionIVA.ReadOnly = True
        Me.txtRetencionIVA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRetencionIVA.Size = New System.Drawing.Size(105, 20)
        Me.txtRetencionIVA.TabIndex = 16
        Me.txtRetencionIVA.TabStop = False
        Me.txtRetencionIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(772, 89)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(79, 17)
        Me.Label19.TabIndex = 53
        Me.Label19.Text = "Retención:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComision
        '
        Me.txtComision.AcceptsReturn = True
        Me.txtComision.BackColor = System.Drawing.SystemColors.Window
        Me.txtComision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComision.Location = New System.Drawing.Point(857, 114)
        Me.txtComision.MaxLength = 0
        Me.txtComision.Name = "txtComision"
        Me.txtComision.ReadOnly = True
        Me.txtComision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComision.Size = New System.Drawing.Size(105, 20)
        Me.txtComision.TabIndex = 17
        Me.txtComision.TabStop = False
        Me.txtComision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLiquidacionTarjeta
        '
        Me.txtLiquidacionTarjeta.AcceptsReturn = True
        Me.txtLiquidacionTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtLiquidacionTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLiquidacionTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLiquidacionTarjeta.Location = New System.Drawing.Point(857, 140)
        Me.txtLiquidacionTarjeta.MaxLength = 0
        Me.txtLiquidacionTarjeta.Name = "txtLiquidacionTarjeta"
        Me.txtLiquidacionTarjeta.ReadOnly = True
        Me.txtLiquidacionTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLiquidacionTarjeta.Size = New System.Drawing.Size(105, 20)
        Me.txtLiquidacionTarjeta.TabIndex = 18
        Me.txtLiquidacionTarjeta.TabStop = False
        Me.txtLiquidacionTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtVentasBancos
        '
        Me.txtVentasBancos.AcceptsReturn = True
        Me.txtVentasBancos.BackColor = System.Drawing.SystemColors.Window
        Me.txtVentasBancos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVentasBancos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVentasBancos.Location = New System.Drawing.Point(321, 62)
        Me.txtVentasBancos.MaxLength = 0
        Me.txtVentasBancos.Name = "txtVentasBancos"
        Me.txtVentasBancos.ReadOnly = True
        Me.txtVentasBancos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVentasBancos.Size = New System.Drawing.Size(105, 20)
        Me.txtVentasBancos.TabIndex = 19
        Me.txtVentasBancos.TabStop = False
        Me.txtVentasBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDevolucionesProveedores
        '
        Me.txtDevolucionesProveedores.AcceptsReturn = True
        Me.txtDevolucionesProveedores.BackColor = System.Drawing.SystemColors.Window
        Me.txtDevolucionesProveedores.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDevolucionesProveedores.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDevolucionesProveedores.Location = New System.Drawing.Point(205, 166)
        Me.txtDevolucionesProveedores.MaxLength = 0
        Me.txtDevolucionesProveedores.Name = "txtDevolucionesProveedores"
        Me.txtDevolucionesProveedores.ReadOnly = True
        Me.txtDevolucionesProveedores.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDevolucionesProveedores.Size = New System.Drawing.Size(105, 20)
        Me.txtDevolucionesProveedores.TabIndex = 5
        Me.txtDevolucionesProveedores.TabStop = False
        Me.txtDevolucionesProveedores.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(47, 167)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(150, 17)
        Me.Label24.TabIndex = 65
        Me.Label24.Text = "Devoluciones Proveedores:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDevolucionesClientes
        '
        Me.txtDevolucionesClientes.AcceptsReturn = True
        Me.txtDevolucionesClientes.BackColor = System.Drawing.SystemColors.Window
        Me.txtDevolucionesClientes.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDevolucionesClientes.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDevolucionesClientes.Location = New System.Drawing.Point(205, 282)
        Me.txtDevolucionesClientes.MaxLength = 0
        Me.txtDevolucionesClientes.Name = "txtDevolucionesClientes"
        Me.txtDevolucionesClientes.ReadOnly = True
        Me.txtDevolucionesClientes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDevolucionesClientes.Size = New System.Drawing.Size(105, 20)
        Me.txtDevolucionesClientes.TabIndex = 11
        Me.txtDevolucionesClientes.TabStop = False
        Me.txtDevolucionesClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(47, 283)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(150, 17)
        Me.Label25.TabIndex = 67
        Me.Label25.Text = "Devoluciones a Clientes:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnReporte
        '
        Me.btnReporte.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnReporte.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnReporte.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnReporte.Location = New System.Drawing.Point(12, 12)
        Me.btnReporte.Name = "btnReporte"
        Me.btnReporte.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnReporte.Size = New System.Drawing.Size(64, 21)
        Me.btnReporte.TabIndex = 70
        Me.btnReporte.TabStop = False
        Me.btnReporte.Text = "&Reporte"
        Me.btnReporte.UseVisualStyleBackColor = False
        '
        'txtTotalIngresosTarjeta
        '
        Me.txtTotalIngresosTarjeta.AcceptsReturn = True
        Me.txtTotalIngresosTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalIngresosTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalIngresosTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalIngresosTarjeta.Location = New System.Drawing.Point(857, 62)
        Me.txtTotalIngresosTarjeta.MaxLength = 0
        Me.txtTotalIngresosTarjeta.Name = "txtTotalIngresosTarjeta"
        Me.txtTotalIngresosTarjeta.ReadOnly = True
        Me.txtTotalIngresosTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalIngresosTarjeta.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalIngresosTarjeta.TabIndex = 71
        Me.txtTotalIngresosTarjeta.TabStop = False
        Me.txtTotalIngresosTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(814, 63)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(37, 17)
        Me.Label26.TabIndex = 72
        Me.Label26.Text = "Total:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAdelantosApartadoEfectivo
        '
        Me.txtAdelantosApartadoEfectivo.AcceptsReturn = True
        Me.txtAdelantosApartadoEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdelantosApartadoEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdelantosApartadoEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdelantosApartadoEfectivo.Location = New System.Drawing.Point(205, 88)
        Me.txtAdelantosApartadoEfectivo.MaxLength = 0
        Me.txtAdelantosApartadoEfectivo.Name = "txtAdelantosApartadoEfectivo"
        Me.txtAdelantosApartadoEfectivo.ReadOnly = True
        Me.txtAdelantosApartadoEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdelantosApartadoEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtAdelantosApartadoEfectivo.TabIndex = 73
        Me.txtAdelantosApartadoEfectivo.TabStop = False
        Me.txtAdelantosApartadoEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(31, 89)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(166, 17)
        Me.Label22.TabIndex = 74
        Me.Label22.Text = "Adelantos Apartados:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAdelantosApartadoBancos
        '
        Me.txtAdelantosApartadoBancos.AcceptsReturn = True
        Me.txtAdelantosApartadoBancos.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdelantosApartadoBancos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdelantosApartadoBancos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdelantosApartadoBancos.Location = New System.Drawing.Point(321, 88)
        Me.txtAdelantosApartadoBancos.MaxLength = 0
        Me.txtAdelantosApartadoBancos.Name = "txtAdelantosApartadoBancos"
        Me.txtAdelantosApartadoBancos.ReadOnly = True
        Me.txtAdelantosApartadoBancos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdelantosApartadoBancos.Size = New System.Drawing.Size(105, 20)
        Me.txtAdelantosApartadoBancos.TabIndex = 75
        Me.txtAdelantosApartadoBancos.TabStop = False
        Me.txtAdelantosApartadoBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdelantosApartadoTarjeta
        '
        Me.txtAdelantosApartadoTarjeta.AcceptsReturn = True
        Me.txtAdelantosApartadoTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdelantosApartadoTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdelantosApartadoTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdelantosApartadoTarjeta.Location = New System.Drawing.Point(438, 88)
        Me.txtAdelantosApartadoTarjeta.MaxLength = 0
        Me.txtAdelantosApartadoTarjeta.Name = "txtAdelantosApartadoTarjeta"
        Me.txtAdelantosApartadoTarjeta.ReadOnly = True
        Me.txtAdelantosApartadoTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdelantosApartadoTarjeta.Size = New System.Drawing.Size(105, 20)
        Me.txtAdelantosApartadoTarjeta.TabIndex = 77
        Me.txtAdelantosApartadoTarjeta.TabStop = False
        Me.txtAdelantosApartadoTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdelantosOrdenTarjeta
        '
        Me.txtAdelantosOrdenTarjeta.AcceptsReturn = True
        Me.txtAdelantosOrdenTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdelantosOrdenTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdelantosOrdenTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdelantosOrdenTarjeta.Location = New System.Drawing.Point(438, 114)
        Me.txtAdelantosOrdenTarjeta.MaxLength = 0
        Me.txtAdelantosOrdenTarjeta.Name = "txtAdelantosOrdenTarjeta"
        Me.txtAdelantosOrdenTarjeta.ReadOnly = True
        Me.txtAdelantosOrdenTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdelantosOrdenTarjeta.Size = New System.Drawing.Size(105, 20)
        Me.txtAdelantosOrdenTarjeta.TabIndex = 83
        Me.txtAdelantosOrdenTarjeta.TabStop = False
        Me.txtAdelantosOrdenTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdelantosOrdenBancos
        '
        Me.txtAdelantosOrdenBancos.AcceptsReturn = True
        Me.txtAdelantosOrdenBancos.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdelantosOrdenBancos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdelantosOrdenBancos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdelantosOrdenBancos.Location = New System.Drawing.Point(321, 114)
        Me.txtAdelantosOrdenBancos.MaxLength = 0
        Me.txtAdelantosOrdenBancos.Name = "txtAdelantosOrdenBancos"
        Me.txtAdelantosOrdenBancos.ReadOnly = True
        Me.txtAdelantosOrdenBancos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdelantosOrdenBancos.Size = New System.Drawing.Size(105, 20)
        Me.txtAdelantosOrdenBancos.TabIndex = 81
        Me.txtAdelantosOrdenBancos.TabStop = False
        Me.txtAdelantosOrdenBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdelantosOrdenEfectivo
        '
        Me.txtAdelantosOrdenEfectivo.AcceptsReturn = True
        Me.txtAdelantosOrdenEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdelantosOrdenEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdelantosOrdenEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdelantosOrdenEfectivo.Location = New System.Drawing.Point(205, 114)
        Me.txtAdelantosOrdenEfectivo.MaxLength = 0
        Me.txtAdelantosOrdenEfectivo.Name = "txtAdelantosOrdenEfectivo"
        Me.txtAdelantosOrdenEfectivo.ReadOnly = True
        Me.txtAdelantosOrdenEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdelantosOrdenEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtAdelantosOrdenEfectivo.TabIndex = 79
        Me.txtAdelantosOrdenEfectivo.TabStop = False
        Me.txtAdelantosOrdenEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(12, 115)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(185, 17)
        Me.Label30.TabIndex = 80
        Me.Label30.Text = "Adelantos Ordenes de Servicio:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPagosCxCBancos
        '
        Me.txtPagosCxCBancos.AcceptsReturn = True
        Me.txtPagosCxCBancos.BackColor = System.Drawing.SystemColors.Window
        Me.txtPagosCxCBancos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPagosCxCBancos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPagosCxCBancos.Location = New System.Drawing.Point(321, 140)
        Me.txtPagosCxCBancos.MaxLength = 0
        Me.txtPagosCxCBancos.Name = "txtPagosCxCBancos"
        Me.txtPagosCxCBancos.ReadOnly = True
        Me.txtPagosCxCBancos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPagosCxCBancos.Size = New System.Drawing.Size(105, 20)
        Me.txtPagosCxCBancos.TabIndex = 85
        Me.txtPagosCxCBancos.TabStop = False
        Me.txtPagosCxCBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPagosCxCTarjeta
        '
        Me.txtPagosCxCTarjeta.AcceptsReturn = True
        Me.txtPagosCxCTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtPagosCxCTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPagosCxCTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPagosCxCTarjeta.Location = New System.Drawing.Point(438, 140)
        Me.txtPagosCxCTarjeta.MaxLength = 0
        Me.txtPagosCxCTarjeta.Name = "txtPagosCxCTarjeta"
        Me.txtPagosCxCTarjeta.ReadOnly = True
        Me.txtPagosCxCTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPagosCxCTarjeta.Size = New System.Drawing.Size(105, 20)
        Me.txtPagosCxCTarjeta.TabIndex = 87
        Me.txtPagosCxCTarjeta.TabStop = False
        Me.txtPagosCxCTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPagosCxPBancos
        '
        Me.txtPagosCxPBancos.AcceptsReturn = True
        Me.txtPagosCxPBancos.BackColor = System.Drawing.SystemColors.Window
        Me.txtPagosCxPBancos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPagosCxPBancos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPagosCxPBancos.Location = New System.Drawing.Point(321, 257)
        Me.txtPagosCxPBancos.MaxLength = 0
        Me.txtPagosCxPBancos.Name = "txtPagosCxPBancos"
        Me.txtPagosCxPBancos.ReadOnly = True
        Me.txtPagosCxPBancos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPagosCxPBancos.Size = New System.Drawing.Size(105, 20)
        Me.txtPagosCxPBancos.TabIndex = 89
        Me.txtPagosCxPBancos.TabStop = False
        Me.txtPagosCxPBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalAdelantosApartado
        '
        Me.txtTotalAdelantosApartado.AcceptsReturn = True
        Me.txtTotalAdelantosApartado.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalAdelantosApartado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalAdelantosApartado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalAdelantosApartado.Location = New System.Drawing.Point(674, 88)
        Me.txtTotalAdelantosApartado.MaxLength = 0
        Me.txtTotalAdelantosApartado.Name = "txtTotalAdelantosApartado"
        Me.txtTotalAdelantosApartado.ReadOnly = True
        Me.txtTotalAdelantosApartado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalAdelantosApartado.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalAdelantosApartado.TabIndex = 91
        Me.txtTotalAdelantosApartado.TabStop = False
        Me.txtTotalAdelantosApartado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalAdelantosOrden
        '
        Me.txtTotalAdelantosOrden.AcceptsReturn = True
        Me.txtTotalAdelantosOrden.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalAdelantosOrden.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalAdelantosOrden.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalAdelantosOrden.Location = New System.Drawing.Point(674, 114)
        Me.txtTotalAdelantosOrden.MaxLength = 0
        Me.txtTotalAdelantosOrden.Name = "txtTotalAdelantosOrden"
        Me.txtTotalAdelantosOrden.ReadOnly = True
        Me.txtTotalAdelantosOrden.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalAdelantosOrden.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalAdelantosOrden.TabIndex = 93
        Me.txtTotalAdelantosOrden.TabStop = False
        Me.txtTotalAdelantosOrden.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnTiquete
        '
        Me.btnTiquete.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnTiquete.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnTiquete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnTiquete.Location = New System.Drawing.Point(76, 12)
        Me.btnTiquete.Name = "btnTiquete"
        Me.btnTiquete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnTiquete.Size = New System.Drawing.Size(64, 21)
        Me.btnTiquete.TabIndex = 95
        Me.btnTiquete.TabStop = False
        Me.btnTiquete.Text = "&Tiquete"
        Me.btnTiquete.UseVisualStyleBackColor = False
        '
        'txtEfectivoCaja
        '
        Me.txtEfectivoCaja.AcceptsReturn = True
        Me.txtEfectivoCaja.BackColor = System.Drawing.SystemColors.Window
        Me.txtEfectivoCaja.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEfectivoCaja.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEfectivoCaja.Location = New System.Drawing.Point(857, 309)
        Me.txtEfectivoCaja.MaxLength = 0
        Me.txtEfectivoCaja.Name = "txtEfectivoCaja"
        Me.txtEfectivoCaja.ReadOnly = True
        Me.txtEfectivoCaja.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEfectivoCaja.Size = New System.Drawing.Size(105, 20)
        Me.txtEfectivoCaja.TabIndex = 132
        Me.txtEfectivoCaja.TabStop = False
        Me.txtEfectivoCaja.Text = "0.00"
        Me.txtEfectivoCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(717, 310)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(132, 17)
        Me.Label47.TabIndex = 133
        Me.Label47.Text = "Efectivo en caja"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSaldo
        '
        Me.txtSaldo.AcceptsReturn = True
        Me.txtSaldo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldo.Location = New System.Drawing.Point(857, 335)
        Me.txtSaldo.MaxLength = 0
        Me.txtSaldo.Name = "txtSaldo"
        Me.txtSaldo.ReadOnly = True
        Me.txtSaldo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldo.Size = New System.Drawing.Size(105, 20)
        Me.txtSaldo.TabIndex = 134
        Me.txtSaldo.TabStop = False
        Me.txtSaldo.Text = "0.00"
        Me.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.Transparent
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(717, 336)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(132, 17)
        Me.Label48.TabIndex = 135
        Me.Label48.Text = "Sobrante de efectivo"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(205, 41)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(105, 17)
        Me.Label49.TabIndex = 136
        Me.Label49.Text = "En efectivo"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(321, 41)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(105, 17)
        Me.Label23.TabIndex = 137
        Me.Label23.Text = "En bancos"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(438, 41)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(105, 17)
        Me.Label50.TabIndex = 138
        Me.Label50.Text = "Con tarjetas"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(674, 41)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(105, 17)
        Me.Label51.TabIndex = 139
        Me.Label51.Text = "Total"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(798, 115)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(53, 17)
        Me.Label20.TabIndex = 55
        Me.Label20.Text = "Comisión:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(787, 141)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(64, 17)
        Me.Label21.TabIndex = 57
        Me.Label21.Text = "Saldo:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotalPagoCxC
        '
        Me.txtTotalPagoCxC.AcceptsReturn = True
        Me.txtTotalPagoCxC.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalPagoCxC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalPagoCxC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalPagoCxC.Location = New System.Drawing.Point(674, 140)
        Me.txtTotalPagoCxC.MaxLength = 0
        Me.txtTotalPagoCxC.Name = "txtTotalPagoCxC"
        Me.txtTotalPagoCxC.ReadOnly = True
        Me.txtTotalPagoCxC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalPagoCxC.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalPagoCxC.TabIndex = 140
        Me.txtTotalPagoCxC.TabStop = False
        Me.txtTotalPagoCxC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(556, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(105, 17)
        Me.Label1.TabIndex = 141
        Me.Label1.Text = "Con crédito"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalPagoCxP
        '
        Me.txtTotalPagoCxP.AcceptsReturn = True
        Me.txtTotalPagoCxP.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalPagoCxP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalPagoCxP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalPagoCxP.Location = New System.Drawing.Point(674, 256)
        Me.txtTotalPagoCxP.MaxLength = 0
        Me.txtTotalPagoCxP.Name = "txtTotalPagoCxP"
        Me.txtTotalPagoCxP.ReadOnly = True
        Me.txtTotalPagoCxP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalPagoCxP.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalPagoCxP.TabIndex = 142
        Me.txtTotalPagoCxP.TabStop = False
        Me.txtTotalPagoCxP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(799, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(163, 17)
        Me.Label2.TabIndex = 143
        Me.Label2.Text = "Liquidación Tarjetas"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCierreEfectivoProx
        '
        Me.txtCierreEfectivoProx.AcceptsReturn = True
        Me.txtCierreEfectivoProx.BackColor = System.Drawing.SystemColors.Window
        Me.txtCierreEfectivoProx.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCierreEfectivoProx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCierreEfectivoProx.Location = New System.Drawing.Point(857, 387)
        Me.txtCierreEfectivoProx.MaxLength = 0
        Me.txtCierreEfectivoProx.Name = "txtCierreEfectivoProx"
        Me.txtCierreEfectivoProx.ReadOnly = True
        Me.txtCierreEfectivoProx.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCierreEfectivoProx.Size = New System.Drawing.Size(105, 20)
        Me.txtCierreEfectivoProx.TabIndex = 146
        Me.txtCierreEfectivoProx.TabStop = False
        Me.txtCierreEfectivoProx.Text = "0.00"
        Me.txtCierreEfectivoProx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(677, 388)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(172, 17)
        Me.Label7.TabIndex = 147
        Me.Label7.Text = "Nuevo inicio de efectivo"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRetiroEfectivo
        '
        Me.txtRetiroEfectivo.AcceptsReturn = True
        Me.txtRetiroEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRetiroEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRetiroEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRetiroEfectivo.Location = New System.Drawing.Point(857, 361)
        Me.txtRetiroEfectivo.MaxLength = 0
        Me.txtRetiroEfectivo.Name = "txtRetiroEfectivo"
        Me.txtRetiroEfectivo.ReadOnly = True
        Me.txtRetiroEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRetiroEfectivo.Size = New System.Drawing.Size(105, 20)
        Me.txtRetiroEfectivo.TabIndex = 144
        Me.txtRetiroEfectivo.TabStop = False
        Me.txtRetiroEfectivo.Text = "0.00"
        Me.txtRetiroEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(717, 362)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(132, 17)
        Me.Label12.TabIndex = 145
        Me.Label12.Text = "Deposito Bancario"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtObservaciones
        '
        Me.txtObservaciones.AcceptsReturn = True
        Me.txtObservaciones.BackColor = System.Drawing.SystemColors.Window
        Me.txtObservaciones.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtObservaciones.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtObservaciones.Location = New System.Drawing.Point(97, 460)
        Me.txtObservaciones.MaxLength = 0
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.ReadOnly = True
        Me.txtObservaciones.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtObservaciones.Size = New System.Drawing.Size(865, 20)
        Me.txtObservaciones.TabIndex = 148
        Me.txtObservaciones.TabStop = False
        Me.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(9, 461)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(82, 17)
        Me.Label13.TabIndex = 149
        Me.Label13.Text = "Observaciones"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvDetalleEfectivoCierreCaja
        '
        Me.dgvDetalleEfectivoCierreCaja.AllowUserToAddRows = False
        Me.dgvDetalleEfectivoCierreCaja.AllowUserToDeleteRows = False
        Me.dgvDetalleEfectivoCierreCaja.AllowUserToResizeColumns = False
        Me.dgvDetalleEfectivoCierreCaja.AllowUserToResizeRows = False
        Me.dgvDetalleEfectivoCierreCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetalleEfectivoCierreCaja.Location = New System.Drawing.Point(341, 290)
        Me.dgvDetalleEfectivoCierreCaja.Name = "dgvDetalleEfectivoCierreCaja"
        Me.dgvDetalleEfectivoCierreCaja.ReadOnly = True
        Me.dgvDetalleEfectivoCierreCaja.RowHeadersVisible = False
        Me.dgvDetalleEfectivoCierreCaja.Size = New System.Drawing.Size(350, 150)
        Me.dgvDetalleEfectivoCierreCaja.TabIndex = 150
        '
        'FrmConsultaCierreCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(978, 497)
        Me.Controls.Add(Me.dgvDetalleEfectivoCierreCaja)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtCierreEfectivoProx)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtRetiroEfectivo)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTotalPagoCxP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTotalPagoCxC)
        Me.Controls.Add(Me.Label51)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label49)
        Me.Controls.Add(Me.txtSaldo)
        Me.Controls.Add(Me.Label48)
        Me.Controls.Add(Me.txtEfectivoCaja)
        Me.Controls.Add(Me.Label47)
        Me.Controls.Add(Me.btnTiquete)
        Me.Controls.Add(Me.txtTotalAdelantosOrden)
        Me.Controls.Add(Me.txtTotalAdelantosApartado)
        Me.Controls.Add(Me.txtPagosCxPBancos)
        Me.Controls.Add(Me.txtPagosCxCTarjeta)
        Me.Controls.Add(Me.txtPagosCxCBancos)
        Me.Controls.Add(Me.txtAdelantosOrdenTarjeta)
        Me.Controls.Add(Me.txtAdelantosOrdenBancos)
        Me.Controls.Add(Me.txtAdelantosOrdenEfectivo)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.txtAdelantosApartadoTarjeta)
        Me.Controls.Add(Me.txtAdelantosApartadoBancos)
        Me.Controls.Add(Me.txtAdelantosApartadoEfectivo)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtTotalIngresosTarjeta)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.btnReporte)
        Me.Controls.Add(Me.txtDevolucionesClientes)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtDevolucionesProveedores)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtVentasBancos)
        Me.Controls.Add(Me.txtLiquidacionTarjeta)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtComision)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtRetencionIVA)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtTotalCompras)
        Me.Controls.Add(Me.txtTotalVentas)
        Me.Controls.Add(Me.txtComprasBancos)
        Me.Controls.Add(Me.txtPagosCxPEfectivo)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtPagosCxCEfectivo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtEgresosEfectivo)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtVentasEfectivo)
        Me.Controls.Add(Me.txtTotalIngresos)
        Me.Controls.Add(Me.txtIngresosEfectivo)
        Me.Controls.Add(Me.txtVentasTarjeta)
        Me.Controls.Add(Me.txtVentasCredito)
        Me.Controls.Add(Me.txtComprasCredito)
        Me.Controls.Add(Me.txtFondoInicio)
        Me.Controls.Add(Me.txtTotalEfectivo)
        Me.Controls.Add(Me.txtTotalEgresos)
        Me.Controls.Add(Me.txtComprasEfectivo)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmConsultaCierreCaja"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cierre Diario"
        CType(Me.dgvDetalleEfectivoCierreCaja, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtVentasEfectivo As System.Windows.Forms.TextBox
    Public WithEvents txtTotalIngresos As System.Windows.Forms.TextBox
    Public WithEvents txtIngresosEfectivo As System.Windows.Forms.TextBox
    Public WithEvents txtVentasTarjeta As System.Windows.Forms.TextBox
    Public WithEvents txtVentasCredito As System.Windows.Forms.TextBox
    Public WithEvents txtPagosCxCEfectivo As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtComprasEfectivo As System.Windows.Forms.TextBox
    Public WithEvents txtTotalEgresos As System.Windows.Forms.TextBox
    Public WithEvents txtComprasCredito As System.Windows.Forms.TextBox
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents txtEgresosEfectivo As System.Windows.Forms.TextBox
    Public WithEvents txtPagosCxPEfectivo As System.Windows.Forms.TextBox
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents txtComprasBancos As System.Windows.Forms.TextBox
    Public WithEvents txtTotalVentas As System.Windows.Forms.TextBox
    Public WithEvents txtTotalCompras As System.Windows.Forms.TextBox
    Public WithEvents txtRetencionIVA As System.Windows.Forms.TextBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents txtComision As System.Windows.Forms.TextBox
    Public WithEvents txtLiquidacionTarjeta As System.Windows.Forms.TextBox
    Public WithEvents txtVentasBancos As System.Windows.Forms.TextBox
    Public WithEvents txtDevolucionesProveedores As System.Windows.Forms.TextBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents txtDevolucionesClientes As System.Windows.Forms.TextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents btnReporte As System.Windows.Forms.Button
    Public WithEvents txtTotalIngresosTarjeta As System.Windows.Forms.TextBox
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents txtAdelantosApartadoEfectivo As TextBox
    Public WithEvents Label22 As Label
    Public WithEvents txtAdelantosApartadoBancos As TextBox
    Public WithEvents txtAdelantosApartadoTarjeta As TextBox
    Public WithEvents txtAdelantosOrdenTarjeta As TextBox
    Public WithEvents txtAdelantosOrdenBancos As TextBox
    Public WithEvents txtAdelantosOrdenEfectivo As TextBox
    Public WithEvents Label30 As Label
    Public WithEvents txtPagosCxCBancos As TextBox
    Public WithEvents txtPagosCxCTarjeta As TextBox
    Public WithEvents txtPagosCxPBancos As TextBox
    Public WithEvents txtTotalAdelantosApartado As TextBox
    Public WithEvents txtTotalAdelantosOrden As TextBox
    Public WithEvents btnTiquete As Button
    Public WithEvents txtEfectivoCaja As TextBox
    Public WithEvents Label47 As Label
    Public WithEvents txtSaldo As TextBox
    Public WithEvents Label48 As Label
    Public WithEvents Label49 As Label
    Public WithEvents Label23 As Label
    Public WithEvents Label50 As Label
    Public WithEvents Label51 As Label
    Public WithEvents Label20 As Label
    Public WithEvents Label21 As Label
    Public WithEvents txtTotalPagoCxC As TextBox
    Public WithEvents Label1 As Label
    Public WithEvents txtTotalPagoCxP As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents txtCierreEfectivoProx As TextBox
    Public WithEvents Label7 As Label
    Public WithEvents txtRetiroEfectivo As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents txtObservaciones As TextBox
    Public WithEvents Label13 As Label
    Friend WithEvents dgvDetalleEfectivoCierreCaja As DataGridView
End Class