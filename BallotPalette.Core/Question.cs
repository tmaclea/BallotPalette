using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallotPalette.Core
{

    public class Question
    {
        public int Id { get; set; }
        public int BallotId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
