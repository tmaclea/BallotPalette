using BallotPalette.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallotPalette.Data
{
    public interface IOptionData
    {
        IEnumerable<Option> GetOptionsByQuestion(int questionId);
        Option Add(Option newOption);
        Option Update(Option updatedOption);
    }

    public class InMemoryOptionData : IOptionData
    {
        List<Option> options;

        public InMemoryOptionData()
        {
            options = new List<Option>
            {
                new Option { Id = 1, QuestionId = 1, Text = "Option 1", NumVotes = 0 },
                new Option { Id = 2, QuestionId = 1, Text = "Option 2", NumVotes = 1 },
                new Option { Id = 3, QuestionId = 1, Text = "Option 3", NumVotes = 0 },
                new Option { Id = 4, QuestionId = 1, Text = "Option 4", NumVotes = 8 },
                new Option { Id = 5, QuestionId = 2, Text = "Option 5", NumVotes = 7 },
                new Option { Id = 6, QuestionId = 2, Text = "Option 6", NumVotes = 4 },
                new Option { Id = 7, QuestionId = 3, Text = "Option 7", NumVotes = 0 },
                new Option { Id = 8, QuestionId = 3, Text = "Option 8", NumVotes = 8 },
                new Option { Id = 9, QuestionId = 3, Text = "Option 9", NumVotes = 0 },
                new Option { Id = 10, QuestionId = 4, Text = "Option 10", NumVotes = 0 },
                new Option { Id = 11, QuestionId = 4, Text = "Option 11", NumVotes = 6 },
                new Option { Id = 12, QuestionId = 5, Text = "Option 12", NumVotes = 5 },
                new Option { Id = 13, QuestionId = 5, Text = "Option 13", NumVotes = 0 },
                new Option { Id = 14, QuestionId = 5, Text = "Option 14", NumVotes = 0 },
                new Option { Id = 15, QuestionId = 5, Text = "Option 15", NumVotes = 1 },
                new Option { Id = 16, QuestionId = 5, Text = "Option 16", NumVotes = 2 },
                new Option { Id = 17, QuestionId = 6, Text = "Option 17", NumVotes = 4 },
                new Option { Id = 18, QuestionId = 6, Text = "Option 18", NumVotes = 0 },
                new Option { Id = 19, QuestionId = 7, Text = "Option 19", NumVotes = 0 },
                new Option { Id = 20, QuestionId = 7, Text = "Option 20", NumVotes = 1 }
            };
        }

        public Option Add(Option newOption)
        {
            newOption.Id = options.Max(o => o.Id) + 1;
            options.Add(newOption);

            return newOption;
        }

        public IEnumerable<Option> GetOptionsByQuestion(int questionId)
        {
            return from o in options
                where  o.QuestionId == questionId
                orderby o.Id
                select o;
        }

        public Option Update(Option updatedOption)
        {
            var option = options.SingleOrDefault(o => o.Id == updatedOption.Id);

            if(option != null)
            {
                option.Text = updatedOption.Text;
                foreach (Option o in options)
                {
                    if(o.QuestionId == updatedOption.QuestionId)
                    {
                        o.NumVotes = 0;
                    }
                }
            }

            return updatedOption;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
