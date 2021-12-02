using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientWebAPI.Web.Models;
using System.Reflection;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using static ClientWebAPI.Api.Core.Enveloped.EnvelopedObject;
using Client;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientWebAPI.Web
{
    public class UsuariosController : Controller
    {
        private readonly ITokenService _tokenService;
        public string RestAPIPath = String.Empty;
        public UsuariosController(ITokenService tokenService)
        {
            _tokenService = tokenService;
            RestAPIPath = (_tokenService.GetRestAPIPath().Result);
        }

        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////

        
        [HttpPost, Produces("application/json")]
        public async Task<List<User>> ListAllUsers()
        {
            //GET Method: Handle the RestAPI:
            var APIKeyToken = await _tokenService.GetToken();
            var url = RestAPIPath + "/ObtenerUsuarios";
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
                    list.AddRange(response.usersNuevoTokenAsignado.Select(item => new User()
                    {
                        Password = item.Password,
                        Token = new Guid(item.Token),
                        Tokenleasetime = item.Tokenleasetime,
                        Name = item.Name
                    }));
                    return list;
                }
            }
            return null;
        }


        [HttpPost, Produces("application/json")]
        public async Task<List<Card>> ListAllCards()
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var APIKeyToken = await _tokenService.GetToken();
            client.Headers["Token"] = APIKeyToken;

            //Forge the RestAPI request + API Key
            string envelopeSignedUserTokenOut = "";
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(envelopeSignedUserTokenOut);
            var json = client.UploadString(RestAPIPath + "/ObtenerTarjetas", "Post", envelopeSignedUserTokenOutStr);
            CardTokenServiceResponse response = JsonConvert.DeserializeObject<CardTokenServiceResponse>(json);
            List<Card> list = new List<Card>();
            list.AddRange(response.cardInfoResponse.Select(item => new Card()
            {
                Amount = Convert.ToDecimal(item.Amount),
                Estado = item.Estado,
                Id = new Guid(item.Id),
                Pin = item.Pin,
                Name = item.Name,
                Pan = item.Pan
            }));
            return list;
        }

        [HttpPost, Produces("application/json")]
        public async Task<List<Card>> GetCard(string inId)
        {
            //GET Method: Handle the RestAPI:
            var APIKeyToken = await _tokenService.GetToken();
            var url = RestAPIPath + "/ObtenerTarjeta/" + inId;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Api Key";
            httpRequest.Headers["Token"] = APIKeyToken;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                CardTokenServiceResponse response = JsonConvert.DeserializeObject<CardTokenServiceResponse>(streamReader.ReadToEnd());
                List<Card> list = new List<Card>();
                if(response.cardInfoResponse.Count > 0) { 
                    list.AddRange(response.cardInfoResponse.Select(item => new Card()
                    {
                        Amount = Convert.ToDecimal(item.Amount),
                        Estado = item.Estado,
                        Id = new Guid(item.Id),
                        Pin = item.Pin,
                        Name = item.Name,
                        Pan = item.Pan
                    }));
                    return list;
                }
            }

            // httpResponse.StatusCode
            return null;
        }

        [HttpPost, Produces("application/json")]
        public async Task<Card> GenerateCard(Card card)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var APIKeyToken = await _tokenService.GetToken();
            client.Headers["Token"] = APIKeyToken;

            //Forge the RestAPI request + API Key
            List<Card> list = new List<Card>();
            list.Add(card);
            CardTokenServiceRequest request = new CardTokenServiceRequest(list);
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(request);
            var json = client.UploadString(RestAPIPath + "/CrearTarjeta", "Put", envelopeSignedUserTokenOutStr);
            CardTokenServiceResponse response = JsonConvert.DeserializeObject<CardTokenServiceResponse>(json);
            var cardResponse = response.cardInfoResponse.FirstOrDefault();
            return new Card() { Name = cardResponse.Name, Amount = Convert.ToDecimal(cardResponse.Amount), Estado = cardResponse.Estado, Id = new Guid(cardResponse.Id), Pan = cardResponse.Pan, Pin = cardResponse.Pin, Users = new List<User>() };
        }


        [HttpPost, Produces("application/json")]
        public async Task<User> GenerateUser(User user)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var APIKeyToken = await _tokenService.GetToken();
            client.Headers["Token"] = APIKeyToken;

            //Forge the RestAPI request + API Key
            List<User> list = new List<User>();
            list.Add(user);
            UserTokenServiceRequest request = new UserTokenServiceRequest(list);
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(request);
            var json = client.UploadString(RestAPIPath + "/CrearUsuario", "Put", envelopeSignedUserTokenOutStr);
            UserTokenServiceResponse response = JsonConvert.DeserializeObject<UserTokenServiceResponse>(json);
            var userResponse = response.usersNuevoTokenAsignado.FirstOrDefault();
            return new User() { Name = userResponse.Name, Password = userResponse.Password, Token = new Guid(userResponse.Token), Tokenleasetime = userResponse.Tokenleasetime };
        }

        [HttpPost, Produces("application/json")]
        public async Task<CardTokenServiceResponse> UpdateCard(string inIdUsuarios, Card card)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var APIKeyToken = await _tokenService.GetToken();
            client.Headers["Token"] = APIKeyToken;

            //Forge the RestAPI request + API Key
            List<Card> list = new List<Card>();
            list.Add(card);
            CardTokenServiceRequest request = new CardTokenServiceRequest(list);
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(request);
            var json = client.UploadString(RestAPIPath + "/ActualizarTarjeta", "Put", envelopeSignedUserTokenOutStr);
            CardTokenServiceResponse response = JsonConvert.DeserializeObject<CardTokenServiceResponse>(json);
            return response;
        }

        [HttpPost, Produces("application/json")]
        public async Task<CardTokenServiceResponse> DeleteCard(string inId)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var APIKeyToken = await _tokenService.GetToken();
            client.Headers["Token"] = APIKeyToken;

            //Forge the RestAPI request + API Key
            List<Card> list = GetCard(inId).Result;
            CardTokenServiceRequest request = new CardTokenServiceRequest(list);
            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(request);
            var json = client.UploadString(RestAPIPath + "/EliminarTarjeta", "Delete", envelopeSignedUserTokenOutStr);
            CardTokenServiceResponse response = JsonConvert.DeserializeObject<CardTokenServiceResponse>(json);
            return response;
        }

        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Fin /////////////////////////

        // GET: Usuarios
        public async Task<IActionResult> List()
        {
            List<Card> list = await ListAllCards();
            return View(list);
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string nombreUsuario){
            List<Card> ret = new List<Card>();
            if ((nombreUsuario != null) && !nombreUsuario.Contains("*"))
            {
                try
                {
                    List<Card> lst = (await ListAllCards());
                    ret.AddRange(lst.Where(item => item.Name.ToUpper().Contains(nombreUsuario.ToUpper())));
                }
                catch (Exception ex)
                {

                }
            }
            else {
                List<Card> lst = (await ListAllCards());
                ret.AddRange(lst);
            }
            return View(ret);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosset = await GetCard(id.ToString());
            if (usuariosset == null)
            {
                return NotFound();
            }

            return View(usuariosset.FirstOrDefault());
        }

        public IActionResult CreateUser()
        {
            var user = new User() { Token = Guid.NewGuid() };
            return View(user);
        }

        public IActionResult CreateCard()
        {
            List<User> users = ListAllUsers().Result;
            var card = new Card() { Id = Guid.NewGuid(), Users = users };
            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("Token,Name,Password,Tokenleasetime")] ClientWebAPI.Web.Models.User usuariosInst)
        {
            var usuariosset = await GenerateUser(usuariosInst);
            return View(usuariosset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCard([Bind("Id,Name,Pan,Pin,Estado,Amount,Users")] ClientWebAPI.Web.Models.Card cardInst)
        {
            var usuariosset = await GenerateCard(cardInst);
            return View(usuariosset);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosset = await GetCard(id.ToString());
            if (usuariosset == null)
            {
                return NotFound();
            }
            return View(usuariosset.FirstOrDefault());
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Pan,Pin,Estado,Amount")] Card card)
        {
            if (null == card.Id)
            {
                return NotFound();
            }

            var usuariosset = await UpdateCard(id.ToString(), card);
            if (usuariosset == null)
            {
                return NotFound();
            }
            if (usuariosset.httpCode != 200)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosset = await GetCard(id.ToString());
            if (usuariosset == null)
            {
                return NotFound();
            }

            return View(usuariosset.FirstOrDefault());
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await DeleteCard(id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}


