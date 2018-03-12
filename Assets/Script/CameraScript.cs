using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private pillarSystem pillarSystem;
    private CameraLocking CameraLocking;
    private PossessedSystem PossessedSystem;
    private PlayerManager playerManager;
    public GameObject PossessTarget;
    public GameObject SoulVisionEffect, PossessEffect, Crosshairs;
    public GameObject NowCharacter;
    public GameObject MoveEnd, PlayerView,Normal003;
    public Transform[] AttachedBodyChildren;
    public Vector3 NormalPosition;//鏡頭正常位置
    private Vector3 RedressVector = Vector3.zero;
    private Vector3 Move;//鏡頭"每次"前進/後退的距離
    private Vector3 VectorMoveDistance;//鏡頭總共要前進的距離
    private Vector3 CameraNowPosition;//鏡頭前進完要後退的位置 用來測量要後退多少距離
    private Vector3 FixedPosition;//固定視角後的座標
    private Quaternion RotationEuler;
    public string CameraState;//鏡頭狀態
    public float rotX;
    public float rotY;
    public int ShakeTime =0;
    private float sensitivity = 30f;//靈敏度
    private float FowardAndBackTime;//鏡頭前進/後退計時
    private float FowardStop = 0.2f; //鏡頭前進的秒數
    private float PossessTime; //附身鏡頭計時
    private float PossessStop = 2; //附身鏡頭的秒數
    public bool CanPossess = false;//靈視狀態下才會為true 代表可以附身;
    public bool LockingAnimal = false;//鎖定動物中
    public bool IsPossessing = false;//附身中為true直到附身結束切回正常狀態才會被重置為false
    public bool CantLeftPossess = false;//靈視不能退出附身
    public bool CantSoulVison = false;//附身後不能持續按著E進入靈視
    public bool FixedVison = false;//固定視角
    public bool IsSoulVision = false;
    public bool Backing = false;
    public bool Shake = false;

    // Use this for initialization
    void Start()
    {
        pillarSystem = GameObject.Find("TotemPole").GetComponent<pillarSystem>();
        CameraLocking = GetComponent<CameraLocking>();
        PossessedSystem = GameObject.Find("Pine").GetComponent<PossessedSystem>();
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        SoulVisionEffect.SetActive(false);//開始時靈視關閉
        PossessEffect.SetActive(false);//開始時附身鏡頭關閉
        Crosshairs.SetActive(false);//開始時準心關閉
        CameraState = "NormalState";//初始狀態為正常狀態
        AttachedBodyChildren = new Transform[3];//只抓前四個物件(包含本身)
        PlayerView = GameObject.Find("FirstPersonCamPoint");
        MoveEnd = GameObject.Find("CamMoveEndPoint");//一開始取正確腳色位置
        NowCharacter = GameObject.Find("Pine");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
            Shake = true;
        else
            Shake = false;
        if (Shake)
        {
            if (ShakeTime%2==0)
            Normal003.transform.localPosition += new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.2f, 0.2f));
            ShakeTime += 1;
        }
        else
        {
            Normal003.transform.localPosition = new Vector3(0, 0, -3);
            ShakeTime = 0;
        }
        if (!Backing&&!IsPossessing&&!FixedVison&&!LockingAnimal) CameraRotate();//如果不是附身模式或固定視角模式 讓鏡頭可以轉
        NormalPosition = RotationEuler * Normal003.transform.localPosition + PlayerView.transform.position;//每幀確認鏡頭正常的位置 讓前進後退順暢
        switch (CameraState)
        {
            case "NormalState":
                NormalState();
                break;
            case "EnterSoulVision":
                EnterSoulVision();
                break;
            case "SoulVision":
                SoulVision();
                break;
            case "SoulVisionLocking":
                SoulVisionLocking();
                break;
            case "SoulVisionOver":
                SoulVisionOver();
                break;
            case "GettingPossess":
                GettingPossess();
                break;
        }

        if (Input.GetButton("L2") && Input.GetButton("R2Run"))//同時按著R2L2 視角固定不能轉
            FixedVison = true;
        else
        {
            if (FixedVison)
            {
                rotX = transform.eulerAngles.y;//讓角度跟固定視角的角度一樣
                rotY = transform.eulerAngles.x;
                FixedVison = false;
            }
        }
        if (Input.GetButtonUp("L1SoulVison"))
            CantSoulVison = false;
        if (Input.GetButton("L1SoulVison") && !IsPossessing && !CantSoulVison )//持續按著靈視鍵可以進入靈視 但附身過程或附身後還按著無效(IsPossessing為true)
        {
            if (IsSoulVision)
                CameraState = "SoulVision";
            else if (LockingAnimal)
                CameraState = "SoulVisionLocking";
            else
                CameraState = "EnterSoulVision";
        }


    }
    public void ResetValue()//重置一些前進後退中用到的值 以防下次進入其他模式出問題
    {
        if (IsSoulVision|| CanPossess)
        FixedVison = false;
        Backing = false;
        CameraLocking.Player = null;
        IsSoulVision = false;
        CantLeftPossess = false;
        CanPossess = false;//不能附身
        LockingAnimal = false;
        IsPossessing = false;//可以進入靈視
        FowardAndBackTime = 0;//前進後退的計時為0
        PossessTime = 0;//前進後退的計時為0
    }
    public void CameraRotate()//攝影機旋轉
    {
        if (Time.timeScale == 1)
        {
            //讀取滑鼠的X、Y軸移動訊息
            rotX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            rotY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            rotX -= Input.GetAxis("SoulVisonHorizontal") * sensitivity * Time.deltaTime * 5;
            rotY -= Input.GetAxis("SoulVisonVertical") * sensitivity * Time.deltaTime * 5;
        }
        else if (Time.timeScale == 0.5f)
        {
            //讀取滑鼠的X、Y軸移動訊息
            rotX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 2;
            rotY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * 2;
            rotX -= Input.GetAxis("SoulVisonHorizontal") * sensitivity * Time.deltaTime * 10;
            rotY -= Input.GetAxis("SoulVisonVertical") * sensitivity * Time.deltaTime * 10;
        }
        //保證X在360度以內
        if (rotX > 360) rotX -= 360;
        else if (rotX < 0) rotX += 360;
        if (rotY > 45) rotY = 45;
        else if (rotY < -45) rotY = -45;
        //運算攝影機旋轉
        RotationEuler = Quaternion.Euler(rotY, rotX, 0);
        transform.rotation = RotationEuler; //鏡頭轉動
    }
    public void NormalState()
    {
        ResetValue();//只要進入正常模式 就重置一些參數
        //鏡頭穿牆處理
        RaycastHit hit;
        if (Physics.Linecast(PlayerView.transform.position, NormalPosition, out hit)&&!FixedVison)
        {
            int HitTag = hit.collider.gameObject.layer;//撞到的物件的layer
            if (HitTag != 9 && HitTag != 11&&HitTag!=8&& HitTag != 10)//9為player 11為ragdoll 8為CanPossess 10為武器
            {
                RedressVector = NormalPosition - hit.point;//如果撞到物件 設一個向量為 撞到的位置和原來鏡頭位置之差
                transform.position = NormalPosition - RedressVector;//減掉位置差 讓鏡頭移動到撞到的位置 其實值等於 hit.point即可 只是變數留著可以做變化 先不做優化
                //Debug.DrawLine(PlayerView.transform.position, hit.point, Color.red);
            }
            else
            {
                transform.position = NormalPosition;//如果撞到主角身上物件 座標為正常位置
            }
        }
        else
        {
            if (FixedVison)
            {
                transform.rotation = NowCharacter.transform.rotation;
                transform.position = transform.rotation*Normal003.transform.localPosition + PlayerView.transform.position;
                FixedPosition = transform.position;
                rotX = transform.eulerAngles.y;
                rotY = transform.eulerAngles.x;
            }
            else
            {
                transform.position = NormalPosition;//如果沒撞到物件 座標為正常位置
            }
        }
    }
    public void EnterSoulVision()//鏡頭前進為靈視狀態
    {
        Backing = false;
        CantLeftPossess = true;
        NowCharacter.transform.rotation = Quaternion.Euler(0, rotX, 0);//靈視狀態下腳色轉動
        if (!Input.GetKey(KeyCode.E) && !IsPossessing)//只要在靈視狀態下放開靈視鍵則退出靈視
            CameraState = "SoulVisionOver";
        if (FowardAndBackTime < FowardStop)//0.25秒移動到到指定位置
        {
            FowardAndBackTime += Time.deltaTime;
            VectorMoveDistance = RotationEuler * new Vector3(0, -0.2f, 0) + MoveEnd.transform.position - NormalPosition;//距離為終點減正常位置
            Move = VectorMoveDistance * Time.deltaTime * 5;
            transform.position += Move;
            CameraNowPosition = transform.position;
        }
        else if (FowardAndBackTime >= FowardStop)//到指定位置後開啟靈視效果和準心 並可以進入附身
        {
            FowardAndBackTime = FowardStop;
            IsSoulVision = true;
        }
    }
    public void SoulVision()//靈視
    {

        NowCharacter.transform.rotation = Quaternion.Euler(0, rotX, 0);//靈視狀態下腳色轉動
        if (!Input.GetKey(KeyCode.E) && !IsPossessing)//只要在靈視狀態下放開靈視鍵則退出靈視
            CameraState = "SoulVisionOver";
        CameraNowPosition = RotationEuler * new Vector3(0, -0.2f, 0) + MoveEnd.transform.position;
        transform.position = RotationEuler * new Vector3(0, -0.2f, 0) + MoveEnd.transform.position;
        SoulVisionEffect.SetActive(true);
        Crosshairs.SetActive(true);
        CanPossess = true;
        pillarSystem.pillarSystemBool = true;
        if (PossessedSystem.RangeObject.Count > 0&&Input.GetButtonDown("CircleLockingAnimal"))
        {
            LockingAnimal = true;
            CameraLocking.LockingAnimals();
            transform.position = CameraLocking.CameraRotation * new Vector3(0,-0.2f,0) + MoveEnd.transform.position;
            transform.rotation = CameraLocking.CameraRotation;
            CameraNowPosition = transform.position;
            rotX = transform.eulerAngles.y;//讓角度跟固定視角的角度一樣
            rotY = transform.eulerAngles.x;
            RotationEuler = Quaternion.Euler(rotY, rotX, 0);
            IsSoulVision = false;//換成鎖定

        }
    }
    public void SoulVisionLocking()//靈視鎖定狀態
    {
        if (!Input.GetKey(KeyCode.E) && !IsPossessing)//只要在靈視狀態下放開靈視鍵則退出靈視
            CameraState = "SoulVisionOver";
        if (Input.GetAxis("SoulVisonHorizontal") > 0.01f || Input.GetAxis("SoulVisonVertical") > 0.01f)
        {
            LockingAnimal = false;
            CameraLocking.Player = null;
            IsSoulVision = true;//換成靈視
        }
        else if (Input.GetButtonDown("CircleLockingAnimal"))
        {
            CameraLocking.LockingAnimals();
            transform.position = CameraLocking.CameraRotation * new Vector3(0, -0.2f, 0) + MoveEnd.transform.position;
            transform.rotation = CameraLocking.CameraRotation;
            CameraNowPosition = transform.position;
            rotX = transform.eulerAngles.y;//讓角度跟固定視角的角度一樣
            rotY = transform.eulerAngles.x;
            RotationEuler = Quaternion.Euler(rotY, rotX, 0);
        }
    }   
    public void SoulVisionOver()//鏡頭後退為正常狀態
    {
        pillarSystem.pillarSystemBool = false;
        Backing = true ;
        CameraLocking.Player = null;
        IsSoulVision = false;
        SoulVisionEffect.SetActive(false);
        Crosshairs.SetActive(false);
        CanPossess = false;
        LockingAnimal = false;
        if (FowardAndBackTime > 0)//從移動到的位置退回正常位置
        {
            FowardAndBackTime -= Time.deltaTime;
            VectorMoveDistance = NormalPosition - CameraNowPosition;//距離為正常位置減當前位置
            Move = VectorMoveDistance * Time.deltaTime * 5;
            transform.position += Move;
        }
        else if (FowardAndBackTime <= 0)
        {
            CameraState = "NormalState";
        }
    }
    public void GettingPossess()
    {
        SoulVisionEffect.SetActive(false);
        AttachedBodyChildren = PossessTarget.GetComponentsInChildren<Transform>();
        if (PossessTime < 0.2)//鏡頭回到正常的位置
        {
            if (LockingAnimal)
            {
                PossessTime += Time.deltaTime;
                VectorMoveDistance = transform.rotation * Normal003.transform.localPosition + PlayerView.transform.position - transform.position;
                Move = VectorMoveDistance * Time.deltaTime * 5;
                transform.position += Move;
            }
            else
            { 
                PossessTime += Time.deltaTime;
                VectorMoveDistance = NormalPosition - transform.position;
                Move = VectorMoveDistance * Time.deltaTime * 5;
                transform.position += Move;
            }
        }
        else if (PossessTime >= 0.2 && PossessTime < 0.8)
        {

            PossessTime += Time.deltaTime;

            if (PossessTime >= 0.6 && PossessTime < 0.8)
            {
                if (LockingAnimal)
                {
                    PossessEffect.SetActive(true);
                    VectorMoveDistance = transform.rotation * Normal003.transform.localPosition + AttachedBodyChildren[2].transform.position - transform.rotation * Normal003.transform.localPosition - PlayerView.transform.position;//距離為終點減正常位置
                    Move = VectorMoveDistance * Time.deltaTime * 3;
                    transform.position += Move;
                    CameraNowPosition = transform.position;
                }
                else
                {
                    PossessEffect.SetActive(true);
                    VectorMoveDistance = AttachedBodyChildren[2].transform.position - PlayerView.transform.position;//距離為終點減正常位置
                    Move = VectorMoveDistance * Time.deltaTime * 3;
                    transform.position += Move;
                    CameraNowPosition = transform.position;
                }

               
            }
        }
        else if (PossessTime >= 0.8)
        {
            PossessEffect.SetActive(false);
            PossessedSystem.InToPossess();
            LoadCharacterPosition();//讀取動物鏡頭位置
            CameraState = "NormalState";
        }
    }
    public void LoadCharacterPosition()
    {
        switch (playerManager.PossessType)
        {
            case "Human":
                PossessedSystem = GameObject.Find("Pine").GetComponent<PossessedSystem>();
                NowCharacter = GameObject.Find("Pine");
                PlayerView = GameObject.Find("FirstPersonCamPoint");
                MoveEnd = GameObject.Find("CamMoveEndPoint");
                break;
            case "WolfMaster":
                NowCharacter = PossessTarget;
                PossessedSystem = PossessTarget.GetComponent<PossessedSystem>();
                AttachedBodyChildren = PossessedSystem.AttachedBody.GetComponentsInChildren<Transform>();
                PlayerView = AttachedBodyChildren[2].transform.gameObject;
                MoveEnd = AttachedBodyChildren[1].transform.gameObject;
                break;

        }
    }
}
