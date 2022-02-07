using BallotPalette.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallotPalette.Data
{
    public class BallotPaletteDbContext : DbContext
    {
        public DbSet<Ballot> Ballots { get; set; }
    }
}
