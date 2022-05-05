using UnityEngine;
public class DBContorller
{
    private System.Collections.Generic.Dictionary<string, AssetBundle> assets = new System.Collections.Generic.Dictionary<string, AssetBundle>();
    private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, Object>> resources = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, Object>>();
    private AssetBundle mainAB;
    private AssetBundleManifest manifest;
    private bool isLoading;
    private string AssetBundlePath
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    private string MainAssetBundleName
    {
        get
        {
            return "StandaloneWindows";
        }
    }
    private void OnDestroy()
    {
        ClearAb();
    }
    internal async System.Threading.Tasks.Task LoadMainAssetBundle()
    {
        if (isLoading || mainAB != null)
        {
            while (isLoading)
                await System.Threading.Tasks.Task.Yield();
            return;
        }
        isLoading = true;
        var task1 = AssetBundle.LoadFromFileAsync(AssetBundlePath + MainAssetBundleName);
        while (!task1.isDone) await System.Threading.Tasks.Task.Yield();
        mainAB = task1.assetBundle;
        var task2 = mainAB.LoadAssetAsync<AssetBundleManifest>("AssetBundleManifest");
        while (!task2.isDone) await System.Threading.Tasks.Task.Yield();
        manifest = task2.asset as AssetBundleManifest;
        isLoading = false;
    }
    internal async System.Threading.Tasks.Task<T> LoadAssetBundleAsync<T>(string abName, string resName) where T : Object
    {
        await LoadMainAssetBundle();
        if (!assets.ContainsKey(abName))
        {
            string[] strs = manifest.GetAllDependencies(abName);
            foreach (string str in strs)
            {
                if (!assets.ContainsKey(str))
                {
                    var t = AssetBundle.LoadFromFileAsync(AssetBundlePath + str);
                    while (!t.isDone) await System.Threading.Tasks.Task.Yield();
                    assets.Add(str, t.assetBundle);
                }
            }
            var task = AssetBundle.LoadFromFileAsync(AssetBundlePath + abName);
            while (!task.isDone) await System.Threading.Tasks.Task.Yield();
            assets.Add(abName, task.assetBundle);
            resources.Add(abName, new System.Collections.Generic.Dictionary<string, Object>());
        }
        if (resources[abName].ContainsKey(resName))
            return resources[abName][resName] as T;
        if (assets[abName].Contains(resName))
        {
            var task1 = assets[abName].LoadAssetAsync<T>(resName);
            while (!task1.isDone) await System.Threading.Tasks.Task.Yield();
            resources[abName].Add(resName, task1.asset);
            return resources[abName][resName] as T;
        }
        return default(T);
    }
    internal void Unload(string abName)
    {
        if (!assets.ContainsKey(abName)) return;
        assets[abName].Unload(false);
        assets.Remove(abName);
    }

    internal void ClearAb()
    {
        foreach (var ab in assets)
        {
            ab.Value.Unload(false);
        }
        assets.Clear();
        mainAB.Unload(false);
        mainAB = null;
        manifest = null;
    }
}