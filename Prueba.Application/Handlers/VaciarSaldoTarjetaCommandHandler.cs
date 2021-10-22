﻿using MediatR;
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
    public class VaciarSaldoTarjetaCommandHandler : IRequestHandler<VaciarSaldoTarjetaCommand, CardResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        public VaciarSaldoTarjetaCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
        }

        public async Task<CardResponse> Handle(VaciarSaldoTarjetaCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new VaciarSaldoTarjetaModel();
            var middleWareHandlerResponse = (await middleWareHandler.VaciarSaldoTarjetas(request.objBodyObjectRequest, cardRepository));
            return middleWareHandlerResponse;
        }
    }
}