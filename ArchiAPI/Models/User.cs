using System;
using System.Collections.Generic;
namespace ArchiAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string IconURI { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
