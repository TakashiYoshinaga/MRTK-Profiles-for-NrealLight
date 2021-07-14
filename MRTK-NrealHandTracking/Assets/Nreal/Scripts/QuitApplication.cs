using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
public class QuitApplication : MonoBehaviour
{
    public void QuitApp()
    {
        NRDevice.QuitApp();
    }
}
