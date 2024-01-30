namespace MyFoodTracker.Api;

public interface IFileSystem
{
   string Read(string dataFileDirectory);

   void Write(string dataFileDirectory, string content);
}

public class FileSystem : IFileSystem
{
   private const string DataFileName = "my-food-tracker.json";
   
   public string Read(string dataFileDirectory)
   {
      var dataFilePath = Path.Combine(dataFileDirectory, DataFileName);

      return File.Exists(dataFilePath) ? File.ReadAllText(dataFilePath) : string.Empty;
   }

   public void Write(string dataFileDirectory, string content)
   {
      if (!Directory.Exists(dataFileDirectory))
      {
         Directory.CreateDirectory(dataFileDirectory);
      }
      var dataFilePath = Path.Combine(dataFileDirectory, DataFileName);

      File.WriteAllText(dataFilePath, content);
   }
}