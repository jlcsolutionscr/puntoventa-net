<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCatalogoContableListado
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCatalogoContableListado))
        Me.dgvDatos = New System.Windows.Forms.DataGridView()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDatos
        '
        Me.dgvDatos.AllowUserToAddRows = False
        Me.dgvDatos.AllowUserToDeleteRows = False
        Me.dgvDatos.AllowUserToResizeColumns = False
        Me.dgvDatos.AllowUserToResizeRows = False
        Me.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatos.Location = New System.Drawing.Point(12, 50)
        Me.dgvDatos.Name = "dgvDatos"
        Me.dgvDatos.ReadOnly = True
        Me.dgvDatos.RowHeadersVisible = False
        Me.dgvDatos.Size = New System.Drawing.Size(670, 400)
        Me.dgvDatos.TabIndex = 0
        Me.dgvDatos.TabStop = False
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(77, 24)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(320, 20)
        Me.txtDescripcion.TabIndex = 0
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.Location = New System.Drawing.Point(12, 27)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(66, 13)
        Me.lblDescripcion.TabIndex = 2
        Me.lblDescripcion.Text = "Descripción:"
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(436, 22)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(78, 22)
        Me.btnAgregar.TabIndex = 3
        Me.btnAgregar.TabStop = False
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.Location = New System.Drawing.Point(520, 22)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(78, 22)
        Me.btnEditar.TabIndex = 4
        Me.btnEditar.TabStop = False
        Me.btnEditar.Text = "Editar"
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Location = New System.Drawing.Point(604, 22)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(78, 22)
        Me.btnEliminar.TabIndex = 5
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Image = CType(resources.GetObject("btnFiltrar.Image"), System.Drawing.Image)
        Me.btnFiltrar.Location = New System.Drawing.Point(403, 22)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Size = New System.Drawing.Size(22, 22)
        Me.btnFiltrar.TabIndex = 6
        Me.btnFiltrar.TabStop = False
        Me.btnFiltrar.UseVisualStyleBackColor = True
        '
        'FrmCatalogoContableListado
        '
        Me.AcceptButton = Me.btnFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 463)
        Me.Controls.Add(Me.btnFiltrar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnEditar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.dgvDatos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmCatalogoContableListado"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento del Catálogo Contable"
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvDatos As System.Windows.Forms.DataGridView
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents btnEditar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnFiltrar As System.Windows.Forms.Button
End Class
