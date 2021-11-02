using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMusic : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioSource.Play();
        }
    }
}
