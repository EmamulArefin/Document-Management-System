using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF;
using System;

namespace BLL.Services
{
    public class AuthService
    {
        private static readonly IMapper mapper;

        static AuthService()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Token, TokenDTO>();
      
            });
           
            mapper = config.CreateMapper();
        }


        public static TokenDTO Auth(string email, string pass)
        {
            var user = DataAccess.AuthData().Authenticate(email, pass);
            if (user != null)
            {
                Token tk = new Token();
                tk.Token_Key = Guid.NewGuid().ToString();
                tk.Created_At = DateTime.Now;
                tk.Expire_At = null;
                tk.User_Id = user.Id;

                var token = DataAccess.TokenData().Create(tk);
                return mapper.Map<TokenDTO>(token);
            }
            return null;
        }

        public static bool IsTokenValid(string key)
        {
            var token = DataAccess.TokenData().Get(key);
            return token != null && token.Expire_At == null;
        }

        public static int? GetUserIdFromToken(string tokenKey)
        {
            var token = DataAccess.TokenData().Get(tokenKey);
            if (token != null && token.Expire_At == null)
            {
                return token.User_Id; 
            }
            return null;
        }


        public static TokenDTO Logout(string key)
        {
            var token = DataAccess.TokenData().Get(key);
            if (token != null)
            {
                token.Expire_At = DateTime.Now.AddMinutes(-2); 
                var rettk = DataAccess.TokenData().Update(token);
                return mapper.Map<TokenDTO>(rettk);
            }
            return null;
        }
    }
}