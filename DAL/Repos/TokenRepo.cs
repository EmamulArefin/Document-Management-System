using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class TokenRepo : Repo, IRepo<Token, string, Token>
    {
        public Token Create(Token obj)
        {
            var existingToken = db.Tokens.FirstOrDefault(t => t.User_Id == obj.User_Id);

            if (existingToken != null)
            {
                // Update existing token
                existingToken.Token_Key = obj.Token_Key;
                existingToken.Created_At = obj.Created_At;
                existingToken.Expire_At = null;
            }
            else
            {
                // Insert new token
                db.Tokens.Add(obj);
            }

            db.SaveChanges();
            return obj;
        }


        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Token> Get()
        {
            throw new NotImplementedException();
        }

        public Token Get(string id)
        {
            return db.Tokens.FirstOrDefault(t => t.Token_Key.Equals(id));
        }

        public Token Update(Token obj)
        {
            var existingToken = db.Tokens.FirstOrDefault(t => t.Token_Key == obj.Token_Key);

            if (existingToken != null)
            {
                existingToken.Expire_At = obj.Expire_At;
            }
            else
            {
                db.Tokens.Add(obj);
            }

            db.SaveChanges();
            return existingToken ?? obj;
        }

    }
}
