using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controler : MonoBehaviour
{
    public float hForce;
    public float hSpeed;
    public float vForce;
    public float airControl;
    public Transform groundDetect;
    public float groundRadius;
    public LayerMask groundLayer;
    public bool isGrounded;

    public Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        
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
        var x = Input.GetAxisRaw("Horizontal");

        if (x != 0.0f)
        {
            x = (x > 0.0f) ? 1.0f : -1.0f;

            rigidbody.AddForce(Vector2.right * x * hForce * ((isGrounded) ? 1.0f : airControl));

            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, hSpeed);
        }

        
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump");

        if ((isGrounded) && (y > 0.0))
        {
            rigidbody.AddForce(Vector2.up * vForce, ForceMode2D.Impulse);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundDetect.position, groundRadius); 
    }
}
