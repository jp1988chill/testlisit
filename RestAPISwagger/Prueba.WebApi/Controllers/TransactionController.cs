using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Prueba.Repository;
using System.Collections.Generic;
using Prueba.Domain;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Prueba.WebApi.Responses;
using Prueba.Application.Commands;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;
using Swashbuckle.Swagger;
using System.Net.Http.Json;
using Prueba.Domain.Interfaces.Helper;
using Microsoft.Extensions.Logging;

namespace Prueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IAppSettingsRepository _appSettingsRepository;
        private IMiscHelpers _miscHelpers;
        private readonly ILogger<TransactionController> _logger;
        private PruebaUnitaria _PruebaUnitaria;
        public TransactionController(IMediator mediator, IAppSettingsRepository appSettingsRepository, IMiscHelpers miscHelpers, ILogger<TransactionController> logger)
        {
            _mediator = mediator;
            _appSettingsRepository = appSettingsRepository;
            _miscHelpers = miscHelpers;
            _logger = logger;
            _PruebaUnitaria = new PruebaUnitaria(appSettingsRepository, miscHelpers);
        }


        


        /// <summary>
        /// Implementación de Prueba Unitaria, que automatiza en una sola llamada a un servicio expuesto, los requerimientos en Prueba técnica - backend.pdf
        /// 
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EjecutarPruebaUnitaria")]
        [HttpGet]
        [ProducesResponseType(typeof(PruebaUnitariaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EjecutarPruebaUnitaria()
        {
            //Generamos 2 usuarios + Login de 10 minutos de sesión para cada uno
            List<User> usu = new List<User>();
            usu.Add(new User(new Guid(), 0, "JP", "pass", "")); //Será Usuario normal
            usu.Add(new User(new Guid(), 0, "Catalina", "pass", "")); //Será Administrador 
            List<User> usuariosRegistrados = _PruebaUnitaria.CrearUsuarioPruebaUnitaria(new UserBody() { Users = usu });
            usuariosRegistrados = _PruebaUnitaria.CrearLoginSessionUsersPruebaUnitaria(new UserBody() { Users = usuariosRegistrados });

            //Generamos un rol "Usuario", y el otro "Administrador"
            List<RolUser> rolUsu = new List<RolUser>();
            rolUsu.Add(new RolUser("Usuario"));
            rolUsu.Add(new RolUser("Administrador"));
            List<RolUser> rolUsuariosRegistrados = _PruebaUnitaria.CrearRolUsuarioPruebaUnitaria(new RolUserBody() { RolUsers = rolUsu });

            //"Catalina" es "Administrador" && "JP" is "Usuario" normal
            usuariosRegistrados.Where(id => id.Name == "Catalina").ToList().FirstOrDefault().Idroluser = rolUsuariosRegistrados.Where(id => id.Nombreroluser == "Administrador").ToList().FirstOrDefault().Idroluser;
            usuariosRegistrados.Where(id => id.Name == "JP").ToList().FirstOrDefault().Idroluser = rolUsuariosRegistrados.Where(id => id.Nombreroluser == "Usuario").ToList().FirstOrDefault().Idroluser;

            //Seleccionamos 1 de los 2 usuarios conectados:
            //string TokenActualUsuarioConectado = (usuariosRegistrados.Where(id => id.Name == "JP").ToList().FirstOrDefault().Token).ToString(); //Perfil Usuario: No permite acceso a servicios tipo POST, PUTS, DELETE
            string TokenActualUsuarioConectado = (usuariosRegistrados.Where(id => id.Name == "Catalina").ToList().FirstOrDefault().Token).ToString(); //Perfil Administrador: Acceso permitido a servicios tipo POST, PUTS, DELETE

            _PruebaUnitaria.ActualizarUsuarioPruebaUnitaria(new UserBody() { Users = usuariosRegistrados, Token = TokenActualUsuarioConectado });
           
            //Generamos 1 País con 2 Regiones, a su vez con 2 Comunas. 
            //Los ids son relacionales de manera dinámica. Por ende, se crean primero las Comunas, luego Regiones y finalmente Pais. Luego se arma la tupla completa en OOP.  
            //Nota: Estos servicios sólo pueden ser creados por un Administrador!
            List<int> comunasIdComunaAntof = new List<int>();
            List<Comuna> comAntof = new List<Comuna>();
            comAntof.Add(new Comuna() { IdComuna = 0, Nombre = "Calama" });     
            comAntof.Add(new Comuna() { IdComuna = 0, Nombre = "Tocopilla" });
            comAntof = _PruebaUnitaria.CrearComunaPruebaUnitaria(new ComunaBody() { Comunas = comAntof, Token = TokenActualUsuarioConectado }); //So far implemenedt Administrador services validation. Todo: Implement the same for the rest of services
            foreach(Comuna comuna in comAntof){ comunasIdComunaAntof.Add(comuna.IdComuna); }

            List<int> comunasIdComunaValpo = new List<int>();
            List<Comuna> comValpo = new List<Comuna>();
            comValpo.Add(new Comuna() { IdComuna = 0, Nombre = "Casablanca" });
            comValpo.Add(new Comuna() { IdComuna = 0, Nombre = "Concón" });
            comValpo = _PruebaUnitaria.CrearComunaPruebaUnitaria(new ComunaBody() { Comunas = comValpo, Token = TokenActualUsuarioConectado });
            foreach (Comuna comuna in comValpo) { comunasIdComunaValpo.Add(comuna.IdComuna); }

            List<int> regionesIdRegion = new List<int>();
            List<Region_> reg = new List<Region_>();
            reg.Add(new Region_() { Idregion = 0, Idcomuna = comunasIdComunaAntof, Nombre = "Antofagasta" } );
            reg.Add(new Region_() { Idregion = 0, Idcomuna = comunasIdComunaValpo, Nombre = "Valparaiso" });
            reg = _PruebaUnitaria.CrearRegionPruebaUnitaria(new RegionBody() { Regiones = reg });
            foreach (Region_ region in reg) { regionesIdRegion.Add(region.Idregion); }

            List<Pais> pais = new List<Pais>();
            pais.Add(new Pais() { Idpais = 0, Idregion = regionesIdRegion, Nombre = "Chile" });
            pais = _PruebaUnitaria.CrearPaisPruebaUnitaria(new PaisBody() { Paises = pais, Token = TokenActualUsuarioConectado });

            //Fin de pruebas, limpieza de tablas.
            _PruebaUnitaria.EliminarUsuarioPruebaUnitaria(new UserBody() { Users = _PruebaUnitaria.ObtenerUsuariosPruebaUnitaria(), Token = TokenActualUsuarioConectado });
            _PruebaUnitaria.EliminarRolUsuariosPruebaUnitaria(new RolUserBody() { RolUsers = _PruebaUnitaria.ObtenerRolUsuariosPruebaUnitaria(), Token = TokenActualUsuarioConectado });
            _PruebaUnitaria.EliminarComunaPruebaUnitaria(new ComunaBody() { Comunas = _PruebaUnitaria.ObtenerComunasPruebaUnitaria(), Token = TokenActualUsuarioConectado });
            _PruebaUnitaria.EliminarRegionPruebaUnitaria(new RegionBody() { Regiones = _PruebaUnitaria.ObtenerRegionesPruebaUnitaria(), Token = TokenActualUsuarioConectado });
            _PruebaUnitaria.EliminarPaisPruebaUnitaria(new PaisBody() { Paises = _PruebaUnitaria.ObtenerPaisesPruebaUnitaria(), Token = TokenActualUsuarioConectado });

            //Se implementa lo siguiente:
            //Servicios de ayudas sociales: Están asignados por comuna y solo a los residentes de dichas comunas
            //A una persona no se le puede asignar más de una vez con el mismo servicio social el mismo año.
            //El administrador puede ver personas y los servicios asignados, le puede asignar alguna ayuda social.
            //Una persona puede obtener sus ayudas sociales asignados por año y el último vigente.
            //El administrador puede obtener las ayudas sociales asignadas a un usuario.
            //El administrador puede crear nuevas ayudas sociales para las comunas o regiones. Si se crea en una región
            //se asigna a todas las comunas de esta.
            return Ok(new PruebaUnitariaResponse() { HttpCode = 200, HttpMessage = "Prueba Unitaria ejecutada correctamente", MoreInformation = "----", userFriendlyError = "----" });
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Permite asignar roles para un User registrado
        /// 
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearLoginUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //La generación de Tokens por sesión requieren de que un usuario exista primero.
        [HttpPut]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearLoginUser([FromBody] UserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearLoginUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: CrearLoginUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        ///////////////////////////////////////////////// CRUD Implementation User /////////////////////////////////////////////////

        /// <summary>
        /// Crear un usuario.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //No se puede generar un Token sin un usuario primero, asique no tiene seguridad por el momento. 
        [HttpPut]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearUser([FromBody] UserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: CrearUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todos los User(s) registrados.
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerUsers")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //Verbo GET no tiene capa de seguridad
        [HttpGet]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerUsers()
        {
            var handlerResponse = await _mediator.Send(new ObtenerUsersCommand() { }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerUsers():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene User(s) registrados por iduser.
        /// </summary>
        /// <param name="iduser">IdUser a consultar. Si el registro existe, retornará la Entidad User con los valores pertinentes en el response. </param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerUser/{iduser}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerUser([FromRoute] string iduser)
        {
            var handlerResponse = await _mediator.Send(new ObtenerUserCommand() { IdUser = iduser }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza cada User(s) registrado(s) por _IdUser(s) por nuevos valores incluídos en Body JSON.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //todo: re-enable when Administrator role is available so only Administrator can use this, and User is rejected
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarUser([FromBody] UserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new ActualizarUserCommand() { Users = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: ActualizarUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Elimina usuario(s).
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpDelete]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarUser([FromBody] UserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: EliminarUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        ///////////////////////////////////////////////// CRUD Implementation Pais /////////////////////////////////////////////////

        /// <summary>
        /// Crear un Pais.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearPais")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpPut]
        [ProducesResponseType(typeof(PaisResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearPais([FromBody] PaisBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearPaisCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: CrearPais():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todos los Pais(es) registrados.
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerPaises")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(PaisResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerPaises()
        {
            var handlerResponse = await _mediator.Send(new ObtenerPaisesCommand() { }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerPaises():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene Pais(es) registrados por Idpais.
        /// </summary>
        /// <param name="idpais">idpais a consultar. Si el registro existe, retornará la Entidad Pais con los valores pertinentes en el response. </param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerPais/{idpais}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(PaisResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerPais([FromRoute] string idpais)
        {
            var handlerResponse = await _mediator.Send(new ObtenerPaisCommand() { IdPais = idpais }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerPais():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza cada Pais(s) registrado(s) por IdPais(s) por nuevos valores incluídos en Body JSON.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarPais")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //todo: re-enable when Administrator role is available so only Administrator can use this, and User is rejected
        [HttpPost]
        [ProducesResponseType(typeof(PaisResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarPais([FromBody] PaisBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new ActualizarPaisCommand() { Paises = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: ActualizarPais():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Elimina pais(es).
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarPais")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpDelete]
        [ProducesResponseType(typeof(PaisResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarPais([FromBody] PaisBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarPaisCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: EliminarPais():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        ///////////////////////////////////////////////// CRUD Implementation Region /////////////////////////////////////////////////

        /// <summary>
        /// Crear una Region.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearRegion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpPut]
        [ProducesResponseType(typeof(RegionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearRegion([FromBody] RegionBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearRegionCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: CrearRegion():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todas las Regiones registradas.
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerRegiones")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(RegionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerRegiones()
        {
            var handlerResponse = await _mediator.Send(new ObtenerRegionesCommand() { }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerRegiones():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene Region(es) registrados por Idregion.
        /// </summary>
        /// <param name="Idregion">Idregion a consultar. Si el registro existe, retornará la Entidad Region con los valores pertinentes en el response. </param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerRegion/{Idregion}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(RegionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerRegion([FromRoute] string idregion)
        {
            var handlerResponse = await _mediator.Send(new ObtenerRegionCommand() { Idregion = idregion }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerRegion():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza cada Region(es) registrado(s) por Idregion(s) por nuevos valores incluídos en Body JSON.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarRegion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //todo: re-enable when Administrator role is available so only Administrator can use this, and User is rejected
        [HttpPost]
        [ProducesResponseType(typeof(RegionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarRegion([FromBody] RegionBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new ActualizarRegionCommand() { Regiones = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: ActualizarRegion():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Elimina region(es).
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarRegion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpDelete]
        [ProducesResponseType(typeof(RegionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarRegion([FromBody] RegionBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarRegionCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: EliminarRegion():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        ///////////////////////////////////////////////// CRUD Implementation Comuna /////////////////////////////////////////////////

        /// <summary>
        /// Crear una Comuna.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearComuna")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpPut]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearComuna([FromBody] ComunaBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearComunaCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: CrearComuna():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todas las Comuna(s) registradas.
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerComunas")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerComunas()
        {
            var handlerResponse = await _mediator.Send(new ObtenerComunasCommand() { }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerComunas():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }


        /// <summary>
        /// Obtiene User(s) registrados por iduser.
        /// </summary>
        /// <param name="idcomuna">Idcomuna a consultar. Si el registro existe, retornará la Entidad Comuna con los valores pertinentes en el response. </param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerComuna/{idcomuna}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerComuna([FromRoute] string idcomuna)
        {
            var handlerResponse = await _mediator.Send(new ObtenerComunaCommand() { Idcomuna = idcomuna }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerComuna():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza cada Comuna(s) registrada(s) por Idcomuna(s) por nuevos valores incluídos en Body JSON.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarComuna")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //todo: re-enable when Administrator role is available so only Administrator can use this, and User is rejected
        [HttpPost]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarComuna([FromBody] ComunaBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new ActualizarComunaCommand() { Comunas = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: ActualizarComuna():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Elimina Comuna(s).
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarComuna")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpDelete]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarComuna([FromBody] ComunaBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarComunaCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: EliminarComuna():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        ///////////////////////////////////////////////// CRUD Implementation ServicioSocial /////////////////////////////////////////////////

        /// <summary>
        /// Crear un Servicio Social por comuna vinculado al usuario.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearServicioSocial")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpPut]
        [ProducesResponseType(typeof(ServicioSocialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearServicioSocial([FromBody] ServicioSocialBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearServicioSocialCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: CrearServicioSocial():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todos los Servicios Sociales registrados en el sistema.
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerServiciosSociales")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(ServicioSocialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerServiciosSociales()
        {
            var handlerResponse = await _mediator.Send(new ObtenerServiciosSocialesCommand() { }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerServiciosSociales():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene registro de Servicio Social por idserviciosocial.
        /// </summary>
        /// <param name="idserviciosocial">Idserviciosocial a consultar. Si el registro existe, retornará la Entidad Comuna con los valores pertinentes en el response. </param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerServicioSocial/{idserviciosocial}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(ServicioSocialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerServicioSocial([FromRoute] string idserviciosocial)
        {
            var handlerResponse = await _mediator.Send(new ObtenerServicioSocialCommand() { Idserviciosocial = idserviciosocial }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerServicioSocial():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza Servicio(s) Social(es) registrado(s) mediante Idserviciosocial(s) por nuevos valores incluídos en Body JSON.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarServicioSocial")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //todo: re-enable when Administrator role is available so only Administrator can use this, and User is rejected
        [HttpPost]
        [ProducesResponseType(typeof(ServicioSocialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarServicioSocial([FromBody] ServicioSocialBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new ActualizarServicioSocialCommand() { ServiciosSociales = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: ActualizarServicioSocial():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Elimina Servicio Social registrado mediante Idserviciosocial(s).
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarServicioSocial")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpDelete]
        [ProducesResponseType(typeof(ServicioSocialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarServicioSocial([FromBody] ServicioSocialBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarServicioSocialCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: EliminarServicioSocial():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }



        ///////////////////////////////////////////////// CRUD Implementation RolUser /////////////////////////////////////////////////

        /// <summary>
        /// Crear rol(es) para User(s).
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearRolUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token se genera acá, no se puede validar un valor que se generará a continuación (IdRol->Administrador).
        [HttpPut]
        [ProducesResponseType(typeof(RolUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearRolUser([FromBody] RolUserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearRolUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: CrearRolUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todos los rol(es) de User(s) registrados en el sistema.
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerRolesUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(RolUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerRolesUser()
        {
            var handlerResponse = await _mediator.Send(new ObtenerRolUsersCommand() { }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerRolesUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }


        /// <summary>
        /// Obtiene registro de RolUser por idroluser.
        /// </summary>
        /// <param name="idroluser">idroluser a consultar. Si el registro existe, retornará la Entidad RolUser con los valores pertinentes en el response. </param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerRolUser/{idroluser}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(RolUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerRolUser([FromRoute] string idroluser)
        {
            var handlerResponse = await _mediator.Send(new ObtenerRolUserCommand() { IdRolUser = idroluser }).ConfigureAwait(false);
            _logger.LogInformation("Log: ObtenerRolUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza Rol(s) de Usuario(s) registrado(s) mediante idroluser(s) por nuevos valores incluídos en Body JSON.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarRolUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //todo: re-enable when Administrator role is available so only Administrator can use this, and User is rejected
        [HttpPost]
        [ProducesResponseType(typeof(RolUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarRolUser([FromBody] RolUserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new ActualizarRolUserCommand() { RolUsers = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: ActualizarRolUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }


        /// <summary>
        /// Elimina Rol de Usuario registrado mediante idroluser(s) del sistema.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarRolUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpDelete]
        [ProducesResponseType(typeof(RolUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarRolUser([FromBody] RolUserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarRolUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            _logger.LogInformation("Log: EliminarRolUser():" + handlerResponse.HttpMessage);
            return Ok(handlerResponse);
        }
    }
}
