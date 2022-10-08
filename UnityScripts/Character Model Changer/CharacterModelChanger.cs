// Made by OlympianGames
// https://github.com/OlympianGames/UnityResources

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterModelChanger : MonoBehaviour
{
    [Tooltip("List of all models")] public List<GameObject> models;
    [Tooltip("Integer of active model")] [HideInInspector] public int activeModel;
    [Tooltip("Gameobject of the current model")] [HideInInspector] public GameObject currentModel;


    /// <summary>
    /// Sets a model from a list to active from a integer
    /// </summary>
    /// <param name="modelToActivate"> The integer of the model in the list to activate</param>
    public void SetModel(int modelToActivate)
    {
        foreach (var model in models)
        {
            model.SetActive(false);
        }

        if(activeModel > models.Count - 1)
        {
            activeModel = 0;
        }

        if(activeModel < 0)
        {
            activeModel = models.Count - 1;
        }

        foreach (var model in models)
        {
            if(models.IndexOf(model) == activeModel)
            {
                model.SetActive(true);
                currentModel = model;
            }
        }
    }

    /// <summary>
    /// Checks to see if the current model integer is the same as the current model
    /// </summary>
    public void CheckModels()
    {
        if(activeModel != models.IndexOf(currentModel))
        {
            SetModel(activeModel);
        }
    }

    /// <summary>
    /// Displays the next model in the list
    /// </summary>
    public void NextModel()
    {
        SetModel(activeModel++);
    }

    /// <summary>
    /// Displays the previous model in the list
    /// </summary>
    public void PreviousModel()
    {
        SetModel(activeModel--);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterModelChanger))]
public class CharacterModelChangerInspector : Editor 
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        CharacterModelChanger script = (CharacterModelChanger)target;

        if(script.currentModel == null)
            script.SetModel(0);

        EditorGUILayout.LabelField("Current Model", script.currentModel.name.ToString());

        script.activeModel = EditorGUILayout.IntSlider(script.activeModel, 0, script.models.Count - 1);

        InspectorHelper.Button("Next Model", script.NextModel);
        InspectorHelper.Button("Previous Model", script.PreviousModel);


        script.CheckModels();
    }
}
#endif