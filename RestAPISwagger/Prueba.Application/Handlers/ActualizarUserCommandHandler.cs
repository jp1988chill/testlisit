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
    public class ActualizarUserCommandHandler : IRequestHandler<ActualizarUserCommand, UserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;

        public ActualizarUserCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<UserResponse> Handle(ActualizarUserCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ActualizarUserModel();
            var middleWareHandlerResponse = (await middleWareHandler.ActualizarUser(request.Users, userRepository));
            return middleWareHandlerResponse;
        }
    }
}
