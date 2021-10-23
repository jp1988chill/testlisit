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
    public class ObtenerTarjetaPorNombreUsuarioCommandHandler : IRequestHandler<ObtenerTarjetaPorNombreUsuarioCommand, CardInfoResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public ObtenerTarjetaPorNombreUsuarioCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardInfoResponse> Handle(ObtenerTarjetaPorNombreUsuarioCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerTarjetaPorNombreUsuarioModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerTarjetaPorNombreUsuario(request.NombreUsuario, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}
