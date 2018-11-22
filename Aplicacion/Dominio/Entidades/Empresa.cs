using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("empresa")]
    public partial class Empresa
    {
        public Empresa()
        {
            DetalleRegistro = new HashSet<DetalleRegistro>();
            ModuloPorEmpresa = new HashSet<ModuloPorEmpresa>();
            ReportePorEmpresa = new HashSet<ReportePorEmpresa>();
        }
        [Key]
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreComercial { get; set; }
        [ForeignKey("TipoIdentificacion")]
        public int IdTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
        public int IdDistrito { get; set; }
        public int IdBarrio { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CuentaCorreoElectronico { get; set; }
        public DateTime? FechaVence { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public int LineasPorFactura { get; set; }
        public bool Contabiliza { get; set; }
        public bool AutoCompletaProducto { get; set; }
        public bool ModificaDescProducto { get; set; }
        public bool DesglosaServicioInst { get; set; }
        public double PorcentajeInstalacion { get; set; }
        public int CodigoServicioInst { get; set; }
        public bool IncluyeInsumosEnFactura { get; set; }
        public bool RespaldoEnLinea { get; set; }
        public bool CierrePorTurnos { get; set; }
        public bool CierreEnEjecucion { get; set; }
        public bool FacturaElectronica { get; set; }
        public string ServicioFacturaElectronicaURL { get; set; }
        public string IdCertificado { get; set; }
        public string PinCertificado { get; set; }
        public int UltimoDocFE { get; set; }
        public int UltimoDocND { get; set; }
        public int UltimoDocNC { get; set; }
        public int UltimoDocTE { get; set; }
        public int UltimoDocMR { get; set; }

        public TipoIdentificacion TipoIdentificacion { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        [ForeignKey("IdProvincia, IdCanton, IdDistrito, IdBarrio")]
        public Barrio Barrio { get; set; }
        public ICollection<DetalleRegistro> DetalleRegistro { get; set; }
        public ICollection<ModuloPorEmpresa> ModuloPorEmpresa { get; set; }
        public ICollection<ReportePorEmpresa> ReportePorEmpresa { get; set; }
    }
}
