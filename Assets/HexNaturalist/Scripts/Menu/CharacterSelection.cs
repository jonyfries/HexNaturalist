using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public CharacterName character;
    public GameInformation gameInfo;
    public MainMenu menuControls;

    void OnMouseUp()
    {
        gameInfo.characterName = character;
        menuControls.StartGame();
    }
}
