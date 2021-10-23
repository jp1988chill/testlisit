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
    public class ActualizarNombreUsuarioCommandHandler : IRequestHandler<ActualizarNombreUsuarioCommand, CardResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Card> cardRepository = null;
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;

        public ActualizarNombreUsuarioCommandHandler(PruebaContext pruebaContext)
        {
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<CardResponse> Handle(ActualizarNombreUsuarioCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ActualizarNombreUsuarioModel();
            var middleWareHandlerResponse = (await middleWareHandler.ActualizarNombreUsuario(request.NombreUsuarioOriginal, request.NombreUsuarioNuevo, cardRepository, userRepository));
            return middleWareHandlerResponse;
        }
    }
}
