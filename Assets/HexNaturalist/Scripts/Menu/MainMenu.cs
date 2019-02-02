using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MenuHandler
{
    public float rotateTime = 3;
    public Transform camera;
    public Transform lookTarget;
    public Canvas mainMenu;

    Quaternion startRotation;
    Quaternion endRotation;
    float rotateTimer = 0;

    void Start()
    {
        startRotation = camera.rotation;
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("HexNaturalist/Scenes/Game", LoadSceneMode.Single);
    }

    /// <summary>
    /// Starts rotating the camera to allow player to select their character
    /// </summary>
    public void StartCharacterSelect()
    {
        enabled = true;
        mainMenu.enabled = false;
        endRotation = Quaternion.LookRotation(new Vector3(1, 0, 1), Vector3.up);
    }

    /// <summary>
    /// Process camera rotations
    /// </summary>
    public void Update()
    {
        rotateTimer += Time.deltaTime;
        camera.rotation = Quaternion.Slerp(startRotation, endRotation, rotateTimer / rotateTime);
        if (camera.rotation == endRotation)
        {
            enabled = false;
            rotateTimer = 0;
        }
    }
}
