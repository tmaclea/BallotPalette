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
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }

        public BallotPaletteDbContext(DbContextOptions<BallotPaletteDbContext> options)
            : base(options)
        {

        }
    }
}
