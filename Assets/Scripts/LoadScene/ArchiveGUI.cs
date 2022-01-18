using Managers;
using Managers.Archive;
using UnityEditor;
using UnityEngine;
namespace ArchiveGUI
{
    public class ArchiveGUI : MonoBehaviour
    {
        public readonly static string ItemPath = "Resources/Prefabs/LoadScene/Item";
        public readonly static int ItemCount = 20;
        private ArchivePresenter presenter;
        [Header("Item预制体")]
        public GameObject Item;
        [Header("默认存档图片")]
        public Sprite DefualtImage;
        [Header("是否为加载界面")]
        public bool isLoad;
        private void OnEnable()
        {
            ArchiveManager manager = ArchiveManager.Instance;
            if (manager == null)
            {
                return;
            }
            presenter = manager.GetPresenter(isLoad);
            addContent();
        }
        private void OnDisable()
        {
            presenter = null;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i));
            }
        }
        private void addContent()
        {
            int count = presenter.getCount();
            for (int i = 0; i < count; i++)
            {
                createItem(i, false);
            }
            if (count < ItemCount)
            {
                for (int i = count; i < ItemCount; i++)
                {
                    createItem(i, true);
                }
            }
        }
        private void createItem(int tag, bool isNull)
        {
            GameObject item = Instantiate(Item);
            item.transform.SetParent(gameObject.transform, false);
            initItem(tag, item.GetComponent<ArchiveItem>(), isNull);
        }
        private void initItem(int index, ArchiveItem item, bool isNull)
        {
            if (isNull || presenter.isInvalid(index))
            {
                item.initItem(getImageSprite(-1), null, null, index, d =>
                {
                    presenter.cilckAction(d);
                });
            }
            else
            {
                item.initItem(getImageSprite(-1), presenter.getName(index), null, index, d =>
                {
                    presenter.cilckAction(d);
                });
            }
        }
        public void finish()
        {
            presenter.finish();
        }
        private Sprite getImageSprite(int tag)
        {
            if (tag == -1)
            {
                return DefualtImage;
            }
            return null;
        }
    }
}