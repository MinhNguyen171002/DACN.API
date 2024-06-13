using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class User
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string SDT { get; set; }
        public string Email {  get; set; }
        [ForeignKey(nameof(UserID))]
        public IdentityUser user { get; set; }
        public virtual ICollection<SentenceComplete> sencoms {  get; set; }
        public virtual ICollection<QuestionComplete> quescoms { get; set; }
        public virtual ICollection<Vocabulary> vocabularies { get; set; }
        public virtual ICollection<Topic> topic { get; set; }

    }
}
