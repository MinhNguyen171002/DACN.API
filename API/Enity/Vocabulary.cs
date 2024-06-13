using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace API.Enity
{
    public class Vocabulary
    {
        [Key]
        public string? VocabularyId { get; set; }
        public string? vocabulary {  get; set; }
        public string? Description { get; set; }
        public virtual Topic? topic { get; set; }
        public virtual User? user { get; set; }

    }
}
