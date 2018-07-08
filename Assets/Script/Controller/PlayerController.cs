using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region 技能事件
    public const string SKILL1 = "SKILL1";
    public const string SKILL2 = "SKILL2";
    public const string SKILL3 = "SKILL3";
    #endregion

    #region 動畫事件
    public const string Move = "move";
    public const string ATTACK = "attack";

    #endregion
    Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {
        InitEvent();
    }

    void InitEvent()
    {
        EventHandler.Add(SKILL1, Skill1);
        EventHandler.Add(SKILL2, Skill2);
        EventHandler.Add(SKILL3, Skill3);
    }

    void OnDestroy()
    {
        EventHandler.Remove(SKILL1, Skill1);
        EventHandler.Remove(SKILL2, Skill2);
        EventHandler.Remove(SKILL3, Skill3);
    }

    private void Update()
    {
        // 按下.
        if (Input.GetMouseButtonDown(1))
        {
            Skill1();
        }
    }

    #region EVENT
    void Skill1() 
    {
        if(m_Animator != null){
            m_Animator.SetTrigger(ATTACK);
        }
    }

    void Skill1Fire()
    {
        GameObject bengGo = BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "Bang");
        SkillController skillController = bengGo.AddComponent<SkillController>();
        skillController.Init(transform);
    }


    void Skill2()
    {

    }

    void Skill2Fire()
    {

    }

    void Skill3()
    {

    }

    void Skill3Fire()
    {

    }
    #endregion
}
