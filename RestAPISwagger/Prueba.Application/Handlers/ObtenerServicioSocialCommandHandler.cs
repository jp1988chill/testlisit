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
    public class ObtenerServicioSocialCommandHandler : IRequestHandler<ObtenerServicioSocialCommand, ServicioSocialResponse>
    {
        private IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository = null;
        public ObtenerServicioSocialCommandHandler(PruebaContext pruebaContext)
        {
            servicioSocialRepository = new RepositoryEntityFrameworkCQRS<ServicioSocial>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<ServicioSocialResponse> Handle(ObtenerServicioSocialCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerServicioSocialModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerServicioSocial(request.Idserviciosocial, servicioSocialRepository));
            return middleWareHandlerResponse;
        }
    }

    public class ObtenerServiciosSocialesCommandHandler : IRequestHandler<ObtenerServiciosSocialesCommand, ServicioSocialResponse>
    {
        private IRepositoryEntityFrameworkCQRS<ServicioSocial> servicioSocialRepository = null;
        public ObtenerServiciosSocialesCommandHandler(PruebaContext pruebaContext)
        {
            servicioSocialRepository = new RepositoryEntityFrameworkCQRS<ServicioSocial>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<ServicioSocialResponse> Handle(ObtenerServiciosSocialesCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerServicioSocialModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerServiciosSocialesCollection(servicioSocialRepository));
            return middleWareHandlerResponse;
        }
    }
}
