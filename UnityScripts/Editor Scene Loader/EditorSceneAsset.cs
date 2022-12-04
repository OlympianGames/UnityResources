// Made by OlympianGames
// https://github.com/OlympianGames/UnityResources

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Editor Scene Asset", menuName = "Editor Scene Asset")]
public class EditorSceneAsset : ScriptableObject 
{
    public List<SceneListItem> scenes;
}

[System.Serializable]
public class SceneListItem
{
    public string sceneName;
    public SceneAsset sceneAsset;
}

#endif
