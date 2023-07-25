using System.ComponentModel.DataAnnotations;

namespace EmailVerification
{
    public class RegisterDto
    {


            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string UserName { get; set; } = string.Empty;

            [Required]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
            public string Password { get; set; }

}
}
