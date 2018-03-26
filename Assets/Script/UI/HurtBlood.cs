using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtBlood : MonoBehaviour {
    [SerializeField] private Image[] Blood;
    public bool Up,Down,Left,Right;
    public float Uptime, Downtime, Lefttime, Righttime;
    [SerializeField] private CameraScript CameraScript; 
    // Use this for initialization
    private void Update()
    {
        if (Up)
        {
            if (Uptime < 1)
            {
                CameraScript.Shake = true;
                Uptime += Time.deltaTime/2;
                Blood[0].color = new Color(1, 1, 1, 1 - Uptime);
                Blood[4].color = new Color(1, 1, 1, 1 - Uptime);
                Blood[8].color = new Color(1, 1, 1, 1 - Uptime);
                if (Uptime >= 0.3)
                    CameraScript.Shake = false;
            }
            else if (Uptime>=1)
            {
                Uptime = 0;
                Up = false;
            }
        }
        if (Down)
        {
            if (Downtime < 1)
            {
                CameraScript.Shake = true;
                Downtime += Time.deltaTime / 2;
                Blood[1].color = new Color(1, 1, 1, 1 - Downtime);
                Blood[5].color = new Color(1, 1, 1, 1 - Downtime);
                Blood[9].color = new Color(1, 1, 1, 1 - Downtime);
                if (Downtime >= 0.3)
                    CameraScript.Shake = false;
            }
            else if (Downtime >= 1)
            {
                CameraScript.Shake = false;
                Downtime = 0;
                Down = false;
            }
        }
        if (Left)
        {
            if (Lefttime < 1)
            {
                CameraScript.Shake = true;
                Lefttime += Time.deltaTime / 2;
                Blood[3].color = new Color(1, 1, 1, 1 - Lefttime);
                Blood[7].color = new Color(1, 1, 1, 1 - Lefttime);
                Blood[11].color = new Color(1, 1, 1, 1 - Lefttime);
                if (Lefttime >= 0.3)
                    CameraScript.Shake = false;
            }
            else if (Lefttime >= 1)
            {
                CameraScript.Shake = false;
                Lefttime = 0;
                Left = false;
            }
        }
        if (Right)
        {
            if (Righttime < 1)
            {
                CameraScript.Shake = true;
                Righttime += Time.deltaTime / 2;
                Blood[2].color = new Color(1, 1, 1, 1 - Righttime);
                Blood[6].color = new Color(1, 1, 1, 1 - Righttime);
                Blood[10].color = new Color(1, 1,1, 1 - Righttime);
                if (Righttime >= 0.3)
                    CameraScript.Shake = false;
            }
            else if (Righttime >= 1)
            {
                CameraScript.Shake = false;
                Uptime = 0;
                Right = false;
            }
        }
    }
   
}
