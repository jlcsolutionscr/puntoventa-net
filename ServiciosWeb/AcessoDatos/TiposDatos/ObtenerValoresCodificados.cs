namespace LeandroSoftware.Core.TiposDatosHacienda
{
    public static class ObtenerValoresCodificados
    {
        public static string ObtenerCondicionDeVenta(int intIdCondicion)
        {
            switch (intIdCondicion)
            {
                case 1:
                    {
                        return "Contado";
                    }
                case 2:
                    {
                        return "Crédito";
                    }
                case 3:
                    {
                        return "Consignación";
                    }
                case 4:
                    {
                        return "Apartado";
                    }
                case 5:
                    {
                        return "Arrendamiento con opción de compra";
                    }
                case 6:
                    {
                        return "Arrendamiento en función financiera";
                    }
                default:
                    {
                        return "Otros";
                    }
            }
        }

        public static string ObtenerMedioDePago(int intIdMedioPago)
        {
            switch (intIdMedioPago)
            {
                case 1:
                    {
                        return "Efectivo";
                    }
                case 2:
                    {
                        return "Tarjeta";
                    }
                case 3:
                    {
                        return "Cheque";
                    }
                case 4:
                    {
                        return "Transferencia – depósito bancario";
                    }
                case 5:
                    {
                        return "Recaudado por terceros";
                    }
                default:
                    {
                        return "Otros";
                    }
            }
        }
    }
}
