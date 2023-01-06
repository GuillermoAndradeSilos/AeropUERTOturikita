using aeropuertoturiki.Models;
using aeropuertoturiki.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace aeropuertoturiki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarView : ContentPage
    {

        public EditarView(Vuelo vuelo)
        {
            InitializeComponent();
            BindingContext = vuelo;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        PrincipalViewModel vm;
        string Error;
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            vm = new PrincipalViewModel();
            Error = "";
            if (string.IsNullOrWhiteSpace(CodigoVuelo.Text))
                Error = "Favor de Agregar un codigo de vuelo";
            if (string.IsNullOrWhiteSpace(Salida.Text))
                Error = "Favor de agregar el origen de vuelo o la procedencia de este";
            if ((Estado.SelectedItem as Estado) == null)
                Error = "Favor de seleccionar el estado actual de este";
            if(Error=="")
            {
                vm.Vuelo = new Vuelo
                {
                    CodigoVuelo = CodigoVuelo.Text,
                    HoraLlegada = HoraLlegada.Time,
                    Hora = Hora.Time,
                    Salida = Salida.Text,
                    IdVuelo = int.Parse(Id.Text),
                    Estado = Estado.SelectedItem.ToString()
                };
                vm.EstadoSeleccionado = Estado.SelectedItem as Estado;
                vm.GuardarCommand.Execute(vm);
            }
        }
    }
}