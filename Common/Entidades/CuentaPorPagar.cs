﻿using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class CuentaPorPagar
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdCxP { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdPropietario { get; set; }
        public string Referencia { get; set; }
        public int NroDocOrig { get; set; }
        public DateTime Fecha { get; set; }
        public int Plazo { get; set; }
        public short Tipo { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public int IdAsiento { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
    }
}
