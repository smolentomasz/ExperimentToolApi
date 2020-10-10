using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface IUserRepository
    {
        bool FindByEmail(string email);
        //UserToken GetUserTokenByEmail(string username, string password);
        User GetUserByEmail(string email);
    }
}