using System.Collections;
using System.Collections.Generic;
using MiniGame.Volunteer;
using UnityEngine;
[CreateAssetMenu(fileName = "CharactersAsset", menuName = "MiniGame/CharactersAsset")]
public class CharactersAsset : ScriptableObject
{
    [SerializeField]
    [Header("玩家")]
    private MiniGame.Volunteer.CharacterInfo player;
    [SerializeField]
    [Header("玩家响应动画")]
    private Sprite activePlanting;
    [SerializeField]
    [Header("Npc预制体")]
    private GameObject perfabs;
    [SerializeField]
    [Header("NPC预设列表")]
    private List<MiniGame.Volunteer.CharacterInfo> npcs;

    public Sprite ActivePlanting { get => activePlanting; }
    public GameObject Perfabs { get => perfabs; }
    public List<MiniGame.Volunteer.CharacterInfo> Npcs { get => npcs; }
    public MiniGame.Volunteer.CharacterInfo Player { get => player; }
}
