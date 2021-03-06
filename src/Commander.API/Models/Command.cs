﻿using System.ComponentModel.DataAnnotations;

namespace Commander.API.Models
{
    // Model of the object as it exists in the db
    public class Command
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}
