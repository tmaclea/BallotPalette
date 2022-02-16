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

        //Deletes all options associated with a question
        IEnumerable<Option> Clear(int questionId);
        Option Vote(int optionId);

        Option Add(Option newOption);
        int Commit();
    }

    public class InMemoryOptionData : IOptionData
    {
        List<Option> options;

        public InMemoryOptionData()
        {
            options = new List<Option>
            {
                new Option { Id = 1, QuestionId = 1, Text = "Choice number 1", NumVotes = 0 },
                new Option { Id = 2, QuestionId = 1, Text = "Choice number 2", NumVotes = 1 },
                new Option { Id = 3, QuestionId = 1, Text = "Choice number 3", NumVotes = 0 },
                new Option { Id = 4, QuestionId = 1, Text = "Choice number 4", NumVotes = 8 },
                new Option { Id = 5, QuestionId = 2, Text = "Choice number 5", NumVotes = 7 },
                new Option { Id = 6, QuestionId = 2, Text = "Choice number 6", NumVotes = 4 },
                new Option { Id = 7, QuestionId = 3, Text = "Choice number 7", NumVotes = 0 },
                new Option { Id = 8, QuestionId = 3, Text = "Choice number 8", NumVotes = 8 },
                new Option { Id = 9, QuestionId = 3, Text = "Choice number 9", NumVotes = 0 },
                new Option { Id = 10, QuestionId = 4, Text = "Choice number 10", NumVotes = 0 },
                new Option { Id = 11, QuestionId = 4, Text = "Choice number 11", NumVotes = 6 },
                new Option { Id = 12, QuestionId = 5, Text = "Choice number 12", NumVotes = 5 },
                new Option { Id = 13, QuestionId = 5, Text = "Choice number 13", NumVotes = 0 },
                new Option { Id = 14, QuestionId = 5, Text = "Choice number 14", NumVotes = 0 },
                new Option { Id = 15, QuestionId = 5, Text = "Choice number 15", NumVotes = 1 },
                new Option { Id = 16, QuestionId = 5, Text = "Choice number 16", NumVotes = 2 },
                new Option { Id = 17, QuestionId = 6, Text = "Choice number 17", NumVotes = 4 },
                new Option { Id = 18, QuestionId = 6, Text = "Choice number 18", NumVotes = 0 },
                new Option { Id = 19, QuestionId = 7, Text = "Choice number 19", NumVotes = 0 },
                new Option { Id = 20, QuestionId = 7, Text = "Choice number 20", NumVotes = 1 }
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

        public IEnumerable<Option> Clear(int questionId)
        {
            options = options.Except(GetOptionsByQuestion(questionId)).ToList();
            return options;
        }

        public Option Vote(int optionId)
        {
            var option = options.SingleOrDefault(o => o.Id == optionId);

            if(option != null)
            {
                option.NumVotes++;
            }

            return option;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
