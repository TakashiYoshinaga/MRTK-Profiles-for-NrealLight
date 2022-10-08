using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARFukuoka.MixedReality.Toolkit.Nreal.Input
{
    public class AutoMrtkCamSetting : MonoBehaviour
    {
        GameObject m_CameraTarget;
        public float HeadMoveSpeed = 1.0f;
        // Start is called before the first frame update
        void Start()
        {
            var cam=gameObject.GetComponent<Camera>();
            if(cam!=null)
            {
                cam.clearFlags=CameraClearFlags.SolidColor;
                cam.fieldOfView=25;
#if UNITY_EDITOR
                cam.depth=1;
#else
                cam.depth=-1;
#endif
            }
            StartCoroutine(FindNREmulatorCameraTarget());
        }
        IEnumerator FindNREmulatorCameraTarget(){
            yield return new WaitForSeconds(1);
            if (m_CameraTarget == null)
            {
                m_CameraTarget = GameObject.Find("NREmulatorCameraTarget");
            }
        }

#if UNITY_EDITOR
        void  Update()
        {
            if(m_CameraTarget==null){ return;}
            Vector3 pos = m_CameraTarget.transform.position;
            Vector3 p = GetBaseInput();
            p = p * HeadMoveSpeed * Time.deltaTime;
            pos += p;
            m_CameraTarget.transform.position = pos;
        }

         private Vector3 GetBaseInput()
        {
            Vector3 p_Velocity = new Vector3();
            if (UnityEngine.Input.GetKey(KeyCode.Q))
            {
                p_Velocity += m_CameraTarget.transform.up.normalized;
            }
            if (UnityEngine.Input.GetKey(KeyCode.E))
            {
                p_Velocity += -m_CameraTarget.transform.up.normalized;
            }
           
            return p_Velocity;
        }
#endif
    }
}