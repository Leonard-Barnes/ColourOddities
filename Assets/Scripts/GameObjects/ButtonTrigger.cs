using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
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

    void OnTriggerStay2D (Collider2D col)
    {
        if (col.gameObject.tag == "BluePlayer" || col.gameObject.tag == "RedPlayer")
        {
            ButtonCollisionEnter();
            Debug.Log("Pressed Button");
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "BluePlayer" || col.gameObject.tag == "RedPlayer")
        {
            ButtonCollisionLeave();
            Debug.Log("Let Go of Button");
        }
    }
}
