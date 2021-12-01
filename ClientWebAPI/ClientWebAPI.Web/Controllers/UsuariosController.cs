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

namespace ClientWebAPI.Web
{
    public class UsuariosController : Controller
    {
        private readonly ITokenService _tokenService;
        public string apiUrlMantenedorMVCEntity = "https://localhost:44344/action";

        public UsuariosController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////

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
            var json = client.UploadString(apiUrlMantenedorMVCEntity + "/ObtenerTarjetas", "Post", envelopeSignedUserTokenOutStr);
            CardTokenServiceResponse response = JsonConvert.DeserializeObject<CardTokenServiceResponse>(json);
            List<Card> list = new List<Card>();
            list.AddRange(response.cardInfoResponse.Select(item => new Card(item.Name, item.Pan)
            {
                Amount = Convert.ToDecimal(item.Amount),
                Estado = item.Estado,
                Id = new Guid(item.Id),
                Pin = item.Pin
            }));
            return list;
        }

        [HttpPost, Produces("application/json")]
        public async Task<User> ObtenerUSUARIOSSet(string inIdUsuarios)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var UserToken = await _tokenService.GetToken();

            //Forge the RestAPI request
            Enveloped envelopeSignedUserTokenOut = JsonConvert.DeserializeObject<Enveloped>(UserToken);
            envelopeSignedUserTokenOut.body = new
            {
                IdUsuarios = inIdUsuarios,
            };

            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(envelopeSignedUserTokenOut);
            var json = client.UploadString(apiUrlMantenedorMVCEntity + "/Obtener", "Post", envelopeSignedUserTokenOutStr);
            Enveloped envelopeSignedUserToken = JsonConvert.DeserializeObject<Enveloped>(json);
            User det = JsonConvert.DeserializeObject<User>(envelopeSignedUserToken.body.ToString());
            return det;
        }

        [HttpPost, Produces("application/json")]
        public async Task<User> CrearUSUARIOSSet(User user)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var UserToken = await _tokenService.GetToken();

            //Forge the RestAPI request
            Enveloped envelopeSignedUserTokenOut = JsonConvert.DeserializeObject<Enveloped>(UserToken);
            envelopeSignedUserTokenOut.body = new
            {
                token = user.Token,
                name = user.Name,
            };

            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(envelopeSignedUserTokenOut);
            var json = client.UploadString(apiUrlMantenedorMVCEntity + "/Crear", "Post", envelopeSignedUserTokenOutStr);
            Enveloped envelopeSignedUserToken = JsonConvert.DeserializeObject<Enveloped>(json);
            User det = JsonConvert.DeserializeObject<User>(envelopeSignedUserToken.body.ToString());
            return det;
        }

        [HttpPost, Produces("application/json")]
        public async Task<User> ActualizarUSUARIOSSet(string inIdUsuarios, User user)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var UserToken = await _tokenService.GetToken();

            //Forge the RestAPI request
            Enveloped envelopeSignedUserTokenOut = JsonConvert.DeserializeObject<Enveloped>(UserToken);
            envelopeSignedUserTokenOut.body = new
            {
                IdUsuarios = inIdUsuarios,
                name = user.Name,
            };

            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(envelopeSignedUserTokenOut);
            var json = client.UploadString(apiUrlMantenedorMVCEntity + "/Actualizar", "Put", envelopeSignedUserTokenOutStr);
            Enveloped envelopeSignedUserToken = JsonConvert.DeserializeObject<Enveloped>(json);
            User det = JsonConvert.DeserializeObject<User>(envelopeSignedUserToken.body.ToString());
            return det;
        }

        [HttpPost, Produces("application/json")]
        public async Task<User> EliminarUSUARIOSSet(string inIdUsuarios)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var UserToken = await _tokenService.GetToken();

            //Forge the RestAPI request
            Enveloped envelopeSignedUserTokenOut = JsonConvert.DeserializeObject<Enveloped>(UserToken);
            envelopeSignedUserTokenOut.body = new
            {
                IdUsuarios = inIdUsuarios,
            };

            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(envelopeSignedUserTokenOut);
            var json = client.UploadString(apiUrlMantenedorMVCEntity + "/Eliminar", "Delete", envelopeSignedUserTokenOutStr);
            Enveloped envelopeSignedUserToken = JsonConvert.DeserializeObject<Enveloped>(json);
            User det = JsonConvert.DeserializeObject<User>(envelopeSignedUserToken.body.ToString());
            return det;
        }

        /////////////////////////Métodos ASP .NET Core 2.x: Implementación Rest API Microservicios Fin /////////////////////////

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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosset = await ObtenerUSUARIOSSet(id.ToString());
            if (usuariosset == null)
            {
                return NotFound();
            }

            return View(usuariosset);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuarios,NombreUsuario,Apellido")] ClientWebAPI.Web.Models.User usuariosInst)
        {
            var usuariosset = await CrearUSUARIOSSet(usuariosInst);
            return View(usuariosset);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosset = await ObtenerUSUARIOSSet(id.ToString());
            if (usuariosset == null)
            {
                return NotFound();
            }
            return View(usuariosset);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuarios,NombreUsuario,Apellido")] ClientWebAPI.Web.Models.User usuariosInst)
        {
            if (null == usuariosInst.Token)
            {
                return NotFound();
            }

            var usuariosset = await ActualizarUSUARIOSSet(id.ToString(), usuariosInst);
            if (usuariosset == null)
            {
                return NotFound();
            }
            if (await ObtenerUSUARIOSSet(usuariosset.Token.ToString()) == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosset = await ObtenerUSUARIOSSet(id.ToString());
            if (usuariosset == null)
            {
                return NotFound();
            }

            return View(usuariosset);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await EliminarUSUARIOSSet(id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}


