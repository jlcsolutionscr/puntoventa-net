namespace LeandroSoftware.Activator
{
    partial class FrmParametrosSistema
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdAbrirDocumento = new System.Windows.Forms.OpenFileDialog();
            this.txtPendientes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRecepcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkModoMantenimiento = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtPendientes
            // 
            this.txtPendientes.Location = new System.Drawing.Point(159, 13);
            this.txtPendientes.Name = "txtPendientes";
            this.txtPendientes.Size = new System.Drawing.Size(145, 20);
            this.txtPendientes.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ultima ejecución pendientes:";
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnActualizar.Enabled = false;
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnActualizar.Location = new System.Drawing.Point(114, 103);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnActualizar.Size = new System.Drawing.Size(90, 25);
            this.btnActualizar.TabIndex = 9;
            this.btnActualizar.TabStop = false;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Ultima ejecución recepción:";
            // 
            // txtRecepcion
            // 
            this.txtRecepcion.Location = new System.Drawing.Point(159, 39);
            this.txtRecepcion.Name = "txtRecepcion";
            this.txtRecepcion.Size = new System.Drawing.Size(145, 20);
            this.txtRecepcion.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "En mantenimiento:";
            // 
            // chkModoMantenimiento
            // 
            this.chkModoMantenimiento.AutoSize = true;
            this.chkModoMantenimiento.Location = new System.Drawing.Point(159, 67);
            this.chkModoMantenimiento.Name = "chkModoMantenimiento";
            this.chkModoMantenimiento.Size = new System.Drawing.Size(15, 14);
            this.chkModoMantenimiento.TabIndex = 14;
            this.chkModoMantenimiento.UseVisualStyleBackColor = true;
            // 
            // FrmParametrosSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(320, 140);
            this.Controls.Add(this.chkModoMantenimiento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRecepcion);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPendientes);
            this.MaximumSize = new System.Drawing.Size(336, 179);
            this.MinimumSize = new System.Drawing.Size(336, 179);
            this.Name = "FrmParametrosSistema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizar parámetros del sistema";
            this.Load += new System.EventHandler(this.FrmSubirActualizacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.OpenFileDialog ofdAbrirDocumento;
        private System.Windows.Forms.TextBox txtPendientes;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRecepcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkModoMantenimiento;
    }
}