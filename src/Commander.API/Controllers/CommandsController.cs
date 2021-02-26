using System.Collections.Generic;
using AutoMapper;
using Commander.API.Data;
using Commander.API.Dtos;
using Commander.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        // PUT: /api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            // Check to see if the model already exists. If not, return NotFound
            var commandModelFromRepository = _repository.GetCommandById(id);

            if (commandModelFromRepository == null)
            {
                return NotFound();
            }

            // If it does exist, update the model in the db
            // Automapper can update the commandModelFromRepository with the data sent from the client
            _mapper.Map(commandUpdateDto, commandModelFromRepository);

            _repository.UpdateCommand(commandModelFromRepository);
            _repository.SaveChanges();

            // Once done, return a 204 to indicate the resource has been updated
            return NoContent();
        }

        // PATCH: /api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
        {
            // Check to see if the model already exists. If not, return NotFound
            var commandModelFromRepository = _repository.GetCommandById(id);

            if (commandModelFromRepository == null)
            {
                return NotFound();
            }

            // If it does exist, update the model in the db
            // Construct a Command Dto (commandToPatch) which we can apply the model from the db onto, using Automapper
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepository);

            // Apply data from the client's request to the Dto
            patchDocument.ApplyTo(commandToPatch, ModelState);

            // Perform some validation and return an error if it fails
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // We then want to update the model data in our repository
            // First, update the model to have the new changes
            _mapper.Map(commandToPatch, commandModelFromRepository);

            // Then update the command, and save the changes to the db
            _repository.UpdateCommand(commandModelFromRepository);
            _repository.SaveChanges();

            // Once done, return a 204 to indicate the resource has been updated
            return NoContent();
        }

        // DELETE: /api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            // Check to see if the model already exists. If not, return NotFound
            var commandModelFromRepository = _repository.GetCommandById(id);

            if (commandModelFromRepository == null)
            {
                return NotFound();
            }

            // If it does exist, delete the command from context and save to the db
            _repository.DeleteCommand(commandModelFromRepository);
            _repository.SaveChanges();

            // Once done, return a 204 to indicate the resource has been deleted
            return NoContent();
        }
    }
}