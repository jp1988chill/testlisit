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
    public class ObtenerTarjetaPorNombreUsuarioCommandHandler : IRequestHandler<ObtenerTarjetaPorNombreUsuarioCommand, CardInfoResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public ObtenerTarjetaPorNombreUsuarioCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<CardInfoResponse> Handle(ObtenerTarjetaPorNombreUsuarioCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerTarjetaPorNombreUsuarioModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerTarjetaPorNombreUsuario(request.NombreUsuario, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
