using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    public Boundery boundery;
    public float verticalPosition;
    public Camera camera;
    public bool usingMobileInput = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        usingMobileInput = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingMobileInput)
        {
            MobileInput();
        }
        else
        {
            ConventionalInput();
        }


        Move();
    }

    public void ConventionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;


        //transform.position = new Vector2(transform.position.x + x, transform.position.y + y); ugly BLEH

        transform.position += new Vector3(x, 0.0f, 0.0f);
    }

    public void MobileInput()
    {
        foreach (var touch in Input.touches)
        {
            var destination = transform.position = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, destination, speed * Time.deltaTime);
        }
    }

    public void Move()
    {
        

        float clampedPosition = Mathf.Clamp(transform.position.x, boundery.min, boundery.max);
        transform.position = new Vector2(clampedPosition, verticalPosition);
    }



    
}
