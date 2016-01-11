using System;
using System.Collections.Generic;

namespace AccessManagement.Repository
{
    public interface IUserRepository
    {
        KeyValuePair<int, string> IsValidUser(string email, string password);
        KeyValuePair<bool, string> IsManager(int employeeId);
    }
}
