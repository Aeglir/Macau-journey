using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers.Config
{
    public class SettingUpdater
    {
        #region private field
        private ConfigData data;
        private ConfigData bakup;
        private AudioMixer audioMixer;
        #endregion
        public static float transitionToVolume(float value)
        {
            return ConfigManager.StartVolume + 1 - (float)Math.Exp(ConfigManager.DefaultVolumeFactor - value / 100 * ConfigManager.DefaultVolumeFactor);
        }
        public SettingUpdater(ConfigData data, AudioMixer audioMixer)
        {
            this.data = data;
            this.audioMixer = audioMixer;
        }
        /// <summary>
        /// 初始化设置更改
        /// </summary>
        /// <param name="bakup">目的设置数据</param>
        public void enable(ConfigData bakup)
        {
            this.bakup = bakup;
        }
        /// <summary>
        /// 关闭设置更改
        /// </summary>
        public void disable()
        {
            bakup = null;
        }
        /// <summary>
        /// 更新设置数据
        /// </summary>
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
            if (data.mainVolume != bakup.mainVolume)
            {
                data.mainVolume = bakup.mainVolume;
            }
            if (data.bgm != bakup.bgm)
            {
                data.bgm = bakup.bgm;
            }
            if (data.se != bakup.se)
            {
                data.se = bakup.se;
            }
            apply();
        }
        /// <summary>
        /// 应用更改
        /// </summary>
        public void apply()
        {
            string[] dpi = data.dpi.Split('X');
            int width = int.Parse(dpi[0]);
            int height = int.Parse(dpi[1]);
            Screen.SetResolution(width, height, data.isFull);
            audioMixer.SetFloat("MAINVolume", transitionToVolume(data.mainVolume));
            audioMixer.SetFloat("BGMVolume", transitionToVolume(data.bgm));
            audioMixer.SetFloat("SEVolume", transitionToVolume(data.se));
        }
    }
}