using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utill
{
    public class CameraContorler :MonoBehaviour
    {
        public float fiexdZ;
        public float fiexdY;
        public float fiexdX;
        public float fiexdRotationX;
        public float fiexdRotationY;
        public float fiexdRotationZ;
        GameObject focuse;
        Camera mainCamera;
        Vector3 focusePosition;
        Vector3 focuseRotation;
        IEnumerator focusing;
        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }
        public void SetFocus(GameObject focuse,Action callBack=null)
        {
            this.focuse = focuse;
            if (focusing != null)
            {
                StopCoroutine(focusing);
            }
            focusing = Focusing(callBack);
            StartCoroutine(focusing);

        }
        private IEnumerator Focusing(Action callBack = null)
        {
            while (true)
            {
                focusePosition = focuse.transform.position + new Vector3(fiexdX, fiexdY, fiexdZ);
                focuseRotation = focuse.transform.rotation.eulerAngles + new Vector3(fiexdRotationX, fiexdRotationY,fiexdRotationZ);


                if ((focuse != null && focuse.transform.position != mainCamera.transform.position) || (focuse.transform.rotation.eulerAngles == transform.rotation.eulerAngles))
                {
                    transform.position = Vector3.Lerp(transform.position, focusePosition, Time.deltaTime * 2f);
                    transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(focuseRotation), Time.deltaTime * 2f);
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    if (callBack != null)
                        callBack();
                    yield break;
                }

            }


        }

    }
}

