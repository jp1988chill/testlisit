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
    public class ObtenerUsuariosCommandHandler : IRequestHandler<ObtenerUsuariosCommand, UserResponse>
    {
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;
        public ObtenerUsuariosCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);
        }

        public async Task<UserResponse> Handle(ObtenerUsuariosCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerUsuariosModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerUsuario(userRepository));
            return middleWareHandlerResponse;
        }
    }
}
