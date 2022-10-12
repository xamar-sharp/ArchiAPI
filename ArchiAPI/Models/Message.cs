using System;
namespace ArchiAPI.Models
{
    public sealed class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
