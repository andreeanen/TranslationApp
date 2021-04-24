using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TranslationApplication.Models;

namespace TranslationApplication.Controllers
{
    public class WordlistController : Controller
    {

        public IActionResult Wordlist()
        {

            return View();
        }

        public IActionResult ShowWordlist()
        {
            var wordlist = new Wordlist()
            {
                Title = "Animals"
            };
            return View("ShowWordlist", wordlist);
        }

        public IActionResult CreateWordlist()
        {
            return View("CreateWordlist");
        }


        [HttpPost]
        public IActionResult CreateWordlist(Wordlist wordlist)
        {
            return View("ShowWordlist", wordlist);
        }


        public IActionResult AddWordToList()
        {
            var wordlist = new Wordlist()
            {
                Title = "Animals",

            };
            return View("AddWordToList", wordlist);
        }

        [HttpPost]
        public IActionResult AddWordToList(int id, Word word)
        {
            var wordList = new Wordlist()
            {
                Id = 2,
                Language1 = "Romanian",
                Language2 = "English",
                Words = new List<Word>()
                {
                    new Word
                    {
                        Language1 = "Romanian",
                        Language2 = "English",
                        Word1 = "Pisica",
                        Word2 = "Cat"
                    },
                    new Word
                    {
                        Language1 = "Romanian",
                        Language2 = "English",
                        Word1 = "Caine",
                        Word2 = "Dog"
                    }
                }
            };
            return View("CompleteWordlist", wordList);
        }
    }
}
