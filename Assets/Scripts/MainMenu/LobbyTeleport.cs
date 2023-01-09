using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTeleport : MonoBehaviour
{
    private Transform redPlayer;
    private Transform bluePlayer;
    private Transform redCheckpoint;
    private Transform blueCheckpoint;
    private SoundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        redCheckpoint = GameObject.FindWithTag("RedCheckpoint").GetComponent<Transform>();
        blueCheckpoint = GameObject.FindWithTag("BlueCheckpoint").GetComponent<Transform>();
        SetTargets();
        if(bluePlayer != null)
        {
            bluePlayer.position = blueCheckpoint.position;
        }
        if(redPlayer != null)
        {
            redPlayer.position = redCheckpoint.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (redPlayer != null)
        {
            if (redPlayer.position.y < -15f)
            {
                RedTeleport();
                Debug.Log("Red fell off-screen. Teleporting...");
                sound.PlayerSound("death");
            }
        }
        
        if (bluePlayer != null)
        {
            if (bluePlayer.position.y < -15f)
            {
                Debug.Log("Blue fell off-screen. Teleporting...");
                BlueTeleport();
                sound.PlayerSound("death");
            }
        }
    }

    public void SetTargets()
    {
        if (GameObject.FindWithTag("RedPlayer") != null)
        {
            redPlayer = GameObject.FindWithTag("RedPlayer").GetComponent<Transform>();
            Debug.Log("Red Target Set");
        }
        if (GameObject.FindWithTag("BluePlayer") != null)
        {
            bluePlayer = GameObject.FindWithTag("BluePlayer").GetComponent<Transform>();
            Debug.Log("Blue Target Set");
        }
        else
        {
            return;
        }
    }

    public void BlueTeleport()
    {
        bluePlayer.position = blueCheckpoint.position;
    }

    public void RedTeleport()
    {
        redPlayer.position = redCheckpoint.position;
    }
}
