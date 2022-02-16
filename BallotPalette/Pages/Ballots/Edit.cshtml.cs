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
        [BindProperty]
        public List<Question> Questions { get; set; }

        [BindProperty]
        public List<Option> Q1_Options { get; set; }
        [BindProperty]
        public List<Option> Q2_Options { get; set; }
        [BindProperty]
        public List<Option> Q3_Options { get; set; }
        [BindProperty]
        public List<Option> Q4_Options { get; set; }
        [BindProperty]
        public List<Option> Q5_Options { get; set; }
        [BindProperty]
        public List<Option> Q6_Options { get; set; }

        public EditModel(IBallotData ballotData, IQuestionData questionData, IOptionData optionData)
        {
            this.ballotData = ballotData;
            this.questionData = questionData;
            this.optionData = optionData;
        }

        public IActionResult OnGet(int? ballotId)
        {

            Questions = Enumerable.Repeat(new Question(), MAX_QUESTIONS).ToList();
            Q1_Options = new List<Option>();
            Q2_Options = new List<Option>();
            Q3_Options = new List<Option>();
            Q4_Options = new List<Option>();
            Q5_Options = new List<Option>();
            Q6_Options = new List<Option>();
            int[] question_Ids = new int[MAX_QUESTIONS];
            //fill question ids with zeros
            Array.Clear(question_Ids, 0, question_Ids.Length);

            if (ballotId.HasValue)
            {
                Ballot = ballotData.GetBallotById(ballotId.Value);

                int x = 0;
                foreach(Question q in questionData.GetQuestionsByBallot(ballotId.Value).ToList())
                {
                    Questions[x] = q;
                    question_Ids[x] = q.Id;
                    switch(x)
                    {
                        case 0:
                            Q1_Options.AddRange(optionData.GetOptionsByQuestion(q.Id));
                            break;
                        case 1:
                            Q2_Options.AddRange(optionData.GetOptionsByQuestion(q.Id));
                            break;
                        case 2:
                            Q3_Options.AddRange(optionData.GetOptionsByQuestion(q.Id));
                            break;
                        case 3:
                            Q4_Options.AddRange(optionData.GetOptionsByQuestion(q.Id));
                            break;
                        case 4:
                            Q5_Options.AddRange(optionData.GetOptionsByQuestion(q.Id));
                            break;
                        case 5:
                            Q6_Options.AddRange(optionData.GetOptionsByQuestion(q.Id));
                            break;
                    }
                    x++;
                };

            } else
            {
                Ballot = new Ballot();
            }

            //Fill option arrays with empty options so that populating them is in-range
            //Should stear away from hard-coding the question IDs like this
            for (int i = 0; i < MAX_OPTIONS; i++)
            {
                if (Q1_Options.ElementAtOrDefault(i) == null)
                {
                    Q1_Options.Add(new Option() { Id = question_Ids[0], Text = string.Empty });
                }
                if (Q2_Options.ElementAtOrDefault(i) == null)
                {
                    Q2_Options.Add(new Option() { Id = question_Ids[1], Text = string.Empty });
                }
                if (Q3_Options.ElementAtOrDefault(i) == null)
                {
                    Q3_Options.Add(new Option() { Id = question_Ids[2], Text = string.Empty });
                }
                if (Q4_Options.ElementAtOrDefault(i) == null)
                {
                    Q4_Options.Add(new Option() { Id = question_Ids[3], Text = string.Empty });
                }
                if (Q5_Options.ElementAtOrDefault(i) == null)
                {
                    Q5_Options.Add(new Option() { Id = question_Ids[4], Text = string.Empty });
                }
                if (Q6_Options.ElementAtOrDefault(i) == null)
                {
                    Q6_Options.Add(new Option() { Id = question_Ids[5], Text = string.Empty });
                }
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

            //add/update/remove questions
            //Remove options for each question entirely
            //then remove the questions entirely
            
            foreach(Question q in questionData.GetQuestionsByBallot(Ballot.Id).ToList())
            {
                optionData.Clear(q.Id);
                questionData.Delete(q);
            }

            //then re-add the questions/options as they appear on the form

            for (int i = 0; i < MAX_OPTIONS; i++)
            {
                if (!string.IsNullOrEmpty(Questions[i].Text))
                {
                    Questions[i].BallotId = Ballot.Id;
                    Question q = questionData.Add(Questions[i]);
                    List<Option> q_options;
                    switch (i)
                    {
                        case 0:
                            q_options = new List<Option>(Q1_Options);
                            break;
                        case 1:
                            q_options = new List<Option>(Q2_Options);
                            break;
                        case 2:
                            q_options = new List<Option>(Q3_Options);
                            break;
                        case 3:
                            q_options = new List<Option>(Q4_Options);
                            break;
                        case 4:
                            q_options = new List<Option>(Q5_Options);
                            break;
                        case 5:
                            q_options = new List<Option>(Q6_Options);
                            break;
                        default:
                            throw new NullReferenceException();
                    }

                    try
                    {
                        foreach (Option o in q_options)
                        {
                            o.QuestionId = q.Id;
                            optionData.Add(o);
                        }
                    }catch (NullReferenceException e)
                    {
                        Console.WriteLine("q_options not set");
                        Console.WriteLine(e.Message);
                    }
                }
            }

            Ballot = ballotData.Update(Ballot);
            ballotData.Commit();
            TempData["Message"] = "Ballot saved";
            return RedirectToPage("./Detail", new { ballotId = Ballot.Id }); 
        }
    }
}