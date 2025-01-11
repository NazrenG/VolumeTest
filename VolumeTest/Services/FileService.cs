using System.IO;

namespace VolumeTest.Services
{
    public class FileService : IFileService
    {
        public async Task<string> GetData(string path)
        {
            try
            {

                if (File.Exists(path))
                {
                    return await File.ReadAllTextAsync(path);
                }
                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> SetData(string value, string path)
        {
            try
            {
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }


                await File.WriteAllTextAsync(path, value);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error writing to file: {ex.Message}");
                return false;
            }
        }
    }
}
