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
    public class AgregarSaldoTarjetaCommandHandler : IRequestHandler<AgregarSaldoTarjetaCommand, CardResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public AgregarSaldoTarjetaCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardResponse> Handle(AgregarSaldoTarjetaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new AgregarSaldoTarjetaModel();
            var middleWareHandlerResponse = (await middleWareHandler.AgregarSaldoTarjeta(request.objBodyObjectRequest, request.saldoPorAgregr, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
