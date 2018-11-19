<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmParametroContable
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
    Public WithEvents txtIdParametro As System.Windows.Forms.TextBox
    Public WithEvents lblIdCuenta As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtIdParametro = New System.Windows.Forms.TextBox()
        Me.lblIdCuenta = New System.Windows.Forms.Label()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.lblCuentaGrupo = New System.Windows.Forms.Label()
        Me.cboCuentaContable = New System.Windows.Forms.ComboBox()
        Me.cboTipoParametro = New System.Windows.Forms.ComboBox()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboProducto = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'txtIdParametro
        '
        Me.txtIdParametro.AcceptsReturn = True
        Me.txtIdParametro.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdParametro.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdParametro.Enabled = False
        Me.txtIdParametro.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdParametro.Location = New System.Drawing.Point(94, 38)
        Me.txtIdParametro.MaxLength = 0
        Me.txtIdParametro.Name = "txtIdParametro"
        Me.txtIdParametro.ReadOnly = True
        Me.txtIdParametro.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdParametro.Size = New System.Drawing.Size(37, 20)
        Me.txtIdParametro.TabIndex = 0
        Me.txtIdParametro.TabStop = False
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
        'lblTipo
        '
        Me.lblTipo.BackColor = System.Drawing.Color.Transparent
        Me.lblTipo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTipo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTipo.Location = New System.Drawing.Point(2, 65)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTipo.Size = New System.Drawing.Size(86, 17)
        Me.lblTipo.TabIndex = 18
        Me.lblTipo.Text = "Tipo parámetro:"
        Me.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCuentaGrupo
        '
        Me.lblCuentaGrupo.BackColor = System.Drawing.Color.Transparent
        Me.lblCuentaGrupo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCuentaGrupo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCuentaGrupo.Location = New System.Drawing.Point(12, 92)
        Me.lblCuentaGrupo.Name = "lblCuentaGrupo"
        Me.lblCuentaGrupo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCuentaGrupo.Size = New System.Drawing.Size(76, 17)
        Me.lblCuentaGrupo.TabIndex = 42
        Me.lblCuentaGrupo.Text = "Cuenta Cont.:"
        Me.lblCuentaGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCuentaContable
        '
        Me.cboCuentaContable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCuentaContable.FormattingEnabled = True
        Me.cboCuentaContable.Location = New System.Drawing.Point(94, 91)
        Me.cboCuentaContable.Name = "cboCuentaContable"
        Me.cboCuentaContable.Size = New System.Drawing.Size(294, 21)
        Me.cboCuentaContable.TabIndex = 8
        '
        'cboTipoParametro
        '
        Me.cboTipoParametro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoParametro.FormattingEnabled = True
        Me.cboTipoParametro.Location = New System.Drawing.Point(94, 64)
        Me.cboTipoParametro.Name = "cboTipoParametro"
        Me.cboTipoParametro.Size = New System.Drawing.Size(209, 21)
        Me.cboTipoParametro.TabIndex = 10
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
        Me.Label1.Location = New System.Drawing.Point(12, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(76, 17)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Producto:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboProducto
        '
        Me.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProducto.Enabled = False
        Me.cboProducto.FormattingEnabled = True
        Me.cboProducto.Location = New System.Drawing.Point(94, 118)
        Me.cboProducto.Name = "cboProducto"
        Me.cboProducto.Size = New System.Drawing.Size(294, 21)
        Me.cboProducto.TabIndex = 49
        '
        'FrmParametroContable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(400, 153)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboProducto)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.cboTipoParametro)
        Me.Controls.Add(Me.lblCuentaGrupo)
        Me.Controls.Add(Me.cboCuentaContable)
        Me.Controls.Add(Me.lblTipo)
        Me.Controls.Add(Me.txtIdParametro)
        Me.Controls.Add(Me.lblIdCuenta)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmParametroContable"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Datos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents lblTipo As System.Windows.Forms.Label
    Public WithEvents lblCuentaGrupo As System.Windows.Forms.Label
    Friend WithEvents cboCuentaContable As System.Windows.Forms.ComboBox
    Friend WithEvents cboTipoParametro As System.Windows.Forms.ComboBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboProducto As System.Windows.Forms.ComboBox
End Class