using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Cliente
    {
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdTipoPrecio { get; set; }
        public int IdTipoExoneracion { get; set; }
        public string NumDocExoneracion { get; set; }
        public string ArticuloExoneracion { get; set; }
        public string IncisoExoneracion { get; set; }
        public int IdNombreInstExoneracion { get; set; }
        public DateTime FechaEmisionDoc { get; set; }
        public int PorcentajeExoneracion { get; set; }
        public bool PermiteCredito { get; set; }
        public string CodigoActividad { get; set; }
    }
}
