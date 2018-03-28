using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadData : MonoBehaviour {
    public GameObject LoadingCanvas;
    public GameObject PlayerPrefab, WolfPrefab, EnemyPrefab;
    public GameObject Wolf;
    public List<int> WolfState;
    public List<Vector3> WolfVector3;
    public List<Quaternion> WolfQuaternion;
    public List<int> EnemyState;
    public List<Vector3> EnemyVector3;
    public List<Quaternion> EnemyQuaternion;
    public string PlayerState;
    public Vector3 PlayerVector3;
    public Quaternion PlayerQuaternion;
    public float SaveRotx, SaveRoty;
    public Slider LoadingSlider;
    private AsyncOperation _async;
    public string LoadSelectedData;
    private CameraScript CameraScript;

    // Use this for initialization
    void Awake () {
        SpawnAllObject();
    }
    private void Start()
    {
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        CameraScript.rotX = SaveRotx;
        CameraScript.rotY = SaveRoty;
    }

    // Update is called once per frame
    void Update () {

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
    public  void SpawnAllObject()
    {
        if (File.Exists(Application.persistentDataPath + @"\Save\NewGame.sav")) 
            LoadSelectedData = GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData;
        else
        {
            LoadSelectedData = "NewGame";//測試用
            StreamWriter streamWriter = File.CreateText(Application.persistentDataPath + @"\Save\NewGame.sav");
            streamWriter.Write("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");
            streamWriter.Close();
        }
        SaveData.Data Load = (SaveData.Data)IOHelper.GetData(Application.persistentDataPath+ @"\Save\"+ LoadSelectedData + ".sav", typeof(SaveData.Data));
       // Debug.Log("讀取了" + LoadSelectedData);
        PlayerState = Load.PlayerState;
        PlayerVector3 = Load.PlayerVector3;
        PlayerQuaternion = Load.PlayerQuaternion;
        SaveRotx = Load.SaveRotx;
        SaveRoty = Load.SaveRoty;




        Instantiate(PlayerPrefab, PlayerVector3, PlayerQuaternion).name = "Pine";
        if (Load.WolfState.Count > 0)
        {
            for (int A = 0; A < Load.WolfState.Count; A++)     //讀取動物數據
            {
                WolfState.Add(Load.WolfState[A]);           //讀取動物狀態
                WolfVector3.Add(Load.WolfVector3[A]);       //讀取動物座標
                WolfQuaternion.Add(Load.WolfQuaternion[A]); //讀取動物旋轉角度
                if (WolfState[A] == 1)                          //如果動物活著(WolfState=1)才生成
                {
                    Instantiate(WolfPrefab, WolfVector3[A], WolfQuaternion[A]).name = "Wolf" + A;
                    //Debug.Log("讀取了第" + (A + 1) + "隻狼," + "狀態為" + WolfState[A] + ",座標為" + WolfVector3[A]);
                }
                if (WolfState[A] == 2)                        //如果動物被附身(WolfState=2)生成後把主角掛在狼身上
                {
                    Wolf = Instantiate(WolfPrefab, WolfVector3[A], WolfQuaternion[A]);
                    GameObject.Find("Pine").GetComponent<PossessedSystem>().Target = Wolf.transform.GetChild(3).GetComponent<Collider>();
                    GameObject.Find("Pine").GetComponent<PossessedSystem>().EnterPossessed();
                    //Debug.Log("讀取了第" + (A + 1) + "隻狼," + "狀態為" + WolfState[A] + ",座標為" + WolfVector3[A]);
                }
                //Debug.Log(D1.WolfVector3[A]);
                //Debug.Log("讀" + WolfVector3[A]);
            }
        }
        //Debug.Log("讀取了派恩的位置,座標為" + PlayerVector3+ ",狀態為"+ PlayerState);
        if (Load.EnemyState.Count > 0)
        {
            EnemyState = new List<int> { };
            EnemyVector3 = new List<Vector3> { };
            EnemyQuaternion = new List<Quaternion> { };
            for (int E = 0; E < Load.EnemyState.Count; E++)   //讀取敵人數據
            {
                
                EnemyState.Add( Load.EnemyState[E]);           //讀取敵人狀態
                EnemyVector3.Add(Load.EnemyVector3[E]);       //讀取敵人座標
                EnemyQuaternion.Add(Load.EnemyQuaternion[E]); //讀取敵人旋轉角度
                //Debug.Log(Load.EnemyVector3[E]);
                //Debug.Log(EnemyVector3[E]);
                if (EnemyState[E] == 1)                       //如果敵人活著(EnemyState=1)才生成
                {
                    Instantiate(EnemyPrefab, EnemyVector3[E], EnemyQuaternion[E]).name = "Enemy" + E;
                    //Debug.Log("讀取了第" + (E + 1) + "個敵人," + "狀態為" + EnemyState[E] + ",座標為" + EnemyVector3[E]);
                }
            }
        }


    }

}
