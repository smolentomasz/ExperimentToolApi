using System;
using System.ComponentModel.Design.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using ExperimentToolApi.Secure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExperimentToolApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ExperimentToolDbContext _context;
        private readonly IOptions<JwtSettings> config;

        public UserRepository(ExperimentToolDbContext context, IOptions<JwtSettings> config)
        {
            _context = context;
            this.config = config;
        }
        public bool FindByUsername(string username)
        {
            if (_context.Users.ToList().Any(user => user.Username.Equals(username)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public LoginResponse GetRefreshedUserToken(string token)
        {
            TokenManager tokenManager = new TokenManager(config);
            string username = tokenManager.decodeUsername(token);

            if (FindByUsername(username))
            {
                User user = GetUserByUsername(username);
                LoginResponse refreshedLoginResponse = GetUserToken(user);
                return refreshedLoginResponse;
            }
            else
            {
                return null;
            }
        }

        public string getRefreshToken(string token)
        {
            TokenManager tokenManager = new TokenManager(config);
            string username = tokenManager.decodeUsername(token);

            User user = GetUserByUsername(username);

            return user.RefreshToken;

        }

        public User GetUserByUsername(string username)
        {
            if (FindByUsername(username))
            {
                return _context.Users.Where(user => user.Username.Equals(username)).Single();
            }
            else
            {
                return null;
            }
        }

        public LoginResponse GetUserToken(User user)
        {
            TokenManager tokenManager = new TokenManager(config);

            LoginResponse newLoginResponse = new LoginResponse(user);
            newLoginResponse.Token = tokenManager.GenerateToken(user);
            newLoginResponse.RefreshToken = tokenManager.RefreshToken();

            user.RefreshToken = newLoginResponse.RefreshToken;

            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return newLoginResponse;
        }
    }
}