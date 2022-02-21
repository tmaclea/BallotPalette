using BallotPalette.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BallotPalette.Data
{
    public class SqlQuestionData : IQuestionData
    {
        private readonly BallotPaletteDbContext db;

        public SqlQuestionData(BallotPaletteDbContext db)
        {
            this.db = db;
        }

        public Question Add(Question newQuestion)
        {
            db.Questions.Add(newQuestion);
            //I must commit changes because I need the Id value in the output
            Commit();
            return newQuestion;
        }

        public void Delete(Question question)
        {
            var deletedQuestion = db.Questions.Find(question.Id);
            if(deletedQuestion != null)
            {
                db.Questions.Remove(deletedQuestion);
            }
        }

        public IEnumerable<Question> GetQuestionsByBallot(int ballotId)
        {
            var query = from q in db.Questions
                        where q.BallotId == ballotId
                        orderby q.Id
                        select q;
            return query;
        }

        public Question Update(Question updatedQuestion)
        {
            var entity = db.Questions.Attach(updatedQuestion);
            entity.State = EntityState.Modified;
            return updatedQuestion;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }
    }
}
