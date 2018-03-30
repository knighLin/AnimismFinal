using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HintDetector : MonoBehaviour
{
    //private PlayerMovement PlayerMovement;
    private CharacterController characterController;
    private PossessedSystem PossessedSystem;
    [SerializeField] private PlayerManager PlayerManager;
    [SerializeField] private CameraScript CameraScript;
    [SerializeField] private GameObject Canvas, Triangle, Square, Circle, Cross, R1, R2, L1, L2, Spin, DeerHint, WolfHint, BearHint;
    private List<GameObject> HintRange = new List<GameObject>();
    private RaycastHit hit;//點擊的物件
    private GameObject HitObj;
    private bool CanPossess, SoulVision, IsPillar;

    private void Start()
    {
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }
    private void Update()
    {

        if (GameObject.FindWithTag("Player"))
            transform.position = GameObject.FindWithTag("Player").transform.position;
        else
            transform.position = GameObject.Find("Pine").transform.position;
        if (CameraScript.CameraState == "EnterSoulVision" || CameraScript.CameraState == "SoulVisionOver" || CameraScript.CameraState == "GettingPossess")
        {
            Canvas.SetActive(false);
        }
        else//如果在進入靈視、退出靈視、進入附身狀態不要有UI
        {
            Canvas.SetActive(true);
            if (PlayerManager.PossessType != "Human")
            {
                if (PlayerManager.PossessType == "Pillar")
                {
                    DeerHint.SetActive(false);
                    WolfHint.SetActive(false);
                    BearHint.SetActive(false);
                    IsPillar = true;
                    R1.SetActive(false);
                    Spin.SetActive(true);
                    Triangle.SetActive(true);
                    Triangle.GetComponent<Image>().sprite = Resources.Load("UI/Hint/T_up", typeof(Sprite)) as Sprite;
                    Cross.SetActive(true);
                    Cross.GetComponent<Image>().sprite = Resources.Load("UI/Hint/X_down", typeof(Sprite)) as Sprite;
                }
                else
                {
                    if (CanPossess)
                    {
                        Triangle.SetActive(true);
                        Triangle.GetComponent<Image>().sprite = Resources.Load("UI/Hint/T_getout", typeof(Sprite)) as Sprite;
                    }
                    else
                    {
                        Triangle.SetActive(true);
                        Triangle.GetComponent<Image>().sprite = Resources.Load("UI/Hint/T_skill", typeof(Sprite)) as Sprite;
                    }
                    IsPillar = false;
                    Cross.GetComponent<Image>().sprite = Resources.Load("UI/Hint/X_jump", typeof(Sprite)) as Sprite;
                    if (SoulVision)
                    {
                        Circle.SetActive(false);
                        Cross.SetActive(false);
                    }
                    else
                    {
                        Circle.SetActive(true);
                        Cross.SetActive(true);
                    }
                }
            }
            else
            {
                if (CanPossess)
                {
                    Triangle.SetActive(true);
                    Triangle.GetComponent<Image>().sprite = Resources.Load("UI/Hint/T_getout", typeof(Sprite)) as Sprite;
                }
                else
                {
                    Triangle.SetActive(false);
                }
                IsPillar = false;
                Cross.SetActive(true);
                Cross.GetComponent<Image>().sprite = Resources.Load("UI/Hint/X_jump", typeof(Sprite)) as Sprite;
                Circle.SetActive(false);
                Spin.SetActive(false);
                if (SoulVision)
                    Cross.SetActive(false);
                else
                    Cross.SetActive(true);
            }
            if (CameraScript.CameraState == "SoulVision" || CameraScript.CameraState == "SoulVisionLocking")//靈視
            {
                PossessedSystem = GameObject.FindWithTag("Player").GetComponent<PossessedSystem>();
                SoulVision = true;
                L1.SetActive(false);
                L2.SetActive(false);
                Cross.SetActive(false);
                if (PossessedSystem.RangeObject.Count > 0)//有動物才能鎖定
                    R1.SetActive(true);
                else
                    R1.SetActive(false);
            }
            else if (!IsPillar)
            {
                R1.SetActive(false);
                SoulVision = false;
                L1.SetActive(true);
                L2.SetActive(true);
            }
            if (Input.GetButton("R2Run") || SoulVision)
                R2.SetActive(false);
            else if (!IsPillar)
                R2.SetActive(true);
            if (GameObject.FindWithTag("Player") && !SoulVision)
            {
                // PlayerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
                characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
                if (characterController.isGrounded)
                    Cross.SetActive(true);
                else
                    Cross.SetActive(false);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 12);
            if (Physics.Raycast(ray, out hit))//準心碰到物件
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 0.1f, true);
                if (hit.transform.gameObject != HitObj)
                {
                    HitObj = hit.transform.gameObject;
                    switch (hit.transform.tag)
                    {
                        case "WolfMaster":
                            if (SoulVision)
                            {
                                CanPossess = true;
                                //hit.transform.GetComponentsInChildren<Transform>()[3].gameObject.SetActive(true);
                            }
                            else
                            {
                                CanPossess = false;
                            }
                            break;
                        case "BearMaster":
                            break;
                        case "Pillar":
                            if (!SoulVision)
                            {
                                switch (hit.collider.name)
                                {
                                    case "TotemPole_Deer":
                                        Triangle.SetActive(false);
                                        CanPossess = true;
                                        break;
                                    case "TotemPole_Wolf":
                                        Triangle.SetActive(false);
                                        CanPossess = true;
                                        break;
                                    case "TotemPole_Bear":
                                        Triangle.SetActive(false);
                                        CanPossess = true;
                                        break;
                                }
                            }
                            else
                            {
                                Triangle.SetActive(true);
                                CanPossess = false;
                            }
                            break;
                        case "Enemy":
                            break;
                        default:
                            CanPossess = false;
                            Spin.SetActive(false);
                            Square.SetActive(false);
                            return;
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "WolfMaster":
                if (SoulVision|| PlayerManager.PossessType == other.tag)
                    other.transform.GetChild(2).gameObject.SetActive(false);
                else
                    other.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case "BearMaster":
                break;
            case "Pillar":
                if (SoulVision || PlayerManager.PossessType == other.tag)
                {
                    DeerHint.SetActive(false);
                    WolfHint.SetActive(false);
                    BearHint.SetActive(false);
                }
                else
                {
                    DeerHint.SetActive(true);
                    WolfHint.SetActive(true);
                    BearHint.SetActive(true);
                }
                break;
            case "Enemy":
                Square.SetActive(true);
                break;
            default:
                return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "WolfMaster":
                other.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case "BearMaster":
                break;
            case "Pillar":
                DeerHint.SetActive(false);
                WolfHint.SetActive(false);
                BearHint.SetActive(false);
                break;
            case "Enemy":
                Square.SetActive(false);
                break;
            default:
                return;
        }
    }
}
