using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BallotPalette.Core;
using BallotPalette.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BallotPalette.Pages.Ballots
{
    public class EditModel : PageModel
    {
        private readonly IBallotData ballotData;
        private readonly IQuestionData questionData;
        private readonly IOptionData optionData;
        private const int NUM_QUESTIONS = 6;

        [BindProperty]
        public Ballot Ballot { get; set; }
        [BindProperty]
        public List<Question> Questions { get; set; }
        public List<Option> Options { get; set; } 

        public EditModel(IBallotData ballotData, IQuestionData questionData, IOptionData optionData)
        {
            this.ballotData = ballotData;
            this.questionData = questionData;
            this.optionData = optionData;
        }

        public IActionResult OnGet(int? ballotId)
        {

            Questions = Enumerable.Repeat(new Question(), NUM_QUESTIONS).ToList();
            Options = new List<Option>();

            if (ballotId.HasValue)
            {
                Ballot = ballotData.GetBallotById(ballotId.Value);

                int i = 0;
                foreach(Question q in questionData.GetQuestionsByBallot(ballotId.Value).ToList())
                {
                    Questions[i] = q;
                    Options.AddRange(optionData.GetOptionsByQuestion(q.Id));
                    i++;
                };

            } else
            {
                Ballot = new Ballot();
            }

            if (Ballot == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(Ballot.Id > 0)
            {
                ballotData.Update(Ballot);
            }
            else
            {
                ballotData.Add(Ballot);
            }

            Ballot = ballotData.Update(Ballot);
            ballotData.Commit();
            TempData["Message"] = "Ballot saved";
            return RedirectToPage("./Detail", new { ballotId = Ballot.Id }); 
        }

        public string GetNextOption(int? questionId)
        {
            string optionText = string.Empty;

            if (questionId.HasValue)
            {
                foreach (Option o in Options)
                {
                    if (o.QuestionId == questionId)
                    {
                        optionText = o.Text;
                        Options.Remove(o);
                        break;
                    }
                }
            }

            return optionText;
        }
    }
}