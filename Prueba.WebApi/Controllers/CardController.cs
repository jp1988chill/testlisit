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
        private readonly PruebaContext _context;
        public CardController(PruebaContext pruebaContext)
        {
            _context = pruebaContext;

            // Drop the database if it exists
            //_context.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            _context.Database.EnsureCreated();
        }

        [HttpGet("action/GetUsers")]
        public List<User> GetUsers()
        {
            User usuario = Create(new User("", "", new Guid()));
            var res = _context.Users.ToList();
            return res;
        }

        [HttpGet("action/CreateUser")]
        public User Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }


        [HttpGet("action/download")]
        public FileResult Download()
        {
            throw new NotImplementedException();
        }

        //CreacionUsuario

    }
}
