using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class UserRepo : Repo, IRepo<User, string, User>, IAuth
    {
        public User Authenticate(string email, string pass)
        {
            var user = (from u in db.Users
                        where u.Email.Equals(email) &&
                        u.Pass.Equals(pass)
                        select u).SingleOrDefault();
            return user;
        }

        public User Create(User obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<User> Get()
        {
            throw new NotImplementedException();
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public User Update(User obj)
        {
            throw new NotImplementedException();
        }

    }
}
