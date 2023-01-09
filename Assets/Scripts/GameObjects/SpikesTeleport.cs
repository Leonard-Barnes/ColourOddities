using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTeleport : MonoBehaviour
{
    public LobbyTeleport LobbyTeleport;
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "BluePlayer")
        {
            LobbyTeleport.BlueTeleport();
        }
        
        if (col.gameObject.tag == "RedPlayer")
        {
            LobbyTeleport.RedTeleport();
        }
        
    }
}
