using EmailVerification.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailVerification.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<Registeration> Registerations { get; set; }
    }
}
