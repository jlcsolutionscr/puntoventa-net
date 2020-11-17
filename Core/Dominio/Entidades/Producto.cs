using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
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
        public string CodigoProveedor { get; set; }
        public string CodigoClasificacion { get; set; }
        [ForeignKey("Proveedor")]
        public int IdProveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta1 { get; set; }
        public decimal PrecioVenta2 { get; set; }
        public decimal PrecioVenta3 { get; set; }
        public decimal PrecioVenta4 { get; set; }
        public decimal PrecioVenta5 { get; set; }
        public decimal PorcDescuento { get; set; }
        [ForeignKey("ParametroImpuesto")]
        public int IdImpuesto { get; set; }
        [NotMapped]
        public int IndExistencia { get; set; }
        public byte[] Imagen { get; set; }
        public string Marca { get; set; }
        public string Observacion { get; set; }
        public bool ModificaPrecio { get; set; }
        public bool Activo { get; set; }
        [NotMapped]
        public string TipoProductoDesc { get { if (TipoProducto == null) return ""; else return TipoProducto.Descripcion; } }
        [NotMapped]
        public decimal Existencias { get; set; }

        public Linea Linea { get; set; }
        public Empresa Empresa { get; set; }
        public Proveedor Proveedor { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public ParametroImpuesto ParametroImpuesto { get; set; }
        public ICollection<MovimientoProducto> MovimientoProducto { get; set; }
    }
}
