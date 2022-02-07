using System;
using System.Collections.Generic;
using System.Text;

namespace BallotPalette.Core
{

    public class Question
    {
        public int Id { get; set; }
        public int BallotId { get; set; }
        public string Content { get; set; }
        public int TotalVotes { get; set; }

        public List<Option> Options { get; set; }

        private int NumOptions { get; set; }

        public Question(string text)
        {
            NumOptions = 0;
            Content = text;
        }

        public void AddOption(string text)
        {
            NumOptions++;

            Option option = new Option
            {
                Id = NumOptions,
                Text = text,
                NumVotes = 0
            };

            Options.Add(option);
        }

        public void Vote(int optNumber)
        {
            TotalVotes++;
            Options.Find(o=>o.Id == optNumber).NumVotes++;
        }
    }
}
