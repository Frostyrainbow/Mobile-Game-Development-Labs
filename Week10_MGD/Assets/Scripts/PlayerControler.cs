using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float hForce;
    public float hSpeed;
    public float vForce;
    public float airControl;
    public Transform groundDetect;
    public float groundRadius;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Animations")]
    public Animator animator;
    public PlayerAnimationState playerAnimationState;

    [Header("Controls")]
    public Joystick joystick;

    public Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        joystick = (Application.isMobilePlatform) ? GameObject.Find("Joystick").GetComponent<Joystick>() : null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Ground check
        var hit = Physics2D.OverlapCircle(groundDetect.position, groundRadius, groundLayer);
        isGrounded = hit;

        Move();
        Jump();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? joystick.Horizontal : 0.0f);

        if (x != 0.0f)
        {
            x = (x > 0.0f) ? 1.0f : -1.0f;

            rigidbody.AddForce(Vector2.right * x * hForce * ((isGrounded) ? 1.0f : airControl));

            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, hSpeed);

            
        }

        if (x < 0.0f)
        {
            ChangeAnimation(PlayerAnimationState.RUNL);
        }
        else if (x > 0.0f)
        {
            ChangeAnimation(PlayerAnimationState.RUNR);
        }

        if (isGrounded && x == 0.0f)
        {
            ChangeAnimation(PlayerAnimationState.IDLE);
        }

        
    }

    private void AirCheck()
    {
        //if (!isGrounded)
        //{
        //    ChangeAnimation(PlayerAnimationState)
        //}
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump");

        if ((isGrounded) && (y > 0.0))
        {
            rigidbody.AddForce(Vector2.up * vForce, ForceMode2D.Impulse);
        }
    }

    private void ChangeAnimation(PlayerAnimationState animationState)
    {
        playerAnimationState = animationState;
        animator.SetInteger("AnimationState", (int)playerAnimationState);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundDetect.position, groundRadius); 
    }


}
