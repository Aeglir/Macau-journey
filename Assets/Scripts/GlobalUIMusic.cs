using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUIMusic : MonoBehaviour
{
    public GameObject globalUIMusic;
    GameObject MyUIMusic = null;
    // Start is called before the first frame update
    void Start()
    {
        //�����Ϸ���Ƿ����Ԥ����
        MyUIMusic = GameObject.FindGameObjectWithTag("GlobalUIMusic");
        if (MyUIMusic == null)
        {
            MyUIMusic = (GameObject)Instantiate(globalUIMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
