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
    Public WithEvents CmdFiltro As System.Windows.Forms.Button
    Public WithEvents TxtDesc As System.Windows.Forms.TextBox
    Public WithEvents TxtCodigo As System.Windows.Forms.TextBox
    Public WithEvents Id2Label As System.Windows.Forms.Label
    Public WithEvents IdLabel As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CmdFiltro = New System.Windows.Forms.Button()
        Me.TxtDesc = New System.Windows.Forms.TextBox()
        Me.TxtCodigo = New System.Windows.Forms.TextBox()
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
        CType(Me.dgvListado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdFiltro
        '
        Me.CmdFiltro.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CmdFiltro.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdFiltro.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdFiltro.Location = New System.Drawing.Point(601, 57)
        Me.CmdFiltro.Name = "CmdFiltro"
        Me.CmdFiltro.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdFiltro.Size = New System.Drawing.Size(81, 21)
        Me.CmdFiltro.TabIndex = 4
        Me.CmdFiltro.TabStop = False
        Me.CmdFiltro.Text = "Filtrar"
        Me.CmdFiltro.UseVisualStyleBackColor = False
        '
        'TxtDesc
        '
        Me.TxtDesc.AcceptsReturn = True
        Me.TxtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtDesc.Location = New System.Drawing.Point(80, 58)
        Me.TxtDesc.MaxLength = 200
        Me.TxtDesc.Name = "TxtDesc"
        Me.TxtDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtDesc.Size = New System.Drawing.Size(516, 20)
        Me.TxtDesc.TabIndex = 1
        '
        'TxtCodigo
        '
        Me.TxtCodigo.AcceptsReturn = True
        Me.TxtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCodigo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCodigo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCodigo.Location = New System.Drawing.Point(80, 32)
        Me.TxtCodigo.MaxLength = 50
        Me.TxtCodigo.Name = "TxtCodigo"
        Me.TxtCodigo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCodigo.Size = New System.Drawing.Size(242, 20)
        Me.TxtCodigo.TabIndex = 0
        '
        'Id2Label
        '
        Me.Id2Label.BackColor = System.Drawing.Color.Transparent
        Me.Id2Label.Cursor = System.Windows.Forms.Cursors.Default
        Me.Id2Label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Id2Label.Location = New System.Drawing.Point(9, 59)
        Me.Id2Label.Name = "Id2Label"
        Me.Id2Label.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Id2Label.Size = New System.Drawing.Size(65, 17)
        Me.Id2Label.TabIndex = 5
        Me.Id2Label.Text = "Descripción"
        Me.Id2Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IdLabel
        '
        Me.IdLabel.BackColor = System.Drawing.Color.Transparent
        Me.IdLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.IdLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.IdLabel.Location = New System.Drawing.Point(9, 33)
        Me.IdLabel.Name = "IdLabel"
        Me.IdLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IdLabel.Size = New System.Drawing.Size(65, 17)
        Me.IdLabel.TabIndex = 4
        Me.IdLabel.Text = "Código"
        Me.IdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvListado
        '
        Me.dgvListado.AllowUserToAddRows = False
        Me.dgvListado.AllowUserToDeleteRows = False
        Me.dgvListado.AllowUserToResizeColumns = False
        Me.dgvListado.AllowUserToResizeRows = False
        Me.dgvListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvListado.Location = New System.Drawing.Point(12, 120)
        Me.dgvListado.Name = "dgvListado"
        Me.dgvListado.ReadOnly = True
        Me.dgvListado.RowHeadersVisible = False
        Me.dgvListado.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvListado.Size = New System.Drawing.Size(670, 310)
        Me.dgvListado.TabIndex = 5
        Me.dgvListado.TabStop = False
        '
        'chkExacta
        '
        Me.chkExacta.AutoSize = True
        Me.chkExacta.Location = New System.Drawing.Point(572, 31)
        Me.chkExacta.Name = "chkExacta"
        Me.chkExacta.Size = New System.Drawing.Size(110, 17)
        Me.chkExacta.TabIndex = 3
        Me.chkExacta.TabStop = False
        Me.chkExacta.Text = "Búsqueda Exacta"
        Me.chkExacta.UseVisualStyleBackColor = True
        '
        'lblPagina
        '
        Me.lblPagina.AutoSize = True
        Me.lblPagina.Location = New System.Drawing.Point(484, 441)
        Me.lblPagina.Name = "lblPagina"
        Me.lblPagina.Size = New System.Drawing.Size(77, 13)
        Me.lblPagina.TabIndex = 28
        Me.lblPagina.Text = "Página N de N"
        '
        'btnLast
        '
        Me.btnLast.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLast.Enabled = False
        Me.btnLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLast.Location = New System.Drawing.Point(654, 436)
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
        Me.btnNext.Location = New System.Drawing.Point(625, 436)
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
        Me.btnPrevious.Location = New System.Drawing.Point(596, 436)
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
        Me.btnFirst.Location = New System.Drawing.Point(567, 436)
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
        Me.Label2.Location = New System.Drawing.Point(9, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 17)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Línea"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboLinea
        '
        Me.cboLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLinea.FormattingEnabled = True
        Me.cboLinea.Location = New System.Drawing.Point(80, 84)
        Me.cboLinea.Name = "cboLinea"
        Me.cboLinea.Size = New System.Drawing.Size(242, 21)
        Me.cboLinea.TabIndex = 38
        '
        'FrmBusquedaProducto
        '
        Me.AcceptButton = Me.CmdFiltro
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(695, 472)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboLinea)
        Me.Controls.Add(Me.lblPagina)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.chkExacta)
        Me.Controls.Add(Me.dgvListado)
        Me.Controls.Add(Me.CmdFiltro)
        Me.Controls.Add(Me.TxtDesc)
        Me.Controls.Add(Me.TxtCodigo)
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
        Me.Text = "Búsqueda de Producto"
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
End Class