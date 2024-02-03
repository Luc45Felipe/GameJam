using UnityEngine;

namespace Remake
{
    public class PlayerCtrl : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;

        [Header("References")]
        [SerializeField] private AudioClip _jumpClip;
        [SerializeField] private AudioClip _walkClip;

        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;
        private Rigidbody2D _rigidBody;
        private Animator _animator;

        private float _horizontalMov;
        private float _verticalMov;
        private bool _isOnLadder;
        private bool _isClimbing;
        private bool _onGround;
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
            _verticalMov = Input.GetAxis("Vertical");

            if(_isOnLadder && Mathf.Abs(_verticalMov) > 0f)
            {
                _isClimbing = true;
            }

            //jump
            _onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x - 0.06f, transform.position.y - 0.45f), 0.4f, 1<<3);

            if(Input.GetButtonDown("Jump") && _toJump == false && _onGround)
            {
                _toJump = true; 
            }
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
            else if(_horizontalMov != 0 && _onGround)
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

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.CompareTag("Ladder"))
            {
                _isOnLadder = true;
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
