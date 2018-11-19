<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEstadoDocumentoElectronico
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEstadoDocumentoElectronico))
        Me.btnEnviar = New System.Windows.Forms.Button()
        Me.dgvDatos = New System.Windows.Forms.DataGridView()
        Me.picLoader = New System.Windows.Forms.PictureBox()
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEnviar
        '
        Me.btnEnviar.Location = New System.Drawing.Point(609, 429)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(173, 22)
        Me.btnEnviar.TabIndex = 31
        Me.btnEnviar.TabStop = False
        Me.btnEnviar.Text = "Enviar Documentos pendientes"
        Me.btnEnviar.UseVisualStyleBackColor = True
        '
        'dgvDatos
        '
        Me.dgvDatos.AllowUserToAddRows = False
        Me.dgvDatos.AllowUserToDeleteRows = False
        Me.dgvDatos.AllowUserToOrderColumns = True
        Me.dgvDatos.AllowUserToResizeColumns = False
        Me.dgvDatos.AllowUserToResizeRows = False
        Me.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatos.Location = New System.Drawing.Point(12, 12)
        Me.dgvDatos.Name = "dgvDatos"
        Me.dgvDatos.ReadOnly = True
        Me.dgvDatos.RowHeadersVisible = False
        Me.dgvDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvDatos.Size = New System.Drawing.Size(770, 411)
        Me.dgvDatos.TabIndex = 35
        Me.dgvDatos.TabStop = False
        '
        'picLoader
        '
        Me.picLoader.BackColor = System.Drawing.Color.Transparent
        Me.picLoader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picLoader.ErrorImage = Nothing
        Me.picLoader.Image = CType(resources.GetObject("picLoader.Image"), System.Drawing.Image)
        Me.picLoader.InitialImage = Nothing
        Me.picLoader.Location = New System.Drawing.Point(4, 2)
        Me.picLoader.Name = "picLoader"
        Me.picLoader.Size = New System.Drawing.Size(788, 461)
        Me.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picLoader.TabIndex = 36
        Me.picLoader.TabStop = False
        Me.picLoader.Visible = False
        '
        'FrmEstadoDocumentoElectronico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 463)
        Me.Controls.Add(Me.picLoader)
        Me.Controls.Add(Me.btnEnviar)
        Me.Controls.Add(Me.dgvDatos)
        Me.Name = "FrmEstadoDocumentoElectronico"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Documentos Electrónicos Pendientes de Enviar"
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnEnviar As Button
    Friend WithEvents dgvDatos As DataGridView
    Friend WithEvents picLoader As PictureBox
End Class
