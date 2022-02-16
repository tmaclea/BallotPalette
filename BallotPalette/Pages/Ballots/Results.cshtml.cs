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
    public class ResultsModel : PageModel
    {
        private readonly IBallotData ballotData;

        public Ballot Ballot { get; set; }

        public ResultsModel(IBallotData ballotData)
        {
            this.ballotData = ballotData;
        }

        public IActionResult OnGet(int ballotId)
        {
            Ballot = ballotData.GetBallotById(ballotId);

            if (Ballot == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}