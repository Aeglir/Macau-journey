using UnityEngine;

namespace Managers.Config
{
    public class DataChanger
    {
        private Data data;

        private Data backup;

        public Data BACKUP
        {
            get
            {
                return backup;
            }
        }
        #region init
        public DataChanger(Data data)
        {
            this.data = data;
        }
        /// <summary>
        /// 初始化设置变更
        /// </summary>
        public void enable()
        {
            backup = data.Clone() as Data;
        }
        /// <summary>
        /// 关闭设置变更
        /// </summary>
        public void disable()
        {
            backup = null;
        }
        #endregion
        /// <summary>
        /// 暂存更改
        /// </summary>
        /// <param name="type">待更改设置项</param>
        /// <param name="t">目的值</param>
        public void changeSetting(ConfigManager.TYPE type, System.Object t)
        {
            if (backup == null)
                backup = data.Clone() as Data;
            switch (type)
            {
                case ConfigManager.TYPE.FULLSCREEN:
                    switchFullscreen(t);
                    break;
                case ConfigManager.TYPE.DPI:
                    setDpi(t);
                    break;
                case ConfigManager.TYPE.MAINVOLUME:
                    setMainVolume(t);
                    break;
                case ConfigManager.TYPE.BGM:
                    setBGM(t);
                    break;
                case ConfigManager.TYPE.SE:
                    setSE(t);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 暂存dpi
        /// </summary>
        /// <param name="o">值</param>
        private void setDpi(System.Object o)
        {
            string dpi = o as string;
            if (dpi != null)
            {
                backup.dpi = dpi;
            }
        }
        /// <summary>
        /// 暂存全屏设置
        /// </summary>
        /// <param name="o">值</param>
        private void switchFullscreen(System.Object o)
        {
            backup.isFull = (bool)o;
        }
        /// <summary>
        /// 暂存主音量
        /// </summary>
        /// <param name="o">值</param>
        private void setMainVolume(System.Object o)
        {
            backup.mainVolume = (float)o;
        }
        /// <summary>
        /// 暂存背景音量
        /// </summary>
        /// <param name="o">值</param>
        private void setBGM(System.Object o)
        {
            backup.bgm = (float)o;
        }
        /// <summary>
        /// 暂存效果音量
        /// </summary>
        /// <param name="o">值</param>
        private void setSE(System.Object o)
        {
            backup.se = (float)o;
        }
    }

}