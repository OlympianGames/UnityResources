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

public class ObjectPooler : ObjectCreator
{

    [SerializeField] private List<GameObject> objectPool;
    [HideInInspector] public GameObject previousObject;
    [HideInInspector] public GameObject currentObject;

    /// <summary>
    /// Retrieve a object from a object pool
    /// </summary>
    /// <param name="targetPosition"> Position to insantiate </param>
    /// <param name="targetRotation"> Rotation to insantiate </param>
    /// <param name="targetParent"> Prefabs parent </param>
    public void RetrieveObject(Vector3 targetPosition, Quaternion targetRotation, Transform targetParent)
    {
        int previousObjectInt = objectPool.IndexOf(previousObject);
        Debug.Log(previousObjectInt);

        int currentObjectInt = previousObjectInt + 1;
        Debug.Log(currentObjectInt);

        if(currentObjectInt > objectPool.Count - 1)
        {
            currentObjectInt = 0;
        }


        foreach (var item in objectPool)
        {
            if(objectPool.IndexOf(item) == currentObjectInt)
            {
                currentObject = item;
            }
        }

        currentObject.transform.position = targetPosition;
        currentObject.transform.rotation = targetRotation;
        currentObject.transform.SetParent(targetParent);

        currentObject.SetActive(true);

        previousObject = currentObject;
    }

    /// <summary>
    /// Retrieve a rigidbody object from a object pool
    /// </summary>
    /// <param name="targetPosition"> Position to insantiate </param>
    /// <param name="targetRotation"> Rotation to insantiate </param>
    /// <param name="targetParent"> Prefabs parent </param>
    public void RetrieveRigidBody(Vector3 targetPosition, Quaternion targetRotation, Transform targetParent)
    {
        RetrieveObject(targetPosition, targetRotation, targetParent);
        Rigidbody currentRB = currentObject.GetComponent<Rigidbody>();
        currentRB.velocity = Vector3.zero;
    }
    
}


