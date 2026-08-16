
namespace Webapi.Utils
{
    public static class Utilitarios
    {
        public static int ParaIntOuPadrao(string valor, int valorPadrao)
        {
            if (int.TryParse(valor, out int resultado))
            {
                return resultado;
            }

            return valorPadrao;
        }
    }
}