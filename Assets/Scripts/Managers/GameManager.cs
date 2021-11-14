using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region c# properties

        private static GameManager instance = null;

        public static GameManager Instance
        {
            get{
                return instance;
            }
        }

        #endregion

        #region c# vriables
        public LogManager logManager;

        public ArchiveManager archiveManager;

        #endregion
        private void Awake() {
            if(instance)
            {
                DestroyImmediate(gameObject);
                return;
            }

            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public bool gameSave(){
            return false;
        }

        public bool gameLoad(){
            return false;
        }
    }
}
