# DataSaver

Made by OlympianGames - [Link](https://github.com/OlympianGames/UnityResources/tree/main/UnityScripts)


## About

Simple script that allows for the saving and loading of data from a json file.

## Example

``` C#
    public class DataSaverExample : DataSaver
    {
        public string exampleString;
        public bool exampleBool;
        public float exampleFloat;

        public void Save()
        {
            SaveJson("DataSaverExample", this);
        }

        public void Load()
        {
            LoadJson("DataSaverExample", this);
        }
    }
```





