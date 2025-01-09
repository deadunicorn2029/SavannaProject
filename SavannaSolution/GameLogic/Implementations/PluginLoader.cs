using GameLogic.Implementations;
using Newtonsoft.Json;

public static class PluginLoader
{
    /// <summary>
    /// Loads animal configurations from JSON files located in the "Plugins" directory.
    /// Reads each JSON file in the directory, deserializes its contents into `AnimalConfig` objects, and returns a list of these configurations.
    /// </summary>
    /// <returns> A list of `AnimalConfig` objects loaded from the "Plugins" directory. </returns>
    /// <exception cref="DirectoryNotFoundException"> Thrown if the "Plugins" directory does not exist. </exception>
    public static List<AnimalConfig> LoadAnimalConfigs()
    {
        string pluginsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");

        if (!Directory.Exists(pluginsDirectory))
        {
            throw new DirectoryNotFoundException($"Plugins directory not found: {pluginsDirectory}");
        }

        List<AnimalConfig> animalConfigs = new List<AnimalConfig>();

        foreach (var filePath in Directory.GetFiles(pluginsDirectory, "*.json"))
        {
            var jsonContent = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<AnimalConfig>(jsonContent);
            animalConfigs.Add(config);
        }

        return animalConfigs;
    }
}