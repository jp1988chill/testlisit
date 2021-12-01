using MediatR;
using Prueba.Application.Commands;
using Prueba.Domain;
using Prueba.Domain.Models;
using Prueba.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prueba.Application.Handlers
{
    public class ObtenerTarjetaCommandHandler : IRequestHandler<ObtenerTarjetaCommand, CardInfoResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public ObtenerTarjetaCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardInfoResponse> Handle(ObtenerTarjetaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerTarjetaModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerTarjeta(request.GuidTarjeta, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
