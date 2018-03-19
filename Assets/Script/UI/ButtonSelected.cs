using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonSelected : MonoBehaviour, ISelectHandler
{

    public void OnSelect(BaseEventData eventData)
    {
        switch (this.name)
        {
            case "Save":
                GameObject.Find("SaveSelect").GetComponent<Image>().enabled = true;
                break;
            case "Load":
                GameObject.Find("LoadSelect").GetComponent<Image>().enabled = true;
                break;
            case "SaveData1":
                GameObject.Find("Data1Select").GetComponent<Image>().enabled = true;
                break;
            case "SaveData2":
                GameObject.Find("Data2Select").GetComponent<Image>().enabled = true;
                break;
            case "SaveData3":
                GameObject.Find("Data3Select").GetComponent<Image>().enabled = true;
                break;
            case "LoadData1":
                GameObject.Find("Data1Select").GetComponent<Image>().enabled = true;
                break;
            case "LoadData2":
                GameObject.Find("Data2Select").GetComponent<Image>().enabled = true;
                break;
            case "LoadData3":
                GameObject.Find("Data3Select").GetComponent<Image>().enabled = true;
                break;
            case "Yes":
                GameObject.Find("YesSelect").GetComponent<Image>().enabled = true;
                GameObject.Find("SaveBoard").GetComponent<MenuSelect>().Yes = true;
                break;
            case "No":
                GameObject.Find("NoSelect").GetComponent<Image>().enabled = true;
                break;
                //以下Setting
            case "Music":
                GameObject.Find("MusicSelect").GetComponent<Image>().enabled = true;
                break;
            case "MusicBar":
                GameObject.Find("MusicBarSelect").GetComponent<Image>().enabled = true;
                break;
            case "Sound":
                GameObject.Find("SoundSelect").GetComponent<Image>().enabled = true;
                break;
            case "SoundBar":
                GameObject.Find("SoundBarSelect").GetComponent<Image>().enabled = true;
                break;
            case "Return":
                GameObject.Find("ExitSelect").GetComponent<Image>().enabled = false;
                GameObject.Find("ReturnSelect").GetComponent<Image>().enabled = true;
                break;
            case "Exit":
                GameObject.Find("ReturnSelect").GetComponent<Image>().enabled = false;
                GameObject.Find("ExitSelect").GetComponent<Image>().enabled = true;
                break;
        }
    }

}
