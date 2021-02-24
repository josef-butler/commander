using AutoMapper;
using Commander.API.Dtos;
using Commander.API.Models;

namespace Commander.API.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            // Convert the Command from the db to CommandReadDto when retriving data from the db
            CreateMap<Command, CommandReadDto>();
            // Convert the Dto from the client into a Command object when adding to the db
            CreateMap<CommandCreateDto, Command>();
        }
    }
}