using DAL.EF;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {
        public static IRepo<Token, string, Token> TokenData()
        {
            return new TokenRepo();
        }
        public static IRepo<User, string, User> UserData()
        {
            return new UserRepo();
        }
        public static IAuth AuthData()
        {
            return new UserRepo();
        }
        public static IRepo<File_Info, int, File_Info> FileManage()
        {
            return new FileRepo();
        }
        public static IRepo<Tag, int, Tag> TagData()
        {
            return new TagRepo();
        }

    }
}
