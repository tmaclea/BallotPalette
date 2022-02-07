using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BallotPalette.Data;
using BallotPalette.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BallotPalette.Pages
{
    public class BallotsModel : PageModel
    {
        private readonly IBallotData ballotData;
        public IEnumerable<Ballot> Ballots { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public BallotsModel(IBallotData ballotData)
        {
            this.ballotData = ballotData;
        }


        public void OnGet(string SearchTerm)
        {
            Ballots = ballotData.GetBallotsByName(SearchTerm);

        }
    }
}