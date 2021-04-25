using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TranslationApplication.Models;

namespace TranslationApplication.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            var wordLists = new List<Wordlist>();
            var wordList1 = new Wordlist()
            {
                Id = 2,
                LanguagePair = new LanguagePair
                {
                    Language1 = "Romanian",
                    Language2 = "English"
                },
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
            var wordList2 = new Wordlist()
            {
                Id = 2,
                LanguagePair = new LanguagePair
                {
                    Language1 = "Swedish",
                    Language2 = "English"
                },

                Words = new List<Word>()
                {
                    new Word
                    {
                        Language1 = "Swedish",
                        Language2 = "English",
                        Word1 = "Katt",
                        Word2 = "Cat"
                    },
                    new Word
                    {
                        Language1 = "Swedish",
                        Language2 = "English",
                        Word1 = "Hund",
                        Word2 = "Dog"
                    }
                }
            };
            wordLists.Add(wordList1);
            wordLists.Add(wordList2);

            var languagePairs = wordLists.Select(x => new LanguagePair { Language1 = x.LanguagePair.Language1, Language2 = x.LanguagePair.Language2 }).Distinct().ToList();


            return View(languagePairs);
        }

        [HttpPost]
        public IActionResult ValidateQuiz(int? id, Quiz quiz)
        {
            var gg = quiz;
            return View();
        }

        [HttpPost]
        public IActionResult GenerateQuiz(string languagePair)
        {
            var wordLists = new List<Wordlist>();
            var wordList1 = new Wordlist()
            {
                Id = 2,
                LanguagePair = new LanguagePair
                {
                    Language1 = "Romanian",
                    Language2 = "English"
                },

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
            var wordList2 = new Wordlist()
            {
                Id = 2,
                LanguagePair = new LanguagePair
                {
                    Language1 = "Swedish",
                    Language2 = "English"
                },

                Words = new List<Word>()
                {
                    new Word
                    {
                        Language1 = "Swedish",
                        Language2 = "English",
                        Word1 = "Katt",
                        Word2 = "Cat"
                    },
                    new Word
                    {
                        Language1 = "Swedish",
                        Language2 = "English",
                        Word1 = "Hund",
                        Word2 = "Dog"
                    }
                }
            };

            var wordList3 = new Wordlist()
            {
                Id = 2,
                LanguagePair = new LanguagePair
                {
                    Language1 = "English",
                    Language2 = "Romanian"
                },

                Words = new List<Word>()
                {
                    new Word
                    {
                        Language1 = "English",
                        Language2 = "Romanian",
                        Word1 = "Horse",
                        Word2 = "Cal"
                    },
                    new Word
                    {
                        Language1 = "English",
                        Language2 = "Romanian",
                        Word1 = "Pig",
                        Word2 = "Porc"
                    }
                }
            };
            wordLists.Add(wordList1);
            wordLists.Add(wordList2);
            wordLists.Add(wordList3);


            List<string> languages = languagePair.Split('&').ToList();
            string firstLanguage = languages[0];
            string secondLanguage = languages[1];

            var allWords = wordLists.Where(x => x.LanguagePair.Language1 == firstLanguage && x.LanguagePair.Language2 == secondLanguage ||
                                            x.LanguagePair.Language2 == firstLanguage && x.LanguagePair.Language1 == secondLanguage).Select(x => x.Words).ToList();

            var words = new List<Word>();

            var wordsToShow = new List<Word>();

            foreach (var item in allWords)
            {
                words.AddRange(item);
            }

            foreach (var word in words)
            {
                if (word.Language1 == firstLanguage && word.Language2 == secondLanguage)
                {
                    wordsToShow.Add(word);

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

                    wordsToShow.Add(wordReverse);
                }
            }

            var quizCorrect = new Quiz() { Id = 1 };
            //quizCorrect.Questions.ToList().AddRange(wordsToShow);

            var quiz = new Quiz() { Id = 2 };
            foreach (var item in wordsToShow)
            {
                var zz = new Question()
                {
                    Language1 = item.Language1,
                    Word1 = item.Word1,
                    Language2 = item.Language2,
                    Word2 = ""
                };
                quiz.Questions.Add(zz);
            }



            return View("ShowQuiz", quiz);
        }
    }
}
