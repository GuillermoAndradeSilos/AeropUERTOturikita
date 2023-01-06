using aeropuertoturiki.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace aeropuertoturiki.Services
{
    public class VueloService
    {
        HttpClient cliente = new HttpClient
        {
            BaseAddress = new Uri("https://vuelostt.sistemas19.com/")
        };
        public event Action<List<string>> Error;
        public async Task<List<Vuelo>> GetVuelos()
        {
            List<Vuelo> vuelos = null;
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
        public async Task<bool> Insert(Vuelo vuelo)
        {
            var json = JsonConvert.SerializeObject(vuelo);
            var response = await cliente.PostAsync("api/vuelo", new StringContent(json, Encoding.UTF8,
                "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            return true;
        }
        void LanzarErrorJson(string json)
        {
            List<string> obj = JsonConvert.DeserializeObject<List<string>>(json);
            if (obj != null)
                Error?.Invoke(obj);
        }
        public async Task<bool> Update(Vuelo vuelo)
        {
            var json = JsonConvert.SerializeObject(vuelo);
            var response = await cliente.PostAsync("api/vuelo/put", new StringContent(json, Encoding.UTF8,
                "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                LanzarError("No se encontro el vuelo en cuestión, favor de reintentar una vez más en caso de que el problema persista es posible que el vuelo fuese eliminado por alguien más.");
            return true;
        }
        void LanzarError(string mensaje)
        {
            Error?.Invoke(new List<string> { mensaje });
        }
        public async Task<bool> Delete(Vuelo vuelo)
        {
            var response = await cliente.DeleteAsync("api/vuelo/" + vuelo.IdVuelo);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                LanzarError("No se encontro el vuelo en cuestión, favor de reintentar una vez más en caso de que el problema persista es posible que el vuelo fuese eliminado por alguien más.");
            return true;
        }
    }
}
