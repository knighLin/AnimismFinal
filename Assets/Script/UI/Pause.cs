using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
    
    public GameObject PauseCanvas,Menu;
    public Button Quest;
    public Image FadeIn;
    public float time=0;
    private float CanvasTime=0;
    private Vector3 CanvasDistance= new Vector3 (-650,0,0);
    private bool Fade, CanvasMove,Show;
    private bool IsPause=false;
    private bool ShowCursor = false;

    // Use this for initialization
    void Start () {
        Fade = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Cursor"))
        {
            Debug.Log(ShowCursor);
            ShowCursor = !ShowCursor;
            if (ShowCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;//鎖滑鼠標
                Cursor.visible = false;
            }
        }
        if (CanvasMove)
        {
            if (Show)
            {
                if (CanvasTime < 1)
                {
                    CanvasTime += 0.2f;
                    Menu.transform.localPosition += CanvasDistance / 5;
                }
                else if (CanvasTime >= 1)
                {
                    Time.timeScale = 0f;
                    IsPause = true;
                    CanvasTime = 0;
                    CanvasMove = false;
                }
            }
            else if (!Show)
            {
                if (CanvasTime < 1)
                {
                    CanvasTime += 0.2f;
                    Menu.transform.localPosition -= CanvasDistance / 5;
                }
                else if (CanvasTime >= 1)
                {
                    PauseCanvas.SetActive(false);
                    Time.timeScale = 1;
                    IsPause = false;
                    CanvasTime = 0;
                    CanvasMove = false;
                }
            }
        }
        if (Fade)
        {
            if (time < 1)
            {
                FadeIn.color = new Color(0, 0, 0, 1 - time);
                time += 0.05f;
                if (time >= 1)
                    time = 1;
            }
            else if (time >= 1)
            {
                time = 1;
                Fade = false;
            }
        }
        if (Input.GetButtonDown("OptionsCancel")&& PauseCanvas&& !IsPause)
        {
            if (!IsPause)
                CreatPauseCanvas();
            else
                DestroyPauseCanvas();
        }
        else if (Input.GetButtonDown("CrossJump") && IsPause)
        {
            DestroyPauseCanvas();
        }
    }
    public void CreatPauseCanvas()
    {
        //Instantiate(canvasPrefab, Vector2.zero, Quaternion.identity).name= "PauseCanvas";
        PauseCanvas.SetActive(true);
        Quest.Select();
        Show = true;
        CanvasMove = true;
    }
    public void DestroyPauseCanvas()
    {
        Show = false;
        CanvasMove = true;
    }
}
