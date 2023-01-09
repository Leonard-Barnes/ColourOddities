using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class RedObjectTrigger : MonoBehaviour
{
    private TilemapCollider2D platformCollider;
    private TilemapRenderer platformRenderer;
    private Transform player;
 
    void Awake()
    {
        platformCollider = GetComponent<TilemapCollider2D>();
        platformRenderer = GetComponent<TilemapRenderer>();
    }
 
    public void RedCollisionEnter()
    {
        platformCollider.enabled = true;
        platformRenderer.sortingOrder = 2;
    }
            
    public void RedCollisionLeave()
    {
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
