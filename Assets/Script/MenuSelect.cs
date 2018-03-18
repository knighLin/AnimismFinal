using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour {
    [SerializeField] private GameObject Quest, Save, Setting, Options;
    private float H;
    // Use this for initialization

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            H += Input.GetAxis("Horizontal");
            if (Mathf.Ceil(H) > 5)
            {
                switch (this.name)
                {
                    case "Quest":
                        Save.SetActive(true);
                        H = 0;
                        Quest.SetActive(false);
                        break;
                    case "Save":
                        Setting.SetActive(true);
                        H = 0;
                        Save.SetActive(false);
                        break;
                    case "Setting":
                        Options.SetActive(true);
                        H = 0;
                        Setting.SetActive(false);
                        break;
                    case "Options":
                        Quest.SetActive(true);
                        H = 0;
                        Options.SetActive(false);
                        break;
                }
            }
        }
        else if (Input.GetAxis("Horizontal") <0.01f)
        {
            H += Input.GetAxis("Horizontal");
            if (Mathf.Ceil(H) < -5)
            {
                switch (this.name)
                {
                    case "Quest":
                        Options.SetActive(true);
                        H = 0;
                        Quest.SetActive(false);
                        break;
                    case "Save":
                        Quest.SetActive(true);
                        H = 0;
                        Save.SetActive(false);
                        break;
                    case "Setting":
                        Save.SetActive(true);
                        H = 0;
                        Setting.SetActive(false);
                        break;
                    case "Options":
                        Setting.SetActive(true);
                        H = 0;
                        Options.SetActive(false);
                        break;
                }
            }
        }
    }
}
