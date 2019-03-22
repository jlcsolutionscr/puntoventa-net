using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("empresa")]
    public partial class Empresa
    {
        public Empresa()
        {
            TerminalPorEmpresa = new HashSet<TerminalPorEmpresa>();
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
        public DateTime? FechaVence { get; set; }
        public int LineasPorFactura { get; set; }
        public bool Contabiliza { get; set; }
        public bool AutoCompletaProducto { get; set; }
        public bool ModificaDescProducto { get; set; }
        public bool DesglosaServicioInst { get; set; }
        public double PorcentajeInstalacion { get; set; }
        public int CodigoServicioInst { get; set; }
        public bool IncluyeInsumosEnFactura { get; set; }
        public bool CierrePorTurnos { get; set; }
        public bool CierreEnEjecucion { get; set; }
        public bool RegimenSimplificado { get; set; }
        public bool PermiteFacturar { get; set; }
        public byte[] Certificado { get; set; }
        public string NombreCertificado { get; set; }
        public string PinCertificado { get; set; }
        public string UsuarioHacienda { get; set; }
        public string ClaveHacienda { get; set; }
        public string CorreoNotificacion { get; set; }
        public byte[] Logotipo { get; set; }
        public string AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
        public int? RefreshExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? EmitedAt { get; set; }

        public TipoIdentificacion TipoIdentificacion { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        [ForeignKey("IdProvincia, IdCanton, IdDistrito, IdBarrio")]
        public Barrio Barrio { get; set; }
        public ICollection<TerminalPorEmpresa> TerminalPorEmpresa { get; set; }
        public ICollection<ModuloPorEmpresa> ModuloPorEmpresa { get; set; }
        public ICollection<ReportePorEmpresa> ReportePorEmpresa { get; set; }
    }
}
