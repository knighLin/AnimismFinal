using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressCrossCancel : MonoBehaviour {
    [SerializeField] private GameObject LastCanvas, LastButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("CrossJump"))
        {
            if (name == "QuestionBoard")
            {
                LastCanvas.SetActive(true);
                LastButton.GetComponent<Button>().Select();
                this.gameObject.SetActive(false);
            }
        }
    }
}
