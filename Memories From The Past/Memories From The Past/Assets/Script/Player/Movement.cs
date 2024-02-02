using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private SpriteRenderer spriteRndr;
    private Animator animator;
    private Rigidbody2D rb;

    private float directionMov;
    public float speed;

    private bool isGround;
    private Transform jumpDetect;
    public LayerMask GroundLayer;
    public float jumpForce;

    private AudioSource walk_audioSrc;
    private AudioSource jump_audioSrc;
    public AudioClip jumpAudio;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRndr = GetComponent<SpriteRenderer>();
        walk_audioSrc = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        jumpDetect = transform.Find("pointJump").GetComponent<Transform>();        
        jump_audioSrc = jumpDetect.GetComponent<AudioSource>();
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(jumpDetect.position, 0.5f, GroundLayer);
        directionMov = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump_audioSrc.clip = jumpAudio;
            jump_audioSrc.Play();
        }
    }

    void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        rb.velocity = new Vector2(directionMov * speed, rb.velocity.y);
        Walk_Ornament();
    }

    public IEnumerator GetDemage()
    {
        Debug.Log("AI");
        yield return new WaitForSeconds(1);
        Debug.Log("doeu");
    }

    private void Walk_Ornament()
    {
        FlipSprite();

        if(!isGround || directionMov == 0)
        {
            walk_audioSrc.Stop();
            animator.SetBool("walk", false);
        }
        else
        {
            animator.SetBool("walk", true);
            if(!walk_audioSrc.isPlaying)
            {
                walk_audioSrc.PlayDelayed(0.2f);
            }
        }
    }

    private void FlipSprite()
    {
        if(directionMov < 0)
        {
            spriteRndr.flipX = true;

        }
        else if(directionMov > 0)
        {
            spriteRndr.flipX = false;
        }
    }
}
