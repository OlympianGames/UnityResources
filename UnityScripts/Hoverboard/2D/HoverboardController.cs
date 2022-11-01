using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hoverboard))]
public class HoverboardController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpAmount;
    private Hoverboard hoverboard;
    private Rigidbody2D rb;
    private float playerInput;
    private bool jump;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hoverboard = gameObject.GetComponent<Hoverboard>();
    }

    private void FixedUpdate() 
    {
        AddForces();
    }

    private void AddForces()
    {
        if(Input.GetButtonDown("Jump"))
        {
            foreach (var point in hoverboard.pointsData)
            {
                if(point.isHit)
                {
                    hoverboard.AddForce(point, jumpAmount);

                    Debug.Log($"{point.point.name} has jumped");
                }
            }

        }

        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed, 0));
    }
}
