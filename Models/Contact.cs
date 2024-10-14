using System.ComponentModel.DataAnnotations;

namespace Contact_Management.Models
{
    public class Contact
    {
        [Key]
        
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        
        [EmailAddress]
        public string Email { get; set; }
    }
}
