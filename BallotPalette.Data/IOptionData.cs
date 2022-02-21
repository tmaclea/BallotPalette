using BallotPalette.Core;
using System.Collections.Generic;
using System.Text;

namespace BallotPalette.Data
{
    public interface IOptionData
    {
        IEnumerable<Option> GetOptionsByQuestion(int questionId);

        //Deletes all options associated with a question
        IEnumerable<Option> Clear(int questionId);
        Option Vote(int optionId);

        Option Add(Option newOption);
        int Commit();
    }
}
