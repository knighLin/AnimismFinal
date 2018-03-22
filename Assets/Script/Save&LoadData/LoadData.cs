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
                    GameObject.Find("Pine").GetComponent<PossessedSystem>().Target = Wolf.GetComponentsInChildren<Transform>()[3].gameObject.GetComponent<BoxCollider>();
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
