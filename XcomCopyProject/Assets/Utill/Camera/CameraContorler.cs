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
        Camera mainCamera;

        IEnumerator focusing;
        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }
        public void SetFocus(GameObject focuse,Action callBack=null)
        {

            if (focusing != null)
            {
                StopCoroutine(focusing);
            }
            focusing = Focusing(focuse, callBack);
            StartCoroutine(focusing);

        }

        public void CamToCam(Camera camera,bool shadow = false)
        {

            if (focusing != null)
            {
                StopCoroutine(focusing);
            }
            focusing = CameraToCamera(camera, shadow);
            StartCoroutine(focusing);

        }
        private IEnumerator Focusing(GameObject focuse, Action callBack = null)
        {
            Vector3 focusePosition;
            Vector3 focuseRotation;
            while (true)
            {
                focusePosition = focuse.transform.position + new Vector3(fiexdX, fiexdY, fiexdZ);
                focuseRotation = focuse.transform.rotation.eulerAngles + new Vector3(fiexdRotationX, fiexdRotationY,fiexdRotationZ);


                if ((focuse.transform.position != mainCamera.transform.position) || (focuse.transform.rotation.eulerAngles == transform.rotation.eulerAngles))
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
        private IEnumerator CameraToCamera(Camera camera,bool shadow=false)
        {
            if (shadow == false)
            {
            Vector3 mainCameraPosition = mainCamera.transform.position;
            Vector3 mainCameraRotation = mainCamera.transform.rotation.eulerAngles;

            mainCamera.transform.position = camera.transform.position;
            mainCamera.transform.rotation = camera.transform.rotation;

         
                mainCamera.enabled = true;
                camera.enabled = false;
       


            Vector3 lerpPosition;
            Quaternion lerpRotation;
        
            while (true)
            {



                if ((mainCameraPosition != mainCamera.transform.position) || (mainCameraRotation == transform.rotation.eulerAngles))
                {
                    lerpPosition = Vector3.Lerp(transform.position, mainCameraPosition, Time.deltaTime * 2f);
                    lerpRotation= Quaternion.Slerp(transform.rotation, Quaternion.Euler(mainCameraRotation), Time.deltaTime * 2f);
                    transform.position = lerpPosition;
                    transform.rotation = lerpRotation;
                    camera.transform.position = lerpPosition;
                    camera.transform.rotation = lerpRotation;
                    yield return new WaitForFixedUpdate();
                }
                else
                {

                    yield break;
                }

            }
            }
            else
            {
                while (true)
                {
                    mainCamera.transform.position = camera.transform.position;
                    mainCamera.transform.rotation = camera.transform.rotation;
                    yield return new WaitForSeconds(0.2f);
                }


         

            }

        }


    }
}

