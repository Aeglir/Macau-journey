namespace Managers.Config
{
    public class ConfigPresenter
    {
        public bool IsFull
        {
            get
            {
                return data.isFull;
            }
        }
        public string DPI
        {
            get
            {
                return data.dpi;
            }
        }
        public float MAINVOLUME
        {
            get
            {
                return data.mainVolume;
            }
        }
        public float BGM
        {
            get
            {
                return data.bgm;
            }
        }
        public float SE
        {
            get
            {
                return data.se;
            }
        }
        private Data data;
        private DataSaver dataSaver;
        private DataChanger dataChanger;
        private SettingUpdater updater;

        public ConfigPresenter(Data data, DataSaver dataSaver, DataChanger dataChanger, SettingUpdater updater)
        {
            this.data = data;
            this.dataSaver = dataSaver;
            this.dataChanger = dataChanger;
            this.updater = updater;
            dataChanger.enable();
            updater.enable(dataChanger.BACKUP);
        }
        /// <summary>
        /// 更改设置项
        /// </summary>
        /// <param name="type">待更改类型</param>
        /// <param name="o">目的值</param>
        public void changeSetting(ConfigManager.TYPE type, System.Object o)
        {
            dataChanger.changeSetting(type, o);
        }
        /// <summary>
        /// 确认变更
        /// </summary>
        public void confirmSetting()
        {
            updater.update();
            dataSaver.save(data);
        }
        /// <summary>
        /// 取消变更
        /// </summary>
        public void cancelSetting()
        {
            updater.apply();
        }
    }
}