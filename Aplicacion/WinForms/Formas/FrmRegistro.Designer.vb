<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRegistro
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
    Public WithEvents btnCancelar As System.Windows.Forms.Button
    Public WithEvents btnRegistrar As System.Windows.Forms.Button
    Public WithEvents txtClave As System.Windows.Forms.TextBox
    Public WithEvents txtIdentificacion As System.Windows.Forms.TextBox
    Public WithEvents LblClave As System.Windows.Forms.Label
    Public WithEvents LblUsuario As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnRegistrar = New System.Windows.Forms.Button()
        Me.txtClave = New System.Windows.Forms.TextBox()
        Me.txtIdentificacion = New System.Windows.Forms.TextBox()
        Me.LblClave = New System.Windows.Forms.Label()
        Me.LblUsuario = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvDatos = New System.Windows.Forms.DataGridView()
        Me.btnConsultar = New System.Windows.Forms.Button()
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancelar.Location = New System.Drawing.Point(301, 295)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnCancelar.Size = New System.Drawing.Size(81, 25)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.TabStop = False
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnRegistrar
        '
        Me.btnRegistrar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnRegistrar.Enabled = False
        Me.btnRegistrar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnRegistrar.Location = New System.Drawing.Point(214, 295)
        Me.btnRegistrar.Name = "btnRegistrar"
        Me.btnRegistrar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnRegistrar.Size = New System.Drawing.Size(81, 25)
        Me.btnRegistrar.TabIndex = 5
        Me.btnRegistrar.Text = "&Registrar"
        Me.btnRegistrar.UseVisualStyleBackColor = False
        '
        'txtClave
        '
        Me.txtClave.AcceptsReturn = True
        Me.txtClave.BackColor = System.Drawing.SystemColors.Window
        Me.txtClave.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtClave.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClave.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtClave.Location = New System.Drawing.Point(87, 38)
        Me.txtClave.MaxLength = 0
        Me.txtClave.Name = "txtClave"
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtClave.Size = New System.Drawing.Size(107, 20)
        Me.txtClave.TabIndex = 1
        '
        'txtIdentificacion
        '
        Me.txtIdentificacion.AcceptsReturn = True
        Me.txtIdentificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdentificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdentificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdentificacion.Location = New System.Drawing.Point(87, 64)
        Me.txtIdentificacion.MaxLength = 0
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacion.Size = New System.Drawing.Size(107, 20)
        Me.txtIdentificacion.TabIndex = 2
        '
        'LblClave
        '
        Me.LblClave.BackColor = System.Drawing.Color.Transparent
        Me.LblClave.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblClave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblClave.Location = New System.Drawing.Point(15, 39)
        Me.LblClave.Name = "LblClave"
        Me.LblClave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblClave.Size = New System.Drawing.Size(66, 17)
        Me.LblClave.TabIndex = 5
        Me.LblClave.Text = "Contraseña"
        Me.LblClave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblUsuario
        '
        Me.LblUsuario.BackColor = System.Drawing.Color.Transparent
        Me.LblUsuario.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUsuario.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUsuario.Location = New System.Drawing.Point(5, 65)
        Me.LblUsuario.Name = "LblUsuario"
        Me.LblUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUsuario.Size = New System.Drawing.Size(76, 17)
        Me.LblUsuario.TabIndex = 4
        Me.LblUsuario.Text = "Identificación"
        Me.LblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUsuario
        '
        Me.txtUsuario.AcceptsReturn = True
        Me.txtUsuario.BackColor = System.Drawing.SystemColors.Window
        Me.txtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUsuario.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUsuario.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUsuario.Location = New System.Drawing.Point(87, 12)
        Me.txtUsuario.MaxLength = 0
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUsuario.Size = New System.Drawing.Size(83, 20)
        Me.txtUsuario.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(15, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(66, 17)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Usuario"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvDatos
        '
        Me.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatos.Location = New System.Drawing.Point(12, 94)
        Me.dgvDatos.Name = "dgvDatos"
        Me.dgvDatos.RowHeadersVisible = False
        Me.dgvDatos.Size = New System.Drawing.Size(370, 194)
        Me.dgvDatos.TabIndex = 4
        '
        'btnConsultar
        '
        Me.btnConsultar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnConsultar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnConsultar.Location = New System.Drawing.Point(200, 63)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnConsultar.Size = New System.Drawing.Size(182, 25)
        Me.btnConsultar.TabIndex = 3
        Me.btnConsultar.Text = "&Consultar Terminales Disponibles"
        Me.btnConsultar.UseVisualStyleBackColor = False
        '
        'FrmRegistro
        '
        Me.AcceptButton = Me.btnConsultar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(395, 332)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnConsultar)
        Me.Controls.Add(Me.dgvDatos)
        Me.Controls.Add(Me.txtUsuario)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnRegistrar)
        Me.Controls.Add(Me.txtClave)
        Me.Controls.Add(Me.txtIdentificacion)
        Me.Controls.Add(Me.LblClave)
        Me.Controls.Add(Me.LblUsuario)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(411, 370)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(411, 370)
        Me.Name = "FrmRegistro"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seguridad"
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtUsuario As TextBox
    Public WithEvents Label5 As Label
    Friend WithEvents dgvDatos As DataGridView
    Public WithEvents btnConsultar As Button
End Class