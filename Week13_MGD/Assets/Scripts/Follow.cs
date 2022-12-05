using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Target
{
    public Transform transform;
    public Vector2 offset;
    public bool x;
    public bool y;
}


public class Follow : MonoBehaviour
{
    public Target target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((target.x) ? target.transform.position.x + target.offset.x : transform.position.x,
            (target.y) ? target.transform.position.y + target.offset.y : transform.position.y, transform.position.z);
    }
}
