<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDetalleDocumentoElectronico
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDetalleDocumentoElectronico))
        Me.picLoader = New System.Windows.Forms.PictureBox()
        Me.btnMostrarRespuesta = New System.Windows.Forms.Button()
        Me.lblPagina = New System.Windows.Forms.Label()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.dgvDatos = New System.Windows.Forms.DataGridView()
        Me.rtxDetalleRespuesta = New System.Windows.Forms.RichTextBox()
        Me.btnReenviarNotificacion = New System.Windows.Forms.Button()
        Me.btnMostrarXML = New System.Windows.Forms.Button()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picLoader
        '
        Me.picLoader.BackColor = System.Drawing.Color.Transparent
        Me.picLoader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picLoader.ErrorImage = Nothing
        Me.picLoader.Image = CType(resources.GetObject("picLoader.Image"), System.Drawing.Image)
        Me.picLoader.InitialImage = Nothing
        Me.picLoader.Location = New System.Drawing.Point(0, -1)
        Me.picLoader.Name = "picLoader"
        Me.picLoader.Size = New System.Drawing.Size(794, 489)
        Me.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picLoader.TabIndex = 37
        Me.picLoader.TabStop = False
        Me.picLoader.Visible = False
        '
        'btnMostrarRespuesta
        '
        Me.btnMostrarRespuesta.Location = New System.Drawing.Point(439, 450)
        Me.btnMostrarRespuesta.Name = "btnMostrarRespuesta"
        Me.btnMostrarRespuesta.Size = New System.Drawing.Size(100, 22)
        Me.btnMostrarRespuesta.TabIndex = 4
        Me.btnMostrarRespuesta.TabStop = False
        Me.btnMostrarRespuesta.Text = "Mostrar respuesta"
        Me.btnMostrarRespuesta.UseVisualStyleBackColor = True
        '
        'lblPagina
        '
        Me.lblPagina.AutoSize = True
        Me.lblPagina.Location = New System.Drawing.Point(555, 455)
        Me.lblPagina.Name = "lblPagina"
        Me.lblPagina.Size = New System.Drawing.Size(77, 13)
        Me.lblPagina.TabIndex = 44
        Me.lblPagina.Text = "Página N de N"
        '
        'btnLast
        '
        Me.btnLast.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLast.Enabled = False
        Me.btnLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLast.Location = New System.Drawing.Point(753, 450)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(29, 23)
        Me.btnLast.TabIndex = 8
        Me.btnLast.TabStop = False
        Me.btnLast.Text = ">>"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Enabled = False
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(724, 450)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(29, 23)
        Me.btnNext.TabIndex = 7
        Me.btnNext.TabStop = False
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevious.Enabled = False
        Me.btnPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevious.Location = New System.Drawing.Point(695, 450)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(29, 23)
        Me.btnPrevious.TabIndex = 6
        Me.btnPrevious.TabStop = False
        Me.btnPrevious.Text = "<"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFirst.Enabled = False
        Me.btnFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFirst.Location = New System.Drawing.Point(666, 450)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(29, 23)
        Me.btnFirst.TabIndex = 5
        Me.btnFirst.TabStop = False
        Me.btnFirst.Text = "<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'dgvDatos
        '
        Me.dgvDatos.AllowUserToAddRows = False
        Me.dgvDatos.AllowUserToDeleteRows = False
        Me.dgvDatos.AllowUserToOrderColumns = True
        Me.dgvDatos.AllowUserToResizeColumns = False
        Me.dgvDatos.AllowUserToResizeRows = False
        Me.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatos.Location = New System.Drawing.Point(12, 47)
        Me.dgvDatos.Name = "dgvDatos"
        Me.dgvDatos.ReadOnly = True
        Me.dgvDatos.RowHeadersVisible = False
        Me.dgvDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvDatos.Size = New System.Drawing.Size(770, 397)
        Me.dgvDatos.TabIndex = 45
        Me.dgvDatos.TabStop = False
        '
        'rtxDetalleRespuesta
        '
        Me.rtxDetalleRespuesta.Location = New System.Drawing.Point(12, 47)
        Me.rtxDetalleRespuesta.Name = "rtxDetalleRespuesta"
        Me.rtxDetalleRespuesta.Size = New System.Drawing.Size(770, 397)
        Me.rtxDetalleRespuesta.TabIndex = 0
        Me.rtxDetalleRespuesta.Text = ""
        '
        'btnReenviarNotificacion
        '
        Me.btnReenviarNotificacion.Location = New System.Drawing.Point(12, 450)
        Me.btnReenviarNotificacion.Name = "btnReenviarNotificacion"
        Me.btnReenviarNotificacion.Size = New System.Drawing.Size(115, 22)
        Me.btnReenviarNotificacion.TabIndex = 2
        Me.btnReenviarNotificacion.TabStop = False
        Me.btnReenviarNotificacion.Text = "Reenviar notificación"
        Me.btnReenviarNotificacion.UseVisualStyleBackColor = True
        '
        'btnMostrarXML
        '
        Me.btnMostrarXML.Location = New System.Drawing.Point(333, 450)
        Me.btnMostrarXML.Name = "btnMostrarXML"
        Me.btnMostrarXML.Size = New System.Drawing.Size(100, 22)
        Me.btnMostrarXML.TabIndex = 3
        Me.btnMostrarXML.TabStop = False
        Me.btnMostrarXML.Text = "Mostrar XML"
        Me.btnMostrarXML.UseVisualStyleBackColor = True
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(72, 12)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(300, 21)
        Me.cboSucursal.TabIndex = 151
        Me.cboSucursal.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(14, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 19)
        Me.Label6.TabIndex = 152
        Me.Label6.Text = "Sucursal:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmDetalleDocumentoElectronico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 487)
        Me.Controls.Add(Me.picLoader)
        Me.Controls.Add(Me.cboSucursal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnMostrarXML)
        Me.Controls.Add(Me.btnReenviarNotificacion)
        Me.Controls.Add(Me.lblPagina)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.btnMostrarRespuesta)
        Me.Controls.Add(Me.rtxDetalleRespuesta)
        Me.Controls.Add(Me.dgvDatos)
        Me.Name = "FrmDetalleDocumentoElectronico"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta de Documentos Electrónicos Procesados"
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents picLoader As PictureBox
    Friend WithEvents btnMostrarRespuesta As Button
    Friend WithEvents lblPagina As Label
    Private WithEvents btnLast As Button
    Private WithEvents btnNext As Button
    Private WithEvents btnPrevious As Button
    Private WithEvents btnFirst As Button
    Friend WithEvents dgvDatos As DataGridView
    Friend WithEvents rtxDetalleRespuesta As RichTextBox
    Friend WithEvents btnReenviarNotificacion As Button
    Friend WithEvents btnMostrarXML As Button
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents Label6 As Label
End Class
