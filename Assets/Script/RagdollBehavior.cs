﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBehavior : MonoBehaviour {

    public Rigidbody[] allRigids;
    
    //public Transform Root;

    private void Awake()
    {
        //allRigids = Root.GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool toggle)
    {
        for(int i =0; i < allRigids.Length; i++)
        {
            allRigids[i].isKinematic = !toggle;
        }
    }
}
