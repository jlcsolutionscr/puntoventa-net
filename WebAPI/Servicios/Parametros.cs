using LeandroSoftware.Common.DatosComunes;

namespace LeandroSoftware.ServicioWeb.Parametros
{
    public static class CondicionDeVenta
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Contado"),
            new LlaveDescripcion(2, "Crédito"),
            new LlaveDescripcion(3, "Consignación"),
            new LlaveDescripcion(4, "Apartado"),
            new LlaveDescripcion(5, "Arrendamiento con opción de compra"),
            new LlaveDescripcion(6, "Arrendamiento en función financiera"),
            new LlaveDescripcion(7, "Otros")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }

        public static string ObtenerDescripcion(int intId)
        {
            LlaveDescripcion item = listado.FirstOrDefault(x => x.Id == intId);
            if (item != null) return item.Descripcion;
            return "";
        }
    }

    public static class FormaDePago
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Contado"),
            new LlaveDescripcion(2, "Tarjeta"),
            new LlaveDescripcion(3, "Cheque"),
            new LlaveDescripcion(4, "Transferencia – depósito bancario"),
            new LlaveDescripcion(5, "Recaudado por terceros"),
            new LlaveDescripcion(6, "Otros")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }

        public static string ObtenerDescripcion(int intId)
        {
            LlaveDescripcion item = listado.FirstOrDefault(x => x.Id == intId);
            if (item != null) return item.Descripcion;
            return "";
        }
    }

    public static class TipoDeExoneracion
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Compras Autorizadas"),
            new LlaveDescripcion(2, "Ventas exentas a diplomáticos"),
            new LlaveDescripcion(3, "Autorizado por Ley Especial"),
            new LlaveDescripcion(4, "Exenciones Direccion General de Hacienda"),
            new LlaveDescripcion(5, "Transitorio V"),
            new LlaveDescripcion(6, "Transitorio IX"),
            new LlaveDescripcion(7, "Transitorio XVII"),
            new LlaveDescripcion(8, "Otros")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }

        public static string ObtenerDescripcion(int intId)
        {
            LlaveDescripcion item = listado.FirstOrDefault(x => x.Id == intId);
            if (item != null) return item.Descripcion;
            return "";
        }
    }

    public static class TipoDeImpuesto
    {
        private static readonly List<LlaveDescripcionValor> listado = new List<LlaveDescripcionValor>()
        {
            new LlaveDescripcionValor(1, "Tarifa 0% (Exento)", 0),
            new LlaveDescripcionValor(2, "Tarifa Reducida 1%", 1),
            new LlaveDescripcionValor(3, "Tarifa Reducida 2%", 2),
            new LlaveDescripcionValor(4, "Tarifa Reducida 4%", 4),
            new LlaveDescripcionValor(5, "Transitorio 0%", 0),
            new LlaveDescripcionValor(6, "Transitorio 4%", 4),
            new LlaveDescripcionValor(7, "Transitorio 8%", 8),
            new LlaveDescripcionValor(8, "Tarifa General 13%", 13)
        };

        public static IList<LlaveDescripcionValor> ObtenerListado()
        {
            return listado;
        }

        public static LlaveDescripcionValor ObtenerParametro(int intId)
        {
            return listado.FirstOrDefault(x => x.Id == intId);
        }
    }

    public static class TipoDeIdentificacion
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(0, "Cédula Física"),
            new LlaveDescripcion(1, "Cédula Jurídica"),
            new LlaveDescripcion(2, "DIMEX"),
            new LlaveDescripcion(3, "NITE")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }

        public static string ObtenerDescripcion(int intId)
        {
            LlaveDescripcion item = listado.FirstOrDefault(x => x.Id == intId);
            if (item != null) return item.Descripcion;
            return "";
        }
    }

    public static class TipoDePrecioProducto
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Precio 1"),
            new LlaveDescripcion(2, "Precio 2"),
            new LlaveDescripcion(3, "Precio 3"),
            new LlaveDescripcion(4, "Precio 4"),
            new LlaveDescripcion(5, "Precio 5")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }

        public static string ObtenerDescripcion(int intId)
        {
            LlaveDescripcion item = listado.FirstOrDefault(x => x.Id == intId);
            if (item != null) return item.Descripcion;
            return "";
        }
    }

    public static class TipoDeMoneda
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Colones"),
            new LlaveDescripcion(2, "Dólares")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }
    }

    public static class TipoDeProducto
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Producto"),
            new LlaveDescripcion(2, "Servicios Profesionales"),
            new LlaveDescripcion(3, "Otros Servicios"),
            new LlaveDescripcion(4, "Transitorio"),
            new LlaveDescripcion(5, "Impuesto al servicio 10%")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }
    }

    public static class TipoDeUnidad
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Und"),
            new LlaveDescripcion(2, "SP")
        };

        public static IList<LlaveDescripcion> ObtenerListado()
        {
            return listado;
        }
    }
}