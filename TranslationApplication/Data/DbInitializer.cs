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
            //dbContext.Database.EnsureDeleted();

            using var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

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
                var wordList = new Wordlist()
                {

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

                dbContext.Wordlists.Add(wordList);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
