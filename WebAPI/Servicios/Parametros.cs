using LeandroSoftware.Common.DatosComunes;

namespace LeandroSoftware.ServicioWeb.Parametros
{
    public static class CondicionDeVenta
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Contado"),
            new LlaveDescripcion(2, "Crédito")
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
            new LlaveDescripcion(6, "Sinpe Móvil"),
            new LlaveDescripcion(7, "Plataforma digital"),
            new LlaveDescripcion(99, "Otros")
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
            new LlaveDescripcion(4, "Exenciones Dirección General de Hacienda Autorización Local Genérica"),
            new LlaveDescripcion(5, "Exenciones Dirección General de Hacienda Transitorio V"),
            new LlaveDescripcion(6, "Servicios turísticos inscritos ante el Instituto Costarricense de Turismo (ICT)"),
            new LlaveDescripcion(7, "Transitorio XVII"),
            new LlaveDescripcion(8, "Exoneración a Zona Franca"),
            new LlaveDescripcion(9, "Exoneración de servicios complementarios para la exportación articulo 11 RLIVA"),
            new LlaveDescripcion(10, "Organo de las corporaciones municipales"),
            new LlaveDescripcion(11, "Exenciones Dirección General de Hacienda Autorización de Impuesto Local Concreta"),
            new LlaveDescripcion(12, "Otros")
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

    public static class TipoDeNombreInstExoneracion
    {
        private static readonly List<LlaveDescripcion> listado = new List<LlaveDescripcion>()
        {
            new LlaveDescripcion(1, "Ministerio de Hacienda"),
            new LlaveDescripcion(2, "Ministerio de Relaciones Exteriores y Culto"),
            new LlaveDescripcion(3, "Ministerio de Agricultura y Ganadería"),
            new LlaveDescripcion(4, "Ministerio de Economía, Industria y Comercio"),
            new LlaveDescripcion(5, "Cruz Roja Costarricense"),
            new LlaveDescripcion(6, "Benemérito Cuerpo de Bomberos de Costa Rica"),
            new LlaveDescripcion(7, "Asociación Obras del Espíritu Santo"),
            new LlaveDescripcion(8, "Federación Cruzada Nacional de protección al Anciano (Fecrunapa)"),
            new LlaveDescripcion(9, "Escuela de Agricultura de la Región Húmeda (EARTH)"),
            new LlaveDescripcion(10, "Instituto Centroamericano de Administración de Empresas (INCAE)"),
            new LlaveDescripcion(11, "Junta de Protección Social (JPS)"),
            new LlaveDescripcion(12, "Autoridad Reguladora de los Servicios Públicos (Aresep)"),
            new LlaveDescripcion(13, "Otros")
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
            new LlaveDescripcionValor(1, "Tarifa 0% (Artículo 32, num 1, RLIVA)", 0),
            new LlaveDescripcionValor(2, "Tarifa reducida 1%", 1),
            new LlaveDescripcionValor(3, "Tarifa Reducida 2%", 2),
            new LlaveDescripcionValor(4, "Tarifa Reducida 4%", 4),
            new LlaveDescripcionValor(5, "Transitorio 0%", 0),
            new LlaveDescripcionValor(6, "Transitorio 4%", 4),
            new LlaveDescripcionValor(7, "Transitorio 8%", 8),
            new LlaveDescripcionValor(8, "Tarifa General 13%", 13),
            new LlaveDescripcionValor(9, "Tarifa reducida 0.5%", (decimal)0.5),
            new LlaveDescripcionValor(10, "Tarifa Exenta", 0),
            new LlaveDescripcionValor(11, "Tarifa 0% sin derecho a crédito", 0)
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
            new LlaveDescripcion(3, "NITE"),
            new LlaveDescripcion(4, "Extranjero No Domiciliado"),
            new LlaveDescripcion(5, "No Contribuyente")
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