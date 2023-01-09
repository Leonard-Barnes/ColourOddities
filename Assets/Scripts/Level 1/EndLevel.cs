using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private bool redReady = false;
    private bool blueReady = false;

    public void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log(SceneManager.GetActiveScene().name.ToString() + " completed, teleporting to next level...");
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "RedPlayer")
        {
            redReady = true;
        }
        if (col.gameObject.tag == "BluePlayer")
        {
            blueReady = true;
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "RedPlayer")
        {
            redReady = false;
        }
        if (col.gameObject.tag == "BluePlayer")
        {
            blueReady = false;
        }
    }

    private void Update()
    {
        if (redReady == true && blueReady == true)
        {
            ChangeLevel();
        }
    }
}
