using aeropuertoturiki.Models;
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
    public partial class VerDetallesView : ContentPage
    {
        public VerDetallesView()
        {
            InitializeComponent();
        }
        public VerDetallesView(Vuelo vuelo)
        {
            InitializeComponent();
            BindingContext = vuelo;
        }
    }
}