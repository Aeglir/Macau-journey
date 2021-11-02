using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    static bool isReady;
    // Start is called before the first frame update
    void Start()
    {
        if (isReady)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            isReady = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
