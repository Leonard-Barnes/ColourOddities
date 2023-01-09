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
        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("currentScene"));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.GetInt("tutorial", 1);
        }
        Debug.Log("Loading Scene: " + SceneManager.GetActiveScene().ToString());
    }

    public void RestartLevel()
    {
        PlayerPrefs.DeleteKey("checkpoint_x");
        PlayerPrefs.DeleteKey("checkpoint_y");
        PlayerPrefs.DeleteKey("futureScene");
        PlayerPrefs.DeleteKey("currentScene");
        PlayerPrefs.DeleteKey("checkpointNum");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restarted Level");
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Destroy(GameObject.FindWithTag("GameController"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartCheckpoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Sent to Last Checkpoint");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Loading Main Menu...");
    }
}