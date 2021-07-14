using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
public class QuitApplication : MonoBehaviour
{
    public void QuitApp()
    {
// MRTK will become not to work correctly after quit through script in Unity Editor.
#if !UNITY_EDITOR
        NRDevice.QuitApp();
#endif
    }
}
