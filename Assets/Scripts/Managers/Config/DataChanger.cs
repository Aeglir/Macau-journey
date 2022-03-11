using System;
namespace Managers.Config
{
    public class DataChanger
    {
        #region private fields
        private ConfigData data;
        private ConfigData backup;
        #endregion
        #region properties
        public ConfigData BACKUP { get => backup; }
        private Object Dpi
        {
            set
            {
                string dpi = value as string;
                if (dpi != null)
                {
                    backup.dpi = dpi;
                }
            }
        }
        private Object isFull { set => backup.isFull = (bool)value; }
        private Object MainVolume { set => backup.mainVolume = (float)value; }
        private Object BGM { set => backup.bgm = (float)value; }
        private Object SE { set => backup.se = (float)value; }
        #endregion=
        public DataChanger(ConfigData data)
        {
            this.data = data;
        }
        public void enable()
        {
            backup = data.Clone() as ConfigData;
        }
        public void disable()
        {
            backup = null;
        }
        public void changeSetting(ConfigManager.TYPE type, Object t)
        {
            if (backup == null)
                backup = data.Clone() as ConfigData;
            switch (type)
            {
                case ConfigManager.TYPE.FULLSCREEN:
                    isFull = t;
                    break;
                case ConfigManager.TYPE.DPI:
                    Dpi = t;
                    break;
                case ConfigManager.TYPE.MAINVOLUME:
                    MainVolume = t;
                    break;
                case ConfigManager.TYPE.BGM:
                    BGM = t;
                    break;
                case ConfigManager.TYPE.SE:
                    SE = t;
                    break;
                default:
                    break;
            }
        }
    }

}