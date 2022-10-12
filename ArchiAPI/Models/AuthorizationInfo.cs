using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ArchiAPI.Models
{
    public sealed class AuthorizationInfo
    {
        public string Jwt { get; set; }
        public DateTime JwtExpires { get; set; }
        public string Refresh { get; set; }
    }
}
