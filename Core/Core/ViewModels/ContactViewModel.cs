using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5,ErrorMessage ="Length of Name must than five")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Too long")]
        public string Message { get; set; }
    }
}
