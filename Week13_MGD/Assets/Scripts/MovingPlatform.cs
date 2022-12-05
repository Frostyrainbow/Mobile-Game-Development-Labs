using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public PlatformDirection direction;
    public float speed = 5.0f;

    [Range(1.0f, 20.0f)]
    public float horizontalDistance = 5.0f;
    [Range(1.0f, 20.0f)]
    public float horizontalSpeed = 2.0f;

    [Range(1.0f, 20.0f)]
    public float verticalDistance = 5.0f;
    [Range(1.0f, 20.0f)]
    public float verticalSpeed = 2.0f;

    private Vector2 startPoint;
    private Vector2 endPoint;

    //[Header("Platform Path Points")]
    public Transform[] wayPoints;
    //public Transform centerPosition;
    int waypointToMoveTo;
    private Vector2 destinationPoint;



    // Start is called before the first frame update
    void Start()
    {
        
        startPoint = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        switch (direction)
        {
            case PlatformDirection.HORIZONTAL:
                transform.position = new Vector2(Mathf.PingPong(horizontalSpeed * Time.time, horizontalDistance) + startPoint.x, startPoint.y);
                break;

            case PlatformDirection.VERTICAL:
                transform.position = new Vector2(startPoint.x, Mathf.PingPong(verticalSpeed * Time.time, verticalDistance) + startPoint.y);
                break;

            case PlatformDirection.DIAGONAL_UP:
                transform.position = new Vector2(Mathf.PingPong(horizontalSpeed * Time.time, horizontalDistance) + startPoint.x, Mathf.PingPong(verticalSpeed * Time.time, verticalDistance) + startPoint.y);
                break;

            case PlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(Mathf.PingPong(horizontalSpeed * Time.time, horizontalDistance) + startPoint.x, startPoint.y - Mathf.PingPong(verticalSpeed * Time.time, verticalDistance));
                break;

            case PlatformDirection.CUSTOM:
                if (waypointToMoveTo <= wayPoints.Length)
                {
                    transform.position = Vector2.MoveTowards(transform.position, wayPoints[waypointToMoveTo].transform.position, speed * Time.deltaTime);

                    if (transform.position == wayPoints[waypointToMoveTo].transform.position)
                    {
                        if (waypointToMoveTo >= 3)
                        {
                            waypointToMoveTo = 0;
                        }
                        else
                        {
                            waypointToMoveTo++;
                        }

                    }

                }
                break;

        }

    }
}
