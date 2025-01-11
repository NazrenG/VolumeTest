namespace VolumeTest.Services
{
    public interface IFileService
    { 
        Task<bool> SetData(string value, string path);
        Task<string> GetData(string path);
    }
}
