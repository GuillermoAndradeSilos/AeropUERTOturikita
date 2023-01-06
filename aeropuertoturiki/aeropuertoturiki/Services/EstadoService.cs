using aeropuertoturiki.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace aeropuertoturiki.Services
{
    public class EstadoService
    {
        HttpClient client;
        public EstadoService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://vuelostt.sistemas19.com/")
            };
        }
        public async Task<List<Estado>> GetEstados()
        {
            List<Estado> estado = null;
            var prueba = await client.GetAsync("api/estado");
            if (prueba.IsSuccessStatusCode)
            {
                var json = await prueba.Content.ReadAsStringAsync();
                estado = JsonConvert.DeserializeObject<List<Estado>>(json);
            }
            if (estado != null)
                return estado;
            else
                return new List<Estado>();
        }
    }
}
