using System.ComponentModel.DataAnnotations;

namespace TODO.Models
{
    public class TODOUser
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
