using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTeleport : MonoBehaviour
{
    private Transform boxTransform;
    private Rigidbody2D rbBox; 
    public Transform boxCheckpoint;
    private Vector3 boxPosition;

    // Start is called before the first frame update
    void Start()
    {
        boxTransform = GetComponent<Transform>();
        rbBox = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        boxPosition = boxTransform.position;
        if (boxPosition.y < -10f)
        {
            rbBox.velocity = new Vector2 (0, 0);
            boxTransform.position = boxCheckpoint.position;
        }
    }
}
