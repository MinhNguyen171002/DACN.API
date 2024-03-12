using System.ComponentModel.DataAnnotations;

namespace API.Enity
{
    public class User
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string SDT { get; set; }
        public string Email {  get; set; }
                
    }
}
