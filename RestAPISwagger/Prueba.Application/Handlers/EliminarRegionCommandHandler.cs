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
    public class EliminarRegionCommandHandler : IRequestHandler<EliminarRegionCommand, RegionResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Region_> regionRepository = null;
        public EliminarRegionCommandHandler(PruebaContext pruebaContext)
        {
            regionRepository = new RepositoryEntityFrameworkCQRS<Region_>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RegionResponse> Handle(EliminarRegionCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new EliminarRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.EliminarRegion(request.objBodyObjectRequest, regionRepository));
            return middleWareHandlerResponse;
        }
    }
}
