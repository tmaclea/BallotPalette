using BallotPalette.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallotPalette.Data
{
    public interface IUserData
    {
        IEnumerable<User> GetUsersByName(string name);
        IEnumerable<User> SignIn(string username, string password);
    }

    public class InMemoryUserData : IUserData
    {
        List<User> users;

        public InMemoryUserData()
        {
            users = new List<User>()
            {
                new User { Id = 1, Username = "Admin", Password = "password", TeamId = 0 },
                new User { Id = 2, Username = "TestUser1", Password = "password", TeamId = 1 },
                new User { Id = 3, Username = "TestUser2", Password = "password", TeamId = 1 },
                new User { Id = 4, Username = "TestUser3", Password = "password", TeamId = 2 },
            };
        }

        public IEnumerable<User> GetUsersByName(string name = null)
        {
            return from u in users
                   where string.IsNullOrEmpty(name) || u.Username.Contains(name)
                   orderby u.Username
                   select u;
        }

        public IEnumerable<User> SignIn(string username, string password)
        {
            return from u in users
                   where u.Username == username && u.Password == password
                   select u;
        }
    }
}
