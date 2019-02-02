using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetup : MonoBehaviour
{
    public Hex location;
    public GameMenu menu;
    public UnityEngine.UI.Text actionText;
    public UnityEngine.UI.Text suppliesText;
    public CharacterName characterName;
    public List<GameObject> characterPrefabs;
    public CameraFollow camera;

    void Awake()
    {
        characterName = GameInformation.Instance.characterName;

        // Create the player
        GameObject character = Instantiate(characterPrefabs[(int)characterName]) as GameObject;

        //setup the player's location
        character.GetComponent<CharacterMovement>().SetCurrentHex(location);

        //setup player controller
        PlayerController controller = character.GetComponent<PlayerController>();
        controller.actionText = actionText;
        controller.suppliesText = suppliesText;
        controller.menu = menu;

        //setup the game menu
        menu.playerController = controller;

        //setup the camera
        camera.target = character.transform;
    }
}
