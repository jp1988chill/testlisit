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
    public class ObtenerTarjetasCommandHandler : IRequestHandler<ObtenerTarjetasCommand, CardInfoResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public ObtenerTarjetasCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardInfoResponse> Handle(ObtenerTarjetasCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerTarjetasModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerTarjeta(cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
