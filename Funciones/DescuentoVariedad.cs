namespace Prueba_Tecnica.Funciones
{
    public class DescuentoVariedad
    {
        public static double AplicarDescuentoVariedad(double total, int variedad)
        {
            if (variedad >= 5)
            {
                return total * 0.95;
            }
            else
            {
                return total;
            }
        }
    }
}
