using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ArchiveManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private Dictionary<string, ArchiveData> archiveDatas;
        public List<ArchiveDataManager> archiveDataManagers;

        private void Awake()
        {
            
        }

        public void init()
        {
            archiveDatas = new Dictionary<string, ArchiveData>();
        }

        public bool registerManager(ArchiveDataManager manager)
        {
            return false;
        }
        public ArchiveData getArchiveData(string archiveType)
        {
            if (archiveDatas == null)
            {
                return null;
            }
            return archiveDatas[archiveType];
        }

        public bool initArchiveData(string archiveType, ArchiveData data)
        {
            if (archiveDatas.ContainsKey(archiveType) && archiveDatas == null)
            {
                return false;
            }
            else
            {
                archiveDatas.Add(archiveType, data);
                return true;
            }
        }
    }

    abstract public class ArchiveData : MonoBehaviour { }

    abstract public class ArchiveDataManager : MonoBehaviour
    {

    }
}
