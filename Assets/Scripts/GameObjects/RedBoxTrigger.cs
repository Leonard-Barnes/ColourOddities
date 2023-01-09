using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class RedBoxTrigger : MonoBehaviour
{
     private TilemapCollider2D platformCollider;
     private TilemapRenderer platformRenderer;
     private Rigidbody2D rb;
 
    void Awake()
    {
        platformCollider = GetComponent<TilemapCollider2D>();
        platformRenderer = GetComponent<TilemapRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
 
    public void RedCollisionEnter()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        platformCollider.enabled = true;
        platformRenderer.sortingOrder = 2;
    }
            
    public void RedCollisionLeave()
    {
        rb.bodyType = RigidbodyType2D.Static;
        platformCollider.enabled = false;
        platformRenderer.sortingOrder = 0;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Red")
        {
            RedCollisionEnter();
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Red")
        {
            RedCollisionLeave();
        }
    }
}
