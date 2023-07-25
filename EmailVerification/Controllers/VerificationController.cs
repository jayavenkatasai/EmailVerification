using EmailVerification.Data;
using EmailVerification.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EmailVerification.Services.emailservice;

namespace EmailVerification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public VerificationController(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
           /* var ue = await _context.Registerations.AnyAsync(r => r.Email == registerDto.Email);
            if (ue != null)
            {
                return BadRequest("Email already taken");
            }*/
           // var verificationToken = Guid.NewGuid().ToString();
            var verificationToken = GenerateRandomString();
            var newUser = new Registeration
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                VerificationToken = verificationToken,
                IsVerified = false // Set to false initially
            };
            _context.Registerations.Add(newUser);
            await _context.SaveChangesAsync();
            await _emailService.SendVerificationEmailAsync(registerDto.Email, verificationToken);

            // Return a success message to the user
            return Ok("Registration successful. Please check your email for verification.");
        }

        [HttpPost("verify")]
        public async Task<ActionResult> Verify(string email, string token)
        {
            // Find the user with the provided email and verification token
            var user = await _context.Registerations.FirstOrDefaultAsync(r => r.Email == email && r.VerificationToken == token);

            if (user == null)
            {
                return BadRequest("Invalid verification token or email");
            }

            // Mark the user as verified in the database
            user.IsVerified = true;
            user.VerificationToken = null;
            _context.Update(user);
            await _context.SaveChangesAsync();

            // You can return a success message or redirect the user to a success page after verification.
            return Ok("Email verification successful.");
        }
        public static string GenerateRandomString()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int length = 6;

            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
