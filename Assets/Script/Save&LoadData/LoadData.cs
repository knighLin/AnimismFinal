using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
        _async = Application.LoadLevelAsync(level);
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
            streamWriter.Write("OvJdSpMhdZWQ3sVZRtJBkssZq54mypOWJbWzq5ir2cZ/aMMFzt71JDJSi+lOW0z9YTG9w4Rc/rdIFtdr/HG8WzNeLH3A5Gf03Iu/rqonrEqy2g09WJZGatSeOHP9nI5NQxV2em/IVE13xOkAnzpE3sElxiCsox5Kesaay9cXk++Ej9ZufWIuZYRBi/1/Z1AZ0rI5bHxnBlar1CpBuNWdahWtGCOC50X0cEEC/ZEzjuRn6C0Y7DyrAEY9qHmy+pBAbk1W5fz780aXAZ3YSa4Duag/1tn/eriQhRsEQpnl8LUbgc0C5Le1HEo3SSauQ55Q7k1zohRTgogw4Uf1Tj3dR5PKgpyOSkG7IbbgZdGwsRacGSQboAc7ELNvj6FLXXRehMZ/cv74wDbP6l9o1JRmXjIf/7dWGHvT5H7T/+txOZUuJQmfjoniDctRdYrpYiMB8AcyzNAr+UfISjJsYpPgnlL8vLSm3KwQJpkQkATw5sHYwm9tHeBcfoVZ7PABW1B2/r228Z5KFgxLsmdufU0+9lef5cvKny4M1WSZJ4NmIM0kxTzOVaxsM1aRMYqhhYeapsI5Eo3q4j2P08MgvihoxbnQ1W5/mA/ep1jegje2A5yJlxjqz/t/jw2Ol7jhY0PCcIumFsYGWDrvEOZmPF1FjEqLWp0a+6aYZU1HOWzS64PW1toIupZmNNCJpOP/6pQ7V2GLozq+Ll3teYXHWcm1xflUsC1V5duHEL4jeLPFJQzKpc3vdMYWBuicIAnVTy98iM4XnBY+d3UV90CjsYxBTsUOLo11VOum1Cgp34uhac0YKZA97QrF2eF7pqDaGyooYeDmulmEq85RKqMWcEBkVA==");
            streamWriter.Close();
        }
        SaveData.Data Load = (SaveData.Data)IOHelper.GetData(Application.persistentDataPath+ @"\Save\"+ LoadSelectedData + ".sav", typeof(SaveData.Data));
        Debug.Log("讀取了" + LoadSelectedData);
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
                    Debug.Log("讀取了第" + (A + 1) + "隻狼," + "狀態為" + WolfState[A] + ",座標為" + WolfVector3[A]);
                }
                if (WolfState[A] == 2)                        //如果動物被附身(WolfState=2)生成後把主角掛在狼身上
                {
                    Wolf = Instantiate(WolfPrefab, WolfVector3[A], WolfQuaternion[A]);
                    GameObject.Find("Pine").GetComponent<PossessedSystem>().Target = Wolf.GetComponentsInChildren<Transform>()[3].gameObject.GetComponent<BoxCollider>();
                    GameObject.Find("Pine").GetComponent<PossessedSystem>().EnterPossessed();
                    Debug.Log("讀取了第" + (A + 1) + "隻狼," + "狀態為" + WolfState[A] + ",座標為" + WolfVector3[A]);
                }
                //Debug.Log(D1.WolfVector3[A]);
                //Debug.Log("讀" + WolfVector3[A]);
            }
        }
        Debug.Log("讀取了派恩的位置,座標為" + PlayerVector3+ ",狀態為"+ PlayerState);
        if (Load.EnemyState.Count > 0)
        {
            for (int E = 0; E < Load.EnemyState.Count; E++)   //讀取敵人數據
            {
                EnemyState.Add(Load.EnemyState[E]);           //讀取敵人狀態
                EnemyVector3.Add(Load.EnemyVector3[E]);       //讀取敵人座標
                EnemyQuaternion.Add(Load.EnemyQuaternion[E]); //讀取敵人旋轉角度
                if (EnemyState[E] == 1)                       //如果敵人活著(EnemyState=1)才生成
                {
                    Instantiate(EnemyPrefab, EnemyVector3[E], EnemyQuaternion[E]).name = "Enemy" + E;
                    Debug.Log("讀取了第" + (E + 1) + "個敵人," + "狀態為" + EnemyState[E] + ",座標為" + EnemyVector3[E]);
                }
            }
        }


    }

}
