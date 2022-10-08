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


public class ObjectCreator : MonoBehaviour 
{
    /// <summary>
    /// Instantiate a new prefab
    /// </summary>
    /// <param name="requestedObject"> Prefab to insantiate </param>
    /// <param name="targetPosition"> Position to insantiate </param>
    /// <param name="targetRotation"> Rotation to insantiate </param>
    /// <param name="targetParent"> Prefabs parent </param>
    public static void Instantiate(GameObject requestedObject, Vector3 targetPosition, Quaternion targetRotation, Transform targetParent)
    {
        GameObject currentObject = UnityEngine.Object.Instantiate(requestedObject, targetPosition, targetRotation);
        currentObject.transform.SetParent(targetParent);
    }

    /// <summary>
    /// Retrieve a object from a object pool
    /// </summary>
    /// <param name="pool"> Pool to retrieve from </param>
    /// <param name="targetPosition"> Position to insantiate </param>
    /// <param name="targetRotation"> Rotation to insantiate </param>
    /// <param name="targetParent"> Prefabs parent </param>
    public static void RequestObject(ObjectPooler pool, Vector3 targetPosition, Quaternion targetRotation, Transform targetParent)
    {
        pool.RetrieveObject(targetPosition, targetRotation, targetParent);
    }
}