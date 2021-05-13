using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TranslationApplication.Data;
using TranslationApplication.Models;

namespace TranslationApplication.Controllers
{
    public class QuizController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public QuizController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var wordLists = _dbContext.Wordlists
                .Include(x => x.LanguagePair)
                .Include(x => x.Words)
                .ToList();
            var languagePairs = wordLists.Select(x => new LanguagePair { Language1 = x.LanguagePair.Language1, Language2 = x.LanguagePair.Language2 }).Distinct().ToList();
            return View(languagePairs);
        }

        [HttpPost]
        public IActionResult ValidateQuiz(int id, Quiz quizWithAnswers)
        {

            var quiz = _dbContext.Quizzes
                .Where(x => x.Id == id)
                .Include(x => x.Questions)
                .FirstOrDefault();
            if (quiz == null)
            {
                return NotFound($"There was no quiz found with this id: {id}.");
            }

            for (int i = 0; i < quizWithAnswers.Questions.Count; i++)
            {
                for (int j = 0; j < quiz.Questions.Count; j++)
                {
                    if (i == j)
                    {
                        quiz.Questions[j].Answer = quizWithAnswers.Questions[i].Answer;
                    }
                }
            }

            foreach (var question in quiz.Questions)
            {
                if (question.Answer.ToLower().Trim() == question.CorrectAnswer.ToLower().Trim())
                {
                    quiz.Score++;
                }
            }
            _dbContext.Quizzes.Update(quiz);
            _dbContext.SaveChanges();

            return View("QuizResult", quiz);
        }

        [HttpPost]
        public IActionResult GenerateQuiz(string languagePair)
        {
            var wordLists = _dbContext.Wordlists
                .Include(x => x.LanguagePair)
                .Include(x => x.Words)
                .ToList();
            var words = new List<Word>();
            var wordsForQuiz = new List<Word>();

            List<string> languages = languagePair.Split('&').ToList();
            string firstLanguage = languages[0];
            string secondLanguage = languages[1];

            var listsOfWords = wordLists.Where(x => x.LanguagePair.Language1 == firstLanguage && x.LanguagePair.Language2 == secondLanguage ||
                                            x.LanguagePair.Language2 == firstLanguage && x.LanguagePair.Language1 == secondLanguage)
                                    .Select(x => x.Words)
                                    .ToList();

            foreach (var list in listsOfWords)
            {
                words.AddRange(list);
            }

            foreach (var word in words)
            {
                if (word.Language1 == firstLanguage && word.Language2 == secondLanguage)
                {
                    wordsForQuiz.Add(word);
                }

                if (word.Language2 == firstLanguage && word.Language1 == secondLanguage)
                {
                    var wordReverse = new Word
                    {
                        Language1 = word.Language2,
                        Word1 = word.Word2,
                        Language2 = word.Language1,
                        Word2 = word.Word1
                    };

                    wordsForQuiz.Add(wordReverse);
                }
            }

            var quiz = new Quiz();

            foreach (var word in wordsForQuiz.Distinct())
            {
                var question = new Question()
                {
                    Language1 = word.Language1,
                    Word1 = word.Word1,
                    Language2 = word.Language2,
                    CorrectAnswer = word.Word2,
                    Answer = ""
                };
                quiz.Questions.Add(question);
            }

            _dbContext.Quizzes.Add(quiz);
            _dbContext.SaveChanges();

            return View("ShowQuiz", quiz);
        }
    }
}
