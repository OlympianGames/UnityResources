// Made by OlympianGames
// https://github.com/OlympianGames/UnityResources

using UnityEngine;
using System.IO;

public class DataSaver : MonoBehaviour
{
    // Singleton
    // To access, the game must be running, and the data saver script should be on a gameobject
    public static DataSaver Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }


    /// <summary>
    /// Saves data to a JSON file
    /// </summary>
    /// <param name="fileName"> Name for the JSON file</param>
    /// <param name="data"> Script you are calling this function from</param>
    public void SaveJson(string fileName, MonoBehaviour data)
    {
        // Sets the path in one variable for code readability 
        var targetDirectory = Path.Combine(Application.persistentDataPath, $"{fileName}.json");

        // This creates a new StreamWriter to write to a specific file path
        using (StreamWriter writer = new StreamWriter(targetDirectory))
        {
            // This will convert our Data object into a string of JSON
            string json = JsonUtility.ToJson(data);

            // This is where we actually write to the file
            writer.Write(json);
        }
    }

    /// <summary>
    /// Loads data from a JSON file
    /// </summary>
    /// <param name="fileName"> Name for the JSON file</param>
    /// <param name="data"> Script you are calling this function from</param>
    public void LoadJson(string fileName, MonoBehaviour data)
    {
        // Sets the path in one variable for code readability 
        var targetDirectory = Path.Combine(Application.persistentDataPath, $"{fileName}.json");

        // Checks if the target file exists before trying to load it
        if(File.Exists(targetDirectory))
        {
            // This creates a StreamReader, which allows us to read the data from the specified file path
            using (StreamReader reader = new StreamReader(targetDirectory))
            {
                // We read in the file as a string
                string dataToLoad = reader.ReadToEnd();

                // Here we convert the JSON formatted string into an actual Object in memory
                JsonUtility.FromJsonOverwrite(dataToLoad, data);
            }
        }
    }
}
