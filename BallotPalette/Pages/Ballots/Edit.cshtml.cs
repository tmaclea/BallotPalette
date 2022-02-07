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

        [BindProperty]
        public Ballot Ballot { get; set; }

        public EditModel(IBallotData ballotData)
        {
            this.ballotData = ballotData;
        }


        public IActionResult OnGet(int? ballotId)
        {

            if(ballotId.HasValue)
            {
                Ballot = ballotData.GetBallotById(ballotId.Value);
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
    }
}