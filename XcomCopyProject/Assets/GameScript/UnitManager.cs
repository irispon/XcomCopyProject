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
        public bool attackMode = false;

        int index = -1;


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
                if (h != 0&&!selectUnit.ablityMode)
                {
                    selectUnit.DiSelect();
                 
                    //selectUnit = null;
                    if (attackMode==true)
                    attackMode = false;
                }
            }//스택으로 명령을 기억해놨어야 했음.
            
            if (Input.GetButtonUp("tab"))
            {

                if (units.Count > 0)
                {

                    if (selectUnit != null && !selectUnit.ablityMode)
                    {
                        index++;

                        if (index >= units.Count)
                        {
                            index = 0;
                        }

                        selectUnit.isSelect = false;
                        selectUnit = units[index];
                        selectUnit.Select();
                    }else if (selectUnit == null)
                    {
                        index++;

                        if (index >= units.Count)
                        {
                            index = 0;
                        }

                        selectUnit = units[index];
                        selectUnit.Select();
                    }
 

             
                  

                }

         
            }else if (Input.GetButtonUp("one") && !selectUnit.ablityMode)
            {
                if (selectUnit == null&&units.Count>0)
                    selectUnit = units[index];
                selectUnit.AttackMode();
                if (attackMode == false && !selectUnit.moving)
                {
                    attackMode = true;

                }
            }


        }
    }



}
