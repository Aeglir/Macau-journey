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

        public void enable()
        {
            backup = data.Clone() as Data;
        }

        public void disable()
        {
            backup = null;
        }
        #endregion
        public void changeSetting(ConfigManager.TYPE type, System.Object t)
        {
            if (backup == null)
                return;
            switch (type)
            {
                case ConfigManager.TYPE.FULLSCREEN:
                    switchFullscreen(t);
                    break;
                case ConfigManager.TYPE.DPI:
                    setDpi(t);
                    break;
                default:
                    break;
            }
        }
        private void setDpi(System.Object o)
        {
            string dpi = o as string;
            if (dpi != null)
            {
                backup.dpi = dpi;
            }
        }
        private void switchFullscreen(System.Object o)
        {
            backup.isFull = (bool)o;
        }
    }

}