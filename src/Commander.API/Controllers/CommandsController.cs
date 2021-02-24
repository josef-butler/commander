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

        // GET: api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            // Return an IEnumerable of the mapped Dto, not the IEnumerable returned by the db query
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET: api/commands/{id}
        // This ActionResult is named so we can make use of it in the CreatedAtRoute method
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
            {
                // Return a mapped Dto, not the object returned by the db query
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        // POST: api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            // Use Automapper to convert the Dto from the client into a Command model
            var commandModel = _mapper.Map<Command>(commandCreateDto);

            // Create the model in the repository, and save the changes to the db
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            // Use Automapper to create the Dto to be passed back to the client based on the commandModel provided
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            // Returns the commandReadDto with the URI to the newly created resource URI included in the header (utilising the GetCommandById ActionResult)
            // This is important as part of the REST API spec of creating a new resource
            // CreatedAtRoute will also return a 201 Created response
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }
    }
}