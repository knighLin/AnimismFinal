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
    public int LockingEnemyNumber;
    public float Distance;

    // Use this for initialization
    void Start () {
        CameraScript = GetComponent<CameraScript>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void LockingEnemy()
    {
        if (Player == null)//如果第一次點擊鎖定 抓取現在角色可附身的動物
        {
            LockingEnemyNumber = 0;
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
                    //Debug.Log(Vector3.Dot(Vector3.Normalize(Camera.main.transform.position), Vector3.Normalize(Animals[i].transform.position)));
                    //if (Distance < Vector3.Dot(Vector3.Normalize(Camera.main.transform.position), Vector3.Normalize(Animals[i].transform.position)))
                    //{
                    //    Distance = Vector3.Dot(Vector3.Normalize(Camera.main.transform.position), Vector3.Normalize(Animals[i].transform.position));
                    //    LockingAnimalNumber = i;
                    //}

                }
            }

        }
        else if (LockingAnimalNumber == PossessedSystem.RangeObject.Count - 1)
            LockingAnimalNumber = 0;
        else
            LockingAnimalNumber += 1;
        Vector3 PlayerForward = new Vector3((Animals[LockingAnimalNumber].transform.position.x - Player.transform.position.x), 0, (Animals[LockingAnimalNumber].transform.position.z - Player.transform.position.z));
        Quaternion PlayerRotation = Quaternion.LookRotation(PlayerForward);
        Player.transform.rotation = PlayerRotation;
        Camera.main.transform.rotation = PlayerRotation;
        Camera.main.transform.position = CameraScript.MoveEnd.transform.position;
        Vector3 CameraFoward = Animals[LockingAnimalNumber].transform.position - Camera.main.transform.position;
        CameraRotation = Quaternion.LookRotation(CameraFoward);
    }
    public void LockingAnimals ()
    {
        if (Player == null)//如果第一次點擊鎖定 抓取現在角色可附身的動物
        {
            Distance = 0;
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
                    //Debug.Log(Vector3.Dot(Vector3.Normalize(Camera.main.transform.position), Vector3.Normalize(Animals[i].transform.position)));
                    //if (Distance < Vector3.Dot(Vector3.Normalize(Camera.main.transform.position), Vector3.Normalize(Animals[i].transform.position)))
                    //{
                    //    Distance = Vector3.Dot(Vector3.Normalize(Camera.main.transform.position), Vector3.Normalize(Animals[i].transform.position));
                    //    LockingAnimalNumber = i;
                    //}
                    
                }
            }
              
        }
        else if (LockingAnimalNumber == PossessedSystem.RangeObject.Count-1)
            LockingAnimalNumber = 0;
        else
            LockingAnimalNumber += 1;
        Vector3 PlayerForward = new Vector3 ((Animals[LockingAnimalNumber].transform.position.x - Player.transform.position.x),0,(Animals[LockingAnimalNumber].transform.position.z - Player.transform.position.z));
        Quaternion PlayerRotation = Quaternion.LookRotation(PlayerForward);
        Player.transform.rotation = PlayerRotation;
        Camera.main.transform.rotation = PlayerRotation;
        Camera.main.transform.position = CameraScript.MoveEnd.transform.position;
        Vector3 CameraFoward = Animals[LockingAnimalNumber].transform.position - Camera.main.transform.position;
        CameraRotation = Quaternion.LookRotation(CameraFoward);
    }
    
}
