using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace ArchiAPI.Models
{
    public sealed class UserProfile
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public byte[] IconData { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
