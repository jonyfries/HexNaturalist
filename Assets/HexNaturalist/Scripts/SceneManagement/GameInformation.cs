using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : Singleton<GameInformation>
{
    public CharacterName characterName = CharacterName.Female;
}

public enum CharacterName { Male, Female };