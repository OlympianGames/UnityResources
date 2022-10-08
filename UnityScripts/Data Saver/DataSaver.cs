// Made by OlympianGames
// https://github.com/OlympianGames/UnityResources

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class DataSaver : MonoBehaviour
{
    /// <summary>
    /// Saves data to a JSON file
    /// </summary>
    /// <param name="fileName"> Name for the JSON file</param>
    /// <param name="data"> Script you are calling this function from</param>
    public void SaveJson(string fileName, MonoBehaviour data)
    {
        // This creates a new StreamWriter to write to a specific file path
        using (StreamWriter writer = new StreamWriter(Path.Combine(Application.persistentDataPath, $"{fileName}.json")))
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
        // This creates a StreamReader, which allows us to read the data from the specified file path
        using (StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, $"{fileName}.json")))
        {
            // We read in the file as a string
            string dataToLoad = reader.ReadToEnd();

            // Here we convert the JSON formatted string into an actual Object in memory
            // data = JsonUtility.FromJson<typeof(data)>(dataToLoad);
            JsonUtility.FromJsonOverwrite(dataToLoad, data);
        }
    }
}