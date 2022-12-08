using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        public CommandsController(ICommandAPIRepo repository){
            _repository = repository
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"This", "is", "hard", "coded"};
        }
    }
}