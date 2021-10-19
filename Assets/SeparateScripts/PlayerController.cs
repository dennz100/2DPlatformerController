using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float MaxSpeed = 7f;
    public float JumpTakeOffSpeed = 7f;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if(IsGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Velocity.y = JumpTakeOffSpeed;
            }
            else if(Input.GetButtonUp("Jump"))
            {
                if(Velocity.y > 0)
                {
                    Velocity.y *= 0.5f;
                }
            }
        }

        

        bool flipSprite = _spriteRenderer.flipX ? move.x > 0f : move.x < 0f;
        if(flipSprite)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        _animator.SetBool("grounded", IsGrounded);
        _animator.SetFloat("velocityX", Mathf.Abs(move.x) / MaxSpeed);

        TargetVelocity = move * MaxSpeed;
    }
}
