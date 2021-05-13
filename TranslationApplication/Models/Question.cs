namespace TranslationApplication.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Language1 { get; set; }
        public string Word1 { get; set; }
        public string Language2 { get; set; }
        public string CorrectAnswer { get; set; }
        public string Answer { get; set; }
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
