using System.Collections.Generic;
using AutoMapper;
using Commander.API.Data;
using Commander.API.Dtos;
using Commander.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.API.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        // Create readonly variables assigned with base implementations of our interfaces
        private readonly ICommanderRepository _repository;
        private readonly IMapper _mapper;

        // Use a constructor allow dependencies to be injected - dependencies are added in Startup.cs, and can be included in the constructor as added arguments
        public CommandsController(ICommanderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            // Return an IEnumerable of the mapped Dto, not the IEnumerable returned by the db query
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if(commandItem != null)
            {
                // Return a mapped Dto, not the object returned by the db query
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }
    }
}