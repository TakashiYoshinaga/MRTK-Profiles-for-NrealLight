//[Reference]
//https://hacchi-man.hatenablog.com/entry/2020/04/01/220000
using UnityEngine;
using UnityEditor;
[InitializeOnLoad]
public static class Startup
{
    private class StartUpData : ScriptableSingleton<StartUpData>
    {
        [SerializeField]
        private int _callCount;
        public bool IsStartUp()
        {
            return _callCount++ == 0;
        }
    }

    static Startup()
    {
        if (!StartUpData.instance.IsStartUp())
            return;
        //Force loading of NRSK folder at Unity Editor startup
        string pathRsc = "Assets/NRSDK";
        AssetDatabase.ImportAsset(pathRsc, ImportAssetOptions.ImportRecursive);
    }
}