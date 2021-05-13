using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslationApplication.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public virtual IList<Question> Questions { get; set; }
        public int Score { get; set; }

        public Quiz()
        {
            Questions = new List<Question>();
        }
    }

}
