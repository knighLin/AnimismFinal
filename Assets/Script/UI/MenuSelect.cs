using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] private GameObject Quest, Save, Setting, Options,Music,Sound;
    public string NowSelectMenu;
    // Use this for initialization

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("CrossJump"))
        {
            switch (NowSelectMenu)
                {
                case "Quest":
                    GameObject.Find("B_Quest").GetComponent<Button>().Select();
                    break;
                case "Save":
                    if (GameObject.Find("SaveDataBoard"))
                    {
                        if (GameObject.Find("ChooseBoard"))
                        {
                            GameObject.Find("ChooseBoard").SetActive(false);
                            GameObject.Find("YesSelect").GetComponent<Image>().enabled = false;
                            GameObject.Find("NoSelect").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data1Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data2Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data3Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("SaveData1").GetComponent<Button>().Select();
                        }
                        else
                        {
                            GameObject.Find("SaveDataBoard").SetActive(false);
                            GameObject.Find("Data1Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data2Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data3Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Save").GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_save_B", typeof(Sprite)) as Sprite;
                            GameObject.Find("Save").GetComponent<Image>().SetNativeSize();
                            GameObject.Find("Save").GetComponent<Button>().Select();
                        }
                    }
                    else if (GameObject.Find("LoadDataBoard"))
                    {
                        if (GameObject.Find("ChooseBoard"))
                        {
                            GameObject.Find("ChooseBoard").SetActive(false);
                            GameObject.Find("YesSelect").GetComponent<Image>().enabled = false;
                            GameObject.Find("NoSelect").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data1Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data2Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data3Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("LoadData1").GetComponent<Button>().Select();
                        }
                        else
                        {
                            GameObject.Find("LoadDataBoard").SetActive(false);
                            GameObject.Find("Data1Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data2Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Data3Select").GetComponent<Image>().enabled = false;
                            GameObject.Find("Load").GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_load_B", typeof(Sprite)) as Sprite;
                            GameObject.Find("Load").GetComponent<Image>().SetNativeSize();
                            GameObject.Find("Load").GetComponent<Button>().Select();
                        }
                    }
                    else if (GameObject.Find("SaveSelect").GetComponent<Image>().enabled || GameObject.Find("LoadSelect").GetComponent<Image>().enabled)
                    {
                        GameObject.Find("SaveSelect").GetComponent<Image>().enabled = false;
                        GameObject.Find("LoadSelect").GetComponent<Image>().enabled = false;
                        GameObject.Find("B_Save").GetComponent<Button>().Select();
                    }
                    break;
                //以下Setting
                case "Setting":
                    if (GameObject.Find("MusicBarSelect").GetComponent<Image>().enabled)
                    {
                        GameObject.Find("Music").GetComponent<Image>().sprite = Resources.Load("UI/Menu/d_music_B", typeof(Sprite)) as Sprite;
                        GameObject.Find("Music").GetComponent<Image>().SetNativeSize();
                        GameObject.Find("Music").GetComponent<Button>().Select();
                    }
                    else if (GameObject.Find("SoundBarSelect").GetComponent<Image>().enabled)
                    {
                        GameObject.Find("Sound").GetComponent<Image>().sprite = Resources.Load("UI/Menu/d_sound_B", typeof(Sprite)) as Sprite;
                        GameObject.Find("Sound").GetComponent<Image>().SetNativeSize();
                        GameObject.Find("Sound").GetComponent<Button>().Select();
                    }
                    else if (GameObject.Find("MusicSelect").GetComponent<Image>().enabled|| GameObject.Find("SoundSelect").GetComponent<Image>().enabled)
                    {
                        GameObject.Find("B_Setting").GetComponent<Button>().Select();
                    }
                    break;
                //以下Options
                case "Options":
                    GameObject.Find("ExitSelect").GetComponent<Image>().enabled = false;
                    GameObject.Find("ReturnSelect").GetComponent<Image>().enabled = false;
                    GameObject.Find("B_Options").GetComponent<Button>().Select();
                    break;
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

        Music.GetComponent<Image>().sprite = Resources.Load("UI/Menu/d_music_B", typeof(Sprite)) as Sprite;
        Music.GetComponent<Image>().SetNativeSize();
        Sound.GetComponent<Image>().sprite = Resources.Load("UI/Menu/d_sound_B", typeof(Sprite)) as Sprite;
        Sound.GetComponent<Image>().SetNativeSize();
        Setting.SetActive(false);
    }
    public void CloseOptions()
    {
         Options.SetActive(false);
    }
}
