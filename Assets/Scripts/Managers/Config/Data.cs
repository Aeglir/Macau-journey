using System;
namespace Managers.Config
{
    [Serializable]
    public class Data : ICloneable
    {
        public bool isFull;
        public string dpi;
        public float mainVolume;
        public float bgm;
        public float se;
        public Data()
        {
            this.isFull = ConfigManager.DefaultFullScreen;
            this.dpi = ConfigManager.DefaultDPI;
            this.mainVolume = this.bgm = this.se = ConfigManager.DefaultVolume;
        }
        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>返回数据的拷贝</returns>
        public object Clone()
        {
            Data data = new Data();
            data.isFull = this.isFull;
            data.dpi = this.dpi;
            return data;
        }
    }
}
