using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : Singleton<TurnManager>
{
    public GameObject endTurnButton;
    public CharacterController player;

    List<CharacterController> characterList = new List<CharacterController>();

    /// <summary>
    /// Setup the turn manager
    /// </summary>
    void Start()
    {
        Invoke("LateStart", .12f);
        endTurnButton.SetActive(false);
    }

    /// <summary>
    /// Start the first character's turn
    /// </summary>
    void LateStart()
    {
        StartTurn(characterList[0]);
    }

    /// <summary>
    /// Remove character from the turn order.
    /// </summary>
    /// <param name="character">Character to remove.</param>
    public void DestroyCharacter(CharacterController character)
    {
        characterList.Remove(character);
    }

    /// <summary>
    /// End the current character's turn and start the next turn.
    /// </summary>
    /// <param name="character">The character's turn which is ending.</param>
    public void EndTurn(CharacterController character)
    {
        if (character != characterList[0])
        {
            throw new System.ArgumentException("Incorrect Character");
        }

        CharacterController temp = characterList[0];
        characterList.RemoveAt(0);
        characterList.Add(temp);
        StartTurn(characterList[0]);
    }

    /// <summary>
    /// Start a character's turn
    /// </summary>
    /// <param name="character">The character who's turn is starting</param>
    private void StartTurn(CharacterController character)
    {
        character.StartTurn();
        endTurnButton.SetActive(character == player);
    }

    /// <summary>
    /// Add a character to the turn order.
    /// </summary>
    /// <param name="character">Character to add.</param>
    public void SubscribeCharacter(CharacterController character)
    {
        characterList.Add(character);
        character.transform.parent = transform;
    }

    /// <summary>
    /// Handles when the player pushes the end turn button.
    /// </summary>
    public void EndTurnButtonPress()
    {
        player.EndTurn();
    }
}
