using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.FacturaElectronicaHacienda.Dominio.Entidades
{
    [Table("tipodecambiodolar")]
    public class TipoDeCambioDolar
    {
        [Key]
        public string FechaTipoCambio { get; set; }
        public decimal ValorTipoCambio { get; set; }
    }
}