using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public CharacterController playerController;
    public Canvas menu;
    public Canvas gameHUD;

    /// <summary>
    /// Closes the game entirely.
    /// </summary>
    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("HexNaturalist/Scenes/Game", LoadSceneMode.Single);
    }

    /// <summary>
    /// Returns to the main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("HexNaturalist/Scenes/MainMenu", LoadSceneMode.Single);
    }

    /// <summary>
    /// Closes or displays the game menu.
    /// </summary>
    /// <param name="showGameMenu">True to show game menu, false to hide.</param>
    public void GameMenu(bool showGameMenu)
    {
        playerController.enabled = !showGameMenu;
        gameHUD.enabled = !showGameMenu;
        menu.enabled = showGameMenu;
    }
}
