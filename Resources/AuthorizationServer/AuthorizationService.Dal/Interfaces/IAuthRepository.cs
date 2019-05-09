using AuthorizationService.Dal.Base;
using AuthorizationService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationService.Dal.Interfaces
{
    public interface IAuthRepository : IRepositoryBase<LogInInfomation>
    {
        LogInInfomation GetLogInInfomationById(int id);
        LogInInfomation GetLoginInfomationByName(string username);
        bool ValidatePassword(string username, string plainTextPassword);
    }
}

