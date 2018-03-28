using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private TypeValue value;

    private CharacterController characterController;
    private Animator animator;
    private Transform m_Cam;//參考場景中地主相機位置
    private Vector3 m_CamForward;//相機當前面向的位置
    private Vector3 moveDirection;//移動位置
    private Vector3 gravity;
    private float _Speed;//移動速度的乘數
    //float m_TurnAmount;//轉向值
    //float m_ForwardAmount;//前進值
    bool Jump = false;
    bool OnWalk = false;
    // Use this for initialization
    void Awake()
    {
        value = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<TypeValue>();
    }

    void OnEnable()
    {
        characterController = GetComponent<CharacterController>();
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
        if (m_Cam != null)
        {
            //計算相機相對方向移動：
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            moveDirection = v * m_CamForward + h * m_Cam.right;
            //moveDirection = transform.forward* v + transform.right * h;
        }
        else
        {
            //在沒有主相機的情況下，我們使用世界相對的方向
            moveDirection = v * Vector3.forward + h * Vector3.right;
        }

        moveDirection = transform.TransformDirection(moveDirection);
        
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetButton("R2Run")))//當按下shift，有跑步動作
            _Speed = value.RunSpeed;
        else
            _Speed = value.MoveSpeed;
        

        if (moveDirection.magnitude > 1f)//向量大于1，则变为单位向量
        {
            moveDirection.Normalize();
        }
        moveDirection = transform.InverseTransformDirection(moveDirection);//转换为本地坐标
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, characterController.radius, Vector3.down, out hitInfo,characterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal).normalized;

       // m_TurnAmount = Mathf.Atan2(moveDirection.x, moveDirection.z);//产生一个方位角，即与z轴的夹角，用于人物转向
        
        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDirection );
        if (moveDirection != Vector3.zero)
            OnWalk = true;
        else
            OnWalk = false;

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("CrossJump"))
            {
                Jump = true;

            }
            else
                Jump = false;
        }
        else
        {
            gravity += Physics.gravity * Time.deltaTime;
        }
        moveDirection += gravity;
        characterController.Move(moveDirection * _Speed * Time.deltaTime);
        SetAnimator();
    }

    void SetAnimator()
    {
        animator.SetFloat("Speed", characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        animator.SetBool("OnWalk", OnWalk);
        animator.SetBool("Jump", Jump);
    }

    void ToJump()
    {
        gravity.y = value.JumpPower;
    }

}