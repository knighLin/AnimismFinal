using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class LoadOrDelet : MonoBehaviour {
    [SerializeField] private GameObject LoadGameMenu,QuestionBoard, Yes;
    [SerializeField] private Image Sure;
    public bool Select=false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("TriangleAbility") && Select)
        {
            switch (this.name)
            {
                case "H_LoadData1":
                        QuestionBoard.SetActive(true);
                        Sure.sprite = Resources.Load("UI/Homepage/DEL_Q", typeof(Sprite)) as Sprite;
                        Yes.GetComponent<ButtonScript>().YesChooseData = 1;
                        Yes.GetComponent<ButtonScript>().LoadOrDelet = 1;
                        Yes.GetComponent<Button>().Select();
                        LoadGameMenu.SetActive(false);
                    break;
                case "H_LoadData2":
                        QuestionBoard.SetActive(true);
                        Sure.sprite = Resources.Load("UI/Homepage/DEL_Q", typeof(Sprite)) as Sprite;
                        Yes.GetComponent<ButtonScript>().YesChooseData = 2;
                        Yes.GetComponent<ButtonScript>().LoadOrDelet = 1;
                        Yes.GetComponent<Button>().Select();
                        LoadGameMenu.SetActive(false);
                    break;
                case "H_LoadData3":
                        QuestionBoard.SetActive(true);
                        Sure.sprite = Resources.Load("UI/Homepage/DEL_Q", typeof(Sprite)) as Sprite;
                        Yes.GetComponent<ButtonScript>().YesChooseData = 3;
                        Yes.GetComponent<ButtonScript>().LoadOrDelet = 1;
                        Yes.GetComponent<Button>().Select();
                        LoadGameMenu.SetActive(false);
                    break;
            }
        }
        else if(Input.GetButtonDown("CircleUnpossess") && Select)
        {
            switch (this.name)
            {
                case "H_LoadData1":
                        QuestionBoard.SetActive(true);
                        Sure.sprite = Resources.Load("UI/Homepage/LOAD_Q", typeof(Sprite)) as Sprite;
                        Yes.GetComponent<ButtonScript>().YesChooseData = 1;
                        Yes.GetComponent<ButtonScript>().LoadOrDelet = 2;
                        Yes.GetComponent<Button>().Select();
                        LoadGameMenu.SetActive(false);
                    break;
                case "H_LoadData2":
                        QuestionBoard.SetActive(true);
                        Sure.sprite = Resources.Load("UI/Homepage/LOAD_Q", typeof(Sprite)) as Sprite;
                        Yes.GetComponent<ButtonScript>().YesChooseData = 2;
                        Yes.GetComponent<ButtonScript>().LoadOrDelet = 2;
                        Yes.GetComponent<Button>().Select();
                        LoadGameMenu.SetActive(false);
                    break;
                case "H_LoadData3":

                        QuestionBoard.SetActive(true);
                        Sure.sprite = Resources.Load("UI/Homepage/LOAD_Q", typeof(Sprite)) as Sprite;
                        Yes.GetComponent<ButtonScript>().YesChooseData = 3;
                        Yes.GetComponent<ButtonScript>().LoadOrDelet = 2;
                        Yes.GetComponent<Button>().Select();
                        LoadGameMenu.SetActive(false);
                    break;
            }
        }
    }
}
