using AuthorizationService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Dal
{
    public class LogInDbContext : DbContext
    {
        public LogInDbContext(DbContextOptions<LogInDbContext> options) : base(options) { }

        public DbSet<LogInInfomation> LoginInfomation;
    }
}
