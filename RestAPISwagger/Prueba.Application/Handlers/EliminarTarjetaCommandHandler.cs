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
    public class EliminarTarjetaCommandHandler : IRequestHandler<EliminarTarjetaCommand, CardResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public EliminarTarjetaCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardResponse> Handle(EliminarTarjetaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new EliminarTarjetaModel();
            var middleWareHandlerResponse = (await middleWareHandler.EliminarTarjeta(request.objBodyObjectRequest, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
