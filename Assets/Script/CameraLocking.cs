using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocking : MonoBehaviour
{
    public Quaternion CameraRotation;
    private CameraScript CameraScript;
    private PossessedSystem PossessedSystem;
    public GameObject Player;
    public List<Transform> Animals;
    public int LockingAnimalNumber;

    // Use this for initialization
    void Start () {
        CameraScript = GetComponent<CameraScript>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void LockingAnimals ()
    {
        if (Player == null)//如果第一次點擊鎖定 抓取現在角色可附身的動物
        {
            LockingAnimalNumber = 0;
            Animals = new List<Transform> { };
            if (GameObject.FindWithTag("Player"))
            {
                Player = GameObject.FindWithTag("Player");
                PossessedSystem = GameObject.FindWithTag("Player").GetComponent<PossessedSystem>();
            }
            if (PossessedSystem.RangeObject.Count > 0)
            {
                for (int i = 0; i < PossessedSystem.RangeObject.Count; i++)
                {
                    Animals.Add(PossessedSystem.RangeObject[i].transform);
                }
            }
              
        }
        else if (LockingAnimalNumber == PossessedSystem.RangeObject.Count-1)
            LockingAnimalNumber = 0;
        else
            LockingAnimalNumber += 1;
        //Player.transform.LookAt(Animals[LockingAnimalNumber]);
        Vector3 PlayerForward = new Vector3 ((Animals[LockingAnimalNumber].transform.position.x - Player.transform.position.x),0,(Animals[LockingAnimalNumber].transform.position.z - Player.transform.position.z));
        Quaternion PlayerRotation = Quaternion.LookRotation(PlayerForward);
        Player.transform.rotation = PlayerRotation;
        Camera.main.transform.rotation = PlayerRotation;
        Camera.main.transform.position = PlayerRotation * (CameraScript.Normal003.transform.localPosition + new Vector3(0, 0.2f, 3)) + CameraScript.MoveEnd.transform.position;
        Vector3 CameraFoward = Animals[LockingAnimalNumber].transform.position - Camera.main.transform.position;
        CameraRotation = Quaternion.LookRotation(CameraFoward);
    }
    
}
