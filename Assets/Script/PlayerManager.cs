using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string NowType, PossessType;
    public GameObject NowCharacter;
    private string PreviousType;
    [SerializeField]private TypeValue typevalue;
    

    void Awake()
    {
        PossessType = "Human";//一開始型態為Human
        NowType = "Human";//一開始型態為Human
        PreviousType = "Human";
        typevalue.HumanVal();
    }

    private void Start()
    {
        NowCharacter = GameObject.FindWithTag("Player");
    }
    //void Update()
    //{
    //    if (NowType != PreviousType)//如果數值沒有變化就不做數值改變，反之則要
    //    {
    //        switch (NowType)
    //        {//給予回應 //判斷目前形態為何，不同型態應有不同數值
    //            case "Human":
    //                typevalue.HumanVal();
    //                break;
    //            case "Bear":
    //                typevalue.BearVal();
    //                break;
    //            case "Wolf":
    //                typevalue.WolfVal();
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}

    public void TurnType(string TypeTag, string Previous)//收到物件的回饋
    {
        switch (TypeTag)
        {
            case "Human":
                NowType = "Human";
                PreviousType = Previous;
                typevalue.HumanVal();
                break;

            case "Bear":
                NowType = "Bear";
                PreviousType = Previous;
                typevalue.BearVal();
                break;

            case "Wolf":
                NowType = "Wolf";
                PreviousType = Previous;
                typevalue.WolfVal();
                break;
        }
    }
}
