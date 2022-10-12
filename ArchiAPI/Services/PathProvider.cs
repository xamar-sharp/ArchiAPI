using System.IO;
using System;
namespace ArchiAPI.Services
{
    public sealed class PathProvider : IPathProvider
    {
        public string BasePath { get; }
        public PathProvider()
        {
            BasePath = "C:\\0\\ArchiAPI";
        }
        public byte[] Read(string rel)
        {
            return File.ReadAllBytes(Path.Combine(BasePath, rel));
        }
        public string Add(string userName,byte[] data,bool isIcon)
        {
            var userPath = Path.Combine(BasePath, userName);
            try
            {
                if (!Directory.Exists(userPath))
                {
                    Directory.CreateDirectory(userPath);
                }
                var fileName= isIcon ? $"Icon_{Guid.NewGuid()}.jpg" : $"Store_{Guid.NewGuid()}.jpg";
                var forWrite = Path.Combine(userPath, fileName);
                var result = Path.Combine(userPath, fileName);
                File.WriteAllBytes(forWrite, data);
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public bool Remove(string rel)
        {
            try
            {
                File.Delete(Path.Combine(BasePath, rel));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
