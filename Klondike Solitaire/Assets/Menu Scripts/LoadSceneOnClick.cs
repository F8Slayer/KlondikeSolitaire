using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//namespace TogHui
//{
public class Data
{
   
    public static byte val = 1;
    public static bool chek1 = false;
    public static bool chek2 = true;


}
//}


public class LoadSceneOnClick : MonoBehaviour
{
    public Toggle toggle_1;
    public Toggle toggle_3;

    public void LoadByIndex(int sceneIndex)
    {
        //if (toggle_1.isOn)
        SceneManager.LoadScene(sceneIndex);
        //TogHui.Data.
    }



    
}
