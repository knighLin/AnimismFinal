using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonDeselect : MonoBehaviour, IDeselectHandler
{
    public void OnDeselect(BaseEventData data)
    {
        switch (this.name)
        {
            case "Save":
                GameObject.Find("SaveSelect").GetComponent<Image>().enabled = false;
                break;
            case "Load":
                GameObject.Find("LoadSelect").GetComponent<Image>().enabled = false;
                break;
            case "Data1":
                GameObject.Find("Data1Select").GetComponent<Image>().enabled = false;
                break;
            case "Data2":
                GameObject.Find("Data2Select").GetComponent<Image>().enabled = false;
                break;
            case "Data3":
                GameObject.Find("Data3Select").GetComponent<Image>().enabled = false;
                break;
            case "Yes":
                GameObject.Find("YesSelect").GetComponent<Image>().enabled = false;
                break;
            case "No":
                GameObject.Find("NoSelect").GetComponent<Image>().enabled = false;
                break;
        }
    }
}
