using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarSystem : MonoBehaviour {

    public static bool pillarSystemBool;

   
    private int rotatespeed = 10;
    private float shakelevel = 0.13f;

    public GameObject[] pillarother = new GameObject[3];

    public GameObject player;
    private Transform peopleVector;//紀錄原本位置

    public static int pillarLevel;//目前階層

    bool p1bool;
    bool p2bool;
    bool p3bool;

    // Update is called once per frame
    void Update()
    {
        if (pillarSystemBool == true)
        {

            if (pillarLevel == 1 || pillarLevel == 2 || pillarLevel == 3)
            {
                

                    
                    if (Input.GetKeyDown(KeyCode.UpArrow) && pillarLevel != 3)
                    {

                        pillarLevel += 1;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow) && pillarLevel != 1)
                    {

                        pillarLevel -= 1;
                    }
            }
        }
        if (Input.GetKeyDown("e"))
        {//要改寫成進入靈視模式， 應該要在外面把pillarSystem布林 啟動/關閉
            pillarSystemBool = true;
        }
    }

    public  void  pillarrota1()
    {
        StartCoroutine("pillarShakeTurn");//轉動效果
    }


    IEnumerator pillarShakeTurn() //圖騰柱轉動效果
    {
        //轉乎快忽慢+放開會有飄移
        int pillarnum = pillarLevel;

            yield return new WaitForSeconds(0.1f);
            float rotation = Input.GetAxis("Horizontal") * rotatespeed + Random.Range(0, 3f);

            rotation *= Time.deltaTime;


            //------------------------------

           // transform.Rotate(shakelevel, rotation, 0);//上震動+旋轉
        
           
            if (pillarnum == 1)
            {
                pillarother[0].gameObject.transform.Rotate(shakelevel, -rotation, 0);
                pillarother[1].gameObject.transform.Rotate(shakelevel, -rotation, 0);
            pillarother[2].gameObject.transform.Rotate(shakelevel, -rotation, 0);
        }

        if (pillarnum == 2)
        {
            pillarother[2].gameObject.transform.Rotate(shakelevel, -rotation, 0);
            pillarother[1].gameObject.transform.Rotate(shakelevel, -rotation, 0);
            
        }
        if (pillarnum == 3)
        {
            pillarother[2].gameObject.transform.Rotate(shakelevel, -rotation, 0);
        }

        yield return new WaitForSeconds(0.01f);
            //transform.Rotate(-shakelevel, rotation, 0);//下震動

            if (pillarnum == 1)
            {
                pillarother[0].gameObject.transform.Rotate(-shakelevel, -rotation, 0);
            pillarother[1].gameObject.transform.Rotate(-shakelevel, -rotation, 0);
            pillarother[2].gameObject.transform.Rotate(-shakelevel, -rotation, 0);

        }
        if (pillarnum == 2)
        {
            pillarother[2].gameObject.transform.Rotate(-shakelevel, -rotation, 0);
            pillarother[1].gameObject.transform.Rotate(-shakelevel, -rotation, 0);
        }
        if (pillarnum == 3)
        {
            pillarother[2].gameObject.transform.Rotate(-shakelevel, -rotation, 0);
        }

        angleJudge();
    }

    public void playersetativeF()
    {
        peopleVector = player.gameObject.transform;//記錄人物原本位置
        player.SetActive(false);
    }

    private void pillarSystemEnd()
    {
        player.gameObject.transform.position = peopleVector.position;
        player.gameObject.transform.rotation = peopleVector.rotation;
    }

    public void angleJudge()
    {
        //就是算角度有沒有對，外一個函數抓取每個pillar的 pillarangle值都true就代表角度都正確4
        float p1 = pillarother[0].gameObject.transform.rotation.y;
        float p2 = pillarother[1].gameObject.transform.rotation.y;
        float p3 = pillarother[2].gameObject.transform.rotation.y;
        if (p1 > 0 && p1 < 0.1)
        {
            Debug.Log("P1OK");
            p1bool = true;
            succeedpillar();
        }else {
            p1bool = false;
        }

        if (p2 > 0 && p2 < 0.1)
        {
            Debug.Log("P2OK");
            p2bool = true;
            succeedpillar();
        } else
        {
            p2bool = false;
        }

        if (p3 > 0 && p3 < 0.1)
        {
            Debug.Log("P3OK");
            p3bool = true;
            succeedpillar();
        }else
        {
            p3bool = false;
        }


    }
    void succeedpillar() { //轉到指定角度
        if (p1bool = true &&  p2bool == true && p3bool == true)
        {
            player.SetActive(true);
            pillarSystemBool = false;
        }
    }
}
