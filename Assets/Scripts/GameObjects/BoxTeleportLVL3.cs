using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTeleportLVL3 : MonoBehaviour
{
    private Transform box;
    public Vector2 checkpoint;

    void Awake()
    {
        box = gameObject.GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (box.position.y < 0.35f)
        {
            Debug.Log("Teleporting Box...");
            box.position = checkpoint;
            box.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
