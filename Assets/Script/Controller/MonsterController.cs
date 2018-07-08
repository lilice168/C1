using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct MoveData
{
    public Vector3 m_Move;
    public bool m_ToRight;
}

public class MonsterController : MonoBehaviour
{
    #region 移動事件
    public const string MOVE = "MOVE";
    #endregion

    #region 技能事件
    public const string SKILL1 = "SKILL1";
    public const string SKILL2 = "SKILL2";
    public const string SKILL3 = "SKILL3";
    #endregion

    #region 出生/死亡事件
    public const string BORN = "BORN";
    public const string DEATH = "DEATH";
    #endregion

    #region 動畫事件
    public const string RUN = "run";
    public const string ATTACK = "attack";
    #endregion


    AI m_AI;
    Animator m_Animator;
    DissovleController m_DissovleController;

    Transform m_MonsterPoint;

    bool m_FaceToRight;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_DissovleController = GetComponent<DissovleController>();
    }

    void Start()
    {
        InitEvent();
        m_AI = new AIIdle(AI.AIIndex.EAction1);
        m_FaceToRight = false;

        if (m_MonsterPoint == null)
        {
            m_MonsterPoint = BattleManager.m_Instance.m_MonsterPoint.transform;
        }

        if (m_MonsterPoint != null)
        {
            transform.position = m_MonsterPoint.position;
        }

        Born();
    }

    void InitEvent()
    {
        EventHandler.Add<MoveData>(MOVE, Move);
        EventHandler.Add(SKILL1, Skill1);
        EventHandler.Add(SKILL2, Skill2);
        EventHandler.Add(SKILL3, Skill3);

        EventHandler.Add(BORN, Born);
        EventHandler.Add(DEATH, Die);
    }

    void OnDestroy()
    {
        EventHandler.Remove(SKILL1, Skill1);
        EventHandler.Remove(SKILL2, Skill2);
        EventHandler.Remove(SKILL3, Skill3);

        EventHandler.Remove(BORN, Born);
        EventHandler.Remove(DEATH, Die);
    }

    #region Update
    private void Update()
    {
        m_AI.Update();
    }
    #endregion

    #region Move
    void Move(MoveData _MoveData)
    {
        if (_MoveData.m_ToRight)
        {
            if (!m_FaceToRight)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

        }
        else
        {
            if (m_FaceToRight)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        m_FaceToRight = _MoveData.m_ToRight;

        transform.position += _MoveData.m_Move;
    }
    #endregion

    #region Skill
    void Skill1()
    {
        if (m_Animator != null)
        {
            m_Animator.SetTrigger(ATTACK);
        }

    }

    void Skill2()
    {

    }

    void Skill3()
    {

    }
    #endregion

    #region born/die
    void Born()
    {
        m_DissovleController.ShowOn();
    }

    void Die()
    {
        m_DissovleController.ShowOff();
    }
    #endregion

}
