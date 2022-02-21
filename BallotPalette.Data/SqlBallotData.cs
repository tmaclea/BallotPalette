using BallotPalette.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BallotPalette.Data
{
    public class SqlBallotData : IBallotData
    {
        private readonly BallotPaletteDbContext db;

        public SqlBallotData(BallotPaletteDbContext db)
        {
            this.db = db;
        }

        public Ballot Add(Ballot newBallot)
        {
            db.Ballots.Add(newBallot);
            return newBallot;
        }

        public Ballot Delete(int id)
        {
            var ballot = GetBallotById(id);
            if(ballot != null)
            {
                db.Ballots.Remove(ballot);
            }

            return ballot;
        }

        public Ballot GetBallotById(int id)
        {
            return db.Ballots.Find(id);
        }

        public Ballot Update(Ballot updatedBallot)
        {
            var entity = db.Ballots.Attach(updatedBallot);
            entity.State = EntityState.Modified;
            return updatedBallot;
        }

        public IEnumerable<Ballot> GetBallotsByName(string name)
        {
            var query = from b in db.Ballots
                        where string.IsNullOrEmpty(name) || b.Name.ToLower().Contains(name.ToLower())
                        orderby b.Id
                        select b;

            return query;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

    }
}
