using System;
namespace ArchiAPI.Services
{
    public sealed class RefreshGenerator:IRefreshBuilder
    {
        public string Generate()
        {
            return $"{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}";
        }
    }
}
