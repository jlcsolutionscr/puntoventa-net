<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrincipal
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
    Public WithEvents mnuArchivoCierre As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuArchivoReporte As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuArchivoSalir As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuArchivo As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuCapturaFactura As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuCaptura As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMantCliente As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMantLinea As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMantProveedor As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMantProducto As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMantUsuario As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMantInv As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMant As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuApRCxP As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuCC As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuMenuPrincipal As System.Windows.Forms.MenuStrip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPrincipal))
        Me.mnuMenuPrincipal = New System.Windows.Forms.MenuStrip()
        Me.MnuArchivo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuArchivoCierre = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuArchivoIngreso = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuArchivoEgreso = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuArchivoReporte = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuArchivoCambio = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuArchivoSalir = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParam = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParamEmpresa = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParamBA = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParamCB = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParamPC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParamRegistro = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMant = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantCliente = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantLinea = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantProveedor = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantProducto = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantUsuario = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantCE = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantCI = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantCC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantInv = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMantVend = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCaptura = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCapturaProforma = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCapturaOrdenServicio = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCapturaApartado = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCapturaFactura = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDevolucionCliente = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCompra = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCompraRegistro = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCompraTraslado = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCompraAplicTraslado = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCompraAjusteInv = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCompraDevolucion = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDocElect = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDocElectFC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDocElectCDE = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDocElectRDE = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuApRCxC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuApRCxP = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAnRCxC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAnRCxP = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImpCxC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImpCxP = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBC = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBCMov = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuConta = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuContaAsiento = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuContaCierre = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuContaReporte = New System.Windows.Forms.ToolStripMenuItem()
        Me.picLoader = New System.Windows.Forms.PictureBox()
        Me.mnuMenuPrincipal.SuspendLayout()
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuMenuPrincipal
        '
        Me.mnuMenuPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuArchivo, Me.MnuParam, Me.MnuMant, Me.MnuCaptura, Me.MnuCompra, Me.MnuDocElect, Me.MnuCC, Me.MnuBC, Me.MnuConta})
        Me.mnuMenuPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.mnuMenuPrincipal.Name = "mnuMenuPrincipal"
        Me.mnuMenuPrincipal.Size = New System.Drawing.Size(1276, 24)
        Me.mnuMenuPrincipal.TabIndex = 1
        Me.mnuMenuPrincipal.Visible = False
        '
        'MnuArchivo
        '
        Me.MnuArchivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuArchivoCierre, Me.MnuArchivoIngreso, Me.MnuArchivoEgreso, Me.MnuArchivoReporte, Me.mnuArchivoCambio, Me.MnuArchivoSalir})
        Me.MnuArchivo.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.MnuArchivo.Name = "MnuArchivo"
        Me.MnuArchivo.Size = New System.Drawing.Size(60, 20)
        Me.MnuArchivo.Text = "Archivo"
        '
        'mnuArchivoCierre
        '
        Me.mnuArchivoCierre.Name = "mnuArchivoCierre"
        Me.mnuArchivoCierre.Size = New System.Drawing.Size(179, 22)
        Me.mnuArchivoCierre.Text = "Cierre de Caja"
        Me.mnuArchivoCierre.Visible = False
        '
        'MnuArchivoIngreso
        '
        Me.MnuArchivoIngreso.Name = "MnuArchivoIngreso"
        Me.MnuArchivoIngreso.Size = New System.Drawing.Size(179, 22)
        Me.MnuArchivoIngreso.Text = "Ingresos de efectivo"
        '
        'MnuArchivoEgreso
        '
        Me.MnuArchivoEgreso.Name = "MnuArchivoEgreso"
        Me.MnuArchivoEgreso.Size = New System.Drawing.Size(179, 22)
        Me.MnuArchivoEgreso.Text = "Egresos de efectivo"
        '
        'MnuArchivoReporte
        '
        Me.MnuArchivoReporte.Name = "MnuArchivoReporte"
        Me.MnuArchivoReporte.Size = New System.Drawing.Size(179, 22)
        Me.MnuArchivoReporte.Text = "Menu de Reportes"
        Me.MnuArchivoReporte.Visible = False
        '
        'mnuArchivoCambio
        '
        Me.mnuArchivoCambio.Name = "mnuArchivoCambio"
        Me.mnuArchivoCambio.Size = New System.Drawing.Size(179, 22)
        Me.mnuArchivoCambio.Text = "Cambio Contraseña"
        '
        'MnuArchivoSalir
        '
        Me.MnuArchivoSalir.Name = "MnuArchivoSalir"
        Me.MnuArchivoSalir.Size = New System.Drawing.Size(179, 22)
        Me.MnuArchivoSalir.Text = "Salir"
        '
        'MnuParam
        '
        Me.MnuParam.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuParamEmpresa, Me.MnuParamBA, Me.MnuParamCB, Me.MnuParamPC, Me.MnuParamRegistro})
        Me.MnuParam.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.MnuParam.Name = "MnuParam"
        Me.MnuParam.Size = New System.Drawing.Size(79, 20)
        Me.MnuParam.Text = "Parámetros"
        Me.MnuParam.Visible = False
        '
        'MnuParamEmpresa
        '
        Me.MnuParamEmpresa.Name = "MnuParamEmpresa"
        Me.MnuParamEmpresa.Size = New System.Drawing.Size(180, 22)
        Me.MnuParamEmpresa.Text = "Empresa"
        Me.MnuParamEmpresa.Visible = False
        '
        'MnuParamBA
        '
        Me.MnuParamBA.Name = "MnuParamBA"
        Me.MnuParamBA.Size = New System.Drawing.Size(180, 22)
        Me.MnuParamBA.Text = "Banco Adquiriente"
        Me.MnuParamBA.Visible = False
        '
        'MnuParamCB
        '
        Me.MnuParamCB.Name = "MnuParamCB"
        Me.MnuParamCB.Size = New System.Drawing.Size(180, 22)
        Me.MnuParamCB.Text = "Cuentas Bancarias"
        '
        'MnuParamPC
        '
        Me.MnuParamPC.Name = "MnuParamPC"
        Me.MnuParamPC.Size = New System.Drawing.Size(180, 22)
        Me.MnuParamPC.Text = "Parámetro Contable"
        Me.MnuParamPC.Visible = False
        '
        'MnuParamRegistro
        '
        Me.MnuParamRegistro.Name = "MnuParamRegistro"
        Me.MnuParamRegistro.Size = New System.Drawing.Size(180, 22)
        Me.MnuParamRegistro.Text = "Registrar Equipo"
        '
        'MnuMant
        '
        Me.MnuMant.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMantCliente, Me.MnuMantLinea, Me.MnuMantProveedor, Me.MnuMantProducto, Me.MnuMantUsuario, Me.MnuMantCE, Me.MnuMantCI, Me.MnuMantCC, Me.MnuMantInv, Me.MnuMantVend})
        Me.MnuMant.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.MnuMant.Name = "MnuMant"
        Me.MnuMant.Size = New System.Drawing.Size(101, 20)
        Me.MnuMant.Text = "Mantenimiento"
        Me.MnuMant.Visible = False
        '
        'MnuMantCliente
        '
        Me.MnuMantCliente.Name = "MnuMantCliente"
        Me.MnuMantCliente.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantCliente.Text = "Clientes"
        Me.MnuMantCliente.Visible = False
        '
        'MnuMantLinea
        '
        Me.MnuMantLinea.Name = "MnuMantLinea"
        Me.MnuMantLinea.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantLinea.Text = "Líneas"
        Me.MnuMantLinea.Visible = False
        '
        'MnuMantProveedor
        '
        Me.MnuMantProveedor.Name = "MnuMantProveedor"
        Me.MnuMantProveedor.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantProveedor.Text = "Proveedores"
        Me.MnuMantProveedor.Visible = False
        '
        'MnuMantProducto
        '
        Me.MnuMantProducto.Name = "MnuMantProducto"
        Me.MnuMantProducto.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantProducto.Text = "Productos"
        Me.MnuMantProducto.Visible = False
        '
        'MnuMantUsuario
        '
        Me.MnuMantUsuario.Name = "MnuMantUsuario"
        Me.MnuMantUsuario.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantUsuario.Text = "Usuarios"
        Me.MnuMantUsuario.Visible = False
        '
        'MnuMantCE
        '
        Me.MnuMantCE.Name = "MnuMantCE"
        Me.MnuMantCE.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantCE.Text = "Cuentas de Egresos"
        Me.MnuMantCE.Visible = False
        '
        'MnuMantCI
        '
        Me.MnuMantCI.Name = "MnuMantCI"
        Me.MnuMantCI.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantCI.Text = "Cuentas de Ingresos"
        Me.MnuMantCI.Visible = False
        '
        'MnuMantCC
        '
        Me.MnuMantCC.Name = "MnuMantCC"
        Me.MnuMantCC.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantCC.Text = "Cuentas Contables"
        Me.MnuMantCC.Visible = False
        '
        'MnuMantInv
        '
        Me.MnuMantInv.Name = "MnuMantInv"
        Me.MnuMantInv.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantInv.Text = "Inventario"
        Me.MnuMantInv.Visible = False
        '
        'MnuMantVend
        '
        Me.MnuMantVend.Name = "MnuMantVend"
        Me.MnuMantVend.Size = New System.Drawing.Size(180, 22)
        Me.MnuMantVend.Text = "Vendedores"
        Me.MnuMantVend.Visible = False
        '
        'MnuCaptura
        '
        Me.MnuCaptura.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuCapturaProforma, Me.MnuCapturaOrdenServicio, Me.MnuCapturaApartado, Me.MnuCapturaFactura, Me.MnuDevolucionCliente})
        Me.MnuCaptura.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.MnuCaptura.Name = "MnuCaptura"
        Me.MnuCaptura.Size = New System.Drawing.Size(81, 20)
        Me.MnuCaptura.Text = "Facturación"
        Me.MnuCaptura.Visible = False
        '
        'MnuCapturaProforma
        '
        Me.MnuCapturaProforma.Name = "MnuCapturaProforma"
        Me.MnuCapturaProforma.Size = New System.Drawing.Size(206, 22)
        Me.MnuCapturaProforma.Text = "Proformas"
        Me.MnuCapturaProforma.Visible = False
        '
        'MnuCapturaOrdenServicio
        '
        Me.MnuCapturaOrdenServicio.Name = "MnuCapturaOrdenServicio"
        Me.MnuCapturaOrdenServicio.Size = New System.Drawing.Size(206, 22)
        Me.MnuCapturaOrdenServicio.Text = "Ordenes de Servicio"
        Me.MnuCapturaOrdenServicio.Visible = False
        '
        'MnuCapturaApartado
        '
        Me.MnuCapturaApartado.Name = "MnuCapturaApartado"
        Me.MnuCapturaApartado.Size = New System.Drawing.Size(206, 22)
        Me.MnuCapturaApartado.Text = "Apartados"
        Me.MnuCapturaApartado.Visible = False
        '
        'MnuCapturaFactura
        '
        Me.MnuCapturaFactura.Name = "MnuCapturaFactura"
        Me.MnuCapturaFactura.Size = New System.Drawing.Size(206, 22)
        Me.MnuCapturaFactura.Text = "Facturación"
        Me.MnuCapturaFactura.Visible = False
        '
        'MnuDevolucionCliente
        '
        Me.MnuDevolucionCliente.Name = "MnuDevolucionCliente"
        Me.MnuDevolucionCliente.Size = New System.Drawing.Size(206, 22)
        Me.MnuDevolucionCliente.Text = "Devoluciones de Clientes"
        Me.MnuDevolucionCliente.Visible = False
        '
        'MnuCompra
        '
        Me.MnuCompra.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuCompraRegistro, Me.MnuCompraTraslado, Me.MnuCompraAplicTraslado, Me.MnuCompraAjusteInv, Me.MnuCompraDevolucion})
        Me.MnuCompra.Name = "MnuCompra"
        Me.MnuCompra.Size = New System.Drawing.Size(163, 20)
        Me.MnuCompra.Text = "Movimientos de mercancia"
        '
        'MnuCompraRegistro
        '
        Me.MnuCompraRegistro.Name = "MnuCompraRegistro"
        Me.MnuCompraRegistro.Size = New System.Drawing.Size(207, 22)
        Me.MnuCompraRegistro.Text = "Compras"
        '
        'MnuCompraTraslado
        '
        Me.MnuCompraTraslado.Name = "MnuCompraTraslado"
        Me.MnuCompraTraslado.Size = New System.Drawing.Size(207, 22)
        Me.MnuCompraTraslado.Text = "Ingreso de traslados"
        '
        'MnuCompraAplicTraslado
        '
        Me.MnuCompraAplicTraslado.Name = "MnuCompraAplicTraslado"
        Me.MnuCompraAplicTraslado.Size = New System.Drawing.Size(207, 22)
        Me.MnuCompraAplicTraslado.Text = "Aplicacion de traslados"
        '
        'MnuCompraAjusteInv
        '
        Me.MnuCompraAjusteInv.Name = "MnuCompraAjusteInv"
        Me.MnuCompraAjusteInv.Size = New System.Drawing.Size(207, 22)
        Me.MnuCompraAjusteInv.Text = "Ajustes de inventario"
        '
        'MnuCompraDevolucion
        '
        Me.MnuCompraDevolucion.Name = "MnuCompraDevolucion"
        Me.MnuCompraDevolucion.Size = New System.Drawing.Size(207, 22)
        Me.MnuCompraDevolucion.Text = "Devolucion de proveedor"
        '
        'MnuDocElect
        '
        Me.MnuDocElect.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuDocElectFC, Me.MnuDocElectCDE, Me.MnuDocElectRDE})
        Me.MnuDocElect.Name = "MnuDocElect"
        Me.MnuDocElect.Size = New System.Drawing.Size(154, 20)
        Me.MnuDocElect.Text = "Documentos Electrónicos"
        Me.MnuDocElect.Visible = False
        '
        'MnuDocElectFC
        '
        Me.MnuDocElectFC.Name = "MnuDocElectFC"
        Me.MnuDocElectFC.Size = New System.Drawing.Size(258, 22)
        Me.MnuDocElectFC.Text = "Generar factura de compra"
        Me.MnuDocElectFC.Visible = False
        '
        'MnuDocElectCDE
        '
        Me.MnuDocElectCDE.Name = "MnuDocElectCDE"
        Me.MnuDocElectCDE.Size = New System.Drawing.Size(258, 22)
        Me.MnuDocElectCDE.Text = "Consultar documentos pendientes"
        Me.MnuDocElectCDE.Visible = False
        '
        'MnuDocElectRDE
        '
        Me.MnuDocElectRDE.Name = "MnuDocElectRDE"
        Me.MnuDocElectRDE.Size = New System.Drawing.Size(258, 22)
        Me.MnuDocElectRDE.Text = "Consultar documentos procesados"
        Me.MnuDocElectRDE.Visible = False
        '
        'MnuCC
        '
        Me.MnuCC.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuApRCxC, Me.MnuApRCxP, Me.MnuAnRCxC, Me.MnuAnRCxP, Me.MnuImpCxC, Me.MnuImpCxP})
        Me.MnuCC.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.MnuCC.Name = "MnuCC"
        Me.MnuCC.Size = New System.Drawing.Size(103, 20)
        Me.MnuCC.Text = "Crédito y Cobro"
        Me.MnuCC.Visible = False
        '
        'MnuApRCxC
        '
        Me.MnuApRCxC.Name = "MnuApRCxC"
        Me.MnuApRCxC.Size = New System.Drawing.Size(289, 22)
        Me.MnuApRCxC.Text = "Aplicar Pago sobre Cuenta por Cobrar"
        Me.MnuApRCxC.Visible = False
        '
        'MnuApRCxP
        '
        Me.MnuApRCxP.Name = "MnuApRCxP"
        Me.MnuApRCxP.Size = New System.Drawing.Size(289, 22)
        Me.MnuApRCxP.Text = "Aplicar Pago sobre Cuenta por Pagar"
        Me.MnuApRCxP.Visible = False
        '
        'MnuAnRCxC
        '
        Me.MnuAnRCxC.Name = "MnuAnRCxC"
        Me.MnuAnRCxC.Size = New System.Drawing.Size(289, 22)
        Me.MnuAnRCxC.Text = "Anula Recibo de Cuenta por Cobrar"
        Me.MnuAnRCxC.Visible = False
        '
        'MnuAnRCxP
        '
        Me.MnuAnRCxP.Name = "MnuAnRCxP"
        Me.MnuAnRCxP.Size = New System.Drawing.Size(289, 22)
        Me.MnuAnRCxP.Text = "Anula Recibo de Cuenta por Pagar"
        Me.MnuAnRCxP.Visible = False
        '
        'MnuImpCxC
        '
        Me.MnuImpCxC.Name = "MnuImpCxC"
        Me.MnuImpCxC.Size = New System.Drawing.Size(289, 22)
        Me.MnuImpCxC.Text = "Reimprimir Recibo de Cuenta por Cobrar"
        Me.MnuImpCxC.Visible = False
        '
        'MnuImpCxP
        '
        Me.MnuImpCxP.Name = "MnuImpCxP"
        Me.MnuImpCxP.Size = New System.Drawing.Size(289, 22)
        Me.MnuImpCxP.Text = "Reimprimir Recibo de Cuenta por Pagar"
        Me.MnuImpCxP.Visible = False
        '
        'MnuBC
        '
        Me.MnuBC.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuBCMov})
        Me.MnuBC.Name = "MnuBC"
        Me.MnuBC.Size = New System.Drawing.Size(149, 20)
        Me.MnuBC.Text = "Bancos y Corresponsales"
        Me.MnuBC.Visible = False
        '
        'MnuBCMov
        '
        Me.MnuBCMov.Name = "MnuBCMov"
        Me.MnuBCMov.Size = New System.Drawing.Size(278, 22)
        Me.MnuBCMov.Text = "Ingreso/Actualización de Movimientos"
        Me.MnuBCMov.Visible = False
        '
        'MnuConta
        '
        Me.MnuConta.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuContaAsiento, Me.MnuContaCierre, Me.MnuContaReporte})
        Me.MnuConta.Name = "MnuConta"
        Me.MnuConta.Size = New System.Drawing.Size(87, 20)
        Me.MnuConta.Text = "Contabilidad"
        Me.MnuConta.Visible = False
        '
        'MnuContaAsiento
        '
        Me.MnuContaAsiento.Name = "MnuContaAsiento"
        Me.MnuContaAsiento.Size = New System.Drawing.Size(233, 22)
        Me.MnuContaAsiento.Text = "Ingreso de Asientos Contables"
        Me.MnuContaAsiento.Visible = False
        '
        'MnuContaCierre
        '
        Me.MnuContaCierre.Name = "MnuContaCierre"
        Me.MnuContaCierre.Size = New System.Drawing.Size(233, 22)
        Me.MnuContaCierre.Text = "Cierre Mensual"
        Me.MnuContaCierre.Visible = False
        '
        'MnuContaReporte
        '
        Me.MnuContaReporte.Name = "MnuContaReporte"
        Me.MnuContaReporte.Size = New System.Drawing.Size(233, 22)
        Me.MnuContaReporte.Text = "Menú de Reportes"
        Me.MnuContaReporte.Visible = False
        '
        'picLoader
        '
        Me.picLoader.BackColor = System.Drawing.Color.Transparent
        Me.picLoader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picLoader.ErrorImage = Nothing
        Me.picLoader.Image = CType(resources.GetObject("picLoader.Image"), System.Drawing.Image)
        Me.picLoader.InitialImage = Nothing
        Me.picLoader.Location = New System.Drawing.Point(84, 70)
        Me.picLoader.Name = "picLoader"
        Me.picLoader.Size = New System.Drawing.Size(70, 70)
        Me.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLoader.TabIndex = 3
        Me.picLoader.TabStop = False
        Me.picLoader.Visible = False
        '
        'FrmPrincipal
        '
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1276, 542)
        Me.Controls.Add(Me.picLoader)
        Me.Controls.Add(Me.mnuMenuPrincipal)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Location = New System.Drawing.Point(11, 57)
        Me.Name = "FrmPrincipal"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Menu Principal"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.mnuMenuPrincipal.ResumeLayout(False)
        Me.mnuMenuPrincipal.PerformLayout()
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuAnRCxP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuBC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuBCMov As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuArchivoCambio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConta As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuContaAsiento As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMantCI As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuApRCxC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAnRCxC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuImpCxC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMantCC As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuCapturaOrdenServicio As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuDevolucionCliente As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuParam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuParamPC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuParamBA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMantCE As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuContaCierre As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuContaReporte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuImpCxP As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuCapturaProforma As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMantVend As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picLoader As PictureBox
    Friend WithEvents MnuDocElect As ToolStripMenuItem
    Friend WithEvents MnuDocElectCDE As ToolStripMenuItem
    Friend WithEvents MnuDocElectFC As ToolStripMenuItem
    Friend WithEvents MnuDocElectRDE As ToolStripMenuItem
    Friend WithEvents MnuParamEmpresa As ToolStripMenuItem
    Friend WithEvents MnuParamRegistro As ToolStripMenuItem
    Public WithEvents MnuCapturaApartado As ToolStripMenuItem
    Friend WithEvents MnuParamCB As ToolStripMenuItem
    Friend WithEvents MnuArchivoIngreso As ToolStripMenuItem
    Friend WithEvents MnuArchivoEgreso As ToolStripMenuItem
    Friend WithEvents MnuCompra As ToolStripMenuItem
    Friend WithEvents MnuCompraRegistro As ToolStripMenuItem
    Friend WithEvents MnuCompraTraslado As ToolStripMenuItem
    Friend WithEvents MnuCompraAplicTraslado As ToolStripMenuItem
    Friend WithEvents MnuCompraAjusteInv As ToolStripMenuItem
    Friend WithEvents MnuCompraDevolucion As ToolStripMenuItem
End Class