using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Togglevalue : MonoBehaviour
{
    public Toggle toggle_1;
    public Toggle toggle_3;

    void Start()
    {
        toggle_1.isOn = Data.chek1;
        toggle_3.isOn = Data.chek2;
    }



    public void Change(bool nv)
    {
      //  nv = false;
       // print(nv.ToString());
       
        Data.chek1= toggle_1.isOn;
        Data.chek2= toggle_3.isOn;
        if (Data.chek1) Data.val = 2;
        if (Data.chek2) Data.val = 1;
        print(Data.chek1.ToString());
        print(Data.chek2.ToString());
        print(Data.val.ToString());
        // else if() Data.val == 1

    }


}
