using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prueba.Domain.Interfaces.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Text;

namespace Prueba.Domain
{
    //Juan Pablo: Generalmente implemento las pruebas unitarias apropiadamente, pero por temas de tiempo, tuve que abstraerlas directamente en la capa Dominio!
    public class PruebaUnitaria
    {
        private IAppSettingsRepository _appSettingsRepository;
        private IMiscHelpers _miscHelpers;
        public PruebaUnitaria(IAppSettingsRepository appSettingsRepository, IMiscHelpers miscHelpers)
        {
            _appSettingsRepository = appSettingsRepository;
            _miscHelpers = miscHelpers;
        }

        ////////////////////////////////// Sólo Prueba Unitaria actual. Este código se remueve en producción. /////////////////////////////////////////////////////////////////////////
        public List<User> ObtenerUsuariosPruebaUnitaria()
        {
            var lst = new List<User>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                var verb = "ObtenerUsers";
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.usersNuevoTokenAsignado;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<User> EliminarUsuarioPruebaUnitaria(UserBody objBodyObjectRequest)
        {
            var lst = new List<User>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                    client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                    var result = _miscHelpers.DeleteAsJsonAsync(client, "EliminarUser", objBodyObjectRequest).Result;
                    result.EnsureSuccessStatusCode();
                    UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(result.Content.ReadAsStringAsync().Result);
                    lst = resp.usersNuevoTokenAsignado;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<User> CrearUsuarioPruebaUnitaria(UserBody objBodyObjectRequest)
        {
            var lst = new List<User>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "CrearUser";
                try
                {
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.usersNuevoTokenAsignado;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<User> ActualizarUsuarioPruebaUnitaria(UserBody objBodyObjectRequest)
        {
            var lst = new List<User>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "ActualizarUser";
                try
                {
                    HttpResponseMessage response = client.PostAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.usersNuevoTokenAsignado;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }


        ///////////////////////////////////////////////////////////////////////////////////
        public List<User> CrearLoginSessionUsersPruebaUnitaria(UserBody objBodyObjectRequest)
        {
            var lst = new List<User>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "CrearLoginUser";
                try
                {
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.usersNuevoTokenAsignado;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        ///////////////////////////////////////////////////////////////////////////////////
        public List<RolUser> ObtenerRolUsuariosPruebaUnitaria()
        {
            var lst = new List<RolUser>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                var verb = "ObtenerRolesUser";
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<RolUserResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.RolUsers;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<RolUser> EliminarRolUsuariosPruebaUnitaria(RolUserBody objBodyObjectRequest)
        {
            var lst = new List<RolUser>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                    client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                    var result = _miscHelpers.DeleteAsJsonAsync(client, "EliminarRolUser", objBodyObjectRequest).Result;
                    result.EnsureSuccessStatusCode();
                    RolUserResponse resp = JsonConvert.DeserializeObject<RolUserResponse>(result.Content.ReadAsStringAsync().Result);
                    lst = resp.RolUsers;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<RolUser> CrearRolUsuarioPruebaUnitaria(RolUserBody objBodyObjectRequest)
        {
            var lst = new List<RolUser>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "CrearRolUser";
                try
                {
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    RolUserResponse resp = JsonConvert.DeserializeObject<RolUserResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.RolUsers;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }
        /////////////////////////////////////////////////////////////////////////
        public List<Pais> ObtenerPaisesPruebaUnitaria()
        {
            var lst = new List<Pais>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                var verb = "ObtenerPaises";
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<PaisResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.Paises;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<Pais> EliminarPaisPruebaUnitaria(PaisBody objBodyObjectRequest)
        {
            var lst = new List<Pais>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                    client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                    var result = _miscHelpers.DeleteAsJsonAsync(client, "EliminarPais", objBodyObjectRequest).Result;
                    result.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<PaisResponse>(result.Content.ReadAsStringAsync().Result);
                    lst = resp.Paises;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<Pais> CrearPaisPruebaUnitaria(PaisBody objBodyObjectRequest)
        {
            var lst = new List<Pais>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "CrearPais";
                try
                {
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    PaisResponse resp = JsonConvert.DeserializeObject<PaisResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.Paises;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }
        /////////////////////////////////////////////////////////////////////////

        public List<Region_> ObtenerRegionesPruebaUnitaria()
        {
            var lst = new List<Region_>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                var verb = "ObtenerRegiones";
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<RegionResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.Regiones;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<Region_> EliminarRegionPruebaUnitaria(RegionBody objBodyObjectRequest)
        {
            var lst = new List<Region_>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                    client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                    var result = _miscHelpers.DeleteAsJsonAsync(client, "EliminarRegion", objBodyObjectRequest).Result;
                    result.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<RegionResponse>(result.Content.ReadAsStringAsync().Result);
                    lst = resp.Regiones;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<Region_> CrearRegionPruebaUnitaria(RegionBody objBodyObjectRequest)
        {
            var lst = new List<Region_>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "CrearRegion";
                try
                {
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    RegionResponse resp = JsonConvert.DeserializeObject<RegionResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.Regiones;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }
        /////////////////////////////////////////////////////////////////////////

        public List<Comuna> ObtenerComunasPruebaUnitaria()
        {
            var lst = new List<Comuna>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                var verb = "ObtenerComunas";
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<ComunaResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.Comunas;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<Comuna> EliminarComunaPruebaUnitaria(ComunaBody objBodyObjectRequest)
        {
            var lst = new List<Comuna>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                    client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                    var result = _miscHelpers.DeleteAsJsonAsync(client, "EliminarComuna", objBodyObjectRequest).Result;
                    result.EnsureSuccessStatusCode();
                    ComunaResponse resp = JsonConvert.DeserializeObject<ComunaResponse>(result.Content.ReadAsStringAsync().Result);
                    lst = resp.Comunas;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<Comuna> CrearComunaPruebaUnitaria(ComunaBody objBodyObjectRequest)
        {
            var lst = new List<Comuna>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "CrearComuna";
                try
                {
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    ComunaResponse resp = JsonConvert.DeserializeObject<ComunaResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.Comunas;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        ////////////////////////////////////////////////////////////////////////
        public List<ServicioSocial> ObtenerServiciosSociales(string Token)
        {
            var lst = new List<ServicioSocial>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", Token);
                var verb = "ObtenerServiciosSociales";
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    ServicioSocialResponse resp = JsonConvert.DeserializeObject<ServicioSocialResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.ServiciosSociales;
                }
                catch (Exception ex)
                {

                }
            }
            return lst;
        }

        public List<ServicioSocial> ObtenerServicioSocial(string IdServicioSocial)
        {
            var lst = new List<ServicioSocial>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                var verb = "ObtenerServicioSocial/" + IdServicioSocial;
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    ServicioSocialResponse resp = JsonConvert.DeserializeObject<ServicioSocialResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.ServiciosSociales;
                }
                catch /*(Exception ex)*/
                {

                }
            }
            return lst;
        }

        public List<ServicioSocial> EliminarServicioSocialPruebaUnitaria(ServicioSocialBody objBodyObjectRequest)
        {
            var lst = new List<ServicioSocial>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                    client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                    var result = _miscHelpers.DeleteAsJsonAsync(client, "EliminarServicioSocial", objBodyObjectRequest).Result;
                    result.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<ServicioSocialResponse>(result.Content.ReadAsStringAsync().Result);
                    lst = resp.ServiciosSociales;
                }
                catch 
                {

                }
            }
            return lst;
        }

        //Todo: ahora

        //Lógica: Servicios de ayudas sociales: Están asignados por comuna y solo a los residentes de dichas comunas
        //A una persona no se le puede asignar más de una vez con el mismo servicio social el mismo año.

        //crear método AsignacionServicioSocialExiste(IdComuna, IdUsuario, Año) == true/false. Se implementa en Create / Update de ServicioSocial.
        //Si es true, no se puede registrar, si es false, se registra.

        public List<ServicioSocial> CrearServicioSocialPruebaUnitaria(ServicioSocialBody objBodyObjectRequest)
        {
            var lst = new List<ServicioSocial>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "CrearServicioSocial";
                try
                {
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<ServicioSocialResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.ServiciosSociales;
                }
                catch 
                {

                }
            }
            return lst;
        }

        public List<ServicioSocial> ActualizarServicioSocialPruebaUnitaria(ServicioSocialBody objBodyObjectRequest)
        {
            var lst = new List<ServicioSocial>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettingsRepository.GetRestAPIPath());
                client.DefaultRequestHeaders.Add("Token", objBodyObjectRequest.Token);
                var verb = "ActualizarServicioSocial";
                try
                {
                    HttpResponseMessage response = client.PostAsync(verb, new StringContent(JsonConvert.SerializeObject(objBodyObjectRequest), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    var resp = JsonConvert.DeserializeObject<ServicioSocialResponse>(response.Content.ReadAsStringAsync().Result);
                    lst = resp.ServiciosSociales;
                }
                catch 
                {

                }
            }
            return lst;
        }
        
        ////////////////////////////////// Sólo Prueba Unitaria actual (Fin). Este código se remueve en producción. /////////////////////////////////////////////////////////////////////////
    }

    public class PruebaUnitariaResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}