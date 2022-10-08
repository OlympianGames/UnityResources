// Made by OlympianGames
// https://github.com/OlympianGames/UnityResources

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InspectorHelperExample : MonoBehaviour 
{
    public void ButtonExample()
    {
        Debug.Log("Button Clicked!");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(InspectorHelperExample))]
public class InspectorHelperExampleInspector : Editor 
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        InspectorHelperExample script = (InspectorHelperExample)target;

        InspectorHelper.Button("Button", script.ButtonExample);
    }
}
#endif