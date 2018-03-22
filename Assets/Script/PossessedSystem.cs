using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PossessedSystem : MonoBehaviour
{
    private Attack attack;
    private PlayerMovement playerMovement;
    private PlayerManager playerManager;
    private HPcontroller HPcontroller;
    private CameraScript CameraScript;
    private pillar1 pillar1;

    public Collider Target;
    public static bool OnPossessed = false;//附身狀態
    public static GameObject AttachedBody;//附身物
    public static SphereCollider PossessedCol;//附身範圍
    public LayerMask PossessedLayerMask;//可被附身物的階層
    public static int WolfCount;//狼的連續附身次數
    public List<Collider> RangeObject = new List<Collider>();//範圍附身物
    public GameObject Possessor;//人的型態
    private RaycastHit hit;//點擊的動物物件
    private string PreviousTag;//附身前的標籤
    public bool ChooseRightObject;
    private bool clear = true;

    private Animator animator;

    //audio
    private AudioSource audioSource;
    public AudioClip HumanSurgery;
    public AudioClip WolfSurgery;
    private void Awake()
    {
        attack = GetComponent<Attack>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        HPcontroller = GameObject.Find("PlayerManager").GetComponent<HPcontroller>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Possessor = GameObject.Find("Pine");
    }
    private void Start()
    {
        if (!Possessor)
            Possessor = GameObject.Find("Pine");
    }
    private void OnEnable()//當打開程式，呼叫當前正開啟的物件的附身範圍和移動
    {
        if (!Possessor)
            Possessor = GameObject.Find("Pine");
        attack = GetComponent<Attack>();
        playerMovement = GetComponent<PlayerMovement>();
        PossessedCol = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CameraScript.CanPossess)//打開附身系統
        {
            if (clear)//開啟附身系統只清一次，播放一次動畫
            {
                animator.SetTrigger("Surgery");//播放附身動畫
                playerMovement.enabled = false;
                attack.enabled = false;
                //清掉之前範圍的動物物件和Highlight
                RangeObject.Clear();
                clear = false;
                if (Possessor.tag == "Player")//判斷目前形體，播放不同附身的音效
                {
                    audioSource.PlayOneShot(HumanSurgery);
                }
                else if (Possessor.tag == "Wolf")
                {
                    audioSource.PlayOneShot(WolfSurgery);
                }
            }
            PossessedCol.enabled = true;
            if (Time.timeScale == 1f)
                Time.timeScale = 0.5f;//如果時間正常則遊戲慢動作
            MouseChoosePossessed();
        }
        else
        {
            if (!CameraScript.IsPossessing)//附身不能動
            {
                playerMovement.enabled = true;
                attack.enabled = true;
            }
            clear = true;//讓下次開啟附身清理範圍物件
            PossessedCol.enabled = false;//附身範圍collider關閉
            if (Time.timeScale == 0.5f)
                Time.timeScale = 1f;//如果有變慢 才取消慢動作
                                    //joycontroller.joypossessed = false; //搖桿
        }

        if (Input.GetButtonDown("CircleUnpossess") && AttachedBody != null && !CameraScript.IsPossessing && !CameraScript.CantLeftPossess)//解除附身
        {
            LifedPossessed();//離開附身物
        }
    }

    public void MouseChoosePossessed()//滑鼠點擊附身物
    {
        if (ChooseRightObject)//如果點到可附身物件
        {
            playerMovement.enabled = false;//附身不能動
            if (hit.collider.tag == "Bear" || hit.collider.tag == "Wolf" || hit.collider.tag == "Deer")
                CameraScript.PossessTarget = hit.collider.gameObject.transform.parent.gameObject;
            else
                CameraScript.PossessTarget = hit.collider.gameObject;
            ChooseRightObject = false;//重置
            CameraScript.CameraState = "GettingPossess";//轉為附身模式
        }
        else if ((Input.GetButtonDown("TriangleAbility") && PossessedSystem.PossessedCol.enabled == true))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 15, PossessedLayerMask);
            // Debug.DrawLine(ray.origin, hit.transform.position, Color.red, 1f, true);
            for (int i = 0; i < RangeObject.Count; i++)
            {

                //if (!hit.collider.CompareTag("Player"))//如果是自己本身不執行
                //{
                if (hit.collider == RangeObject[i])//當點擊的物件是附身範圍裡的物件時
                {
                    ChooseRightObject = true;
                    break;
                }
                else
                    ChooseRightObject = false;
                //}
            }
        }
    }

    public void InToPossess()
    {
        Target = hit.collider;
        EnterPossessed();//執行附身
        Time.timeScale = 1f;//慢動作回覆
        PossessedCol.enabled = false;//關掉附身範圍
    }

    public void EnterPossessed()//附身
    {
        if (Target.tag == "Bear" || Target.tag == "Wolf" || Target.tag == "Deer")
        {
            if (Target.gameObject != this.gameObject)//當下一個物件不是目前物件時，可以繼續附身
            {
                if (AttachedBody != null && OnPossessed == true)//如果先前有附身物，而且正在附身
                {
                    AttachedBody.tag = Possessor.tag + "Master";//將TAG換回原本的
                    Possessor.transform.parent = null;//將玩家物件分離出現在的被附身物
                    AttachedBody.GetComponent<PlayerMovement>().enabled = false;
                    AttachedBody.GetComponent<PossessedSystem>().enabled = false;
                }
                PreviousTag = Possessor.tag;//附身後將先前附身的tag存起來
                Possessor.tag = Target.tag;//將目前人的tag轉為附身後動物的
                AttachedBody = Target.gameObject.transform.parent.gameObject;//讓新的附身物等於AttachedBody
                playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
                playerManager.NowCharacter = AttachedBody;
                playerManager.PossessType = AttachedBody.tag;
                switch (AttachedBody.transform.tag)
                {//將附身物的標籤傳到管理者，方便變換動物數值
                    case "BearMaster":
                        playerManager.TurnType("Bear", PreviousTag);
                        break;
                    case "WolfMaster":
                        playerManager.TurnType("Wolf", PreviousTag);
                        break;
                }
                AttachedBody.tag = "Player";//將目前附身動物tag改成player
                                            //附身者的位置到新被附身物的位置
                Possessor.transform.position = AttachedBody.transform.position;

                PossessedCol.enabled = false;//關掉當前附身範圍
                Possessor.transform.parent = AttachedBody.transform;//將附身者變為被附身物的子物件
                Possessor.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                AttachedBody.GetComponent<PlayerMovement>().enabled = true;//打開動物的移動和附身
                AttachedBody.GetComponent<PossessedSystem>().enabled = true;//打開動物的附身系統
                AttachedBody.GetComponent<Attack>().enabled = true;//攻擊
                AttachedBody.GetComponent<Health>().enabled = true;
                Possessor.SetActive(false);//關掉人型態的任何事
                OnPossessed = true;//已附身

                if (Target.tag == "Wolf")
                {
                    WolfCount++;
                }
                else
                {
                    WolfCount = 0;
                }
            }
            HPcontroller.Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            HPcontroller.CharacterHpControll();
            HPcontroller.CharacterSwitch();//換圖片
        }
        else if (Target.tag == "Pillar")
        {
            if (Target.gameObject != this.gameObject)//當下一個物件不是目前物件時，可以繼續附身
            {
                if (AttachedBody != null && OnPossessed == true)//如果先前有附身物，而且正在附身
                {
                    AttachedBody.tag = Possessor.tag + "Master";//將TAG換回原本的
                    Possessor.transform.parent = null;//將玩家物件分離出現在的被附身物
                    Debug.Log(AttachedBody);
                    AttachedBody.GetComponent<PlayerMovement>().enabled = false;
                    AttachedBody.GetComponent<PossessedSystem>().enabled = false;
                    Possessor.SetActive(true);
                }
                Possessor.tag = "Player";//進入圖騰柱人的tag先為Player
                playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
                playerManager.TurnType("Human", "Human");//附身圖騰柱後狀態先設為人 解除後才不用再抓一次
                playerManager.PossessType = "Human";
                HPcontroller.Health = Possessor.GetComponent<Health>();
                HPcontroller.CharacterHpControll();
                HPcontroller.CharacterSwitch();//換圖片
                GameObject.Find("FaceBlack").GetComponent<Image>().color = new Color(1, 1, 1, 0);//隱藏血量
                GameObject.Find("FaceWhite").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("FaceRed").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("HpBlack").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("HpWhite").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("HpRed").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                AttachedBody = Target.gameObject;//讓新的附身物等於AttachedBody
                playerManager.NowCharacter = AttachedBody;
                playerManager.PossessType = AttachedBody.tag;
                PossessedCol.enabled = false;//關掉當前附身範圍
                OnPossessed = true;//已附身
                pillar1 = AttachedBody.GetComponent<pillar1>();
                pillar1.changLevel();
                Possessor.GetComponent<PlayerMovement>().enabled = false;
                Possessor.transform.position = GameObject.Find("TotemPole").transform.position + new Vector3(0, 0, -3);
                Possessor.transform.rotation = Quaternion.Euler(0, 0, 0);
                Possessor.tag = "Pillar";//設為不是Player敵人才抓不到
            }
        }
    }

    void OnTriggerEnter(Collider Object)//送出訊息
    {
        switch (Object.transform.tag)
        {//判斷是不是可以附身的物件
         //case "Human":
            case "Bear":
            case "Wolf":
            case "Deer":
            case "Pillar":
                break;
            default:
                return;
        }
        RangeObject.Add(Object);
    }

    public void LifedPossessed()//解除變身
    {
        if (playerManager.PossessType != "Pillar")
        {
            Possessor.transform.parent = null;//將玩家物件分離出被附身物
            Possessor.transform.position = new Vector3(AttachedBody.transform.position.x + 1.5f, transform.position.y + 0.5f, AttachedBody.transform.position.z + 1.5f);
            //將被附身物與人的位置分離
            //關閉動物所有程式
            AttachedBody.GetComponent<PlayerMovement>().enabled = false;
            AttachedBody.GetComponent<PossessedSystem>().enabled = false;
            AttachedBody.GetComponent<Attack>().enabled = false;
            AttachedBody.GetComponent<Health>().enabled = false;
            AttachedBody.tag = Possessor.tag + "Master";//將TAG換回原本的

            Possessor.GetComponent<PlayerMovement>().enabled = true;
            Possessor.tag = "Player";//將型態變回Human
            Possessor.SetActive(true);//打開人型態的任何事
            playerManager.TurnType("Human", AttachedBody.tag);//將標籤傳至管理者，變換數值
            AttachedBody = null;//解除附身後清除附身物，防止解除附身後按Ｑ還有反應
            OnPossessed = false;//取消附身
            playerManager.NowCharacter = Possessor;
            playerManager.PossessType = "Human";
            CameraScript.LoadCharacterPosition();

            HPcontroller.CharacterSwitch();
            HPcontroller.Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            HPcontroller.CharacterHpControll();
        }
        else
        {
            GameObject.Find("FaceBlack").GetComponent<Image>().color = new Color(1, 1, 1, 1);//開啟血量
            GameObject.Find("FaceWhite").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.Find("FaceRed").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.Find("HpBlack").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.Find("HpWhite").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.Find("HpRed").GetComponent<Image>().color = new Color(1, 1, 1 , 1);
            Possessor.tag = "Player";
            CameraScript.rotX = 0;
            CameraScript.rotY = 0;
            Possessor.GetComponent<PlayerMovement>().enabled = true;
            AttachedBody = null;//解除附身後清除附身物，防止解除附身後按Ｑ還有反應
            OnPossessed = false;//取消附身
            playerManager.NowCharacter = Possessor;
            playerManager.PossessType = "Human";
            CameraScript.LoadCharacterPosition();
        }
    }
}