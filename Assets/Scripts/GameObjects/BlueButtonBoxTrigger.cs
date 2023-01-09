using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class BlueButtonBoxTrigger : MonoBehaviour
{
     private TilemapRenderer platformRenderer;
     private BoxCollider2D parentBox;
     private TilemapCollider2D parentTile;
 
    void Awake()
    {
        parentBox = transform.parent.gameObject.GetComponent<BoxCollider2D>();
        parentTile = transform.parent.gameObject.GetComponent<TilemapCollider2D>();
        platformRenderer = GetComponent<TilemapRenderer>();
    }
 
    public void ButtonCollisionEnter()
    {
        platformRenderer.sortingOrder = 2;
        parentBox.enabled = false;
        parentTile.enabled = false;
    }
            
    public void ButtonCollisionLeave()
    {
        platformRenderer.sortingOrder = 0;
        parentBox.enabled = true;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "BlueBox")
        {
            ButtonCollisionEnter();
            Debug.Log("Pressed Button");
        }
    }
}
