using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomepageReturn : MonoBehaviour {
    [SerializeField] private GameObject Menu,NewGame;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("CrossJump"))
        {
            Menu.SetActive(true);
            NewGame.GetComponent<Button>().Select();
            this.gameObject.SetActive(false);
        }
	}
}
