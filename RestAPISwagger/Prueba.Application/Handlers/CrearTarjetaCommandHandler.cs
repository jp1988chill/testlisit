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
    public class CrearTarjetaCommandHandler : IRequestHandler<CrearTarjetaCommand, CardResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public CrearTarjetaCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardResponse> Handle(CrearTarjetaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearTarjetaModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearTarjeta(request.objBodyObjectRequest, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
