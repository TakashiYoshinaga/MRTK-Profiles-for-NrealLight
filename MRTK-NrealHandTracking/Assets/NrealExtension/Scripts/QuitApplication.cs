///
///Copyright © 2021 Takashi Yoshinaga.
///

using System.Collections;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using NRKernal;

public class QuitApplication : MonoBehaviour
{
    [SerializeField]
    NRHandPointer NRHandPointer_R;
    [SerializeField]
    NRHandPointer NRHandPointer_L;

    public void QuitApp()
    {
// MRTK will become not to work correctly after quit through script in Unity Editor.
#if !UNITY_EDITOR
       NRKernal.NRDevice.QuitApp();
#endif
    }

    // [Notice] Do NOT click Exit button. MRTK will become not to work correctly after quit this application.
    //  If project became not to work, please right click Assets folder and [Reimport]. 
    private void Start()
    {
        
            NRKernal.NRHomeMenu.OnHomeMenuShow += new System.Action(() => {
                if (NRHandPointer_R != null)
                {
                    NRHandPointer_R.gameObject.SetActive(true);
                }
                if (NRHandPointer_L != null)
                {
                    NRHandPointer_L.gameObject.SetActive(true);
                }
                PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);
            });
            NRKernal.NRHomeMenu.OnHomeMenuHide += new System.Action(() => {
                if (NRHandPointer_R != null)
                {
                    NRHandPointer_R.gameObject.SetActive(false);
                }
                if (NRHandPointer_L != null)
                {
                    NRHandPointer_L.gameObject.SetActive(false);
                }
                PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOn);

            });
        //StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5);
        NRKernal.NRHomeMenu.Show();
    }
}
