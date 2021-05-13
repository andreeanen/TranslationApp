using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TranslationApplication.Data;
using TranslationApplication.Models;

namespace TranslationApplication.Controllers
{
    public class WordController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public WordController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //// GET: WordController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: WordController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: WordController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: WordController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: WordController/Edit/5
        public ActionResult Edit(int id)
        {
            var word = _dbContext.Words.Where(x => x.WordId == id).FirstOrDefault();
            if (word == null)
            {
                return NotFound($"There is no word found with this id:{id}");
            }

            return View("EditWord", word);
        }

        // POST: WordController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Word newWord)
        {
            var word = _dbContext.Words.Where(x => x.WordId == id).FirstOrDefault();
            word.Word1 = newWord.Word1;
            word.Word2 = newWord.Word2;
            _dbContext.Words.Update(word);
            _dbContext.SaveChanges();

            var wordlistId = word.WordlistId;

            var wordlist = _dbContext.Wordlists
                          .Include(x => x.LanguagePair)
                          .Include(x => x.Words)
                          .Where(x => x.Id == wordlistId)
                          .FirstOrDefault();

            return View("~/Views/Wordlist/CompleteWordlist.cshtml", wordlist);
        }


        public ActionResult Delete(int id)
        {
            var word = _dbContext.Words.Where(x => x.WordId == id).FirstOrDefault();

            var wordlistId = word.WordlistId;

            _dbContext.Words.Remove(word);
            _dbContext.SaveChanges();

            var wordlist = _dbContext.Wordlists
                          .Include(x => x.LanguagePair)
                          .Include(x => x.Words)
                          .Where(x => x.Id == wordlistId)
                          .FirstOrDefault();

            return View("~/Views/Wordlist/CompleteWordlist.cshtml", wordlist);
        }

        //// POST: WordController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
