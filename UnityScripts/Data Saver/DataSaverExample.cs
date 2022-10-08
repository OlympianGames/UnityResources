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

#if UNITY_EDITOR
[CustomEditor(typeof(DataSaverExample))]
public class DataSaverExampleInspector : Editor 
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        DataSaverExample script = (DataSaverExample)target;

        InspectorHelper.Button("Save", script.Save);
        InspectorHelper.Button("Load", script.Load);
    }
}
#endif