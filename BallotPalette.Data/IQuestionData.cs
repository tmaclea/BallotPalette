using BallotPalette.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public class InMemoryQuestionData : IQuestionData
    {
        List<Question> questions;

        public InMemoryQuestionData()
        {
            questions = new List<Question>
            {
                new Question { Id = 1, BallotId = 1, Text = "Is this question one?"},
                new Question { Id = 2, BallotId = 1, Text = "Is this question two?"},
                new Question { Id = 3, BallotId = 2, Text = "Is this question three?"},
                new Question { Id = 4, BallotId = 2, Text = "Is this question four?"},
                new Question { Id = 5, BallotId = 3, Text = "Is this question five?"},
                new Question { Id = 6, BallotId = 4, Text = "Is this question six?"},
                new Question { Id = 7, BallotId = 5, Text = "Is this question seven?"}
            };
        }

        public IEnumerable<Question> GetQuestionsByBallot(int ballotId)
        {
            return from q in questions
                   where q.BallotId == ballotId
                   orderby q.Id
                   select q;
        }

        public Question Add(Question newQuestion)
        {
            if(string.IsNullOrEmpty(newQuestion.Text))
            {
                return newQuestion;
            }

            newQuestion.Id = questions.Max(q => q.Id) + 1;
            questions.Add(newQuestion);
            return newQuestion;
        }

        public Question Update(Question updatedQuestion)
        {
            var question = questions.SingleOrDefault(q => q.Id == updatedQuestion.Id);
            if(question != null)
            {
                question.Text = updatedQuestion.Text;
            }

            return question;
        }

        public void Delete(Question question)
        {
            questions.Remove(question);

        }

        public int Commit()
        {
            return 0;
        }
    }

}
