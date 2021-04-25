using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TranslationApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual IList<Wordlist> Wordlists { get; set; }
        public virtual IList<Quiz> Quizzes { get; set; }
    }
}
