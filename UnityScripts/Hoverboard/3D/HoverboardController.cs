using System.Collections.Generic;
using UnityEngine;

#region 
[System.Serializable]
public class HoverboardPointData 
{
    public Transform point;
    public Transform localRaycast;

    [Space(25)]

    public bool isHit;
    public Vector3 raycastPosition;
    public Vector3 pointPosition;

    [Space(25)]

    public float distance;
    public float yDistance;
    public Vector3 finalAmount;
}

[System.Serializable]
public class GizmosSettings
{
    public float radius = 0.1f;
    public Color color = Color.red;
    public bool alwaysDraw;
    public bool selectedDraw;
}

[System.Serializable]
public class RaycastSettings
{
    public float maxDistance = 1;
    public LayerMask layerMask;
}

[System.Serializable]
public class ForceSettings
{
    public float forceMultiplier = -25;
}

#endregion

public class HoverboardController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private List<HoverboardPointData> pointsData;
    [SerializeField] private GizmosSettings gizmosSettings;
    [SerializeField] private RaycastSettings raycastSettings;
    [SerializeField] private ForceSettings forceSettings;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        CreateRaycastObjects();
    }

    private void Update()
    {
        DrawRaycasts();
    }
    
    private void FixedUpdate() 
    {
        
        AddForce();
    }

    private void DrawRaycasts()
    {
        foreach (var point in pointsData)
        {
            point.pointPosition = point.point.position;
            RaycastHit hit;
            point.isHit = Physics.Raycast(point.point.position, Vector3.down, out hit, raycastSettings.maxDistance, raycastSettings.layerMask);
            
            point.raycastPosition = hit.point;
            point.localRaycast.position = hit.point;
            
        }
    }

    private void AddForce()
    {
        foreach (var point in pointsData)
        {
            point.yDistance = (Mathf.Abs(point.raycastPosition.y) - Mathf.Abs(point.pointPosition.y));
            point.distance = Mathf.Abs(raycastSettings.maxDistance - (Vector3.Distance(point.raycastPosition, point.pointPosition)));
            point.finalAmount = (point.raycastPosition * point.distance) * forceSettings.forceMultiplier;


            rb.AddForceAtPosition(point.finalAmount, point.raycastPosition, ForceMode.Force);
        }
    }

    private void CreateRaycastObjects()
    {
        foreach(var point in pointsData)
        {
            if(point.localRaycast == null)
            {
                GameObject go = new GameObject();
                point.localRaycast = go.transform;
                go.name = point.point.name + " - Local Raycast Position";
                go.transform.SetParent(this.transform);
            }
        }
    }

    private void OnDrawGizmos() 
    {
        if(gizmosSettings.alwaysDraw)
            DrawGizmos();
    }

    private void OnDrawGizmosSelected() 
    {
        if(gizmosSettings.selectedDraw)
            DrawGizmos();
    }

    private void DrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = gizmosSettings.color;
            foreach (var point in pointsData)
            {
                if(point.isHit)
                {
                    Gizmos.DrawWireSphere(point.pointPosition, gizmosSettings.radius);
                    Gizmos.DrawLine(point.raycastPosition, point.pointPosition);
                    Gizmos.DrawWireSphere(point.raycastPosition, gizmosSettings.radius);
                }
            }
        }
    }
}