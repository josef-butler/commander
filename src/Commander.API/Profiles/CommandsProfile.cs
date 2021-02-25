using AutoMapper;
using Commander.API.Dtos;
using Commander.API.Models;

namespace Commander.API.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target. Convert db model to Dto or vice versa, depending on use case
            // Used for GET requests
            CreateMap<Command, CommandReadDto>();
            
            // Used for POST requests
            CreateMap<CommandCreateDto, Command>();

            // Used for PUT requests
            CreateMap<CommandUpdateDto, Command>();

            // Used for PATCH requests
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}