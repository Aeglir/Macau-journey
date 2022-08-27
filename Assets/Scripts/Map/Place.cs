using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MAP
{
    [RequireComponent(typeof(Image))]
    public class Place : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Image img;
        public GameObject hlightedImg;
        public Map map;
        public Sprite listImg;
        private bool isSelect;
        private void Awake()
        {
            img = GetComponent<Image>();
        }
        public void RayEnable()
        {
            img.raycastTarget=true;
        }
        public void RayDisable()
        {
            img.raycastTarget=false;
            UnSelect();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if(map.CurPlace!=null)
                map.CurPlace.UnSelect();
            isSelect=true;
            hlightedImg.SetActive(true);
            map.SetListImg(listImg);
            map.CurPlace=this;
        }
        public void UnSelect()
        {
            isSelect=false;
            hlightedImg.SetActive(false);
            map.ListReset();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!isSelect)
                hlightedImg.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!isSelect)
                hlightedImg.SetActive(true);
        }
    }


}