using System.Threading.Tasks;
using Managers.Config;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    public class ConfigManager : MonoBehaviour
    {
        private static ConfigManager instance = null;
        public readonly static bool DefaultFullScreen = false;
        public readonly static string HD = "1920X1080";
        public readonly static string DefaultDPI = "1280X720";
        public static ConfigManager Instance
        {
            get => instance;
        }
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
        public enum TYPE
        {
            FULLSCREEN,
            DPI,
        };
        [SerializeField]
        private Data data;
        private DataSaver dataSaver;
        private DataChanger dataChanger;
        private SettingUpdater updater;
        private void Awake()
        {
            instance = this;

            dataSaver = new DataSaver();
            dataSaver.setLoadAction(d =>
            {
                data = d;
                dataChanger = new DataChanger(data);
                updater = new SettingUpdater(data);
                updater.apply();
            });
            dataSaver.setSaveAction(() =>
            {
                disable();
            });
            dataSaver.load();
        }
        public void init()
        {
            dataChanger.enable();
            updater.enable(dataChanger.BACKUP);
        }
        private void disable()
        {
            dataChanger.disable();
            updater.disable();
        }
        public void changeSetting(ConfigManager.TYPE type, System.Object o)
        {
            dataChanger.changeSetting(type, o);
        }
        public void confirmSetting()
        {
            updater.update();
            dataSaver.save(data);
        }
        public void cancelSetting()
        {
            disable();
        }
    }
}