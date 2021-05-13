using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslationApplication.Models;

namespace TranslationApplication.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {

            using var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            await dbContext.Database.EnsureCreatedAsync();
            try
            {
                if (dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (!dbContext.Wordlists.Any())
            {
                var wordList1 = new Wordlist()
                {
                    Title = "Animals1",
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
                    Title = "Animals2",
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
                    Title = "Animals3",
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

                dbContext.Wordlists.Add(wordList1);
                dbContext.Wordlists.Add(wordList2);
                dbContext.Wordlists.Add(wordList3);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
