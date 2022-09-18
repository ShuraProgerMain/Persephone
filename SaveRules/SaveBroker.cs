using System.Text;
using System.Text.Json;
using CARDINAL.Persephone.Interfaces;

namespace CARDINAL.Persephone.SaveRules;

internal static class SaveBroker
{
    public static async Task SaveSerializeData<T>(string pathToDirectory, string fileName, T data)
    {
        await using FileStream fileStream = new FileStream(pathToDirectory + fileName, FileMode.Create);
        
        var options = new JsonSerializerOptions
        {
            IncludeFields = true,
        };
        var serializedData = JsonSerializer.Serialize(data, options);
        
        byte[] buffer = Encoding.Default.GetBytes(serializedData);
        await fileStream.WriteAsync(buffer, 0, buffer.Length);
    }

    public static T? LoadSerializeData<T>(string pathToDirectory, string fileName)
    {
        T? loadData = default(T);
        
        if (!Directory.Exists(pathToDirectory))
        {
            Directory.CreateDirectory(pathToDirectory);
        }
        else
        {
            if (File.Exists(pathToDirectory + fileName))
            {
                var stream = new StreamReader(pathToDirectory + fileName);

                try
                {
                    var data = stream.ReadToEnd();

                    if (data != string.Empty)
                    {
                        loadData = JsonSerializer.Deserialize<T>(data);
                    }
                }
                catch (Exception e)
                {
                    Helpers.Console.LogError(e.Message);
                    stream.Close();
                    File.Delete(pathToDirectory + fileName);
                }
                
                stream.Close();
            }
        }

        return loadData;
    }

    public static async Task SaveText(string pathToDirectory, string fileName, string data)
    {
        await using FileStream fileStream = new FileStream(pathToDirectory + fileName, FileMode.Create);

        byte[] buffer = Encoding.Default.GetBytes(data);
        await fileStream.WriteAsync(buffer, 0, buffer.Length);
    }
}