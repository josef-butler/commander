using System.Collections.Generic;
using Commander.API.Models;

namespace Commander.API.Data
{
    public class MockCommanderRepository : ICommanderRepository
    {
        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
                {
                    new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and pan" },
                    new Command { Id = 1, HowTo = "Slice bread", Line = "Get a knife", Platform = "Knife and chopping board" },
                    new Command { Id = 2, HowTo = "Make a cup of tea", Line = "Place teabag in cup", Platform = "Kettle and cup" }
                };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and pan" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}