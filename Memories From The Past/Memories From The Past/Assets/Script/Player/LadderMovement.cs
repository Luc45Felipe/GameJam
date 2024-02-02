using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 5;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if(isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    void FixedUpdate()
    {
        if(isClimbing)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, speed * vertical);
        }
        else
        {
            rb.gravityScale = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
