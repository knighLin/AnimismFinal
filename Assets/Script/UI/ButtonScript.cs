﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject LoadingCanvas, ChooseSaveData;
    private AsyncOperation _async;
    public AudioSource audioSource;
    public Slider LoadingSlider;
    public Image FadeOut;
    public float time;
    private bool Fade;
    public int YesChooseData,SaveOrLoad = 0;//Save=1 Load=2


    void Update()
    {
        if (Fade && time < 1)
        {
            time += Time.deltaTime*0.8f;
            if (time >= 1)
                time = 1;
            FadeOut.color = new Color(0, 0, 0, time);
        }
        else if (time >= 1)
        {
            //AudioFadeOut(audioSource, time);
            time = 0;
            Fade = false;
            Debug.Log("LoadSence");
            SceneManager.LoadScene("Game");
            //Application.LoadLevelAsync("Game");
           // LoadSence();
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
                ChooseSaveData.GetComponent<ChooseSaveData>().SelectedData = "NewGame";
                Fade = true;
                break;
            case "Yes":
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
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data1" + ".sav"))
                            {
                                ChooseSaveData.GetComponent<ChooseSaveData>().SelectedData = "Data1";
                                Fade = true;
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
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data2" + ".sav"))
                            {
                                ChooseSaveData.GetComponent<ChooseSaveData>().SelectedData = "Data2";
                                Fade = true;
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
                            if (File.Exists(Application.persistentDataPath + @"\Save\" + "Data3" + ".sav"))
                            {
                                ChooseSaveData.GetComponent<ChooseSaveData>().SelectedData = "Data3";
                                Fade = true;
                            }
                        }
                        break;
                }
                break;
            case "Exit":
                //離開遊戲
                break;
            case "Return":
                if (Time.timeScale == 0)//如果暫停狀態下回到主畫面則讓時間恢復
                    Time.timeScale = 1;
                if (GameObject.Find("ChooseSaveData")!=null)
                GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().DestroyThis();
                SceneManager.LoadScene("HomePage");
                //Application.LoadLevelAsync("HomePage");
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

