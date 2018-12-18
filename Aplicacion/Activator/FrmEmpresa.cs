using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using LeandroSoftware.Core;
using System.Threading.Tasks;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresa : Form
    {
        private DataTable dtEquipos;
        private DataTable dtModuloPorEmpresa;
        private DataTable dtReportePorEmpresa;
        private Empresa empresa;
        private bool bolLoading = true;
        private bool bolLogoModificado = false;
        private bool bolCertificadoModificado = false;
        private string strRutaCertificado;
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private static HttpClient client = new HttpClient();
        public string strServicioPuntoventaURL;
        public bool bolEditing;
        public int intIdEmpresa = -1;

        public FrmEmpresa()
        {
            InitializeComponent();
        }

        private void IniciaMaestroDetalle()
        {
            dtEquipos = new DataTable();
            dtEquipos.Columns.Add("ValorRegistro", typeof(string));
            dtEquipos.Columns.Add("ImpresoraFactura", typeof(string));
            dtEquipos.Columns.Add("UsaImpresoraImpacto", typeof(bool));
            DataColumn[] columns = new DataColumn[1];
            columns[0] = dtEquipos.Columns[0];
            dtEquipos.PrimaryKey = columns;

            dtModuloPorEmpresa = new DataTable();
            dtModuloPorEmpresa.Columns.Add("IdModulo", typeof(int));
            dtModuloPorEmpresa.Columns.Add("Descripcion", typeof(string));
            columns = new DataColumn[1];
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
            List<DetalleRegistro> detalleRegistro = empresa.DetalleRegistro.ToList();
            dtEquipos.Rows.Clear();
            foreach (DetalleRegistro row in detalleRegistro)
            {
                DataRow objRowEquipo = dtEquipos.NewRow();
                objRowEquipo["ValorRegistro"] = row.ValorRegistro;
                objRowEquipo["ImpresoraFactura"] = row.ImpresoraFactura;
                objRowEquipo["UsaImpresoraImpacto"] = row.UsaImpresoraImpacto;
                dtEquipos.Rows.Add(objRowEquipo);
            }
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

        private void CargarLineaDetalleEquipo()
        {
            if (txtEquipo.Text != "")
            {
                int intIndice = dtEquipos.Rows.IndexOf(dtEquipos.Rows.Find(txtEquipo.Text));
                if (intIndice >= 0)
                {
                    dtEquipos.Rows[intIndice]["ImpresoraFactura"] = txtImpresoraFactura.Text;
                    dtEquipos.Rows[intIndice]["UsaImpresoraImpacto"] = chkUsaImpresoraImpacto.Checked;
                }
                else
                {
                    DataRow objRowEquipo = dtEquipos.NewRow();
                    objRowEquipo["ValorRegistro"] = txtEquipo.Text;
                    objRowEquipo["ImpresoraFactura"] = txtImpresoraFactura.Text;
                    objRowEquipo["UsaImpresoraImpacto"] = chkUsaImpresoraImpacto.Checked;
                    dtEquipos.Rows.Add(objRowEquipo);
                    dgvEquipos.Refresh();
                    txtEquipo.Text = "";
                    txtImpresoraFactura.Text = "";
                }
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
            DataGridViewTextBoxColumn dvcEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcDetalle1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcDetalle2 = new DataGridViewTextBoxColumn();

            dgvEquipos.Columns.Clear();
            dgvEquipos.AutoGenerateColumns = false;

            dvcEmpresa.DataPropertyName = "ValorRegistro";
            dvcEmpresa.HeaderText = "Equipo";
            dvcEmpresa.Width = 150;
            dvcEmpresa.Visible = true;
            dvcEmpresa.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcEmpresa);

            dvcDetalle1.DataPropertyName = "ImpresoraFactura";
            dvcDetalle1.HeaderText = "Impresora";
            dvcDetalle1.Width = 100;
            dvcDetalle1.Visible = true;
            dvcDetalle1.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcDetalle1);

            dvcDetalle2.DataPropertyName = "UsaImpresoraImpacto";
            dvcDetalle2.HeaderText = "Impacto";
            dvcDetalle2.Width = 80;
            dvcDetalle2.Visible = true;
            dvcDetalle2.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcDetalle2);

            DataGridViewTextBoxColumn dvcModuloPorEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcDescripcion = new DataGridViewTextBoxColumn();

            dgvModuloPorEmpresa.Columns.Clear();
            dgvModuloPorEmpresa.AutoGenerateColumns = false;

            dvcModuloPorEmpresa.DataPropertyName = "IdModulo";
            dvcModuloPorEmpresa.HeaderText = "";
            dvcModuloPorEmpresa.Width = 5;
            dvcModuloPorEmpresa.Visible = false;
            dvcModuloPorEmpresa.ReadOnly = false;
            dgvModuloPorEmpresa.Columns.Add(dvcModuloPorEmpresa);

            dvcDescripcion.DataPropertyName = "Descripcion";
            dvcDescripcion.HeaderText = "Módulo";
            dvcDescripcion.Width = 342;
            dvcDescripcion.Visible = true;
            dvcDescripcion.ReadOnly = true;
            dgvModuloPorEmpresa.Columns.Add(dvcDescripcion);

            DataGridViewTextBoxColumn dvcReportePorEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcNombreReporte = new DataGridViewTextBoxColumn();

            dgvReportePorEmpresa.Columns.Clear();
            dgvReportePorEmpresa.AutoGenerateColumns = false;

            dvcReportePorEmpresa.DataPropertyName = "IdReporte";
            dvcReportePorEmpresa.HeaderText = "";
            dvcReportePorEmpresa.Width = 5;
            dvcReportePorEmpresa.Visible = false;
            dvcReportePorEmpresa.ReadOnly = false;
            dgvReportePorEmpresa.Columns.Add(dvcReportePorEmpresa);

            dvcNombreReporte.DataPropertyName = "NombreReporte";
            dvcNombreReporte.HeaderText = "Reporte";
            dvcNombreReporte.Width = 342;
            dvcNombreReporte.Visible = true;
            dvcNombreReporte.ReadOnly = true;
            dgvReportePorEmpresa.Columns.Add(dvcNombreReporte);
        }

        public async Task CargarListaParametros()
        {
            try
            {
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerListaTipoIdentificacion",
                    DatosPeticion = ""
                };
                string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                string strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                IList<TipoIdentificacion> dsTipoIdentificacion = Array.Empty<TipoIdentificacion>();
                dsTipoIdentificacion = serializer.Deserialize<List<TipoIdentificacion>>(strRespuesta);
                cboTipoIdentificacion.DataSource = dsTipoIdentificacion;
                cboTipoIdentificacion.ValueMember = "IdTipoIdentificacion";
                cboTipoIdentificacion.DisplayMember = "Descripcion";
                // Carga listado módulos
                peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerListaModulos",
                    DatosPeticion = ""
                };
                strPeticion = new JavaScriptSerializer().Serialize(peticion);
                strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                IList<Modulo> dsModulos = Array.Empty<Modulo>();
                dsModulos = serializer.Deserialize<List<Modulo>>(strRespuesta);
                cboModuloPorEmpresa.DataSource = dsModulos;
                cboModuloPorEmpresa.ValueMember = "IdModulo";
                cboModuloPorEmpresa.DisplayMember = "Descripcion";
                // Carga listado reportes
                peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerListaReportes",
                    DatosPeticion = ""
                };
                strPeticion = new JavaScriptSerializer().Serialize(peticion);
                strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                IList<CatalogoReporte> dsReportes = Array.Empty<CatalogoReporte>();
                dsReportes = serializer.Deserialize<List<CatalogoReporte>>(strRespuesta);
                cboReportePorEmpresa.DataSource = dsReportes;
                cboReportePorEmpresa.ValueMember = "IdReporte";
                cboReportePorEmpresa.DisplayMember = "NombreReporte";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public async Task CargarProvincias()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProvincias",
                DatosPeticion = ""
            };
            try
            {
                string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                string strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                IList<Provincia> dsDataSet = Array.Empty<Provincia>();
                dsDataSet = serializer.Deserialize<List<Provincia>>(strRespuesta);
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
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCantones",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + "}"
            };
            try
            {
                string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                string strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                IList<Canton> dsDataSet = Array.Empty<Canton>();
                dsDataSet = serializer.Deserialize<List<Canton>>(strRespuesta);
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
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDistritos",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ",IdCanton: " + intIdCanton + "}"
            };
            try
            {
                string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                string strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                IList<Distrito> dsDataSet = Array.Empty<Distrito>();
                dsDataSet = serializer.Deserialize<List<Distrito>>(strRespuesta);
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
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaBarrios",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + ", IdDistrito: " + intIdDistrito + "}"
            };
            try
            {
                string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                string strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                IList<Barrio> dsDataSet = Array.Empty<Barrio>();
                dsDataSet = serializer.Deserialize<List<Barrio>>(strRespuesta);
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

        private async void FrmEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                IniciaMaestroDetalle();
                EstablecerPropiedadesDataGridView();
                await CargarListaParametros();
                await CargarProvincias();
                await CargarCantones(1);
                await CargarDistritos(1, 1);
                await CargarBarrios(1, 1, 1);
                bolLoading = false;
                dgvEquipos.DataSource = dtEquipos;
                dgvModuloPorEmpresa.DataSource = dtModuloPorEmpresa;
                dgvReportePorEmpresa.DataSource = dtReportePorEmpresa;
                if (bolEditing)
                {
                    RequestDTO peticion = new RequestDTO
                    {
                        NombreMetodo = "ObtenerEmpresa",
                        DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
                    };
                    try
                    {
                        string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                        string strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                        strRespuesta = serializer.Deserialize<string>(strRespuesta);
                        if (strRespuesta != "")
                        {
                            empresa = serializer.Deserialize<Empresa>(strRespuesta);
                            txtIdEmpresa.Text = empresa.IdEmpresa.ToString();
                            txtNombreEmpresa.Text = empresa.NombreEmpresa;
                            txtNombreComercial.Text = empresa.NombreComercial;
                            cboTipoIdentificacion.SelectedValue = empresa.IdTipoIdentificacion;
                            txtIdentificacion.Text = empresa.Identificacion;
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
                            txtPorcentajeIVA.Text = empresa.PorcentajeIVA.ToString();
                            txtPorcentajeInstalacion.Text = empresa.PorcentajeInstalacion.ToString();
                            txtLineasFactura.Text = empresa.LineasPorFactura.ToString();
                            txtCodigoServInst.Text = empresa.CodigoServicioInst.ToString();
                            if (empresa.FechaVence != null) txtFecha.Text = DateTime.Parse(empresa.FechaVence.ToString()).ToString("dd-MM-yyyy");
                            txtUltimoDocFE.Text = empresa.UltimoDocFE.ToString();
                            txtUltimoDocNC.Text = empresa.UltimoDocNC.ToString();
                            txtUltimoDocND.Text = empresa.UltimoDocND.ToString();
                            txtUltimoDocTE.Text = empresa.UltimoDocTE.ToString();
                            txtUltimoDocMR.Text = empresa.UltimoDocMR.ToString();
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
                    empresa = new Empresa();
                }
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
                empresa.PorcentajeIVA = int.Parse(txtPorcentajeIVA.Text);
                empresa.PorcentajeInstalacion = int.Parse(txtPorcentajeInstalacion.Text);
                empresa.LineasPorFactura = int.Parse(txtLineasFactura.Text);
                empresa.CodigoServicioInst = int.Parse(txtCodigoServInst.Text);
                if (txtFecha.Text != "") empresa.FechaVence = DateTime.Parse(txtFecha.Text + " 23:59:59");
                empresa.UltimoDocFE = int.Parse(txtUltimoDocFE.Text);
                empresa.UltimoDocNC = int.Parse(txtUltimoDocNC.Text);
                empresa.UltimoDocND = int.Parse(txtUltimoDocND.Text);
                empresa.UltimoDocTE = int.Parse(txtUltimoDocTE.Text);
                empresa.UltimoDocMR = int.Parse(txtUltimoDocMR.Text);
                empresa.Contabiliza = chkContabiliza.Checked;
                empresa.IncluyeInsumosEnFactura = chkIncluyeInsumosEnFactura.Checked;
                empresa.AutoCompletaProducto = chkAutoCompleta.Checked;
                empresa.ModificaDescProducto = chkModificaDesc.Checked;
                empresa.CierrePorTurnos = chkCierrePorTurnos.Checked;
                empresa.DesglosaServicioInst = chkDesgloseInst.Checked;
                empresa.PermiteFacturar = chkFacturaElectronica.Checked;
                empresa.RegimenSimplificado = chkRegimenSimplificado.Checked;
                empresa.DetalleRegistro.Clear();
                foreach (DataRow row in dtEquipos.Rows)
                {
                    DetalleRegistro detalle = new DetalleRegistro();
                    if (txtIdEmpresa.Text != "") detalle.IdEmpresa = empresa.IdEmpresa;
                    detalle.ValorRegistro = row["ValorRegistro"].ToString();
                    detalle.ImpresoraFactura = row["ImpresoraFactura"].ToString();
                    detalle.UsaImpresoraImpacto = (bool)row["UsaImpresoraImpacto"];
                    empresa.DetalleRegistro.Add(detalle);
                }
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
                string strDatos = serializer.Serialize(empresa);
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = txtIdEmpresa.Text == "" ? "AgregarEmpresa" : "ActualizarEmpresa",
                    DatosPeticion = strDatos
                };
                try
                {
                    string strPeticion = serializer.Serialize(peticion);
                    string strRespuesta = null;
                    if (txtIdEmpresa.Text == "")
                    {
                        strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
                        strRespuesta = new JavaScriptSerializer().Deserialize<string>(strRespuesta);
                        txtIdEmpresa.Text = strRespuesta;
                    }
                    else
                    {
                        await Utilitario.Ejecutar(strPeticion, strServicioPuntoventaURL, "");
                    }
                       
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (bolLogoModificado && picLogo.Image != null)
                {
                    byte[] bytLogotipo;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        picLogo.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        bytLogotipo = stream.ToArray();
                    }
                    peticion = new RequestDTO
                    {
                        NombreMetodo = "ActualizarLogoEmpresa",
                        DatosPeticion = "{IdEmpresa: " + txtIdEmpresa.Text + ", Logotipo: '" + Convert.ToBase64String(bytLogotipo) + "'}"
                    };
                    try
                    {
                        string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                        await Utilitario.Ejecutar(strPeticion, strServicioPuntoventaURL, "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (bolCertificadoModificado && txtNombreCertificado.Text != "")
                {
                    byte[] bytCertificado = File.ReadAllBytes(strRutaCertificado);
                    peticion = new RequestDTO
                    {
                        NombreMetodo = "ActualizarCertificadoEmpresa",
                        DatosPeticion = "{IdEmpresa: " + txtIdEmpresa.Text + ", Certificado: '" + Convert.ToBase64String(bytCertificado) + "'}"
                    };
                    try
                    {
                        string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                        await Utilitario.Ejecutar(strPeticion, strServicioPuntoventaURL, "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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
            txtEquipo.Text = Utilitario.ObtenerSerialEquipo();
        }

        private void BtnInsertarDetalle_Click(object sender, EventArgs e)
        {
            CargarLineaDetalleEquipo();
        }

        private void BtnEliminarDetalle_Click(object sender, EventArgs e)
        {
            if (dtEquipos.Rows.Count > 0)
                dtEquipos.Rows.Remove(dtEquipos.Rows.Find(dgvEquipos.CurrentRow.Cells[0].Value));
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
    }
}
