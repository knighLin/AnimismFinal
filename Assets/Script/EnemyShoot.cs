using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public Rigidbody bullet;
	[SerializeField]
	private float force = 10;
    private Rigidbody newBullet;
   
    private AudioSource audioSource;
    public AudioClip GunBang;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shooting(Transform Target)
    {
        transform.LookAt(Target);
        newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(GunBang);
        newBullet.AddForce((Target.position - transform.position+new Vector3(0,0.5f,0)).normalized * force);
    }
    
}
