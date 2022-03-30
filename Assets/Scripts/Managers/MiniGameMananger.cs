using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager
{
    public int tag;
    public void VolunteerGameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
    }
}
