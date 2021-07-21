using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Personalizer.Models;

namespace PersonalizerMobile.Servicios
{
    public class ServicioMenu
    {
        public static IList<RankableAction> ObtenerAcciones()
        {
            var acciones = new List<RankableAction>
            {
                new RankableAction
                {
                    Id = "pasta",
                    Features = new List<object>() { new { sabor = "salado", especias = "medio" }, new { nivelNutricional = 5, cocina = "italiana" } }
                },

                new RankableAction
                {
                    Id = "helado",
                    Features = new List<object>() { new { sabor = "dulce", especias = "ninguno" }, new { nivelNutricional = 2 } }
                },

                new RankableAction
                {
                    Id = "jugo",
                    Features = new List<object>() { new { sabor = "dulce", especias = "ninguno" }, new { nivelNutricional = 5 }, new { bebida = true } }
                },

                new RankableAction
                {
                    Id = "ensalada",
                    Features = new List<object>() { new { sabor = "salado", especias = "bajo" }, new { nivelNutricional = 8 } }
                }
            };

            return acciones;
        }

        public static List<string> ObtenerHoras() => new List<string>() { "mañana", "tarde", "noche" };
        public static List<string> ObtenerSabores() => new List<string> { "salado", "dulce" };
    }
}
