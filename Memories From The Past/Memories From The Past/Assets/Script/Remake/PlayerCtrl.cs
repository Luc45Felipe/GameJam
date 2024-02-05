using System;
using UnityEngine;

namespace Remake
{
    public sealed class PlayerCtrl : MonoBehaviour
    {
        public static Action FallInLimbo;

        [Header("Settings")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;

        [Header("References")]
        [SerializeField] private AudioClip _walkClip;
        [SerializeField] private AudioClip _jumpClip;

        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;
        private Rigidbody2D _rigidBody;
        private Animator _animator;

        private float _horizontalMov;
        private float _verticalMov;
        private bool _isOnLadder;
        private bool _isClimbing;
        private bool _toJump;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if(_horizontalMov != Input.GetAxis("Horizontal"))
            {
                _horizontalMov = Input.GetAxis("Horizontal");
            }

            // climb
            if(_isOnLadder)
            {
                _verticalMov = Input.GetAxis("Vertical");
                if(Mathf.Abs(Input.GetAxis("Vertical")) > 0f)
                {
                    _isClimbing = true;
                }
            }

            //jump
            if(Input.GetButtonDown("Jump"))
            {
                if(_toJump == false && IsOnGround())
                {
                    _toJump = true; 
                }
            }
        }

        bool IsOnGround()
        {
            return Physics2D.OverlapCircle(new Vector2(transform.position.x - 0.06f, transform.position.y - 0.45f), 0.4f, 1<<3);
        }

        void FixedUpdate()
        {
            EffectCtrl();

            //climb
            if(_isClimbing)
            {
                _rigidBody.gravityScale = 0;
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _movementSpeed * _verticalMov);
            }
            else
            {
                _rigidBody.gravityScale = 3;
            }

            //jump
            if(_toJump)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
                _toJump = false;
            }
            
            _rigidBody.velocity = new Vector2(_horizontalMov * _movementSpeed, _rigidBody.velocity.y);
        }

        // this controll sprite, animation and Sound of player
        private void EffectCtrl()
        {
            FlipSpriteCtrl();

            if(_toJump)
            {
                _audioSource.clip = _jumpClip;
                _audioSource.loop = false;
                _audioSource.Play();
            }
            else if(_horizontalMov != 0 && IsOnGround())
            {
                _animator.SetBool("walk", true);

                if(_audioSource.isPlaying == false)
                {
                    _audioSource.clip = _walkClip;
                    _audioSource.loop = true;
                    _audioSource.Play();
                }
            }
            else
            {
                _animator.SetBool("walk", false);

                if(_audioSource.clip == _walkClip)
                {
                    _audioSource.Stop();
                }
            }
        }

        private void FlipSpriteCtrl()
        {
            if(_horizontalMov < 0)
            {
                _spriteRenderer.flipX = true;

            }
            else if(_horizontalMov > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }

        private void OnFallInLimbo()
        {
            FallInLimbo?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.CompareTag("Ladder"))
            {
                _isOnLadder = true;
            }

            if(collider.gameObject.CompareTag("Limbo"))
            {
                OnFallInLimbo();
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if(collider.gameObject.CompareTag("Ladder"))
            {
                _isOnLadder = false;
                _isClimbing = false;
            }
        }
    }
}
