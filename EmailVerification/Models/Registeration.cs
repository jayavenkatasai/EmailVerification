using System.ComponentModel.DataAnnotations;

namespace EmailVerification.Models
{
    public class Registeration
    {
        [Key]
        [Required]
        public int EmployeeID { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public bool IsVerified { get; set; }

        public string ?VerificationToken { get; set; }
    }
}
