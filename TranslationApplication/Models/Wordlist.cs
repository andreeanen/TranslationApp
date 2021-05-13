using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslationApplication.Models
{
    public class Wordlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public LanguagePair LanguagePair { get; set; }
        public virtual IList<Word> Words { get; set; }

        public Wordlist()
        {
            Words = new List<Word>();
        }
    }
}
