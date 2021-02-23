using Commander.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.API.Data
{
    // Create our db context class, inheriting from the DbContext class
    public class CommanderContext : DbContext
    {
        // Define a constructor and give the opportunity to set some particular options. In this case we have no extra options to set
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }

        // Create a representation of our Command model in our database by defining it as a property. This is saying we want to represent our Command object down to our database as a DbSet, and it will be called Commands
        public DbSet<Command> Commands { get; set; }
    }
}