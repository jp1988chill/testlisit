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
    public class ActualizarRegionCommandHandler : IRequestHandler<ActualizarRegionCommand, RegionResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Region_> regionRepository = null;

        public ActualizarRegionCommandHandler(PruebaContext pruebaContext)
        {
            regionRepository = new RepositoryEntityFrameworkCQRS<Region_>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RegionResponse> Handle(ActualizarRegionCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ActualizarRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.ActualizarRegion(request.Regiones, regionRepository));
            return middleWareHandlerResponse;
        }
    }
}
