﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("tipoidentificacion")]
    public partial class TipoIdentificacion
    {
        [Key]
        public int IdTipoIdentificacion { get; set; }
        public string Descripcion { get; set; }
    }
}
