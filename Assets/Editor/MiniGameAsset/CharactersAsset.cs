using System.Collections;
using System.Collections.Generic;
using MiniGame.Volunteer;
using UnityEngine;
[CreateAssetMenu(fileName ="CharactersAsset",menuName ="MiniGame/CharactersAsset")]
public class CharactersAsset : ScriptableObject
{
    public Player Player;
    public List<Npc> npcs;
}
