<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInventario
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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.CmdFiltrar = New System.Windows.Forms.Button()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.dgvListado = New System.Windows.Forms.DataGridView()
        Me.btnReporte = New System.Windows.Forms.Button()
        Me.cboLinea = New System.Windows.Forms.ComboBox()
        Me.lblPagina = New System.Windows.Forms.Label()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnCardex = New System.Windows.Forms.Button()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCodigoProveedor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkFiltrarActivos = New System.Windows.Forms.CheckBox()
        Me.chkFiltrarExistencias = New System.Windows.Forms.CheckBox()
        CType(Me.dgvListado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCodigo
        '
        Me.txtCodigo.AcceptsReturn = True
        Me.txtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigo.Location = New System.Drawing.Point(105, 13)
        Me.txtCodigo.MaxLength = 0
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigo.Size = New System.Drawing.Size(260, 20)
        Me.txtCodigo.TabIndex = 0
        '
        'CmdFiltrar
        '
        Me.CmdFiltrar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdFiltrar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdFiltrar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdFiltrar.Location = New System.Drawing.Point(384, 12)
        Me.CmdFiltrar.Name = "CmdFiltrar"
        Me.CmdFiltrar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdFiltrar.Size = New System.Drawing.Size(73, 21)
        Me.CmdFiltrar.TabIndex = 3
        Me.CmdFiltrar.TabStop = False
        Me.CmdFiltrar.Text = "Filtrar"
        Me.CmdFiltrar.UseVisualStyleBackColor = False
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(105, 65)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(707, 20)
        Me.txtDescripcion.TabIndex = 2
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(34, 91)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_0.TabIndex = 7
        Me._lblLabels_0.Text = "Línea:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_5.Location = New System.Drawing.Point(34, 65)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_5.TabIndex = 6
        Me._lblLabels_5.Text = "Descripción"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(34, 13)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_1.TabIndex = 5
        Me._lblLabels_1.Text = "Código:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvListado
        '
        Me.dgvListado.AllowUserToAddRows = False
        Me.dgvListado.AllowUserToDeleteRows = False
        Me.dgvListado.AllowUserToResizeColumns = False
        Me.dgvListado.AllowUserToResizeRows = False
        Me.dgvListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvListado.Location = New System.Drawing.Point(12, 151)
        Me.dgvListado.Name = "dgvListado"
        Me.dgvListado.ReadOnly = True
        Me.dgvListado.RowHeadersVisible = False
        Me.dgvListado.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvListado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvListado.Size = New System.Drawing.Size(1003, 377)
        Me.dgvListado.TabIndex = 5
        Me.dgvListado.TabStop = False
        '
        'btnReporte
        '
        Me.btnReporte.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnReporte.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnReporte.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnReporte.Location = New System.Drawing.Point(463, 12)
        Me.btnReporte.Name = "btnReporte"
        Me.btnReporte.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnReporte.Size = New System.Drawing.Size(91, 21)
        Me.btnReporte.TabIndex = 4
        Me.btnReporte.TabStop = False
        Me.btnReporte.Text = "Reporte"
        Me.btnReporte.UseVisualStyleBackColor = False
        '
        'cboLinea
        '
        Me.cboLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLinea.FormattingEnabled = True
        Me.cboLinea.Location = New System.Drawing.Point(105, 91)
        Me.cboLinea.Name = "cboLinea"
        Me.cboLinea.Size = New System.Drawing.Size(306, 21)
        Me.cboLinea.TabIndex = 3
        Me.cboLinea.TabStop = False
        '
        'lblPagina
        '
        Me.lblPagina.AutoSize = True
        Me.lblPagina.Location = New System.Drawing.Point(723, 542)
        Me.lblPagina.Name = "lblPagina"
        Me.lblPagina.Size = New System.Drawing.Size(77, 13)
        Me.lblPagina.TabIndex = 33
        Me.lblPagina.Text = "Página N de N"
        '
        'btnLast
        '
        Me.btnLast.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLast.Enabled = False
        Me.btnLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLast.Location = New System.Drawing.Point(987, 537)
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
        Me.btnNext.Location = New System.Drawing.Point(958, 537)
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
        Me.btnPrevious.Location = New System.Drawing.Point(929, 537)
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
        Me.btnFirst.Location = New System.Drawing.Point(900, 537)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(29, 23)
        Me.btnFirst.TabIndex = 6
        Me.btnFirst.TabStop = False
        Me.btnFirst.Text = "<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnCardex
        '
        Me.btnCardex.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCardex.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCardex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCardex.Location = New System.Drawing.Point(12, 538)
        Me.btnCardex.Name = "btnCardex"
        Me.btnCardex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnCardex.Size = New System.Drawing.Size(73, 21)
        Me.btnCardex.TabIndex = 6
        Me.btnCardex.TabStop = False
        Me.btnCardex.Text = "Kardex"
        Me.btnCardex.UseVisualStyleBackColor = False
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(105, 118)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(306, 21)
        Me.cboSucursal.TabIndex = 4
        Me.cboSucursal.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(34, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 19)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Sucursal:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigoProveedor
        '
        Me.txtCodigoProveedor.AcceptsReturn = True
        Me.txtCodigoProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigoProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigoProveedor.Location = New System.Drawing.Point(105, 39)
        Me.txtCodigoProveedor.MaxLength = 50
        Me.txtCodigoProveedor.Name = "txtCodigoProveedor"
        Me.txtCodigoProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigoProveedor.Size = New System.Drawing.Size(260, 20)
        Me.txtCodigoProveedor.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(-3, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(102, 17)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Código proveedor:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFiltrarActivos
        '
        Me.chkFiltrarActivos.AutoSize = True
        Me.chkFiltrarActivos.Location = New System.Drawing.Point(436, 120)
        Me.chkFiltrarActivos.Name = "chkFiltrarActivos"
        Me.chkFiltrarActivos.Size = New System.Drawing.Size(138, 17)
        Me.chkFiltrarActivos.TabIndex = 44
        Me.chkFiltrarActivos.Text = "Filtrar productos activos"
        Me.chkFiltrarActivos.UseVisualStyleBackColor = True
        '
        'chkFiltrarExistencias
        '
        Me.chkFiltrarExistencias.AutoSize = True
        Me.chkFiltrarExistencias.Location = New System.Drawing.Point(589, 120)
        Me.chkFiltrarExistencias.Name = "chkFiltrarExistencias"
        Me.chkFiltrarExistencias.Size = New System.Drawing.Size(195, 17)
        Me.chkFiltrarExistencias.TabIndex = 45
        Me.chkFiltrarExistencias.Text = "Filtrar productos con existencias > 0"
        Me.chkFiltrarExistencias.UseVisualStyleBackColor = True
        '
        'FrmInventario
        '
        Me.AcceptButton = Me.CmdFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1028, 568)
        Me.Controls.Add(Me.chkFiltrarExistencias)
        Me.Controls.Add(Me.chkFiltrarActivos)
        Me.Controls.Add(Me.txtCodigoProveedor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboSucursal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCardex)
        Me.Controls.Add(Me.lblPagina)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.cboLinea)
        Me.Controls.Add(Me.btnReporte)
        Me.Controls.Add(Me.dgvListado)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.CmdFiltrar)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Controls.Add(Me._lblLabels_5)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInventario"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Inventario"
        CType(Me.dgvListado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvListado As System.Windows.Forms.DataGridView
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents CmdFiltrar As System.Windows.Forms.Button
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Friend WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Friend WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Friend WithEvents btnReporte As System.Windows.Forms.Button
    Friend WithEvents cboLinea As System.Windows.Forms.ComboBox
    Friend WithEvents lblPagina As System.Windows.Forms.Label
    Private WithEvents btnLast As System.Windows.Forms.Button
    Private WithEvents btnNext As System.Windows.Forms.Button
    Private WithEvents btnPrevious As System.Windows.Forms.Button
    Private WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnCardex As System.Windows.Forms.Button
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents Label1 As Label
    Public WithEvents txtCodigoProveedor As TextBox
    Public WithEvents Label2 As Label
    Friend WithEvents chkFiltrarActivos As CheckBox
    Friend WithEvents chkFiltrarExistencias As CheckBox
End Class