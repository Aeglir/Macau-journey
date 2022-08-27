using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGame.Bar
{
    public class ClickChecker : MonoBehaviour
    {
        public SceneAudioManager audioManager;
        public InputAction inputAction;
        public void enable()
        {
            inputAction.performed += (c) =>
            {
                audioManager.Play(MiniGame.Bar.GobalSetting.CLICKNAME);
            };
            inputAction.Enable();
        }

        private void OnDestroy() {
            inputAction.Disable();
            inputAction.Dispose();
        }
    }
}
