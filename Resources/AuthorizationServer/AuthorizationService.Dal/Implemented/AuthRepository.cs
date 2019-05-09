using AuthorizationService.Dal.Implemented.Base;
using AuthorizationService.Dal.Interfaces;
using AuthorizationService.Domain;
using System;
using System.Linq;

namespace AuthorizationService.Dal.Implemented
{
    public class AuthRepository : RepositoryBase<LogInInfomation>, IAuthRepository
    {
        private LogInDbContext dbContext;

        public AuthRepository(LogInDbContext dbContext) : base(dbContext)
        {
        }

        public LogInInfomation GetLogInInfomationById(int id)
        {
            return dbContext.LoginInfomation.Where(u => u.Id == id).FirstOrDefault();
        }

        public LogInInfomation GetLoginInfomationByName(string name)
        {
            return dbContext.LoginInfomation.Where(u => u.Name == name).FirstOrDefault();
        }

        public bool ValidatePassword(string name, string plainTextPassword)
        {
            return dbContext.LoginInfomation.Where(u => u.Name == name && u.Pwd == plainTextPassword).Any();
        }
    }
}
