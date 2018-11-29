using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("producto")]
    public partial class Producto
    {
        public Producto()
        {
            MovimientoProducto = new HashSet<MovimientoProducto>();
        }
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdProducto { get; set; }
        [ForeignKey("TipoProducto")]
        public int Tipo { get; set; }
        [ForeignKey("Linea")]
        public int IdLinea { get; set; }
        public string Codigo { get; set; }
        [ForeignKey("Proveedor")]
        public int IdProveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta1 { get; set; }
        public decimal PrecioVenta2 { get; set; }
        public decimal PrecioVenta3 { get; set; }
        public decimal PrecioVenta4 { get; set; }
        public decimal PrecioVenta5 { get; set; }
        public bool Excento { get; set; }
        public int IndExistencia { get; set; }
        [ForeignKey("TipoUnidad")]
        public int IdTipoUnidad { get; set; }
        public byte[] Imagen { get; set; }
        [NotMapped]
        public string TipoProductoDesc { get { if (TipoProducto == null) return ""; else return TipoProducto.Descripcion; } }

        public Linea Linea { get; set; }
        public Empresa Empresa { get; set; }
        public Proveedor Proveedor { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public TipoUnidad TipoUnidad { get; set; }
        public ICollection<MovimientoProducto> MovimientoProducto { get; set; }
    }
}
