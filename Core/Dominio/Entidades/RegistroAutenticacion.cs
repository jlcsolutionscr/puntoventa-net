using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("registroautenticacion")]
    public partial class RegistroAutenticacion
    {
        [Key]
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Role { get; set; }
    }
}
