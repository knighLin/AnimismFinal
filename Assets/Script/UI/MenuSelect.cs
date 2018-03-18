using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour {
    [SerializeField] private GameObject Quest, Save, Setting, Options;
    private float H;
    public bool Yes,No;
    // Use this for initialization

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            H += Input.GetAxis("Horizontal");
            if (Mathf.Ceil(H) > 3.5f)
            {
                switch (this.name)
                {
                    case "QuestBoard":
                        SwitchSave();
                        H = 0;
                        CloseQuest();
                        break;
                    case "SaveBoard":
                        
                        if (Yes)
                        {
                            GameObject.Find("No").GetComponent<Button>().Select();
                            H = 0;
                        }
                        else if (No)
                        {
                            GameObject.Find("Yes").GetComponent<Button>().Select();
                            H = 0;
                        }
                        else
                        {
                            SwitchSetting();
                            H = 0;
                            CloseSave();
                        }
                        break;
                    case "SettingBoard":
                        SwitchOptions();
                        H = 0;
                        CloseSetting();
                        break;
                    case "OptionsBoard":
                        SwitchQuest();
                        H = 0;
                        CloseOptions();
                        break;
                }
            }
        }
        else if (Input.GetAxis("Horizontal") <0.01f)
        {
            H += Input.GetAxis("Horizontal");
            if (Mathf.Ceil(H) < -3.5f)
            { 
                switch (this.name)
                {
                    case "QuestBoard":
                        SwitchOptions();
                        H = 0;
                        CloseQuest();
                        break;
                    case "SaveBoard":
                        if (Yes)
                        {
                            GameObject.Find("No").GetComponent<Button>().Select();
                            H = 0;
                        }
                        else if (No)
                        {
                            GameObject.Find("Yes").GetComponent<Button>().Select();
                            H = 0;
                        }
                        else
                        {
                            SwitchQuest();
                            H = 0;
                            CloseSave();
                        }
                        break;
                    case "SettingBoard":
                        SwitchSave();
                        H = 0;
                        CloseSetting();
                        break;
                    case "OptionsBoard":
                        SwitchSetting();
                        H = 0;
                        CloseOptions();
                        break;
                }
            }
        }
    }
    public void SwitchQuest()
    {
        Quest.SetActive(true);
    }
    public void SwitchSave()
    {
        Save.SetActive(true);
        GameObject.Find("Save").GetComponent<Button>().Select();
        GameObject.Find("SaveSelect").GetComponent<Image>().enabled = true;
    }
    public void SwitchSetting()
    {
        Setting.SetActive(true);
    }
    public void SwitchOptions()
    {
        Options.SetActive(true);
    }
    public void CloseQuest()
    {
        Quest.SetActive(false);
    }
    public void CloseSave()
    {
        this.GetComponent<ResetSaveBoard>().ResetTheSaveBoard();
        Save.SetActive(false);
    }
    public void CloseSetting()
    {
        Setting.SetActive(false);
    }
    public void CloseOptions()
    {
        Options.SetActive(false);
    }
}
