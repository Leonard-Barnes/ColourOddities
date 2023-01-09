using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoinMessage : MonoBehaviour
{
    private PlayerInput redPlayerInput;
    private PlayerInput bluePlayerInput;
    public GameObject keyboard;
    public GameObject controller;
    public GameObject line;
    public Button play;
    private Vector3 controllerOnly = new Vector3 (-415f, 0, 0);
    private Vector3 keyboardOnly = new Vector3 (415f, 0, 0);


    void Awake()
    {
        redPlayerInput = null;
        bluePlayerInput = null;
        SetTargets();
    }
    void Update()
    {
        if (redPlayerInput != null)
        {
            CheckSchemeRed();
        }
        if (bluePlayerInput != null)
        {
            CheckSchemeBlue();
        }
        if (redPlayerInput != null && bluePlayerInput != null)
        {
            play.interactable = true;
            gameObject.SetActive(false);
        }
        else
        {
            play.interactable = false;
            gameObject.SetActive(true);
        }

    }

    private void CheckSchemeRed()
    {
        if (redPlayerInput.currentControlScheme == "Keyboard")
        {
            keyboard.SetActive(false);
            line.SetActive(false);
            controller.transform.localPosition = controllerOnly;
        }
        else if (redPlayerInput.currentControlScheme == "Gamepad")
        {
            controller.SetActive(false);
            line.SetActive(false);
            keyboard.transform.localPosition = keyboardOnly;
        }
    }

    private void CheckSchemeBlue()
    {
        if (bluePlayerInput.currentControlScheme == "Keyboard")
        {
            keyboard.SetActive(false);
            line.SetActive(false);
            controller.transform.localPosition = controllerOnly;
        }
        else if (bluePlayerInput.currentControlScheme == "Gamepad")
        {
            controller.SetActive(false);
            line.SetActive(false);
            keyboard.transform.localPosition = keyboardOnly;
        }
    }

    public void SetTargets()
    {
        if (GameObject.FindWithTag("RedPlayer") != null)
        {
            redPlayerInput = GameObject.FindWithTag("RedPlayer").GetComponent<PlayerInput>();
            Debug.Log("Red Target Set");
        }
        if (GameObject.FindWithTag("BluePlayer") != null)
        {
            bluePlayerInput = GameObject.FindWithTag("BluePlayer").GetComponent<PlayerInput>();
            Debug.Log("Blue Target Set");
        }
        else
        {
            return;
        }
    }
}
