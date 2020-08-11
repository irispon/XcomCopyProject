using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xcopy
{
    public class UnitManager : SingletonObject<UnitManager>
    {
      // public Dictionary<string, Character> units;
       Character selectUnit;
       Queue<Character> units;
       Queue<Character> enemunits;
       bool keyState = true;

        public override void Init()
        {
       //     units = new Dictionary<string, Character>();
            selectUnit = null;
            units = new Queue<Character>();
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

            }

            

        }
        public void AddUnit(Character unit)
        {
            units.Enqueue(unit);
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

                        
                        Character unit = units.Dequeue();
                        unit.Select();
                        selectUnit = unit;
                        units.Enqueue(unit);
                        Debug.Log(unit.name);

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
