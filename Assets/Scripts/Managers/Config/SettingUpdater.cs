using UnityEngine;

namespace Managers.Config
{
    public class SettingUpdater
    {
        private Data data;
        private Data bakup;
        #region init
        public SettingUpdater(Data data)
        {
            this.data = data;
        }

        public void enable(Data bakup)
        {
            this.bakup = bakup;
        }

        public void disable()
        {
            bakup = null;
        }
        #endregion
        public void update()
        {
            if (data.isFull != bakup.isFull)
            {
                data.isFull = bakup.isFull;
            }
            if (!data.dpi.Equals(bakup.dpi))
            {
                data.dpi = bakup.dpi;
            }
            apply();
        }

        public void apply()
        {
            string[] dpi = data.dpi.Split('X');
            int width = int.Parse(dpi[0]);
            int height = int.Parse(dpi[1]);
            Screen.SetResolution(width, height, data.isFull);
        }
    }
}