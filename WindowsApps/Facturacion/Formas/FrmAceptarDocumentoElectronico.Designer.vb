<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAceptarDocumentoElectronico
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
        Me.btnEnviar = New System.Windows.Forms.Button()
        Me.ofdAbrirDocumento = New System.Windows.Forms.OpenFileDialog()
        Me.btnCargar = New System.Windows.Forms.Button()
        Me.lblEtiqueta = New System.Windows.Forms.Label()
        Me.grpEstado = New System.Windows.Forms.GroupBox()
        Me.rbnRechazar = New System.Windows.Forms.RadioButton()
        Me.rbnAceptarParcial = New System.Windows.Forms.RadioButton()
        Me.rbnAceptado = New System.Windows.Forms.RadioButton()
        Me.txtMensaje = New System.Windows.Forms.RichTextBox()
        Me.grpEstado.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnEnviar
        '
        Me.btnEnviar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEnviar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEnviar.Enabled = False
        Me.btnEnviar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEnviar.Location = New System.Drawing.Point(443, 417)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEnviar.Size = New System.Drawing.Size(73, 25)
        Me.btnEnviar.TabIndex = 5
        Me.btnEnviar.TabStop = False
        Me.btnEnviar.Text = "Enviar"
        Me.btnEnviar.UseVisualStyleBackColor = False
        '
        'btnCargar
        '
        Me.btnCargar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCargar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCargar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCargar.Location = New System.Drawing.Point(12, 12)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnCargar.Size = New System.Drawing.Size(90, 25)
        Me.btnCargar.TabIndex = 0
        Me.btnCargar.TabStop = False
        Me.btnCargar.Text = "Cargar archivo"
        Me.btnCargar.UseVisualStyleBackColor = False
        '
        'lblEtiqueta
        '
        Me.lblEtiqueta.AutoSize = True
        Me.lblEtiqueta.Location = New System.Drawing.Point(14, 423)
        Me.lblEtiqueta.Name = "lblEtiqueta"
        Me.lblEtiqueta.Size = New System.Drawing.Size(116, 13)
        Me.lblEtiqueta.TabIndex = 20
        Me.lblEtiqueta.Text = "Seleccione el mensaje:"
        '
        'grpEstado
        '
        Me.grpEstado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.grpEstado.Controls.Add(Me.rbnRechazar)
        Me.grpEstado.Controls.Add(Me.rbnAceptarParcial)
        Me.grpEstado.Controls.Add(Me.rbnAceptado)
        Me.grpEstado.Location = New System.Drawing.Point(136, 408)
        Me.grpEstado.Name = "grpEstado"
        Me.grpEstado.Size = New System.Drawing.Size(291, 37)
        Me.grpEstado.TabIndex = 21
        Me.grpEstado.TabStop = False
        '
        'rbnRechazar
        '
        Me.rbnRechazar.AutoSize = True
        Me.rbnRechazar.Location = New System.Drawing.Point(209, 13)
        Me.rbnRechazar.Name = "rbnRechazar"
        Me.rbnRechazar.Size = New System.Drawing.Size(71, 17)
        Me.rbnRechazar.TabIndex = 4
        Me.rbnRechazar.TabStop = True
        Me.rbnRechazar.Text = "Rechazar"
        Me.rbnRechazar.UseVisualStyleBackColor = True
        '
        'rbnAceptarParcial
        '
        Me.rbnAceptarParcial.AutoSize = True
        Me.rbnAceptarParcial.Location = New System.Drawing.Point(78, 13)
        Me.rbnAceptarParcial.Name = "rbnAceptarParcial"
        Me.rbnAceptarParcial.Size = New System.Drawing.Size(125, 17)
        Me.rbnAceptarParcial.TabIndex = 3
        Me.rbnAceptarParcial.TabStop = True
        Me.rbnAceptarParcial.Text = "Aceptar parcialmente"
        Me.rbnAceptarParcial.UseVisualStyleBackColor = True
        '
        'rbnAceptado
        '
        Me.rbnAceptado.AutoSize = True
        Me.rbnAceptado.Location = New System.Drawing.Point(10, 13)
        Me.rbnAceptado.Name = "rbnAceptado"
        Me.rbnAceptado.Size = New System.Drawing.Size(62, 17)
        Me.rbnAceptado.TabIndex = 2
        Me.rbnAceptado.TabStop = True
        Me.rbnAceptado.Text = "Aceptar"
        Me.rbnAceptado.UseVisualStyleBackColor = True
        '
        'txtMensaje
        '
        Me.txtMensaje.BackColor = System.Drawing.Color.White
        Me.txtMensaje.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMensaje.Location = New System.Drawing.Point(12, 43)
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.ReadOnly = True
        Me.txtMensaje.Size = New System.Drawing.Size(776, 368)
        Me.txtMensaje.TabIndex = 1
        Me.txtMensaje.TabStop = False
        Me.txtMensaje.Text = ""
        '
        'FrmAceptarDocumentoElectronico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 457)
        Me.Controls.Add(Me.txtMensaje)
        Me.Controls.Add(Me.grpEstado)
        Me.Controls.Add(Me.lblEtiqueta)
        Me.Controls.Add(Me.btnCargar)
        Me.Controls.Add(Me.btnEnviar)
        Me.Name = "FrmAceptarDocumentoElectronico"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmAceptarDocumentoElectronico"
        Me.grpEstado.ResumeLayout(False)
        Me.grpEstado.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents btnEnviar As Button
    Friend WithEvents ofdAbrirDocumento As OpenFileDialog
    Public WithEvents btnCargar As Button
    Friend WithEvents lblEtiqueta As Label
    Friend WithEvents grpEstado As GroupBox
    Friend WithEvents rbnRechazar As RadioButton
    Friend WithEvents rbnAceptarParcial As RadioButton
    Friend WithEvents rbnAceptado As RadioButton
    Friend WithEvents txtMensaje As RichTextBox
End Class
