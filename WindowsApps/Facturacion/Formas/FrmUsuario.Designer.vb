<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUsuario
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
    Public WithEvents txtUsuario As System.Windows.Forms.TextBox
    Public WithEvents txtIdUsuario As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.txtIdUsuario = New System.Windows.Forms.TextBox()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboRole = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvRoleXUsuario = New System.Windows.Forms.DataGridView()
        Me.btnInsertarRole = New System.Windows.Forms.Button()
        Me.btnEliminarRole = New System.Windows.Forms.Button()
        Me.chkRegistraDispositivo = New System.Windows.Forms.CheckBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtPorcMaxDescuento = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        CType(Me.dgvRoleXUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtUsuario
        '
        Me.txtUsuario.AcceptsReturn = True
        Me.txtUsuario.BackColor = System.Drawing.SystemColors.Window
        Me.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUsuario.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUsuario.Location = New System.Drawing.Point(79, 64)
        Me.txtUsuario.MaxLength = 10
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUsuario.Size = New System.Drawing.Size(64, 20)
        Me.txtUsuario.TabIndex = 1
        '
        'txtIdUsuario
        '
        Me.txtIdUsuario.AcceptsReturn = True
        Me.txtIdUsuario.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdUsuario.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdUsuario.Enabled = False
        Me.txtIdUsuario.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdUsuario.Location = New System.Drawing.Point(79, 39)
        Me.txtIdUsuario.MaxLength = 0
        Me.txtIdUsuario.Name = "txtIdUsuario"
        Me.txtIdUsuario.ReadOnly = True
        Me.txtIdUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdUsuario.Size = New System.Drawing.Size(41, 20)
        Me.txtIdUsuario.TabIndex = 0
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(8, 65)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(65, 17)
        Me._lblLabels_1.TabIndex = 0
        Me._lblLabels_1.Text = "Usuario:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(8, 40)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(65, 17)
        Me._lblLabels_0.TabIndex = 0
        Me._lblLabels_0.Text = "Id:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPassword
        '
        Me.txtPassword.AcceptsReturn = True
        Me.txtPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPassword.Location = New System.Drawing.Point(221, 64)
        Me.txtPassword.MaxLength = 0
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPassword.Size = New System.Drawing.Size(137, 20)
        Me.txtPassword.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(150, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Contraseña:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboRole
        '
        Me.cboRole.FormattingEnabled = True
        Me.cboRole.Location = New System.Drawing.Point(119, 117)
        Me.cboRole.Name = "cboRole"
        Me.cboRole.Size = New System.Drawing.Size(342, 21)
        Me.cboRole.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(13, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(99, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Seleccione un role:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvRoleXUsuario
        '
        Me.dgvRoleXUsuario.AllowUserToAddRows = False
        Me.dgvRoleXUsuario.AllowUserToDeleteRows = False
        Me.dgvRoleXUsuario.AllowUserToResizeColumns = False
        Me.dgvRoleXUsuario.AllowUserToResizeRows = False
        Me.dgvRoleXUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRoleXUsuario.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvRoleXUsuario.Location = New System.Drawing.Point(14, 144)
        Me.dgvRoleXUsuario.MultiSelect = False
        Me.dgvRoleXUsuario.Name = "dgvRoleXUsuario"
        Me.dgvRoleXUsuario.ReadOnly = True
        Me.dgvRoleXUsuario.RowHeadersVisible = False
        Me.dgvRoleXUsuario.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvRoleXUsuario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvRoleXUsuario.Size = New System.Drawing.Size(447, 212)
        Me.dgvRoleXUsuario.TabIndex = 0
        Me.dgvRoleXUsuario.TabStop = False
        '
        'btnInsertarRole
        '
        Me.btnInsertarRole.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertarRole.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertarRole.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertarRole.Location = New System.Drawing.Point(13, 359)
        Me.btnInsertarRole.Name = "btnInsertarRole"
        Me.btnInsertarRole.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertarRole.Size = New System.Drawing.Size(64, 21)
        Me.btnInsertarRole.TabIndex = 0
        Me.btnInsertarRole.TabStop = False
        Me.btnInsertarRole.Text = "&Insertar"
        Me.btnInsertarRole.UseVisualStyleBackColor = False
        '
        'btnEliminarRole
        '
        Me.btnEliminarRole.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminarRole.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminarRole.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminarRole.Location = New System.Drawing.Point(79, 359)
        Me.btnEliminarRole.Name = "btnEliminarRole"
        Me.btnEliminarRole.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminarRole.Size = New System.Drawing.Size(64, 21)
        Me.btnEliminarRole.TabIndex = 0
        Me.btnEliminarRole.TabStop = False
        Me.btnEliminarRole.Text = "&Eliminar"
        Me.btnEliminarRole.UseVisualStyleBackColor = False
        '
        'chkRegistraDispositivo
        '
        Me.chkRegistraDispositivo.AutoSize = True
        Me.chkRegistraDispositivo.Location = New System.Drawing.Point(313, 362)
        Me.chkRegistraDispositivo.Name = "chkRegistraDispositivo"
        Me.chkRegistraDispositivo.Size = New System.Drawing.Size(153, 17)
        Me.chkRegistraDispositivo.TabIndex = 0
        Me.chkRegistraDispositivo.TabStop = False
        Me.chkRegistraDispositivo.Text = "Permite registrar dispositivo"
        Me.chkRegistraDispositivo.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(94, 10)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(78, 22)
        Me.btnCancelar.TabIndex = 56
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(10, 10)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(78, 22)
        Me.btnGuardar.TabIndex = 55
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtPorcMaxDescuento
        '
        Me.txtPorcMaxDescuento.AcceptsReturn = True
        Me.txtPorcMaxDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcMaxDescuento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPorcMaxDescuento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcMaxDescuento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcMaxDescuento.Location = New System.Drawing.Point(417, 62)
        Me.txtPorcMaxDescuento.MaxLength = 10
        Me.txtPorcMaxDescuento.Name = "txtPorcMaxDescuento"
        Me.txtPorcMaxDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcMaxDescuento.Size = New System.Drawing.Size(44, 20)
        Me.txtPorcMaxDescuento.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(364, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(47, 17)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "% Desc:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(13, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(99, 17)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Sucursal:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboSucursal
        '
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(119, 90)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(342, 21)
        Me.cboSucursal.TabIndex = 59
        '
        'FrmUsuario
        '
        Me.AcceptButton = Me.btnInsertarRole
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(475, 394)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboSucursal)
        Me.Controls.Add(Me.txtPorcMaxDescuento)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.chkRegistraDispositivo)
        Me.Controls.Add(Me.btnEliminarRole)
        Me.Controls.Add(Me.btnInsertarRole)
        Me.Controls.Add(Me.dgvRoleXUsuario)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboRole)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtUsuario)
        Me.Controls.Add(Me.txtIdUsuario)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(491, 433)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(491, 433)
        Me.Name = "FrmUsuario"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Datos"
        CType(Me.dgvRoleXUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtPassword As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboRole As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgvRoleXUsuario As System.Windows.Forms.DataGridView
    Public WithEvents btnInsertarRole As System.Windows.Forms.Button
    Public WithEvents btnEliminarRole As System.Windows.Forms.Button
    Friend WithEvents chkRegistraDispositivo As System.Windows.Forms.CheckBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtPorcMaxDescuento As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents Label4 As Label
    Friend WithEvents cboSucursal As ComboBox
End Class