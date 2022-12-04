using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

/// <summary>
/// Editor window for changing scenes faster
/// </summary>
public class EditorSceneLoader : EditorWindow 
{
    /// <summary>
    /// List of scenes items
    /// </summary>
    List<SceneListItem> scenes;

    /// <summary>
    /// Action called when gettings a list of <paramref name="SceneListItem"/> assets
    /// </summary>
    static Action getSceneAsset;

    /// <summary>
    /// Action called when clearing the <paramref name="scenes"/> list
    /// </summary>
    static Action listCleared;

    /// <summary>
    /// Shows the editor window
    /// </summary>

    [MenuItem("Tools/Scene Loader/Open Window")]
    private static void ShowWindow() 
    {
        var window = GetWindow<EditorSceneLoader>();
        window.titleContent = new GUIContent("Scene Loader");
        window.Show();
    }

    /// <summary>
    /// OnGUI
    /// </summary>
    private void OnGUI() 
    {
        ButtonsGUI();
    }

    /// <summary>
    /// Creates all the buttons for loading the scenes
    /// </summary>
    private void ButtonsGUI()
    {
        if(scenes.Count != 0)   
        {
            foreach (var item in scenes)
            {
                if (GUILayout.Button($"{item.sceneName}"))
                {
                    LoadScene(item);
                }
            }
        }
    }
    
    /// <summary>
    /// Get the scriptable object for the scenes
    /// </summary>
    private void GetSceneAsset()
    {
        List<EditorSceneAsset> localScenes = GetAllInstances<EditorSceneAsset>();

        foreach (EditorSceneAsset item in GetAllInstances<EditorSceneAsset>())
        {
            foreach (SceneListItem scenesItem in item.scenes)
            {
                if(scenesItem.sceneName == "")
                    scenesItem.sceneName = scenesItem.sceneName.ToString();

                if(!scenes.Contains(scenesItem))
                    scenes.Add(scenesItem);
            }
        }

        if(scenes.Count <= 1)
            return;

        if(scenes.Count >= 2)
        {
            scenes.RemoveRange(1, scenes.Count - 2);
            Debug.LogError("There can only be one EditorSceneAsset object");
        }
    }

    /// <summary>
    /// Loads a selected scene
    /// </summary>
    /// <param name="asset"> SceneListItem to load the scene from </param>
    private void LoadScene(SceneListItem asset)
    {
        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(asset.sceneAsset));
    }

    /// <summary>
    /// Get all instances of a scriptable object
    /// </summary>
    public static List<T> GetAllInstances<T>() where T : ScriptableObject
    {
        return AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .ToList();
    }

    /// <summary>
    /// Mainly used for callbacks
    /// </summary>
    private void OnEnable() 
    {
        getSceneAsset += GetSceneAsset;
        listCleared += ClearList;
        listCleared += ClearList;
    }

    /// <summary>
    /// Mainly used for callbacks
    /// </summary>
    private void OnDisable()
    {
        getSceneAsset -= GetSceneAsset;
        listCleared -= ClearList;
        listCleared -= ClearList;
    }

    /// <summary>
    /// Menu item function for clearing the scenes list
    /// </summary>
    [MenuItem("Tools/Scene Loader/Clear Scene Assets")] public static void MenuItem() { listCleared.Invoke(); }
    private void ClearList() { scenes.Clear(); }

    /// <summary>
    /// Menu item function for gettings the scene scriptable object
    /// </summary>

    [MenuItem("Tools/Scene Loader/Get Scene Asset")]
    public static void GetSceneAssetMenuItem() { getSceneAsset.Invoke(); }


}