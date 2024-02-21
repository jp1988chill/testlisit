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
    public class EliminarRolUserCommandHandler : IRequestHandler<EliminarRolUserCommand, RolUserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<RolUser> RoluserRepository = null;
        public EliminarRolUserCommandHandler(PruebaContext pruebaContext)
        {
            RoluserRepository = new RepositoryEntityFrameworkCQRS<RolUser>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RolUserResponse> Handle(EliminarRolUserCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new EliminarRolUserModel();
            var middleWareHandlerResponse = (await middleWareHandler.EliminarRolUser(request.objBodyObjectRequest, RoluserRepository));
            return middleWareHandlerResponse;
        }
    }
}
