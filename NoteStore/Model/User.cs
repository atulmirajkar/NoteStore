using System;
using AspNetCore.Identity.MongoDbCore.Models;

namespace NoteStore.Model   
{
    public class User:MongoIdentityUser<Guid>
    {
        public User() :base()
        {
        }

        public User(string userName, string email): base(userName,email)
        {
            
        }
    }
}