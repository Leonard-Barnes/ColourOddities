using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField] private int maxPlayers = 2;
    public GameObject redPlayerPrefab;
    public GameObject bluePlayerPrefab;
    private Transform redCheckpoint;
    private Transform blueCheckpoint;
    private Quaternion rotation = new Quaternion(0,0,0,0);
    public static PlayerConfigurationManager Instance { get; private set; }
    private float numPlayers;
    private PlayerInputManager playerIM;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Trying to create another instance!");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
        numPlayers = 0;
        playerIM = GetComponent<PlayerInputManager>();
        redCheckpoint = GameObject.FindWithTag("RedCheckpoint").GetComponent<Transform>();
        blueCheckpoint = GameObject.FindWithTag("BlueCheckpoint").GetComponent<Transform>();
    }
    
    public void HandlePlayerJoin(PlayerInput pi)
    {
        numPlayers += 1;

        if(!playerConfigs.Exists(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }

        if (numPlayers == 2)
        {
            GameObject.FindWithTag("BluePlayer").GetComponent<Transform>().position = blueCheckpoint.position;
            Debug.Log("Spawning in Player 2 (Blue)");
        }
        else if (numPlayers == 1)
        { 
            GameObject.FindWithTag("RedPlayer").GetComponent<Transform>().position = redCheckpoint.position;
            Debug.Log("Spawning in Player 1 (Red)");
            playerIM.playerPrefab = bluePlayerPrefab;
        }
        else
        {
            Debug.Log("Already at maximum amount of players: " + maxPlayers);
            return;
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        input = pi;
    }
    public PlayerInput input { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }
    public GameObject character { get; set; }
}