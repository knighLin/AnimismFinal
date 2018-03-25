using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonSelected : MonoBehaviour, ISelectHandler
{

    public void OnSelect(BaseEventData eventData)
    {
        switch (this.name)
        {
            //以下Save
            case "B_Quest":
                this.GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_quest_W", typeof(Sprite)) as Sprite;
                this.GetComponent<Image>().SetNativeSize();
                this.transform.localPosition = new Vector3(565, 360, 0);
                GameObject.Find("B_Save").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_record_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Save").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Save").transform.localPosition = new Vector3(695, 345, 0);
                GameObject.Find("B_Setting").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_setting_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Setting").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Setting").transform.localPosition = new Vector3(795, 345, 0);
                GameObject.Find("B_Options").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_option_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Options").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Options").transform.localPosition = new Vector3(895, 345, 0);
                GameObject.Find("Menu").GetComponent<MenuSelect>().SwitchQuest();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseSave();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseSetting();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseOptions();
                GameObject.Find("Menu").GetComponent<MenuSelect>().NowSelectMenu = "Quest";
                break;
            case "B_Save":
                this.GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_record_W", typeof(Sprite)) as Sprite;
                this.GetComponent<Image>().SetNativeSize();
                this.transform.localPosition = new Vector3(665, 360, 0);
                GameObject.Find("B_Quest").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_quest_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Quest").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Quest").transform.localPosition = new Vector3(535, 345, 0);
                GameObject.Find("B_Setting").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_setting_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Setting").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Setting").transform.localPosition = new Vector3(795, 345, 0);
                GameObject.Find("B_Options").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_option_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Options").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Options").transform.localPosition = new Vector3(895, 345, 0);
                GameObject.Find("Menu").GetComponent<MenuSelect>().SwitchSave();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseQuest();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseSetting();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseOptions();
                GameObject.Find("Menu").GetComponent<MenuSelect>().NowSelectMenu = "Save";
                break;
            case "B_Setting":
                this.GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_setting_W", typeof(Sprite)) as Sprite;
                this.GetComponent<Image>().SetNativeSize();
                this.transform.localPosition = new Vector3(765, 360, 0);
                GameObject.Find("B_Quest").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_quest_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Quest").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Quest").transform.localPosition = new Vector3(535, 345, 0);
                GameObject.Find("B_Save").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_record_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Save").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Save").transform.localPosition = new Vector3(635, 345, 0);
                GameObject.Find("B_Options").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_option_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Options").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Options").transform.localPosition = new Vector3(895, 345, 0);
                GameObject.Find("Menu").GetComponent<MenuSelect>().SwitchSetting();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseQuest();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseSave();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseOptions();
                GameObject.Find("Menu").GetComponent<MenuSelect>().NowSelectMenu = "Setting";
                break;
            case "B_Options":
                this.GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_option_W", typeof(Sprite)) as Sprite;
                this.GetComponent<Image>().SetNativeSize();
                this.transform.localPosition = new Vector3(865, 360, 0);
                GameObject.Find("B_Quest").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_quest_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Quest").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Quest").transform.localPosition = new Vector3(535, 345, 0);
                GameObject.Find("B_Save").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_record_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Save").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Save").transform.localPosition = new Vector3(635, 345, 0);
                GameObject.Find("B_Setting").GetComponent<Image>().sprite = Resources.Load("UI/Menu/a_setting_B", typeof(Sprite)) as Sprite;
                GameObject.Find("B_Setting").GetComponent<Image>().SetNativeSize();
                GameObject.Find("B_Setting").transform.localPosition = new Vector3(735, 345, 0);
                GameObject.Find("Menu").GetComponent<MenuSelect>().SwitchOptions();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseQuest();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseSave();
                GameObject.Find("Menu").GetComponent<MenuSelect>().CloseSetting();
                GameObject.Find("Menu").GetComponent<MenuSelect>().NowSelectMenu = "Options";
                break;
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
            //以下為主畫面
            case "NewGame":
                GameObject.Find("NewGameSelect").GetComponent<Image>().enabled = true;
                GameObject.Find("H_Cross").GetComponent<Image>().enabled = false;
                GameObject.Find("H_Triangle").GetComponent<Image>().enabled = false;
                GameObject.Find("H_Circle").GetComponent<Image>().enabled = true;
                break;
            case "LoadGame":
                GameObject.Find("LoadGameSelect").GetComponent<Image>().enabled = true;
                break;
            case "ExitGame":
                GameObject.Find("ExitGameSelect").GetComponent<Image>().enabled = true;
                break;
            case "H_LoadData1":
                GetComponent<LoadOrDelet>().Select = true;
                if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data1.sav"))
                {
                    GameObject.Find("H_Cross").GetComponent<Image>().enabled = true;
                    GameObject.Find("H_Triangle").GetComponent<Image>().enabled = true;
                    GetComponent<LoadOrDelet>().Select = true;
                }
                else
                {
                    GameObject.Find("H_Cross").GetComponent<Image>().enabled = true;
                    GameObject.Find("H_Triangle").GetComponent<Image>().enabled = false;
                    GetComponent<LoadOrDelet>().Select = false;
                }
                break;
            case "H_LoadData2":
                if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data2.sav"))
                {
                    GameObject.Find("H_Triangle").GetComponent<Image>().enabled = true;
                    GetComponent<LoadOrDelet>().Select = true;
                }
                else
                {
                    GameObject.Find("H_Triangle").GetComponent<Image>().enabled = false;
                    GetComponent<LoadOrDelet>().Select = false;
                }
                break;
            case "H_LoadData3":
                if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data3.sav"))
                {
                    GameObject.Find("H_Triangle").GetComponent<Image>().enabled = true;
                    GetComponent<LoadOrDelet>().Select = true;
                }
                else
                {
                    GameObject.Find("H_Triangle").GetComponent<Image>().enabled = false;
                    GetComponent<LoadOrDelet>().Select = false;
                }
                break;
            case "SureYes":
                GameObject.Find("SureYesSelect").GetComponent<Image>().enabled = true;
                GameObject.Find("H_Triangle").GetComponent<Image>().enabled = false;
                break;
            case "SureNo":
                GameObject.Find("SureNoSelect").GetComponent<Image>().enabled = true;
                break;
        }
    }

}
