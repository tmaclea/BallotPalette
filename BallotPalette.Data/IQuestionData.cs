using BallotPalette.Core;
using System.Collections.Generic;
using System.Text;

namespace BallotPalette.Data
{
    public interface IQuestionData
    {
        IEnumerable<Question> GetQuestionsByBallot(int ballotId);
        Question Add(Question newQuestion);
        Question Update(Question updatedQuestion);
        void Delete(Question question);
        int Commit();
    }
}
