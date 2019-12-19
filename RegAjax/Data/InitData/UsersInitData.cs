using System.Collections.Generic;
using RegAjax.Data.Entities;
using RegAjax.Helpers;

namespace RegAjax.Data.InitData
{
    public static class UsersInitData
    {
        public static IEnumerable<User> Get()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "admin",
                    PasswordHash = HashHelper.GenerateHash("admin") 
                }
            };
        }
    }
}