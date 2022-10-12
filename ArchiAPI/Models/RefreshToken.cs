using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ArchiAPI.Models
{
    public sealed class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        //3guid
        [MaxLength(96)]
        public string Value { get; set; }
        public bool IsAlive { get; set; }
        public User User { get; set; }
    }
}
