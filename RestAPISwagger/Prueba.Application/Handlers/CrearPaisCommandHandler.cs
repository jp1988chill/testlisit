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
    public class CrearPaisCommandHandler : IRequestHandler<CrearPaisCommand, PaisResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Pais> paisRepository = null;
        public CrearPaisCommandHandler(PruebaContext pruebaContext)
        {
            paisRepository = new RepositoryEntityFrameworkCQRS<Pais>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<PaisResponse> Handle(CrearPaisCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearPaisModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearPais(request.objBodyObjectRequest, paisRepository));
            return middleWareHandlerResponse;
        }
    }
}
