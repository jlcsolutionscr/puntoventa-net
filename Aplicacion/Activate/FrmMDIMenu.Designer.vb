<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMDIMenu
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.tsRegistrarEquipo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsActualizaConfig = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsActivacion = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsRespaldo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsRestaura = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSalir = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsRegistrarEquipo, Me.tsActualizaConfig, Me.tsActivacion, Me.tsRespaldo, Me.tsRestaura, Me.tsSalir})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(632, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'tsRegistrarEquipo
        '
        Me.tsRegistrarEquipo.Name = "tsRegistrarEquipo"
        Me.tsRegistrarEquipo.Size = New System.Drawing.Size(105, 20)
        Me.tsRegistrarEquipo.Text = "Registrar Equipo"
        Me.tsRegistrarEquipo.Visible = False
        '
        'tsActualizaConfig
        '
        Me.tsActualizaConfig.Name = "tsActualizaConfig"
        Me.tsActualizaConfig.Size = New System.Drawing.Size(150, 20)
        Me.tsActualizaConfig.Text = "Actualizar Configuración"
        Me.tsActualizaConfig.Visible = False
        '
        'tsActivacion
        '
        Me.tsActivacion.Name = "tsActivacion"
        Me.tsActivacion.Size = New System.Drawing.Size(151, 20)
        Me.tsActivacion.Text = "Activar Leandro Software"
        Me.tsActivacion.Visible = False
        '
        'tsRespaldo
        '
        Me.tsRespaldo.Name = "tsRespaldo"
        Me.tsRespaldo.Size = New System.Drawing.Size(70, 20)
        Me.tsRespaldo.Text = "Respaldar"
        Me.tsRespaldo.Visible = False
        '
        'tsRestaura
        '
        Me.tsRestaura.Name = "tsRestaura"
        Me.tsRestaura.Size = New System.Drawing.Size(85, 20)
        Me.tsRestaura.Text = "Desencriptar"
        Me.tsRestaura.Visible = False
        '
        'tsSalir
        '
        Me.tsSalir.Name = "tsSalir"
        Me.tsSalir.Size = New System.Drawing.Size(41, 20)
        Me.tsSalir.Text = "Salir"
        '
        'FrmMDIMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMDIMenu"
        Me.Text = "Menú de Configuración"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents tsRegistrarEquipo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsActualizaConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsSalir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsActivacion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsRespaldo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsRestaura As System.Windows.Forms.ToolStripMenuItem

End Class
