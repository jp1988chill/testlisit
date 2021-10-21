using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;

namespace Prueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpGet("action/download")]
        public FileResult Download()
        {
            throw new NotImplementedException();
        }
    }
}
