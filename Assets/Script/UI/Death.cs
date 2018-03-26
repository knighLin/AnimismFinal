using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{

    public GameObject DeathCanvas;
    public Button ReturnHomepage;
    public bool IsDeath;
    // Update is called once per frame
    void Update()
    {
        if (DeathCanvas&& IsDeath)
        {
            CreatPauseCanvas();
        }
    }
    public void CreatPauseCanvas()
    {
        //Instantiate(canvasPrefab, Vector2.zero, Quaternion.identity).name= "PauseCanvas";
        DeathCanvas.SetActive(true);
        ReturnHomepage.Select();
    }
}
