using System.ComponentModel.DataAnnotations;

namespace Commander.API.Dtos
{
    // Structure of the object that clients must send to create a command in the db
    // Required annotations are included so that the API can return useful error messaging to the client (instead of a 500 server error)
    public class CommandCreateDto
    {
        [Required]
        public string HowTo { get; set; }
        
        [Required]
        public string Line { get; set; }
        
        [Required]
        public string Platform { get; set; }
    }
}