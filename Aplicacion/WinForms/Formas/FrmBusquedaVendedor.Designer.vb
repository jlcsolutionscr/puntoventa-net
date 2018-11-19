<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBusquedaVendedor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBusquedaVendedor))
        Me.FlexVendedor = New System.Windows.Forms.DataGridView()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        CType(Me.FlexVendedor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FlexVendedor
        '
        Me.FlexVendedor.AllowUserToAddRows = False
        Me.FlexVendedor.AllowUserToDeleteRows = False
        Me.FlexVendedor.AllowUserToResizeColumns = False
        Me.FlexVendedor.AllowUserToResizeRows = False
        Me.FlexVendedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FlexVendedor.Location = New System.Drawing.Point(12, 38)
        Me.FlexVendedor.Name = "FlexVendedor"
        Me.FlexVendedor.ReadOnly = True
        Me.FlexVendedor.RowHeadersVisible = False
        Me.FlexVendedor.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.FlexVendedor.Size = New System.Drawing.Size(620, 337)
        Me.FlexVendedor.TabIndex = 2
        Me.FlexVendedor.TabStop = False
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Image = CType(resources.GetObject("btnFiltrar.Image"), System.Drawing.Image)
        Me.btnFiltrar.Location = New System.Drawing.Point(430, 10)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Size = New System.Drawing.Size(22, 22)
        Me.btnFiltrar.TabIndex = 1
        Me.btnFiltrar.TabStop = False
        Me.btnFiltrar.UseVisualStyleBackColor = True
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.Location = New System.Drawing.Point(12, 15)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(47, 13)
        Me.lblDescripcion.TabIndex = 8
        Me.lblDescripcion.Text = "Nombre:"
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(65, 12)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(359, 20)
        Me.txtNombre.TabIndex = 0
        '
        'FrmBusquedaVendedor
        '
        Me.AcceptButton = Me.btnFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(644, 387)
        Me.Controls.Add(Me.btnFiltrar)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.FlexVendedor)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBusquedaVendedor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Búsqueda de Vendedores"
        CType(Me.FlexVendedor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FlexVendedor As System.Windows.Forms.DataGridView
    Friend WithEvents btnFiltrar As System.Windows.Forms.Button
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
End Class