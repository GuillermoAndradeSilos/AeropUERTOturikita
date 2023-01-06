using AeropUERTOturikita.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AeropUERTOturikita.Services
{
    public class VueloService
    {
        HttpClient cliente = new HttpClient
        {
            BaseAddress = new Uri("https://vuelostt.sistemas19.com/")
        };
        public async Task<List<Vuelo>> Get()
        {
            List<Vuelo>? vuelos = null;
            var response = await cliente.GetAsync("api/vuelo/get/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                vuelos = JsonConvert.DeserializeObject<List<Vuelo>>(json);
            }
            if (vuelos == null)
                return new List<Vuelo>();
            else
                return vuelos;
        }
        public async Task<List<Vuelo>> GetArrivadosYCancelados()
        {
            List<Vuelo>? vuelos = null;
            var response = await cliente.GetAsync("api/vuelo/get/eliminar/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                vuelos = JsonConvert.DeserializeObject<List<Vuelo>>(json);
            }
            if (vuelos == null)
                return new List<Vuelo>();
            else
                return vuelos;
        }
        public async Task<bool> Delete(Vuelo vuelo)
        {
            var response = await cliente.DeleteAsync("api/vuelo/" + vuelo.IdVuelo);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return false;
            return true;
        }
        public async Task<bool> Update(Vuelo vuelo)
        {
            var json = JsonConvert.SerializeObject(vuelo);
            var response = await cliente.PostAsync("api/vuelo/put", new StringContent(json, Encoding.UTF8,
                "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return false;
            return true;
        }
    }
}
