using BallotPalette.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallotPalette.Data
{
    public interface ITeamData
    {
        IEnumerable<Team> GetTeamsByName(string team);
    }

    public class InMemoryTeamData : ITeamData
    {
        List<Team> teams;

        public InMemoryTeamData()
        {
            teams = new List<Team>
            {
                new Team { Id = 1, Name = "Aperture Science"},
                new Team { Id = 2, Name = "Black Mesa"},
                new Team { Id = 3, Name = "Example Org three"},
                new Team { Id = 4, Name = "Example org four"}
            };
        }

        public IEnumerable<Team> GetTeamsByName(string name = null)
        {
            return from t in teams
                   where string.IsNullOrEmpty(name) || t.Name.Contains(name)
                   orderby t.Name
                   select t;
        }
    }
}
