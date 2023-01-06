using aeropuertoturiki.Models;
using aeropuertoturiki.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace aeropuertoturiki.ViewModels
{
    public class PrincipalViewModel : INotifyPropertyChanged
    {
        public ICommand GuardarCommand { get; set; }
        public ICommand ActualizarCommand { get; set; }
        public Vuelo Vuelo { get; set; } = new Vuelo();
        public Estado EstadoSeleccionado { get; set; }
        public ObservableCollection<Estado> Estados { get; set; } = new ObservableCollection<Estado>();
        public ObservableCollection<Vuelo> Vuelos { get; set; } = new ObservableCollection<Vuelo>();
        private VueloService vueloservice = new VueloService();
        private EstadoService estadoservice = new EstadoService();
        public PrincipalViewModel()
        {
            GuardarCommand = new RelayCommand(Guardar);
            ActualizarCommand = new RelayCommand(Actualizar);
            CargarEstados();
            CargarVuelos();
        }
        private string error = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
            }
        }

        private void Actualizar()
        {
            CargarEstados();
            CargarVuelos();
        }

        private async void CargarVuelos()
        {
            Vuelos.Clear();
            var vuelos = await vueloservice.GetVuelos();
            foreach (var item in vuelos)
            {
                Vuelos.Add(item);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vuelos)));
        }
        private async void CargarEstados()
        {
            Estados.Clear();
            var estados = await estadoservice.GetEstados();
            foreach (var item in estados)
            {
                Estados.Add(item);
            }
        }
        private async void Guardar()
        {
            if (Validar())
            {
                Vuelo.Estado = EstadoSeleccionado.Estado1;
                if (Vuelo != null)
                    if (Vuelo.IdVuelo == 0)
                        await vueloservice.Insert(Vuelo);
                    else
                        await vueloservice.Update(Vuelo);
            }
            Actualizar();
        }

        private bool Validar()
        {
            Error = "";
            if (string.IsNullOrWhiteSpace(Vuelo.CodigoVuelo))
                Error = "Favor de Agregar un codigo de vuelo";
            if (string.IsNullOrWhiteSpace(Vuelo.Salida))
                Error = "Favor de agregar el origen de vuelo o la procedencia de este";
            if (EstadoSeleccionado.Estado1 == null)
                Error = "Favor de seleccionar el estado actual de este";
            if (Error != "")
                return false;
            else
            {
                Vuelo.Estado = EstadoSeleccionado.Estado1;
                return true;
            }
        }
    }
}
