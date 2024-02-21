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
    public class ObtenerRolUserCommandHandler : IRequestHandler<ObtenerRolUserCommand, RolUserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository = null;
        public ObtenerRolUserCommandHandler(PruebaContext pruebaContext)
        {
            rolUserRepository = new RepositoryEntityFrameworkCQRS<RolUser>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RolUserResponse> Handle(ObtenerRolUserCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerRolUserModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerRolUser(request.IdRolUser, rolUserRepository));
            return middleWareHandlerResponse;
        }
    }

    public class ObtenerRolUsersCommandHandler : IRequestHandler<ObtenerRolUsersCommand, RolUserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository = null;
        public ObtenerRolUsersCommandHandler(PruebaContext pruebaContext)
        {
            rolUserRepository = new RepositoryEntityFrameworkCQRS<RolUser>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RolUserResponse> Handle(ObtenerRolUsersCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerRolUserModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerRolUsersCollection(rolUserRepository));
            return middleWareHandlerResponse;
        }
    }
}
