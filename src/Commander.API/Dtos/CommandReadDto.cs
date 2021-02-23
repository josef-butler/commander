namespace Commander.API.Dtos
{
    // Structure of the object that clients will receive
    public class CommandReadDto
    {
        public int Id { get; set; }

        public string HowTo { get; set; }

        public string Line { get; set; }
    }
}