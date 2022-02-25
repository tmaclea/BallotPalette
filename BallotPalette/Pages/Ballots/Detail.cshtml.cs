using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BallotPalette.Core;
using BallotPalette.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BallotPalette.Pages.Ballots
{
    public class DetailModel : PageModel
    {
        private readonly IBallotData ballotData;
        private readonly IQuestionData questionData;
        private readonly IOptionData optionData;

        public Ballot Ballot { get; set; }
        public List<Question> Questions { get; set; }
        public List<Option> Options { get; set; }

        [BindProperty]
        public Dictionary<string, int> Selections { get; set; }

        public DetailModel(IBallotData ballotData, IQuestionData questionData, 
                            IOptionData optionData)
        {
            this.ballotData = ballotData;
            this.questionData = questionData;
            this.optionData = optionData;
        }

        public IActionResult OnGet(int ballotId)
        {
            Ballot = ballotData.GetBallotById(ballotId);
            if(Ballot == null)
            {
                return RedirectToPage("./NotFound");
            }

            Questions = questionData.GetQuestionsByBallot(Ballot.Id).ToList();
            Options = new List<Option>();
            Selections = new Dictionary<string, int>();
            foreach(Question q in Questions)
            {
                Selections[q.Text] = 0;
                Options.AddRange(optionData.GetOptionsByQuestion(q.Id).ToList());
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            foreach(KeyValuePair<string, int> entry in Selections)
            {
                try
                {
                    optionData.Vote(entry.Value);
                }
                catch(ArgumentNullException e)
                {
                    Console.WriteLine("Voted on a null ballot");
                    Console.WriteLine(e);
                }
                    
            }

            optionData.Commit();
            TempData["Message"] = "Votes successfully cast";
            return RedirectToPage("./List");
        }
    }
}