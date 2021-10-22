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
    public class DescontarSaldoTarjetaCommandHandler : IRequestHandler<DescontarSaldoTarjetaCommand, CardResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public DescontarSaldoTarjetaCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<CardResponse> Handle(DescontarSaldoTarjetaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new DescontarSaldoTarjetaModel();
            var middleWareHandlerResponse = (await middleWareHandler.DescontarSaldoTarjeta(request.objBodyObjectRequest, request.saldoPorDescontar, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
