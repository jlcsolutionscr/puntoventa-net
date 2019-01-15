namespace LeandroSoftware.Migracion.ClienteWeb
{
    partial class Migracion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbSalida = new System.Windows.Forms.RichTextBox();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.txtIdSucursal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdTerminal = new System.Windows.Forms.TextBox();
            this.chkBancoAdquiriente = new System.Windows.Forms.CheckBox();
            this.chkCliente = new System.Windows.Forms.CheckBox();
            this.chkLinea = new System.Windows.Forms.CheckBox();
            this.chkProveedor = new System.Windows.Forms.CheckBox();
            this.chkProducto = new System.Windows.Forms.CheckBox();
            this.chkFactura = new System.Windows.Forms.CheckBox();
            this.chkEgreso = new System.Windows.Forms.CheckBox();
            this.chkCuentaBanco = new System.Windows.Forms.CheckBox();
            this.chkCuentaEgreso = new System.Windows.Forms.CheckBox();
            this.chkUsuario = new System.Windows.Forms.CheckBox();
            this.chkDocElect = new System.Windows.Forms.CheckBox();
            this.chkVendedor = new System.Windows.Forms.CheckBox();
            this.btnLimpiarRegistro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbSalida
            // 
            this.rtbSalida.Location = new System.Drawing.Point(12, 87);
            this.rtbSalida.Name = "rtbSalida";
            this.rtbSalida.ReadOnly = true;
            this.rtbSalida.Size = new System.Drawing.Size(776, 321);
            this.rtbSalida.TabIndex = 2;
            this.rtbSalida.TabStop = false;
            this.rtbSalida.Text = "";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(671, 414);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(117, 23);
            this.btnProcesar.TabIndex = 3;
            this.btnProcesar.TabStop = false;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // txtIdSucursal
            // 
            this.txtIdSucursal.Location = new System.Drawing.Point(69, 6);
            this.txtIdSucursal.Name = "txtIdSucursal";
            this.txtIdSucursal.Size = new System.Drawing.Size(50, 20);
            this.txtIdSucursal.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sucursal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Terminal:";
            // 
            // txtIdTerminal
            // 
            this.txtIdTerminal.Location = new System.Drawing.Point(192, 6);
            this.txtIdTerminal.Name = "txtIdTerminal";
            this.txtIdTerminal.Size = new System.Drawing.Size(50, 20);
            this.txtIdTerminal.TabIndex = 1;
            // 
            // chkBancoAdquiriente
            // 
            this.chkBancoAdquiriente.AutoSize = true;
            this.chkBancoAdquiriente.Checked = true;
            this.chkBancoAdquiriente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBancoAdquiriente.Location = new System.Drawing.Point(27, 41);
            this.chkBancoAdquiriente.Name = "chkBancoAdquiriente";
            this.chkBancoAdquiriente.Size = new System.Drawing.Size(112, 17);
            this.chkBancoAdquiriente.TabIndex = 6;
            this.chkBancoAdquiriente.Text = "Banco adquiriente";
            this.chkBancoAdquiriente.UseVisualStyleBackColor = true;
            // 
            // chkCliente
            // 
            this.chkCliente.AutoSize = true;
            this.chkCliente.Checked = true;
            this.chkCliente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCliente.Location = new System.Drawing.Point(146, 41);
            this.chkCliente.Name = "chkCliente";
            this.chkCliente.Size = new System.Drawing.Size(58, 17);
            this.chkCliente.TabIndex = 7;
            this.chkCliente.Text = "Cliente";
            this.chkCliente.UseVisualStyleBackColor = true;
            // 
            // chkLinea
            // 
            this.chkLinea.AutoSize = true;
            this.chkLinea.Checked = true;
            this.chkLinea.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLinea.Location = new System.Drawing.Point(265, 41);
            this.chkLinea.Name = "chkLinea";
            this.chkLinea.Size = new System.Drawing.Size(52, 17);
            this.chkLinea.TabIndex = 8;
            this.chkLinea.Text = "Linea";
            this.chkLinea.UseVisualStyleBackColor = true;
            // 
            // chkProveedor
            // 
            this.chkProveedor.AutoSize = true;
            this.chkProveedor.Checked = true;
            this.chkProveedor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProveedor.Location = new System.Drawing.Point(384, 41);
            this.chkProveedor.Name = "chkProveedor";
            this.chkProveedor.Size = new System.Drawing.Size(75, 17);
            this.chkProveedor.TabIndex = 9;
            this.chkProveedor.Text = "Proveedor";
            this.chkProveedor.UseVisualStyleBackColor = true;
            // 
            // chkProducto
            // 
            this.chkProducto.AutoSize = true;
            this.chkProducto.Checked = true;
            this.chkProducto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProducto.Location = new System.Drawing.Point(503, 41);
            this.chkProducto.Name = "chkProducto";
            this.chkProducto.Size = new System.Drawing.Size(69, 17);
            this.chkProducto.TabIndex = 10;
            this.chkProducto.Text = "Producto";
            this.chkProducto.UseVisualStyleBackColor = true;
            // 
            // chkFactura
            // 
            this.chkFactura.AutoSize = true;
            this.chkFactura.Checked = true;
            this.chkFactura.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFactura.Location = new System.Drawing.Point(503, 64);
            this.chkFactura.Name = "chkFactura";
            this.chkFactura.Size = new System.Drawing.Size(62, 17);
            this.chkFactura.TabIndex = 15;
            this.chkFactura.Text = "Factura";
            this.chkFactura.UseVisualStyleBackColor = true;
            // 
            // chkEgreso
            // 
            this.chkEgreso.AutoSize = true;
            this.chkEgreso.Checked = true;
            this.chkEgreso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEgreso.Location = new System.Drawing.Point(384, 64);
            this.chkEgreso.Name = "chkEgreso";
            this.chkEgreso.Size = new System.Drawing.Size(59, 17);
            this.chkEgreso.TabIndex = 14;
            this.chkEgreso.Text = "Egreso";
            this.chkEgreso.UseVisualStyleBackColor = true;
            // 
            // chkCuentaBanco
            // 
            this.chkCuentaBanco.AutoSize = true;
            this.chkCuentaBanco.Checked = true;
            this.chkCuentaBanco.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCuentaBanco.Location = new System.Drawing.Point(265, 64);
            this.chkCuentaBanco.Name = "chkCuentaBanco";
            this.chkCuentaBanco.Size = new System.Drawing.Size(91, 17);
            this.chkCuentaBanco.TabIndex = 13;
            this.chkCuentaBanco.Text = "CuentaBanco";
            this.chkCuentaBanco.UseVisualStyleBackColor = true;
            // 
            // chkCuentaEgreso
            // 
            this.chkCuentaEgreso.AutoSize = true;
            this.chkCuentaEgreso.Checked = true;
            this.chkCuentaEgreso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCuentaEgreso.Location = new System.Drawing.Point(146, 64);
            this.chkCuentaEgreso.Name = "chkCuentaEgreso";
            this.chkCuentaEgreso.Size = new System.Drawing.Size(96, 17);
            this.chkCuentaEgreso.TabIndex = 12;
            this.chkCuentaEgreso.Text = "Cuenta Egreso";
            this.chkCuentaEgreso.UseVisualStyleBackColor = true;
            // 
            // chkUsuario
            // 
            this.chkUsuario.AutoSize = true;
            this.chkUsuario.Checked = true;
            this.chkUsuario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUsuario.Location = new System.Drawing.Point(622, 41);
            this.chkUsuario.Name = "chkUsuario";
            this.chkUsuario.Size = new System.Drawing.Size(62, 17);
            this.chkUsuario.TabIndex = 11;
            this.chkUsuario.Text = "Usuario";
            this.chkUsuario.UseVisualStyleBackColor = true;
            // 
            // chkDocElect
            // 
            this.chkDocElect.AutoSize = true;
            this.chkDocElect.Checked = true;
            this.chkDocElect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDocElect.Location = new System.Drawing.Point(622, 64);
            this.chkDocElect.Name = "chkDocElect";
            this.chkDocElect.Size = new System.Drawing.Size(136, 17);
            this.chkDocElect.TabIndex = 16;
            this.chkDocElect.Text = "Documento electrónico";
            this.chkDocElect.UseVisualStyleBackColor = true;
            // 
            // chkVendedor
            // 
            this.chkVendedor.AutoSize = true;
            this.chkVendedor.Checked = true;
            this.chkVendedor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVendedor.Location = new System.Drawing.Point(27, 64);
            this.chkVendedor.Name = "chkVendedor";
            this.chkVendedor.Size = new System.Drawing.Size(72, 17);
            this.chkVendedor.TabIndex = 17;
            this.chkVendedor.Text = "Vendedor";
            this.chkVendedor.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarRegistro
            // 
            this.btnLimpiarRegistro.Location = new System.Drawing.Point(548, 414);
            this.btnLimpiarRegistro.Name = "btnLimpiarRegistro";
            this.btnLimpiarRegistro.Size = new System.Drawing.Size(117, 23);
            this.btnLimpiarRegistro.TabIndex = 18;
            this.btnLimpiarRegistro.TabStop = false;
            this.btnLimpiarRegistro.Text = "Limpiar registro";
            this.btnLimpiarRegistro.UseVisualStyleBackColor = true;
            this.btnLimpiarRegistro.Click += new System.EventHandler(this.btnLimpiarRegistro_Click);
            // 
            // Migracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLimpiarRegistro);
            this.Controls.Add(this.chkVendedor);
            this.Controls.Add(this.chkDocElect);
            this.Controls.Add(this.chkFactura);
            this.Controls.Add(this.chkEgreso);
            this.Controls.Add(this.chkCuentaBanco);
            this.Controls.Add(this.chkCuentaEgreso);
            this.Controls.Add(this.chkUsuario);
            this.Controls.Add(this.chkProducto);
            this.Controls.Add(this.chkProveedor);
            this.Controls.Add(this.chkLinea);
            this.Controls.Add(this.chkCliente);
            this.Controls.Add(this.chkBancoAdquiriente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIdTerminal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdSucursal);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.rtbSalida);
            this.Name = "Migracion";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbSalida;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.TextBox txtIdSucursal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdTerminal;
        private System.Windows.Forms.CheckBox chkBancoAdquiriente;
        private System.Windows.Forms.CheckBox chkCliente;
        private System.Windows.Forms.CheckBox chkLinea;
        private System.Windows.Forms.CheckBox chkProveedor;
        private System.Windows.Forms.CheckBox chkProducto;
        private System.Windows.Forms.CheckBox chkFactura;
        private System.Windows.Forms.CheckBox chkEgreso;
        private System.Windows.Forms.CheckBox chkCuentaBanco;
        private System.Windows.Forms.CheckBox chkCuentaEgreso;
        private System.Windows.Forms.CheckBox chkUsuario;
        private System.Windows.Forms.CheckBox chkDocElect;
        private System.Windows.Forms.CheckBox chkVendedor;
        private System.Windows.Forms.Button btnLimpiarRegistro;
    }
}

