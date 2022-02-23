using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BallotPalette.Core;
using BallotPalette.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BallotPalette.Pages.Ballots
{
    public class ResultsModel : PageModel
    {
        private readonly IBallotData ballotData;
        private readonly IQuestionData questionData;
        private readonly IOptionData optionData;

        public Ballot Ballot { get; set; }
        public List<Question> Questions { get; set; }
        public List<Option> Options { get; set; }
        public DateTime time { get; set; }

        public ResultsModel(IBallotData ballotData, IQuestionData questionData, 
                        IOptionData optionData)
        {
            this.ballotData = ballotData;
            this.questionData = questionData;
            this.optionData = optionData;
        }

        public IActionResult OnGet(int ballotId)
        {
            Ballot = ballotData.GetBallotById(ballotId);
            Questions = questionData.GetQuestionsByBallot(Ballot.Id).ToList();
            Options = new List<Option>();
            time = DateTime.Now;

            foreach(Question q in Questions)
            {
                Options.AddRange(optionData.GetOptionsByQuestion(q.Id).ToList());
            }

            if (Ballot == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public string GetLabels(int questionId)
        {
            string labels = string.Empty;
            List<Option> questionOptions = optionData.GetOptionsByQuestion(questionId).ToList();

            foreach(Option o in questionOptions)
            {
                labels += "'" +  o.Text + "',";
            }

            return labels;
        }

        public string GetData(int questionId)
        {
            string data = string.Empty;
            List<Option> questionOptions = optionData.GetOptionsByQuestion(questionId).ToList();

            foreach(Option o in questionOptions)
            {
                data += o.NumVotes.ToString() + ",";
            }

            return data;
        }

        public List<Option> GetOptionsByQuestion(int questionId)
        {
            return optionData.GetOptionsByQuestion(questionId).ToList();
        }
        public int GetTotalVotes(int questionId)
        {
            int total = 0;
            foreach(Option o in optionData.GetOptionsByQuestion(questionId))
            {
                total += o.NumVotes;
            }
            return total;
        }
    }
}