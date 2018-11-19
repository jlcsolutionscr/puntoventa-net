<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTipoMoneda
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
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtIdTipoMoneda As System.Windows.Forms.TextBox
    Public WithEvents lblIdCuenta As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtIdTipoMoneda = New System.Windows.Forms.TextBox()
        Me.lblIdCuenta = New System.Windows.Forms.Label()
        Me.lblDescripción = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTipoCambioCompra = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTipoCambioVenta = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(94, 64)
        Me.txtDescripcion.MaxLength = 10
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(184, 20)
        Me.txtDescripcion.TabIndex = 1
        '
        'txtIdTipoMoneda
        '
        Me.txtIdTipoMoneda.AcceptsReturn = True
        Me.txtIdTipoMoneda.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdTipoMoneda.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdTipoMoneda.Enabled = False
        Me.txtIdTipoMoneda.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdTipoMoneda.Location = New System.Drawing.Point(94, 38)
        Me.txtIdTipoMoneda.MaxLength = 0
        Me.txtIdTipoMoneda.Name = "txtIdTipoMoneda"
        Me.txtIdTipoMoneda.ReadOnly = True
        Me.txtIdTipoMoneda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdTipoMoneda.Size = New System.Drawing.Size(37, 20)
        Me.txtIdTipoMoneda.TabIndex = 0
        Me.txtIdTipoMoneda.TabStop = False
        '
        'lblIdCuenta
        '
        Me.lblIdCuenta.BackColor = System.Drawing.Color.Transparent
        Me.lblIdCuenta.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIdCuenta.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIdCuenta.Location = New System.Drawing.Point(19, 39)
        Me.lblIdCuenta.Name = "lblIdCuenta"
        Me.lblIdCuenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIdCuenta.Size = New System.Drawing.Size(69, 17)
        Me.lblIdCuenta.TabIndex = 2
        Me.lblIdCuenta.Text = "Id:"
        Me.lblIdCuenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDescripción
        '
        Me.lblDescripción.BackColor = System.Drawing.Color.Transparent
        Me.lblDescripción.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDescripción.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescripción.Location = New System.Drawing.Point(19, 64)
        Me.lblDescripción.Name = "lblDescripción"
        Me.lblDescripción.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDescripción.Size = New System.Drawing.Size(69, 20)
        Me.lblDescripción.TabIndex = 16
        Me.lblDescripción.Text = "Descripción:"
        Me.lblDescripción.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(10, 10)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(78, 22)
        Me.btnGuardar.TabIndex = 47
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(94, 10)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(78, 22)
        Me.btnCancelar.TabIndex = 48
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "TC Compra:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTipoCambioCompra
        '
        Me.txtTipoCambioCompra.AcceptsReturn = True
        Me.txtTipoCambioCompra.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambioCompra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoCambioCompra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoCambioCompra.Location = New System.Drawing.Point(94, 90)
        Me.txtTipoCambioCompra.MaxLength = 10
        Me.txtTipoCambioCompra.Name = "txtTipoCambioCompra"
        Me.txtTipoCambioCompra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoCambioCompra.Size = New System.Drawing.Size(78, 20)
        Me.txtTipoCambioCompra.TabIndex = 2
        Me.txtTipoCambioCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(10, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(78, 20)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "TC Venta:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTipoCambioVenta
        '
        Me.txtTipoCambioVenta.AcceptsReturn = True
        Me.txtTipoCambioVenta.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambioVenta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoCambioVenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoCambioVenta.Location = New System.Drawing.Point(94, 116)
        Me.txtTipoCambioVenta.MaxLength = 10
        Me.txtTipoCambioVenta.Name = "txtTipoCambioVenta"
        Me.txtTipoCambioVenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoCambioVenta.Size = New System.Drawing.Size(78, 20)
        Me.txtTipoCambioVenta.TabIndex = 3
        Me.txtTipoCambioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FrmTipoMoneda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(290, 147)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTipoCambioVenta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTipoCambioCompra)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.lblDescripción)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtIdTipoMoneda)
        Me.Controls.Add(Me.lblIdCuenta)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmTipoMoneda"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Datos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents lblDescripción As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtTipoCambioCompra As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtTipoCambioVenta As System.Windows.Forms.TextBox
End Class