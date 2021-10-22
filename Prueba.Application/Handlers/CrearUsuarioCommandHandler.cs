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
    public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, UserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;
        public CrearUsuarioCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);
        }

        public async Task<UserResponse> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearUsuarioModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearUsuario(request.objBodyObjectRequest, userRepository));
            return middleWareHandlerResponse;
        }
    }
}
