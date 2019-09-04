using System;
using AspNetCore.Identity.MongoDbCore.Models;

namespace NoteStore.Model
{
    public class Role: MongoIdentityRole<Guid>
    {
        public Role() : base()
        {
        }   

        public Role(string roleName): base(roleName){
            
        }
    }
}