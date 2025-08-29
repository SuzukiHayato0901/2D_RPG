using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject����Actor�𐶐����郆�[�e�B���e�B
/// </summary>
public static class ActorFactory
{
    public static Actor Create(CharacterData data)
    {
        return new Actor(
            data.characterName,
            data.maxHp,
            data.atk,
            data.def,
            data.speed
        );
    }
}
