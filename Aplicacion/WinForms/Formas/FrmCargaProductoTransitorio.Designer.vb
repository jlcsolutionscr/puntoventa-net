<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCargaProductoTransitorio
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
    Public WithEvents txtPrecioVenta1 As System.Windows.Forms.TextBox
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents lblPrecioVenta1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPrecioVenta1 = New System.Windows.Forms.TextBox()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.lblPrecioVenta1 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me.ofdAbrirImagen = New System.Windows.Forms.OpenFileDialog()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.cboTipoImpuesto = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPrecioImpuesto1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtPrecioVenta1
        '
        Me.txtPrecioVenta1.AcceptsReturn = True
        Me.txtPrecioVenta1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta1.Location = New System.Drawing.Point(106, 70)
        Me.txtPrecioVenta1.MaxLength = 0
        Me.txtPrecioVenta1.Name = "txtPrecioVenta1"
        Me.txtPrecioVenta1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta1.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta1.TabIndex = 2
        Me.txtPrecioVenta1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(106, 17)
        Me.txtDescripcion.MaxLength = 200
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(570, 20)
        Me.txtDescripcion.TabIndex = 0
        '
        'lblPrecioVenta1
        '
        Me.lblPrecioVenta1.BackColor = System.Drawing.Color.Transparent
        Me.lblPrecioVenta1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrecioVenta1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrecioVenta1.Location = New System.Drawing.Point(17, 71)
        Me.lblPrecioVenta1.Name = "lblPrecioVenta1"
        Me.lblPrecioVenta1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrecioVenta1.Size = New System.Drawing.Size(83, 17)
        Me.lblPrecioVenta1.TabIndex = 0
        Me.lblPrecioVenta1.Text = "Precio:"
        Me.lblPrecioVenta1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_4.Location = New System.Drawing.Point(17, 18)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_4.TabIndex = 0
        Me._lblLabels_4.Text = "Descripción:"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ofdAbrirImagen
        '
        Me.ofdAbrirImagen.FileName = "ofdAbrirImagen"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(364, 158)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(78, 22)
        Me.btnCancelar.TabIndex = 52
        Me.btnCancelar.TabStop = False
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(280, 158)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(78, 22)
        Me.btnAgregar.TabIndex = 51
        Me.btnAgregar.TabStop = False
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'cboTipoImpuesto
        '
        Me.cboTipoImpuesto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoImpuesto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoImpuesto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoImpuesto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoImpuesto.Items.AddRange(New Object() {"UND", "MT2", "MT3", "MT", "LT", "GL", "CTO", "CUB", "PAQ", "LAM", "VAR", "PZA"})
        Me.cboTipoImpuesto.Location = New System.Drawing.Point(106, 43)
        Me.cboTipoImpuesto.Name = "cboTipoImpuesto"
        Me.cboTipoImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoImpuesto.Size = New System.Drawing.Size(319, 21)
        Me.cboTipoImpuesto.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(17, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(83, 17)
        Me.Label6.TabIndex = 144
        Me.Label6.Text = "Tipo Impuesto:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrecioImpuesto1
        '
        Me.txtPrecioImpuesto1.AcceptsReturn = True
        Me.txtPrecioImpuesto1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioImpuesto1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioImpuesto1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioImpuesto1.Location = New System.Drawing.Point(106, 96)
        Me.txtPrecioImpuesto1.MaxLength = 0
        Me.txtPrecioImpuesto1.Name = "txtPrecioImpuesto1"
        Me.txtPrecioImpuesto1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioImpuesto1.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioImpuesto1.TabIndex = 3
        Me.txtPrecioImpuesto1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(17, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(83, 17)
        Me.Label1.TabIndex = 145
        Me.Label1.Text = "Precio con IVA:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(17, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(83, 17)
        Me.Label2.TabIndex = 147
        Me.Label2.Text = "Precio con IVA:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(106, 122)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(68, 20)
        Me.txtCantidad.TabIndex = 4
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FrmCargaProductoTransitorio
        '
        Me.AcceptButton = Me.btnAgregar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(699, 195)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPrecioImpuesto1)
        Me.Controls.Add(Me.cboTipoImpuesto)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.txtPrecioVenta1)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.lblPrecioVenta1)
        Me.Controls.Add(Me._lblLabels_4)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(715, 204)
        Me.Name = "FrmCargaProductoTransitorio"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datos para producto transitorio"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ofdAbrirImagen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents cboTipoImpuesto As ComboBox
    Public WithEvents Label6 As Label
    Public WithEvents txtPrecioImpuesto1 As TextBox
    Public WithEvents Label1 As Label
    Public WithEvents Label2 As Label
    Public WithEvents txtCantidad As TextBox
End Class