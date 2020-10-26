using System.Security.Claims;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface IUserRepository
    {
        bool FindByUsername(string username);
        User GetUserByUsername(string username);
        LoginResponse GetUserToken(User user);
        string getRefreshToken(string token);
        LoginResponse GetRefreshedUserToken(string token);
    }
}