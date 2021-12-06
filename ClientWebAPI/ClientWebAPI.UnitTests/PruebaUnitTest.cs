using Client;
using ClientWebAPI.Web;
using ClientWebAPI.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prueba.UnitTests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


/// <summary>
/// ///////////////////////////////// Prueba Unitaria que realiza prueba en tiempo real de un Cliente consumiendo los servicios
/// </summary>
namespace ClientWebAPI.UnitTests
{
    

    [TestClass]
    public class ClientWebAPIUnitTest
    {
        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////
        private readonly Client.ITokenService _tokenService;
        private readonly UsuariosController _usuariosController;
        private readonly IConfiguration Configuration;
        public ClientWebAPIUnitTest()
        {
            var services = new ServiceCollection();

            //Add IOption dependency
            services.AddOptions();

            //Build a new Configuration required by Client Rest API Controller
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            //Rest API Security implements and rely on Token Handler Management
            services.AddSingleton<ITokenService, TokenService>();
            services.Configure<ClientWebAPIConfig>(Configuration.GetSection("ClientWebAPI"));
            _tokenService = services.BuildServiceProvider().GetService<Client.ITokenService>();       
            _usuariosController = new UsuariosController(_tokenService);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Casos de Prueba #1:

            //Eliminar/Crear Usuarios, generar Token y crear una Tarjeta
            var listUsers = _usuariosController.ListAllUsers().Result;
            var listCards = _usuariosController.ListAllCards().Result;
            if(listUsers.Count > 0) {
                var res = _usuariosController.DeleteUsers(listUsers).Result;
            }
            if (listCards.Count > 0)
            {
                var res = _usuariosController.DeleteCards(listCards).Result;
            }

            _tokenService.CleanToken(); //Clean and force new Token generation
                                        //because we deleted the old user associated to it

            listUsers = _usuariosController.ListAllUsers().Result;
            listCards = _usuariosController.ListAllCards().Result;
            Assert.AreEqual(listUsers.Count + listCards.Count, 1); //Only the current new user per Token is allowed

            //Create a list of Cards
            List<Card> newCards = new List<Card>();
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente2", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente3", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente4", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente5", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente6", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente7", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente8", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente9", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898" });
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente10", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898"});
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente11", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898"});
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente12", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898"});
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente13", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898"});
            newCards.Add(new Card() { Amount = 0, Estado = "no-vigente14", Id = new Guid(), Name = listUsers[0].Name, Pan = "1234", Pin = "9898"});

            var resCardsAdded = _usuariosController.GenerateCard(newCards).Result;
            Assert.AreEqual(resCardsAdded.Count, 14);
        }
    }
}
