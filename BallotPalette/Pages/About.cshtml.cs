using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BallotPalette.Data;
using BallotPalette.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BallotPalette.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IUserData userData;

        public string Message { get; set; }
        public IEnumerable<User> Users { get; set; }

        public AboutModel(IConfiguration config, IUserData userData)
        {
            this.config = config;
            this.userData = userData;
        }

        public void OnGet()
        {
            Message = config["Message"];
            Users = userData.GetUsersByName(null);
        }
    }
}