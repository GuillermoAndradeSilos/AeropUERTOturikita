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
    public partial class AgregarView : ContentPage
    {
        public AgregarView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}