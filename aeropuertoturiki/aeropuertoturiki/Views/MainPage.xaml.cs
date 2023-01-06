using aeropuertoturiki.Models;
using aeropuertoturiki.ViewModels;
using aeropuertoturiki.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace aeropuertoturiki
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        PrincipalViewModel prueba;

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AgregarView());
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            prueba = new PrincipalViewModel();
            prueba.Vuelo = lstviewPrueba.SelectedItem as Vuelo;
            if (prueba.Vuelo != null)
                Navigation.PushModalAsync(new EditarView(prueba.Vuelo));
            else
                Error.Text = "Selecciona el vuelo a editar";
        }
    }
}
