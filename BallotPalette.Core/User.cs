using System;
using System.Collections.Generic;
using System.Text;

namespace BallotPalette.Core
{
    public class User
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
