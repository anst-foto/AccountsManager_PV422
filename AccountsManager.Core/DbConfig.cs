using System.Text.Json;

namespace AccountsManager.Core;

public class DbConfig
{
    public string? ConnectionString { get; set; }
    public static string GetConnectionString(string pathToConfig)
    {
        if (string.IsNullOrWhiteSpace(pathToConfig)) 
            throw new ArgumentNullException(nameof(pathToConfig));
        
        var json = File.ReadAllText(pathToConfig);
        var result = JsonSerializer.Deserialize<DbConfig>(json)?.ConnectionString;
        
        if (string.IsNullOrWhiteSpace(result)) 
            throw new FailDeserializeException(pathToConfig);
        
        return result;
    }
}