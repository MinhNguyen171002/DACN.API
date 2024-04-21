using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class TestComplete
    {
        [Key]
        public int TCId { get; set; }
        public int TestID { get; set; }
        public bool? IsCorrect { get; set; }
        [ForeignKey(nameof(TestID))]
        public virtual Test? test { get; set; }

    }
}
