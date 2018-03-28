using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    private TypeValue value;

    private CharacterController characterController;
    private Animator animator;
    private Transform m_Cam;//參考場景中地主相機位置
    private Vector3 m_CamForward;//相機當前面向的位置
    private Vector3 moveDirection;//移動位置
    private Vector3 gravity;
    private float _Speed;//移動速度的乘數
    public LayerMask Ground;
    float m_TurnAmount;//轉向值
    float m_ForwardAmount;//前進值
    bool Jump;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    // Use this for initialization
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        value = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<TypeValue>();
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //if (m_Cam != null)
        //{
        //    //計算相機相對方向移動：
        //    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        //    moveDirection = v * m_CamForward + h * m_Cam.right;
        //}
        //else
        //{
        //    //在沒有主相機的情況下，我們使用世界相對的方向
        //    moveDirection = v * Vector3.forward + h * Vector3.right;
        //}

        moveDirection = transform.TransformDirection(moveDirection);
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {//當按下shift，有跑步動作
            _Speed = value.RunSpeed;
        }
        else
        {
            _Speed = value.MoveSpeed;
        }

        m_TurnAmount = h;
        transform.Rotate(0,m_TurnAmount * 60f* Time.deltaTime,0);

        if (characterController.isGrounded)
        {
            
            Jump = true;
            if (Input.GetKey(KeyCode.Space))
            {
                Jump = false;
                gravity.y = value.JumpPower;
            }
        }
        else
        {
            gravity += Physics.gravity * Time.deltaTime;
        }
        moveDirection = transform.forward * Input.GetAxis("Vertical") ;
        moveDirection += gravity;
        characterController.Move(moveDirection * _Speed * Time.deltaTime);
      
        animator.SetFloat("Speed", characterController.velocity.magnitude, 0.1f, Time.deltaTime);

        
    }

}
//var turn：float = Input.GetAxis（“Horizo​​ntal”）;
//    transform.Rotate（0，turn turnpeed Time.deltaTime，0）;
//    if（controller.isGrounded）{moveDirection = transform.forward Input.GetAxis（“Vertical”） speed; 
//if（Input.GetButton（“Jump”））{moveDirection.y = jumpSpeed; }} //應用重力moveDirection.y - = gravity Time.deltaTime; 