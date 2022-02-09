﻿using BallotPalette.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallotPalette.Data
{
    public interface IBallotData
    {
        IEnumerable<Ballot> GetBallotsByName(string teamName);
        Ballot GetBallotById(int id);
        Ballot Update(Ballot updatedBallot);
        Ballot Add(Ballot newBallot);
        int Commit();
    }

    public class InMemoryBallotData : IBallotData
    {
        List<Ballot> ballots;

        public InMemoryBallotData()
        {
            ballots = new List<Ballot>
            {
                new Ballot { Id = 1, Name = "Aperture Science ballot one"},
                new Ballot { Id = 2, Name = "Aperture Science second ballot"},
                new Ballot { Id = 3, Name = "Black Mesa ballot one"},
                new Ballot { Id = 4, Name = "Black mesa second ballot"},
                new Ballot { Id = 5, Name = "EO3 ballot one"},
                new Ballot { Id = 6, Name = "EO3 second ballot"},
                new Ballot { Id = 7, Name = "EO4 ballot one"},
                new Ballot { Id = 8, Name = "EO4 second ballot"}
            };
        }

        public IEnumerable<Ballot> GetBallotsByName(string name = null)
        {
            return from b in ballots
                   where string.IsNullOrEmpty(name) || b.Name.ToLower().Contains(name.ToLower())
                   orderby b.Name
                   select b;
        }

        public Ballot GetBallotById(int id)
        {
            return ballots.SingleOrDefault(b => b.Id == id);
        }

        public Ballot Update(Ballot updatedBallot)
        {
            var ballot = ballots.SingleOrDefault(r => r.Id == updatedBallot.Id);
            if(ballot != null)
            {
                ballot.Name = updatedBallot.Name;
            }
            return ballot;
        }

        public Ballot Add(Ballot newBallot)
        {
            newBallot.Id = ballots.Max(b => b.Id) + 1;
            ballots.Add(newBallot);

            return newBallot;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
