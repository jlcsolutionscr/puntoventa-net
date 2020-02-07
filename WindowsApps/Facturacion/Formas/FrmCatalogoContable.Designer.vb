<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCatalogoContable
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
    Public WithEvents txtIdCuenta As System.Windows.Forms.TextBox
    Public WithEvents lblCodigo As System.Windows.Forms.Label
    Public WithEvents lblIdCuenta As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtIdCuenta = New System.Windows.Forms.TextBox()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.lblIdCuenta = New System.Windows.Forms.Label()
        Me.txtNivel_1 = New System.Windows.Forms.TextBox()
        Me.lblDescripción = New System.Windows.Forms.Label()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.txtNivel_2 = New System.Windows.Forms.TextBox()
        Me.txtNivel_3 = New System.Windows.Forms.TextBox()
        Me.txtNivel_4 = New System.Windows.Forms.TextBox()
        Me.txtNivel_5 = New System.Windows.Forms.TextBox()
        Me.txtNivel_6 = New System.Windows.Forms.TextBox()
        Me.txtNivel_7 = New System.Windows.Forms.TextBox()
        Me.lblCuentaGrupo = New System.Windows.Forms.Label()
        Me.cboCuentaGrupo = New System.Windows.Forms.ComboBox()
        Me.chkPermiteSobrejiro = New System.Windows.Forms.CheckBox()
        Me.chkPermiteMovimiento = New System.Windows.Forms.CheckBox()
        Me.cboTipoSaldo = New System.Windows.Forms.ComboBox()
        Me.txtSaldoActual = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.chkCuentaBalance = New System.Windows.Forms.CheckBox()
        Me.cboClaseCuenta = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(94, 140)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(295, 35)
        Me.txtDescripcion.TabIndex = 10
        '
        'txtIdCuenta
        '
        Me.txtIdCuenta.AcceptsReturn = True
        Me.txtIdCuenta.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCuenta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCuenta.Enabled = False
        Me.txtIdCuenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCuenta.Location = New System.Drawing.Point(94, 38)
        Me.txtIdCuenta.MaxLength = 0
        Me.txtIdCuenta.Name = "txtIdCuenta"
        Me.txtIdCuenta.ReadOnly = True
        Me.txtIdCuenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdCuenta.Size = New System.Drawing.Size(37, 20)
        Me.txtIdCuenta.TabIndex = 0
        Me.txtIdCuenta.TabStop = False
        '
        'lblCodigo
        '
        Me.lblCodigo.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCodigo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCodigo.Location = New System.Drawing.Point(19, 64)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCodigo.Size = New System.Drawing.Size(69, 17)
        Me.lblCodigo.TabIndex = 4
        Me.lblCodigo.Text = "Código:"
        Me.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.lblIdCuenta.Text = "Cuenta No.:"
        Me.lblIdCuenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNivel_1
        '
        Me.txtNivel_1.AcceptsReturn = True
        Me.txtNivel_1.BackColor = System.Drawing.SystemColors.Window
        Me.txtNivel_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNivel_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNivel_1.Location = New System.Drawing.Point(94, 64)
        Me.txtNivel_1.MaxLength = 1
        Me.txtNivel_1.Name = "txtNivel_1"
        Me.txtNivel_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNivel_1.Size = New System.Drawing.Size(15, 20)
        Me.txtNivel_1.TabIndex = 1
        '
        'lblDescripción
        '
        Me.lblDescripción.BackColor = System.Drawing.Color.Transparent
        Me.lblDescripción.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDescripción.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescripción.Location = New System.Drawing.Point(19, 140)
        Me.lblDescripción.Name = "lblDescripción"
        Me.lblDescripción.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDescripción.Size = New System.Drawing.Size(69, 17)
        Me.lblDescripción.TabIndex = 16
        Me.lblDescripción.Text = "Descripción:"
        Me.lblDescripción.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTipo
        '
        Me.lblTipo.BackColor = System.Drawing.Color.Transparent
        Me.lblTipo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTipo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTipo.Location = New System.Drawing.Point(20, 182)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTipo.Size = New System.Drawing.Size(69, 17)
        Me.lblTipo.TabIndex = 18
        Me.lblTipo.Text = "Tipo saldo:"
        Me.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNivel_2
        '
        Me.txtNivel_2.AcceptsReturn = True
        Me.txtNivel_2.BackColor = System.Drawing.SystemColors.Window
        Me.txtNivel_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNivel_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNivel_2.Location = New System.Drawing.Point(110, 64)
        Me.txtNivel_2.MaxLength = 2
        Me.txtNivel_2.Name = "txtNivel_2"
        Me.txtNivel_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNivel_2.Size = New System.Drawing.Size(21, 20)
        Me.txtNivel_2.TabIndex = 2
        '
        'txtNivel_3
        '
        Me.txtNivel_3.AcceptsReturn = True
        Me.txtNivel_3.BackColor = System.Drawing.SystemColors.Window
        Me.txtNivel_3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNivel_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNivel_3.Location = New System.Drawing.Point(132, 64)
        Me.txtNivel_3.MaxLength = 2
        Me.txtNivel_3.Name = "txtNivel_3"
        Me.txtNivel_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNivel_3.Size = New System.Drawing.Size(21, 20)
        Me.txtNivel_3.TabIndex = 3
        '
        'txtNivel_4
        '
        Me.txtNivel_4.AcceptsReturn = True
        Me.txtNivel_4.BackColor = System.Drawing.SystemColors.Window
        Me.txtNivel_4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNivel_4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNivel_4.Location = New System.Drawing.Point(154, 64)
        Me.txtNivel_4.MaxLength = 2
        Me.txtNivel_4.Name = "txtNivel_4"
        Me.txtNivel_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNivel_4.Size = New System.Drawing.Size(21, 20)
        Me.txtNivel_4.TabIndex = 4
        '
        'txtNivel_5
        '
        Me.txtNivel_5.AcceptsReturn = True
        Me.txtNivel_5.BackColor = System.Drawing.SystemColors.Window
        Me.txtNivel_5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNivel_5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNivel_5.Location = New System.Drawing.Point(176, 64)
        Me.txtNivel_5.MaxLength = 2
        Me.txtNivel_5.Name = "txtNivel_5"
        Me.txtNivel_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNivel_5.Size = New System.Drawing.Size(21, 20)
        Me.txtNivel_5.TabIndex = 5
        '
        'txtNivel_6
        '
        Me.txtNivel_6.AcceptsReturn = True
        Me.txtNivel_6.BackColor = System.Drawing.SystemColors.Window
        Me.txtNivel_6.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNivel_6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNivel_6.Location = New System.Drawing.Point(198, 64)
        Me.txtNivel_6.MaxLength = 2
        Me.txtNivel_6.Name = "txtNivel_6"
        Me.txtNivel_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNivel_6.Size = New System.Drawing.Size(21, 20)
        Me.txtNivel_6.TabIndex = 6
        '
        'txtNivel_7
        '
        Me.txtNivel_7.AcceptsReturn = True
        Me.txtNivel_7.BackColor = System.Drawing.SystemColors.Window
        Me.txtNivel_7.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNivel_7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNivel_7.Location = New System.Drawing.Point(220, 64)
        Me.txtNivel_7.MaxLength = 2
        Me.txtNivel_7.Name = "txtNivel_7"
        Me.txtNivel_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNivel_7.Size = New System.Drawing.Size(21, 20)
        Me.txtNivel_7.TabIndex = 7
        '
        'lblCuentaGrupo
        '
        Me.lblCuentaGrupo.BackColor = System.Drawing.Color.Transparent
        Me.lblCuentaGrupo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCuentaGrupo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCuentaGrupo.Location = New System.Drawing.Point(12, 91)
        Me.lblCuentaGrupo.Name = "lblCuentaGrupo"
        Me.lblCuentaGrupo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCuentaGrupo.Size = New System.Drawing.Size(76, 17)
        Me.lblCuentaGrupo.TabIndex = 42
        Me.lblCuentaGrupo.Text = "Cuenta grupo:"
        Me.lblCuentaGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCuentaGrupo
        '
        Me.cboCuentaGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCuentaGrupo.FormattingEnabled = True
        Me.cboCuentaGrupo.Location = New System.Drawing.Point(94, 90)
        Me.cboCuentaGrupo.Name = "cboCuentaGrupo"
        Me.cboCuentaGrupo.Size = New System.Drawing.Size(260, 21)
        Me.cboCuentaGrupo.TabIndex = 8
        '
        'chkPermiteSobrejiro
        '
        Me.chkPermiteSobrejiro.AutoSize = True
        Me.chkPermiteSobrejiro.Location = New System.Drawing.Point(95, 231)
        Me.chkPermiteSobrejiro.Name = "chkPermiteSobrejiro"
        Me.chkPermiteSobrejiro.Size = New System.Drawing.Size(105, 17)
        Me.chkPermiteSobrejiro.TabIndex = 13
        Me.chkPermiteSobrejiro.Text = "Permite Sobrejiro"
        Me.chkPermiteSobrejiro.UseVisualStyleBackColor = True
        '
        'chkPermiteMovimiento
        '
        Me.chkPermiteMovimiento.AutoSize = True
        Me.chkPermiteMovimiento.Location = New System.Drawing.Point(95, 208)
        Me.chkPermiteMovimiento.Name = "chkPermiteMovimiento"
        Me.chkPermiteMovimiento.Size = New System.Drawing.Size(118, 17)
        Me.chkPermiteMovimiento.TabIndex = 12
        Me.chkPermiteMovimiento.Text = "Permite Movimiento"
        Me.chkPermiteMovimiento.UseVisualStyleBackColor = True
        '
        'cboTipoSaldo
        '
        Me.cboTipoSaldo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoSaldo.FormattingEnabled = True
        Me.cboTipoSaldo.Location = New System.Drawing.Point(95, 181)
        Me.cboTipoSaldo.Name = "cboTipoSaldo"
        Me.cboTipoSaldo.Size = New System.Drawing.Size(124, 21)
        Me.cboTipoSaldo.TabIndex = 11
        '
        'txtSaldoActual
        '
        Me.txtSaldoActual.AcceptsReturn = True
        Me.txtSaldoActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoActual.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoActual.Location = New System.Drawing.Point(95, 254)
        Me.txtSaldoActual.MaxLength = 0
        Me.txtSaldoActual.Name = "txtSaldoActual"
        Me.txtSaldoActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoActual.Size = New System.Drawing.Size(118, 20)
        Me.txtSaldoActual.TabIndex = 14
        Me.txtSaldoActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 254)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(69, 17)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Saldo actual:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'chkCuentaBalance
        '
        Me.chkCuentaBalance.AutoSize = True
        Me.chkCuentaBalance.Location = New System.Drawing.Point(94, 117)
        Me.chkCuentaBalance.Name = "chkCuentaBalance"
        Me.chkCuentaBalance.Size = New System.Drawing.Size(117, 17)
        Me.chkCuentaBalance.TabIndex = 9
        Me.chkCuentaBalance.Text = "Cuenta de Balance"
        Me.chkCuentaBalance.UseVisualStyleBackColor = True
        '
        'cboClaseCuenta
        '
        Me.cboClaseCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboClaseCuenta.FormattingEnabled = True
        Me.cboClaseCuenta.Location = New System.Drawing.Point(94, 280)
        Me.cboClaseCuenta.Name = "cboClaseCuenta"
        Me.cboClaseCuenta.Size = New System.Drawing.Size(124, 21)
        Me.cboClaseCuenta.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(10, 281)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(78, 17)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Clase cuenta:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmCatalogoContable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(400, 310)
        Me.Controls.Add(Me.cboClaseCuenta)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.chkCuentaBalance)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtSaldoActual)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboTipoSaldo)
        Me.Controls.Add(Me.chkPermiteSobrejiro)
        Me.Controls.Add(Me.chkPermiteMovimiento)
        Me.Controls.Add(Me.lblCuentaGrupo)
        Me.Controls.Add(Me.cboCuentaGrupo)
        Me.Controls.Add(Me.txtNivel_7)
        Me.Controls.Add(Me.txtNivel_6)
        Me.Controls.Add(Me.txtNivel_5)
        Me.Controls.Add(Me.txtNivel_4)
        Me.Controls.Add(Me.txtNivel_3)
        Me.Controls.Add(Me.txtNivel_2)
        Me.Controls.Add(Me.lblTipo)
        Me.Controls.Add(Me.lblDescripción)
        Me.Controls.Add(Me.txtNivel_1)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtIdCuenta)
        Me.Controls.Add(Me.lblCodigo)
        Me.Controls.Add(Me.lblIdCuenta)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCatalogoContable"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Datos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtNivel_1 As System.Windows.Forms.TextBox
    Public WithEvents lblDescripción As System.Windows.Forms.Label
    Public WithEvents lblTipo As System.Windows.Forms.Label
    Public WithEvents txtNivel_2 As System.Windows.Forms.TextBox
    Public WithEvents txtNivel_3 As System.Windows.Forms.TextBox
    Public WithEvents txtNivel_4 As System.Windows.Forms.TextBox
    Public WithEvents txtNivel_5 As System.Windows.Forms.TextBox
    Public WithEvents txtNivel_6 As System.Windows.Forms.TextBox
    Public WithEvents txtNivel_7 As System.Windows.Forms.TextBox
    Public WithEvents lblCuentaGrupo As System.Windows.Forms.Label
    Friend WithEvents cboCuentaGrupo As System.Windows.Forms.ComboBox
    Friend WithEvents chkPermiteSobrejiro As System.Windows.Forms.CheckBox
    Friend WithEvents chkPermiteMovimiento As System.Windows.Forms.CheckBox
    Friend WithEvents cboTipoSaldo As System.Windows.Forms.ComboBox
    Public WithEvents txtSaldoActual As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents chkCuentaBalance As System.Windows.Forms.CheckBox
    Friend WithEvents cboClaseCuenta As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
End Class