<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSeguridad
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
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.TxtClave = New System.Windows.Forms.TextBox()
        Me.TxtUsuario = New System.Windows.Forms.TextBox()
        Me.LblClave = New System.Windows.Forms.Label()
        Me.LblUsuario = New System.Windows.Forms.Label()
        Me.cboEmpresa = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCancelar.Location = New System.Drawing.Point(163, 114)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCancelar.Size = New System.Drawing.Size(81, 25)
        Me.CmdCancelar.TabIndex = 4
        Me.CmdCancelar.TabStop = False
        Me.CmdCancelar.Text = "&Cancelar"
        Me.CmdCancelar.UseVisualStyleBackColor = False
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAceptar.Location = New System.Drawing.Point(72, 114)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAceptar.Size = New System.Drawing.Size(81, 25)
        Me.CmdAceptar.TabIndex = 3
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
        Me.TxtClave.Location = New System.Drawing.Point(123, 46)
        Me.TxtClave.MaxLength = 0
        Me.TxtClave.Name = "TxtClave"
        Me.TxtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtClave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtClave.Size = New System.Drawing.Size(129, 20)
        Me.TxtClave.TabIndex = 1
        '
        'TxtUsuario
        '
        Me.TxtUsuario.AcceptsReturn = True
        Me.TxtUsuario.BackColor = System.Drawing.SystemColors.Window
        Me.TxtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtUsuario.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtUsuario.Location = New System.Drawing.Point(123, 22)
        Me.TxtUsuario.MaxLength = 0
        Me.TxtUsuario.Name = "TxtUsuario"
        Me.TxtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtUsuario.Size = New System.Drawing.Size(129, 20)
        Me.TxtUsuario.TabIndex = 0
        '
        'LblClave
        '
        Me.LblClave.BackColor = System.Drawing.Color.Transparent
        Me.LblClave.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblClave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblClave.Location = New System.Drawing.Point(51, 46)
        Me.LblClave.Name = "LblClave"
        Me.LblClave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblClave.Size = New System.Drawing.Size(65, 17)
        Me.LblClave.TabIndex = 5
        Me.LblClave.Text = "Contraseña"
        Me.LblClave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblUsuario
        '
        Me.LblUsuario.BackColor = System.Drawing.Color.Transparent
        Me.LblUsuario.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUsuario.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUsuario.Location = New System.Drawing.Point(51, 22)
        Me.LblUsuario.Name = "LblUsuario"
        Me.LblUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUsuario.Size = New System.Drawing.Size(65, 17)
        Me.LblUsuario.TabIndex = 4
        Me.LblUsuario.Text = "Usuario"
        Me.LblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboEmpresa
        '
        Me.cboEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpresa.FormattingEnabled = True
        Me.cboEmpresa.Location = New System.Drawing.Point(82, 72)
        Me.cboEmpresa.Name = "cboEmpresa"
        Me.cboEmpresa.Size = New System.Drawing.Size(221, 21)
        Me.cboEmpresa.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(23, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(53, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Empresa"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmSeguridad
        '
        Me.AcceptButton = Me.CmdAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.CmdCancelar
        Me.ClientSize = New System.Drawing.Size(315, 155)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboEmpresa)
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
        Me.MinimizeBox = False
        Me.Name = "FrmSeguridad"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seguridad"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboEmpresa As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As Label
End Class