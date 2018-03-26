using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectNewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("L2FixCamera"))
        {
            if (GameObject.Find("NewGame"))
                GameObject.Find("NewGame").GetComponent<Button>().Select();
            else if (GameObject.Find("Return"))
                GameObject.Find("Return").GetComponent<Button>().Select();
        }
    }
}
