using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class MiniGameManager
    {
        public void VolunteerGameStart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }
    }
}
