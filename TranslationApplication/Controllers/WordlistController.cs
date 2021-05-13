using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TranslationApplication.Data;
using TranslationApplication.Models;

namespace TranslationApplication.Controllers
{
    public class WordlistController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public WordlistController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Wordlist()
        {
            return View();
        }

        public IActionResult ShowWordlist()
        {
            var wordLists = _dbContext.Wordlists
                .Include(x => x.LanguagePair)
                .Include(x => x.Words)
                .ToList();

            return View("ShowWordlist", wordLists);
        }

        public IActionResult CreateWordlist()
        {
            return View("CreateWordlist");
        }


        [HttpPost]
        public IActionResult CreateWordlist(Wordlist wordlist)
        {
            var newWordlist = new Wordlist()
            {
                Title = wordlist.Title,
                LanguagePair = new LanguagePair()
                {
                    Language1 = wordlist.LanguagePair.Language1,
                    Language2 = wordlist.LanguagePair.Language2
                }
            };

            _dbContext.Wordlists.Add(newWordlist);
            _dbContext.SaveChanges();

            return View("CompleteWordlist", newWordlist);
        }

        [HttpGet]
        public IActionResult CompleteWordlist(int id)
        {

            var wordlist = _dbContext.Wordlists
              .Include(x => x.LanguagePair)
              .Include(x => x.Words)
              .Where(x => x.Id == id)
              .FirstOrDefault();

            if (wordlist == null)
            {
                return NotFound("No wordlist with this id was found");
            }

            return View("CompleteWordlist", wordlist);
        }

        [HttpGet]
        public IActionResult AddWordToList(int id)
        {
            var wordlist = _dbContext.Wordlists
              .Include(x => x.LanguagePair)
              .Include(x => x.Words)
              .Where(x => x.Id == id)
              .FirstOrDefault();

            if (wordlist == null)
            {
                return NotFound("No wordlist with this id was found.");
            }
            var word = new Word()
            {
                WordlistId = id,
                Language1 = wordlist.LanguagePair.Language1,
                Language2 = wordlist.LanguagePair.Language2
            };

            return View("AddWordToList", word);
        }

        [HttpPost]
        public IActionResult AddWordToList(Word word)
        {
            var wordlistId = word.WordlistId;

            var wordlist = _dbContext.Wordlists
                          .Include(x => x.LanguagePair)
                          .Include(x => x.Words)
                          .Where(x => x.Id == wordlistId)
                          .FirstOrDefault();
            if (wordlist == null)
            {
                return NotFound("No wordlist with this id was found");
            }
            if (word != null)
            {
                word.Language1 = wordlist.LanguagePair.Language1;
                word.Language2 = wordlist.LanguagePair.Language2;
                wordlist.Words.Add(word);
                _dbContext.Update(wordlist);
                _dbContext.SaveChanges();
            }

            return View("CompleteWordlist", wordlist);
        }


    }
}
