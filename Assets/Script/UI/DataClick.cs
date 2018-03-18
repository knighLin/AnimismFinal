using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataClick : MonoBehaviour {

    public void Click()
    {
        switch (this.name)
        {
            case "SaveData1":
                GameObject.Find("Yes").GetComponent<ButtonScript>().YesChooseData = 1;
                GameObject.Find("Yes").GetComponent<ButtonScript>().SaveOrLoad = 1;
                GameObject.Find("Data1Select").GetComponent<Image>().enabled = true;
                break;
            case "SaveData2":
                GameObject.Find("Yes").GetComponent<ButtonScript>().YesChooseData = 2;
                GameObject.Find("Yes").GetComponent<ButtonScript>().SaveOrLoad = 1;
                GameObject.Find("Data2Select").GetComponent<Image>().enabled = true;
                break;
            case "SaveData3":
                GameObject.Find("Yes").GetComponent<ButtonScript>().YesChooseData = 3;
                GameObject.Find("Yes").GetComponent<ButtonScript>().SaveOrLoad = 1;
                GameObject.Find("Data3Select").GetComponent<Image>().enabled = true;
                break;
            case "LoadData1":
                GameObject.Find("Yes").GetComponent<ButtonScript>().YesChooseData = 1;
                GameObject.Find("Yes").GetComponent<ButtonScript>().SaveOrLoad = 2;
                GameObject.Find("Data1Select").GetComponent<Image>().enabled = true;
                break;
            case "LoadData2":
                GameObject.Find("Yes").GetComponent<ButtonScript>().YesChooseData = 2;
                GameObject.Find("Yes").GetComponent<ButtonScript>().SaveOrLoad = 2;
                GameObject.Find("Data2Select").GetComponent<Image>().enabled = true;
                break;
            case "LoadData3":
                GameObject.Find("Yes").GetComponent<ButtonScript>().YesChooseData = 3;
                GameObject.Find("Yes").GetComponent<ButtonScript>().SaveOrLoad = 2;
                GameObject.Find("Data3Select").GetComponent<Image>().enabled = true;
                break;
        }
    }
}
