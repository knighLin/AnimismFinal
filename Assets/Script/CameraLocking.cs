using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocking : MonoBehaviour
{
    private CameraScript CameraScript;
    private PossessedSystem PossessedSystem;
    public GameObject Player;
    public List<Transform> Animals;
    public int LockingAnimalNumber=0;

    // Use this for initialization
    void Start () {
        CameraScript = GetComponent<CameraScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("CircleLockingAnimal") && CameraScript.CanLockAnimal)
        {
            LockingAnimals();
        }
        else if (Player != null&& !CameraScript.CanLockAnimal)
            Player = null;


    }
    public void LockingAnimals ()
    {
        if (Player == null)//如果第一次點擊鎖定 抓取現在角色可附身的動物
        {
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
                    Animals.Add( PossessedSystem.RangeObject[i].transform);
                }
            }
        }
        else if (LockingAnimalNumber == PossessedSystem.RangeObject.Count-1)
            LockingAnimalNumber = 0;
        else
            LockingAnimalNumber += 1;
        Vector3 targetDir = Animals[LockingAnimalNumber].position - Player.transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0, 0);
        Player.transform.rotation = Quaternion.LookRotation(newDir);

    }
    
}
