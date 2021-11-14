using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ArchiveManager : MonoBehaviour
    {
        private Dictionary<string, ArchiveData> archiveDatas;
        public List<ArchiveDataManager> archiveDataManagers;

        public void init()
        {
            archiveDatas = new Dictionary<string, ArchiveData>();
        }
    }

    abstract public class ArchiveData : MonoBehaviour { }

    abstract public class ArchiveDataManager : MonoBehaviour
    {
        public abstract void loadData(ArchiveData data);

        public abstract string getArchiveType();
    }
}


