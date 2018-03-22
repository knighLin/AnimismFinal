using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class DataName : MonoBehaviour {
    public GameObject Delet;
    public FileInfo DataInfo;
    public Text TextName;
    public string WhitchData;
    // Use this for initialization
    void Start () {
        SetDataName();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetDataName()
    {
        switch (transform.parent.name)
        {
            case "SaveData1" :
                WhitchData = "Data1";
                break;
            case "LoadData1":
                WhitchData = "Data1";
                break;
            case "SaveData2":
                WhitchData = "Data2";
                break;
            case "LoadData2":
                WhitchData = "Data2";
                break;
            case "SaveData3":
                WhitchData = "Data3";
                break;
            case "LoadData3":
                WhitchData = "Data3";
                break;
        }
        if (File.Exists(Application.persistentDataPath + @"\Save\" + WhitchData + ".sav"))
        {
            if (!GameObject.Find("Logo"))
                TextName.fontSize = 25;
            else
                TextName.fontSize = 41;
            DataInfo = new FileInfo(Application.persistentDataPath + @"\Save\" + WhitchData + ".sav");
            Debug.Log(DataInfo.LastWriteTime);
            TextName.text = DataInfo.LastWriteTime.ToString();
            if (Delet!=null)
            Delet.SetActive(true);
        }
        else
        {
            if (!GameObject.Find("Logo"))
                TextName.fontSize = 25;
            else
                TextName.fontSize = 45;
            TextName.text = "空的存檔";
            if (Delet != null)
                Delet.SetActive(false);
        }
    }
}
