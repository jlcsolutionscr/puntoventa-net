﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("catalogoreporte")]
    public class CatalogoReporte
    {
        [Key]
        public int IdReporte { get; set; }
        public string NombreReporte { get; set; }
    }
}
