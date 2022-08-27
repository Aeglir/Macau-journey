using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MAP
{
    [RequireComponent(typeof(Image))]
    public class Work : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject hlightedImg;
        public int SceneTag;
        private bool isSelect;
        public Map map;
        public Sprite listImg;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (map.CurWork != null)
                map.CurWork.UnSelect();
            isSelect = true;
            map.CurWork=this;
            map.SetListImg(listImg);
            hlightedImg.SetActive(true);
        }

        public void UnSelect()
        {
            hlightedImg.SetActive(false);
            map.ListReset();
            isSelect = false;
        }

        public void ToWork()
        {
            if(SceneTag>=0&&SceneTag<UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneTag);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isSelect)
                hlightedImg.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isSelect)
                hlightedImg.SetActive(false);
        }
    }
}
