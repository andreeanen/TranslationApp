﻿namespace TranslationApplication.Models
{
    public class Word
    {
        public int WordId { get; set; }
        public string Language1 { get; set; }
        public string Word1 { get; set; }
        public string Language2 { get; set; }
        public string Word2 { get; set; }
        public int WordlistId { get; set; }
        public virtual Wordlist Wordlist { get; set; }
    }
}
