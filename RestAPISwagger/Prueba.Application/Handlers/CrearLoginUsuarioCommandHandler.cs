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
    public class CrearLoginUsuarioCommandHandler : IRequestHandler<CrearLoginUsuarioCommand, LoginUsuarioResponse>
    {
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;
        public CrearLoginUsuarioCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);
        }

        public async Task<LoginUsuarioResponse> Handle(CrearLoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearLoginUsuarioModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearLoginUsuario(request.objBodyObjectRequest, userRepository));
            return middleWareHandlerResponse;
        }
    }
}
