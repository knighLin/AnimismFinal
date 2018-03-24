using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pillarSystem : MonoBehaviour
{

    public static bool pillarSystemBool;
    [SerializeField] private GameObject OverCanvas;
    [SerializeField] private Button ReturnButton;
    private int rotatespeed = 10;
    private float shakelevel = 0.2f;
    public GameObject[] pillarother = new GameObject[3];
    public static int pillarLevel;//目前階層

    bool p1bool;
    bool p2bool;
    bool p3bool;

    public void pillarrota1()
    {
        StartCoroutine(pillarShakeTurn());//轉動效果
    }

    IEnumerator pillarShakeTurn() //圖騰柱轉動效果
    {
        //轉乎快忽慢+放開會有飄移
        int pillarnum = pillarLevel;

        yield return new WaitForSeconds(0.1f);
        float rotation = Input.GetAxis("Horizontal") * rotatespeed + Random.Range(0, 3f);

        rotation *= Time.deltaTime * 3;

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
    public void angleJudge()
    {
        //就是算角度有沒有對，外一個函數抓取每個pillar的 pillarangle值都true就代表角度都正確4
        float p1 = pillarother[0].gameObject.transform.rotation.y;
        float p2 = pillarother[1].gameObject.transform.rotation.y;
        float p3 = pillarother[2].gameObject.transform.rotation.y;
        if (p1 > -0.1 && p1 < 0.1)
        {
            Debug.Log("P1OK");
            p1bool = true;
            succeedpillar();
        }
        else
        {
            p1bool = false;
        }

        if (p2 > -0.1 && p2 < 0.1)
        {
            Debug.Log("P2OK");
            p2bool = true;
            succeedpillar();
        }
        else
        {
            p2bool = false;
        }

        if (p3 > -0.1 && p3 < 0.1)
        {
            Debug.Log("P3OK");
            p3bool = true;
            succeedpillar();
        }
        else
        {
            p3bool = false;
        }
    }
    void succeedpillar()
    { //轉到指定角度
        if (p1bool == true && p2bool == true && p3bool == true&& GameObject.Find("PlayerManager").GetComponent<PlayerManager>().PossessType == "Pillar")
        {
            pillarother[0].tag = "Untagged";
            pillarother[1].tag = "Untagged";
            pillarother[2].tag = "Untagged";
            GameObject.Find("Pine").GetComponent<PossessedSystem>().LifedPossessed();
            GameObject.Find("Main Camera").GetComponent<CameraScript>().Shake=true;
            Invoke("TurnToCanvas", 2f);
        }
    }
    void TurnToCanvas()
    {
        GameObject.Find("Main Camera").GetComponent<CameraScript>().CameraMoveToCave();
        Time.timeScale = 0;
        //OverCanvas.SetActive(true);
        //ReturnButton.Select();
    }
}
