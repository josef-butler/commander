using AutoMapper;
using Commander.API.Dtos;
using Commander.API.Models;

namespace Commander.API.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Map an object's structure from source (Command) to destination (CommandReadDto). Where properties have the same name, AutoMapper will map them automatically
            CreateMap<Command, CommandReadDto>();
        }
    }
}