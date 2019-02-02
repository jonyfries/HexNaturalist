using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MenuHandler
{
    public CharacterController playerController;
    public Canvas menu;
    public Canvas gameHUD;
    public Canvas gameOver;

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
    public void ShowGameMenu(bool showGameMenu)
    {
        playerController.enabled = !showGameMenu;
        gameHUD.enabled = !showGameMenu;
        menu.enabled = showGameMenu;
    }

    public void GameOver()
    {
        playerController.enabled = false;
        gameHUD.enabled = true;
        gameOver.enabled = true;
    }
}
