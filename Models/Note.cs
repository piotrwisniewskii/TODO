using System.ComponentModel.DataAnnotations;
using TODO.Data.Enums;

namespace TODO.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public string PictureURL { get; set; }

        [Required(ErrorMessage = "Please provide Task Name")]
        [StringLength(15, MinimumLength = 4)]
        public string NoteName { get; set; }

        [Required(ErrorMessage = "Please provide Task Message")]
        [StringLength(200, MinimumLength = 4)]
        public string NoteMessage { get; set; }

        [Required(ErrorMessage = "Please provide Task Date")]
        public DateTime NoteDate { get; set; }


        public Priority Priority { get; set; }
    }
}
