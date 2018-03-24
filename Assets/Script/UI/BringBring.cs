using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BringBring : MonoBehaviour {
    private float abc;
    private bool def=true;
    [SerializeField] private Image ghi;
    [SerializeField] private GameObject jkl;
    [SerializeField] private Button mno;


    void Update () {
        if (def)
        {
            abc += Time.timeScale;
            ghi.color = new Color(1, 1, 1, abc / 40);
        }
        else
        {
            abc -= Time.timeScale;
            ghi.color = new Color(1, 1, 1, abc / 40);
        }
        if (abc >=40)
        {
            def = false;
        }
        else if (abc<=0)
        {
            def = true;
        }

        if (Input.GetButtonDown("SquareAttack")|| Input.GetButtonDown("TriangleAbility")|| Input.GetButtonDown("CircleUnpossess")|| Input.GetButtonDown("CrossJump")|| Input.GetButtonDown("R1Locking")|| Input.GetButtonDown("OptionsCacel")|| Input.GetButtonDown("R1Locking")|| Input.GetButtonDown("R2Run")|| Input.GetButtonDown("L1SoulVison")|| Input.GetButtonDown("R2FixCamera"))
        {
            jkl.SetActive(true);
            mno.Select();
            this.gameObject.SetActive(false);
        }

	}
}
