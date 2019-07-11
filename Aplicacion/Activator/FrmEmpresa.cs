using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using LeandroSoftware.Core;
using System.Threading.Tasks;
using LeandroSoftware.AccesoDatos.ClienteWCF;
using LeandroSoftware.Puntoventa.CommonTypes;
using LeandroSoftware.Core.Dominio.Entidades;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresa : Form
    {
        private DataTable dtModuloPorEmpresa;
        private DataTable dtReportePorEmpresa;
        private Empresa empresa;
        private bool bolLoading = true;
        private bool bolLogoModificado = false;
        private bool bolCertificadoModificado = false;
        private string strRutaCertificado;
        public bool bolEditing;
        public int intIdEmpresa = -1;

        public FrmEmpresa()
        {
            InitializeComponent();
        }

        private void IniciaMaestroDetalle()
        {
            dtModuloPorEmpresa = new DataTable();
            dtModuloPorEmpresa.Columns.Add("IdModulo", typeof(int));
            dtModuloPorEmpresa.Columns.Add("Descripcion", typeof(string));
            DataColumn[] columns = new DataColumn[1];
            columns[0] = dtModuloPorEmpresa.Columns[0];
            dtModuloPorEmpresa.PrimaryKey = columns;

            dtReportePorEmpresa = new DataTable();
            dtReportePorEmpresa.Columns.Add("IdReporte", typeof(int));
            dtReportePorEmpresa.Columns.Add("NombreReporte", typeof(string));
            columns = new DataColumn[1];
            columns[0] = dtReportePorEmpresa.Columns[0];
            dtReportePorEmpresa.PrimaryKey = columns;
        }

        private void CargarDetalleEmpresa(Empresa empresa)
        {
            List<ModuloPorEmpresa> moduloPorEmpresa = empresa.ModuloPorEmpresa.ToList();
            dtModuloPorEmpresa.Rows.Clear();
            foreach (ModuloPorEmpresa row in moduloPorEmpresa)
            {
                DataRow objRowModulo = dtModuloPorEmpresa.NewRow();
                objRowModulo["IdModulo"] = row.IdModulo;
                objRowModulo["Descripcion"] = row.Modulo.Descripcion;
                dtModuloPorEmpresa.Rows.Add(objRowModulo);
            }
            List<ReportePorEmpresa> reportePorEmpresa = empresa.ReportePorEmpresa.ToList();
            dtReportePorEmpresa.Rows.Clear();
            foreach (ReportePorEmpresa row in reportePorEmpresa)
            {
                DataRow objRowReporte = dtReportePorEmpresa.NewRow();
                objRowReporte["IdReporte"] = row.IdReporte;
                objRowReporte["NombreReporte"] = row.CatalogoReporte.NombreReporte;
                dtReportePorEmpresa.Rows.Add(objRowReporte);
            }
        }

        private void CargarLineaDetalleModulo()
        {
            if (cboModuloPorEmpresa.SelectedValue != null)
            {
                string strValor = cboModuloPorEmpresa.SelectedValue.ToString();
                if (dtModuloPorEmpresa.Rows.IndexOf(dtModuloPorEmpresa.Rows.Find(strValor)) >= 0)
                {
                    MessageBox.Show("El módulo seleccionado ya fue agregado al detalle.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataRow objRowEquipo = dtModuloPorEmpresa.NewRow();
                    objRowEquipo["IdModulo"] = cboModuloPorEmpresa.SelectedValue;
                    objRowEquipo["Descripcion"] = cboModuloPorEmpresa.Text;
                    dtModuloPorEmpresa.Rows.Add(objRowEquipo);
                    dgvModuloPorEmpresa.Refresh();
                }
            }
        }

        private void CargarLineaDetalleReporte()
        {
            if (cboReportePorEmpresa.SelectedValue != null)
            {
                string strValor = cboReportePorEmpresa.SelectedValue.ToString();
                if (dtReportePorEmpresa.Rows.IndexOf(dtReportePorEmpresa.Rows.Find(strValor)) >= 0)
                {
                    MessageBox.Show("El reporte seleccionado ya fue agregado al detalle.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataRow objRowEquipo = dtReportePorEmpresa.NewRow();
                    objRowEquipo["IdReporte"] = cboReportePorEmpresa.SelectedValue;
                    objRowEquipo["NombreReporte"] = cboReportePorEmpresa.Text;
                    dtReportePorEmpresa.Rows.Add(objRowEquipo);
                    dgvReportePorEmpresa.Refresh();
                }
            }
        }

        private void EstablecerPropiedadesDataGridView()
        {
            DataGridViewTextBoxColumn dvcModuloPorEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcDescripcion = new DataGridViewTextBoxColumn();

            dgvModuloPorEmpresa.Columns.Clear();
            dgvModuloPorEmpresa.AutoGenerateColumns = false;

            dvcModuloPorEmpresa.DataPropertyName = "IdModulo";
            dvcModuloPorEmpresa.HeaderText = "";
            dvcModuloPorEmpresa.Width = 20;
            dvcModuloPorEmpresa.Visible = true;
            dvcModuloPorEmpresa.ReadOnly = true;
            dgvModuloPorEmpresa.Columns.Add(dvcModuloPorEmpresa);

            dvcDescripcion.DataPropertyName = "Descripcion";
            dvcDescripcion.HeaderText = "Módulo";
            dvcDescripcion.Width = 322;
            dvcDescripcion.Visible = true;
            dvcDescripcion.ReadOnly = true;
            dgvModuloPorEmpresa.Columns.Add(dvcDescripcion);
            dgvModuloPorEmpresa.Columns[0].Visible = false;

            DataGridViewTextBoxColumn dvcReportePorEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcNombreReporte = new DataGridViewTextBoxColumn();

            dgvReportePorEmpresa.Columns.Clear();
            dgvReportePorEmpresa.AutoGenerateColumns = false;

            dvcReportePorEmpresa.DataPropertyName = "IdReporte";
            dvcReportePorEmpresa.HeaderText = "";
            dvcReportePorEmpresa.Width = 20;
            dvcReportePorEmpresa.Visible = true;
            dvcReportePorEmpresa.ReadOnly = true;
            dgvReportePorEmpresa.Columns.Add(dvcReportePorEmpresa);

            dvcNombreReporte.DataPropertyName = "NombreReporte";
            dvcNombreReporte.HeaderText = "Reporte";
            dvcNombreReporte.Width = 322;
            dvcNombreReporte.Visible = true;
            dvcNombreReporte.ReadOnly = true;
            dgvReportePorEmpresa.Columns.Add(dvcNombreReporte);
        }

        public async Task CargarListaParametros()
        {
            try
            {
                IList<TipoIdentificacion> dsTipoIdentificacion = Array.Empty<TipoIdentificacion>();
                dsTipoIdentificacion = await PuntoventaWCF.ObtenerListaTipoIdentificacion();
                cboTipoIdentificacion.DataSource = dsTipoIdentificacion;
                cboTipoIdentificacion.ValueMember = "IdTipoIdentificacion";
                cboTipoIdentificacion.DisplayMember = "Descripcion";
                // Carga listado módulos
                IList<Modulo> dsModulos = Array.Empty<Modulo>();
                dsModulos = await PuntoventaWCF.ObtenerlistaModulos();
                cboModuloPorEmpresa.DataSource = dsModulos;
                cboModuloPorEmpresa.ValueMember = "IdModulo";
                cboModuloPorEmpresa.DisplayMember = "Descripcion";
                // Carga listado reportes
                IList<CatalogoReporte> dsReportes = Array.Empty<CatalogoReporte>();
                dsReportes = await PuntoventaWCF.ObtenerListaReportes();
                cboReportePorEmpresa.DataSource = dsReportes;
                cboReportePorEmpresa.ValueMember = "IdReporte";
                cboReportePorEmpresa.DisplayMember = "NombreReporte";
                // Carga Tipo Contrato
                IList<TipodeContrato> dsTipoContrato = new List<TipodeContrato>();
                TipodeContrato tipo = new TipodeContrato(1, "Pago mensual o anual");
                dsTipoContrato.Add(tipo);
                tipo = new TipodeContrato(2, "Limite de documentos anual");
                dsTipoContrato.Add(tipo);
                cboTipoContrato.DataSource = dsTipoContrato;
                cboTipoContrato.ValueMember = "IdTipoContrato";
                cboTipoContrato.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public async Task CargarProvincias()
        {
            try
            {
                IList<Provincia> dsDataSet = Array.Empty<Provincia>();
                dsDataSet = await PuntoventaWCF.ObtenerListaProvincias();
                cboProvincia.DataSource = dsDataSet;
                cboProvincia.ValueMember = "IdProvincia";
                cboProvincia.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public async Task CargarCantones(int intIdProvincia)
        {
            try
            {
                IList<Canton> dsDataSet = Array.Empty<Canton>();
                dsDataSet = await PuntoventaWCF.ObtenerListaCantones(intIdProvincia);
                cboCanton.DataSource = dsDataSet;
                cboCanton.ValueMember = "IdCanton";
                cboCanton.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public async Task CargarDistritos(int intIdProvincia, int intIdCanton)
        {
            try
            {
                IList<Distrito> dsDataSet = Array.Empty<Distrito>();
                dsDataSet = await PuntoventaWCF.ObtenerListaDistritos(intIdProvincia, intIdCanton);
                cboDistrito.DataSource = dsDataSet;
                cboDistrito.ValueMember = "IdDistrito";
                cboDistrito.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public async Task CargarBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            try
            {
                IList<Barrio> dsDataSet = Array.Empty<Barrio>();
                dsDataSet = await PuntoventaWCF.ObtenerListaBarrios(intIdProvincia, intIdCanton, intIdDistrito);
                cboBarrio.DataSource = dsDataSet;
                cboBarrio.ValueMember = "IdBarrio";
                cboBarrio.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async void CargarTerminalPorEmpresa()
        {
            if (txtIdEmpresa.Text != "" && txtSucursal.Text != "" && txtTerminal.Text != "")
            {
                TerminalPorEmpresa terminal = await PuntoventaWCF.ObtenerTerminalPorEmpresa(int.Parse(txtIdEmpresa.Text), int.Parse(txtSucursal.Text), int.Parse(txtTerminal.Text));
                if (terminal == null)
                {
                    txtEquipo.Text = "";
                    txtImpresoraFactura.Text = "";
                    txtUltimoDocFE.Text = "0";
                    txtUltimoDocND.Text = "0";
                    txtUltimoDocNC.Text = "0";
                    txtUltimoDocTE.Text = "0";
                    txtUltimoDocMR.Text = "0";
                    MessageBox.Show("La sucursal y terminal ingresados no están registrados para la empresa actual. Ingrese la información y proceda a guardar los cambios. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtEquipo.Text = terminal.ValorRegistro;
                    txtImpresoraFactura.Text = terminal.ImpresoraFactura;
                    txtUltimoDocFE.Text = terminal.UltimoDocFE.ToString();
                    txtUltimoDocND.Text = terminal.UltimoDocND.ToString();
                    txtUltimoDocNC.Text = terminal.UltimoDocNC.ToString();
                    txtUltimoDocTE.Text = terminal.UltimoDocTE.ToString();
                    txtUltimoDocMR.Text = terminal.UltimoDocMR.ToString();
                }
            }
            else
            {
                txtEquipo.Text = "";
                txtImpresoraFactura.Text = "";
                txtUltimoDocFE.Text = "0";
                txtUltimoDocND.Text = "0";
                txtUltimoDocNC.Text = "0";
                txtUltimoDocTE.Text = "0";
                txtUltimoDocMR.Text = "0";
            }
        }

        private async void FrmEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                IniciaMaestroDetalle();
                EstablecerPropiedadesDataGridView();
                await CargarListaParametros();
                await CargarProvincias();
                dgvModuloPorEmpresa.DataSource = dtModuloPorEmpresa;
                dgvReportePorEmpresa.DataSource = dtReportePorEmpresa;
                if (bolEditing)
                {
                    try
                    {
                        empresa = await PuntoventaWCF.ObtenerEmpresa(intIdEmpresa);
                        if (empresa != null)
                        {
                            txtIdEmpresa.Text = empresa.IdEmpresa.ToString();
                            txtNombreEmpresa.Text = empresa.NombreEmpresa;
                            txtNombreComercial.Text = empresa.NombreComercial;
                            cboTipoIdentificacion.SelectedValue = empresa.IdTipoIdentificacion;
                            txtIdentificacion.Text = empresa.Identificacion;
                            await CargarCantones(empresa.IdProvincia);
                            await CargarDistritos(empresa.IdProvincia, empresa.IdCanton);
                            await CargarBarrios(empresa.IdProvincia, empresa.IdCanton, empresa.IdDistrito);
                            cboProvincia.SelectedValue = empresa.IdProvincia;
                            cboCanton.SelectedValue = empresa.IdCanton;
                            cboDistrito.SelectedValue = empresa.IdDistrito;
                            cboBarrio.SelectedValue = empresa.IdBarrio;
                            txtDireccion.Text = empresa.Direccion;
                            txtTelefono.Text = empresa.Telefono;
                            txtCorreoNotificacion.Text = empresa.CorreoNotificacion;
                            txtNombreCertificado.Text = empresa.NombreCertificado;
                            txtPinCertificado.Text = empresa.PinCertificado;
                            txtUsuarioATV.Text = empresa.UsuarioHacienda;
                            txtClaveATV.Text = empresa.ClaveHacienda;
                            txtPorcentajeInstalacion.Text = empresa.PorcentajeInstalacion.ToString();
                            txtLineasFactura.Text = empresa.LineasPorFactura.ToString();
                            txtCodigoServInst.Text = empresa.CodigoServicioInst.ToString();
                            if (empresa.FechaVence != null) txtFecha.Text = DateTime.Parse(empresa.FechaVence.ToString()).ToString("dd-MM-yyyy");
                            cboTipoContrato.SelectedValue = empresa.TipoContrato;
                            txtCantidadDocumentos.Text = empresa.CantidadDisponible.ToString();
                            chkContabiliza.Checked = empresa.Contabiliza;
                            chkIncluyeInsumosEnFactura.Checked = empresa.IncluyeInsumosEnFactura;
                            chkAutoCompleta.Checked = empresa.AutoCompletaProducto;
                            chkModificaDesc.Checked = empresa.ModificaDescProducto;
                            chkCierrePorTurnos.Checked = empresa.CierrePorTurnos;
                            chkDesgloseInst.Checked = empresa.DesglosaServicioInst;
                            chkFacturaElectronica.Checked = empresa.PermiteFacturar;
                            chkRegimenSimplificado.Checked = empresa.RegimenSimplificado;
                            if (empresa.Logotipo != null)
                            {
                                try
                                {
                                    using (MemoryStream ms = new MemoryStream(empresa.Logotipo))
                                        picLogo.Image = Image.FromStream(ms);
                                }
                                catch (Exception)
                                {
                                    picLogo.Image = null;
                                }
                            }
                            else
                            {
                                picLogo.Image = null;
                            }
                            CargarDetalleEmpresa(empresa);
                        }
                        else
                        {
                            MessageBox.Show("No se logró obtener información de la empresa seleccionada.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                else
                {
                    await CargarCantones(1);
                    await CargarDistritos(1, 1);
                    await CargarBarrios(1, 1, 1);
                    empresa = new Empresa();
                }
                bolLoading = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private bool ValidarCampos()
        {
            if (txtIdEmpresa.Text == "" & txtNombreEmpresa.Text == "" & txtUsuarioATV.Text == "" & txtClaveATV.Text == "")
                return false;
            else
            {
                return true;
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void CmdUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Información incompleta", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (txtIdEmpresa.Text != "") empresa.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                empresa.NombreEmpresa = txtNombreEmpresa.Text;
                empresa.NombreComercial = txtNombreComercial.Text;
                empresa.IdTipoIdentificacion = (int)cboTipoIdentificacion.SelectedValue;
                empresa.Identificacion = txtIdentificacion.Text;
                empresa.IdProvincia = (int)cboProvincia.SelectedValue;
                empresa.IdCanton = (int)cboCanton.SelectedValue;
                empresa.IdDistrito = (int)cboDistrito.SelectedValue;
                empresa.IdBarrio = (int)cboBarrio.SelectedValue;
                empresa.Direccion = txtDireccion.Text;
                empresa.Telefono = txtTelefono.Text;
                empresa.CorreoNotificacion = txtCorreoNotificacion.Text;
                empresa.NombreCertificado = txtNombreCertificado.Text;
                empresa.PinCertificado = txtPinCertificado.Text;
                empresa.UsuarioHacienda = txtUsuarioATV.Text;
                empresa.ClaveHacienda = txtClaveATV.Text;
                empresa.PorcentajeInstalacion = int.Parse(txtPorcentajeInstalacion.Text);
                empresa.LineasPorFactura = int.Parse(txtLineasFactura.Text);
                empresa.CodigoServicioInst = int.Parse(txtCodigoServInst.Text);
                if (txtFecha.Text != "") empresa.FechaVence = DateTime.Parse(txtFecha.Text + " 23:59:59");
                empresa.IdTipoMoneda = 1;
                empresa.TipoContrato = (int)cboTipoContrato.SelectedValue;
                empresa.CantidadDisponible = int.Parse(txtCantidadDocumentos.Text);
                empresa.Contabiliza = chkContabiliza.Checked;
                empresa.IncluyeInsumosEnFactura = chkIncluyeInsumosEnFactura.Checked;
                empresa.AutoCompletaProducto = chkAutoCompleta.Checked;
                empresa.ModificaDescProducto = chkModificaDesc.Checked;
                empresa.CierrePorTurnos = chkCierrePorTurnos.Checked;
                empresa.DesglosaServicioInst = chkDesgloseInst.Checked;
                empresa.PermiteFacturar = chkFacturaElectronica.Checked;
                empresa.RegimenSimplificado = chkRegimenSimplificado.Checked;
                empresa.TerminalPorEmpresa.Clear();
                empresa.ModuloPorEmpresa.Clear();
                foreach (DataRow row in dtModuloPorEmpresa.Rows)
                {
                    ModuloPorEmpresa modulo = new ModuloPorEmpresa();
                    if (txtIdEmpresa.Text != "") modulo.IdEmpresa = empresa.IdEmpresa;
                    modulo.IdModulo = int.Parse(row["IdModulo"].ToString());
                    empresa.ModuloPorEmpresa.Add(modulo);
                }
                empresa.ReportePorEmpresa.Clear();
                foreach (DataRow row in dtReportePorEmpresa.Rows)
                {
                    ReportePorEmpresa reporte = new ReportePorEmpresa();
                    if (txtIdEmpresa.Text != "") reporte.IdEmpresa = empresa.IdEmpresa;
                    reporte.IdReporte = int.Parse(row["IdReporte"].ToString());
                    empresa.ReportePorEmpresa.Add(reporte);
                }
                if (txtIdEmpresa.Text == "")
                {
                    string strRespuesta = await PuntoventaWCF.AgregarEmpresa(empresa);
                    strRespuesta = new JavaScriptSerializer().Deserialize<string>(strRespuesta);
                    txtIdEmpresa.Text = strRespuesta;
                    await PuntoventaWCF.AgregarUsuarioPorEmpresa(1, int.Parse(txtIdEmpresa.Text));
                }
                else
                {
                    await PuntoventaWCF.ActualizarEmpresa(empresa);
                }
                if (txtSucursal.Text != "" && txtTerminal.Text != "")
                {
                    TerminalPorEmpresa terminal = new TerminalPorEmpresa();
                    if (txtIdEmpresa.Text != "") terminal.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                    terminal.IdSucursal = int.Parse(txtSucursal.Text);
                    terminal.IdTerminal = int.Parse(txtTerminal.Text);
                    terminal.ValorRegistro = txtEquipo.Text;
                    terminal.ImpresoraFactura = txtImpresoraFactura.Text;
                    terminal.UltimoDocFE = int.Parse(txtUltimoDocFE.Text);
                    terminal.UltimoDocND = int.Parse(txtUltimoDocND.Text);
                    terminal.UltimoDocNC = int.Parse(txtUltimoDocNC.Text);
                    terminal.UltimoDocTE = int.Parse(txtUltimoDocTE.Text);
                    terminal.UltimoDocMR = int.Parse(txtUltimoDocMR.Text);
                    await PuntoventaWCF.ActualizarTerminalPorEmpresa(terminal);
                }
                if (bolLogoModificado)
                {
                    if (picLogo.Image != null)
                    {
                        byte[] bytLogotipo;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            picLogo.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            bytLogotipo = stream.ToArray();
                        }
                        await PuntoventaWCF.ActualizarLogoEmpresa(int.Parse(txtIdEmpresa.Text), Convert.ToBase64String(bytLogotipo));
                    }
                    else
                    {
                        await PuntoventaWCF.RemoverLogoEmpresa(int.Parse(txtIdEmpresa.Text));
                    }
                }
                if (bolCertificadoModificado && txtNombreCertificado.Text != "")
                {
                    byte[] bytCertificado = File.ReadAllBytes(strRutaCertificado);
                    await PuntoventaWCF.ActualizarCertificadoEmpresa(int.Parse(txtIdEmpresa.Text), Convert.ToBase64String(bytCertificado));
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro" + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void CboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                await CargarCantones(intIdProvincia);
                await CargarDistritos(intIdProvincia, 1);
                await CargarBarrios(intIdProvincia, 1, 1);
            }
        }

        private async void CboCanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                int intIdCanton = (int)cboCanton.SelectedValue;
                await CargarDistritos(intIdProvincia, intIdCanton);
                await CargarBarrios(intIdProvincia, intIdCanton, 1);
            }
        }

        private async void CboDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                int intIdCanton = (int)cboCanton.SelectedValue;
                int intIdDistrito = (int)cboDistrito.SelectedValue;
                await CargarBarrios(intIdProvincia, intIdCanton, intIdDistrito);
            }
        }

        private void CmdConsultar_Click(object sender, EventArgs e)
        {
            txtEquipo.Text = Utilitario.ObtenerIdentificadorEquipo();
        }

        private void BtnInsertaModulo_Click(object sender, EventArgs e)
        {
            CargarLineaDetalleModulo();
        }

        private void BtnEliminaModulo_Click(object sender, EventArgs e)
        {
            if (dtModuloPorEmpresa.Rows.Count > 0)
                dtModuloPorEmpresa.Rows.Remove(dtModuloPorEmpresa.Rows.Find(dgvModuloPorEmpresa.CurrentRow.Cells[0].Value));
        }

        private void BtnInsertaReporte_Click(object sender, EventArgs e)
        {
            CargarLineaDetalleReporte();
        }

        private void BtnEliminaReporte_Click(object sender, EventArgs e)
        {
            if (dtReportePorEmpresa.Rows.Count > 0)
                dtReportePorEmpresa.Rows.Remove(dtReportePorEmpresa.Rows.Find(dgvReportePorEmpresa.CurrentRow.Cells[0].Value));
        }

        private void BtnCargarLogo_Click(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "png";
            ofdAbrirDocumento.Filter = "PNG Image Files|*.png;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    picLogo.Image = Image.FromFile(ofdAbrirDocumento.FileName);
                    bolLogoModificado = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el archivo. Verifique que sea un archivo de imagen válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiarLogo_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Desea eliminar el logotipo?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                picLogo.Image = null;
                bolLogoModificado = true;
            }
        }

        private void BtnCargarCertificado_Click(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "p12";
            ofdAbrirDocumento.Filter = "Certificate Files|*.p12;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    strRutaCertificado = ofdAbrirDocumento.FileName;
                    txtNombreCertificado.Text = Path.GetFileName(strRutaCertificado);
                    bolCertificadoModificado = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el certificado. Verifique que sea un archivo .p12 válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSucursal_TextChanged(object sender, EventArgs e)
        {
            if (txtSucursal.Text != "" && txtTerminal.Text != "")
            {
                CargarTerminalPorEmpresa();
            }
        }

        private void txtTerminal_TextChanged(object sender, EventArgs e)
        {
            CargarTerminalPorEmpresa();
        }
    }
}
