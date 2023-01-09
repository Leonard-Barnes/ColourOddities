using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour {

    // Reference to Audio Source component
    private AudioListener AudioListener;
    public Slider slider;

    // Music volume variable that will be modified
    // by dragging slider knob
    private float musicVolume;
	
    void Awake()
    {
        SetTargets();
        slider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        if (PlayerPrefs.HasKey("musicVolume") == false)
        {
            slider.value = 0.25f;
            Debug.Log("Setting default volume");
        }
    }

	// Update is called once per frame
	void Update () {

        // Setting volume option of Audio Source to be equal to musicVolume
        AudioListener.volume = musicVolume;
        
	}

    // Method that is called by slider game object
    // This method takes vol value passed by slider
    // and sets it as musicValue
    public void SetVolume(float vol)
    {
        musicVolume = vol;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    private void SetTargets()
    {
        AudioListener = GameObject.FindWithTag("MainCamera").GetComponent<AudioListener>();
    }
}