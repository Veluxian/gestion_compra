namespace Prueba_Tecnica.Funciones
{
    public class DescuentoGrandesCompras
    {
        public static double AplicarDescuentoGranCompra(double total)
        {
            if (total > 500)
            {
                return total * 0.9;
            }
            else
            {
                return total;
            }
        }
    }
}
