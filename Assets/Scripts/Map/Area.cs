using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MAP
{
    [RequireComponent(typeof(Image))]
    public class Area : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // public List<>
        private Image img;
        public GameObject hlightedImg;
        public Place[] places;
        public Map map;
        private bool isSelect;
        private void Awake()
        {
            img = GetComponent<Image>();
            img.alphaHitTestMinimumThreshold = 0.5f;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if(map.CurArea!=null)
                map.CurArea.Close();
            map.CurArea=this;
            isSelect=true;
            foreach(var p in places)
            {
                p.RayEnable();
            }
        }

        public void Close(){
            isSelect=false;
            hlightedImg.SetActive(false);
            foreach(var p in places)
            {
                p.RayDisable();
            }
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