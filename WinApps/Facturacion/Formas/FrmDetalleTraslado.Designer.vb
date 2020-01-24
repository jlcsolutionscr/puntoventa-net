<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDetalleTraslado
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
    Public WithEvents btnAplicar As System.Windows.Forms.Button
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
        Me.btnAplicar = New System.Windows.Forms.Button()
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNombreSucursalOrigen = New System.Windows.Forms.TextBox()
        Me.txtNombreSucursalDestino = New System.Windows.Forms.TextBox()
        CType(Me.grdDetalleTraslado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAplicar
        '
        Me.btnAplicar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAplicar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAplicar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAplicar.Location = New System.Drawing.Point(8, 8)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAplicar.Size = New System.Drawing.Size(64, 21)
        Me.btnAplicar.TabIndex = 31
        Me.btnAplicar.TabStop = False
        Me.btnAplicar.Text = "Aplicar"
        Me.btnAplicar.UseVisualStyleBackColor = False
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
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(738, 386)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 75
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
        Me.txtReferencia.ReadOnly = True
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
        Me.LblTotal.Location = New System.Drawing.Point(667, 389)
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
        Me.grdDetalleTraslado.Location = New System.Drawing.Point(11, 180)
        Me.grdDetalleTraslado.MultiSelect = False
        Me.grdDetalleTraslado.Name = "grdDetalleTraslado"
        Me.grdDetalleTraslado.ReadOnly = True
        Me.grdDetalleTraslado.RowHeadersVisible = False
        Me.grdDetalleTraslado.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleTraslado.TabIndex = 71
        Me.grdDetalleTraslado.TabStop = False
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
        'txtNombreSucursalDestino
        '
        Me.txtNombreSucursalDestino.AcceptsReturn = True
        Me.txtNombreSucursalDestino.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreSucursalDestino.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreSucursalDestino.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreSucursalDestino.Location = New System.Drawing.Point(113, 119)
        Me.txtNombreSucursalDestino.MaxLength = 0
        Me.txtNombreSucursalDestino.Name = "txtNombreSucursalDestino"
        Me.txtNombreSucursalDestino.ReadOnly = True
        Me.txtNombreSucursalDestino.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreSucursalDestino.Size = New System.Drawing.Size(351, 20)
        Me.txtNombreSucursalDestino.TabIndex = 157
        Me.txtNombreSucursalDestino.TabStop = False
        '
        'FrmDetalleTraslado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(823, 419)
        Me.Controls.Add(Me.txtNombreSucursalDestino)
        Me.Controls.Add(Me.txtNombreSucursalOrigen)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdDetalleTraslado)
        Me.Controls.Add(Me.btnAplicar)
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
        Me.MinimizeBox = False
        Me.Name = "FrmDetalleTraslado"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Aplicación de Traslado de Mercancía"
        CType(Me.grdDetalleTraslado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleTraslado As System.Windows.Forms.DataGridView
    Public WithEvents Label2 As Label
    Public WithEvents txtNombreSucursalOrigen As TextBox
    Public WithEvents txtNombreSucursalDestino As TextBox
End Class