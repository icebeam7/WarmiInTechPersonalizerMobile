using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using PersonalizerMobile.Models;
using PersonalizerMobile.Servicios;

using Xamarin.Forms;
using Microsoft.Azure.CognitiveServices.Personalizer.Models;

namespace PersonalizerMobile.ViewModels
{
    public class UsuarioViewModel : BaseViewModel
    {
        private Usuario usuario;

        public Usuario Usuario
        {
            get { return usuario; }
            set { SetProperty(ref usuario, value); }
        }

        private IList<RankableAction> acciones { get; set; }
        public ObservableCollection<string> Horas { get; set; }
        public ObservableCollection<string> Sabores { get; set; }

        private RankResponse recomendacion;

        public RankResponse Recomendacion
        {
            get { return recomendacion; }
            set { SetProperty(ref recomendacion, value); }
        }

        private bool retroalimentacion;

        public bool Retroalimentacion
        {
            get { return retroalimentacion; }
            set { SetProperty(ref retroalimentacion, value); }
        }

        public ICommand ComandoObtenerRecomendacion { private set; get; }
        public ICommand ComandoDarRetroalimentacion { private set; get; }

        private async Task ObtenerRecomendacion()
        {
            var contextoUsuario = new List<object>()
            {
                new { hora = Usuario.Hora},
                new { sabor = Usuario.Sabor }
            };

            var accionesIgnoradas = Usuario.Hora != "mañana" ? new List<string> { "jugo" } : new List<string>();

            recomendacion = ServicioPersonalizer.ObtenerRecomendacion(acciones, contextoUsuario, accionesIgnoradas);
            await App.Current.MainPage.DisplayAlert("Resultado", recomendacion.RewardActionId, "OK");
        }

        private async Task DarRetroalimentacion()
        {
            var reward = retroalimentacion ? 1.0f : 0.0f;
            ServicioPersonalizer.EnviarRetroalimentacion(recomendacion, reward);
            await App.Current.MainPage.DisplayAlert("Retroalimentación", "¡Gracias por tu retroalimentación!", "OK");
        }

        public UsuarioViewModel()
        {
            Usuario = new Usuario();
            Recomendacion = new RankResponse();

            acciones = ServicioMenu.ObtenerAcciones();
            Horas = new ObservableCollection<string>(ServicioMenu.ObtenerHoras());
            Sabores = new ObservableCollection<string>(ServicioMenu.ObtenerSabores());

            ComandoObtenerRecomendacion = new Command(async () => await ObtenerRecomendacion());
            ComandoDarRetroalimentacion = new Command(async () => await DarRetroalimentacion());
        }
    }
}
