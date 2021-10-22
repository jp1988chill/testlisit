using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Prueba.Repository;
using System.Collections.Generic;
using Prueba.Domain;
using System.Linq;

namespace Prueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private IGenericRepository<User> userRepository = null;
        private IGenericRepository<Card> cardRepository = null;

        public CardController(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<User>(pruebaContext);
            cardRepository = new RepositoryEntityFrameworkCQRS<Card>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        [HttpGet("action/GetUsers")]
        public List<User> GetUsers()
        {
            return userRepository.Get().ToList();
        }

        [HttpGet("action/CreateUser")]
        public void Create(User user)
        {
            userRepository.Insert(user);
            userRepository.Save();
        }


        [HttpGet("action/download")]
        public FileResult Download()
        {
            Create(new Domain.User("Usuario8", "Password8", new Guid()));
            throw new NotImplementedException();
        }

    }
}
