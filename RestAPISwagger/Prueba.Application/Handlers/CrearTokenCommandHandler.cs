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
    public class CrearTokenCommandHandler : IRequestHandler<CrearTokenCommand, TokenResponse>
    {
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;
        public CrearTokenCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);
        }

        public async Task<TokenResponse> Handle(CrearTokenCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new CrearTokenModel();
            var middleWareHandlerResponse = (await middleWareHandler.CrearToken(request.objBodyObjectRequest, userRepository));
            return middleWareHandlerResponse;
        }
    }
}
