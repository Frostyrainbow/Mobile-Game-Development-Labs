using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 2.0f;
    public Boundery boundery;
    public float verticalPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;


        //transform.position = new Vector2(transform.position.x + x, transform.position.y + y); ugly BLEH

        transform.position += new Vector3(x, 0.0f, 0.0f);

        float clampedPosition = Mathf.Clamp(transform.position.x, boundery.min, boundery.max);
        transform.position = new Vector2(clampedPosition, verticalPosition);
    }

    
}
