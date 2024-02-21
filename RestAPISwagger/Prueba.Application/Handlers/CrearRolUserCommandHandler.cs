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
    public class CrearRolUserCommandHandler : IRequestHandler<CrearRolUserCommand, RolUserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository = null;
        public CrearRolUserCommandHandler(PruebaContext pruebaContext)
        {
            rolUserRepository = new RepositoryEntityFrameworkCQRS<RolUser>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RolUserResponse> Handle(CrearRolUserCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearRolUserModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearRolUser(request.objBodyObjectRequest, rolUserRepository));
            return middleWareHandlerResponse;
        }
    }
}
