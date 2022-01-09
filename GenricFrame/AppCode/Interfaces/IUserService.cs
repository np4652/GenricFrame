using GenricFrame.Models;
using System.Collections.Generic;

namespace GenricFrame.AppCode.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(LoginRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
