<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAutorizacionEspecial
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
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents CmdCancelar As System.Windows.Forms.Button
    Public WithEvents CmdAceptar As System.Windows.Forms.Button
    Public WithEvents TxtClave As System.Windows.Forms.TextBox
    Public WithEvents TxtUsuario As System.Windows.Forms.TextBox
    Public WithEvents LblClave As System.Windows.Forms.Label
    Public WithEvents LblUsuario As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.TxtClave = New System.Windows.Forms.TextBox()
        Me.TxtUsuario = New System.Windows.Forms.TextBox()
        Me.LblClave = New System.Windows.Forms.Label()
        Me.LblUsuario = New System.Windows.Forms.Label()
        Me.txtPorcentaje = New System.Windows.Forms.TextBox()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCancelar.Location = New System.Drawing.Point(169, 127)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCancelar.Size = New System.Drawing.Size(81, 25)
        Me.CmdCancelar.TabIndex = 5
        Me.CmdCancelar.TabStop = False
        Me.CmdCancelar.Text = "&Cancelar"
        Me.CmdCancelar.UseVisualStyleBackColor = False
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.CmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAceptar.Location = New System.Drawing.Point(73, 127)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAceptar.Size = New System.Drawing.Size(81, 25)
        Me.CmdAceptar.TabIndex = 4
        Me.CmdAceptar.TabStop = False
        Me.CmdAceptar.Text = "&Aceptar"
        Me.CmdAceptar.UseVisualStyleBackColor = False
        '
        'TxtClave
        '
        Me.TxtClave.AcceptsReturn = True
        Me.TxtClave.BackColor = System.Drawing.SystemColors.Window
        Me.TxtClave.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtClave.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtClave.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtClave.Location = New System.Drawing.Point(118, 52)
        Me.TxtClave.MaxLength = 0
        Me.TxtClave.Name = "TxtClave"
        Me.TxtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtClave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtClave.Size = New System.Drawing.Size(114, 20)
        Me.TxtClave.TabIndex = 1
        '
        'TxtUsuario
        '
        Me.TxtUsuario.AcceptsReturn = True
        Me.TxtUsuario.BackColor = System.Drawing.SystemColors.Window
        Me.TxtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtUsuario.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtUsuario.Location = New System.Drawing.Point(118, 26)
        Me.TxtUsuario.MaxLength = 0
        Me.TxtUsuario.Name = "TxtUsuario"
        Me.TxtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtUsuario.Size = New System.Drawing.Size(114, 20)
        Me.TxtUsuario.TabIndex = 0
        '
        'LblClave
        '
        Me.LblClave.BackColor = System.Drawing.Color.Transparent
        Me.LblClave.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblClave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblClave.Location = New System.Drawing.Point(47, 53)
        Me.LblClave.Name = "LblClave"
        Me.LblClave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblClave.Size = New System.Drawing.Size(65, 17)
        Me.LblClave.TabIndex = 5
        Me.LblClave.Text = "Contraseña"
        Me.LblClave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblUsuario
        '
        Me.LblUsuario.BackColor = System.Drawing.Color.Transparent
        Me.LblUsuario.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUsuario.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUsuario.Location = New System.Drawing.Point(66, 27)
        Me.LblUsuario.Name = "LblUsuario"
        Me.LblUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUsuario.Size = New System.Drawing.Size(46, 17)
        Me.LblUsuario.TabIndex = 4
        Me.LblUsuario.Text = "Usuario"
        Me.LblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPorcentaje
        '
        Me.txtPorcentaje.AcceptsReturn = True
        Me.txtPorcentaje.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentaje.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentaje.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentaje.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPorcentaje.Location = New System.Drawing.Point(92, 89)
        Me.txtPorcentaje.MaxLength = 0
        Me.txtPorcentaje.Name = "txtPorcentaje"
        Me.txtPorcentaje.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentaje.Size = New System.Drawing.Size(52, 20)
        Me.txtPorcentaje.TabIndex = 2
        Me.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMonto
        '
        Me.txtMonto.AcceptsReturn = True
        Me.txtMonto.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonto.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtMonto.Location = New System.Drawing.Point(199, 89)
        Me.txtMonto.MaxLength = 0
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonto.Size = New System.Drawing.Size(92, 20)
        Me.txtMonto.TabIndex = 3
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDescuento
        '
        Me.lblDescuento.BackColor = System.Drawing.Color.Transparent
        Me.lblDescuento.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDescuento.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescuento.Location = New System.Drawing.Point(150, 90)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDescuento.Size = New System.Drawing.Size(43, 17)
        Me.lblDescuento.TabIndex = 8
        Me.lblDescuento.Text = "Monto"
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(66, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Porcentaje"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAutorizacionEspecial
        '
        Me.AcceptButton = Me.CmdAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.CmdCancelar
        Me.ClientSize = New System.Drawing.Size(318, 172)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDescuento)
        Me.Controls.Add(Me.txtMonto)
        Me.Controls.Add(Me.txtPorcentaje)
        Me.Controls.Add(Me.CmdCancelar)
        Me.Controls.Add(Me.CmdAceptar)
        Me.Controls.Add(Me.TxtClave)
        Me.Controls.Add(Me.TxtUsuario)
        Me.Controls.Add(Me.LblClave)
        Me.Controls.Add(Me.LblUsuario)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(334, 211)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(334, 211)
        Me.Name = "FrmAutorizacionEspecial"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autorización especial"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents txtPorcentaje As TextBox
    Public WithEvents txtMonto As TextBox
    Public WithEvents lblDescuento As Label
    Public WithEvents Label1 As Label
End Class