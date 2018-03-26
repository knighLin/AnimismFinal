using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSaveBoard : MonoBehaviour {

    [SerializeField] private Image[] Select;
    [SerializeField] private GameObject[] Board;
    public void ResetTheSaveBoard()
    {
<<<<<<< HEAD
        Select[0].enabled = true;
=======

        Select[0].enabled = false;
>>>>>>> 156c71daae9569ffc66ccd1a1883560e766a745d
        Select[1].enabled = false;
        Select[2].enabled = false;
        Select[3].enabled = false;
        Select[4].enabled = false;
        Select[5].enabled = false;
        Select[6].enabled = false;
        Board[0].SetActive(false);
        Board[1].SetActive(false);
        Board[2].SetActive(false);
        Board[3].SetActive(false);
        Board[4].GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_save_B", typeof(Sprite)) as Sprite;
        Board[5].GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_load_B", typeof(Sprite)) as Sprite;
    }
}
