using MediatR;
using Prueba.Application.Commands;
using Prueba.Domain;
using Prueba.Domain.Models;
using Prueba.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prueba.Application.Handlers
{
    public class ObtenerRegionCommandHandler : IRequestHandler<ObtenerRegionCommand, RegionResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Region_> regionRepository = null;
        public ObtenerRegionCommandHandler(PruebaContext pruebaContext)
        {
            regionRepository = new RepositoryEntityFrameworkCQRS<Region_>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RegionResponse> Handle(ObtenerRegionCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerRegion(request.Idregion, regionRepository));
            return middleWareHandlerResponse;
        }
    }

    public class ObtenerRegionesCommandHandler : IRequestHandler<ObtenerRegionesCommand, RegionResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Region_> regionRepository = null;
        public ObtenerRegionesCommandHandler(PruebaContext pruebaContext)
        {
            regionRepository = new RepositoryEntityFrameworkCQRS<Region_>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RegionResponse> Handle(ObtenerRegionesCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerRegionesCollection(regionRepository));
            return middleWareHandlerResponse;
        }
    }
}
