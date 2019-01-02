using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    List<CharacterController> characterList = new List<CharacterController>();

    void Start()
    {
        Invoke("LateStart", .12f);
    }

    void LateStart()
    {
        characterList[0].StartTurn();
    }

    public void SubscribeCharacter(CharacterController character)
    {
        characterList.Add(character);
    }

    public void DestroyCharacter(CharacterController character)
    {
        characterList.Remove(character);
    }

    public void EndTurn(CharacterController character)
    {
        if (character != characterList[0])
        {
            throw new System.ArgumentException("Incorrect Character");
        }

        CharacterController temp = characterList[0];
        characterList.RemoveAt(0);
        characterList.Add(temp);
        characterList[0].StartTurn();
    }
}
