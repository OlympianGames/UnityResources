using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

/// <summary>
/// Editor window for changing scenes faster
/// </summary>
public class EditorSceneLoader : EditorWindow 
{
    /// <summary>
    /// List of all <paramref name="EditorSceneAsset"/> used
    /// </summary>
    List<EditorSceneAsset> sceneContainers;

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
    /// Action called when requesting a new <paramref name="EditorSceneAsset"/> asset creation
    /// </summary>
    static Action createSceneAsset;

    /// <summary>
    /// Bool for if <paramref name="InitializeStyle"/> has been run
    /// </summary>
    private bool fontTypeInitialized = false;

    /// <summary>
    /// Custom <paramref name="GUIStyle"/> for title text
    /// </summary>
    private GUIStyle TitleTextStyle;

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

    private void InitializeStyle()
    {
        fontTypeInitialized = true;

        TitleTextStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20,
            fontStyle = FontStyle.Bold
        };
    }

    /// <summary>
    /// Renders the inspecter and all of its fields
    /// </summary>
    private void OnGUI() 
    {
        if(!fontTypeInitialized)
            InitializeStyle();

        EditorGUILayout.Space(5);
        
        EditorGUILayout.LabelField("Scene Loader", TitleTextStyle);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.Space(10);

        if(ShowSceneButtons())
            ButtonsGUI();
        else if(!ShowSceneButtons())
            DefaultButtons();

    }

    /// <summary>
    /// Checks if there are currents scenes to create buttons for
    /// </summary>
    private bool ShowSceneButtons()
    {
        if(sceneContainers.Count > 0)
            if(scenes.Count > 0)
                return true;
        else if(scenes.Count > 0)
            return true;
        
        return false;
    }

    /// <summary>
    /// Buttons for if there is no <paramref name="EditorSceneAsset"/> found
    /// </summary>
    private void DefaultButtons()
    {
        InspectorHelper.Button("Create Scene Asset", CreateSceneAsset);

        EditorGUILayout.Space(5);

        InspectorHelper.Button("Find Scene Asset", EditorSceneLoader.GetSceneAssetMenuItem);
    }

    /// <summary>
    /// Function for creating a <paramref name="EditorSceneAsset"/>
    /// </summary>
    private void CreateSceneAsset()
    {
        EditorSceneAsset sceneAsset = ScriptableObject.CreateInstance<EditorSceneAsset>();

        // path has to start at "Assets"
        string path = "Assets/Editor/SceneAsset.asset";

        AssetDatabase.CreateAsset(sceneAsset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Selection.activeObject = sceneAsset;

        getSceneAsset.Invoke();
    }

    /// <summary>
    /// Creates all the buttons for loading the scenes
    /// </summary>
    private void ButtonsGUI()
    {
        if(scenes.Count > 0)   
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
        listCleared.Invoke();

        sceneContainers = GetAllInstances<EditorSceneAsset>();

        try
        {
            if(sceneContainers.Count > 0)
            {
                foreach (EditorSceneAsset item in sceneContainers)
                {
                    if(item.scenes.Count > 0)
                    {
                        foreach (SceneListItem scenesItem in item.scenes)
                        {
                            if(String.IsNullOrEmpty(scenesItem.sceneName))
                                scenesItem.sceneName = scenesItem.sceneAsset.name;

                            if(!scenes.Contains(scenesItem))
                                scenes.Add(scenesItem);
                        }
                    }
                }
            }
        }
        catch
        {
             // TODO
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
        createSceneAsset += CreateSceneAsset;
    }

    /// <summary>
    /// Mainly used for callbacks
    /// </summary>
    private void OnDisable()
    {
        getSceneAsset -= GetSceneAsset;
        listCleared -= ClearList;
        createSceneAsset -= CreateSceneAsset;
    }

    /// <summary>
    /// Menu item function for clearing the scenes list
    /// </summary>
    [MenuItem("Tools/Scene Loader/Clear Scene Assets")] public static void MenuItem() { listCleared.Invoke(); }
    private void ClearList() 
    { 
        scenes.Clear(); 
        sceneContainers.Clear();
    }

    /// <summary>
    /// Menu item function for gettings the scene scriptable object
    /// </summary>

    [MenuItem("Tools/Scene Loader/Get Scene Asset")]
    public static void GetSceneAssetMenuItem() { getSceneAsset.Invoke(); }

    /// <summary>
    /// Menu item function for gettings the scene scriptable object
    /// </summary>

    [MenuItem("Tools/Scene Loader/Create Scene Asset")]
    public static void CreateSceneAssetMenuItem() { createSceneAsset.Invoke(); }


}
