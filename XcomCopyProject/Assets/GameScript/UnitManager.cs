using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xcopy
{
    public class UnitManager : SingletonObject<UnitManager>
    {
       public Dictionary<string, Character> units;
       Character selectUnit;

        public override void Init()
        {
            units = new Dictionary<string, Character>();
            selectUnit = null;
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
        public void Update()
        {
            if (selectUnit != null)
            {
                float h = Input.GetAxis("escape");
                if (h != 0)
                {
                    selectUnit.DiSelect();
                    selectUnit = null;
                }
            }


        }
    }



}
