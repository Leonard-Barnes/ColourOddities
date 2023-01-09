using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class BlueObjectTrigger : MonoBehaviour
{
     private TilemapCollider2D platformCollider;
     private TilemapRenderer platformRenderer;
 
    void Awake()
    {
        platformCollider = GetComponent<TilemapCollider2D>();
        platformRenderer = GetComponent<TilemapRenderer>();
    }
 
    public void BlueCollisionEnter()
    {
        platformCollider.enabled = true;
        platformRenderer.sortingOrder = 2;
    }
            
    public void BlueCollisionLeave()
    {
        platformCollider.enabled = false;
        platformRenderer.sortingOrder = 0;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Blue")
        {
            BlueCollisionEnter();
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Blue")
        {
            BlueCollisionLeave();
        }
    }
}
