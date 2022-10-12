using System;
using System.ComponentModel.DataAnnotations;
namespace ArchiAPI.Models
{
    public sealed class MessageDTO
    {
        [StringLength(5000,MinimumLength =1)]
        public string Text { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string UserIconUri { get; set; }
    }
}
