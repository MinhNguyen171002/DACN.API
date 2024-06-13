using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Enity
{
    public class Topic
    {
        [Key]
        public string? TopicId { get; set; }
        public string? TopicName { get; set; }
        public virtual User? user { get; set; }
        public virtual ICollection<Vocabulary>? vocabularies { get; set; }

    }
}
