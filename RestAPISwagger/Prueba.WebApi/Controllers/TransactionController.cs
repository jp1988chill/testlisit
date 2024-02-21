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

namespace Prueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crear una sesión para un User registrado por 10 minutos.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearLoginUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado se genera desde este método, no se puede crear dependencia circular
        [HttpPut]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearLoginUser([FromBody] UserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearLoginUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpPut]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearUser([FromBody] UserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerUsers()
        {
            var handlerResponse = await _mediator.Send(new ObtenerUsersCommand() { }).ConfigureAwait(false);
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpDelete]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarUser([FromBody] UserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpPut]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearComuna([FromBody] ComunaBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearComunaCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de este usuario, no se valida acá.
        [HttpPut]
        [ProducesResponseType(typeof(RolUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearRolUser([FromBody] RolUserBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearRolUserCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
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
        [ProducesResponseType(typeof(ServicioSocialResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerRolUser([FromRoute] string idroluser)
        {
            var handlerResponse = await _mediator.Send(new ObtenerRolUserCommand() { IdRolUser = idroluser }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }
        
    }
}
