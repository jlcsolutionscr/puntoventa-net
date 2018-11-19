using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("proveedor")]
    public partial class Proveedor
    {
        public Proveedor()
        {
            Orden = new HashSet<OrdenCompra>();
            Producto = new HashSet<Producto>();
            Compra = new HashSet<Compra>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdProveedor { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Fax { get; set; }
        public string Correo { get; set; }
        public int? PlazoCredito { get; set; }
        public string Contacto1 { get; set; }
        public string TelCont1 { get; set; }
        public string Contacto2 { get; set; }
        public string TelCont2 { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<OrdenCompra> Orden { get; set; }
        public ICollection<Producto> Producto { get; set; }
        public ICollection<Compra> Compra { get; set; }
    }
}
