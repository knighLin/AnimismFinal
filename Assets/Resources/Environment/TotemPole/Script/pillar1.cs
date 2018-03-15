using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillar1 : MonoBehaviour
{
   

    public Vector3 ChangePosition;//相機要改變的位置
    
    public int Nowlevel;//外填樓層

    public GameObject camera_pos;//相機
    

    public pillarSystem pillarsys;


    void Update()
    {


        if (pillarSystem.pillarSystemBool == true)
        { //圖騰柱有沒有被啟動


            if (  Mathf.Abs(Input.GetAxis("Horizontal")) > 0)//判斷圖騰柱 進入階層 & 按下旋轉
            {
               
               
                int pausetime = Random.Range(0, 3);//圖騰柱卡卡的轉不太動的感覺
                if (pausetime == 1)
                {
                    //Debug.Log("eeaa");

                    pillarsys.pillarrota1();
                }

            }
          
          
        }


    } 

    void OnMouseDown()//被選擇進入後
    {
        if (pillarSystem.pillarSystemBool == true)
        {

camera_pos.gameObject.transform.position = ChangePosition;//相機移到指定位置
            changLevel();
            
            pillarsys.playersetativeF();


        }
    }
    public void changLevel() {
switch (Nowlevel)
            {//判斷進入的階層
                case 1:
                    Debug.Log("階層 1");
                pillarSystem.pillarLevel = 1;

                    break;
                case 2:
                    Debug.Log("階層 2");
                pillarSystem.pillarLevel = 2;
                    break;
                case 3:
                    Debug.Log("階層 3");
                pillarSystem.pillarLevel = 3;
                    break;
            }

    }


    

}


