using UnityEngine;
using UnityEngine.UI;

public class CircleButtons : MonoBehaviour
{
    public System.Collections.Generic.List<GameObject> objs;

    public void SetBlinIndex(int index)
    {
        objs[index].transform.SetSiblingIndex(index);
    }

    public void ResetBlinIndex(int index)
    {
        objs[index].transform.SetAsLastSibling();
    }
}
