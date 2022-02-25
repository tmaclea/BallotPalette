using BallotPalette.Core;
using System.Collections.Generic;
using System.Text;

namespace BallotPalette.Data
{
    public interface IBallotData
    {
        Ballot GetBallotById(int id);
        IEnumerable<Ballot> GetBallotsByName(string name);
        Ballot Update(Ballot updatedBallot);
        Ballot Add(Ballot newBallot);
        Ballot Delete(int id);
        Ballot Delete(string name);
        int Commit();
    }
}
