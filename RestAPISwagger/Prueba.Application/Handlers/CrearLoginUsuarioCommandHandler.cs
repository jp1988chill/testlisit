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
    public class CrearLoginUserCommandHandler : IRequestHandler<CrearLoginUserCommand, LoginUsuarioResponse>
    {
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;
        public CrearLoginUserCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<LoginUsuarioResponse> Handle(CrearLoginUserCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearLoginUserModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearLoginUser(request.objBodyObjectRequest, userRepository));
            return middleWareHandlerResponse;
        }
    }
}
