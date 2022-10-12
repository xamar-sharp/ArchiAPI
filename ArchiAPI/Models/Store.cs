using System;
namespace ArchiAPI.Models
{
    public sealed class Store
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string IconURI { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}
