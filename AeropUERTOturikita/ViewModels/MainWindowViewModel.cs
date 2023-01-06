using AeropUERTOturikita.Models;
using AeropUERTOturikita.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace AeropUERTOturikita.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Vuelo> VuelosRegistrados { get; set; } = new ObservableCollection<Vuelo>();
        Dispatcher dispatcher;
        //timer verificar
        Timer timer;
        //timer actualizar - NO VEO A NINGUN DIOS AQUI ARRIVA, SOLO ESTOY YO 
        Timer timer2;
        private VueloService service = new VueloService();
        //Contructor
        public MainWindowViewModel()
        {
            MostrarVuelos();
            dispatcher = Dispatcher.CurrentDispatcher;
            //inicializamos el timer
            //                 Metodo  - nada -empezara en 0 - cada cuanto hara el tick
            timer = new Timer(Verificar, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            //Este timer es nada mas para actualizar la tabla, se actualiza cada 5 segundos
            timer2 = new Timer(Actualizar, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        private void Actualizar(object? state)
        {
            //El dispatcher es necesario ya que el timer es un hilo distinto al original/principal
            dispatcher.Invoke(() => MostrarVuelos());
        }

        //Metodo que se repetira por cada tick que haga el timer, en este caso cada minuto
        private async void Verificar(object? state)
        {
            var vuelos = await service.GetArrivadosYCancelados();
            if (vuelos != null)
                foreach (var item in vuelos)
                {
                    await service.Delete(item);
                }
            dispatcher.Invoke(() => MostrarVuelos());
        }
        async void MostrarVuelos()
        {
            var vuelos = await service.Get();
            VuelosRegistrados.Clear();
            vuelos.ForEach(x => VuelosRegistrados.Add(x));
        }

    }
}