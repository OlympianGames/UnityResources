// Made by OlympianGames
// https://github.com/OlympianGames/UnityResources

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Editor Scene Object", menuName = "Editor Scene Object")]
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

[CustomEditor(typeof(EditorSceneAsset))]
public class EditorSceneAssetInspector : Editor 
{
    EditorSceneAsset script;
    private void OnEnable() 
    {
        script = target as EditorSceneAsset;
    }

    public override void OnInspectorGUI() 
    {
        if(script == null)
            script = target as EditorSceneAsset;

        base.OnInspectorGUI();
        
        EditorGUILayout.Space(10);

        try
        {
            if(CurrentSceneInList())
            {
                // EditorGUILayout.LabelField("Scene Not In List");
                InspectorHelper.Button("Remove Current Scene", RemoveCurrentSceneFromList);
            }
            else
            {
                // EditorGUILayout.LabelField("Scene In List");
                InspectorHelper.Button("Add Current Scene", AddCurrentSceneToList);
            } 
        }
        catch
        {
            // error
        }
    }

    private bool CurrentSceneInList()
    {
        if(script.scenes.Count > 0)
        {
            foreach (var item in script.scenes)
            {
                if(SceneManager.GetActiveScene().path == AssetDatabase.GetAssetPath(item.sceneAsset))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void AddCurrentSceneToList()
    {
        SceneListItem listItem = new SceneListItem();


        listItem.sceneAsset = (SceneAsset)AssetDatabase.LoadAssetAtPath(SceneManager.GetActiveScene().path, typeof(SceneAsset));
        listItem.sceneName = listItem.sceneAsset.name;

        script.scenes.Add(listItem);
    }

    private void RemoveCurrentSceneFromList()
    {
        foreach (var item in script.scenes)
        {
            if(SceneManager.GetActiveScene().path == AssetDatabase.GetAssetPath(item.sceneAsset))
            {
                script.scenes.Remove(item);
                return;
            }
        }
    }
}

#endif
