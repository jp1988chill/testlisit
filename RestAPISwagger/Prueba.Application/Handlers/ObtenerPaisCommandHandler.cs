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
    public class ObtenerPaisCommandHandler : IRequestHandler<ObtenerPaisCommand, PaisResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Pais> paisRepository = null;
        public ObtenerPaisCommandHandler(PruebaContext pruebaContext)
        {
            paisRepository = new RepositoryEntityFrameworkCQRS<Pais>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<PaisResponse> Handle(ObtenerPaisCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerPaisModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerPais(request.IdPais, paisRepository));
            return middleWareHandlerResponse;
        }
    }
}
