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
        public string CodigoActividad { get; set; }
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
        public byte[] Certificado { get; set; }
        public string NombreCertificado { get; set; }
        public string PinCertificado { get; set; }
        public string UsuarioHacienda { get; set; }
        public string ClaveHacienda { get; set; }
        public string CorreoNotificacion { get; set; }
        public decimal MontoRedondeoDescuento { get; set; }
        public string LeyendaFactura { get; set; }
        public string LeyendaProforma { get; set; }
        public string LeyendaApartado { get; set; }
        public string LeyendaOrdenServicio { get; set; }
        public byte[] Logotipo { get; set; }
        public string AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
        public int? RefreshExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? EmitedAt { get; set; }
        public int Modalidad { get; set; }
        public Usuario Usuario;
        public EquipoRegistrado EquipoRegistrado;

        public Barrio Barrio { get; set; }
        public PlanFacturacion PlanFacturacion { get; set; }
        public List<ReportePorEmpresa> ReportePorEmpresa { get; set; }
        public List<RolePorEmpresa> RolePorEmpresa { get; set; }
        public List<SucursalPorEmpresa> SucursalPorEmpresa { get; set; }
    }
}