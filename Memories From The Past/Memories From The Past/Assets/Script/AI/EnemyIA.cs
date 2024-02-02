using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;

    [SerializeField]private int walkSpeed;
    [SerializeField]private Transform groundDetectionPoint;
    [SerializeField]private Transform wallDetectionPoint;
    [SerializeField]private LayerMask groundLayer;

    bool goingToLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(walkSpeed * SetDirection(), rb.velocity.y);
    }

    //this just return 1 and -1 to directions
    private int SetDirection()
    {
        bool groundDetected = Physics2D.OverlapCircle(groundDetectionPoint.position, 0.5f, groundLayer);
        bool wallDetected = Physics2D.OverlapCircle(wallDetectionPoint.position, 0.5f, groundLayer);

        if(!groundDetected || wallDetected)
        {
            FlipGameObject();
        }

        if(goingToLeft)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    private void FlipGameObject()
    {
        goingToLeft = !goingToLeft;
        if(goingToLeft)
        {
            tr.localScale = new Vector3(-1,1,1);    
        }
        else
        {
            tr.localScale = new Vector3(1,1,1);
        }
    }
}
