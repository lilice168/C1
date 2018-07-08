using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{

    const string Move = "move";

    public Transform m_StartPoint;
    public Transform m_EndPoint;

    Animator m_Animator;
    Rigidbody m_Rigidbody;

    // 移動
    public Vector3 m_Move;
    bool m_IsMove;
    Vector3 m_BeginPostion;

    // 面向.
    bool m_FaceToRight;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
       
    }

    void Start()
    {
        if(m_StartPoint == null){
            m_StartPoint = BattleManager.m_Instance.m_StartPoint.transform;
        }

        if (m_StartPoint != null) {
            transform.position = m_StartPoint.position;
            m_Move = Vector3.zero;
            m_FaceToRight = true;
        }
    }


    void FixedUpdate()
    {

        m_IsMove = m_Animator.GetBool(Move);
        if (m_IsMove) {
            transform.position += m_Move;
        }

        if (m_EndPoint != null) {
            if (transform.position.x >= m_EndPoint.position.x) {
                m_Animator.SetBool(Move, false);
            }
        }

        // 決定左右上下.
        if (m_IsMove)
        {
            // right
            if (Input.mousePosition.x - m_BeginPostion.x > 0)
            {
                m_Move = new Vector3(0.03f, 0.0f, 0.0f);
                ToFace(true);

            }
            else if (Input.mousePosition.x - m_BeginPostion.x < 0)
            {
                m_Move = new Vector3(-0.03f, 0.0f, 0.0f);
                ToFace(false);
            }
        }
    }

    #region 控制移動區塊
    void Update()
    {
        // 按下.
        if (Input.GetMouseButtonDown(0)) { 
            m_Animator.SetBool(Move, true);
            m_BeginPostion = Input.mousePosition;
        }

        // 放開.
        if (Input.GetMouseButtonUp(0)) {
            m_Animator.SetBool(Move, false);
            m_Move = Vector3.zero;
            m_BeginPostion = Vector3.zero;
        }
       
	}

    // 控制面向
    void ToFace(bool toRight)
    {
        if(!m_FaceToRight.Equals(toRight)){ // 轉向.
            if(m_FaceToRight){ // 向左轉.
                transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }else{ // 向右轉.
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            m_BeginPostion = Input.mousePosition;
        }
        m_FaceToRight = toRight;
    }


	#endregion

	#region 碰撞區塊
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == VerDef.Tag_Block) {
            m_Rigidbody.constraints = VerDef.MoveX;
        }
    }
    #endregion
}
