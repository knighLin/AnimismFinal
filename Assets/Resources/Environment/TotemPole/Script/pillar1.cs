using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillar1 : MonoBehaviour
{
    public int Nowlevel;//外填樓層
    public pillarSystem pillarSystem;

    private void Start()
    {
        pillarSystem = GameObject.Find("TotemPole").GetComponent<pillarSystem>();
    }

    public void changLevel()
    {
        switch (Nowlevel)
        {//判斷進入的階層
            case 1:
                Debug.Log("階層 1");
                pillarSystem.pillarLevel = 1;//熊
                break;
            case 2:
                Debug.Log("階層 2");
                pillarSystem.pillarLevel = 2;//狼
                break;
            case 3:
                Debug.Log("階層 3");
                pillarSystem.pillarLevel = 3;//鹿
                break;
        }
    }
}


