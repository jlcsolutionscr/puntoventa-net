using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Management;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresa : Form
    {
        private DataTable dtEquipos;
        private DataRow objRowEquipo;
        private Empresa empresa;
        private bool bolLoading = true;
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

        private void IniciaDetalleEmpresa()
        {
            dtEquipos = new DataTable();
            dtEquipos.Columns.Add("VALORREGISTRO", typeof(string));
            dtEquipos.Columns.Add("IMPRESORAFACTURA", typeof(string));
            dtEquipos.Columns.Add("USAIMPRESORAIMPACTO", typeof(bool));
            DataColumn[] columns = new DataColumn[1];
            columns[0] = dtEquipos.Columns[0];
            dtEquipos.PrimaryKey = columns;
        }

        private void CargarDetalleEquipos(List<DetalleRegistro> detalleRegistro)
        {
            dtEquipos.Rows.Clear();
            if (txtIdEmpresa.Text != "")
            {

                foreach (DetalleRegistro row in detalleRegistro)
                {
                    objRowEquipo = dtEquipos.NewRow();
                    objRowEquipo["VALORREGISTRO"] = row.ValorRegistro;
                    objRowEquipo["IMPRESORAFACTURA"] = row.ImpresoraFactura;
                    objRowEquipo["USAIMPRESORAIMPACTO"] = row.UsaImpresoraImpacto;
                    dtEquipos.Rows.Add(objRowEquipo);
                }
            }
        }

        private void CargarLineaDetalleEquipo()
        {
            if (txtEquipo.Text != "")
            {
                int intIndice = dtEquipos.Rows.IndexOf(dtEquipos.Rows.Find(txtEquipo.Text));
                if (intIndice >= 0)
                {
                    dtEquipos.Rows[intIndice]["IMPRESORAFACTURA"] = txtImpresoraFactura.Text;
                    dtEquipos.Rows[intIndice]["USAIMPRESORAIMPACTO"] = chkUsaImpresoraImpacto.Checked;
                }
                else
                {
                    objRowEquipo = dtEquipos.NewRow();
                    objRowEquipo["VALORREGISTRO"] = txtEquipo.Text;
                    objRowEquipo["IMPRESORAFACTURA"] = txtImpresoraFactura.Text;
                    objRowEquipo["USAIMPRESORAIMPACTO"] = chkUsaImpresoraImpacto.Checked;
                    dtEquipos.Rows.Add(objRowEquipo);
                    dgvEquipos.Refresh();
                    txtEquipo.Text = "";
                    txtImpresoraFactura.Text = "";
                }
            }
        }

        private void EstablecerPropiedadesDataGridView()
        {
            dgvEquipos.Columns.Clear();
            dgvEquipos.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn dvcValorEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcImpresoraFactura = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcUsaImpresoraImpacto = new DataGridViewTextBoxColumn();

            dvcValorEmpresa.DataPropertyName = "VALORREGISTRO";
            dvcValorEmpresa.HeaderText = "Equipo";
            dvcValorEmpresa.Width = 150;
            dvcValorEmpresa.Visible = true;
            dvcValorEmpresa.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcValorEmpresa);

            dvcImpresoraFactura.DataPropertyName = "IMPRESORAFACTURA";
            dvcImpresoraFactura.HeaderText = "Impresora";
            dvcImpresoraFactura.Width = 100;
            dvcImpresoraFactura.Visible = true;
            dvcImpresoraFactura.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcImpresoraFactura);

            dvcUsaImpresoraImpacto.DataPropertyName = "USAIMPRESORAIMPACTO";
            dvcUsaImpresoraImpacto.HeaderText = "Impacto";
            dvcUsaImpresoraImpacto.Width = 80;
            dvcUsaImpresoraImpacto.Visible = true;
            dvcUsaImpresoraImpacto.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcUsaImpresoraImpacto);
        }

        public void CargarTiposIdentificacion()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoIdentificacion",
                DatosPeticion = ""
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = serializer.Serialize(peticion);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = serializer.Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<TipoIdentificacion> dsDataSet = Array.Empty<TipoIdentificacion>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = serializer.Deserialize<List<TipoIdentificacion>>(respuesta.DatosPeticion);
                }
                cboTipoIdentificacion.DataSource = dsDataSet;
                cboTipoIdentificacion.ValueMember = "IdTipoIdentificacion";
                cboTipoIdentificacion.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarProvincias()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProvincias",
                DatosPeticion = ""
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = serializer.Serialize(peticion);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = serializer.Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Provincia> dsDataSet = Array.Empty<Provincia>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = serializer.Deserialize<List<Provincia>>(respuesta.DatosPeticion);
                }
                cboProvincia.DataSource = dsDataSet;
                cboProvincia.ValueMember = "IdProvincia";
                cboProvincia.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarCantones(int intIdProvincia)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCantones",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + "}"
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = serializer.Serialize(peticion);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = serializer.Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Canton> dsDataSet = Array.Empty<Canton>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = serializer.Deserialize<List<Canton>>(respuesta.DatosPeticion);
                }
                cboCanton.DataSource = dsDataSet;
                cboCanton.ValueMember = "IdCanton";
                cboCanton.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarDistritos(int intIdProvincia, int intIdCanton)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDistritos",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ",IdCanton: " + intIdCanton + "}"
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = serializer.Serialize(peticion);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = serializer.Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Distrito> dsDataSet = Array.Empty<Distrito>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = serializer.Deserialize<List<Distrito>>(respuesta.DatosPeticion);
                }
                cboDistrito.DataSource = dsDataSet;
                cboDistrito.ValueMember = "IdDistrito";
                cboDistrito.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaBarrios",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + ", IdDistrito: " + intIdDistrito + "}"
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = serializer.Serialize(peticion);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = serializer.Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Barrio> dsDataSet = Array.Empty<Barrio>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = serializer.Deserialize<List<Barrio>>(respuesta.DatosPeticion);
                }
                cboBarrio.DataSource = dsDataSet;
                cboBarrio.ValueMember = "IdBarrio";
                cboBarrio.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void FrmEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                IniciaDetalleEmpresa();
                EstablecerPropiedadesDataGridView();
                CargarTiposIdentificacion();
                CargarProvincias();
                CargarCantones(1);
                CargarDistritos(1, 1);
                CargarBarrios(1, 1, 1);
                dgvEquipos.DataSource = dtEquipos;
                if (bolEditing)
                {
                    RequestDTO peticion = new RequestDTO
                    {
                        NombreMetodo = "ConsultarEmpresa",
                        DatosPeticion = intIdEmpresa.ToString()
                    };

                    Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
                    string jsonRequest = serializer.Serialize(peticion);
                    StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
                    try
                    {
                        task1.Wait();
                        if (!task1.Result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close();
                        }
                        else
                        {
                            string results = task1.Result.Content.ReadAsStringAsync().Result;
                            ResponseDTO respuesta = serializer.Deserialize<ResponseDTO>(results);
                            if (respuesta.DatosPeticion != null)
                            {
                                empresa = serializer.Deserialize<Empresa>(respuesta.DatosPeticion);
                            }
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
                            chkRespaldoEnLinea.Checked = empresa.RespaldoEnLinea;
                            chkModificaDesc.Checked = empresa.ModificaDescProducto;
                            chkCierrePorTurnos.Checked = empresa.CierrePorTurnos;
                            chkDesgloseInst.Checked = empresa.DesglosaServicioInst;
                            chkFacturaElectronica.Checked = empresa.PermiteFacturar;
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
                            CargarDetalleEquipos(empresa.DetalleRegistro.ToList());
                        }
                    }
                    catch (AggregateException ex)
                    {
                        Exception newEx = ex.Flatten();
                        MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                else
                {
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
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
                empresa.RespaldoEnLinea = chkRespaldoEnLinea.Checked;
                empresa.ModificaDescProducto = chkModificaDesc.Checked;
                empresa.CierrePorTurnos = chkCierrePorTurnos.Checked;
                empresa.DesglosaServicioInst = chkDesgloseInst.Checked;
                empresa.PermiteFacturar = chkFacturaElectronica.Checked;
                if (picLogo.Image != null)
                {
                    MemoryStream stream = new MemoryStream();
                    picLogo.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    empresa.Logotipo = stream.ToArray();
                }
                else
                {
                    empresa.Logotipo = null;
                }
                empresa.DetalleRegistro.Clear();
                foreach (DataRow row in dtEquipos.Rows)
                {
                    DetalleRegistro detalle = new DetalleRegistro();
                    if (txtIdEmpresa.Text != "") detalle.IdEmpresa = empresa.IdEmpresa;
                    detalle.ValorRegistro = row["VALORREGISTRO"].ToString();
                    detalle.ImpresoraFactura = row["IMPRESORAFACTURA"].ToString();
                    detalle.UsaImpresoraImpacto = (bool)row["USAIMPRESORAIMPACTO"];
                    empresa.DetalleRegistro.Add(detalle);
                }
                string strDatos = serializer.Serialize(empresa);
                RequestDTO peticion = new RequestDTO();
                Uri uri;
                if (txtIdEmpresa.Text == "")
                {
                    peticion.NombreMetodo = "AgregarEmpresa";
                    uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
                }
                else
                {
                    peticion.NombreMetodo = "ActualizarEmpresa";
                    uri = new Uri(strServicioPuntoventaURL + "/ejecutar");
                }
                peticion.DatosPeticion = strDatos;
                string jsonRequest = serializer.Serialize(peticion);
                StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
                try
                {
                    task1.Wait();
                    if (!task1.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (txtIdEmpresa.Text == "")
                    {
                        string results = task1.Result.Content.ReadAsStringAsync().Result;
                        ResponseDTO respuesta = serializer.Deserialize<ResponseDTO>(results);
                        txtIdEmpresa.Text = respuesta.DatosPeticion;
                    }
                       
                }
                catch (AggregateException ex)
                {
                    Exception newEx = ex.Flatten();
                    MessageBox.Show("Error al actualizar el registro: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro" + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCargarLogo_Click(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "png";
            ofdAbrirDocumento.Filter = "PNG Image Files|*.png;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    picLogo.Image = Image.FromFile(ofdAbrirDocumento.FileName);
                } catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el archivo. Verifique que sea un archivo de imagen válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                CargarCantones(intIdProvincia);
                CargarDistritos(intIdProvincia, 1);
                CargarBarrios(intIdProvincia, 1, 1);
            }
        }

        private void cboCanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                int intIdCanton = (int)cboCanton.SelectedValue;
                CargarDistritos(intIdProvincia, intIdCanton);
                CargarBarrios(intIdProvincia, intIdCanton, 1);
            }
        }

        private void cboDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                int intIdCanton = (int)cboCanton.SelectedValue;
                int intIdDistrito = (int)cboDistrito.SelectedValue;
                CargarBarrios(intIdProvincia, intIdCanton, intIdDistrito);
            }
        }

        private void CmdConsultar_Click(object sender, EventArgs e)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                if (wmi_HD["SerialNumber"] != null)
                    txtEquipo.Text = wmi_HD["SerialNumber"].ToString();
            }
        }

        private void btnInsertarDetalle_Click(object sender, EventArgs e)
        {
            CargarLineaDetalleEquipo();
        }

        private void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            if (dtEquipos.Rows.Count > 0)
                dtEquipos.Rows.Remove(dtEquipos.Rows.Find(dgvEquipos.CurrentRow.Cells[0].Value));
        }

        private void btnCargarLogo_Click_1(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "png";
            ofdAbrirDocumento.Filter = "PNG Image Files|*.png;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    picLogo.Image = Image.FromFile(ofdAbrirDocumento.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el archivo. Verifique que sea un archivo de imagen válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCargarCertificado_Click(object sender, EventArgs e)
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el certificado. Verifique que sea un archivo .p12 válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
