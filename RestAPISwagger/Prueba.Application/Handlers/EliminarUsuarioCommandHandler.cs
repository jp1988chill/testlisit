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
    public class EliminarUsuarioCommandHandler : IRequestHandler<EliminarUsuarioCommand, UserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;
        public EliminarUsuarioCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);
        }

        public async Task<UserResponse> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new EliminarUsuarioModel();
            var middleWareHandlerResponse = (await middleWareHandler.EliminarUsuario(request.objBodyObjectRequest, userRepository));
            return middleWareHandlerResponse;
        }
    }
}
