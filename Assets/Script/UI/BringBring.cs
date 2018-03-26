using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BringBring : MonoBehaviour {
    private float abc=0;
    [SerializeField]private int pqr;
    private bool def=true;

    [SerializeField] private Image ghi;
    [SerializeField] private GameObject jkl;
    [SerializeField] private Button mno;


    void Update () {
        if (def)
        {
            abc += Time.timeScale;
            ghi.color = new Color(1, 1, 1, abc / pqr);
        }
        else
        {
            abc -= Time.timeScale;
            ghi.color = new Color(1, 1, 1, abc / pqr);
        }
        if (abc >= pqr)
        {
            def = false;
        }
        else if (abc<=0)
        {
            def = true;
        }

        if (name=="Press"&&(Input.GetButtonDown("SquareAttack")|| Input.GetButtonDown("TriangleAbility")|| Input.GetButtonDown("CircleUnpossess")|| Input.GetButtonDown("CrossJump")|| Input.GetButtonDown("R1Locking")|| Input.GetButtonDown("OptionsCancel") || Input.GetButtonDown("R1Locking")|| Input.GetButtonDown("R2Run")|| Input.GetButtonDown("L1SoulVison")|| Input.GetButtonDown("L2FixCamera")|| Input.GetButtonDown("Cursor")))
        {
            jkl.SetActive(true);
            mno.Select();
            this.gameObject.SetActive(false);
        }

	}
}
