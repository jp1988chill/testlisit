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
    public class EliminarComunaCommandHandler : IRequestHandler<EliminarComunaCommand, ComunaResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository = null;
        public EliminarComunaCommandHandler(PruebaContext pruebaContext)
        {
            comunaRepository = new RepositoryEntityFrameworkCQRS<Comuna>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<ComunaResponse> Handle(EliminarComunaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new EliminarComunaModel();
            var middleWareHandlerResponse = (await middleWareHandler.EliminarComuna(request.objBodyObjectRequest, comunaRepository));
            return middleWareHandlerResponse;
        }
    }
}
