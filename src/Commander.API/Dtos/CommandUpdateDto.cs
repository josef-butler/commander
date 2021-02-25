using System.ComponentModel.DataAnnotations;

namespace Commander.API.Dtos
{
    public class CommandUpdateDto
    {
        // Id is not needed as it will be included in the request URL

        [Required]
        public string HowTo { get; set; }
        
        [Required]
        public string Line { get; set; }
        
        [Required]
        public string Platform { get; set; }
    }
}