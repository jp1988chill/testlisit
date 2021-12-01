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
        public string apiUrlMantenedorMVCEntity = "https://localhost:44344/action/";

        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////
        public async Task<string> GetUserLogin()
        {
            var assembly = Assembly.GetEntryAssembly();
            var resourceStream = assembly.GetManifestResourceStream("ClientWebAPI.Web.Data.LoginUserExample.json");
            var data = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        [HttpPost, Produces("application/json")]
        public async Task<List<User>> ListarTodosUSUARIOSSet()
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var UserToken = await GetUserLogin();

            //Forge the RestAPI request
            Enveloped envelopeSignedUserTokenOut = JsonConvert.DeserializeObject<Enveloped>(UserToken);
            envelopeSignedUserTokenOut.body = new
            {
                
            };

            string envelopeSignedUserTokenOutStr = JsonConvert.SerializeObject(envelopeSignedUserTokenOut);
            var json = client.UploadString(apiUrlMantenedorMVCEntity + "/ListarTodos", "Post", envelopeSignedUserTokenOutStr);
            Enveloped envelopeSignedUserToken = JsonConvert.DeserializeObject<Enveloped>(json);
            List<User> det = JsonConvert.DeserializeObject<List<User>>(envelopeSignedUserToken.body.ToString());
            return det;
        }

        [HttpPost, Produces("application/json")]
        public async Task<User> ObtenerUSUARIOSSet(string inIdUsuarios)
        {
            //Handle the RestAPI:
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var UserToken = await GetUserLogin();

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
            var UserToken = await GetUserLogin();

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
            var UserToken = await GetUserLogin();

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
            var UserToken = await GetUserLogin();

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
            List<User> list = await ListarTodosUSUARIOSSet();
            return View(list);
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string nombreUsuario){
            List<User> ret = new List<User>();
            if ((nombreUsuario != null) && !nombreUsuario.Contains("*"))
            {
                try
                {
                    List<User> lst = (await ListarTodosUSUARIOSSet());
                    ret.AddRange(lst.Where(item => item.Name.ToUpper().Contains(nombreUsuario.ToUpper())));
                }
                catch (Exception ex)
                {

                }
            }
            else {
                List<User> lst = new List<User>(); //(await ListarTodosUSUARIOSSet());
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


