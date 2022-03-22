using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Producto
    {
        public int IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public int Tipo { get; set; }
        public int IdLinea { get; set; }
        public string Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string CodigoClasificacion { get; set; }
        public int IdProveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta1 { get; set; }
        public decimal PrecioVenta2 { get; set; }
        public decimal PrecioVenta3 { get; set; }
        public decimal PrecioVenta4 { get; set; }
        public decimal PrecioVenta5 { get; set; }
        public decimal PorcDescuento { get; set; }
        public int IdImpuesto { get; set; }
        public int IndExistencia { get; set; }
        public byte[] Imagen { get; set; }
        public string Marca { get; set; }
        public string Observacion { get; set; }
        public bool ModificaPrecio { get; set; }
        public bool Activo { get; set; }
        public decimal Existencias { get; set; }
        public Linea Linea { get; set; }
        public Proveedor Proveedor { get; set; }
        public List<MovimientoProducto> MovimientoProducto { get; set; }
    }
}