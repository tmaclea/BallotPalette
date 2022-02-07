using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BallotPalette.Core;
using BallotPalette.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BallotPalette.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserData userData;
        private readonly ITeamData teamData;

        public string VerifyPassword { get; set; }
        public User user { get; set; }
        [BindProperty]
        public string Team { get; set; }
        public List<string> Teams { get; set; }

        public RegisterModel(IUserData userData,
                            ITeamData teamData)
        {
            this.userData = userData;
            this.teamData = teamData;
            VerifyPassword = string.Empty;
        }

        public void OnGet()
        {
            user = new User();
            Teams = new List<string>();
            IEnumerable<Team> teamsList = teamData.GetTeamsByName(null);
            foreach (Team t in teamsList)
            {
                Teams.Add(t.Name);
            }
        }
    }
}