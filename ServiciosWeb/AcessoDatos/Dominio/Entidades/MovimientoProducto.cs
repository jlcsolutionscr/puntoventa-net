using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("movimientoproducto")]
    public partial class MovimientoProducto
    {
        [Key, Column(Order = 0), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        [Key, Column(Order = 1)]
        public DateTime Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public string Tipo { get; set; }
        public string Origen { get; set; }
        public string Referencia { get; set; }
        public decimal PrecioCosto { get; set; }

        public Producto Producto { get; set; }
    }
}
