using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteStore.Model
{
    public class RefreshToken
    {
        [BsonId]
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public Guid UserId { get; set; }

        //foreign key
        //public IdentityUser User { get; set; }

    }
}
