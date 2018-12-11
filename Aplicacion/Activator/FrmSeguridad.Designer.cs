namespace LeandroSoftware.Activator
{
    partial class FrmSeguridad
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
            this.CmdCancelar = new System.Windows.Forms.Button();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.TxtClave = new System.Windows.Forms.TextBox();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.LblClave = new System.Windows.Forms.Label();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CmdCancelar
            // 
            this.CmdCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CmdCancelar.Cursor = System.Windows.Forms.Cursors.Default;
            this.CmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CmdCancelar.Location = new System.Drawing.Point(112, 80);
            this.CmdCancelar.Name = "CmdCancelar";
            this.CmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmdCancelar.Size = new System.Drawing.Size(81, 25);
            this.CmdCancelar.TabIndex = 9;
            this.CmdCancelar.Text = "&Cancelar";
            this.CmdCancelar.UseVisualStyleBackColor = false;
            this.CmdCancelar.Click += new System.EventHandler(this.CmdCancelar_Click);
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CmdAceptar.Cursor = System.Windows.Forms.Cursors.Default;
            this.CmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CmdAceptar.Location = new System.Drawing.Point(16, 80);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmdAceptar.Size = new System.Drawing.Size(81, 25);
            this.CmdAceptar.TabIndex = 8;
            this.CmdAceptar.Text = "&Aceptar";
            this.CmdAceptar.UseVisualStyleBackColor = false;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // TxtClave
            // 
            this.TxtClave.AcceptsReturn = true;
            this.TxtClave.BackColor = System.Drawing.SystemColors.Window;
            this.TxtClave.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtClave.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtClave.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtClave.Location = new System.Drawing.Point(96, 48);
            this.TxtClave.MaxLength = 0;
            this.TxtClave.Name = "TxtClave";
            this.TxtClave.PasswordChar = '*';
            this.TxtClave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtClave.Size = new System.Drawing.Size(89, 20);
            this.TxtClave.TabIndex = 7;
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.AcceptsReturn = true;
            this.TxtUsuario.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtUsuario.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtUsuario.Location = new System.Drawing.Point(96, 24);
            this.TxtUsuario.MaxLength = 0;
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtUsuario.Size = new System.Drawing.Size(89, 20);
            this.TxtUsuario.TabIndex = 6;
            // 
            // LblClave
            // 
            this.LblClave.BackColor = System.Drawing.Color.Transparent;
            this.LblClave.Cursor = System.Windows.Forms.Cursors.Default;
            this.LblClave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblClave.Location = new System.Drawing.Point(24, 48);
            this.LblClave.Name = "LblClave";
            this.LblClave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblClave.Size = new System.Drawing.Size(65, 17);
            this.LblClave.TabIndex = 11;
            this.LblClave.Text = "Contraseña";
            // 
            // LblUsuario
            // 
            this.LblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.LblUsuario.Cursor = System.Windows.Forms.Cursors.Default;
            this.LblUsuario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblUsuario.Location = new System.Drawing.Point(24, 24);
            this.LblUsuario.Name = "LblUsuario";
            this.LblUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblUsuario.Size = new System.Drawing.Size(65, 17);
            this.LblUsuario.TabIndex = 10;
            this.LblUsuario.Text = "Usuario";
            // 
            // FrmSeguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(210, 115);
            this.Controls.Add(this.CmdCancelar);
            this.Controls.Add(this.CmdAceptar);
            this.Controls.Add(this.TxtClave);
            this.Controls.Add(this.TxtUsuario);
            this.Controls.Add(this.LblClave);
            this.Controls.Add(this.LblUsuario);
            this.Name = "FrmSeguridad";
            this.Text = "FrmSeguridad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button CmdCancelar;
        public System.Windows.Forms.Button CmdAceptar;
        public System.Windows.Forms.TextBox TxtClave;
        public System.Windows.Forms.TextBox TxtUsuario;
        public System.Windows.Forms.Label LblClave;
        public System.Windows.Forms.Label LblUsuario;
    }
}