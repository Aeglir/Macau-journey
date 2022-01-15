using System;
namespace Managers.Config
{
    [Serializable]
    public class Data : ICloneable
    {
        public bool isFull;

        public string dpi;

        public Data()
        {
            this.isFull = ConfigManager.DefaultFullScreen;
            this.dpi = ConfigManager.DefaultDPI;
        }

        public object Clone()
        {
            Data data = new Data();
            data.isFull = this.isFull;
            data.dpi = this.dpi;
            return data;
        }
    }
}
