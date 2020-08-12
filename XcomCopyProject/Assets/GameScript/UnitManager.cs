﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xcopy
{
    public class UnitManager : SingletonObject<UnitManager>
    {
      // public Dictionary<string, Character> units;
       Character selectUnit;
       List<Character> units;
       Queue<Character> enemunits;
        int index = -1;
       bool keyState = true;

        public override void Init()
        {
       //     units = new Dictionary<string, Character>();
            selectUnit = null;
            units = new List<Character>();
            enemunits = new Queue<Character>();
        }

        public void SelectUnit(Character unit)
        {

            if (selectUnit != unit)
            {
                if (selectUnit != null)
                {
                    selectUnit.DiSelect();
                   
                }
                selectUnit = unit;
                selectUnit.Select();
                index= units.IndexOf(selectUnit);
                Debug.Log("인덱스" + index);
            }

            

        }
        public void AddUnit(Character unit)
        {
            units.Add(unit);
        }
        public void Update()
        {
            if (selectUnit != null)
            {
                float h = Input.GetAxis("escape");
                if (h != 0)
                {
                    selectUnit.DiSelect();
                    //selectUnit = null;
                }
            }

            if (Input.GetButton("tab"))
            {

                if (units.Count > 0 && keyState)
                {
                    index++;
           
                    if (index >= units.Count)
                    {
                        index = 0;
                    }
                    Debug.Log("인덱스" + index);
                    selectUnit = units[index];
                    selectUnit.Select();
             
                  

                }

                keyState = false;
            }
            else
            {
                keyState = true;
            }


        }
    }



}
