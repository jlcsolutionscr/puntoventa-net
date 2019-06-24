<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenuReportes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMenuReportes))
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.FechaFinal = New System.Windows.Forms.DateTimePicker()
        Me.FechaInicio = New System.Windows.Forms.DateTimePicker()
        Me.LstReporte = New System.Windows.Forms.ListBox()
        Me.CmdVistaPrevia = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Id2Label = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtProveedor
        '
        Me.txtProveedor.AcceptsReturn = True
        Me.txtProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProveedor.Enabled = False
        Me.txtProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProveedor.Location = New System.Drawing.Point(71, 98)
        Me.txtProveedor.MaxLength = 0
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProveedor.Size = New System.Drawing.Size(233, 20)
        Me.txtProveedor.TabIndex = 142
        Me.txtProveedor.TabStop = False
        '
        'txtCliente
        '
        Me.txtCliente.AcceptsReturn = True
        Me.txtCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCliente.Enabled = False
        Me.txtCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCliente.Location = New System.Drawing.Point(71, 75)
        Me.txtCliente.MaxLength = 0
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCliente.Size = New System.Drawing.Size(233, 20)
        Me.txtCliente.TabIndex = 141
        Me.txtCliente.TabStop = False
        '
        'FechaFinal
        '
        Me.FechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaFinal.Location = New System.Drawing.Point(88, 48)
        Me.FechaFinal.Name = "FechaFinal"
        Me.FechaFinal.Size = New System.Drawing.Size(84, 20)
        Me.FechaFinal.TabIndex = 140
        Me.FechaFinal.Value = New Date(2013, 6, 9, 0, 0, 0, 0)
        '
        'FechaInicio
        '
        Me.FechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaInicio.Location = New System.Drawing.Point(88, 22)
        Me.FechaInicio.Name = "FechaInicio"
        Me.FechaInicio.Size = New System.Drawing.Size(84, 20)
        Me.FechaInicio.TabIndex = 139
        Me.FechaInicio.Value = New Date(2013, 6, 9, 0, 0, 0, 0)
        '
        'LstReporte
        '
        Me.LstReporte.BackColor = System.Drawing.SystemColors.Window
        Me.LstReporte.Cursor = System.Windows.Forms.Cursors.Default
        Me.LstReporte.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LstReporte.Location = New System.Drawing.Point(12, 125)
        Me.LstReporte.Name = "LstReporte"
        Me.LstReporte.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LstReporte.Size = New System.Drawing.Size(314, 225)
        Me.LstReporte.TabIndex = 143
        Me.LstReporte.TabStop = False
        '
        'CmdVistaPrevia
        '
        Me.CmdVistaPrevia.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdVistaPrevia.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdVistaPrevia.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdVistaPrevia.Location = New System.Drawing.Point(253, 22)
        Me.CmdVistaPrevia.Name = "CmdVistaPrevia"
        Me.CmdVistaPrevia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdVistaPrevia.Size = New System.Drawing.Size(73, 24)
        Me.CmdVistaPrevia.TabIndex = 145
        Me.CmdVistaPrevia.TabStop = False
        Me.CmdVistaPrevia.Text = "&Vista Previa"
        Me.CmdVistaPrevia.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label3.Location = New System.Drawing.Point(6, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(59, 17)
        Me.Label3.TabIndex = 148
        Me.Label3.Text = "Cliente:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Id2Label
        '
        Me.Id2Label.BackColor = System.Drawing.Color.Transparent
        Me.Id2Label.Cursor = System.Windows.Forms.Cursors.Default
        Me.Id2Label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Id2Label.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Id2Label.Location = New System.Drawing.Point(6, 99)
        Me.Id2Label.Name = "Id2Label"
        Me.Id2Label.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Id2Label.Size = New System.Drawing.Size(59, 17)
        Me.Id2Label.TabIndex = 147
        Me.Id2Label.Text = "Proveedor:"
        Me.Id2Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(40, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(42, 17)
        Me.Label2.TabIndex = 146
        Me.Label2.Text = "Hasta"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(40, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(42, 17)
        Me.Label1.TabIndex = 144
        Me.Label1.Text = "Desde"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Image = CType(resources.GetObject("btnBuscarProveedor.Image"), System.Drawing.Image)
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(306, 97)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarProveedor.TabIndex = 150
        Me.btnBuscarProveedor.TabStop = False
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Image = CType(resources.GetObject("btnBuscarCliente.Image"), System.Drawing.Image)
        Me.btnBuscarCliente.Location = New System.Drawing.Point(306, 74)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarCliente.TabIndex = 149
        Me.btnBuscarCliente.TabStop = False
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'FrmMenuReportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 361)
        Me.Controls.Add(Me.btnBuscarProveedor)
        Me.Controls.Add(Me.txtProveedor)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.txtCliente)
        Me.Controls.Add(Me.FechaFinal)
        Me.Controls.Add(Me.FechaInicio)
        Me.Controls.Add(Me.LstReporte)
        Me.Controls.Add(Me.CmdVistaPrevia)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Id2Label)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmMenuReportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu de Reportes del Sistema"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnBuscarProveedor As Button
    Public WithEvents txtProveedor As TextBox
    Friend WithEvents btnBuscarCliente As Button
    Public WithEvents txtCliente As TextBox
    Friend WithEvents FechaFinal As DateTimePicker
    Friend WithEvents FechaInicio As DateTimePicker
    Public WithEvents LstReporte As ListBox
    Public WithEvents CmdVistaPrevia As Button
    Public WithEvents Label3 As Label
    Public WithEvents Id2Label As Label
    Public WithEvents Label2 As Label
    Public WithEvents Label1 As Label
End Class
