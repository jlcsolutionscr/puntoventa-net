<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmGestionAbonoCxP
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
    Public WithEvents CmdAnular As System.Windows.Forms.Button
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGestionAbonoCxP))
        Me.CmdAnular = New System.Windows.Forms.Button()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.grdDetalleRecibo = New System.Windows.Forms.DataGridView()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        CType(Me.grdDetalleRecibo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdAnular
        '
        Me.CmdAnular.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAnular.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAnular.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAnular.Location = New System.Drawing.Point(568, 13)
        Me.CmdAnular.Name = "CmdAnular"
        Me.CmdAnular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAnular.Size = New System.Drawing.Size(64, 21)
        Me.CmdAnular.TabIndex = 0
        Me.CmdAnular.TabStop = False
        Me.CmdAnular.Text = "&Anular"
        Me.CmdAnular.UseVisualStyleBackColor = False
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(2, 14)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(64, 19)
        Me._lblLabels_2.TabIndex = 9
        Me._lblLabels_2.Text = "Cuenta:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleRecibo
        '
        Me.grdDetalleRecibo.AllowUserToAddRows = False
        Me.grdDetalleRecibo.AllowUserToDeleteRows = False
        Me.grdDetalleRecibo.AllowUserToResizeColumns = False
        Me.grdDetalleRecibo.AllowUserToResizeRows = False
        Me.grdDetalleRecibo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleRecibo.Location = New System.Drawing.Point(12, 47)
        Me.grdDetalleRecibo.MultiSelect = False
        Me.grdDetalleRecibo.Name = "grdDetalleRecibo"
        Me.grdDetalleRecibo.ReadOnly = True
        Me.grdDetalleRecibo.RowHeadersVisible = False
        Me.grdDetalleRecibo.Size = New System.Drawing.Size(620, 352)
        Me.grdDetalleRecibo.StandardTab = True
        Me.grdDetalleRecibo.TabIndex = 3
        Me.grdDetalleRecibo.TabStop = False
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(72, 13)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(394, 20)
        Me.txtDescripcion.TabIndex = 0
        Me.txtDescripcion.TabStop = False
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Image = CType(resources.GetObject("btnBuscarProveedor.Image"), System.Drawing.Image)
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(472, 12)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarProveedor.TabIndex = 11
        Me.btnBuscarProveedor.TabStop = False
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'CmdImprimir
        '
        Me.CmdImprimir.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdImprimir.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdImprimir.Location = New System.Drawing.Point(498, 13)
        Me.CmdImprimir.Name = "CmdImprimir"
        Me.CmdImprimir.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdImprimir.Size = New System.Drawing.Size(64, 21)
        Me.CmdImprimir.TabIndex = 12
        Me.CmdImprimir.TabStop = False
        Me.CmdImprimir.Text = "&Imprimir"
        Me.CmdImprimir.UseVisualStyleBackColor = False
        '
        'FrmGestionAbonoCxP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(646, 413)
        Me.Controls.Add(Me.CmdImprimir)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.btnBuscarProveedor)
        Me.Controls.Add(Me.grdDetalleRecibo)
        Me.Controls.Add(Me.CmdAnular)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGestionAbonoCxP"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gesti�n de Abonos sobre Cuentas por Pagar"
        CType(Me.grdDetalleRecibo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleRecibo As System.Windows.Forms.DataGridView
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarProveedor As System.Windows.Forms.Button
    Public WithEvents CmdImprimir As Button
End Class