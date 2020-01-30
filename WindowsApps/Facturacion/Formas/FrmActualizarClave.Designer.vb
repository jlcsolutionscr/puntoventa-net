<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmActualizarClave
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
    Public WithEvents TxtClave1 As System.Windows.Forms.TextBox
    Public WithEvents TxtClave2 As System.Windows.Forms.TextBox
    Public WithEvents CmdAceptar As System.Windows.Forms.Button
    Public WithEvents CmdCancelar As System.Windows.Forms.Button
    Public WithEvents LblClave1 As System.Windows.Forms.Label
    Public WithEvents LblClave2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TxtClave1 = New System.Windows.Forms.TextBox()
        Me.TxtClave2 = New System.Windows.Forms.TextBox()
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.LblClave1 = New System.Windows.Forms.Label()
        Me.LblClave2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TxtClave1
        '
        Me.TxtClave1.AcceptsReturn = True
        Me.TxtClave1.BackColor = System.Drawing.SystemColors.Window
        Me.TxtClave1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtClave1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtClave1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtClave1.Location = New System.Drawing.Point(112, 16)
        Me.TxtClave1.MaxLength = 0
        Me.TxtClave1.Name = "TxtClave1"
        Me.TxtClave1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtClave1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtClave1.Size = New System.Drawing.Size(89, 20)
        Me.TxtClave1.TabIndex = 0
        '
        'TxtClave2
        '
        Me.TxtClave2.AcceptsReturn = True
        Me.TxtClave2.BackColor = System.Drawing.SystemColors.Window
        Me.TxtClave2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtClave2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtClave2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtClave2.Location = New System.Drawing.Point(112, 40)
        Me.TxtClave2.MaxLength = 0
        Me.TxtClave2.Name = "TxtClave2"
        Me.TxtClave2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtClave2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtClave2.Size = New System.Drawing.Size(89, 20)
        Me.TxtClave2.TabIndex = 1
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAceptar.Location = New System.Drawing.Point(25, 72)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAceptar.Size = New System.Drawing.Size(81, 25)
        Me.CmdAceptar.TabIndex = 3
        Me.CmdAceptar.TabStop = False
        Me.CmdAceptar.Text = "&Aceptar"
        Me.CmdAceptar.UseVisualStyleBackColor = False
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCancelar.Location = New System.Drawing.Point(121, 72)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCancelar.Size = New System.Drawing.Size(81, 25)
        Me.CmdCancelar.TabIndex = 2
        Me.CmdCancelar.TabStop = False
        Me.CmdCancelar.Text = "&Cancelar"
        Me.CmdCancelar.UseVisualStyleBackColor = False
        '
        'LblClave1
        '
        Me.LblClave1.BackColor = System.Drawing.Color.Transparent
        Me.LblClave1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblClave1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblClave1.Location = New System.Drawing.Point(22, 19)
        Me.LblClave1.Name = "LblClave1"
        Me.LblClave1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblClave1.Size = New System.Drawing.Size(84, 17)
        Me.LblClave1.TabIndex = 5
        Me.LblClave1.Text = "Contraseña"
        '
        'LblClave2
        '
        Me.LblClave2.BackColor = System.Drawing.Color.Transparent
        Me.LblClave2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblClave2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblClave2.Location = New System.Drawing.Point(22, 43)
        Me.LblClave2.Name = "LblClave2"
        Me.LblClave2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblClave2.Size = New System.Drawing.Size(84, 17)
        Me.LblClave2.TabIndex = 4
        Me.LblClave2.Text = "Reingrese clave"
        '
        'FrmActualizarClave
        '
        Me.AcceptButton = Me.CmdAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.CmdCancelar
        Me.ClientSize = New System.Drawing.Size(227, 110)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtClave1)
        Me.Controls.Add(Me.TxtClave2)
        Me.Controls.Add(Me.CmdAceptar)
        Me.Controls.Add(Me.CmdCancelar)
        Me.Controls.Add(Me.LblClave1)
        Me.Controls.Add(Me.LblClave2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "FrmActualizarClave"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Contraseña"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class