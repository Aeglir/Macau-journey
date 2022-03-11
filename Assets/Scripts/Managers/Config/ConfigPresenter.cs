namespace Managers.Config
{
    public class ConfigPresenter
    {
        private ConfigManager manager;
        #region properties
        public bool IsFull { get => manager.Data.isFull; }
        public string DPI { get => manager.Data.dpi; }
        public float MAINVOLUME { get => manager.Data.mainVolume; }
        public float BGM { get => manager.Data.bgm; }
        public float SE { get => manager.Data.se; }
        #endregion
        public ConfigPresenter(ConfigManager manager)
        {
            this.manager=manager;
            manager.DataChanger.enable();
            manager.Updater.enable(manager.DataChanger.BACKUP);
        }
        /// <summary>
        /// 更改设置项
        /// </summary>
        /// <param name="type">待更改类型</param>
        /// <param name="o">目的值</param>
        public void ChangeSetting(ConfigManager.TYPE type, System.Object o)
        {
            manager.DataChanger.changeSetting(type, o);
        }
        /// <summary>
        /// 确认变更
        /// </summary>
        public void ConfirmSetting()
        {
            manager.Updater.update();
            manager.DataSaver.save(manager.Data);
        }
        /// <summary>
        /// 取消变更
        /// </summary>
        public void CancelSetting()
        {
            manager.Updater.apply();
        }
    }
}