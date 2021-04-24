using System.Collections.Generic;

namespace TranslationApplication.Models
{
    public class Wordlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language1 { get; set; }
        public string Language2 { get; set; }

        public List<Word> Words { get; set; }
    }
}
