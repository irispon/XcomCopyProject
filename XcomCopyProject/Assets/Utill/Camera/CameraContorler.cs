using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utill
{
    public class CameraContorler :MonoBehaviour
    {
        public float fiexdZ;
        public float fiexdX;
        GameObject focuse;
        Camera mainCamera;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }
        public void SetFocus(GameObject focuse)
        {
            this.focuse = focuse;

        }
        private void FixedUpdate()
        {
            if(focuse!=null&&focuse.transform.position != mainCamera.transform.position)
            transform.position = Vector3.Lerp(transform.position, new Vector3(focuse.transform.position.x, transform.position.y,focuse.transform.position.z) +new Vector3(fiexdX, 0,fiexdZ), Time.deltaTime * 2f);
        }

    }
}

