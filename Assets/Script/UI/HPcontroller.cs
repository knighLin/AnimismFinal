using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPcontroller : MonoBehaviour
{
    public Health Health;
    [SerializeField]private PlayerManager playerManager;
    public Image HpB, HpW, HpR;//腳色血條圖片
    public Image FaceB, FaceW, FaceR;//腳色臉圖片
    public float BlinkTime;
    public float PineHpMax, PineHpNow, WolfHpMax, WolfHpNow, BearHpMax, BearHpNow, DeerHpMax, DeerHpNow;//腳色血量數值
    public bool Blink;

    // Use this for initialization
    void Start()
    {
        Blink = false;
      //  CharacterSwitch();
    }

    // Update is called once per frame
    void Update()
    {
        if (Blink)//閃爍
            UIBlink();
        else
            BlinkTime = 0;
    }

    public void CharacterSwitch()
    {
        switch (playerManager.PossessType)
        {
            case "Human":
                FaceB.sprite = Resources.Load("UI/Avantar/Pine/pine_bak", typeof(Sprite)) as Sprite;
                FaceW.sprite = Resources.Load("UI/Avantar/Pine/pine_front", typeof(Sprite)) as Sprite;
                FaceR.sprite = Resources.Load("UI/Avantar/Pine/pine_front", typeof(Sprite)) as Sprite;
                break;
            case "WolfMaster":
                FaceB.sprite = Resources.Load("UI/Avantar/Wolf/wolf_bak", typeof(Sprite)) as Sprite;
                FaceW.sprite = Resources.Load("UI/Avantar/Wolf/wolf_front", typeof(Sprite)) as Sprite;
                FaceR.sprite = Resources.Load("UI/Avantar/Wolf/wolf_front", typeof(Sprite)) as Sprite;
                break;
            case "BearMaster":
                FaceB.sprite = Resources.Load("UI/Avantar/Bear/BEAR_BUTTOM", typeof(Sprite)) as Sprite;
                FaceW.sprite = Resources.Load("UI/Avantar/Bear/BEAR_WHITE", typeof(Sprite)) as Sprite;
                FaceR.sprite = Resources.Load("UI/Avantar/Bear/BEAR_WHITE", typeof(Sprite)) as Sprite;
                break;
            case "DeerMaster":
                FaceB.sprite = Resources.Load("UI/Avantar/Deer/DEER_BUTTOM", typeof(Sprite)) as Sprite;
                FaceW.sprite = Resources.Load("UI/Avantar/Deer/DEER_WHITE", typeof(Sprite)) as Sprite;
                FaceR.sprite = Resources.Load("UI/Avantar/Deer/DEER_WHITE", typeof(Sprite)) as Sprite;
                break;
        }
    }
    public void UIBlink()
    {
        BlinkTime += Time.deltaTime * 8;
        if (BlinkTime <= 5)
        {
            if (BlinkTime % 2 < 1)
            {
                HpR.sprite = Resources.Load("UI/Avantar/Hp/hp_front", typeof(Sprite)) as Sprite;
                switch (playerManager.NowType)
                {
                    case "Human":
                        FaceR.sprite = Resources.Load("UI/Avantar/Pine/pine_front", typeof(Sprite)) as Sprite;
                        break;
                    case "Wolf":
                        FaceR.sprite = Resources.Load("UI/Avantar/Wolf/wolf_front", typeof(Sprite)) as Sprite;
                        break;
                    case "Bear":
                        FaceR.sprite = Resources.Load("UI/Avantar/Bear/BEAR_WHITE", typeof(Sprite)) as Sprite;
                        break;
                    case "Deer":
                        FaceR.sprite = Resources.Load("UI/Avantar/Deer/DEER_WHITE", typeof(Sprite)) as Sprite;
                        break;
                    case "WolfGuard":
                        break;
                }
            }
            else
            {
                HpR.sprite = Resources.Load("UI/Avantar/Hp/hp_hurt", typeof(Sprite)) as Sprite;
                switch (playerManager.NowType)
                {
                    case "Human":
                        FaceR.sprite = Resources.Load("UI/Avantar/Pine/pine_hurt", typeof(Sprite)) as Sprite;
                        break;
                    case "Wolf":
                        FaceR.sprite = Resources.Load("UI/Avantar/Wolf/wolf_hurt", typeof(Sprite)) as Sprite;
                        break;
                    case "Bear":
                        FaceR.sprite = Resources.Load("UI/Avantar/Bear/BEAR_RED", typeof(Sprite)) as Sprite;
                        break;
                    case "Deer":
                        FaceR.sprite = Resources.Load("UI/Avantar/Deer/DEER_RED", typeof(Sprite)) as Sprite;
                        break;
                    case "WolfGuard":
                        break;
                }
            }
        }
        else Blink = false;
    }
    public void CharacterHpControll()
    {
        if (Health!= GameObject.FindGameObjectWithTag("Player").GetComponent<Health>())
            Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (Health)
        { 
            HpW.fillAmount = (Health.currentHealth * 0.74f + Health.MaxHealth * 0.26f) / Health.MaxHealth;//(75%當前血量+25%血量最大值)/血量最大值
            HpR.fillAmount = (Health.currentHealth * 0.74f + Health.MaxHealth * 0.26f) / Health.MaxHealth;//Ex:當前血20 血量最大值100 為 (20*75%+100*25%)/100 = 0.4
        }
    }
    public void WolfGuardHpControll()
    {
        if (Health != GameObject.FindGameObjectWithTag("WolfGuard").GetComponent<Health>())
            Health = GameObject.FindGameObjectWithTag("WolfGuard").GetComponent<Health>();
        if (Health)
        {
            HpW.fillAmount = (Health.currentHealth * 0.74f + Health.MaxHealth * 0.26f) / Health.MaxHealth;//(75%當前血量+25%血量最大值)/血量最大值
            HpR.fillAmount = (Health.currentHealth * 0.74f + Health.MaxHealth * 0.26f) / Health.MaxHealth;//Ex:當前血20 血量最大值100 為 (20*75%+100*25%)/100 = 0.4
        }
    }
}
