using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Move : SingletonObject<Move>
{

    float range;
    public Image moveRange;
    public Vector3 position;
    Camera mainCamera;
    public Canvas destiCanvas;
    RaycastHit hit;
    Ray ray;
    IEnumerator move;
    void Start()
    {
        gameObject.SetActive(false);
        mainCamera = CameraManager.GetInstance().mainCamera;
    }

    // Update is called once per frame
    /// <summary>
    /// 유닛 입력
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    /// 
    void Set(Character unit)
    {
  
       // Debug.Log(unit.name);
        transform.SetParent(unit.transform);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        range = unit.status.moveRange;
        moveRange.rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
  
    }
    public void GetTurn(Character unit)
    {
        Set(unit);
        gameObject.SetActive(true);
        if (move != null)
            StopCoroutine(move);
        move = MoveTurn(unit);
        StartCoroutine(move);
    }
    public IEnumerator MoveTurn(Character unit)
    {
        
        while (true&&unit.status.movePoint>0)
        {
            range = unit.status.moveRange;
            moveRange.rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
            if (CameraManager.GetInstance().mainCamera == true&&unit.moving==false&&!unit.IsAttackMode())
            {
     
                    destiCanvas.enabled = true;
                    moveRange.enabled = true;



                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
               // Debug.Log("Input.mousePosition: " + Input.mousePosition);
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
                     //   Debug.Log("hit.point: " + position);

                        newHitPos = hit.point;
                        newHitPos += new Vector3(0, 0.2f, 0);
                    }
                    else
                    {
                        newHitPos = transform.position;
                    }

                }


                destiCanvas.transform.position = (newHitPos);
                // Debug.Log("최종값"+newHitPos);
                if (Input.GetMouseButtonDown(1))
                {
                    unit.status.movePoint -= 1;
                    //이 부분에 움직이는 거 넣어주면 될듯
                    unit.MoveTo(newHitPos);
                }

            }
            else
            {
               
                    destiCanvas.enabled = false;
                    moveRange.enabled = false;
                

            }



            yield return new WaitForFixedUpdate();

        }
        gameObject.SetActive(false);


    }

}
