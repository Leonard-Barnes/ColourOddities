using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip walk, jump, landing, death;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        walk = Resources.Load<AudioClip> ("walking");
        jump = Resources.Load<AudioClip> ("jump");
        landing = Resources.Load<AudioClip> ("landing");
        death = Resources.Load<AudioClip> ("death");
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerSound(string clip)
    {
        switch (clip)
        {
            case "walking":
                audioSource.PlayOneShot(walk);
                break;
            case "jump":
                audioSource.PlayOneShot(jump);
                break;
            case "landing":
                audioSource.PlayOneShot(landing);
                break;
            case "death":
                audioSource.PlayOneShot(death);
                break;
        }
    }
}
