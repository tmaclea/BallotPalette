using BallotPalette.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BallotPalette.Data
{
    public class SqlOptionData : IOptionData
    {
        private readonly BallotPaletteDbContext db;

        public SqlOptionData(BallotPaletteDbContext db)
        {
            this.db = db;
        }

        public Option Add(Option newOption)
        {
            db.Options.Add(newOption);
            return newOption;
        }

        public IEnumerable<Option> Clear(int questionId)
        {
            IEnumerable<Option> questionOptions = GetOptionsByQuestion(questionId);
            db.Options.RemoveRange(questionOptions);
            return questionOptions;
        }

        public IEnumerable<Option> GetOptionsByQuestion(int questionId)
        {
            var query = from o in db.Options
                        where o.QuestionId == questionId
                        select o;
            return query;
        }

        public Option Vote(int optionId)
        {
            Option option = db.Options.Find(optionId);
            if(option != null)
            {
                option.NumVotes++;
                var entity = db.Options.Attach(option);
                entity.State = EntityState.Modified;
            }else
            {
                throw new ArgumentNullException();
            }
            
            return option;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }
    }
}
