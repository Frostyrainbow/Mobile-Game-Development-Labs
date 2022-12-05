using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birb : MonoBehaviour
{
    [Header("Movement Properties")]
    public float horizontalSpeed;
    public Transform forwardPoint;
    public Transform centerPoint;
    public bool obstacleAhead;
    public Vector2 direction;
    public LayerMask groundLayer;
    public PlayerDection playerDection;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        obstacleAhead = Physics2D.Linecast(centerPoint.position, forwardPoint.position, groundLayer);

        if (!obstacleAhead && !playerDection.playerDetected)
        {
            Move();
        }

        if (obstacleAhead)
        {
            Flip();
        }

        if (playerDection.playerDetected)
        {
            MoveToPlayer();
        }
    }

    public void Flip()
    {
        var x = transform.localScale.x * -1.0f;
        direction *= -1.0f;
        transform.localScale = new Vector3(x, 0.6f, 0.6f);
    }

    public void Move()
    {
        transform.position += new Vector3(direction.x * horizontalSpeed * Time.deltaTime, 0.0f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(centerPoint.position, forwardPoint.position);
    }

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerDection.playerTransform.position, horizontalSpeed * Time.deltaTime);
    }
}
