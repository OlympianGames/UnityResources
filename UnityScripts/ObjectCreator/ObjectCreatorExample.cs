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


public class ObjectCreatorExample : MonoBehaviour 
{
    public GameObject instantiatePrefab;
    public Transform instantiateTransform;
    public Transform instantiateParent;

    [Space(15)]
    
    public ObjectPooler objectPooler;
    public Transform objectPoolTransform;
    public Transform objectPoolParent;

    public void InstantiateButton()
    {
        ObjectCreator.Instantiate(instantiatePrefab, instantiateTransform.position, instantiateTransform.rotation, instantiateParent);
    }

    public void RequestObjectButton()
    {
        ObjectCreator.RequestObject(objectPooler, objectPoolTransform.position, objectPoolTransform.rotation, objectPoolParent);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ObjectCreatorExample))]
public class ObjectCreatorExampleInspector : Editor 
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        ObjectCreatorExample script = (ObjectCreatorExample)target;

        InspectorHelper.Button("Instantiate", script.InstantiateButton);
        InspectorHelper.Button("Request", script.RequestObjectButton);
    }
}
#endif