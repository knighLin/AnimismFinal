using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip attack;

   
    void Awake()
    {
       
        audioSource = GetComponent<AudioSource>();
       
    }

    void WeaponSound()
    {
        audioSource.PlayOneShot(attack);

    }
}
