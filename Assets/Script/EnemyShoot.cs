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
        newBullet.AddForce((Target.position +Vector3.up - transform.position).normalized * force);
    }
    
}
