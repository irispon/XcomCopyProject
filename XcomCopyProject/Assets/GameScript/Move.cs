﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{

    public float range;
    public Image moveRange;
    public Vector3 position;
    public Camera mainCamera;
    public Canvas destiCanvas;
    RaycastHit hit;
    Ray ray;
    void Start()
    {
        moveRange.rectTransform.sizeDelta = new Vector2(range*2, range*2);
        mainCamera = CameraManager.GetInstance().mainCamera;
    }

    // Update is called once per frame
    void Update()
    {

        if (CameraManager.GetInstance().mainCamera == true)
        {
            if(destiCanvas==false)
            {
                destiCanvas.enabled = true;
                moveRange.enabled = true;
            }


            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Input.mousePosition: " + Input.mousePosition);
            ////Ability 1 Inputs
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            //{
            //    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            //}

            //Ability 2 Inputs
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject != this.gameObject)
                {

                    position = hit.point;
                }
            }



            //     Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
            //   ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);

            //Ability 2 Canvas Inputs
            var hitPosDir = (hit.point - transform.position).normalized;
            float distance = Vector3.Distance(hit.point, transform.position);

            float fixdistance = Mathf.Min(distance, range);


            var newHitPos = transform.position + hitPosDir * fixdistance;
            ray = new Ray(newHitPos + new Vector3(0, 200, 0), Vector3.down);
            //보정
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject != this.gameObject)
                {
                    Debug.Log("hit.point: " + position);
               
                    newHitPos = hit.point;
                    newHitPos += new Vector3(0, 0.2f, 0);
                }
                else
                {
                    newHitPos = transform.position;
                }
        
            }


            destiCanvas.transform.position = (newHitPos);

        }
        else
        {
            if (destiCanvas == true)
            {
                destiCanvas.enabled = false;
                moveRange.enabled = false;
            }

        }

    }
}
