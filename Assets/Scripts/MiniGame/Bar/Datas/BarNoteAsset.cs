using UnityEngine;
namespace MiniGame.Bar.Datas{
    [CreateAssetMenu(fileName = "BarNoteAsset", menuName = "MiniGame/Bar/BarNoteAsset")]
    public class BarNoteAsset : ScriptableObject
    {
        [SerializeField]
        [Header("笔记提示")]
        private Sprite[] notetips;
        public Sprite[] Notetips { get => notetips;  }
    }
}
