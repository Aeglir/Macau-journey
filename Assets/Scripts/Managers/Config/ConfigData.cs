using System;
namespace Managers.Config
{
    [Serializable]
    public class ConfigData : ICloneable
    {
        public bool isFull;
        public string dpi;
        public float mainVolume;
        public float bgm;
        public float se;
        public ConfigData()
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
            ConfigData data = new ConfigData();
            data.isFull = this.isFull;
            data.dpi = this.dpi;
            return data;
        }
    }
}
