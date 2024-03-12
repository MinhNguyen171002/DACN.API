using System.ComponentModel.DataAnnotations;

namespace API.Enity
{
    public class Level
    {
        [Key]
        public string LevelID { get; set; }
        public string LevelName { get; set; }
    }
}
