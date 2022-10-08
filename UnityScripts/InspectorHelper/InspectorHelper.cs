// Made by OlympianGames
// https://github.com/OlympianGames/UnityResources

#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using UnityEngine.Events;

public class InspectorHelper : Editor 
{
    /// <summary>
    /// A simple function that helps making inspector buttons a bit easier
    /// </summary>
    /// <param name="ButtonName"> The text that will be show on the button in the inspector</param>
    /// <param name="action"> The function that will run when the button is pressed</param>
    public static void Button(string ButtonName, Action action)
    {
        if (GUILayout.Button(ButtonName))
        {
            action?.Invoke();
        }
    }
}
#endif