using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallotPalette.Core
{
    //Represents a selectable option in a question
    public class Option
    {
        [Required]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int NumVotes { get; set; }
    }
}
