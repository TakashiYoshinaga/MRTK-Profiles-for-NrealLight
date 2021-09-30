//[Reference]
//https://hacchi-man.hatenablog.com/entry/2020/04/01/220000
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public static class Startup
{
#if UNITY_EDITOR
    private class StartUpData : ScriptableSingleton<StartUpData>
    {
        [SerializeField]
        private int _callCount;
        public bool IsStartUp()
        {
            return _callCount++ == 1;
        }
    }
    //private static readonly string TMP_FILE_PATH = "Temp/StartupLandmark";
    static Startup()
    {
        if (!StartUpData.instance.IsStartUp())
            return;
        //Force loading of NRSK folder at Unity Editor startup
        string pathRsc = "Assets/NRSDK";
        AssetDatabase.ImportAsset(pathRsc, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
        Debug.Log("load nrsdk");

    }
#endif
}