<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBusquedaProducto
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
    Public WithEvents btnFiltrar As System.Windows.Forms.Button
    Public WithEvents txtDesc As System.Windows.Forms.TextBox
    Public WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents Id2Label As System.Windows.Forms.Label
    Public WithEvents IdLabel As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Id2Label = New System.Windows.Forms.Label()
        Me.IdLabel = New System.Windows.Forms.Label()
        Me.dgvListado = New System.Windows.Forms.DataGridView()
        Me.chkExacta = New System.Windows.Forms.CheckBox()
        Me.lblPagina = New System.Windows.Forms.Label()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboLinea = New System.Windows.Forms.ComboBox()
        Me.txtCodigoProveedor = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.dgvListado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnFiltrar
        '
        Me.btnFiltrar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnFiltrar.Enabled = False
        Me.btnFiltrar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFiltrar.Location = New System.Drawing.Point(951, 83)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnFiltrar.Size = New System.Drawing.Size(81, 21)
        Me.btnFiltrar.TabIndex = 6
        Me.btnFiltrar.TabStop = False
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.UseVisualStyleBackColor = False
        '
        'txtDesc
        '
        Me.txtDesc.AcceptsReturn = True
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesc.Location = New System.Drawing.Point(106, 84)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesc.Size = New System.Drawing.Size(839, 20)
        Me.txtDesc.TabIndex = 3
        '
        'txtCodigo
        '
        Me.txtCodigo.AcceptsReturn = True
        Me.txtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigo.Location = New System.Drawing.Point(106, 32)
        Me.txtCodigo.MaxLength = 50
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigo.Size = New System.Drawing.Size(260, 20)
        Me.txtCodigo.TabIndex = 0
        '
        'Id2Label
        '
        Me.Id2Label.BackColor = System.Drawing.Color.Transparent
        Me.Id2Label.Cursor = System.Windows.Forms.Cursors.Default
        Me.Id2Label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Id2Label.Location = New System.Drawing.Point(35, 85)
        Me.Id2Label.Name = "Id2Label"
        Me.Id2Label.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Id2Label.Size = New System.Drawing.Size(65, 17)
        Me.Id2Label.TabIndex = 5
        Me.Id2Label.Text = "Descripci�n:"
        Me.Id2Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IdLabel
        '
        Me.IdLabel.BackColor = System.Drawing.Color.Transparent
        Me.IdLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.IdLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.IdLabel.Location = New System.Drawing.Point(35, 33)
        Me.IdLabel.Name = "IdLabel"
        Me.IdLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IdLabel.Size = New System.Drawing.Size(65, 17)
        Me.IdLabel.TabIndex = 4
        Me.IdLabel.Text = "C�digo:"
        Me.IdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvListado
        '
        Me.dgvListado.AllowUserToAddRows = False
        Me.dgvListado.AllowUserToDeleteRows = False
        Me.dgvListado.AllowUserToResizeColumns = False
        Me.dgvListado.AllowUserToResizeRows = False
        Me.dgvListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvListado.Location = New System.Drawing.Point(12, 148)
        Me.dgvListado.Name = "dgvListado"
        Me.dgvListado.ReadOnly = True
        Me.dgvListado.RowHeadersVisible = False
        Me.dgvListado.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvListado.Size = New System.Drawing.Size(1023, 375)
        Me.dgvListado.TabIndex = 5
        Me.dgvListado.TabStop = False
        '
        'chkExacta
        '
        Me.chkExacta.AutoSize = True
        Me.chkExacta.Location = New System.Drawing.Point(381, 34)
        Me.chkExacta.Name = "chkExacta"
        Me.chkExacta.Size = New System.Drawing.Size(110, 17)
        Me.chkExacta.TabIndex = 7
        Me.chkExacta.TabStop = False
        Me.chkExacta.Text = "B�squeda Exacta"
        Me.chkExacta.UseVisualStyleBackColor = True
        '
        'lblPagina
        '
        Me.lblPagina.AutoSize = True
        Me.lblPagina.Location = New System.Drawing.Point(731, 539)
        Me.lblPagina.Name = "lblPagina"
        Me.lblPagina.Size = New System.Drawing.Size(77, 13)
        Me.lblPagina.TabIndex = 28
        Me.lblPagina.Text = "P�gina N de N"
        '
        'btnLast
        '
        Me.btnLast.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLast.Enabled = False
        Me.btnLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLast.Location = New System.Drawing.Point(1006, 534)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(29, 23)
        Me.btnLast.TabIndex = 9
        Me.btnLast.TabStop = False
        Me.btnLast.Text = ">>"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Enabled = False
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(977, 534)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(29, 23)
        Me.btnNext.TabIndex = 8
        Me.btnNext.TabStop = False
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevious.Enabled = False
        Me.btnPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevious.Location = New System.Drawing.Point(948, 534)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(29, 23)
        Me.btnPrevious.TabIndex = 7
        Me.btnPrevious.TabStop = False
        Me.btnPrevious.Text = "<"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFirst.Enabled = False
        Me.btnFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFirst.Location = New System.Drawing.Point(919, 534)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(29, 23)
        Me.btnFirst.TabIndex = 6
        Me.btnFirst.TabStop = False
        Me.btnFirst.Text = "<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(35, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 17)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "L�nea:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboLinea
        '
        Me.cboLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLinea.FormattingEnabled = True
        Me.cboLinea.Location = New System.Drawing.Point(106, 110)
        Me.cboLinea.Name = "cboLinea"
        Me.cboLinea.Size = New System.Drawing.Size(320, 21)
        Me.cboLinea.TabIndex = 4
        '
        'txtCodigoProveedor
        '
        Me.txtCodigoProveedor.AcceptsReturn = True
        Me.txtCodigoProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigoProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigoProveedor.Location = New System.Drawing.Point(106, 58)
        Me.txtCodigoProveedor.MaxLength = 50
        Me.txtCodigoProveedor.Name = "txtCodigoProveedor"
        Me.txtCodigoProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigoProveedor.Size = New System.Drawing.Size(260, 20)
        Me.txtCodigoProveedor.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(-2, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(102, 17)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "C�digo proveedor:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(70, 536)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(300, 21)
        Me.cboSucursal.TabIndex = 157
        Me.cboSucursal.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 536)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 19)
        Me.Label6.TabIndex = 158
        Me.Label6.Text = "Sucursal:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmBusquedaProducto
        '
        Me.AcceptButton = Me.btnFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1047, 568)
        Me.Controls.Add(Me.cboSucursal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtCodigoProveedor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboLinea)
        Me.Controls.Add(Me.lblPagina)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.chkExacta)
        Me.Controls.Add(Me.dgvListado)
        Me.Controls.Add(Me.btnFiltrar)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Id2Label)
        Me.Controls.Add(Me.IdLabel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBusquedaProducto"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "B�squeda de Producto"
        CType(Me.dgvListado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvListado As System.Windows.Forms.DataGridView
    Friend WithEvents chkExacta As System.Windows.Forms.CheckBox
    Friend WithEvents lblPagina As System.Windows.Forms.Label
    Private WithEvents btnLast As System.Windows.Forms.Button
    Private WithEvents btnNext As System.Windows.Forms.Button
    Private WithEvents btnPrevious As System.Windows.Forms.Button
    Private WithEvents btnFirst As System.Windows.Forms.Button
    Public WithEvents Label2 As Label
    Friend WithEvents cboLinea As ComboBox
    Public WithEvents txtCodigoProveedor As TextBox
    Public WithEvents Label1 As Label
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents Label6 As Label
End Class