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
    public class ObtenerComunaCommandHandler : IRequestHandler<ObtenerComunaCommand, ComunaResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository = null;
        public ObtenerComunaCommandHandler(PruebaContext pruebaContext)
        {
            comunaRepository = new RepositoryEntityFrameworkCQRS<Comuna>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<ComunaResponse> Handle(ObtenerComunaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerComunaModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerComuna(request.Idcomuna, comunaRepository));
            return middleWareHandlerResponse;
        }
    }

    public class ObtenerComunasCommandHandler : IRequestHandler<ObtenerComunasCommand, ComunaResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository = null;
        public ObtenerComunasCommandHandler(PruebaContext pruebaContext)
        {
            comunaRepository = new RepositoryEntityFrameworkCQRS<Comuna>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<ComunaResponse> Handle(ObtenerComunasCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerComunaModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerComunasCollection(comunaRepository));
            return middleWareHandlerResponse;
        }
    }
}
