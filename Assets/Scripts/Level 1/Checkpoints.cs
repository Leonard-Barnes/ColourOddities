using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public GameObject checkpoints;
    private Vector2 checkpointLocation;
    public Sprite checkpointSprite;
    public Sprite checkpointSpriteHalf;
    private bool redReady;
    private bool blueReady;

    void Awake()
    {
        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
        if (PlayerPrefs.GetInt("currentScene") != PlayerPrefs.GetInt("futureScene") || PlayerPrefs.HasKey("futureScene") == false)
        {
            Debug.Log("Current Scene: " + PlayerPrefs.GetInt("currentScene"));
            Debug.Log("Future Scene: " + PlayerPrefs.GetInt("futureScene"));
            PlayerPrefs.SetFloat("checkpointNum", 0);
            PlayerPrefs.SetFloat("checkpoint_x", checkpoints.transform.position.x);
            PlayerPrefs.SetFloat("checkpoint_y", checkpoints.transform.position.y);
        }
        if (PlayerPrefs.GetFloat("checkpointNum") == 1)
        {
            checkpointLocation = new Vector2(PlayerPrefs.GetFloat("checkpoint_x"), PlayerPrefs.GetFloat("checkpoint_y"));
            checkpoints.transform.position = checkpointLocation;
        }
        Debug.Log("Is Checkpointed: " + PlayerPrefs.GetFloat("checkpointNum"));

    }
    private void SetCheckpoint()
    {
        checkpoints.transform.position = gameObject.transform.position;
        checkpointLocation = checkpoints.transform.position;
        PlayerPrefs.SetFloat("checkpointNum", 1);
        PlayerPrefs.SetFloat("checkpoint_x", checkpointLocation.x);
        PlayerPrefs.SetFloat("checkpoint_y", checkpointLocation.y);
        gameObject.GetComponent<SpriteRenderer>().sprite = checkpointSprite;
        Debug.Log(PlayerPrefs.GetFloat("checkpointNum"));
        Debug.Log("Checkpoint Set! Destroying Object...");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "BluePlayer" && col.gameObject.tag == "RedPlayer")
        {
            SetCheckpoint();
        }
        else if (col.gameObject.tag == "BluePlayer")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = checkpointSpriteHalf;
            blueReady = true;
        }
        else if (col.gameObject.tag == "RedPlayer")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = checkpointSpriteHalf;
            redReady = true;
        }
    }
    private void Update()
    {
        if(redReady == true && blueReady == true)
        {
            SetCheckpoint();
            redReady = false;
            blueReady = false;
        }
        PlayerPrefs.SetInt("futureScene", SceneManager.GetActiveScene().buildIndex);
    }
}
