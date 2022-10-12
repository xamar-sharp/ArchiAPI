using System;
using System.ComponentModel.DataAnnotations;
namespace ArchiAPI.Models
{
    public class StoreDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(1024*1024)]
        public string Description { get; set; }
        [Required]
        [MaxLength(1024*1024*8)]
        public string Code { get; set; }
        public byte[] IconData { get; set; }
        [Required]
        [MaxLength(9)]
        public string Type { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
    public enum PatternType : byte
    {
        Principle, Struct, Creative, Behavior, Custom
    }
}
