using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuButtons : MonoBehaviour
{
    private Transform redCheckpoint;
    private Transform blueCheckpoint;
    
    void Awake()
    {
    redCheckpoint = GameObject.FindWithTag("RedCheckpoint").GetComponent<Transform>();
    blueCheckpoint = GameObject.FindWithTag("BlueCheckpoint").GetComponent<Transform>();
    }

    public void ButtonQuitGame()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }

    public void ToggleFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen set to " + !isFullscreen);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Loading Scene: " + SceneManager.GetActiveScene().ToString());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restarted Level");
    }

    public void RestartCheckpoint()
    {
        GameObject.FindWithTag("RedPlayer").transform.position = redCheckpoint.position;
        GameObject.FindWithTag("BluePlayer").transform.position = blueCheckpoint.position;
        Debug.Log("Sent to Last Checkpoint");
    }

}