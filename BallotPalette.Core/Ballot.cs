using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallotPalette.Core
{
    public class Ballot
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
