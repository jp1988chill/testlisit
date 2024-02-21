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
    public class EliminarServicioSocialCommandHandler : IRequestHandler<EliminarServicioSocialCommand, ServicioSocialResponse>
    {
        private IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository = null;
        public EliminarServicioSocialCommandHandler(PruebaContext pruebaContext)
        {
            servicioSocialRepository = new RepositoryEntityFrameworkCQRS<ServicioSocial>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<ServicioSocialResponse> Handle(EliminarServicioSocialCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new EliminarServicioSocialModel();
            var middleWareHandlerResponse = (await middleWareHandler.EliminarServicioSocial(request.objBodyObjectRequest, servicioSocialRepository));
            return middleWareHandlerResponse;
        }
    }
}
