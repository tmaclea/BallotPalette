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
        private const int MAX_QUESTIONS = 6;
        private const int MAX_OPTIONS = 6;

        [BindProperty]
        public Ballot Ballot { get; set; }
       
        public List<Question> Questions { get; set; }

        [BindProperty]
        public List<string> Questions_text { get; set; }
        [BindProperty]
        public List<string> Q1_Options { get; set; }
        [BindProperty]
        public List<string> Q2_Options { get; set; }
        [BindProperty]
        public List<string> Q3_Options { get; set; }
        [BindProperty]
        public List<string> Q4_Options { get; set; }
        [BindProperty]
        public List<string> Q5_Options { get; set; }
        [BindProperty]
        public List<string> Q6_Options { get; set; }

        public EditModel(IBallotData ballotData, IQuestionData questionData, IOptionData optionData)
        {
            this.ballotData = ballotData;
            this.questionData = questionData;
            this.optionData = optionData;
        }

        public IActionResult OnGet(int? ballotId)
        {
            Questions_text = new List<string>();
            Q1_Options = new List<string>();
            Q2_Options = new List<string>();
            Q3_Options = new List<string>();
            Q4_Options = new List<string>();
            Q5_Options = new List<string>();
            Q6_Options = new List<string>();

            if (ballotId.HasValue)
            {
                Ballot = ballotData.GetBallotById(ballotId.Value);
                Questions = questionData.GetQuestionsByBallot(ballotId.Value).ToList();

                int x = 0;
                foreach(Question q in Questions)
                {
                    Questions_text.Add(q.Text);
                    List<Option> tempOptions = optionData.GetOptionsByQuestion(q.Id).ToList();
                    List<string> optionsText = new List<string>();
                    tempOptions.ForEach(o => optionsText.Add(o.Text));

                    switch(x)
                    {
                        case 0:
                            Q1_Options = optionsText;
                            break;
                        case 1:
                            Q2_Options = optionsText;
                            break;
                        case 2:
                            Q3_Options = optionsText;
                            break;
                        case 3:
                            Q4_Options = optionsText;
                            break;
                        case 4:
                            Q5_Options = optionsText;
                            break;
                        case 5:
                            Q6_Options = optionsText;
                            break;
                    }
                    x++;
                };

            } else
            {
                Ballot = new Ballot();
            }

            //ensure the capacity of the bound lists so that they're in range to display
            List<string> filler = Enumerable.Repeat(string.Empty, MAX_OPTIONS).ToList();
            Questions_text.AddRange(filler);
            Q1_Options.AddRange(filler);
            Q2_Options.AddRange(filler);
            Q3_Options.AddRange(filler);
            Q4_Options.AddRange(filler);
            Q5_Options.AddRange(filler);
            Q6_Options.AddRange(filler);

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
                Ballot = ballotData.Update(Ballot);
            }
            else
            {
                Ballot = ballotData.Add(Ballot);
            }

            Questions = questionData.GetQuestionsByBallot(Ballot.Id).ToList();

            //add/update/remove questions
            //Remove options for each question entirely
            //then remove the questions entirely
            
            foreach (Question q in Questions)
            {
                optionData.Clear(q.Id);
                questionData.Delete(q);
            }

            //then re-add the questions/options as they appear on the form

            for (int i = 0; i < MAX_QUESTIONS; i++)
            {
                if(string.IsNullOrEmpty(Questions_text[i]))
                {
                    continue;
                }

                Question newQuestion = questionData.Add(new Question() { Text = Questions_text[i], BallotId = Ballot.Id });
                List<string> newOptions = new List<string>();

                switch (i)
                {
                    case 0:
                        newOptions = Q1_Options;
                        break;
                    case 1:
                        newOptions = Q2_Options;
                        break;
                    case 2:
                        newOptions = Q3_Options;
                        break;
                    case 3:
                        newOptions = Q4_Options;
                        break;
                    case 4:
                        newOptions = Q5_Options;
                        break;
                    case 5:
                        newOptions = Q6_Options;
                        break;
                }

                foreach(string o in newOptions)
                {
                    if(!string.IsNullOrEmpty(o))
                    {
                        optionData.Add(new Option() { Text = o, QuestionId = newQuestion.Id });
                    }
                }
            }

            ballotData.Commit();
            questionData.Commit();
            optionData.Commit();
            TempData["Message"] = "Ballot saved";
            return RedirectToPage("./List"); 
        }
    }
}