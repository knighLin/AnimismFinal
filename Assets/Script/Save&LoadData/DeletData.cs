using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeletData : MonoBehaviour {


    public void DeletSaveData()
    {
        switch (this.name)
        {
            case "H_LoadData1":
                File.Delete(Application.persistentDataPath + @"\Save\" + "Data1.sav");
                break;
            case "H_LoadData2":
                File.Delete(Application.persistentDataPath + @"\Save\" + "Data2.sav");
                break;
            case "H_LoadData3":
                File.Delete(Application.persistentDataPath + @"\Save\" + "Data3.sav");
                break;
        }
    }
}
