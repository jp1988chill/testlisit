using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prueba.Domain;
using Prueba.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


/// <summary>
/// ///////////////////////////////// Prueba Unitaria que realiza prueba en tiempo real de un Cliente consumiendo los servicios
/// </summary>
namespace Prueba.UnitTests
{
    

    [TestClass]
    public class PruebaUnitTest
    {
        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////
        private readonly ITokenService _tokenService;

        public PruebaUnitTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITokenService, TokenService>();
            var serviceProvider = services.BuildServiceProvider();
            _tokenService = serviceProvider.GetService<ITokenService>();
        }


        [HttpPost, Produces("application/json")]
        public async Task<List<User>> ListAllUsers()
        {
            //GET Method: Handle the RestAPI:
            var APIKeyToken = await _tokenService.GetToken();   //Todo: implement this method, then the rest
            var url = _tokenService.GetRestAPIPath() + "/ObtenerUsuarios";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Api Key";
            httpRequest.Headers["Token"] = APIKeyToken;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                UserTokenServiceResponse response = JsonConvert.DeserializeObject<UserTokenServiceResponse>(streamReader.ReadToEnd());
                List<User> list = new List<User>();
                if (response.usersNuevoTokenAsignado.Count > 0)
                {
                    list.AddRange(response.usersNuevoTokenAsignado.Select(item => new User(name: item.Name, password: item.Password, token: item.Token, tokenleasetime: item.Tokenleasetime)));
                    return list;
                }
            }
            return null;
        }

        [TestMethod]
        public void TestRestAPIOperability()
        {
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Implementar llamadas Rest aquí (mismas que Cliente WebAPI) además de lógica de negocio
            var list = ListAllUsers().Result;
        }
    }
}
