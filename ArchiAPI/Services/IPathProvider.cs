namespace ArchiAPI.Services
{
    public interface IPathProvider
    {
        string BasePath { get; }
        byte[] Read(string relativePath);
        string Add(string userName,byte[] content, bool fromIcon);
        bool Remove(string relativePath);
    }
}
