<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDevolucionAProveedores
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
    Public WithEvents CmdAnular As System.Windows.Forms.Button
    Public WithEvents CmdAgregar As System.Windows.Forms.Button
    Public WithEvents CmdBuscar As System.Windows.Forms.Button
    Public WithEvents CmdImprimir As System.Windows.Forms.Button
    Public WithEvents CmdGuardar As System.Windows.Forms.Button
    Public WithEvents txtImpuesto As System.Windows.Forms.TextBox
    Public WithEvents txtIdDevolucion As System.Windows.Forms.TextBox
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents txtIdCompra As System.Windows.Forms.TextBox
    Public WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents LblImpuesto As System.Windows.Forms.Label
    Public WithEvents LblTotal As System.Windows.Forms.Label
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
        Me.CmdAnular = New System.Windows.Forms.Button()
        Me.CmdAgregar = New System.Windows.Forms.Button()
        Me.CmdBuscar = New System.Windows.Forms.Button()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        Me.CmdGuardar = New System.Windows.Forms.Button()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me.txtIdDevolucion = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtIdCompra = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.LblImpuesto = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel2 = New System.Windows.Forms.Label()
        Me.lblLabel0 = New System.Windows.Forms.Label()
        Me.grdDetalleDevolucion = New System.Windows.Forms.DataGridView()
        Me.btnBuscarFactura = New System.Windows.Forms.Button()
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.txtNumFactura = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdDetalleDevolucion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.CmdAnular.TabIndex = 35
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
        Me.CmdAgregar.TabIndex = 34
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
        Me.CmdBuscar.TabIndex = 33
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
        Me.CmdImprimir.TabIndex = 32
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
        Me.CmdGuardar.TabIndex = 31
        Me.CmdGuardar.TabStop = False
        Me.CmdGuardar.Text = "&Guardar"
        Me.CmdGuardar.UseVisualStyleBackColor = False
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.Enabled = False
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(733, 397)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.ReadOnly = True
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 7
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIdDevolucion
        '
        Me.txtIdDevolucion.AcceptsReturn = True
        Me.txtIdDevolucion.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdDevolucion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdDevolucion.Enabled = False
        Me.txtIdDevolucion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdDevolucion.Location = New System.Drawing.Point(72, 40)
        Me.txtIdDevolucion.MaxLength = 0
        Me.txtIdDevolucion.Name = "txtIdDevolucion"
        Me.txtIdDevolucion.ReadOnly = True
        Me.txtIdDevolucion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdDevolucion.Size = New System.Drawing.Size(73, 20)
        Me.txtIdDevolucion.TabIndex = 0
        Me.txtIdDevolucion.TabStop = False
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.Enabled = False
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(733, 421)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 8
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIdCompra
        '
        Me.txtIdCompra.AcceptsReturn = True
        Me.txtIdCompra.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCompra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCompra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCompra.Location = New System.Drawing.Point(72, 66)
        Me.txtIdCompra.MaxLength = 0
        Me.txtIdCompra.Name = "txtIdCompra"
        Me.txtIdCompra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdCompra.Size = New System.Drawing.Size(81, 20)
        Me.txtIdCompra.TabIndex = 1
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.Enabled = False
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(733, 371)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 6
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.Enabled = False
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(72, 92)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 4
        Me.txtFecha.TabStop = False
        '
        'LblImpuesto
        '
        Me.LblImpuesto.BackColor = System.Drawing.Color.Transparent
        Me.LblImpuesto.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblImpuesto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblImpuesto.Location = New System.Drawing.Point(662, 400)
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
        Me.LblTotal.Location = New System.Drawing.Point(662, 424)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(65, 19)
        Me.LblTotal.TabIndex = 19
        Me.LblTotal.Text = "Total:"
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.lblLabel4.Location = New System.Drawing.Point(-2, 67)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(68, 18)
        Me.lblLabel4.TabIndex = 13
        Me.lblLabel4.Text = "Compra No:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(9, 93)
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
        Me.lblLabel2.Location = New System.Drawing.Point(178, 66)
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
        Me.lblLabel0.Location = New System.Drawing.Point(0, 40)
        Me.lblLabel0.Name = "lblLabel0"
        Me.lblLabel0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel0.Size = New System.Drawing.Size(69, 19)
        Me.lblLabel0.TabIndex = 10
        Me.lblLabel0.Text = "Empresa No:"
        Me.lblLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleDevolucion
        '
        Me.grdDetalleDevolucion.AllowUserToAddRows = False
        Me.grdDetalleDevolucion.AllowUserToDeleteRows = False
        Me.grdDetalleDevolucion.AllowUserToResizeColumns = False
        Me.grdDetalleDevolucion.AllowUserToResizeRows = False
        Me.grdDetalleDevolucion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleDevolucion.Location = New System.Drawing.Point(6, 118)
        Me.grdDetalleDevolucion.MultiSelect = False
        Me.grdDetalleDevolucion.Name = "grdDetalleDevolucion"
        Me.grdDetalleDevolucion.RowHeadersVisible = False
        Me.grdDetalleDevolucion.Size = New System.Drawing.Size(800, 247)
        Me.grdDetalleDevolucion.TabIndex = 5
        '
        'btnBuscarFactura
        '
        Me.btnBuscarFactura.Location = New System.Drawing.Point(154, 65)
        Me.btnBuscarFactura.Name = "btnBuscarFactura"
        Me.btnBuscarFactura.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarFactura.TabIndex = 136
        Me.btnBuscarFactura.TabStop = False
        Me.btnBuscarFactura.UseVisualStyleBackColor = True
        '
        'txtProveedor
        '
        Me.txtProveedor.AcceptsReturn = True
        Me.txtProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProveedor.Location = New System.Drawing.Point(247, 66)
        Me.txtProveedor.MaxLength = 0
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProveedor.Size = New System.Drawing.Size(442, 20)
        Me.txtProveedor.TabIndex = 2
        Me.txtProveedor.TabStop = False
        '
        'txtNumFactura
        '
        Me.txtNumFactura.AcceptsReturn = True
        Me.txtNumFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumFactura.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumFactura.Enabled = False
        Me.txtNumFactura.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumFactura.Location = New System.Drawing.Point(733, 65)
        Me.txtNumFactura.MaxLength = 0
        Me.txtNumFactura.Name = "txtNumFactura"
        Me.txtNumFactura.ReadOnly = True
        Me.txtNumFactura.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumFactura.Size = New System.Drawing.Size(73, 20)
        Me.txtNumFactura.TabIndex = 3
        Me.txtNumFactura.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(695, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(32, 19)
        Me.Label1.TabIndex = 138
        Me.Label1.Text = "Fact:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmDevolucionAProveedores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(814, 447)
        Me.Controls.Add(Me.txtNumFactura)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBuscarFactura)
        Me.Controls.Add(Me.txtProveedor)
        Me.Controls.Add(Me.grdDetalleDevolucion)
        Me.Controls.Add(Me.CmdAnular)
        Me.Controls.Add(Me.CmdAgregar)
        Me.Controls.Add(Me.CmdBuscar)
        Me.Controls.Add(Me.CmdImprimir)
        Me.Controls.Add(Me.CmdGuardar)
        Me.Controls.Add(Me.txtImpuesto)
        Me.Controls.Add(Me.txtIdDevolucion)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtIdCompra)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.LblImpuesto)
        Me.Controls.Add(Me.LblTotal)
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
        Me.Name = "FrmDevolucionAProveedores"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Devoluciones de Mercancía a Proveedores"
        CType(Me.grdDetalleDevolucion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleDevolucion As System.Windows.Forms.DataGridView
    Friend WithEvents btnBuscarFactura As System.Windows.Forms.Button
    Public WithEvents txtProveedor As System.Windows.Forms.TextBox
    Public WithEvents txtNumFactura As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
End Class