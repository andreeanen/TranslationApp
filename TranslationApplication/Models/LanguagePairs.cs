namespace TranslationApplication.Models
{
    public class LanguagePair
    {
        public int Id { get; set; }
        public string Language1 { get; set; }
        public string Language2 { get; set; }
        public int WordlistId { get; set; }
        public Wordlist Wordlist { get; set; }
    }
}
