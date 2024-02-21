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
    public class CrearRegionCommandHandler : IRequestHandler<CrearRegionCommand, RegionResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Region> regionRepository = null;
        public CrearRegionCommandHandler(PruebaContext pruebaContext)
        {
            regionRepository = new RepositoryEntityFrameworkCQRS<Region>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RegionResponse> Handle(CrearRegionCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearRegion(request.objBodyObjectRequest, regionRepository));
            return middleWareHandlerResponse;
        }
    }
}
