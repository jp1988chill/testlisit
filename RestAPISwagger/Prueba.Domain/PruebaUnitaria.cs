using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Prueba.Domain.Interfaces.Helper;
using Prueba.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        
        public RolUser getRolUserFromServicioSocialId(string IdServicioSocial)
        {
            return null;
        }

        //Servicio que extrae Servicios Sociales disponibles, por rol.
        //Si es usuario, sólo genera sus ayudas sociales asignados por año y el último vigente.
        //Si es administrador, todos los registros disponibles.
        public List<ServicioSocial> getServicioSocialCollectionFromAuthenticatedUser(string currentLoggedUserToken) {
            List<ServicioSocial> result = new List<ServicioSocial>();

            List<ServicioSocial> servicioSocialRegistrados = ObtenerServiciosSociales(currentLoggedUserToken);
            List<User> users = ObtenerUsuariosPruebaUnitaria();
            List<RolUser> rolUsers = ObtenerRolUsuariosPruebaUnitaria();

            User user = users.Where(id => id.Token == new Guid(currentLoggedUserToken)).FirstOrDefault();
            RolUser rolUser = rolUsers.Where(id => id.Idroluser == user.Idroluser).FirstOrDefault();
            if ((rolUser != null) && (rolUser.Nombreroluser == "Administrador")){
                result.AddRange(servicioSocialRegistrados);
            }

            //Posiblemente el Usuario no tenga acceso a las API por Token. Asignamos un Token Administrador al azar, y extraemos la información pertinente únicamente al usuario conectado.
            else if (servicioSocialRegistrados.Count == 0) {
                RolUser adminRol = rolUsers.Where(id => id.Nombreroluser == "Administrador").FirstOrDefault();
                if (adminRol != null)
                {
                    User adminUser = users.Where(id => id.Idroluser == adminRol.Idroluser).FirstOrDefault();
                    servicioSocialRegistrados = ObtenerServiciosSociales(adminUser.Token.ToString());
                    result.AddRange(servicioSocialRegistrados.Where(id => id.Iduser.ToString() == user.Iduser.ToString()));
                }
            }
            return result;
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