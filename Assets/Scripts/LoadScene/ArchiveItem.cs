using System;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArchiveGUI
{
    public class ArchiveItem : MonoBehaviour
    {
        public const string DefaultDesctiption = "这个人很懒什么都没有写...";
        public const string DefaultTitle = "这里什么都没有";
        private int archiveTag;
        [Header("存档图片")]
        public Image image;
        [Header("标题文本")]
        public TextMeshProUGUI titleText;
        [Header("描述文本")]
        public TextMeshProUGUI descriptionText;
        [Header("点击按钮")]
        public Button button;
        public void initItem(Sprite image, string titleString, string descriptionString, int archiveTag, UnityAction<int> action)
        {
            this.archiveTag = archiveTag;
            setImage(image);
            setTitleString(titleString);
            setDescriptionString(descriptionString);
            setClickAction(action);
            // gameObject.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
        private void setImage(Sprite image)
        {
            if (image != null)
            {
                this.image.sprite = image;
            }
        }
        private void setTitleString(string titleString)
        {
            if (titleString == null)
            {
                titleText.SetText(DefaultTitle);
            }
            else
            {
                titleText.SetText(titleString);
            }
        }
        private void setDescriptionString(string descriptionString)
        {
            if (descriptionString == null)
            {
                descriptionText.SetText(DefaultDesctiption);
            }
            else
            {
                descriptionText.SetText(descriptionString);
            }
        }
        private void setClickAction(UnityAction<int> action)
        {
            if (action == null)
            {
                return;
            }
            button.onClick.AddListener(() =>
            {
                action.Invoke(archiveTag);
            });
        }
        public void click()
        {
            Debug.Log("click!");
        }
    }
}