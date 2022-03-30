using System;
namespace Managers.Archive
{
    [Serializable]
    public class ArchiveInfo : ArchiveBank
    {
        public int tag;
        public string name;
        public bool isInvalid;

        public ArchiveInfo(int tag, string name, bool isInvalid)
        {
            this.tag = tag;
            this.name = name;
            this.isInvalid = isInvalid;
        }
    }
}