using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSaveBoard : MonoBehaviour {

    [SerializeField] private Image[] Select;
    [SerializeField] private GameObject[] Board;
    public void ResetTheSaveBoard()
    {
        Select[0].enabled = true;
        Select[1].enabled = false;
        Select[2].enabled = false;
        Select[3].enabled = false;
        Select[4].enabled = false;
        Select[5].enabled = false;
        Select[6].enabled = false;
        Board[0].SetActive(false);
        Board[1].SetActive(false);
        Board[2].SetActive(false);

    }
}
