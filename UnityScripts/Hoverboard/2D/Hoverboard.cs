using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoverboardPointData 
{
    public Transform point;    
    
    [Space(25)]

    public bool isHit;
    public Vector2 raycastPosition;

    [Space(25)]

    public float distance;
    public Vector2 finalAmount;

}

public class Hoverboard : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private List<HoverboardPointData> pointsData;
    [SerializeField] private GizmosSettings gizmosSettings;
    [SerializeField] private RaycastSettings raycastSettings;
    [SerializeField] private ForceSettings forceSettings;

    [System.Serializable]
    public struct GizmosSettings
    {
        public float radius;
        public Color color;
    }

    [System.Serializable]
    public struct RaycastSettings
    {
        public int maxDistance;
        public LayerMask layerMask;
    }

    [System.Serializable]
    public struct ForceSettings
    {
        public float forceMultiplier;
    }

    private void Start() 
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DrawRaycasts();
        AddForces();
        
    }

    private void DrawRaycasts()
    {
        foreach (var point in pointsData)
        {
            RaycastHit2D hit = Physics2D.Raycast(point.point.position, -Vector2.up, raycastSettings.maxDistance, raycastSettings.layerMask);

            point.isHit = IsHit(hit);

            point.raycastPosition = hit.point;
        }
    }

    private void AddForces()
    {
        foreach (var point in pointsData)
        {
            point.distance = Mathf.Abs(raycastSettings.maxDistance - (Vector2.Distance(point.raycastPosition, point.point.position)));
            point.finalAmount = (point.raycastPosition * point.distance) * forceSettings.forceMultiplier;


            rb.AddForceAtPosition(point.finalAmount, point.raycastPosition, ForceMode2D.Force);
        }
    }
    
    private void OnDrawGizmosSelected() 
    {
        if(Application.isPlaying)
        {
            Gizmos.color = gizmosSettings.color;
            foreach (var point in pointsData)
            {
                if(point.isHit)
                {
                    Gizmos.DrawWireSphere(point.point.position, gizmosSettings.radius);
                    Gizmos.DrawLine(point.raycastPosition, point.point.position);
                    Gizmos.DrawWireSphere(point.raycastPosition, gizmosSettings.radius);
                }
            }
        }
    }

    private bool IsHit(RaycastHit2D hit)
    {
        if(hit.collider == null)
        {   
            return false;  
        }
        return true;
    }
}