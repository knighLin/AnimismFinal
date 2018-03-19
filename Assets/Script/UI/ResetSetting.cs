using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSetting : MonoBehaviour {

    [SerializeField] private GameObject[] Board;
    public void ResetTheSaveBoard()
    {
        Board[0].SetActive(false);
        Board[1].SetActive(false);
        Board[2].SetActive(false);
        Board[3].SetActive(false);
        Board[4].GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_save_B", typeof(Sprite)) as Sprite;
        Board[5].GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_load_B", typeof(Sprite)) as Sprite;
    }
}
