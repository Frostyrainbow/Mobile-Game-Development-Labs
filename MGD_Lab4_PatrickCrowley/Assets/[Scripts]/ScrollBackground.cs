using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float verticalsSpeed;
    public Boundery boundery;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    public void Move()
    {
        transform.position -= new Vector3(0.0f, verticalsSpeed * Time.deltaTime, 0.0f);
    }

    public void CheckBounds()
    {
        if (transform.position.y < boundery.min)
        {
            ResetStars();
        }
    }

    public void ResetStars()
    {
        transform.position = new Vector2(0.0f, boundery.max);
    }
}
