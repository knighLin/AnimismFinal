using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject Success;
    public GameObject LoadingCanvas;
    private AsyncOperation _async;
    public AudioSource audioSource;
    public Slider LoadingSlider;
    public Image Fade;
    public float time;
    [SerializeField] private bool FadeIn, FadeOut;
    public int YesChooseData,SaveOrLoad,LoadOrDelet;//Save=1 Load=2 Delet=1 Load=2
    [SerializeField] private GameObject LoadGameMenu,D1,T1,D2,T2,D3,T3;



    void Update()
    {
        if (FadeIn)
        { 
            if (time > 0 && time < 1)
            {
                time += 0.05f;
                if (time >= 1)
                    time = 1;
                Fade.color = new Color(0, 0, 0, time);
            }
            else if (time >= 1)
            {
                //AudioFadeOut(audioSource, time);
                time = 0;
                FadeIn = false;
                if (Time.timeScale == 0)//如果暫停狀態下回到主畫面則讓時間恢復
                    Time.timeScale = 1;
                Debug.Log("LoadSence");
                SceneManager.LoadScene("Game");
                //Application.LoadLevelAsync("Game");
                // LoadSence();
            }
        }
        else if (FadeOut)
        {
            if (time>0 &&time < 1)
            {
                time += 0.05f;
                if (time >= 1)
                    time = 1;
                Fade.color = new Color(0, 0, 0,time);
            }
            else if (time >= 1)
            {
                //AudioFadeOut(audioSource, time);
                time = 0;
                FadeOut = false;
                SceneManager.LoadScene("HomePage");
                //Application.LoadLevelAsync("HomePage");
            }
        }
    }
    public void LoadSence()
    {
        Instantiate(LoadingCanvas, Vector2.zero, Quaternion.identity).name = "LoadingCanvas";
        StartCoroutine(LoadLevelWithBar("Game"));
    }

    IEnumerator LoadLevelWithBar(string level)
    {
        _async = SceneManager.LoadSceneAsync(level);
        while (!_async.isDone)
        {
            LoadingSlider.value = _async.progress;
            yield return null;
        }
    }
    public void Click()
    {
        switch (this.name)
        {
            case "NewGame":
                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "NewGame";
                time = 0.05f;
                FadeIn = true;
                break;
            case "Data1":
                if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data1" + ".sav"))
                {
                    GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data1";
                    time = 0.05f;
                    FadeIn = true;
                }
                break;
            case "Data2":
                if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data2" + ".sav"))
                {
                    GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data2";
                    time = 0.05f;
                    FadeIn = true;
                }
                break;
            case "Data3":
                if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data3" + ".sav"))
                {
                    GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data3";
                    time = 0.05f;
                    FadeIn = true;
                }
                break;
            case "Yes":
                switch (SaveOrLoad)
                {
                    case 1:
                        Success.GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_save_success_w", typeof(Sprite)) as Sprite;
                        Success.SetActive(true);
                        break;
                    case 2:
                        Success.GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_load_success_w", typeof(Sprite)) as Sprite;
                        Success.SetActive(true);
                        Success.GetComponent<Button>().Select();
                        break;

                }
                switch (YesChooseData)
                {
                    case 1:
                        if (SaveOrLoad == 1)//1=Save
                        {
                            GameObject.Find("PlayerManager").GetComponent<SaveData>().SaveDataValue("Data1");
                            GameObject.Find("T_Save1").GetComponent<DataName>().SetDataName();
                        }
                        else if (SaveOrLoad == 2)//2=Load
                        {
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data1.sav"))
                            {
                                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data1";
                                time = 0.05f;
                                FadeIn = true;
                            }
                        }
                        break;
                    case 2:
                        if (SaveOrLoad == 1)
                        {
                            GameObject.Find("PlayerManager").GetComponent<SaveData>().SaveDataValue("Data2");
                            GameObject.Find("T_Save2").GetComponent<DataName>().SetDataName();
                        }
                        else if (SaveOrLoad == 2)
                        {
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data2.sav"))
                            {
                                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data2";
                                time = 0.05f;
                                FadeIn = true;
                            }
                        }
                        break;
                    case 3:
                        if (SaveOrLoad == 1)
                        {
                            GameObject.Find("PlayerManager").GetComponent<SaveData>().SaveDataValue("Data3");
                            GameObject.Find("T_Save3").GetComponent<DataName>().SetDataName();
                        }
                        else if(SaveOrLoad == 2)
                        {
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data3.sav"))
                            {
                                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data3";
                                time = 0.05f;
                                FadeIn = true;
                            }
                        }
                        break;
                }
                break;
            case "Exit":
                //離開遊戲
                Application.Quit();
                break;
            case "ExitGame"://主畫面的按鍵名稱
                //離開遊戲
                Application.Quit();
                break;
            case "Return":
                if (Time.timeScale == 0)//如果暫停狀態下回到主畫面則讓時間恢復
                    Time.timeScale = 1;
                if (GameObject.Find("ChooseSaveData")!=null)
                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().DestroyThis();
                time = 0.05f;
                FadeOut = true;
                break;
            case "SureYes":
                switch (YesChooseData)
                {
                    case 1:
                        if (LoadOrDelet == 1)//1=Delet
                        {
                            D1.GetComponent<DeletData>().DeletSaveData();
                            T1.GetComponent<DataName>().SetDataName();
                            LoadGameMenu.SetActive(true);
                            D1.GetComponent<Button>().Select();
                            GameObject.Find("QuestionBoard").SetActive(false);
                        }
                        else if (LoadOrDelet == 2)//2=Load
                        {
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data1.sav"))
                            {
                                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data1";
                                time = 0.05f;
                                FadeIn = true;
                            }
                        }
                        break;
                    case 2:
                        if (LoadOrDelet == 1)//1=Delet
                        {
                            D2.GetComponent<DeletData>().DeletSaveData();
                            T2.GetComponent<DataName>().SetDataName();
                            LoadGameMenu.SetActive(true);
                            D2.GetComponent<Button>().Select();
                            GameObject.Find("QuestionBoard").SetActive(false);
                        }
                        else if (LoadOrDelet == 2)//2=Load
                        {
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data2.sav"))
                            {
                                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data2";
                                time = 0.05f;
                                FadeIn = true;
                            }
                        }
                        break;
                    case 3:
                        if (LoadOrDelet == 1)//1=Delet
                        {
                            D3.GetComponent<DeletData>().DeletSaveData();
                            T3.GetComponent<DataName>().SetDataName();
                            LoadGameMenu.SetActive(true);
                            D3.GetComponent<Button>().Select();
                            GameObject.Find("QuestionBoard").SetActive(false);
                        }
                        else if (LoadOrDelet == 2)//2=Load
                        {
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data3.sav"))
                            {
                                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData = "Data3";
                                time = 0.05f;
                                FadeIn = true;
                            }
                        } 
                        break;
                }
                break;
        }
    }
    public static IEnumerator AudioFadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}


