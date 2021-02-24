namespace Commander.API.Dtos
{
    // Structure of the object that clients must send to create a command in the db
    public class CommandCreateDto
    {
        public string HowTo { get; set; }

        public string Line { get; set; }

        public string Platform { get; set; }
    }
}