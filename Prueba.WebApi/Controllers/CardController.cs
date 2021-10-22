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

namespace Prueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Ajuste de Capital (Ajuste Masivo).
        /// </summary>
        /// <param name="PolicyNumber">Número de poliza</param>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <param name="sequence">Número de llamada </param>
        /// <param name="Contractor">Rut contratante </param>
        /// <param name="transactionId">Número de transacción </param>
        /// <param name="dummyTest">Si está definido, el Microservicio realiza un test de operabilidad, y retorna OK. </param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearTarjeta")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //todo
        [HttpPut]
        [ProducesResponseType(typeof(CardResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearTarjeta([FromBody] CardBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CardTransactionCommand() { objBodyObjectRequest = objBodyObjectRequest}).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        [HttpGet("action/download")]
        public FileResult Download()
        {
            throw new NotImplementedException();
        }
    }
}
