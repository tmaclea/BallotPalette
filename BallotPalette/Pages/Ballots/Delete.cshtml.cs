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
    public class DeleteModel : PageModel
    {
        private readonly IBallotData ballotData;
        private readonly IQuestionData questionData;
        private readonly IOptionData optionData;

        public Ballot Ballot { get; set; }

        public DeleteModel(IBallotData ballotData, IQuestionData questionData, IOptionData optionData)
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
            return Page();
        }

        public IActionResult OnPost(int ballotId)
        {
            Ballot = ballotData.GetBallotById(ballotId);
            if (Ballot == null)
            {
                return RedirectToPage("./NotFound");
            }

            foreach (Question q in questionData.GetQuestionsByBallot(ballotId))
            {
                optionData.Clear(q.Id);
                questionData.Delete(q);
            }
            ballotData.Delete(ballotId);

            optionData.Commit();
            questionData.Commit();
            ballotData.Commit();

            TempData["Message"] = $"Ballot \"{Ballot.Name}\" deleted.";
            return RedirectToPage("./List");
        }
    }
}