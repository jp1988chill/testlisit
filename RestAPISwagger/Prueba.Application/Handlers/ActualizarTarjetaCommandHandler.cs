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
    public class ActualizarTarjetaCommandHandler : IRequestHandler<ActualizarTarjetaCommand, CardResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public ActualizarTarjetaCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardResponse> Handle(ActualizarTarjetaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ActualizarTarjetaModel();
            var middleWareHandlerResponse = (await middleWareHandler.ActualizarTarjeta(request.objBodyObjectRequest, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
