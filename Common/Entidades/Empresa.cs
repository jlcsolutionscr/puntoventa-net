using System;
using System.Collections.Generic;
using LeandroSoftware.Common.DatosComunes;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Empresa
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreComercial { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
        public int IdDistrito { get; set; }
        public int IdBarrio { get; set; }
        public int IdTipoMoneda { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public int TipoContrato { get; set; }
        public int CantidadDisponible { get; set; }
        public DateTime? FechaVence { get; set; }
        public int LineasPorFactura { get; set; }
        public bool Contabiliza { get; set; }
        public bool AutoCompletaProducto { get; set; }
        public bool RegimenSimplificado { get; set; }
        public bool PermiteFacturar { get; set; }
        public bool RecepcionGastos { get; set; }
        public bool AsignaVendedorPorDefecto { get; set; }
        public bool IngresaPagoCliente { get; set; }
        public bool PrecioVentaIncluyeIVA { get; set; }
        public string CorreoNotificacion { get; set; }
        public decimal MontoRedondeoDescuento { get; set; }
        public string LeyendaFactura { get; set; }
        public string LeyendaProforma { get; set; }
        public string LeyendaApartado { get; set; }
        public string LeyendaOrdenServicio { get; set; }
        public byte[] Logotipo { get; set; }
        public int Modalidad { get; set; }
        public Usuario Usuario { get; set; }
        public EquipoRegistrado EquipoRegistrado { get; set; }
        public Barrio Barrio { get; set; }
        public PlanFacturacion PlanFacturacion { get; set; }
        public List<ReportePorEmpresa> ReportePorEmpresa { get; set; }
        public List<RolePorEmpresa> RolePorEmpresa { get; set; }
        public List<ActividadEconomicaEmpresa> ActividadEconomicaEmpresa { get; set; }
        public List<SucursalPorEmpresa> SucursalPorEmpresa { get; set; }
        public IList<LlaveDescripcion> ListadoTipoIdentificacion { get; set; }
        public IList<LlaveDescripcion> ListadoFormaPagoCliente { get; set; }
        public IList<LlaveDescripcion> ListadoFormaPagoEmpresa { get; set; }
        public IList<LlaveDescripcion> ListadoTipoProducto { get; set; }
        public IList<LlaveDescripcionValor> ListadoTipoImpuesto { get; set; }
        public IList<LlaveDescripcion> ListadoTipoMoneda { get; set; }
        public IList<LlaveDescripcion> ListadoCondicionVenta { get; set; }
        public IList<LlaveDescripcion> ListadoTipoExoneracion { get; set; }
        public IList<LlaveDescripcion> ListadoNombreInstExoneracion { get; set; }
        public IList<LlaveDescripcion> ListadoTipoPrecio { get; set; }
    }
}